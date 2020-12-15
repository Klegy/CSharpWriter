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
using System.ComponentModel;
using DCSoft.CSharpWriter.Controls ;
using DCSoft.Drawing;
using DCSoft.WinForms;
using System.Windows.Forms;
using DCSoft.WinForms.Native;

namespace DCSoft.CSharpWriter.Dom
{
    public class DomContentRender
    {

        public DomContentRender()
        {
        }

        private DomDocument _Document = null;
        /// <summary>
        /// 对象所属文档对象
        /// </summary>
        public DomDocument Document
        {
            get
            {
                return _Document; 
            }
            set
            {
                _Document = value;
                ClearCharSizeBuffer();
            }
        }

        public virtual void DrawLabel(
            System.Drawing.Graphics graphics,
            Font font,
            string text,
            Color textColor ,
            Color backColor,
            Color borderColor,
            Rectangle bounds )
        {
            using (SolidBrush b = new SolidBrush(backColor))
            {
                graphics.FillRectangle(b, bounds);
            }
            if (text != null && text.Length > 0)
            {
                using (StringFormat format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center ;
                    format.FormatFlags = StringFormatFlags.NoWrap ;
                    using (SolidBrush b = new SolidBrush(textColor))
                    {
                        graphics.DrawString(
                            text,
                            font,
                            b, 
                            new RectangleF(
                                bounds.Left,
                                bounds.Top,
                                bounds.Width,
                                bounds.Height),
                            format);
                    }//using
                }//using
            }
            using (Pen pen = new Pen(borderColor))
            {
                graphics.DrawRectangle(pen, bounds);
            }
        }
        //private TextDocumentEditControl _BindControl = null;
        ///// <summary>
        ///// 显示文档内容的编辑器控件对象
        ///// </summary>
        //public TextDocumentEditControl BindControl
        //{
        //    get { return _BindControl; }
        //    set { _BindControl = value; }
        //}

        /// <summary>
        /// 重新计算对象大小
        /// </summary>
        /// <param name="g">图形绘制对象</param>
        public virtual void RefreshSize( DomElement element , System.Drawing.Graphics g)
        {
            if (element is DomCharElement)
            {
                RefreshSize((DomCharElement)element, g);
            }
            else if (element is DomParagraphFlagElement)
            {
                RefreshSize((DomParagraphFlagElement)element, g);
            }
            else if (element is DomLineBreakElement)
            {
                RefreshSize((DomLineBreakElement)element, g);
            }
            
            //else if (element is XTextFieldElement)
            //{
            //    // 字段元素
            //    XTextFieldElement field = (XTextFieldElement)element;
            //    if (field.StartElement != null)
            //    {
            //        RefreshSize(field.StartElement, g);
            //    }
            //    foreach (XTextElement e in field.Elements)
            //    {
            //        RefreshSize(e, g);
            //    }

            //    XTextElementList list = new XTextElementList();
            //    field.AppendContent(list, false);
            //    foreach (XTextElement e in list)
            //    {
            //        if (e != field)
            //        {
            //            RefreshSize(e, g);
            //        }
            //    }

            //    if (field.EndElement != null)
            //    {
            //        RefreshSize(field.EndElement, g);
            //    }
            //}
            //else if (element is XTextContainerElement)
            //{

            //    XTextContainerElement c = (XTextContainerElement)element;
            //    //XTextElementList list = new XTextElementList();
            //    //c.AppendContent(list, false);
            //    foreach (XTextElement e in c.Elements)
            //    {
            //        RefreshSize(e, g);
            //    }
            //    //if (c.EOFElement != null)
            //    //{
            //    //    RefreshSize(c.EOFElement, g);
            //    //}
            //}
            
        }

        /// <summary>
        /// 重新绘制对象
        /// </summary>
        /// <param name="g">图形绘制对象</param>
        /// <param name="ClipRectangle">剪切矩形</param>
        public virtual void RefreshView(DomElement element, DocumentPaintEventArgs args)
        {
            if (element == null || element.OwnerLine == null)
            {
                return;
            }
            this.DrawBackground(element, args);
            this.DrawContent(element, args);
            if (args.RenderStyle == DocumentRenderStyle.Paint && args.ActiveMode)
            {
                if (element is DomObjectElement)
                {
                    DomObjectElement oe = (DomObjectElement)element;
                    if (oe.ShowDragRect)
                    {
                        DragRectangle dr = oe.CreateDragRectangle();
                        dr.Bounds = new Rectangle(
                            (int)element.AbsLeft,
                            (int)element.AbsTop,
                            dr.Bounds.Width,
                            dr.Bounds.Height);
                        dr.RefreshView(args.Graphics);
                    }
                }
            }
        }

