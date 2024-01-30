using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Migrations;

public class RecipeMigrations : DataMigration
{
    private readonly IRecipeMigrator _recipeMigrator;

    public RecipeMigrations(IRecipeMigrator recipeMigrator) => _recipeMigrator = recipeMigrator;

    public async Task<int> CreateAsync()
    {
        await _recipeMigrator.ExecuteAsync("Lombiq.BaseTheme.LayersAndZones.recipe.json", this);

        return 1;
    }
}
