namespace YouDefine.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Definiton Model Entity
    /// Used along with Idea Model
    /// </summary>
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
