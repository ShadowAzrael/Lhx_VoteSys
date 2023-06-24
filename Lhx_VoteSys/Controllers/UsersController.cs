using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lhx_VoteSys.Models.Database;
using Lhx_VoteSys.service;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Lhx_VoteSys.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly VoteSysContext _db;
        private readonly IJWTService _jwtService;
        public UsersController(VoteSysContext db, IJWTService jwtService)
        {
            _db = db;
            _jwtService = jwtService;
        }

        [HttpPost]
        public object registered(string email, string pwd, string ispwd)
        {
            if (email == null || pwd == null) { return "请确认有无漏填"; };
            if (ispwd != pwd) { return "密码不一致"; };
            if (_db.Users.Any(x => x.Email == email)) { return "邮箱已注册"; };

            _db.Users.Add(new User()
            {
                Email = email,
                Password = pwd,
            });

            if (_db.SaveChanges() > 0) return "注册成功";
            return "注册失败";
        }

        [HttpPost]
        public object login(string email, string pwd)
        {
            if (Response.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value != null) return "请先退出登录";
            if (email == null || pwd == null) { return "请确认有无漏填"; };


            var list = _db.Users.FirstOrDefault(x => x.Email == email);
            if (list == null) return "邮箱不存在";
            if (string.Concat(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(pwd)).Select(b => b.ToString("X2"))) != list.Password) return "密码错误";

            //创建token
            return _jwtService.CreateToken(email, list.Userid.ToString());
        }
    }
}
