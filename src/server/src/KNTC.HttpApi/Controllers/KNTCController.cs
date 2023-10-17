using KNTC.Localization;
using Volo.Abp.AspNetCore.Mvc;

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