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
	/// Provides a print template with access to page setup and printer settings and control over print jobs initiated from the template.
	/// </summary>
	public class HTMLIETemplateprinter : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回"ie:templateprinter"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.IETemplatePrinter ;}
		}
		/// <summary>
		/// Sets or retrieves whether all documents linked to in the current document are printed with the current print job.
		/// </summary>
		public string AllLinkedDocuments
		{
			get{ return GetAttribute( StringConstAttributeName.AllLinkedDocuments );}
			set{ SetAttribute( StringConstAttributeName.AllLinkedDocuments , value);}
		}
		/// <summary>
		/// Sets or retrieves whether the pages of a document are collated when printed.
		/// </summary>
		public string Collate
		{
			get{ return GetAttribute( StringConstAttributeName.Collate );}
			set{ SetAttribute( StringConstAttributeName.Collate , value);}
		}
		/// <summary>
		/// Sets or retrieves how many copies of the document to print.
		/// </summary>
		public string Copies
		{
			get{ return GetAttribute( StringConstAttributeName.Copies );}
			set{ SetAttribute( StringConstAttributeName.Copies , value);}
		}
		/// <summary>
		/// Sets or retrieves the footer formatting string from the Page Setup dialog box.
		/// </summary>
		public string Footer
		{
			get{ return GetAttribute( StringConstAttributeName.Footer );}
			set{ SetAttribute( StringConstAttributeName.Footer , value);}
		}
		/// <summary>
		/// Sets or retrieves a value that indicates whether the option button labeled Only the selected frame in the Print frames section of the Print dialog box is selected.
		/// </summary>
		public string FrameActive
		{
			get{ return GetAttribute( StringConstAttributeName.FrameActive );}
			set{ SetAttribute( StringConstAttributeName.FrameActive , value);}
		}
		/// <summary>
		/// Sets or retrieves whether the document's frames are printed exactly as they appear on the screen.
		/// </summary>
		public string FrameAsShown
		{
			get{ return GetAttribute( StringConstAttributeName.FrameAsShown );}
			set{ SetAttribute( StringConstAttributeName.FrameAsShown , value);}
		}
		/// <summary>
		/// Sets or retrieves whether all the documents in the frameset are printed.
		/// </summary>
		public string FramesetDocument
		{
			get{ return GetAttribute( StringConstAttributeName.FrameSetDocument );}
			set{ SetAttribute( StringConstAttributeName.FrameSetDocument , value);}
		}
		/// <summary>
		/// Sets or retrieves the header formatting string from the Page Setup dialog box.
		/// </summary>
		public string Header
		{
			get{ return GetAttribute( StringConstAttributeName.Header );}
			set{ SetAttribute( StringConstAttributeName.Header , value);}
		}
		/// <summary>
		/// Sets or retrieves the bottom margin of the document to be printed.
		/// </summary>
		public string MarginBottom
		{
			get{ return GetAttribute( StringConstAttributeName.MarginBottom );}
			set{ SetAttribute( StringConstAttributeName.MarginBottom , value);}
		}
		/// <summary>
		/// Sets or retrieves the left margin of the document to be printed.
		/// </summary>
		public string MarginLeft
		{
			get{ return GetAttribute( StringConstAttributeName.MarginLeft );}
			set{ SetAttribute( StringConstAttributeName.MarginLeft , value);}
		}
		/// <summary>
		/// Sets or retrieves the top margin of the document to be printed.
		/// </summary>
		public string MarginTop
		{
			get{ return GetAttribute( StringConstAttributeName.MarginTop );}
			set{ SetAttribute( StringConstAttributeName.MarginTop , value);}
		}
		/// <summary>
		/// Sets or retrieves the right margin of the document to be printed.
		/// </summary>
		public string MarginRight
		{
			get{ return GetAttribute( StringConstAttributeName.MarginRight );}
			set{ SetAttribute( StringConstAttributeName.MarginRight , value);}
		}
		/// <summary>
		/// Sets or retrieves the first page in the document to be printed.
		/// </summary>
		public string PageFrom
		{
			get{ return GetAttribute( StringConstAttributeName.PageFrom );}
			set{ SetAttribute( StringConstAttributeName.PageFrom , value);}
		}
		/// <summary>
		/// Retrieves the current height of a page on the printer.
		/// </summary>
		public string PageHeight
		{
			get{ return GetAttribute( StringConstAttributeName.PageHeight );}
			set{ SetAttribute( StringConstAttributeName.PageHeight , value);}
		}
		/// <summary>
		/// Sets or retrieves the last page in the document to be printed.
		/// </summary>
		public string PageTo
		{
			get{ return GetAttribute( StringConstAttributeName.PageTo );}
			set{ SetAttribute( StringConstAttributeName.PageTo , value);}
		}
		/// <summary>
		/// Retrieves the current width of a page on the printer.
		/// </summary>
		public string PageWidth
		{
			get{ return GetAttribute(StringConstAttributeName.PageWidth );}
			set{ SetAttribute( StringConstAttributeName.PageWidth , value);}
		}
		/// <summary>
		/// Sets or retrieves whether a selected range of pages is printed, rather than the whole document.
		/// </summary>
		public string SelectedPages
		{
			get{ return GetAttribute( StringConstAttributeName.SelectedPages );}
			set{ SetAttribute( StringConstAttributeName.SelectedPages , value);}
		}
		/// <summary>
		/// Sets or retrieves whether the selected portion of the document is the only part to be printed.
		/// </summary>
		public string Selection
		{
			get{ return GetAttribute( StringConstAttributeName.Selection );}
			set{ SetAttribute( StringConstAttributeName.Selection ,value);}
		}
		/// <summary>
		/// Sets or retrieves whether to print a table of links as part of the current print job.
		/// </summary>
		public string TableOfLinks
		{
			get{ return GetAttribute( StringConstAttributeName.TableOfLinks );}
			set{ SetAttribute( StringConstAttributeName.TableOfLinks , value);}
		}
	}//public class HTMLIETemplateprinter : HTMLElement
}