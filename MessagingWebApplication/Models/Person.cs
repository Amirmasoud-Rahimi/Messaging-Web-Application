
namespace MessagingWebApplication.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Person
    {
        public int PersonId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }
        public Nullable<System.DateTime> DateOfJoining { get; set; }
        public string PhtotFileName { get; set; }
    }
}
