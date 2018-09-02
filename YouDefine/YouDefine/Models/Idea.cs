using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouDefine.Models
{
    public class Idea
    {
        public long Id { get; set; }

        public string Title { get; set; }
        
        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public List<Definition> Definitions { get; set; }

        public List<Author> Authors { get; set; }

    }
}
