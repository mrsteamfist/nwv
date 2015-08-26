using NativeWebView.HTML.Base;
using System;

namespace NativeWebView.JavaScript.Base
{
    /// <summary>
    /// Abstract class for all Javacript elements
    /// ToDo: Also implement an interface to fit injection methods
    /// ToDo: Implement javascript code validator
    /// </summary>
    public abstract class JavaScriptElement : HtmlElement
    {
        /// <summary>
        /// The tags to encapsulate javascript code.
        /// ToDo: turn into internal constant.  Conuser does not need to see this
        /// </summary>
        public const String JAVASCRIPT_FORMAT_SCRIPT_TAGS = "<script type='text/javascript'>{0}</script>";
    }
}
