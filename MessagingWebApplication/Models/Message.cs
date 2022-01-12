
namespace MessagingWebApplication.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Messages")]
    public partial class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }

        [Key, ForeignKey("Sender")]
        public int SenderId { get; set; }

        [Key, ForeignKey("Receiver")]
        public int ReceiverId { get; set; }
        public string MessageContent { get; set; }
        public DateTime SendingDate { get; set; }
        public DateTime? ReceivingDate { get; set; }
        public string Attachment { get; set; }

        public virtual User Sender { get; set; }

        public virtual User Receiver { get; set; }
    }
}
