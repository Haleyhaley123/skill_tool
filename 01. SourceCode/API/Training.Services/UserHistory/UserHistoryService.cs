using Microsoft.EntityFrameworkCore;
using NTS.Common;
using NTS.Document.Excel;
using NTSCommon.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Training.Models.Entities;
using Training.Models.Models.UserHistory;

namespace Training.Services.UserHistorys
{
    public class UserHistoryService : IUserHistoryService
    {
        private readonly TrainingContext _sqlContext;
        private readonly IExcelService _excelService;

        public UserHistoryService(TrainingContext sqlContext, IExcelService excelService)
        {
            this._sqlContext = sqlContext;
            this._excelService = excelService;
        }

        public async Task<SearchBaseResultModel<UserHistorySearchResultModel>> SearchHistoryAsync(UserHistorySearchModel searchModel)
        {
            SearchBaseResultModel<UserHistorySearchResultModel> resultModel = new SearchBaseResultModel<UserHistorySearchResultModel>();

            var dataQuery = (from a in _sqlContext.UserHistory.AsNoTracking()
                             join b in _sqlContext.User.AsNoTracking() on a.UserId equals b.Id into ab
                             from ba in ab.DefaultIfEmpty()
                             orderby a.CreateDate descending
                             select new UserHistorySearchResultModel
                             {
                                 //Content = a.Content,
                                 BrowserName = a.BrowserName,
                                 BrowserVersion = a.BrowserVersion,
                                 ClientIP = a.ClientIP,
                                 CreateDate = a.CreateDate,
                                 Device = a.Device,
                                 OS = a.OS,
                                 UserName = ba != null ? ba.UserName : "",
                                 FullName = ba != null ? ba.FullName : "",
                                 Name = a.Name,
                                 UserId = ba != null ? ba.Id : "",
                                 Type = a.Type
                             }).AsQueryable();

            if (searchModel.Type.HasValue)
            {
                dataQuery = dataQuery.Where(a => a.Type == searchModel.Type);
            }

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                dataQuery = dataQuery.Where(a => a.Name.ToLower().Contains(searchModel.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(searchModel.UserId))
            {
                dataQuery = dataQuery.Where(a => a.UserId.Equals(searchModel.UserId));
            }

            if (searchModel.DateFrom.HasValue)
            {
                dataQuery = dataQuery.Where(a => a.CreateDate >= DateTimeHelper.ToStartDate(searchModel.DateFrom.Value));
            }

            if (searchModel.DateTo.HasValue)
            {
                dataQuery = dataQuery.Where(a => a.CreateDate <= DateTimeHelper.ToEndDate(searchModel.DateTo.Value));
            }

            resultModel.TotalItems = dataQuery.Count();

            if (searchModel.GetAllData)
            {
                resultModel.DataResults = dataQuery.ToList();
            }
            else
            {
                resultModel.DataResults = dataQuery.Skip((searchModel.PageNumber - 1) * searchModel.PageSize).Take(searchModel.PageSize).ToList();
            }

            return resultModel;
        }

        /// <summary>
        /// Xuất danh sách lịch sử thao tác
        /// </summary>
        /// <param name="searchModel">Thông tin báo cáo</param>
        /// <returns></returns>
        public async Task<MemoryStream> ExportFileAsync(UserHistorySearchModel searchModel, string pathTemplate, NTSConstants.OptionExport optionExport)
        {
            searchModel.GetAllData = true;
            SearchBaseResultModel<UserHistorySearchResultModel> searchResult = SearchHistoryAsync(searchModel).Result;

            int index = 1;
            var dataExport = searchResult.DataResults.Select(a => new
            {
                Index = index++,
                Type = a.Type == NTSConstants.UserHistory_Type_Login ? "Đăng nhập" : a.Type == NTSConstants.UserHistory_Type_Data ? "Khai thác dữ liệu" : "",
                a.FullName,
                a.UserName,
                a.BrowserName,
                a.BrowserVersion,
                a.ClientIP,
                a.Device,
                a.OS,
                ThoiGian = a.CreateDate.ToStringDDMMYY(),
                a.Name
            }).ToList();

            MemoryStream streamFile = null;
            if (optionExport == NTSConstants.OptionExport.Excel)
            {
                streamFile = _excelService.ExportExcel(dataExport, pathTemplate, 11);
            }
            else if (optionExport == NTSConstants.OptionExport.Pdf)
            {
                streamFile = _excelService.ExportPdf(dataExport, pathTemplate, 11);
            }

            return streamFile;
        }

        /// <summary>
        /// Xuất danh sách lịch sử thao tác
        /// </summary>
        /// <param name="searchModel">Thông tin báo cáo</param>
        /// <returns></returns>
        public async Task<MemoryStream> ExportFileClientAsync(UserHistorySearchModel searchModel, string pathTemplate, NTSConstants.OptionExport optionExport)
        {
            searchModel.IsExport = true;
            SearchBaseResultModel<UserHistorySearchResultModel> searchResult = SearchHistoryAsync(searchModel).Result;

            int index = 1;
            var dataExport = searchResult.DataResults.Select(a => new
            {
                Index = index++,
                Type = a.Type == NTSConstants.UserHistory_Type_Login ? "Đăng nhập" : a.Type == NTSConstants.UserHistory_Type_Data ? "Khai thác dữ liệu" : "",
                a.FullName,
                a.UserName,
                ThoiGian = a.CreateDate.ToString("dd/MM/yyyy HH:mm:ss"),
                a.Name
            }).ToList();

            MemoryStream streamFile = null;
            if (optionExport == NTSConstants.OptionExport.Excel)
            {
                streamFile = _excelService.ExportExcel(dataExport, pathTemplate, 6);
            }
            else if (optionExport == NTSConstants.OptionExport.Pdf)
            {
                streamFile = _excelService.ExportPdf(dataExport, pathTemplate, 6);
            }

            return streamFile;
        }
    }
}
