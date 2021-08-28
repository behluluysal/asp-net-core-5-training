using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rokWebsite.Models
{
    public class KvkPlayers
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Update Time")]
        [Display(Name = "Update Time", Description = "Update Time")]
        public DateTime UpdateTime { get; set; }

        public virtual Kvk Kvk { get; set; }
        public virtual User User { get; set; }
        public virtual Perfomance Perfomance { get; set; }

    }
}
