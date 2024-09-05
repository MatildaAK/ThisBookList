using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public DateTime ReleseDate { get; set; }
        public int Pages { get; set; }
        
    }
}