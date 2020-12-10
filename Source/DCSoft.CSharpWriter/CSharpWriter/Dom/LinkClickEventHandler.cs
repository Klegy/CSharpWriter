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
	/// 文档外部链接点击事件
	/// </summary>
	public delegate void LinkClickEventHandler( object Sender , LinkClickEventArgs args );
	/// <summary>
	/// 文档链接点击事件参数类型
	/// </summary>
	public class LinkClickEventArgs : EventArgs
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="doc">文档对象</param>
		/// <param name="e">元素</param>
		/// <param name="link">链接目标</param>
		public LinkClickEventArgs( DomDocument doc , DomElement e , string link )
		{
			myDocument = doc ;
			myElement = e ;
			strLink = link ;
		}
		private DomDocument myDocument = null;
		/// <summary>
		/// 文档对象
		/// </summary>
		public DomDocument Document
		{
			get{ return myDocument ;}
		}

		private DomElement myElement = null;
		/// <summary>
		/// 触发链接的文档元素对象
		/// </summary>
		public DomElement Element
		{
			get{ return myElement ;}
		}

		private string strLink = null;
		/// <summary>
		/// 链接目标地址
		/// </summary>
		public string Link
		{
			get{ return strLink ;}
		}
	}
}