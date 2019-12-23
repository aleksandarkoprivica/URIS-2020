using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Entities
{
    [Table("User")]
    public class User: BaseEntity
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public Roles Role { get; set; }
        public Guid? StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        public Guid? AssistentId { get; set; }
        [ForeignKey("AssistentId")]
        public virtual Assistent Assistent { get; set; }

        public Guid? ProfessorId { get; set; }
        [ForeignKey("ProfessorId")]
        public virtual Professor Professor { get; set; }

    }
}
