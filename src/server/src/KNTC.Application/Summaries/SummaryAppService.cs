using KNTC.Complains;
using KNTC.Denounces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using System.Linq.Dynamic.Core;
using KNTC.DocumentTypes;
using KNTC.SpatialDatas;

namespace KNTC.Summaries;

public class SummaryAppService : KNTCAppService, ISummaryAppService
{
    private readonly IComplainRepository _complainRepo;
    private readonly IDenounceRepository _denounceRepo;
    private readonly ISummaryRepository _summaryRepo;
    public SummaryAppService(IComplainRepository complainRepo, IDenounceRepository denounceRepo, ISummaryRepository summaryRepo)
    {
        _complainRepo = complainRepo;
        _denounceRepo = denounceRepo;
        _summaryRepo = summaryRepo;
    }

    public async Task<PagedResultDto<SummaryDto>> GetListAsync(GetSummaryListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = $"{nameof(SummaryDto.MaHoSo)}";
        }
        var query = await _summaryRepo.GetListAsync(input.LandComplain,
                                                    input.EnviromentComplain,
                                                    input.WaterComplain,
                                                    input.MineralComplain,
                                                    input.LandDenounce,
                                                    input.EnviromentDenounce,
                                                    input.WaterDenounce,
                                                    input.MineralDenounce,
                                                    input.Keyword,
                                                    input.KetQua,
                                                    input.maTinhTP,
                                                    input.maQuanHuyen,
                                                    input.maXaPhuongTT,
                                                    input.FromDate,
                                                    input.ToDate);
        var totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting)
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<SummaryDto>(
            totalCount,
            ObjectMapper.Map<List<Summary>, List<SummaryDto>>(result)
       );
    }
}
