using System;
using System.Collections.Generic;

#nullable disable

namespace Lhx_VoteSys.Models.Database
{
    public partial class VoteList
    {
        public int Voteid { get; set; }
        public int Userid { get; set; }
        public DateTime Date { get; set; }
    }
}
