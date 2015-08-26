using System.ComponentModel;

namespace NativeWebView.HTML.Base
{
    /// <summary>
    /// HTML element which can be added to an html document
    /// </summary>
    public interface IHtmlElement : INotifyPropertyChanged
    {
        /// <summary>
        /// The code to be added to the HTML document as a separte element
        /// </summary>
        string HtmlCode { get; }
    }
}
