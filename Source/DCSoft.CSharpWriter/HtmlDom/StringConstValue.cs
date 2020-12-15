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
	/// 某些表示真假的属性的值
	/// </summary>
	public sealed class StringConstValue
	{
		/// <summary>
		/// 常量 "true"
		/// </summary>
		public const string _True			= "true";
		/// <summary>
		/// 常量 "false"
		/// </summary>
		public const string _False			= "false";
		/// <summary>
		/// 常量 "yes"
		/// </summary>
		public const string _Yes			= "yes";
		/// <summary>
		/// 常量 "no"
		/// </summary>
		public const string _No				= "no";

		private StringConstValue(){}
	}//public sealed class StringConstValue
}