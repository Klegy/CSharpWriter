/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections ;
using DCSoft.RTF ;
using DCSoft.WinForms ;
using DCSoft.Printing ;
using DCSoft.Drawing;
using System.IO;
using System.Drawing ;
using System.Drawing.Drawing2D ;
using DCSoft.CSharpWriter.Dom;
using DCSoft.Common;
using System.Text;
using System.Xml.Serialization;

namespace DCSoft.CSharpWriter.RTF
{
	
	/// <summary>
	/// RTF文档生成器
	/// </summary>
	public class RTFContentWriter : DocumentContentWriter
	{
        /// <summary>
        /// 初始化对象
        /// </summary>
        public RTFContentWriter( )
        {
        }

        /// <summary>
		/// 初始化对象
		/// </summary>
		public RTFContentWriter( TextWriter writer )
		{
            this._Writer = new RTFDocumentWriter(writer);
            this._Writer.CollectionInfo = false;
		}

        private RTFDocumentWriter _Writer = null;
         
		public virtual bool Open( System.IO.TextWriter writer )
		{
            _Writer = new RTFDocumentWriter(writer);
            _Writer.CollectionInfo = false;
			return true ;
		}
	 
		public virtual void Close()
		{
			_Writer.Close();
		}

        public RTFDocumentWriter Writer
        {
            get
            {
                return _Writer;
            }
            set
            {
                _Writer = value;
            }
        }

        ////private bool bolCollectionInfo = true ;
        ///// <summary>
        ///// 正在收集文档信息,并不真的输出文档
        ///// </summary>
        //public bool CollectionInfo
        //{
        //    get
        //    {
        //        return _Writer.CollectionInfo;
        //    }
        //    set
        //    {
        //        _Writer.CollectionInfo = value;
        //    }
        //}

        private bool bolForcePage = false;
        /// <summary>
        /// 强制分页
        /// </summary>
        public bool ForcePage
        {
            get
            {
                return bolForcePage;
            }
            set
            {
                bolForcePage = value;
            }
        }


        private bool bolOutputHeaderFooter = true;
        /// <summary>
        /// 输出页眉页脚
        /// </summary>
        public bool OutputHeaderFooter
        {
            get
            {
                return bolOutputHeaderFooter;
            }
            set
            {
                bolOutputHeaderFooter = value;
            }
        }

        ///// <summary>
        ///// 正在输出页眉页脚
        ///// </summary>
        //private bool bolWritingHeaderFooter = false;
        ///// <summary>
        ///// 正在输出页眉页脚
        ///// </summary>
        //internal bool WritingHeaderFooter
        //{
        //    get
        //    {
        //        return bolWritingHeaderFooter;
        //    }
        //}

        private DomElement _LastOutputElement = null;
        /// <summary>
        /// 最后一次输出的元素
        /// </summary>
        public DomElement LastOutputElement
        {
            get { return _LastOutputElement; }
            set { _LastOutputElement = value; }
        }

        public int ToTwips(float Value)
        {
            return GraphicsUnitConvert.ToTwips(Value, this.MainDocument.DocumentGraphicsUnit);
        }

        public const string XWriterObjectPrefix = "XWriter.Object:";

