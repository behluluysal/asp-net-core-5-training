using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rokWebsite.Models
{
    public class Kvk
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Kvk Season is required.")]
        [Display(Name = "Kvk Name", Description = "Kvk Season")]
        public string Season { get; set; }

        [Display(Name = "Allies", Description = "Kvk Allies")]
        public string Allies { get; set; }

        [Display(Name = "Opponents", Description = "Kvk Opponents")]
        public string Opponents { get; set; }

        public virtual Kingdom Kingdom { get; set; }
    }
}
