﻿using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Liyanjie.Jsonql.Core;

namespace Liyanjie.Jsonql.DynamicInclude
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonqlIncluder : IJsonqlIncluder
    {
        static readonly MethodInfo includeMethodInfo = typeof(QueryableExtensions)
              .GetTypeInfo().GetDeclaredMethods("Include")
              .Single(mi => mi.GetParameters().Any(pi => pi.Name == "navigationPropertyPath" && pi.ParameterType != typeof(string)));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="navigationPropertyPath"></param>
        /// <returns></returns>
        public IQueryable Include(IQueryable queryable, params string[] navigationPropertyPath)
        {
            foreach (var path in navigationPropertyPath)
            {
                var parameter = Expression.Parameter(queryable.ElementType);
                var propertyOrField = Expression.PropertyOrField(parameter, path);
                var lambda = Expression.Lambda(propertyOrField, parameter);
                var expression = Expression.Call(
                    instance: null,
                    method: includeMethodInfo.MakeGenericMethod(queryable.ElementType, propertyOrField.Type),
                    arguments: new[] { queryable.Expression, Expression.Quote(lambda) }
                );
                queryable = queryable.Provider.CreateQuery(expression);
            }
            return queryable;
        }
    }
}
