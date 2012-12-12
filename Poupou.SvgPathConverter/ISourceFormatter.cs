// Authors:
//	Sebastien Pouliot  <sebastien@xamarin.com>
//
// Copyright 2012 Xamarin Inc.
//
// Licensed under the GNU LGPL 2 license only (no "later versions")

using System;
using System.Drawing;

namespace Poupou.SvgPathConverter {
	
	public interface ISourceFormatter {
	
		void Prologue (string name);
		void Epilogue ();
	
		void MoveTo (PointF pt);
		void LineTo (PointF pt);
		void QuadCurveTo (PointF pt1, PointF pt2);
		void CurveTo (PointF pt1, PointF pt2, PointF pt3);
		void ArcTo (PointF size, float angle, bool isLarge, bool sweep, PointF ep, PointF sp);
		void ClosePath ();
	}
}