using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CameraFinder.Web.Infrastructure.MessageHandlers
{
    public class ElapsedTimeHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            var sw = Stopwatch.StartNew();

            return base.SendAsync(request, cancellationToken).ContinueWith(t => {
                sw.Stop();
                t.Result.Headers.Add("X-Elapsed-Milliseconds", sw.ElapsedMilliseconds.ToString());

                return t.Result;
            });
        }
    }
}