# Lombiq Base Theme for Orchard Core - Samples

## About

Example Orchard Core theme that makes use of Lombiq Base Theme for Orchard Core.

For general details about and usage instructions see the [root Readme](../Readme.md).

Do you want to quickly try out this project and see it in action? Check it out in our [Open-Source Orchard Core Extensions](https://github.com/Lombiq/Open-Source-Orchard-Core-Extensions) full Orchard Core solution and also see our other useful Orchard Core-related open-source projects!

## Training sections

This module doesn't cover Orchard Core basics. Instead, sometimes we reference the [Lombiq Training Demo for Orchard Core](https://github.com/Lombiq/Orchard-Training-Demo-Module) which does. The [Open-Source Orchard Core Extensions](https://github.com/Lombiq/Open-Source-Orchard-Core-Extensions) repository includes both the Training Demo and this project.

If you need help with setting up SCSS compilation, read the documentation of [Lombiq Node.js Extensions](https://github.com/Lombiq/NodeJs-Extensions/#available-pipelines) first. Also check out the [package.json](package.json) file in this project.

You can start with any of the top-level sections, but the indented sections should be followed in sequence.

- Make a custom theme
  - [Configuration](Manifest.cs)
  - [Layout injection](Views/Widget-LayoutInjection.cshtml)
  - [Sass styling and structure](Assets/Styles/site.scss)
- [Front-end navigation via the `"main"` menu](Services/AccountNavigationProvider.cs)
- [Set up favicon using recipe migrations](Migrations/RecipeMigrations.cs)
