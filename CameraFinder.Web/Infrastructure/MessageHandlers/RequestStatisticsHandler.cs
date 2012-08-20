using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CameraFinder.Web.Models;

namespace CameraFinder.Web.Infrastructure.MessageHandlers
{
    public class RequestStatisticsHandler: DelegatingHandler {
        public const string ApiRequestMetadataKey = "__ApiRequestMetadata__";

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            var metadata = new RequestStatistics {
                RequestStart = DateTime.UtcNow,
                Method = request.Method,
                Uri = request.RequestUri.ToString()
            };

            request.Properties.Add(ApiRequestMetadataKey, metadata);

            return base.SendAsync(request, cancellationToken).ContinueWith(t => {
                metadata.RequestEnd = DateTime.UtcNow;

                var elapsed = (metadata.RequestEnd - metadata.RequestStart).Value;
                metadata.RequestMilliseconds = elapsed.Milliseconds;

                // Do something useful with the request statistics.

                return t.Result;
            });
        }
    }
}