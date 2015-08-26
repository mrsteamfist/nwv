using NativeWebView.HTML.DOM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NativeWebView
{
    public class ContentElement : DisplayElement
    {
        public String Content { get; set; }
        public ContentElement(String content) : base(null, null)
        {
            Content=content;
        }
        public override string HtmlCode
        {
            get { return Content; }
        }
    }
}
