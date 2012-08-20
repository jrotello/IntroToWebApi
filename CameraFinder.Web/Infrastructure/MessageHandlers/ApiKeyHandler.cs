using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CameraFinder.Web.Infrastructure.MessageHandlers
{
    public class ApiKeyHandler: DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            string apikey = GetApiKeyFromQueryString(request);

            if (!IsValidApiKey(apikey)) {
                var response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "A valid API key is required.");
                return Task.Factory.StartNew(() => response);

            }
            return base.SendAsync(request, cancellationToken);
        }

        private bool IsValidApiKey(string apikey) {
            if (string.IsNullOrEmpty(apikey)) {
                return false;
            }

            return apikey.Equals("secret", StringComparison.CurrentCultureIgnoreCase);
        }

        private static string GetApiKeyFromQueryString(HttpRequestMessage request) {
            return request.GetQueryNameValuePairs()
                .Where(pair => pair.Key == "apikey")
                .Select(pair => pair.Value)
                .FirstOrDefault();
        }
    }
}