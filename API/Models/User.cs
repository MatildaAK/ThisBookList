using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public required string Name { get; set; }
        public string? UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int IsActive { get; set; } = 1;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        
    }
}