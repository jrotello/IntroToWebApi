using System;
using System.Web.Http;
using CameraFinder.Web.Infrastructure.ActionFilters;
using CameraFinder.Web.Infrastructure.MessageHandlers;
using CameraFinder.Web.Infrastructure.Formatters;

namespace CameraFinder.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config) {
            ConfigureRoutes(config);
            ConfigureMessageHandlers(config);
            ConfigureActionFilters(config);
            ConfigureFormatters(config);
        }

        private static void ConfigureFormatters(HttpConfiguration config) {
            config.Formatters.Add(new GifFormatter());
        }

        private static void ConfigureActionFilters(HttpConfiguration config) {
            config.Filters.Add(new RequestStatisticsFilter());
        }

        private static void ConfigureMessageHandlers(HttpConfiguration config) {
            config.MessageHandlers.Add(new ApiKeyHandler());
            config.MessageHandlers.Add(new FormatSelectionHandler());
            config.MessageHandlers.Add(new ElapsedTimeHandler());
            config.MessageHandlers.Add(new RequestStatisticsHandler());                        
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
