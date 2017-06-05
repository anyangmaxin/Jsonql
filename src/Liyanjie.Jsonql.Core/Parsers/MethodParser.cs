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
    class MethodParser
    {
        readonly string template;

        public MethodParser(string template)
        {
            this.template = template;
        }

        public KeyValuePair<string, KeyValuePair<string, string[]>> Parse()
        {
            if (!Regex.IsMatch(template, @"^[a-zA-Z_]\w*\("))
                throw new Exception("Parse method call error!");

            var name = template.Substring(0, template.IndexOf('('));
            var param = template.Substring(template.IndexOf('('));

            var matches = Regex.Matches(param, @"[\+\-\*\/\=\>\<\&\^\|\!\%\(]\$[a-zA-Z1-9_]+");
            var parameters = new List<string>();
            foreach (var item in matches)
            {
                var parameter = item.ToString().Substring(1);
                param = param.Replace(parameter, $"@{parameters.Count}");
                if (!parameters.Contains(parameter))
                    parameters.Add(parameter);
            }

            return new KeyValuePair<string, KeyValuePair<string, string[]>>(name, new KeyValuePair<string, string[]>(param.TrimStart('(').TrimEnd(')'), parameters.ToArray()));
        }
    }
}
