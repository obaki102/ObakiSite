﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObakiSite.Shared.DTO;

namespace ObakiSite.Shared.Events
{
    public class ChatMessageEventArgs : EventArgs
    {
        public ChatMessage ChatMessage { get; set; } = new();
    }
}
