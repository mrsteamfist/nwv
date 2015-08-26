using NativeWebView.Core.HTML.DOM.Base;
using NativeWebView.HTML.DOM.Base;
using System;
using System.Text;

namespace NativeWebView
{
    public enum InputElementsTypes
    {
        button,
        checkbox,
        color,
        date ,
        datetime ,
        //ToDo: handle - datetime_local ,
        email ,
        file,
        hidden,
        image,
        month ,
        number ,
        password,
        radio,
        range ,
        reset,
        search,
        submit,
        tel,
        text,
        time ,
        url,
        week,
    };
    public class InputElement : DisplayElement
    {
        public InputElement(InputElementsTypes type = InputElementsTypes.text, String id = null):
            base(id, "input")
        {
            PlaceholderText = null;
            Type = type;
        }

        public InputElementsTypes Type { get; private set; }
        [TagAttribute("placeholder")]
        public String PlaceholderText { get; private set; }
        public String Value { get; set; }
        private int? _size;

        public int? Size
        {
            get { return _size; }
            set { 
                _size = value;
                if (value == null)
                    OnPropertyChanged("size", "");
                else
                    OnPropertyChanged("size", value.ToString());
            }
        }
        
        public override string HtmlCode
        {
            get
            {
                StringBuilder reply = new StringBuilder();
                reply.Append("<input");
                if (!string.IsNullOrEmpty(PlaceholderText))
                {
                    reply.Append(" placeholder='");
                    reply.Append(PlaceholderText);
                    reply.Append("'");
                }
                if (!String.IsNullOrEmpty(Value))
                {
                    reply.Append(" value='");
                    reply.Append(Value);
                    reply.Append("'");
                }

                if (Size != null)
                {
                    if (Type == InputElementsTypes.number || Type == InputElementsTypes.text)
                    {
                        reply.Append(" size='");
                        reply.Append(Size.Value);
                        reply.Append("'");
                    }
                }

                reply.AppendFormat(" type='{0}' id='{1}'/>", Type.ToString().Replace('_', '-'), Id);
                return reply.ToString();
            }
        }
    }
}
