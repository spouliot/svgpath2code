SvgPath2Code
============

This is a C# library to ease converting SVG paths into source code.

There's only a single backend (initially) that produce C# code targeting CoreGraphics, i.e. for MonoTouch and MonoMac, but it's fully extensible and provide helpers (e.g. to create SVG arcs from bezier curves).

A few tools are provided:

* convert-font-awesome : will convert FontAwesome SVG paths into a (large) C# file;

* svgpath2code : a command line tool that accept the SVG path data and output source code;

A sample application (useful for screenshots ;-) is also included in the AwesomeDemo directory.


License
=======

The samples, like the library, is licensed under the 
[Apache License, Version 2.0](http://www.apache.org/licenses/LICENSE-2.0)

Note that the source code that is produced by this tool falls under the license of the original SVG path.


Screenshots
===========

![MonoTouch Sample](https://raw.github.com/spouliot/svgpath2code/master/awesome-app.png "MonoTouch Sample Application")
