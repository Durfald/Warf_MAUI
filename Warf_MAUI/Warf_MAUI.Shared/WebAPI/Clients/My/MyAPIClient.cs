using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warf_MAUI.Shared.WebAPI.Clients.My
{
    internal class MyAPIClient
    {
        private readonly IHttpClient _apiClient;

        public MyAPIClient(IHttpClient apiClient)
        {
            _apiClient = apiClient;

        }
    }
}
