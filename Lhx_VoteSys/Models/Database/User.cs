using System;
using System.Collections.Generic;

#nullable disable

namespace Lhx_VoteSys.Models.Database
{
    public partial class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Userid { get; set; }
    }
}
