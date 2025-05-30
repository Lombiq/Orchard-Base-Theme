namespace Lombiq.BaseTheme.Core.Constants;

public static class ResourceNames
{
    private const string Prefix = FeatureIds.Area + ".";

    public const string General = Prefix + nameof(General);
    public const string Helpers = Prefix + nameof(Helpers);
    public const string NativeVariables = Prefix + nameof(NativeVariables);

    // This is the style and script resource registered by OrchardCore. See
    // https://docs.orchardcore.net/en/main/reference/modules/Resources/ for the complete list of stock resources.
    public const string Bootstrap = "bootstrap";
}
