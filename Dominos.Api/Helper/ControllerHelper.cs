using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace Dominos.Api.Helper
{
    public static class ControllerHelper
    {
        public static HttpResponseMessage GetJsonResponseMessage(object obj)
        {
            HttpResponseMessage response = new HttpResponseMessage() { Content = new StringContent(JsonConvert.SerializeObject(obj, new IsoDateTimeConverter() { DateTimeFormat = "dd.MM.yyyy HH:mm:ss" }), Encoding.UTF8) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return response;
        }
    }
}