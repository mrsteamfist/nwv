using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NativeWebView.API
{
    /// <summary>
    /// Generic delegate to handeling typed callbacks
    /// </summary>
    /// <typeparam name="TSource">The type of the thrower of the event</typeparam>
    /// <typeparam name="TEventArgs">The type of the event</typeparam>
    /// <param name="sender">The thrower of the event</param>
    /// <param name="e">Event args of the event</param>
    public delegate void TypedEventHandler<TSource, TEventArgs>(TSource sender, TEventArgs e);
    /// <summary>
    /// Generic delegate to handeling typed callbacks
    /// </summary>
    /// <typeparam name="TSource">The type of the thrower of the event</typeparam>
    /// <param name="sender">The thrower of the event</param>
    public delegate void TypedEventHandler<TSource>(TSource sender);
    /// <summary>
    /// Communication constants used in communication between the IWebView and the ContentView.
    /// </summary>
    public sealed class WebViewConstants
    {
        public const string JSON_SCRIPT = "MsgHandler";
        public const string ADD_CSS_SCRIPT = "AddCSSRule";
    }
    /// <summary>
    /// User Interface element which displays the HTML.
    /// </summary>
    public interface IWebView
    {
        /// <summary>
        /// Event which needs to be executed on the UI Dispatched
        /// </summary>
        event TypedEventHandler<IWebView, Action> UiTask;
        event EventHandler ControlLoaded;
        /// <summary>
        /// Wrapper to fire an action on the UI thread
        /// </summary>
        /// <param name="a"></param>
        void OnUiTask(Action a);
        /// <summary>
        /// Message send from this webview.
        /// String must be a WebViewConstant
        /// </summary>
        event TypedEventHandler<IWebView, String> ReceivedMsg;
        /// <summary>
        /// Sets the WebView to display the HTML
        /// </summary>
        /// <param name="html">The HTML to display</param>
        void SetPage(String html);
        /// <summary>
        /// Sends the following JSON package to the HTML code
        /// </summary>
        /// <param name="json">The content of the message</param>
        void SendJavaScript(String json);
        /// <summary>
        /// Gets the byte array information according the local image path
        /// </summary>
        /// <param name="path">Local path with the image</param>
        /// <returns></returns>
		System.Threading.Tasks.Task<String> GetImageData(String path, string format);
    }
}