        /// <summary>
        /// 输出嵌入在RTF文档中的文档元素对象
        /// </summary>
        /// <param name="element">文档元素对象</param>
        /// <param name="attributes">属性列表</param>
        /// <param name="resultImage">表示元素内容的图片对象</param>
        public void WriteEmbObject(
            DomElement element ,
            Image resultImage )
        {
            bool disposeImage = false ;
            if( resultImage == null )
            {
                resultImage = element.CreateContentImage();
                disposeImage = true ;
            }
            Size size = new Size( 
                GraphicsUnitConvert.ToTwips( resultImage.Width , GraphicsUnit.Pixel ) ,
                GraphicsUnitConvert.ToTwips( resultImage.Height , GraphicsUnit.Pixel ));

            this.WriteStartGroup();
            this.WriteKeyword("object");
            this.WriteKeyword("objemb");
            this.WriteCustomAttribute("objclass", XWriterObjectPrefix + element.GetType().FullName , true);
            this.WriteKeyword("objw"+ size.Width );
            this.WriteKeyword("objh" + size.Height);

            StringWriter writer = new StringWriter();
            XmlSerializer ser = Xml.MyXmlSerializeHelper.GetElementXmlSerializer(element.GetType());
            ser.Serialize(writer, element);
            writer.Close();
            string xmlText = writer.ToString();
            byte[] bs = Encoding.UTF8.GetBytes( xmlText );
            this.WriteStartGroup();
            this.WriteKeyword("objdata", true);
            this.WriteBytes(bs);
            this.WriteEndGroup();

            this.WriteStartGroup();

            this.WriteKeyword("result");
            this.WriteImage(
                resultImage ,
                resultImage.Width ,
                resultImage.Height ,
                null,
                element.RuntimeStyle);
            this.WriteEndGroup();
            
            this.WriteEndGroup();

            if (disposeImage)
            {
                resultImage.Dispose();
            }
        }

        /// <summary>
        /// 当前组合等级
        /// </summary>
        public int GroupLevel
        {
            get
            {
                return this.Writer.Writer.GroupLevel;
            }
        }

        public void WriteStartGroup()
        {
            this.Writer.WriteStartGroup();
        }

        public void WriteEndGroup()
        {
            this.Writer.WriteEndGroup();
        }

        public void WriteKeyword(string name, bool ext)
        {
            this.Writer.WriteKeyword(name, ext);
        }

        public void WriteBytes(byte[] bs)
        {
            this.Writer.Writer.WriteBytes(bs);
        }

        /// <summary>
        /// 输出自定义属性
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="text">属性值</param>
        /// <param name="extKeyWord">是否采用扩展关键字模式</param>
        public void WriteCustomAttribute(string name, string text , bool extKeyWord )
        {
            this.Writer.WriteStartGroup();
            this.Writer.Writer.WriteKeyword( name , extKeyWord );
            this.Writer.WriteText(text);
            this.Writer.WriteEndGroup();
        }

        /// <summary>
        /// 直接输出一个RTF指令
        /// </summary>
        /// <param name="key"></param>
        public void WriteKeyword(string key)
        {
            this.Writer.WriteKeyword(key);
        }

        public void CollectionDocumentsStyle( )
        {
            foreach (DomDocument document2 in this.Documents)
            {
                this.Writer.FontTable.Add(document2.DefaultStyle.FontName);
                this.Writer.ColorTable.Add(document2.DefaultStyle.Color);
                foreach (DocumentContentStyle style in document2.ContentStyles.Styles)
                {
                    this.Writer.FontTable.Add(style.FontName);
                    this.Writer.ColorTable.Add(style.Color);
                    this.Writer.ColorTable.Add(style.BackgroundColor);
                    this.Writer.ColorTable.Add(style.BorderColor);
                }
            }
        }

