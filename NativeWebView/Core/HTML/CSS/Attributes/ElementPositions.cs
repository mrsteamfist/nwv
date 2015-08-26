using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NativeWebView.HTML.CSS.Attributes
{
    /// <summary>
    /// Conversion the enumeration to actual CSS code
    /// </summary>
    public static class ElementPositionsExtension
    {
        /// <summary>
        /// Function to write CSS text
        /// </summary>
        public static String CSSText(this ElementPositions position)
        {
            var reply = position.ToString();
            return reply.Substring(9);
        }
        public static String ToCSSText(ElementPositions position)
        {
            return position.ToString().Substring(9);
        }
        public static String ToCSSText(ElementPositions? position)
        {
            if (position == null)
                return null;

            return position.ToString().Substring(9);
        }
    }
    /// <summary>
    /// Enumeration of the CSS positions of an element
    /// </summary>
    public enum ElementPositions
    {
        /// <summary>
        /// Default value. Elements render in order, as they appear in the document flow
        /// </summary>
        position_static,
        /// <summary>
        /// The element is positioned relative to the browser window. does not move with scrolling
        /// </summary>
        position_fixed,
        /// <summary>
        /// The element is positioned relative to its normal position, so "left:20" adds 20 pixels to the element's LEFT position 
        /// </summary>
        position_relative,
        /// <summary>
        /// The element is positioned relative to the browser window
        /// </summary>
        position_absolute,
        /// <summary>
        /// Sets this property to its default value
        /// </summary>
        position_initial,
    }
}