        /// <summary>
        /// 绘制授权状态标记
        /// </summary>
        /// <param name="element">文档元素对象</param>
        /// <param name="args">参数</param>
        public virtual void DrawPermissionMark(DomElement element, DocumentPaintEventArgs args)
        {
            DocumentContentStyle style = element.Style;
            if (style.DeleterIndex >= 0)
            {
                DomContentLine line = element.OwnerLine;
                RectangleF bounds = new RectangleF(
                    element.AbsLeft, 
                    line.AbsTop,
                    element.Width + element.WidthFix,
                    line.Height);
                int level = element.DeleterPermissionLevel;
                if (level >= 0)
                {
                    if (level == 0)
                    {
                        args.Graphics.DrawLine(
                            Pens.Blue,
                            bounds.Left,
                            bounds.Top + bounds.Height / 2,
                            bounds.Right,
                            bounds.Top + bounds.Height / 2);
                    }
                    else
                    {
                        float step = bounds.Height / (level + 1);
                        for (int iCount = 1; iCount <= level; iCount++)
                        {
                            args.Graphics.DrawLine(
                                Pens.Red,
                                bounds.Left,
                                bounds.Top + step * iCount,
                                bounds.Right,
                                bounds.Top + step * iCount);
                        }//for
                    }
                }
            }
            else if (style.CreatorIndex >= 0)
            {
                DomContentLine line = element.OwnerLine;
                RectangleF bounds = new RectangleF(
                    element.AbsLeft,
                    line.AbsTop,
                    element.Width + element.WidthFix,
                    line.Height);
                int level = element.CreatorPermessionLevel;
                if (level > 0)
                {
                    if (level == 1)
                    {
                        args.Graphics.DrawLine(Pens.Blue, bounds.Left, bounds.Bottom, bounds.Right, bounds.Bottom);
                    }
                    else
                    {
                        args.Graphics.DrawLine(Pens.Blue, bounds.Left, bounds.Bottom, bounds.Right, bounds.Bottom);
                        args.Graphics.DrawLine(Pens.Blue, bounds.Left, bounds.Bottom - 6 , bounds.Right, bounds.Bottom - 6 );
                    }
                }
            }
        }

        /// <summary>
        /// 绘制对象内容
        /// </summary>
        /// <param name="g">图形绘制对象</param>
        /// <param name="ClipRectangle">剪切矩形</param>
        public virtual void DrawContent(DomElement element , DocumentPaintEventArgs args)
        {
            if (element is DomCharElement)
            {
                DrawContent((DomCharElement)element, args);
            }
            else if (element is DomParagraphFlagElement)
            {
                DrawContent((DomParagraphFlagElement)element, args);
            }
            else if (element is DomImageElement)
            {
                DrawContent((DomImageElement)element, args);
            }
            else if (element is DomLineBreakElement)
            {
                DrawContent((DomLineBreakElement)element, args);
            }
             
        }

        private HighlightInfoList _HighlightBuffer = null;

        /// <summary>
        /// 获得高亮度显示信息对象
        /// </summary>
        /// <param name="element"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private HighlightInfo GetHighlightInfo(DomElement element )
        {
            if (_HighlightBuffer == null)
            {
                _HighlightBuffer = new HighlightInfoList();
            }
            else
            {
                // 在缓存区中逆向搜索
                // 由于绘制文档内容都是按照顺序绘制文档内容
                for (int iCount = _HighlightBuffer.Count - 1; iCount >= 0; iCount--)
                {
                    if (_HighlightBuffer[iCount].Contains(element))
                    {
                        return _HighlightBuffer[iCount];
                    }
                }
            }

            DomContainerElement parent = element.Parent ;
            while (parent != null)
            {
                HighlightInfoList infos = parent.GetHighlightInfos();
                if (infos != null && infos.Count > 0)
                {
                    foreach (HighlightInfo info in infos)
                    {
                        if (info.Contains(element))
                        {
                            // 保存在缓存区中
                            _HighlightBuffer.Add(info);
                            return info;
                        }
                    }
                }
                parent = parent.Parent;
            }
            return null;
        }

