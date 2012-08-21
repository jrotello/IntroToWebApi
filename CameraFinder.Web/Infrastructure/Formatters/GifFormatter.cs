using CameraFinder.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace CameraFinder.Web.Infrastructure.Formatters {
    public class GifFormatter: MediaTypeFormatter {
        public GifFormatter() {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("image/gif"));
        }

        public override bool CanWriteType(Type type) {
            return type == typeof(Camera);
        }

        public override bool CanReadType(Type type) {
            return false;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext) {
            var camera = value as Camera;
            if (camera == null) {
                throw new ArgumentNullException("value");
            }

            return Task.Factory.StartNew(() => {
                string path = GetImagePath(camera);
                using (var fs = File.OpenRead(path)) {
                    fs.CopyTo(writeStream);
                }
            });
        }

        private static string GetImagePath(Camera camera) {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string path = String.Format("App_Data\\Cameras\\{0}\\{1}.gif", camera.Make, camera.Model.Replace(" ", ""));
            path = Path.Combine(baseDir, path);

            if (!File.Exists(path)) {
                path = Path.Combine(baseDir, "App_Data\\Cameras\\default.gif");
            }

            return path;
        }
    }
}