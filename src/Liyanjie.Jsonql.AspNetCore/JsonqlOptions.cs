using Liyanjie.Jsonql.Core;
using Microsoft.AspNetCore.Http;
using System;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonqlOptions
    {
        private ResourceTable resources = new ResourceTable();

        /// <summary>
        /// 资源表
        /// </summary>
        public ResourceTable Resources => resources;

        /// <summary>
        /// 授权验证
        /// </summary>
        public Func<HttpContext, bool> Authorize { get; set; } = context => true;

        /// <summary>
        /// 查找查询
        /// </summary>
        public Func<HttpRequest, string> FindQuery { internal get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IDynamicEvaluator DynamicEvaluator { internal get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IDynamicLinq DynamicLinq { internal get; set; }
    }
}
