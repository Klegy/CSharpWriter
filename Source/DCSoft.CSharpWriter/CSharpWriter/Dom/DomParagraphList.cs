/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
 
namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 段落列表对象
	/// </summary>
	public class DomParagraphList : DomContainerElement
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public DomParagraphList()
		{
		}

		/// <summary>
		/// 段落列表样式
		/// </summary>
		protected ParagraphListStyle intListStyle = ParagraphListStyle.None ;
		/// <summary>
		/// 段落列表样式
		/// </summary>
		public ParagraphListStyle ListStyle
		{
			get{ return intListStyle ;}
			set{ intListStyle = value;}
		}
		 
	}
}
