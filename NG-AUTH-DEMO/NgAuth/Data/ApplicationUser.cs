using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NgAuth.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [NotMapped]
        public string Password { get; set; }

        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;
        public DateTime? DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public void MapFrom(ApplicationUser model)
        {
            this.UserName = model.UserName;
            this.Email = model.Email;
            this.PhoneNumber = model.PhoneNumber;
        }
    }
}
