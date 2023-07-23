using Volo.Abp.Settings;

namespace KNTC.Settings;

public class KNTCSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(KNTCSettings.MySetting1));
    }
}
