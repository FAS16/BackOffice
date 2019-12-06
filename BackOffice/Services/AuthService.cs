using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice.Services
{
    public class AuthService
    {

        public static readonly string TokenString = "ApiAccessToken";
        public static readonly string BaseUrl = "https://localhost:44305";

        public static bool IsAuthenticated()
        {

            var token = Models.Domain.User.Instance.Token;

            if (token == null)

            {
                HttpContext.Current.Session[TokenString] = null;
                return false;
            }

            var expiresIn = DateTime.Parse(token.Expires);

            if (expiresIn <= DateTime.UtcNow)
            {

                HttpContext.Current.Session[TokenString] = null;

                return false;
            }

            return true;


        }

        public static void SignOut()
        {
            HttpContext.Current.Session[TokenString] = null;
            Models.Domain.User.Instance.Nullify();
        }
    }
}