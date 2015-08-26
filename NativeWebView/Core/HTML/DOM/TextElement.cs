using NativeWebView.Core.HTML.DOM.Base;
using NativeWebView.HTML;
using NativeWebView.HTML.Base;
using NativeWebView.HTML.DOM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeWebView
{
    public class TextElement : DisplayElement
    {
        private String _innerText;
        /// <summary>
        /// Constructor setting the id and the inner text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="id"></param>
        public TextElement(String text = "", String id = null) :
            base(id, "p")
        {
            _innerText = text;
        }
        [InnerText]
        public String InnerText {
            get { return _innerText; }
            set { 
                _innerText = value;
                OnPropertyChanged("InnerText", value);
            }
        }
    }
}
