using NativeWebView.HTML.Base;
using NativeWebView.HTML.DOM.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NativeWebView.HTML
{
    /// <summary>
    /// This represents the body of the HTML page
    /// </summary>
    public sealed class HtmlBody : INotifyPropertyChanged /*: BindableObject*/
    {
        private static readonly string _required = ""; //ToDo: handle required body head information
        private IList<HtmlElement> _children = new List<HtmlElement>();
        /// <summary>
        /// Ctor
        /// </summary>
        public HtmlBody()
        {
        }
        /// <summary>
        /// These are elements which need to be placed in the head
        /// </summary>
        /// <returns></returns>
        public String HeadElements()
        {
            StringBuilder reply = new StringBuilder();
            reply.AppendLine("<style>");
            foreach (var element in _children)
            {
                if(element is DisplayElement)
                    reply.AppendLine((element as DisplayElement).Style.HtmlCode);
            }
            reply.AppendLine("</style>");
            return reply.ToString();
        }
        /// <summary>
        /// Adds an element to the body to be displayed
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool AddElement(HtmlElement element)
        {

            //OnPropertyChanging(HtmlContainer.CHILD_UPDATE_VALUE);
            _children.Add(element);
            element.PropertyChanged += element_PropertyChanged;
            OnPropertyChanged(HtmlContainer.CHILD_UPDATE_VALUE);
            return true;
        }

        void element_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(sender, e);
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        /*
        private void OnPropertyChanging(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
         * */
        public string Html
        {
            get
            {
                StringBuilder reply = new StringBuilder();
                reply.Append(_required);
                foreach (var element in _children)
                {
                    reply.AppendLine(element.HtmlCode);
                }
                    
                return reply.ToString();
            }
        }
        //ToDo: PropertyChanging
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
