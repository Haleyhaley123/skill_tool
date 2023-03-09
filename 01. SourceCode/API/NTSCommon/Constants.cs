using Newtonsoft.Json;
using NTS.Common.Models;
using System.Collections.Generic;
using System.IO;

namespace NTS.Common
{
    public class NTSConstants
    {
        #region Search helpers

        /// <summary>
        /// Ngày hôm nay
        /// </summary>
        public const string TimeType_Today = "1";

        /// <summary>
        /// Ngày qua
        /// </summary>
        public const string TimeType_Yesterday = "2";

        /// <summary>
        /// Tuần này
        /// </summary>
        public const string TimeType_ThisWeek = "3";

        /// <summary>
        /// Tuần trước
        /// </summary>
        public const string TimeType_LastWeek = "4";

        /// <summary>
        /// 7 ngày gần đây
        /// </summary>
        public const string TimeType_SevenDay = "5";

        /// <summary>
        /// Tháng này
        /// </summary>
        public const string TimeType_ThisMonth = "6";

        /// <summary>
        /// Tháng trước
        /// </summary>
        public const string TimeType_LastMonth = "7";

        /// <summary>
        /// Tháng
        /// </summary>
        public const string TimeType_Month = "8";

        /// <summary>
        /// Quý
        /// </summary>
        public const string TimeType_Quarter = "9";

        /// <summary>
        /// Năm nay
        /// </summary>
        public const string TimeType_ThisYear = "10";

        /// <summary>
        /// Năm trước
        /// </summary>
        public const string TimeType_LastYear = "11";

        /// <summary>
        /// Năm 
        /// </summary>
        public const string TimeType_Year = "12";

        /// <summary>
        /// Khoảng thời gian
        /// </summary>
        public const string TimeType_Between = "13";

        /// <summary>
        /// Quý này
        /// </summary>
        public const string TimeType_ThisQuarter = "14";

        /// <summary>
        /// Quý trước
        /// </summary>
        public const string TimeType_LastQuarter = "15";

        #endregion

        #region Cache
        public const string PrefixSystemKey = "PCMT_DEV:";
        public const string PrefixKey_DiaBan = "DiaBan:";
        public const string PrefixKey_DiaBanNgBien = "DiaBanNgBien:";
        public const string PrefixKey_DiaBanLienQuan = "DiaBanLienQuan:";

        public const string PrefixKey_Total_QLNV_Tinh = "QLNV_Tinh:";
        public const string PrefixKey_Statistical_QLNV_UpDown = "QLNV_UpDown";
        public const string PrefixKey_Statistical_QLNV_HeLoai = "QLNV_HeLoai";
        public const string PrefixKey_Statistical_QLNV_DanhMuc = "QLNV_DanhMuc";
        public const string PrefixKey_Statistical_QLNV_DanToc = "QLNV_DanToc";
        public const string PrefixKey_Statistical_QLNV_KetThuc = "QLNV_KetThuc";
        public const string PrefixKey_Statistical_QLNV_PhanLoai = "QLNV_PhanLoai";

        public const string PrefixKey_Statistical_KTNV_UpDown = "KTNV_UpDown";
        public const string PrefixKey_Statistical_KTNV_HeLoai = "KTNV_HeLoai";
        public const string PrefixKey_Statistical_KTNV_DanToc = "KTNV_DanToc";


        public const string PrefixKey_QLNV = "QLNV:";
        public const string PrefixKey_Statistical_QLNV = "Statistical_QLNV";
        public const string PrefixKey_Tuyen = "Tuyen:";

        public const string Prefixkey_Dashboard_TinhHinhChung = "DashboardTinhHinhChung";
        public const string Prefixkey_Dashboard_NghiepVuCoBan = "DashboardNghiepVuCoBan";
        public const string Prefixkey_Dashboard_BatGiuXuLy = "DashboardBatGiuXuLy";

        public const string Prefixkey_Dashboard_ViPhamHC_DoiTuong = "DoiTuongVPHC";
        public const string Prefixkey_Dashboard_ChuyenAn_DoiTuong = "DoiTuongChuyenAn";
        public const string Prefixkey_Dashboard_DTHS_DoiTuong = "DoiTuongVPHS";
        public const string Prefixkey_Dashboard_DoTuoiNguoiNghien = "DoTuoiNguoiNghien";
        public const string Prefixkey_Dashboard_DoTuoiNanNhanMuaBan = "DoTuoiNanNhanMuaBan";
        public const string Prefixkey_Dashboard_DoTuoiNguoiVPHC = "DoTuoiNguoiVPHC";
        public const string Prefixkey_Dashboard_DoTuoiNguoiDTHS = "DoTuoiNguoiDTHS";
        public const string Prefixkey_Dashboard_TinhChatChuyenAn = "TinhChatChuyenAn";
        public const string Prefixkey_Dashboard_TinhChatSoLuongChuyenAn = "SoLuongChuyenAn";
        #endregion

