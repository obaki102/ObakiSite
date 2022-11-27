using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObakiSite.Shared.Events
{
    public record ClosedConnectionEventArgs(bool IsSuccess, string Message);
}
