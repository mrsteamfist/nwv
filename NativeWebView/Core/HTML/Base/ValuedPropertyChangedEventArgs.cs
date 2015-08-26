using System;
using System.ComponentModel;

namespace NativeWebView.HTML.Base
{
    /// <summary>
    /// Event args for a value on a string property has changed
    /// Contains the name of the proeprty
    /// </summary>
    public class ValuedPropertyChangedEventArgs : PropertyChangedEventArgs
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="name">The property updated</param>
        /// <param name="value">The new value of the property</param>
        public ValuedPropertyChangedEventArgs(String name, String value) :
            base(name)
        {
            NewValue = value;
        }
        /// <summary>
        /// The new value of the property set in the ctor
        /// </summary>
        public String NewValue { get; private set; }
    }
}
