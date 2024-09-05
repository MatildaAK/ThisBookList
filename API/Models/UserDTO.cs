using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class UserDTO
    {
        public required string Name { get; set; }
        public string? UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }  
        
    }
}