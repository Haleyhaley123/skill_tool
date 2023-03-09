using Microsoft.AspNetCore.Http;
using NTS.Common.Users;
using System.Threading.Tasks;

namespace Training.Services.Auth
{
    public interface IAuthService
    {
        Task<NtsUserTokenModel> LoginAsync(NtsLogInModel loginModel, HttpRequest request);

        //string GetById(string id);

        Task<bool> LogOutAsync(string userId, HttpRequest request);

        /// <summary>
        /// Check token userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool IsTokenAlive(string userId);

        /// <summary>
        /// Xóa rediscache theo userId
        /// </summary>
        /// <param name="userId"></param>
        public void RemoveRedis(string userId);

        /// <summary>
        /// Quên mật khẩu
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> ForgotPasswordAsync(string email, string otp);

        /// <summary>
        /// Lấy mã OTP
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> GetOTPAsync(string email);
    }
}
