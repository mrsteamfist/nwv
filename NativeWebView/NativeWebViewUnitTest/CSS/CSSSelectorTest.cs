using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NativeWebView.HTML.CSS;
using NativeWebView.HTML.CSS.Attributes;
using NativeWebView.HTML.DOM.Base;

namespace NativeWebViewUnitTest.CSS
{
    [TestClass]
    public class CSSSelectorTest
    {
        [TestMethod]
        public void PositionMethod()
        {
            var hasFired = false;
            var position =  ElementPositions.position_initial;
            var selector = new CSSSelector("TEST");
            selector.PropertyChanged += (sender, e) =>
            {
                hasFired = e.PropertyName == "position";
            };
            Assert.AreEqual(selector.Position, ElementPositions.position_relative);
            selector.Position = position;
            Assert.IsTrue(hasFired);
            Assert.AreEqual(position, selector.Position);
            Assert.AreEqual(selector.HtmlCode, @"#TEST {
position : " + ElementPositions.position_initial.CSSText() + @";
}
");
        }
        [TestMethod]
        public void TopMethod()
        {
            var hasFired = false;
            var top = new LengthDescriptor(33.3, LengthUnits.Percent);
            var selector = new CSSSelector("TEST");
            selector.PropertyChanged += (sender, e) =>
            {
                hasFired = e.PropertyName == "top";
            };
            selector.Top = top;
            Assert.IsTrue(hasFired);
            Assert.AreEqual(top, selector.Top);
            Assert.AreEqual(selector.HtmlCode, @"#TEST {
position : relative;
top : "+ top.ToString() +@"
}
");
        }

        [TestMethod]
        public void BottomMethod()
        {
            var hasFired = false;
            var length = new LengthDescriptor(33.3, LengthUnits.Percent);
            var selector = new CSSSelector("TEST");
            var expectedText = String.Format("TEST {{{0}position : relative;{0}bottom : {1};{0}}}{0}", Environment.NewLine, length.ToString());
            selector.PropertyChanged += (sender, e) =>
            {
                hasFired = e.PropertyName == "bottom";
            };
            selector.Bottom = length;
            Assert.IsTrue(hasFired);
            Assert.AreEqual(length, selector.Bottom);
            Assert.AreEqual(selector.HtmlCode, expectedText);
        }
        
        [TestMethod]
        public void RightTest()
        {
            var hasFired = false;
            var length = new LengthDescriptor(33.3, LengthUnits.Percent);
            var selector = new CSSSelector("TEST");
            selector.PropertyChanged += (sender, e) =>
            {
                hasFired = e.PropertyName == "right";
            };
            selector.Right = length;
            Assert.IsTrue(hasFired);
            Assert.AreEqual(length, selector.Right);
            Assert.AreEqual(selector.HtmlCode, @"#TEST {
position : relative;
right : "+ length.ToString() +@"
}
");
        }
        
        [TestMethod]
        public void LeftTest()
        {
            var hasFired = false;
            var length = new LengthDescriptor(33.3, LengthUnits.Percent);
            var selector = new CSSSelector("TEST");
            selector.PropertyChanged += (sender, e) =>
            {
                hasFired = e.PropertyName == "left";
            };
            selector.Left= length;
            Assert.IsTrue(hasFired);
            Assert.AreEqual(length, selector.Left);
            Assert.AreEqual(selector.HtmlCode, @"#TEST {
position : relative;
left : "+ length.ToString() +@"
}
");
        }
        
        [TestMethod]
        public void TextAlignmentTest()
        {
            var hasFired = false;
            var align = TextAlign.center;
            var selector = new CSSSelector("TEST");
            selector.PropertyChanged += (sender, e) =>
            {
                hasFired = e.PropertyName == "text-align";
            };
            selector.TextAlignment= align;
            Assert.IsTrue(hasFired);
            Assert.AreEqual(align, selector.TextAlignment);
            Assert.AreEqual(selector.HtmlCode, @"#TEST {
position : relative;
text-align : "+ align.ToString() + @"
}
");
        }
        
        [TestMethod]
        public void DisplayTest()
        {
            var hasFired = false;
            var display = NativeWebView.HTML.CSS.Attributes.Display.flex;
            var selector = new CSSSelector("TEST");
            selector.PropertyChanged += (sender, e) =>
            {
                hasFired = e.PropertyName == "display";
            };
            selector.Display= display;
            Assert.IsTrue(hasFired);
            Assert.AreEqual(display, selector.Display);
            Assert.AreEqual(selector.HtmlCode, @"#TEST {
position : relative;
display : "+ display.ToString() + @"
}
");
        }
        
        [TestMethod]
        public void VisibilityTest()
        {
            var hasFired = false;
            var visibility = Visibility.visible;
            var selector = new CSSSelector("TEST");
            selector.PropertyChanged += (sender, e) =>
            {
                hasFired = e.PropertyName == "visibility";
            };
            selector.Visibility = visibility;
            Assert.IsTrue(hasFired);
            Assert.AreEqual(visibility, selector.Visibility);
            Assert.AreEqual(selector.HtmlCode, @"#TEST {
position : relative;
visibility : " + visibility.ToString() + @"
}
");
        }

        [TestMethod]
        public void WidthTest()
        {
            var hasFired = false;
            var size = new LengthDescriptor(33.3, LengthUnits.Percent);
            var selector = new CSSSelector("TEST");
            selector.PropertyChanged += (sender, e) =>
            {
                hasFired = e.PropertyName == "width";
            };
            selector.Width = size;
            Assert.IsTrue(hasFired);
            Assert.AreEqual(size, selector.Width);
            Assert.AreEqual(selector.HtmlCode, @"#TEST {
position : relative;
width : " + size.ToString() + @"
}
");
        }
        [TestMethod]
        public void HeightTest()
        {
            var hasFired = false;
            var size = new LengthDescriptor(33.3, LengthUnits.Percent);
            var selector = new CSSSelector("TEST");
            selector.PropertyChanged += (sender, e) =>
            {
                hasFired = e.PropertyName == "heigth";
            };
            selector.Height = size;
            Assert.IsTrue(hasFired);
            Assert.AreEqual(size, selector.Height);
            Assert.AreEqual(selector.HtmlCode, @"#TEST {
position : relative;
height : " + size.ToString() + @"
}
");
        }

        [TestMethod]
        public void CtorTest()
        {
            var selector = new CSSSelector("TEST");
            Assert.AreEqual(selector.Id, "TEST");
            Assert.AreEqual(selector.Position, ElementPositions.position_relative);
        }
    }
}
