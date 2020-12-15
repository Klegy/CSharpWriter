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
	/// Provides a tool so that a print template can convert header and footer formatting strings to formatted HTML
	/// </summary>
	public class HTMLIEHeaderfooter : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回"ie:headerfooter"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.IEHeaderFooter ;}
		}
		/// <summary>
		/// Sets or retrieves the current date in long format.
		/// </summary>
		public string DateLong
		{
			get{ return GetAttribute( StringConstAttributeName.DateLong );}
			set{ SetAttribute( StringConstAttributeName.DateLong , value);}
		}
		/// <summary>
		/// Sets or retrieves the current date in short format.
		/// </summary>
		public string DateShort
		{
			get{ return GetAttribute( StringConstAttributeName.DateShort );}
			set{ SetAttribute( StringConstAttributeName.DateShort , value);}
		}
		/// <summary>
		/// Sets or retrieves the page number that the HeaderFooter behavior uses when generating HTML for headers and footers.
		/// </summary>
		public string Page
		{
			get{ return GetAttribute( StringConstAttributeName.Page );}
			set{ SetAttribute( StringConstAttributeName.Page , value);}
		}
		/// <summary>
		/// Sets or retrieves the page total that the HeaderFooter behavior uses when generating HTML for headers and footers.
		/// </summary>
		public string PageTotal
		{
			get{ return GetAttribute( StringConstAttributeName.PageTotal );}
			set{ SetAttribute( StringConstAttributeName.PageTotal , value);}
		}
		/// <summary>
		/// Sets or retrieves the control string used by the HeaderFooter behavior to generate HTML for the footer.
		/// </summary>
		public string TextFoot
		{
			get{ return GetAttribute( StringConstAttributeName.TextFoot );}
			set{ SetAttribute( StringConstAttributeName.TextFoot , value);}
		}
		/// <summary>
		/// Sets or retrieves the control string used by the HeaderFooter behavior to generate HTML for the header.
		/// </summary>
		public string TextHead
		{
			get{ return GetAttribute( StringConstAttributeName.TextHead );}
			set{ SetAttribute( StringConstAttributeName.TextHead , value);}
		}
		/// <summary>
		/// Sets or retrieves the current time in long format.
		/// </summary>
		public string TimeLong
		{
			get{ return GetAttribute( StringConstAttributeName.TimeLong );}
			set{ SetAttribute( StringConstAttributeName.TimeLong , value);}
		}
		/// <summary>
		/// Sets or retrieves the current time in short format.
		/// </summary>
		public string TimeShort
		{
			get{ return GetAttribute( StringConstAttributeName.TimeShort );}
			set{ SetAttribute( StringConstAttributeName.TimeShort , value);}
		}
		/// <summary>
		/// Sets or retrieves the title of the document currently being printed or print-previewed.
		/// </summary>
		public string Title
		{
			get{ return GetAttribute( StringConstAttributeName.Title );}
			set{ SetAttribute( StringConstAttributeName.Title , value);}
		}
		/// <summary>
		/// Sets or retrieves the URL of the document currently being printed or print-previewed.
		/// </summary>
		public string URL
		{
			get{ return GetAttribute( StringConstAttributeName.URL );}
			set{ SetAttribute( StringConstAttributeName.URL , value);}
		}
	}//public class HTMLIEHeaderfooter : HTMLElement
}