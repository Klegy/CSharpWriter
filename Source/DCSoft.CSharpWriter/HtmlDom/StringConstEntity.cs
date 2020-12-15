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
	/// HTML实体字符串常量列表
	/// </summary>
	public sealed class StringConstEntity
	{
		/// <summary>
		/// 空格
		/// </summary>
		public const string WhiteSpace				= "&nbsp;";
		/// <summary>
		/// and 符号
		/// </summary>
		public const string Entity_And				= "&amp;";	// 实体 &
		/// <summary>
		/// 小于号
		/// </summary>
		public const string Entity_Small			= "&lt;";	// 实体 <
		/// <summary>
		/// 大于号
		/// </summary>
		public const string Entity_Big				= "&gt;";	// 实体 >
		/// <summary>
		/// 单引号
		/// </summary>
		public const string Entity_Mark				= "&apos;";	// 实体 '
		/// <summary>
		/// 双引号
		/// </summary>
		public const string Entity_DblMark			= "&quot;";	// 实体 "

		private StringConstEntity(){}
	}
}