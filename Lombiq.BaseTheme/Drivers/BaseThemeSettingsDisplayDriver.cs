using Lombiq.BaseTheme.Models;
using Lombiq.BaseTheme.Permissions;
using Lombiq.BaseTheme.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Drivers;

public class BaseThemeSettingsDisplayDriver : SectionDisplayDriver<ISite, BaseThemeSettings>
{
    public const string EditorGroupId = "BaseTheme";

    private readonly IAuthorizationService _authorizationService;
    private readonly IHttpContextAccessor _hca;

    public BaseThemeSettingsDisplayDriver(IAuthorizationService authorizationService, IHttpContextAccessor hca)
    {
        _authorizationService = authorizationService;
        _hca = hca;
    }

    public override async Task<IDisplayResult> EditAsync(BaseThemeSettings section, BuildEditorContext context) =>
        Initialize<BaseThemeSettingsViewModel>($"{nameof(BaseThemeSettings)}_Edit", viewModel =>
            {
                viewModel.Thumbnail = section.Thumbnail;
            })
            .PlaceInContent()
            .OnGroup(EditorGroupId)
            .RenderWhen(AuthorizeAsync);

    public override async Task<IDisplayResult> UpdateAsync(BaseThemeSettings section, BuildEditorContext context)
    {
        if (await context.CreateModelMaybeAsync<BaseThemeSettingsViewModel>(Prefix, EditorGroupId, AuthorizeAsync) is { } viewModel)
        {
            section.Thumbnail = viewModel.Thumbnail;
        }

        return await EditAsync(section, context);
    }

    private Task<bool> AuthorizeAsync() =>
        _hca.HttpContext?.User is { } user
        ? _authorizationService.AuthorizeAsync(user, BaseThemeSettingsPermissions.ManageBaseThemeSettings)
        : Task.FromResult(false);
}
