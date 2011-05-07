using System;
using System.Web;

namespace PivotalPoker.Models
{
    public class CookieGameStarter : IGameStarter
    {
        public CookieGameStarter(HttpContextBase httpContext)
        {
            HttpContext = httpContext;
        }

        public HttpContextBase HttpContext { get; private set; }

        public HttpRequestBase Request { get { return HttpContext.Request; } }

        public HttpResponseBase Response { get { return HttpContext.Response; } }

        public string Name
        {
            get
            {
                var cookie = Request.Cookies.Get("pp.name");
                if (cookie == null)
                    return String.Empty;

                return cookie.Value;
            }

            set { Request.Cookies.Add(new HttpCookie("pp.name", value)); }
        }
    }
}