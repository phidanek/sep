using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace SEP.Models
{
    public class DriverLicense
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="License Type")]
        public string Name { get; set; }
    }
}
