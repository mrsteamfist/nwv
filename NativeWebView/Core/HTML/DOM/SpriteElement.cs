using NativeWebView.HTML.CSS.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NativeWebView
{
    public class SpriteElement : ImageElement
    {
        private int _currentFrame;
        private int _clipSize;
        /// <summary>
        /// The current frame to show
        /// </summary>
        public int CurrentFrame { get { 
            return _currentFrame; }
            set
            {
                if(value >= 0 && value < FrameCount && _currentFrame != value)
                {
                    _currentFrame = value;
                    Style.MarginTop = 0 - (value * _clipSize);
                    ImageCSS.Clip = new rect()
                    {
                        Bottom = (value+1) * _clipSize,
                        Top = value * _clipSize,
                        Left = 0,
                        Right = Width,
                    };
                }
            }
        }
        /// <summary>
        /// Gets the max number of frames
        /// </summary>
        public int FrameCount { get; private set; }
        /// <summary>
        /// Builds the image and the sprite
        /// </summary>
        /// <param name="format"></param>
        /// <param name="imageBytes"></param>
        /// <param name="frames"></param>
        /// <param name="totalWidth"></param>
        /// <param name="totalHeight"></param>
        public SpriteElement(string img, int frames, int totalWidth, int totalHeight) : base(img)
        {
            CurrentFrame = 0;
            Width = totalWidth;
            Height = totalHeight;
            FrameCount = frames;
            if(frames < 1)
            {
                frames = 1;
            }
            _clipSize = (totalHeight / frames);
            ImageCSS.Clip = new rect()
            {
                Bottom = _clipSize,
                Top = 0,
                Right = totalWidth,
                Left = 0,
            };
        }
    }
}
