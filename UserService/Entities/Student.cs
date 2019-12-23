using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Entities
{
    [Table("Student")]
    public class Student:BaseEntity
    {
        public string Index { get; set; }
    }
}
