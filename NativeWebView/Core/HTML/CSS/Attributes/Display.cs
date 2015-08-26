using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NativeWebView.HTML.CSS.Attributes
{
    /// <summary>
    /// Extention method to convert the CSS text to something which can be sent.
    /// </summary>
    public static class DisplayExtension
    {
        /// <summary>
        /// Text conversion
        /// </summary>
        /// <param name="display"></param>
        /// <returns></returns>
        public static String CSSText(this Display display)
        {
            return display.ToString().Replace('_','-');
        }
        public static String ToCSSText(Display display)
        {
            return display.ToString().Replace('_', '-');
        }
        public static String ToCSSText(Display? display)
        {
            if (display == null)
                return null;

            return display.ToString().Replace('_', '-');
        }
    }
    /// <summary>
    /// Enumeration of the possible CSS display values
    /// </summary>
    public enum Display
    {
        /// <summary>
        /// Default value. Displays an element as an inline element (like <span>)
        /// </summary>
        inline,
        /// <summary>
        /// Displays an element as a block element (like <p>) Play it » 
        /// </summary>
        block,
        /// <summary>
        /// Displays an element as an block-level flex container. New in CSS3  
        /// </summary>
        flex,
        /// <summary>
        ///  Displays an element as an inline-level block container. The inside of this block is formatted as block-level box, and the element itself is formatted as an inline-level box  
        /// </summary>
        inline_block,
        /// <summary>
        ///  Displays an element as an inline-level flex container. New in CSS3  
        /// </summary>
        inline_flex,
        /// <summary>
        /// The element is displayed as an inline-level table   
        /// </summary>
        inline_table,
        /// <summary>
        ///  Let the element behave like a <li> element Play it » 
        /// </summary>
        list_item,
        /// <summary>
        ///  Displays an element as either block or inline, depending on context
        /// </summary>
        run_in,
        /// <summary>
        /// Let the element behave like a <table> element
        /// </summary>
        table,
        /// <summary>
        /// Let the element behave like a <caption> element  
        /// </summary>
        table_caption,
        /// <summary>
        ///  Let the element behave like a <colgroup> element  
        /// </summary>
        table_column_group,
        /// <summary>
        /// Let the element behave like a <col> element
        /// </summary>
        table_column,
        /// <summary>
        /// Let the element behave like a <tr> element
        /// </summary>
        table_row,
        /// <summary>
        /// The element will not be displayed at all (has no effect on layout)
        /// </summary>
        /// /// <summary>
        ///  Let the element behave like a <thead> element  
        /// </summary>
        table_header_group,
        /// <summary>
        /// Let the element behave like a <tfoot> element
        /// </summary>
        table_footer_group,
        /// <summary>
        /// Let the element behave like a <tbody> element
        /// </summary>
        table_row_group,
        /// <summary>
        /// Let the element behave like a <td> element
        /// </summary>
        table_cell,
        none,
    }
}
