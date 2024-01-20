using Lombiq.HelpfulLibraries.OrchardCore.Data;
using OrchardCore.Recipes.Services;

namespace Lombiq.BaseTheme.Samples.Migrations;

// Migrations based on the RecipeMigrationsBase class have a default CreateAsync method that invokes the recipe in the
// same directory called "{module-or-theme-id}.UpdateFrom0.recipe.json". For any subsequent update migrations, you can
// create an UpdateFrom1Async, UpdateFrom2Async, etc as usual, but all you have to put in it is ExecuteAsync(N) to
// invoke the corresponding "{module-or-theme-id}.UpdateFromN.recipe.json" recipe and return the incremented version
// number.
// If you just want a static default icon, check out the DerivedTheme.Favicon in Manifest.cs!
public class RecipeMigrations(IRecipeMigrator recipeMigrator) : RecipeMigrationsBase(recipeMigrator)
{
}

// NEXT STATION: Migrations/Lombiq.BaseTheme.Samples.UpdateFrom0.recipe.json
