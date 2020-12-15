/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;

using System.Drawing;
using DCSoft.Drawing;
using System.Xml.Serialization;

namespace DCSoft.CSharpWriter.Dom
{

    /// <summary>
    /// 段落结束标记对象,XWriterLib内部使用
    /// </summary>
    [Serializable()]
    [System.Xml.Serialization.XmlType("XParagraphFlag")]
    public class DomParagraphFlagElement : DomEOFElement
    {
        private static System.Drawing.Bitmap myIcon = null;
        /// <summary>
        /// 静态构造函数,加载图标
        /// </summary>
        static DomParagraphFlagElement()
        {
            myIcon = (System.Drawing.Bitmap)WriterResources.paragrapheof.Clone();
            myIcon.MakeTransparent(Color.White);
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomParagraphFlagElement()
        {
        }

        private DomElement _ParagraphFirstContentElement = null;
        /// <summary>
        /// 段落中第一个文档内容元素
        /// </summary>
        internal DomElement ParagraphFirstContentElement
        {
            get
            {
                return _ParagraphFirstContentElement; 
            }
            set
            {
                _ParagraphFirstContentElement = value; 
            }
        }

        /// <summary>
        /// 段落列表样式
        /// </summary>
        internal ParagraphListStyle ListStyle
        {
            get
            {
                DocumentContentStyle style = this.RuntimeStyle;
                if (style.NumberedList)
                {
                    return ParagraphListStyle.NumberedList;
                }
                else if (style.BulletedList)
                {
                    return ParagraphListStyle.BulletedList;
                }
                else
                {
                    return ParagraphListStyle.None;
                }
            }
        }

        [NonSerialized()]
        internal int DocumentParagraphIndex = -1;

        /// <summary>
        /// 段落在段落列表中的序号
        /// </summary>
        private int intListIndex = 0;
        /// <summary>
        /// 段落在段落列表中的序号
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public int ListIndex
        {
            get
            {
                return intListIndex;
            }
            set
            {
                intListIndex = value;
            }
        }

        private bool _AutoCreate = false;
        /// <summary>
        /// 本对象是否是自动产生的
        /// </summary>
        [System.ComponentModel.DefaultValue( false )]
        public bool AutoCreate
        {
            get
            {
                return _AutoCreate; 
            }
            set
            {
                _AutoCreate = value; 
            }
        }

        //internal XTextElement _FirstContentElement = null;
        /// <summary>
        /// 所在段落的第一个内容元素对象
        /// </summary>
        public override DomElement FirstContentElement
        {
            get
            {
                if (this._ParagraphFirstContentElement == null)
                {
                    return this;
                }
                else
                {
                    return this._ParagraphFirstContentElement;
                }
                //if (this.OwnerDocument == null)
                //{
                //    return null;
                //}
                //else
                //{
                //    XTextContentElement ce = this.ContentElement;
                //    XTextElementList content = ce.PrivateContent;
                //    int index = content.IndexOf(this);
                //    if (index >= 0)
                //    {
                //        for (int iCount = index - 1; iCount >= 0; iCount--)
                //        {
                //            if (content[iCount] is XTextParagraphFlagElement)
                //            {
                //                return content[iCount + 1];
                //            }
                //        }
                //    }
                //    return content[0];
                //}
            }
        }

        
        ///// <summary>
        ///// 所在段落的第一个内容元素对象
        ///// </summary>
        //[System.ComponentModel.Browsable( false )]
        //[System.Xml.Serialization.XmlIgnore()]
        //internal XTextElement FirstContentElement1
        //{
        //    get
        //    {
        //        return _FirstContentElement;
        //    }
        //    set
        //    {
        //        _FirstContentElement = value; 
        //    }
        //}

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="Deeply">是否深入复制子元素</param>
        /// <returns>复制品</returns>
        public override DomElement Clone(bool Deeply)
        {
            DomParagraphFlagElement pe = (DomParagraphFlagElement)base.Clone( Deeply );
            //this.CopyAttributesTo(pe);
            return pe;
        }

        internal void RefreshSize2(DomElement PreElement)
        {
            float h = this.OwnerDocument.DefaultStyle.DefaultLineHeight;
            if (PreElement != null && PreElement.Height > 0)
            {
                h = PreElement.Height;
            }
            h = Math.Max(h, this.OwnerDocument.PixelToDocumentUnit(10));
            this.Height = h;
            this.Width = this.OwnerDocument.PixelToDocumentUnit(10);
            this.SizeInvalid = false;
        }

        /// <summary>
        /// 段落边界矩形
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        internal RectangleF ParagraphBounds
        {
            get
            {
                DomDocumentContentElement ce = this.DocumentContentElement;
                DomContentLine line = this.OwnerLine;
                RectangleF rect = line.Bounds;
                for (int iCount = ce.PrivateLines.IndexOf( line ) ; iCount >= 0; iCount--)
                {
                    DomContentLine line2 = ce.PrivateLines[iCount];
                    if (line2.LastElement is DomParagraphFlagElement)
                    {
                        break;
                    }
                    rect = RectangleF.Union(rect, line2.Bounds);
                }//for
                return rect;
            }
        }

        ///// <summary>
        ///// 刷新元素大小
        ///// </summary>
        ///// <param name="g">参数</param>
        //public override void RefreshSize(System.Drawing.Graphics g)
        //{
        //    int h = (int)Math.Ceiling((double)this.OwnerDocument.DefaultStyle.DefaultLineHeight);
        //    this.Height = h;
        //    this.Width = this.OwnerDocument.PixelToDocumentUnit(10);
        //    this.SizeInvalid = false;
        //}

        ///// <summary>
        ///// 绘制元素
        ///// </summary>
        ///// <param name="g">图形绘制对象</param>
        ///// <param name="ClipRectangle">剪切矩形</param>
        //public override void DrawContent(DocumentPaintEventArgs args)
        //{
        //    if (this.OwnerDocument.Options.ShowParagraphFlag
        //        && args.RenderStyle == DocumentRenderStyle.Paint)
        //    {
        //        System.Drawing.RectangleF rect = this.AbsBounds;
        //        if (args.RenderStyle == DocumentRenderStyle.Paint)
        //        {
        //            System.Drawing.Size size = myIcon.Size;
        //            size = this.OwnerDocument.PixelToDocumentUnit(size);
        //            System.Drawing.Drawing2D.InterpolationMode back = args.Graphics.InterpolationMode;
        //            args.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        //            args.Graphics.DrawImage(myIcon, rect.Left, rect.Bottom - size.Height);
        //            args.Graphics.InterpolationMode = back;
        //        }
        //    }
        //}

        /// <summary>
        /// 返回对象包含的字符串数据
        /// </summary>
        /// <returns>字符串数据</returns>
        public override string ToString()
        {
            return "PEOF";
        }

        public override string ToPlaintString()
        {
            return Environment.NewLine;
        }

        /// <summary>
        /// 表示对象内容的纯文本数据
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get
            {
                return Environment.NewLine;
            }
            set
            {
            }
        }
    }//public class XTextParagraphEOF : XTextEOF
}
