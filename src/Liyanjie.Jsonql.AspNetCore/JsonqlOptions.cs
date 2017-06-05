using System;
using Liyanjie.Jsonql.Core;
using Microsoft.AspNetCore.Http;

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
        public IJsonqlIncluder JsonqlIncluder { internal get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IJsonqlEvaluator JsonqlEvaluator { internal get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IJsonqlLinqer JsonqlLinqer { internal get; set; }
    }
}