        /// <summary>
        /// 绘制文档元素背景
        /// </summary>
        /// <param name="element">文档元素对象</param>
        /// <param name="args">参数</param>
        /// <param name="bounds">要绘制背景的区域</param>
        public virtual void DrawBackground(
            DomElement element,
            DocumentPaintEventArgs args,
            RectangleF bounds)
        {
            if (bounds.IsEmpty)
            {
                // 不绘制图形
                return;
            }

            DocumentContentStyle rs = null;
            if (element.Parent != null)
            {
                rs = element.Parent.GetContentBackgroundStyle(element);
            }
            if (rs == null)
            {
                rs = element.RuntimeStyle;
            }
            System.Drawing.Color c = rs.BackgroundColor;
            if (c.A != 0)
            {
                System.Drawing.SolidBrush b = GraphicsObjectBuffer.GetSolidBrush(c);
                bounds = System.Drawing.RectangleF.Intersect(bounds, args.ClipRectangleF);
                if (!bounds.IsEmpty)
                {
                    args.Graphics.FillRectangle(b, bounds);
                    return;
                }
            }

            HighlightInfo highlight = null;
            if (args.ActiveMode && args.RenderStyle == DocumentRenderStyle.Paint)
            {
                highlight = this.Document.HighlightManager[element];
            }
            if (highlight != null)
            {
                if (args.RenderStyle == DocumentRenderStyle.Print
                    && highlight.ActiveStyle != HighlightActiveStyle.AllTime)
                {
                    // 当进行打印时不显示不能打印的高亮度色
                    return;
                }
                // 如果需要高亮度显示
                // 则绘制高亮度背景
                bounds = RectangleF.Intersect(bounds, args.ClipRectangleF);
                if (highlight.BackColor.A != 0)
                {
                    if (!bounds.IsEmpty)
                    {
                        args.Graphics.FillRectangle(
                            GraphicsObjectBuffer.GetSolidBrush(highlight.BackColor),
                            bounds);
                    }
                }
            }
        }

        public virtual void RenderBorder(
            DomElement element ,
            DocumentPaintEventArgs args,
            RectangleF bounds)
        {
            DocumentContentStyle style = element.RuntimeStyle;

            if (style.HasVisibleBorder)
            {
                using (Pen p = style.CreateBorderPen())
                {
                    style.DrawBorder(args.Graphics, p, bounds);
                }
            }
        }

        /// <summary>
        /// 绘制对象背景
        /// </summary>
        /// <param name="g">图形绘制对象</param>
        /// <param name="ClipRectangle">剪切矩形</param>
        public virtual void DrawBackground(DomElement element , DocumentPaintEventArgs args)
        {
            if (element == null || element.OwnerLine == null)
            {
                System.Console.WriteLine("");
                return;
            }

            System.Drawing.RectangleF bounds = new System.Drawing.RectangleF(
                        element.AbsLeft,
                        element.OwnerLine.AbsTop ,
                        element.Width + element.WidthFix,
                        element.OwnerLine.Height);
            DrawBackground(element, args, bounds);
        }

        private static System.Drawing.StringFormat myMeasureFormat2 = null;
         
         

        #region 计算和绘制字符元素

        private bool _UseGDIFontSize = true  ;
        /// <summary>
        /// 使用GDI模式来计算字符大小
        /// </summary>
        /// <remarks>GDI+和GDI计算字符的大小是不一样的，因此相同的RTF在
        /// 写RichTextBox控件、字板或者Word中排版就和在本程序中的排版不一样。因此本程序提供
        /// 一种使用GDI计算字符大小的方式，使得相同的文档在本程序和RichTextBox控件中显示的
        /// 保持高度的相似性。</remarks>
        public bool UseGDIFontSize
        {
            get
            {
                return _UseGDIFontSize; 
            }
            set
            {
                _UseGDIFontSize = value; 
            }
        }

        

        private static Dictionary<string, GDIFont> 
            gdiFonts = new Dictionary<string, GDIFont>();

