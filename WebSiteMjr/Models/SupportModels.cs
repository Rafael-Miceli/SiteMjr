using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace WebSiteMjr.Models
{
    public class SupportContext: DbContext
    {
        public SupportContext()
            : base("DefaultConnection")
        {
                
        }
    }

    [Table("TIcket")]
    public class SupportTicket
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TicketId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public Employee Employee { get; set; }
        public DateTime TimeBegin { get; set; }
        public DateTime TimeDone { get; set; }
        public string FeedBack { get; set; }
    }

    public enum TicketStatus
    {
        Open,
        Doing,
        Done
    }
}