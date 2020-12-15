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
	/// 表单域类型可选值字符串常量列表
	/// </summary>
	public sealed class StringConstInputType
	{
		/// <summary>
		/// 常量 "button"
		/// </summary>
		public const string Button			= "button";
		/// <summary>
		/// 常量 "radio"
		/// </summary>
		public const string Radio			= "radio";
		/// <summary>
		/// 常量 "checkbox"
		/// </summary>
		public const string CheckBox		= "checkbox";
		/// <summary>
		/// 常量 "file"
		/// </summary>
		public const string File			= "file";
		/// <summary>
		/// 常量 "text"
		/// </summary>
		public const string Text			= "text";
		/// <summary>
		/// 常量 "password"
		/// </summary>
		public const string Password		= "password";
		/// <summary>
		/// 常量 "image"
		/// </summary>
		public const string Image			= "image";
		/// <summary>
		/// 常量 "hidden"
		/// </summary>
		public const string Hidden			= "hidden";
		
		private StringConstInputType(){}
	}//public sealed class StringConstInputType
}