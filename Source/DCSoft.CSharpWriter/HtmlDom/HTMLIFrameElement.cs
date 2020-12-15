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
	/// 内嵌的框架对象
	/// </summary>
	public class HTMLIFrameElement : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回"iframe"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.IFrame ;}
		}
		/// <summary>
		/// Sets or retrieves a value that indicates the table alignment. 
		/// </summary>
		public string Align
		{
			get{ return GetAttribute( StringConstAttributeName.Align );}
			set{ SetAttribute( StringConstAttributeName.Align , value);}
		}
		/// <summary>
		/// Sets or retrieves whether the object can be transparent.
		/// </summary>
		public string AllowTransparency
		{
			get{ return GetAttribute( StringConstAttributeName.AllowTransparency );}
			set{ SetAttribute( StringConstAttributeName.AllowTransparency , value);}
		}
		/// <summary>
		/// Sets or retrieves whether to display a border for the frame.
		/// </summary>
		public string FrameBorder
		{
			get{ return GetAttribute( StringConstAttributeName.FrameBorder );}
			set{ SetAttribute( StringConstAttributeName.FrameBorder , value);}
		}
		/// <summary>
		/// Sets or retrieves the height of the object. 
		/// </summary>
		public string Height
		{
			get{ return GetAttribute( StringConstAttributeName.Height );}
			set{ SetAttribute( StringConstAttributeName.Height , value);}
		}
		/// <summary>
		/// Sets or retrieves the horizontal margin for the object. 
		/// </summary>
		public string HSpace
		{
			get{ return GetAttribute( StringConstAttributeName.HSpace );}
			set{ SetAttribute( StringConstAttributeName.HSpace , value);}
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
		/// Sets or retrieves the frame name.
		/// </summary>
		public string Name
		{
			get{ return GetAttribute( StringConstAttributeName.Name );}
			set{ SetAttribute( StringConstAttributeName.Name , value);}
		}
		/// <summary>
		/// Sets or retrieves whether the frame can be scrolled.
		/// </summary>
		public string Scrolling
		{
			get{ return GetAttribute( StringConstAttributeName.Scrolling );}
			set{ SetAttribute( StringConstAttributeName.Scrolling , value);}
		}
		/// <summary>
		/// Sets or retrieves a URL to be loaded by the object.
		/// </summary>
		public string Src
		{
			get{ return GetAttribute( StringConstAttributeName.Src );}
			set{ SetAttribute( StringConstAttributeName.Src , value);}
		}
		/// <summary>
		/// Sets or retrieves the type of timeline associated with an element.
		/// </summary>
		public string TimeContainer
		{
			get{ return GetAttribute( StringConstAttributeName.TimeContainer );}
			set{ SetAttribute( StringConstAttributeName.TimeContainer ,value );}
		}
		/// <summary>
		/// Sets or retrieves advisory information (a ToolTip) for the object. 
		/// </summary>
		public string Title
		{
			get{ return GetAttribute( StringConstAttributeName.Title );}
			set{ SetAttribute ( StringConstAttributeName.Title , value);}
		}
		/// <summary>
		/// Sets or retrieves the vertical margin for the object. 
		/// </summary>
		public string VSpace
		{
			get{ return GetAttribute( StringConstAttributeName.VSpace );}
			set{ SetAttribute( StringConstAttributeName.VSpace , value);}
		}
		/// <summary>
		/// Sets or retrieves the width of the object.
		/// </summary>
		public string Width
		{
			get{ return GetAttribute( StringConstAttributeName.Width );}
			set{ SetAttribute( StringConstAttributeName.Width , value);}
		}
	}//public class HTMLIFrameElement : HTMLElement
}