        #region File Path
        public const string PathFolderUpload = "/FileUpload";

        public static string PathFolderImport = Path.Combine(PathFolderUpload, "/Import");

        public static string PathFolderDiaBan = Path.Combine(PathFolderUpload, "/DiaBan");

        public static string PathFolderNguoi = Path.Combine(PathFolderUpload, "/Nguoi");

        public const string FileNameNhomDanhMucExport = "DanhMucExport.json";

        public const string FileNameCategory = "Category.json";

        public const string FileNameDiaBan = "DiaBanData.json";
        public const string FileNameDiaBanLienQuan = "DiaBanLienQuanData.json";
        public const string FileNameQLNV = "QLNVData.json";
        public const string FileNameKTNV = "KTNVData.json";
        public static string FolderExportData = "Export";

        public const string FileNameDonViTinh = "DonViTinh.json";
        public const string FileNameThamQuyenVPHC = "ThamQuyenVPHC.json";
        public const string FileNameTrangThietBi = "TrangThietBi.json";
        public const string FileNameLoaiTangVat = "LoaiTangVat.json";
        public const string FileNameLoaiMaTuy = "LoaiMaTuy.json";
        public const string FileNameGroupCategory = "GroupCategory.json";
        public const string FileNameDiaBanNgBien = "DiaBanNgBienData.json";
        public static string FileNameChuyenDeData = "ChuyenDeData.json";
        public static string FileNameNanNhanMBNData = "NanNhanMBNData.json";
        public static string FileNameLucLuongMatData = "LucLuongMatData.json";
        public static string FileNameKhenThuongTTData = "KhenThuongTTData.json";
        public static string FileNameKyLuatTTData = "KyLuatTTData.json";
        public static string FileNameChuyenAnData = "ChuyenAnData.json";
        public static string FileNameChuyenAnPHData = "ChuyenAnPhoiHopData.json";
        public static string FileNameTrinhSatNoiTuyenData = "TrinhSatNoiTuyenData.json";
        public static string FileNameNguoiNghienData = "NguoiNghienData.json";
        public static string FileNameTuDiemData = "TuDiemData.json";
        public static string FileNameSuuTapDiaBanNgoaiBienData = "DiaBanNgBienData.json";
        public static string FileNameTuyenData = "TuyenData.json";
        public static string FileNameTuyenNgBData = "TuyenNgBData.json";
        public static string FileNameVanBanQPPLData = "VanBanQPPLData.json";
        public static string FileNameVanBanNoiBoData = "VanBanNoiBoData.json";
        public static string FileNameViPhamHCData = "ViPhamHCData.json";
        public static string FileNameDonViData = "DonVi.json";
        public static string FileNameDTHSData = "DTHSData.json";
        public static string FileNameVuViecPhoiHopData = "VuViecPhoiHopData.json";
        public static string FileNameCanBoData = "CanBoData.json";
        public static string FileNameCanBoGapNanData = "CanBoGapNanData.json";
        public static string FileNameKhenThuongCNData = "KhenThuongCNData.json";
        public static string FileNameKyLuatCNData = "KyLuatCNData.json";
        public static string FileNameTrangBiKyThuatData = "TrangBiKyThuatData.json";
        public static string FileNameTinBaoToiPhamData = "TinBaoToiPhamData.json";
        public static string FileNameDonThuKhieuNaiData = "DonThuKhieuNaiData.json";
        public static string FileNameThuongNongData = "ThuongNongData.json";
        public static string FileNameXNCTPData = "XNCTPData.json";
        public static string FileNameDoiTuongTruyNaData = "DoiTuongTruyNaData.json";
        public static string FileNameXuLyTaiSanData = "XuLyTaiSanData.json";
        public const string FileNameTinh = "Tinh.json";
        public const string FileNameHuyen = "Huyen.json";
        public const string FileNameXa = "Xa.json";
        #endregion

        #region Giới tính
        /// <summary>
        /// Nam
        /// </summary>
        public const int Male = 1;

        /// <summary>
        /// Nữ
        /// </summary>
        public const int Female = 2;
        #endregion

        #region Template
        /// <summary>
        /// Đường dẫn thư mục chua file mẫu
        /// </summary>
        public const string TemplatePath = "Template";

        /// <summary>
        /// Template lực lượng mật
        /// </summary>
        public const string Export_Template_LucLuongMat = "Export_Template_LucLuongMat";

        /// <summary>
        /// Template lịch sử
        /// </summary>
        public const string TemplateHistory = "Template/LichSuThaoTac.xlsx";

