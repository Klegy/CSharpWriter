/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using DCSoft.WinForms  ;
using DCSoft.Common;
using DCSoft.Drawing;
using DCSoft.CSharpWriter.Dom ;
using System.Text;
using System.Collections;
using System.Collections.Generic ;
using System.ComponentModel ;

using System.Drawing;
using System.Drawing.Drawing2D;

namespace DCSoft.CSharpWriter.Html
{
	/// <summary>
	/// HTML文档书写器
	/// </summary>
	public class XHtmlWriter
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public XHtmlWriter()
		{
			this.Reset();
		}

		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="indent">师父进行缩进处理</param>
		public XHtmlWriter( bool indent )
		{
			this.Options.Indent = indent ;
			this.Reset();
		}

        private DomDocument _MainDocument = null;
        /// <summary>
        /// 主文档
        /// </summary>
        public DomDocument MainDocument
        {
            get
            {
                if (_MainDocument == null)
                {
                    if (_Documents != null)
                    {
                        return _Documents.FirstDocument;
                    }
                }
                return _MainDocument; 
            }
            set
            {
                _MainDocument = value; 
            }
        }

        private DomDocumentList _Documents = new DomDocumentList();
        /// <summary>
        /// 文档对象列表
        /// </summary>
        public DomDocumentList Documents
        {
            get { return _Documents; }
            set { _Documents = value; }
        }

		/// <summary>
		/// 默认字体
		/// </summary>
		protected System.Drawing.Font myDefaultFont
            = System.Windows.Forms.Control.DefaultFont ;
		/// <summary>
		/// 默认字体
		/// </summary>
		public System.Drawing.Font DefaultFont
		{
			get{ return myDefaultFont ;}
			set{ myDefaultFont = value;}
		}

        private HtmlWriterOptions _Options = new HtmlWriterOptions();
        /// <summary>
        /// 输出HTML代码的选项
        /// </summary>
        public HtmlWriterOptions Options
        {
            get 
            {
                if (_Options == null)
                {
                    _Options = new HtmlWriterOptions();
                }
                return _Options; 
            }
            set
            {
                _Options = value; 
            }
        }

		/// <summary>
		/// 输出文本使用的编码格式
		/// </summary>
		protected System.Text.Encoding myContentEncoding =
			System.Text.Encoding.GetEncoding( 936 );
		/// <summary>
		/// 输出文本使用的编码格式
		/// </summary>
		public System.Text.Encoding ContentEncoding
		{
			get{ return myContentEncoding ;}
			set{ myContentEncoding = value;}
		}

		protected string strTitle = "";
		/// <summary>
		/// 报表标题
		/// </summary>
		public string Title
		{
			get{ return strTitle ;}
			set{ strTitle = value;}
		}

		/// <summary>
		/// 只包含选择的部分
		/// </summary>
		protected bool bolIncludeSelectionOnly = false;
		/// <summary>
		/// 只包含选择的部分
		/// </summary>
		public bool IncludeSelectionOndly
		{
			get
            {
                return bolIncludeSelectionOnly ;
            }
			set
            {
                bolIncludeSelectionOnly = value;
            }
		}

        private Rectangle _ClipRectangle = Rectangle.Empty;
        /// <summary>
        /// 输出区域的剪切矩形
        /// </summary>
        public Rectangle ClipRectangle
        {
            get { return _ClipRectangle; }
            set { _ClipRectangle = value; }
        }


        private Dictionary<DocumentContentStyle, string> _CssStyleStrings
            = new Dictionary<DocumentContentStyle, string>();

        /// <summary>
		/// HTML文档字符串对象
		/// </summary>
		protected System.IO.StringWriter myStrWriter = new System.IO.StringWriter();
		/// <summary>
		/// 内置的XML书写器
		/// </summary>
		protected System.Xml.XmlTextWriter myWriter = null;

		/// <summary>
		/// 重新设置对象,开始输出新的HTML文档
		/// </summary>
        public void Reset()
        {
            myStrWriter = new System.IO.StringWriter();
            myWriter = new System.Xml.XmlTextWriter(myStrWriter);
            if ( this.Options.Indent )
            {
                myWriter.Formatting = System.Xml.Formatting.Indented;
                myWriter.Indentation = 3;
                myWriter.IndentChar = ' ';
            }
            else
            {
                myWriter.Formatting = System.Xml.Formatting.None;
            }
        }

		/// <summary>
		/// 开始创建元素
		/// </summary>
		/// <param name="strName">元素名称</param>
		public void WriteStartElement( string strName )
		{
			myWriter.WriteStartElement( strName );
		}
		/// <summary>
		/// 结束创建元素
		/// </summary>
		public void WriteEndElement( )
		{
			myWriter.WriteEndElement();
		}
		/// <summary>
		/// 输出一段文本
		/// </summary>
		/// <param name="Value">文本内容</param>
		public void WriteString( string Value )
		{
			myWriter.WriteString( Value );
		}
		/// <summary>
		/// 输出可用属性
		/// </summary>
		/// <param name="Enabled">当前元素是否可用</param>
		public void WriteEnabledAttribute( bool Enabled )
		{
			if( Enabled == false )
				myWriter.WriteAttributeString("disabled" , "true");
		}
		/// <summary>
		/// 输出一个属性
		/// </summary>
		/// <param name="strName">属性名</param>
		/// <param name="strValue">属性值</param>
		public void WriteAttributeString( string strName , string strValue )
		{
			myWriter.WriteAttributeString( strName , strValue );
		}
		/// <summary>
		/// 输出一个元素
		/// </summary>
		/// <param name="strName">元素名称</param>
		/// <param name="strValue">元素内容</param>
		public void WriteElementString( string strName , string strValue )
		{
			myWriter.WriteElementString( strName , strValue );
		}


