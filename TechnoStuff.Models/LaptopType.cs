using System.ComponentModel.DataAnnotations;

namespace TechnoStuff.Models
{
    public class LaptopType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
