using Lhx_VoteSys.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Lhx_VoteSys.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IJWTService _jwtService;
        public AuthenticationController(IJWTService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpGet]
        public string CreateToken()
        {
            var name = "123";
            return "Bearer " + _jwtService.CreateToken(name, "123");
        }

        [HttpGet]
        [Authorize]
        public string UserInfo()
        {
            var user = Response.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return user;
        }

        [HttpPost]
        public string MD5Encrypt32(string text)
        {
            string result = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                result += s[i].ToString("X");
            }
            return result;
        }

        [HttpPost]
        public string SHA256Encrypt(string text)
        {
            return string.Concat(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(text)).Select(b => b.ToString("X2")));
        }
    }
}
