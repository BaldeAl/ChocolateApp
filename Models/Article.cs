using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Reference { get; set; }
        public float Price { get; set; }
    }
}
