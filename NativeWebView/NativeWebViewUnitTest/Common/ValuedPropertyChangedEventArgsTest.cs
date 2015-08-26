using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NativeWebView.HTML.Base;

namespace NativeWebViewUnitTest
{
    /// <summary>
    /// Tests to verify ValuedPropertyChangedEventArgs
    /// </summary>
    [TestClass]
    public class ValuedPropertyChangedEventArgsTest
    {
        /// <summary>
        /// Test to verify the constructor is setting the property name and the new value
        /// </summary>
        [TestMethod]
        public void CtorTest()
        {
            const String propName = "PROPERTY_NAME";
            const String newValue = "PROPERTY_VALUE";

            var changeArgs = new ValuedPropertyChangedEventArgs(propName, newValue);
            Assert.AreEqual(changeArgs.PropertyName, propName);
            Assert.AreEqual(changeArgs.NewValue, newValue);
        }
    }
}
