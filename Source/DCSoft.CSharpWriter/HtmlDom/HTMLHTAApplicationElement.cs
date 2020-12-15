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
	/// HTA:Application 元素
	/// </summary>
	public class HTMLHTAApplicationElement : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回"hta:application"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.HTAApplication ;}
		}
		/// <summary>
		/// Sets or retrieves the name of the HTML Application (HTA).
		/// </summary>
		public string ApplicationName
		{
			get{ return GetAttribute( StringConstAttributeName.ApplicationName );}
			set{ SetAttribute( StringConstAttributeName.ApplicationName , value);}
		}
		/// <summary>
		/// Sets or retrieves the type of window border for the HTML Application (HTA). 
		/// </summary>
		public string Border
		{
			get{ return GetAttribute( StringConstAttributeName.Border );}
			set{ SetAttribute( StringConstAttributeName.Border , value);}
		}
		/// <summary>
		/// Sets or retrieves the style set for the content border within the HTML Application (HTA) window
		/// </summary>
		public string BorderStyle
		{
			get{ return GetAttribute( StringConstAttributeName.BorderStyle );}
			set{ SetAttribute( StringConstAttributeName.BorderStyle , value);}
		}
		/// <summary>
		/// Sets or retrieves a Boolean value that indicates whether the window is set to display a title bar or caption, for the HTML Application (HTA).
		/// </summary>
		public string Caption
		{
			get{ return GetAttribute( StringConstAttributeName.Caption );}
			set{ SetAttribute( StringConstAttributeName.Caption , value);}
		}
		/// <summary>
		/// Sets or retrieves whether the context menu is displayed when the right mouse button is clicked.
		/// </summary>
		public bool ContextMenu
		{
			get{ return GetBoolAttribute2(  StringConstAttributeName.ContextMenu , true) ; }
			set{ SetBoolAttribute2( StringConstAttributeName.ContextMenu , value ) ;}
		}
		/// <summary>
		/// Sets or retrieves the name and location of the icon specified in the HTML Application (HTA).
		/// </summary>
		public string Icon
		{
			get{ return GetAttribute( StringConstAttributeName.ICON );}
			set{ SetAttribute( StringConstAttributeName.ICON , value);}
		}
		/// <summary>
		/// Sets or retrieves whether the inside 3-D border is displayed.
		/// </summary>
		public bool InnerBorder
		{
			get{ return GetBoolAttribute2( StringConstAttributeName.InnerBorder , true );}
			set{ SetBoolAttribute2( StringConstAttributeName.InnerBorder , value ) ;}
		}
		/// <summary>
		/// Sets or retrieves a Boolean value that indicates whether a Maximize button is displayed in the title bar of the HTML Application (HTA) window.
		/// </summary>
		public bool MaximizeButton
		{
			get{ return GetBoolAttribute2( StringConstAttributeName.MaximizeButton , true );}
			set{ SetBoolAttribute2( StringConstAttributeName.MaximizeButton , value );}
		}
		/// <summary>
		/// Sets or retrieves a Boolean value that indicates whether a Miximize button is displayed in the title bar of the HTML Application (HTA) window.
		/// </summary>
		public bool MinimizeButton
		{
			get{ return GetBoolAttribute2( StringConstAttributeName.MinimizeButton  , true );}
			set{ SetBoolAttribute2( StringConstAttributeName.MinimizeButton , value) ;}
		}
		/// <summary>
		/// Sets or retrieves whether the scroll bars are displayed.
		/// yes:Default. Scroll bars are displayed. 
		/// no: Scroll bars are not displayed. 
		/// auto: Scroll bars are displayed only when the content exceeds what can fit in the client area. 
		/// </summary>
		public string Scroll
		{
			get{ return GetAttribute( StringConstAttributeName.Scroll );}
			set{ SetAttribute( StringConstAttributeName.Scroll , value);}
		}
		/// <summary>
		/// Sets or retrieves whether the scroll bar is 3-D or flat. NO for Default 
		/// </summary>
		public bool ScrollFlat
		{
			get{ return GetBoolAttribute2( StringConstAttributeName.ScrollFlat , false ) ;}
			set{ SetBoolAttribute2( StringConstAttributeName.ScrollFlat , value );}
		}
		/// <summary>
		/// Sets or retrieves whether the content can be selected with the mouse or keyboard.
		/// </summary>
		public bool Selection
		{
			get{ return GetBoolAttribute2( StringConstAttributeName.Selection , true  );}
			set{ SetBoolAttribute2( StringConstAttributeName.Selection , value ) ;}
		}
		/// <summary>
		/// Sets or retrieves a value that indicates whether the HTML Application (HTA) is displayed in the Microsoft Windows taskbar
		/// </summary>
		public bool ShowinTaskBar
		{
			get{ return GetBoolAttribute2( StringConstAttributeName.ShowInTaskBar , true);}
			set{ SetBoolAttribute2( StringConstAttributeName.ShowInTaskBar , value);}
		}
		/// <summary>
		/// Sets or retrieves a value that indicates whether only one instance of the specified HTML Application (HTA) can run at a time
		/// </summary>
		public bool SingleInstance
		{
			get{ return GetBoolAttribute2( StringConstAttributeName.SingleInstance , false);}
			set{ SetBoolAttribute2( StringConstAttributeName.SingleInstance , value);}
		}
		/// <summary>
		/// Sets or retrieves a Boolean value that indicates whether a system menu is displayed in the HTML Application (HTA).
		/// </summary>
		public bool SysMenu
		{
			get{ return GetBoolAttribute2( StringConstAttributeName.SysMenu , true);}
			set{ SetBoolAttribute2( StringConstAttributeName.SysMenu , value);}
		}
		/// <summary>
		/// Sets or retrieves the version number of the HTML Application (HTA)
		/// </summary>
		public string Version
		{
			get{ return GetAttribute( StringConstAttributeName.Version );}
			set{ SetAttribute( StringConstAttributeName.Version , value);}
		}
		/// <summary>
		/// Sets or retrieves the initial size of the HTML Application (HTA) window.
		/// </summary>
		public string WindowState
		{
			get{ return GetAttribute( StringConstAttributeName.WindowState );}
			set{ SetAttribute( StringConstAttributeName.WindowState , value );}
		}

		private void SetBoolAttribute2( string vName , bool vValue)
		{
			SetAttribute( vName , vValue ?  StringConstValue._Yes : StringConstValue._No  );
		}
		private bool GetBoolAttribute2( string vName , bool vDefault)
		{
			string vValue = GetAttribute( vName );
			if( vValue == null)
				return vDefault;
			if( vDefault)
				return ! vValue.ToLower().Trim().Equals( StringConstValue._No );
			else
				return vValue.ToLower().Trim().Equals( StringConstValue._Yes );
		}
	}//public class HTMLHTAApplicationElement : HTMLElement
}