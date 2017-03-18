using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace Liyanjie.Jsonql.Tester.AspNet
{
    internal class JsonqlTesterVirtualPathProvider : VirtualPathProvider
    {
        readonly Assembly assembly;
        readonly string baseNamespace;
        readonly string pathPrefix;

        public JsonqlTesterVirtualPathProvider(Assembly assembly, string baseNamespace, string pathPrefix)
        {
            this.assembly = assembly;
            this.baseNamespace = baseNamespace;
            this.pathPrefix = pathPrefix;
        }

        bool isEmbeddedResourcePath(string virtualPath)
        {
            var checkPath = VirtualPathUtility.ToAppRelative(virtualPath);
            return checkPath.StartsWith($"~{pathPrefix}", StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool FileExists(string virtualPath)
        {
            return isEmbeddedResourcePath(virtualPath) || base.FileExists(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (isEmbeddedResourcePath(virtualPath))
                return new JsonqlTesterVirtualFile(assembly, baseNamespace, virtualPath.Remove(0, pathPrefix.Length).Replace('/', '.'));
            else
                return base.GetFile(virtualPath);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (isEmbeddedResourcePath(virtualPath))
                return null;
            else
                return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }
    }
}
