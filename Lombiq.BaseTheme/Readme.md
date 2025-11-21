# Lombiq Base Theme for Orchard Core

[![Lombiq.BaseTheme NuGet](https://img.shields.io/nuget/v/Lombiq.BaseTheme?label=Lombiq.BaseTheme)](https://www.nuget.org/packages/Lombiq.BaseTheme/) [![Lombiq.BaseTheme.Samples NuGet](https://img.shields.io/nuget/v/Lombiq.BaseTheme?label=Lombiq.BaseTheme.Samples)](https://www.nuget.org/packages/Lombiq.BaseTheme.Samples/) [![Lombiq.BaseTheme.Tests.UI NuGet](https://img.shields.io/nuget/v/Lombiq.BaseTheme?label=Lombiq.BaseTheme.Tests.UI)](https://www.nuget.org/packages/Lombiq.BaseTheme.Tests.UI/)

> [!WARNING]
> The current latest version of `Lombiq.BaseTheme` is deprecated. It will keep working for the foreseeable future, but won't receive any updates besides security fixes. On why we moved away from Node.js-using builds and how, see [our "Step away from that Node.js" blog post](https://orcharddojo.net/blog/step-away-from-that-node-js).

## About

A common base theme for our Orchard Core themes using Bootstrap v5.3.6. It can contain any shared content that are not specific to a specific project's theme.

You can find a sample module with a commented walkthrough in this repository. Check it out [here](Lombiq.BaseTheme.Samples/Readme.md)!

We at [Lombiq](https://lombiq.com/) also used this theme for the following projects:

- The new [Lombiq website](https://lombiq.com/) when migrating it from Orchard 1 to Orchard Core ([see case study](https://lombiq.com/blog/how-we-renewed-and-migrated-lombiq-com-from-orchard-1-to-orchard-core)).
- The new [Show Orchard website](https://showorchard.com/) when migrating it from Orchard 1 DotNest to DotNest Core ([see case study](https://dotnest.com/blog/show-orchard-case-study-migrating-an-orchard-1-dotnest-site-to-orchard-core)).
- The new [Git-hg Mirror website](https://githgmirror.com/) when migrating it from Orchard 1 to Orchard Core ([see case study](https://lombiq.com/blog/git-hg-mirror-is-running-on-orchard-core)).
- The new [Hastlayer website](https://hastlayer.com/) when migrating it from Orchard 1 to Orchard Core ([see case study](https://lombiq.com/blog/modernization-and-orchard-core-migration-of-hastlayer-com)).
- The new [Orchard Dojo website](https://orcharddojo.net/) when migrating it from Orchard 1 to Orchard Core ([see case study](https://orcharddojo.net/blog/another-lombiq-site-was-improved-orchard-dojo)).

This theme is also available on all sites of [DotNest, the Orchard Core SaaS](https://dotnest.com/).

Do you want to quickly try out this project and see it in action? Check it out in our [Open-Source Orchard Core Extensions](https://github.com/Lombiq/Open-Source-Orchard-Core-Extensions) full Orchard Core solution and also see our other useful Orchard Core-related open-source projects!

## Demo video

[![Watch the video](Docs/Assets/Images/DemoVideoThumbnail.jpg)](https://www.youtube.com/watch?v=9DjKxEumoRE&feature=youtu.be)

## Documentation

Use this as the base theme of any custom frontend themes you create. There are two versions, `Lombiq.BaseTheme` that uses Sass as a styling pre-processor, and `Lombiq.BaseTheme.Native` that uses pure CSS. For information on how to use either in your custom theme, check out their respective sample projects ([`Lombiq.BaseTheme.Samples` for Sass](Lombiq.BaseTheme.Samples/Readme.md) or [`Lombiq.BaseTheme.Native.Samples` for CSS](Lombiq.BaseTheme.Native.Samples/Readme.md)).

Both themes make use of the [`ICssClassHolder`](Lombiq.BaseTheme/Services/ICssClassHolder.cs) service which provides a scoped container for adding class names from your own code. Use the provided zone names in the [`ZoneNames`](Lombiq.BaseTheme/Constants/ZoneNames.cs) static class to address it.

The version of Bootstrap used by Orchard Core is not necessarily the same as the one in this project. The Sass-based theme automatically removes the built-in Bootstrap resource manifests on the current tenant and replaces them with the vendor's JavaScript file pulled from NPM. As the Bootstrap stylesheet is already bundled into the site stylesheet there is no need to include that in the resource manifest. If you want to switch over to a different theme that doesn't use this as its base, please reload your tenant by going to Admin → Configuration → Tenants and clicking on the current tenant's Reload button.

Besides the style and layout, the Sass-based theme also automatically includes a minimalist helper script that eases transition away from jQuery, which hasn't really been necessary since Internet Explorer died back in 2022. The script gives you the `window.helper` object. You can use `helper.ready(($) => {})` in your scripts, where `$(querySelector, baseElement)` returns a JavaScript `Array` of `Element`s. We opted to not include it in the native CSS version of the theme, and should be considered a legacy feature.

If you use Sass, your styles will be automatically linted during compilation. If you use CSS, there is no linting by default. We suggest using our GitHub workflow to enable linting for unprocessed CSS and JS. You can learn more about either approach in the documentation of [Lombiq Node.js Extensions](https://github.com/Lombiq/NodeJs-Extensions/).

## Liquid

The following Liquid features are made available:

- `{% display-zones "default" %}`: The `display-zones` tag renders the default zone structure (see `ZoneDescriptor.GetDefaultZoneDescriptors()`) to the page. Useful if you want to override the `Layout` shape.
- `{{ "JSON object or array" | display-zones }}`: The same, but in filter form. You can specify the zone descriptor list or tree in a JSON serialized form, if you need different zones from the default. Use this if you have added custom zones in the admin settings!
- `{{ 'layoutAside, layoutAside_anotherClass' | zone-classes: zone: 'AsideSecond' }}`: The `zone-classes` filter adds the items of the input list to the indicated zone.
  - `{{ 'unwanted-class' | zone-classes: zone: 'AsideSecond', remove: true }}`: The above filter can also remove classes from a zone, if the `remove: true` argument is used.
- `{{ Theme.ZoneCss["zoneName"] }}`: The `Theme.ZoneCss` accessor returns the class list for the zone called "zoneName". We use it for the special "Body" pseudo-zone (like this: `class="{{ Theme.ZoneCss["Body"] | join: " " }}"`) to display any classes you may have added to it using the above `zone-classes` filter.

## Recipes

- Lombiq Orchard Core Base Theme - Layers and Zones: Sets up all the supported zones and some common layers. Automatically executed when you first enable the theme.
- Lombiq Orchard Core Base Theme - Styling Demo: Creates a Demo Page that can be helpful to see how your theme's stylesheet behaves on various HTML elements.

## Contributing and support

Bug reports, feature requests, comments, questions, code contributions and love letters are warmly welcome. You can send them to us via GitHub issues and pull requests. Please adhere to our [open-source guidelines](https://lombiq.com/open-source-guidelines) while doing so.

This project is developed by [Lombiq Technologies](https://lombiq.com/). Commercial-grade support is available through Lombiq.
