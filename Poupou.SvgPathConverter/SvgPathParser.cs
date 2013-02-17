// Authors:
//	Sebastien Pouliot  <sebastien@xamarin.com>
//
// Copyright 2012-2013 Xamarin Inc.
//
// This file is mostly based on the C++ code from once magnificent Moonlight
// https://github.com/mono/moon/blob/master/src/xaml.cpp
// Copyright 2007 Novell, Inc. (http://www.novell.com)
//
// Licensed under the GNU LGPL 2 license only (no "later versions")

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace Poupou.SvgPathConverter {

	public class SvgPathParser {

		static int i;

		public ISourceFormatter Formatter { get; set; }

		public void Parse (string svgPath, string name = null)
		{
			if (Formatter == null)
				throw new InvalidOperationException ("Missing formatter");

			if (name == null)
				name = "Unnamed_" + (++i).ToString ();

			Parse (svgPath, name, Formatter);
		}

		static void Advance (string s, ref int pos)
		{
			if (pos >= s.Length)
				return;
			char c = s [pos];
			while (!Char.IsLetterOrDigit (c) && c != '.' && c!= '-' && c != '+') {
				if (++pos == s.Length)
					return;
				c = s [pos];
			}
		}
		
		static int FindNonFloat (string s, int pos)
		{
			char c = s [pos];
			while ((Char.IsNumber (c) || c == '.' || c == '-' || c == '+')) {
				if (++pos == s.Length)
					return pos;
				c = s [pos];
			}
			return pos;
		}
		
		static bool MorePointsAvailable (string s, int pos)
		{
			if (pos >= s.Length)
				return false;
			char c = s [pos];
			while (Char.IsWhiteSpace (c) || c == ',')
				c = s [++pos];
			return Char.IsDigit (c) || c == '.' || c == '-' || c == '+';
		}
		
		static float GetFloat (string svg, ref int pos)
		{
			int end = FindNonFloat (svg, pos);
			string s = svg.Substring (pos, end - pos);
			float f = Single.Parse (s, CultureInfo.InvariantCulture);
			pos = end;
			return f;
		}
		
		static PointF GetPoint (string svg, ref int pos)
		{
			while (Char.IsWhiteSpace (svg [pos]))
				pos++;
			float x = GetFloat (svg, ref pos);
			
			while (Char.IsWhiteSpace (svg [pos]))
				pos++;
			if (svg [pos] == ',')
				pos++;
			while (Char.IsWhiteSpace (svg [pos]))
				pos++;
			
			float y = GetFloat (svg, ref pos);
			
			return new PointF (x, y);
		}
		
		static PointF MakeRelative (PointF c, PointF m)
		{
			return new PointF (m.X + c.X, m.Y + c.Y);
		}

		static void Parse (string svg, string name, ISourceFormatter formatter)
		{
			formatter.Prologue (name);
			
			PointF start;
			PointF cp = new PointF (0, 0);
			PointF cp1, cp2, cp3;
			PointF qbzp, cbzp;
			int fill_rule = 0;
			int pos = 0;
			bool cbz = false;
			bool qbz = false;
			while (pos < svg.Length) {
				char c = svg [pos++];
				if (Char.IsWhiteSpace (c))
					continue;
				
				bool relative = false;
				switch (c) {
				case 'f':
				case 'F':
					c = svg [pos++];
					if (c == '0')
						fill_rule = 0;
					else if (c == '1')
						fill_rule = 1;
					else
						throw new FormatException ();
					break;
				case 'h':
					relative = true;
					goto case 'H';
				case 'H':
					float x = GetFloat (svg, ref pos);
					if (relative)
						x += cp.X;
					cp = new PointF (x, cp.Y);
					formatter.LineTo (cp);
					cbz = qbz = false;
					break;
				case 'm':
					relative = true;
					goto case 'M';
				case 'M':
					cp1 = GetPoint (svg, ref pos);
					if (relative)
						cp1 = MakeRelative (cp, cp1);
					formatter.MoveTo (cp1);
					
					start = cp = cp1;
					
					Advance (svg, ref pos);
					while (MorePointsAvailable (svg, pos)) {
						cp1 = GetPoint (svg, ref pos);
						if (relative)
							cp1 = MakeRelative (cp, cp1);
						formatter.LineTo (cp1);
					}
					cp = cp1;
					cbz = qbz = false;
					break;
				case 'l':
					relative = true;
					goto case 'L';
				case 'L':
					while (MorePointsAvailable (svg, pos)) {
						cp1 = GetPoint (svg, ref pos);
						if (relative)
							cp1 = MakeRelative (cp, cp1);
						Advance (svg, ref pos);
						
						formatter.LineTo (cp1);
						cp = cp1;
					}
					cbz = qbz = false;
					break;
				case 'a':
					relative = true;
					goto case 'A';
				case 'A':
					while (MorePointsAvailable (svg, pos)) {
						cp1 = GetPoint (svg, ref pos);
						// this is a width and height so it's not made relative to cp
						Advance (svg, ref pos);

						float angle = GetFloat (svg, ref pos);
						Advance (svg, ref pos);

						bool is_large = GetFloat (svg, ref pos) != 0.0f;
						Advance (svg, ref pos);

						bool positive_sweep = GetFloat (svg, ref pos) != 0.0f;
						Advance (svg, ref pos);

						cp2 = GetPoint (svg, ref pos);
						if (relative)
							cp2 = MakeRelative (cp, cp2);
						Advance (svg, ref pos);
						
						formatter.ArcTo (cp1, angle, is_large, positive_sweep, cp2, cp);
						
						cp = cp2;
						Advance (svg, ref pos);
					}
					qbz = false;
					cbz = false;
					break;
				case 'q':
					relative = true;
					goto case 'Q';
				case 'Q':
					while (MorePointsAvailable (svg, pos)) {
						cp1 = GetPoint (svg, ref pos);
						if (relative)
							cp1 = MakeRelative (cp, cp1);
						Advance (svg, ref pos);
						
						cp2 = GetPoint (svg, ref pos);
						if (relative)
							cp2 = MakeRelative (cp, cp2);
						Advance (svg, ref pos);
						
						formatter.QuadCurveTo (cp1, cp2);
						
						cp = cp2;
						Advance (svg, ref pos);
					}
					qbz = true;
					qbzp = cp1;
					cbz = false;
					break;
				case 'c':
					relative = true;
					goto case 'C';
				case 'C':
					while (MorePointsAvailable (svg, pos)) {
						cp1 = GetPoint (svg, ref pos);
						if (relative)
							cp1 = MakeRelative (cp, cp1);
						Advance (svg, ref pos);
						
						cp2 = GetPoint (svg, ref pos);
						if (relative)
							cp2 = MakeRelative (cp, cp2);
						Advance (svg, ref pos);
						
						cp3 = GetPoint (svg, ref pos);
						if (relative)
							cp3 = MakeRelative (cp, cp3);
						Advance (svg, ref pos);

						formatter.CurveTo (cp1, cp2, cp3);
						
						cp1 = cp3;
					}
					cp = cp3;
					cbz = true;
					cbzp = cp2;
					qbz = false;
					break;
				case 't':
					relative = true;
					goto case 'T';
				case 'T':
					while (MorePointsAvailable (svg, pos)) {
						cp2 = GetPoint (svg, ref pos);
						if (relative)
							cp2 = MakeRelative (cp, cp2);
						if (qbz) {
							cp1.X = 2 * cp.X - qbzp.X;
							cp1.Y = 2 * cp.Y - qbzp.Y;
						} else {
							cp1 = cp;
						}
						formatter.QuadCurveTo (cp1, cp2);
						qbz = true;
						qbzp = cp1;
						cp = cp2;
						Advance (svg, ref pos);
					}
					cbz = false;
					break;
				case 's':
					relative = true;
					goto case 'S';
				case 'S':
					while (MorePointsAvailable (svg, pos)) {
						cp2 = GetPoint (svg, ref pos);
						if (relative)
							cp2 = MakeRelative (cp, cp2);
						Advance (svg, ref pos);

						cp3 = GetPoint (svg, ref pos);
						if (relative)
							cp3 = MakeRelative (cp, cp3);

						if (cbz) {
							cp1.X = 2 * cp.X - cbzp.X;
							cp1.Y = 2 * cp.Y - cbzp.Y;
						} else {
							cp1 = cp;
						}
						formatter.CurveTo (cp1, cp2, cp3);
						cbz = true;
						cbzp = cp2;
						cp = cp3;
						Advance (svg, ref pos);
					}
					qbz = false;
					break;
				case 'v':
					relative = true;
					goto case 'V';
				case 'V':
					float y = GetFloat (svg, ref pos);
					if (relative)
						y += cp.Y;
					cp = new PointF (cp.X, y);
					formatter.LineTo (cp);
					cbz = qbz = false;
					break;
				case 'z':
				case 'Z':
					formatter.ClosePath ();
					formatter.MoveTo (start);
					cp = start;
					cbz = qbz = false;
					break;
				default:
					throw new FormatException (c.ToString ());
				}
			}
			formatter.Epilogue ();
		}
	}
}