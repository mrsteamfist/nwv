using NativeWebView.Core.HTML.DOM.Base;
using NativeWebView.HTML.CSS.Attributes;

namespace NativeWebView.HTML.CSS
{
    public class ImageSelector : CSSSelector
    {
        public ImageSelector(string id) : base(id)
        {

        }
        private rect? _clip;
        /// <summary>
        /// 
        /// </summary>
        [TagAttribute("clip", true)]
        public rect? Clip
        {
            get
            {
                return _clip;
            }
            set
            {
                _clip = value;
                if(_clip == null)
                    OnPropertyChanged("clip", "auto");
                else
                    
                    OnPropertyChanged("clip", _clip.ToString());
            }
        }
        #region Helper methods to set clip object
        public int? ClipTop
        {
            get
            {
                if (_clip == null)
                    return null;
                return _clip.Value.Top;
            }
            set
            {
                if (value == null)
                    Clip = null;
                else
                {
                    if (_clip == null)
                        Clip = new rect() { Top = value.Value };
                    else
                    {
                        Clip = new rect()
                        {
                            Bottom = ClipBottom.Value,
                            Left = ClipLeft.Value,
                            Right = ClipRight.Value,
                            Top = value.Value,
                        };
                    }
                }
            }
        }
        public int? ClipRight
        {
            get
            {
                if (_clip == null)
                    return null;
                return _clip.Value.Right;
            }
            set
            {
                if (value == null)
                    Clip = null;
                else
                {
                    if (_clip == null)
                        Clip = new rect() { Right = value.Value };
                    else
                    {
                        Clip = new rect()
                        {
                            Bottom = ClipBottom.Value,
                            Left = ClipLeft.Value,
                            Right = value.Value,
                            Top = ClipTop.Value,
                        };
                    }
                }
            }
        }
        public int? ClipLeft
        {
            get
            {
                if (_clip == null)
                    return null;
                return _clip.Value.Left;
            }
            set
            {
                if (value == null)
                    Clip = null;
                else
                {
                    if (_clip == null)
                        Clip = new rect() { Left = value.Value };
                    else
                    {
                        Clip = new rect()
                        {
                            Bottom = ClipBottom.Value,
                            Left = value.Value,
                            Right = ClipRight.Value,
                            Top = ClipTop.Value,
                        };
                    }                      
                }
            }
        }
        public int? ClipBottom
        {
            get
            {
                if (_clip == null)
                    return null;
                return _clip.Value.Bottom;
            }
            set
            {
                if (value == null)
                    Clip = null;
                else
                {
                    if (_clip == null)
                        Clip = new rect() { Bottom = value.Value };
                    else
                    {
                        Clip = new rect()
                        {
                            Bottom = value.Value,
                            Left = ClipLeft.Value,
                            Right = ClipRight.Value,
                            Top = ClipTop.Value,
                        };
                    }
                }
            }
        }
        #endregion
    }
}
