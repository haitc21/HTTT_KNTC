import type { GetSumaryMapDto, GetSummaryListDto, SummaryChartDto, SummaryDto, SummaryMapDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SummaryService {
  apiName = 'Default';
  

  getChart = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, SummaryChartDto>({
      method: 'GET',
      url: '/api/app/summary/chart',
    },
    { apiName: this.apiName,...config });
  

  getExcel = (input: GetSummaryListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, number[]>({
      method: 'GET',
      url: '/api/app/summary/excel',
      params: { loaiVuViec: input.loaiVuViec, linhVuc: input.linhVuc, landComplain: input.landComplain, enviromentComplain: input.enviromentComplain, waterComplain: input.waterComplain, mineralComplain: input.mineralComplain, landDenounce: input.landDenounce, enviromentDenounce: input.enviromentDenounce, waterDenounce: input.waterDenounce, mineralDenounce: input.mineralDenounce, maTinhTP: input.maTinhTP, maQuanHuyen: input.maQuanHuyen, maXaPhuongTT: input.maXaPhuongTT, fromDate: input.fromDate, toDate: input.toDate, ketQua: input.ketQua, congKhai: input.congKhai, trangThai: input.trangThai, nguoiNopDon: input.nguoiNopDon, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetSummaryListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<SummaryDto>>({
      method: 'GET',
      url: '/api/app/summary',
      params: { loaiVuViec: input.loaiVuViec, linhVuc: input.linhVuc, landComplain: input.landComplain, enviromentComplain: input.enviromentComplain, waterComplain: input.waterComplain, mineralComplain: input.mineralComplain, landDenounce: input.landDenounce, enviromentDenounce: input.enviromentDenounce, waterDenounce: input.waterDenounce, mineralDenounce: input.mineralDenounce, maTinhTP: input.maTinhTP, maQuanHuyen: input.maQuanHuyen, maXaPhuongTT: input.maXaPhuongTT, fromDate: input.fromDate, toDate: input.toDate, ketQua: input.ketQua, congKhai: input.congKhai, trangThai: input.trangThai, nguoiNopDon: input.nguoiNopDon, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getLogBookExcel = (input: GetSummaryListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, number[]>({
      method: 'GET',
      url: '/api/app/summary/log-book-excel',
      params: { loaiVuViec: input.loaiVuViec, linhVuc: input.linhVuc, landComplain: input.landComplain, enviromentComplain: input.enviromentComplain, waterComplain: input.waterComplain, mineralComplain: input.mineralComplain, landDenounce: input.landDenounce, enviromentDenounce: input.enviromentDenounce, waterDenounce: input.waterDenounce, mineralDenounce: input.mineralDenounce, maTinhTP: input.maTinhTP, maQuanHuyen: input.maQuanHuyen, maXaPhuongTT: input.maXaPhuongTT, fromDate: input.fromDate, toDate: input.toDate, ketQua: input.ketQua, congKhai: input.congKhai, trangThai: input.trangThai, nguoiNopDon: input.nguoiNopDon, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getMap = (input: GetSumaryMapDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SummaryMapDto[]>({
      method: 'GET',
      url: '/api/app/summary/map',
      params: { loaiVuViec: input.loaiVuViec, linhVuc: input.linhVuc, landComplain: input.landComplain, enviromentComplain: input.enviromentComplain, waterComplain: input.waterComplain, mineralComplain: input.mineralComplain, landDenounce: input.landDenounce, enviromentDenounce: input.enviromentDenounce, waterDenounce: input.waterDenounce, mineralDenounce: input.mineralDenounce, maTinhTP: input.maTinhTP, maQuanHuyen: input.maQuanHuyen, maXaPhuongTT: input.maXaPhuongTT, fromDate: input.fromDate, toDate: input.toDate, ketQua: input.ketQua, congKhai: input.congKhai, trangThai: input.trangThai, keyword: input.keyword, nguoiNopDon: input.nguoiNopDon },
    },
    { apiName: this.apiName,...config });
  

  getReportExcel = (input: GetSummaryListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, number[]>({
      method: 'GET',
      url: '/api/app/summary/report-excel',
      params: { loaiVuViec: input.loaiVuViec, linhVuc: input.linhVuc, landComplain: input.landComplain, enviromentComplain: input.enviromentComplain, waterComplain: input.waterComplain, mineralComplain: input.mineralComplain, landDenounce: input.landDenounce, enviromentDenounce: input.enviromentDenounce, waterDenounce: input.waterDenounce, mineralDenounce: input.mineralDenounce, maTinhTP: input.maTinhTP, maQuanHuyen: input.maQuanHuyen, maXaPhuongTT: input.maXaPhuongTT, fromDate: input.fromDate, toDate: input.toDate, ketQua: input.ketQua, congKhai: input.congKhai, trangThai: input.trangThai, nguoiNopDon: input.nguoiNopDon, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