        /// <summary>
        /// Template khen thưởng cá nhân
        /// </summary>
        public const string TemplateKhenThuongCN = "Template/KhenThuongCN.xlsx";

        /// <summary>
        /// Template khen thưởng tập thể
        /// </summary>
        public const string TemplateKhenThuongTT = "Template/KhenThuongTT.xlsx";

        /// <summary>
        /// Template kỷ luật cá nhân
        /// </summary>
        public const string TemplateKyLuatCN = "Template/KyLuatCN.xlsx";

        /// <summary>
        /// Template kỷ luật tập thể
        /// </summary>
        public const string TemplateKyLuatTT = "Template/KyLuatTT.xlsx";

        /// <summary>
        /// Template cán bộ gặp nạn
        /// </summary>
        public const string TemplateCanBoGapNan = "Template/CanBoGapNan.xlsx";

        /// <summary>
        /// Template văn bản QPPL
        /// </summary>
        public const string TemplateVanBanQPPL = "Template/VanBanQPPL.xlsx";

        /// <summary>
        /// Template Văn bản, hướng dẫn nghiệp vụ
        /// </summary>
        public const string TemplateVanBanNoiBo = "Template/VanBanNoiBo.xlsx";

        /// <summary>
        /// Template vi phạm hành chính
        /// </summary>
        public const string TemplateViPhamHC = "Template/ViPhamHC.xlsx";

        /// <summary>
        /// Template điều tra hình sự
        /// </summary>
        public const string TemplateDTHS = "Template/DTHS.xlsx";

        /// <summary>
        /// Template trang bị kỹ thuật
        /// </summary>
        public const string TemplateTrangBiKyThuat = "Template/TrangBiKyThuat.xlsx";

        /// <summary>
        /// Template tin báo tội phạm
        /// </summary>
        public const string TemplateTinBaoToiPham = "Template/TinBaoToiPham.xlsx";

        /// <summary>
        /// Template đơn thư khiếu nại
        /// </summary>
        public const string TemplateDonThuKhieuNai = "Template/DonThuKhieuNai.xlsx";

        /// <summary>
        /// Template thưởng nóng
        /// </summary>
        public const string FileTemplateThuongNong = "Template/ThuongNong.xlsx";

        /// <summary>
        /// Template tra cứu đối tượng
        /// </summary>
        public const string FileTemplateDoiTuong = "Template/DoiTuong.xlsx";

        /// <summary>
        /// Template thưởng nóng
        /// </summary>
        public const string FileTemplatePhoiHopDTXL = "Template/PhoiHopDTXL.xlsx";
        public const string FileTemplateXuLyTaiSan = "Template/XuLyTaiSan.xlsx";


        #endregion

        public enum OptionExport
        {
            /// <summary>
            /// Xuất file excel
            /// </summary>
            Excel = 1,
            /// <summary>
            /// Xuất word
            /// </summary>
            Word = 2,
            /// <summary>
            /// Xuất pdf
            /// </summary>
            Pdf = 3
        }

        /// <summary>
        /// Số bản gi lưu vào CSDL một lần sử dụng cho insert list
        /// </summary>
        public const int TotalSaveChange = 100;

        public const string DanhMuc = "DanhMuc";

        public const string DonViTinh = "DonViTinh";

        public const string ThamQuyenXPVPHC = "ThamQuyenXPVPHC";
        public const string Category_TrangThietBi = "TrangThietBi";
        public const string Category_LoaiTangVat = "LoaiTangVat";
        public const string Category_LoaiMaTuy = "LoaiMaTuy";
        public const string Category_Tinh = "Tinh";
        public const string Category_Huyen = "Huyen";
        public const string Category_Xa = "Xa";

        /// <summary>
        /// Mật khẩu mặc định
        /// </summary>
        public const string IdUserAdminFix = "1";

        /// <summary>
        /// Mật khẩu mặc định
        /// </summary>
        public const string PassWord = "123456";

        /// <summary>
        /// Khóa tài khoản
        /// </summary>
        public const int Lock = 1;

        /// <summary>
        /// Mở tài khoản
        /// </summary>
        public const int UnLock = 2;

        public const string NoLogEvent = "NoLogEvent";

        public const int UserHistory_Type_Login = 1;
        public const int UserHistory_Type_Data = 2;

        /// <summary>
        /// Địa chỉ email gửi thông tin đăng nhập
        /// </summary>
        public const string SystemParam_SP01 = "SP01";

        /// <summary>
        /// Mật khẩu email gửi thông tin đăng nhập
        /// </summary>
        public const string SystemParam_SP02 = "SP02";

        /// <summary>
        /// Nội dung gửi email thông tin đăng nhập
        /// </summary>
        public const string SystemParam_SP03 = "SP03";

