using System.ComponentModel.DataAnnotations;

namespace crudopration.Models
{
    public class Users
    {
        [Key]

        public int ID { get; set; }

        public string Name { get; set; } 

        public string ContactNo { get; set; }
    }
}
