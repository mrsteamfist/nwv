using System;
using System.ComponentModel;

namespace NativeWebView.HTML.Base
{
    /// <summary>
    /// Base for html element which is used by the entire system.
    /// Holds some property changed events to simplify event notification
    /// Enforces requiring all elements be able to return some displayable HTML5 code.
    /// </summary>
    public abstract class HtmlElement : IHtmlElement
    {
        /// <summary>
        /// HTML code to be displayed to the system
        /// </summary>
        public abstract String HtmlCode { get; }
        /// <summary>
        /// Event for an HTML attribute or child has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Wrapper for PropertyChanged
        /// </summary>
        /// <param name="sender">Source of the event</param>
        /// <param name="args">Property changed</param>
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (PropertyChanged != null)
                PropertyChanged(sender, args);
        }
        /// <summary>
        /// Wrapper for property changed event
        /// where the sender is always the <see cref="this"/> element
        /// </summary>
        /// <param name="args">Property changed</param>
        protected void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, args);
        }
        /// <summary>
        /// Wrapper for the property changed event
        /// where the content of the args is passed in
        /// </summary>
        /// <param name="propertyName">Name of the property changed</param>
        /// <param name="value">New string value to recipicate</param>
        protected void OnPropertyChanged(String propertyName, String value)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new ValuedPropertyChangedEventArgs(propertyName, value));
        }
    }
}
