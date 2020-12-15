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
	/// target属性可选值字符串常量列表
	/// </summary>
	public sealed class StringConstTarget
	{
		/// <summary>
		/// 新页面 "_blank"
		/// </summary>
		public const string _Blank			= "_blank";
		/// <summary>
		/// 媒体页面 "_media"
		/// </summary>
		public const string _Media			= "_media";
		/// <summary>
		/// 父框架 "_parent"
		/// </summary>
		public const string _Parent			= "_parent";
		/// <summary>
		/// 搜索页面 "_search"
		/// </summary>
		public const string _Search			= "_search";
		/// <summary>
		/// 当前框架 "_self"
		/// </summary>
		public const string _Self			= "_self";
		/// <summary>
		/// 顶框架 "_top"
		/// </summary>
		public const string _Top			= "_top";

		private StringConstTarget()
		{
		}
	}//public sealed class HTMLTargetConst
}