using OrchardCore.Liquid;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Core.Services;

/// <summary>
/// Extension point for adding more classes to the body element.
/// </summary>
public interface IBodyClassProvider
{
    /// <summary>
    /// Adds some CSS classes to the body element.
    /// </summary>
    Task<IEnumerable<string>> GetClassesAsync(LiquidTemplateContext context);
}

public abstract class SyncBodyClassProvider : IBodyClassProvider
{
    public Task<IEnumerable<string>> GetClassesAsync(LiquidTemplateContext context) =>
        Task.FromResult(GetClasses(context));

    protected abstract IEnumerable<string> GetClasses(LiquidTemplateContext context);
}
