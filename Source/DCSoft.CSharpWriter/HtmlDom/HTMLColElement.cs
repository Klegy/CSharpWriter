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
	/// 表格列对象(Specifies column-based defaults for the table properties.)
	/// </summary>
	public class HTMLColElement : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回"col"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Col ; }
		}
		/// <summary>
		/// Sets or retrieves the alignment of the object relative to the display or table. 
		/// </summary>
		public string Align
		{
			get{ return GetAttribute( StringConstAttributeName.Align );}
			set{ SetAttribute( StringConstAttributeName.Align , value);}
		}
		/// <summary>
		/// Sets the background color behind the object. 
		/// </summary>
		public string BgColor
		{
			get{ return GetAttribute( StringConstAttributeName.BgColor );}
			set{ SetAttribute( StringConstAttributeName.BgColor , value);}
		}
		/// <summary>
		/// Sets or retrieves the number of columns in the group. 
		/// </summary>
		public string Span
		{
			get{ return GetAttribute( StringConstAttributeName.Span );}
			set{ SetAttribute( StringConstAttributeName.Span , value);}
		}
		/// <summary>
		/// Sets or retrieves how text and other content are vertically aligned within the object that contains them. 
		/// </summary>
		public string VAlign
		{
			get{ return GetAttribute( StringConstAttributeName.VAlign );}
			set{ SetAttribute( StringConstAttributeName.VAlign , value);}
		}
		/// <summary>
		/// Sets or retrieves the width of the object.
		/// </summary>
		public string Width
		{
			get{ return GetAttribute( StringConstAttributeName.Width );}
			set{ SetAttribute( StringConstAttributeName.Width , value);}
		}
	}//public class HTMLColElement : HTMLElement
}