import { LoaiKetQua, LoaiKhieuNai } from '@proxy';

export const KetquaOptions = ['Chưa có KQ', 'Đúng', 'Có Đúng/Có Sai', 'Sai'];
export const LinhVucNameOptions = ['', 'Đất đai', 'Môi trường', 'Khoáng sản', 'Tài nguyên nước'];
export const LoaiVuViecOptions = ['', 'Khiếu nại/Khiếu kiện', 'Tố cáo'];
export const congKhaiOptions = [
  { value: true, text: 'Công khai' },
  { value: false, text: 'Không công khai' },
];
export const loaiKQOptions = [
  { value: LoaiKetQua.ChuaCoKQ, text: 'Chưa có KQ' },
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
