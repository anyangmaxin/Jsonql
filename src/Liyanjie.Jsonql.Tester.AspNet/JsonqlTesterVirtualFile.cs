using System.IO;
using System.Reflection;
using System.Web.Hosting;

namespace Liyanjie.Jsonql.Tester.AspNet
{
    internal class JsonqlTesterVirtualFile : VirtualFile
    {
        readonly Assembly assembly;
        readonly string baseNamespace;
        readonly string fileName;

        public JsonqlTesterVirtualFile(Assembly assembly, string baseNamespace, string fileName)
            : base(fileName)
        {
            this.assembly = assembly;
            this.baseNamespace = baseNamespace;
            this.fileName = fileName;
        }

        public override Stream Open()
        {
            return assembly?.GetManifestResourceStream($"{baseNamespace}.{fileName}");
        }
    }
}
