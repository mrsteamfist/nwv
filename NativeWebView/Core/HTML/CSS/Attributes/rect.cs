using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NativeWebView.HTML.CSS.Attributes
{
    public struct rect
    {
        public int Left
        {
            get;
            set;
        }
        public int Right
        {
            get;
            set;
        }
        public int Top
        {
            get;
            set;
        }
        public int Bottom
        {
            get;
            set;
        }
        public override string ToString()
        {
            return string.Format("rect({0}px,{1}px,{2}px,{3}px)", Top, Right, Bottom, Left);
        }
    }
}
