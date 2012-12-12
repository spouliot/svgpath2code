// Authors:
//	Sebastien Pouliot  <sebastien@xamarin.com>
//
// Copyright 2012 Xamarin Inc.
//
// Licensed under the GNU LGPL 2 license only (no "later versions")

using System;
using System.Collections.Generic;
using System.IO;
using Poupou.SvgPathConverter;

// This sample shows how you can use the library to converts every SVG path inside FontAwesome into a MonoTouch.Dialog
// based application to show them all. Since MonoTouch uses C# and iOS is CoreGraphics based then the parameters (and
// the extra code generation) are hardcoded inside the sample.

class Program {

	static void Usage (string error, params string[] values)
	{
		Console.WriteLine ("Usage: convert-font-awesome <font-directory> [generated-file.cs]");
		if (error != null)
			Console.WriteLine (error, values);
		Environment.Exit (1);
	}

	public static int Main (string[] args)
	{
		if (args.Length < 1)
			Usage ("error: Path to FontAwesome directory required");

		string font_dir = args [0];
		string css_file = Path.Combine (font_dir, "css/font-awesome.css");
		if (!File.Exists (css_file))
			Usage ("error: Missing '{0}' file.", css_file);

		string svg_file = Path.Combine (font_dir, "font/fontawesome-webfont.svg");
		if (!File.Exists (svg_file))
			Usage ("error: Missing '{0}' file.", svg_file);

		TextWriter writer = (args.Length < 2) ? Console.Out : new StreamWriter (args [1]);
		writer.WriteLine ("// note: Generated file - do not modify - use convert-font-awesome to regenerate");
		writer.WriteLine ();
		writer.WriteLine ("using MonoTouch.CoreGraphics;");
		writer.WriteLine ("using MonoTouch.Dialog;");
		writer.WriteLine ("using MonoTouch.Foundation;");
		writer.WriteLine ("using MonoTouch.UIKit;");
		writer.WriteLine ();
		writer.WriteLine ("namespace Poupou.Awesome.Demo {");
		writer.WriteLine ();
		writer.WriteLine ("\t[Preserve]");
		writer.WriteLine ("\tpublic partial class Elements {");

		Dictionary<string,string> names = new Dictionary<string,string> ();
		foreach (string line in File.ReadLines (css_file)) {
			if (!line.StartsWith (".icon-", StringComparison.Ordinal))
				continue;
			int p = line.IndexOf (':');
			string name = line.Substring (1, p - 1).Replace ('-', '_');
			p = line.IndexOf ("content: \"\\", StringComparison.Ordinal);
			if (p == -1)
				continue;
			string value = line.Substring (p + 11, 4);
			writer.WriteLine ("\t\t// {0} : {1}", name, value);
			writer.WriteLine ("\t\tImageStringElement {0}_element = new ImageStringElement (\"{0}\", GetAwesomeIcon ({0}));", name);
			writer.WriteLine ();
			names.Add (value, name);
		}
		writer.WriteLine ("\t\t// total: {0}", names.Count);
		writer.WriteLine ();

		// MonoTouch uses C# and CoreGraphics
		var code = new CSharpCoreGraphicsFormatter (writer);
		var parser = new SvgPathParser () {
			Formatter = code
		};

		foreach (string line in File.ReadLines (svg_file)) {
			if (!line.StartsWith ("<glyph unicode=\"&#x", StringComparison.Ordinal))
				continue;
			string id = line.Substring (19, 4);
			string name;
			if (!names.TryGetValue (id, out name))
				continue;
			int p = line.IndexOf (" d=\"") + 4;
			int e = line.LastIndexOf ('"');
			string data = line.Substring (p, e - p);
			parser.Parse (data, name);
		}
		writer.WriteLine ("\t}");
		writer.WriteLine ("}");
		writer.Close ();

		return 0;
	}
}