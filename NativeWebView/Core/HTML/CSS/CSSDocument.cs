using NativeWebView.HTML.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NativeWebView.HTML.CSS
{
    /// <summary>
    /// Wrapper for external css code being imported into the system
    /// </summary>
    //ToDo: Parse inputted css code
    public class CSSDocument : HtmlElement
    {
        private String _cssCode;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cssCode">Valid CSS code</param>
        public CSSDocument(String cssCode)
        {
            _cssCode = cssCode;
        }
        /// <summary>
        /// The valid css sheet
        /// </summary>
        public override string HtmlCode
        {
            get { return _cssCode; }
        }
    }
}
