using System;
using System.Collections.Generic;

#nullable disable

namespace Lhx_VoteSys.Models.Database
{
    public partial class VoteItem
    {
        public int Id { get; set; }
        public int? Userid { get; set; }
        public string Projectname { get; set; }
        public string Einformation { get; set; }
        public string Photo { get; set; }
        public string Awards { get; set; }
        public bool Verify { get; set; }
        public int? Likes { get; set; }
    }
}
