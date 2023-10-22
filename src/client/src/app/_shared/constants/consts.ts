import { TrangThai, LoaiKetQua, LoaiKhieuNai, LoaiVuViec } from '@proxy';
import { UserType } from '@proxy/user-type.enum';

export const KetquaOptions = ['Chưa có KQ', 'Đúng', 'Có Đúng/Có Sai', 'Sai'];
export const LinhVucOptions = ['', 'Đất đai', 'Môi trường', 'Khoáng sản', 'Tài nguyên nước'];
export const LoaiVuViecOptions = ['', 'Khiếu nại/Khiếu kiện', 'Tố cáo'];
export const TrangThaiOptions = ['Tiếp nhận', 'Đang xử lý', 'Đã thụ lý', 'Đã kết luận','Rút đơn','Trả lại đơn','Chuyển đơn','Khóa hồ sơ'];
export const loaiVuViecNameOptions = [
  { value: LoaiVuViec.TatCa, text: 'Tất cả' },
  { value: LoaiVuViec.KhieuNai, text: 'Khiếu nại' },
  { value: LoaiVuViec.ToCao, text: 'Tố cáo' },
];
export const linhVucNameOptions = [
  { value: 0, text: 'Tất cả' },
  { value: 1, text: 'Đất đai' },
  { value: 2, text: 'Môi trường' },
  { value: 3, text: 'Khoáng sản' },
  { value: 4, text: 'Tài nguyên nước' },
];
export const congKhaiOptions = [
  { value: true, text: 'Công khai' },
  { value: false, text: 'Không công khai' },
];
export const trangthaiOptions = [
  { value: TrangThai.TiepNhan, text: 'Tiếp nhận' },
  { value: TrangThai.DangXuLy, text: 'Đang xử lý' },
  { value: TrangThai.DaThuLy, text: 'Đã thụ lý' },
  { value: TrangThai.DaKetLuan, text: 'Đã kết luận' },
  { value: TrangThai.RutDon, text: 'Rút đơn' },
  { value: TrangThai.TraLaiDon, text: 'Trả lại đơn' },
  { value: TrangThai.ChuyenDon, text: 'Chuyển đơn' },
];
export const loaiKQOptions = [
  { value: LoaiKetQua.Dung, text: 'Đúng' },
  { value: LoaiKetQua.CoDungCoSai, text: 'Có Đúng/Có Sai' },
  { value: LoaiKetQua.Sai, text: 'Sai' },
];
export const giaiDoanOptions = [
  { value: 0, text: 'Tất cả' },
  { value: 1, text: 'Khiếu nại lần I' },
  { value: 2, text: 'Khiếu nại lần II' },
];
export const loaiKhieuNaiOptions = [
  { value: LoaiKhieuNai.KhieuNai, text: 'Khiếu nại' },
  { value: LoaiKhieuNai.KhieuKien, text: 'Khiếu kiện' },
];
export const userTypeOptions = [
  { value: UserType.QuanLyTinh, text: 'Quản lý đơn thư trong phạm vi toàn Tỉnh' },
  { value: UserType.QuanLyHuyen, text: 'Quản lý đơn thư trong phạm vi: Thành phố (thuộc Tỉnh)/Quận/Huyện' },
  { value: UserType.QuanLyXa, text: 'Quản lý đơn thư trong phạm vi: Phường/Xã' },
];
