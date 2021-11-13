using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace DapperApiDemo.Models
{
    public class Paragraph
    {
        public int ParagraphId { get; set; }
        public string Content { get; set; }
        public int PageId { get; set; }
        [Write(false)]
        public virtual Page Page { get; set; }
    }
}