        /// <summary>
        /// Quyền xem tất cả LLM
        /// </summary>
        public const string Function_F000214 = "F000214";

        #region Đơn vị giải cứu
        public const string DonViGiaiCuu_DonVi = "Đơn vị tự giải cứu";
        public const string DonViGiaiCuu_NuocNgoai = "Đơn vị tiếp nhận do nước ngoài trao trả";
        public const string DonViGiaiCuu_TuTroVe = "Nạn nhân tự trở về đến trình báo";
        #endregion

        #region DataChangeLog TableName
        public const string TableName_DiaBan = "DiaBan";
        public const string TableName_DiaBanNgBien = "DiaBanNgBien";
        public const string TableName_ChuyenDe = "ChuyenDe";
        public const string TableName_Tuyen = "Tuyen";
        public const string TableName_TuyenNgB = "TuyenNgB";
        public const string TableName_TuDiem = "TuDiem";
        public const string TableName_DoiTuongQLNV = "DoiTuongQLNV";
        public const string TableName_DoiTuongKTNV = "DoiTuongKTNV";
        public const string TableName_LucLuongMat = "LucLuongMat";
        public const string TableName_TSNoiTuyen = "TSNoiTuyen";
        public const string TableName_ChuyenAn = "ChuyenAn";
        public const string TableName_ChuyenAnPhoiHop = "ChuyenAnPhoiHop";
        public const string TableName_NguoiNghien = "NguoiNghien";
        public const string TableName_NanNhanMBN = "NanNhanMBN";
        public const string TableName_ViPhamHC = "ViPhamHC";
        public const string TableName_DonThuKhieuNai = "DonThuKhieuNai";
        public const string TableName_TinBaoToiPham = "TinBaoToiPham";
        public const string TableName_DTHS = "DTHS";
        public const string TableName_XNCTP = "XNCTP";
        public const string TableName_PhoiHopDTXL = "PhoiHopDTXL";
        public const string TableName_DiaBanLienQuan = "DiaBanLienQuan";
        public const string TableName_DoiTuongTruyNa = "DoiTuongTruyNa";
        public const string TableName_XuLyTaiSan = "XuLyTaiSan";
        #endregion

        #region Fix Id dùng trên hệ thống
        public const string Id_VanBanQPPL_NghiDinh = "LVB008";
        public const string Id_LinhVuc_VPHC = "2";
        public const string Id_BienPhap_TSKT = "fa46bf91-5f41-4594-bbc7-c3b6e6fef432";

        public const string Id_TinhChat_MT = "1";
        public const string Id_TinhChat_BL = "8";
        public const string Id_TinhChat_MBN = "3";

        public const string Id_XuLy_KhoiTo = "2";
        public const string Id_XuLy_Khong_KhoiTo = "3";
        public const string Id_XuLy_TamDinhChi = "4";
        public const string Id_XuLy_KienNghiKhoiTo = "5";
        public const string Id_XuLy_Chuyen_CQKhac = "6";
        public const string Id_XuLy_Chuyen_VPHC = "7";

        public const string Id_HeLoaiDT_KinhTe = "30bf3bf2-7c2d-41ac-af19-8552836773d0";
        public const string Id_HeLoaiDT_MaTuy = "a1575c6d-ac71-4bc6-b536-6778ea2d6afe";

        public const string Id_KetThucDT_DTCA = "3";
        public const string Id_KetThucDT_XLCA = "2";

        #region fix id nhóm tội
        /// <summary>
        /// Tội phạm ma tuý
        /// </summary>
        public const string NhomToiMT = "1";
        /// <summary>
        /// Buôn lậu, GLTM
        /// </summary>
        public const string NhomToiBuonLau = "2";
        /// <summary>
        /// Mua bán người
        /// </summary>
        public const string NhomToiMBN = "3";
        /// <summary>
        /// Mua bán, tàng trữ, sử dụng vũ khí, VLN
        /// </summary>
        public const string NhomToiVuKhiVLN = "4";
        /// <summary>
        /// XNC trái phép
        /// </summary>
        public const string NhomToiXNC = "5";
        /// <summary>
        /// Tội phạm và vi phạm pháp luật khác
        /// </summary>
        public const string NhomToiKhac = "6";
        #endregion

        #region Danh giá LLM
        public const string Id_ChatLuongLLM_Kha = "1";
        public const string Id_ChatLuongLLM_TB = "2";
        public const string Id_ChatLuongLLM_Kem = "3";
        #endregion

        #region Tình trang chuyên án
        public const string Id_TinhTrangCA_DangDT = "3918e63b-1125-47a3-b8c8-b7fa8c5dd3e0";
        public const string Id_TinhTrangCA_KT = "7ea859cc-18e9-4c73-9842-e52471b7b4b8";
        public const string Id_TinhTrangCA_TamDC = "3f6f8382-5020-49db-a724-74a30aff5450";
        public const string Id_TinhTrangCA_DC = "2c1b5efd-78be-4484-a1be-26d10fd14065";
        #endregion
        #endregion

