import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

@Injectable({
    providedIn: 'root'
})
export class Constants {
    QLNV: number = 1;
    KTNVS: number = 0;
    minDate = { year: 1945, month: 1, day: 1 };

    ScrollConfig: PerfectScrollbarConfigInterface = {
        suppressScrollX: false,
        suppressScrollY: false,
        minScrollbarLength: 20,
        wheelPropagation: true
    };

    ScrollXConfig: PerfectScrollbarConfigInterface = {
        suppressScrollX: false,
        suppressScrollY: true,
        minScrollbarLength: 20,
        wheelPropagation: true
    };
    ScrollYConfig: PerfectScrollbarConfigInterface = {
        suppressScrollX: true,
        suppressScrollY: false,
        minScrollbarLength: 20,
        wheelPropagation: true
    };

    HttpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };

    FileHttpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'multipart/form-data' })
    };

    StatusCode = {
        Success: 1,
        Error: 2,
        Validate: 3
    };

    statusSearch: any = {
        search: 1,
        excel: 2,
        pdf: 3
    }

    data: any = {
        statusSearch: 1,
        searchData: {}
    }

    Category_DonViTinh = "DonViTinh";
    Category_ThamQuyenXPVPHC = "ThamQuyenXPVPHC";
    Category_TrangThietBi = "TrangThietBi";
    Category_LoaiTangVat = "LoaiTangVat";
    Category_LoaiMaTuy = "LoaiMaTuy";
    Category_Tinh = "Tinh";
    Category_Huyen = "Huyen";
    Category_Xa = "Xa";

    // Giới tính
    Gender = [
        { Id: 1, Name: 'Nam', Checked: false },
        { Id: 2, Name: 'Nữ', Checked: false },
    ];

    SearchTimeTypes: any[] = [
        { Id: '1', Name: 'Hôm nay' },
        { Id: '2', Name: 'Hôm qua' },
        { Id: '3', Name: 'Tuần này' },
        { Id: '4', Name: 'Tuần trước' },
        { Id: '5', Name: '7 ngày gần đây' },
        { Id: '6', Name: 'Tháng này' },
        { Id: '7', Name: 'Tháng trước' },
        { Id: '8', Name: 'Tháng' },
        { Id: '9', Name: 'Quý' },
        { Id: '10', Name: 'Năm nay' },
        { Id: '11', Name: 'Năm trước' },
        { Id: '12', Name: 'Năm' },
        { Id: '13', Name: 'Khoảng thời gian' }
    ];

    TimeTypesDashboad: any[] = [

        { Id: '6', Name: 'Tháng này' },
        { Id: '7', Name: 'Tháng trước' },
        { Id: '8', Name: 'Tháng' },
        { Id: '9', Name: 'Quý' },
        { Id: '10', Name: 'Năm nay' },
        { Id: '11', Name: 'Năm trước' },
        { Id: '12', Name: 'Năm' },
    ];

    SearchExpressionTypes: any[] = [
        { Id: 1, Name: '=' },
        { Id: 2, Name: '>' },
        { Id: 3, Name: '>=' },
        { Id: 4, Name: '<' },
        { Id: 5, Name: '<=' }
    ];
    Disable = [
        { Id: true, Name: 'Đang sử dụng', Checked: false, BadgeClass: 'badge-success', },
        { Id: false, Name: 'Không sử dụng', Checked: false, BadgeClass: 'badge-danger', },
    ];
    EmployeeStatus = [
        { Id: 1, Name: 'Đang làm việc', Checked: false, BadgeClass: 'badge-success', },
        { Id: 2, Name: 'Đã nghỉ việc', Checked: false, BadgeClass: 'badge-danger', },
    ];
    ProductStatus = [
        { Id: 0, Name: 'Chưa kiểm tra', Checked: false, BadgeClass: 'badge-warning' },
        { Id: 1, Name: 'Đạt', Checked: false, BadgeClass: 'badge-success' },
        { Id: 2, Name: 'Không đạt', Checked: false, BadgeClass: 'badge-danger' },
    ];
    TestRequestStatus = [
        { Id: 0, Name: 'Chờ xác nhận', Checked: false, BadgeClass: 'badge-danger' },
        { Id: 1, Name: 'Đang chờ mẫu', Checked: false, BadgeClass: 'badge-warning' },
        { Id: 2, Name: 'Đang chờ kiểm tra', Checked: false, BadgeClass: 'badge-warning' },
        { Id: 3, Name: 'Đang kiểm tra', Checked: false, BadgeClass: 'badge-primary' },
        { Id: 4, Name: 'Đã kiểm tra', Checked: false, BadgeClass: 'badge-success' },
    ];
    TestRequestProductResult = [
        { Id: 0, Name: 'Chưa kiểm tra', Checked: false, BadgeClass: 'badge-warning' },
        { Id: 1, Name: 'Đạt', Checked: false, BadgeClass: 'badge-success' },
        { Id: 2, Name: 'Không đạt', Checked: false, BadgeClass: 'badge-danger' },
    ];

    listStatus: any[] = [
        { Id: 1, Name: "Nháp", BadgeClass: 'badge-warning', },
        { Id: 2, Name: "Hoàn thành", BadgeClass: 'badge-success', },
        { Id: 3, Name: "Từ chối", BadgeClass: 'badge-danger', },
        { Id: 4, Name: "Tất cả", BadgeClass: '', }
    ]

    Allocation: any[] = [
        { Id: 1, Name: "Nháp", BadgeClass: 'badge-danger', },
        { Id: 2, Name: "Đã phân bổ", BadgeClass: 'badge-success', }
    ]

    AllocationStatus: any[] = [
        { Id: 1, Name: "Admin duyệt", BadgeClass: 'badge-success', },
        { Id: 2, Name: "Chủ nhiệm duyệt", BadgeClass: 'badge-warning', },
        { Id: 3, Name: "Chủ nhiệm từ chối", BadgeClass: 'badge-danger', },
        { Id: 4, Name: "Chủ nhiệm chốt", BadgeClass: 'badge-success', },
        { Id: 5, Name: "Phân bổ", BadgeClass: 'badge-success', },
    ]

    AllocationStatusSearch: any[] = [
        { Id: 1, Name: "Phân bổ", BadgeClass: 'badge-success', },
        { Id: 2, Name: "Admin duyệt", BadgeClass: 'badge-success', },
        { Id: 3, Name: "Chủ nhiệm duyệt", BadgeClass: 'badge-warning', },
        //{ Id: 4, Name: "Chủ nhiệm từ chối", BadgeClass: 'badge-danger', },
        { Id: 5, Name: "Chủ nhiệm chốt", BadgeClass: 'badge-success', },
    ]

    list: any[] = [
        { id: '1', code: 'PBCP-NCSX-2020', name: 'Dự án 5G', status: 1, description: '', },
        { id: '2', code: 'PBCP-NCSX-2019', name: 'Dự án 5G', status: 1, description: '' },
        { id: '3', code: 'PBCP-NCSX-2018', name: 'Dự án 4G', status: 2, description: '' },
        { id: '4', code: 'PBCP-NCSX-2017', name: 'Phân bổ chi phí nhân công 2017', status: 2, description: '' },
        { id: '5', code: 'PBCP-NCSX-2016', name: 'Phần mềm phân bổ chi phí', status: 3, description: '' },
        { id: '6', code: 'PBCP-NCSX-2015', name: 'Phân bổ chi phí sản xuất', status: 3, description: '' },
        { id: '7', code: 'PBCP-NCSX-2014', name: 'PBCP-NCSX-2014', status: 2, description: '' },
        { id: '8', code: 'PBCP-NCSX-2013', name: 'PBCP-NCSX-2013', status: 2, description: '' },
        { id: '9', code: 'PBCP-NCSX-2012', name: 'PBCP-NCSX-2012', status: 2, description: '' },
        { id: '10', code: 'PBCP-NCSX-2011', name: 'PBCP-NCSX-2011', status: 2, description: '' },
        { id: '11', code: 'PBCP-NCSX-2010', name: 'PBCP-NCSX-2010', status: 2, description: '' },
    ];

    listTask: any[] = [
        { id: '1', name: 'Thiết kế', employee: 1, time: 2.9 },
        { id: '2', name: 'Lập trình', employee: 1, time: 3.0 },
        { id: '3', name: 'Test', employee: 1, time: 3.5 },
    ];

    EmployeesStatus: any[] = [
        { Id: 1, Name: "Đang tham gia", BadgeClass: 'badge-warning', },
        { Id: 2, Name: "Kết thúc", BadgeClass: 'badge-success', },
        // { Id: 3, Name: "Từ chối", BadgeClass: 'badge-danger', },
        // { Id: 4, Name: "Tất cả", BadgeClass: '', }
    ]

    MissionProfileLevel: any[] = [
        { Id: 1, Name: 'Tập đoàn' },
        { Id: 2, Name: 'Cơ sở' },
    ];

    MissionProfileUnit: any[] = [
        { Id: 1, Name: 'Tập đoàn Công nghiệp Viễn thông Quân đội' },
        { Id: 2, Name: 'Phòng hành chính' },
        { Id: 3, Name: 'Phòng kế toán' },
    ];

    MissionProfileStatus: any[] = [
        { Id: 0, Name: "Tất cả", BadgeClass: '', },
        { Id: 1, Name: "Đang chạy", BadgeClass: 'badge-warning', },
        { Id: 2, Name: "Kết thúc", BadgeClass: 'badge-success', },
    ]

    PriceStatus: any[] = [
        { Id: 0, Name: "Tất cả", BadgeClass: '', },
        { Id: 1, Name: "Quá thấp", BadgeClass: 'badge-secondary', },
        { Id: 2, Name: "Sắp vượt dự toán", BadgeClass: 'badge-warning', },
        { Id: 3, Name: "Đã vượt dự toán", BadgeClass: 'badge-danger', },
    ]

    SearchDataType = {
        Sample: 1,
        QuestionType: 2,
    };

    ListPageSize = [5, 10, 15, 20, 25, 30];
    PageSizeFours = [9, 12, 15, 18, 21, 24];

    validEmailRegEx = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    MissionStatus: any[] = [
        { Id: 0, Name: "Tất cả", BadgeClass: '', },
        { Id: 1, Name: "Đăng ký", BadgeClass: 'badge-warning', },
        { Id: 2, Name: "Xét duyệt", BadgeClass: 'badge-info', },
        { Id: 3, Name: "Giao nhiệm vụ", BadgeClass: 'badge-primary', },
        { Id: 4, Name: "Dừng nhiệm vụ", BadgeClass: 'badge-danger', },
        { Id: 5, Name: "Hủy nhiệm vụ", BadgeClass: 'badge-dark', },
        { Id: 6, Name: "Nghiệm thu", BadgeClass: 'badge-success', },
    ]

    MissionGroup: any[] = [
        { Id: 1, Name: "Viễn Thông" },
        { Id: 2, Name: "Điện Tử" }
    ]

    listProcedureStatus: any[] = [
        { Id: 1, Name: "Dừng áp dụng", BadgeClass: 'badge-warning', },
        { Id: 2, Name: "Còn áp dụng", BadgeClass: 'badge-success', },
    ];

    listPayStatus: any[] = [
        { Id: 1, Name: "Sắp đến hạn", BadgeClass: 'badge-warning', },
        { Id: 2, Name: "Đã quá hạn", BadgeClass: 'badge-danger', },
    ];

    listUnitType: any[] = [
        { Id: 1, Name: "Diện tích" },
        { Id: 2, Name: "Khối lượng" },
        { Id: 3, Name: "Thể tích" },
        { Id: 4, Name: "Độ dài" },
        { Id: 5, Name: "Tiền tệ" },
        { Id: 6, Name: "Khác" },
    ];

    listGioiTinh: any[] = [
        { Id: 1, Name: "Nam", BadgeClass: 'badge-warning', },
        { Id: 2, Name: "Nữ", BadgeClass: 'badge-success', },
        { Id: 3, Name: "Khác", BadgeClass: 'badge-success', },
    ];

    danhSachNgach: any[] = [
        { Id: 1, Name: "SQ" },
        { Id: 2, Name: "QNCN" },
        { Id: 3, Name: "HSC-CS" },
        { Id: 4, Name: "CNVQP" },
    ];

    danhSachTrinhDo: any[] = [
        { Id: 1, Name: "Tiến sĩ" },
        { Id: 2, Name: "Thạc sĩ" },
        { Id: 3, Name: "Đại học" },
        { Id: 4, Name: "Cao đẳng" },
        { Id: 5, Name: "Trung cấp" },
        { Id: 6, Name: "Sơ cấp" },
    ];

    danhSachTinhTrangCanBo: any[] = [
        { Id: 1, Name: "Đang công tác" },
        { Id: 2, Name: "Nghỉ hưu" },
        { Id: 3, Name: "Xuất ngũ" },
        { Id: 4, Name: "Chuyển đơn vị khác không thuộc PCMT" },
    ];

    DanhMuc: string = "DanhMuc";
    User_Name: string = "admin";
    User_Status = [
        { Id: 1, Name: 'Đang hoạt động', Checked: false, BadgeClass: 'badge-success', },
        { Id: 2, Name: 'Không hoạt động', Checked: false, BadgeClass: 'badge-danger', },
    ];

    UserHistory_Type: any = [
        { Id: 1, Name: 'Đăng nhập', Checked: false },
        { Id: 2, Name: 'Khai thác dữ liệu', Checked: false },
    ];

    tinhTrangCanBo: any[] = [
        { Id: 1, Name: "Đang công tác" },
        { Id: 2, Name: "Nghỉ hưu" },
        { Id: 3, Name: "Xuất ngũ" },
        { Id: 4, Name: "Chuyển đơn vị khác không thuộc PCMT" },
    ];

    CanBo_Ngach: any[] = [
        { Id: 1, Name: 'SQ', Checked: false },
        { Id: 2, Name: 'QNCN', Checked: false },
        { Id: 3, Name: 'HSQ - CS', Checked: false },
        { Id: 4, Name: 'CNVQP', Checked: false },
    ];

    CanBo_TinhTrang: any[] = [
        { Id: 1, Name: 'Đang công tác', Checked: false },
        { Id: 2, Name: 'Nghỉ hưu', Checked: false },
        { Id: 3, Name: 'Xuất ngũ', Checked: false },
        { Id: 4, Name: 'Chuyển đơn vị khác không thuộc PCMT', Checked: false },
    ];

    CanBo_TrinhDo: any[] = [
        { Id: 1, Name: 'Tiến sĩ', Checked: false },
        { Id: 2, Name: 'Thạc sĩ', Checked: false },
        { Id: 3, Name: 'Đại học', Checked: false },
        { Id: 4, Name: 'Cao đẳng', Checked: false },
        { Id: 5, Name: 'Trung cấp', Checked: false },
        { Id: 6, Name: 'Sơ cấp', Checked: false },
    ];

    CanBoGapNan_TinhTrang: any[] = [
        { Id: 1, Name: 'Hy sinh', Checked: false },
        { Id: 2, Name: 'Bị thương', Checked: false },
        { Id: 3, Name: 'Phơi nhiễm HIV', Checked: false },
    ];

    CanBoGapNan_CheDoCS: any[] = [
        { Id: 1, Name: 'Liệt sĩ', Checked: false },
        { Id: 2, Name: 'Thương binh', Checked: false },
        { Id: 3, Name: 'Trợ cấp khác', Checked: false },
    ];

    VanBan_HieuLuc: any[] = [
        { Id: true, Name: 'Còn hiệu lực', Checked: false },
        { Id: false, Name: 'Hết hiệu lực', Checked: false },
    ];

    HieuLuc: any[] = [
        { Id: 1, Name: 'Còn hiệu lực', Checked: false },
        { Id: 2, Name: 'Hết hiệu lực', Checked: false },
    ];

    TrangBiKyThuat_PhanCapSuDung: any[] = [
        { Id: 1, Name: 'Cấp 1', Checked: false },
        { Id: 2, Name: 'Cấp 2', Checked: false },
        { Id: 3, Name: 'Cấp 3', Checked: false },
        { Id: 4, Name: 'Cấp 4', Checked: false },
        { Id: 5, Name: 'Cấp 5', Checked: false },
    ];

    DoiTuongXNCTP_DienDoiTuong: any[] = [
        { Id: 1, Name: "Xuất cảnh" },
        { Id: 2, Name: "Nhập cảnh" },
        { Id: 3, Name: "Tiếp nhận" },
        { Id: 4, Name: "Trao trả" },
        { Id: 5, Name: "Buộc quay lại" },
    ];

    DoiTuongXNCTP_XuLy: any[] = [
        { Id: 1, Name: "Xử lý VPHC" },
        { Id: 2, Name: "Khởi tố" },
    ];

    TrangThai_CanBo: any[] = [
        { Id: 1, Name: "Đang công tác", Checked: false, BadgeClass: 'badge-success' },
        { Id: 2, Name: "Nghỉ hưu", Checked: false, BadgeClass: 'badge-info' },
        { Id: 3, Name: "Xuất ngũ", Checked: false, BadgeClass: 'badge-secondary' },
        { Id: 4, Name: "Chuyển đơn vị", Checked: false, BadgeClass: 'badge-primary' },
        { Id: 5, Name: "Hy sinh", Checked: false, BadgeClass: 'badge-warning' },
        { Id: 6, Name: "Chết", Checked: false, BadgeClass: 'badge-danger' },
    ]

    DoiTuongTruyNa_LoaiDoiTuong: any[] = [
        { Id: 1, Name: "Bị can, bị cáo bỏ trốn" },
        { Id: 2, Name: "Người bị kết án trục xuất, người chấp hành án phạt trục xuất bỏ trốn" },
        { Id: 3, Name: "Người bị kết án phạt tù bỏ trốn" },
        { Id: 4, Name: "Người bị kết án tử hình bỏ trốn" },
        { Id: 5, Name: "Người đang chấp hành án phạt tù, người được tạm đình chỉ chấp hành án phạt tù, người được hoãn chấp hành án bỏ trốn." },
    ]

    MoreOrLessStatus: any[] = [
        { Id: 1, Name: "Tăng", Checked: false, BadgeClass: 'badge-danger' },
        { Id: 0, Name: "" },
        { Id: -1, Name: "Giảm", Checked: false, BadgeClass: 'badge-success' }
    ];

    SoSanhCategory: any[] = [
        { Id: 1, Name: "Lực lượng mật" },
        { Id: 2, Name: "Đối tượng quản lý nghiệp vụ" },
        { Id: 3, Name: "Đối tượng kiểm tra nghiệp vụ" },
        { Id: 4, Name: "Đối tượng nghiện" },
        { Id: 5, Name: "Nạn nhân mua bán người" },
        { Id: 6, Name: "Xuất nhập cảnh trái phép" },
        { Id: 7, Name: "Chuyên án" },
        { Id: 8, Name: "Tiếp nhận tố giác, tin báo tội phạm" },
        { Id: 9, Name: "Điều tra hình sự" },
        { Id: 10, Name: "Xử lý vi phạm hành chính" },
    ];

    TimeTypeReport: any[] = [
        { Id: "2", Name: "Tháng" },
        { Id: "3", Name: "6 Tháng" },
        { Id: "4", Name: "9 Tháng" },
        { Id: "5", Name: "Năm" }
    ];

    TienTrinhXL_TNTB_Status: any[] = [
        { Id: 0, Name: "Tiếp nhận, phát hiện", Checked: false, BadgeClass: 'badge-secondary' },
        { Id: 1, Name: "Điều tra, xác minh", Checked: false, BadgeClass: 'badge-info' },
        { Id: 2, Name: "Xử lý, giải quyết", Checked: false, BadgeClass: 'badge-warning' },
        { Id: 3, Name: "Kết thúc", Checked: false, BadgeClass: 'badge-success' }
    ];
    TienTrinhXL_VPHC_Status: any[] = [
        { Id: 0, Name: "Tiếp nhận, phát hiện", Checked: false, BadgeClass: 'badge-secondary' },
        { Id: 1, Name: "Kiểm tra, xác minh", Checked: false, BadgeClass: 'badge-info' },
        { Id: 2, Name: "Xử lý", Checked: false, BadgeClass: 'badge-warning' },
        { Id: 3, Name: "Kết thúc", Checked: false, BadgeClass: 'badge-success' }
    ];
    TienTrinhXL_DTHS_Status: any[] = [
        { Id: 0, Name: "Tiếp nhận, phát hiện", Checked: false, BadgeClass: 'badge-secondary' },
        { Id: 1, Name: "Điều tra, xác minh", Checked: false, BadgeClass: 'badge-info' },
        { Id: 2, Name: "Xử lý", Checked: false, BadgeClass: 'badge-warning' },
        { Id: 3, Name: "Kết thúc", Checked: false, BadgeClass: 'badge-success' }
    ];

    SuaDoiSauKetThuc = [
        { Id: 1, Name: 'Có sửa đổi', Checked: false, BadgeClass: 'badge-success', },
        { Id: 2, Name: 'Không sửa đổi', Checked: false, BadgeClass: 'badge-danger', },
    ];

    /// <summary>
    /// Ngày qua
    /// </summary>
    TimeType_Yesterday = "2";

    /// <summary>
    /// Tuần này
    /// </summary>
    TimeType_ThisWeek = "3";

    /// <summary>
    /// Tuần trước
    /// </summary>
    TimeType_LastWeek = "4";

    /// <summary>
    /// 7 ngày gần đây
    /// </summary>
    TimeType_SevenDay = "5";

    /// <summary>
    /// Tháng này
    /// </summary>
    TimeType_ThisMonth = "6";

    /// <summary>
    /// Tháng trước
    /// </summary>
    TimeType_LastMonth = "7";

    /// <summary>
    /// Tháng
    /// </summary>
    TimeType_Month = "8";

    /// <summary>
    /// Quý
    /// </summary>
    TimeType_Quarter = "9";

    /// <summary>
    /// Năm nay
    /// </summary>
    TimeType_ThisYear = "10";

    /// <summary>
    /// Năm trước
    /// </summary>
    TimeType_LastYear = "11";

    /// <summary>
    /// Năm 
    /// </summary>
    TimeType_Year = "12";

    /// <summary>
    /// Khoảng thời gian
    /// </summary>
    TimeType_Between = "13";

    /// <summary>
    /// Quý này
    /// </summary>
    TimeType_ThisQuarter = "14";

    /// <summary>
    /// Quý trước
    /// </summary>
    TimeType_LastQuarter = "15";
}