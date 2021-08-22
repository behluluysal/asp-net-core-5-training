using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rokWebsite.Models
{
    public class Alliance
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage ="Alliance name is required")]
        [Display(Name = "Alliance Name", Description = "Alliance Name")]
        [MinLength(4, ErrorMessage = "Alliance must be longer than 4 characters")]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
