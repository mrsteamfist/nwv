using System;
using Android.Webkit;
using NativeWebView.API;
using Android.Content;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NativeWebView
{
	public class WebUserControl : WebView, IWebView
	{
		JavascripManager _mgr;

		public WebUserControl (Context context) : base(context)
		{
			Settings.JavaScriptEnabled = true;
			SetWebChromeClient(new WebChromeClient());
			_mgr = new JavascripManager (context);
			_mgr.notifyEvent += Notified;
		}

		#region IWebView implementation

		public event TypedEventHandler<IWebView, Action> UiTask;
		public event TypedEventHandler<IWebView, string> ReceivedMsg;

		public void OnUiTask (Action a)
		{
			if (UiTask != null)
				UiTask (this, a);
		}

		public void SetPage (string html)
		{
			LoadData(html, "text/html", "UTF-8");
		}

		public void SendJavaScript (string json)
		{
			LoadUrl("javascript:"+json);
		}

		public System.Threading.Tasks.Task<byte[]> GetImageData (string path)
		{
			return Task.Factory.StartNew<byte[]> (() => {
				using (var stream = new System.IO.FileStream (path, FileMode.Open)) {
					var reader = new BinaryReader (stream);
					var bytes = new List<byte>();
					var data = reader.ReadBytes(1024);
					while(data != null && bytes.Count > 0)
					{
						bytes.AddRange(data);
					}
					return bytes.ToArray();
				}
			});
			
		}

		#endregion
		void Notified(object sender, string e)
		{
			if (ReceivedMsg != null)
				ReceivedMsg(this, e);
		}
	}
}

