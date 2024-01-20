using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Builders;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using System.Threading.Tasks;
using static Lombiq.BaseTheme.Constants.ContentTypes;

namespace Lombiq.BaseTheme.Migrations;

public class LayoutInjectionMigrations(IContentDefinitionManager contentDefinitionManager) : DataMigration
{
    public async Task<int> CreateAsync()
    {
        await contentDefinitionManager.AlterTypeDefinitionAsync(LayoutInjection, builder => builder
            .SetAbilities(creatable: true)
            .Stereotype("Widget"));

        return 1;
    }
}