        private Dictionary<int, Dictionary<char, SizeF>> charSizes = null;

        private bool _EnableCharSizeBuffer = true;
        /// <summary>
        /// 允许缓存字母大小
        /// </summary>
        public bool EnableCharSizeBuffer
        {
            get
            {
                return _EnableCharSizeBuffer;
            }
            set
            {
                _EnableCharSizeBuffer = value;
            }
        }

        /// <summary>
        /// 清空字符大小缓存区
        /// </summary>
        public void ClearCharSizeBuffer()
        {
            if (_EnableCharSizeBuffer)
            {
                if (charSizes == null)
                {
                    charSizes = new Dictionary<int, Dictionary<char, SizeF>>();
                }
                else
                {
                    charSizes.Clear();
                }
            }
            else
            {
                charSizes = null;
            }
        }

        private static System.Drawing.StringFormat myMeasureFormat = null;
        /// <summary>
        /// 重新计算对象大小
        /// </summary>
        /// <param name="g">图形绘制对象</param>
        protected virtual void RefreshSize( DomCharElement chr , System.Drawing.Graphics g)
        {
            DocumentViewOptions opt = this.Document.Options.ViewOptions;
            if (myMeasureFormat == null)
            {
                myMeasureFormat = new System.Drawing.StringFormat(
                    System.Drawing.StringFormat.GenericTypographic);
                myMeasureFormat.FormatFlags = System.Drawing.StringFormatFlags.FitBlackBox
                    | System.Drawing.StringFormatFlags.MeasureTrailingSpaces
                    | StringFormatFlags.NoClip ;
            }
            DomDocument document = chr.OwnerDocument;
            if (_EnableCharSizeBuffer)
            {
                if (chr.CharValue != '\t')
                {
                    if (charSizes != null)
                    {
                        if (charSizes.ContainsKey(chr.StyleIndex))
                        {
                            Dictionary<char, SizeF> sizes = charSizes[chr.StyleIndex];
                            if (sizes.ContainsKey(chr.CharValue))
                            {
                                // 从缓存区中获得字母大小
                                SizeF bsize = sizes[chr.CharValue];
                                chr.Width = bsize.Width;
                                chr.Height = bsize.Height;
                                chr._FontHeight = chr.RuntimeStyle.Font.GetHeight(g);
                                return;
                            }
                        }
                    }
                }//if
            }//if

            DocumentContentStyle rs = chr.RuntimeStyle;
            System.Drawing.SizeF size = System.Drawing.SizeF.Empty;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel ;
            //this.UseGDIFontSize = true;
            if ( opt.RichTextBoxCompatibility )
            {
                // 使用GDI模式计算字符大小
                string key = rs.FontName + "|" + rs.FontSize + "|" + rs.FontStyle ;
                GDIFont font = null;
                if (gdiFonts.ContainsKey(key))
                {
                    font = gdiFonts[key];
                }
                else
                {
                    font = new GDIFont(
                        rs.FontName,
                        //(int)( rs.FontSize *  1.3837 * 3 ),
                        (int)(GraphicsUnitConvert.Convert( 
                            rs.FontSize ,
                            GraphicsUnit.Point ,
                            GraphicsUnit.Document )),
                        rs.Bold,
                        rs.Italic,
                        rs.Underline,
                        rs.Strikeout);
                    gdiFonts[key] = font;
                }

                Size[] sizes = font.MeasureCharactersSize(g, chr.CharValue.ToString());
                size.Width = sizes[0].Width ;
                size.Height = sizes[0].Height;
                if (chr.CharValue == ' ')
                {
                    if (rs.FontName == "Times New Roman")
                    {
                        size.Width = size.Width * 1.28f ;
                    }
                }
            }
            else
            {
                if (chr.CharValue == ' ')
                {
                    size = g.MeasureString(
                        " ",
                        rs.Font.Value,
                        10000,
                        System.Drawing.StringFormat.GenericDefault);
                    size.Width = size.Width * 0.57f;
                }
                else if (chr.CharValue == '\t')
                {
                }
                else
                {
                    size = g.MeasureString(
                        chr.CharValue.ToString(),
                        rs.Font.Value,
                        10000,
                        myMeasureFormat);
                    size.Width = size.Width ;
                }
            }
            chr.Width = size.Width;
            chr.Height = size.Height + 1;
            chr._FontHeight = rs.Font.GetHeight(g);
            chr.Height = chr._FontHeight;// size.Height;
            if (rs.Superscript)
            {
                chr.Height = chr.Height * 1.2f;
            }
            if (rs.Superscript || rs.Subscript)
            {
                chr.Width = chr.Width * 0.6f;
            }
            if (_EnableCharSizeBuffer)
            {
                if (charSizes != null)
                {
                    Dictionary<char, SizeF> sizes2 = null;
                    if (charSizes.ContainsKey(chr.StyleIndex))
                    {
                        sizes2 = charSizes[chr.StyleIndex];
                    }
                    else
                    {
                        sizes2 = new Dictionary<char, SizeF>();
                        charSizes[chr.StyleIndex] = sizes2;
                    }
                    sizes2[chr.CharValue] = new SizeF(chr.Width, chr.Height);
                }
            }
        }

