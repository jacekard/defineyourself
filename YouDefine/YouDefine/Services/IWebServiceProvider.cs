using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouDefine.Services
{
    /// <summary>
    /// IWebServiceProvider interface
    /// provides methods for Web Service Provider
    /// </summary>
    public interface IWebServiceProvider
    {
        TimeSpan GetWebsiteOnlineDateTime();

        long GetVisitors();
    }
}
