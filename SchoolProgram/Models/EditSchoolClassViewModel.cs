using SchoolProgram.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProgram.Models
{
    public class EditSchoolClassViewModel
    {
        public SchoolClass CurrentClass { get; set; }
        public virtual ICollection<AppUser> Teachers { get; set; }

        public virtual ICollection<AppUser> Students { get; set; }

    }
}
