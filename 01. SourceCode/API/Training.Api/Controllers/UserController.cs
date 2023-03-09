using Microsoft.AspNetCore.Mvc;
using NTS.Common;
using NTS.Common.Attributes;
using NTS.Common.Models;
using NTS.Common.Resource;
using NTSCommon.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Api;
using Training.Api.Attributes;
using Training.Api.Controllers;
using Training.Models.Models.GroupFunction;
using Training.Models.Models.User;
using Training.Services.Users;

namespace PCMT.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    [ValidateModel]
    [ApiHandleExceptionSystem]
    [Logging]
    [NTSAuthorize]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// Tìm kiếm user
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("search")]
        [ActionName(TextResourceKey.Action_Search)]
        [AllowPermission(Permissions = "F000950")]
        public async Task<ActionResult<SearchBaseResultModel<UserSearchResultModel>>> SearchUser([FromBody] UserSearchModel searchModel)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await _userService.SearchUserAsync(searchModel);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Thêm mới người dùng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [ActionName(TextResourceKey.Action_Create)]
        [AllowPermission(Permissions = "F000951")]
        public async Task<ActionResult<ApiResultModel>> CreateUser([FromBody] UserCreateModel model)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            await _userService.CreateUserAsync(model, GetUserIdByRequest());

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Cập nhật tài khoản
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPut]
        [Route("update/{id}")]
        [ActionName(TextResourceKey.Action_Update)]
        [AllowPermission(Permissions = "F000952")]
        public async Task<ActionResult<ApiResultModel>> UpdateUser([FromRoute] string id, [FromBody] UserCreateModel model)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            await _userService.UpdateUserAsync(id, model, GetUserIdByRequest());

            return Ok(apiResultModel);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [ActionName(TextResourceKey.Action_Delete)]
        [AllowPermission(Permissions = "F000953")]
        public async Task<ActionResult<ApiResultModel>> DeleteUser(string id)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            await _userService.DeleteUserAsync(id, GetUserIdByRequest());

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Lấy dữ liệu tài khoản admin theo id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-user-by-id/{userId}")]
        [ActionName(TextResourceKey.Action_Get)]
        [AllowPermission(Permissions = "F000952;F000954")]
        public async Task<ActionResult<ApiResultModel<UserCreateModel>>> GetUserById([FromRoute] string userId)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await _userService.GetUserByIdAsnyc(userId);

            return Ok(apiResultModel);
        }

        [HttpGet]
        [Route("get-permission")]
        [AllowPermission(Permissions = "F000951;F000952")]
        [ActionName(NTSConstants.NoLogEvent)]
        public async Task<ActionResult<ApiResultModel<List<GroupFunctionModel>>>> GetPermission()
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await _userService.GetPermissionAsync();

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Khóa tài khoản
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("lock/{userId}")]
        [ActionName(TextResourceKey.Action_User_Lock)]
        [AllowPermission(Permissions = "F000956")]
        public async Task<ActionResult<ApiResultModel>> UserLock(string userId)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            await _userService.UserLockAsync(userId);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Reset mật khẩu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("reset-pass/{id}")]
        [ActionName(TextResourceKey.Action_User_ResetPass)]
        [AllowPermission(Permissions = "F000955")]
        public async Task<ActionResult<ApiResultModel>> ResetPassword([FromRoute] string id)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            await _userService.ResetPasswordAsync(id);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("change-pass")]
        [ActionName(TextResourceKey.Action_User_ChangePass)]
        public async Task<ActionResult<ApiResultModel>> ResetPassword([FromBody] ChangePasswordModel model)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            var userId = GetUserIdByRequest();
            await _userService.ChangePasswordAsync(userId, model);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Cập nhật tài khoản
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-info/{id}")]
        [ActionName(TextResourceKey.Action_Update)]
        public async Task<ActionResult<ApiResultModel>> UpdateUserInfo([FromRoute] string id, [FromBody] UserInfoModel model)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            await _userService.UpdateUserInfoAsync(id, model, GetUserIdByRequest());

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Lấy quyền theo nhóm Id
        /// </summary>
        /// <param name="groupUserId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-group-permission/{groupUserId}")]
        [AllowPermission(Permissions = "F000951;F000952")]
        [ActionName(NTSConstants.NoLogEvent)]
        public async Task<ActionResult<ApiResultModel>> GetGroupPermissionById([FromRoute] string groupUserId)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await _userService.GetGroupPermissionByIdAsync(groupUserId);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Lấy dữ liệu tài khoản admin theo id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-user-by-info/{userId}")]
        [ActionName(TextResourceKey.Action_Get)]
        public async Task<ActionResult<ApiResultModel<UserCreateModel>>> GetUserInfo([FromRoute] string userId)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await _userService.GetUserInfoAsnyc(userId);

            return Ok(apiResultModel);
        }
    }
}