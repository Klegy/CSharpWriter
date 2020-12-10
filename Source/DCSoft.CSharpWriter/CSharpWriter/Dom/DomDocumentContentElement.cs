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
using System.ComponentModel;
using System.Xml.Serialization;
using DCSoft.Printing;
using DCSoft.Drawing;
using System.Drawing;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 文档级内容对象
    /// </summary>
    [Serializable()]
    public class DomDocumentContentElement : DomContentElement
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomDocumentContentElement()
        {
            this._Selection = new DomSelection(this);
            this.Content._DocumentContentElement = this;
        }

        [NonSerialized]
        private DomContent _Content = new DomContent();
        /// <summary>
        /// 文档内容管理对象
        /// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public DomContent Content
        {
            get
            {
                if (_Content == null)
                {
                    System.Diagnostics.Debugger.Break();
                }
                if (_Content.Count == 0)
                {
                    FillContent();
                }
                return _Content;
            }
        }

        /// <summary>
        /// 填充 Content 列表
        /// </summary>
        internal void FillContent()
        {
            _Content.Clear();
            _Content._OwnerDocument = this.OwnerDocument;
            _Content._DocumentContentElement = this;
            this.AppendContent(_Content, false);
            int iCount = 0;
            foreach (DomElement e in _Content)
            {
                e._ViewIndex = iCount;
                iCount++;
            }//foreach
        }

        /// <summary>
        /// 更新文档内容元素列表
        /// </summary>
        public override void UpdateContentElements(bool updateParentContentElement)
        {
            int count = this.Elements.Count;
            _ContentElements = null;
            if (_Content == null)
            {
                _Content = new DomContent();
            }
            _Content._DocumentContentElement = this;
            _Content._OwnerDocument = this.OwnerDocument;
            _Content.Clear();
            //this.Selection.Refresh(0, 0);
            //if (this.Elements.Count != count) System.Windows.Forms.MessageBox.Show("zzz-1");
            base.UpdateContentElements( updateParentContentElement );
            //if (this.Elements.Count != count) System.Windows.Forms.MessageBox.Show("zzz-2"); 
            this.OwnerDocument.HighlightManager.UpdateHighlightInfos();
        }

        

        [NonSerialized]
        private DomElementList _ContentElements = null;
        /// <summary>
        /// 所有的可承载内容的容器元素列表
        /// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public DomElementList ContentElements
        {
            get
            {
                if (_ContentElements == null)
                {
                    _ContentElements = new DomElementList();
                    _ContentElements.Add(this);
                    GetContentElements(this, _ContentElements);
                }
                return _ContentElements; 
            }
        }

        private void GetContentElements(DomContainerElement rootElement, DomElementList result)
        {
            foreach (DomElement element in rootElement.Elements)
            {
                 if (element is DomContentElement)
                {
                    result.Add(element);
                }
                if (element is DomContainerElement)
                {
                    GetContentElements((DomContainerElement)element, result);
                }
            }
        }
         
        /// <summary>
        /// 元素是否处于选择状态
        /// </summary>
        /// <param name="element">元素对象</param>
        /// <returns>是否选择</returns>
        public virtual bool IsSelected(DomElement element)
        {
            return _Selection.Contains(element);
        }

        /// <summary>
        /// 当前选择区域信息对象
        /// </summary>
        [NonSerialized]
        private DomSelection _Selection = null;
        /// <summary>
        /// 当前选择区域信息对象
        /// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public DomSelection Selection
        {
            get
            {
                if (_Selection == null)
                {
                    _Selection = new DomSelection(this);
                }
                return _Selection;
            }
        }

        /// <summary>
        /// 判断是否存在被用户选择的内容元素
        /// </summary>
        [Browsable( false )]
        new public bool HasSelection
        {
            get
            {
                return this.Selection.Length != 0;
            }
        }

        /// <summary>
        /// 判断是否存在没有被逻辑删除的被选择的元素内容
        /// </summary>
        public bool HasSelectionWithouLogicDeleted
        {
            get
            {
                if (this.Selection.Length != 0)
                {
                    foreach (DomElement element in this.Selection.ContentElements)
                    {
                        if (element.Style.DeleterIndex < 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 更新文内容选择状态
        /// </summary>
        /// <param name="startIndex">选择区域的起始位置</param>
        /// <param name="length">选择区域的包含文档内容元素的个数</param>
        public bool SetSelection(int startIndex, int length)
        {
            int oldSelectionStart = this.Selection.StartIndex;
            int oldSelectionLength = this.Selection.Length;
            // 修正旧的选择区域
            // 由于执行了某些操作改变了文档内容但没有更新选择区域，因此旧的选择区域可能超出文档范围，
            // 在此进行修正。
            this.Content.FixRange(ref oldSelectionStart, ref oldSelectionLength);

            if (this.Selection.NeedRefresh(startIndex, length))
            {
                // 触发文档对象的OnSelectionChanging事件
                SelectionChangingEventArgs args = new SelectionChangingEventArgs();
                args.Documnent = this.OwnerDocument;
                args.OldSelectionIndex = this.Selection.StartIndex;
                args.OldSelectionLength = this.Selection.Length;
                args.NewSelectionIndex = startIndex;
                args.NewSelectionLength = length;
                this.OwnerDocument.OnSelectionChanging(args);
                if (args.Cancel)
                {
                    // 用户取消操作
                    return false;
                }
                // 使用用户设置的新的选择区域配置
                startIndex = args.NewSelectionIndex;
                length = args.NewSelectionLength;
                
                if (this.Selection.Refresh(startIndex, length))
                {
                    if (this.OwnerDocument.EditorControl != null)
                    {
                        //this.OwnerDocument.EditorControl.Invalidate();
                        this.OwnerDocument.EditorControl.UpdateTextCaret();
                    }
                    DomElement oldCurrentElement = this.Content[oldSelectionStart];
                    RaiseFocusEvent(
                        this.Content.SafeGet(oldSelectionStart),
                        this.Content.SafeGet(this.Selection.StartIndex));

                    //XTextElementList oldParents = WriterUtils.GetParentList(this.Content[oldSelectionStart]);
                    //XTextElementList newParents = WriterUtils.GetParentList(this.Content[this.Selection.StartIndex]);
                    //// 触发旧的容器元素的失去输入焦点事件
                    //foreach (XTextElement oldParent in oldParents)
                    //{
                    //    if (newParents.Contains(oldParent) == false)
                    //    {
                    //        ((XTextContainerElement)oldParent).OnLostFocus(this, EventArgs.Empty);

                    //        //DocumentEventArgs args2 = new DocumentEventArgs(
                    //        //    this.OwnerDocument,
                    //        //    oldParent,
                    //        //    DocumentEventStyles.LostFocus);
                    //        //oldParent.HandleDocumentEvent(args2);
                    //    }
                    //}
                    //// 触发新的容器元素的获得输入焦点事件
                    //foreach (XTextElement newParent in newParents)
                    //{
                    //    if (oldParents.Contains(newParent) == false)
                    //    {
                    //        ((XTextContainerElement)newParent).OnGotFocus(this, EventArgs.Empty);

                    //        //DocumentEventArgs args2 = new DocumentEventArgs(
                    //        //    this.OwnerDocument,
                    //        //    newParent,
                    //        //    DocumentEventStyles.GotFocus);
                    //        //newParent.HandleDocumentEvent(args2);
                    //    }
                    //}
                    // 触发文档对象的选择区域发生改变事件
                    this.OwnerDocument.OnSelectionChanged( );
                    return true;
                }
            }
            return false;
        }

        internal void RaiseFocusEvent(DomElement oldCurrentElement, DomElement newCurrentElement)
        {
            DomElementList oldParents = oldCurrentElement == null ? 
                new DomElementList() : WriterUtils.GetParentList2(oldCurrentElement);
            if (oldCurrentElement != null)
            {
                oldParents.Insert(0, oldCurrentElement);
            }
            DomElementList newParents = newCurrentElement == null ?
                new DomElementList() : WriterUtils.GetParentList2(newCurrentElement);
            if (newCurrentElement != null)
            {
                newParents.Insert(0, newCurrentElement);
            }
            // 触发旧的容器元素的失去输入焦点事件
            foreach (DomElement oldParent in oldParents)
            {
                if (newParents.Contains(oldParent) == false)
                {
                    //((XTextContainerElement)oldParent).OnLostFocus(this, EventArgs.Empty);

                    DocumentEventArgs args2 = new DocumentEventArgs(
                        this.OwnerDocument,
                        oldParent,
                        DocumentEventStyles.LostFocus);
                    oldParent.HandleDocumentEvent(args2);
                }
            }
            // 触发新的容器元素的获得输入焦点事件
            foreach (DomElement newParent in newParents)
            {
                if (oldParents.Contains(newParent) == false)
                {
                    //((XTextContainerElement)newParent).OnGotFocus(this, EventArgs.Empty);

                    DocumentEventArgs args2 = new DocumentEventArgs(
                        this.OwnerDocument,
                        newParent,
                        DocumentEventStyles.GotFocus);
                    newParent.HandleDocumentEvent(args2);
                }
            }
        }


        /// <summary>
        /// 返回指定区域的文档区域
        /// </summary>
        /// <param name="StartIndex">区域开始位置</param>
        /// <param name="EndIndex">区域结束位置</param>
        /// <returns>区域对象</returns>
        public virtual ContentRange GetRange(int StartIndex, int EndIndex)
        {
            return new ContentRange(this, StartIndex, EndIndex);
        }

        /// <summary>
        /// 当前行
        /// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public DomContentLine CurrentLine
        {
            get
            {
                return this.Content.CurrentLine;
            }
        }
        ///// <summary>
        ///// 判断元素是否是高亮度显示
        ///// </summary>
        ///// <param name="element">元素对象</param>
        ///// <returns>是否高亮度显示</returns>
        //public virtual bool IsHighlight(XTextElement element)
        //{
        //    // 搜索用户设置的高亮度显示区域，用户设置的优先处理
        //    if (_HighlightRanges != null && _HighlightRanges.Count > 0)
        //    {
        //        for (int iCount = _HighlightRanges.Count - 1; iCount >= 0; iCount++)
        //        {
        //            HighlightInfo info = _HighlightRanges[iCount];
        //            if (info.Contains(element))
        //            {

        //            }
        //        }
        //        return _HighlightRanges[element] != null;
        //    }
        //    return false;
        //}


        /// <summary>
        /// 当前元素
        /// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public DomElement CurrentElement
        {
            get
            {
                return this.Content.CurrentElement;
            }
        }


        /// <summary>
        /// 当前段落对象
        /// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public DomParagraphFlagElement CurrentParagraphEOF
        {
            get
            {
                return this.Content.CurrentParagraphEOF;
            }
        }

        /// <summary>
        /// 文本行列表
        /// </summary>
        [NonSerialized]
        private DomContentLineList _Lines = null;
        /// <summary>
        /// 文本行列表
        /// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public DomContentLineList Lines
        {
            get
            {
                if (_Lines == null)
                {
                    _Lines = new DomContentLineList();
                    FillLines(this, _Lines);
                }
                return _Lines;
            }
        }

        /// <summary>
        /// 更新文档行的编号
        /// </summary>
        internal void GlobalUpdateLineIndex()
        {
            UpdateLineIndex(1);
            PrintPage lastPage = null;
            int pageFirstLineIndex = 0 ;
            foreach (DomContentLine line in this.Lines)
            {
                if (line.OwnerPage != lastPage)
                {
                    lastPage = line.OwnerPage;
                    pageFirstLineIndex = line.GlobalIndex;
                }
                line.IndexInPage = line.GlobalIndex - pageFirstLineIndex + 1 ;
            }//foreach
        }

        /// <summary>
        /// 重新分行
        /// </summary>
        /// <param name="startElement"></param>
        /// <param name="endElement"></param>
        /// <returns></returns>
        internal override bool ParticalRefreshLines(
            DomElement startElement,
            DomElement endElement ,
            VerticalAlignStyle verticalAlign )
        {
            bool result = base.ParticalRefreshLines(
                startElement,
                endElement,
                verticalAlign);
            _Lines = null;
            this.Height = this.ContentHeight;
            return result;
        }

        internal void RefreshGlobalLines()
        {
            _Lines = null;
        }

        /// <summary>
        /// 收集各层元素包含的文档行对象
        /// </summary>
        /// <param name="contentElement"></param>
        /// <param name="lines"></param>
        private void FillLines(DomContentElement contentElement, DomContentLineList lines)
        {
            foreach (DomContentLine line in contentElement.PrivateLines)
            {
                if (line.Count == 0)
                {
                    throw new InvalidOperationException("line count = 0 ");
                }
                 
                {
                    lines.Add(line);
                }
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
            this.Selection.Refresh(0, 0);
        }


        /// <summary>
        /// 清空文档内容
        /// </summary>
        public override void Clear()
        {
            this.Elements.Clear();
            this.Elements.Add(this.OwnerDocument.CreateParagraphEOF());
            //myElements.Add( this.myEOFElement );
            this.UpdateContentElements(true);
            this.ExecuteLayout();
        }

        /// <summary>
        /// 输出RTF文档
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteRTF(DCSoft.CSharpWriter.RTF.RTFContentWriter writer)
        {
            writer.bolFirstParagraph = true;
            writer.ClipRectangle = new RectangleF(0, 0, this.OwnerDocument.Width, this.Height);
            base.WriteRTF(writer);
            writer.bolFirstParagraph = false;
        }

        public int FixPageLinePosition(int pos)
        {
            PageLineInfo info = new PageLineInfo();
            info._CurrentPoistion = pos;
            info.LastPosition = 0;
            info.ForJumpPrint = false ;

            this.FixPageLine(info);

            return info.CurrentPoistion;
        }

        #region 一些属性无效 ***********************************************

        /// <summary>
        /// 方法无效
        /// </summary>
        private new void EditorDelete(bool logUndo)
        {
            throw new NotSupportedException("EditorDelete");
        }
         
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        new private DomElement PreviousElement { get { return null; } }
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        new private DocumentContentStyle RuntimeStyle { get { return null; } }
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        new private DocumentContentStyle Style { get { return null; } }
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        new private int StyleIndex { get { return 0; } }
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        new private int ViewIndex { get { return 0; } }
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        new private int ColumnIndex { get { return 0; } }
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        new private int ElementIndex { get { return 0; } }

        #endregion


    
    }//public class XTextDocumentContentElement : XTextContentElement


    /// <summary>
    /// 文档正文对象
    /// </summary>
    [Serializable()]
    [System.Xml.Serialization.XmlType("XTextBody")]
    [System.Diagnostics.DebuggerDisplay("Body :{ PreviewString }")]
    public class XTextDocumentBodyElement : DomDocumentContentElement
    {
        public override string PreviewString
        {
            get
            {
                return "Body:" + base.PreviewString;
            }
        }
    }

    /// <summary>
    /// 文档页眉对象
    /// </summary>
    [Serializable()]
    [System.Xml.Serialization.XmlType("XTextHeader")]
    [System.Diagnostics.DebuggerDisplay("Header :{ PreviewString }")]
    public class XTextDocumentHeaderElement : DomDocumentContentElement
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XTextDocumentHeaderElement()
        {
        }

        public override void FixElements()
        {
            if (this.Elements.Count == 0
                || (this.Elements.LastElement is DomParagraphFlagElement) == false)
            {
                DomParagraphFlagElement flag = new DomParagraphFlagElement();
                flag.AutoCreate = true;
                DocumentContentStyle style = new DocumentContentStyle();
                style.Align = DocumentContentAlignment.Center;
                //style.BorderWidth = 1;
                //style.BorderColor = Color.Black;
                //style.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                //style.BorderLeft = false;
                //style.BorderTop = false;
                //style.BorderRight = false;
                //style.BorderBottom = true;
                //style.BorderSpacing = 8;
                flag.StyleIndex = this.OwnerDocument.ContentStyles.GetStyleIndex(style);
                this.AppendChildElement(flag);
            }
        }

        public override string PreviewString
        {
            get
            {
                return "Header:" + base.PreviewString;
            }
        }

        
    }

    /// <summary>
    ///文档页脚对象
    /// </summary>
    [Serializable()]
    [System.Xml.Serialization.XmlType("XTextFooter")]
    [System.Diagnostics.DebuggerDisplay("Footer :{ PreviewString }")]
    public class XTextDocumentFooterElement : DomDocumentContentElement
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XTextDocumentFooterElement()
        {
        }

        public override string PreviewString
        {
            get
            {
                return "Footer:" + base.PreviewString;
            }
        }
    }
}