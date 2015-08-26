using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NativeWebView;
using NativeWebView.HTML;
using NativeWebView.HTML.Base;
using System.Collections.Generic;
using System.Reflection;

namespace NativeWebViewUnitTest
{
    [TestClass]
    public class HtmlBodyTest
    {
        const String TEST_NAME = "TESTNAME";
        const String TEST_TEXT = "TESTTEXT";
        [TestMethod]
        public void HeadElementTest()
        {
            var textElement = new TextElement(TEST_TEXT, TEST_NAME);
            var body = new HtmlBody();
            body.AddElement(textElement);
            var headerText = String.Format("<style>{1}{0}{1}</style>{1}", textElement.Style.HtmlCode, Environment.NewLine);
            Assert.AreEqual(body.HeadElements(), headerText);
        }
        /// <summary>
        /// Verfies that the following when invoked:
        /// element is added to the collection 
        /// event fired 
        /// changing the element bubbles up the event
        /// </summary>
        [TestMethod]
        public void AddElementTest()
        {
            var hasFiredChanged =false;
            var text = new TextElement();
            var body = new HtmlBody();
            var bodyObj = new PrivateObject(body);
            body.PropertyChanged += (source, args) => { hasFiredChanged = args.PropertyName == HtmlContainer.CHILD_UPDATE_VALUE; };
            Assert.IsTrue(body.AddElement(text));
            var collection = bodyObj.GetField("_children") as IList<HtmlElement>;
            Assert.IsTrue(hasFiredChanged);
            Assert.IsTrue(collection.Contains(text));
            Assert.IsTrue(collection.Count == 1);
        }
        /// <summary>
        /// Verified that the property changed element is fired for the given element
        /// </summary>
        [TestMethod]
        public void ChildPropertyChangedTest()
        {
            var hasFiredChanged = false;
            var text = new TextElement();
            var body = new HtmlBody();
            body.PropertyChanged += (source, args) => { hasFiredChanged = args.PropertyName == "InnerText" && ((ValuedPropertyChangedEventArgs)args).NewValue == TEST_TEXT; };
            body.AddElement(text);
            text.InnerText = TEST_TEXT;
            Assert.IsTrue(hasFiredChanged);
        }
        /// <summary>
        /// Verifying the outputed html text
        /// </summary>
        [TestMethod]
        public void HtmlTest()
        {
            var text = new TextElement(TEST_TEXT, TEST_NAME);
            var body = new HtmlBody();
            var bodyObject = new PrivateObject(body);
            var expectedText = String.Format("{0}{1}", bodyObject.GetField("_required", BindingFlags.NonPublic | BindingFlags.Static), text.HtmlCode);
            body.AddElement(text);
            Assert.AreEqual(expectedText, body.Html);
        }
    }
}
