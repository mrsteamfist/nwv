using NativeWebView.API;
using System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NativeWebView
{
    ///<summary>
    /// Custom browser to display webview code
    ///</summary>
    public sealed partial class WebUserControl : UserControl, IWebView
    {
        public WebUserControl()
        {
            this.InitializeComponent();
        }

        public event TypedEventHandler<IWebView, Action> UiTask;

        public void OnUiTask(Action a)
        {
            if (UiTask != null && a != null)
                UiTask(this, a);
        }

        public event TypedEventHandler<IWebView, string> ReceivedMsg;

        public async void SendJavaScript(string json)
        {
            await Browser.InvokeScriptAsync(WebViewConstants.JSON_SCRIPT, new String[] { json });
        }

        public void SetPage(string html)
        {
            Browser.NavigateToString(html);
        }

        private void Brower_ScriptNotify(object sender, NotifyEventArgs e)
        {
            if (ReceivedMsg != null)
                ReceivedMsg(this, e.Value);
        }

        private async Task<byte[]> GetImage(Uri image)
        {
            try
            {
                var file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(image);
                using (var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    var reader = new DataReader(stream.GetInputStreamAt(0));
                    var bytes = new byte[stream.Size];
                    await reader.LoadAsync((uint)stream.Size);
                    reader.ReadBytes(bytes);
                    return bytes;
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<byte[]> GetImageData(String path)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            var imagePath = String.Format("ms-appx://{0}", path);
            var image = await GetImage(new Uri(imagePath));
            return image;

        }
    }
}
