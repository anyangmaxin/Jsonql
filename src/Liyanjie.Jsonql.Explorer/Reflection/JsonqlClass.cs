using System.Collections.Generic;

namespace Liyanjie.Jsonql.Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonqlClass
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Inherit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<JsonqlProperty> Properties { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<JsonqlMethod> Methods { get; set; }
    }
}
