using Volo.Abp.Application.Dtos;

namespace KNTC;

public class BaseListFilterDto : PagedResultRequestDto
{
    public string Keyword { get; set; }
}