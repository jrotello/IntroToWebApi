using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CameraFinder.Web.Infrastructure.Extensions;

namespace CameraFinder.Web.Infrastructure.MessageHandlers
{
    public class ElapsedTimeHandler : DelegatingHandler
    {        
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            
            return base.SendAsync(request, cancellationToken).ContinueWith(t => {
                var metadata = request.GetApiRequestMetaData();
                if (metadata != null) {
                    t.Result.Headers.Add("X-Elapsed-Milliseconds", metadata.RequestMilliseconds.ToString());
                }

                return t.Result;
            });
        }
    }
}