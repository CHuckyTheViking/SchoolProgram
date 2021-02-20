using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProgram.Data
{
    public class AppUser : IdentityUser
    {

        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Role { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(200)")]
        public string Picture { get; set; }

        [Column(TypeName = "varchar(5)")]
        public string inClass { get; set; }


        [PersonalData]
        public string DisplayName => $"{FirstName} {LastName}";

        public string DisplayPicture => $"{Picture}";
    }
}
