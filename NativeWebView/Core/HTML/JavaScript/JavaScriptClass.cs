using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NativeWebView.JavaScript.Base;

namespace NativeWebView.HTML.JavaScript
{
    /// <summary>
    /// Currently only handle non static functions
    /// ToDo: To be tested
    /// Not in use, goal to only to be used internally
    /// </summary>
    [Obsolete("This class is not ready for production yet")]
    public class JavaScriptClass : JavaScriptElement
    {
        #region Declarations
        //ToDo: add member variables
        /// <summary>
        /// List of functions making up the class
        /// </summary>
        public IList<JavaScriptFunction> Definitions { get; private set; }
        /// <summary>
        /// Name of the class
        /// </summary>
        public String Name { get; private set; }
        /// <summary>
        /// Script to be embed into an html page.
        /// </summary>
        public override string HtmlCode
        {
            get
            {
                StringBuilder reply = new StringBuilder();
                reply.AppendFormat("var {0} = (function() {", Name);
                reply.AppendLine();
                foreach (var defintion in Definitions)
                {
                    reply.AppendFormat("{0}.prototype.{1} = function{2}", Name, defintion.Name, defintion.ToString()); 
                    reply.AppendLine();
                }
                reply.Append("})();");
                reply.AppendLine();
                return String.Format(JAVASCRIPT_FORMAT_SCRIPT_TAGS, reply.ToString());
            }
        }
        #endregion
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="name">Name of the class</param>
        public JavaScriptClass(String name)
        {
            Name = name;
            Definitions = new List<JavaScriptFunction>();
        }

    }
}
