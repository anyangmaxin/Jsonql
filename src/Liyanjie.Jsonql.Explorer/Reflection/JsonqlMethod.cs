using System.Collections.Generic;

namespace Liyanjie.Jsonql.Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonqlMethod
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JsonqlType ReturnType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<JsonqlParameter> Parameters { get; set; }
    }
}
