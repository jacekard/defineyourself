namespace YouDefine.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    /// <summary>
    /// Author Model Entity
    /// Used for creating accounts and login authorization
    /// </summary>
    public class Author
    {
        [Key]
        public long Id { get; set; }

        public string Username { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastVisitedDate { get; set; }

        public string AuthorizationRights { get; set; }
    }
}
