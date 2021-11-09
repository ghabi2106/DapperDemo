using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Models
{
    public class Junction
    {
        public int JunctionId { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Author Author { get; set; }
    }
}
