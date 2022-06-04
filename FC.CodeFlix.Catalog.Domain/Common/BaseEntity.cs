﻿namespace FC.CodeFlix.Catalog.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }

        protected BaseEntity() 
            => Id = Guid.NewGuid();
    }
}
