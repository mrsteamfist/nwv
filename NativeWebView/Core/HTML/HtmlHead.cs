using NativeWebView.HTML.Base;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NativeWebView.HTML
{
    public sealed class HtmlHead
    {
        private static readonly string _required = "<meta charset=\"utf-8\" /><meta name=\"viewport\" content=\"width=device-width, user-scalable=no\"/><title>MyApp</title>";
        private IList<IHtmlElement> _children = new List<IHtmlElement>();

        public HtmlHead()
        {
        }

        public bool AddElement(IHtmlElement element)
        {
            _children.Add(element);
            return true;
        }

        public string Html
        {
            get
            {
                StringBuilder reply = new StringBuilder();
                reply.Append(_required);
                foreach (var element in _children)
                    reply.Append(element.HtmlCode);
                return reply.ToString();
            }
        }
    }
}
