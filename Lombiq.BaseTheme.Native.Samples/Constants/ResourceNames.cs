namespace Lombiq.BaseTheme.Native.Samples.Constants;

public static class ResourceNames
{
    private const string Prefix = $"{FeatureIds.Area}.";
    private const string AbstractPrefix = $"{Prefix}Abstract.";
    private const string GeneralPrefix = $"{Prefix}General.";
    private const string PagesPrefix = $"{Prefix}Pages.";

    public const string Helpers = AbstractPrefix + nameof(Helpers);
    public const string NativeVariables = AbstractPrefix + nameof(NativeVariables);
    public const string General = GeneralPrefix + nameof(General);
    public const string Navigation = GeneralPrefix + nameof(Navigation);
    public const string BlogPost = PagesPrefix + nameof(BlogPost);
    public const string Site = Prefix + nameof(Site);
}
