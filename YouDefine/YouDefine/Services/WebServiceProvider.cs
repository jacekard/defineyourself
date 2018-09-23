using System;

namespace YouDefine.Services
{
    public class WebServiceProvider : IWebServiceProvider
    {
        public static readonly DateTime CreationDateTime = DateTime.Now;

        public static TimeSpan OnlineDateTime;

        public static long VisitorsCount;

        public WebServiceProvider()
        {
            VisitorsCount = 0;
        }

        public TimeSpan GetWebsiteOnlineDateTime()
        {
            OnlineDateTime = DateTime.Now - CreationDateTime;

            return OnlineDateTime;
        }

        public long GetVisitors()
        {
            return VisitorsCount;
        }
    }
}
