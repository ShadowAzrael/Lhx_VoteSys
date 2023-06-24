namespace Lhx_VoteSys.service
{
    /// <summary>
    /// 认证处理接口
    /// </summary>
    public interface IJWTService
    {
        string CreateToken(string Email, string id);
    }
}
