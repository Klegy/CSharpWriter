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
using System.Xml.Serialization;
using System.ComponentModel;
using System.Drawing;

namespace DCSoft.CSharpWriter
{
    /// <summary>
    /// 文档视图相关选项
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable()]
    [TypeConverter( typeof( CommonTypeConverter ))]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class DocumentViewOptions : ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DocumentViewOptions()
        {
        }

        private bool _PrintImageAltText = false;
        /// <summary>
        /// 是否打印图片的Alt提示文本
        /// </summary>
        [DefaultValue( false )]
        public bool PrintImageAltText
        {
            get
            {
                return _PrintImageAltText; 
            }
            set
            {
                _PrintImageAltText = value; 
            }
        }

        private bool _ShowHeaderBottomLine = true;
        /// <summary>
        /// 当页眉有内容时显示页眉下边框线。若为false，则页眉和正文之间就没有分隔横线。
        /// 该选项默认值为true。
        /// </summary>
        [DefaultValue( true )]
        public bool ShowHeaderBottomLine
        {
            get
            {
                return _ShowHeaderBottomLine; 
            }
            set
            {
                _ShowHeaderBottomLine = value; 
            }
        }

        private bool _ShowCellNoneBorder = true;
        /// <summary>
        /// 是否显示表格单元格的隐藏的边框线。当为true时，若表格没有边框线，
        /// 则在编辑器中也会使用NoneBorderColor选项指定的颜色显示出边框线。
        /// 该设置不影响打印结果。该选项默认值为true。
        /// </summary>
        [DefaultValue( true )]
        public bool ShowCellNoneBorder
        {
            get
            {
                return _ShowCellNoneBorder; 
            }
            set
            {
                _ShowCellNoneBorder = value; 
            }
        }

        private Color _NoneBorderColor = Color.LightGray;
        /// <summary>
        /// 绘制隐藏的边框线使用的颜色。默认淡灰色。
        /// </summary>
        [DefaultValue( typeof( Color ), "LightGray")]
        public Color NoneBorderColor
        {
            get
            {
                return _NoneBorderColor; 
            }
            set
            {
                _NoneBorderColor = value; 
            }
        }

        /// <summary>
        /// 文本展现样式
        /// </summary>
        private System.Drawing.Text.TextRenderingHint intTextRenderStyle =
            System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        /// <summary>
        /// 在绘制文字时的质量设置。默认为ClearTypeGridFit。
        /// </summary>
        [DefaultValue(System.Drawing.Text.TextRenderingHint.ClearTypeGridFit)]
        public System.Drawing.Text.TextRenderingHint TextRenderStyle
        {
            get
            {
                return intTextRenderStyle;
            }
            set
            {
                intTextRenderStyle = value;
            }
        }

        ///// <summary>
        ///// 是否显示书签
        ///// </summary>
        //private bool bolShowBookmark = false ;
        ///// <summary>
        ///// 是否显示书签
        ///// </summary>
        //[DefaultValue( false )]
        //public bool ShowBookmark
        //{
        //    get
        //    {
        //        return bolShowBookmark;
        //    }
        //    set
        //    {
        //        bolShowBookmark = value;
        //    }
        //}

        private bool _ShowParagraphFlag = true;
        /// <summary>
        /// 显示段落标记。不影响打印，默认为true。
        /// </summary>
        [DefaultValue( true )]
        public bool ShowParagraphFlag
        {
            get
            {
                return _ShowParagraphFlag; 
            }
            set
            {
                _ShowParagraphFlag = value; 
            }
        }

        private bool _ShowPageLine = true;
        /// <summary>
        /// 当编辑器处于普通视图模式（不分页），则设置是否显示分页线。默认为true。
        /// </summary>
        [DefaultValue( true )]
        public bool ShowPageLine
        {
            get
            {
                return _ShowPageLine; 
            }
            set
            {
                _ShowPageLine = value; 
            }
        }

        private bool _RichTextBoxCompatibility = false  ;
        /// <summary>
        /// 兼容RTF文本控件模式,若为true，则使得同一个RTF文档，在本编辑器中和标准RichTextBox控件中
        /// 显示的结果误差比较小。默认为false。
        /// </summary>
        [DefaultValue( false )]
        public bool RichTextBoxCompatibility
        {
            get
            {
                return _RichTextBoxCompatibility; 
            }
            set
            {
                _RichTextBoxCompatibility = value; 
            }
        }

        private float _MinTableColumnWidth = 50f;
        /// <summary>
        /// 表格列的最小宽度，默认为50。
        /// </summary>
        [DefaultValue(50f )]
        public float MinTableColumnWidth
        {
            get
            {
                return _MinTableColumnWidth; 
            }
            set
            {
                _MinTableColumnWidth = value; 
            }
        }

        private TypeConverter _ColorConverter = TypeDescriptor.GetConverter(typeof(Color));

        private Color _FieldBackColor = Color.AliceBlue ;
        /// <summary>
        /// 文本输入域的默认背景颜色，默认为浅蓝色。
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        [DefaultValue(typeof(Color), "AliceBlue")]
        public Color FieldBackColor
        {
            get
            {
                return _FieldBackColor; 
            }
            set
            {
                _FieldBackColor = value; 
            }
        }

        [System.Xml.Serialization.XmlElement()]
        [Browsable( false )]
        [DefaultValue("LightGray")]
        public string FieldBackColorValue
        {
            get
            {
                return _ColorConverter.ConvertToString( _FieldBackColor );
            }
            set
            {
                _FieldBackColor = (Color)_ColorConverter.ConvertFrom(value);
            }
        }

        private Color _FieldHoverBackColor = Color.LightBlue;
        /// <summary>
        /// 鼠标悬浮在文本输入域时文本输入域的高亮度背景颜色，默认为淡蓝色。
        /// </summary>
        [XmlIgnore()]
        [DefaultValue(typeof( Color ) , "LightBlue")]
        public Color FieldHoverBackColor
        {
            get
            {
                return _FieldHoverBackColor; 
            }
            set
            {
                _FieldHoverBackColor = value; 
            }
        }

        [Browsable( false )]
        [XmlElement()]
        [DefaultValue("LightBlue")]
        public string FieldHoverBackColorValue
        {
            get
            {
                return _ColorConverter.ConvertToString(_FieldHoverBackColor);
            }
            set
            {
                _FieldHoverBackColor = (Color)_ColorConverter.ConvertFromString(value);
            }
        }

        private Color _FieldFocusedBackColor = Color.LightBlue;
        /// <summary>
        /// 文本输入域获得输入焦点时的高亮度背景颜色,默认为淡蓝色。
        /// </summary>
        [XmlIgnore()]
        [DefaultValue(typeof(Color), "LightBlue")]
        public Color FieldFocusedBackColor
        {
            get
            {
                return _FieldFocusedBackColor;
            }
            set
            {
                _FieldFocusedBackColor = value;
            }
        }

        [Browsable(false)]
        [XmlElement()]
        [DefaultValue("LightBlue")]
        public string FieldFocusedBackColorValue
        {
            get
            {
                return _ColorConverter.ConvertToString(_FieldFocusedBackColor);
            }
            set
            {
                _FieldFocusedBackColor = (Color)_ColorConverter.ConvertFromString(value);
            }
        }

        private Color _FieldInvalidateValueBackColor = Color.LightCoral ;
        /// <summary>
        /// 文本输入域数据异常时的高亮度背景色，默认为淡红色。
        /// </summary>
        [XmlIgnore()]
        [DefaultValue(typeof(Color), "LightCoral")]
        public Color FieldInvalidateValueBackColor
        {
            get
            {
                return _FieldInvalidateValueBackColor; 
            }
            set
            {
                _FieldInvalidateValueBackColor = value; 
            }
        }

        [Browsable( false )]
        [XmlElement()]
        [DefaultValue("LightCoral")]
        public string FieldInvalidateValueBackColorValue
        {
            get
            {
                return _ColorConverter.ConvertToString(_FieldInvalidateValueBackColor);
            }
            set
            {
                _FieldInvalidateValueBackColor = (Color)_ColorConverter.ConvertFromString(value);
            }
        }

        private Color _BackgroundTextColor = Color.Gray;
        /// <summary>
        /// 字段域的背景文本颜色，默认为灰色。
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        [DefaultValue(typeof(Color), "Gray")]
        public Color BackgroundTextColor
        {
            get
            {
                return _BackgroundTextColor;
            }
            set
            {
                _BackgroundTextColor = value;
            }
        }

        [System.Xml.Serialization.XmlElement()]
        [Browsable(false)]
        [DefaultValue("Gray")]
        public string BackgroundTextColorValue
        {
            get
            {
                return _ColorConverter.ConvertToString(_BackgroundTextColor);
            }
            set
            {
                _BackgroundTextColor = (Color)_ColorConverter.ConvertFrom(value);
            }
        }

        private SelectionHighlightStyle _SelectionHighlight = SelectionHighlightStyle.MaskColor  ;
        /// <summary>
        /// 选择区域高亮度显示方式
        /// </summary>
        [DefaultValue( SelectionHighlightStyle.MaskColor )]
        public SelectionHighlightStyle SelectionHighlight
        {
            get
            {
                return _SelectionHighlight; 
            }
            set
            {
                _SelectionHighlight = value; 
            }
        }

        private Color _SelectionHightlightMaskColor = Color.FromArgb(50, Color.Blue);
        /// <summary>
        /// 选择区域高亮度遮盖色，默认为半透明淡蓝色。
        /// </summary>
        [XmlIgnore]
        public Color SelectionHightlightMaskColor
        {
            get
            {
                return _SelectionHightlightMaskColor; 
            }
            set
            {
                _SelectionHightlightMaskColor = value; 
            }
        }

        [Browsable(false)]
        [XmlElement()]
        public string SelectionHightlightMaskColorValue
        {
            get
            {
                return _ColorConverter.ConvertToString(_SelectionHightlightMaskColor);
            }
            set
            {
                _SelectionHightlightMaskColor = (Color)_ColorConverter.ConvertFromString(value);
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            DocumentViewOptions opt = (DocumentViewOptions)this.MemberwiseClone();
            return opt;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public DocumentViewOptions Clone()
        {
            return ( DocumentViewOptions ) ((ICloneable)this).Clone();
        }
    }

    /// <summary>
    /// 选择区域的高亮度显示方式
    /// </summary>
    public enum SelectionHighlightStyle
    {
        /// <summary>
        /// 无任何高亮度显示方式
        /// </summary>
        None ,
        /// <summary>
        /// 反色高亮度显示
        /// </summary>
        Invert ,
        /// <summary>
        /// 使用遮盖色高亮度显示
        /// </summary>
        MaskColor 
    }

    /// <summary>
    /// DocumentViewOptions类型配套的类型转换器
    /// </summary>
    public class DocumentViewOptionsTypeConverter : System.ComponentModel.TypeConverter 
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override PropertyDescriptorCollection GetProperties(
            ITypeDescriptorContext context, 
            object value,
            Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(value, attributes);
        }
    }
    
}
