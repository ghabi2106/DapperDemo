using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApiDemo.Models
{
    public class Chapter
    {
        public int ChapterId { get; set; }
        public string Title { get; set; }
        public int ChapterNumber { get; set; }
        public int BookId { get; set; }
        [Write(false)]
        public virtual Book Book { get; set; }
        public virtual List<Page> Pages { get; set; }
    }
}
