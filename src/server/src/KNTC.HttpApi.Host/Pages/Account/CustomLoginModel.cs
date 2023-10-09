using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Volo.Abp.Account.Web.Pages.Account;

namespace KNTC.Pages.Account;

public class CustomLoginModel : LoginModel
{
    public CustomLoginModel(
    Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider schemeProvider,
    Microsoft.Extensions.Options.IOptions<Volo.Abp.Account.Web.AbpAccountOptions> accountOptions,
    Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Identity.IdentityOptions> identityOptions)
        : base(schemeProvider, accountOptions, identityOptions)
    {
    }
}