        public void WriteStartDocument(DomDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            this.Writer.CollectionInfo = false;
            this.Writer.Info["title"] = document.Info.Title;
            this.Writer.Info["subject"] = document.Info.Description;
            this.Writer.Info["author"] = document.Info.Author;
            this.Writer.Info["creatim"] = document.Info.CreationTime;
            this.Writer.Info["comment"] = document.Info.Comment;
            this.Writer.Info["operator"] = document.Info.Operator;
            this.Writer.WriteStartDocument();
            XPageSettings ps = document.PageSettings;
            // 输出纸张大小
            this.Writer.WriteKeyword("paperw" + ToTwips(ps.ViewPaperWidth));
            this.Writer.WriteKeyword("paperh" + ToTwips(ps.ViewPaperHeight));
            // 横向打印
            if (ps.Landscape)
            {
                this.Writer.WriteKeyword("landscape");
            }
            // 左右页边距
            this.Writer.WriteKeyword("margl" + ToTwips(ps.ViewLeftMargin));
            this.Writer.WriteKeyword("margr" + ToTwips(ps.ViewRightMargin));

            //int fix = 80 ;
            int fix = 0;

            if (this.ForcePage)
            {
                fix = 80;
            }

            this.Writer.WriteKeyword("margt" + this.ToTwips(ps.ViewTopMargin - fix));
            this.Writer.WriteKeyword("margb" + this.ToTwips(ps.ViewBottomMargin - fix));
            if (this.OutputHeaderFooter)
            {
                if (ps.FooterDistance > 0)
                {
                    this.Writer.WriteKeyword("footery" + this.ToTwips(ps.ViewFooterDistance - fix));
                }
                if (ps.HeaderDistance > 0)
                {
                    this.Writer.WriteKeyword("headery" + this.ToTwips(ps.ViewHeaderDistance - fix));
                }
            }
        }

		/// <summary>
		/// 开始输出文档
		/// </summary>
        public void WriteStartDocument()
        {
            DomDocument mainDocument = this.MainDocument;
            WriteStartDocument(this.MainDocument);
          
         }

        public override void WriteElement(DomElement element)
        {
            element.WriteRTF(this);
        }


        /// <summary>
        /// 结束输出文档
        /// </summary>
        public void WriteEndDocument()
        {
            _Writer.WriteEndDocument();
        }

        ///// <summary>
        ///// 开始输出页眉
        ///// </summary>
        //public override void WriteStartHeader()
        //{
        //    _Writer.WriteStartHeader();
        //}

        ///// <summary>
        ///// 结束输出页眉
        ///// </summary>
        //public override void WriteEndHeader()
        //{
        //    _Writer.WriteEndHeader();
        //}

        ///// <summary>
        ///// 开始输出页脚
        ///// </summary>
        //public override void WriteStartFooter()
        //{
        //    _Writer.WriteStartFooter();
        //}

        ///// <summary>
        ///// 结束输出页脚
        ///// </summary>
        //public override void WriteEndFooter()
        //{
        //    _Writer.WriteEndFooter();
        //}


        private DocumentContentStyle myLastParagraphInfo = null;

		internal bool bolFirstParagraph = true ;
        
