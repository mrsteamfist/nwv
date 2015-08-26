using NativeWebView.HTML.Base;
using NativeWebView.HTML.DOM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NativeWebView
{
    public struct DisplayElementLocation
    {
        public DisplayElementLocation(DisplayElement element, ushort row = 1, ushort column = 1)
        {
            Element = element;
            Row = row;
            Column = column;
        }
        public DisplayElement Element;
        public ushort Row;
        public ushort Column;
    }

    public class TableContainer : HtmlContainer
    {
        /// <summary>
        /// Container to hold each element and where they live in the control
        /// The first int is the row
        /// The 2nd int is the column
        /// </summary>
        public IList<IList<DisplayElement>> _children = new List<IList<DisplayElement>>();

        public TableContainer(String id = null, params DisplayElementLocation[] elements) :
            base(id, "table")
        {
            if (elements != null)
            {
                foreach (var element in elements)
                    AddElement(element);
            }
        }

        public bool AddElement(DisplayElementLocation element)
        {
            return AddElement(element.Element, element.Row, element.Column);
        }

        public bool AddElement(DisplayElement element, ushort row, ushort column)
        {
            if (row < 1 || column < 1)
                throw new ArgumentException("Rows and Columns are 1 based");
            while (_children.Count < row)
                _children.Add(new List<DisplayElement>());
            while (_children[row - 1].Count < column)
                _children[row - 1].Add(null);

            _children[row - 1][column - 1] = element;
            element.PropertyChanged += (s, e) => OnPropertyChanged(s, e);
            OnPropertyChanged(new CollectionChangedArgs<DisplayElement>(CHILD_UPDATE_VALUE, new List<DisplayElement>() { element}));
            return true;
        }
        public override string HtmlCode
        {
            get
            {
                StringBuilder reply = new StringBuilder("<table id='" + Id + "'>");
                reply.Append(InnerHtml);
                reply.Append("</table>");
                return reply.ToString();
            }
        }
        public override bool AddElement(DisplayElement element)
        {
            if (_children.Count == 0)
                _children.Add(new List<DisplayElement>());
            _children.Last().Add(element);
            OnPropertyChanged(new CollectionChangedArgs<DisplayElement>(CHILD_UPDATE_VALUE, new List<DisplayElement>() { element }));
            return true;
        }
        public override String InnerHtml
        {
            get
            {
                StringBuilder reply = new StringBuilder();
                foreach(var children in _children)
                {
                    reply.Append("<tr>");
                    foreach(var child in children)
                    {
                        reply.Append("<td>");
                        if (child != null)
                            reply.Append(child.HtmlCode);
                        reply.Append("</td>");
                    }
                    reply.Append("</tr>");
                }
                
                return reply.ToString();
            }
        }
    }
}
