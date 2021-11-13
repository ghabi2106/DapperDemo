using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApiDemo.Models
{
    public class Cover
    {
        public int CoverId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BookId { get; set; }
        [Write(false)]
        public virtual Book Book { get; set; }
    }
}
