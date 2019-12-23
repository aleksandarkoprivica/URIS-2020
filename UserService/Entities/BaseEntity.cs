using System;
using System.ComponentModel.DataAnnotations;

namespace UserService.Entities
{
    public abstract class BaseEntity
    {
        private DateTime createdAt;
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt {
            get { return createdAt; } 
            set { createdAt = DateTime.Now; } 
        }

        public string CreatedBy { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public string LastUpdatedBy { get; set; }
    }
}
