# Lombiq Base Theme for Orchard Core - Samples

## About

Example Orchard Core theme that makes use of Lombiq Base Theme for Orchard Core.

For general details about and usage instructions see the [root Readme](../Readme.md).

Do you want to quickly try out this project and see it in action? Check it out in our [Open-Source Orchard Core Extensions](https://github.com/Lombiq/Open-Source-Orchard-Core-Extensions) full Orchard Core solution and also see our other useful Orchard Core-related open-source projects!

## Training sections

This module doesn't cover Orchard Core basics. Instead, sometimes we reference the [Lombiq Training Demo for Orchard Core](https://github.com/Lombiq/Orchard-Training-Demo-Module) which does. The [Open-Source Orchard Core Extensions](https://github.com/Lombiq/Open-Source-Orchard-Core-Extensions) repository includes both the Training Demo and this project.

> [!NOTE]
> This project uses native CSS and the compiled version of Bootstrap included by Orchard Core. Because it avoids using Sass/SCSS, the setup and compilation is more efficient, but some features are less flexible (e.g. can't set custom grid breakpoint sizes) and some Sass features are not yet possible in CSS (e.g. mixins and functions). If you require those capabilities, check out the sample for the [Sass-based version of our base theme](../Lombiq.BaseTheme.Samples/Readme.md)

The indented sections should be followed in sequence.

- Make a custom theme
  - [Configuration](Manifest.cs)
  - [Layout injection](Views/Widget-LayoutInjection.liquid)
  - [CSS styling and structure](wwwroot/css/site.css)
  - [Resource management](ResourceManagementOptionsConfiguration.cs)
