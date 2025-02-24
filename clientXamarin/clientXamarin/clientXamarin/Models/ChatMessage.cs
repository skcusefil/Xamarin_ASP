﻿using System;
using System.Collections.Generic;
using System.Text;

namespace clientXamarin.Models
{
    public class ChatMessage
    {

        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderUsername { get; set; } 
        public string SenderDisplayName { get; set; }
        public string SenderPhotoUrl { get; set; }
        public int RecipientId { get; set; }
        public string RecipientUsername { get; set; }
        public string RecipientDisplayName { get; set; }
        public string RecipientPhotoUrl { get; set; }
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }
    }
}
