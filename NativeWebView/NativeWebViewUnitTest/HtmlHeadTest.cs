using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NativeWebView.HTML.JavaScript;
using NativeWebView.HTML;
using NativeWebView.HTML.Base;
using System.Collections.Generic;
using System.Reflection;

namespace NativeWebViewUnitTest
{
    /// <summary>
    /// Tests the HTML head object
    /// </summary>
    [TestClass]
    public class HtmlHeadTest
    {
        const String TEST_NAME = "TESTNAME";
        const String TEST_TEXT = "TESTTEXT";
        /// <summary>
        /// Verifies the add element adds the element to the private list
        /// </summary>
        [TestMethod]
        public void AddElementTest()
        {
            var line = new JavaScriptOperation(TEST_TEXT);
            var head = new HtmlHead();
            PrivateObject headObj = new PrivateObject(head);
            Assert.IsTrue(head.AddElement(line));
            var collection = headObj.GetField("_children") as IList<IHtmlElement>;
            Assert.IsNotNull(collection);
            Assert.IsTrue(collection.Contains(line));
            Assert.IsTrue(collection.Count == 1);
        }
        /// <summary>
        /// Verifies the test element of the HTML code
        /// </summary>
        [TestMethod]
        public void HtmlTest()
        {
            var line = new JavaScriptOperation(TEST_TEXT);
            var head = new HtmlHead();
            PrivateObject headObj = new PrivateObject(head);
            var expectedHtmlCode = String.Format("{0}{1}", headObj.GetField("_required", BindingFlags.NonPublic | BindingFlags.Static), line.HtmlCode);
            Assert.IsTrue(head.AddElement(line));
            Assert.AreEqual(head.Html, expectedHtmlCode);
        }
    }
}
