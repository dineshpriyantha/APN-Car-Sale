using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APN_Car_Sale.Models
{
    public partial class Registration
    {
        public int UserId { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(150)]
        [Display(Name = "Email Address..")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(150, MinimumLength = 6)]
        [Display(Name = "Password.")]
        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        [Required]
        [Display(Name = "Firt Name.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string UserType { get; set; }
        public int MyProperty { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string IPAddress { get; set; }

    }

}