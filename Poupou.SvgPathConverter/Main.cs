using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using SvgPath2code;

class Program {

	public static void Main (string[] args)
	{
		var code = new CSharpCoreGraphicsFormatter ();
#if false
		// icon-search
		string search = "M0 437q0 65 24.5 122t67 99.5t99.5 67t122 24.5q64 0 121 -24.5t99.5 -67t67 -99.5t24.5 -122q0 -48 -13.5 -91t-38.5 -81l168 -167q9 -10 9 -23t-9 -22l-44 -44q-9 -9 -22 -9t-22 9l-168 168q-38 -25 -81 -38.5t-91 -13.5q-65 0 -122 24.5t-99.5 67t-67 99t-24.5 121.5z M125 437q0 -39 14.5 -73t40 -59.5t60 -40t73.5 -14.5t73 14.5t59.5 40t40 59.5t14.5 73t-14.5 73t-40 59.5t-59.5 40.5t-73 15t-73.5 -15t-60 -40.5t-40 -59.5t-14.5 -73zM194 437q0 25 9.5 46.5t25.5 37.5t37.5 25.5t46.5 9.5q10 0 16.5 -7t6.5 -17t-6.5 -16.5t-16.5 -6.5 q-30 0 -51 -21t-21 -51q0 -10 -6.5 -16.5t-16.5 -6.5t-17 6.5t-7 16.5z";
		Parse ("search", search, code);
		// icon-home f015
		string home = "M1 384.5q3 11.5 13 19.5l412 338q11 8 24 8t24 -8l126 -104v58q0 19 19 19h112q19 0 19 -19v-180l136 -112q10 -8 13 -19.5t-1 -22.5q-10 -24 -36 -24h-75v-300q0 -16 -10.5 -27t-26.5 -11h-206v225h-188v-225h-206q-16 0 -27 11t-11 27v300h-75q-25 0 -35 24 q-4 11 -1 22.5z";
		Parse ("home", home, code);
		// icon-star f005
		string star = "M0.5 465q4.5 13 25.5 16l238 35l106 215q10 20 23.5 20t22.5 -20l107 -215l237 -35q22 -3 26 -16t-11 -28l-172 -168l40 -236q4 -22 -7 -30t-30 3l-213 111l-212 -111q-20 -11 -31 -3t-7 30l41 236l-172 168q-16 15 -11.5 28z";
		Parse ("star", star, code);
		// icon-list f03a
		string list = "M0 38v56q0 15 11 26t27 11h75q15 0 26 -11t11 -26v-56q0 -16 -11 -27t-26 -11h-75q-16 0 -27 11t-11 27zM0 244v56q0 16 11 27t27 11h75q15 0 26 -11t11 -27v-56q0 -16 -11 -27t-26 -11h-75q-16 0 -27 11t-11 27zM0 450v56q0 16 11 27t27 11h75q15 0 26 -11t11 -27v-56 q0 -16 -11 -26.5t-26 -10.5h-75q-16 0 -27 10.5t-11 26.5zM0 656v57q0 15 11 26t27 11h75q15 0 26 -11t11 -26v-57q0 -15 -11 -26t-26 -11h-75q-16 0 -27 11t-11 26zM225 38v56q0 15 11 26t27 11h600q15 0 26 -11t11 -26v-56q0 -16 -11 -27t-26 -11h-600q-16 0 -27 11 t-11 27zM225 244v56q0 16 11 27t27 11h600q15 0 26 -11t11 -27v-56q0 -16 -11 -27t-26 -11h-600q-16 0 -27 11t-11 27zM225 450v56q0 16 11 27t27 11h600q15 0 26 -11t11 -27v-56q0 -16 -11 -26.5t-26 -10.5h-600q-16 0 -27 10.5t-11 26.5zM225 656v57q0 15 11 26t27 11h600 q15 0 26 -11t11 -26v-57q0 -15 -11 -26t-26 -11h-600q-16 0 -27 11t-11 26z";
		Parse ("list", list, code);
		// icon-cogs f085
		string cogs = "M0 391v84q0 6 5 6q14 4 29.5 6t30.5 4q4 0 7 0.5t7 0.5q6 21 17 42q-9 14 -20 28t-23 28q-5 5 0 9q14 17 30 33.5t33 30.5q6 4 9 -1q8 -8 17 -14.5t18 -13.5l21 -15q21 11 42 17q2 21 4.5 39t6.5 35q0 5 6 5h84q7 0 7 -6q2 -14 4.5 -28.5t4.5 -29.5l2 -15q20 -6 41 -17 q8 7 19 14q10 8 19.5 15t18.5 15q6 4 9 -1q5 -4 9 -8l8 -8l22 -22q12 -12 23 -25q3 -5 0 -9q-10 -11 -20 -24.5t-23 -30.5q6 -11 10.5 -22t8.5 -22q8 -2 17.5 -3t19.5 -3l18 -2q9 -1 17 -3q6 -2 6 -7v-84q0 -5 -5 -7q-14 -3 -29.5 -5t-30.5 -4q-4 0 -14 -2q-6 -20 -17 -41 q9 -14 20 -28t23 -28q4 -5 0 -9q-14 -17 -30 -33.5t-33 -30.5q-6 -4 -9 1q-8 7 -17 14t-18 13q-5 5 -10.5 8.5t-10.5 7.5q-21 -11 -42 -17q-2 -17 -4 -36.5t-7 -37.5q-2 -5 -7 -5h-84q-6 0 -6 5q-3 14 -5 29l-4 30l-2 15q-20 6 -41 17q-5 -4 -9.5 -7t-9.5 -7 q-10 -8 -19.5 -15t-18.5 -15q-6 -4 -9 1q-5 4 -8 8l-9 8q-11 11 -22.5 22t-22.5 25q-4 4 0 8q12 14 22.5 28.5t19.5 27.5q-10 20 -18 44q-8 2 -17.5 3t-19.5 3l-18 2q-9 1 -18 3q-5 2 -5 7zM197 432q0 -35 25 -60t60 -25t60 25t25 60t-25 60t-60 25t-60 -25t-25 -60z M524 188q-2 6 4 8q11 4 21 8t21 8q1 5 1.5 9t2.5 9t3.5 8.5t3.5 8.5q-7 10 -13 19.5t-12 19.5q-3 5 2 8l62 56q4 4 9 1q9 -7 17.5 -14t17.5 -15q18 7 35 8q5 11 10.5 21t10.5 19q3 5 8 3l80 -25q5 -2 5 -8q-2 -11 -4 -21.5t-4 -21.5q8 -6 14 -13t11 -15q12 1 23 1.5t22 0.5 q5 0 7 -5l18 -83q2 -5 -4 -7q-11 -5 -21 -8.5t-21 -7.5q-1 -5 -1.5 -9t-2.5 -9t-3.5 -8.5t-3.5 -7.5q7 -10 13.5 -19.5t11.5 -19.5q2 -5 -2 -8l-62 -57q-4 -4 -9 0q-8 7 -17 14t-17 14q-20 -7 -37 -8q-5 -11 -10 -21t-10 -19q-3 -5 -8 -3l-80 25q-5 2 -5 8q2 11 3.5 22 t3.5 22q-14 12 -24 27q-12 -2 -23.5 -2.5t-22.5 0.5q-5 0 -7 5zM560 607q0 5 5 7q10 2 20 4.5t20 4.5q2 4 3 8t3 8t4.5 7t4.5 7q-5 10 -9 19.5t-8 18.5q-2 4 2 8l64 42q5 3 8 -1q8 -7 14.5 -14.5t13.5 -15.5q16 3 33 3q12 18 24 33q3 3 8 2l69 -34q5 -3 3 -8 q-2 -10 -5.5 -19t-6.5 -19q10 -13 18 -29q11 -1 21.5 -1.5t20.5 -2.5q5 -2 5 -6l5 -77q0 -4 -5 -6q-10 -2 -19.5 -4.5t-20.5 -4.5q-2 -4 -3 -7.5t-3 -7.5q-3 -7 -8 -14q5 -10 9 -19.5t8 -18.5q2 -5 -3 -8l-63 -42q-5 -3 -8 1q-13 12 -28 30q-8 -2 -16.5 -3t-17.5 0 q-6 -9 -12 -17.5t-12 -16.5q-3 -3 -8 -1l-69 34q-5 2 -3 7q3 10 6 19.5t7 19.5q-6 6 -10.5 13t-8.5 15q-11 1 -21.5 1.5t-20.5 2.5q-5 0 -5 6zM658 203q-7 -22 3.5 -42.5t33.5 -27.5q22 -8 42.5 2.5t27.5 33.5q8 22 -2.5 42.5t-33.5 28.5q-22 7 -42.5 -3.5t-28.5 -33.5z M681 564q7 -20 26 -30q20 -9 40 -2.5t29 25.5q10 20 3 40t-26 29q-19 10 -39 3t-30 -26t-3 -39z";
		Parse ("cogs", cogs, code);
		// icon-exclamation-sign f06a
		string exclamation_sign = "M0 375q0 78 29.5 146t80.5 119t119 80.5t146 29.5t146 -29.5t119 -80.5t80.5 -119t29.5 -146t-29.5 -146t-80.5 -119t-119 -80.5t-146 -29.5t-146 29.5t-119 80.5t-80.5 119t-29.5 146zM316 613l6 -347q2 -14 15 -14h76q14 0 14 14l7 347q1 5 -4 10q-3 4 -10 4h-90 q-7 0 -10 -4q-4 -4 -4 -10zM319 125q0 -14 14 -14h85q5 0 9.5 4t4.5 10v82q0 6 -4.5 10t-9.5 4h-85q-14 0 -14 -14v-82z";
		Parse ("exclamation_sign", exclamation_sign, code);
#else
		Dictionary<string,string> names = new Dictionary<string,string> ();
		foreach (string line in File.ReadLines ("/Users/poupou/Downloads/FortAwesome-Font-Awesome-ee55c85/css/font-awesome.css")) {
			if (!line.StartsWith (".icon-", StringComparison.Ordinal))
				continue;
			int p = line.IndexOf (':');
			string name = line.Substring (1, p - 1).Replace ('-', '_');
			p = line.IndexOf ("content: \"\\", StringComparison.Ordinal);
			if (p == -1)
				continue;
			string value = line.Substring (p + 11, 4);
			Console.WriteLine ("// {0} : {1}", name, value);
			Console.WriteLine ("ImageStringElement {0}_element = new ImageStringElement (\"{0}\", GetAwesomeIcon ({0}));", name);
			names.Add (value, name);
		}
		Console.WriteLine ();
		Console.WriteLine ("// total: {0}", names.Count);
		Console.WriteLine ();
	
		foreach (string line in File.ReadLines ("/Users/poupou/Downloads/FortAwesome-Font-Awesome-ee55c85/font/fontawesome-webfont.svg")) {
			if (!line.StartsWith ("<glyph unicode=\"&#x", StringComparison.Ordinal))
				continue;
			string id = line.Substring (19, 4);
			string name;
			if (!names.TryGetValue (id, out name))
				continue;
			int p = line.IndexOf (" d=\"") + 4;
			int e = line.LastIndexOf ('"');
			string data = line.Substring (p, e - p);
			new PathParser () { Formatter = code }.Parse (data, name);
			Console.WriteLine ();
		}
#endif
	}
}