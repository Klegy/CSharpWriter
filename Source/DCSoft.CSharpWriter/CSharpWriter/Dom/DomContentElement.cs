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
using System.ComponentModel;
using System.Xml.Serialization;
using DCSoft.Printing;
using DCSoft.Drawing;
using System.Drawing;
using DCSoft.CSharpWriter.Html;
using DCSoft.Common;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 文档内容元素类型
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable()]
    public class DomContentElement : DomContainerElement
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomContentElement()
        {

        }

        private bool _IsEmpty = false;
        /// <summary>
        /// 内容为空
        /// </summary>
        [DefaultValue(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public bool IsEmpty
        {
            get
            {
                return _IsEmpty;
            }
            set
            {
                _IsEmpty = value;
            }
        }

        public override bool AppendChildElement(DomElement element)
        {
            _IsEmpty = false;
            return base.AppendChildElement(element);
        }

        /// <summary>
        /// 文本行列表
        /// </summary>
        [NonSerialized]
        private DomContentLineList _PrivateLines = new DomContentLineList();
        /// <summary>
        /// 元素私有的文本行列表
        /// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public DomContentLineList PrivateLines
        {
            get
            {
                if (_PrivateLines == null)
                {
                    _PrivateLines = new DomContentLineList();
                }
                return _PrivateLines;
            }
        }


        /// <summary>
        /// 内容高度
        /// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        public float ContentHeight
        {
            get
            {
                DocumentContentStyle rs = this.RuntimeStyle;
                float ch = 0;
                if (this.PrivateLines.Count > 0)
                {
                    ch = this.PrivateLines.LastLine.Bottom - this.PrivateLines[0].Top + this.PrivateLines[0].BeforeSpacing ;
                }
                ch = ch + rs.PaddingTop + rs.PaddingBottom;
                return ch;
            }
        }

        /// <summary>
        /// 更新文档行的行号
        /// </summary>
        /// <param name="startIndex">起始行号</param>
        /// <returns>本文档中累计的行数</returns>
        internal protected int UpdateLineIndex(int startIndex)
        {
            if (this.PrivateLines.Count == 0)
            {
                return 0;
            }
            foreach (DomContentLine line in this.PrivateLines)
            {
                line.GlobalIndex = startIndex;
                 
                {
                    line.GlobalIndex = startIndex;
                    startIndex++;
                }
            }//foreach
            return startIndex;
        }


        ///// <summary>
        ///// 内容区域宽度
        ///// </summary>
        //public virtual float ContentWidth
        //{
        //    get
        //    {
        //        return this.OwnerDocument.PageSettings.ViewClientWidth;
        //    }
        //}

        /// <summary>
        /// 设置多个文本行为无效状态
        /// </summary>
        /// <param name="startLine">开始行</param>
        /// <param name="endLine">结束行</param>
        internal void SetLinesInvalidateState(DomContentLine startLine, DomContentLine endLine)
        {
            int startIndex = this.PrivateLines.IndexOf(startLine);
            if (startIndex < 0)
            {
                startIndex = 0;
            }
            int endIndex = this.PrivateLines.IndexOf(endLine);
            if (endIndex < 0)
            {
                endIndex = this.PrivateLines.Count - 1;
            }
            for (int iCount = startIndex; iCount <= endIndex; iCount++)
            {
                this.PrivateLines[iCount].InvalidateState = true;
            }
        }

        [NonSerialized]
        private DomElementList _PrivateContent = null;
        /// <summary>
        /// 文档内容管理对象
        /// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public DomElementList PrivateContent
        {
            get
            {
                if (_PrivateContent == null)
                {
                    _PrivateContent = new DomElementList();
                    this.AppendContent(_PrivateContent, true);
                    // 更新段落首元素和序号
                    int startIndex = 0;
                    for (int iCount = 0; iCount < _PrivateContent.Count; iCount++)
                    {
                        if (_PrivateContent[iCount] is DomParagraphFlagElement)
                        {
                            DomParagraphFlagElement flag = (DomParagraphFlagElement)_PrivateContent[iCount];
                            flag.ParagraphFirstContentElement = _PrivateContent[startIndex];
                            startIndex = iCount + 1;
                        }//if
                    }//for
                    this.RefreshParagraphState( null );
                }
                return _PrivateContent;
            }
        }



        /// <summary>
        /// 加载文档后的处理
        /// </summary>
        public override void AfterLoad(FileFormat format)
        {
            base.AfterLoad(format);
            this.FixElements();
            this.UpdateContentElements(false);
        }


        /// <summary>
        /// 清空文档内容
        /// </summary>
        public virtual void Clear()
        {
            this.Elements.Clear();
            this.Elements.Add(this.OwnerDocument.CreateParagraphEOF());
            //myElements.Add( this.myEOFElement );
            this.UpdateContentElements(true);
            this.ExecuteLayout();
        }


        public override bool RemoveChild(DomElement element)
        {
            if (element == this.Elements.LastElement)
            {
                return false;
            }
            return base.RemoveChild(element);
        }

        public virtual void FixElements()
        {
            if (this.Elements.Count == 0
                || (this.Elements.LastElement is DomParagraphFlagElement) == false)
            {
                DomParagraphFlagElement flag = new DomParagraphFlagElement();
                flag.AutoCreate = true;
                flag.StyleIndex = this.StyleIndex;
                this.AppendChildElement( flag );
            }
        }

        /// <summary>
        /// 更新所有段落的状态
        /// </summary>
        /// <param name="startElement">更新区域的开始元素</param>
        public void RefreshParagraphState(DomElement startElement)
        {
            int index = 0;
            if (startElement != null)
            {
                index = this.PrivateContent.IndexOf(startElement);
            }
            if (index < 0)
            {
                index = 0;
            }
            ParagraphListStyle lastStyle = ParagraphListStyle.None;
            int lastIndex = 1;
            int pIndex = 0;
            for (int iCount = index; iCount < this.PrivateContent.Count; iCount++)
            {
                DomParagraphFlagElement eof = this.PrivateContent[iCount]
                    as DomParagraphFlagElement;
                if (eof != null)
                {
                    eof.DocumentParagraphIndex = pIndex++;
                    if (eof.ListStyle == lastStyle)
                    {
                        eof.ListIndex = lastIndex;
                    }
                    else
                    {
                        lastStyle = eof.ListStyle;
                        eof.ListIndex = 1;
                    }
                    lastIndex = eof.ListIndex + 1;
                }
            }//for
        }

        /// <summary>
        /// 更新文档内容元素列表
        /// </summary>
        public virtual void UpdateContentElements(bool updateParentContentElement)
        {
            if (this.OwnerDocument != null)
            {
                this.OwnerDocument.UpdateContentVersion();
            }
            _PrivateContent = null;
            //this.AppendContent(_PrivateContent , true );
            this.FixElements();
            //int iCount = 0;
            //foreach (XTextElement e in this.PrivateContent)
            //{
            //    e.intViewIndex = iCount;
            //    iCount++;
            //}
            this.RefreshParagraphState(null);
            if (updateParentContentElement)
            {
                // 更新父元素的文档内容元素列表
                DomElement element = this.Parent;
                while (element != null)
                {
                    if (element is DomContentElement)
                    {
                        ((DomContentElement)element).UpdateContentElements(updateParentContentElement);
                    }
                    element = element.Parent;
                }// while
            }
        }

        //public override bool AcceptChildElement(Type elementType)
        //{
        //    return typeof(XTextParagraphElement) == elementType
        //        || elementType.IsSubclassOf(typeof(XTextParagraphElement));
        //}

        /// <summary>
        /// 创建段落对象
        /// </summary>
        /// <param name="elements">元素对象列表</param>
        /// <returns>创建的段落对象列表</returns>
        public DomElementList CreateParagraphs(DomElementList elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }
            DomElementList result = new DomElementList();
            DomParagraphElement p = new DomParagraphElement();
            p.OwnerDocument = this.OwnerDocument;
            p.Parent = this;
            result.Add(p);
            foreach (DomElement element in elements)
            {
                if (element is DomParagraphFlagElement)
                {
                    DomParagraphFlagElement eof = (DomParagraphFlagElement)element;
                    p.StyleIndex = eof.StyleIndex;
                    //p.myEOFElement = eof;
                    p.Elements.Add(element);

                    p = new DomParagraphElement();
                    p.OwnerDocument = this.OwnerDocument;
                    p.Parent = this;
                    result.Add(p);
                }
                else
                {
                    p.Elements.Add(element);
                }
            }//foreach
            foreach (DomParagraphElement p2 in result)
            {
                DomElementList list = WriterUtils.MergeElements(p2.Elements, false);
                //if (list.LastElement is XTextParagraphEOF)
                //{
                //    list.RemoveAt(list.Count - 1);
                //}
                p2.Elements.Clear();
                p2.Elements.AddRange(list);
            }
            return result;
        }

        /// <summary>
        /// 内容垂直对齐方式
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public virtual VerticalAlignStyle ContentVertialAlign
        {
            get
            {
                return VerticalAlignStyle.Top;
            }
        }

        public bool RefreshPrivateContent(int StartIndex)
        {
            return RefreshPrivateContent(StartIndex, -1, false);
        }

        /// <summary>
        /// 调用 RefreshPrivateContent 函数中是否需要重新分页的标志
        /// </summary>
        [NonSerialized]
        internal bool _NeedRefreshPage = false;

        /// <summary>
        /// 刷新私有内容
        /// </summary>
        /// <param name="StartIndex">需要刷新区域的起始序号</param>
        /// <param name="EndIndex">需要刷新区域的结束序号</param>
        /// <param name="fastMode">快速模式</param>
        /// <returns></returns>
        public virtual bool RefreshPrivateContent(int StartIndex, int EndIndex, bool fastMode)
        {
            _NeedRefreshPage = false;
            StartIndex = this.PrivateContent.FixElementIndex(StartIndex);

            bool fix = true;
            DomElement element = this.PrivateContent[StartIndex];
            if (element.OwnerLine != null)
            {
                if (element.OwnerLine.IndexOf(element) == 0)
                {
                    int index = this.PrivateLines.IndexOf(element.OwnerLine);
                    if (index > 0)
                    {
                        DomContentLine line2 = this.PrivateLines[index - 1];
                        if (line2.HasLineEndElement)
                        {
                            DomElement le = line2.LastElement;
                            if (this.PrivateContent.Contains(le))
                            {
                                fix = false;
                            }
                        }//if
                    }//if
                }//if
            }//if
            if (fix)
            {
                if (StartIndex > 0)
                {
                    StartIndex--;
                }
            }
            StartIndex = this.PrivateContent.FixElementIndex(StartIndex);
            //if( myContent.IndexOf( myElements[ S
            if (_ModifyParagraph)
            {
                _ModifyParagraph = false;
                this.PrivateLines.Clear();
                StartIndex = 0;
            }
            DomElement EndElement = this.PrivateContent.SafeGet(EndIndex);
            if (this.ParticalRefreshLines(
                this.PrivateContent[StartIndex],
                EndElement,
                this.ContentVertialAlign))
            {
                this.DocumentContentElement.UpdateLineIndex(0);
                bool refreshPage = false;
                 
                {
                    refreshPage = true;
                }
                _NeedRefreshPage = refreshPage;
                if (refreshPage)
                {
                    this.OwnerDocument.PageRefreshed = false;

                    //if(( this.Parent is XTextDocument ) == false )
                    //{
                    //    // 搜索上层容器对象，并对其内容进行排版
                    //    XTextElement parent = this.Parent;
                    //    while (parent != null )
                    //    {
                    //        if( parent is XTextDocumentContentElement ) 
                    //        {
                    //            // 文档级容器对象的内部不进行排版
                    //            break;
                    //        }
                    //        if (parent is XTextContentElement)
                    //        {
                    //            // 遇到上层容器对象则对其进行内容重新排版
                    //            XTextContentElement parentContent = (XTextContentElement)parent;
                    //            XTextElement parent2 = this;
                    //            int startIndex2 = -1;
                    //            while (parent2 != null)
                    //            {
                    //                startIndex2 = parentContent.PrivateContent.IndexOf(parent2);
                    //                if (startIndex2 >= 0)
                    //                {
                    //                    break;
                    //                }
                    //                parent2 = parent2.Parent;
                    //            }//while

                    //        }
                    //        parent = parent.Parent;
                    //    }
                    //}
                    //if (this is XTextTableCellElement )
                    //{
                    //    // 如果当前对象是表格单元格，则还需要进行表格套嵌的判断
                    //    XTextElement parent = this.Parent;
                    //    while (parent != null)
                    //    {

                    //    }
                    //}
                    if (fastMode == false)
                    {
                        if (this.OwnerDocument != null)
                        {
                            this.OwnerDocument.RefreshPages();
                        }

                        if (this.OwnerDocument.EditorControl != null
                            && this.OwnerDocument.EditorControl.IsUpdating == false )
                        {
                            this.OwnerDocument.EditorControl.UpdatePages();
                            this.OwnerDocument.EditorControl.UpdateTextCaret();
                            this.OwnerDocument.EditorControl.Invalidate();
                        }
                    }
                }
            }
            else
            {
                if (fastMode == false)
                {
                    if (this.OwnerDocument != null
                        && this.OwnerDocument.EditorControl != null
                        && this.OwnerDocument.EditorControl.IsUpdating == false )
                    {
                        this.OwnerDocument.EditorControl.UpdateTextCaret();
                        this.OwnerDocument.EditorControl.Update();
                    }
                }
            }
            
            return true;
        }


        /// <summary>
        /// 针对若干个元素状态发生改变而刷新文档
        /// </summary>
        /// <param name="list">元素列表</param>
        /// <param name="PreserveSelection">是否保存选择状态</param>
        /// <remarks>若某些操作导致文档中部分元素视图或大小发生改变但并未新增或删除文档元素,则可以
        /// 使用本函数来高效率的更新文档而不必刷新整个文档的视图.</remarks>
        public void RefreshContentByElements(DomElementList list, bool PreserveSelection , bool fastMode )
        {
            if (list == null || list.Count == 0)
            {
                return;
            }
            int StartIndex = int.MaxValue;
            int EndIndex = -1;
            DomElement LastElement = null;
            using (System.Drawing.Graphics g = this.OwnerDocument.CreateGraphics())
            {
                foreach (DomElement element in list)
                {
                    int elementIndex = this.PrivateContent.IndexOf(element);
                    if (elementIndex < 0)
                    {
                        throw new Exception("Element no in content");
                    }
                    if (StartIndex > elementIndex )
                    {
                        StartIndex = elementIndex;
                    }
                    if (element.SizeInvalid || element.ViewInvalid)
                    {
                        element.InvalidateView();
                    }
                    if (element.SizeInvalid)
                    {
                        this.OwnerDocument.Render.RefreshSize(element, g);
                        element.InvalidateView();
                    }
                    element.ViewInvalid = false;
                    if ( elementIndex > 0 && elementIndex > EndIndex)
                    {
                        EndIndex = elementIndex;
                    }
                    LastElement = element;
                }//foreach
            }//using
            this.RefreshPrivateContent(StartIndex, EndIndex, fastMode);
            if (PreserveSelection == false)
            {
                this.DocumentContentElement.Content.AutoClearSelection = true;
                this.DocumentContentElement.Content.MoveSelectStart(StartIndex);
            }
        }

        /// <summary>
        /// 对整个内容执行重新排版操作
        /// </summary>
        public override void ExecuteLayout()
        {
            this.Width = this.OwnerDocument.PageSettings.ViewClientWidth;
            this.UpdateContentElements(true);
            this.PrivateLines.Clear();
            foreach (DomElement element in this.PrivateContent)
            {
                element.OwnerLine = null;
            }
            this.ParticalRefreshLines(null, null, this.ContentVertialAlign);
        }

        /// <summary>
        /// 编辑操作中是否新增或删除了段落，若设置该属性则刷新文档时将强迫刷新整个文档
        /// </summary>
        [NonSerialized]
        internal bool _ModifyParagraph = false;

        /// <summary>
        /// 根据文档内容高度来设置本文档元素的高度
        /// </summary>
        public virtual void UpdateHeightByContentHeight()
        {
            this.Height = this.ContentHeight;
        }

        /// <summary>
        /// 更新文档行位置
        /// </summary>
        /// <param name="align">文档行垂直对齐方式</param>
        /// <returns>是否有文档行的位置发生改变</returns>
        internal virtual bool UpdateLinePosition(
            VerticalAlignStyle align,
            bool refreshLineState,
            bool deeply)
        {
            if (this.PrivateLines.Count == 0)
            {
                // 没有文档行，不进行处理
                return false;
            }
            // 记下旧的文档行位置和高度
            float[] oldLinesInfo = new float[this.PrivateLines.Count * 2];
            for (int iCount = 0; iCount < this.PrivateLines.Count; iCount++)
            {
                DomContentLine line = ( DomContentLine ) this.PrivateLines[ iCount ];
                oldLinesInfo[iCount * 2 ] = line.Top;
                oldLinesInfo[iCount * 2 + 1] = line.BeforeSpacing + line.Height + line.AdditionHeight;
            }

            float totalHeight = 0;
            ListDictionary<DomParagraphFlagElement, List<DomContentLine>> paragraphs = this.GetParagraphLines();
            foreach (DomParagraphFlagElement flagElement in paragraphs.Keys)
            {
                List<DomContentLine> lines = paragraphs[flagElement];
                DocumentContentStyle ps = flagElement.RuntimeStyle;
                foreach (DomContentLine line in lines)
                {
                    //if (line.IsTableLine)
                    //{
                    //    Console.WriteLine("");
                    //}
                    if (refreshLineState)
                    {
                        
                         
                        {
                            line.RefreshState();
                        }
                    }
                    // 计算行间距
                    float h = ps.GetLineSpacing( 
                        line.ContentHeight , 
                        line.MaxFontHeight , 
                        this.OwnerDocument.DocumentGraphicsUnit );
                    
                    // 根据行间距设置额外行高度
                    line.AdditionHeight = h - line.Height;
                    if (line.AdditionHeight < 0  )
                    {
                        // 行间距过小，调整行内元素位置
                        line.RefreshState();
                    }
                    if (line == lines[0])
                    {
                        // 段落中的首行则设置段落前间距
                        line.BeforeSpacing = ps.SpacingBeforeParagraph;
                    }
                    else
                    {
                        line.BeforeSpacing = 0 ;
                    }
                    if (line == lines[lines.Count - 1])
                    {
                        // 段落中的最后一行则追加段落后间距
                        line.AdditionHeight = line.AdditionHeight + ps.SpacingAfterParagraph;
                    }
                    totalHeight = totalHeight + line.BeforeSpacing + line.Height + line.AdditionHeight;
                }//foreach
            }//foreach
             
            // 计算行开始位置
            float topCount = 0;
            DocumentContentStyle rs = this.RuntimeStyle;
            switch (align)
            {
                case VerticalAlignStyle.Top:
                    // 顶端位置
                    topCount = rs.PaddingTop;
                    break;
                case VerticalAlignStyle.Middle:
                    // 垂直居中对齐
                    topCount = (this.Height - rs.PaddingTop - rs.PaddingBottom - totalHeight) / 2.0f;
                    topCount = rs.PaddingTop + Math.Max(0, topCount);
                    break;
                case VerticalAlignStyle.Bottom:
                    // 低端对齐
                    topCount = this.Height - rs.PaddingTop - rs.PaddingBottom - totalHeight;
                    topCount = rs.PaddingTop + Math.Max(0, topCount);
                    break;
                case VerticalAlignStyle.Justify:
                    // 垂直两边对齐
                    topCount = rs.PaddingTop;
                    break;
            }//switch
            //topCount = topCount + 4;
            if (align == VerticalAlignStyle.Justify)
            {
                // 两端对齐
                float spacing = 0;
                if (this.PrivateLines.Count > 1)
                {
                    // 计算分配的额外的行间距
                    spacing = Math.Max(0, (this.Height - totalHeight) / (this.PrivateLines.Count - 1));
                }
                foreach (DomContentLine line in this.PrivateLines)
                {
                    line.Top = topCount + line.BeforeSpacing ;
                    topCount = topCount + line.Height + spacing + line.AdditionHeight + line.BeforeSpacing ;
                }//foreach
            }
            else
            {
                // 累计计算文档行的位置
                foreach (DomContentLine line in this.PrivateLines)
                {
                    //if (line.IsTableLine)
                    //{
                    //    Console.WriteLine("");
                    //}
                    line.Top = topCount + line.BeforeSpacing ;
                    topCount = topCount + line.Height + line.BeforeSpacing + line.AdditionHeight ;
                }//foreach
            }
            bool result = false;
             
            if (result == false)
            {
                // 比较新旧文档行的位置是否发生改变
                for (int iCount = 0; iCount < this.PrivateLines.Count; iCount++)
                {
                    DomContentLine line = this.PrivateLines[iCount];
                    if ( line.Top !=  oldLinesInfo[iCount * 2 ] 
                        || line.BeforeSpacing + line.Height + line.AdditionHeight != oldLinesInfo[ iCount * 2 + 1 ])
                    {
                        //  文档行的位置和旧的文档行的位置有不符合
                        result = true;
                        break;
                    }
                }//for
            }//if
            if (result)
            {
                // 文档行的位置发生则更新文档元素的高度
                this.UpdateHeightByContentHeight();
            }
            return result;
        }

        /// <summary>
        /// 进行部分分行操作
        /// </summary>
        /// <param name="startElement">开始分行的元素对象</param>
        /// <param name="endElement">结束分行操作的元素对象</param>
        /// <param name="vertialAlign">行垂直对齐方式</param>
        /// <returns>操作是否导致文档需要重新分页</returns>
        internal virtual bool ParticalRefreshLines(
            DomElement startElement,
            DomElement endElement,
            VerticalAlignStyle vertialAlign)
        {
            // 是否是正文块,若为正文块，可能导致重新分页。
            bool isBodyContent = true;
            if (this.Width <= 0)
            {
                this.Width = this.OwnerDocument.PageSettings.ViewClientWidth;
            }
            if (this.PrivateContent.Count == 0)
            {
                this.PrivateLines.Clear();
                //this.Height = 0;
                return isBodyContent;
            }

            // 获得文档内容控制器
            DocumentControler controler = this.OwnerDocument.DocumentControler;
            // 行号编号备份
            int[] lineIndexBack = new int[this.PrivateLines.Count * 2];
            // 对旧的行状态数据进行备份，以便重新分行后判断是否需要重新分页
            float[] linesInfoBack = new float[this.PrivateLines.Count * 2];
            for (int iCount = 0; iCount < this.PrivateLines.Count; iCount++)
            {
                DomContentLine line = this.PrivateLines[iCount];
                //if (line.InvalidateState)
                //{
                //    linesInfoBack[iCount * 2] = -1;
                //    linesInfoBack[iCount * 2 + 1] = -1;
                //}
                //else
                {
                    linesInfoBack[iCount * 2] = line.Top;
                    linesInfoBack[iCount * 2 + 1] = line.Height;

                    lineIndexBack[iCount * 2] = line.GlobalIndex;
                    lineIndexBack[iCount * 2 + 1] = line.IndexInPage;
                }
            }//for

            int endIndex = -1;
            if (endElement != null)
            {
                endIndex = this.PrivateContent.IndexOf(endElement);
            }
            if (startElement == null)
            {
                startElement = this.PrivateContent[0];
            }
            int startIndex = this.PrivateContent.IndexOf(startElement);
            if (startIndex < 0)
            {
                throw new System.Exception("未找到起始元素");
            }
            //用户删除的文档开头部分多行标记
            //bool DeleteHeadLines = false;
            if (startElement.OwnerLine != null)
            {
                DomContentLine line2 = startElement.OwnerLine;
                if (startIndex == 0)
                {
                    // 检查旧的第一行是否被完整的删除了
                    if (this.PrivateLines.Count > 0)
                    {
                        DomContentLine line = this.PrivateLines[0];
                        bool fullDelete = true;
                        foreach (DomElement element in line)
                        {
                            if (this.PrivateContent.Contains(element))
                            {
                                fullDelete = false;
                                break;
                            }
                        }
                        if (fullDelete)
                        {
                            startElement.OwnerLine = null;
                            this.PrivateLines.Clear();
                        }
                    }
                }
                else
                {
                    foreach (DomElement element in line2)
                    {
                        if (this.PrivateContent.Contains(element))
                        {
                            startIndex = this.PrivateContent.IndexOf(element);
                            startElement = element;
                            break;
                        }
                    }//foreach
                }//else
            }//if

            DocumentContentStyle rs = this.RuntimeStyle;
            // 保存反复无常的元素的列表
            List<DomElement> freakElements = new List<DomElement>();
            //System.Collections.ArrayList InvalidLines = new System.Collections.ArrayList();
            DomContentLine StopLine = null;

            DomContentLineList newLines = new DomContentLineList();

            DomContentLine newLine = new DomContentLine();

            newLine.Spacing = startElement.RuntimeStyle.Spacing;
            newLine.Width = this.Width - rs.PaddingLeft - rs.PaddingRight;
            newLine._OwnerDocument = this.OwnerDocument;
            newLine._OwnerContentElement = this;
            newLines.Add(newLine);

            // 将参与分行计算的元素从列表形式转化为堆栈形式
            System.Collections.Stack myStack
                = new System.Collections.Stack(_PrivateContent.Count - startIndex);
            for (int iCount = _PrivateContent.Count - 1;
                iCount >= startIndex;
                iCount--)
            {
                DomElement element = _PrivateContent[iCount];
                //if (element.SizeInvalid == true)
                //{
                //    // 出现没有计算大小的文档元素，则操作失败
                //    return false;
                //}
                myStack.Push( element );
            }//for
            // 产生第一个新行对象标记
            bool first = true;
            while (myStack.Count > 0)
            {
                bool bolNewLine = false;
                DomElement element = (DomElement)myStack.Pop();
                //if( element is XTextTableElement )
                //{
                //    // 表格内容排版
                //    XTextTableElement table = ( XTextTableElement ) element ;
                //    if( table.LayoutInvalidate )
                //    {
                //        table.ExecuteLayout();
                //    }
                //}
                //if (element.ToString() == "C")
                //{
                //    Console.Write("aa");
                //}
                if (newLine.Count == 0)
                {
                    newLine.AddElement(element);
                    if (controler.OwnerHoleLine(element)
                        || controler.IsNewLine(element))
                    {
                        bolNewLine = true;
                    }
                }
                else
                {
                    if (controler.OwnerHoleLine(element))
                    {
                        myStack.Push(element);
                        bolNewLine = true;
                    }
                    else
                    {
                        if (newLine.AddElement(element) == false)
                        {
                            // 向文本行添加内容失败，说明文本行已经无法添加内容了，准备换行。
                            myStack.Push(element);
                            bolNewLine = true;
                            if (newLine.Count > 1 && controler.CanBeLineEnd(newLine.LastElement) == false)
                            {
                                // 文档行中最后一个元素不能放置在行尾，则提前换行
                                DomElement lastElement = newLine.PopupLastElement();
                                if (lastElement != null)
                                {
                                    myStack.Push(lastElement);
                                    freakElements.Add(lastElement);
                                }
                            }
                            else
                            {
                                if (controler.CanBeLineHead(element) == false
                                    || controler.CanBeLineEnd(newLine.LastElement) == false)
                                {
                                    DomElement LastElement = newLine.PopupLastElement();
                                    if (LastElement != null)
                                    {
                                        myStack.Push(LastElement);
                                        // 此处由于修整行首字符和行尾字符导致已经加入本行的元素退出本行
                                        // 由于XTextLine.Add函数会设置元素的Left值，因此导致退出本行的元素
                                        // 退出本行但没有恢复Left值，因此换行操作后需要对这些元素所在的文本
                                        // 行重新进行行内排版操作,此处保存这些立场反复无常的元素，便于以后
                                        // 对所影响到的文本行进行内部排版操作
                                        freakElements.Add(LastElement);
                                    }
                                }
                                DomCharElement c = myStack.Peek() as DomCharElement;
                                if (c != null && controler.IsEnglishLetterOrDigit(c.CharValue))
                                {
                                    // 在当前行中向前搜索，判断能否执行提前换行
                                    float WidthCount = 0;
                                    float ContentWidth = newLine.ContentWidth;
                                    int chrElementCount = 0;
                                    int freakIndex = -1;
                                    for (int iCount = newLine.Count - 1; iCount >= 0; iCount--)
                                    {
                                        if (newLine[iCount] is DomCharElement)
                                        {
                                            if (controler.IsEnglishLetterOrDigit(((DomCharElement)newLine[iCount]).CharValue))
                                            {
                                                WidthCount = WidthCount + newLine[iCount].Width;
                                                chrElementCount++;
                                                freakIndex = iCount;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (freakIndex > newLine.Count / 3
                                        && WidthCount < ContentWidth / 3)
                                    {
                                        // 提前换行
                                        for (int iCount = newLine.Count - 1; iCount >= freakIndex; iCount--)
                                        {
                                            DomElement element2 = newLine[iCount];
                                            newLine.RemoveAt(iCount);
                                            myStack.Push(element2);
                                            // 此处由于修整连续的英文单词和数字导致已经加入本行的元素退出本行
                                            // 由于XTextLine.Add函数会设置元素的Left值，因此导致退出本行的元素
                                            // 退出本行但没有恢复Left值，因此换行操作后需要对这些元素所在的文本
                                            // 行重新进行行内排版操作,此处保存这些立场反复无常的元素，便于以后
                                            // 对所影响到的文本行进行内部排版操作
                                            freakElements.Add(element2);
                                        }
                                    }
                                    //bool doFreak = true ;
                                    //if (chrElementCount == newLine.Count
                                    //    || WidthCount > ContentWidth / 3)
                                    //{
                                    //    doFreak = false;
                                    //}
                                    //if ( doFreak )
                                    //{
                                    //    WidthCount = 0;
                                    //    ContentWidth = newLine.ContentWidth;
                                    //    // 为英文字母或者数字
                                    //    while (newLine.Count > 1)
                                    //    {
                                    //        XTextCharElement c2 = newLine.LastElement as XTextCharElement;
                                    //        if (c2 == null)
                                    //        {
                                    //            break;
                                    //        }
                                    //        if (controler.IsEnglishLetterOrDigit(c2.CharValue))
                                    //        {
                                    //            WidthCount += c2.Width;
                                    //            if (WidthCount > ContentWidth / 3)
                                    //            {
                                    //                break;
                                    //            }
                                    //            c2 = newLine.PopupLastElement() as XTextCharElement;
                                    //            if (c2 == null)
                                    //            {
                                    //                break;
                                    //            }
                                    //            myStack.Push(c2);
                                    //            // 此处由于修整连续的英文单词和数字导致已经加入本行的元素退出本行
                                    //            // 由于XTextLine.Add函数会设置元素的Left值，因此导致退出本行的元素
                                    //            // 退出本行但没有恢复Left值，因此换行操作后需要对这些元素所在的文本
                                    //            // 行重新进行行内排版操作,此处保存这些立场反复无常的元素，便于以后
                                    //            // 对所影响到的文本行进行内部排版操作
                                    //            freakElements.Add(c2);
                                    //        }
                                    //        else
                                    //        {
                                    //            break;
                                    //        }
                                    //    }//while( NewLine.Count > 0 )
                                    //}//if
                                }
                            }
                        }
                        else
                        {
                            
                            {
                                if (controler.IsNewLine(element))
                                {
                                    bolNewLine = true;
                                }
                            }
                        }
                    }
                }

                DomCharElement CharElement = element as DomCharElement;
                if (CharElement != null && CharElement.CharValue == '\t')
                {
                    CharElement.SetWidthForTab();
                }
                if (bolNewLine)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        if (_PrivateLines.Count > 0)
                        {
                            if (endIndex <= 0 || _PrivateContent.IndexOf(element) > endIndex)
                            {
                                DomContentLine LastLine = newLines.LastLine;
                                DomElement FirstElement = LastLine.FirstElement;
                                for (int iCount = this.PrivateLines.Count - 1; iCount >= 0; iCount--)
                                {
                                    DomContentLine oldLine = this.PrivateLines[iCount];
                                    if (oldLine.InvalidateState)
                                    {
                                        // 只要之前出现任何状态无效的行则不会命中截止行。
                                        break;
                                    }
                                    if (CheckStopLine(LastLine, _PrivateLines[iCount]))
                                    {
                                        // 若已经存在的文本行和新建的最后一个文本行的内部设置一样,包括元素清单
                                        // 和行设置,而且该行所有元素的大小没有发生改变,则设置该旧行为停止行
                                        // 停止行以后的旧行就没有必要进行刷新操作,提前退出刷新行操作,避免不必要
                                        // 的工作量
                                        // 某些情况下某些位置处元素发生改变影响行分布,主调程序就使用EndIndex指明
                                        // 停止行必须在这些位置后出现,因此本段程序稍前进行了 EndIndex 判断操作
                                        StopLine = _PrivateLines[iCount];
                                        goto EndAddLine;
                                    }//if
                                }//for
                            }//if
                        }//if
                    }//else
                    newLine = new DomContentLine();
                    //NewLine.Spacing = this.CharSpacing ;
                    newLine.Width = this.Width - rs.PaddingLeft - rs.PaddingRight;
                    newLine._OwnerDocument = this.OwnerDocument;
                    newLine._OwnerContentElement = this;
                    newLines.Add(newLine);
                }//if
            }//while( myStack.Count > 0 )
        EndAddLine:
            if (newLines.Count > 0)
            {
                DomContentLine LastLine = newLines.LastLine;
                if (LastLine.Count == 0)
                {
                    newLines.Remove(LastLine);
                }
            }

            int startLineIndex = 0;
            // 计算新的文档行的位置
            float topCount = rs.PaddingTop;// this.Top + this.OwnerDocument.Top;
            if (startElement.OwnerLine != null)
            {
                topCount = startElement.OwnerLine.Top;
                startLineIndex = _PrivateLines.IndexOf(startElement.OwnerLine);
            }
            if (startLineIndex < 0)
            {
                startLineIndex = 0;
                //throw new System.Exception("AllElements 和 Content 内容不同步");
            }
            // 计算行高
            foreach (DomContentLine myLine in newLines)
            {
                myLine.UpdateContentHeight();
                DocumentContentStyle ps = this.OwnerDocument.GetParagraphStyle(myLine.LastElement);
                //// 计算行间距
                //if (ps.RTFLineSpacing != 0)
                //{
                //    float ch = myLine.ContentHeight;
                //    if (ps.RTFLineSpacing > 0)
                //    {
                //        if (ps.RTFLineSpacing > ch)
                //        {
                //            myLine.Height = ps.RTFLineSpacing ;
                //        }
                //    }
                //    else
                //    {
                //        myLine.Height = ps.RTFLineSpacing;
                //    }
                //}
                //else
                {
                    myLine.Height = myLine.ContentHeight;
                    //float lh = ps.GetLineSpacing(GraphicsUnitConvert.ToTwips(myLine.ContentHeight, GraphicsUnit.Document));

                    //myLine.Height = ( float ) GraphicsUnitConvert.FromTwips(lh, GraphicsUnit.Document);
                }
                //if (myLine.LastElement is XTextParagraphFlagElement)
                //{
                //    // 本文本行是段落中的最后一行,行间距追加段落后间距
                //    myLine.LineSpacing = myLine.LineSpacing + ps.SpacingAfterParagraph;
                //}
                //else if (this.PrivateContent.GetPreElement(myLine[0]) is XTextParagraphFlagElement)
                //{
                //    // 本文本行是段落中第一行，设置行前间距
                   
                //}
                myLine.Spacing = ps.Spacing;
                myLine.Align = ps.Align;
                //myLine.VerticalAlign = ps.VerticalAlign;
                myLine.Left = rs.PaddingLeft;// this.Left + this.OwnerDocument.Left;
                //myLine.Top = topCount;
                
                {
                    myLine.RefreshState();
                }
                //topCount = (float)Math.Ceiling(topCount + myLine.Height);//+ myLine.LineSpacing ;
            }//foreach

            // 补充查找 StopLine
            if (StopLine == null && newLines.Count > 1)
            {
                DomContentLine LastLine = newLines.LastLine;
                DomElement FirstElement = LastLine.FirstElement;
                for (int iCount = _PrivateLines.Count - 1; iCount >= 0; iCount--)
                {
                    if (CheckStopLine(LastLine, _PrivateLines[iCount]))
                    {
                        StopLine = _PrivateLines[iCount];
                        break;
                    }
                }
            }

            if (StopLine == null
                && newLines.Count == 1
                && _PrivateLines.Count > 0)
            {
                if (_PrivateLines.LastLine.FirstElement == newLines[0].FirstElement)
                {
                    StopLine = _PrivateLines.LastLine;
                }
            }

            // 用户界面无效的文本行对象
            List<DomContentLine> invalidateLines = new List<DomContentLine>();

            // 是否需要重新分页标记
            bool pageFlag = false;
            if (StopLine == null)
            {
                pageFlag = true;
                if (_PrivateLines.Count > 0)
                {
                    for (int iCount = _PrivateLines.Count - 1;
                        iCount >= startLineIndex;
                        iCount--)
                    {
                        _PrivateLines.RemoveAt(iCount);
                    }
                }
                foreach (DomContentLine line2 in newLines)
                {
                    _PrivateLines.Add(line2);
                }
            }
            else
            {
                DomContentLine startLine = startElement.OwnerLine;
                if (startLine == null)
                {
                    startLine = _PrivateLines[0];
                }
                int endLineIndex = _PrivateLines.IndexOf(StopLine);
                if (endLineIndex - startLineIndex + 1 != newLines.Count)
                {
                    // 新增的行和要删除的行数不一致,需要重新分页
                    pageFlag = true;
                }
                else
                {
                    for (int iCount = 0; iCount < newLines.Count; iCount++)
                    {
                        DomContentLine line1 = newLines[iCount];
                        DomContentLine line2 = _PrivateLines[iCount + startLineIndex];
                        if (line1.Height != line2.Height)
                        {
                            // 行高发生改变,则需要重新分页
                            pageFlag = true;
                            break;
                        }
                    }
                }
                // 判断 StopLine 是否更新
                if (pageFlag == false && newLines.Count > 1)
                {
                    endLineIndex--;
                    newLines.RemoveAt(newLines.Count - 1);
                }

                // 将新的文档行对象替换掉旧的文档行对象
                if (pageFlag == false)
                {
                    // 若没有分页则直接替换文本行
                    for (int iCount = 0; iCount < newLines.Count; iCount++)
                    {
                        DomContentLine line = newLines[iCount];
                        invalidateLines.Add(line);
                        //if (this.OwnerDocument.EditorControl != null)
                        //{
                        //    this.OwnerDocument.EditorControl.ViewInvalidate(
                        //        line.AbsBounds ,
                        //        this.ContentPartyStyle );
                        //}
                        line._OwnerPage = _PrivateLines[iCount + startLineIndex].OwnerPage;
                        this.PrivateLines.Replace(iCount + startLineIndex, line);
                    }
                }
                else
                {
                    // 若需要分页则删除旧行,然后插入新的文本行
                    for (int iCount = endLineIndex; iCount >= startLineIndex; iCount--)
                    {
                        if (iCount >= 0)
                        {
                            if (this.OwnerDocument.EditorControl != null)
                            {
                                DomContentLine OldLine = _PrivateLines[iCount];
                                invalidateLines.Add(OldLine);

                                //this.OwnerDocument.EditorControl.ViewInvalidate(
                                //    OldLine.AbsBounds ,
                                //    this.ContentPartyStyle );
                            }
                            this.PrivateLines.RemoveAt(iCount);
                        }
                    }//for
                    for (int iCount = 0; iCount < newLines.Count; iCount++)
                    {
                        if (this.OwnerDocument.EditorControl != null)
                        {
                            DomContentLine line = newLines[iCount];
                            invalidateLines.Add(line);

                            //this.OwnerDocument.EditorControl.ViewInvalidate(
                            //    line.AbsBounds,
                            //    this.ContentPartyStyle );
                        }
                        this.PrivateLines.Insert(iCount + startLineIndex, newLines[iCount]);
                    }//for
                }
            }

            // 更新文档行位置
            UpdateLinePosition(vertialAlign, false, false);

            if (this.OwnerDocument.EditorControl != null)
            {
                //if (vertialAlign == VerticalAlignStyle.Top)
                {
                    foreach (DomContentLine line in invalidateLines)
                    {
                        this.OwnerDocument.EditorControl.ViewInvalidate(
                            line.AbsBounds,
                            this.ContentPartyStyle);
                    }
                    foreach (DomContentLine line in newLines)
                    {
                        this.OwnerDocument.EditorControl.ViewInvalidate(
                            line.AbsBounds,
                            this.ContentPartyStyle);
                    }
                }
                //else
                //{
                //    //this.OwnerDocument.EditorControl.ViewInvalidate(
                //    //    this.AbsBounds, 
                //    //    this.ContentPartyStyle);
                //}
                //myBindControl.Update();
            }

            foreach (DomContentLine line2 in _PrivateLines)
            {
                foreach (DomElement element in line2)
                {
                    element.OwnerLine = line2;
                    element.SizeInvalid = false;
                }
            }

            if (freakElements.Count > 0)
            {
                // 由于修整连续的英文字母和数字导致元素进入某个新行而又退出行
                // 元素Left值发生改变，此处使用文本行的 RefreshState 函数刷新
                // 行内设置来恢复这些反复无常的元素的位置
                DomContentLine LastFixLine = null;
                foreach (DomElement element in freakElements)
                {
                    if (element.OwnerLine != null
                        && _PrivateLines.Contains(element.OwnerLine)
                        && element.OwnerLine != LastFixLine)
                    {
                        LastFixLine = element.OwnerLine;
                        LastFixLine.RefreshState();
                    }
                }
            }

            pageFlag = false;
            // 比较新的文本行的高度和旧的文本行的高度是否一致
            // 若文本行个数或高度发生改变,则会导致重新分页
            // 若文本行个数和高度都没变,则无需重新分页
            if (linesInfoBack.Length != _PrivateLines.Count * 2)
            {
                pageFlag = true;
            }
            else
            {
                for (int iCount = 0; iCount < this.PrivateLines.Count; iCount++)
                {
                    DomContentLine line = this.PrivateLines[iCount];
                    if (line.Top != linesInfoBack[iCount * 2])
                    {
                        pageFlag = true;
                        break;
                    }
                    if (line.Height != linesInfoBack[iCount * 2 + 1])
                    {
                        pageFlag = true;
                        break;
                    }
                    line.GlobalIndex = lineIndexBack[iCount * 2];
                    line.IndexInPage = lineIndexBack[iCount * 2 + 1];
                }//for
            }

            if (pageFlag)
            {
                if (this.OwnerDocument.EditorControl != null)
                {
                    this.OwnerDocument.EditorControl.ViewInvalidate(
                            this.AbsBounds,
                            this.ContentPartyStyle);
                }
                return isBodyContent;
                //return true;
            }
            else
            {
                if (isBodyContent)
                {
                    this.UpdateLineOwnerPage();
                }
                return false;
            }
        }


        /// <summary>
        /// 刷新文本行所在的文档页对象
        /// </summary>
        private void UpdateLineOwnerPage()
        {
            foreach (DomContentLine myLine in this.PrivateLines)
            {
                if (myLine.OwnerPage == null)
                {
                    foreach (PrintPage page in this.OwnerDocument.Pages)
                    {
                        if (myLine.Top >= page.Top
                            && myLine.Top < page.Bottom)
                        {
                            myLine._OwnerPage = page;
                            break;
                        }
                    }//foreach
                }//if
            }//foreach
        }

        private bool CheckStopLine(DomContentLine line1, DomContentLine line2)
        {
            if (line1.InvalidateState || line2.InvalidateState)
            {
                return false;
            }
            if (line1.Count != line2.Count)
            {
                return false;
            }
            if (line1.PaddingLeft != line2.PaddingLeft)
            {
                return false;
            }
            for (int iCount = 0; iCount < line1.Count; iCount++)
            {
                DomElement element = line1[iCount];
                if (element != line2[iCount])
                {
                    return false;
                }
                if (element.SizeInvalid)
                {
                    return false;
                }
            }//for
            return true;
        }

        /// <summary>
        /// 内容部分样式
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public PageContentPartyStyle ContentPartyStyle
        {
            get
            {
                if (this.OwnerDocument == null)
                {
                    return PageContentPartyStyle.Body;
                }
                else if (this == this.OwnerDocument.Body)
                {
                    return PageContentPartyStyle.Body;
                }
                else if (this == this.OwnerDocument.Header)
                {
                    return PageContentPartyStyle.Header;
                }
                else if (this == this.OwnerDocument.Footer)
                {
                    return PageContentPartyStyle.Footer;
                }
                return PageContentPartyStyle.Body;
            }
        }

        /// <summary>
        /// 元素是否可见
        /// </summary>
        /// <param name="element">元素对象</param>
        /// <returns>是否可见</returns>
        public virtual bool IsVisible(DomElement element)
        {
            return this.OwnerDocument.IsVisible(element);
        }


        /// <summary>
        /// 计算分页符
        /// </summary>
        /// <param name="pos">分页符位置</param>
        /// <returns>修正后的分页符位置</returns>
        public void FixPageLine( PageLineInfo info )
        {
            if (this.PrivateLines.Count == 0)
            {
                // 没有任何文本行，不进行分页
                return ;
            }
            //if (this.PrivateLines.Count == 1)
            //{
            //    // 只有一行内容
            //    XTextLine line = this.PrivateLines[0];
            //    if (this is XTextTableCellElement)
            //    {
            //        // 若当前行是表格
            //        if (this.AbsTop - info.LastPosition > info.MinPageContentHeight)
            //        {
            //            if (info.CurrentPoistion < line.AbsBottom)
            //            {
            //                //if (info.CurrentPoistion - this.AbsTop < info.MinPageContentHeight)
            //                {
            //                    info.CurrentPoistion = (int)this.AbsTop;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            // 该文本行高度非常高，若分页可能导致不合适的分页状态
            //        }
            //    }
            //    else
            //    {
            //        info.CurrentPoistion = (int)line.AbsBottom;
            //    }
            //    return;
            //}
            // 获得当前行
            DomContentLine currentLine = null;
            foreach (DomContentLine line in this.PrivateLines)
            {
                if (info.CurrentPoistion > line.AbsTop)
                {
                    currentLine = line;
                     
                }
                else
                {
                    break;
                }
            }//foreach
            if (currentLine == null)
            {
                // 没有找到当前行，不修改分页
                return ;
            }
            if ( info.CurrentPoistion > currentLine.AbsBottom)
            {
                // 若分页线位置超过文本行，则不修改分页
                if (currentLine == this.PrivateLines.LastLine )
                {
                    // 若当前行是最后一行文本
                    RectangleF bounds = this.AbsBounds ;
                    if( info.CurrentPoistion >= bounds.Bottom )
                    {
                        // 若分页线位置大于对象边框低边界位置，则设置为对象低边界位置
                        info.CurrentPoistion = (int)Math.Ceiling( bounds.Bottom );
                        info.SourceElement = this;
                    }
                    else if( bounds.Bottom - info.CurrentPoistion 
                        < this.OwnerDocument.PixelToDocumentUnit( 15 ) )
                    {
                        // 若分页线的位置在最后一行和对象底边界位置之间，
                        // 而且距离底边界之间距离小于15个像素
                        // 则进行提前一行分页
                        goto SetPagePosition;
                    }
                }
                
                return ;
                //return (int)currentLine.AbsBottom;
            }

    SetPagePosition :
            {
                 
                {
                    int index = this.PrivateLines.IndexOf(currentLine);
                    int newPos = 0;
                    if (index > 0)
                    {
                        // 分页线移动到上一个文本行和当前文本行中间
                        DomContentLine preLine = this.PrivateLines[index - 1];
                        newPos = (int)(preLine.AbsTop + preLine.Height + preLine.AdditionHeight / 2 );
                    }
                    else
                    {
                        // 分页线移动到本文本行的顶端位置
                        newPos = (int)currentLine.AbsTop;
                    }
                    info.SourceElement = currentLine[0];
                    if (newPos - info.LastPosition > info.MinPageContentHeight)
                    {
                        // 设置新分页线
                        info.CurrentPoistion = newPos;
                        RectangleF bounds = this.AbsBounds;
                        if (info.CurrentPoistion - bounds.Top < this.OwnerDocument.PixelToDocumentUnit(15))
                        {
                            info.CurrentPoistion =(int) bounds.Top;
                            info.SourceElement = this;
                        }
                    }
                    else
                    {
                        // 该文本行可能非常高，可能一页放不下，因此不适合进行分页
                    }
                }
            }
        }
         
        /// <summary>
        /// 内容是否为空
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public bool HasContentElement
        {
            get
            {
                if (this.Elements.Count == 0)
                {
                    return false;
                }
                if (this.Elements.Count == 1 && this.Elements[0] is DomParagraphFlagElement)
                {
                    return false;
                }
                return true;
            }
        }


        /// <summary>
        /// 绘制文档内容
        /// </summary>
        /// <param name="args">参数</param>
        public override void DrawContent(DocumentPaintEventArgs args)
        {
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed ;
            DomDocumentContentElement dce = this.DocumentContentElement;
            //XTextParagraphFlagElement lastPE = null;
            bool showPermissionMark = this.OwnerDocument.Options.SecurityOptions.ShowPermissionMark;
            foreach (DomContentLine line in this.PrivateLines)
            {
                if (line.Count == 0)
                {
                    // 这行文档没有任何内容
                    continue;
                }
                // 绘制一行文档
                bool drawLine = true;
                if (args.ClipRectangle.IsEmpty == false)
                {
                    Rectangle cr = Rectangle.Intersect(
                        args.ClipRectangle,
                        Rectangle.Ceiling(line.AbsBounds));
                    if (cr.IsEmpty)
                    {
                        drawLine = false;
                    }
                    else
                    {
                        if (args.RenderStyle != DocumentRenderStyle.Paint)
                        {
                            if (cr.Height <= 5)
                            {
                                drawLine = false;
                            }
                        }
                    }
                }
                if (drawLine == false)
                {
                    continue;
                }
                // 绘制行首标记
                DomElement FirstElement = line[0];
                DomParagraphFlagElement pe = this.OwnerDocument.GetParagraphEOFElement(FirstElement);
                 
                RectangleFCounter LightRect = new RectangleFCounter();
                foreach (DomElement e in line)
                {
                    // 遍历文档行中的元素，绘制图形
                     

                    // 绘制一个文档元素
                    System.Drawing.RectangleF rect = e.AbsBounds;
                    System.Drawing.RectangleF bounds = new System.Drawing.RectangleF(
                        e.AbsLeft,
                        line.AbsTop + DomContentLine.ContentTopFix * 2,
                        e.Width + e.WidthFix,
                        line.Height );
                    if (args.ClipRectangle.IsEmpty == false)
                    {
                        rect = System.Drawing.RectangleF.Intersect(args.ClipRectangle, bounds);
                    }
                    if (rect.IsEmpty == false)
                    {
                        if (e.OwnerLine == null)
                        {
                            //Console.Write("aa");
                            //e.OwnerLine = line;
                            //continue;
                        }
                        args.Element = e;
                        args.Style = e.RuntimeStyle;
                        e.Draw(args);
                        if (showPermissionMark)
                        {
                            args.Render.DrawPermissionMark(e, args);
                        }
                        //if (e is XTextTableElement)
                        //{
                        //    XTextTableElement table = (XTextTableElement)e;
                        //    table.DrawContent(args);
                        //}
                        //else
                        //{
                        //    args.Render.RefreshView(e, args);
                        //}
                        if (args.Cancel)
                        {
                            // 用户取消操作,退出绘制操作
                            break;
                        }
                        //e.RefreshView(args);
                        if (args.ActiveMode && dce.IsSelected(e))
                        {
                            LightRect.Add(bounds);
                        }
                    }//if
                }//foreach( XTextElement e in line )
                //if (line.LastElement is XTextParagraphFlagElement)
                //{
                //    XTextParagraphFlagElement pe2 = (XTextParagraphFlagElement)line.LastElement;
                //    DocumentContentStyle style = pe2.RuntimeStyle;
                //    if (style.HasVisibleBorder)
                //    {
                //        // 绘制段落边框
                //        RectangleF pBounds = pe2.ParagraphBounds;
                //        pBounds.Inflate(style.BorderSpacing * 2, style.BorderSpacing * 2);
                //        using (Pen pen = style.CreateBorderPen())
                //        {
                //            style.DrawBorder(args.Graphics, pen, pBounds);
                //        }
                //    }
                //}
                if (args.RenderStyle == DocumentRenderStyle.Paint)
                {
                    if (LightRect.IsEmpty == false)
                    {
                        if (this.OwnerDocument.EditorControl != null)
                        {
                            RectangleF rect = RectangleF.Empty;
                           
                            {
                                rect = LightRect.Value;
                            }
                            this.OwnerDocument.EditorControl.AddSelectionAreaRectangle( Rectangle.Ceiling ( rect));

                            //this.OwnerDocument.EditorControl.ReversibleViewFillRect(
                            //    rect,
                            //    args.Graphics);
                        }
                    }
                }
            }//foreach( XTextLine line in myLines )
        }

        internal ListDictionary<DomParagraphFlagElement, List<DomContentLine>> GetParagraphLines()
        {
            int startIndex = 0;
            ListDictionary<DomParagraphFlagElement, List<DomContentLine>> result
                = new ListDictionary<DomParagraphFlagElement, List<DomContentLine>>();
            for (int iCount = 0; iCount < this.PrivateLines.Count; iCount++)
            {
                DomContentLine line = this.PrivateLines[iCount];
                if (line.LastElement is DomParagraphFlagElement)
                {
                    List<DomContentLine> lines = new List<DomContentLine>();
                    for (int iCount2 = startIndex; iCount2 <= iCount; iCount2++)
                    {
                        lines.Add( this.PrivateLines[ iCount2 ] );
                    }
                    startIndex = iCount + 1 ;
                    result[ ( DomParagraphFlagElement ) line.LastElement ] = lines ;
                }
            }
            return result ;
        }

        public override void WriteHTML(WriterHtmlDocumentWriter writer)
        {
            // 段落中第一行对象
            int paragraphStartLineIndex = 0;
            // 段落分组列表
            ListDictionary<DomParagraphFlagElement, List<DomContentLine>> paragraphs
                = new ListDictionary<DomParagraphFlagElement, List<DomContentLine>>();
            // 按照所属段落对所有的文本行进行分组
            for (int iCount = 0; iCount < this.PrivateLines.Count; iCount++)
            {
                DomContentLine line = this.PrivateLines[iCount];
                if (line.LastElement is DomParagraphFlagElement)
                {
                    List<DomContentLine> lines = new List<DomContentLine>();
                    for (int iCount2 = paragraphStartLineIndex; iCount2 <= iCount; iCount2++)
                    {
                        DomContentLine line2 = this.PrivateLines[iCount2];
                        // 判断该行是否输出
                        if (writer.ClipRectangle.IsEmpty == false)
                        {
                            if (writer.ClipRectangle.IntersectsWith(
                                Rectangle.Ceiling(line2.AbsBounds)) == false)
                            {
                                // 该文本行不输出
                                continue;
                            }//if
                        }//if
                        if (writer.IncludeSelectionOndly)
                        {
                            // 判断是否包含被选中的内容
                            bool output = false;
                            foreach (DomElement element in line2)
                            {
                                if (element.HasSelection)
                                {
                                    output = true;
                                    break;
                                }
                            }//foreach
                            if (output == false)
                            {
                                continue;
                            }
                        }
                        lines.Add(line2);
                    }//for
                    if (lines.Count > 0)
                    {
                        paragraphs[(DomParagraphFlagElement)line.LastElement] = lines;
                    }
                    paragraphStartLineIndex = iCount + 1;
                }//if
            }//for

            // 最后一个段落列表样式
            ParagraphListStyle lastListStyle = ParagraphListStyle.None;
            foreach (DomParagraphFlagElement flag in paragraphs.Keys)
            {
                if (flag.ListStyle != lastListStyle)
                {
                    if (lastListStyle == ParagraphListStyle.BulletedList
                        || lastListStyle == ParagraphListStyle.NumberedList)
                    {
                        writer.WriteEndElement();
                    }
                    lastListStyle = flag.ListStyle;
                    if (lastListStyle == ParagraphListStyle.BulletedList)
                    {
                        writer.WriteStartElement("ul");
                    }
                    else if (lastListStyle == ParagraphListStyle.NumberedList)
                    {
                        writer.WriteStartElement("ol");
                    }
                }
                if (lastListStyle == ParagraphListStyle.BulletedList
                    || lastListStyle == ParagraphListStyle.NumberedList)
                {
                    writer.WriteStartElement("li");
                }
                else
                {
                    writer.WriteStartElement("p");
                }
                DocumentContentStyle pstyle = flag.RuntimeStyle ;
                writer.WriteStartStyle();
                writer.WriteDocumentContentStyle(pstyle, flag);
                writer.WriteEndStyle();
                // 输出文本行内容
                int lineIndex = 0;
                
                foreach (DomContentLine line in paragraphs[flag])
                {
                    bool keepLineBreak = false;
                    if (lineIndex > 0
                        && this is DomDocumentContentElement
                        && writer.Options.KeepLineBreak )
                    {
                        keepLineBreak = true;
                    }

                    if (keepLineBreak)
                    {
                        writer.WriteStartElement("br");
                        writer.WriteEndElement();
                    }
                    lineIndex++;
                    DomElementList elements = WriterUtils.MergeElements(
                        line,
                        writer.IncludeSelectionOndly);
                    //if (splitLines)
                    //{
                    //    writer.WriteStartElement("span");
                    //    writer.WriteStartStyle();
                    //    writer.WriteStyleItem("white-space", "nowrap");
                    //    writer.WriteStyleItem("text-align", "justify");
                    //    //writer.WriteStyleItem("width", "100%");
                    //    writer.WriteEndStyle();
                    //}
                    foreach (DomElement element in elements )
                    {
                        element.WriteHTML(writer);
                    }
                    //if (keepLineBreak)
                    //{
                    //    writer.WriteEndElement();
                    //}
                }//foreach
                // 结束处理一个段落
                writer.WriteEndElement();
            }//foreach

            if (lastListStyle == ParagraphListStyle.BulletedList
                || lastListStyle == ParagraphListStyle.NumberedList)
            {
                writer.WriteEndElement();
            }

        }

        /// <summary>
        /// 获得区间包含的段落对象列表
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore ]
        public DomElementList ParagraphsEOFs
        {
            get
            {
                DomElementList list = new DomElementList();
                if (this.PrivateContent.Count > 0  )
                {
                    foreach (DomElement element in this.PrivateContent)
                    {
                        if (element is DomParagraphFlagElement)
                        {
                            list.Add(element);
                        }
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// 对象所属文档对象
        /// </summary>
        [Browsable( false )]
        [System.Xml.Serialization.XmlIgnore ]
        public override DomDocument OwnerDocument
        {
            get
            {
                return base.OwnerDocument;
            }
            set
            {
                base.OwnerDocument = value;
                if (this._PrivateLines != null)
                {
                    foreach (DomContentLine line in this._PrivateLines)
                    {
                        line._OwnerDocument = value ;
                    }
                }
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="Deeply">是否深入复制子对象</param>
        /// <returns>复制品</returns>
        public override DomElement Clone(bool Deeply)
        {
            DomContentElement ce = ( DomContentElement ) base.Clone(Deeply);
            if (Deeply && this.Visible )
            {
                if (this._PrivateContent != null)
                {
                    ce._PrivateContent = new DomElementList();
                    ce.AppendContent(ce._PrivateContent, true);
                }
                if (ce._PrivateContent.Count != this._PrivateContent.Count)
                {
                    //System.Console.WriteLine("");
                    throw new InvalidOperationException( this.GetType().Name + "状态不对");
                }
                if (this._PrivateLines != null)
                {
                    ce._PrivateLines = new DomContentLineList();
                    foreach (DomContentLine line in this._PrivateLines)
                    {
                        DomContentLine newLine = ( DomContentLine ) line.Clone();
                        newLine._OwnerContentElement = ce ;
                        newLine.Clear();
                        for (int iCount = 0; iCount < line.Count; iCount++)
                        {
                            int index = this._PrivateContent.IndexOf(line[iCount]);
                            newLine.AddRaw(ce._PrivateContent[index]);
                            ce._PrivateContent[index].OwnerLine = newLine;
                        }//for
                        ce._PrivateLines.Add(newLine);
                        newLine.Width = line.Width;
                        newLine.Height = line.Height;
                        newLine.AdditionHeight = line.AdditionHeight;
                        newLine.BeforeSpacing = line.BeforeSpacing;
                        newLine.Left = line.Left;
                        newLine.PaddingLeft = line.PaddingLeft;
                        newLine.PaddingRight = line.PaddingRight;

                    }//foreach
                }
            }
            return ce;
        }

    }//public class XTextDocumentContent : XTextElementContainer
}
