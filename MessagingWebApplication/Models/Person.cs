
namespace MessagingWebApplication.Models
{
    using System;
    
    public partial class Person
    {
        public int PersonId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> DateOfJoining { get; set; }
        public string PhtotFileName { get; set; }
    }
}
