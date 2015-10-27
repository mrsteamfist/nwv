using System;
using Android.Webkit;
using NativeWebView.API;
using Android.Content;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Android.Runtime;
using Android.Util;
using System.Text;
using System.Threading;

namespace NativeWebView
{
	public class WebUserControl : WebView, IWebView
	{
		JavascripManager _mgr;
		TaskScheduler _baseThread;
		bool loaded = false;

		public WebUserControl (Context context) : base(context)
		{
			Init (context);
		}

		protected WebUserControl (IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
			Init (null);
		}

		public WebUserControl (Context context, IAttributeSet attrs)
			: base(context, attrs, 0)
		{
			Init (context);
		}

		public WebUserControl (Context context, IAttributeSet attrs, int defStyle)
			: base(context, attrs, defStyle)
		{
			Init (context);
		}

		public WebUserControl (Context context, IAttributeSet attrs, int defStyleAttr, bool privateBrowsing)
			: base(context, attrs, defStyleAttr, privateBrowsing)
		{

		}

		public WebUserControl (Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
			: base(context, attrs, defStyleAttr, defStyleRes)
		{
			Init (context);
		}
		protected void Init(Context context = null)
		{
			_baseThread = TaskScheduler.FromCurrentSynchronizationContext();
			if (context != null) {
				_mgr = new JavascripManager (context);
				_mgr.notifyEvent += Notified;
			}
			Settings.JavaScriptEnabled = true;
			SetWebChromeClient(new WebChromeClient());
			AddJavascriptInterface (_mgr, "External");
		}
		private string _pagePath = null;

		protected override void OnSizeChanged (int w, int h, int oldw, int oldh)
		{
			base.OnSizeChanged (w, h, oldw, oldh);
			if (!loaded) {
				loaded = true;
				if (ControlLoaded != null) {
					ControlLoaded (this, EventArgs.Empty);
				}
			}

		}
		#region IWebView implementation

		public event TypedEventHandler<IWebView, Action> UiTask;
		public event TypedEventHandler<IWebView, string> ReceivedMsg;
		public event EventHandler ControlLoaded;

		public void OnUiTask (Action a)
		{
			if (UiTask != null)
				UiTask (this, a);
		}

		public void SetPage (string html)
		{
			Task.Factory.StartNew (() => {
			if (_pagePath == null) {
				_pagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "index.html");
				if (File.Exists (_pagePath))
					File.Delete (_pagePath);
				using (var file = File.Create (_pagePath)) {
					var writer = new StreamWriter (file);
					writer.Write (html);
				}
				LoadUrl ("file://" + _pagePath);
			} else
				LoadDataWithBaseURL (_pagePath, html, "text/html", "UTF-8", _pagePath);
			}, CancellationToken.None, TaskCreationOptions.AttachedToParent, _baseThread);
		}

		public void SendJavaScript (string json)
		{
			Task.Factory.StartNew (() => {
				LoadUrl (String.Format("javascript:{0}(\'{1}\')", WebViewConstants.JSON_SCRIPT, json));
			}, CancellationToken.None, TaskCreationOptions.AttachedToParent, _baseThread);
		}

		public System.Threading.Tasks.Task<String> GetImageData (string path, string format)
		{
			return Task.Factory.StartNew<String> (() => {
				using (var stream = Context.Assets.Open(path.Substring(8))) {
					var reader = new BinaryReader (stream);
					var bytes = new List<byte>();
					var data = reader.ReadBytes(1024);
					while(data != null && data.Length > 0)
					{
						bytes.AddRange(data);
						data = reader.ReadBytes(1024);
					}
					//return ;
					var stringBuilder = new StringBuilder("data:image/");
					stringBuilder.AppendFormat("{0};base64,", format);
					stringBuilder.Append(Convert.ToBase64String(bytes.ToArray()));
					return stringBuilder.ToString();
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

