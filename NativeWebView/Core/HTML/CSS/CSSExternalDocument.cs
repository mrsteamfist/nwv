using NativeWebView.HTML.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeWebView
{
    /// <summary>
    /// Current development of the externally linked style sheet
    /// </summary>
    public class CSSExternalDocument : HtmlElement
    {
        private String _file;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="externalStyleSheet">Path of the external style sheet to link to</param>
        public CSSExternalDocument(String externalStyleSheet = null)
        {
            _file = externalStyleSheet;
        }
        /// <summary>
        /// Writing the external style sheet code to file
        /// </summary>
        public override string HtmlCode
        {
            get { return "<link type='text/css' href='" + _file + "' rel='stylesheet' >"; }
        }
    }
}
