using NTS.Common;
using NTSCommon.Models;
using System.IO;
using System.Threading.Tasks;
using Training.Models.Models.UserHistory;

namespace Training.Services.UserHistorys
{
    public interface IUserHistoryService
    {
        Task<SearchBaseResultModel<UserHistorySearchResultModel>> SearchHistoryAsync(UserHistorySearchModel searchModel);

        /// <summary>
        /// Xuất danh sách lịch sử thao tác
        /// </summary>
        /// <param name="searchModel">Thông tin báo cáo</param>
        /// <returns></returns>
        Task<MemoryStream> ExportFileAsync(UserHistorySearchModel searchModel, string pathTemplate, NTSConstants.OptionExport optionExport);

        /// <summary>
        /// Xuất danh sách lịch sử thao tác
        /// </summary>
        /// <param name="searchModel">Thông tin báo cáo</param>
        /// <returns></returns>
        Task<MemoryStream> ExportFileClientAsync(UserHistorySearchModel searchModel, string pathTemplate, NTSConstants.OptionExport optionExport);
    }
}
