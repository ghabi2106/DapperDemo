using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApiDemo.Models
{
    public class Junction
    {
        public int JunctionId { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        [Write(false)]
        public virtual Book Book { get; set; }
        [Write(false)]
        public virtual Author Author { get; set; }
    }
}
