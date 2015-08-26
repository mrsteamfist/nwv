using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NativeWebView.Core.HTML.DOM.Base
{
    [AttributeUsage(AttributeTargets.Property, Inherited=false, AllowMultiple=true)]
    internal class TagAttributeAttribute : HtmlBaseAttribute
    {
        public TagAttributeAttribute(string attributeName, bool canBeNull = true, Type conversionType = null, string conversionMethod = null)
        {
            NameString = attributeName;
            CanBeNullBoolean = canBeNull;
            ConversionTypeType = conversionType;
            ConversionMethodString = conversionMethod;
        }
        public string NameString { get; private set; }
        public bool CanBeNullBoolean { get; private set; }
        public string ConversionMethodString { get; private set; }
        public Type ConversionTypeType { get; private set; }
    }
}
