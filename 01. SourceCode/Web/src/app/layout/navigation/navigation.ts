import { NtsNavigation } from './navigationitem';

export const navigation: NtsNavigation[] = [
    {
        id: 'Trangchu',
        title: 'Trang chủ',
        icon: 'home',
        type: 'item',
        permission: ['F000000'],
        ismdi: true,
        url: '/trang-chu'
    },
    {
        id: 'QuanTri',
        title: 'Quản trị hệ thống',
        icon: 'setting',
        type: 'collapsable',
        permission: ['F000550', 'F000800', 'F000950', 'F001000', 'F001250'],
        ismdi: true,
        children: [
            {
                id: 'QuanLyDanhMuc',
                title: 'Quản lý danh mục',
                icon: 'category',
                type: 'item',
                permission: ['F000550'],
                url: '/danh-muc/tat-ca-danh-muc'
            },
            {
                id: 'TaiKhoan',
                title: 'Tài khoản',
                icon: 'user',
                type: 'collapsable',
                permission: ['F001250', 'F000950', 'F001000'],
                ismdi: true,
                children: [
                    {
                        id: 'NhomNguoiDung',
                        title: 'Nhóm người dùng',
                        icon: 'group-user',
                        type: 'item',
                        permission: ['F001000'],
                        url: '/nguoi-dung/nhom-nguoi-dung'
                    },
                    {
                        id: 'NguoiDung',
                        title: 'Người dùng',
                        icon: 'user-login',
                        type: 'item',
                        permission: ['F000950'],
                        url: '/nguoi-dung/tai-khoan'
                    },
                    {
                        id: 'LichSuThaoTac',
                        title: 'Lịch sử thao tác',
                        icon: 'history',
                        type: 'item',
                        permission: ['F001250'],
                        url: '/nguoi-dung/tra-cuu-lich-su'
                    },
                ]
            },
        ]
    },
];