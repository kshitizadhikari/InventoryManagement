﻿namespace Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public bool isDeleted { get; set; } = false;

    }
}
