// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace SvgPathCoder
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSTextField SvgPathInput { get; set; }

		[Outlet]
		MonoMac.AppKit.NSPopUpButton ConversionSelector { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextView SourceCodeOutput { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton Convert { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SvgPathInput != null) {
				SvgPathInput.Dispose ();
				SvgPathInput = null;
			}

			if (ConversionSelector != null) {
				ConversionSelector.Dispose ();
				ConversionSelector = null;
			}

			if (SourceCodeOutput != null) {
				SourceCodeOutput.Dispose ();
				SourceCodeOutput = null;
			}

			if (Convert != null) {
				Convert.Dispose ();
				Convert = null;
			}
		}
	}

	[Register ("MainWindow")]
	partial class MainWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
