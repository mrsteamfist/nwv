using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NativeWebView.HTML.JavaScript;

namespace NativeWebViewUnitTest
{
    [TestClass]
    public class JavaScriptFunctionTest
    {
        const String TEST_NAME = "TESTNAME";
        const String TEST_TEXT = "TESTTEXT";
            
        /// <summary>
        /// Verifies the name is being set in the ctor
        /// </summary>
        [TestMethod]
        public void NameTest()
        {
            var func = new JavaScriptFunction(TEST_NAME);
            Assert.AreEqual(func.Name, TEST_NAME);
        }
        /// <summary>
        /// Verfies adding and accessing lines
        /// </summary>
        [TestMethod]
        public void LinesTest()
        {
            var line = new JavaScriptOperation(TEST_TEXT);
            var func = new JavaScriptFunction(TEST_NAME);
            func.Lines.Add(line);
            Assert.AreSame(line, func.Lines[0]);
        }
        /// <summary>
        /// Verify that the parameters can be set and retrieved
        /// </summary>
        [TestMethod]
        public void ParametersTest()
        {
            var func = new JavaScriptFunction(TEST_NAME);
            func.Parameters.Add(TEST_TEXT);
            Assert.AreEqual(func.Parameters[0], TEST_TEXT);
        }
        #region ToString
        [TestMethod]
        public void EmptyToStringTest()
        {
            var func = new JavaScriptFunction(TEST_TEXT);
            Assert.AreEqual(func.ToString(), "() {"+Environment.NewLine+"}");

        }
        [TestMethod]
        public void ParamToStringTest()
        {
            var func = new JavaScriptFunction(TEST_TEXT);
            func.Parameters.Add("PARAM1");
            func.Parameters.Add("PARAM2");
            Assert.AreEqual(func.ToString(), "(PARAM1,PARAM2) {" + Environment.NewLine + "}");
        }
        [TestMethod]
        public void BodyToStringTest()
        {
            var func = new JavaScriptFunction(TEST_TEXT);
            var line = new JavaScriptOperation("LINETEXT");
            func.Lines.Add(line);
            Assert.AreEqual(func.ToString(), "() {" + Environment.NewLine + "LINETEXT" + Environment.NewLine + "}");
        }
        [TestMethod]
        public void ToStringTest()
        {
            var func = new JavaScriptFunction(TEST_TEXT);
            var line = new JavaScriptOperation("LINETEXT");
            func.Lines.Add(line);
            func.Parameters.Add("PARAM1");
            func.Parameters.Add("PARAM2");
            Assert.AreEqual(func.ToString(), "(PARAM1,PARAM2) {" + Environment.NewLine + "LINETEXT" + Environment.NewLine + "}");
        }
        #endregion
        #region HtmlCode
        [TestMethod]
        public void EmptyHtmlCodeTest()
        {
            var func = new JavaScriptFunction(TEST_TEXT);
            Assert.AreEqual(func.HtmlCode, String.Format(JavaScriptFunction.JAVASCRIPT_FORMAT_SCRIPT_TAGS, "function " + TEST_TEXT + " () {" + Environment.NewLine + "}"));
        }
        [TestMethod]
        public void ParamEmptyHtmlCodeTest()
        {
            var func = new JavaScriptFunction(TEST_TEXT);
            func.Parameters.Add("PARAM1");
            func.Parameters.Add("PARAM2");
            Assert.AreEqual(func.HtmlCode, String.Format(JavaScriptFunction.JAVASCRIPT_FORMAT_SCRIPT_TAGS, "function " + TEST_TEXT + " (PARAM1,PARAM2) {" + Environment.NewLine + "}"));
        }
        [TestMethod]
        public void BodyEmptyHtmlCodeTest()
        {
            var func = new JavaScriptFunction(TEST_TEXT);
            var line = new JavaScriptOperation("LINETEXT");
            func.Lines.Add(line);
            Assert.AreEqual(func.HtmlCode, String.Format(JavaScriptFunction.JAVASCRIPT_FORMAT_SCRIPT_TAGS, "function " + TEST_TEXT + " () {" + Environment.NewLine + "LINETEXT" + Environment.NewLine + "}"));
        }
        [TestMethod]
        public void HtmlCodeTest()
        {
            var func = new JavaScriptFunction(TEST_TEXT);
            var line = new JavaScriptOperation("LINETEXT");
            func.Lines.Add(line);
            func.Parameters.Add("PARAM1");
            func.Parameters.Add("PARAM2");
            Assert.AreEqual(func.HtmlCode, String.Format(JavaScriptFunction.JAVASCRIPT_FORMAT_SCRIPT_TAGS, "function " + TEST_TEXT + " (PARAM1,PARAM2) {" + Environment.NewLine + "LINETEXT" + Environment.NewLine + "}"));
        }
        #endregion

    }
}