        private static System.Drawing.StringFormat InnerFormat
            = System.Drawing.StringFormat.GenericTypographic;
        /// <summary>
        /// 绘制对象
        /// </summary>
        /// <param name="g">图形绘制对象</param>
        /// <param name="ClipRectangle">剪切矩形</param>
        protected virtual void DrawContent(DomCharElement chr, DocumentPaintEventArgs args)
        {
            
            DocumentContentStyle rs = chr.RuntimeStyle;
            System.Drawing.RectangleF rect = chr.AbsBounds;
            rect.Height = rect.Height * 1.5f;
            rect.Width = rect.Width * 1.5f;
            //if (chr.OwnerLine.AdditionHeight < 0)
            {
                rect.Height = Math.Max(
                    rect.Height,
                    chr.OwnerLine.Height + chr.OwnerLine.AdditionHeight);
            }
            //rect.Height = Math.Min(rect.Height, chr.OwnerLine.Height);
            Color cc = rs.Color;
             
            if (args.RenderStyle == DocumentRenderStyle.Paint)
            {
                HighlightInfo info = this.Document.HighlightManager[chr];
                if (info != null && info.Color.A != 0)
                {
                    // 设置高亮度文本值
                    cc = info.Color;
                }
            }
            SolidBrush b = GraphicsObjectBuffer.GetSolidBrush(cc);
            XFontValue font = rs.Font.Clone();
            if (rs.Subscript || rs.Superscript)
            {
                font.Size = font.Size * 0.6f;
                if (rs.Superscript)
                {
                    args.Graphics.DrawString(chr.CharValue.ToString(),
                        font.Value,
                        b,
                        rect.Left,
                        rect.Top,
                        myMeasureFormat);
                }
                else
                {
                    args.Graphics.DrawString(chr.CharValue.ToString(),
                        font.Value,
                        b,
                        rect.Left,
                        (int)Math.Floor(rect.Top + (rect.Height * 0.4)),
                        myMeasureFormat);
                }
            }
            else
            {
                if ( ( rs.Underline || rs.Strikeout ) && chr.CharValue == ' ')
                {
                    // .NET框架存在一个BUG，不能为空格绘制下划线和删除线，因此在此进行替换绘制成不带下划线的下划线字母。
                    XFontValue font2 = rs.Font.Clone();
                    if (font2.Underline)
                    {
                        font2.Underline = false;
                        args.Graphics.DrawString(
                            "_",
                            font2.Value,
                            b,
                            rect.Left,
                            rect.Top,
                            myMeasureFormat);
                    }
                    else
                    {
                        font2.Strikeout = false;
                        args.Graphics.DrawString(
                            "-",
                            font2.Value,
                            b,
                            rect.Left,
                            rect.Top,
                            myMeasureFormat);
                    }
                }
                else
                {
                    //InnerFormat.FormatFlags = InnerFormat.FormatFlags | StringFormatFlags.MeasureTrailingSpaces;
                    //InnerFormat.FormatFlags = StringFormatFlags.FitBlackBox | StringFormatFlags.MeasureTrailingSpaces ;
                    args.Graphics.DrawString(
                        chr.CharValue.ToString(),
                        rs.Font.Value,
                        b,
                        rect ,
                        myMeasureFormat );
                }

            }
            //if ( font.Underline )
            //{
            //    float fh = font.GetHeight(args.Graphics);
            //    Pen p = SolidBrushBuffer.GetPen(cc);
            //    args.Graphics.DrawLine(
            //        p, 
            //        rect.Left ,
            //        rect.Top + fh , 
            //        rect.Right + chr.WidthFix, 
            //        rect.Top + fh );
            //}
        }

