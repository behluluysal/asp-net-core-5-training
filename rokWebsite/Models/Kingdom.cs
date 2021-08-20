using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rokWebsite.Models
{
    public class Kingdom 
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Kingdom name is required.")]
        [Display(Name ="Kingdom Name", Description = "Kingdom Name")]
        [StringLength(6, ErrorMessage ="Kingdom must be shorter than 6 characters")]
        public string Name { get; set; }
    }
}
