namespace Lhx_VoteSys.Models
{
    public class EnterpriseClass
    {
        public int Projectid { get; set; }
        public string Projectname { get; set; }
        public string Photo { get; set; }
        public string Einformation { get; set; }
        public string Awards { get; set; }
    }

    public class EnterpriseDetails
    {
        public string Photo { get; set; }
        public string Projectname { get; set; }
        public string Einformation { get; set; }
        public string Awards { get; set; }

        public int Likes { get; set; }
    }

    public class VoteItemList
    {
        public int Voteid { get; set; }
        public string Photo { get; set; }
        public string Projectname { get; set; }
        public int Likes { get; set; }
    }
}
