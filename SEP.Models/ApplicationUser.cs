using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Telephone { get; set; }

        /*        For future use:
         *        public int ? EmployerId { get; set; }
                [ForeignKey(nameof(EmployerId))]
                [ValidateNever]
                public Employer Employer { get; set; }*/


    }
}
