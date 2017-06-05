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
    class SelectParser
    {
        readonly IDictionary<string, string> templates;

        public SelectParser(IDictionary<string, string> templates)
        {
            this.templates = templates;
        }

        public string Parse(string template)
        {
            return parse(template).ToString();
        }

        private Object parse(string template)
        {
            var @object = new Object();

            if (template.StartsWith("#"))
            {
                template = templates[template];
                var properties = template.TrimStart('{').TrimEnd('}').Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);//.Where(_ => _.IndexOf("=>") < 0);
                foreach (var property in properties)
                {
                    if (property.IndexOf(':') > -1)
                    {
                        var tmp_property = property;
                        if (property.IndexOf("=>") > -1)
                            tmp_property = property.Substring(0, property.IndexOf("=>"));
                        var namedProperty = tmp_property.Split(':');
                        if (namedProperty[1].StartsWith("$."))
                        {
                            var segment = namedProperty[1].Substring(2);
                            if (segment.IndexOf('.') > 0)
                                @object.AddProperty(parse(segment.Split('.')));
                            else
                                @object.AddProperty(new Property
                                {
                                    Name = segment,
                                });
                        }
                        else if (namedProperty[1].StartsWith("#"))
                            @object.AddProperty(parse(namedProperty[1]).Properties.ToArray());
                        else
                            continue;
                    }
                    else
                        @object.AddProperty(new Property
                        {
                            Name = property,
                        });
                }
            }
            else if (template.StartsWith("$."))
            {
                var segment = template.Substring(2);
                if (segment.IndexOf('.') > 0)
                    @object.AddProperty(parse(segment.Split('.')));
                else
                    @object.AddProperty(new Property
                    {
                        Name = segment,
                    });
            }

            return @object;
        }

        private Property parse(string[] segments, int index = 0)
        {
            if (index < segments.Length - 1)
                return new Property
                {
                    Name = segments[index],
                    Value = new Object().AddProperty(parse(segments, ++index)),
                };
            else
                return new Property
                {
                    Name = string.Join(".", segments)
                };
        }

        private class Property
        {
            public string Name { get; set; }
            public Object Value { get; set; }
        }

        private class Object
        {
            internal IList<Property> Properties { get; set; } = new List<Property>();

            public Object AddProperty(params Property[] properties)
            {
                foreach (var property in properties)
                {
                    var find = Properties.FirstOrDefault(_ => _.Name == property.Name);
                    if (find == null)
                        Properties.Add(property);
                    else if (property.Value != null)
                        (find.Value ?? (find.Value = new Object())).AddProperty(property.Value.Properties.ToArray());
                }
                return this;
            }

            public override string ToString()
            {
                if (Properties.Count > 0)
                    return $"new({string.Join(",", Properties.Select(_ => _.Value == null ? _.Name : $"{_.Value.ToString()} as {_.Name}"))})";
                return null;
            }
        }
    }
}
