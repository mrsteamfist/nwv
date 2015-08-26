using NativeWebView.JavaScript.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NativeWebView.HTML.JavaScript
{
    /// <summary>
    /// A javascript function which can be executed
    /// </summary>
    public sealed class JavaScriptFunction : JavaScriptElement
    {
        #region Declarations
        /// <summary>
        /// Name of the function
        /// </summary>
        public String Name { get; private set; }
        /// <summary>
        /// List of the lines of the in the javascript operation
        /// </summary>
        public IList<JavaScriptOperation> Lines { get; private set; }
        /// <summary>
        /// Passed in parameters of the javascript function
        /// </summary>
        public IList<String> Parameters { get; private set; }
        /// <summary>
        /// The javascript function code wrapped in script tags
        /// </summary>
        public override string HtmlCode
        {
            get
            {
                return String.Format(JAVASCRIPT_FORMAT_SCRIPT_TAGS, String.Format("function {0} {1}", Name, ToString()));
            }
        }
        #endregion
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="name">Name of the javascript function</param>
        public JavaScriptFunction(string name)
        {
            Name = name;
            Parameters = new List<String>();
            Lines = new List<JavaScriptOperation>(); 
        }
        #region Methods
        /// <summary>
        /// Override of the ToString to generate the actual javascript code
        /// </summary>
        /// <returns>The javascritp funtion code</returns>
        public override string ToString()
        {
            StringBuilder reply = new StringBuilder("(");
            foreach (var param in Parameters)
                reply.AppendFormat("{0},", param);
            if (Parameters.Count > 0)
                reply.Replace(',', ')', reply.Length - 1, 1);
            else
                reply.Append(')');
            reply.Append(" {");
            reply.AppendLine();

            foreach(var line in Lines)
                reply.AppendLine(line.JavaScript);

            reply.Append('}');
            return reply.ToString();
        }
        #endregion
    }
}
