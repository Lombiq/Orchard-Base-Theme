using Lombiq.HelpfulLibraries.OrchardCore.Data;
using OrchardCore.Recipes.Services;

namespace Lombiq.BaseTheme.Samples.Migrations;

// Migrations based on the RecipeMigrationsBase class have a default CreateAsync method that invokes the recipe in the
// same directory called "{module-or-theme-id}.UpdateFrom0.recipe.json". For any subsequent update migrations, you can
// create an UpdateFromNAsync method as usual, but all you have to put in it is ExecuteAsync(N) to invoke the
// corresponding "{module-or-theme-id}.UpdateFromN.recipe.json" recipe and return the incremented version number.
public class RecipeMigrations : RecipeMigrationsBase
{
    public RecipeMigrations(IRecipeMigrator recipeMigrator)
        : base(recipeMigrator)
    {
    }
}
