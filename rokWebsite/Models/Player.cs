using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rokWebsite.Models
{
    public abstract class Player : BasePlayer
    {
        [Required(ErrorMessage = "Player name is required.")]
        [Display(Name = "Player Name", Description = "Player Name")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Player name must between 6-15 characters")]
        public string Name { get; set; }


        [RegularExpression(@"-?\d+(?:\.\d+)?", ErrorMessage = "Please enter valid minimum price")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid minimum price")]
        public double Dkp { get; set; }
    }
}
