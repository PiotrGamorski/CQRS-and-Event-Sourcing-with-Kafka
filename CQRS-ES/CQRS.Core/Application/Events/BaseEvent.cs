﻿using CQRS.Core.Messages;

namespace CQRS.Core.Application.Events
{
    public class BaseEvent : Message
    {
        protected BaseEvent(string type)
        {
            Type = type;
        }

        public int Version { get; set; }
        public string Type { get; init; } = null!;

    }
}
