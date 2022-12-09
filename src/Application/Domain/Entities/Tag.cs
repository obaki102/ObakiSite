using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObakiSite.Application.Domain.Entities
{
    public class Tag
    {
        public string Id { get; set; }  = string.Empty;
        public string TagName { get; set; } = string.Empty;
        public string Type { get; set; } = "Tag";
    }
}
