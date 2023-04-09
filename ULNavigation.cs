using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text.Json;

namespace ULNavigation
{
    public class NavigationURL
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public NavigationURL() { }
        public NavigationURL(string Title, string URL)
        {
            this.Title = Title;
            this.URL = URL;
        }
        public override string ToString()
        {
            return $"<a href='{URL}'>{Title}</a>";
        }
    }
    public class NavigationPrinter : IEnumerable
    {
        private List<NavigationURL> _NavigationURLs;
        public NavigationPrinter()
        {
            _NavigationURLs = new List<NavigationURL>();
        }
        public void Add(NavigationURL url) => _NavigationURLs.Add(url);
        public void Add(string Title, string URL) => Add(new NavigationURL(Title, URL));
        public IEnumerator GetEnumerator() => _NavigationURLs.GetEnumerator();
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
    public static class NavPrinterLoader
    {
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
