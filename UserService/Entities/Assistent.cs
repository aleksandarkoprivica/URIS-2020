using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Entities
{
    [Table("Assistent")]
    public class Assistent: BaseEntity
    {
        public string University { get; set; }
        public string Department { get; set; }
        public string AreaOfStudy { get; set; }
    }
}
