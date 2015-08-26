using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NativeWebView.HTML.Base
{
    /// <summary>
    /// Base class for objects which can be referenced by UID
    /// </summary>
    public abstract class IdentifiableElement : HtmlElement
    {
        /// <summary>
        /// UID to reference an html item
        /// </summary>
        public String Id { get; protected set; }
    }
}
