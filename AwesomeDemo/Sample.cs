// Authors:
//	Sebastien Pouliot  <sebastien@xamarin.com>
//
// Copyright 2012 Xamarin Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and

using System;
using System.Drawing;
using System.Reflection;

using MonoTouch.CoreGraphics;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Poupou.Awesome.Demo {

	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate {

		UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			RootElement root = new RootElement (String.Empty);
			root.Add (Populate ());
			window.RootViewController = new DialogViewController (UITableViewStyle.Plain, root);
			window.MakeKeyAndVisible ();
			return true;
		}

		// it's still fast enough for my iPad 1st gen - but it might need moving to a separate thread in the future
		static Section Populate ()
		{
			Elements e = new Elements ();
			Section s = new Section ();
			foreach (FieldInfo fi in typeof (Elements).GetFields (BindingFlags.NonPublic | BindingFlags.Instance)) {
				if (!fi.Name.StartsWith ("icon_"))
					continue;
				s.Add ((Element) fi.GetValue (e));
			}
			return s;
		}

		static void Main (string[] args)
		{
			UIApplication.Main (args, null, "AppDelegate");
		}
	}

	public partial class Elements {

		static public UIImage GetAwesomeIcon (Action<CGContext> render)
		{
			float size = 40f;
			UIGraphics.BeginImageContextWithOptions (new SizeF (size, size), false, 0.0f);
			using (var c = UIGraphics.GetCurrentContext ()) {
				c.SetFillColor (0.5f, 0.5f, 0.5f, 0.5f);
				c.SetStrokeColor (0.5f, 0.5f, 0.5f, 0.5f);
				// settings needs to be adjusted from the SVG path values, those works well for FontAwesome
				c.TranslateCTM (4f, size - 4);
				c.ScaleCTM (size / 2500, -size / 2500);
				render (c);
			}
			UIImage img = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return img;
		}
	}
}