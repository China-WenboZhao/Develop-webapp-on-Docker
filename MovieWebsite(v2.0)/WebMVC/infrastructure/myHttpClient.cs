using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMVC.infrastructure
{
    public class myHttpClient : HttpClient
    {
        public myHttpClient()
        {
            Timeout = TimeSpan.FromMinutes(2);
        }
    }
}
