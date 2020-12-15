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
	/// 图片对齐方式字符串常量列表,应用于 img 标签的 align 属性
	/// </summary>
	public sealed class StringConstImgAlign
	{
		/// <summary>
		/// 默认值
		/// </summary>
		public const string DefaultValue= "left";

		public const string absbottom	= "absbottom";
		public const string absmiddle	= "absmiddle";
		public const string baseline	= "baseline";
		public const string bottom		= "bottom";
		public const string left		= "left";
		public const string middle		= "middle";
		public const string right		= "right";
		public const string texttop		= "texttop";
		public const string top			= "top";

		private StringConstImgAlign(){}
	}//public sealed class StringConstImgAlign
}