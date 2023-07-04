import type { GetSumaryMapDto, GetSummaryListDto, SummaryChartDto, SummaryDto, SummaryMapDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SummaryService {
  apiName = 'Default';
  

  getChart = () =>
    this.restService.request<any, SummaryChartDto>({
      method: 'GET',
      url: '/api/app/summary/chart',
    },
    { apiName: this.apiName });
  

  getExcel = (input: GetSummaryListDto) =>
    this.restService.request<any, number[]>({
      method: 'GET',
      url: '/api/app/summary/excel',
      params: { loaiVuViec: input.loaiVuViec, linhVuc: input.linhVuc, landComplain: input.landComplain, enviromentComplain: input.enviromentComplain, waterComplain: input.waterComplain, mineralComplain: input.mineralComplain, landDenounce: input.landDenounce, enviromentDenounce: input.enviromentDenounce, waterDenounce: input.waterDenounce, mineralDenounce: input.mineralDenounce, maTinhTP: input.maTinhTP, maQuanHuyen: input.maQuanHuyen, maXaPhuongTT: input.maXaPhuongTT, fromDate: input.fromDate, toDate: input.toDate, ketQua: input.ketQua, congKhai: input.congKhai, nguoiNopDon: input.nguoiNopDon, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getList = (input: GetSummaryListDto) =>
    this.restService.request<any, PagedResultDto<SummaryDto>>({
      method: 'GET',
      url: '/api/app/summary',
      params: { loaiVuViec: input.loaiVuViec, linhVuc: input.linhVuc, landComplain: input.landComplain, enviromentComplain: input.enviromentComplain, waterComplain: input.waterComplain, mineralComplain: input.mineralComplain, landDenounce: input.landDenounce, enviromentDenounce: input.enviromentDenounce, waterDenounce: input.waterDenounce, mineralDenounce: input.mineralDenounce, maTinhTP: input.maTinhTP, maQuanHuyen: input.maQuanHuyen, maXaPhuongTT: input.maXaPhuongTT, fromDate: input.fromDate, toDate: input.toDate, ketQua: input.ketQua, congKhai: input.congKhai, nguoiNopDon: input.nguoiNopDon, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getLogBookExcel = (input: GetSummaryListDto) =>
    this.restService.request<any, number[]>({
      method: 'GET',
      url: '/api/app/summary/log-book-excel',
      params: { loaiVuViec: input.loaiVuViec, linhVuc: input.linhVuc, landComplain: input.landComplain, enviromentComplain: input.enviromentComplain, waterComplain: input.waterComplain, mineralComplain: input.mineralComplain, landDenounce: input.landDenounce, enviromentDenounce: input.enviromentDenounce, waterDenounce: input.waterDenounce, mineralDenounce: input.mineralDenounce, maTinhTP: input.maTinhTP, maQuanHuyen: input.maQuanHuyen, maXaPhuongTT: input.maXaPhuongTT, fromDate: input.fromDate, toDate: input.toDate, ketQua: input.ketQua, congKhai: input.congKhai, nguoiNopDon: input.nguoiNopDon, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getMap = (input: GetSumaryMapDto) =>
    this.restService.request<any, SummaryMapDto[]>({
      method: 'GET',
      url: '/api/app/summary/map',
      params: { loaiVuViec: input.loaiVuViec, linhVuc: input.linhVuc, landComplain: input.landComplain, enviromentComplain: input.enviromentComplain, waterComplain: input.waterComplain, mineralComplain: input.mineralComplain, landDenounce: input.landDenounce, enviromentDenounce: input.enviromentDenounce, waterDenounce: input.waterDenounce, mineralDenounce: input.mineralDenounce, maTinhTP: input.maTinhTP, maQuanHuyen: input.maQuanHuyen, maXaPhuongTT: input.maXaPhuongTT, fromDate: input.fromDate, toDate: input.toDate, ketQua: input.ketQua, congKhai: input.congKhai, keyword: input.keyword, nguoiNopDon: input.nguoiNopDon },
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
