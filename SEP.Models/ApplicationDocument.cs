using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEP.Models
{
    public class ApplicationDocument
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        [ValidateNever]
        public StudentApplication Application { get; set; }
        //public StudentApplication StudentApplication { get; set; }
        public string? Name { get; set; }
        [Required]
        public string Description { get; set; }
        [ValidateNever]
        public string FilePath { get; set; }


    }
    }
