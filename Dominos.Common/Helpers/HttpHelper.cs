using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dominos.Common.Helpers
{
    public class HttpHelper
    {
        public static T Get<T>(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 1000000;
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString.FromJson<T>();
        }

        public static R Post<R, T>(T entity, string url, string type)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 1000000;
            var postString = entity.ToJson();
            var postData = Encoding.UTF8.GetBytes(postString);

            request.Method = "POST";
            request.ContentType = type;
            request.ContentLength = postData.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(postData, 0 , postData.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString.FromJson<R>();
        }
    }
}
