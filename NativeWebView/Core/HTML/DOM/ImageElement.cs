using NativeWebView.API;
using NativeWebView.Core.HTML.DOM.Base;
using NativeWebView.HTML.CSS;
using NativeWebView.HTML.DOM.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NativeWebView
{
    public class ImageElement : DisplayElement
    {
        private string _src;
        private int _height;
        private int _width;
        #region Ctor
        public ImageSelector ImageCSS
        {
            get
            {
                return Style as ImageSelector;
            }
        }
        /// <summary>
        /// constructor taking in a external image
        /// Also handles if there is not a predefined image
        /// </summary>
        /// <param name="externalPath">Path outside of the application where the image is</param>
        public ImageElement(string img, string id = null)
            : base(id, "img")
        {
			Src = img;
			Style = new ImageSelector(Id);
			Style.PropertyChanged += Style_PropertyChanged;
        }
        #endregion
        /// <summary>
        /// Source which holds the image
        /// </summary>
        [TagAttribute("src", false)]
        public String Src
        {
            get
            {
                return _src;
            }
            set
            {
                _src = value;
                if (_src == null)
                    _src = string.Empty;

                OnPropertyChanged("Src", _src);
            }
        }
        [TagAttribute("width")]
        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged("Width", value.ToString());
            }
        }
        [TagAttribute("height")]
        public int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged("Height", value.ToString());
            }
        }
        /// <summary>
        /// loads the image via byte array
        /// </summary>
        /// <param name="format">Format of the image</param>
        /// <param name="imageBytes">Array with the bytes of the image</param>
		/*
        public void SetImage(string format, byte[] imageBytes)
        {
            var stringBuilder = new StringBuilder("data:image/");
            stringBuilder.AppendFormat("{0};base64,", format);
            stringBuilder.Append(Convert.ToBase64String(imageBytes));
            Src = stringBuilder.ToString();
        }
        */
    }
}
