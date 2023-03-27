import type { GetSummaryListDto, SummaryDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SummaryService {
  apiName = 'Default';
  

  getList = (input: GetSummaryListDto) =>
    this.restService.request<any, PagedResultDto<SummaryDto>>({
      method: 'GET',
      url: '/api/app/summary',
      params: { landComplain: input.landComplain, enviromentComplain: input.enviromentComplain, waterComplain: input.waterComplain, mineralComplain: input.mineralComplain, landDenounce: input.landDenounce, enviromentDenounce: input.enviromentDenounce, waterDenounce: input.waterDenounce, mineralDenounce: input.mineralDenounce, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
