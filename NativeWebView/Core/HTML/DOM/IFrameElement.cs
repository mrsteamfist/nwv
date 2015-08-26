using NativeWebView.Core.HTML.DOM.Base;
using NativeWebView.HTML.DOM.Base;
using System;

namespace NativeWebView
{
    public class IFrameElement : DisplayElement
    {
        private String _src;
        [TagAttribute("src")]
        public String Src
        {
            get { return _src; }
            set
            {
                _src = value;
                OnPropertyChanged("src", value ?? "");
            }
        }
        [InnerText]
        internal String DefaultMsg
        {
            get
            {
                return String.Format("Unable to load {0}", _src);
            }
        }
        public IFrameElement(String url, String id = null) :
            base(id, "iframe")
        {
            Src = url;
        }
    }
}
