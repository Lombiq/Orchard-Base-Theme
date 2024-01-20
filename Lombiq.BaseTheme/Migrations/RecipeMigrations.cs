using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Migrations;

public class RecipeMigrations(IRecipeMigrator recipeMigrator) : DataMigration
{
    public async Task<int> CreateAsync()
    {
        await recipeMigrator.ExecuteAsync("Lombiq.BaseTheme.LayersAndZones.recipe.json", this);

        return 1;
    }
}
