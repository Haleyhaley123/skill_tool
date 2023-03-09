using NTS.Common;
using NTS.Common.Utils;
using Training.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Training.Models.Entities;

namespace Training.Api.AppInitialize
{
    public static class InitUser
    {
        public static void Init(TrainingContext _sqlContext)
        {
            if (!_sqlContext.User.Any())
            {
                using (var trans = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        User user = new User()
                        {
                            Id = NTSConstants.IdUserAdminFix,
                            UserName = "admin",
                            FullName = "Admin",
                            Status = 1,
                            Password = PasswordUtils.CreatePasswordHash(),
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            IdDonVi = "100000",
                        };
                        user.PasswordHash = PasswordUtils.ComputeHash(NTSConstants.PassWord + user.Password);

                        _sqlContext.User.Add(user);
                        _sqlContext.SaveChanges();

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
