namespace YouDefine.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Bug Model Entity
    /// Used for reporting bugs by users via API controller
    /// </summary>
    public class Bug
    {
        [Key]
        public long Id { get; set; }

        public string Information { get; set; }

        public bool IsFixed { get; set; }

        public DateTime ReportDate { get; set; }

        public Bug()
        {
            ReportDate = DateTime.Now;
            IsFixed = false;
        }
    }
}
