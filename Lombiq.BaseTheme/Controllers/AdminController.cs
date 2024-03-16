using Lombiq.BaseTheme.Models;
using Lombiq.BaseTheme.ViewModels;
using Lombiq.HelpfulExtensions.Extensions.ContentTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Entities;
using OrchardCore.Media.Fields;
using OrchardCore.Media.Settings;
using OrchardCore.Media.ViewModels;
using OrchardCore.Modules;
using OrchardCore.Settings;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Controllers;

// This controller is there for editing the BaseThemeSettings. We can't use a site settings driver for this, because you
// can't declare admin-accessible shapes in a site theme.
public class AdminController : Controller
{
    private readonly IClock _clock;
    private readonly INotifier _notifier;
    private readonly ISiteService _siteService;
    private readonly IShapeFactory _shapeFactory;
    private readonly IHtmlLocalizer<AdminController> H;

    public AdminController(
        IClock clock,
        INotifier notifier,
        ISiteService siteService,
        IShapeFactory shapeFactory,
        IHtmlLocalizer<AdminController> htmlLocalizer)
    {
        _clock = clock;
        _notifier = notifier;
        _siteService = siteService;
        _shapeFactory = shapeFactory;

        H = htmlLocalizer;
    }

    public async Task<IActionResult> Index()
    {
        var section = (await _siteService.LoadSiteSettingsAsync()).As<BaseThemeSettings>();

        var model = new BaseThemeSettingsViewModel
        {
            HideMenu = section.HideMenu,
            Icon = section.Icon,
            Editor = await _shapeFactory.CreateAsync<EditMediaFieldViewModel>("MediaField_Edit", editor =>
            {
                var part = CreatePart(section);

                editor.Paths = string.IsNullOrWhiteSpace(section.Icon)
                    ? "[]"
                    : JsonSerializer.Serialize(new[] { new { path = section.Icon } });
                editor.Field = part.Icon;
                editor.Part = part;
                editor.PartFieldDefinition = new ContentPartFieldDefinition(
                    new ContentFieldDefinition(nameof(BaseThemeSettingsPart.Icon)),
                    nameof(BaseThemeSettingsPart.Icon),
                    (JsonObject)JsonSerializer.SerializeToNode(new Dictionary<string, object>
                    {
                        [nameof(MediaFieldSettings)] = new MediaFieldSettings { Multiple = false },
                    }))
                {
                    PartDefinition = new ContentPartDefinition(nameof(BaseThemeSettingsPart)),
                };
            }),
        };

        model.Editor.Metadata.OnDisplaying(context => context.DisplayContext.HtmlFieldPrefix = nameof(model.Editor));

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update([FromForm] BaseThemeSettingsViewModel viewModel)
    {
        var siteSettings = await _siteService.LoadSiteSettingsAsync();
        siteSettings.Alter<BaseThemeSettings>(nameof(BaseThemeSettings), settings =>
        {
            settings.TimeStamp = _clock.UtcNow.Ticks;
            settings.Icon = viewModel.Icon;
            settings.HideMenu = viewModel.HideMenu;
        });

        await _siteService.UpdateSiteSettingsAsync(siteSettings);
        await _notifier.SuccessAsync(H["Site settings updated successfully."]);

        return RedirectToAction(nameof(Index));
    }

    private static BaseThemeSettingsPart CreatePart(BaseThemeSettings section)
    {
        var content = new ContentItem { ContentType = ContentTypes.Empty };

        content.Weld(new BaseThemeSettingsPart
        {
            ContentItem = content,
            Icon = new MediaField
            {
                ContentItem = content,
                MediaTexts = [section.Icon],
                Paths = [section.Icon],
            },
        });

        return content.As<BaseThemeSettingsPart>();
    }

    public class BaseThemeSettingsPart : ContentPart
    {
        public MediaField Icon { get; set; } = new();
    }
}
