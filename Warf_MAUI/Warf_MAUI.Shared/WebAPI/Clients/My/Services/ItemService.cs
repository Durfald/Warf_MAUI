using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warf_MAUI.Shared.WebAPI.Models.My;

namespace Warf_MAUI.Shared.WebAPI.Clients.My.Services
{
    internal class ItemService
    {
        private readonly IHttpClient _apiClient;

        public ItemService(IHttpClient client)
        {
            _apiClient = client;
        }

        public async Task<RawSortedItemData> GetItems()
        {

        }
    }
}
