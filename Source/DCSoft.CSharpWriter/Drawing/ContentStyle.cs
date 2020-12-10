/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel ;
using DCSoft.Common ;
using System.Xml.Serialization ;
using System.Drawing.Drawing2D;

namespace DCSoft.Drawing
{
    /// <summary>
    /// 样式对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable()]
    public class ContentStyle : XDependencyObject , ICloneable , IXDependencyPropertyLoggable , IDisposable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ContentStyle()
        {
        }

        public bool EqualsStyleValue(ContentStyle style)
        {
            return XDependencyObject.EqualsValue(this, style)
                && string.Equals(
                    this.DefaultValuePropertyNames, 
                    style.DefaultValuePropertyNames, 
                    StringComparison.CurrentCultureIgnoreCase);
        }


        /// <summary>
        /// 删除与默认样式相同的项目，只保留不同的项目
        /// </summary>
        /// <param name="defaultStyle">默认样式对象</param>
        /// <returns>经过操作后本对象剩余的项目</returns>
        public int RemoveSameStyle(ContentStyle defaultStyle)
        {
            if (defaultStyle == null)
            {
                throw new ArgumentNullException("defaultStyle");
            }

            foreach (XDependencyProperty p in defaultStyle.InnerValues.Keys)
            {
                if (this.InnerValues.ContainsKey(p))
                {
                    object v1 = defaultStyle.InnerValues[p];
                    object v2 = this.InnerValues[p];
                    if (v1 == v2)
                    {
                        this.InnerValues.Remove(p);
                    }
                }
            }
            return this.InnerValues.Count;
        }

