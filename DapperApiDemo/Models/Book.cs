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
        [Write(false)]
        public virtual Category Category { get; set; }

        public virtual List<Junction> Junctions { get; set; }

        public virtual Cover Cover { get; set; }

        public virtual List<Chapter> Chapters { get; set; }
    }
}
