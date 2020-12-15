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
	/// Creates a container element for document content in a print or print preview template.
	/// </summary>
	public class HTMLIELayoutrect : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回"ie:layoutrect"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.IELayoutrect ;}
		}
		internal override bool CheckParent(HTMLContainer vParent)
		{
			return vParent is HTMLIEDeviceRect ;
		}
		/// <summary>
		/// 边框
		/// </summary>
		public string Border
		{
			get{ return GetAttribute( StringConstAttributeName.Border );}
			set{ SetAttribute( StringConstAttributeName.Border , value);}
		}
		/// <summary>
		/// 边框颜色
		/// </summary>
		public string BorderColor
		{
			get{ return GetAttribute( StringConstAttributeName.BorderColor );}
			set{ SetAttribute( StringConstAttributeName.BorderColor , value);}
		}
		/// <summary>
		/// 文档来源
		/// </summary>
		public string ContentSrc
		{
			get{ return GetAttribute( StringConstAttributeName.ContentSrc );}
			set{ SetAttribute( StringConstAttributeName.ContentSrc , value);}
		}
		/// <summary>
		/// Sets or retrieves the top and bottom margin heights before displaying the text in a frame.
		/// </summary>
		public string MarginHeight
		{
			get{ return GetAttribute( StringConstAttributeName.MarginHeight );}
			set{ SetAttribute( StringConstAttributeName.MarginHeight , value);}
		}
		/// <summary>
		/// Sets or retrieves the left and right margin widths before displaying the text in a frame. 
		/// </summary>
		public string MarginWidth
		{
			get{ return GetAttribute( StringConstAttributeName.MarginWidth );}
			set{ SetAttribute( StringConstAttributeName.MarginWidth , value);}
		}
		/// <summary>
		/// Sets or retrieves the identifier of the next LayoutRect element used to format the document.
		/// </summary>
		public string NextRect
		{
			get{ return GetAttribute( StringConstAttributeName.NextRect );}
			set{ SetAttribute( StringConstAttributeName.NextRect , value);}
		}
	}
}