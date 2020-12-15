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
	/// WordWrap属性可选值字符串常量列表
	/// </summary>
	public sealed class StringConstWordWrap
	{
		/// <summary>
		/// 常量 "normal"
		/// </summary>
		public const string Normal			= "normal";
		/// <summary>
		/// 常量 "break_word"
		/// </summary>
		public const string Break_Word		= "break-word";

		private StringConstWordWrap(){}
	}//public sealed class StringConstWordWrap
}