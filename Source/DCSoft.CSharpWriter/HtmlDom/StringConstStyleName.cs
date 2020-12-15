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
	/// 样式表项目名称字符串常量
	/// </summary>
	public sealed class StringConstStyleName
	{
		///<summary>Sets or retrieves a string that indicates whether the object contains an accelerator key.</summary>
		public const string accelerator = "accelerator" ;	
		///<summary>Sets or retrieves up to five separate background properties of the object.</summary>
		public const string background = "background" ;	
		///<summary>Sets or retrieves how the background image is attached to the object within the document.</summary>
		public const string backgroundAttachment = "background-attachment" ;	
		///<summary>Sets or retrieves the color behind the content of the object. </summary>
		public const string backgroundColor = "background-color" ;	
		///<summary>Sets or retrieves the background image of the object. </summary>
		public const string backgroundImage = "background-image" ;	
		///<summary>Sets or retrieves the position of the background of the object. </summary>
		public const string backgroundPosition = "background-position" ;	
		///<summary>Sets or retrieves the x-coordinate of the backgroundPosition property. </summary>
		public const string backgroundPositionX = "background-position-x" ;	
		///<summary>Sets or retrieves the y-coordinate of the backgroundPosition property. </summary>
		public const string backgroundPositionY = "background-position-y" ;	
		///<summary>Sets or retrieves how the backgroundImage property of the object is tiled.</summary>
		public const string backgroundRepeat = "background-repeat" ;	
		///<summary>Sets or retrieves the location of the Introduction to DHTML Behaviors.</summary>
		public const string behavior = "behavior" ;	
		///<summary>Sets or retrieves the properties to draw around the object.</summary>
		public const string border = "border" ;	
		///<summary>Sets or retrieves the properties of the bottom border of the object.</summary>
		public const string borderBottom = "border-bottom" ;	
		///<summary>Sets or retrieves the color of the bottom border of the object. </summary>
		public const string borderBottomColor = "border-bottom-color" ;	
		///<summary>Sets or retrieves the style of the bottom border of the object. </summary>
		public const string borderBottomStyle = "border-bottom-style" ;	
		///<summary>Sets or retrieves the width of the bottom border of the object. </summary>
		public const string borderBottomWidth = "border-bottom-width" ;	
		///<summary>Sets or retrieves a value that indicates whether the row and cell borders of a table are joined in a single border or detached as in standard HTML.</summary>
		public const string borderCollapse = "border-collapse" ;	
		///<summary>Sets or retrieves the border color of the object.</summary>
		public const string borderColor = "border-color" ;	
		///<summary>Sets or retrieves the properties of the left border of the object.</summary>
		public const string borderLeft = "border-left" ;	
		///<summary>Sets or retrieves the color of the left border of the object. </summary>
		public const string borderLeftColor = "border-left-color" ;	
		///<summary>Sets or retrieves the style of the left border of the object. </summary>
		public const string borderLeftStyle = "border-left-style" ;	
		///<summary>Sets or retrieves the width of the left border of the object. </summary>
		public const string borderLeftWidth = "border-left-width" ;	
		///<summary>Sets or retrieves the properties of the right border of the object. </summary>
		public const string borderRight = "border-right" ;	
		///<summary>Sets or retrieves the color of the right border of the object. </summary>
		public const string borderRightColor = "border-right-color" ;	
		///<summary>Sets or retrieves the style of the right border of the object. </summary>
		public const string borderRightStyle = "border-right-style" ;	
		///<summary>Sets or retrieves the width of the right border of the object. </summary>
		public const string borderRightWidth = "border-right-width" ;	
		///<summary>Sets or retrieves the style of the left, right, top, and bottom borders of the object. </summary>
		public const string borderStyle = "border-style" ;	
		///<summary>Sets or retrieves the properties of the top border of the object. </summary>
		public const string borderTop = "border-top" ;	
		///<summary>Sets or retrieves the color of the top border of the object. </summary>
		public const string borderTopColor = "border-top-color" ;	
		///<summary>Sets or retrieves the style of the top border of the object. </summary>
		public const string borderTopStyle = "border-top-style" ;	
		///<summary>Sets or retrieves the width of the top border of the object. </summary>
		public const string borderTopWidth = "border-top-width" ;	
		///<summary>Sets or retrieves the width of the left, right, top, and bottom borders of the object. </summary>
		public const string borderWidth = "border-width" ;	
		///<summary>Sets or retrieves the bottom position of the object in relation to the bottom of the next positioned object in the document hierarchy. </summary>
		public const string bottom = "bottom" ;	
		///<summary>Sets or retrieves whether the object allows floating objects on its left side, right side, or both, so that the next text displays past the floating objects.</summary>
		public const string clear = "clear" ;	
		///<summary>Sets or retrieves which part of a positioned object is visible. </summary>
		public const string clip = "clip" ;	
		///<summary>Sets or retrieves the color of the text of the object. </summary>
		public const string color = "color" ;	
		///<summary>Sets or retrieves the type of cursor to display as the mouse pointer moves over the object. </summary>
		public const string cursor = "cursor" ;	
		///<summary>Sets or retrieves the reading order of the object. </summary>
		public const string direction = "direction" ;	
		///<summary>Sets or retrieves whether the object is rendered.</summary>
		public const string display = "display" ;	
		///<summary>Sets or retrieves the filter or collection of filters applied to the object. </summary>
		public const string filter = "filter" ;	
		///<summary>Sets or retrieves a combination of separate font properties of the object. Alternatively, sets or retrieves one or more of six user-preference fonts.</summary>
		public const string font = "font" ;	
		///<summary>Sets or retrieves the name of the font used for text in the object. </summary>
		public const string fontFamily = "font-family" ;	
		///<summary>Sets or retrieves a value that indicates the font size used for text in the object. </summary>
		public const string fontSize = "font-size" ;	
		///<summary>Sets or retrieves the font style of the object as italic, normal, or oblique. </summary>
		public const string fontStyle = "font-style" ;	
		///<summary>Sets or retrieves whether the text of the object is in small capital letters.</summary>
		public const string fontVariant = "font-variant" ;	
		///<summary>Sets or retrieves the weight of the font of the object. </summary>
		public const string fontWeight = "font-weight" ;	
		///<summary>Sets or retrieves the height of the object. </summary>
		public const string height = "height" ;	
		///<summary>Sets or retrieves the state of an Input Method Editor (IME).</summary>
		public const string imeMode = "ime-mode" ;	
		///<summary>Sets or retrieves the direction and flow of the content in the object.</summary>
		public const string layoutFlow = "layout-flow" ;	
		///<summary>Sets or retrieves the composite document grid properties that specify the layout of text characters.</summary>
		public const string layoutGrid = "layout-grid" ;	
		///<summary>Sets or retrieves the size of the character grid used for rendering the text content of an element.</summary>
		public const string layoutGridChar = "layout-grid-char" ;	
		///<summary>Sets or retrieves the gridline value used for rendering the text content of an element.</summary>
		public const string layoutGridLine = "layout-grid-line" ;	
		///<summary>Sets or retrieves whether the text layout grid uses two dimensions.</summary>
		public const string layoutGridMode = "layout-grid-mode" ;	
		///<summary>Sets or retrieves the type of grid used for rendering the text content of an element.</summary>
		public const string layoutGridType = "layout-grid-type" ;	
		///<summary>Sets or retrieves the position of the object relative to the left edge of the next positioned object in the document hierarchy. </summary>
		public const string left = "left" ;	
		///<summary>Sets or retrieves the amount of additional space between letters in the object. </summary>
		public const string letterSpacing = "letter-spacing" ;	
		///<summary>Sets or retrieves line-breaking rules for Japanese text.</summary>
		public const string lineBreak = "line-break" ;	
		///<summary>Sets or retrieves the distance between lines in the object. </summary>
		public const string lineHeight = "line-height" ;	
		///<summary>Sets or retrieves up to three separate listStyle properties of the object.</summary>
		public const string listStyle = "list-style" ;	
		///<summary>Sets or retrieves a value that indicates which image to use as a list-item marker for the object. </summary>
		public const string listStyleImage = "list-style-image" ;	
		///<summary>Sets or retrieves a variable that indicates how the list-item marker is drawn relative to the content of the object. </summary>
		public const string listStylePosition = "list-style-position" ;	
		///<summary>Sets or retrieves the predefined type of the line-item marker for the object. </summary>
		public const string listStyleType = "list-style-type" ;	
		///<summary>Sets or retrieves the width of the top, right, bottom, and left margins of the object. </summary>
		public const string margin = "margin" ;	
		///<summary>Sets or retrieves the height of the bottom margin of the object. </summary>
		public const string marginBottom = "margin-bottom" ;	
		///<summary>Sets or retrieves the width of the left margin of the object. </summary>
		public const string marginLeft = "margin-left" ;	
		///<summary>Sets or retrieves the width of the right margin of the object. </summary>
		public const string marginRight = "margin-right" ;	
		///<summary>Sets or retrieves the height of the top margin of the object. </summary>
		public const string marginTop = "margin-top" ;	
		///<summary>Sets or retrieves the media type. </summary>
		public const string media = "media" ;	
		///<summary>Sets or retrieves the minimum height for an element.</summary>
		public const string minHeight = "min-height" ;	
		///<summary>Sets or retrieves a value indicating how to manage the content of the object when the content exceeds the height or width of the object.</summary>
		public const string overflow = "overflow" ;	
		///<summary>Sets or retrieves how to manage the content of the object when the content exceeds the width of the object.</summary>
		public const string overflowX = "overflow-x" ;	
		///<summary>Sets or retrieves how to manage the content of the object when the content exceeds the height of the object.</summary>
		public const string overflowY = "overflow-y" ;	
		///<summary>Sets or retrieves the amount of space to insert between the object and its margin or, if there is a border, between the object and its border. </summary>
		public const string padding = "padding" ;	
		///<summary>Sets or retrieves the amount of space to insert between the bottom border of the object and the content. </summary>
		public const string paddingBottom = "padding-bottom" ;	
		///<summary>Sets or retrieves the amount of space to insert between the left border of the object and the content. </summary>
		public const string paddingLeft = "padding-left" ;	
		///<summary>Sets or retrieves the amount of space to insert between the right border of the object and the content. </summary>
		public const string paddingRight = "padding-right" ;	
		///<summary>Sets or retrieves the amount of space to insert between the top border of the object and the content. </summary>
		public const string paddingTop = "padding-top" ;	
		///<summary>Sets or retrieves a value indicating whether a page break occurs after the object. </summary>
		public const string pageBreakAfter = "pageBreakAfter" ;	
		///<summary>Sets or retrieves a string indicating whether a page break occurs before the object.</summary>
		public const string pageBreakBefore = "pageBreakBefore" ;	
		///<summary>Sets or retrieves the type of positioning used for the object. </summary>
		public const string position = "position" ;	
		///<summary>Sets or retrieves the position of the object relative to the right edge of the next positioned object in the document hierarchy. </summary>
		public const string right = "right" ;	
		///<summary>Sets or retrieves the position of the ruby text specified by the rt object.</summary>
		public const string rubyAlign = "ruby-align" ;	
		///<summary>Sets or retrieves the position of the ruby text specified by the rt object. </summary>
		public const string rubyOverhang = "ruby-overhang" ;	
		///<summary>Sets or retrieves the position of the ruby text specified by the rt object.</summary>
		public const string rubyPosition = "ruby-position" ;	
		///<summary>Sets or retrieves the color of the top and left edges of the scroll box and scroll arrows of a scroll bar.</summary>
		public const string scrollbar3dLightColor = "scrollbar-3dlight-color" ;	
		///<summary>Sets or retrieves the color of the arrow elements of a scroll arrow.</summary>
		public const string scrollbarArrowColor = "scrollbar-arrow-color" ;	
		///<summary>Sets or retrieves the color of the main elements of a scroll bar, which include the scroll box, track, and scroll arrows.</summary>
		public const string scrollbarBaseColor = "scrollbar-base-color" ;	
		///<summary>Sets or retrieves the color of the gutter of a scroll bar.</summary>
		public const string scrollbarDarkShadowColor = "scrollbar-darkshadow-color" ;	
		///<summary>Sets or retrieves the color of the scroll box and scroll arrows of a scroll bar.</summary>
		public const string scrollbarFaceColor = "scrollbar-face-color" ;	
		///<summary>Sets or retrieves the color of the top and left edges of the scroll box and scroll arrows of a scroll bar.</summary>
		public const string scrollbarHighlightColor = "scrollbar-highlight-color" ;	
		///<summary>Sets or retrieves the color of the bottom and right edges of the scroll box and scroll arrows of a scroll bar.</summary>
		public const string scrollbarShadowColor = "scrollbar-shadow-color" ;	
		///<summary>Sets or retrieves the color of the track element of a scroll bar.</summary>
		public const string scrollbarTrackColor = "scrollbar-track-color" ;	
		///<summary>Sets or retrieves on which side of the object the text will flow.</summary>
		public const string styleFloat = "float" ;	
		///<summary>Sets or retrieves a string that indicates whether the table layout is fixed.</summary>
		public const string tableLayout = "table-layout" ;	
		///<summary>Sets or retrieves whether the text in the object is left-aligned, right-aligned, centered, or justified. </summary>
		public const string textAlign = "text-align" ;	
		///<summary>Sets or retrieves how to align the last line or only line of text in the object.</summary>
		public const string textAlignLast = "text-align-last" ;	
		///<summary>Sets or retrieves the autospacing and narrow space width adjustment of text. </summary>
		public const string textAutospace = "text-autospace" ;	
		///<summary>Sets or retrieves a value that indicates whether the text in the object has blink, line-through, overline, or underline decorations. </summary>
		public const string textDecoration = "text-decoration" ;	
		///<summary>Sets or retrieves the indentation of the first line of text in the object. </summary>
		public const string textIndent = "text-indent" ;	
		///<summary>Sets or retrieves the type of alignment used to justify text in the object.</summary>
		public const string textJustify = "text-justify" ;	
		///<summary>Sets or retrieves the ratio of kashida expansion to white space expansion when justifying lines of text in the object.</summary>
		public const string textKashidaSpace = "text-kashida-space" ;	
		///<summary>Sets or retrieves a value that indicates whether to render ellipses(...) to indicate text overflow.</summary>
		public const string textOverflow = "text-overflow" ;	
		///<summary>Sets or retrieves the rendering of the text in the object. </summary>
		public const string textTransform = "text-transform" ;	
		///<summary>Sets or retrieves the position of the underline decoration that is set through the textDecoration property of the object.</summary>
		public const string textUnderlinePosition = "text-underline-position" ;	
		///<summary>Sets or retrieves the position of the object relative to the top of the next positioned object in the document hierarchy. </summary>
		public const string top = "top" ;	
		///<summary>Sets or retrieves the level of embedding with respect to the bidirectional algorithm.</summary>
		public const string unicodeBidi = "unicode-bidi" ;	
		///<summary>Sets or retrieves the vertical alignment of the object. </summary>
		public const string verticalAlign = "vertical-align" ;	
		///<summary>Sets or retrieves whether the content of the object is displayed. </summary>
		public const string visibility = "visibility" ;	
		///<summary>Sets or retrieves a value that indicates whether lines are automatically broken inside the object.</summary>
		public const string whiteSpace = "white-space" ;	
		///<summary>Sets or retrieves the width of the object. </summary>
		public const string width = "width" ;	
		///<summary>Sets or retrieves line-breaking behavior within words, particularly where multiple languages appear in the object.</summary>
		public const string wordBreak = "word-break" ;	
		///<summary>Sets or retrieves the amount of additional space between words in the object. </summary>
		public const string wordSpacing = "word-spacing" ;	
		///<summary>Sets or retrieves whether to break words when the content exceeds the boundaries of its container.</summary>
		public const string wordWrap = "word-wrap" ;	
		///<summary>Sets or retrieves the direction and flow of the content in the object.</summary>
		public const string writingMode = "writing-mode" ;	
		///<summary>Sets or retrieves the stacking order of positioned objects. </summary>
		public const string zIndex = "z-index" ;	
		///<summary>Sets or retrieves the magnification scale of the object.</summary>
		public const string zoom = "zoom" ;	
		private StringConstStyleName(){}
	}//public sealed class StringConstStyleName
}