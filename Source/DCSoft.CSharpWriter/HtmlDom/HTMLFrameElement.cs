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
	/// 框架对象
	/// </summary>
	public class HTMLFrameElement : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回"frame"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Frame ;}
		}
		/// <summary>
		/// Sets or retrieves the border color of the object.
		/// </summary>
		public string BorderColor
		{
			get{ return GetAttribute( StringConstAttributeName.BorderColor );}
			set{ SetAttribute( StringConstAttributeName.BorderColor , value);}
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
		/// Retrieves the height of the object. 
		/// </summary>
		public string Height
		{
			get{ return GetAttribute( StringConstAttributeName.Height );}
			set{ SetAttribute( StringConstAttributeName.Height , value);}
		}
		/// <summary>
		/// Sets or retrieves the value indicating whether the object visibly indicates that it has focus.
		/// </summary>
		public string HideFocus
		{
			get{ return GetAttribute( StringConstAttributeName.HideFocus );}
			set{ SetAttribute( StringConstAttributeName.HideFocus , value);}
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
		/// 对象名称
		/// </summary>
		public string Name
		{
			get{ return GetAttribute( StringConstAttributeName.Name );}
			set{ SetAttribute( StringConstAttributeName.Name , value);}
		}
		/// <summary>
		/// Sets or retrieves whether the user can resize the frame.
		/// </summary>
		public string NoResize
		{
			get{ return GetAttribute( StringConstAttributeName.NoResize );}
			set{ SetAttribute( StringConstAttributeName.NoResize , value);}
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
		/// Sets or retrieves advisory information (a ToolTip) for the object. 
		/// </summary>
		public string Title
		{
			get{ return GetAttribute( StringConstAttributeName.Title );}
			set{ SetAttribute( StringConstAttributeName.Title , value);}
		}
		/// <summary>
		/// Retrieves the width of the object.
		/// </summary>
		public string Width
		{
			get{ return GetAttribute( StringConstAttributeName.Width );}
			set{ SetAttribute( StringConstAttributeName.Width , value);}
		}
	}//public class HTMLFrameElement : HTMLElement
}