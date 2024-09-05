using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public string QuoteMessage{ get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        
    }
}