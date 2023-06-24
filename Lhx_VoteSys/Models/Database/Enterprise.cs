using System;
using System.Collections.Generic;

#nullable disable

namespace Lhx_VoteSys.Models.Database
{
    public partial class Enterprise
    {
        public int Projectid { get; set; }
        public string Projectname { get; set; }
        public string Year { get; set; }
        public string Einformation { get; set; }
        public int? Likes { get; set; }
    }
}
