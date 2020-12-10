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
using DCSoft.RTF;
using System.Drawing;
using DCSoft.Drawing;
using DCSoft.Printing;
using DCSoft.CSharpWriter.Dom;
using DCSoft.Common;
using System.Xml.Serialization;
using System.IO;
using DCSoft.CSharpWriter.Xml;

namespace DCSoft.CSharpWriter.RTF
{
    internal class RTFLoader
    {

        private bool bolChangeTimesNewRoman = false;
 
        private string strDefaultFontName
            = System.Windows.Forms.Control.DefaultFont.Name;
       

        private bool bolEnableDocumentSetting = true;
        /// <summary>
        /// 允许读取文档设置
        /// </summary>
        public bool EnableDocumentSetting
        {
            get
            {
                return bolEnableDocumentSetting;
            }
            set
            {
                bolEnableDocumentSetting = value;
            }
        }


        ///// <summary>
        ///// 当前段落
        ///// </summary>
        //private XTextParagraphFlagElement CurrentParagraphEOF = null;

        private RTFDomDocument _RTFDocument = new RTFDomDocument();

        /// <summary>
        /// 从指定的文本读取器中加载RTF文档
        /// </summary>
        /// <param name="reader">文本读取器</param>
        /// <returns>操作是否成功</returns>
        public virtual bool Load(System.IO.TextReader reader)
        {
            _RTFDocument.Load(reader);
            _RTFDocument.FixForParagraphs( _RTFDocument );
            //format.Reset();
            //CurrentParagraphEOF = null;
            return true;
        }
        /// <summary>
        /// 从指定的RTF字符串加载RTF文档
        /// </summary>
        /// <param name="rtf">RTF文档字符串</param>
        public virtual bool LoadRTFText(string rtf)
        {
            _RTFDocument.LoadRTFText(rtf);
            _RTFDocument.FixForParagraphs( _RTFDocument );
            //format.Reset();
            //CurrentParagraphEOF = null;
            return true;
        }


        /// <summary>
        /// 从RTF文档生成文本文档内容对象
        /// </summary>
        /// <param name="document">文本文档对象</param>
        /// <returns>包含生成的内容对象的列表</returns>
        public void ReadContent(DomDocument document)
        {
            document.Clear();
            if (this.EnableDocumentSetting)
            {
                XPageSettings ps = new XPageSettings();
                ps.Landscape = _RTFDocument.Landscape;
                ps.LeftMargin = GraphicsUnitConvert.FromTwips(
                    _RTFDocument.LeftMargin, GraphicsUnit.Document) / 3;
                ps.TopMargin = GraphicsUnitConvert.FromTwips(
                    _RTFDocument.TopMargin, GraphicsUnit.Document) / 3;
                ps.RightMargin = GraphicsUnitConvert.FromTwips(
                    _RTFDocument.RightMargin, GraphicsUnit.Document) / 3;
                ps.BottomMargin = GraphicsUnitConvert.FromTwips(
                    _RTFDocument.BottomMargin, GraphicsUnit.Document) / 3;
                ps.PaperWidth = GraphicsUnitConvert.FromTwips(
                    _RTFDocument.PaperWidth, GraphicsUnit.Document) / 3;
                ps.PaperHeight = GraphicsUnitConvert.FromTwips(
                    _RTFDocument.PaperHeight, GraphicsUnit.Document) / 3;
                document.PageSettings = ps;

                document.Info.Title = _RTFDocument.Info.Title;
                document.Info.Author = _RTFDocument.Info.Author;
                document.Info.CreationTime = _RTFDocument.Info.Creatim;
                document.Info.Description = _RTFDocument.Info.Comment;
                document.Info.LastPrintTime = _RTFDocument.Info.Printim;
                document.Info.LastModifiedTime = _RTFDocument.Info.Revtim;
                document.Info.EditMinute = _RTFDocument.Info.edmins;
            }
            document.Elements.Clear();
            document.Initializing = true;
            ReadContent(
                _RTFDocument , 
                document ,
                document.Elements ,
                new DocumentFormatInfo());
            document.Initializing = false;
            document.AfterLoad(FileFormat.RTF);
        }

