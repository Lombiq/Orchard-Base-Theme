# Lombiq Base Theme for Orchard Core

[![Lombiq.BaseTheme NuGet](https://img.shields.io/nuget/v/Lombiq.BaseTheme?label=Lombiq.BaseTheme)](https://www.nuget.org/packages/Lombiq.BaseTheme/) [![Lombiq.BaseTheme.Samples NuGet](https://img.shields.io/nuget/v/Lombiq.BaseTheme?label=Lombiq.BaseTheme.Samples)](https://www.nuget.org/packages/Lombiq.BaseTheme.Samples/) [![Lombiq.BaseTheme.Tests.UI NuGet](https://img.shields.io/nuget/v/Lombiq.BaseTheme?label=Lombiq.BaseTheme.Tests.UI)](https://www.nuget.org/packages/Lombiq.BaseTheme.Tests.UI/)

## About

A common base theme for our Orchard Core themes using Bootstrap 5.1. It can contain any shared content that are not specific to a specific project's theme.

You can find a sample module with a commented walkthrough in this repository. Check it out [here](Lombiq.BaseTheme.Samples/Readme.md)!

Do you want to quickly try out this project and see it in action? Check it out in our [Open-Source Orchard Core Extensions](https://github.com/Lombiq/Open-Source-Orchard-Core-Extensions) full Orchard Core solution and also see our other useful Orchard Core-related open-source projects!

## Demo video

[![Watch the video](Docs/Assets/Images/DemoVideoThumbnail.jpg)](https://www.youtube.com/watch?v=9DjKxEumoRE&feature=youtu.be)

## Documentation

Use this as the base theme of any custom frontend themes you create. For instructions on how to import and override this theme with your own theme's Sass stylesheets, see the header comments in [site.scss](Assets/Styles/site.scss) and [_native-variables.scss](Assets/Styles/abstracts/_native-variables.scss).

The theme makes use of the [ICssClassHolder](Services/ICssClassHolder.cs) service which provides a scoped container for adding class names from your own code. Use the provided zone names in the [ZoneNames](Constants/ZoneNames.cs) static class to address it.

You may have noticed, that we mentioned Bootstrap 5.1, even though your version of Orchard Core may be still using Bootstrap 5.0. This theme automatically removes the built-in Bootstrap resource manifests on the current tenant and replaces them with the vendor's JavaScript file pulled from NPM. As the Bootstrap stylesheet is already bundled into the site stylesheet there is no need to include that in the resource manifest. If you want to switch over to a different theme that doesn't use this as its base, please reload your tenant by going to Admin → Configuration → Tenants and clicking on the current tenant's Reload button.

Besides the style and layout, the theme also automatically includes a minimalist helper script that eases transition away from jQuery. You don't really need full jQuery now that Internet Explorer is effectively dead (Internet Explorer 11 is going end of life on June 15, 2022 so you should not support it in any new project at this time). The script gives you the `window.helper` object. You can use `helper.ready(($) => {})` in your scripts, where `$(querySelector, baseElement)` returns a JavaScript `Array` of `Element`s.

## Recipes

- Lombiq Orchard Core Base Theme - Layers and Zones: Call this if you plan on using widgets. It sets up all the supported zones and some common layers.
- Lombiq Orchard Core Base Theme - Styling Demo: Creates a Demo Page that can be helpful to see how your theme's stylesheet behaves on various HTML elements.

## Contributing and support

Bug reports, feature requests, comments, questions, code contributions and love letters are warmly welcome. You can send them to us via GitHub issues and pull requests. Please adhere to our [open-source guidelines](https://lombiq.com/open-source-guidelines) while doing so.

This project is developed by [Lombiq Technologies](https://lombiq.com/). Commercial-grade support is available through Lombiq.
