using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObakiSite.Application.Infra.Data.Firebase
{
    public interface IFirebaseEntity
    {
        public string Id { get; set; }
    }
}
