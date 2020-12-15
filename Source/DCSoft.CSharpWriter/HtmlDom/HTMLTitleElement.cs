/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System ;
namespace DCSoft.HtmlDom
{
	/// <summary>
	/// 文档标题对象
	/// </summary>
	public class HTMLTitleElement : HTMLElement
	{
		private string strText ;
		/// <summary>
		/// 对象标签名称,返回"title"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstAttributeName.Title ;}
		}
		/// <summary>
		/// 对象文本
		/// </summary>
		public override string Text
		{
			get{ return strText ; }
			set{ strText = value; }
		}
		/// <summary>
		/// 对象值
		/// </summary>
		public override string Value
		{
			get{ return strText ; }
			set{ strText = value; }
		}
		/// <summary>
		/// 内部使用
		/// </summary>
		protected override bool HasText
		{
			get{ return true;}
		}
	}//public class HTMLTitleElement : HTMLElement
}