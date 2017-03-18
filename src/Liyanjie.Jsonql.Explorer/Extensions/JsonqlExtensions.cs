using Liyanjie.Jsonql.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Liyanjie.Jsonql.Explorer
{
    internal static class JsonqlExtensions
    {
        public static IEnumerable<JsonqlProperty> JsonqlProperties(this Type type)
        {
            return type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .OrderBy(_ => _.Name)
                .Select(_ => new JsonqlProperty
                {
                    Name = _.Name.ToCamelCase(),
                    Type = _.PropertyType.JsonqlType(),
                })
                .ToList();
        }

        public static IEnumerable<JsonqlMethod> JsonqlMethods(this Type type)
        {
            return type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .OrderBy(_ => _.Name)
                .Select(_ => new JsonqlMethod
                {
                    Name = _.Name.ToCamelCase(),
                    ReturnType = _.ReturnType.JsonqlType(),
                    Parameters = _.JsonqlParameters(),
                })
                .ToList();
        }

        private static List<JsonqlParameter> JsonqlParameters(this MethodInfo method)
        {
            return method.GetParameters()
                .Select(_ => new JsonqlParameter
                {
                    Name = _.Name.ToCamelCase(),
                    Type = _.ParameterType.JsonqlType(),
                    IsOptional = _.IsOptional,
                })
                .ToList();
        }

        public static JsonqlType JsonqlType(this Type type)
        {
            var array = string.Empty;
            if (type.IsArray)
            {
                array = "[]";
                type = type.GetElementType();
            }
            var @return = new JsonqlType();
            if (type == typeof(Resource))
                @return.Name = nameof(Resource);
            else if ("Nullable`1".Equals(type.Name))
            {
                var flag = type.GenericTypeArguments[0].Name.JsonqlTypeName();
                @return.Name = $"{flag.Key}?{array}";
                @return.IsBaseType = flag.Value;
            }
            else if ("IList`1".Equals(type.Name) || "ICollection`1".Equals(type.Name) || "IEnumerable`1".Equals(type.Name))
            {
                var flag = type.GenericTypeArguments[0].Name.JsonqlTypeName();
                @return.Name = $"{flag.Key}[]{array}";
                @return.IsBaseType = flag.Value;
            }
            else
            {
                var flag = type.Name.JsonqlTypeName();
                @return.Name = $"{flag.Key}{array}";
                @return.IsBaseType = flag.Value;
            }
            return @return;
        }

        public static KeyValuePair<string, bool> JsonqlTypeName(this string typeName)
        {
            var key = string.Empty;
            var value = false;
            switch (typeName.ToLower())
            {
                case "int16":
                    key = "short";
                    value = true;
                    break;
                case "uint16":
                    key = "ushort";
                    value = true;
                    break;
                case "int32":
                    key = "int";
                    value = true;
                    break;
                case "uint32":
                    key = "uint";
                    value = true;
                    break;
                case "int64":
                    key = "long";
                    value = true;
                    break;
                case "uint64":
                    key = "ulong";
                    value = true;
                    break;
                case "string":
                case "boolean":
                case "double":
                case "decimal":
                case "float":
                case "byte":
                case "sbyte":
                case "object":
                    key = typeName.ToLower();
                    value = true;
                    break;
                default:
                    key = typeName;
                    value = false;
                    break;
            }
            return new KeyValuePair<string, bool>(key, value);
        }

        public static string ToCamelCase(this string input)
        {
            if (input == null)
                return null;

            if (input.Length < 2)
                return input.ToLower();

            var i = 0;
            var find = false;

            for (; i < input.Length; i++)
            {
                var value = (int)input[i];
                if (value < 65 || value > 90)
                {
                    find = true;
                    break;
                }
            }

            if (!find)
                i++;

            if (i < 1)
                return input;
            else if (i == 1)
                return $"{input.Substring(0, 1).ToLower()}{input.Substring(1)}";
            else
                return $"{input.Substring(0, i - 1).ToLower()}{input.Substring(i - 1)}";
        }
    }
}
