Documentation
-------------

### Overview

This code provides a simple C# library for managing and rendering a list of navigation URLs on a webpage. The library consists of three main classes:

1.  `NavigationURL`
2.  `NavigationPrinter`
3.  `NavPrinterLoader`

The code uses `System.Collections`, `System.Collections.Generic`, `System.Web`, `System.IO`, and `System.Text.Json` namespaces to handle necessary functionality.

### NavigationURL Class

The `NavigationURL` class is used to represent a single navigation link. It has the following properties:

*   `Title` (string): The text to be displayed for the navigation link.
*   `URL` (string): The URL that the navigation link points to.

The class also has the following constructors:

*   `NavigationURL()`: Initializes a new instance of the `NavigationURL` class with default property values.
*   `NavigationURL(string Title, string URL)`: Initializes a new instance of the `NavigationURL` class with the specified `Title` and `URL` values.

There is an overridden `ToString()` method that returns an HTML anchor (`<a>`) element with the `URL` as the `href` attribute and the `Title` as the text.

### NavigationPrinter Class

The `NavigationPrinter` class is responsible for managing a collection of `NavigationURL` objects and rendering them as an unordered list (`<ul>`). The class implements `IEnumerable` and has the following methods:

*   `Add(NavigationURL url)`: Adds a `NavigationURL` object to the internal list of navigation URLs.
*   `Add(string Title, string URL)`: Creates a new `NavigationURL` object with the specified `Title` and `URL`, and adds it to the internal list of navigation URLs.
*   `GetEnumerator()`: Returns an enumerator for iterating over the internal list of navigation URLs.
*   `PrintNavigation()`: Renders the navigation list as an unordered list (`<ul>`) with list items (`<li>`) containing the HTML anchor elements.

### NavPrinterLoader Class

The `NavPrinterLoader` class is a utility class for loading `NavigationURL` objects from a JSON file. It has a single static method:

*   `Read(string FilePath)`: Reads a JSON file at the specified `FilePath`, deserializes it into a list of `NavigationURL` objects, and initializes a new `NavigationPrinter` object with the deserialized list.

### Usage

1.  Create an instance of the `NavigationPrinter` class.
2.  Add navigation URLs using the `Add` methods.
3.  Call the `PrintNavigation()` method to render the navigation list on a webpage.
4.  (Optional) Load navigation URLs from a JSON file using the `NavPrinterLoader.Read` method.

Example:

```csharp
NavigationPrinter navPrinter = new NavigationPrinter();
navPrinter.Add("Home", "/");
navPrinter.Add("About", "/about");
navPrinter.PrintNavigation();

// Load navigation URLs from a JSON file
string filePath = "~/navUrls.json";
NavigationPrinter navPrinterFromFile = NavPrinterLoader.Read(filePath);
navPrinterFromFile.PrintNavigation();
```
Here's an example JSON file with a list of navigation URLs:

```json
[
  {
    "Title": "Home",
    "URL": "/"
  },
  {
    "Title": "About",
    "URL": "/about"
  },
  {
    "Title": "Services",
    "URL": "/services"
  },
  {
    "Title": "Contact",
    "URL": "/contact"
  }
]
```

This JSON file represents an array of objects, each with a `Title` and `URL` property. You can use this file with the `NavPrinterLoader.Read` method to load the navigation URLs into a `NavigationPrinter` object.
