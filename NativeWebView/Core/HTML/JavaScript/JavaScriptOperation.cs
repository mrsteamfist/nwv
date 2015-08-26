using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NativeWebView.JavaScript.Base;

namespace NativeWebView.HTML.JavaScript
{
    /// <summary>
    /// This is a wrapper some javascript code.
    /// This is not a function or a class, just some set of code wrapped by the script tags.
    /// This is useful for including plugging code for external html objects.
    /// Also used as general lines in a javascript function.
    /// </summary>
    public sealed class JavaScriptOperation : JavaScriptElement
    {
        /// <summary>
        /// The javascript passed in to display
        /// </summary>
        public String JavaScript { get; private set; }
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="text">The javascript you wish to display</param>
        public JavaScriptOperation(String text)
        {
            JavaScript = text;
        }
        /// <summary>
        /// Output of the javascript code (ie the tag elements)
        /// </summary>
        public override string HtmlCode
        {
            get 
            {
                return String.Format(JAVASCRIPT_FORMAT_SCRIPT_TAGS, JavaScript);
            }
        }
    }
}
