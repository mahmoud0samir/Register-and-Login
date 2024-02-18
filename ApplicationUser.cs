using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingmachineInfastruction.Extend
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            CreateOn = DateTime.Now.ToShortDateString();
        }
        public string CreateOn { get; set; }
        public bool IsAgree { get; set; }
    }
}
