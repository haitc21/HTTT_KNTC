using Volo.Abp.AspNetCore.Mvc;
using KNTC.Localization;

namespace KNTC.Controllers;

/* Inherit your controllers from this class.
 */

public abstract class KNTCController : AbpControllerBase
{
    protected KNTCController()
    {
        LocalizationResource = typeof(KNTCResource);
    }
}