using Foundatio.Caching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NTS.Common;
using NTS.Common.RedisCache;
using NTS.Common.Resource;
using NTS.Common.Utils;
using NTS.Document.Excel;
using NTS.Document.Word;
using NTSCommon.Models;
using Training.Models.Entities;
using Training.Models.Models.Function;
using Training.Models.Models.GroupFunction;
using Training.Models.Models.User;
using Training.Services.Combobox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Training.Services.Auth;

namespace Training.Services.Users
{
    public class UserService : IUserService
    {
        private readonly TrainingContext _sqlContext;
        private readonly RedisCacheSettingModel _redisCacheSetting;
        private readonly ICacheClient _cacheClient;
        private readonly IAuthService _authService;
        private readonly IComboboxService _comboboxService;
        private readonly IWordService _wordService;
        private readonly IExcelService _excelService;

        public UserService(TrainingContext sqlContext, IOptions<RedisCacheSettingModel> redisOptions, ICacheClient cacheClient, IAuthService authService, IComboboxService comboboxService, IWordService wordService, IExcelService excelService)
        {
            this._sqlContext = sqlContext;
            this._redisCacheSetting = redisOptions?.Value;
            this._cacheClient = cacheClient;
            this._authService = authService;
            _comboboxService = comboboxService;
            _wordService = wordService;
            _excelService = excelService;
        }

        /// <summary>
        /// Tìm kiếm user quản trị
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public async Task<SearchBaseResultModel<UserSearchResultModel>> SearchUserAsync(UserSearchModel searchModel)
        {
            SearchBaseResultModel<UserSearchResultModel> searchResult = new SearchBaseResultModel<UserSearchResultModel>();

            var dataQuery = (from a in _sqlContext.User.AsNoTracking()
                             orderby a.FullName
                             select new UserSearchResultModel()
                             {
                                 Id = a.Id,
                                 UserName = a.UserName,
                                 FullName = a.FullName,
                                 Email = a.Email,
                                 PhoneNumber = a.PhoneNumber,
                                 Status = a.Status,
                                 Description = a.Description,
                             }).AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.UserName))
            {
                dataQuery = dataQuery.Where(a => a.UserName.ToUpper().Contains(searchModel.UserName.ToUpper()));
            }

            if (!string.IsNullOrEmpty(searchModel.FullName))
            {
                dataQuery = dataQuery.Where(a => a.FullName.ToUpper().Contains(searchModel.FullName.ToUpper()));
            }

            if (searchModel.Status.HasValue)
            {
                dataQuery = dataQuery.Where(a => a.Status == searchModel.Status);
            }

            searchResult.TotalItems = dataQuery.Count();

            if (searchModel.GetAllData)
            {
                searchResult.DataResults = dataQuery.ToList();
            }
            else
            {
                searchResult.DataResults = dataQuery.Skip((searchModel.PageNumber - 1) * searchModel.PageSize).Take(searchModel.PageSize).ToList();
            }

