using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Models
{
    [Dapper.Contrib.Extensions.Table("Categories")]
    public class Category
    {
        public Category()
        {
            Books = new List<Book>();
        }

        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Write(false)]
        public virtual List<Book> Books { get; set; }
    }
}
