using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObakiSite.Shared.DTO
{
    public class PostSummaryDTO
    {
        public string Id { get; init; } = string.Empty;
        public required string Title { get; init; } = string.Empty;
        public string Author { get; init; } = string.Empty;
        public DateTime CreationDate { get; set; }
    }
}
