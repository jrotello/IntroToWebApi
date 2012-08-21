using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CameraFinder.Web.Infrastructure.MessageHandlers {
    public class FormatSelectionHandler: DelegatingHandler {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {

            string format = GetFormatFromQuerystring(request);
            if (!string.IsNullOrEmpty(format)) {
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(GetMediaType(format));
            }
            
            return base.SendAsync(request, cancellationToken);
        }

        private static string GetFormatFromQuerystring(HttpRequestMessage request) {
            string format = request.GetQueryNameValuePairs()
                .Where(pair => pair.Key.Equals("format", StringComparison.InvariantCultureIgnoreCase))
                .Select(pair => pair.Value)
                .FirstOrDefault();
            return format;
        }

        private MediaTypeWithQualityHeaderValue GetMediaType(string format) {
            string mediaType;
            switch (format.ToLower()) {
                case "xml":
                    mediaType = "application/xml";
                    break;
                case "image":
                    mediaType = "image/*";
                    break;
                default: 
                    mediaType = "application/json";
                    break;
            }
            
            return new MediaTypeWithQualityHeaderValue(mediaType);
        }
    }
}