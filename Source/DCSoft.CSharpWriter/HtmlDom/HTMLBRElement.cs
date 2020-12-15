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
	/// 换行元素
	/// </summary>
	public class HTMLBRElement : HTMLElement
	{
		/// <summary>
		/// 对象标签名称, 返回 "br"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Br ; }
		}
		/// <summary>
		/// 对象表示的文本,返回一个换行回车符
		/// </summary>
		public override string InnerText
		{
			get
			{
				return System.Environment.NewLine ;
			}
		}
	}//public class HTMLBRElement : HTMLElement
}