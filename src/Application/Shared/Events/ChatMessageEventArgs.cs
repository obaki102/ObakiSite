using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObakiSite.Application.Shared.DTO;

namespace ObakiSite.Application.Shared.Events
{
    public class ChatMessageEventArgs : EventArgs
    {
        public ChatMessage ChatMessage { get; set; } = new();
    }
}
