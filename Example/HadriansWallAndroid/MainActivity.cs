using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Android.OS;
using NativeWebView;
using HadriansWall;

namespace HadriansWallAndroid
{
	[Activity (Label = "HadriansWallAndroid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private Rules _rules;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var control = FindViewById<WebUserControl> (Resource.Id.webController);
			_rules = new Rules (control);
		}
	}
}