        #endregion

        #region 绘制段落符


        /// <summary>
        /// 刷新元素大小
        /// </summary>
        /// <param name="g">参数</param>
        protected virtual void RefreshSize( DomParagraphFlagElement eof , System.Drawing.Graphics g)
        {
            int h = (int)Math.Ceiling((double)this.Document.DefaultStyle.DefaultLineHeight);
            DocumentContentStyle rs = eof.RuntimeStyle;
            SizeF size = g.MeasureString("#", rs.Font.Value);
            eof.Height = rs.Font.GetHeight(g);// size.Height;
            eof.Width = this.Document.PixelToDocumentUnit(10);
            eof.SizeInvalid = false;
        }

        private static Bitmap _ParagraphEOFIcon = null;
        /// <summary>
        /// 绘制元素
        /// </summary>
        /// <param name="g">图形绘制对象</param>
        /// <param name="ClipRectangle">剪切矩形</param>
        protected virtual void DrawContent( DomParagraphFlagElement eof , DocumentPaintEventArgs args)
        {
            if (_ParagraphEOFIcon == null)
            {
                _ParagraphEOFIcon = (System.Drawing.Bitmap)WriterResources.paragrapheof.Clone();
                _ParagraphEOFIcon.MakeTransparent(Color.White);
            }
            if (this.Document.Options.ViewOptions.ShowParagraphFlag
                && args.RenderStyle == DocumentRenderStyle.Paint)
            {
                System.Drawing.RectangleF rect = eof.AbsBounds;
                if (args.RenderStyle == DocumentRenderStyle.Paint)
                {
                    System.Drawing.Size size = _ParagraphEOFIcon.Size;
                    size = this.Document.PixelToDocumentUnit(size);
                    System.Drawing.Drawing2D.InterpolationMode back 
                        = args.Graphics.InterpolationMode;
                    args.Graphics.InterpolationMode 
                        = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    args.Graphics.DrawImage(
                        _ParagraphEOFIcon,
                        rect.Left,
                        rect.Bottom - size.Height);
                    args.Graphics.InterpolationMode = back;
                }
            }
        }
        
        #endregion

        #region 绘制图片


        /// <summary>
        /// 绘制元素内容
        /// </summary>
        /// <param name="g">图形绘制对象</param>
        /// <param name="ClipRectangle">剪切矩形</param>
        protected virtual void DrawContent( DomImageElement imgElement , DocumentPaintEventArgs args)
        {
            System.Drawing.Image img = null;
            if (args.RenderStyle == DocumentRenderStyle.Paint)
            {
                img = imgElement.PreviewImage;
            }
            else
            {
                img = imgElement.Image.Value;
            }
            System.Drawing.RectangleF bounds = imgElement.AbsBounds;
            if (img != null)
            {
                args.Graphics.DrawImage(
                    img,
                    bounds.X,
                    bounds.Y,
                    imgElement.Width,
                    imgElement.Height);
            }
            else
            {
                using (System.Drawing.StringFormat f
                    = new System.Drawing.StringFormat())
                {
                    f.Alignment = System.Drawing.StringAlignment.Center;
                    f.LineAlignment = System.Drawing.StringAlignment.Center;
                    args.Graphics.DrawString(
                        WriterStrings.NoImage,
                        System.Windows.Forms.Control.DefaultFont,
                        System.Drawing.Brushes.Red,
                        bounds,
                        f);
                }
            }
            //base.DrawContent( g , ClipRectangle );
        }

        #endregion
         

        #region 绘制分行符


