using NativeWebView.Core.HTML.DOM.Base;
using NativeWebView.HTML.Base;
using NativeWebView.HTML.CSS.Attributes;
using NativeWebView.HTML.DOM.Base;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace NativeWebView.HTML.CSS
{
    /// <summary>
    /// Class to manage the CSS access
    /// </summary>
    public class CSSSelector : IdentifiableElement
    {
        private readonly Dictionary<PropertyInfo, TagAttributeAttribute> _attributes = new Dictionary<PropertyInfo, TagAttributeAttribute>();
        private ElementPositions _position;
        private LengthDescriptor? _top;
        private LengthDescriptor? _bottom;
        private LengthDescriptor? _right;
        private LengthDescriptor? _left;
        private TextAlign? _textAlignment;
        private Display? _display;
        private Visibility? _visibility;
        private LengthDescriptor? _width;
        private LengthDescriptor? _height;
        private int? _zindex;
        /// <summary>
        /// Current element position.  Defaults to initial.
        /// </summary>
        [TagAttribute("position", false, typeof(ElementPositionsExtension), "ToCSSText")]
        public ElementPositions Position
        {
            get { return _position; }
            set {
                _position = value;
                OnPropertyChanged("position", value.CSSText());
            }
        }
        /// <summary>
        /// Distance of the element from top
        /// </summary>
        [TagAttribute("top", false)]
        public LengthDescriptor? Top
        {
            get { return _top; }
            set {
                _top = value;
                if(value == null)
                    OnPropertyChanged("top", "");
                else
                    OnPropertyChanged("top", value.Value.ToString());
            }
        }
        /// <summary>
        /// Distance of element from bottom
        /// </summary>
        [TagAttribute("bottom", false)]
        public LengthDescriptor? Bottom
        {
            get { return _bottom; }
            set {
                _bottom = value;
                if (value == null)
                    OnPropertyChanged("bottom", "");
                else
                    OnPropertyChanged("bottom", value.Value.ToString());
            }
        }
        /// <summary>
        /// Distance from elementy from right
        /// </summary>
        [TagAttribute("right", false)]
        public LengthDescriptor? Right
        {
            get { return _right; }
            set {
                _right = value;
                if (value == null)
                    OnPropertyChanged("right", "");
                else
                    OnPropertyChanged("right", value.Value.ToString());
            }
        }
        /// <summary>
        /// Distance of elment from left
        /// </summary>
        [TagAttribute("left", false)]
        public LengthDescriptor? Left
        {
            get { return _left; }
            set {
                _left = value;
                if (value == null)
                    OnPropertyChanged("left", "");
                else
                    OnPropertyChanged("left", value.Value.ToString());
            }
        }
        /// <summary>
        /// Text alignement of element
        /// </summary>
        [TagAttribute("text-align", false)]
        public TextAlign? TextAlignment
        {
            get { return _textAlignment; }
            set {
                _textAlignment = value;
                if (value == null)
                    OnPropertyChanged("text-align", "");
                else
                    OnPropertyChanged("text-align", value.Value.ToString());
            }
        }
        /// <summary>
        /// Display type
        /// </summary>
        [TagAttribute("display", false, typeof(DisplayExtension), "ToCSSText")]
        public Display? Display
        {
            get { return _display; }
            set {
                _display = value;
                if (value == null)
                    OnPropertyChanged("display", "");
                else
                    OnPropertyChanged("display", value.Value.CSSText());
            }
        }
        /// <summary>
        /// visibilty of element
        /// </summary>
        [TagAttribute("visibility", false)]
        public Visibility? Visibility
        {
            get { return _visibility; }
            set {
                _visibility = value;
                if (value == null)
                    OnPropertyChanged("visibility", "");
                else
                    OnPropertyChanged("visibility", value.Value.ToString());
            }
        }
        /// <summary>
        /// Width of element
        /// </summary>
        [TagAttribute("width", false)]
        public LengthDescriptor? Width
        {
            get { return _width; }
            set
            {
                _width = value;
                if (value == null)
                    OnPropertyChanged("width", "");
                else
                    OnPropertyChanged("width", value.ToString());
            }
        }
        /// <summary>
        /// Height of element
        /// </summary>
        [TagAttribute("height", false)]
        public LengthDescriptor? Height
        {
            get { return _height; }
            set
            {
                _height = value;
                if (value == null)
                    OnPropertyChanged("height", "");
                else
                    OnPropertyChanged("height", value.ToString());
            }
        }
        protected LengthDescriptor? _marginTop;
        [TagAttribute("margin-top", false)]
        public LengthDescriptor? MarginTop
        {
            get { return _marginTop; }
            set
            {
                _marginTop = value;
                if (value == null)
                    OnPropertyChanged("margin-top", "");
                else
                    OnPropertyChanged("margin-top", value.ToString());
            }
        }
        /// <summary>
        /// Depth order
        /// </summary>
        [TagAttribute("z-index", false)]
        public int? ZIndex
        {
            get { return _zindex; }
            set
            {
                _zindex = value;
                if (value == null)
                    OnPropertyChanged("z-index", "");
                else
                    OnPropertyChanged("z-index", value.ToString());
            }
        }
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="id">UID of the element</param>
        public CSSSelector(String id)
        {
            Id = id;
            Position = ElementPositions.position_relative;
            //load all of the attributes
            var type = this.GetType();
            foreach (var property in type.GetProperties())
            {
                foreach (var attribute in property.GetCustomAttributes(false))
                {
                    if (attribute.GetType() == typeof(TagAttributeAttribute))
                    {
                        _attributes.Add(property, (attribute as TagAttributeAttribute));
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// Creates a style block, currently not used
        /// </summary>
        public override String HtmlCode
        {
            get
            {
                StringBuilder reply = new StringBuilder();
                reply.AppendFormat("#{0} ", Id);
                reply.AppendLine("{");
                foreach (var attribute in _attributes)
                {
                    var tmp = GeneratePropertyValue(attribute);
                    if (tmp != null)
                    {
                        reply.AppendLine(" " + tmp);
                    }
                }
                reply.AppendLine("}");
                return reply.ToString();
            }
        }
        public override string ToString()
        {
 	        StringBuilder reply = new StringBuilder();
            foreach (var attribute in _attributes)
            {
                var tmp = GeneratePropertyValue(attribute);
                if (tmp != null)
                    reply.Append(" " + tmp);
            }
            return reply.ToString();
        }
        string GeneratePropertyValue(KeyValuePair<PropertyInfo, TagAttributeAttribute> attribute)
        {
            var tmpValue = attribute.Key.GetValue(this, null);
            string value = null;
            if (tmpValue != null && attribute.Value.ConversionTypeType != null && attribute.Value.ConversionMethodString != null)
            {
                var method = attribute.Value.ConversionTypeType.GetMethods(BindingFlags.Static | BindingFlags.Public).Where(x => x.GetParameters().Where(p => p.ParameterType == tmpValue.GetType()).Any()).FirstOrDefault();
                if (method != null)
                    value = method.Invoke(null, new object[] { tmpValue }).ToString();
                else if(tmpValue != null)
                    value = tmpValue.ToString();
            }
            else if (tmpValue != null)
                value = tmpValue.ToString();

            if (!(string.IsNullOrEmpty(value) && !attribute.Value.CanBeNullBoolean))
            {
                return String.Format("{0}: {1};", attribute.Value.NameString, value);
            }
            return null;
        }
    }
}