        #region Tình trạng cán bộ
        public const string DangCongTac = "1";
        public const string NghiHuu = "2";
        public const string XuatNgu = "3";
        public const string ChuyenDonVi = "4";
        public const string HySinhCB = "5";
        public const string Chet = "6";

        public const int Int_DangCongTac = 1;
        public const int Int_NghiHuu = 2;
        public const int Int_XuatNgu = 3;
        public const int Int_ChuyenDonVi = 4;
        public const int Int_HySinh = 5;
        public const int Int_Chet = 6;
        #endregion

        #region Tình trạng gặp nạn
        public const int HySinh = 1;
        public const int BiThuong = 2;
        public const int PhoiNhiemHIV = 3;
        #endregion

        #region Chế độ chính sách người gặp nạn
        public const int LietSi = 1;
        public const int ThuongBinh = 2;
        public const int TroCapKhac = 3;
        #endregion

        #region Đơn vị
        public const int DonVi_Level_Tinh = 2;
        #endregion

        #region Mẫu báo cáo
        public const int BCDinhKy = 1;
        public const int MauThongKe = 2;
        public const int BCVuViec = 3;
        #endregion

        #region Loại đối tượng gửi đơn thư khiếu nại
        public enum DoiTuongKhieuNai
        {
            /// <summary>
            /// Nguyên đơn
            /// </summary>
            NguyenDon = 1,
            /// <summary>
            /// Cá nhân
            /// </summary>
            CaNhan = 2,
            /// <summary>
            /// Cơ quan
            /// </summary>
            CoQuan = 3,
            /// <summary>
            /// Tổ chức
            /// </summary>
            ToChuc = 4
        }

        public enum TiepNhanTinBao
        {
            /// <summary>
            /// Cá nhân
            /// </summary>
            CaNhan = 1,
            /// <summary>
            /// Cơ quan
            /// </summary>
            CoQuan = 2,
            /// <summary>
            /// Tổ chức
            /// </summary>
            ToChuc = 3
        }

        #region
        /// <summary>
        /// Phân loại tin: thuộc thẩm quyền BDPB
        /// </summary>
        public const int PhanLoai_ThuocThamQuyenBDBP = 1;

        /// <summary>
        /// Phân loại tin: Không thuộc thẩm quyền BDPB
        /// </summary>
        public const int PhanLoai_KoThuocThamQuyenBDPB = 2;
        /// <summary>
        /// Phân loại tin: Có dấu hiệu tội phạm
        /// </summary>
        public const int TiepNhanTinBao_KQDTXM_TP = 1;

        /// <summary>
        /// Phân loại tin: Không có dấu hiệu tội phạm
        /// </summary>
        public const int TiepNhanTinBao_KQDTXM_KTP = 2;

        /// <summary>
        /// Xử lý : Điều tra
        /// </summary>
        public const int TiepNhanTinBao_XuLy_DieuTra = 0;

        /// <summary>
        /// Xử lý : Tạm đình chỉ
        /// </summary>
        public const int TiepNhanTinBao_XuLy_TamDinhChi = 1;

        /// <summary>
        /// Xử lý : Khởi tố
        /// </summary>
        public const int TiepNhanTinBao_XuLy_KhoiTo = 2;

        /// <summary>
        /// Xử lý : Không khởi tố
        /// </summary>
        public const int TiepNhanTinBao_XuLy_KhongKhoiTo = 3;

        /// <summary>
        ///  Xử lý : Chuyển đơn vị trong ngành
        /// </summary>
        public const int TiepNhanTinBao_XuLy_ChuyenDonViTrongNganh = 4;
        /// <summary>
        ///  Xử lý : Chuyển đơn vị ngoài ngành
        /// </summary>
        public const int TiepNhanTinBao_XuLy_ChuyenDonViNgoaiNganh = 5;

        /// <summary>
        ///  Xử lý : DTHS Khởi tố
        /// </summary>
        public const string DTHS_XuLy_KhoiTo = "2";
        /// <summary>
        ///  Xử lý : DTHS Không khởi tố
        /// </summary>
        public const string DTHS_XuLy_KhongKhoiTo = "3";
        /// <summary>
        ///  Xử lý : DTHS Tạm đình chỉ
        /// </summary>
        public const string DTHS_XuLy_TamDinhChi = "4";
        /// <summary>
        ///  Xử lý : DTHS Kiến nghị khởi tố
        /// </summary>
        public const string DTHS_XuLy_KienNghiKhoiTo = "5";
        /// <summary>
        ///  Xử lý : Chuyển cơ quan khác
        /// </summary>
        public const string DTHS_XuLy_ChuyenCoQuanKhac = "6";
        /// <summary>
        ///  Xử lý : DTHS Chuyển xử lý hành chính
        /// </summary>
        public const string DTHS_XuLy_ChuyenVPHC = "7";

