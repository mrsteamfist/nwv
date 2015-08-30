using System;
using Android.Content;
using Android.Runtime;
using Android.Webkit;
using Java.Interop;

namespace NativeWebView
{
	public class JavascripManager : Java.Lang.Object
	{
		Context context;
		public EventHandler<string> notifyEvent;

		public JavascripManager(Context context)
		{
			this.context = context;
		}

		public JavascripManager(IntPtr handle, JniHandleOwnership transfer)
			: base(handle, transfer)
		{
		}
		[Export]
		[JavascriptInterface]
		// to become consistent with Java/JS interop convention, the argument cannot be System.String.
		public void notify(Java.Lang.String message)
		{
			if (notifyEvent != null)
				notifyEvent(this, message.ToString());
		}
	}
}

