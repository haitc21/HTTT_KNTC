using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace KNTC.Summaries;

public interface ISummaryAppService
{
    Task<PagedResultDto<SummaryDto>> GetListAsync(GetSummaryListDto input);
    Task<byte[]> GetExcelAsync(GetSummaryListDto input);
    Task<List<SummaryDto>> GetMapAsync(GetSumaryMapDto input);
    Task<SumaryChartDto> GetChartAsync();
}
