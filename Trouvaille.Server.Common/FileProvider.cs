using System.IO;
using System.Web;
using Trouvaille.Server.Common.Contracts;

namespace Trouvaille.Server.Common
{
    public class FileProvider : IFileProvider
    {
        private readonly HttpRequestBase request;
        private readonly HttpServerUtilityBase server;

        public FileProvider(HttpRequestBase request, HttpServerUtilityBase server)
        {
            this.request = request;
            this.server = server;
        }

        public string SavePhotoToFileSystem(string filePath, string path)
        {
            if (this.request.Files.Count > 0)
            {
                var file = request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    filePath = path + "-" + fileName;
                    file.SaveAs(this.server.MapPath(filePath));
                }
            }

            return filePath;
        }
    }
}
