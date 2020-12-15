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
	/// Wrap属性可选值字符串常量列表
	/// </summary>
	public sealed class StringConstWrap
	{
		/// <summary>
		/// 常量 "soft"
		/// </summary>
		public const string Soft			= "soft";
		/// <summary>
		/// 常量 "hard"
		/// </summary>
		public const string Hard			= "hard";
		/// <summary>
		/// 常量 "off"
		/// </summary>
		public const string Off				= "off";

		private StringConstWrap(){}
	}//public sealed class StringConstWrap
}