using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouDefine.Models
{
    public class DefinitionResult
    {
        public long Id { get; set; }

        public string Text { get; set; }

        public int Likes { get; set; }

        public string Date { get; set; }
    }
}
