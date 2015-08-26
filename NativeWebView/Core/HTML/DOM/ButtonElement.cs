using NativeWebView.Core.HTML.DOM.Base;
using NativeWebView.HTML.DOM.Base;
using System;
using System.Text;

namespace NativeWebView
{
    public enum ButtonElementsTypes
    {
        button,
        submit,
        reset,
    }
    public class ButtonElement : DisplayElement
    {
        [InnerText]
        public String Text { get; private set; }
        [TagAttribute("type", false)]
        public ButtonElementsTypes ButtonType { get; private set; }
        public ButtonElement(String text, ButtonElementsTypes type = ButtonElementsTypes.button, String id = null) :
            base(id, "button")
        {
            Text = text;
            ButtonType = type;
        }
    }
}
