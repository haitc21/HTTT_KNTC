using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Localization;
using Volo.Abp.OpenIddict.Applications;

namespace KNTC.Pages;

public class IndexModel : AbpPageModel
{
    public List<OpenIddictApplication> Applications { get; protected set; }

    public IReadOnlyList<LanguageInfo> Languages { get; protected set; }

    public string CurrentLanguage { get; protected set; }

    protected IOpenIddictApplicationRepository OpenIdApplicationRepository { get; }

    protected ILanguageProvider LanguageProvider { get; }

    public IndexModel(IOpenIddictApplicationRepository openIdApplicationmRepository, ILanguageProvider languageProvider)
    {
        OpenIdApplicationRepository = openIdApplicationmRepository;
        LanguageProvider = languageProvider;
    }

    public async Task OnGetAsync()
    {
        Applications = await OpenIdApplicationRepository.GetListAsync();
        CultureInfo.CurrentCulture = new CultureInfo("vi");
        Languages = await LanguageProvider.GetLanguagesAsync();
        CurrentLanguage = CultureInfo.CurrentCulture.DisplayName;
    }
}