		/// <summary>
		/// 开始输出段落
		/// </summary>
		/// <param name="style">文档格式信息</param>
        public void WriteStartParagraph(DocumentContentStyle style)
        {
            if (bolFirstParagraph)
            {
                bolFirstParagraph = false;
                this.Writer.Writer.WriteRaw(System.Environment.NewLine);
                //this.Writer.WriteKeyword("par");
            }
            else
            {
                this.Writer.WriteKeyword("par");
            }
            if (style.NumberedList || style.BulletedList)
            {
                if (style.NumberedList)
                {
                    // 输出数字列表样式
                    if (myLastParagraphInfo == null
                        || myLastParagraphInfo.NumberedList != style.NumberedList)
                    {
                        this.Writer.WriteKeyword("pard");
                        this.Writer.WriteStartGroup();
                        this.Writer.WriteKeyword("pn", true);
                        this.Writer.WriteKeyword("pnlvlbody");
                        this.Writer.WriteKeyword("pnindent" + ToTwips(style.FirstLineIndent));
                        this.Writer.WriteKeyword("pnstart1");
                        this.Writer.WriteKeyword("pndec");
                        this.Writer.WriteEndGroup();
                    }
                }
                else
                {
                    if (myLastParagraphInfo == null
                        || myLastParagraphInfo.BulletedList != style.BulletedList)
                    {
                        //输出圆点列表样式
                        this.Writer.WriteKeyword("pard");
                        this.Writer.WriteStartGroup();
                        this.Writer.WriteKeyword("pn", true);
                        this.Writer.WriteKeyword("pnlvlblt");
                        this.Writer.WriteKeyword("pnindent" + ToTwips(style.FirstLineIndent));
                        this.Writer.WriteKeyword("pnf" + this.Writer.FontTable.IndexOf("Wingdings"));
                        this.Writer.WriteStartGroup();
                        this.Writer.WriteKeyword("pntxtb");
                        this.Writer.WriteText("l");
                        //this.Writer.WriteKeyword("'B7");
                        this.Writer.WriteEndGroup();
                        this.Writer.WriteEndGroup();
                    }
                }
                //if (style.FirstLineIndent > 0)
                {
                    //    this.Writer.WriteKeyword("fi" + ToTwips(style.FirstLineIndent));
                }
            }
            else
            {
                if (myLastParagraphInfo != null)
                {
                    if (myLastParagraphInfo.NumberedList || myLastParagraphInfo.BulletedList)
                    {
                        this.Writer.WriteKeyword("pard");
                    }
                }
            }
            // 输出边框样式
            WriteBorderStyle(style, true);

            // 内容对齐方式
            if (style.Align == DocumentContentAlignment.Left)
            {
                this.Writer.WriteKeyword("ql");
            }
            if (style.Align == DocumentContentAlignment.Center)
            {
                this.Writer.WriteKeyword("qc");
            }
            else if (style.Align == DocumentContentAlignment.Right)
            {
                this.Writer.WriteKeyword("qr");
            }
            else if (style.Align == DocumentContentAlignment.Justify)
            {
                this.Writer.WriteKeyword("qj");
            }
            //if (style.NumberedList == false && style.BulletedList == false)
            {
                if (style.FirstLineIndent != 0)
                {
                    this.Writer.WriteKeyword("fi" + ToTwips(style.FirstLineIndent));
                }
                else
                {
                    this.Writer.WriteKeyword("fi0");
                }
            }
            //if( info.NumberedList == false && info.BulletedList == false )
            {
                if (style.LeftIndent != 0)
                {
                    this.Writer.WriteKeyword("li" + ToTwips(style.LeftIndent));
                }
                else
                {
                    this.Writer.WriteKeyword("li0");
                }
            }
            if (style.SpacingBeforeParagraph > 0.1 )
            {
                // 输出段落前间距
                this.WriteKeyword("sb" + ToTwips( style.SpacingBeforeParagraph ));
            }
            if( style.SpacingAfterParagraph >0.1)
            {
                // 输出段落后间距
                this.WriteKeyword("sa" + ToTwips( style.SpacingAfterParagraph ));
            }
            // 输出行间距
            switch (style.LineSpacingStyle)
            {
                case LineSpacingStyle.SpaceSingle :
                    this.WriteKeyword("slmult0");
                    break;
                case LineSpacingStyle.SpaceSpecify :
                    this.WriteKeyword("sl" + ToTwips(style.LineSpacing));
                    break;
                case LineSpacingStyle.SpaceMultiple :
                    this.WriteKeyword("slmult1");
                    this.WriteKeyword("sl" + Convert.ToInt32(ContentStyle._StandLineHeightTwips * style.LineSpacing ));
                    break;
                case LineSpacingStyle.Space1pt5:
                    this.WriteKeyword("slmult1");
                    this.WriteKeyword("sl" + Convert.ToInt32( ContentStyle._StandLineHeightTwips * 1.5));
                    break;
                case LineSpacingStyle.SpaceDouble:
                this.WriteKeyword("slmult1");
                    this.WriteKeyword("sl" + Convert.ToInt32( ContentStyle._StandLineHeightTwips * 2 ));
                        break;
                case LineSpacingStyle.SpaceExactly:
                    break;
            }
            //this.Writer.WriteKeyword("plain");
            myLastParagraphInfo = style;
        }

