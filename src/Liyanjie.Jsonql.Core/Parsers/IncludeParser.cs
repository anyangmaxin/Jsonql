using System;
using System.Collections.Generic;
using System.Linq;

namespace Liyanjie.Jsonql.Core.Parsers
{
#if DEBUG
    public
#else
    internal sealed
#endif
    class IncludeParser
    {
        readonly IDictionary<string, string> templates;
        string template;
        public IncludeParser(string template, IDictionary<string, string> templates)
        {
            this.template = template ?? throw new ArgumentNullException(nameof(template));
            this.templates = templates ?? new Dictionary<string, string>();
        }

        public string[] Parse()
        {
            return parse(template).Distinct().ToArray();
        }

        private IList<string> parse(string template)
        {
            var paths = new List<string>();

            if (template.StartsWith("#"))
            {
                template = templates[template.TrimEnd(']').TrimEnd('[')];
                var properties = template.TrimStart('{').TrimEnd('}').Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var property in properties)
                {
                    if (property.IndexOf("$.") > -1)
                        paths.AddRange(parse(property.Substring(property.IndexOf("$."))));
                    else if (property.IndexOf('#') > -1)
                        paths.AddRange(parse(property.Substring(property.IndexOf('#'))));
                }
            }
            else if (template.StartsWith("$."))
            {
                var @string = template.Substring(template.IndexOf("$."));
                if (@string.IndexOf("=>") > -1)
                    paths.Add(@string.Substring(0, @string.IndexOf("=>")).Substring(2));
                else if (@string.LastIndexOf('.') > 1)
                    paths.Add(@string.Substring(0, @string.LastIndexOf('.')).Substring(2));
            }

            return paths;
        }
    }
}
