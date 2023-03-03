using Volo.Abp.Application.Dtos;

namespace KNTC;

public class BaseListFilterDto : PagedAndSortedResultRequestDto
{
    public string Keyword { get; set; }
}