        public DomElementList ImportContent(DomDocument document)
        {
            DomElementList result = new DomElementList();
            ReadContent(
                _RTFDocument, 
                document , 
                result, 
                new DocumentFormatInfo());
            return result;
        }

        /// <summary>
        /// 将RTF文档内容填充到文本文档中
        /// </summary>
        /// <param name="document">文本文档对象</param>
        public void FillTo(DomDocument document)
        {
            document.Clear();

            ReadContent(document);
             
            //document.FixElements();
        }


        public static DocumentFormatInfo ToDocumentFormatInfo(
            DocumentContentStyle style,
            GraphicsUnit documentUnit )
        {
            if (style == null)
            {
                throw new ArgumentNullException("style");
            }
            DocumentFormatInfo result = new DocumentFormatInfo();
            switch (style.Align)
            {
                case DocumentContentAlignment.Left:
                    result.Align = RTFAlignment.Left;
                    break;
                case DocumentContentAlignment.Center:
                    result.Align = RTFAlignment.Center;
                    break;
                case DocumentContentAlignment.Right:
                    result.Align = RTFAlignment.Right;
                    break;
                case DocumentContentAlignment.Justify:
                    result.Align = RTFAlignment.Justify;
                    break;
            }
            result.BackColor = style.BackgroundColor;
            result.Bold = style.Bold;
            result.BulletedList = style.BulletedList;
            result.FontName = style.FontName;
            result.FontSize = style.FontSize;
            result.Italic = style.Italic;
            
            result.LeftBorder = style.BorderLeft;
            result.TopBorder = style.BorderTop;
            result.RightBorder = style.BorderRight;
            result.BottomBorder = style.BorderBottom;
            result.BorderColor = style.BorderColor;
            result.BorderStyle = style.BorderStyle;
            result.BorderSpacing = (int)GraphicsUnitConvert.ToTwips(
                style.BorderSpacing,
                documentUnit);
            result.LeftIndent = (int)GraphicsUnitConvert.ToTwips(
                style.LeftIndent,
                documentUnit);
            result.LineSpacing = (int)GraphicsUnitConvert.ToTwips(
                style.LineSpacing, 
                documentUnit);
            result.Link = style.Link;
            result.NumberedList = style.NumberedList;
            result.ParagraphFirstLineIndent = (int)GraphicsUnitConvert.ToTwips(
                style.FirstLineIndent,
                documentUnit);
            result.Spacing = (int)GraphicsUnitConvert.ToTwips(
                style.Spacing,
                documentUnit);
            result.Strikeout = style.Strikeout;
            result.Subscript = style.Subscript;
            result.Superscript = style.Superscript;
            result.TextColor = style.Color;
            result.Underline = style.Underline;
            return result;
        }

        public static DocumentContentStyle ToDocumentContentStyle(
            DocumentFormatInfo format,
            GraphicsUnit documentUnit )
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }

            DocumentContentStyle result = new DocumentContentStyle();
            switch (format.Align)
            {
                case RTFAlignment.Left:
                    result.Align = DocumentContentAlignment.Left;
                    break;
                case RTFAlignment.Center:
                    result.Align = DocumentContentAlignment.Center;
                    break;
                case RTFAlignment.Right:
                    result.Align = DocumentContentAlignment.Right;
                    break;
                case RTFAlignment.Justify:
                    result.Align = DocumentContentAlignment.Justify;
                    break;
            }
            result.BackgroundColor = format.BackColor;
            if (format.BackColor == Color.White)
            {
                result.BackgroundColor = Color.Transparent;
            }
            result.Bold = format.Bold;
            result.BorderColor = format.BorderColor;
            result.BorderStyle = format.BorderStyle;
            result.BorderLeft = format.LeftBorder;
            result.BorderTop = format.TopBorder;
            result.BorderBottom = format.BottomBorder;
            result.BorderRight = format.RightBorder ;
            result.BorderWidth = format.BorderWidth;
            result.BorderSpacing = GraphicsUnitConvert.FromTwips(
                format.BorderSpacing,
                documentUnit);
            if ( format.LeftBorder 
                || format.TopBorder 
                || format.RightBorder 
                || format.BottomBorder )
            {
                if ( format.BorderThickness )
                {
                    result.BorderWidth = 2;
                }
                else
                {
                    result.BorderWidth = 1;
                }
            }
            result.BulletedList = format.BulletedList;
            result.FontName = format.FontName;
            result.FontSize = format.FontSize;
            result.Italic = format.Italic;
            result.LeftIndent = GraphicsUnitConvert.FromTwips(
                format.LeftIndent,
                documentUnit);

