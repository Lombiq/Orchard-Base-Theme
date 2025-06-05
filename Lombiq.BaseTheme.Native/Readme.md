# Lombiq Base Theme for Orchard Core - Native CSS

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
