using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YouDefine.Models
{
    public class Idea
    {
        [Key]
        public long IdeaId { get; set; }

        public string Title { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public virtual ICollection<Definition> Definitions { get; set; }

        public int Likes { get; set; }

        public Idea()
        {
            CreationDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
            Likes = 0;
            Definitions = new List<Definition>();
        }

        public Idea(string title)
        {
            Title = title;
            CreationDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
            Likes = 0;
            Definitions = new List<Definition>();
        }

        //public void Append(Definition definition)
        //{
        //    Definitions.Add(definition);
        //    UpdateLastModifiedDate();
        //}

        public int CountLikes()
        {
            foreach (var def in Definitions)
            {
                Likes += def.Likes;
            }

            return Likes;
        }

        private void UpdateLastModifiedDate()
        {
            LastModifiedDate = DateTime.Now;
        }
    }
}
