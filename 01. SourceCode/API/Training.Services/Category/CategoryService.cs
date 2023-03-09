using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NTS.Common;
using NTS.Common.Files;
using NTS.Common.Resource;
using NTS.Common.Utils;
using NTSCommon.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Training.Models.Entities;
using Training.Models.Models.Category;
using Training.Models.Models.GroupCategory;

namespace Training.Services.Categorys
{
    public class CategoryService : ICategoryService
    {
        //private Dictionary<Type, Dictionary<string, string>> columnNames = new Dictionary<Type, Dictionary<string, string>>(30);
        private readonly TrainingContext _sqlContext;

        public CategoryService(TrainingContext sqlContext)
        {
            this._sqlContext = sqlContext;
        }

        /// <summary>
        /// Tìm kiếm danh mục
        /// </summary>
        /// <param name="searchModel">Dữ liệu tìm kiếm</param>
        /// <returns></returns>
        public async Task<List<GroupCategoryModel>> SearchGroupCategoryAsync()
        {
            var groupCategories = await (from a in _sqlContext.GroupCategory.AsNoTracking()
                                         orderby a.Order
                                         select new GroupCategoryModel
                                         {
                                             Id = a.GroupCategoryId,
                                             Type = 1,
                                             Name = a.Name,
                                             Order = a.Order
                                         }).ToListAsync();

            groupCategories.AddRange((from a in _sqlContext.Category.AsNoTracking()
                                      join b in _sqlContext.GroupCategory.AsNoTracking() on a.GroupCategoryId equals b.GroupCategoryId
                                      orderby a.Order
                                      select new GroupCategoryModel
                                      {
                                          Id = a.CategoryId,
                                          ParentId = a.GroupCategoryId,
                                          Type = 2,
                                          Name = a.Name,
                                          Order = a.Order,
                                          TableName = a.TableName,
                                      }).ToList());

            return groupCategories;
        }

        /// <summary>
        /// Thêm danh mục
        /// </summary>
        /// <param name="model">Dữ liệu thêm mới</param>
        /// <param name="userId">Id người tạo</param>
        /// <returns></returns>
        public async Task CreateCategoryAsync(CategoryCreateModel model)
        {
            var categoryExist = _sqlContext.Category.AsNoTracking().FirstOrDefault(u => u.Name.ToLower().Equals(model.Name.NTSTrim().ToLower()) && !string.IsNullOrEmpty(model.TableName) && u.TableName.ToLower().Equals(model.TableName.NTSTrim().ToLower()));
            if (categoryExist != null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0002, TextResourceKey.Category);
            }

            var indexs = _sqlContext.Category.ToList();
            var maxIndex = 1;
            if (indexs.FirstOrDefault(i => i.Order == model.Order) != null)
            {
                if (indexs.Count > 0)
                {
                    maxIndex = indexs.Select(i => i.Order).Max();
                }

                if (model.Order <= maxIndex)
                {
                    int modelIndex = model.Order;
                    var listOrder = indexs.Where(i => i.Order >= modelIndex).ToList();
                    if (listOrder.Count > 0 && listOrder != null)
                    {
                        foreach (var item in listOrder)
                        {
                            item.Order++;
                        }
                    }
                }
            }

            Category category = new Category()
            {
                CategoryId = Guid.NewGuid().ToString(),
                GroupCategoryId = model.GroupCategoryId,
                Name = model.Name.NTSTrim(),
                Order = model.Order,
                TableName = model.TableName.NTSTrim(),
            };

            _sqlContext.Category.Add(category);
            await _sqlContext.SaveChangesAsync();
        }

        /// <summary>
        /// Cập nhật danh mục
        /// </summary>
        /// <param name="id">Id danh mục</param>
        /// <param name="model">Dữ liệu cập nhật</param>
        /// <param name="userId">Id người cập nhật</param>
        /// <returns></returns>
        public async Task UpdateCategoryAsync(string id, CategoryCreateModel model)
        {
            var category = _sqlContext.Category.FirstOrDefault(i => i.CategoryId.Equals(id));
            if (category == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.Category);
            }