        /// <summary>
        /// 0	Trạng thái chưa xử lý
        /// </summary>
        public const string XuLy_ChuaXuLy = "0";
        /// <summary>
        /// 1	Xử phạt vi phạm hành chính
        /// </summary>
        public const string VPHC_XuLy_XuPhat = "1";

        /// <summary>
        /// 2	Chuyển truy cứu trách nhiệm hình sự
        /// </summary>
        public const string VPHC_XuLy_ChuyenDTHS = "2";

        /// <summary>
        /// 3	Áp dụng biện pháp thay thế nhắc nhở đối với người chưa thành niên
        /// </summary>
        public const string VPHC_XuLy_ApDungNhacNho = "3";
        /// <summary>
        /// 4	Chuyển cơ quan khác xử lý
        /// </summary>
        public const string VPHC_XuLy_ChuyenCQKhac = "4";

        /// <summary>
        /// Khởi tố bị can vi phạm HS
        /// </summary>
        public const int DTHS_XuLyNguoi_KhoiToBiCan = 2;
        /// <summary>
        /// Không khởi tố bị can vi phạm HS
        /// </summary>
        public const int DTHS_XuLyNguoi_TraTuDo = 3;
        #endregion

        public static List<ComboboxBaseModel> DoiTuongKhieuNaiList = new List<ComboboxBaseModel>()
        {
            new ComboboxBaseModel()
            {
                Id = ((int)DoiTuongKhieuNai.CaNhan).ToString(),
                Name = "Cá nhân"
            },
            new ComboboxBaseModel()
            {
                Id = ((int)DoiTuongKhieuNai.CoQuan).ToString(),
                Name = "Cơ quan"
            },
            new ComboboxBaseModel()
            {
                Id = ((int)DoiTuongKhieuNai.ToChuc).ToString(),
                Name = "Tổ chức"
            },
        };

        public static List<ComboboxBaseModel> DoiTuongTinBaoList = new List<ComboboxBaseModel>()
        {
            new ComboboxBaseModel()
            {
                Id = ((int)TiepNhanTinBao.CaNhan).ToString(),
                Name = "Cá nhân"
            },
            new ComboboxBaseModel()
            {
                Id = ((int)TiepNhanTinBao.CoQuan).ToString(),
                Name = "Cơ quan"
            },
            new ComboboxBaseModel()
            {
                Id = ((int)TiepNhanTinBao.ToChuc).ToString(),
                Name = "Tổ chức"
            },
        };
        #endregion

        #region Giải quyết đơn thư
        public enum GiaiQuyetDonThu
        {
            /// <summary>
            /// Chuyển đơn
            /// </summary>
            ChuyenDon = 1,
            /// <summary>
            /// Xác minh
            /// </summary>
            XacMinh = 2,
            /// <summary>
            /// Cơ quan
            /// </summary>
            GiaiQuyet = 3,
            /// <summary>
            /// Tổ chức
            /// </summary>
            DinhChi = 4
        }

        public static List<ComboboxBaseModel> GiaiQuyetDonThuList = new List<ComboboxBaseModel>()
        {
            new ComboboxBaseModel(){
                Id = "0",
                Name = "Chưa giải quyết"
            },
            new ComboboxBaseModel(){
                Id = ((int)GiaiQuyetDonThu.ChuyenDon).ToString(),
                Name = "Chuyển đơn"
            },
            new ComboboxBaseModel()
            {
                Id = ((int)GiaiQuyetDonThu.XacMinh).ToString(),
                Name = "Phân công xác minh"
            },
            new ComboboxBaseModel()
            {
                Id = ((int)GiaiQuyetDonThu.GiaiQuyet).ToString(),
                Name = "Giải quyết khiếu nại"
            },
            new ComboboxBaseModel()
            {
                Id = ((int)GiaiQuyetDonThu.DinhChi).ToString(),
                Name = "Đình chỉ giải quyết khiếu nại"
            },
        };
        #endregion

        #region Các cấp quản lý
        /// <summary>
        ///Cấp cục
        /// </summary>
        public const string LevelCuc = "1";

        /// <summary>
        /// Cấp bộ chỉ huy tỉnh
        /// </summary>
        public const string LevelBCHTinh = "2";

        /// <summary>
        /// Cấp cửa khẩu cảng và phòng của tỉnh
        /// </summary>
        public const string LevelCKCVaPhongPCMTTinh = "3";

