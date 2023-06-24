using Lhx_VoteSys.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Lhx_VoteSys.Models.Database;
using Lhx_VoteSys.Models;
using System.Linq;
using System.Security.Claims;
using System;

namespace Lhx_VoteSys.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InformationController : Controller
    {
        private readonly VoteSysContext _db;
        public InformationController(VoteSysContext db)
        {
            _db = db;
        }

        [HttpPost]
        public object ProjectVerified(int EnterpriseIdentification)
        {
            var bit = Response.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (bit == null) return "请先登录";
            var userid = int.Parse(bit);

            if (_db.VoteItems.Any(x => x.Userid == userid && x.Id == EnterpriseIdentification)) return true;

            return false;
        }


        [HttpPost]
        public object DataUpdated(EnterpriseClass enterpriseClass)
        {
            var bit = Response.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (bit == null) return "请先登录";
            var userId = int.Parse(bit);

            if (enterpriseClass.Projectname == null || enterpriseClass.Projectname == "string") return "请填写企业名称";
            if (!_db.Enterprises.Any(x => x.Projectid == enterpriseClass.Projectid)) return "项目类别不存在";
            if (_db.VoteItems.Any(x => x.Projectname == enterpriseClass.Projectname && x.Userid == userId)) return "已提交过该项目";

            VoteItem votingItem = new VoteItem
            {
                Userid = userId,
                Projectname = enterpriseClass.Projectname,
                Photo = (enterpriseClass.Photo == null || enterpriseClass.Photo == "string") ? null : enterpriseClass.Photo,
                Einformation = (enterpriseClass.Einformation == null || enterpriseClass.Einformation == "string") ? null : enterpriseClass.Einformation,
                Awards = (enterpriseClass.Awards == null || enterpriseClass.Awards == "string") ? null : enterpriseClass.Awards,
                Likes = 0,
                Verify = false
            };
            _db.VoteItems.Add(votingItem);
            if (_db.SaveChanges() > 0) return "提交成功";
            return "提交失败";
        }

        [HttpPost]
        public object ReviewInformation(int VoteItemId, Boolean verify)
        {
            if (verify == null || verify == false) return "请输入审核结果";
            var list = _db.VoteItems.FirstOrDefault(x => x.Id == VoteItemId);
            if (list == null) return "该项目ID不存在";
            if (list.Verify != false) return "该项目已审核";

            list.Verify = verify;
            _db.VoteItems.Update(list);

            if (_db.SaveChanges() > 0) return "审核成功";
            return "审核失败";
        }

        [HttpGet]
        public object EnterpriseDetails(int VoteItemId)
        {
            var list = _db.VoteItems.FirstOrDefault(x => x.Id == VoteItemId);
            if (list == null || !list.Verify == true) return "项目ID不存在";

            EnterpriseDetails enterpriseDetails = new EnterpriseDetails
            {
                Photo = list.Photo,
                Projectname = list.Projectname,
                Einformation = list.Einformation,
                Awards = list.Awards,
                Likes = (int)list.Likes,
            };

            return enterpriseDetails;
        }
    }
}
