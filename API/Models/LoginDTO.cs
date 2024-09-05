using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class LoginDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; } 
        
    }
}