using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NativeWebView.HTML.JavaScript;
using NativeWebView.JavaScript.Base;

namespace NativeWebViewUnitTest
{
    [TestClass]
    public class JavaScriptOperationTest
    {
        /// <summary>
        /// Test to verify that the javascript code passed in is then set to the JavaScript property
        /// </summary>
        [TestMethod]
        public void CtorTest()
        {
            const String JAVASCRIPT_CODE = "JAVASCRIPTCODE";
            var operation = new JavaScriptOperation(JAVASCRIPT_CODE);
            Assert.AreEqual(JAVASCRIPT_CODE, operation.JavaScript);
        }
        /// <summary>
        /// Verify that the javascript code passed in is enscapulated with
        /// the html javascript tags.
        /// </summary>
        [TestMethod]
        public void HtmlCodeTest()
        {
            const String JAVASCRIPT_CODE = "JAVASCRIPTCODE";
            var operation = new JavaScriptOperation(JAVASCRIPT_CODE);
            Assert.AreEqual(String.Format(JavaScriptElement.JAVASCRIPT_FORMAT_SCRIPT_TAGS, JAVASCRIPT_CODE), operation.HtmlCode);
        }
    }
}
