using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace BackOffice.Services
{
    public class AuthService
    {

        public static readonly string TokenString = "ApiAccessToken";
        public static readonly string TokenExpireDate = "TokenExpireDate";
        public static readonly string BaseUrl = "https://attendingengine20191213102651.azurewebsites.net/";
        private static HttpClient HttpClient;

        public AuthService()
        {
        }


        public static HttpClient GetHttpClient()
        {
            if (HttpClient == null)
            {
                HttpClient = new HttpClient();
            }

            return HttpClient;
        }

        public static bool IsAuthenticated()
        {

            var token = (string) HttpContext.Current.Session[TokenString];
            var expireDate = (string) HttpContext.Current.Session[TokenExpireDate];

            if (token == null || expireDate == null)

            {
                SignOut();
                return false;
            }

            var expiresIn = DateTime.Parse(expireDate);

            if (expiresIn <= DateTime.UtcNow)
            {

                SignOut();

                return false;
            }

            return true;


        }

        public static string GetBearerToken()
        {
            return "Bearer " + HttpContext.Current.Session[TokenString];
        }

        public static void SignOut()
        {
            HttpContext.Current.Session[TokenString] = null;
            Models.Domain.User.Instance.Nullify();
        }
    }
}