            if (format.LineSpacing == 0)
            {
                // 单倍行距
                result.LineSpacingStyle = LineSpacingStyle.SpaceSingle;
            }
            else if (format.LineSpacing < 0)
            {
                // 行间距是固定值
                result.LineSpacingStyle = LineSpacingStyle.SpaceSpecify;
                result.LineSpacing = GraphicsUnitConvert.FromTwips(format.LineSpacing, documentUnit);
            }
            else
            {
                if (format.MultipleLineSpacing)
                {
                    // 多倍行距
                    result.LineSpacingStyle = LineSpacingStyle.SpaceMultiple;
                    result.LineSpacing = format.LineSpacing / 240.0f;
                }
                else
                {
                    // 最小行距
                    result.LineSpacingStyle = LineSpacingStyle.SpaceExactly;
                }
            }

            result.Link = format.Link;
            result.NumberedList = format.NumberedList;
            result.FirstLineIndent = GraphicsUnitConvert.FromTwips(
                format.ParagraphFirstLineIndent, 
                documentUnit);
            result.Spacing = GraphicsUnitConvert.FromTwips(
                format.Spacing,
                documentUnit);
            result.SpacingBeforeParagraph = GraphicsUnitConvert.FromTwips(
                format.SpacingBefore,
                documentUnit);
            result.SpacingAfterParagraph = GraphicsUnitConvert.FromTwips(
                format.SpacingAfter,
                documentUnit);
            result.Strikeout = format.Strikeout;
            result.Subscript = format.Subscript;
            result.Superscript = format.Superscript;
            result.Color = format.TextColor;
            result.Underline = format.Underline;

