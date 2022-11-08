using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObakiSite.Application.Features.Animelist.Services
{
    public class AnimeListOptions
    {
        public static AnimeListOptions Default => new AnimeListOptions();
        public Uri? BaseAddress { get; set; }
        public string DefaultRequestHeader { get; set; } = string.Empty;
    }
}
