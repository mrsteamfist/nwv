using NativeWebView.HTML.DOM.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeWebView.HTML.Base
{
    /// <summary>
    /// Interface for HTML objects which can contain children
    /// </summary>
    public interface IHtmlContainer : IHtmlElement, INotifyPropertyChanged
    {
        /// <summary>
        /// Adds a child element to this element
        /// </summary>
        /// <param name="element"><Element to be added/param>
        /// <returns>If that element can be added</returns>
        bool AddElement(DisplayElement element);
    }
}
