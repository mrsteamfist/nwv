using NativeWebView.HTML.Base;
using NativeWebView.HTML.DOM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NativeWebView
{
    public class DivContainer : HtmlContainer
    {
        public IList<DisplayElement> _children = new List<DisplayElement>();

        public DivContainer(String id = null, params DisplayElement[] elements) :
            base(id, "div")
        {
            if (elements != null)
            {
                foreach (var element in elements)
                    AddElement(element);
            }
        }

        public override bool AddElement(DisplayElement element)
        {
            if (!_children.Where((DisplayElement e) => e.Id == element.Id).Any())
            {
                _children.Add(element);
                element.PropertyChanged += (s, e) => OnPropertyChanged(s, e);
                OnPropertyChanged(new CollectionChangedArgs<DisplayElement>(CHILD_UPDATE_VALUE, new List<DisplayElement>() { element }));
                return true;
            }
            return false;
        }

        public override string InnerHtml
        {
            get
            {
                StringBuilder reply = new StringBuilder();
                foreach(var child in _children)
                {
                    reply.AppendLine(child.HtmlCode);
                }
                return reply.ToString();
            }
        }

        public override string HtmlCode
        {
            get
            {
                StringBuilder reply = new StringBuilder("<div id='" + Id + "'");
                if(!String.IsNullOrEmpty(Class))
                    reply.AppendFormat(" class='{0}'", Class);
                reply.Append(">");
                reply.Append(InnerHtml);
                reply.Append("</div>");
                return reply.ToString();
            }
        }
    }
}
