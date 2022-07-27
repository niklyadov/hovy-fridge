﻿namespace HovyFridge.Entity
{
    public interface IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateTime { get; set; }
    }
}