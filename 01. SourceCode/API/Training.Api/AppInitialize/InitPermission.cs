using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NTS.Common;
using Training.Models.Models.InitData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Training.Models.Entities;

namespace Training.Api.AppInitialize
{
    public class InitPermission
    {
        public static void Init(TrainingContext _sqlContext)
        {
            string pathGroupFunction = Path.Combine(Directory.GetCurrentDirectory(), "InitData/GroupFunction.json");
            if (File.Exists(pathGroupFunction))
            {
                StreamReader stream = new StreamReader(pathGroupFunction);
                string json = stream.ReadToEnd();
                var groupFunctions = JsonConvert.DeserializeObject<StaticPermission>(File.ReadAllText(pathGroupFunction));
                //StaticPermission groupFunctions = JsonConvert.DeserializeObject<StaticPermission>(json);

                var listGroup = _sqlContext.GroupFunction.ToList();
                bool isExist = false;
                List<Function> listFunction;
                GroupFunction group;
                StaticGroupFunction groupFunction;
                StaticFuncfion function;

                foreach (var groupF in listGroup)
                {
                    isExist = false;
                    listFunction = _sqlContext.Function.Where(r => r.GroupFunctionId == groupF.Id).ToList();
                    groupFunction = groupFunctions.Groups.FirstOrDefault(r => groupF.Id.Equals(r.Id));
                    if (groupFunction != null)
                    {
                        foreach (var func in listFunction)
                        {
                            function = groupFunctions.Functions.FirstOrDefault(r => func.Id.Equals(r.Id));
                            if (function == null)
                            {
                                _sqlContext.UserPermission.RemoveRange(_sqlContext.UserPermission.Where(r => r.FunctionId.Equals(func.Id)).ToList());

                                _sqlContext.GroupPermission.RemoveRange(_sqlContext.GroupPermission.Where(r => r.FunctionId.Equals(func.Id)).ToList());

                                _sqlContext.Function.Remove(_sqlContext.Function.FirstOrDefault(r => r.Id.Equals(func.Id)));
                            }
                        }
                    }
                    else
                    {
                        _sqlContext.UserPermission.RemoveRange(from p in _sqlContext.Function
                                                               join u in _sqlContext.UserPermission.AsNoTracking() on p.Id equals u.FunctionId
                                                               where p.GroupFunctionId.Equals(groupF.Id)
                                                               select u);

                        _sqlContext.GroupPermission.RemoveRange(from p in _sqlContext.Function
                                                                join u in _sqlContext.GroupPermission.AsNoTracking() on p.Id equals u.FunctionId
                                                                where p.GroupFunctionId.Equals(groupF.Id)
                                                                select u);

                        _sqlContext.Function.RemoveRange(from p in _sqlContext.Function
                                                         where p.GroupFunctionId.Equals(groupF.Id)
                                                         select p);

                        _sqlContext.GroupFunction.RemoveRange(from p in _sqlContext.GroupFunction
                                                              where p.Id.Equals(groupF.Id)
                                                              select p);
                    }
                }

                _sqlContext.SaveChanges();

                foreach (var groupf in groupFunctions.Groups)
                {
                    group = _sqlContext.GroupFunction.FirstOrDefault(r => r.Id.Equals(groupf.Id));

                    if (group == null)
                    {
                        group = new GroupFunction
                        {
                            Name = groupf.Name,
                            Code = groupf.Code,
                            Index = groupf.Index,
                            Id = groupf.Id
                        };

                        _sqlContext.GroupFunction.Add(group);

                    }
                    else
                    {
                        group.Name = groupf.Name;
                        group.Code = groupf.Code;
                        group.Index = groupf.Index;
                    }
                }

                _sqlContext.SaveChanges();

                Function permission;
                UserPermission userPermission;
                foreach (var fun in groupFunctions.Functions)
                {
                    permission = _sqlContext.Function.FirstOrDefault(r => r.Id.Equals(fun.Id));
                    if (permission == null)
                    {
                        permission = new Function
                        {
                            Code = fun.Code,
                            Id = fun.Id,
                            GroupFunctionId = fun.GroupId,
                            Name = fun.Name,
                            ScreenCode = fun.ScreenCode,
                            Index = fun.Index
                        };

                        _sqlContext.Function.Add(permission);
                    }
                    else
                    {
                        permission.Name = fun.Name;
                        permission.Code = fun.Code;
                        permission.ScreenCode = fun.ScreenCode;
                        permission.GroupFunctionId = fun.GroupId;
                        permission.Index = fun.Index;
                    }

                    var permissionExit = _sqlContext.UserPermission.Where(r => r.UserId.Equals(NTSConstants.IdUserAdminFix) && r.FunctionId.Equals(fun.Id)).FirstOrDefault();
                    if (permissionExit == null)
                    {
                        userPermission = new UserPermission()
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserId = NTSConstants.IdUserAdminFix,
                            FunctionId = fun.Id
                        };
                        _sqlContext.UserPermission.Add(userPermission);
                    }
                }

                _sqlContext.SaveChanges();

            }
        }
    }
}
