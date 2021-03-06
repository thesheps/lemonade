﻿using Lemonade.Domain.Infrastructure;

namespace Lemonade.Domain.Events
{
    public class ResourceHasBeenCreated : IDomainEvent
    {
        public int ResourceId { get; private set; }
        public int ApplicationId { get; private set; }
        public string ResourceSet { get; private set; }
        public string ResourceKey { get; private set; }
        public string Locale { get; private set; }
        public string Value { get; private set; }

        public ResourceHasBeenCreated(int resourceId, int applicationId, string resourceSet, string resourceKey, string locale, string value)
        {
            ResourceId = resourceId;
            ApplicationId = applicationId;
            ResourceSet = resourceSet;
            ResourceKey = resourceKey;
            Locale = locale;
            Value = value;
        }
    }
}