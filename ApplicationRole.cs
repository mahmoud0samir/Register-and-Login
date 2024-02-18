using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingmachineInfastruction.Extend
{
    public class ApplicationRole :IdentityRole
    {
        public ApplicationRole()
        {
            CreateOn = DateTime.Now.ToShortDateString();
        }

        public string CreateOn { get; set; }
    
}
}
