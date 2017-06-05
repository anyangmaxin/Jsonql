using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Liyanjie.Jsonql.Core.Parsers
{
#if DEBUG
    public
#else
    internal sealed
#endif
    class QueryParser : IDisposable
    {
        const string regex_Resource = @"[a-zA-Z_]\w*\[\]";
        const string regex_Expression = @"{{[^{}]*}}";
        const string regex_Template = @"{[^{}]*}";
        readonly Dictionary<string, string> resources = new Dictionary<string, string>();
        readonly Dictionary<string, string> expressions = new Dictionary<string, string>();
        readonly Dictionary<string, string> templates = new Dictionary<string, string>();
        readonly string entry;

        public QueryParser(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(nameof(query));

            var queue = new Queue<char>();
            var quota1 = false;
            var quota2 = false;
            foreach (var @char in query)
            {
                if (@char == '\'')
                {
                    queue.Enqueue(@char);
                    if (!quota2)
                        quota1 = !quota1;
                }
                if (@char == '"')
                {
                    queue.Enqueue(@char);
                    if (!quota1)
                        quota2 = !quota2;
                }
                else if (@char == '@' || @char == '#')
                {
                    if (quota1 || quota2)
                        queue.Enqueue(@char);
                    else
                        throw new Exception("Parse query error!");
                }
                else if (@char == 9 || @char == 10 || @char == 13 || @char == 32)
                {
                    if (quota1 || quota2)
                        queue.Enqueue(@char);
                }
                else if (@char < 32)
                {
                    if (quota1 || quota2)
                        queue.Enqueue(@char);
                    else
                        throw new Exception("Parse query error!");
                }
                else
                    queue.Enqueue(@char);
            }

            var @string = string.Join(string.Empty, queue.ToArray());
            {
                foreach (var item in Regex.Matches(@string, regex_Resource))
                {
                    var match = item as Match;
                    var value = match.Value;
                    if (!resources.ContainsValue(value))
                    {
                        var key = $"@{resources.Count}";
                        resources.Add(key, value);
                        @string = @string.Replace(value, key);
                    }
                }
            }
            {
                foreach (var item in Regex.Matches(@string, regex_Expression))
                {
                    var match = item as Match;
                    var value = match.Value;
                    if (!expressions.ContainsValue(value))
                    {
                        var key = $"`{expressions.Count}";
                        expressions.Add(key, value);
                        @string = @string.Replace(value, key);
                    }
                }
            }

            do
            {
                foreach (var item in Regex.Matches(@string, regex_Template))
                {
                    var match = item as Match;
                    var value = match.Value;
                    if (!templates.ContainsValue(value))
                    {
                        var key = $"#{templates.Count}";
                        templates.Add(key, value);
                        @string = @string.Replace(value, key);
                    }
                }
            } while (Regex.IsMatch(@string, regex_Template));

            entry = @string;
        }

        public void Dispose()
        {
            resources?.Clear();
            expressions?.Clear();
            templates?.Clear();
        }

        public IDictionary<string, string> Resources => resources;

        public IDictionary<string, string> Expressions => expressions;

        public IDictionary<string, string> Templates => templates;

        public string Entry => entry;
    }
}
