# Lombiq Orchard Core Base Theme



## About

A common base theme for our Orchard Core themes using Bootstrap 5.1. It can contain any shared content that are not specific to a specific project's theme.


## Documentation

Use this as the base theme of any custom frontend themes you create. For instructions on how to import and override this theme with your own theme's Sass stylesheets, see the header comments in [site.scss](Assets/Styles/site.scss) and [_native-variables.scss](Assets/Styles/abstracts/_native-variables.scss).

The theme makes use of the [ICssClassHolder](Services/ICssClassHolder.cs) service which provides a scoped container for adding class names from your own code. Use the provided zone names in the [ZoneNames](Constants/ZoneNames.cs) static class to address it.

You may have noticed, that we mentioned Bootstrap 5.1, even though your version of Orchard Core may be still using Bootstrap 5.0. This theme automatically removes the built-in Bootstrap resource manifests and replaces them with the vendor's JavaScript file pulled from NPM. As the Bootstrap stylesheet is already bundled into the site stylesheet there is no need to include that in the resource manifest. 

Besides the style and layout, the theme also automatically includes a minimalist helper script that eases transition away from jQuery. You don't really need full jQuery now that Internet Explorer is effectively dead (Internet Explorer 11 is going end of life on June 15, 2022 so you should not support it in any new project at this time). The script gives you the `window.helper` object. You can use `helper.ready(($) => {})` in your scripts, where `$(querySelector, baseElement)` returns a JavaScript `Array` of `Element`s.

## Recipes

- Lombiq Orchard Core Base Theme - Layers and Zones: Call this if you plan on using widgets. It sets up all the supported zones and some common layers.
- Lombiq Orchard Core Base Theme - Styling Demo: Creates a Demo Page that can be helpful to see how your theme's stylesheet behaves on various HTML elements.


## Contributing and support

Bug reports, feature requests, comments, questions, code contributions, and love letters are warmly welcome, please do so via GitHub issues and pull requests. Please adhere to our [open-source guidelines](https://lombiq.com/open-source-guidelines) while doing so.

This project is developed by [Lombiq Technologies](https://lombiq.com/). Commercial-grade support is available through Lombiq.
