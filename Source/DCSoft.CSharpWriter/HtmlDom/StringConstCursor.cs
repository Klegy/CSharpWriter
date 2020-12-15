/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;

namespace DCSoft.HtmlDom
{
	/// <summary>
	/// HTML鼠标样式属性可选值字符串常量列表
	/// </summary>
	public sealed class StringConstCursor
	{
		/// <summary>
		/// 默认值
		/// </summary>
		public const string DefaultValue = "auto";
		/// <summary>
		/// Internet Explorer 6 and later. Arrows pointing up, down, left, and right with a dot in the middle, indicating that the page can be scrolled in any direction. 
		/// </summary>
		public const string all_scroll    	= "all-scroll" ;
		/// <summary>
		/// Default. Browser determines which cursor to display based on the current context. 
		/// </summary>
		public const string auto  			= "auto" ; 
		/// <summary>
		/// Internet Explorer 6 and later. Arrows pointing left and right with a vertical bar separating them, indicating that the item/column can be resized horizontally. 
		/// </summary>
		public const string col_resize    	= "col-resize" ;
		/// <summary>
		/// Simple cross hair. 
		/// </summary>
		public const string crosshair  		= "crosshair" ;
		/// <summary>
		/// Platform-dependent default cursor; usually an arrow. 
		/// </summary>
		public const string _default  		= "default" ;
		/// <summary>
		/// Hand with the first finger pointing up, as when the user moves the pointer over a link. 
		/// </summary>
		public const string hand  			= "hand" ; 
		/// <summary>
		/// Arrow with question mark, indicating help is available. 
		/// </summary>
		public const string help  			= "help" ; 
		/// <summary>
		/// Crossed arrows, indicating something is to be moved. 
		/// </summary>
		public const string move  			= "move" ; 
		/// <summary>
		/// Internet Explorer 6 and later. Hand with a small circle with a line through it, indicating that the dragged item cannot be dropped at the current cursor location. 
		/// </summary>
		public const string no_drop    		= "no-drop" ; 
		/// <summary>
		/// Internet Explorer 6 and later. Circle with a line through it, indicating that the requested action will not be carried out. 
		/// </summary>
		public const string not_allowed    	= "not-allowed" ;
		/// <summary>
		/// Internet Explorer 6 and later. Hand with the first finger pointing up, as when the user moves the pointer over a link. Identical to hand. 
		/// </summary>
		public const string pointer  		= "pointer" ; 
		/// <summary>
		/// Internet Explorer 6 and later. Arrow with an hourglass next to it, indicating that a process is running in the background. User interaction with the page is unaffected. 
		/// </summary>
		public const string progress    		= "progress" ; 
		/// <summary>
		/// Internet Explorer 6 and later. Arrows pointing up and down with a horizontal bar separating them, indicating that the item/row can be resized vertically. 
		/// </summary>
		public const string row_resize    	= "row-resize" ;
		/// <summary>
		/// Editable text; usually an I-bar. 
		/// </summary>
		public const string text  			= "text" ;
		/// <summary>
		///  url(uri)   Internet Explorer 6 and later. Cursor is defined by the author, using a custom Uniform Resource Identifier (URI), such as url('mycursor.cur'). Cursors of type .CUR and .ANI are the only supported cursor types. 
		/// </summary>
		public const string url			    = "url" ;
		/// <summary>
		/// Internet Explorer 6 and later. Editable vertical text, indicated by a horizontal I-bar. 
		/// </summary>
		public const string vertical_text    = "vertical-text" ;
		/// <summary>
		/// Hourglass or watch, indicating that the program is busy and the user should wait. 
		/// </summary>
		public const string wait  			= "wait" ; 
		/// <summary>
		/// Arrows, indicating an edge is to be moved; the asterisk (*) can be N, NE, NW, S, SE, SW, E, or W—each representing a compass direction 
		/// </summary>
		//public const string resize  			= "*-resize" ; 
		public const string N_resize	= "n-resize";
		public const string NE_resize	= "ne-resize";
		public const string NW_resize	= "nw-resize";
		public const string S_resize	= "s-resize";
		public const string SE_resize	= "se-resize";
		public const string SW_resize	= "sw-resize";
		public const string E_resize	= "e-resize";
		public const string W_resize	= "w-resize";


		private StringConstCursor(){}
	}//public sealed class HTMLCursorStringConst
}