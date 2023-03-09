using NTS.Common;
using NTSCommon.Models;
using Training.Models.Models.Function;
using Training.Models.Models.GroupFunction;
using Training.Models.Models.User;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Training.Services.Users
{
    public interface IUserService
    {
        /// <summary>
        /// Tìm kiếm user quản trị
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        Task<SearchBaseResultModel<UserSearchResultModel>> SearchUserAsync(UserSearchModel searchModel);

        /// <summary>
        /// Thêm mới user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> CreateUserAsync(UserCreateModel model, string userId = null);

        /// <summary>
        /// Cập nhật user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task UpdateUserAsync(string id, UserCreateModel model, string userId = null);

        /// <summary>
        /// Khóa tài khoản
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task UserLockAsync(string id);

        /// <summary>
        /// Lấy thông tin tài khoản
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserCreateModel> GetUserByIdAsnyc(string userId = null);

        /// <summary>
        /// Xóa user
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task DeleteUserAsync(string id, string userId = null);

        /// <summary>
        /// Lấy quyền danh sách quyền
        /// </summary>
        /// <returns></returns>
        Task<List<GroupFunctionModel>> GetPermissionAsync();

        /// <summary>
        /// Reset mật khẩu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task ResetPasswordAsync(string id);

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task ChangePasswordAsync(string userId, ChangePasswordModel model);

        /// <summary>
        /// Lấy thông tin tài khoản
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserInfoModel> GetUserInfoAsnyc(string userId);

        /// <summary>
        /// Cập nhật người dùng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task UpdateUserInfoAsync(string id, UserInfoModel model, string userId);

        /// <summary>
        /// Lấy quyền của nhóm theo id
        /// </summary>
        /// <param name="groupUserId"></param>
        /// <returns></returns>
        Task<List<FunctionModel>> GetGroupPermissionByIdAsync(string groupUserId);

        /// <summary>
        /// Xuất danh sách người dùng
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="pathTemplate"></param>
        /// <param name="optionExport"></param>
        /// <returns></returns>
        MemoryStream ExportFileAsync(UserSearchModel searchModel, string pathTemplate, NTSConstants.OptionExport optionExport);
    }
}
