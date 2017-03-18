using System.Collections.Generic;

namespace Liyanjie.Jsonql.Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonqlSchema
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<JsonqlResource> ResourceInfos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<JsonqlClass> ResourceTypes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<JsonqlClass> ResourceMethods { get; set; }
    }
}
