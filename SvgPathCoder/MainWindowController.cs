// Authors:
//	Sebastien Pouliot  <sebastien@xamarin.com>
//
// Copyright 2012 Xamarin Inc.
//
// Licensed under the GNU LGPL 2 license only (no "later versions")

using System;
using System.IO;
using MonoMac.Foundation;
using MonoMac.AppKit;

using Poupou.SvgPathConverter;

namespace SvgPathCoder {

	public partial class MainWindowController : NSWindowController {

		const string BugReport = "https://github.com/spouliot/svgpath2code/issues";

		#region Constructors
		
		// Called when created from unmanaged code
		public MainWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public MainWindowController () : base ("MainWindow")
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}
		
		#endregion
		
		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}

		SvgPathParser parser;

		ISourceFormatter GetFormatter (TextWriter tw)
		{
			switch (ConversionSelector.IndexOfSelectedItem) {
			case 0:
			default:
				return new CSharpCoreGraphicsFormatter (tw);
			}
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			SourceCodeOutput.Editable = false;

			parser = new SvgPathParser ();

			Convert.Activated += (object sender, EventArgs e) => {
				using (var tw = new StringWriter ()) {
					parser.Formatter = GetFormatter (tw);
					try {
						parser.Parse (SvgPathInput.StringValue ?? String.Empty, "Unnamed");
						SourceCodeOutput.Value = tw.ToString ();
					}
					catch {
						SourceCodeOutput.Value = "Invalid path data. If this looks like a valid path then please " +
							"file an issue on github: " + Environment.NewLine + BugReport + Environment.NewLine +
							"and include the offending SVG path: " + Environment.NewLine + SvgPathInput.StringValue;
					}
				}
			};
		}
	}
}