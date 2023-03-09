using Microsoft.AspNetCore.Mvc;
using NTS.Common;
using NTS.Common.Attributes;
using NTS.Common.Models;
using NTS.Common.Resource;
using NTS.Common.Users;
using System.Threading.Tasks;
using Training.Api.Attributes;
using Training.Services.Auth;

namespace Training.Api.Controllers
{
    [ApiController]
    [ApiHandleExceptionSystem]
    [Route("api/auth")]
    [Logging]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="logInModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [ActionName(NTSConstants.NoLogEvent)]
        public async Task<ActionResult<ApiResultModel<NtsUserTokenModel>>> LogInAsync([FromBody] NtsLogInModel logInModel)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await _authService.LoginAsync(logInModel, Request);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("logout")]
        [ActionName(NTSConstants.NoLogEvent)]
        public async Task<ActionResult<ApiResultModel>> LogOutAsync()
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            var userId = GetUserIdByRequest();
            apiResultModel.Data = await _authService.LogOutAsync(userId, Request);

            return Ok(apiResultModel);
        }

        [HttpGet]
        [Route("forgot-password")]
        [ActionName(TextResourceKey.Action_Update)]
        public async Task<ActionResult<ApiResultModel>> ForgotPassword([FromQuery] string email, string otp)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await _authService.ForgotPasswordAsync(email, otp);

            return Ok(apiResultModel);
        }

        [HttpGet]
        [Route("get-otp")]
        [ActionName(NTSConstants.NoLogEvent)]
        public async Task<ActionResult<ApiResultModel>> GetOTP([FromQuery] string email)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await _authService.GetOTPAsync(email);

            return Ok(apiResultModel);
        }
    }
}
