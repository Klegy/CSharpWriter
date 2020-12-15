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
	/// HTML属性名称常量列表
	/// </summary>
	public sealed class StringConstAttributeName
	{
		
		#region 事件名称 **************************************************************************

		/// <summary>
		/// 常量 "onclick"
		/// </summary>
		public const string OnClick		= "onclick";
		/// <summary>
		/// 常量 "onmousedown"
		/// </summary>
		public const string OnMouseDown = "onmousedown";
		/// <summary>
		/// 常量 "onmouseenter"
		/// </summary>
		public const string OnMouseEnter= "onmouseenter";
		/// <summary>
		/// 常量 "onmouseover"
		/// </summary>
		public const string OnMouseOver	= "onmouseover";
		/// <summary>
		/// 常量 "onmouseleave"
		/// </summary>
		public const string OnMouseLeave= "onmouseleave";
		/// <summary>
		/// 常量 "onmousewheel"
		/// </summary>
		public const string OnMouseWheel= "onmousewheel";
		/// <summary>
		/// 常量 "onmousemove"
		/// </summary>
		public const string OnMouseMove = "onmousemove";
		/// <summary>
		/// 常量 "onmouseup"
		/// </summary>
		public const string OnMouseUp	= "onmouseup";
		/// <summary>
		/// 常量 "onblur"
		/// </summary>
		public const string OnBlur		= "onblur";

		#endregion

		public const string XMLNS		= "xmlns";
		public const string Style		= "style";
		public const string ID			= "id";
		public const string Type		= "type";
		public const string Value		= "value";
		public const string ClassName	= "class";
		public const string Name		= "name";
		public const string Method		= "method";
		public const string Action		= "action";
		public const string Target		= "target";
		public const string Rows		= "rows";
		public const string Cols		= "cols";
		public const string Readonly	= "readonly";
		public const string Disabled	= "disabled";
		public const string Multiple	= "multiple";
		public const string Checked		= "checked";
		public const string Selected	= "selected";
		public const string Size		= "size";
		public const string Border		= "border";
		public const string BorderColor	= "bordercolor";
		public const string BgColor		= "bgcolor";
		public const string CellPadding = "cellpadding";
		public const string CellSpacing = "cellspacing";
		public const string Rules		= "rules";
		public const string Align		= "align";
		public const string VAlign		= "valign";
		public const string BackGround  = "background";
		public const string RowSpan		= "rowspan";
		public const string ColSpan		= "colspan";
		public const string NoWrap		= "nowrap";
		public const string Width		= "width";
		public const string Height		= "height";
		public const string Src			= "src";
		public const string Href		= "href";
		public const string Color		= "color";
		public const string Noshade		= "noshade";
		public const string Face		= "face";
		public const string Wrap		= "wrap";
		public const string Methods		= "methods";
		public const string Title		= "title";
		public const string Urn			= "urn";
		public const string Shape		= "shape";
		public const string Alt			= "alt";
		public const string HSpace		= "hspace";
		public const string Loop		= "loop";
		public const string LowSrc		= "lowsrc";
		public const string VSpace		= "vspace";
		public const string Defer		= "defer";
		public const string Event		= "event";
		public const string HTMLFor		= "for";
		public const string Language	= "language";
		public const string ALink		= "alink";
		public const string BottomMargin= "bottommargin";
		public const string LeftMargin	= "leftmargin";
		public const string RightMargin = "rightmargin";
		public const string TopMargin	= "topmargin";
		public const string VLink		= "vlink";
		public const string Content		= "content";
		public const string HttpEquiv	= "http-equiv";
		public const string Scheme		= "scheme";
		public const string Coords		= "coords";
		public const string Enctype		= "enctype";
		public const string Rel			= "rel";
		public const string Rev			= "rev";
		public const string Balance		= "balance";
		public const string Volume		= "volume";
		public const string Code		= "code";
		public const string CodeBase	= "codebase";
		public const string ClassID		= "classid";
		public const string CodeType	= "codetype";
		public const string Data		= "data";
		public const string Text		= "text";
		public const string XSLForeach	= "xslforeach";
		public const string XSLIf		= "xslif";
		public const string XSLSource	= "xslsource";
		public const string ValueXSLSource = "valuexslsource";
		public const string TextXSLSource = "textxslsource";
		public const string FrameBorder	= "frameborder";
		public const string FrameSpacing= "framespacing";
		public const string HideFocus	= "hidefocus";
		//public const string MarginHeight= "marginheight";
		//public const string MarginWidth	= "marginwidth";
		public const string NoResize	= "noresize";
		public const string Scrolling	= "scrolling";
		
		

		public const string ApplicationName = "applicationname";
		public const string BorderStyle		= "borderstyle";
		public const string Caption			= "caption";
		public const string ContextMenu		= "contexmenu";
		public const string ICON			= "icon";
		public const string InnerBorder		= "innerborder";
		public const string MaximizeButton	= "maximizebutton";
		public const string MinimizeButton	= "minimizebutton";
		public const string Navigable		= "navigable";
		public const string Scroll			= "scroll";
		public const string ScrollFlat		= "scrollflat";
		public const string Selection		= "selection";
		public const string ShowInTaskBar	= "showintaskbar";
		public const string SingleInstance	= "singleinstance";
		public const string SysMenu			= "sysmenu";
		public const string WindowState		= "windowstate";
		public const string Version			= "version";
		public const string AccessKey		= "accesskey";
		public const string Behavior		= "behavior";
		public const string Direction		= "direction";
		public const string ScrollAmount	= "scrollamount";
		public const string ScrollDelay		= "scrolldelay";
		public const string TimeContainer	= "timecontainer";
		public const string TrueSpeed		= "truespeed";
		public const string AllowTransparency="allowtransparency";
		
		public const string Media			= "media";
		public const string ContentSrc		= "contentsrc";
		public const string MarginHeight	= "marginheight";
		public const string MarginWidth		= "marginwidth";
		public const string NextRect		= "nextrect";

		public const string DateLong		= "datelong";
		public const string DateShort		= "dateshort";
		public const string Page			= "page";
		public const string PageTotal		= "pagetotal";
		public const string TextFoot		= "textfoot";
		public const string TextHead		= "texthead";
		public const string TimeLong		= "timelong";
		public const string TimeShort		= "timeshort";
		public const string URL				= "url";
		public const string AllLinkedDocuments = "alllinkeddocuments";
		public const string Collate			= "collate";
		public const string Copies			= "copies";
		public const string Footer			= "footer";
		public const string FrameActive		= "frameactive";
		public const string FrameAsShown	= "frameasshown";
		public const string FrameSetDocument= "framesetdocument";
		public const string Header			= "header";
		public const string MarginBottom	= "marginbottom";
		public const string MarginLeft		= "marginleft";
		public const string MarginRight		= "marginright";
		public const string MarginTop		= "margintop";
		public const string PageFrom		= "pagefrom";
		public const string PageHeight		= "pageheight";
		public const string PageTo			= "pageto";
		public const string PageWidth		= "pagewidth";
		public const string SelectedPages	= "selectedpages";
		public const string TableOfLinks	= "tableoflinks";

		public const string Link			= "link" ;
		public const string Span			= "span";

		private StringConstAttributeName(){}
	}
}
