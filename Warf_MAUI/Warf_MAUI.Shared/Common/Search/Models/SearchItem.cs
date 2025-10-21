using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warf_MAUI.Shared.Common.BM25;

namespace Warf_MAUI.Shared.Common.Search.Models
{
    internal class SearchItem : IBM25Item
    {

        public string Id { get; set; } = null!;

        public string slug { get; set; } = null!;

        public string Name { get; init; } = null!;

        public string ImageUrl { get; set; } = null!;

        public int? Rank { get; set; }

        public string? Subtype { get; set; }

    }
}
