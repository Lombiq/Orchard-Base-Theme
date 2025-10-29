# Lombiq Base Theme for Orchard Core

[![Lombiq.BaseTheme.Native NuGet](https://img.shields.io/nuget/v/Lombiq.BaseTheme.Native?label=Lombiq.BaseTheme.Native)](https://www.nuget.org/packages/Lombiq.BaseTheme.Native/)
[![Lombiq.BaseTheme.Native.Samples NuGet](https://img.shields.io/nuget/v/Lombiq.BaseTheme.Native.Samples?label=Lombiq.BaseTheme.Native.Samples)](https://www.nuget.org/packages/Lombiq.BaseTheme.Native.Samples/)
[![Lombiq.BaseTheme.Core NuGet](https://img.shields.io/nuget/v/Lombiq.BaseTheme.Core?label=Lombiq.BaseTheme.Core)](https://www.nuget.org/packages/Lombiq.BaseTheme.Core/)
[![Lombiq.BaseTheme.Tests.UI NuGet](https://img.shields.io/nuget/v/Lombiq.BaseTheme.Tests.UI?label=Lombiq.BaseTheme.Tests.UI)](https://www.nuget.org/packages/Lombiq.BaseTheme.Tests.UI/)

## About

Orchard Core theme that contains a layout, zones and Bootstrap CSS base for your stylesheets. Set it as your theme's BaseTheme.

For general details about and usage instructions see the [root Readme](../Readme.md).

Do you want to quickly try out this project and see it in action? Check it out in our [Open-Source Orchard Core Extensions](https://github.com/Lombiq/Open-Source-Orchard-Core-Extensions) full Orchard Core solution and also see our other useful Orchard Core-related open-source projects!

## Features

### Breakpoint classes

This project includes JavaScript code that observes the document's width and applies several indicator classes to the `<body>` element as needed. At any point it will have one of these classes, using [Bootstrap's breakpoint sizes and identifiers](https://getbootstrap.com/docs/5.0/layout/breakpoints/):

- breakpoint-xs
- breakpoint-sm
- breakpoint-md
- breakpoint-lg
- breakpoint-xl
- breakpoint-xxl

Additionally, range classes are added too. For example `breakpoint-xs-sm` and `breakpoint-md-xxl`. These are helpful if you only want to differentiate between phone and desktop styles. For convenience, these two specific ranges are also available as `breakpoint-small` and `breakpoint-big` to improve readability and reduce the risk of accidental typos.

### Gutters and Margins

The `Lombiq.BaseTheme.Core` project defines some breakpoint-based variables for margins and gutters on the page. You should use that for layout spacing whenever possible, for example `--gutter-x-small` or `--page-margin-large`. To make it easier, this project also defines the `--gutter` and `--page-margin` variables that fit . These are defined inside the matching breakpoint class on `<body>` (e.g. `.breakpoint-sm`) instead of the `:root` pseudo-class, but that won't matter as long as you are trying to style something inside the `<body>`. This way, instead of

```css
.my-class {
    .breakpoint-xs & { margin-bottom: var(--gutter-x-small); }
    .breakpoint-sm & { margin-bottom: var(--gutter-small); }
    .breakpoint-md & { margin-bottom: var(--gutter-medium); }
    .breakpoint-lg & { margin-bottom: var(--gutter-large); }
    .breakpoint-xl & { margin-bottom: var(--gutter-x-large); }
    .breakpoint-xxl & { margin-bottom: var(--gutter-xx-large); }
}
```

you can simply write

```css
.my-class {
    margin-bottom: var(--gutter);
}
```
