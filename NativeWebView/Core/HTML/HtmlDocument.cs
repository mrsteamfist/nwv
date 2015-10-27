using NativeWebView;
using NativeWebView.API;
using NativeWebView.HTML.CSS;
using NativeWebView.HTML.DOM.Base;
using NativeWebView.HTML.JavaScript;
using NativeWebView.HTML.JavaScript.Base;
using NativeWebView.JSON;
using System;
using System.Reflection;
using System.Text;

namespace NativeWebView.HTML
{
    /// <summary>
    /// Encapsulation of an HTML document.
    /// It is broken into two parts the head and body, which relate to standard HTML code.
    /// It generates the Javascript to handle the message passsing updating the screen.
    /// It also handles converting all of the childresn intoa n html element
    /// </summary>
    public sealed class HtmlDocument
    {
        /// <summary>
        /// The body content of the HTML document.
        /// </summary>
        public HtmlBody Body { get; private set; }
        /// <summary>
        /// The head content of the HTML document.
        /// </summary>
        public HtmlHead Head { get; private set; }
        /// <summary>
        /// Flag if the document has loaded
        /// </summary>
        public bool HasLoaded { get; internal set; }
        /// <summary>
        /// External stylesheet document passed in to load
        /// </summary>
        public CSSDocument CssDocument { get; set; }
        /// <summary>
        /// Ctor loading the head and the load event.
        /// <seealso cref="GenerateHead()"/>
        /// </summary>
        public HtmlDocument()
        {
            Body = new HtmlBody();
            Head = GenerateHead();

			Body.AddElement(new JavaScriptOperation("window.onload = function () { SendNotification('Loaded'); }"));
        }
        /// <summary>
        /// Generate all of the head messages.
        /// Generate the handle event function
        /// <seealso cref="GenHandleEventFunction()"/>
        /// Generates the reciever function
        /// <seealso cref="GenerateRecieverFunction()"/>
        /// Generates the notification function
        /// <seealso cref="GenerateSendNotificationFunction()"/>
        /// Generate the CSS update functions
        /// <seealso cref="GenerateGetStyleSheetFunction()"/>
        /// </summary>
        /// <returns></returns>
        private static HtmlHead GenerateHead()
        {
            var reply = new HtmlHead();
			/*
#if DEBUG
            reply.AddElement(new JavaScript.JavaScriptOperation(@"window.onerror = function (message, uri, lineNumber, columnNumber) {
    if (columnNumber != null)
        alert(message + ' @ ' + uri + ':(' + lineNumber + ',' + columnNumber + ')');
    else
        alert(message + ' @ ' + uri + ':' + lineNumber);

    return false;
};"));
            reply.AddElement(new NativeWebView.ContentElement("<script src='http://192.168.1.182:8080/target/target-script-min.js#anonymous'></script>"));
#endif
*/
            reply.AddElement(GenHandleEventFunction());
            reply.AddElement(GenerateRecieverFunction());
            reply.AddElement(GenerateSendNotificationFunction());
            reply.AddElement(GenerateGetStyleSheetFunction());
            return reply;
        }
        #region JavaScript Generation
        private static JavaScriptFunction GenHandleEventFunction()
        {
            var reply = new JavaScriptFunction("HandleEvent");
            reply.Parameters.Add("id");
            reply.Parameters.Add("event");
            reply.Lines.Add(new JavaScriptOperation("SendNotification" + "(JSON.stringify({ COMMAND: 0, ID:id, DATA:event}));"));
            return reply;
        }
        private static String GenerateRegisterEvent()
        {
            return @"{ document.getElementById(parsedJson.ID).addEventListener(parsedJson.DATA, function(){ HandleEvent(parsedJson.ID, parsedJson.DATA);},false); }";
        }
        private static String GenerateUnregisterEvent()
        {
            return @"{ document.getElementById(parsedJson.ID).removeEventListener(parsedJson.DATA, function(){HandleEvent(parsedJson.ID, parsedJson.DATA);},true); }";
        }
        private static String GenerateAddChild()
        {
            return @"{ 
    var element = document.getElementById(parsedJson.ID);
    element.innerHTML = parsedJson.DATA[0];s
}";
    //ToDo: handle style update
        }
        private static String GenerateRemoveChild()
        {
            return @"{ document.getElementById('parsedJson.ID').parentElement.removeChild(document.getElementById('parsedJson.DATA[0]')); }";
        }
        public static String GenerateUpdateStyle()
        {
            return @"{ document.getElementById(parsedJson.ID).style.setProperty(parsedJson.DATA[0].toLowerCase(), parsedJson.DATA[1]); }";
        }
        public static String GenerateUpdateProperty()
        {
            return @"{ document.getElementById(parsedJson.ID).setAttribute(parsedJson.DATA[0],parsedJson.DATA[1]); }";
        }
        public static String GenerateUpdateInnerText()
        {
            return @"{ document.getElementById(parsedJson.ID).innerHtml = parsedJson.DATA[0]; }";
        }
        public static JavaScriptFunction GenerateGetStyleSheetFunction()
        {
            var reply = new JavaScriptFunction(WebViewConstants.ADD_CSS_SCRIPT);
            reply.Parameters.Add("selector");
            reply.Parameters.Add("rules");
            var operation = new StringBuilder();
            operation.AppendLine("var sheet = document.styleSheets[0];");
            operation.AppendLine("if ('insertRule' in sheet) { sheet.insertRule(selector + '{' + rules + '}, 1); }");
            operation.AppendLine("else if ('addRule' in sheet) { sheet.insertRule(addRule + '{' + rules + '}, 1); }");
            return reply;
        }
        private static JavaScriptFunction GenerateRecieverFunction()
        {
            var reply = new JavaScriptFunction(WebViewConstants.JSON_SCRIPT);
            reply.Parameters.Add("msg");
            var operation = new StringBuilder();
            operation.AppendLine("var parsedJson = JSON.parse(msg);");
            operation.AppendLine("if(parsedJson != null)");
            operation.AppendLine("{");
            operation.AppendFormat("if(parsedJson.COMMAND == {0}){1}", (int)RegistrationAction.RegisterEvent, GenerateRegisterEvent());
            operation.AppendLine();
            operation.AppendFormat("else if(parsedJson.COMMAND == {0}){1}", (int)RegistrationAction.UnregisterEvent, GenerateUnregisterEvent());
            operation.AppendLine();
            operation.AppendFormat("else if(parsedJson.COMMAND == {0}){1}", (int)RegistrationAction.UpdateStyle, GenerateUpdateStyle());
            operation.AppendLine();
            operation.AppendFormat("else if(parsedJson.COMMAND == {0}){1}", (int)RegistrationAction.UpdateProperty, GenerateUpdateProperty());
            operation.AppendLine();
            operation.AppendFormat("else if(parsedJson.COMMAND == {0}){1}", (int)RegistrationAction.UpdateInnerText, GenerateUpdateInnerText());
            operation.AppendLine();
            operation.AppendFormat("else if(parsedJson.COMMAND == {0}){1}", (int)RegistrationAction.AddChild, GenerateAddChild());
            operation.AppendLine();
            operation.AppendFormat("else if(parsedJson.COMMAND == {0}){1}", (int)RegistrationAction.RemoveChild, GenerateRemoveChild());
            operation.AppendLine();
            operation.AppendLine("}");
            reply.Lines.Add(new JavaScriptOperation(operation.ToString()));
            return reply;
        }
        private static JavaScriptFunction GenerateSendNotificationFunction()
        {
            var reply = new JavaScriptFunction("SendNotification");
            reply.Parameters.Add("msg");
			reply.Lines.Add(new JavaScriptOperation(@"if (typeof window.external !== 'undefined' && typeof window.external.notify !== 'undefined')
    window.external.notify(msg);
else if (typeof External !== 'undefined' && typeof External.notify !== 'undefined')
    External.notify(msg);
else
    console.log(msg);"));
            return reply;
        }
        #endregion
        public string HtmlText
        {
            get
            {
                StringBuilder builder = new StringBuilder("<!DOCTYPE html> <html lang=\"en\"><head>");
                builder.AppendLine(Head.Html);
                //builder.AppendLine(Body.HeadElements());
                if (CssDocument != null)
                {
                    builder.AppendLine("<style>");
                    builder.AppendLine(CssDocument.HtmlCode);
                    builder.AppendLine("</style>");
                }
                    
                builder.AppendLine("</head><body>");
                builder.AppendLine(Body.Html);
                builder.AppendLine("</body></html>");
				return builder.ToString ();
            }
        }

        public override string ToString()
        {
            return HtmlText;
        }
    }
}