		/// <summary>
		/// 输出空格
		/// </summary>
		/// <param name="Length">空格个数</param>
		protected virtual void WriteBlank(  int Length )
		{
            if (Length > 0)
            {
                if (this.Options.Indent)
                {
                    myWriter.WriteStartElement("span");
                }
                for (int iCount = 0; iCount < Length; iCount++)
                {
                    myWriter.WriteRaw("&nbsp;");
                }
                if (this.Options.Indent)
                {
                    myWriter.WriteEndElement();
                }
            }
		}
		
		/// <summary>
		/// 输出纯文本数据
		/// </summary>
		/// <param name="strText">文本内容</param>
		/// <param name="font">字体</param>
		/// <param name="TextColor">文本颜色</param>
		public void WriteText( 
			string strText , 
			System.Drawing.Font font ,
			System.Drawing.Color TextColor )
		{
            if (font == null)
            {
                return;
            }
			System.Drawing.Font df = myDefaultFont ;
			myWriter.WriteStartElement("font");
			if( TextColor.ToArgb() != System.Drawing.Color.Black.ToArgb())
				myWriter.WriteAttributeString(
					"color" , 
					ColorToString( TextColor ));
			if( font != null )
			{
                if (myDefaultFont == null || myDefaultFont.Name != font.Name)
                {
                    myWriter.WriteAttributeString("face", font.Name);
                }
                if (myDefaultFont == null || myDefaultFont.Size != font.Size)
                {
                    myWriter.WriteAttributeString("style", "font-size:" + font.Size + "pt");
                }
                if (font.Bold)
                {
                    myWriter.WriteStartElement("b");
                }
                if (font.Italic)
                {
                    myWriter.WriteStartElement("i");
                }
			}
			WriteText( strText );
			if( font != null )
			{
                if (font.Italic)
                {
                    myWriter.WriteEndElement();
                }
                if (font.Bold)
                {
                    myWriter.WriteEndElement();
                }
			}
			myWriter.WriteEndElement();
		}

		/// <summary>
		/// 输出纯文本数据
		/// </summary>
		/// <param name="strText">文本内容</param>
        public void WriteText(string strText)
        {
            if (strText != null)
            {
                strText = System.Web.HttpUtility.HtmlEncode(strText);
                strText = strText.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
                strText = strText.Replace(" ", "&nbsp;");
                strText = strText.Replace("\r\n", "<br />");
                if (strText.Length == 0)
                {
                    myWriter.WriteString(" ");
                }
                else
                {
                    if (this.Options.OutputXML)
                    {
                        myWriter.WriteString(strText);
                    }
                    else
                    {
                        myWriter.WriteRaw(strText);
                    }
                }
            }
            else
            {
                myWriter.WriteString(" ");
            }
        }

		protected System.Collections.Hashtable myImages = new System.Collections.Hashtable();
		/// <summary>
		/// 添加HTML图片数据
		/// </summary>
		/// <param name="img">图片数据对象</param>
		/// <returns>引用图片的路径字符串</returns>
		public string AddImage(
            byte[] bs ,
            System.Drawing.Imaging.ImageFormat format )
		{
			if( bs == null || bs.Length == 0 )
				return null;

			foreach( string name in myImages.Keys )
			{
				if( myImages[ name ] == bs )
					return name ;
			}
			if( format == null )
			{
				if( FileHeaderHelper.HasBMPHeader( bs ))
					format = System.Drawing.Imaging.ImageFormat.Bmp ;
				else if( FileHeaderHelper.HasGIFHeader( bs ))
					format = System.Drawing.Imaging.ImageFormat.Gif ;
				else if( FileHeaderHelper.HasJpegHeader( bs ))
					format = System.Drawing.Imaging.ImageFormat.Jpeg ;
				else if( FileHeaderHelper.HasPNGHeader( bs ))
					format = System.Drawing.Imaging.ImageFormat.Png ;
			}
            string ext = ImageHelper.GetFileExtersion(format);
			if( ext == null )
				ext = ".bmp";
			string strFileName = myImages.Count + ext ;
			myImages[ strFileName ] = bs ;
			return strFileName ;
		}


		/// <summary>
		/// 添加HTML图片数据
		/// </summary>
		/// <param name="img">图片对象</param>
		/// <param name="format">要保存的格式</param>
		/// <returns>引用图片的路径字符串</returns>
		public string AddImage( System.Drawing.Image img , System.Drawing.Imaging.ImageFormat format )
		{
			if( img == null )
				return null;
			byte[] bs = null;
			using( System.IO.MemoryStream ms = new System.IO.MemoryStream())
			{
				if( format == null )
				{
					format = img.RawFormat ;
					if( format == null )
						format = System.Drawing.Imaging.ImageFormat.Jpeg ;
				}
				img.Save( ms , format );
				ms.Close();
				bs = ms.ToArray();
			}
			string ext = ImageHelper.GetFileExtersion( format );
			string strName = myImages.Count + ext ;
			myImages[ strName ] = bs ;
			return strName ;
		}

		/// <summary>
		/// 保存所有图片到指定目录下
		/// </summary>
		/// <param name="strPath">指定的目录</param>
		public void SaveAllImages( string strPath )
		{
			foreach( string strName in myImages.Keys )
			{
				string strFileName = System.IO.Path.Combine( strPath , strName );
				byte[] bs = ( byte[] ) myImages[ strName ] ;
				using( System.IO.FileStream stream = new System.IO.FileStream( strFileName , System.IO.FileMode.Create , System.IO.FileAccess.Write ))
				{
					stream.Write( bs , 0 , bs.Length );
					stream.Close();
				}
			}
		}

