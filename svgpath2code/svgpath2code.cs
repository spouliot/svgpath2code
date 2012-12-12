// Authors:
//	Sebastien Pouliot  <sebastien@xamarin.com>
//
// Copyright 2012 Xamarin Inc.
//
// Licensed under the GNU LGPL 2 license only (no "later versions")

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Options;
using Poupou.SvgPathConverter;

class Program {

	static void Usage (OptionSet os, string error, params string[] values)
	{
		Console.WriteLine ("Usage: svgpath2code -formatter=FORMATTER [-out:filename] [--name=METHODNAME] svgpath");
		if (error != null)
			Console.WriteLine (error, values);
		os.WriteOptionDescriptions (Console.Out);
		Environment.Exit (error == null ? 0 : 1);
	}
	
	public static int Main (string[] args)
	{
		TextWriter writer = Console.Out;
		string method_name = null;
		string formatter = null;
		bool show_help = false;

		var os = new OptionSet () {
			{ "formatter=", "Source code formatter. Valid values are: 'csharp-coregraphics'", v => formatter = v },
			{ "out=", "Source code output", v => writer = new StreamWriter (v) },
			{ "h|?|help", "Displays the help", v => show_help = true },
		};

		var svg = os.Parse (args);
		string path = (svg.Count > 1) ? String.Concat (svg) : svg [0]; 

		if (show_help)
			Usage (os, null);

		var parser = new SvgPathParser ();

		switch (formatter) {
		case "csharp-coregraphics":
		case "cs-cg":
			parser.Formatter = new CSharpCoreGraphicsFormatter (writer);
			break;
		default:
			Usage (os, "error: unkown {0} code formatter", formatter);
			break;
		}

		parser.Parse (path, method_name);
		return 0;
	}
}