        /// <summary>
        /// 刷新元素大小
        /// </summary>
        /// <param name="g">参数</param>
        protected virtual void RefreshSize( DomLineBreakElement element , System.Drawing.Graphics g)
        {
            float h = this.Document.DefaultStyle.DefaultLineHeight;
            //			XTextElement e = myOwnerDocument.Content.GetPreElement( this );
            //			if( e != null && e.Height > 0 && e.OwnerLine == this.OwnerLine )
            //			{
            //				h = e.Height ;
            //			}
            //			h = Math.Max( h , myOwnerDocument.PixelToDocumentUnit( 10 ));
            element.Height = h;
            element.Width = this.Document.PixelToDocumentUnit(10);
            element.SizeInvalid = false;
            //			this.intHeight = ( int ) Math.Ceiling( myOwnerDocument.BodyFont.GetHeight( g ));// size.Height );
            //			this.intWidth = ( int ) ( intHeight * 0.8 );
            ////			System.Drawing.SizeF size = g.MeasureString( "H" , myOwnerDocument.BodyFont , 1000 , System.Drawing.StringFormat.GenericTypographic );
            ////			this.Width = ( int ) Math.Ceiling( size.Height );
            ////			this.Height = ( int ) Math.Ceiling( size.Height );
            //			this.bolSizeInvalid = false ;
        }

        private static Bitmap _LineBreakIcon = null;
        
        /// <summary>
        /// 绘制元素
        /// </summary>
        /// <param name="g">图形绘制对象</param>
        /// <param name="ClipRectangle">剪切矩形</param>
        protected virtual void DrawContent( DomLineBreakElement element , DocumentPaintEventArgs args)
        {
            if (_LineBreakIcon == null)
            {
                _LineBreakIcon = ( Bitmap ) WriterResources.linebreak.Clone();
                _LineBreakIcon.MakeTransparent(System.Drawing.Color.White);
            }
            System.Drawing.RectangleF rect = element.AbsBounds;
            if (args.RenderStyle == DocumentRenderStyle.Paint)
            {
                System.Drawing.Size size = _LineBreakIcon.Size;
                size = this.Document.PixelToDocumentUnit(size);
                args.Graphics.DrawImage(_LineBreakIcon, rect.Left, rect.Bottom - size.Height);
                //WriterUtils.DrawParagraphFlag( g , rect );
            }
        }

        #endregion
         

        //#region 计算和绘制文本区域结束标记

        //protected virtual void RefreshSize(XTextFieldEOFElement element, Graphics g)
        //{
        //    element.Height = this.Document.DefaultStyle.DefaultLineHeight;
        //    element.Width = this.Document.PixelToDocumentUnit(4);
        //}

        //protected virtual void DrawContent(XTextFieldEOFElement element, DocumentPaintEventArgs args)
        //{
        //    if (args.RenderStyle == DocumentRenderStyle.Paint )
        //    {
        //        System.Drawing.RectangleF bounds = element.AbsBounds  ;
        //        args.Graphics.FillRectangle(System.Drawing.Brushes.Black, bounds);
        //    }
        //}

        //protected virtual void RefreshSize(XTextDropDownFieldElement element, Graphics g)
        //{
        //    element.Height = this.Document.DefaultStyle.DefaultLineHeight;
        //    element.Width = this.Document.PixelToDocumentUnit(XTextDropDownFieldElement.IconBmp.Width+2);

        //    RefreshSize(element.EOFElement, g);

        //    //XTextFieldEOFElement eof = ( XTextFieldEOFElement) element.EOFElement;
        //    //eof.Height = element.Height;
        //    //eof.Width = this.Document.PixelToDocumentUnit(4);

        //    foreach (XTextElement e in element.Elements)
        //    {
        //        RefreshSize(e, g);
        //    }
        //}

        //protected virtual void DrawContent(XTextDropDownFieldElement element, DocumentPaintEventArgs args)
        //{
        //    if (args.RenderStyle == DocumentRenderStyle.Paint)
        //    {
        //        RectangleF rect = element.AbsBounds;
        //        float bmpHeight = this.Document.PixelToDocumentUnit(XTextDropDownFieldElement.IconBmp.Height);
        //        args.Graphics.DrawImage(
        //            XTextDropDownFieldElement.IconBmp,
        //            rect.Left ,
        //            rect.Top + (rect.Height - bmpHeight ) / 2 );
        //        args.Graphics.DrawRectangle(
        //            Pens.Black,
        //            rect.Left ,
        //            rect.Top ,
        //            rect.Width - 2 ,
        //            rect.Height );
        //    }
        //}
        //#endregion
    }
}
