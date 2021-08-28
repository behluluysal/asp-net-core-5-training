using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rokWebsite.Models
{
    public class Perfomance
    {
        [Key]
        public int Id { get; set; }

        //below is for base datas


        [Required(ErrorMessage = "T4 kill base is required.")]
        [Display(Name = "T4 kill base", Description = "T4 kill base")]
        [RegularExpression(@"-?\d+(?:\.\d+)?", ErrorMessage = "Please enter valid minimum price")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid minimum price")]
        public double T4killsBase { get; set; }

        [Required(ErrorMessage = "T5 kill base is required.")]
        [Display(Name = "T5 kill base", Description = "T5 kill base")]
        [RegularExpression(@"-?\d+(?:\.\d+)?", ErrorMessage = "Please enter valid minimum price")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid minimum price")]
        public double T5killsBase { get; set; }


        [Required(ErrorMessage = "T4 deaths base is required.")]
        [Display(Name = "T4 deaths base", Description = "T4 deaths base")]
        [RegularExpression(@"-?\d+(?:\.\d+)?", ErrorMessage = "Please enter valid minimum price")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid minimum price")]
        public double T4deathsBase { get; set; }


        [Required(ErrorMessage = "T5 deaths base is required.")]
        [Display(Name = "T5 deaths base", Description = "T5 deaths base")]
        [RegularExpression(@"-?\d+(?:\.\d+)?", ErrorMessage = "Please enter valid minimum price")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid minimum price")]
        public double T5deathsBase { get; set; }


        [Required(ErrorMessage = "Rss Support base is required.")]
        [Display(Name = "Rss Support base", Description = "Rss Support base")]
        [RegularExpression(@"-?\d+(?:\.\d+)?", ErrorMessage = "Please enter valid minimum price")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid minimum price")]
        public double RssSupportBase { get; set; }


        //below is for current datas


        [Required(ErrorMessage = "T4 kill current is required.")]
        [Display(Name = "T4 kill current", Description = "T4 kill current")]
        [RegularExpression(@"-?\d+(?:\.\d+)?", ErrorMessage = "Please enter valid minimum price")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid minimum price")]
        public double T4killsCurrent { get; set; }

        [Required(ErrorMessage = "T5 kill current is required.")]
        [Display(Name = "T5 kill current", Description = "T5 kill current")]
        [RegularExpression(@"-?\d+(?:\.\d+)?", ErrorMessage = "Please enter valid minimum price")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid minimum price")]
        public double T5killsCurrent { get; set; }


        [Required(ErrorMessage = "T4 deaths current is required.")]
        [Display(Name = "T4 deaths current", Description = "T4 deaths current")]
        [RegularExpression(@"-?\d+(?:\.\d+)?", ErrorMessage = "Please enter valid minimum price")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid minimum price")]
        public double T4deathsCurrent { get; set; }


        [Required(ErrorMessage = "T5 deaths current is required.")]
        [Display(Name = "T5 deaths current", Description = "T5 deaths current")]
        [RegularExpression(@"-?\d+(?:\.\d+)?", ErrorMessage = "Please enter valid minimum price")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid minimum price")]
        public double T5deathsCurrent { get; set; }


        [Required(ErrorMessage = "Rss Support current is required.")]
        [Display(Name = "Rss Support current", Description = "Rss Support current")]
        [RegularExpression(@"-?\d+(?:\.\d+)?", ErrorMessage = "Please enter valid minimum price")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid minimum price")]
        public double RssSupportCurrent { get; set; }
    }
}
