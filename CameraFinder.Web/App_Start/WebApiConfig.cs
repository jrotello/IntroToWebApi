using System.Web.Http;
using CameraFinder.Web.Infrastructure.MessageHandlers;

namespace CameraFinder.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config) {
            ConfigureRoutes(config);
            ConfigureMessageHandlers(config);
        }

        private static void ConfigureMessageHandlers(HttpConfiguration config) {
            config.MessageHandlers.Add(new ApiKeyHandler());
            config.MessageHandlers.Add(new ElapsedTimeHandler());
        }

        private static void ConfigureRoutes(HttpConfiguration config) {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
        }
    }
}
