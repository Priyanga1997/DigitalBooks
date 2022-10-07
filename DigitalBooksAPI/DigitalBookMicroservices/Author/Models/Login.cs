using System;
using System.Collections.Generic;

#nullable disable

namespace Author.Models
{
    public partial class Login
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }
}
