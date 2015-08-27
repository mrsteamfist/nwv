using System;
using Windows.Graphics.Display;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HadriansWall
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Rules rules = new Rules();
        UserControl webControl;
        public MainPage()
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            this.InitializeComponent();
            rules.ReadyEvent += rules_ReadyEvent;
            webControl = rules.WebControl;
            webControl.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            root.Children.Add(webControl);
        }
        void rules_ReadyEvent(object sender, EventArgs e)
        {
            webControl.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }
    }
}
