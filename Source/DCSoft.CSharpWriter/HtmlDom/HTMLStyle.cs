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
	/// 样式表对象
	/// </summary>
	public class HTMLStyle 
	{
		/// <summary>
		/// 属性列表
		/// </summary>
		protected HTMLAttributeList myAttributes = new HTMLAttributeList();
		/// <summary>
		/// 属性列表
		/// </summary>
		public HTMLAttributeList Attributes
		{
			get{ return myAttributes ;}
		}
		/// <summary>
		/// 获得属性值
		/// </summary>
		/// <param name="strName">属性名称</param>
		/// <returns>属性值</returns>
		protected string GetAttribute( string strName )
		{
			return myAttributes.GetAttribute( strName );
		}
		/// <summary>
		/// 设置属性值
		/// </summary>
		/// <param name="strName">属性名称</param>
		/// <param name="strValue">属性值</param>
		protected void SetAttribute( string strName , string strValue)
		{
			if( strValue == null || strValue.Trim().Length == 0 )
			{
				myAttributes.RemoveAttribute( strName );
				return ;
			}
			myAttributes.SetAttribute( strName , strValue );
		}
		/// <summary>
		/// 删除属性
		/// </summary>
		/// <param name="strName">属性名称</param>
		protected void RemoveAttribute( string strName )
		{
			myAttributes.RemoveAttribute( strName );
		}
		/// <summary>
		/// 是否存在指定名称的属性
		/// </summary>
		/// <param name="strName">属性名称</param>
		/// <returns>是否存在指定名称的属性</returns>
		protected bool HasAttribute( string strName)
		{
			return myAttributes.HasAttribute( strName );
		}

		/// <summary>
		/// 表示对象数据的CSS字符串,格式为"属性名=属性值;属性名=属性值"
		/// </summary>
		public string CSSString
		{
			get
			{
				if( myAttributes.Count == 0 )
					return null;
				System.Text.StringBuilder myStr = new System.Text.StringBuilder();
				foreach( HTMLAttribute a in myAttributes )
				{
					if( myStr.Length != 0 )
						myStr.Append(" ; " );
					myStr.Append( a.Name );
					myStr.Append( ':');
					myStr.Append( a.Value );
				}
				return myStr.ToString();
			}
			set
			{
				myAttributes.Clear();
				if( value == null)
					return ;
				string[] strItems = value.Split(";".ToCharArray());
				foreach( string strItem in strItems)
				{
					int index = strItem.IndexOf(":");
					if( index > 0 && index < strItem.Length - 1)
					{
						myAttributes.SetAttribute( strItem.Substring( 0 , index ).Trim().ToLower() ,strItem.Substring( index + 1 ) );
					}
				}//foreach
			}
		}
		/// <summary>
		/// 对象所属HTML对象
		/// </summary>
		protected HTMLElement myOwnerElement = null;
		/// <summary>
		/// 对象所属HTML对象
		/// </summary>
		public HTMLElement OwnerElement 
		{
			get{ return myOwnerElement;}
			set{ myOwnerElement = value;}
		}

		#region CSS 属性 ***************************************************************************
	
		///<summary>Sets or retrieves a string that indicates whether the object contains an accelerator key.</summary>
		public string S_accelerator
		{
			get{ return GetAttribute( StringConstStyleName.accelerator);}
			set{ SetAttribute( StringConstStyleName.accelerator , value);}
		}
		///<summary>Sets or retrieves up to five separate background properties of the object.</summary>
		public string S_background
		{
			get{ return GetAttribute( StringConstStyleName.background);}
			set{ SetAttribute( StringConstStyleName.background , value);}
		}
		///<summary>Sets or retrieves how the background image is attached to the object within the document.</summary>
		public string S_backgroundAttachment
		{
			get{ return GetAttribute( StringConstStyleName.backgroundAttachment);}
			set{ SetAttribute( StringConstStyleName.backgroundAttachment , value);}
		}
		///<summary>Sets or retrieves the color behind the content of the object. </summary>
		public string S_backgroundColor
		{
			get{ return GetAttribute( StringConstStyleName.backgroundColor);}
			set{ SetAttribute( StringConstStyleName.backgroundColor , value);}
		}
		///<summary>Sets or retrieves the background image of the object. </summary>
		public string S_backgroundImage
		{
			get{ return GetAttribute( StringConstStyleName.backgroundImage);}
			set{ SetAttribute( StringConstStyleName.backgroundImage , value);}
		}
		///<summary>Sets or retrieves the position of the background of the object. </summary>
		public string S_backgroundPosition
		{
			get{ return GetAttribute( StringConstStyleName.backgroundPosition);}
			set{ SetAttribute( StringConstStyleName.backgroundPosition , value);}
		}
		///<summary>Sets or retrieves the x-coordinate of the backgroundPosition property. </summary>
		public string S_backgroundPositionX
		{
			get{ return GetAttribute( StringConstStyleName.backgroundPositionX);}
			set{ SetAttribute( StringConstStyleName.backgroundPositionX , value);}
		}
		///<summary>Sets or retrieves the y-coordinate of the backgroundPosition property. </summary>
		public string S_backgroundPositionY
		{
			get{ return GetAttribute( StringConstStyleName.backgroundPositionY);}
			set{ SetAttribute( StringConstStyleName.backgroundPositionY , value);}
		}
		///<summary>Sets or retrieves how the backgroundImage property of the object is tiled.</summary>
		public string S_backgroundRepeat
		{
			get{ return GetAttribute( StringConstStyleName.backgroundRepeat);}
			set{ SetAttribute( StringConstStyleName.backgroundRepeat , value);}
		}
		///<summary>Sets or retrieves the location of the Introduction to DHTML Behaviors.</summary>
		public string S_behavior
		{
			get{ return GetAttribute( StringConstStyleName.behavior);}
			set{ SetAttribute( StringConstStyleName.behavior , value);}
		}
		///<summary>Sets or retrieves the properties to draw around the object.</summary>
		public string S_border
		{
			get{ return GetAttribute( StringConstStyleName.border);}
			set{ SetAttribute( StringConstStyleName.border , value);}
		}
		///<summary>Sets or retrieves the properties of the bottom border of the object.</summary>
		public string S_borderBottom
		{
			get{ return GetAttribute( StringConstStyleName.borderBottom);}
			set{ SetAttribute( StringConstStyleName.borderBottom , value);}
		}
		///<summary>Sets or retrieves the color of the bottom border of the object. </summary>
		public string S_borderBottomColor
		{
			get{ return GetAttribute( StringConstStyleName.borderBottomColor);}
			set{ SetAttribute( StringConstStyleName.borderBottomColor , value);}
		}
		///<summary>Sets or retrieves the style of the bottom border of the object. </summary>
		public string S_borderBottomStyle
		{
			get{ return GetAttribute( StringConstStyleName.borderBottomStyle);}
			set{ SetAttribute( StringConstStyleName.borderBottomStyle , value);}
		}
		///<summary>Sets or retrieves the width of the bottom border of the object. </summary>
		public string S_borderBottomWidth
		{
			get{ return GetAttribute( StringConstStyleName.borderBottomWidth);}
			set{ SetAttribute( StringConstStyleName.borderBottomWidth , value);}
		}
		///<summary>Sets or retrieves a value that indicates whether the row and cell borders of a table are joined in a single border or detached as in standard HTML.</summary>
		public string S_borderCollapse
		{
			get{ return GetAttribute( StringConstStyleName.borderCollapse);}
			set{ SetAttribute( StringConstStyleName.borderCollapse , value);}
		}
		///<summary>Sets or retrieves the border color of the object.</summary>
		public string S_borderColor
		{
			get{ return GetAttribute( StringConstStyleName.borderColor);}
			set{ SetAttribute( StringConstStyleName.borderColor , value);}
		}
		///<summary>Sets or retrieves the properties of the left border of the object.</summary>
		public string S_borderLeft
		{
			get{ return GetAttribute( StringConstStyleName.borderLeft);}
			set{ SetAttribute( StringConstStyleName.borderLeft , value);}
		}
		///<summary>Sets or retrieves the color of the left border of the object. </summary>
		public string S_borderLeftColor
		{
			get{ return GetAttribute( StringConstStyleName.borderLeftColor);}
			set{ SetAttribute( StringConstStyleName.borderLeftColor , value);}
		}
		///<summary>Sets or retrieves the style of the left border of the object. </summary>
		public string S_borderLeftStyle
		{
			get{ return GetAttribute( StringConstStyleName.borderLeftStyle);}
			set{ SetAttribute( StringConstStyleName.borderLeftStyle , value);}
		}
		///<summary>Sets or retrieves the width of the left border of the object. </summary>
		public string S_borderLeftWidth
		{
			get{ return GetAttribute( StringConstStyleName.borderLeftWidth);}
			set{ SetAttribute( StringConstStyleName.borderLeftWidth , value);}
		}
		///<summary>Sets or retrieves the properties of the right border of the object. </summary>
		public string S_borderRight
		{
			get{ return GetAttribute( StringConstStyleName.borderRight);}
			set{ SetAttribute( StringConstStyleName.borderRight , value);}
		}
		///<summary>Sets or retrieves the color of the right border of the object. </summary>
		public string S_borderRightColor
		{
			get{ return GetAttribute( StringConstStyleName.borderRightColor);}
			set{ SetAttribute( StringConstStyleName.borderRightColor , value);}
		}
		///<summary>Sets or retrieves the style of the right border of the object. </summary>
		public string S_borderRightStyle
		{
			get{ return GetAttribute( StringConstStyleName.borderRightStyle);}
			set{ SetAttribute( StringConstStyleName.borderRightStyle , value);}
		}
		///<summary>Sets or retrieves the width of the right border of the object. </summary>
		public string S_borderRightWidth
		{
			get{ return GetAttribute( StringConstStyleName.borderRightWidth);}
			set{ SetAttribute( StringConstStyleName.borderRightWidth , value);}
		}
		///<summary>Sets or retrieves the style of the left, right, top, and bottom borders of the object. </summary>
		public string S_borderStyle
		{
			get{ return GetAttribute( StringConstStyleName.borderStyle);}
			set{ SetAttribute( StringConstStyleName.borderStyle , value);}
		}
		///<summary>Sets or retrieves the properties of the top border of the object. </summary>
		public string S_borderTop
		{
			get{ return GetAttribute( StringConstStyleName.borderTop);}
			set{ SetAttribute( StringConstStyleName.borderTop , value);}
		}
		///<summary>Sets or retrieves the color of the top border of the object. </summary>
		public string S_borderTopColor
		{
			get{ return GetAttribute( StringConstStyleName.borderTopColor);}
			set{ SetAttribute( StringConstStyleName.borderTopColor , value);}
		}
		///<summary>Sets or retrieves the style of the top border of the object. </summary>
		public string S_borderTopStyle
		{
			get{ return GetAttribute( StringConstStyleName.borderTopStyle);}
			set{ SetAttribute( StringConstStyleName.borderTopStyle , value);}
		}
		///<summary>Sets or retrieves the width of the top border of the object. </summary>
		public string S_borderTopWidth
		{
			get{ return GetAttribute( StringConstStyleName.borderTopWidth);}
			set{ SetAttribute( StringConstStyleName.borderTopWidth , value);}
		}
		///<summary>Sets or retrieves the width of the left, right, top, and bottom borders of the object. </summary>
		public string S_borderWidth
		{
			get{ return GetAttribute( StringConstStyleName.borderWidth);}
			set{ SetAttribute( StringConstStyleName.borderWidth , value);}
		}
		///<summary>Sets or retrieves the bottom position of the object in relation to the bottom of the next positioned object in the document hierarchy. </summary>
		public string S_bottom
		{
			get{ return GetAttribute( StringConstStyleName.bottom);}
			set{ SetAttribute( StringConstStyleName.bottom , value);}
		}
		///<summary>Sets or retrieves whether the object allows floating objects on its left side, right side, or both, so that the next text displays past the floating objects.</summary>
		public string S_clear
		{
			get{ return GetAttribute( StringConstStyleName.clear);}
			set{ SetAttribute( StringConstStyleName.clear , value);}
		}
		///<summary>Sets or retrieves which part of a positioned object is visible. </summary>
		public string S_clip
		{
			get{ return GetAttribute( StringConstStyleName.clip);}
			set{ SetAttribute( StringConstStyleName.clip , value);}
		}
		///<summary>Sets or retrieves the color of the text of the object. </summary>
		public string S_color
		{
			get{ return GetAttribute( StringConstStyleName.color);}
			set{ SetAttribute( StringConstStyleName.color , value);}
		}
		///<summary>Sets or retrieves the type of cursor to display as the mouse pointer moves over the object. </summary>
		public string S_cursor
		{
			get{ return GetAttribute( StringConstStyleName.cursor);}
			set{ SetAttribute( StringConstStyleName.cursor , value);}
		}
		///<summary>Sets or retrieves the reading order of the object. </summary>
		public string S_direction
		{
			get{ return GetAttribute( StringConstStyleName.direction);}
			set{ SetAttribute( StringConstStyleName.direction , value);}
		}
		///<summary>Sets or retrieves whether the object is rendered.</summary>
		public string S_display
		{
			get{ return GetAttribute( StringConstStyleName.display);}
			set{ SetAttribute( StringConstStyleName.display , value);}
		}
		///<summary>Sets or retrieves the filter or collection of filters applied to the object. </summary>
		public string S_filter
		{
			get{ return GetAttribute( StringConstStyleName.filter);}
			set{ SetAttribute( StringConstStyleName.filter , value);}
		}
		///<summary>Sets or retrieves a combination of separate font properties of the object. Alternatively, sets or retrieves one or more of six user-preference fonts.</summary>
		public string S_font
		{
			get{ return GetAttribute( StringConstStyleName.font);}
			set{ SetAttribute( StringConstStyleName.font , value);}
		}
		///<summary>Sets or retrieves the name of the font used for text in the object. </summary>
		public string S_fontFamily
		{
			get{ return GetAttribute( StringConstStyleName.fontFamily);}
			set{ SetAttribute( StringConstStyleName.fontFamily , value);}
		}
		///<summary>Sets or retrieves a value that indicates the font size used for text in the object. </summary>
		public string S_fontSize
		{
			get{ return GetAttribute( StringConstStyleName.fontSize);}
			set{ SetAttribute( StringConstStyleName.fontSize , value);}
		}
		///<summary>Sets or retrieves the font style of the object as italic, normal, or oblique. </summary>
		public string S_fontStyle
		{
			get{ return GetAttribute( StringConstStyleName.fontStyle);}
			set{ SetAttribute( StringConstStyleName.fontStyle , value);}
		}
		///<summary>Sets or retrieves whether the text of the object is in small capital letters.</summary>
		public string S_fontVariant
		{
			get{ return GetAttribute( StringConstStyleName.fontVariant);}
			set{ SetAttribute( StringConstStyleName.fontVariant , value);}
		}
		///<summary>Sets or retrieves the weight of the font of the object. </summary>
		public string S_fontWeight
		{
			get{ return GetAttribute( StringConstStyleName.fontWeight);}
			set{ SetAttribute( StringConstStyleName.fontWeight , value);}
		}
		///<summary>Sets or retrieves the height of the object. </summary>
		public string S_height
		{
			get{ return GetAttribute( StringConstStyleName.height);}
			set{ SetAttribute( StringConstStyleName.height , value);}
		}
		///<summary>Sets or retrieves the state of an Input Method Editor (IME).</summary>
		public string S_imeMode
		{
			get{ return GetAttribute( StringConstStyleName.imeMode);}
			set{ SetAttribute( StringConstStyleName.imeMode , value);}
		}
		///<summary>Sets or retrieves the direction and flow of the content in the object.</summary>
		public string S_layoutFlow
		{
			get{ return GetAttribute( StringConstStyleName.layoutFlow);}
			set{ SetAttribute( StringConstStyleName.layoutFlow , value);}
		}
		///<summary>Sets or retrieves the composite document grid properties that specify the layout of text characters.</summary>
		public string S_layoutGrid
		{
			get{ return GetAttribute( StringConstStyleName.layoutGrid);}
			set{ SetAttribute( StringConstStyleName.layoutGrid , value);}
		}
		///<summary>Sets or retrieves the size of the character grid used for rendering the text content of an element.</summary>
		public string S_layoutGridChar
		{
			get{ return GetAttribute( StringConstStyleName.layoutGridChar);}
			set{ SetAttribute( StringConstStyleName.layoutGridChar , value);}
		}
		///<summary>Sets or retrieves the gridline value used for rendering the text content of an element.</summary>
		public string S_layoutGridLine
		{
			get{ return GetAttribute( StringConstStyleName.layoutGridLine);}
			set{ SetAttribute( StringConstStyleName.layoutGridLine , value);}
		}
		///<summary>Sets or retrieves whether the text layout grid uses two dimensions.</summary>
		public string S_layoutGridMode
		{
			get{ return GetAttribute( StringConstStyleName.layoutGridMode);}
			set{ SetAttribute( StringConstStyleName.layoutGridMode , value);}
		}
		///<summary>Sets or retrieves the type of grid used for rendering the text content of an element.</summary>
		public string S_layoutGridType
		{
			get{ return GetAttribute( StringConstStyleName.layoutGridType);}
			set{ SetAttribute( StringConstStyleName.layoutGridType , value);}
		}
		///<summary>Sets or retrieves the position of the object relative to the left edge of the next positioned object in the document hierarchy. </summary>
		public string S_left
		{
			get{ return GetAttribute( StringConstStyleName.left);}
			set{ SetAttribute( StringConstStyleName.left , value);}
		}
		///<summary>Sets or retrieves the amount of additional space between letters in the object. </summary>
		public string S_letterSpacing
		{
			get{ return GetAttribute( StringConstStyleName.letterSpacing);}
			set{ SetAttribute( StringConstStyleName.letterSpacing , value);}
		}
		///<summary>Sets or retrieves line-breaking rules for Japanese text.</summary>
		public string S_lineBreak
		{
			get{ return GetAttribute( StringConstStyleName.lineBreak);}
			set{ SetAttribute( StringConstStyleName.lineBreak , value);}
		}
		///<summary>Sets or retrieves the distance between lines in the object. </summary>
		public string S_lineHeight
		{
			get{ return GetAttribute( StringConstStyleName.lineHeight);}
			set{ SetAttribute( StringConstStyleName.lineHeight , value);}
		}
		///<summary>Sets or retrieves up to three separate listStyle properties of the object.</summary>
		public string S_listStyle
		{
			get{ return GetAttribute( StringConstStyleName.listStyle);}
			set{ SetAttribute( StringConstStyleName.listStyle , value);}
		}
		///<summary>Sets or retrieves a value that indicates which image to use as a list-item marker for the object. </summary>
		public string S_listStyleImage
		{
			get{ return GetAttribute( StringConstStyleName.listStyleImage);}
			set{ SetAttribute( StringConstStyleName.listStyleImage , value);}
		}
		///<summary>Sets or retrieves a variable that indicates how the list-item marker is drawn relative to the content of the object. </summary>
		public string S_listStylePosition
		{
			get{ return GetAttribute( StringConstStyleName.listStylePosition);}
			set{ SetAttribute( StringConstStyleName.listStylePosition , value);}
		}
		///<summary>Sets or retrieves the predefined type of the line-item marker for the object. </summary>
		public string S_listStyleType
		{
			get{ return GetAttribute( StringConstStyleName.listStyleType);}
			set{ SetAttribute( StringConstStyleName.listStyleType , value);}
		}
		///<summary>Sets or retrieves the width of the top, right, bottom, and left margins of the object. </summary>
		public string S_margin
		{
			get{ return GetAttribute( StringConstStyleName.margin);}
			set{ SetAttribute( StringConstStyleName.margin , value);}
		}
		///<summary>Sets or retrieves the height of the bottom margin of the object. </summary>
		public string S_marginBottom
		{
			get{ return GetAttribute( StringConstStyleName.marginBottom);}
			set{ SetAttribute( StringConstStyleName.marginBottom , value);}
		}
		///<summary>Sets or retrieves the width of the left margin of the object. </summary>
		public string S_marginLeft
		{
			get{ return GetAttribute( StringConstStyleName.marginLeft);}
			set{ SetAttribute( StringConstStyleName.marginLeft , value);}
		}
		///<summary>Sets or retrieves the width of the right margin of the object. </summary>
		public string S_marginRight
		{
			get{ return GetAttribute( StringConstStyleName.marginRight);}
			set{ SetAttribute( StringConstStyleName.marginRight , value);}
		}
		///<summary>Sets or retrieves the height of the top margin of the object. </summary>
		public string S_marginTop
		{
			get{ return GetAttribute( StringConstStyleName.marginTop);}
			set{ SetAttribute( StringConstStyleName.marginTop , value);}
		}
		///<summary>Sets or retrieves the media type. </summary>
		public string S_media
		{
			get{ return GetAttribute( StringConstStyleName.media);}
			set{ SetAttribute( StringConstStyleName.media , value);}
		}
		///<summary>Sets or retrieves the minimum height for an element.</summary>
		public string S_minHeight
		{
			get{ return GetAttribute( StringConstStyleName.minHeight);}
			set{ SetAttribute( StringConstStyleName.minHeight , value);}
		}
		///<summary>Sets or retrieves a value indicating how to manage the content of the object when the content exceeds the height or width of the object.</summary>
		public string S_overflow
		{
			get{ return GetAttribute( StringConstStyleName.overflow);}
			set{ SetAttribute( StringConstStyleName.overflow , value);}
		}
		///<summary>Sets or retrieves how to manage the content of the object when the content exceeds the width of the object.</summary>
		public string S_overflowX
		{
			get{ return GetAttribute( StringConstStyleName.overflowX);}
			set{ SetAttribute( StringConstStyleName.overflowX , value);}
		}
		///<summary>Sets or retrieves how to manage the content of the object when the content exceeds the height of the object.</summary>
		public string S_overflowY
		{
			get{ return GetAttribute( StringConstStyleName.overflowY);}
			set{ SetAttribute( StringConstStyleName.overflowY , value);}
		}
		///<summary>Sets or retrieves the amount of space to insert between the object and its margin or, if there is a border, between the object and its border. </summary>
		public string S_padding
		{
			get{ return GetAttribute( StringConstStyleName.padding);}
			set{ SetAttribute( StringConstStyleName.padding , value);}
		}
		///<summary>Sets or retrieves the amount of space to insert between the bottom border of the object and the content. </summary>
		public string S_paddingBottom
		{
			get{ return GetAttribute( StringConstStyleName.paddingBottom);}
			set{ SetAttribute( StringConstStyleName.paddingBottom , value);}
		}
		///<summary>Sets or retrieves the amount of space to insert between the left border of the object and the content. </summary>
		public string S_paddingLeft
		{
			get{ return GetAttribute( StringConstStyleName.paddingLeft);}
			set{ SetAttribute( StringConstStyleName.paddingLeft , value);}
		}
		///<summary>Sets or retrieves the amount of space to insert between the right border of the object and the content. </summary>
		public string S_paddingRight
		{
			get{ return GetAttribute( StringConstStyleName.paddingRight);}
			set{ SetAttribute( StringConstStyleName.paddingRight , value);}
		}
		///<summary>Sets or retrieves the amount of space to insert between the top border of the object and the content. </summary>
		public string S_paddingTop
		{
			get{ return GetAttribute( StringConstStyleName.paddingTop);}
			set{ SetAttribute( StringConstStyleName.paddingTop , value);}
		}
		///<summary>Sets or retrieves a value indicating whether a page break occurs after the object. </summary>
		public string S_pageBreakAfter
		{
			get{ return GetAttribute( StringConstStyleName.pageBreakAfter);}
			set{ SetAttribute( StringConstStyleName.pageBreakAfter , value);}
		}
		///<summary>Sets or retrieves a string indicating whether a page break occurs before the object.</summary>
		public string S_pageBreakBefore
		{
			get{ return GetAttribute( StringConstStyleName.pageBreakBefore);}
			set{ SetAttribute( StringConstStyleName.pageBreakBefore , value);}
		}
		///<summary>Sets or retrieves the type of positioning used for the object. </summary>
		public string S_position
		{
			get{ return GetAttribute( StringConstStyleName.position);}
			set{ SetAttribute( StringConstStyleName.position , value);}
		}
		///<summary>Sets or retrieves the position of the object relative to the right edge of the next positioned object in the document hierarchy. </summary>
		public string S_right
		{
			get{ return GetAttribute( StringConstStyleName.right);}
			set{ SetAttribute( StringConstStyleName.right , value);}
		}
		///<summary>Sets or retrieves the position of the ruby text specified by the rt object.</summary>
		public string S_rubyAlign
		{
			get{ return GetAttribute( StringConstStyleName.rubyAlign);}
			set{ SetAttribute( StringConstStyleName.rubyAlign , value);}
		}
		///<summary>Sets or retrieves the position of the ruby text specified by the rt object. </summary>
		public string S_rubyOverhang
		{
			get{ return GetAttribute( StringConstStyleName.rubyOverhang);}
			set{ SetAttribute( StringConstStyleName.rubyOverhang , value);}
		}
		///<summary>Sets or retrieves the position of the ruby text specified by the rt object.</summary>
		public string S_rubyPosition
		{
			get{ return GetAttribute( StringConstStyleName.rubyPosition);}
			set{ SetAttribute( StringConstStyleName.rubyPosition , value);}
		}
		///<summary>Sets or retrieves the color of the top and left edges of the scroll box and scroll arrows of a scroll bar.</summary>
		public string S_scrollbar3dLightColor
		{
			get{ return GetAttribute( StringConstStyleName.scrollbar3dLightColor);}
			set{ SetAttribute( StringConstStyleName.scrollbar3dLightColor , value);}
		}
		///<summary>Sets or retrieves the color of the arrow elements of a scroll arrow.</summary>
		public string S_scrollbarArrowColor
		{
			get{ return GetAttribute( StringConstStyleName.scrollbarArrowColor);}
			set{ SetAttribute( StringConstStyleName.scrollbarArrowColor , value);}
		}
		///<summary>Sets or retrieves the color of the main elements of a scroll bar, which include the scroll box, track, and scroll arrows.</summary>
		public string S_scrollbarBaseColor
		{
			get{ return GetAttribute( StringConstStyleName.scrollbarBaseColor);}
			set{ SetAttribute( StringConstStyleName.scrollbarBaseColor , value);}
		}
		///<summary>Sets or retrieves the color of the gutter of a scroll bar.</summary>
		public string S_scrollbarDarkShadowColor
		{
			get{ return GetAttribute( StringConstStyleName.scrollbarDarkShadowColor);}
			set{ SetAttribute( StringConstStyleName.scrollbarDarkShadowColor , value);}
		}
		///<summary>Sets or retrieves the color of the scroll box and scroll arrows of a scroll bar.</summary>
		public string S_scrollbarFaceColor
		{
			get{ return GetAttribute( StringConstStyleName.scrollbarFaceColor);}
			set{ SetAttribute( StringConstStyleName.scrollbarFaceColor , value);}
		}
		///<summary>Sets or retrieves the color of the top and left edges of the scroll box and scroll arrows of a scroll bar.</summary>
		public string S_scrollbarHighlightColor
		{
			get{ return GetAttribute( StringConstStyleName.scrollbarHighlightColor);}
			set{ SetAttribute( StringConstStyleName.scrollbarHighlightColor , value);}
		}
		///<summary>Sets or retrieves the color of the bottom and right edges of the scroll box and scroll arrows of a scroll bar.</summary>
		public string S_scrollbarShadowColor
		{
			get{ return GetAttribute( StringConstStyleName.scrollbarShadowColor);}
			set{ SetAttribute( StringConstStyleName.scrollbarShadowColor , value);}
		}
		///<summary>Sets or retrieves the color of the track element of a scroll bar.</summary>
		public string S_scrollbarTrackColor
		{
			get{ return GetAttribute( StringConstStyleName.scrollbarTrackColor);}
			set{ SetAttribute( StringConstStyleName.scrollbarTrackColor , value);}
		}
		///<summary>Sets or retrieves on which side of the object the text will flow.</summary>
		public string S_styleFloat
		{
			get{ return GetAttribute( StringConstStyleName.styleFloat);}
			set{ SetAttribute( StringConstStyleName.styleFloat , value);}
		}
		///<summary>Sets or retrieves a string that indicates whether the table layout is fixed.</summary>
		public string S_tableLayout
		{
			get{ return GetAttribute( StringConstStyleName.tableLayout);}
			set{ SetAttribute( StringConstStyleName.tableLayout , value);}
		}
		///<summary>Sets or retrieves whether the text in the object is left-aligned, right-aligned, centered, or justified. </summary>
		public string S_textAlign
		{
			get{ return GetAttribute( StringConstStyleName.textAlign);}
			set{ SetAttribute( StringConstStyleName.textAlign , value);}
		}
		///<summary>Sets or retrieves how to align the last line or only line of text in the object.</summary>
		public string S_textAlignLast
		{
			get{ return GetAttribute( StringConstStyleName.textAlignLast);}
			set{ SetAttribute( StringConstStyleName.textAlignLast , value);}
		}
		///<summary>Sets or retrieves the autospacing and narrow space width adjustment of text. </summary>
		public string S_textAutospace
		{
			get{ return GetAttribute( StringConstStyleName.textAutospace);}
			set{ SetAttribute( StringConstStyleName.textAutospace , value);}
		}
		///<summary>Sets or retrieves a value that indicates whether the text in the object has blink, line-through, overline, or underline decorations. </summary>
		public string S_textDecoration
		{
			get{ return GetAttribute( StringConstStyleName.textDecoration);}
			set{ SetAttribute( StringConstStyleName.textDecoration , value);}
		}
		///<summary>Sets or retrieves the indentation of the first line of text in the object. </summary>
		public string S_textIndent
		{
			get{ return GetAttribute( StringConstStyleName.textIndent);}
			set{ SetAttribute( StringConstStyleName.textIndent , value);}
		}
		///<summary>Sets or retrieves the type of alignment used to justify text in the object.</summary>
		public string S_textJustify
		{
			get{ return GetAttribute( StringConstStyleName.textJustify);}
			set{ SetAttribute( StringConstStyleName.textJustify , value);}
		}
		///<summary>Sets or retrieves the ratio of kashida expansion to white space expansion when justifying lines of text in the object.</summary>
		public string S_textKashidaSpace
		{
			get{ return GetAttribute( StringConstStyleName.textKashidaSpace);}
			set{ SetAttribute( StringConstStyleName.textKashidaSpace , value);}
		}
		///<summary>Sets or retrieves a value that indicates whether to render ellipses(...) to indicate text overflow.</summary>
		public string S_textOverflow
		{
			get{ return GetAttribute( StringConstStyleName.textOverflow);}
			set{ SetAttribute( StringConstStyleName.textOverflow , value);}
		}
		///<summary>Sets or retrieves the rendering of the text in the object. </summary>
		public string S_textTransform
		{
			get{ return GetAttribute( StringConstStyleName.textTransform);}
			set{ SetAttribute( StringConstStyleName.textTransform , value);}
		}
		///<summary>Sets or retrieves the position of the underline decoration that is set through the textDecoration property of the object.</summary>
		public string S_textUnderlinePosition
		{
			get{ return GetAttribute( StringConstStyleName.textUnderlinePosition);}
			set{ SetAttribute( StringConstStyleName.textUnderlinePosition , value);}
		}
		///<summary>Sets or retrieves the position of the object relative to the top of the next positioned object in the document hierarchy. </summary>
		public string S_top
		{
			get{ return GetAttribute( StringConstStyleName.top);}
			set{ SetAttribute( StringConstStyleName.top , value);}
		}
		///<summary>Sets or retrieves the level of embedding with respect to the bidirectional algorithm.</summary>
		public string S_unicodeBidi
		{
			get{ return GetAttribute( StringConstStyleName.unicodeBidi);}
			set{ SetAttribute( StringConstStyleName.unicodeBidi , value);}
		}
		///<summary>Sets or retrieves the vertical alignment of the object. </summary>
		public string S_verticalAlign
		{
			get{ return GetAttribute( StringConstStyleName.verticalAlign);}
			set{ SetAttribute( StringConstStyleName.verticalAlign , value);}
		}
		///<summary>Sets or retrieves whether the content of the object is displayed. </summary>
		public string S_visibility
		{
			get{ return GetAttribute( StringConstStyleName.visibility);}
			set{ SetAttribute( StringConstStyleName.visibility , value);}
		}
		///<summary>Sets or retrieves a value that indicates whether lines are automatically broken inside the object.</summary>
		public string S_whiteSpace
		{
			get{ return GetAttribute( StringConstStyleName.whiteSpace);}
			set{ SetAttribute( StringConstStyleName.whiteSpace , value);}
		}
		///<summary>Sets or retrieves the width of the object. </summary>
		public string S_width
		{
			get{ return GetAttribute( StringConstStyleName.width);}
			set{ SetAttribute( StringConstStyleName.width , value);}
		}
		///<summary>Sets or retrieves line-breaking behavior within words, particularly where multiple languages appear in the object.</summary>
		public string S_wordBreak
		{
			get{ return GetAttribute( StringConstStyleName.wordBreak);}
			set{ SetAttribute( StringConstStyleName.wordBreak , value);}
		}
		///<summary>Sets or retrieves the amount of additional space between words in the object. </summary>
		public string S_wordSpacing
		{
			get{ return GetAttribute( StringConstStyleName.wordSpacing);}
			set{ SetAttribute( StringConstStyleName.wordSpacing , value);}
		}
		///<summary>Sets or retrieves whether to break words when the content exceeds the boundaries of its container.</summary>
		public string S_wordWrap
		{
			get{ return GetAttribute( StringConstStyleName.wordWrap);}
			set{ SetAttribute( StringConstStyleName.wordWrap , value);}
		}
		///<summary>Sets or retrieves the direction and flow of the content in the object.</summary>
		public string S_writingMode
		{
			get{ return GetAttribute( StringConstStyleName.writingMode);}
			set{ SetAttribute( StringConstStyleName.writingMode , value);}
		}
		///<summary>Sets or retrieves the stacking order of positioned objects. </summary>
		public string S_zIndex
		{
			get{ return GetAttribute( StringConstStyleName.zIndex);}
			set{ SetAttribute( StringConstStyleName.zIndex , value);}
		}
		///<summary>Sets or retrieves the magnification scale of the object.</summary>
		public string S_zoom
		{
			get{ return GetAttribute( StringConstStyleName.zoom);}
			set{ SetAttribute( StringConstStyleName.zoom , value);}
		}
		#endregion
	}//public class HTMLStyle 
}