/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;

namespace DCSoft.HtmlDom
{
	/// <summary>
	/// HTML样式BorderStyle属性可选值字符串常量列表
	/// </summary>
	public sealed class StringConstBorderStyle
	{
		/// <summary>
		/// Default. No border is drawn, regardless of any specified borderWidth . 
		/// </summary>
		public const string none   		= "none"; 
		/// <summary>
		/// Border is a dotted line. This value is supported on the Macintosh platform, as of Microsoft? Internet Explorer 4.01, and on the Microsoft Windows? platform, as of Internet Explorer 5.5. It renders as a solid line on Unix platforms, and on Windows systems running earlier versions of Internet Explorer. 
		/// </summary>
		public const string dotted 		= "dotted"; 
		/// <summary>
		/// Border is a dashed line. This value is supported on the Macintosh platform as of Internet Explorer 4.01 and on the Windows platform, as of Internet Explorer 5.5. It renders as a solid line on Unix platforms, and on Windows systems running earlier versions of Internet Explorer. 
		/// </summary>
		public const string dashed 		= "dashed"; 
		/// <summary>
		/// Border is a solid line. 
		/// </summary>
		public const string solid  		= "solid"; 
		/// <summary>
		/// Border is a double line drawn on top of the background of the object. The sum of the two single lines and the space between equals the borderWidth value. The border width must be at least 3 pixels wide to draw a double border. 
		/// </summary>
		public const string _double 		= "double"; 
		/// <summary>
		/// 3-D groove is drawn in colors based on the value. The borderWidth property of the object must be specified for the style to render correctly. 
		/// </summary>
		public const string groove 		= "groove"; 
		/// <summary>
		/// 3-D ridge is drawn in colors based on the value. 
		/// </summary>
		public const string ridge  		= "ridge"; 
		/// <summary>
		/// 3-D inset is drawn in colors based on the value. 
		/// </summary>
		public const string inset       = "inset"; 
		/// <summary>
		/// Border is the same as inset, but is surrounded by an additional single line, drawn in colors based on the value. 
		/// </summary>
		public const string window_inset= "window-inset"; 
		/// <summary>
		///  3-D outset is drawn in colors based on the value. 
		/// </summary>
		public const string outset      = "outset";

		private StringConstBorderStyle(){}
	}
}//public sealed class HTMLBorderStyleStringConst