        /// <summary>
        /// Cấp Đồn và tương đương
        /// </summary>
        public const string LevelDonBP = "4";
        #endregion

        #region Quốc tịch
        public const string IdQuocTichVN = "1";
        #endregion


        public const string ExtensionPDF = ".pdf";

        #region Giải quyết tin báo
        public const int GiaiQuyetTinBao_PhanLoaiTin_CoDauHieu = 1;

        public const int GiaiQuyetTinBao_PhanLoaiTin_KhongCoDauHieu = 2;

        /// <summary>
        /// Phân công phó thủ trưởng
        /// </summary>
        public const string GiaiQuyetTinBao_PhanCong_ThuTruong = "1";
        /// <summary>
        /// Phân công ĐTV
        /// </summary>
        public const string GiaiQuyetTinBao_PhanCong_DTV = "2";

        public const int GiaiQuyetTinBao_KetQuaGiaiQuyet_TamDinhChi = 1;

        public const int GiaiQuyetTinBao_KetQuaGiaiQuyet_KhoiTo = 2;

        public const int GiaiQuyetTinBao_KetQuaGiaiQuyet_KhongKhoiTo = 3;
        #endregion

        #region Quân số TSKT
        public const int ChinhThucTSKT = 1;
        public const int KiemNhiemTSKT = 2;
        #endregion

        #region Trình độ đào tạo
        public const int DaiHoc = 3;
        public const int TrungCap = 5;
        public const int SoCap = 6;
        #endregion

        #region Nhóm lực lượng mật
        public const int LLMGroupNoiBien = 1;
        public const int LLMGroupNgoaiBien = 2;
        #endregion

        #region Tiến trình hồ sơ
        public const int TiepNhan = 0;
        public const int DieuTraXuLy = 1;
        public const int GiaiQuyet = 2;
        public const int KetThuc = 3;
        #endregion

        #region Ngạch cán bộ
        public const int Ngach_SQ = 1;
        public const int Ngach_QNCN = 2;
        public const int Ngach_HSQCS = 3;
        public const int Ngach_CNV = 4;
        #endregion

        #region Tình trạng VPHC
        public const string VPHC_TinhTrang_XuPhat = "1";
        public const string VPHC_TinhTrang_TruyCuuHS = "2";
        public const string VPHC_TinhTrang_NhacNho = "3";
        #endregion

        #region Kết luận điều tra
        public const int KetLuan_ViPham = 1;
        public const int KetLuan_KhongViPham = 2;
        #endregion

        /// <summary>
        /// Tình trạng cán bộ: Đang công tác
        /// </summary>
        public const int CanBo_TinhTrang_DangCongTac = 1;

        /// <summary>
        /// Loại đối tượng địa bàn
        /// </summary>
        public const int TypeDTDiaBan = 0;
        /// <summary>
        /// Loại đối tượng tuyến
        /// </summary>
        public const int TypeDTTuyen = 1;

        /// <summary>
        /// Loại đối tượng địa bàn liên quan
        /// </summary>
        public const int TypeDTDiaBanLienQuan = 2;

        #region Báo cáo người nghiện
        // Cơ sở cai nghiện
        public const string CoSoCaiNghien = "45c57192-b52b-47b1-bf09-9dd6be230e19";
        public const string CoSoCongDong = "a8fd2fc4-c747-4576-addf-d9837eef3cb6";
        public const string GiaDinh = "18642c8b-bda5-44e2-ae79-3ce7cff913d5";
        public const string Khong = "5a6e07bb-dcaf-4f06-afb8-78f25842d68f";

        // Nghề nghiệp
        public const string HocSinh = "8";
        public const string SinhVien = "9";
        public const string GiaoVien = "3";

        // Tình trạng nghiện
        public const string NghienMoi = "1";
        public const string NghienCu = "2";
        public const string MoiPhatHien = "3";
        public const string TaiNghien = "4";
        public const string TuTha = "5";
        public const string TuNoiKhac = "6";

        public const string NghienChet = "1";
        public const string CaiNghienThanhCong = "2";
        public const string ChuyenDiNoiKhac = "3";
        public const string DiTraiGiam = "4";
        public const string NguyenNhanKhac = "5";
        #endregion

        #region Nguồn tin
        public const string NguonTin_TrucTiep = "NTVPHC002";
        #endregion

        #region Địa điểm phát hiện vi phạm
        /// <summary>
        /// Loại địa điểm: Trên đất liền
        /// </summary>
        public const int LoaiDiaDiem_TrenDatLien = 0;
        /// <summary>
        /// Loại địa điểm: Trên biển
        /// </summary>
        public const int LoaiDiaDiem_TrenBien = 1;
        #endregion

