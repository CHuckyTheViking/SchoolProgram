using SchoolProgram.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProgram.Models
{
    public class SchoolClass
    {

        [Required]
        [Key]
        public string Id { get; set; }

        public AppUser Teacher { get; set; }

        public virtual ICollection<AppUser> Students { get; set; }
    }
}
