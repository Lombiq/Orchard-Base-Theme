using System;

namespace Lombiq.BaseTheme.Core.Models;

public enum BootstrapNotifyType
{
    // Matching values from OrchardCore.DisplayManagement.Notify.NotifyType. If new values are ever added, this should
    // be updated as well.
    Success,
    Information,
    Warning,
    Error,

    // Values of additional Bootstrap alert types. Spaced apart from the above types for future-proofing.
    Primary = 100,
    Secondary,
    Danger,
    Info,
    Light,
    Dark,
}

public static class BootstrapNotifyTypeExtensions
{
    public static string GetMessageClass(this BootstrapNotifyType type) =>
        "message-" + type.ToString().ToSnakeCase();

    public static string GetAlertClass(this BootstrapNotifyType type) =>
        type switch
        {
            BootstrapNotifyType.Information => "alert-info",
            BootstrapNotifyType.Error => "alert-danger",
            _ => "alert-" + type.ToString().ToSnakeCase(),
        };
}
