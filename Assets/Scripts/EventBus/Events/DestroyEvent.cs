﻿using Entities;

namespace EventBus.Events
{
    public struct DestroyEvent : IEvent
    {
        public IEntity Entity;

        public DestroyEvent(IEntity entity)
        {
            Entity = entity;
        }
    }
}