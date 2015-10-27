using NativeWebView.Core.HTML.DOM.Base;
using NativeWebView.HTML.Base;
using NativeWebView.HTML.CSS;
using NativeWebView.HTML.CSS.Attributes;
using NativeWebView.HTML.DOM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NativeWebView.HTML.DOM.Base
{
    public abstract class DisplayElement : IdentifiableElement
    {
        private readonly Dictionary<PropertyInfo, TagAttributeAttribute> _attributes = new Dictionary<PropertyInfo, TagAttributeAttribute>();
        private readonly PropertyInfo _innerText = null;
        private readonly PropertyInfo _children = null;
        protected readonly String _tag;
        [TagAttribute("style", false)]
        public CSSSelector Style { get; protected set; }
        public String Class { get; protected set; }
        public DisplayElement(String id, String tag)
        {
            _tag = tag;
            if (String.IsNullOrWhiteSpace(id))
            {
                if(string.IsNullOrEmpty(_tag))
                    Id = "u" + Guid.NewGuid().ToString();
                else
                    Id = _tag + Guid.NewGuid().ToString();
            }
            else
                Id = id;

            Style = new CSSSelector(Id);
            Style.PropertyChanged += Style_PropertyChanged;
            //Generate a list of the all of the properties
            var type = this.GetType();
            foreach (var property in type.GetProperties())
            {
                foreach (var attribute in property.GetCustomAttributes(false))
                {
                    if (attribute.GetType() == typeof(InnerTextAttribute))
                    { _innerText = property; break; }
                    else if (attribute.GetType() == typeof(ChildrenAttribute))
                    { _children = property; break; }
                    else if (attribute.GetType() == typeof(TagAttributeAttribute))
                        _attributes.Add(property, (attribute as TagAttributeAttribute));
                }
            }
        }
        protected void Style_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(sender, e);
        }

        public void SetLocation(LengthUnits unit, Double? left = null, Double? right = null, Double? top = null, Double? bottom = null, ElementPositions? position = null)
        {
            if (position != null)
                Style.Position = position.Value;
            if (left != null)
                Style.Left = new LengthDescriptor(left.Value, unit);
            if (right != null)
                Style.Right = new LengthDescriptor(right.Value, unit);
            if (top != null)
                Style.Top = new LengthDescriptor(top.Value, unit);
            if (bottom != null)
                Style.Bottom = new LengthDescriptor(bottom.Value, unit);
        }
        public override String HtmlCode
        {
            get
            {
                var reply = new StringBuilder(String.Format("<{0} id='{1}'", _tag, Id));
                if(!String.IsNullOrEmpty(Class))
                    reply.AppendFormat(" class='{0}'", Class);
                foreach(var attribute in _attributes)
                {
                    var tmpValue = attribute.Key.GetValue(this, null);
                    string value = tmpValue == null ? null : tmpValue.ToString();
                    if(!(string.IsNullOrEmpty(value) && !attribute.Value.CanBeNullBoolean))
                        reply.AppendFormat(" {0}='{1}'", attribute.Value.NameString, value);
                }
                reply.Append(">");
                if (_innerText != null)
                    reply.Append(_innerText.GetValue(this, null).ToString());
                else if(_children != null)
                {
                    //ToDo: handle in custom way
                }
                reply.AppendFormat("</{0}>", _tag);
                return reply.ToString();
            }
        }
    }
}
