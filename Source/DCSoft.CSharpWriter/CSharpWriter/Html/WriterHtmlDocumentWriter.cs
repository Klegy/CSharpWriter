/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DCSoft.Common;
using DCSoft.CSharpWriter;
using DCSoft.CSharpWriter.Dom;
using DCSoft.Drawing;

namespace DCSoft.CSharpWriter.Html
{
    public class WriterHtmlDocumentWriter : HtmlDocumentWriter
    {
        public WriterHtmlDocumentWriter()
        {
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

        private WriterHtmlViewStyle _ViewStyle = WriterHtmlViewStyle.Page;
        /// <summary>
        /// HTML文档视图样式
        /// </summary>
        public WriterHtmlViewStyle ViewStyle
        {
            get
            {
                return _ViewStyle; 
            }
            set
            {
                _ViewStyle = value; 
            }
        }

        public int ToPixel(float documentLength)
        {
            return (int) GraphicsUnitConvert.Convert(
                documentLength,
                this.MainDocument.DocumentGraphicsUnit,
                GraphicsUnit.Pixel);
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

        public override bool Indent
        {
            get
            {
                return this.Options.Indent;
            }
            set
            {
            }
        }

        public override bool UseClassAttribute
        {
            get
            {
                return this.Options.UseClassAttribute;
            }
            set
            {
            }
        }

        /// <summary>
        /// 只包含选择的部分
        /// </summary>
        private bool bolIncludeSelectionOnly = false;
        /// <summary>
        /// 只包含选择的部分
        /// </summary>
        public bool IncludeSelectionOndly
        {
            get
            {
                return bolIncludeSelectionOnly;
            }
            set
            {
                bolIncludeSelectionOnly = value;
            }
        }

        public bool IsVisible(DomElement element)
        {
            return element.Visible;
        }

        private Rectangle _ClipRectangle = Rectangle.Empty;
        /// <summary>
        /// 输出区域的剪切矩形
        /// </summary>
        public Rectangle ClipRectangle
        {
            get
            {
                return _ClipRectangle;
            }
            set
            {
                _ClipRectangle = value;
            }
        }

        public void Refresh()
        {
            this.Reset();
            switch (this.ViewStyle)
            {
                case WriterHtmlViewStyle.Normal :
                    this.WriteStartDocument();
                    this.WriteStartElement("div");
                    this.ClipRectangle = Rectangle.Empty;
                    if (this.Documents != null && this.Documents.Count > 0)
                    {
                        if (this.Options.OutputHeaderFooter)
                        {
                            // 输出页眉
                            if (this.MainDocument.Header.HasContentElement)
                            {
                                this.MainDocument.Header.WriteHTML(this);
                            }
                        }
                        if (this.IncludeSelectionOndly)
                        {
                            // 只输出选中的文档内容
                            DomDocument document = this.Documents[0];
                            document.CurrentContentElement.WriteHTML(this);
                        }
                        else
                        {
                            // 输出所有文档的正文内容
                            foreach (DomDocument document in this.Documents)
                            {
                                document.Body.WriteHTML(this);
                            }
                        }
                        if (this.Options.OutputHeaderFooter)
                        {
                            // 输出页脚
                            if (this.MainDocument.Footer.HasContentElement)
                            {
                                this.MainDocument.Footer.WriteHTML(this);
                            }
                        }
                    }
                    this.WriteEndElement();
                    this.WriteEndDocument();
                    break;
            }
        }

        //public override void WriteStartDocument()
        //{
        //    base.WriteStartDocument();
        //    this.WriteStartElement("div");
        //}

        //public override void WriteEndDocument()
        //{
        //    this.WriteEndElement();
        //    base.WriteEndDocument();
        //}
        /// <summary>
        /// 输出文档样式
        /// </summary>
        /// <param name="style">文档样式信息对象</param>
        /// <param name="element">相关的文档元素对象</param>
        public void WriteDocumentContentStyle(DocumentContentStyle style, DomElement element)
        {
            if (style == null)
            {
                throw new ArgumentNullException("style");
            }
            GraphicsUnit documentUnit = GraphicsUnit.Document;
            if (this.MainDocument != null)
            {
                documentUnit = this.MainDocument.DocumentGraphicsUnit;
            }
            // 输出边框线
            base.WriteBorderStyle(
                style.BorderLeft,
                style.BorderTop,
                style.BorderRight,
                style.BorderBottom,
                style.BorderColor,
                (int)style.BorderWidth,
                style.BorderStyle);
            // 输出字体
            base.WriteFontStyle(style.Font.Value);
            // 输出内边距
            base.WritePaddingStyle(
                ToPixel( style.PaddingLeft),
                ToPixel( style.PaddingTop),
                ToPixel( style.PaddingRight),
                ToPixel( style.PaddingBottom));

            // 输出外边距
            base.WriteMarginStyle(
                ToPixel( style.MarginLeft),
                ToPixel( style.MarginTop),
                ToPixel( style.MarginRight),
                ToPixel( style.MarginBottom));

            if (element is DomParagraphFlagElement 
                || element is DomParagraphElement )
            {
                // 文本水平对齐方式
                switch (style.Align)
                {
                    case DocumentContentAlignment.Left:
                        base.WriteStyleItem("text-align", "left");
                        break;
                    case DocumentContentAlignment.Center:
                        base.WriteStyleItem("text-align", "center");
                        break;
                    case DocumentContentAlignment.Right:
                        base.WriteStyleItem("text-align", "right");
                        break;
                    case DocumentContentAlignment.Justify:
                        base.WriteStyleItem("text-align", "justify");
                        break;
                    default:
                        base.WriteStyleItem("text-align", "left");
                        break;
                }
            }
            if (XDependencyObject.HasPropertyValue(style, ContentStyle.PropertyName_BackgroundColor))
            {
                // 输出背景色
                WriteStyleItem("background-color", ColorToString(style.BackgroundColor));
            }
            if (style.BackgroundImage != null && style.BackgroundImage.HasContent)
            {
                // 输出背景图片
                HtmlAttachFile file = base.AddImage(style.BackgroundImage.Value, null);
                WriteStyleItem("background-image", "url(" + file.ReferenceCode + ")");
            }
            if (XDependencyObject.HasPropertyValue(style, ContentStyle.PropertyName_BackgroundPosition))
            {
                // 背景位置
                string strItem = null;
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
                WriteStyleItem("background-position", strItem);
            }
            if (style.BackgroundPositionX != 0)
            {
                WriteStyleItem("background-position-x", GraphicsUnitConvert.ToCSSLength(
                                ToPixel( style.BackgroundPositionX),
                                documentUnit,
                                CssLengthUnit.Pixels));
            }
            if (style.BackgroundPositionY != 0)
            {
                WriteStyleItem("background-position-y", GraphicsUnitConvert.ToCSSLength(
                                ToPixel( style.BackgroundPositionY),
                                documentUnit,
                                CssLengthUnit.Pixels));
            }
            if (style.BackgroundRepeat)
            {
                WriteStyleItem("background-repeat", "repeat");
            }
            if (XDependencyObject.HasPropertyValue(style, ContentStyle.PropertyName_Color))
            {
                WriteStyleItem("color", ColorToString(style.Color));
            }
            // 首行缩进
            if (Math.Abs(style.FirstLineIndent) > 0.05)
            {
                WriteStyleItem("text-indent", GraphicsUnitConvert.ToCSSLength(
                    style.FirstLineIndent,
                    documentUnit,
                    CssLengthUnit.Pixels));
            }

            //case ContentStyle.PropertyName_LineSpacing:
            //    {
            //        // 行间距
            //        float ls = style.LineSpacing;
            //        if (Math.Abs(ls) > 0.05)
            //        {

            //        }
            //    }
            //    break;

            // 后置强制分页
            if (style.PageBreakAfter)
            {
                WriteStyleItem("page-break-after", "always");
            }
            // 前置强制分页
            if (style.PageBreakBefore)
            {
                WriteStyleItem("page-break-before", "always");
            }
            // 从右到左排版
            if (style.RightToLeft)
            {
                WriteStyleItem("direction", "rtl");
            }
            // 字符间距
            if (Math.Abs(style.Spacing) > 0.05)
            {
                WriteStyleItem("letter-spacing", GraphicsUnitConvert.ToCSSLength(
                    style.Spacing,
                    documentUnit,
                    CssLengthUnit.Pixels));
            }
            // 垂直显示文本
            if (style.VertialText)
            {
            }
             
            // 可见性
            if (style.Visible == false)
            {
                WriteStyleItem("visibility", "hidden");
            }
            if (XDependencyObject.HasPropertyValue(style, ContentStyle.PropertyName_Zoom))
            {
                // 缩放比率
                WriteStyleItem("zoom", style.Zoom.ToString());
            }
            
        }
         

        /// <summary>
        /// 输出图片类型的元素
        /// </summary>
        /// <param name="element">文档元素对象</param>
        public void WriteImageElement(DomElement element)
        {
            DocumentContentStyle style = element.RuntimeStyle;
            Image img = element.CreateContentImage();
            if (img != null)
            {
                HtmlAttachFile file = this.AddImage(img, null);
                WriteStartElement("img");
                WriteStartStyle();
                WriteDocumentContentStyle(element.RuntimeStyle , element );
                WriteEndStyle();
                WriteAttributeString("src", file.ReferenceCode);
                WriteAttributeString(
                    "width", 
                    GraphicsUnitConvert.ToCSSLength(
                        element.Width,
                        element.OwnerDocument.DocumentGraphicsUnit,
                        CssLengthUnit.Pixels));
                WriteAttributeString(
                    "width",
                    GraphicsUnitConvert.ToCSSLength(
                        element.Height,
                        element.OwnerDocument.DocumentGraphicsUnit,
                        CssLengthUnit.Pixels));
                if (element is DomImageElement)
                {
                    string title = ((DomImageElement)element).Title;
                    if (title != null && title.Length > 0)
                    {
                        WriteAttributeString("title", title);
                    }
                }
                WriteEndElement();
            }
        }

    }

    /// <summary>
    /// 报表HTML样式
    /// </summary>
    public enum WriterHtmlViewStyle
    {
        /// <summary>
        /// 正常方式显示HTML预览
        /// </summary>
        Normal,
        /// <summary>
        /// 正常居中显示HTML预览
        /// </summary>
        NormalCenter,
        /// <summary>
        /// 分页模式显示HTML预览
        /// </summary>
        Page,
        /// <summary>
        /// 采用图片以分页模式显示HTML预览,此时使用一张图片显示一整页的内容
        /// </summary>
        PageUseImage,
        /// <summary>
        /// 显示打印用HTML
        /// </summary>
        Print
    }

}
