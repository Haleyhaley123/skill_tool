using Foundatio.Caching;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NTS.Common.Cache;
using NTS.Common.RedisCache;
using NTS.Common.Resource;
using NTS.Common.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NTS.Common.Users
{
    public class NtsUserService : INtsUserService
    {
        private readonly ICacheClient _cacheClient;
        private readonly RedisCacheSettingModel _redisCacheSettings;
        public NtsUserService(ICacheClient cacheClient, IOptions<RedisCacheSettingModel> options)
        {
            _cacheClient = cacheClient;
            _redisCacheSettings = options.Value;
        }

        /// <summary>
        /// Đăng suất
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task Logout(string userId)
        {
            // Tạo key cache
            string key = $"{_redisCacheSettings.PrefixSystemKey}{_redisCacheSettings.PrefixLoginKey}{userId}";

            // Kiểm tra cache tồn tại
            var keyExist = await _cacheClient.ExistsAsync(key);
            if (keyExist)
            {
                await _cacheClient.RemoveAsync(key);
            }

        }

        /// <summary>
        /// Đăng nhập JWT
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public async Task<NtsUserTokenModel> NtsJwtLogin(NtsUserLoginModel loginModel)
        {
            NtsUserTokenModel userTokenModel = new NtsUserTokenModel();
            var passwordHash = PasswordUtils.ComputeHash(loginModel.Password + loginModel.SecurityStamp);
            if (!loginModel.PasswordHash.Equals(passwordHash))
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0012);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(loginModel.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, loginModel.UserId.ToString()),
                    new Claim("Permissions",string.Join(",", loginModel.Permission)),
                }),
                Expires = DateTime.UtcNow.AddDays(loginModel.ExpireDateAfter),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            userTokenModel.Name = loginModel.UserName;
            userTokenModel.FullName = loginModel.FullName;
            userTokenModel.UserId = loginModel.UserId;
            userTokenModel.ImageLink = loginModel.ImageLink;
            userTokenModel.Token = tokenString;
            userTokenModel.ExpireDateAfter = loginModel.ExpireDateAfter;
            userTokenModel.Permissions = loginModel.Permission;
            //userTokenModel.RefreshToken = await GenerateRefreshToken();

            // Key lưu cache login
            string keyLogin = $"{_redisCacheSettings.PrefixSystemKey}{_redisCacheSettings.PrefixLoginKey}{userTokenModel.UserId}";

            // Ghi thông tin vào cache
            await _cacheClient.RemoveAsync(keyLogin);
            await _cacheClient.AddAsync<NtsUserTokenModel>(keyLogin, userTokenModel, new TimeSpan(loginModel.ExpireDateAfter, 0, 0, 0));

            return userTokenModel;
        }

        public async Task<NTSUserRefreshTokenModel> GenerateRefreshToken()
        {
            // generate token that is valid for 7 days
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            var refreshToken = new NTSUserRefreshTokenModel
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
            };

            return refreshToken;
        }

        public string ValidateJwtToken(string token, string secret)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