            return result;
        }

        private bool _ImportTemplateGenerateParagraph = true;
        /// <summary>
        /// 是否导入临时生成段落对象
        /// </summary>
        public bool ImportTemplateGenerateParagraph
        {
            get { return _ImportTemplateGenerateParagraph; }
            set { _ImportTemplateGenerateParagraph = value; }
        }

        private void ReadContent(
            RTFDomElement parentNode,
            DomDocument doc,
            DomElementList result,
            DocumentFormatInfo format)
        {
            if (format == null)
            {
                format = new DocumentFormatInfo();
            }
            //if ( parentNode == null || parentNode.Elements == null)
            //{
            //    System.Console.WriteLine("");
            //    return;
            //}
            foreach (RTFDomElement element in parentNode.Elements )
            {
                if (element is RTFDomHeader)
                {
                    if (((RTFDomHeader)element).HasContentElement)
                    {
                        XTextDocumentHeaderElement header = new XTextDocumentHeaderElement();
                        doc.PageSettings.HeaderDistance = (int)(GraphicsUnitConvert.FromTwips(
                            (double)this._RTFDocument.HeaderDistance,
                            GraphicsUnit.Inch) * 100.0);
                        result.Add(header);
                        ReadContent(element, doc, header.Elements, format);
                    }
                }
                else if (element is RTFDomFooter)
                {
                    if (((RTFDomFooter)element).HasContentElement)
                    {
                        XTextDocumentFooterElement footer = new XTextDocumentFooterElement();
                        doc.PageSettings.FooterDistance = (int)(GraphicsUnitConvert.FromTwips(
                            (double)this._RTFDocument.FooterDistance,
                            GraphicsUnit.Inch) * 100.0);
                        result.Add(footer);
                        ReadContent(element, doc, footer.Elements, format);
                    }
                }
                else if (element is RTFDomParagraph)
                {
                    RTFDomParagraph domP = (RTFDomParagraph)element;
                    //XTextParagraph p = new XTextParagraph();
                    //p.OwnerDocument = doc;
                    DocumentContentStyle style = ToDocumentContentStyle(
                        domP.Format,
                        doc.DocumentGraphicsUnit );
                    ReadContent(element, doc, result, domP.Format);

                    if (domP.IsTemplateGenerated == false
                        || this.ImportTemplateGenerateParagraph)
                    {
                        DomParagraphFlagElement eof = new DomParagraphFlagElement();
                        eof.StyleIndex = doc.ContentStyles.GetStyleIndex(style);
                        result.Add(eof);
                    }
                }
                else if (element is RTFDomText)
                {
                    RTFDomText domText = (RTFDomText)element;
                    if (domText.Format.Hidden == false
                        && domText.Text != null
                        && domText.Text.Length > 0)
                    {
                        DocumentContentStyle style = ToDocumentContentStyle(
                            domText.Format,
                            doc.DocumentGraphicsUnit );
                        int si = doc.ContentStyles.GetStyleIndex(style);
                        result.AddRange(doc.CreateChars(domText.Text, si));
                    }
                }
                 
                else if (element is RTFDomObject)
                {
                    // 插入对象
                    RTFDomObject domObj = (RTFDomObject)element;
                    if (domObj.ClassName != null && domObj.ClassName.StartsWith( RTFContentWriter.XWriterObjectPrefix))
                    {
                        // 内嵌的XTextDocument对象
                        
                        // 获得类型名称
                        string typeName = domObj.ClassName.Substring(RTFContentWriter.XWriterObjectPrefix.Length);
                        // 获得文档元素类型
                        Type elementType = Type.GetType(typeName , true , true );
                        if (elementType != null)
                        {
                            if (domObj.Content != null && domObj.Content.Length > 0 )
                            {
                                // 若有内容则进行XML反序列化
                                string attstr = System.Text.Encoding.UTF8.GetString(domObj.Content);
                                StringReader reader = new StringReader(attstr);
                                XmlSerializer ser = MyXmlSerializeHelper.GetElementXmlSerializer(elementType);
                                DomElement newElement = (DomElement)ser.Deserialize(reader);
                                if (newElement != null)
                                {
                                    newElement.OwnerDocument = doc;
                                }
                                result.Add(newElement);
                            }
                            else
                            {
                                // 若无内容则直接创建对象
                                DomElement newElement = (DomElement)System.Activator.CreateInstance(elementType);
                                newElement.OwnerDocument = doc;
                                result.Add(newElement);
                            }
                        }
                    }
                    else
                    {
                        
                         
                    }
                }
                else if (element is RTFDomField)
                {
                    RTFDomField domField = (RTFDomField)element;
                    if (domField.Result != null)
                    {
                        string fldinst = domField.Instructions;
                         
                        {
                            ReadContent(domField.Result, doc, result, format.Clone());
                        }
                    }
                }
                 
                else if (element is RTFDomShape)
                {
                }
                else if (element is RTFDomShapeGroup)
                {
                }
                else if (element is RTFDomLineBreak)
                {
                    // 软回车
                    result.Add(new DomLineBreakElement());// doc.CreateLineBreak());
                }
                else if (element is RTFDomPageBreak)
                {
                    // 强制换页符
                    result.Add(new DomPageBreakElement());
                }
                else if (element.Elements != null
                    && element.Elements.Count > 0)
                {
                    ReadContent(element, doc, result, format.Clone());
                }
            }//foreach
        }
    }
}
