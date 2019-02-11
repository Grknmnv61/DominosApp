using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dominos.Common.Helpers
{
    public static class JsonHelper
    {
        public static string ToJson(this object value)
        {
            try
            {
                return JsonConvert.SerializeObject(value, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            }
            catch
            {
                return string.Empty;
            }
        }

        public static T FromJson<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, new IsoDateTimeConverter() { DateTimeFormat = "dd.MM.yyyy HH:mm:ss" });
            }
            catch
            {
                return default(T);
            }
        }
    }
}
