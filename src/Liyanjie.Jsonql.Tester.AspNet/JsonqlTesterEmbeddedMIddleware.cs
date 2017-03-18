using Liyanjie.TemplateMatching;
using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace Liyanjie.Jsonql.Tester.AspNet
{
    internal class JsonqlTesterEmbeddedMIddleware
    {
        readonly Assembly assembly;
        readonly string baseNamespace;
        readonly string pathBase;

        public JsonqlTesterEmbeddedMIddleware(Assembly assembly, string baseNamespace, string pathBase)
        {
            this.assembly = assembly;
            this.baseNamespace = baseNamespace;
            this.pathBase = pathBase;
        }

        public bool Invoke(HttpContext httpContext)
        {
            if (matchRequesting(httpContext.Request))
            {
                using (var stream = new JsonqlTesterVirtualPathProvider(assembly, baseNamespace, pathBase).GetFile(httpContext.Request.Path).Open())
                using (var reader = new StreamReader(stream))
                {
                    respondStream(httpContext.Response, reader.ReadToEnd());
                }
                return true;
            }

            return false;
        }

        bool matchRequesting(HttpRequest request)
        {
            return "GET".Equals(request.HttpMethod, StringComparison.OrdinalIgnoreCase) && request.Path.StartsWith(pathBase, StringComparison.OrdinalIgnoreCase);
        }

        void respondStream(HttpResponse response, string content)
        {
            response.StatusCode = 200;
            using (var writter = new StreamWriter(response.OutputStream))
            {
                writter.Write(content);
            }
        }
    }
}
