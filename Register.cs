using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingmachineInfastruction.Entity
{
    [Table("Registertion")]
    internal class Register
    {
        [Required(ErrorMessage = " User Name Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = " Email Required")]
        [EmailAddress(ErrorMessage = "invalid mail formate")]
        public string Email { get; set; }


        [Required(ErrorMessage = " Password Required")]
        [MinLength(6, ErrorMessage = "Min Len 6")]
        [MaxLength(8, ErrorMessage = "Max Len 8")]
        public string Password { get; set; }

        [Required(ErrorMessage = " Confirm Password Required")]
        [MinLength(6, ErrorMessage = "Min Len 6")]
        [MaxLength(8, ErrorMessage = "Max Len 8")]
        [Compare("Password", ErrorMessage = "password not match")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }
    }
}
