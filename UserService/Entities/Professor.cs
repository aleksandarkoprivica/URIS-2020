using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Entities
{
    [Table("Professor")]
    public class Professor: BaseEntity
    {
        public string University { get; set; }
        public string Department { get; set; }
        public string Vocation { get; set; }
    }
}
