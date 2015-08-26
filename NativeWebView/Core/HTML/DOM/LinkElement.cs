using NativeWebView.Core.HTML.DOM.Base;
using NativeWebView.HTML.DOM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NativeWebView
{
    public class LinkElement: DisplayElement
    {
        [TagAttribute("href",false)]
        public String Href { get; private set; }
        [InnerText]
        public String InnerHtml { get; private set; }
        //public String Data { get; private set; }
        public LinkElement(String href, String content = null, String id = null)
            : base(id, "a")
        {
            //Todo handle null input on href
            Href = href;
            InnerHtml = content;
        }
    }
}
