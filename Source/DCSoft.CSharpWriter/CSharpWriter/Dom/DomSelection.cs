/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using DCSoft.CSharpWriter.Dom.Undo ;
using DCSoft.Drawing;
using System.Drawing;
using DCSoft.Common;
using System.Collections;
using System.Collections.Generic;
using DCSoft.CSharpWriter.RTF;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 文档区域对象
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("StartIndex={StartIndex},Length={Length}")]
    [System.Diagnostics.DebuggerTypeProxy(typeof(DCSoft.Common.ListDebugView))]
    public class DomSelection : System.Collections.IEnumerable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomSelection( DomDocumentContentElement dce)
        {
            _DocumentContent = dce;
        }

        private ContentRangeMode _Mode = ContentRangeMode.Content;

        public ContentRangeMode Mode
        {
            get { return _Mode; }
        }

        private DomDocumentContentElement _DocumentContent = null;

        public DomDocumentContentElement DocumentContent
        {
            get { return _DocumentContent; }
        }

        public DomDocument Document
        {
            get
            {
                return _DocumentContent.OwnerDocument;
            }
        }

        private DomContent Content
        {
            get
            {
                return _DocumentContent.Content;
            }
        }
         
        private DomElementList _ContentElements = new DomElementList();
        /// <summary>
        /// 内容元素列表
        /// </summary>
        public DomElementList ContentElements
        {
            get
            {
                return _ContentElements; 
            }
        }

        /// <summary>
        /// 区域中第一个文档内容元素
        /// </summary>
        public DomElement FirstElement
        {
            get
            {
                if (_ContentElements == null)
                {
                    return null;
                }
                else
                {
                    return _ContentElements.FirstElement;
                }
            }
        }

        /// <summary>
        /// 区域中最后一个文档内容元素
        /// </summary>
        public DomElement LastElement
        {
            get
            {
                if (_ContentElements == null)
                {
                    return null;
                }
                else
                {
                    return _ContentElements.LastElement;
                }
            }
        }


        /// <summary>
        /// 原始的起始位置
        /// </summary>
        private int _NativeStartIndex = 0;

        public int NativeStartIndex
        {
            get { return _NativeStartIndex; }
        }

        private int _NativeLength = 0;
        /// <summary>
        /// 原始的区域长度
        /// </summary>
        public int NativeLength
        {
            get { return _NativeLength; }
        }

        /// <summary>
        /// 实际的起始位置
        /// </summary>
        private int _StartIndex = 0;

        public int StartIndex
        {
            get
            {
                return _StartIndex; 
            }
        }

        private int _Length = 0;
        /// <summary>
        /// 实际的区域长度
        /// </summary>
        public int Length
        {
            get
            {
                return _Length; 
            }
        }

        public int AbsStartIndex
        {
            get
            {
                if( _Length >= 0 )
                {
                    return _StartIndex ;
                }
                else
                {
                    return _StartIndex + _Length ;
                }
            }
        }

        public int AbsEndIndex
        {
            get
            {
                if (_Length >= 0)
                {
                    return _StartIndex + _Length;
                }
                else
                {
                    return _StartIndex;
                }
            }
        }

        public int AbsLength
        {
            get
            {
                return Math.Abs(_Length);
            }
        }

        /// <summary>
        /// 判断元素是否包含在区域中
        /// </summary>
        /// <param name="element">元素对象</param>
        /// <returns>是否包含在区域中</returns>
        public bool Contains(DomElement element)
        {
             
            {
                return _ContentElements.Contains(element);
            }
        }
        private int _SelectionVersion = 0;
        /// <summary>
        /// 选择状态版本号，没修改一次选择状态则该版本号就会增加1
        /// </summary>
        public int SelectionVersion
        {
            get
            {
                return _SelectionVersion; 
            }
        }
         
        /// <summary>
        /// 最后一次设置状态时的文档内容版本号
        /// </summary>
        private int _ContentVersion = 0;
        /// <summary>
        /// 最后一次设置状态时的文档行尾标记值
        /// </summary>
        private bool _LineEndFlag = false;
        /// <summary>
        /// 判断是否需要刷新选择区域状态
        /// </summary>
        /// <param name="newStartIndex">新的选择区域位置</param>
        /// <param name="newLength">新的选择区域长度</param>
        /// <returns>是否需要刷新选择区域状态</returns>
        internal bool NeedRefresh(int newStartIndex, int newLength)
        {
            if (_NativeStartIndex == newStartIndex
                && _NativeLength == newLength
                && this.DocumentContent.OwnerDocument.ContentVersion == _ContentVersion
                && this.Content.LineEndFlag == _LineEndFlag)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 更新文内容选择状态
        /// </summary>
        /// <param name="startIndex">选择区域的起始位置</param>
        /// <param name="length">选择区域的包含文档内容元素的个数</param>
        /// <param name="raiseEvent">是否触发文档事件</param>
        /// <returns>成功的改变了选择区域状态</returns>
        internal bool Refresh(int startIndex, int length)
        {
            if (_NativeStartIndex == startIndex
                && _NativeLength == length
                && this.DocumentContent.OwnerDocument.ContentVersion == _ContentVersion
                && this.Content.LineEndFlag == _LineEndFlag)
            {
                return false;
            }
            if (startIndex == 0 || startIndex + length == 0)
            {
                System.Console.Write("");
            }
            if (startIndex < 0)
            {
                System.Console.WriteLine("");
                throw new ArgumentOutOfRangeException("startIndex=" + startIndex);
            }
            _SelectionVersion++;

            this.Document.EditorCurrentStyle = null;

            //if (startIndex == 872)
            //{
            //    System.Console.WriteLine("");
            //}

            // 备份原先被选择的元素的列表 
            DomElementList contentElementsBack = _ContentElements.Clone();
            _ContentElements = new DomElementList();

            
            _ContentVersion = this.DocumentContent.OwnerDocument.ContentVersion;

            if (startIndex == 0 && length == 0)
            {
                _NativeStartIndex = 0;
                _NativeLength = 0;
                _StartIndex = 0;
                _Length = 0;
                _LineEndFlag = this.Content.LineEndFlag;
                this._Mode = ContentRangeMode.Content;
                _ContentElements = new DomElementList();
               
                //return true;
            }
            else
            {
                _NativeStartIndex = startIndex;
                _NativeLength = length;
                _LineEndFlag = this.Content.LineEndFlag;

                DomContent content = this.Content;

                _Mode = ContentRangeMode.Content;
                // 选择区域经过的单元格列表
                DomElementList spanElements = new DomElementList();
               
                // 所经过的单元格个数
                int cellCount = 0;
                //XTextElementList spanCells = new XTextElementList();
                // 是否有不包含在表格单元格中的文档内容元素
                bool hasContentElement = false;

                int absStartIndex = length > 0 ? startIndex : startIndex + length;
                int absLength = Math.Abs(length);

                 

                int lengthFix = 1;
                if (content.LineEndFlag && length < 0)
                {
                    lengthFix = 0;
                }
                // 遍历所有的文档元素，查找所经过的单元格
                for (int iCount = 0; iCount < absLength + lengthFix; iCount++)
                {
                    if (absStartIndex + iCount >= content.Count)
                    {
                        break;
                        //System.Console.Write("");
                    }
                    DomElement element = content[absStartIndex + iCount];
                    if (element.Parent == this.DocumentContent)
                    {
                        spanElements.Add(element);
                        hasContentElement = true;
                    }
                    else
                    {
                        
                            spanElements.Add(element);
                            hasContentElement = true;
                        
                    }
                }//for

                // 首先判断内容模式
                _Mode = ContentRangeMode.Content;
                  
                    // 选择纯文档内容
                    _ContentElements = content.GetRange(absStartIndex, absLength);
                    _StartIndex = startIndex;
                    _Length = length;
                 
                if (length == 0 || _ContentElements.Count == 0)
                {
                    _StartIndex = startIndex;
                    _Length = length;
                }
                else
                {
                    if (length > 0)
                    {
                        _StartIndex = _ContentElements[0].ViewIndex;
                        _Length = _ContentElements.Count;
                        content.LineEndFlag = false;
                    }
                    else
                    {
                        _StartIndex = _ContentElements.LastElement.ViewIndex + 1;
                        _Length = 0 - _ContentElements.Count;
                         
                        {
                            System.Console.Write("");
                        }
                    }
                    _LineEndFlag = content.LineEndFlag;
                }
            }//if
             
            if (this.Mode == ContentRangeMode.Content || this.Mode == ContentRangeMode.Mixed)
            {
                // 让选择状态发生改变的文档内容元素用户界面无效，需要重新绘制
                if (contentElementsBack != null && contentElementsBack.Count > 0)
                {
                    foreach (DomElement element in contentElementsBack)
                    {
                        if (_ContentElements.Contains(element) == false)
                        {
                            element.InvalidateView();
                        }
                    }
                }
                if (_ContentElements.Count > 0)
                {
                    foreach (DomElement element in _ContentElements)
                    {
                        if (contentElementsBack == null
                            || contentElementsBack.Contains(element) == false)
                        {
                            element.InvalidateView();
                        }
                    }
                }
            }
            if (this.StartIndex == -1)
            {
                System.Console.WriteLine("");
            }
            return true;
        }

        private void MyAddRange(DomElementList list, DomElementList content)
        {
            foreach (DomElement element in content)
            {
                 
                {
                    if (list.Contains(element))
                    {
                        // 已经添加过内容了，立即退出函数
                        return;
                    }
                    list.Add(element);
                }
            }
        }

        /// <summary>
        /// 获得元素枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            if (_ContentElements == null)
            {
                return null;
            }
            else
            {
                return _ContentElements.GetEnumerator();
            }
        }


        /// <summary>
        /// 获得纯文本内容
        /// </summary>
        public string Text
        {
            get
            {
                if (_ContentElements == null)
                {
                    return "";
                }
                else
                {
                    System.Text.StringBuilder myStr = new System.Text.StringBuilder();
                    foreach (DomElement element in _ContentElements)
                    {
                        myStr.Append(element.ToPlaintString());
                    }
                    return myStr.ToString();
                }
            }
        }

        /// <summary>
        /// 获得表示内容的RTF文本
        /// </summary>
        public string RTFText
        {
            get
            {
                if (_Length == 0)
                {
                    return "";
                }

                DomDocument document = this._DocumentContent.OwnerDocument;
                System.IO.StringWriter writer = new System.IO.StringWriter();
                RTFContentWriter w = new RTFContentWriter();
                w.Open(writer);
                w.Document = document;
                w.CollectionDocumentsStyle();
                w.WriteStartDocument( document );
                if (this.Mode == ContentRangeMode.Content)
                {
                    w.IncludeSelectionOnly = false;
                    DomElementList list = document.CreateParagraphs(this);
                    if (list == null || list.Count == 0)
                    {
                        return "";
                    }
                    w.WriteItems(list);
                }
                else
                {
                    w.IncludeSelectionOnly = true;
                    this.DocumentContent.WriteRTF(w);
                }
                w.WriteEndDocument();
                w.Close();
                return writer.ToString();
            }
        }

        /// <summary>
        /// 设置段落样式
        /// </summary>
        /// <param name="newStyle"></param>
        /// <returns></returns>
        public DomElementList SetParagraphStyle(DocumentContentStyle newStyle)
        {
            DomDocument document = this._DocumentContent.OwnerDocument;
            Dictionary<DomElement, int> styleIndexs
                = new Dictionary<DomElement, int>();
            if (this.Document.Options.SecurityOptions.EnablePermission)
            {
                newStyle.DisableDefaultValue = true;
                newStyle.CreatorIndex = this.Document.UserHistories.CurrentIndex;
                newStyle.DeleterIndex = -1;
            }
            else
            {
                newStyle.DisableDefaultValue = true;
                newStyle.CreatorIndex = -1;
                newStyle.DeleterIndex = -1;
            }
            foreach (DomParagraphFlagElement p in this.ParagraphsEOFs)
            {
                if (document.DocumentControler.CanModify(p))
                {
                    DocumentContentStyle rs = (DocumentContentStyle)p.RuntimeStyle.Clone();
                    if (XDependencyObject.MergeValues(newStyle, rs, true) > 0)
                    {
                        rs.DefaultValuePropertyNames = newStyle.GetDefaultValuePropertyNames();
                        int newStyleIndex = document.ContentStyles.GetStyleIndex(rs);
                        if (newStyleIndex != p.StyleIndex)
                        {
                            styleIndexs[p] = newStyleIndex;
                        }
                    }
                }
            }//foreach
            if (styleIndexs.Count > 0)
            {
                DomElementList result = document.EditorSetParagraphStyle(styleIndexs, true);
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 设置文档元素的样式
        /// </summary>
        /// <param name="newStyle">新样式</param>
        /// <returns>是否修改了文档内容</returns>
        public bool SetElementStyle(DocumentContentStyle newStyle)
        {
            return SetElementStyle(newStyle, this._DocumentContent.OwnerDocument, this);
        }

        /// <summary>
        /// 设置多个元素的样式
        /// </summary>
        /// <param name="newStyle">新样式</param>
        /// <param name="document">文档对象</param>
        /// <param name="elements">要设置的元素列表</param>
        /// <returns>操作是否成功</returns>
        internal static bool SetElementStyle(
            DocumentContentStyle newStyle ,
            DomDocument document ,
            System.Collections.IEnumerable elements )
        {
            if (newStyle == null)
            {
                throw new ArgumentNullException("newStyle");
            }
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }
            if (document.Options.SecurityOptions.EnablePermission)
            {
                // 运行授权控制，向样式信息添加用户信息
                newStyle.DisableDefaultValue = false;
                newStyle.CreatorIndex = document.UserHistories.CurrentIndex;
                newStyle.DeleterIndex = -1;
            }
            else
            {
                // 去除授权控制相关信息
                newStyle.DisableDefaultValue = false;
                newStyle.CreatorIndex = -1;
                newStyle.DeleterIndex = -1;
            }
            Dictionary<DomElement, int> newStyleIndexs
                = new Dictionary<DomElement, int>();
            DomElementList parents = new DomElementList();
            DomElement lastParent = null;
            foreach (DomElement element in elements )
            {
                DomElement parent = element.Parent;
                //if (parent != lastParent)
                {
                    // 记录所有涉及到的父元素
                    lastParent = parent;
                    bool addParent = false;
                    //if (element is XTextFieldBorderElement)
                    //{
                    //    addParent = true;
                    //}
                    //else 
                     
                    if( addParent )
                    {
                        if (parents.Contains(element.Parent) == false )
                        {
                            parents.Add(element.Parent);
                        }
                    }
                }//if
                DocumentContentStyle rs = (DocumentContentStyle)element.RuntimeStyle.Clone();
                if (XDependencyObject.MergeValues(newStyle, rs, true) > 0)
                {
                    rs.DefaultValuePropertyNames = newStyle.GetDefaultValuePropertyNames();
                    int styleIndex = document.ContentStyles.GetStyleIndex(rs);
                    if (styleIndex != element.StyleIndex)
                    {
                        newStyleIndexs[element] = styleIndex;
                    }
                    if (element.ShadowElement != null && styleIndex != element.ShadowElement.StyleIndex)
                    {
                        newStyleIndexs[element.ShadowElement] = styleIndex;
                    }
                }
            }//foreach
            if (parents.Count > 0)
            {
                // 对涉及到的父元素设置样式
                foreach (DomElement element in parents)
                {
                    DocumentContentStyle rs = (DocumentContentStyle)element.RuntimeStyle.Clone();
                    if (XDependencyObject.MergeValues(newStyle, rs, true) > 0)
                    {
                        rs.DefaultValuePropertyNames = newStyle.GetDefaultValuePropertyNames();
                        int styleIndex = document.ContentStyles.GetStyleIndex(rs);
                        if (styleIndex != element.StyleIndex)
                        {
                            newStyleIndexs[element] = styleIndex;
                        }
                    }
                }
            }
            if (newStyleIndexs.Count > 0)
            {
                DomElementList result = document.EditorSetElementStyle(newStyleIndexs, true);
                if (result != null && result.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 获得区间包含的段落对象列表
        /// </summary>
        public DomElementList ParagraphsEOFs
        {
            get
            {
                DomElementList list = new DomElementList();
                if (this.Length != 0)
                {
                    foreach (DomElement element in _ContentElements )
                    {
                        if (element is DomParagraphFlagElement)
                        {
                            list.Add(element);
                        }
                    }
                    if ( ( _ContentElements.LastElement is DomParagraphFlagElement) == false )
                    {
                        list.Add(_ContentElements.LastElement.OwnerParagraphEOF);
                    }
                }
                else
                {
                    DomElement element = this.Content.SafeGet(this.StartIndex);
                    if (element == null)
                    {
                        System.Console.WriteLine("");
                    }
                    else
                    {
                        list.Add(element.OwnerParagraphEOF);
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// 根据内容创建一个新的文档对象,而且不包含已经被逻辑删除的内容.
        /// </summary>
        /// <returns>创建的文档对象</returns>
        public DomDocument CreateDocument()
        {
            DomDocument document = ( DomDocument ) this.Document.Clone(false);
            DomDocument sourceDocument = this.Document;
            DomContainerElement body = document.Body;
            // 寻找所有内容共同的文档容器元素
            DomElementList parents1 = WriterUtils.GetParentList(this.ContentElements.FirstElement);
            DomElementList parents2 = WriterUtils.GetParentList(this.ContentElements.LastElement);
            foreach (DomContainerElement parent in parents1)
            {
                if( parents2.Contains(parent ))
                {
                     
                    DomContentElement ce = parent.ContentElement;
                    CloneElements(parent, ref body);
                    break;
                }
            }
            
            //CloneElements(this.DocumentContent, ref body );

            // 删除用户操作历史记录信息
            foreach (DocumentContentStyle style in document.ContentStyles.Styles)
            {
                style.CreatorIndex = -1;
                style.DeleterIndex = -1;
            }
            document.UserHistories.Clear();

            // 删除没有引用的样式
            document.DeleteUselessStyle();
            document.EditorControl = null;
            document.DocumentControler = null;
            document.HighlightManager = null;
            document.EditorCurrentStyle = null;
            document.HoverElement = null;
            if (document.UndoList != null)
            {
                document.EndLogUndo();
                document.UndoList.Clear();
            }
            document.FixDomState();
            return document;
        }
         
        private int CloneElements(
            DomContainerElement sourceContainer,
            ref DomContainerElement descContainer)
        {
            int result = 0;
            foreach (DomElement element in sourceContainer.Elements)
            {
                if (element.Style.DeleterIndex >= 0)
                {
                    // 元素被逻辑删除了，无法复制
                    continue;
                }
                 
                else if (element is DomContainerElement)
                {
                    DomContainerElement c = ( DomContainerElement ) element;
                    DomContainerElement newC = null ;
                    int result2 = CloneElements(c, ref newC);
                    if (result2 == 0)
                    {
                        if( element is DomContainerElement )
                        {
                            DomContainerElement container = ( DomContainerElement ) element ;
                            if( c.Elements.Count == 0 )
                            {
                                // 有些特殊的容器元素没有标准意义上的子元素,例如设置了背景文本的没有实际
                                // 内容的输入域等,此时需要判断文档内容元素
                                DomElementList ces = new DomElementList();
                                container.AppendContent( ces , true );
                                foreach( DomElement ce in ces )
                                {
                                    if( this.ContentElements.Contains( ce ))
                                    {
                                        if( newC == null )
                                        { 
                                            // 发现有内容在包含区域中,则创建新的容器元素对象
                                            newC = ( DomContainerElement ) c.Clone( false );
                                            newC.OwnerLine = null;
                                            break;
                                        }
                                    }
                                }//foreach
                            }//if
                        }//if
                    }//if
                    if (newC != null)
                    {
                        if (descContainer == null)
                        {
                            result++;
                            descContainer = (DomContainerElement)sourceContainer.Clone(false);
                            descContainer.OwnerLine = null;
                        }
                        descContainer.AppendChildElement(newC);
                    }
                }
            }//foreach
             
            return result;
        }
    }

    /// <summary>
    /// 内容区域样式
    /// </summary>
    public enum ContentRangeMode
    {
        /// <summary>
        /// 文档内容
        /// </summary>
        Content,
        /// <summary>
        /// 纯表格单元格
        /// </summary>
        Cell,
        /// <summary>
        /// 混合模式，包括文档内容和表格单元格
        /// </summary>
        Mixed
    }
}