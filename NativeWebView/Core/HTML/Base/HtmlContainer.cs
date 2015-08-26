using NativeWebView.HTML.Base;
using NativeWebView.HTML.DOM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NativeWebViewUnitTest")]
namespace NativeWebView.HTML.Base
{
    public abstract class HtmlContainer : DisplayElement, IHtmlContainer
    {
        /// <summary>
        /// Property name for when childresn are updated
        /// </summary>
        internal const String CHILD_UPDATE_VALUE = "children";
        /// <summary>
        /// Bases contructor to enforce requiring generation of an ID element
        /// </summary>
        /// <param name="id">Id Element is how the system access each child element</param>
        public HtmlContainer(String id, String tag) : base(id, tag) { }
        /// <summary>
        /// Adds a child element ot the content control
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public abstract bool AddElement(DisplayElement element);
        /// <summary>
        /// Retrieves the display ready element
        /// </summary>
        public abstract String InnerHtml { get; }
    }
}
