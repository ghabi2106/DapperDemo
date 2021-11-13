using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace DapperApiDemo.Models
{
    public class Page
    {
        public int PageId { get; set; }
        public string Content { get; set; }
        public int ChapterId { get; set; }
        [Write(false)]
        public virtual Chapter Chapter { get; set; }
        public virtual List<Paragraph> Paragraphs { get; set; }
    }
}