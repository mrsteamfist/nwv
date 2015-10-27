using NativeWebView;
using NativeWebView.HTML.CSS.Attributes;
using NativeWebView.HTML.DOM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace HadriansWall
{
    public class Rules
    {
        #region Variables
        private const ushort VILLAGE_LOCATION = 0;
        private const ushort FARM_LOCATION = 1;
        private const ushort QUARRY_LOCATION = 2;
        private const ushort GARRISON_LOCATION = 3;
        private const ushort OUTLANDS_LOCATION = 4;

        public WebUserControl WebControl
        { get; private set; }

        ContentView _contentView;

        private SpriteElement[] EvilMinions = new SpriteElement[OUTLANDS_LOCATION + 1];
        private SpriteElement[] Villagers = new SpriteElement[OUTLANDS_LOCATION + 1];
        private ImageElement[] RightButtons = new ImageElement[OUTLANDS_LOCATION + 1];
        private ImageElement[] LeftButtons = new ImageElement[OUTLANDS_LOCATION + 1];

        private SpriteElement wall;
        public ImageElement HelpScreen;
        public ImageElement LoseScreen;
        public ImageElement WinScreen;

        private Random _random = new Random();

        public event EventHandler ReadyEvent;

        private const string CSS = @"body {
    margin:0px;
    overflow:hidden;
    background:black;
}";
        #endregion
        public Rules()
        {
            WebControl = new WebUserControl();
            WebControl.ControlLoaded += _webControl_Loaded;
            _contentView = new ContentView(WebControl, new NativeWebView.HTML.CSS.CSSDocument(CSS));
        }
        #region Call Backs
        async void _webControl_Loaded(object sender, EventArgs e)
        {
            var width = Math.Max(Window.Current.Bounds.Width, Window.Current.Bounds.Height);
            var height = Math.Min(Window.Current.Bounds.Width, Window.Current.Bounds.Height);
#if WINDOWS_PHONE_APP
            var ui = await GenerateUi((int)(width/2), (int)(height/2));
#else
            var ui = await GenerateUi((int)(width), (int)(height));
#endif
            _contentView.InitializeDisplay(ui.ToArray());
            _contentView.PageLoaded += _contentView_PageLoaded;
            _contentView.DisplayEventFired += _contentView_DisplayEventFired;
        }
        void _contentView_PageLoaded(ContentView sender)
        {
            if (ReadyEvent != null)
                ReadyEvent(this, EventArgs.Empty);
            for (int i = 0; i < OUTLANDS_LOCATION; i++)
            {
                if (i > 0)
                    _contentView.RegisterEvent(LeftButtons[i], NativeWebView.HTML.Base.HtmlEventType.click);
                if (i < OUTLANDS_LOCATION - 1)
                    _contentView.RegisterEvent(RightButtons[i], NativeWebView.HTML.Base.HtmlEventType.click);
                _contentView.RegisterEvent(Villagers[i], NativeWebView.HTML.Base.HtmlEventType.click);
            }
            _contentView.RegisterEvent(WinScreen, NativeWebView.HTML.Base.HtmlEventType.click);
            _contentView.RegisterEvent(LoseScreen, NativeWebView.HTML.Base.HtmlEventType.click);
            _contentView.RegisterEvent(HelpScreen, NativeWebView.HTML.Base.HtmlEventType.click);
            Start();
        }
        void _contentView_DisplayEventFired(DisplayElement sender, NativeWebView.HTML.Base.HtmlEventType e)
        {
            if (sender == WinScreen || sender == LoseScreen)
            {
                Start();
            }
            else if (sender == HelpScreen)
            {
                HelpScreen.Style.Display = Display.none;
            }
            else
            {
                #region Villager clicked
                var index = System.Array.IndexOf(Villagers, sender);
                if (index > -1)
                {
                    switch (index)
                    {
                        case VILLAGE_LOCATION:
                            _remove_villager(VILLAGE_LOCATION);
                            _add_villager(GARRISON_LOCATION, 1);
                            break;
                        case FARM_LOCATION:
                            _remove_villager(FARM_LOCATION);
                            _add_villager(VILLAGE_LOCATION, 2);
                            break;
                        case QUARRY_LOCATION:
                            _remove_villager(QUARRY_LOCATION);
                            if (wall.Style.Display == Display.none)
                            {
                                wall.Style.Display = Display.block;
                                wall.CurrentFrame = 0;
                            }
                            else
                            {
                                wall.CurrentFrame++;
                                if (wall.CurrentFrame == wall.FrameCount - 1)
                                {
                                    WinScreen.Style.Display = Display.block;
                                    return;
                                }
                            }
                            break;
                        case GARRISON_LOCATION:
                            _remove_villager(GARRISON_LOCATION);
                            _remove_baddie(OUTLANDS_LOCATION, 2);
                            break;
                        default:
                            return;
                    }
                    //Do Ai Action
                    PerformAIAction();
                    _add_villager(VILLAGE_LOCATION, 1);
                }
                #endregion
                #region Left Click
                index = System.Array.IndexOf(LeftButtons, sender);
                if (index > -1)
                {
                    switch (index)
                    {
                        case FARM_LOCATION:
                            _remove_villager(FARM_LOCATION);
                            _add_villager(VILLAGE_LOCATION);
                            break;
                        case QUARRY_LOCATION:
                            _remove_villager(QUARRY_LOCATION);
                            _add_villager(FARM_LOCATION);
                            break;
                        case GARRISON_LOCATION:
                            _remove_villager(GARRISON_LOCATION);
                            _add_villager(QUARRY_LOCATION);
                            break;
                        default:
                            return;
                    }
                    //Do Ai Action
                    PerformAIAction();
                    _add_villager(VILLAGE_LOCATION, 1);
                }
                #endregion
                #region Right Click
                index = System.Array.IndexOf(RightButtons, sender);
                if (index > -1)
                {
                    switch (index)
                    {
                        case VILLAGE_LOCATION:
                            _remove_villager(VILLAGE_LOCATION);
                            _add_villager(FARM_LOCATION);
                            break;
                        case FARM_LOCATION:
                            _remove_villager(FARM_LOCATION);
                            _add_villager(QUARRY_LOCATION);
                            break;
                        case QUARRY_LOCATION:
                            _remove_villager(QUARRY_LOCATION);
                            _add_villager(GARRISON_LOCATION);
                            break;
                        default:
                            return;
                    }
                    //Do Ai Action
                    PerformAIAction();
                    _add_villager(VILLAGE_LOCATION, 1);
                }
                #endregion
            }
        }
        #endregion
        public async Task<IEnumerable<DisplayElement>> GenerateUi(float width, float height)
        {
            try
            {
                var heightDiff = height / 1080f;
                var widthDiff = width / 1920f;
                var replies = new List<ImageElement>();
                var image = await WebControl.GetImageData("/Assets/board.jpg", "jpg");
                var background = new ImageElement(image);
                background.Width = (int)width;
                background.Height = (int)height;
                background.Style.Left = 0;
                background.Style.Top = 0;
                background.Style.ZIndex = -1;
                background.Style.Position = NativeWebView.HTML.CSS.Attributes.ElementPositions.position_absolute;
                replies.Add(background);
                image = await WebControl.GetImageData("/Assets/howtoplay.png", "png");
                HelpScreen = new ImageElement(image);
                HelpScreen.Width = (int)width;
                HelpScreen.Height = (int)height;
                HelpScreen.Style.Left = 0;
                HelpScreen.Style.Top = 0;
                HelpScreen.Style.ZIndex = 99;
                HelpScreen.Style.Display = Display.none;
                HelpScreen.Style.Position = NativeWebView.HTML.CSS.Attributes.ElementPositions.position_absolute;
                replies.Add(HelpScreen);
                image = await WebControl.GetImageData("/Assets/gameover-win.png", "png");
                WinScreen = new ImageElement(".png", image);
                WinScreen.Width = (int)width;
                WinScreen.Height = (int)height;
                WinScreen.Style.Left = 0;
                WinScreen.Style.Top = 0;
                WinScreen.Style.ZIndex = 98;
                WinScreen.Style.Display = Display.none;
                WinScreen.Style.Position = NativeWebView.HTML.CSS.Attributes.ElementPositions.position_absolute;
                replies.Add(WinScreen);
                image = await WebControl.GetImageData("/Assets/gameover-lose.png", "png");
                LoseScreen = new ImageElement(".png", image);
                LoseScreen.Width = (int)width;
                LoseScreen.Height = (int)height;
                LoseScreen.Style.Left = 0;
                LoseScreen.Style.Top = 0;
                LoseScreen.Style.ZIndex = 98;
                LoseScreen.Style.Display = Display.none;
                LoseScreen.Style.Position = NativeWebView.HTML.CSS.Attributes.ElementPositions.position_absolute;
                replies.Add(LoseScreen);

                image = await WebControl.GetImageData("/Assets/wall.png", "png");
                var nibble = 60 * widthDiff;
                wall = GenerateItem(image, (int)(width / 2), (int)(170 * heightDiff), (int)(width - (nibble * 2)), (int)(990 * heightDiff), 6);//new SpriteElement(".png", image, 6, 1800, 690);
                replies.Add(wall);

                int buttonWidth = (int)(50 * widthDiff);
                if (buttonWidth < 20)
                    buttonWidth = 20;
                int villagerWidth = (int)(150 * widthDiff);
                int villagerHeight = (int)(3960 * heightDiff);

                var leftbutton = await WebControl.GetImageData("/Assets/button_left.png", "png");
                var rightbutton = await WebControl.GetImageData("/Assets/button_right.png", "png");
                var blue = await WebControl.GetImageData("/Assets/bluemeeples.png", "png");
                var red = await WebControl.GetImageData("/Assets/redmeeples.png", "png");
                var left = background.Width / 5;
                var top = background.Height / 2;
                var buttonTop = top + villagerHeight / 40;
                var center = left / 2;
                for (int i = 0; i < OUTLANDS_LOCATION; i++)
                {
                    if (i > 0)
                    {
                        LeftButtons[i] = GenerateItem(leftbutton, center - villagerWidth, buttonTop, buttonWidth, buttonWidth);
                        replies.Add(LeftButtons[i]);
                    }
                    if (i < OUTLANDS_LOCATION - 1)
                    {
                        RightButtons[i] = GenerateItem(rightbutton, center + villagerWidth, buttonTop, buttonWidth, buttonWidth);
                        replies.Add(RightButtons[i]);
                    }
                    Villagers[i] = GenerateItem(blue, center, top, villagerWidth, villagerHeight, 20);
                    replies.Add(Villagers[i]);
                    EvilMinions[i] = GenerateItem(red, center, top, villagerWidth, villagerHeight, 20);
                    replies.Add(EvilMinions[i]);
                    center += left;
                }
                EvilMinions[OUTLANDS_LOCATION] = GenerateItem(red, center, top, villagerWidth, villagerHeight, 20);
                replies.Add(EvilMinions[OUTLANDS_LOCATION]);
                return replies;
            }
            catch (System.Exception e)
            {
                //ToDo: log it
                Windows.UI.Popups.MessageDialog tmp = new Windows.UI.Popups.MessageDialog(e.Message);
                tmp.ShowAsync();
            }
            return new List<DisplayElement>();
        }
        #region Action Helpers
        void _remove_villager(int location, int count = 1)
        {
            if (count > 0 && location >= 0 && location < Villagers.Length - 1)
            {
                if (Villagers[location] != null && Villagers[location].Style.Display != Display.none)
                {
                    if (Villagers[location].CurrentFrame >= count)
                    {
                        Villagers[location].CurrentFrame -= count;
                    }
                    else
                    {
                        Villagers[location].CurrentFrame = 0;
                        Villagers[location].Style.Display = Display.none;
                        if (LeftButtons[location] != null)
                            LeftButtons[location].Style.Display = Display.none;
                        if (RightButtons[location] != null)
                            RightButtons[location].Style.Display = Display.none;
                    }
                }
            }
        }

        void _remove_baddie(int location, int count = 1)
        {
            if (count > 0 && location >= 0 && location < EvilMinions.Length)
            {
                if (EvilMinions[location] != null && EvilMinions[location].Style.Display != Display.none)
                {
                    if (EvilMinions[location].CurrentFrame >= count)
                    {
                        EvilMinions[location].CurrentFrame -= count;
                    }
                    else
                    {
                        EvilMinions[location].CurrentFrame = 0;
                        EvilMinions[location].Style.Display = Display.none;
                    }
                }
            }
        }

        void _add_villager(int location, int count = 1)
        {
            if (count > 0 && location >= 0 && location < Villagers.Length - 1)
            {
                if (EvilMinions[location] != null && EvilMinions[location].Style.Display != Display.none)
                {
                    var diff = count - EvilMinions[location].CurrentFrame + 1;
                    if (diff >= 0)
                    {
                        EvilMinions[location].CurrentFrame = 0;
                        EvilMinions[location].Style.Display = Display.none;
                        if (diff > 0)
                        {
                            _add_villager(location, diff);
                        }
                    }
                    else if (diff < 0)
                    {
                        EvilMinions[location].CurrentFrame -= count;
                    }
                }
                else if (Villagers[location].Style.Display == Display.none)
                {
                    Villagers[location].Style.Display = Display.block;
                    Villagers[location].CurrentFrame = count - 1;
                    if (LeftButtons[location] != null)
                        LeftButtons[location].Style.Display = Display.block;
                    if (RightButtons[location] != null)
                        RightButtons[location].Style.Display = Display.block;
                }
                else
                {
                    Villagers[location].CurrentFrame += count;
                }
            }
        }

        void _add_baddie(int location, int count = 1)
        {
            if (count > 0 && location >= 0 && location < EvilMinions.Length)
            {
                if (Villagers[location] != null && Villagers[location].Style.Display != Display.none)
                {
                    var diff = count - Villagers[location].CurrentFrame + 1;
                    if (diff >= 0)
                    {
                        Villagers[location].CurrentFrame = 0;
                        Villagers[location].Style.Display = Display.none;
                        if (LeftButtons[location] != null)
                            LeftButtons[location].Style.Display = Display.none;
                        if (RightButtons[location] != null)
                            RightButtons[location].Style.Display = Display.none;
                        if (diff > 0)
                        {
                            _add_baddie(location, diff);
                        }
                    }
                    else if (diff < 0)
                    {
                        Villagers[location].CurrentFrame -= count;
                    }
                }
                else if (EvilMinions[location].Style.Display == Display.none)
                {
                    EvilMinions[location].Style.Display = Display.block;
                    EvilMinions[location].CurrentFrame = count - 1;
                }
                else
                {
                    EvilMinions[location].CurrentFrame += count;
                }
            }
        }
        #endregion
        #region Method
        public void Start()
        {
            for (int i = 0; i < Villagers.Length - 1; i++)
            {
                Villagers[i].CurrentFrame = 0;
                Villagers[i].Style.Display = Display.none;
                EvilMinions[i].CurrentFrame = 0;
                EvilMinions[i].Style.Display = Display.none;
                if (LeftButtons[i] != null)
                    LeftButtons[i].Style.Display = Display.none;
                if (RightButtons[i] != null)
                    RightButtons[i].Style.Display = Display.none;
            }
            EvilMinions[EvilMinions.Length - 1].CurrentFrame = 0;
            EvilMinions[EvilMinions.Length - 1].Style.Display = Display.none;
            wall.Style.Display = Display.none;
            _add_villager(GARRISON_LOCATION, 3);
            _add_villager(VILLAGE_LOCATION, 1);
            WinScreen.Style.Display = Display.none;
            LoseScreen.Style.Display = Display.none;
            HelpScreen.Style.Display = Display.block;
        }
        public void PerformAIAction()
        {
            var add = _random.Next(2) > 0;
            var rng = _random.Next(EvilMinions.Length);
            var diff = wall.CurrentFrame + rng - 2;
            if (diff > 0)
            {
                if (add)
                    _add_baddie(OUTLANDS_LOCATION);
                else if (EvilMinions[OUTLANDS_LOCATION].CurrentFrame > diff)
                {
                    for (int i = 1; i < EvilMinions.Length; i++)
                    {
                        if (EvilMinions[i].Style.Display != Display.none)
                        {
                            _add_baddie(i - 1);
                            if (i - 1 == VILLAGE_LOCATION)
                            {
                                LoseScreen.Style.Display = Display.block;
                            }
                        }
                    }
                }
            }
        }
        ImageElement GenerateItem(string src, int center, int top, int width, int height)
        {
            var image = new ImageElement(src);
            image.Width = width;
            image.Height = height;
            image.Style.Position = NativeWebView.HTML.CSS.Attributes.ElementPositions.position_absolute;
            image.Style.Display = Display.none;
            image.Style.Left = center - (width / 2);
            image.Style.Top = top;
            return image;
        }
        SpriteElement GenerateItem(string src, int center, int top, int width, int height, int frames)
        {
            var image = new SpriteElement(src, frames, width, height);
            image.Style.Position = NativeWebView.HTML.CSS.Attributes.ElementPositions.position_absolute;
            image.Style.Display = Display.none;
            image.Style.Left = center - (width / 2);
            image.Style.Top = top;
            return image;
        }
        #endregion
    }
}
