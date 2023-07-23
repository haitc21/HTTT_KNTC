using KNTC.Localization;
using Volo.Abp.Application.Services;

namespace KNTC;

/* Inherit your application services from this class.
 */

public abstract class KNTCAppService : ApplicationService
{
    protected KNTCAppService()
    {
        LocalizationResource = typeof(KNTCResource);
    }
}