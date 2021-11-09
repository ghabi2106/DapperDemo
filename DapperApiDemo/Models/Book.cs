using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApiDemo.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Write(false)]
        public virtual List<Junction> Junctions { get; set; }
    }
}
