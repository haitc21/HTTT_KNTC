using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Volo.Abp.Account.Web.Pages.Account;

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