using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lombiq.BaseTheme.Core.Services;

public class LayerAwareResourceManager : IResourceManager
{
    private readonly IStyleToLayerMappingAccessor _accessor;
    private readonly IResourceManager _manager;
    private readonly ResourceManagementOptions _options;

    public LayerAwareResourceManager(
        IStyleToLayerMappingAccessor accessor,
        IResourceManager manager,
        IOptions<ResourceManagementOptions> options)
    {
        _accessor = accessor;
        _manager = manager;
        _options = options.Value;
    }

    public void RenderStylesheet(TextWriter writer)
    {
        var stringBuilder = new StringBuilder();
        using var stringWriter = new StringWriter(stringBuilder);

        var layerResources = new List<(TagBuilder ResourceTag, string Layer)>();
        var styleSheets = _manager
            .GetRequiredResources("stylesheet")
            .Where(context => context.Settings.Location != ResourceLocation.Inline)
            .ToList();

        foreach (var context in styleSheets)
        {
            var resourceTag = context.Resource.GetTagBuilder(
                context.Settings,
                _options.ContentBasePath,
                context.FileVersionProvider);

            if (_accessor.Mapping.TryGetValue(context.Resource.Name, out var layer) &&
                resourceTag.Attributes.ContainsKey("href"))
            {
                layerResources.Add((resourceTag, layer));
                continue;
            }

            stringWriter.Write('\n');
            context.WriteTo(stringWriter, _options.ContentBasePath);
        }

        var registeredStyles = _manager.GetRegisteredStyles().AsList();
        foreach (var content in registeredStyles)
        {
            stringWriter.Write('\n');
            content.WriteTo(stringWriter, NullHtmlEncoder.Default);
        }

        var style = new TagBuilder("style");
        foreach (var (resourceTag, layer) in layerResources)
        {
            var url = resourceTag.Attributes["href"];
            style.InnerHtml.AppendHtml($"@import '{url}' layer({layer});\n");
        }

        style.WriteTo(stringWriter, NullHtmlEncoder.Default);

        writer.Write(stringBuilder.ToString().TrimStart());
    }

    #region Undecorated Members

    public ResourceManifest InlineManifest => _manager.InlineManifest;

    public ResourceDefinition FindResource(RequireSettings settings) => _manager.FindResource(settings);
    public void NotRequired(string resourceType, string resourceName) => _manager.NotRequired(resourceType, resourceName);
    public RequireSettings RegisterUrl(string resourceType, string resourcePath, string resourceDebugPath) =>
        _manager.RegisterUrl(resourceType, resourcePath, resourceDebugPath);
    public RequireSettings RegisterResource(string resourceType, string resourceName) =>
        _manager.RegisterResource(resourceType, resourceName);
    public void RegisterHeadScript(IHtmlContent script) => _manager.RegisterHeadScript(script);
    public void RegisterFootScript(IHtmlContent script) => _manager.RegisterFootScript(script);
    public void RegisterStyle(IHtmlContent style) => _manager.RegisterStyle(style);
    public void RegisterLink(LinkEntry link) => _manager.RegisterLink(link);
    public void RegisterMeta(MetaEntry meta) => _manager.RegisterMeta(meta);
    public void AppendMeta(MetaEntry meta, string contentSeparator) => _manager.AppendMeta(meta, contentSeparator);
    public IEnumerable<ResourceRequiredContext> GetRequiredResources(string resourceType) =>
        _manager.GetRequiredResources(resourceType);
    public IEnumerable<LinkEntry> GetRegisteredLinks() => _manager.GetRegisteredLinks();
    public IEnumerable<MetaEntry> GetRegisteredMetas() => _manager.GetRegisteredMetas();
    public IEnumerable<IHtmlContent> GetRegisteredHeadScripts() => _manager.GetRegisteredHeadScripts();
    public IEnumerable<IHtmlContent> GetRegisteredFootScripts() => _manager.GetRegisteredFootScripts();
    public IEnumerable<IHtmlContent> GetRegisteredStyles() => _manager.GetRegisteredStyles();
    public void RenderMeta(TextWriter writer) => _manager.RenderMeta(writer);
    public void RenderHeadLink(TextWriter writer) => _manager.RenderHeadLink(writer);
    public void RenderHeadScript(TextWriter writer) => _manager.RenderHeadScript(writer);
    public void RenderFootScript(TextWriter writer) => _manager.RenderFootScript(writer);
    public void RenderLocalScript(RequireSettings settings, TextWriter writer) => _manager.RenderLocalScript(settings, writer);
    public void RenderLocalStyle(RequireSettings settings, TextWriter writer) => _manager.RenderLocalStyle(settings, writer);

    #endregion
}
