using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text.Json;

namespace ULNavigation
{
    /// <summary>
    /// Represents a navigation URL with a title and URL.
    /// </summary>
    public class NavigationURL
    {
        /// <summary>
        /// Gets or sets the title of the navigation URL.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the URL of the navigation URL.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Initializes a new instance of the NavigationURL class.
        /// </summary>
        public NavigationURL() { }

        /// <summary>
        /// Initializes a new instance of the NavigationURL class with the specified title and URL.
        /// </summary>
        /// <param name="Title">The title of the navigation URL.</param>
        /// <param name="URL">The URL of the navigation URL.</param>
        public NavigationURL(string Title, string URL)
        {
            this.Title = Title;
            this.URL = URL;
        }

        /// <summary>
        /// Returns a string that represents the current NavigationURL object as an HTML anchor tag.
        /// </summary>
        /// <returns>A string that represents the current NavigationURL object as an HTML anchor tag.</returns>
        public override string ToString()
        {
            return $"<a href='{URL}'>{Title}</a>";
        }
    }
    
    /// <summary>
    /// Represents a printer for a collection of navigation URLs.
    /// Implements the IEnumerable interface to allow iteration through its items.
    /// </summary>
    public class NavigationPrinter : IEnumerable
    {
        /// <summary>
        /// A private list of NavigationURL objects.
        /// </summary>
        private List<NavigationURL> _NavigationURLs;

        /// <summary>
        /// Initializes a new instance of the NavigationPrinter class.
        /// </summary>
        public NavigationPrinter()
        {
            _NavigationURLs = new List<NavigationURL>();
        }

        /// <summary>
        /// Adds a NavigationURL object to the collection.
        /// </summary>
        /// <param name="url">The NavigationURL object to add.</param>
        public void Add(NavigationURL url) => _NavigationURLs.Add(url);

        /// <summary>
        /// Adds a new NavigationURL object to the collection with the specified title and URL.
        /// </summary>
        /// <param name="Title">The title of the navigation URL.</param>
        /// <param name="URL">The URL of the navigation URL.</param>
        public void Add(string Title, string URL) => Add(new NavigationURL(Title, URL));

        /// <summary>
        /// Returns an enumerator that iterates through the collection of NavigationURL objects.
        /// </summary>
        /// <returns>An IEnumerator for the NavigationURL objects.</returns>
        public IEnumerator GetEnumerator() => _NavigationURLs.GetEnumerator();

        /// <summary>
        /// Prints the navigation URLs as an unordered HTML list.
        /// </summary>
        public void PrintNavigation()
        {
            HttpContext.Current.Response.Write("<ul>");
            foreach (NavigationURL url in _NavigationURLs)
            {
                HttpContext.Current.Response.Write("<li>" + url.ToString() + "</li>");
            }
            HttpContext.Current.Response.Write("</ul>");
        }
    }

    /// <summary>
    /// Provides functionality to load a NavigationPrinter object from a JSON file.
    /// </summary>
    public static class NavPrinterLoader
    {
        /// <summary>
        /// Reads a JSON file and returns a NavigationPrinter object containing the deserialized NavigationURLs.
        /// </summary>
        /// <param name="FilePath">The path to the JSON file containing the NavigationURLs.</param>
        /// <returns>A NavigationPrinter object populated with NavigationURLs from the JSON file.</returns>
        public static NavigationPrinter Read(string FilePath)
        {
            var jsonString = File.ReadAllText(FilePath);
            var navURLs = JsonSerializer.Deserialize<List<NavigationURL>>(jsonString);
            NavigationPrinter printer = new NavigationPrinter();
            foreach (var url in navURLs)
            {
                printer.Add(url);
            }
            return printer;
        }
    }
}
