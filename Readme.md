# Lombiq Base Theme for Orchard Core

[![Lombiq.BaseTheme NuGet](https://img.shields.io/nuget/v/Lombiq.BaseTheme?label=Lombiq.BaseTheme)](https://www.nuget.org/packages/Lombiq.BaseTheme/) [![Lombiq.BaseTheme.Samples NuGet](https://img.shields.io/nuget/v/Lombiq.BaseTheme?label=Lombiq.BaseTheme.Samples)](https://www.nuget.org/packages/Lombiq.BaseTheme.Samples/) [![Lombiq.BaseTheme.Tests.UI NuGet](https://img.shields.io/nuget/v/Lombiq.BaseTheme?label=Lombiq.BaseTheme.Tests.UI)](https://www.nuget.org/packages/Lombiq.BaseTheme.Tests.UI/)

## About

A common base theme for our Orchard Core themes using Bootstrap v5.2.3. It can contain any shared content that are not specific to a specific project's theme.

You can find a sample module with a commented walkthrough in this repository. Check it out [here](Lombiq.BaseTheme.Samples/Readme.md)!

We at [Lombiq](https://lombiq.com/) also used this theme for the following projects:

- The new [Lombiq website](https://lombiq.com/) when migrating it from Orchard 1 to Orchard Core ([see case study](https://lombiq.com/blog/how-we-renewed-and-migrated-lombiq-com-from-orchard-1-to-orchard-core)).
- The new [Show Orchard website](https://showorchard.com/) when migrating it from Orchard 1 DotNest to DotNest Core ([see case study](https://dotnest.com/blog/show-orchard-case-study-migrating-an-orchard-1-dotnest-site-to-orchard-core)).<!-- #spell-check-ignore-line -->
- The new [Git-hg Mirror website](https://githgmirror.com/) when migrating it from Orchard 1 to Orchard Core ([see case study](https://lombiq.com/blog/git-hg-mirror-is-running-on-orchard-core)).<!-- #spell-check-ignore-line -->
- The new [Hastlayer website](https://hastlayer.com/) when migrating it from Orchard 1 to Orchard Core ([see case study](https://lombiq.com/blog/modernization-and-orchard-core-migration-of-hastlayer-com)).<!-- #spell-check-ignore-line -->

This theme is also available on all sites of [DotNest, the Orchard Core SaaS](https://dotnest.com/).

Do you want to quickly try out this project and see it in action? Check it out in our [Open-Source Orchard Core Extensions](https://github.com/Lombiq/Open-Source-Orchard-Core-Extensions) full Orchard Core solution and also see our other useful Orchard Core-related open-source projects!

## Demo video

[![Watch the video](Docs/Assets/Images/DemoVideoThumbnail.jpg)](https://www.youtube.com/watch?v=9DjKxEumoRE&feature=youtu.be)

## Documentation

Use this as the base theme of any custom frontend themes you create. For instructions on how to import and override this theme with your own theme's Sass stylesheets, see the header comments in [site.scss](Lombiq.BaseTheme/Assets/Styles/site.scss) and [_native-variables.scss](Lombiq.BaseTheme/Assets/Styles/abstracts/_native-variables.scss).

The theme makes use of the [`ICssClassHolder`](Lombiq.BaseTheme/Services/ICssClassHolder.cs) service which provides a scoped container for adding class names from your own code. Use the provided zone names in the [`ZoneNames`](Lombiq.BaseTheme/Constants/ZoneNames.cs) static class to address it.

You may have noticed, that we mentioned Bootstrap v5.2.3, even though your version of Orchard Core may be still using Bootstrap 5.0. This theme automatically removes the built-in Bootstrap resource manifests on the current tenant and replaces them with the vendor's JavaScript file pulled from NPM. As the Bootstrap stylesheet is already bundled into the site stylesheet there is no need to include that in the resource manifest. If you want to switch over to a different theme that doesn't use this as its base, please reload your tenant by going to Admin → Configuration → Tenants and clicking on the current tenant's Reload button.

Besides the style and layout, the theme also automatically includes a minimalist helper script that eases transition away from jQuery. You don't really need full jQuery now that Internet Explorer is effectively dead (Internet Explorer 11 is going end of life on June 15, 2022 so you should not support it in any new project at this time). The script gives you the `window.helper` object. You can use `helper.ready(($) => {})` in your scripts, where `$(querySelector, baseElement)` returns a JavaScript `Array` of `Element`s.

## Recipes

- Lombiq Orchard Core Base Theme - Layers and Zones: Sets up all the supported zones and some common layers. Automatically executed when you first enable the theme.
- Lombiq Orchard Core Base Theme - Styling Demo: Creates a Demo Page that can be helpful to see how your theme's stylesheet behaves on various HTML elements.

## Contributing and support

Bug reports, feature requests, comments, questions, code contributions and love letters are warmly welcome. You can send them to us via GitHub issues and pull requests. Please adhere to our [open-source guidelines](https://lombiq.com/open-source-guidelines) while doing so.

This project is developed by [Lombiq Technologies](https://lombiq.com/). Commercial-grade support is available through Lombiq.