        /// <summary>
        /// 输出边框线样式
        /// </summary>
        /// <param name="style"></param>
        private void WriteBorderStyle(DocumentContentStyle style, bool forParagrah )
        {
            if (style.BorderWidth <= 0)
            {
                // 无边框
                return;
            }
            if (style.BorderLeft == false
                && style.BorderTop == false
                && style.BorderRight == false
                && style.BorderBottom == false )
            {
                // 无边框
                return;
            }
            // 边框颜色
            this.Writer.WriteKeyword(
                "brdrcf"
                + Convert.ToString(
                    this.Writer.ColorTable.IndexOf(style.BorderColor)+1));

            // 边框线
            if (forParagrah)
            {
                //this.Writer.WriteKeyword("");
                if (style.BorderLeft)
                {
                    this.Writer.WriteKeyword("brdrl");
                }
                if (style.BorderTop)
                {
                    this.Writer.WriteKeyword("brdrt");
                }
                if (style.BorderRight)
                {
                    this.Writer.WriteKeyword("brdrr");
                }
                if (style.BorderBottom)
                {
                    this.Writer.WriteKeyword("brdrb");
                }
                if (style.BorderSpacing > 0)
                {
                    this.Writer.WriteKeyword("brsp" + this.ToTwips(style.BorderSpacing));
                }
            }
            else
            {
                if (style.BorderLeft
                    || style.BorderTop
                    || style.BorderRight
                    || style.BorderBottom)
                {
                    this.Writer.WriteKeyword("chbrdr");
                }
            }
            switch (style.BorderStyle)
            {
                case DashStyle.Dash :
                    this.Writer.WriteKeyword("brdrdash");
                    break;
                case DashStyle.DashDot :
                    this.Writer.WriteKeyword("brdrdashd");
                    break;
                case DashStyle.DashDotDot :
                    this.Writer.WriteKeyword("brdrdashdd");
                    break;
                case DashStyle.Dot :
                    this.Writer.WriteKeyword("brdrdot");
                    break;
            }
            if (style.BorderWidth == 1)
            {
                this.Writer.WriteKeyword("brdrs");
            }
            else
            {
                this.Writer.WriteKeyword("brdrth");
            }
        }

		/// <summary>
		/// 结束输出段落
		/// </summary>
		public void WriteEndParagraph()
		{
		}
         

		/// <summary>
		/// 开始输出字符串
		/// </summary>
		/// <param name="style">格式信息</param>
        public void WriteStartString(string strText, DocumentContentStyle style)
        {
            this.Writer.WriteKeyword("plain");
            int index = 0;
            index = this.Writer.FontTable.IndexOf(style.Font.Name);
            if (index >= 0)
            {
                // 字体名称
                this.Writer.WriteKeyword("f" + index);
            }
            if (style.Bold)
            {
                // 粗体
                this.Writer.WriteKeyword("b");
            }
            if (style.Italic)
            {
                // 斜体
                this.Writer.WriteKeyword("i");
            }
            if (style.Underline)
            {
                // 下划线
                this.Writer.WriteKeyword("ul");
            }
            if (style.Strikeout)
            {
                // 删除线
                this.Writer.WriteKeyword("strike");
            }
            this.Writer.WriteKeyword("fs" + Convert.ToInt32(style.FontSize * 2));
            index = this.Writer.ColorTable.IndexOf(style.BackgroundColor);
            if (index >= 0)
            {
                this.Writer.WriteKeyword("chcbpat" + Convert.ToString(index + 1));
            }
            index = this.Writer.ColorTable.IndexOf(style.Color);
            if (index >= 0)
            {
                this.Writer.WriteKeyword("cf" + Convert.ToString(index + 1));
            }
            if (style.Subscript)
            {
                this.Writer.WriteKeyword("sub");
            }
            if (style.Superscript)
            {
                this.Writer.WriteKeyword("super");
            }

            this.Writer.WriteText(strText);

            if (style.Subscript)
            {
                this.Writer.WriteKeyword("sub0");
            }
            if (style.Superscript)
            {
                this.Writer.WriteKeyword("super0");
            }

            if (style.Bold)
            {
                this.Writer.WriteKeyword("b0");
            }
            if (style.Italic)
            {
                this.Writer.WriteKeyword("i0");
            }
            if (style.Underline)
            {
                this.Writer.WriteKeyword("ul0");
            }
            if (style.Strikeout)
            {
                this.Writer.WriteKeyword("strike0");
            }
            // 输出边框
            WriteBorderStyle(style, false);
        }