        private int _Index = -1;
        /// <summary>
        /// 内部编号，本属性供内部使用，没有意义，也不做任何参考
        /// </summary>
        [System.Xml.Serialization.XmlAttribute()]
        [DefaultValue(-1)]
        [System.ComponentModel.Browsable(false)]
        public int Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
        }

        #region 背景和颜色

        private static TypeConverter colorConverter = TypeDescriptor.GetConverter(typeof(Color));

        public const string PropertyName_BackgroundColor = "BackgroundColor";

        private static XDependencyProperty _backgroundColor = XDependencyProperty.Register(
            PropertyName_BackgroundColor, 
            typeof(Color),
            typeof(ContentStyle), 
            Color.Transparent);
        /// <summary>
        /// 背景色
        /// </summary>
        [DefaultValue(typeof(Color), "Transparent")]
        [XmlIgnore()]
        public Color BackgroundColor
        {
            get
            {
                return (Color)GetValue(_backgroundColor);
            }
            set
            {
                SetValue( _backgroundColor , value );
            }
        }

        /// <summary>
        /// 字符串格式的背景色
        /// </summary>
        [DefaultValue("Transparent")]
        [XmlElement("BackgroundColor")]
        [Browsable( false )]
        public string BackgroundColorString
        {
            get
            {
                return colorConverter.ConvertToString( this.BackgroundColor );
            }
            set
            {
                this.BackgroundColor = (Color) colorConverter.ConvertFrom(value);
            }
        }

        public const string PropertyName_BackgroundImage = "BackgroundImage";

        private static XDependencyProperty _backgroundImage = XDependencyProperty.Register(
            PropertyName_BackgroundImage,
            typeof(XImageValue),
            typeof(ContentStyle));
        /// <summary>
        /// 背景图片
        /// </summary>
        [DefaultValue(null)]
        public XImageValue BackgroundImage
        {
            get
            {
                return (XImageValue) GetValue( _backgroundImage );
            }
            set
            {
                SetValue(_backgroundImage, value);
            }
        }

        public const string PropertyName_BackgroundPosition = "BackgroundPosition";

        private static XDependencyProperty _backgroundPostion = XDependencyProperty.Register(
            PropertyName_BackgroundPosition,
            typeof(ContentAlignment),
            typeof(ContentStyle),
            ContentAlignment.TopLeft);
        /// <summary>
        /// 背景对齐样式
        /// </summary>
        [DefaultValue(ContentAlignment.TopLeft )]
        public ContentAlignment BackgroundPosition
        {
            get
            {
                return (ContentAlignment)GetValue( _backgroundPostion );
            }
            set
            {
                SetValue( _backgroundPostion, value );
            }
        }

        public const string PropertyName_BackgroundPositionX = "BackgroundPositionX";

        private static XDependencyProperty _backgroundPositionX = XDependencyProperty.Register(
            PropertyName_BackgroundPositionX,
            typeof(float),
            typeof(ContentStyle),
            0f);
        /// <summary>
        /// 背景X轴方向偏移量
        /// </summary>
        [DefaultValue(0f)]
        public float BackgroundPositionX
        {
            get
            {
                return (float)GetValue(_backgroundPositionX);
            }
            set
            {
                SetValue(_backgroundPositionX, value);
            }
        }

        public const string PropertyName_BackgroundPositionY = "BackgroundPositionY";

        private static XDependencyProperty _backgroundPositionY = XDependencyProperty.Register(
            PropertyName_BackgroundPositionY,
            typeof(float),
            typeof(ContentStyle),
            0f);
        /// <summary>
        /// 背景Y轴方向偏移量
        /// </summary>
        [DefaultValue(0f)]
        public float BackgroundPositionY
        {
            get
            {
                return (float)GetValue(_backgroundPositionY);
            }
            set
            {
                SetValue(_backgroundPositionY, value);
            }
        }


        public const string PropertyName_BackgroundRepeat = "BackgroundRepeat";

        private static XDependencyProperty _BackgroundRepeat = XDependencyProperty.Register(
            PropertyName_BackgroundRepeat,
            typeof(bool),
            typeof(ContentStyle),
            false );
        /// <summary>
        /// 是否重复绘制背景
        /// </summary>
        [DefaultValue(false )]
        public bool BackgroundRepeat
        {
            get
            {
                return (bool)GetValue(_BackgroundRepeat);
            }
            set
            {
                SetValue(_BackgroundRepeat, value);
            }
        }

        public const string PropertyName_Color = "Color";


        private static XDependencyProperty _Color = XDependencyProperty.Register(
            PropertyName_Color,
            typeof(Color),
            typeof(ContentStyle),
            Color.Black);
        /// <summary>
        /// 颜色
        /// </summary>
        [DefaultValue(typeof(Color ) , "Black")]
        [XmlIgnore()]
        public Color Color
        {
            get
            {
                return (Color)GetValue(_Color);
            }
            set
            {
                SetValue(_Color, value);
            }
        }


        /// <summary>
        /// 字符串格式的对象颜色
        /// </summary>
        [DefaultValue("Black")]
        [Browsable( false )]
        [XmlElement("Color")]
        public string ColorString
        {
            get
            {
                return colorConverter.ConvertToString(this.Color);
            }
            set
            {
                this.Color = (Color) colorConverter.ConvertFrom(value);
            }
        }

        // <summary>
        /// 根据设置创建绘制背景的画刷对象
        /// </summary>
        /// <returns>创建的画刷对象</returns>
        public System.Drawing.Brush CreateBackgroundBrush()
        {
            return CreateBackgroundBrush(GraphicsUnit.Pixel);
        }

        /// <summary>
        /// 根据设置创建绘制背景的画刷对象
        /// </summary>
        /// <param name="unit">单位</param>
        /// <returns>创建的画刷对象</returns>
        public System.Drawing.Brush CreateBackgroundBrush(GraphicsUnit unit)
        {
            XBrushStyle brush = new XBrushStyle();
            brush.Color = this.BackgroundColor;
            brush.Image = this.BackgroundImage;
            brush.Repeat = this.BackgroundRepeat;
            brush.OffsetX = this.BackgroundPositionX ;
            brush.OffsetY = this.BackgroundPositionY;
            return brush.CreateBrush(0, 0, 100, 100, unit);
        }

        public void SetBackgroundBrush(Brush b)
        {
            if (b == null)
            {
                return;
            }
            if (b is SolidBrush)
            {
                this.BackgroundColor = ((SolidBrush)b).Color;
            }
            else if (b is TextureBrush)
            {
                TextureBrush tb = (TextureBrush)b;
                this.BackgroundImage = new XImageValue( tb.Image );
                this.BackgroundRepeat = tb.WrapMode == WrapMode.Tile;
                this.BackgroundPositionX = tb.Transform.OffsetX;
                this.BackgroundPositionY = tb.Transform.OffsetY;
            }
            else if (b is LinearGradientBrush)
            {
                LinearGradientBrush lb = (LinearGradientBrush)b;
                this.BackgroundColor = lb.LinearColors[0];
            }
            else if (b is HatchBrush)
            {
                HatchBrush hb = (HatchBrush)b;
                this.BackgroundColor = hb.BackgroundColor;
            }
        }

        /// <summary>
        /// 是否存在可见的背景
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public bool HasVisibleBackground
        {
            get
            {
                if (this.BackgroundImage != null && this.BackgroundImage.Value != null)
                {
                    return true;
                }
                if (this.BackgroundColor.A == 0)
                {
                    return false;
                }
                return true;
            }
        }

        #endregion

        #region 字体和文本

        /// <summary>
        /// 字体对象
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        public XFontValue Font
        {
            get
            {
                XFontValue f = new XFontValue();
                if (this.FontName != null && this.FontName.Trim().Length > 0)
                {
                    f.Name = this.FontName.Trim();
                }
                if (this.FontSize > 0)
                {
                    f.Size = this.FontSize;
                }
                f.Bold = this.Bold;
                f.Italic = this.Italic;
                f.Underline = this.Underline;
                f.Strikeout = this.Strikeout;
                return f;
            }
            set
            {
                if (value != null)
                {
                    this.FontName = value.Name;
                    this.FontSize = value.Size;
                    this.Bold = value.Bold;
                    this.Italic = value.Italic;
                    this.Underline = value.Underline;
                    this.Strikeout = value.Strikeout;
                }
            }
        }

        /// <summary>
        /// 默认字体名称
        /// </summary>
        public static string DefaultFontName
            = System.Windows.Forms.Control.DefaultFont.Name;
        /// <summary>
        /// 默认字体大小
        /// </summary>
        public static float DefaultFontSize
            = System.Windows.Forms.Control.DefaultFont.Size;

        public const string PropertyName_FontName = "FontName";

        private static XDependencyProperty _FontName = XDependencyProperty.Register(
            PropertyName_FontName,
            typeof(string),
            typeof(ContentStyle),
            null);
        /// <summary>
        /// 字体名称
        /// </summary>
        [DefaultValue(null)]
        public string FontName
        {
            get
            {
                return (string)GetValue(_FontName);
            }
            set
            {
                SetValue(_FontName, value);
            }
        }

        public const string PropertyName_FontSize = "FontSize";


        private static XDependencyProperty _FontSize = XDependencyProperty.Register(
            PropertyName_FontSize,
            typeof(float),
            typeof(ContentStyle),
            9f );
        /// <summary>
        /// 字体大小
        /// </summary>
        [DefaultValue(9f )]
        public float FontSize
        {
            get
            {
                return (float)GetValue(_FontSize);
            }
            set
            {
                SetValue(_FontSize, value );
            }
        }

        public const string PropertyName_Bold = "Bold";


        private static XDependencyProperty _Bold = XDependencyProperty.Register(
            PropertyName_Bold,
            typeof(bool),
            typeof(ContentStyle),
            false);
        /// <summary>
        /// 粗体
        /// </summary>
        [DefaultValue(false)]
        public bool Bold
        {
            get
            {
                return (bool)GetValue( _Bold);
            }
            set
            {
                SetValue(_Bold, value );
            }
        }

        public const string PropertyName_Italic = "Italic";

        private static XDependencyProperty _Italic = XDependencyProperty.Register(
            PropertyName_Italic,
            typeof(bool),
            typeof(ContentStyle),
            false);
        /// <summary>
        /// 斜体
        /// </summary>
        [DefaultValue(false )]
        public bool Italic
        {
            get
            {
                return (bool)GetValue(_Italic );
            }
            set
            {
                SetValue(_Italic, value );
            }
        }

        /// <summary>
        /// 字体样式
        /// </summary>
        [Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public FontStyle FontStyle
        {
            get
            {
                FontStyle style = System.Drawing.FontStyle.Regular;
                if (this.Bold)
                    style = style | System.Drawing.FontStyle.Bold;
                if (this.Italic)
                    style = style | System.Drawing.FontStyle.Italic;
                if (this.Underline)
                    style = style | System.Drawing.FontStyle.Underline;
                if (this.Strikeout)
                    style = style | System.Drawing.FontStyle.Strikeout;
                return style;
            }
            set
            {
                this.Bold = ((value & System.Drawing.FontStyle.Bold) == System.Drawing.FontStyle.Bold);
                this.Italic = ((value & System.Drawing.FontStyle.Italic) == System.Drawing.FontStyle.Italic);
                this.Underline = ((value & System.Drawing.FontStyle.Underline) == System.Drawing.FontStyle.Underline);
                this.Strikeout = ((value & System.Drawing.FontStyle.Strikeout) == System.Drawing.FontStyle.Strikeout);
            }
        }

        public const string PropertyName_Underline = "Underline";

        private static XDependencyProperty _Underline = XDependencyProperty.Register(
            PropertyName_Underline,
            typeof(bool),
            typeof(ContentStyle),
            false);
        /// <summary>
        /// 下划线
        /// </summary>
        [DefaultValue(false )]
        public bool Underline
        {
            get
            {
                return (bool)GetValue(_Underline);
            }
            set
            {
                SetValue(_Underline, value);
            }
        }


        public const string PropertyName_Strikeout = "Strikeout";


        private static XDependencyProperty _Strikeout = XDependencyProperty.Register(
            PropertyName_Strikeout,
            typeof(bool),
            typeof(ContentStyle),
            false);
        /// <summary>
        /// 删除线
        /// </summary>
        [DefaultValue( false )]
        public bool Strikeout
        {
            get
            {
                return (bool)GetValue(_Strikeout);
            }
            set
            {
                SetValue(_Strikeout, value );
            }
        }

        public const string PropertyName_Superscript = "Superscript";


        private static XDependencyProperty _Superscript = XDependencyProperty.Register(
            PropertyName_Superscript,
            typeof(bool),
            typeof(ContentStyle),
            false);
        /// <summary>
        /// 上标样式
        /// </summary>
        [DefaultValue(false)]
        public bool Superscript
        {
            get
            {
                return (bool)GetValue(_Superscript);
            }
            set
            {
                SetValue(_Superscript, value);
            }
        }

        public const string PropertyName_Subscript = "Subscript";


        private static XDependencyProperty _Subscript = XDependencyProperty.Register(
            PropertyName_Subscript,
            typeof(bool),
            typeof(ContentStyle),
            false);
        /// <summary>
        /// 下标样式
        /// </summary>
        [DefaultValue(false)]
        public bool Subscript
        {
            get
            {
                return (bool)GetValue(_Subscript);
            }
            set
            {
                SetValue(_Subscript, value);
            }
        }

        ///// <summary>
        ///// 上标
        ///// </summary>
        //Superscript ,
        ///// <summary>
        ///// 下标
        ///// </summary>
        //Subscript

        public const string PropertyName_Spacing = "Spacing";


        private static XDependencyProperty _Spacing = XDependencyProperty.Register(
           PropertyName_Spacing,
           typeof(float),
           typeof(ContentStyle),
           0f);
        /// <summary>
        /// 字符间距
        /// </summary>
        [DefaultValue( 0f)]
        public float Spacing
        {
            get
            {
                return (float)GetValue(_Spacing );
            }
            set
            {
                SetValue(_Spacing, value );
            }
        }

        public const string PropertyName_SpacingAfterParagraph = "SpacingAfterParagraph";

        private static XDependencyProperty _SpacingAfterParagraph = XDependencyProperty.Register(
           PropertyName_SpacingAfterParagraph,
           typeof(float),
           typeof(ContentStyle),
           0f);
        /// <summary>
        /// 段落后间距
        /// </summary>
        [DefaultValue(0f)]
        public float SpacingAfterParagraph
        {
            get
            {
                return (float)GetValue(_SpacingAfterParagraph);
            }
            set
            {
                SetValue(_SpacingAfterParagraph, Math.Max( 0 , value));
            }
        }

        public const string PropertyName_SpacingBeforeParagraph = "SpacingBeforeParagraph";

        private static XDependencyProperty _SpacingBeforeParagraph = XDependencyProperty.Register(
           PropertyName_SpacingBeforeParagraph,
           typeof(float),
           typeof(ContentStyle),
           0f);
        /// <summary>
        /// 段落前间距
        /// </summary>
        [DefaultValue(0f)]
        public float SpacingBeforeParagraph
        {
            get
            {
                return (float)GetValue(_SpacingBeforeParagraph);
            }
            set
            {
                SetValue(_SpacingBeforeParagraph, Math.Max( 0 , value));
            }
        }


        public const string PropertyName_LineSpacingStyle = "LineSpacingStyle";


        private static XDependencyProperty _LineSpacingStyle = XDependencyProperty.Register(
           PropertyName_LineSpacingStyle,
           typeof(LineSpacingStyle),
           typeof(ContentStyle),
           LineSpacingStyle.SpaceSingle);
        /// <summary>
        /// 行间距样式
        /// </summary>
        [DefaultValue( LineSpacingStyle.SpaceSingle )]
        public LineSpacingStyle LineSpacingStyle
        {
            get
            {
                return (LineSpacingStyle)GetValue(_LineSpacingStyle);
            }
            set
            {
                SetValue(_LineSpacingStyle, value);
            }
        }

        public const string PropertyName_LineSpacing = "LineSpacing";

        private static XDependencyProperty _LineSpacing = XDependencyProperty.Register(
           PropertyName_LineSpacing,
           typeof(float),
           typeof(ContentStyle),
           0f);
        /// <summary>
        /// 行间距
        /// </summary>
        [DefaultValue( 0f)]
        public float LineSpacing
        {
            get
            {
                return (float)GetValue(_LineSpacing);
            }
            set
            {
                SetValue(_LineSpacing, Math.Max( 0 , value));
            }
        }

        /// <summary>
        /// 以Twips作为单位的标准行高
        /// </summary>
        public  const int _StandLineHeightTwips = 240;

        ///// <summary>
        ///// 根据行间距设置计算文本行中额外的高度
        ///// </summary>
        ///// <param name="maxFontHeight">文本行中最大的字体高度</param>
        ///// <param name="documentUnit">文档使用的度量单位</param>
        ///// <returns>文本行额外高度</returns>
        //public float GetLineAdditionHeight(float maxFontHeight, GraphicsUnit documentUnit)
        //{
        //    float oneRate = 1.1f;
        //    if (this.LineSpacingStyle == Drawing.LineSpacingStyle.SpaceSpecify)
        //    {
        //        //返回固定行距
        //        return this.LineSpacing - maxFontHeight ;
        //    }
        //    else if (this.LineSpacingStyle == Drawing.LineSpacingStyle.SpaceSpecify)
        //    {
        //        return contentHeight * oneRate;
        //    }
        //    float result = 0;
        //    float slh = GraphicsUnitConvert.FromTwips(_StandLineHeightTwips, documentUnit);
        //    switch (this.LineSpacingStyle)
        //    {
        //        case LineSpacingStyle.SpaceSingle:
        //            result = slh * oneRate;
        //            break;
        //        case LineSpacingStyle.Space1pt5:
        //            result = slh * oneRate * 1.5f;
        //            break;
        //        case LineSpacingStyle.SpaceDouble:
        //            result = slh * oneRate * 2f;
        //            break;
        //        case LineSpacingStyle.SpaceMultiple:
        //            result = slh * oneRate * this.LineSpacing;
        //            break;
        //    }
        //    contentHeight = contentHeight * oneRate;
        //    return Math.Max(contentHeight, result);
        //}

        /// <summary>
        /// 获得实际使用的行间距
        /// </summary>
        /// <param name="contentHeight">文本行内容高度</param>
        /// <param name="maxFontHeight">文本行中最大的字体高度</param>
        /// <param name="documentUnit">文档采用的度量单位</param>
        /// <returns>使用的行间距</returns>
        public float GetLineSpacing(float contentHeight , float maxFontHeight , GraphicsUnit documentUnit )
        {
            //float oneRate = 0.1f;
            if( this.LineSpacingStyle == Drawing.LineSpacingStyle.SpaceSpecify )
            {
                //返回固定行距
                return this.LineSpacing ;
            }
            else if (this.LineSpacingStyle == Drawing.LineSpacingStyle.SpaceSpecify)
            {
                return contentHeight + maxFontHeight * 0.1f ;
            }
            float result = 0;
            float slh = GraphicsUnitConvert.FromTwips(_StandLineHeightTwips, documentUnit);
            switch (this.LineSpacingStyle)
            {
                case LineSpacingStyle.SpaceSingle :
                    result = contentHeight + maxFontHeight * 0.1f;
                    break;
                case LineSpacingStyle.Space1pt5 :
                    result = contentHeight + maxFontHeight * 0.6f;
                    break;
                case LineSpacingStyle.SpaceDouble :
                    result = contentHeight + maxFontHeight * 1.1f;
                    break;
                case LineSpacingStyle.SpaceMultiple :
                    result = contentHeight + maxFontHeight * (this.LineSpacing - 1 + 0.1f);
                    break;
                case Drawing.LineSpacingStyle.SpaceExactly :
                    result = Math.Max(contentHeight, maxFontHeight);
                    break;
            }
            return result;
        }

        private static XDependencyProperty _RTFLineSpacing = XDependencyProperty.Register(
           "RTFLineSpacing",
           typeof(float),
           typeof(ContentStyle),
           0f);
        /// <summary>
        /// 从RTF文档中导入的绝对行间距值
        /// </summary>
        [DefaultValue(0f)]
        public float RTFLineSpacing
        {
            get
            {
                return (float)GetValue(_RTFLineSpacing);
            }
            set
            {
                SetValue(_RTFLineSpacing, value);
            }
        }

        //public const string PropertyName_ParagraphSpacing = "ParagraphSpacing";

        //private static XDependencyProperty _ParagraphSpacing = XDependencyProperty.Register(
        //   PropertyName_ParagraphSpacing,
        //   typeof(float),
        //   typeof(ContentStyle),
        //   0f);
        ///// <summary>
        ///// 段落间距
        ///// </summary>
        //[DefaultValue(0f)]
        //public float ParagraphSpacing
        //{
        //    get
        //    {
        //        return (float)GetValue(_ParagraphSpacing);
        //    }
        //    set
        //    {
        //        SetValue(_ParagraphSpacing, value);
        //    }
        //}

        public const string PropertyName_Align = "Align";


        private static XDependencyProperty _Align = XDependencyProperty.Register(
           PropertyName_Align,
           typeof(DocumentContentAlignment),
           typeof(ContentStyle),
           DocumentContentAlignment.Left);
        /// <summary>
        /// 文本水平对齐方式
        /// </summary>
        [DefaultValue(DocumentContentAlignment.Left )]
        public DocumentContentAlignment Align
        {
            get
            {
                return (DocumentContentAlignment)GetValue(_Align);
            }
            set
            {
                SetValue(_Align, value );
            }
        }

        public const string PropertyName_VerticalAlign = "VerticalAlign";


        private static XDependencyProperty _VerticalAlign = XDependencyProperty.Register(
           PropertyName_VerticalAlign,
           typeof(VerticalAlignStyle),
           typeof(ContentStyle),
           VerticalAlignStyle.Top );
        /// <summary>
        /// 文本垂直对齐方式
        /// </summary>
        [DefaultValue(VerticalAlignStyle.Top )]
        public VerticalAlignStyle VerticalAlign
        {
            get
            {
                return (VerticalAlignStyle)GetValue( _VerticalAlign );
            }
            set
            {
                SetValue( _VerticalAlign, value );
            }
        }

        public const string PropertyName_FirstLineIndent = "FirstLineIndent";


        private static XDependencyProperty _FirstLineIndent = XDependencyProperty.Register(
           PropertyName_FirstLineIndent,
           typeof(float),
           typeof(ContentStyle),
           0f);

        /// <summary>
        /// 首行缩进量
        /// </summary>
        [DefaultValue( 0f )]
        public float FirstLineIndent
        {
            get
            {
                return (float)GetValue(_FirstLineIndent );
            }
            set
            {
                SetValue(_FirstLineIndent, value );
            }
        }


        public const string PropertyName_LeftIndent = "LeftIndent";

        private static XDependencyProperty _LeftIndent = XDependencyProperty.Register(
          PropertyName_LeftIndent,
          typeof(float),
          typeof(ContentStyle),
          0f);

        /// <summary>
        /// 段落左缩进量
        /// </summary>
        [DefaultValue( 0f)]
        public float LeftIndent
        {
            get
            {
                return (float)GetValue(_LeftIndent);
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                SetValue(_LeftIndent, value);
            }
        }

        public const string PropertyName_VertialText = "VertialText";


        private static XDependencyProperty _VertialText = XDependencyProperty.Register(
           PropertyName_VertialText,
           typeof(bool),
           typeof(ContentStyle),
           false);

        /// <summary>
        /// 是否垂直显示文本
        /// </summary>
        [DefaultValue(false )]
        public bool VertialText
        {
            get
            {
                return (bool)GetValue(_VertialText);
            }
            set
            {
                SetValue(_VertialText, value);
            }
        }

        public const string PropertyName_RightToLeft = "RightToLeft";

        private static XDependencyProperty _RightToLeft = XDependencyProperty.Register(
           PropertyName_RightToLeft,
           typeof(bool),
           typeof(ContentStyle),
           false);
        /// <summary>
        /// 是否从右到左显示文本
        /// </summary>
        [DefaultValue(false )]
        public bool RightToLeft
        {
            get
            {
                return (bool)GetValue(_RightToLeft);
            }
            set
            {
                SetValue(_RightToLeft, value);
            }
        }


        public const string PropertyName_Multiline = "Multiline";

        private static XDependencyProperty _Multiline = XDependencyProperty.Register(
           PropertyName_Multiline,
           typeof(bool),
           typeof(ContentStyle),
           false);
        /// <summary>
        /// 允许显示多行文本
        /// </summary>
        [DefaultValue( false )]
        public bool Multiline
        {
            get
            {
                return (bool)GetValue(_Multiline);
            }
            set
            {
                SetValue(_Multiline, value);
            }
        }

        public void SetStringFormat(StringFormat format)
        {
            if (format.LineAlignment == StringAlignment.Center)
            {
                this.Align = DocumentContentAlignment.Center;
            }
            else if (format.LineAlignment == StringAlignment.Far)
            {
                this.Align = DocumentContentAlignment.Right;
            }
            else if (format.LineAlignment == StringAlignment.Near)
            {
                this.Align = DocumentContentAlignment.Left;
            }
            if (format.Alignment == StringAlignment.Center)
            {
                this.VerticalAlign = VerticalAlignStyle.Middle;
            }
            else if (format.Alignment == StringAlignment.Near)
            {
                this.VerticalAlign = VerticalAlignStyle.Top;
            }
            else if (format.Alignment == StringAlignment.Far)
            {
                this.VerticalAlign = VerticalAlignStyle.Bottom;
            }
            if ((format.FormatFlags & StringFormatFlags.DirectionVertical) == StringFormatFlags.DirectionVertical)
            {
                this.VertialText = true;
            }
            else
            {
                this.VertialText = false;
            }
            if ((format.FormatFlags & StringFormatFlags.DirectionRightToLeft) == StringFormatFlags.DirectionRightToLeft)
            {
                this.RightToLeft = true;
            }
            else
            {
                this.RightToLeft = false;
            }
        }

        /// <summary>
        /// 创建字符串格式化对象
        /// </summary>
        /// <returns>创建的格式化对象</returns>
        public StringFormat CreateStringFormat()
        {
            StringFormat format = new StringFormat();
            switch (this.Align)
            {
                case DocumentContentAlignment.Left:
                    format.Alignment = StringAlignment.Near;
                    break;
                case DocumentContentAlignment.Center:
                    format.Alignment = StringAlignment.Center;
                    break;
                case DocumentContentAlignment.Right:
                    format.Alignment = StringAlignment.Far;
                    break;
                case DocumentContentAlignment.Justify:
                    format.Alignment = StringAlignment.Center;
                    break;
            }
            switch ( this.VerticalAlign )
            {
                case VerticalAlignStyle.Top:
                    format.LineAlignment = StringAlignment.Near;
                    break;
                case VerticalAlignStyle.Middle:
                    format.LineAlignment = StringAlignment.Center;
                    break;
                case VerticalAlignStyle.Bottom:
                    format.LineAlignment = StringAlignment.Far;
                    break;
                case VerticalAlignStyle.Justify:
                    format.LineAlignment = StringAlignment.Center;
                    break;
            }
            StringFormatFlags flag = format.FormatFlags;
            if (this.Multiline == false)
            {
                flag = flag | StringFormatFlags.NoWrap;
            }
            if (this.VertialText)
            {
                flag = flag | StringFormatFlags.DirectionVertical;
            }
            if (this.RightToLeft)
            {
                flag = flag | StringFormatFlags.DirectionRightToLeft;
            }
            format.FormatFlags = flag;
            return format;
        }

        #endregion

        #region 布局

        public const string PropertyName_RoundRadio = "RoundRadio";


        private static XDependencyProperty _RoundRadio = XDependencyProperty.Register(
           PropertyName_RoundRadio,
           typeof(float),
           typeof(ContentStyle),
           0f);
        /// <summary>
        /// 圆角半径
        /// </summary>
        [DefaultValue(0f)]
        public float RoundRadio
        {
            get
            {
                return (float)GetValue(_RoundRadio);
            }
            set
            {
                SetValue(_RoundRadio , value);
            }
        }

        public const string PropertyName_Rotate = "Rotate";


        private static XDependencyProperty _Rotate = XDependencyProperty.Register(
          PropertyName_Rotate,
          typeof(float),
          typeof(ContentStyle),
          0f);
        /// <summary>
        /// 图形逆时针旋转角度，以度为单位
        /// </summary>
        [DefaultValue(0f)]
        public float Rotate
        {
            get
            {
                return (float)GetValue(_Rotate);
            }
            set
            {
                SetValue(_Rotate, value);
            }
        }

        /// <summary>
        /// 对图形绘制对象进行顺时针旋转转换操作
        /// </summary>
        /// <param name="g">图形绘制对象</param>
        /// <param name="Bounds">边距矩形</param>
        /// <returns>旋转前的转换矩形</returns>
        public System.Drawing.Drawing2D.Matrix RotateGraphics(
            System.Drawing.Graphics g,
            System.Drawing.Rectangle Bounds)
        {
            if (this.Rotate == 0f)
                return null;
            System.Drawing.Drawing2D.Matrix om = g.Transform;
            System.Drawing.Drawing2D.Matrix nm = om.Clone();
            System.Drawing.Point p = new Point(
                Bounds.Left + Bounds.Width / 2,
                Bounds.Top + Bounds.Height / 2);
            nm.RotateAt(
                this.Rotate,
                new System.Drawing.PointF(p.X, p.Y));
            g.Transform = nm;
            return om;
        }

        public const string PropertyName_BorderColor = "BorderColor";


        private static XDependencyProperty _BorderColor = XDependencyProperty.Register(
          PropertyName_BorderColor,
          typeof(Color),
          typeof(ContentStyle),
          Color.Black);
        /// <summary>
        /// 边框颜色
        /// </summary>
        [DefaultValue(typeof( Color ) , "Black")]
        [XmlIgnore()]
        public Color BorderColor
        {
            get
            {
                return (Color)GetValue(_BorderColor );
            }
            set
            {
                SetValue(_BorderColor, value);
            }
        }

        /// <summary>
        /// 字符串格式的边框颜色
        /// </summary>
        [DefaultValue("Black")]
        [Browsable(false)]
        [XmlElement("BorderColor")]
        public string BorderColorString
        {
            get
            {
                return colorConverter.ConvertToString(this.BorderColor);
            }
            set
            {
                this.BorderColor = (Color)colorConverter.ConvertFrom(value);
            }
        }

        public const string PropertyName_BorderStyle = "BorderStyle";

        private static XDependencyProperty _BorderStyle = XDependencyProperty.Register(
             PropertyName_BorderStyle,
             typeof(System.Drawing.Drawing2D.DashStyle),
             typeof(ContentStyle),
             System.Drawing.Drawing2D.DashStyle.Solid );
        /// <summary>
        /// 边框线型
        /// </summary>
        [DefaultValue(System.Drawing.Drawing2D.DashStyle.Solid)]
        public System.Drawing.Drawing2D.DashStyle BorderStyle
        {
            get
            {
                return (System.Drawing.Drawing2D.DashStyle)GetValue( _BorderStyle );
            }
            set
            {
                SetValue(_BorderStyle, value);
            }
        }

        public const string PropertyName_BorderWidth = "BorderWidth";

        private static XDependencyProperty _BorderWidth = XDependencyProperty.Register(
            PropertyName_BorderWidth,
            typeof(float),
            typeof(ContentStyle),
            0f);
        /// <summary>
        /// 边框线粗细
        /// </summary>
        [DefaultValue(0f)]
        public float BorderWidth
        {
            get
            {
                return (float)GetValue(_BorderWidth);
            }
            set
            {
                SetValue(_BorderWidth, value);
            }
        }

        public const string PropertyName_BorderLeft = "BorderLeft";

        private static XDependencyProperty _BorderLeft = XDependencyProperty.Register(
            PropertyName_BorderLeft,
            typeof(bool),
            typeof(ContentStyle),
            false);
        /// <summary>
        /// 是否显示左边框线
        /// </summary>
        [DefaultValue(false )]
        public bool BorderLeft
        {
            get
            {
                return (bool)GetValue(_BorderLeft);
            }
            set
            {
                SetValue(_BorderLeft, value);
            }
        }

        public const string PropertyName_BorderBottom = "BorderBottom";

        private static XDependencyProperty _BorderBottom = XDependencyProperty.Register(
            PropertyName_BorderBottom,
            typeof(bool),
            typeof(ContentStyle),
            false);

        /// <summary>
        /// 是否显示下边框线
        /// </summary>
        [DefaultValue(false)]
        public bool BorderBottom
        {
            get
            {
                return (bool)GetValue(_BorderBottom);
            }
            set
            {
                SetValue(_BorderBottom, value);
            }
        }

        public const string PropertyName_BorderTop = "BorderTop";

        private static XDependencyProperty _BorderTop = XDependencyProperty.Register(
            PropertyName_BorderTop,
            typeof(bool),
            typeof(ContentStyle),
            false);

        /// <summary>
        /// 是否显示上边框线
        /// </summary>
        [DefaultValue(false)]
        public bool BorderTop
        {
            get
            {
                return (bool)GetValue(_BorderTop);
            }
            set
            {
                SetValue(_BorderTop, value);
            }
        }

        public const string PropertyName_BorderRight = "BorderRight";

        private static XDependencyProperty _BorderRight = XDependencyProperty.Register(
            PropertyName_BorderRight,
            typeof(bool),
            typeof(ContentStyle),
            false);
        /// <summary>
        /// 是否显示右边框线
        /// </summary>
        [DefaultValue(false)]
        public bool BorderRight
        {
            get
            {
                return (bool)GetValue(_BorderRight);
            }
            set
            {
                SetValue(_BorderRight, value);
            }
        }

        public const string PropertyName_BorderSpacing = "BorderSpacing";

        private static XDependencyProperty _BorderSpacing = XDependencyProperty.Register(
            PropertyName_BorderSpacing,
            typeof(float),
            typeof(ContentStyle),
            0f);
        /// <summary>
        /// 边框线间距
        /// </summary>
        [DefaultValue(0f)]
        public float BorderSpacing
        {
            get
            {
                return (float)GetValue(_BorderSpacing);
            }
            set
            {
                SetValue(_BorderSpacing, value);
            }
        }

        /// <summary>
        /// 根据设置创建绘制边框的画笔对象
        /// </summary>
        /// <returns>创建的画笔对象</returns>
        public System.Drawing.Pen CreateBorderPen()
        {
            System.Drawing.Pen pen = new Pen( this.BorderColor, this.BorderWidth);
            pen.DashStyle = this.BorderStyle;
            return pen;
        }

        public void SetBorderPen(Pen p)
        {
            if (p != null)
            {
                this.BorderColor = p.Color;
                this.BorderWidth = p.Width;
                this.BorderStyle = p.DashStyle;
            }
        }

        public void DrawBorder(Graphics g, Pen pen, RectangleF rectangle)
        {
            if (this.BorderLeft && this.BorderTop && this.BorderRight && this.BorderBottom)
            {
                g.DrawRectangle(pen, rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height);
            }
            else
            {
                if (this.BorderLeft)
                {
                    g.DrawLine(pen, rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Top);
                }
                if (this.BorderTop)
                {
                    g.DrawLine(pen, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
                }
                if (this.BorderRight)
                {
                    g.DrawLine(pen, rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom);
                }
                if (this.BorderBottom)
                {
                    g.DrawLine(pen, rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
                }
            }
        }

        /// <summary>
        /// 判断样式是否存在可见的边框效果
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public bool HasVisibleBorder
        {
            get
            {
                if ( this.BorderLeft == false
                    && this.BorderTop == false
                    && this.BorderRight == false
                    && this.BorderBottom == false)
                    return false;
                if (this.BorderColor.A == 0)
                    return false;
                if (this.BorderWidth == 0)
                    return false;
                return true;
            }
        }

        public const string PropertyName_MarginLeft = "MarginLeft";

        private static XDependencyProperty _MarginLeft = XDependencyProperty.Register(
            PropertyName_MarginLeft,
            typeof(float),
            typeof(ContentStyle),
            0f);
        /// <summary>
        /// 左外边距
        /// </summary>
        [DefaultValue(0f)]
        public float MarginLeft
        {
            get
            {
                return (float)GetValue(_MarginLeft);
            }
            set
            {
                SetValue(_MarginLeft, value);
            }
        }

        public const string PropertyName_MarginTop = "MarginTop";

        private static XDependencyProperty _MarginTop = XDependencyProperty.Register(
            PropertyName_MarginTop,
            typeof(float),
            typeof(ContentStyle),
            0f);
        /// <summary>
        /// 上外边距
        /// </summary>
        [DefaultValue(0f)]
        public float MarginTop
        {
            get
            {
                return (float)GetValue(_MarginTop);
            }
            set
            {
                SetValue(_MarginTop, value);
            }
        }

        public const string PropertyName_MarginRight = "MarginRight";

        private static XDependencyProperty _MarginRight = XDependencyProperty.Register(
            PropertyName_MarginRight,
            typeof(float),
            typeof(ContentStyle),
            0f);
        /// <summary>
        /// 右外边距
        /// </summary>
        [DefaultValue(0f)]
        public float MarginRight
        {
            get
            {
                return (float)GetValue(_MarginRight);
            }
            set
            {
                SetValue(_MarginRight, value);
            }
        }

        public const string PropertyName_MarginBottom = "MarginBottom";

        private static XDependencyProperty _MarginBottom = XDependencyProperty.Register(
            PropertyName_MarginBottom,
            typeof(float),
            typeof(ContentStyle),
            0f);
        /// <summary>
        /// 下外边距
        /// </summary>
        [DefaultValue(0f)]
        public float MarginBottom
        {
            get
            {
                return (float)GetValue(_MarginBottom);
            }
            set
            {
                SetValue(_MarginBottom, value);
            }
        }

        public const string PropertyName_PaddingLeft = "PaddingLeft";

        private static XDependencyProperty _PaddingLeft = XDependencyProperty.Register(
            PropertyName_PaddingLeft,
            typeof(float),
            typeof(ContentStyle),
            0f);
        /// <summary>
        /// 左内边距
        /// </summary>
        [DefaultValue(0f)]
        public float PaddingLeft
        {
            get
            {
                return (float)GetValue(_PaddingLeft);
            }
            set
            {
                SetValue(_PaddingLeft, value);
            }
        }

        public const string PropertyName_PaddingTop = "PaddingTop";


        private static XDependencyProperty _PaddingTop = XDependencyProperty.Register(
            PropertyName_PaddingTop,
            typeof(float),
            typeof(ContentStyle),
            0f);
        /// <summary>
        /// 上内边距
        /// </summary>
        [DefaultValue(0f)]
        public float PaddingTop
        {
            get
            {
                return (float)GetValue(_PaddingTop);
            }
            set
            {
                SetValue(_PaddingTop, value);
            }
        }

        public const string PropertyName_PaddingRight = "PaddingRight";

        private static XDependencyProperty _PaddingRight = XDependencyProperty.Register(
            PropertyName_PaddingRight,
            typeof(float),
            typeof(ContentStyle),
            0f);
        /// <summary>
        /// 右内边距
        /// </summary>
        [DefaultValue(0f)]
        public float PaddingRight
        {
            get
            {
                return (float)GetValue(_PaddingRight);
            }
            set
            {
                SetValue(_PaddingRight, value);
            }
        }

        public const string PropertyName_PaddingBottom = "PaddingBottom";

        private static XDependencyProperty _PaddingBottom = XDependencyProperty.Register(
            PropertyName_PaddingBottom,
            typeof(float),
            typeof(ContentStyle),
            0f);
        /// <summary>
        /// 下内边距
        /// </summary>
        [DefaultValue(0f)]
        public float PaddingBottom
        {
            get
            {
                return (float)GetValue(_PaddingBottom);
            }
            set
            {
                SetValue(_PaddingBottom, value);
            }
        }

        public System.Drawing.Rectangle GetClientRectangle(int left, int top, int width, int height)
        {
            return new Rectangle(
                left + (int)this.PaddingLeft ,
                top + (int)this.PaddingTop,
                (int)(width - this.PaddingLeft - this.PaddingRight),
                (int)(height - this.PaddingTop - this.PaddingBottom));
        }

        public System.Drawing.Rectangle GetClientRectangle(System.Drawing.Rectangle bounds)
        {
            return new Rectangle(
                bounds.Left + (int)this.PaddingLeft,
                bounds.Top + (int)this.PaddingTop,
                (int)(bounds.Width - this.PaddingLeft - this.PaddingRight),
                (int)(bounds.Height - this.PaddingTop - this.PaddingBottom));
        }


        public System.Drawing.RectangleF GetClientRectangleF(float left, float top, float width, float height)
        {
            return new RectangleF(
                left + this.PaddingLeft,
                top + this.PaddingTop,
                width - this.PaddingLeft - this.PaddingRight,
                height - this.PaddingTop - this.PaddingBottom);
        }

        public System.Drawing.RectangleF GetClientRectangleF(RectangleF bounds)
        {
            return new RectangleF(
                bounds.Left + this.PaddingLeft,
                bounds.Top + this.PaddingTop,
                bounds.Width - this.PaddingLeft - this.PaddingRight,
                bounds.Height - this.PaddingTop - this.PaddingBottom);
        }

        public const string PropertyName_Zoom = "Zoom";

        private static XDependencyProperty _Zoom = XDependencyProperty.Register(
            PropertyName_Zoom,
            typeof(float),
            typeof(ContentStyle),
            1.0f);
        /// <summary>
        /// 缩放比例
        /// </summary>
        [DefaultValue(1.0f)]
        public float Zoom
        {
            get
            {
                return (float)GetValue(_Zoom);
            }
            set
            {
                SetValue(_Zoom, value);
            }
        }

        #endregion

        #region 位置

        public const string PropertyName_Left = "Left";

        private static XDependencyProperty _Left = XDependencyProperty.Register(
            PropertyName_Left,
            typeof(float),
            typeof(ContentStyle),
            0f);
        /// <summary>
        /// 左端位置
        /// </summary>
        [DefaultValue(0f)]
        public float Left
        {
            get
            {
                return (float)GetValue(_Left);
            }
            set
            {
                SetValue(_Left, value);
            }
        }

        public const string PropertyName_Top = "Top";

        private static XDependencyProperty _Top = XDependencyProperty.Register(
            PropertyName_Top,
            typeof(float),
            typeof(ContentStyle),
            0f);
        /// <summary>
        /// 顶端位置
        /// </summary>
        [DefaultValue(0f)]
        public float Top
        {
            get
            {
                return (float)GetValue(_Top);
            }
            set
            {
                SetValue(_Top, value);
            }
        }

        public const string PropertyName_Width = "Width";

        private static XDependencyProperty _Width = XDependencyProperty.Register(
            PropertyName_Width,
            typeof(float),
            typeof(ContentStyle),
            0f);
        /// <summary>
        /// 宽度
        /// </summary>
        [DefaultValue(0f)]
        public float Width
        {
            get
            {
                return (float)GetValue(_Width);
            }
            set
            {
                SetValue(_Width, value);
            }
        }

        public const string PropertyName_Height = "Height";

        private static XDependencyProperty _Height = XDependencyProperty.Register(
            PropertyName_Height,
            typeof(float),
            typeof(ContentStyle),
            0f);
        /// <summary>
        /// 高度
        /// </summary>
        [DefaultValue(0f)]
        public float Height
        {
            get
            {
                return (float)GetValue(_Height);
            }
            set
            {
                SetValue(_Height, value);
            }
        }

        public const string PropertyName_Visible = "Visible";

        private static XDependencyProperty _Visible = XDependencyProperty.Register(
            PropertyName_Visible,
            typeof(bool),
            typeof(ContentStyle),
            true);
        /// <summary>
        /// 高度
        /// </summary>
        [DefaultValue(true)]
        public bool Visible
        {
            get
            {
                return (bool)GetValue(_Visible);
            }
            set
            {
                SetValue(_Visible, value);
            }
        }

        


        #endregion

        #region 打印相关 

        public const string PropertyName_PageBreakAfter = "PageBreakAfter";

        private static XDependencyProperty _PageBreakAfter = XDependencyProperty.Register(
            PropertyName_PageBreakAfter,
            typeof(bool),
            typeof(ContentStyle),
            false);
        /// <summary>
        /// 高度
        /// </summary>
        [DefaultValue(false)]
        public bool PageBreakAfter
        {
            get
            {
                return (bool)GetValue(_PageBreakAfter);
            }
            set
            {
                SetValue(_PageBreakAfter, value);
            }
        }

        public const string PropertyName_PageBreakBefore = "PageBreakBefore";

        private static XDependencyProperty _PageBreakBefore = XDependencyProperty.Register(
            PropertyName_PageBreakBefore,
            typeof(bool),
            typeof(ContentStyle),
            false);
        /// <summary>
        /// 高度
        /// </summary>
        [DefaultValue(false)]
        public bool PageBreakBefore
        {
            get
            {
                return (bool)GetValue(_PageBreakBefore);
            }
            set
            {
                SetValue(_PageBreakBefore, value);
            }
        }


        #endregion

        public const string PropertyName_BulletedList = "BulletedList";


        private static XDependencyProperty _BulletedList = XDependencyProperty.Register(
            PropertyName_BulletedList,
            typeof(bool),
            typeof(ContentStyle),
            false);
        /// <summary>
        /// list in bulleted style
        /// </summary>
        [DefaultValue(false)]
        public bool BulletedList
        {
            get
            {
                return (bool)GetValue( _BulletedList );
            }
            set
            {
                SetValue(_BulletedList, value);
            }
        }

        public const string PropertyName_NumberedList = "NumberedList";

        private static XDependencyProperty _NumberedList = XDependencyProperty.Register(
            PropertyName_NumberedList,
            typeof(bool),
            typeof(ContentStyle),
            false);
        /// <summary>
        /// list in numbered style
        /// </summary>
        [DefaultValue(false)]
        public bool NumberedList
        {
            get
            {
                return (bool)GetValue(_NumberedList) ;
            }
            set
            {
                SetValue(_NumberedList, value);
            }
        }

        object ICloneable.Clone()
        {
            ContentStyle style = (ContentStyle)this.MemberwiseClone();
            style._InnerValues = new XDependencyPropertyObjectValues();
            //style.InnerValues.Clear();
            XDependencyObject.CopyValueFast(this, style);
            return style;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public ContentStyle Clone()
        {
            return (ContentStyle)((ICloneable)this).Clone();
        }

        public void Merge(ContentStyle style)
        {
            XDependencyObject.MergeValues(style, this  , false );
        }



        #region IXDependencyPropertyLoggable 成员
        private IXDependencyPropertyLogger _XDependencyPropertyLogger = null;
        IXDependencyPropertyLogger IXDependencyPropertyLoggable.PropertyLogger
        {
            get
            {
                return _XDependencyPropertyLogger;
            }
            set
            {
                _XDependencyPropertyLogger = value;
            }
        }

        #endregion

        /// <summary>
        /// 返回表示对象内容的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return XDependencyObject.GetStyleString(this);
        }

        public virtual void Dispose()
        {
            base.ClearWithDispose();
        }

        private string _DefaultValuePropertyNames = null;
        /// <summary>
        /// 采用默认值的属性名称
        /// </summary>
        [DefaultValue( null )]
        public string DefaultValuePropertyNames
        {
            get
            {
                return _DefaultValuePropertyNames; 
            }
            set
            {
                if( string.IsNullOrEmpty( value ))
                {
                    _DefaultValuePropertyNames = null;
                }
                else
                {
                    _DefaultValuePropertyNames = value;
                }
            }
        }

        

        
        ///// <summary>
        ///// 合并数据
        ///// </summary>
        ///// <param name="source">数据源</param>
        ///// <param name="destination">数据目标对象</param>
        ///// <param name="overWrite">源数据是否覆盖目标数据</param>
        ///// <returns>修改了目标对象的属性个数</returns>
        //public static int MergeValues(
        //    ContentStyle source,
        //    ContentStyle destination,
        //    bool overWrite)
        //{
        //    if (source == destination)
        //    {
        //        return 0;
        //    }
        //    if (source == null || destination == null)
        //    {
        //        return 0;
        //    }
        //    int result = 0;
        //    foreach (XDependencyProperty p in source.InnerValues.Keys)
        //    {
        //        if (destination.InnerValues.ContainsKey(p) == false)
        //        {
        //            object v = source.GetValue(p);
        //            if (v is ICloneable)
        //            {
        //                v = ((ICloneable)v).Clone();
        //            }
        //            destination.SetValue(p, v);
        //            result++;
        //        }
        //        else
        //        {
        //            if (overWrite)
        //            {
        //                bool back = destination.DisableDefaultValue;
        //                destination.DisableDefaultValue = source.DisableDefaultValue;
        //                destination.SetValue(p, source.GetValue(p));
        //                destination.DisableDefaultValue = back;
        //                result++;
        //            }
        //            //object v = source.GetValue( p );
        //            //object v2 = destination.GetValue(p);
        //            //if (v != v2)
        //            //{
        //            //    destination.SetValue(p, v);
        //            //    result++;
        //            //}
        //        }
        //    }
        //    return result;
        //}

    }

    /// <summary>
    /// 行间距样式
    /// </summary>
    public enum LineSpacingStyle
    {
        /// <summary>
        /// 单倍行距,此时LineSpacing值无意义。
        /// </summary>
        SpaceSingle = 0 ,
        /// <summary>
        /// 1.5倍行距,此时LineSpacing值无意义。
        /// </summary>
        Space1pt5,
        /// <summary>
        /// 双倍行距,此时LineSpacing值无意义。
        /// </summary>
        SpaceDouble ,
        /// <summary>
        /// 最小值,此时LineSpacing值无意义。
        /// </summary>
        SpaceExactly ,
        /// <summary>
        /// 固定值,此时LineSpacing指定了行间距。
        /// </summary>
        SpaceSpecify ,
        /// <summary>
        /// 多倍行距,此时LineSpacing指定的倍数。
        /// </summary>
        SpaceMultiple
    }


    //public class ContentStyleList : List<ContentStyleListItem >
    //{
    //    public ContentStyle GetStyle(string name)
    //    {
    //        foreach (ContentStyleListItem item in this)
    //        {
    //            if (item.Name == name)
    //            {
    //                return item.Style;
    //            }
    //        }
    //        return null;
    //    }

    //    public void SetStyle(string name, ContentStyle style)
    //    {
    //        foreach (ContentStyleListItem item in this)
    //        {
    //            if (item.Name == name)
    //            {
    //                item.Style = style;
    //                return;
    //            }
    //        }
    //        ContentStyleListItem newItem = new ContentStyleListItem();
    //        newItem.Name = name;
    //        newItem.Style = style;
    //        this.Add(newItem);
    //    }

    //    public string GetName(ContentStyle style)
    //    {
    //        foreach (ContentStyleListItem item in this)
    //        {
    //            if (item.Style == style)
    //            {
    //                return item.Name;
    //            }
    //        }
    //        foreach (ContentStyleListItem item in this)
    //        {
    //            if ( XDependencyObject.EqualsValue( item.Style , style ))
    //            {
    //                return item.Name;
    //            }
    //        }
    //        return null;
    //    }

    //    public string AllocName()
    //    {
    //        return "Style" + this.Count;
    //    }
    //}

    //[Serializable()]
    //public class ContentStyleListItem
    //{
    //    public ContentStyleListItem()
    //    {
    //    }

    //    private string _Name = null;
    //    [DefaultValue(null)]
    //    public string Name
    //    {
    //        get { return _Name; }
    //        set { _Name = value; }
    //    }

    //    private ContentStyle _Style = null;
    //    [DefaultValue(null)]
    //    public ContentStyle Style
    //    {
    //        get { return _Style; }
    //        set { _Style = value; }
    //    }
    //}
}