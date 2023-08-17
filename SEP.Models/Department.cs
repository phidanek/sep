using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models
{
    public class Department
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(FacultyId))]
        public int FacultyId { get; set; }
        [Required]
        public Faculty Faculty { get; set; }

    }
}
