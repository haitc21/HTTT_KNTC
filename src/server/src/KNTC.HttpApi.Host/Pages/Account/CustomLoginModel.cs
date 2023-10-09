using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Volo.Abp.Account.Web.Pages.Account;
using Microsoft.Extensions.Options;

namespace KNTC.Pages.Account;

public class CustomLoginModel : LoginModel
{
    public CustomLoginModel(
    IAuthenticationSchemeProvider schemeProvider,
    IOptions<Volo.Abp.Account.Web.AbpAccountOptions> accountOptions,
    IOptions<IdentityOptions> identityOptions)
        : base(schemeProvider, accountOptions, identityOptions)
    {
    }
}