        #region Lực lượng mật
        public const int LLM_NhomLoai_NoiBien = 1;
        public const int LLM_NhomLoai_NgoaiBien = 2;
        #endregion

        public const string Confirm = "\nLưu ý: Nếu kết thúc, các thông tin sẽ bị khóa và không thể chỉnh sửa hãy kiểm tra kỹ thông tin trước khi nhấn “Đồng ý” để kết thúc.";

        #region Loại đơn vị
        public const string LoaiDonVi_PhongCuc = "DV01";
        public const string LoaiDonVi_PhongTinh = "DV03";
        public const string LoaiDonVi_DonBP = "DV04";
        public const string LoaiDonVi_CKC = "DV05";
        #endregion

        #region Fix id trường hợp bắt
        /// <summary>
        /// Bắt người bị giữ trong trường hợp khẩn cấp
        /// </summary>
        public const string Bat_KhanCap = "THB001";
        /// <summary>
        ///  Bắt người phạm tội quả tang
        /// </summary>
        public const string Bat_QuaTang = "THB002";
        /// <summary>
        /// Bắt người đang bị truy nã
        /// </summary>
        public const string Bat_TruyNa = "THB003";
        /// <summary>
        /// Bắt bị can, bị cáo để tạm giam
        /// </summary>
        public const string Bat_TamGiam = "THB004";
        /// <summary>
        /// Bắt người bị yêu cầu dẫn độ
        /// </summary>
        public const string Bat_DanDo = "THB005";
        /// <summary>
        /// Chưa bắt
        /// </summary>
        public const string Bat_ChuaBat = "THB006";
        #endregion

        #region Fix id Biện pháp ngăn chặn hình sự
        /// <summary>
        ///Tạm giữ người bị giữ trong trường hợp khẩn cấp
        /// </summary>
        public const string BienPhaNNTamGiu_KhanCap = "BPNCHS001";
        /// <summary>
        ///Tạm giữ người phạm tội quả tang
        /// </summary>
        public const string BienPhaNNTamGiu_QuaTang = "BPNCHS002";
        /// <summary>
        ///Tạm giữ người đang bị truy nã
        /// </summary>
        public const string BienPhaNNTamGiu_TruyNa = "BPNCHS003";
        /// <summary>
        ///Tạm giữ người phạm tội tự thú
        /// </summary>
        public const string BienPhaNNTamGiu_TuThu = "BPNCHS004";
        /// <summary>
        ///Tạm giữ người phạm tội đầu thú
        /// </summary>
        public const string BienPhaNNTamGiu_DauThu = "BPNCHS005";
        /// <summary>
        ///Tạm giam
        /// </summary>
        public const string BienPhaNNTamGiam = "BPNCHS006";
        /// <summary>
        ///Bảo lĩnh
        /// </summary>
        public const string BienPhaNNBaoLinh = "BPNCHS007";
        /// <summary>
        ///Đặt tiền
        /// </summary>
        public const string BienPhaNNDatTien = "BPNCHS008";
        /// <summary>
        ///Cấm đi khỏi nơi cư trú
        /// </summary>
        public const string BienPhaNNCamDi = "BPNCHS009";
        /// <summary>
        ///Tạm hoãn xuất cảnh
        /// </summary>
        public const string BienPhaNNHoanXuatCanh = "BPNCHS010";
        #endregion

        #region Nhóm tôi (Lĩnh vực) ĐTHS
        /// <summary>
        /// An ninh Quốc gia
        /// </summary>
        public const string TPAnNinhQG = "NT001";
        /// <summary>
        /// Tính mạng, sức khỏe, nhân phẩm, danh dự
        /// </summary>
        public const string TPTinhMang = "NT002";
        /// <summary>
        /// Tự do, dân chủ
        /// </summary>
        public const string TPQuyenTuDo = "NT003";
        /// <summary>
        ///  Sở hữu
        /// </summary>
        public const string TPSoHuu = "NT004";
        /// <summary>
        /// Hôn nhân
        /// </summary>
        public const string TPHonNhan = "NT005";
        /// <summary>
        /// Kinh tế
        /// </summary>
        public const string TPKinhTe = "NT006";
        /// <summary>
        /// Môi trường
        /// </summary>
        public const string TPMoiTruong = "NT007";
        /// <summary>
        /// Ma tuý
        /// </summary>
        public const string TPMaTuy = "NT008";
        /// <summary>
        /// Công cộng
        /// </summary>
        public const string TPCongCong = "NT009";
        /// <summary>
        /// Hành chính
        /// </summary>
        public const string TPHanhChinh = "NT010";
        /// <summary>
        /// Chương khác của BLHS
        /// </summary>
        public const string TPKhacCuaBLHS = "NT011";
        #endregion

        public const string DateFormatKey = "yyMMdd.HHmmssfff";
    }
}