        /// <summary>
        /// 获得CSS样式字符串
        /// </summary>
        /// <param name="style">文档样式对象</param>
        /// <returns>CSS样式字符串</returns>
        public string GetStyleString(DocumentContentStyle style)
        {
            if (style == null)
            {
                throw new ArgumentNullException("style");
            }
            GraphicsUnit documentUnit = GraphicsUnit.Document ;
            if( this.MainDocument != null )
            {
                documentUnit = this.MainDocument.DocumentGraphicsUnit ;
            }
            StringBuilder result = new StringBuilder();

            // 边框样式
            string borderStyleString = "solid";
            switch (style.BorderStyle)
            {
                case DashStyle.Solid :
                    borderStyleString = "solid";
                    break;
                case DashStyle.Dash :
                    borderStyleString = "dashed";
                    break;
                case DashStyle.DashDot :
                    borderStyleString = "dotted";
                    break;
                case  DashStyle.DashDotDot :
                    borderStyleString = "dotted";
                    break;
                case DashStyle.Dot :
                    borderStyleString = "dotted";
                    break;
            }
            borderStyleString = "1" + borderStyleString + " " + ColorToString(style.BorderColor);
            if (style.BorderLeft
                && style.BorderTop
                && style.BorderRight
                && style.BorderBottom
                && style.BorderWidth > 0 )
            {
                // 输出完整的边框样式
                result.Append("border:" + borderStyleString);
                borderStyleString = null;
            }

            string[] names = XDependencyObject.GetExistPropertyNames( style );
            foreach (string name in names )
            {
                string strItem = null ;
                switch (name)
                {
                    case ContentStyle.PropertyName_BorderLeft:
                        {
                            // 左边框线
                            if ( style.BorderLeft && borderStyleString != null && style.BorderWidth > 0 )
                            {
                                strItem = "border-left:" + borderStyleString;
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_BorderTop:
                        {
                            // 上边框线
                            if (style.BorderTop && borderStyleString != null && style.BorderWidth > 0)
                            {
                                strItem = "border-top:" + borderStyleString;
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_BorderRight:
                        {
                            // 右边框线
                            if (style.BorderRight && borderStyleString != null && style.BorderWidth > 0)
                            {
                                strItem = "border-right:" + borderStyleString;
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_BorderBottom:
                        {
                            // 下边框线
                            if (style.BorderBottom && borderStyleString != null && style.BorderWidth > 0)
                            {
                                strItem = "border-bottom:" + borderStyleString;
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_Align:
                        {
                            // 文本水平对齐方式
                            switch (style.Align)
                            {
                                case DocumentContentAlignment.Left :
                                    strItem = "text-align:left";
                                    break;
                                case DocumentContentAlignment.Center :
                                    strItem = "text-align:center";
                                    break;
                                case DocumentContentAlignment.Right :
                                    strItem = "text-align:right";
                                    break;
                                case DocumentContentAlignment.Justify :
                                    strItem = "text-align:justify";
                                    break;
                                default :
                                    strItem = "text-align:left";
                                    break;
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_BackgroundColor:
                        {
                            // 背景色
                            strItem = ColorToString(style.BackgroundColor);
                        }
                        break;
                    case ContentStyle.PropertyName_BackgroundImage:
                        {
                            // 背景图片
                            XImageValue img = style.BackgroundImage;
                            if (img != null && img.HasContent)
                            {
                                string src = AddImage(img.Value, null);
                                strItem = "background-image:url(" + src + ")";
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_BackgroundPosition:
                        {
                            // 背景位置
                            switch (style.BackgroundPosition)
                            {
                                case ContentAlignment.TopLeft:
                                    strItem = "top left";
                                    break;
                                case ContentAlignment.TopCenter:
                                    strItem = "top center";
                                    break;
                                case ContentAlignment.TopRight:
                                    strItem = "top right";
                                    break;
                                case ContentAlignment.MiddleLeft:
                                    strItem = "center left";
                                    break;
                                case ContentAlignment.MiddleCenter:
                                    strItem = "center center";
                                    break;
                                case ContentAlignment.MiddleRight:
                                    strItem = "center right";
                                    break;
                                case ContentAlignment.BottomLeft:
                                    strItem = "bottom left";
                                    break;
                                case ContentAlignment.BottomCenter:
                                    strItem = "bottom center";
                                    break;
                                case ContentAlignment.BottomRight:
                                    strItem = "bottom right";
                                    break;
                            }
                            strItem = "background-position:" + strItem;
                        }
                        break;
                    case ContentStyle.PropertyName_BackgroundPositionX:
                        {
                            // 背景位置X坐标
                            strItem = "background-position-x:" + GraphicsUnitConvert.ToCSSLength(
                                style.BackgroundPositionX, 
                                documentUnit, 
                                CssLengthUnit.Pixels);
                        }
                        break;
                    case ContentStyle.PropertyName_BackgroundPositionY :
                        {
                            // 背景位置Y坐标
                            strItem = "background-position-y:" + GraphicsUnitConvert.ToCSSLength(
                                style.BackgroundPositionY, 
                                documentUnit,
                                CssLengthUnit.Pixels);
                        }
                        break;
                    case ContentStyle.PropertyName_BackgroundRepeat:
                        {
                            // 背景重复方式
                            if (style.BackgroundRepeat)
                            {
                                strItem = "background-repeat:repeat";
                            }
                            else
                            {
                                strItem = "background-repeat:no-repeat";
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_Bold:
                        {
                            // 粗体
                            if (style.Bold)
                            {
                                strItem = "font-weight:bold";
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_Color  :
                        {
                            // 文本颜色
                            if (style.Color.A != 0)
                            {
                                strItem = "color:" + ColorToString(style.Color);
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_FirstLineIndent:
                        {
                            // 首行缩进
                            if (Math.Abs(style.FirstLineIndent) > 0.05)
                            {
                                strItem = "text-indent:" + GraphicsUnitConvert.ToCSSLength(
                                    style.FirstLineIndent,
                                    documentUnit,
                                    CssLengthUnit.Pixels);
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_FontName:
                        {
                            // 字体名称
                            strItem = "font-family:" + style.FontName;
                        }
                        break;
                    case ContentStyle.PropertyName_FontSize:
                        {
                            // 字体大小
                            strItem = "font-size:" + style.FontSize.ToString() + " pt";
                        }
                        break;
                    case ContentStyle.PropertyName_Italic:
                        {
                            // 斜体字体
                            if (style.Italic)
                            {
                                strItem = "font-style:italic";
                            }
                        }
                        break;
                    //case ContentStyle.PropertyName_LineSpacing:
                    //    {
                    //        // 行间距
                    //        float ls = style.LineSpacing;
                    //        if (Math.Abs(ls) > 0.05)
                    //        {

                    //        }
                    //    }
                    //    break;
                    case ContentStyle.PropertyName_MarginBottom:
                        {
                            // 下外边距
                            if (Math.Abs(style.MarginBottom) > 0.05)
                            {
                                strItem = "margin-bottom:" + GraphicsUnitConvert.ToCSSLength(
                                    style.MarginBottom, 
                                    documentUnit,
                                    CssLengthUnit.Pixels);
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_MarginTop :
                        {
                            // 上外边距
                            if (Math.Abs(style.MarginTop) > 0.05)
                            {
                                strItem = "margin-top:" + GraphicsUnitConvert.ToCSSLength(
                                    style.MarginTop,
                                    documentUnit,
                                    CssLengthUnit.Pixels);
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_MarginLeft:
                        {
                            // 左外边距
                            if (Math.Abs(style.MarginLeft) > 0.05)
                            {
                                strItem = "margin-left:" + GraphicsUnitConvert.ToCSSLength(
                                    style.MarginLeft,
                                    documentUnit,
                                    CssLengthUnit.Pixels);
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_MarginRight:
                        {
                            // 右外边距
                            if (Math.Abs(style.MarginRight) > 0.05)
                            {
                                strItem = "margin-right:" + GraphicsUnitConvert.ToCSSLength(
                                    style.MarginRight,
                                    documentUnit,
                                    CssLengthUnit.Pixels);
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_PaddingBottom:
                        {
                            // 底内边距
                            if (Math.Abs(style.PaddingBottom) > 0.05)
                            {
                                strItem = "padding-bottom:" + GraphicsUnitConvert.ToCSSLength(
                                    style.PaddingBottom,
                                    documentUnit,
                                    CssLengthUnit.Pixels);
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_PaddingTop:
                        {
                            // 上内边距
                            if (Math.Abs(style.PaddingTop) > 0.05)
                            {
                                strItem = "padding-top:" + GraphicsUnitConvert.ToCSSLength(
                                    style.PaddingTop,
                                    documentUnit,
                                    CssLengthUnit.Pixels);
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_PaddingRight:
                        {
                            // 右内边距
                            if (Math.Abs(style.PaddingRight) > 0.05)
                            {
                                strItem = "padding-right:" + GraphicsUnitConvert.ToCSSLength(
                                    style.PaddingRight,
                                    documentUnit,
                                    CssLengthUnit.Pixels);
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_PaddingLeft:
                        {
                            // 左内边距
                            if (Math.Abs(style.PaddingLeft) > 0.05)
                            {
                                strItem = "padding-left:" + GraphicsUnitConvert.ToCSSLength(
                                    style.PaddingLeft,
                                    documentUnit,
                                    CssLengthUnit.Pixels);
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_PageBreakAfter :
                        {
                            // 后置强制分页
                            if( style.PageBreakAfter )
                            {
                                strItem = "page-break-after:always";
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_PageBreakBefore :
                        {
                            // 前置强制分页
                            if( style.PageBreakBefore )
                            {
                                strItem = "page-break-before:always";
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_RightToLeft:
                        {
                            // 从右到左排版
                            if (style.RightToLeft)
                            {
                                strItem = "direction:rtl";
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_Spacing:
                        {
                            // 字符间距
                            if (Math.Abs(style.Spacing) > 0.05)
                            {
                                strItem = "letter-spacing:" + GraphicsUnitConvert.ToCSSLength(
                                    style.Spacing,
                                    documentUnit,
                                    CssLengthUnit.Pixels);
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_Strikeout:
                        {
                            // 删除线
                            if (style.Strikeout)
                            {
                                strItem = "text-decoration:line-through";
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_Underline:
                        {
                            // 下划线
                            if (style.Underline)
                            {
                                strItem = "text-decoration:underline";
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_VertialText:
                        {
                            // 垂直显示文本
                            if (style.VertialText)
                            {
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_VerticalAlign:
                        {
                            // 垂直方向对齐方式
                            switch (style.VerticalAlign)
                            {
                                case VerticalAlignStyle.Top :
                                    strItem = "vertical:top";
                                    break;
                                case VerticalAlignStyle.Middle :
                                    strItem = "vertical:middle";
                                    break;
                                case VerticalAlignStyle.Bottom :
                                    strItem = "vertical:bottom";
                                    break;
                                case VerticalAlignStyle.Justify :
                                    strItem = "vertical:middle";
                                    break;
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_Visible:
                        {
                            // 可见性
                            if (style.Visible == false)
                            {
                                strItem = "visibility:hidden";
                            }
                        }
                        break;
                    case ContentStyle.PropertyName_Zoom:
                        {
                            // 缩放比率
                            strItem = "zoom:" + style.Zoom;
                        }
                        break;
                }//switch
                if (strItem != null)
                {
                    if (result.Length > 0)
                    {
                        result.Append(";");
                    }
                    result.Append(strItem);
                }
            }//foreach
            return result.ToString();
        }

		/// <summary>
		/// 样式字符串字典
		/// </summary>
		protected System.Collections.Specialized.StringDictionary myStyles =
			new System.Collections.Specialized.StringDictionary();

		/// <summary>
		/// 获得指定样式字符串的名称
		/// </summary>
		/// <param name="strValue">样式字符串</param>
		/// <returns>样式名称</returns>
		private string GetStyleName( string strValue )
		{
			foreach( string strName in myStyles.Keys )
			{
				if( string.Compare( strValue , myStyles[ strName ] , false ) == 0 )
					return strName ;
			}
			string strClassName = "x_" + myStyles.Count ;
			myStyles[ strClassName ] = strValue ;
			return strClassName ;
		}

		private System.Text.StringBuilder myStyleString = null;
		/// <summary>
		/// 开始输出样式
		/// </summary>
		public void BeginWriteStyle()
		{
			myStyleString = new System.Text.StringBuilder();
		}
		/// <summary>
		/// 结束输出HTML样式并填写元素的 style 属性值
		/// </summary>
		public void EndWriteStyle( )
		{
			if( myStyleString != null )
			{
				myWriter.WriteAttributeString( "style" , myStyleString.ToString());
			}
			myStyleString = null;
		}

		/// <summary>
		/// 结束输出HTML样式并填写元素 class 属性值
		/// </summary>
		public void EndWriteStyleClass()
		{
			if( myStyleString != null && myStyleString.Length > 0 )
			{
				string strStyle = myStyleString.ToString();
				string strName = GetStyleName( strStyle );
				myWriter.WriteAttributeString("class" , strName );
				//myWriter.WriteAttributeString( "style" , myStyleString.ToString());
			}
			myStyleString = null;
		}

		/// <summary>
		/// 使用内置字典填写HTML样式的 class 属性值
		/// </summary>
		/// <param name="strStyle">HTML样式字符串</param>
		public void WriteStyleClassName( string strStyle )
		{
			string strName = GetStyleName( strStyle );
			if( strName != null )
				myWriter.WriteAttributeString("class" , strName );
		}

		/// <summary>
		/// 内部使用的HTML样式列表
		/// </summary>
		public string DocumentStyles
		{
			get
			{
				if( myStyles.Count == 0 )
					return "";
				System.Text.StringBuilder myStr = new System.Text.StringBuilder();
				foreach( string strName in myStyles.Keys )
				{
					myStr.Append( System.Environment.NewLine);
					myStr.Append( "   ." + strName + "{" + myStyles[ strName ] + "}");
				}
				myStr.Append( System.Environment.NewLine);
				return myStr.ToString();
			}
		}

		
		/// <summary>
		/// 输出边框样式
		/// </summary>
		/// <param name="style">边框样式</param>
		/// <param name="RaiseStyle">是否是隆起样式</param>
		public void StyleWriteBorderStyle( System.Windows.Forms.BorderStyle style , bool RaiseStyle )
		{
			if( style == System.Windows.Forms.BorderStyle.None )
				return ;
			if( style == System.Windows.Forms.BorderStyle.FixedSingle )
				AddStyleStringItem( "border" , "1 solid black");
			if( style == System.Windows.Forms.BorderStyle.Fixed3D )
			{
				if( RaiseStyle )
					AddStyleStringItem( "border" , "2 outset control");
				else
					AddStyleStringItem( "border" , "2 inset control");
			}
		}
		/// <summary>
		/// 添加一个样式项目
		/// </summary>
		/// <param name="strName">样式名称</param>
		/// <param name="strValue">项目值</param>
		public void StyleWriteItem( string strName , string strValue )
		{
			if( myStyleString != null )
			{
				if( myStyleString.Length > 0 )
					myStyleString.Append(";");
				myStyleString.Append( strName );
				myStyleString.Append( ":" );
				myStyleString.Append( System.Web.HttpUtility.HtmlEncode( strValue ) );
			}
		}
		/// <summary>
		/// 输出一个样式项目
		/// </summary>
		/// <param name="strItem">HTML样式项目,格式为 样式名称:样式内容</param>
		public void StyleWriteItem( string strItem )
		{
			if( myStyleString != null )
			{
				if( myStyleString.Length > 0 )
					myStyleString.Append(';');
				myStyleString.Append( strItem );
			}
		}

		/// <summary>
		/// 设置鼠标光标样式
		/// </summary>
		/// <param name="Cursor">鼠标光标对象</param>
		public void StyleWriteCursor( System.Windows.Forms.Cursor Cursor )
		{
			AddStyleStringItem( "cursor" , GetCursorName( Cursor ) );
		}
		/// <summary>
		/// 设置背景色样式
		/// </summary>
		/// <param name="BackColor">背景色</param>
		public void StyleWriteBackColor( System.Drawing.Color BackColor )
		{
			if( BackColor.A != 0 )
			{
				AddStyleStringItem( "background" , ColorToString( BackColor ));
			}
		}
		/// <summary>
		/// 输出大小样式
		/// </summary>
		/// <param name="width">象素宽度</param>
		/// <param name="height">象素高度</param>
		public void StyleWriteSize( int width , int height )
		{
			if( width >0 )
				AddStyleStringItem( "width" , width.ToString() + " px");
			if( height > 0 )
				AddStyleStringItem( "height" , height.ToString() + " px");
		}
		/// <summary>
		/// 输出字体样式
		/// </summary>
		/// <param name="font">字体对象</param>
		/// <param name="TextColor">文本颜色</param>
		public void StyleWriteFont( System.Drawing.Font font , System.Drawing.Color TextColor )
		{
			System.Drawing.Font df = myDefaultFont ;
			if( df == null )
				df = System.Windows.Forms.Control.DefaultFont ;
			if( TextColor.A != 0 && TextColor.ToArgb() != System.Drawing.Color.Black.ToArgb() )
			{
				AddStyleStringItem( "color" , ColorToString( TextColor ));
			}
			if( font != null )
			{
				if( font.Name != df.Name )
				{
					AddStyleStringItem( "font-family" , font.Name );
				}
				if( font.Size != df.Size )
				{
					AddStyleStringItem( "font-size" , font.Size.ToString() + " pt");
				}
				if( font.Italic )
					AddStyleStringItem( "font-style" , "italic");
				if( font.Bold )
					AddStyleStringItem( "font-weight" , "bold");
				if( font.Underline )
					AddStyleStringItem( "text-decoration" , "underline" );
				else if( font.Strikeout )
					AddStyleStringItem( "text-decoration" , "line-through" );
			}
		}

        /// <summary>
        /// 添加一个样式内容
        /// </summary>
        /// <param name="name">样式名称</param>
        /// <param name="Value">样式数值</param>
		public void AddStyleStringItem( string name , string Value )
		{
			if( myStyleString.Length > 0 )
				myStyleString.Append(";");
			myStyleString.Append( name );
			myStyleString.Append( ":" );
			myStyleString.Append( System.Web.HttpUtility.HtmlEncode( Value ) );
		}

		/// <summary>
		/// 输出边距信息
		/// </summary>
		/// <param name="LeftMargin">左边距</param>
		/// <param name="TopMargin">顶边距</param>
		/// <param name="RightMargin">右边距</param>
		/// <param name="BottomMargin">底边距</param>
		public void StyleWritePadding( int LeftMargin , int TopMargin , int RightMargin , int BottomMargin )
		{
			if( LeftMargin != 0 )
				this.AddStyleStringItem("padding-left" , LeftMargin.ToString());
			if( TopMargin != 0 )
				this.AddStyleStringItem("padding-top" , TopMargin.ToString());
			if( RightMargin != 0 )
				this.AddStyleStringItem("padding-right" , RightMargin.ToString());
			if( BottomMargin != 0 )
				this.AddStyleStringItem("padding-bottom" , BottomMargin.ToString());
		}
 
		/// <summary>
		/// 输出位置信息
		/// </summary>
		/// <param name="Left">左端位置</param>
		/// <param name="Top">顶端位置</param>
		public void StyleWriteAbsolutePosition( int Left , int Top )
		{
			this.AddStyleStringItem("position" , "absolute");
			this.AddStyleStringItem("left" , Left.ToString());
			this.AddStyleStringItem("top" , Top.ToString());
		}
 		
		/// <summary>
		/// 输出文本对齐方式信息
		/// </summary>
		/// <param name="align">对齐方式</param>
		public void StyleWriteAlign( System.Drawing.StringAlignment align )
		{
			if( myStyleString.Length > 0 ) myStyleString.Append(';');
			myStyleString.Append("text-align:");
			if( align == System.Drawing.StringAlignment.Far )
				myStyleString.Append( "right");
			else if( align == System.Drawing.StringAlignment.Center )
				myStyleString.Append( "center");
			else 
				myStyleString.Append( "left");
		}
		/// <summary>
		/// 输出文本对齐方式信息
		/// </summary>
		/// <param name="align">对齐方式</param>
		public void StyleWriteAlign( DocumentContentAlignment align )
		{
			if( myStyleString.Length > 0 ) myStyleString.Append(';');
			myStyleString.Append("text-align:");
			if( align == DocumentContentAlignment.Left )
				myStyleString.Append("left");
			else if( align == DocumentContentAlignment.Center )
				myStyleString.Append("center");
			else if( align == DocumentContentAlignment.Right )
				myStyleString.Append("right");
			else if( align == DocumentContentAlignment.Justify )
				myStyleString.Append("justify");
			else
				myStyleString.Append("left");
		}
		/// <summary>
		/// 输出文本垂直对齐方式
		/// </summary>
		/// <param name="align">垂直对齐方式</param>
		public void StyleWriteVAlign( System.Drawing.StringAlignment align )
		{
			if( myStyleString.Length > 0 ) myStyleString.Append(';');
			myStyleString.Append("vertical-align:");
			if( align == System.Drawing.StringAlignment.Near )
				myStyleString.Append("top");
			else if( align == System.Drawing .StringAlignment.Center )
				myStyleString.Append("middle");
			else if( align == System.Drawing.StringAlignment.Far )
				myStyleString.Append("bottom");
			else
				myStyleString.Append("top");
		}
		/// <summary>
		/// 输出文本垂直对齐方式
		/// </summary>
		/// <param name="align">垂直对齐方式</param>
		public void StyleWriteVAlign( DocumentContentAlignment align )
		{
			if( myStyleString.Length > 0 ) myStyleString.Append(';');
			myStyleString.Append("vertical-align:");
			if( align == DocumentContentAlignment.Left )
				myStyleString.Append( "top");
			else if( align == DocumentContentAlignment.Center )
				myStyleString.Append("middle");
			else if( align == DocumentContentAlignment.Right )
				myStyleString.Append("bottom");
			else 
				myStyleString.Append("top");
		}

		
		/// <summary>
		/// 返回生成的所有的字符串数据
		/// </summary>
		/// <returns>字符串数据</returns>
		public override string ToString()
		{
			myWriter.Flush();
			string txt = myStrWriter.ToString();
			txt = RemoveXMLDeclear( txt );
			txt = "<style>" + this.DocumentStyles 
				+ "</style>" + System.Environment.NewLine + txt ;
			return RemoveXMLDeclear( txt );
		}

		/// <summary>
		/// 获得完整的HTML文档字符串
		/// </summary>
		/// <returns>HTML字符串</returns>
		public string GetHtmlDocumentString()
		{
			return this.InnerGetHtmlDocumentString( false );
		}

		
		#region 输出结果的函数群 **********************************************

		/// <summary>
		/// 将报表文档以 MS Word 的文档格式输出到指定的流中
		/// </summary>
		/// <param name="myStream">流对象</param>
		public void SaveWordDocument( System.IO.Stream myStream )
		{
			byte[] bs = myContentEncoding.GetBytes( 
				this.InnerGetHtmlDocumentString( true ));
			myStream.Write( bs , 0 , bs.Length );
		}
		/// <summary>
		/// 将报表文档以 MS Word 的文档格式保存到指定名称的文件中
		/// </summary>
		/// <param name="strFileName">文件名</param>
		public void SaveWordDocument( string strFileName )
		{
			byte[] bs = myContentEncoding.GetBytes( 
				this.InnerGetHtmlDocumentString( true ));
			using( System.IO.FileStream stream = new System.IO.FileStream( 
					   strFileName , 
					   System.IO.FileMode.Create ,
					   System.IO.FileAccess.Write ))
			{
				stream.Write( bs , 0 , bs.Length );
			}
		}

		//		/// <summary>
		//		/// 将报表文档以MS Excel文档格式保存到指定名称的文件中
		//		/// </summary>
		//		/// <param name="strFileName">文件名</param>
		//		public void SaveExecelDocument( string strFileName )
		//		{
		//			byte[] bs = myContentEncoding.GetBytes( 
		//				this.InnerGetHtmlDocumentString( true ));
		//			using( System.IO.FileStream stream = new System.IO.FileStream( 
		//					   strFileName , 
		//					   System.IO.FileMode.Create ,
		//					   System.IO.FileAccess.Write ))
		//			{
		//				stream.Write( bs , 0 , bs.Length );
		//			}
		//		}
		/// <summary>
		/// 将报表以EXCEL文档格式保存报表到指定的流中
		/// </summary>
		/// <param name="myStream">流对象</param>
		public void SaveExcelDocument( System.IO.Stream myStream )
		{
			byte[] bs = myContentEncoding.GetBytes(
				this.InnerGetHtmlDocumentString( true ));
			myStream.Write( bs , 0 , bs.Length );
		}
		/// <summary>
		/// 将报表以Exel文档格式保存到指定的文件中
		/// </summary>
		/// <param name="strFileName">文件名</param>
		public void SaveExcelDocument( string strFileName )
		{
			byte[] bs = myContentEncoding.GetBytes( 
				this.InnerGetHtmlDocumentString( true ));
			using( System.IO.FileStream stream = new System.IO.FileStream( 
					   strFileName , 
					   System.IO.FileMode.Create ,
					   System.IO.FileAccess.Write ))
			{
				stream.Write( bs , 0 , bs.Length );
			}
		}
 
		/// <summary>
		/// 保存输出到一个流中
		/// </summary>
		/// <param name="myStream">流对象</param>
		public void Save( System.IO.Stream myStream )
		{
			byte[] bs = myContentEncoding.GetBytes( 
				this.InnerGetHtmlDocumentString(false));
			myStream.Write( bs , 0 , bs.Length );
		}
		/// <summary>
		/// 保存输出到一个文本书写器中
		/// </summary>
		/// <param name="w">文本书写器</param>
		public void Save( System.IO.TextWriter w )
		{
			w.Write( this.InnerGetHtmlDocumentString( false ));
		}
		/// <summary>
		/// 保存输出到一个文件中
		/// </summary>
		/// <param name="strFileName">文件名</param>
		public void Save( string strFileName )
		{
			using( System.IO.FileStream myStream = 
					   new System.IO.FileStream( strFileName , System.IO.FileMode.Create ))
			{
				Save( myStream );
			}
		}

		#endregion

		#region 内部成员群 ****************************************************

		/// <summary>
		/// 删除XML字符串的	XML声明头
		/// </summary>
		/// <param name="strXML">XML字符串</param>
		/// <returns>去掉XML声明头的XML字符串</returns>
		private string RemoveXMLDeclear( string strXML )
		{
			if( strXML != null)
			{
				int Index = strXML.IndexOf("?>") ;
				if( Index > 0  )
					Index += 2 ;
				else
					Index = 0 ;
				for(int iCount = Index ; iCount < strXML.Length ; iCount ++ )
				{
					if( ! char.IsWhiteSpace( strXML , iCount ))
						return strXML.Substring( iCount );
				}
				return strXML ;
			}
			return null;
		}

		/// <summary>
		/// 获得鼠标光标的名称
		/// </summary>
		/// <param name="Cursor">鼠标光标对象</param>
		/// <returns>鼠标光标样式名称</returns>
		private string GetCursorName( System.Windows.Forms.Cursor Cursor )
		{
			if( Cursor.Equals( System.Windows.Forms.Cursors.Default ))
				return "default";
			if( Cursor.Equals( System.Windows.Forms.Cursors.Cross ))
				return "crosshair";
			if( Cursor.Equals( System.Windows.Forms.Cursors.IBeam ))
				return "text" ;
			if( Cursor.Equals( System.Windows.Forms.Cursors.Arrow ))
				return "point" ;
			if( Cursor.Equals( System.Windows.Forms.Cursors.WaitCursor ))
				return "wait";
			if( Cursor.Equals( System.Windows.Forms.Cursors.AppStarting ))
				return "progress";
			if( Cursor.Equals( System.Windows.Forms.Cursors.Hand ))
				return "hand";
			if( Cursor.Equals( System.Windows.Forms.Cursors.Help ))
				return "help";
			if( Cursor.Equals( System.Windows.Forms.Cursors.SizeAll ))
				return "move";
			if( Cursor.Equals( System.Windows.Forms.Cursors.SizeNS ))
				return "n-resize";
			if( Cursor.Equals( System.Windows.Forms.Cursors.SizeWE ))
				return "e-resize";
			if( Cursor.Equals( System.Windows.Forms.Cursors.SizeNESW ))
				return "ne-resize";
			if( Cursor.Equals( System.Windows.Forms.Cursors.SizeNWSE ))
				return "se-resize";
			return "default";
		}

		/// <summary>
		/// 将对象转换为字符串
		/// </summary>
		/// <param name="vValue">数值</param>
		/// <returns>转换后的字符串</returns>
		private string ColorToString( System.Drawing.Color vValue)
		{
			if( vValue.A != 255 )
				return "#" + vValue.A.ToString("X2") + Convert.ToInt32( vValue.ToArgb() & 0xffffff).ToString("X6");
			else
				return "#" + Convert.ToInt32( vValue.ToArgb() & 0xffffff).ToString("X6");
		}

		
		/// <summary>
		/// 输出HTML文档的框架
		/// </summary>
		/// <param name="forExcel">是否添加EXCEL标记</param>
		private string InnerGetHtmlDocumentString( bool forExcel)
		{
			System.IO.StringWriter myStr = new System.IO.StringWriter();
			System.Xml.XmlTextWriter w = new System.Xml.XmlTextWriter( myStr );
			if( this.Options.Indent )
			{
				w.Formatting = System.Xml.Formatting.Indented ;
				w.IndentChar = ' ' ;
				w.Indentation = 3 ;
			}
			else
				w.Formatting = System.Xml.Formatting.None ;
			w.WriteStartDocument();
			w.WriteStartElement("html");
			if( forExcel )
			{
				w.WriteAttributeString("xmlns:o" , "urn:schemas-microsoft-com:office:office");
				w.WriteAttributeString("xmlns:x" , "urn:schemas-microsoft-com:office:excel");
				w.WriteAttributeString("xmlns:v" , "urn:schemas-microsoft-com:vml");
			}
			w.WriteStartElement("head");

			string title = this.Title ;
			if( title != null && title.Length > 0 )
				w.WriteElementString( "title" , title );
			
			w.WriteStartElement("meta");
			w.WriteAttributeString("http-equiv" , "Content-Type");
			w.WriteAttributeString("content" , "text/html;charset=" + myContentEncoding.BodyName );
			w.WriteEndElement();

			if( forExcel)
				GetExcelXML( title ).WriteContentTo( w );
			//			w.WriteElementString("style" , @"
			//table{font-size:9pt}
			//");
			w.WriteEndElement();
			w.WriteStartElement("body");
			w.WriteAttributeString("style"  , "font-size:" + this.myDefaultFont.Size + "pt");
			w.WriteRaw( System.Environment.NewLine );
			w.WriteRaw( this.ToString());
			w.WriteEndElement();
			w.WriteEndElement();
			w.WriteEndDocument();
			w.Close();

			return RemoveXMLDeclear( myStr.ToString());
		}

		private static System.Xml.XmlDocument ExcelXMLDoc = null;
		private static System.Xml.XmlDocument GetExcelXML( string strTitle )
		{
			if( ExcelXMLDoc == null )
			{
				ExcelXMLDoc = new System.Xml.XmlDocument();
				ExcelXMLDoc.LoadXml(@"<xml xmlns:o='urn:schemas-microsoft-com:office:office'
xmlns:x='urn:schemas-microsoft-com:office:excel'>
						<o:DocumentProperties>
							<o:Author>XReport</o:Author>
							<o:LastAuthor>ys2006</o:LastAuthor>
							<o:Created></o:Created>
							<o:LastSaved></o:LastSaved>
							<o:Company>DCSoft</o:Company>
							<o:Version>9.2812</o:Version>
						</o:DocumentProperties>
						<x:ExcelWorkbook>
							<x:ExcelWorksheets>
								<x:ExcelWorksheet>
									<x:Name id='nameelement' ></x:Name>
									<x:WorksheetOptions>
										<x:DefaultRowHeight>285</x:DefaultRowHeight>
										<x:Print>
											<x:ValidPrinterInfo />
											<x:PaperSizeIndex>9</x:PaperSizeIndex>
											<x:HorizontalResolution>100</x:HorizontalResolution>
											<x:VerticalResolution>100</x:VerticalResolution>
										</x:Print>
										<x:Selected />
										<x:ProtectContents>False</x:ProtectContents>
										<x:ProtectObjects>False</x:ProtectObjects>
										<x:ProtectScenarios>False</x:ProtectScenarios>
									</x:WorksheetOptions>
								</x:ExcelWorksheet>
							</x:ExcelWorksheets>
							<x:WindowHeight>13275</x:WindowHeight>
							<x:WindowWidth>18180</x:WindowWidth>
							<x:WindowTopX>480</x:WindowTopX>
							<x:WindowTopY>15</x:WindowTopY>
							<x:ProtectStructure>False</x:ProtectStructure>
							<x:ProtectWindows>False</x:ProtectWindows>
						</x:ExcelWorkbook>
					</xml>");
			}
			ExcelXMLDoc.SelectSingleNode("//*[@id='nameelement']").InnerText = strTitle ;
			return ExcelXMLDoc ;
		}

		#endregion

	}

    public class HtmlWriterOptions
    {
        private bool _Indent = false;
        /// <summary>
        /// 是否输出缩进的HTML代码
        /// </summary>
        [System.ComponentModel.DefaultValue( false )]
        public bool Indent
        {
            get { return _Indent; }
            set { _Indent = value; }
        }

        private bool _OutputXML = false;
        /// <summary>
        /// 输出严格的XML代码
        /// </summary>
        [DefaultValue( false )]
        public bool OutputXML
        {
            get
            {
                return _OutputXML; 
            }
            set
            {
                _OutputXML = value; 
            }
        }

        private bool _FormStyle = false;
        /// <summary>
        /// 输出表单HTML代码模式
        /// </summary>
        [DefaultValue( false )]
        public bool FormStyle
        {
            get { return _FormStyle; }
            set { _FormStyle = value; }
        }

        private bool _UseClassAttribute = false;
        /// <summary>
        /// 使用HTML元素的class属性，也就是说在一个style标签下集中输出所有的CSS样式字符串，然后HTML内容标签中使用class属性来引用CSS样式。
        /// </summary>
        [DefaultValue( false )]
        public virtual bool UseClassAttribute
        {
            get
            {
                return _UseClassAttribute;
            }
            set
            {
                _UseClassAttribute = value;
            }
        }
         

        private bool _OutputHeaderFooter = true;
        /// <summary>
        /// 是否输出页眉页脚
        /// </summary>
        [DefaultValue( true )]
        public bool OutputHeaderFooter
        {
            get
            {
                return _OutputHeaderFooter; 
            }
            set
            {
                _OutputHeaderFooter = value; 
            }
        }

        private bool _KeepLineBreak = true;
        /// <summary>
        /// 是否保持断行
        /// </summary>
        [DefaultValue( true )]
        public bool KeepLineBreak
        {
            get
            { 
                return _KeepLineBreak; 
            }
            set
            {
                _KeepLineBreak = value; 
            }
        }
    }
}
