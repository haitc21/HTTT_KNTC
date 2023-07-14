using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace KNTC.Summaries;

public interface ISummaryAppService
{
    Task<PagedResultDto<SummaryDto>> GetListAsync(GetSummaryListDto input);

    Task<byte[]> GetExcelAsync(GetSummaryListDto input);

    Task<byte[]> GetLogBookExcelAsync(GetSummaryListDto input);
    Task<byte[]> GetReportExcelAsync(GetSummaryListDto input);

    Task<List<SummaryMapDto>> GetMapAsync(GetSumaryMapDto input);

    Task<SummaryChartDto> GetChartAsync();
}