            return searchResult;
        }

        /// <summary>
        /// Thêm mới user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> CreateUserAsync(UserCreateModel model, string userId)
        {
            var userName = _sqlContext.User.AsNoTracking().FirstOrDefault(a => a.UserName.Equals(model.UserName));
            if (userName != null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0002, TextResourceKey.Account);
            }

            var canbo = _sqlContext.User.AsNoTracking().FirstOrDefault(a => !string.IsNullOrEmpty(a.IdCanBo) && a.IdCanBo.Equals(model.IdCanBo));
            if (canbo != null)
            {
                throw NTSException.CreateInstance("Cán bộ đã có tài khoản!");
            }

            var email = _sqlContext.User.AsNoTracking().FirstOrDefault(a => a.Email.Equals(model.Email) && !string.IsNullOrEmpty(model.Email));
            if (email != null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0002, TextResourceKey.Email);
            }

            var phoneNumber = _sqlContext.User.AsNoTracking().FirstOrDefault(a => a.PhoneNumber.Equals(model.PhoneNumber) && !string.IsNullOrEmpty(model.PhoneNumber));
            if (phoneNumber != null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0002, TextResourceKey.PhoneNumber);
            }

            //var hasNumber = new Regex(@"[0-9]+");
            //var hasUpperChar = new Regex(@"[A-Z]+");
            //var hasLowerChar = new Regex(@"[a-z]+");
            //var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            //var isValidated = hasNumber.IsMatch(model.Password) && hasUpperChar.IsMatch(model.Password) && hasLowerChar.IsMatch(model.Password) && hasSymbols.IsMatch(model.Password);
            //if (isValidated == false)
            //{
            //    throw NTSException.CreateInstance(MessageResourceKey.MSG0045, TextResourceKey.User);
            //}

            User user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName.NTSTrim(),
                IdCanBo = model.IdCanBo,
                FullName = model.FullName.NTSTrim(),
                Anh = model.Anh,
                Email = model.Email.NTSTrim(),
                PhoneNumber = model.PhoneNumber.NTSTrim(),
                Password = PasswordUtils.CreatePasswordHash(),
                Status = model.Status.Value,
                Description = model.Description.NTSTrim(),
                GroupId = model.GroupId,
                CreateBy = userId,
                CreateDate = DateTime.Now,
                UpdateBy = userId,
                UpdateDate = DateTime.Now,
                IdDonVi = model.IdDonVi,
            };

            user.PasswordHash = PasswordUtils.ComputeHash($"{(!string.IsNullOrEmpty(model.Password.NTSTrim()) ? model.Password : StringHelper.NTSTrim(NTSConstants.PassWord))}{user.Password}");
            _sqlContext.User.Add(user);

            // Thêm mới bảng quyền
            List<UserPermission> userPermissions = new List<UserPermission>();
            UserPermission userPermission;

            foreach (var item in model.ListGroupFunction)
            {
                foreach (var per in item.Permissions)
                {
                    if (per.IsChecked)
                    {
                        userPermission = new UserPermission
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserId = user.Id,
                            FunctionId = per.Id,
                        };

                        userPermissions.Add(userPermission);
                    }
                }
            }

            using (var trans = _sqlContext.Database.BeginTransaction())
            {
                try
                {
                    _sqlContext.UserPermission.AddRange(userPermissions);
                    _sqlContext.SaveChanges();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    _sqlContext.ChangeTracker.Clear();
                    throw ex;
                }
            }

            return user.Id;
        }

        /// <summary>
        /// Cập nhật user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateUserAsync(string id, UserCreateModel model, string userId = null)
        {
            var userName = _sqlContext.User.AsNoTracking().FirstOrDefault(a => !a.Id.Equals(id) && a.UserName.Equals(model.UserName.NTSTrim()));
            if (userName != null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0002, TextResourceKey.Account);
            }

            var canbo = _sqlContext.User.AsNoTracking().FirstOrDefault(a => !string.IsNullOrEmpty(a.IdCanBo) && !a.Id.Equals(id) && a.IdCanBo.Equals(model.IdCanBo));
            if (canbo != null)
            {
                throw NTSException.CreateInstance("Cán bộ đã có tài khoản!");
            }

            var email = _sqlContext.User.AsNoTracking().FirstOrDefault(a => !a.Id.Equals(id) && a.Email.Equals(model.Email) && !string.IsNullOrEmpty(model.Email));
            if (email != null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0002, TextResourceKey.Email);
            }

            var phoneNumber = _sqlContext.User.AsNoTracking().FirstOrDefault(a => !a.Id.Equals(id) && a.PhoneNumber.Equals(model.PhoneNumber) && !string.IsNullOrEmpty(model.PhoneNumber));
            if (phoneNumber != null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0002, TextResourceKey.PhoneNumber);
            }

            var user = _sqlContext.User.FirstOrDefault(i => i.Id.Equals(id));
            if (user == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.User);
            }

            using (var trans = _sqlContext.Database.BeginTransaction())
            {
                user.UserName = model.UserName.NTSTrim();
                user.IdCanBo = model.IdCanBo;
                user.FullName = model.FullName.NTSTrim();
                user.Anh = model.Anh;
                user.Email = model.Email.NTSTrim();
                user.PhoneNumber = model.PhoneNumber.NTSTrim();
                user.Status = model.Status.Value;
                user.Description = model.Description.NTSTrim();
                user.GroupId = model.GroupId;
                user.UpdateBy = userId;
                user.UpdateDate = DateTime.Now;
                user.IdDonVi = model.IdDonVi;

                var userPer = _sqlContext.UserPermission.Where(r => r.UserId.Equals(id)).ToList();
                if (userPer != null)
                {
                    _sqlContext.UserPermission.RemoveRange(userPer);
                }

                // Thêm mới bảng quyền

                List<UserPermission> userPermissions = new List<UserPermission>();

                UserPermission addPer;

                foreach (var item in model.ListGroupFunction)
                {
                    foreach (var per in item.Permissions)
                    {
                        if (per.IsChecked)
                        {
                            addPer = new UserPermission
                            {
                                Id = Guid.NewGuid().ToString(),
                                UserId = user.Id,
                                FunctionId = per.Id,
                            };

                            userPermissions.Add(addPer);
                        }
                    }
                }
                _sqlContext.UserPermission.AddRange(userPermissions);

                // Key lưu cache login
                _authService?.RemoveRedis(id);

                try
                {
                    _sqlContext.SaveChanges();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    _sqlContext.ChangeTracker.Clear();
                    throw ex;
                }
            }

        }

        /// <summary>
        /// Khóa tài khoản
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UserLockAsync(string id)
        {
            var user = _sqlContext.User.FirstOrDefault(r => r.Id.Equals(id));

            if (user == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.User);
            }

            if (user != null)
            {
                user.Status = user.Status == NTSConstants.Lock ? NTSConstants.UnLock : NTSConstants.Lock;
            }

            _authService.RemoveRedis(id);

            _sqlContext.SaveChanges();
        }

        /// <summary>
        /// Lấy thông tin tài khoản
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<UserCreateModel> GetUserByIdAsnyc(string UserId)
        {
            // Lấy thông tin user
            var result = (from a in _sqlContext.User.AsNoTracking()
                          where a.Id.Equals(UserId)
                          select new UserCreateModel()
                          {
                              Id = a.Id,
                              UserName = a.UserName,
                              IdCanBo = a.IdCanBo,
                              FullName = a.FullName,
                              Anh = a.Anh,
                              Email = a.Email,
                              PhoneNumber = a.PhoneNumber,
                              Status = a.Status,
                              Description = a.Description,
                              GroupId = a.GroupId,
                              IdDonVi = a.IdDonVi,
                          }).FirstOrDefault();

            if (result == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.User);
            }

            // Lấy thông tin quyền
            List<GroupFunctionModel> groupFunctions = new List<GroupFunctionModel>();

            var gFunctions = _sqlContext.GroupFunction.AsNoTracking().OrderBy(o => o.Index).ToList();

            var userPermissions = (from u in _sqlContext.UserPermission.AsNoTracking()
                                   where u.UserId.Equals(result.Id)
                                   select u.FunctionId).ToList();

            var permissions = (from p in _sqlContext.Function.AsEnumerable()
                               join u in userPermissions on p.Id equals u into pu
                               from pun in pu.DefaultIfEmpty()
                               orderby p.Code
                               select new FunctionModel
                               {
                                   Id = p.Id,
                                   Name = p.Name,
                                   Code = p.Code,
                                   IsChecked = pun != null ? true : false,
                                   GroupFunctionId = p.GroupFunctionId
                               }).ToList();

            GroupFunctionModel paramModel = new GroupFunctionModel();

            foreach (var ite in gFunctions)
            {
                paramModel = new GroupFunctionModel
                {
                    Id = ite.Id,
                    Name = ite.Name,
                    Permissions = permissions.Where(t => t.GroupFunctionId.Equals(ite.Id)).ToList()
                };
                paramModel.PermissionTotal = paramModel.Permissions.Count;
                paramModel.CheckCount = paramModel.Permissions.Count(r => r.IsChecked);
                paramModel.IsChecked = paramModel.PermissionTotal == paramModel.CheckCount ? true : paramModel.CheckCount == 0 ? false : null;
                groupFunctions.Add(paramModel);
            }

            // Nếu chọn nhóm quyền thì chỉ load các nhóm đó
            if (!string.IsNullOrEmpty(result.GroupId))
            {
                var listGroupFunction = (from a in _sqlContext.GroupPermission.AsNoTracking()
                                         where a.GroupId.Equals(result.GroupId)
                                         join b in _sqlContext.Function.AsNoTracking() on a.FunctionId equals b.Id
                                         orderby b.GroupFunctionId
                                         select new { b.GroupFunctionId }).GroupBy(i => i.GroupFunctionId).Select(i => i.Key).ToList();
                groupFunctions = groupFunctions.Where(i => listGroupFunction.Contains(i.Id)).ToList();
            }

            result.ListGroupFunction = groupFunctions;

            return result;
        }

        /// <summary>
        /// Xóa user
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteUserAsync(string id, string userid)
        {
            var user = _sqlContext.User.FirstOrDefault(i => i.Id.Equals(id));
            var userPermission = _sqlContext.UserPermission.Where(a => a.UserId.Equals(id)).ToList();

            if (user == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.User);
            }

            using (var trans = _sqlContext.Database.BeginTransaction())
            {
                if (userPermission.Count > 0)
                {
                    _sqlContext.RemoveRange(userPermission);
                }

                if (user != null)
                {
                    _sqlContext.User.Remove(user);
                }

                _authService?.RemoveRedis(id);
                try
                {
                    _sqlContext.SaveChanges();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    _sqlContext.ChangeTracker.Clear();
                    throw ex;
                }
            }
        }

        ///// <summary>
        ///// Lấy quyền của nhóm theo id
        ///// </summary>
        ///// <param name="groupUserId"></param>
        ///// <returns></returns>
        //public List<PermissionsModel> GetGroupPermissionById(string groupUserId)
        //{
        //    var result = sqlContext.GroupPermission.AsNoTracking().Where(r => r.GroupUserId.Equals(groupUserId)).Select(
        //        s => new PermissionsModel
        //        {
        //            Id = s.PermissionId
        //        }).ToList();

        //    return result;
        //}

        /// <summary>
        /// Lấy quyền danh sách quyền
        /// </summary>
        /// <returns></returns>
        public async Task<List<GroupFunctionModel>> GetPermissionAsync()
        {

            List<GroupFunctionModel> groupFunctions = new List<GroupFunctionModel>();

            var gFunctions = _sqlContext.GroupFunction.AsNoTracking().OrderBy(o => o.Index).ToList();

            var permissions = (from p in _sqlContext.Function.AsNoTracking()
                               orderby p.Code
                               select new FunctionModel
                               {
                                   Id = p.Id,
                                   Name = p.Name,
                                   Code = p.Code,
                                   IsChecked = false,
                                   GroupFunctionId = p.GroupFunctionId,
                                   FunctionId = p.Id
                               }).ToList();

            GroupFunctionModel paramModel = new GroupFunctionModel();

            foreach (var ite in gFunctions)
            {
                paramModel = new GroupFunctionModel();
                paramModel.Id = ite.Id;
                paramModel.Name = ite.Name;
                paramModel.Permissions = permissions.Where(t => t.GroupFunctionId.Equals(ite.Id)).ToList();
                paramModel.PermissionTotal = paramModel.Permissions.Count;
                paramModel.IsChecked = paramModel.Permissions.Count(r => !r.IsChecked) == 0;
                paramModel.CheckCount = paramModel.Permissions.Count(r => r.IsChecked);
                groupFunctions.Add(paramModel);
            }

            return groupFunctions;
        }

        /// <summary>
        /// Reset mật khẩu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task ResetPasswordAsync(string id)
        {
            var user = _sqlContext.User.FirstOrDefault(r => r.Id.Equals(id));

            if (user == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.User);
            }

            user.PasswordHash = PasswordUtils.ComputeHash(StringHelper.NTSTrim(NTSConstants.PassWord) + user.Password);

            _sqlContext.SaveChanges();
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task ChangePasswordAsync(string userId, ChangePasswordModel model)
        {
            var user = _sqlContext.User.FirstOrDefault(r => r.Id.Equals(userId));

            if (user == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.User);
            }

            var passwordHash = PasswordUtils.ComputeHash(model.OldPassword + user.Password);
            if (!user.PasswordHash.Equals(passwordHash))
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0013);
            }

            user.PasswordHash = PasswordUtils.ComputeHash(model.NewPassword + user.Password);

            _sqlContext.SaveChangesAsync();

        }

        /// <summary>
        /// Lấy thông tin tài khoản
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserInfoModel> GetUserInfoAsnyc(string userId)
        {
            // Lấy thông tin user
            var result = await (from a in _sqlContext.User.AsNoTracking()
                                where a.Id.Equals(userId)
                                select new UserInfoModel()
                                {
                                    Id = a.Id,
                                    UserName = a.UserName,
                                    FullName = a.FullName,
                                    Anh = a.Anh,
                                    Email = a.Email,
                                    PhoneNumber = a.PhoneNumber,
                                }).FirstOrDefaultAsync();

            if (result == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.User);
            }

            return result;
        }

        /// <summary>
        /// Cập nhật người dùng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateUserInfoAsync(string id, UserInfoModel model, string userId)
        {
            var userName = _sqlContext.User.AsNoTracking().FirstOrDefault(a => !a.Id.Equals(id) && a.UserName.Equals(model.UserName.NTSTrim()));

            if (userName != null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0002, TextResourceKey.Account);
            }

            var user = _sqlContext.User.FirstOrDefault(i => i.Id.Equals(id));
            if (user == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.User);
            }

            //if (!string.IsNullOrEmpty(model.IdChucVu))
            //{
            //    var canBo = _sqlContext.CanBo.FirstOrDefault(i => i.UserId.Equals(id));
            //    if (canBo != null)
            //    {
            //        canBo.IdChucVu = model.IdChucVu;
            //    }
            //}

            using (var trans = _sqlContext.Database.BeginTransaction())
            {
                user.UserName = model.UserName.NTSTrim();
                user.FullName = model.FullName.NTSTrim();
                user.Email = model.Email.NTSTrim();
                user.PhoneNumber = model.PhoneNumber.NTSTrim();

                user.UpdateBy = userId;
                user.UpdateDate = DateTime.Now;

                try
                {
                    await _sqlContext.SaveChangesAsync();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Lấy quyền của nhóm theo id
        /// </summary>
        /// <param name="groupUserId"></param>
        /// <returns></returns>
        public async Task<List<FunctionModel>> GetGroupPermissionByIdAsync(string groupUserId)
        {
            var result = await _sqlContext.GroupPermission.AsNoTracking().Where(r => r.GroupId.Equals(groupUserId)).Select(
                s => new FunctionModel
                {
                    Id = s.FunctionId
                }).ToListAsync();

            return result;
        }

        /// <summary>
        /// Xuất danh sách người dùng
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="pathTemplate"></param>
        /// <param name="optionExport"></param>
        /// <returns></returns>
        public MemoryStream ExportFileAsync(UserSearchModel searchModel, string pathTemplate, NTSConstants.OptionExport optionExport)
        {
            SearchBaseResultModel<UserSearchResultModel> searchResult = SearchUserAsync(searchModel).Result;

            var dataExport = searchResult.DataResults.UpdateIndex("Index").Select(s => new
            {
                s.Index,
                s.TinhTrang,
                s.UserName,
                s.FullName,
                s.Description,
            }).ToList();

            DateTime dateNow = DateTime.Now;
            Dictionary<string, string> param = new Dictionary<string, string>();

            param.Add("<dd>", dateNow.ToStringDD());
            param.Add("<MM>", dateNow.ToStringMM());
            param.Add("<yyyy>", dateNow.ToStringYYYY());

            MemoryStream streamFile = null;
            if (optionExport == NTSConstants.OptionExport.Excel)
                streamFile = _excelService.ExportExcel(dataExport, pathTemplate, 5, param);
            else if (optionExport == NTSConstants.OptionExport.Pdf)
                streamFile = _excelService.ExportPdf(dataExport, pathTemplate, 5, param);

            return streamFile;
        }
    }
}
