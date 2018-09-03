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

        public int Likes { get; set; }

        public Idea()
        {
            Definitions = new List<Definition>();
            CreationDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
            Likes = 0;
        }

        public Idea(string title, Definition definition = null)
        {
            Title = title;
            Definitions = new List<Definition>();
            if (definition != null)
            {
                Definitions.Add(definition);
            }
            CreationDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
            Likes = 0;
        }

        public void Append(Definition definition)
        {
            Definitions.Add(definition);
            UpdateLastModifiedDate();
        }

        public int CountLikes()
        {
            foreach (var def in Definitions)
            {
                Likes += def.Likes;
            }

            return Likes;
        }

        public void UpdateLastModifiedDate()
        {
            LastModifiedDate = DateTime.Now;
        }
    }
}
