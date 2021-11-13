using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace DapperApiDemo.Models
{
    public class Page
    {
        public int PageId { get; set; }
        public string Content { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        [Write(false)]
        public virtual List<Paragraph> Paragraphs { get; set; }
    }
}