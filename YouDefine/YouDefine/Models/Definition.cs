using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace YouDefine.Models
{
    public class Definition
    {
        [Key]
        public long DefinitionId { get; set; }

        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        public int Likes { get; set; }

        public long IdeaId { get; set; }

        public Definition()
        {
        }
        
        public Definition(string text)
        {
            Text = text;
            Likes = 0;
            CreationDate = DateTime.Now;
        }
    }     
}
