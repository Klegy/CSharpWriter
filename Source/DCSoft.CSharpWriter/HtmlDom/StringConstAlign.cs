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
	/// 对齐方式可选值字符串常量列表
	/// </summary>
	public sealed class StringConstAlign
	{
		/// <summary>
		/// 常量 "center"
		/// </summary>
		public const string Center			= "center";
		/// <summary>
		/// 常量 "justify"
		/// </summary>
		public const string Justify			= "justify";
		/// <summary>
		/// 常量 "left"
		/// </summary>
		public const string Left			= "left";
		/// <summary>
		/// 常量 "right"
		/// </summary>
		public const string Right			= "right";

		private StringConstAlign(){}
	}//public sealed class StringConstAlign
}