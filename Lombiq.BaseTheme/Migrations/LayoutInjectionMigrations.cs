using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Builders;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using System.Threading.Tasks;
using static Lombiq.BaseTheme.Constants.ContentTypes;

namespace Lombiq.BaseTheme.Migrations;

public class LayoutInjectionMigrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public LayoutInjectionMigrations(IContentDefinitionManager contentDefinitionManager) =>
        _contentDefinitionManager = contentDefinitionManager;

    public async Task<int> CreateAsync()
    {
        _contentDefinitionManager.AlterTypeDefinition(LayoutInjection, builder => builder
            .SetAbilities(creatable: true)
            .Stereotype("Widget"));

        return 1;
    }
}
