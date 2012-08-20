using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using CameraFinder.Web.Infrastructure.MessageHandlers;
using CameraFinder.Web.Models;

namespace CameraFinder.Web.Infrastructure.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static RequestStatistics GetApiRequestMetaData(this HttpRequestMessage request) {
            if (request.Properties.ContainsKey(RequestStatisticsHandler.ApiRequestMetadataKey)) {
                return request.Properties[RequestStatisticsHandler.ApiRequestMetadataKey] as RequestStatistics;
            }

            return null;
        }
    }
}