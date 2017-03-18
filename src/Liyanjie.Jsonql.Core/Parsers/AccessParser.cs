using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Liyanjie.Jsonql.Core.Parsers
{
    internal sealed class AccessParser : IDisposable
    {
        string pattern = @"\([^\(\)]*\)";
        string template;
        IDictionary<string, string> segments = new Dictionary<string, string>();

        public AccessParser(string template)
        {
            this.template = template;
        }

        public string[] Parse()
        {
            while (Regex.IsMatch(template, pattern))
            {
                foreach (var item in Regex.Matches(template, pattern))
                {
                    var segment = item.ToString();
                    var key = $"`{segments.Count}";
                    template = template.Replace(segment, key);
                    segments.Add(key, segment);
                }
            }
            var result = template.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < result.Length; i++)
            {
                var segment = result[i];
                foreach (var key in segments.Keys.Reverse())
                {
                    segment = segment.Replace(key, segments[key]);
                }
                result[i] = segment;
            }

            return result;
        }

        public void Dispose()
        {
            segments.Clear();
            segments = null;
        }
    }
}
