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
	/// 脚本语言类型可选值字符串常量列表
	/// </summary>
	public sealed class StringConstScriptLanguage
	{
		public const string JScript		= "jscript";
		public const string JavaScript	= "javascript";
		public const string VBS			= "vbs";
		public const string VBScript	= "vbscript";
		public const string XML			= "xml";

		private StringConstScriptLanguage(){}
	}//public sealed class StringConstScriptLanguage
}