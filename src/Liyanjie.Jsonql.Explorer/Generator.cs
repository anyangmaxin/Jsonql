using Liyanjie.Jsonql.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Liyanjie.Jsonql.Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public class Generator
    {
        readonly string nameOfResource = nameof(Resource);
        readonly string nameOfResource_Ordered = nameof(Resource_Ordered);

        readonly IDictionary<string, Type> types;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceTable"></param>
        public Generator(ResourceTable resourceTable)
        {
            types = resourceTable.ResourceTypes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonqlSchema Generate()
        {
            return new JsonqlSchema
            {
                ResourceInfos = getResourceInfos(),
                ResourceTypes = getResourceTypes(),
                ResourceMethods = getResourceMethods(),
            };
        }

        IEnumerable<JsonqlResource> getResourceInfos()
        {
            return types.OrderBy(_ => _.Key).Select(_ => new JsonqlResource
            {
                Name = _.Key,
                Type = _.Value.JsonqlType(),
            }).ToList();
        }

        IEnumerable<JsonqlClass> getResourceTypes()
        {
            return types.OrderBy(_ => _.Value.Name).Select(_ => new JsonqlClass
            {
                Name = _.Value.Name,
                Properties = _.Value.JsonqlProperties(),
            }).ToList();
        }

        IEnumerable<JsonqlClass> getResourceMethods()
        {
            return new List<JsonqlClass>
            {
                new JsonqlClass
                {
                    Name = nameOfResource,
                    Methods = typeof(Resource).JsonqlMethods(),
                },
                new JsonqlClass
               {
                    Name = nameOfResource_Ordered,
                    Inherit = nameOfResource,
                    Methods = typeof(Resource_Ordered).JsonqlMethods(),
                },
            };
        }

        /// <summary>
        /// 
        /// </summary>
        ~Generator()
        {
            types?.Clear();
        }
    }
}
