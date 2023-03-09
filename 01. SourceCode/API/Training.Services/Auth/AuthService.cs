using Foundatio.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NTS.Common;
using NTS.Common.Models;
using NTS.Common.RedisCache;
using NTS.Common.Resource;
using NTS.Common.Users;
using NTS.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Training.Models.Entities;
using Training.Models.Models.UserHistory;
using Training.Services.Log;

namespace Training.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly TrainingContext _sqlContext;
        private readonly AppSettingModel _appSettingModel;
        private readonly INtsUserService _ntsUserService;
        private readonly ICacheClient _cacheClient;
        private readonly RedisCacheSettingModel _redisCacheSettingModel;
        private readonly ILogEventService _logEventService;

        public AuthService(TrainingContext sqlContext, INtsUserService ntsUserService,
            IOptions<AppSettingModel> options, IOptions<RedisCacheSettingModel> redisOptions, ICacheClient cacheClient, ILogEventService logEventService)
        {
            _sqlContext = sqlContext;
            _appSettingModel = options.Value;
            _ntsUserService = ntsUserService;
            _cacheClient = cacheClient;
            _redisCacheSettingModel = redisOptions.Value;
            _logEventService = logEventService;
        }

        public async Task<NtsUserTokenModel> LoginAsync(NtsLogInModel loginModel, HttpRequest request)
        {
            var user = (from u in _sqlContext.User.AsNoTracking()
                        where u.UserName.Equals(loginModel.Username)
                        select new NtsUserLoginModel
                        {
                            UserId = u.Id,
                            UserName = u.UserName,
                            FullName = u.FullName,
                            PasswordHash = u.PasswordHash,
                            SecurityStamp = u.Password,
                            Password = loginModel.Password,
                            Status = u.Status,
                        }).FirstOrDefault();

            if (user == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0035);
            }

            if (user.Status == NTSConstants.UnLock)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0029, TextResourceKey.Account);
            }

            user.Permission = GetListPermission(user.UserId);

            user.ExpireDateAfter = _appSettingModel.ExpireDateAfter;
            user.Secret = _appSettingModel.Secret;

            var userToken = await _ntsUserService.NtsJwtLogin(user);

            UserHistoryModel userHistoryModel = new UserHistoryModel()
            {
                Name = "Đăng nhập hệ thống",
                Content = JsonConvert.SerializeObject(loginModel),
                Type = NTSConstants.UserHistory_Type_Login
            };
            _logEventService.LogEventAsync(request, userHistoryModel, user.UserId);
            _sqlContext.SaveChanges();

            return userToken;
        }

        public List<string> GetListPermission(string userId)
        {
            List<string> listPermission = new List<string>();

            var userPermission = (from a in _sqlContext.UserPermission.AsNoTracking()
                                  join b in _sqlContext.Function.AsNoTracking() on a.FunctionId equals b.Id
                                  where a.UserId.Equals(userId)
                                  select new { b.Code, b.ScreenCode }).ToList();

            listPermission = userPermission.Select(s => s.Code).ToList();
            listPermission.AddRange(userPermission.Where(r => !string.IsNullOrEmpty(r.ScreenCode)).GroupBy(g => g.ScreenCode).Select(s => s.Key).ToList());

            return listPermission;
        }

        //public string GetById(string id)
        //{
        //    var user = _sqlContext.Users.FirstOrDefault(t => t.Id.Equals(id));
        //    if (user == null)
        //    {
        //        throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.User);
        //    }
        //    return user.Id;
        //}

        /// <summary>
        /// Đăng xuất hệ thống
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> LogOutAsync(string userId, HttpRequest request)
        {
            await _ntsUserService.Logout(userId);

            UserHistoryModel userHistoryModel = new UserHistoryModel()
            {
                Name = "Đăng xuất hệ thống",
                Type = NTSConstants.UserHistory_Type_Login
            };
            _logEventService.LogEventAsync(request, userHistoryModel, userId);
            _sqlContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// Check token userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsTokenAlive(string userId)
        {
            // Key lưu cache login
            string keyLogin = $"{_redisCacheSettingModel.PrefixSystemKey}{_redisCacheSettingModel.PrefixLoginKey}{userId}";
            bool isToken = false;

            try
            {
                isToken = _cacheClient.ExistsAsync(keyLogin).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isToken;
        }

        /// <summary>
        /// Xóa rediscache theo userId
        /// </summary>
        /// <param name="userId"></param>
        public void RemoveRedis(string userId)
        {
            // Key lưu cache login
            string keyLogin = $"{_redisCacheSettingModel.PrefixSystemKey}{_redisCacheSettingModel.PrefixLoginKey}{userId}";
            if (_cacheClient.ExistsAsync(keyLogin).Result)
            {
                var IsCheck = _cacheClient.RemoveAsync(keyLogin).Result;
            }
        }

        /// <summary>
        /// Lấy mã OTP
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> GetOTPAsync(string email)
        {
            bool check = true;
            //var userExist = _sqlContext.User.FirstOrDefault(a => a.Email.Equals(email) && !string.IsNullOrEmpty(email));
            //if (userExist == null)
            //{
            //    throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.Email);
            //}

            //string otp = RandomOTP();
            //string keyOTP = $"{_redisCacheSettingModel.PrefixSystemKey}{_redisCacheSettingModel.PrefixLoginKey}{email}";
            //await _cacheClient.AddAsync<string>(keyOTP, otp, new TimeSpan(0, 0, 5, 0));

            //var list = await _sqlContext.SystemParam.ToListAsync();
            //string fromaddr = list.FirstOrDefault(i => i.ParamName.ToUpper().Equals(NTSConstants.SystemParam_SP01.ToUpper()))?.ParamValue;
            //string toaddr = email;
            //string passwordEmail = list.FirstOrDefault(i => i.ParamName.ToUpper().Equals(NTSConstants.SystemParam_SP02.ToUpper()))?.ParamValue;
            //string text = list.FirstOrDefault(i => i.ParamName.ToUpper().Equals(NTSConstants.SystemParam_SP03.ToUpper()))?.ParamValue;

            //MailMessage msg = new MailMessage();
            //msg.Subject = "Thông tin mật khẩu";
            //msg.From = new MailAddress(fromaddr);
            //msg.Body = string.Format("Mã OTP: {0}", otp);
            //msg.To.Add(new MailAddress(toaddr));
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.Port = 587;
            //smtp.UseDefaultCredentials = false;
            //smtp.EnableSsl = true;
            //NetworkCredential nc = new NetworkCredential(fromaddr, passwordEmail);
            //smtp.Credentials = nc;

            //try
            //{
            //    smtp.Send(msg);
            //}
            //catch (SmtpFailedRecipientException ex)
            //{
            //    check = false;
            //}

            return check;
        }

        /// <summary>
        /// Quên mật khẩu
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> ForgotPasswordAsync(string email, string otp)
        {
            bool check = true;
            //var userExist = _sqlContext.User.FirstOrDefault(a => a.Email.Equals(email) && !string.IsNullOrEmpty(email));
            //if (userExist == null)
            //{
            //    throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.Email);
            //}

            //string keyOTP = $"{_redisCacheSettingModel.PrefixSystemKey}{_redisCacheSettingModel.PrefixLoginKey}{email}";
            //if (_cacheClient.ExistsAsync(keyOTP).Result)
            //{
            //    var otpExist = await _cacheClient.GetAsync<string>(keyOTP);
            //    if (!otpExist.Value.Equals(otp))
            //    {
            //        throw NTSException.CreateInstance(MessageResourceKey.MSG0017, TextResourceKey.OTP);
            //    }
            //}

            //string password = RandomPassword();
            //userExist.PasswordHash = PasswordUtils.ComputeHash(password + userExist.Password);
            //_sqlContext.SaveChanges();

            //var list = await _sqlContext.SystemParam.ToListAsync();

            //string fromaddr = list.FirstOrDefault(i => i.ParamName.ToUpper().Equals(NTSConstants.SystemParam_SP01.ToUpper()))?.ParamValue;
            //string toaddr = email;
            //string passwordEmail = list.FirstOrDefault(i => i.ParamName.ToUpper().Equals(NTSConstants.SystemParam_SP02.ToUpper()))?.ParamValue;
            //string text = list.FirstOrDefault(i => i.ParamName.ToUpper().Equals(NTSConstants.SystemParam_SP03.ToUpper()))?.ParamValue;

            //MailMessage msg = new MailMessage();
            //msg.Subject = "Thông tin mật khẩu";
            //msg.From = new MailAddress(fromaddr);
            //msg.Body = string.Format(text, password);
            //msg.To.Add(new MailAddress(toaddr));
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.Port = 587;
            //smtp.UseDefaultCredentials = false;
            //smtp.EnableSsl = true;
            //NetworkCredential nc = new NetworkCredential(fromaddr, passwordEmail);
            //smtp.Credentials = nc;

            //try
            //{
            //    smtp.Send(msg);
            //}
            //catch (SmtpFailedRecipientException ex)
            //{
            //    check = false;
            //}

            return check;
        }

        private string RandomPassword()
        {
            string listString = "abcdefghijklmnopqursuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789!@£$%^&*()#€";
            char[] _password = new char[9];
            Random random = new Random();
            for (int counter = 0; counter < 9; counter++)
            {
                _password[counter] = listString[random.Next(listString.Length - 1)];
            }
            string passwordHash = string.Join(null, _password);

            return passwordHash;
        }

        private string RandomOTP()
        {
            string listString = "0123456789";
            char[] _password = new char[6];
            Random random = new Random();
            for (int counter = 0; counter < 6; counter++)
            {
                _password[counter] = listString[random.Next(listString.Length - 1)];
            }
            string passwordHash = string.Join(null, _password);

            return passwordHash;
        }
    }
}
