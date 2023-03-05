import { Injectable } from '@angular/core';
import * as moment from 'moment';
import { Complain } from './Complain';

@Injectable({
  providedIn: 'root',
})
export class MockService {
  fullNames = [
    'Nguyễn Văn A',
    'Trần Thị B',
    'Lê Công D',
    'Phạm Thị E',
    'Đặng Văn F',
    'Hoàng Thị G',
    'Vũ Thị H',
    'Đỗ Thị I',
    'Bùi Thị J',
    'Hồ Thị K',
    'Nguyễn Thị L',
    'Đổ Thị M',
    'Trần Công N',
    'Phạm Công O',
    'Lê Văn P',
    'Đặng Thị Q',
    'Vũ Công R',
    'Hoàng Văn S',
    'Bùi Công T',
    'Hồ Công U',
  ];

  districts = [
    'Ba Đình',
    'Hoàn Kiếm',
    'Tây Hồ',
    'Long Biên',
    'Cầu Giấy',
    'Đống Đa',
    'Hai Bà Trưng',
    'Hoàng Mai',
    'Thanh Xuân',
    'Nam Từ Liêm',
  ];
  typeHoSo = [0, 1];
  FieldsHoSo = [0, 1, 2, 3];
  loaiHS = ['khiếu nại', 'Tố cáo'];
  linhVuc = ['Đất đai', 'Môi trường', 'Tài nguyên nước', 'Khoáng sản'];
  results = [true, false];

  constructor() {}
  mockData(): Complain[] {
    let hoSos = [];
    for (let index = 0; index < 500; index++) {
      let item = new Complain();
      let randomDate = this.randomDates();
      // item.code = this.genCode();
      item.code = 'HS' + index;
      item.sender = this.randomItemInArr(this.fullNames);
      item.area = this.randomItemInArr(this.districts);
      item.sentDate = randomDate[0];
      item.typeHoSo = this.randomItemInArr(this.typeHoSo);
      item.fieldType = this.randomItemInArr(this.FieldsHoSo);
      item.title = `${this.loaiHS[item.typeHoSo]} ${this.linhVuc[item.fieldType]} ở ${item.area}`;

      item.returnDate1 = randomDate[1];
      item.result1 = this.randomItemInArr(this.results);
      if (!item.result1) {
        item.returnDate2 = randomDate[2];
        item.result2 = this.randomItemInArr(this.results);
      }
      item.latLng = this.generateCoordinates();
      hoSos.push(item);
    }
    return hoSos;
  }

  randomItemInArr(arr: any[]) {
    const index = Math.floor(Math.random() * arr.length);
    return arr[index];
  }
  genCode(): string {
    const randomNumber = Math.floor(Math.random() * 1000000)
      .toString()
      .padStart(6, '0');
    return `HS${randomNumber}`;
  }
  generateCoordinates(): [number, number] {
    const latitude = Math.random() * (21.3 - 21.0) + 21.0;
    const longitude = Math.random() * (105.9 - 105.5) + 105.5;
    return [latitude, longitude];
  }
  randomDates(): [Date, Date, Date] {
    const start = moment('2022-01-01', 'YYYY-MM-DD');
    const end = moment('2023-12-31', 'YYYY-MM-DD');
    const randomDate1 = moment(start.valueOf() + Math.random() * (end.valueOf() - start.valueOf()));
    const randomDate2 = moment(
      randomDate1.valueOf() + Math.random() * (end.valueOf() - randomDate1.valueOf())
    );
    const randomDate3 = moment(
      randomDate2.valueOf() + Math.random() * (end.valueOf() - randomDate2.valueOf())
    );
    return [randomDate1.toDate(), randomDate2.toDate(), randomDate3.toDate()];
  }
}
