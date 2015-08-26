using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeWebView.HTML.DOM.Base
{
    public enum LengthUnits
    {
        Unknown,
        Pixels,
        Percent,
        Auto,        
    }
    public struct LengthDescriptor
    {
        public LengthDescriptor(double value = 0.0, LengthUnits unit = LengthUnits.Unknown)
        {
            Value = value;
            Unit = unit;
        }
        public double Value;
        public LengthUnits Unit;
        public override string ToString()
        {
            StringBuilder reply = new StringBuilder(Value.ToString());
            switch (Unit)
            {
                case LengthUnits.Pixels:
                    reply.Append("px");
                    break;
                case LengthUnits.Percent:
                    reply.Append("%");
                    break;
            }
            return reply.ToString();
        }
        /// <summary>
        /// By default, if you assign a value it will be that value in pixels
        /// </summary>
        /// <param name="d">Value to set</param>
        /// <returns>new length descriptor instance with that Value and units of pixels</returns>
        public static implicit operator LengthDescriptor(double d)
        {
            return new LengthDescriptor(d, LengthUnits.Pixels);
        }
        /// <summary>
        /// By default, if you assign a value it will be that value in pixels
        /// </summary>
        /// <param name="d">Value to set</param>
        /// <returns>new length descriptor instance with that Value and units of pixels</returns>
        public static implicit operator LengthDescriptor(int d)
        {
            return new LengthDescriptor(d, LengthUnits.Pixels);
        }
    }
}
