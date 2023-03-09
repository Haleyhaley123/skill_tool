using System;
using System.Collections.Generic;
using System.Text;

namespace NTSCommon.Models
{
    public class SearchBaseModel
    {
        public SearchBaseModel()
        {
            PageSize = 10;
            PageNumber = 1;
        }
        /// <summary>
        /// Số bán ghi trên trang
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int PageNumber { get; set; }

        public string OrderBy { get; set; }

        public bool OrderType { get; set; }

        /// <summary>
        /// Từ ngày
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Đến ngày
        /// </summary>
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Phiên bản web
        /// </summary>
        public bool IsVersionWeb { get; set; }

        /// <summary>
        /// Lấy tất cả dữ liệu dùng cho export và không phân trang
        /// </summary>
        public bool GetAllData { get; set; }

        /// <summary>
        /// Id tỉnh biên giới quan lý dư liệu (mở hồ sơ)
        /// </summary>
        public string IdTinhBP { get; set; }
        /// <summary>
        /// Id đơn vi quan lý dư liệu (mở hồ sơ)
        /// </summary>
        public string IdDonVi { get; set; }

        /// <summary>
        /// Id đơn vi quan lý dư liệu cấp dưới
        /// </summary>
        public string IdDonViCapDuoiList { get; set; }

        /// <summary>
        /// Cấp quản lý
        /// </summary>
        public string Level { get; set; }
    }
}
