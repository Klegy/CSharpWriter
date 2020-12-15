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
	/// 带名称的样式表元素名称
	/// </summary>
	public class HTMLNameStyleItem : HTMLStyle 
	{
		/// <summary>
		/// 元素名称
		/// </summary>
		protected string strName = ".";
		/// <summary>
		/// 元素名称
		/// </summary>
		public string Name
		{
			get{ return strName ;}
			set{ strName = value;}
		}
	}//public class HTMLNameStyleItem : HTMLStyle 
}