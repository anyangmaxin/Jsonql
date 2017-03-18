using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Liyanjie.Jsonql.Core.Internals
{
    internal static class Extensions
    {
        public static object GetValue(this object @object, string template)
        {
            if (@object == null || string.IsNullOrEmpty(template))
                return null;

            object value = null;
            string temp = null;
            string code = null;

            var dot = template.IndexOf('.');
            if (dot > 0)
            {
                temp = template.Substring(dot + 1);
                code = template.Substring(0, dot);
            }
            else
                code = template;

            var type = @object.GetType();
            if (Regex.IsMatch(code, @"^\w+\(\S*\)$"))
            {
                var name = code.Substring(0, code.IndexOf('('));
                var parameters = code.Substring(code.IndexOf('(')).TrimStart('(').TrimEnd(')').Split(',');

                var method = type.GetMethod(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase);
                if (method != null)
                    value = method.Invoke(@object, parameters);
            }
            else
            {
                var name = code;

                var property = type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property != null)
                    value = property.GetValue(@object);
            }

            if (value == null)
                return null;
            else if (temp != null)
                return value.GetValue(temp);
            else
                return value;
        }

        public static IDictionary<TKey, TValue> Copy<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            var output = new Dictionary<TKey, TValue>();
            if (source != null && source.Count > 0)
                foreach (var key in source.Keys)
                {
                    output.Add(key, source[key]);
                }
            return output;
        }
    }
}
