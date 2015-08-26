using NativeWebView;
using NativeWebView.API;
using NativeWebView.Core.HTML.DOM.Base;
using NativeWebView.HTML;
using NativeWebView.HTML.Base;
using NativeWebView.HTML.CSS;
using NativeWebView.HTML.DOM.Base;
using NativeWebView.HTML.JavaScript.Base;
using NativeWebView.JSON;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NativeWebView
{
    /// <summary>
    /// Encapulation of the HTMLCSharp user interface.
    /// Uses the UI componeent, the IWebView and generate HTML to display.
    /// To use the library, instantate and manipulate this object.  A hello world example:
    /// var webView = new WebViewUserControl();
    /// var textElement = new TextElement("Hello World");
    /// var contentView = new ContentView(webView, null, textElement);
    /// 
    /// If you wanted to add another element to the screen, you must have an instance of a container element to add to.
    /// var webView = new WebViewUserControl();
    /// var textElement = new TextElement("Hello World");
    /// var divElement = new DivElement(textElement);
    /// var contentView = new ContentView()
    /// contentView.InitializeDisplay(webView, null, divElement);
    /// contentView.PageLoaded += (event, args) => { divElement.AddChild(new TextElement("Hello World 2")); }
    /// </summary>
    public sealed class ContentView
    {
        private static Dictionary<ContentView, IWebView> _WEBVIEW_OWNERS = new Dictionary<ContentView,IWebView>();
        /// <summary>
        /// Event for user interaction which occured on the IWebView to notify the user.
        /// The DisplayElement is the control which fired the event.
        /// The HtmlEventType is the type of the event
        /// </summary>
        public event TypedEventHandler<DisplayElement, HtmlEventType> DisplayEventFired;
        /// <summary>
        /// Event that the page has loaded
        /// All interactions with the system should occur after this event.
        /// </summary>
        public event TypedEventHandler<ContentView> PageLoaded;

        #region Private Variables
        private HtmlDocument _htmlDocument;
        private DisplayElement[] _elements;
        private CSSDocument _externalStyleSheet;
        #endregion
        /// <summary>
        /// Flag property if the HTML page has loaded
        /// </summary>
        public Boolean HasLoaded { get
            { return _htmlDocument.HasLoaded; }
            private set { _htmlDocument.HasLoaded = value; }
        }
        /// <summary>
        /// Defaul constructor.  Ties an IWebView to the given ContentView.
        /// </summary>
        /// <param name="webview">Webciew to send message</param>
        /// <param name="document"></param>
        public ContentView(IWebView webview, CSSDocument document = null)
        {
            if (_WEBVIEW_OWNERS.ContainsValue(webview))
                throw new ArgumentException("WebViewed is used by another ContentView.  Use a new instance");
            _WEBVIEW_OWNERS.Add(this, webview);
            webview.ReceivedMsg += _webView_ReceivedMsg;     
            _externalStyleSheet = document;
            _htmlDocument = new HtmlDocument();
        }
        public ContentView(IWebView webview, CSSDocument document = null, params DisplayElement[] elements):this(webview, document)
        {
            _htmlDocument = new HtmlDocument();
            InitializeDisplay(elements);
        }
        public void InitializeDisplay(params DisplayElement[] elements)
        {
            if (!HasLoaded)
            {
                _elements = elements;
                if (_externalStyleSheet != null)
                {
                    _htmlDocument.CssDocument = _externalStyleSheet;
                }
                if (elements != null)
                {
                    foreach (var element in elements)
                    {
                        _htmlDocument.Body.AddElement(element);
                    }
                }
                _WEBVIEW_OWNERS[this].SetPage(_htmlDocument.HtmlText);
            }
        }

        public void RegisterEvent(DisplayElement target, HtmlEventType type)
        {
            if (HasLoaded)
                PerformAction(RegistrationAction.RegisterEvent, target.Id, type);
            else
                throw new Exception("Cannot load register events untill system has loaded");
        }

        internal void PerformAction(RegistrationAction actionToPerform, String id, HtmlEventType type)
        {
            var msg = new JSON.JsonHelper((int)actionToPerform, id, type.ToString());
            _WEBVIEW_OWNERS[this].SendJavaScript(msg.ToString());
        }

        internal void PerformAction(RegistrationAction actionToPerform, String id, String propertyName, String propertyValue)
        {
            var msg = new JSON.JsonHelper((int)actionToPerform, id, propertyName, propertyValue);
            _WEBVIEW_OWNERS[this].SendJavaScript(msg.ToString());
        }
        internal void PerformAction(RegistrationAction actionToPerform, String sourceId, String objectHtml, String objectId, String css)
        {
            var msg = new JSON.JsonHelper((int)actionToPerform, sourceId, objectHtml, objectId, css);
            _WEBVIEW_OWNERS[this].SendJavaScript(msg.ToString());
        }
        #region Event Callbacks
        private void _webView_ReceivedMsg(IWebView sender, String e)
        {
            if (String.IsNullOrWhiteSpace(e))
                return;
            else if (e == "Loaded")
            {
                HasLoaded = true;
                _htmlDocument.Body.PropertyChanged += Body_PropertyChanged;
                if (PageLoaded != null)
                    PageLoaded(this);
            }
            else if (DisplayEventFired != null)
            {
                var jsonReply = new JsonHelper(e);
                //get the element that fired is
                var source = from element in _elements
                             where element.Id == jsonReply.Id
                             select element;
                if(source.Any() && jsonReply.Data.Any())
                {
                    DisplayEventFired(source.First(), (HtmlEventType)Enum.Parse(typeof(HtmlEventType), jsonReply.Data.First(), false));
                }
            }
        }
        /// <summary>
        /// Call back there is a proprety change in the HTML document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Body_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (HasLoaded)
            {
                RegistrationAction action;
                String value = "";
                #region Collection change event
                if (e is CollectionChangedArgs<DisplayElement>)
                {
                    var collectionEvent = e as CollectionChangedArgs<DisplayElement>;
                    if (collectionEvent.ItemsAdded != null)
                    {
                        foreach (var element in collectionEvent.ItemsAdded)
                        {
                            PerformAction(RegistrationAction.AddChild, (sender as IdentifiableElement).Id, (sender as HtmlContainer).InnerHtml, element.Id, element.Style.HtmlCode);
                        }
                    }
                }
                #endregion
                #region Property
                else
                {
                    if (sender is CSSSelector)
                    {
                        action = RegistrationAction.UpdateStyle;
                    }
                    else if (sender is DisplayElement)
                    {
                        if (sender.GetType().GetProperty(e.PropertyName) != null &&
                            sender.GetType().GetProperty(e.PropertyName).GetCustomAttributes(false).Where(x => x.GetType() == typeof(InnerTextAttribute)).Any())
                            action = RegistrationAction.UpdateInnerText;
                        else
                            action = RegistrationAction.UpdateProperty;
                    }
                    else
                        return;
                    if ((sender is HtmlContainer) && e.PropertyName == HtmlContainer.CHILD_UPDATE_VALUE)
                        return;//todo handle
                    else if (e is ValuedPropertyChangedEventArgs)
                        value = (e as ValuedPropertyChangedEventArgs).NewValue;
                    else
                        value = typeof(CSSSelector).GetProperty(e.PropertyName).GetValue(sender, null).ToString();
                    PerformAction(action, (sender as IdentifiableElement).Id, e.PropertyName, value);
                }
                #endregion
            }
        }
        #endregion
    }
 
}