		/// <summary>
		/// 结束输出字符串
		/// </summary>
		public void WriteEndString()
		{
		}

		/// <summary>
		/// 开始输出书签
		/// </summary>
		/// <param name="strName">书签名称</param>
        public void WriteStartBookmark(string strName)
        {
            this.Writer.WriteStartGroup();
            this.Writer.Writer.WriteKeyword("bkmkstart", true);
            this.Writer.WriteKeyword("f0");
            this.Writer.WriteText(strName);
            this.Writer.WriteEndGroup();
        }

		/// <summary>
		/// 结束输出书签
		/// </summary>
		/// <param name="strName">书签名称</param>
        public void WriteEndBookmark(string strName)
        {
            this.Writer.WriteStartGroup();
            this.Writer.Writer.WriteKeyword("bkmkend", true);
            this.Writer.WriteKeyword("f0");
            this.Writer.WriteText(strName);
            this.Writer.WriteEndGroup();
        }

		/// <summary>
		/// 输出一个换行符
		/// </summary>
		public void WriteLineBreak( )
		{
			this.Writer.WriteKeyword("line");
		}

        public void WriteImage(
            Image img,
            int width,
            int height,
            byte[] imageData,
            DocumentContentStyle style)
        {
            if (imageData == null && img == null )
            {
                return;
            }

            if (imageData == null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                ms.Close();
                imageData = ms.ToArray();
            }
            this.Writer.WriteStartGroup();
            this.Writer.WriteKeyword("pict");
            this.Writer.WriteKeyword("jpegblip");
            this.Writer.WriteKeyword("picscalex" + Convert.ToInt32(width * 100.0 / img.Size.Width));
            this.Writer.WriteKeyword("picscaley" + Convert.ToInt32(height * 100.0 / img.Size.Height));
            this.Writer.WriteKeyword("picwgoal" + Convert.ToString(img.Size.Width * 15));
            this.Writer.WriteKeyword("pichgoal" + Convert.ToString(img.Size.Height * 15));
            this.Writer.Writer.WriteBytes(imageData);
            this.Writer.WriteEndGroup();
        }

        /// <summary>
        /// 输出所有的文档
        /// </summary>
        public void WriteAllDocument()
        {
            DomDocument mainDocument = this.MainDocument;
            this.CollectionDocumentsStyle();
            this.WriteStartDocument(mainDocument);
            if (this.OutputHeaderFooter)
            {
                XPageSettings ps = mainDocument.PageSettings;
                if ( ps.HeaderDistance > 0 && mainDocument.Header.HasContentElement)
                {
                    this.Writer.WriteStartHeader();
                    this.ClipRectangle = new RectangleF(
                        0,
                        0,
                        mainDocument.Width,
                        mainDocument.Header.Height);
                    mainDocument.Header.WriteRTF(this);
                    this.Writer.WriteEndHeader();
                }
                if ( ps.FooterDistance > 0 & mainDocument.Footer.HasContentElement)
                {
                    this.Writer.WriteStartFooter();
                    this.ClipRectangle = new RectangleF(
                        0,
                        0,
                        mainDocument.Width,
                        mainDocument.Footer.Height);
                    mainDocument.Footer.WriteRTF(this);
                    this.Writer.WriteEndFooter();
                }
            }
            foreach (DomDocument doc in this.Documents)
            {
                this.bolFirstParagraph = true;
                doc.Body.WriteRTF( this );
            }
            this.WriteEndDocument();
        }
	}
}