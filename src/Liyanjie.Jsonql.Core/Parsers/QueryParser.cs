using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Liyanjie.Jsonql.Core.Parsers
{
    internal sealed class QueryParser : IDisposable
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
            foreach (var c in query)
            {
                if (c == '\'')
                {
                    queue.Enqueue(c);
                    if (!quota2)
                        quota1 = !quota1;
                }
                if (c == '"')
                {
                    queue.Enqueue(c);
                    if (!quota1)
                        quota2 = !quota2;
                }
                else if (c == '@' || c == '#')
                {
                    if (quota1 || quota2)
                        queue.Enqueue(c);
                    else
                        throw new Exception("Parse query error!");
                }
                else if (c == 9 || c == 10 || c == 13 || c == 32)
                {
                    if (quota1 || quota2)
                        queue.Enqueue(c);
                }
                else if (c < 32)
                {
                    if (quota1 || quota2)
                        queue.Enqueue(c);
                    else
                        throw new Exception("Parse query error!");
                }
                else
                    queue.Enqueue(c);
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
            templates?.Clear();
        }

        public IDictionary<string, string> Resources => resources;

        public IDictionary<string, string> Expressions => expressions;

        public IDictionary<string, string> Templates => templates;

        public string Entry => entry;
    }
}
