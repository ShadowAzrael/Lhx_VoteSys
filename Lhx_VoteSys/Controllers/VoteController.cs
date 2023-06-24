using Lhx_VoteSys.service;
using Lhx_VoteSys.Models;
using Lhx_VoteSys.Models.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Ljh_VoteSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        //定义数据库上下文
        private readonly VoteSysContext _db;
        public VoteController(VoteSysContext db)
        {
            _db = db;
        }


        [HttpGet]
        public object VoteList(int Projectid, string keywords, int pagination, int paginationNumber)
        {
            paginationNumber = paginationNumber <= 0 ? 10 : paginationNumber;//每分页显示多少条数据
            pagination = pagination <= 0 ? 1 : pagination;//当前第几分页

            List<VoteItem> list = null;
            if (keywords == null || keywords.Length == 0)
            {
                list = _db.VoteItems.Where(x => x.Id == Projectid && x.Verify ==true)
                    .Skip((pagination - 1) * paginationNumber).Take(paginationNumber).ToList();
            }
            else
            {
                list = _db.VoteItems.Where(x => x.Id == Projectid && x.Verify == true && x.Projectname.Contains(keywords))
                    .Skip((pagination - 1) * paginationNumber).Take(paginationNumber).ToList();
            }
            List<VoteItemList> voteItemList = new List<VoteItemList>();
            foreach (var item in list)
            {
                VoteItemList voteItemList1 = new VoteItemList
                {
                    Voteid = item.Id,
                    Photo = item.Photo,
                    Projectname = item.Projectname,
                    Likes = (int)item.Likes,
                };
                voteItemList.Add(voteItemList1);
            }

            return voteItemList;
        }


        [HttpPost]
        public object Vote(int VoteItemId)
        {
            var bit = Response.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (bit == null) return "请先登录";
            var userId = int.Parse(bit);
            if (!_db.VoteItems.Any(x => x.Id == VoteItemId && x.Verify == true)) return "该投票项目不存在";

            var date = DateTime.Today;//获取当天日期
            if (_db.VoteLists.Count(x => x.Userid == userId && x.Date == date) >= 5) return "今日已达投票上限";

            //添加投票列表
            VoteList votingList = new VoteList
            {
                Userid = userId,
                Voteid = VoteItemId,
                Date = date,
            };
            _db.VoteLists.Add(votingList);

            //更新点赞数
            var list = _db.VoteItems.FirstOrDefault(x => x.Id == VoteItemId);
            list.Likes++;
            _db.VoteItems.Update(list);

            if (_db.SaveChanges() > 0) return "投票成功";
            return "投票失败";
        }
    }
}
