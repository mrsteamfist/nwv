using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NativeWebView.HTML.CSS.Attributes
{
    /// <summary>
    /// Enumeration of the possible CSS visibility values
    /// </summary>
    public enum Visibility
    {
        /// <summary>
        /// Default value. The element is visible
        /// </summary>
        visible,
        /// <summary>
        /// The element is invisible (but still takes up space)
        /// </summary>
        hidden,
        /// <summary>
        /// Only for table elements. collapse removes a row or column, but it does not affect the table layout. The space taken up by the row or column will be available for other content. If collapse is used on other elements, it renders as "hidden"
        /// </summary>
        collapse,
    }
}
