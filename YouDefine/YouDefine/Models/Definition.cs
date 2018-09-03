using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouDefine.Models
{
    public class Definition
    {
        public long Id { get; set; }

        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        //public Definition(string text = null)
        //{
        //    Text = text;
        //    CreationDate = DateTime.Now;
        //}
    }
}