            var categoryExist = _sqlContext.Category.AsNoTracking().FirstOrDefault(i => !i.CategoryId.Equals(id) && i.Name.ToLower().Equals(model.Name.NTSTrim().ToLower()) && !string.IsNullOrEmpty(model.TableName) && i.TableName.ToLower().Equals(model.TableName.NTSTrim().ToLower()));
            if (categoryExist != null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0002, TextResourceKey.Category);
            }


            var indexs = _sqlContext.Category.ToList();
            if (indexs.FirstOrDefault(i => !i.CategoryId.Equals(id) && i.Order == model.Order) != null)
            {
                var maxIndex = 1;
                if (indexs.Count > 0)
                {
                    maxIndex = indexs.Select(a => a.Order).Max();
                }

                if (model.Order <= maxIndex)
                {
                    int modelIndex = model.Order;
                    var listOrder = indexs.Where(b => b.Order >= modelIndex).ToList();
                    if (listOrder.Count > 0 && listOrder != null)
                    {
                        foreach (var item in listOrder)
                        {
                            item.Order++;
                        }
                    }
                }
            }

            category.GroupCategoryId = model.GroupCategoryId;
            category.Name = model.Name.NTSTrim();
            category.Order = model.Order;
            category.TableName = model.TableName.NTSTrim();

            using (var trans = _sqlContext.Database.BeginTransaction())
            {
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
        /// Xóa danh mục
        /// </summary>
        /// <param name="id">Id danh mục</param>
        /// <returns></returns>
        public async Task DeleteCategoryByIdAsync(string id)
        {
            var category = _sqlContext.Category.FirstOrDefault(i => i.CategoryId.Equals(id));
            if (category == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.Category);
            }

            var indexs = _sqlContext.Category.ToList();
            var maxIndex = 1;
            if (indexs.Count > 0)
            {
                maxIndex = indexs.Select(i => i.Order).Max();
            }

            if (category.Order < maxIndex)
            {
                int modelIndex = category.Order;
                var listOrder = indexs.Where(i => i.Order > modelIndex).ToList();
                if (listOrder.Count > 0 && listOrder != null)
                {
                    foreach (var item in listOrder)
                    {
                        item.Order--;
                    }
                }
            }

            using (var trans = _sqlContext.Database.BeginTransaction())
            {
                try
                {
                    _sqlContext.Category.Remove(category);
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
        /// Lấy thông tin danh mục
        /// </summary>
        /// <param name="id">Id danh mục</param>
        /// <returns></returns>
        public async Task<CategoryCreateModel> GetCategoryByIdAsync(string id)
        {
            var category = await (from a in _sqlContext.Category.AsNoTracking()
                                  where a.CategoryId.Equals(id)
                                  select new CategoryCreateModel()
                                  {
                                      CategoryId = a.CategoryId,
                                      GroupCategoryId = a.GroupCategoryId,
                                      Name = a.Name,
                                      Order = a.Order,
                                      TableName = a.TableName,
                                  }).FirstOrDefaultAsync();
            if (category == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.Category);
            }

            return category;
        }

        /// <summary>
        /// Lấy list order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<int>> GetListOrderAsync(string id)
        {
            List<int> result = new List<int>();
            int max = 1;
            var data = await _sqlContext.Category.OrderBy(i => i.Order).ToListAsync();
            if (data.Count > 0)
            {
                result = data.Select(i => i.Order).ToList();
                max = result.Max(i => i);
                if (string.IsNullOrEmpty(id))
                {
                    result.Add(max + 1);
                }
            }
            else
            {
                result.Add(max);
            }
            return result;
        }

        /// <summary>
        /// Tìm kiếm danh mục
        /// </summary>
        /// <param name="searchModel">Dữ liệu tìm kiếm</param>
        /// <returns></returns>
        public async Task<SearchBaseResultModel<CategoryTableDataModel>> SearchCategoryTableAsync(CategorySearchModel searchModel)
        {
            SearchBaseResultModel<CategoryTableDataModel> searchResult = new SearchBaseResultModel<CategoryTableDataModel>();

            if (!string.IsNullOrEmpty(searchModel.TableName))
            {
                if (!searchModel.TableName.Equals(NTSConstants.DanhMuc))
                {
                    var dataQuery = await GetListDataWidthTable(searchModel.TableName);

                    if (!string.IsNullOrEmpty(searchModel.Name))
                    {
                        dataQuery = dataQuery.Where(i => i.Name.ToUpper().Contains(searchModel.Name.ToUpper())).ToList();
                    }

                    searchResult.TotalItems = dataQuery.Count;
                    searchResult.DataResults = dataQuery.OrderBy(i => i.Order).ToList();
                }
            }

            return searchResult;
        }

        /// <summary>
        /// Thêm mới dữ liệu theo tên bảng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task CreateCategoryTableAsync(CategoryModel model)
        {
            if (!model.TableName.Equals(NTSConstants.DanhMuc))
            {
                var categories = await GetListDataWidthTable(model.TableName);

                var categoryExist = categories.FirstOrDefault(u => u.Name.ToLower().Equals(model.Name.NTSTrim().ToLower()));
                if (categoryExist != null)
                {
                    throw NTSException.CreateInstance(MessageResourceKey.MSG0002, TextResourceKey.Category);
                }

                var indexs = categories.ToList();
                var maxIndex = 1;
                if (indexs.FirstOrDefault(i => i.Order == model.Order) != null)
                {
                    if (indexs.Count > 0)
                    {
                        maxIndex = indexs.Select(i => i.Order).Max();
                    }

                    if (model.Order <= maxIndex)
                    {
                        int modelIndex = model.Order;
                        var listOrder = indexs.Where(i => i.Order >= modelIndex).ToList();
                        if (listOrder.Count > 0 && listOrder != null)
                        {
                            string commandTextUpdate = string.Empty;
                            foreach (var item in listOrder)
                            {
                                item.Order++;
                                commandTextUpdate = "UPDATE " + model.TableName + $" SET [Order] = " + item.Order + " WHERE Id = '" + item.Id + "'";
                                _sqlContext.Database.ExecuteSqlRaw(commandTextUpdate);
                            }
                        }
                    }
                }

                model.Id = Guid.NewGuid().ToString();
                var commandText = "INSERT INTO [dbo].[" + model.TableName + $"] (Id, Name, [Order]) VALUES ('{model.Id}', N'{model.Name}', {model.Order})";
                _sqlContext.Database.ExecuteSqlRaw(commandText);
            }
        }

        /// <summary>
        /// Cập nhật dữ liệu theo tên bảng
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateCategoryTableAsync(string id, CategoryModel model)
        {
            if (!model.TableName.Equals(NTSConstants.DanhMuc))
            {
                var categorys = await GetListDataWidthTable(model.TableName);
                var category = categorys.FirstOrDefault(i => i.Id.Equals(id));
                if (category == null)
                {
                    throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.Category);
                }

                var categoryExist = categorys.FirstOrDefault(i => !i.Id.Equals(id) && i.Name.ToLower().Equals(model.Name.NTSTrim().ToLower()));
                if (categoryExist != null)
                {
                    throw NTSException.CreateInstance(MessageResourceKey.MSG0002, TextResourceKey.Category);
                }

                var commandTextUpdate = string.Empty;
                if (category.Order != model.Order)
                {
                    var indexs = categorys.ToList();
                    if (indexs.FirstOrDefault(i => i.Order == model.Order) != null)
                    {
                        if (category.Order > model.Order)
                        {
                            var listOrder = indexs.Where(i => i.Order >= model.Order && i.Order < category.Order).ToList();
                            if (listOrder.Count > 0 && listOrder != null)
                            {
                                foreach (var item in listOrder)
                                {
                                    item.Order++;
                                    commandTextUpdate = "UPDATE " + model.TableName + $" SET [Order] = " + item.Order + " WHERE Id = '" + item.Id + "'";
                                    _sqlContext.Database.ExecuteSqlRaw(commandTextUpdate);
                                }
                            }
                        }
                        else if (category.Order < model.Order)
                        {
                            var listOrder = indexs.Where(i => i.Order > category.Order && i.Order <= model.Order).ToList();
                            if (listOrder.Count > 0 && listOrder != null)
                            {
                                foreach (var item in listOrder)
                                {
                                    item.Order--;
                                    commandTextUpdate = "UPDATE " + model.TableName + $" SET [Order] = " + item.Order + " WHERE Id = '" + item.Id + "'";
                                    _sqlContext.Database.ExecuteSqlRaw(commandTextUpdate);
                                }
                            }
                        }
                    }
                }

                var commandText = "UPDATE " + model.TableName + $" SET Name = @Name, [Order] = @Order WHERE Id = @Id";
                var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Name", model.Name),
                new SqlParameter("@Order", model.Order),
            };
                _sqlContext.Database.ExecuteSqlRaw(commandText, parameters.ToArray());
            }
        }

        /// <summary>
        /// Xóa dữ liệu theo tên bảng
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public async Task DeleteCategoryTableAsync(string id, string tableName)
        {
            //string foreignKEY = "Id" + tableName;
            //List<string> listTable = new List<string>();

            //foreach (var entityType in _sqlContext.Model.GetEntityTypes())
            //{
            //    var clrType = entityType.ClrType;

            //    if (!columnNames.ContainsKey(clrType))
            //    {
            //        columnNames.Add(clrType, new Dictionary<string, string>(30));
            //    }

            //    foreach (var property in entityType.GetProperties())
            //    {
            //        columnNames[clrType].Add(property.Name, property.GetColumnName());
            //    }
            //}

            //foreach (var item in columnNames)
            //{
            //    foreach (var key in item.Value)
            //    {
            //        if (key.Key.Equals(foreignKEY))
            //        {
            //            listTable.Add(item.Key.Name);
            //        }
            //    }
            //}

            if (!tableName.Equals(NTSConstants.DanhMuc))
            {
                var categorys = await GetListDataWidthTable(tableName);
                var category = categorys.FirstOrDefault(i => i.Id.Equals(id));
                if (category == null)
                {
                    throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.Category);
                }

                var indexs = categorys.ToList();
                var maxIndex = 1;
                if (indexs.Count > 0)
                {
                    maxIndex = indexs.Select(i => i.Order).Max();
                }

                if (category.Order < maxIndex)
                {
                    int modelIndex = category.Order;
                    var listOrder = indexs.Where(i => i.Order > modelIndex).ToList();
                    if (listOrder.Count > 0 && listOrder != null)
                    {
                        var commandTextDelete = string.Empty;
                        foreach (var item in listOrder)
                        {
                            item.Order--;
                            commandTextDelete = "UPDATE " + tableName + $" SET [Order] = " + item.Order + " WHERE Id = '" + item.Id + "'";
                            _sqlContext.Database.ExecuteSqlRaw(commandTextDelete);
                        }
                    }
                }

                var commandText = "DELETE FROM " + tableName + $" WHERE Id = @Id";
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Id", id),
                };
                _sqlContext.Database.ExecuteSqlRaw(commandText, parameters.ToArray());
            }
        }

        /// <summary>
        /// Lấy thông tin danh mục
        /// </summary>
        /// <param name="id">Id danh mục</param>
        /// <returns></returns>
        public async Task<CategoryModel> GetCategoryTableByIdAsync(string id, string tableName)
        {
            CategoryModel category = new CategoryModel();

            if (!tableName.Equals(NTSConstants.DanhMuc))
            {
                using (DbCommand cmd = _sqlContext.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM [dbo].[" + tableName.NTSTrim() + "] WHERE Id = '" + id + "'";
                    await _sqlContext.Database.OpenConnectionAsync();
                    using (DbDataReader ddr = cmd.ExecuteReader())
                    {
                        while (ddr.Read())
                        {
                            category = new CategoryModel()
                            {
                                Id = ddr["Id"].ToString(),
                                Name = ddr["Name"].ToString(),
                                Order = Convert.ToInt32(ddr["Order"].ToString())
                            };
                        }
                    }
                }
            }

            if (category == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.Category);
            }

            return category;
        }

        /// <summary>
        /// Lấy list order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<int>> GetListOrderTableAsync(string id, string tableName)
        {
            List<int> result = new List<int>();

            if (!tableName.Equals(NTSConstants.DanhMuc))
            {
                var categorys = await GetListDataWidthTable(tableName);
                int max = 1;
                var data = categorys.OrderBy(i => i.Order).ToList();
                if (data.Count > 0)
                {
                    result = data.Select(i => i.Order).ToList();
                    max = result.Max(i => i);
                    if (string.IsNullOrEmpty(id))
                    {
                        result.Add(max + 1);
                    }
                }
                else
                {
                    result.Add(max);
                }
            }

            return result;
        }

        /// <summary>
        /// Lấy list data theo tên bảng
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public async Task<List<CategoryTableDataModel>> GetListDataWidthTable(string tableName)
        {
            List<CategoryTableDataModel> categorys = new List<CategoryTableDataModel>();
            using (DbCommand cmd = _sqlContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM [dbo].[" + tableName.NTSTrim() + "]";
                await _sqlContext.Database.OpenConnectionAsync();
                using (DbDataReader ddr = cmd.ExecuteReader())
                {
                    while (ddr.Read())
                    {
                        categorys.Add(new CategoryTableDataModel
                        {
                            Id = ddr["Id"].ToString(),
                            Name = ddr["Name"].ToString(),
                            Order = Convert.ToInt32(ddr["Order"].ToString())
                        });
                    }
                }
            }

            return categorys;
        }

        public List<CategoryTableDataModel> GetListDataWidthTableClient(string tableName)
        {
            List<CategoryTableDataModel> categorys = new List<CategoryTableDataModel>();
            using (DbCommand cmd = _sqlContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM [dbo].[" + tableName.NTSTrim() + "]";
                _sqlContext.Database.OpenConnection();
                using (DbDataReader ddr = cmd.ExecuteReader())
                {
                    while (ddr.Read())
                    {
                        categorys.Add(new CategoryTableDataModel
                        {
                            Id = ddr["Id"].ToString(),
                            Name = ddr["Name"].ToString(),
                            Order = Convert.ToInt32(ddr["Order"].ToString())
                        });
                    }
                }
            }

            return categorys;
        }
    }
}
