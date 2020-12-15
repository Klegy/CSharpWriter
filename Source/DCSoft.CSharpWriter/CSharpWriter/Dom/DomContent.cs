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
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using DCSoft.CSharpWriter.Controls;
using DCSoft.CSharpWriter.Security;
using DCSoft.CSharpWriter.Data;

namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 文档内容管理对象
	/// </summary>
    [System.Diagnostics.DebuggerDisplay("Count={ Count }")]
    [System.Diagnostics.DebuggerTypeProxy(typeof(DCSoft.Common.ListDebugView))]
    public class DomContent : DomElementList
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public DomContent()
		{
		}

        //private List<XTextSignElement> _SignElements = null;
        ///// <summary>
        ///// 文档签名元素列表
        ///// </summary>
        //[System.ComponentModel.Browsable( false )]
        //public List<XTextSignElement> SignElements
        //{
        //    get
        //    {
        //        if (_SignElements == null)
        //        {
        //            _SignElements = new List<XTextSignElement>();
        //            foreach (XTextElement element in this)
        //            {
        //                if (element is XTextSignElement)
        //                {
        //                    _SignElements.Add((XTextSignElement)element);
        //                }
        //            }
        //        }
        //        return _SignElements; 
        //    }
        //    set
        //    {
        //        _SignElements = value; 
        //    }
        //}



		/// <summary>
		/// 对象所属的文档对象
		/// </summary>
		internal DomDocument _OwnerDocument = null;
		/// <summary>
		/// 对象所属的文档对象
		/// </summary>
		public virtual DomDocument OwnerDocument
		{
			get
            {
                return _OwnerDocument ;
            }
		}

        internal DomDocumentContentElement _DocumentContentElement = null;
        /// <summary>
        /// 对象所属的文档区域对象
        /// </summary>
        public DomDocumentContentElement DocumentContentElement
        {
            get
            {
                return _DocumentContentElement; 
            }
        }

		private bool	_LineEndFlag		= false;
		/// <summary>
		/// 光标在行尾标记
		/// </summary>
		public bool LineEndFlag
		{
			get
            {
                return _LineEndFlag ;
            }
			set
            {
                _LineEndFlag = value;
                //if (bolLineEndFlag)
                //{
                //    System.Console.Write("");
                //}
            }
		}

        //private XTextSelection _VirtualSelection = null;
        ///// <summary>
        ///// 虚拟的选择区域
        ///// </summary>
        ///// <remarks>
        ///// 虚拟的选择区域是后台处理时使用的选择区域
        ///// </remarks>
        //public XTextSelection VirtualSelection
        //{
        //    get
        //    {
        //        return _VirtualSelection; 
        //    }
        //    set
        //    {
        //        _VirtualSelection = value; 
        //    }
        //}

        protected override void OnClear()
        {
            base.OnClear();
            _AutoClearSelection = true;
            _LineEndFlag = false;
            //_SignElements = null;
        }
         
        /// <summary>
        /// 判断指定的位置是否被锁定了,锁定区域不能新增、删除和修改样式。
        /// </summary>
        /// <param name="viewIndex">指定的位置</param>
        /// <returns>是否被锁定了</returns>
        public bool IsLockIndex(int viewIndex)
        {
            return false;
        }

        public DomSelection Selection
        {
            get
            {
                return this._DocumentContentElement.Selection;
            }
        }

        public int SelectionStartIndex
        {
            get
            {
                return this._DocumentContentElement.Selection.StartIndex;
            }
        }

        public int SelectionLength
        {
            get
            {
                return this._DocumentContentElement.Selection.Length;
            }
        }

        /// <summary>
        /// 修正区域范围，避免超出边界
        /// </summary>
        /// <param name="startIndex">区域起始序号</param>
        /// <param name="length">区域长度</param>
        internal void FixRange( ref  int startIndex  ,ref int length )
        {
            if (this.Count == 0)
            {
                startIndex = 0;
                length = 0;
            }
            else
            {
                int endIndex = startIndex + length;
                if (startIndex >= this.Count)
                {
                    startIndex = this.Count - 1;
                }
                if (startIndex < 0)
                {
                    startIndex = 0;
                }
                if (endIndex >= this.Count)
                {
                    endIndex = this.Count - 1;
                }
                if (endIndex < 0)
                {
                    endIndex = 0;
                }
                length = endIndex - startIndex;
            }
        }

        /// <summary>
        /// 获得当前插入点的信息
        /// </summary>
        /// <param name="container">插入点所在的容器对象</param>
        /// <param name="elementIndex">插入点所在的容器元素子元素列表中的序号</param>
        public void GetCurrentPositionInfo(out DomContainerElement container, out int elementIndex)
        {
            GetPositonInfo(
                this.Selection.StartIndex,
                out container,
                out elementIndex,
                this.LineEndFlag);
        }

        public int GetPosition(
            DomContainerElement container,
            int elementIndex,
            bool lineEndFlag)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            if (elementIndex <= 0)
            {
                elementIndex = container.FirstContentElement.ViewIndex ;
            }
            else if (elementIndex >= container.Elements.Count)
            {
                elementIndex = container.LastContentElement.ViewIndex;
            }
            else
            {
                DomElement element = container.Elements[elementIndex];
                if (element is DomParagraphFlagElement)
                {
                    elementIndex = this.IndexOf(element);
                }
                else
                {
                    elementIndex = element.FirstContentElement.ViewIndex;
                }
            }
            return elementIndex;
        }

        /// <summary>
        /// 根据指定的插入点的位置获得插入点信息
        /// </summary>
        /// <param name="contentIndex">插入点位置</param>
        /// <param name="container">插入点所在的容器元素对象</param>
        /// <param name="elementIndex">插入点在容器元素列表中的位置</param>
        /// <param name="lineEndFlag">行尾标记</param>
        public void GetPositonInfo( 
            int contentIndex ,
            out DomContainerElement container ,
            out int elementIndex ,
            bool lineEndFlag )
        {
            if (contentIndex < 0)
            {
                contentIndex = 0;
            }
            if (contentIndex >= this.Count )
            {
                contentIndex = this.Count -1;
            }
            if (contentIndex < 0 || contentIndex > this.Count)
            {
                throw new ArgumentOutOfRangeException("contentIndex=" + contentIndex);
            }
            if (contentIndex >= this.Count)
            {
                // 当前位置为最后一个文档元素之后。
                container = this.DocumentContentElement;
                elementIndex = this.DocumentContentElement.Elements.Count;
                return;
            }
            DomElement element = this[contentIndex];
            container = element.Parent;
            elementIndex = container.Elements.IndexOf(element);
             if (lineEndFlag)
            {
                if (contentIndex == 0)
                {
                    System.Console.Write("");
                }
                DomElement preElement = this[contentIndex - 1];
                container = preElement.Parent;
                 
                elementIndex = container.Elements.IndexOf(preElement) + 1;
            }
        }

        ///// <summary>
        ///// 文档中选定的文本起始点。
        ///// </summary>
        //protected int intSelectionStart = 0 ;
        ///// <summary>
        /////  获取或设置文档中选定的文本起始点。
        ///// </summary>
        //public int SelectionStart
        //{
        //    get{ return intSelectionStart ;}
        //    set{ intSelectionStart = value;}
        //}
        ///// <summary>
        /////  文档中选定的字符数。 
        ///// </summary>
        //protected int intSelectionLength = 0 ;
        ///// <summary>
        /////  获取或设置文档中选定的字符数。
        ///// </summary>
        //public int SelectionLength
        //{
        //    get{ return intSelectionLength ;}
        //    set{ intSelectionLength = value;}
        //}

        ///// <summary>
        ///// 选择区域的绝对开始位置
        ///// </summary>
        //public int AbsSelectStart
        //{
        //    get
        //    {
        //        SelectionRange range = new SelectionRange(
        //            this.intSelectionStart ,
        //            this.intSelectionLength ,
        //            this.Count );
        //        return range.AbsSelectionStart ;
        //    }
        //}

        ///// <summary>
        ///// 选择区域的绝对结束位置
        ///// </summary>
        //public int AbsSelectEnd
        //{
        //    get
        //    {
        //        SelectionRange range = new SelectionRange( 
        //            this.intSelectionStart ,
        //            this.intSelectionLength , 
        //            this.Count );
        //        return range.AbsSelectionEnd ;
        //    }
        //}

        ///// <summary>
        ///// 判断是否有元素选中
        ///// </summary>
        //public bool HasSelection
        //{
        //    get
        //    {
        //        return this.intSelectionLength != 0 ;
        //    }
        //}

        ///// <summary>
        ///// 判断指定的元素是否被选中
        ///// </summary>
        ///// <param name="element">元素对象</param>
        ///// <returns>是否选中</returns>
        //public bool IsSelected( XTextElement element )
        //{
        //    if( element == null )
        //        return false;
        //    if (element.intViewIndex >= 0)
        //    {
        //        return IsSelected(element.intViewIndex);
        //    }
        //    else
        //    {
        //        int index = this.IndexOf(element);
        //        element.intViewIndex = index;
        //        return IsSelected(index);
        //    }
        //}

        ///// <summary>
        ///// 判断指定序号处的元素是否被选中
        ///// </summary>
        ///// <param name="index">元素序号</param>
        ///// <returns>是否选中</returns>
        //public bool IsSelected(int index)
        //{
        //    if (index < 0 || index >= this.Count)
        //    {
        //        return false;
        //    }
        //    if (this.intSelectionLength == 0)
        //    {
        //        return false;
        //    }
        //    if (intSelectionLength > 0)
        //    {
        //        return index >= intSelectionStart
        //            && index < intSelectionStart + intSelectionLength;
        //    }
        //    else
        //    {
        //        return index >= intSelectionStart + intSelectionLength
        //            && index < intSelectionStart;
        //    }
        //}
		
		
		protected bool _AutoClearSelection = true ;
		/// <summary>
		/// 是否自动清除选择状态,若为True则插入点位置修改时会自动设置SelectionLength属性，否则会根据
		/// 旧的插入点的位置计算SelectionLength长度
		/// </summary>
		public bool AutoClearSelection
		{
			get
            {
                return _AutoClearSelection ;
            }
			set
            {
                _AutoClearSelection =value ;
            }
		}

		
//		public XTextElement GetElementAt( int x , int y )
//		{
//			if( myOwnerDocument != null && myOwnerDocument.Lines.Count > 0 )
//			{
//				int LastTop = -1 ;
//				foreach( XTextLine line in myOwnerDocument.Lines )
//				{
//					if( LastTop == -1 )
//						LastTop = line.Top ;
//					if( y >= LastTop && y < line.Top + line.Height )
//					{
//						x = x - line.Left - line.Margin ;
//						foreach( XTextElement element in line )
//						{
//							System.Drawing.Rectangle bounds = element.AbsBounds ;
//							if( x > element.Left && x <= element.Left + element.Width + element.WidthFix )
//								return element ;
//						}
//						break;
//					}
//					LastTop = line.Top + line.Height ;
//				}
//			}
//			return null;
//		}

		/// <summary>
		/// 获得插入点前最近的一个字符元素
		/// </summary>
		/// <returns>最近的字符元素,若未找到则返回空引用</returns>
		public DomCharElement GetPreChar()
		{
            for (int iCount = (this.SelectionStartIndex == 0 && this.Count > 1 ? 1 : this.SelectionStartIndex - 1);
                iCount >= 0; iCount--)
            {
                if (this[iCount] is DomCharElement)
                {
                    return (DomCharElement)this[iCount];
                }
            }
			return null;
		}



        /// <summary>
        /// 获得插入点前第一个单词的起始位置
        /// </summary>
        /// <returns></returns>
        public int GetPreWordIndex()
        {
            //intSelectStart = this.FixIndex( intSelectStart );
            int index = -1;
            DomContentLine myLine = this.CurrentLine;
            for (int iCount = this.SelectionStartIndex - 1; iCount >= 0; iCount--)
            {
                if (this[iCount] is DomCharElement)
                {
                    DomCharElement myChar = (DomCharElement)this[iCount];
                    if (char.IsLetter(myChar.CharValue) && myChar.OwnerLine == myLine)
                    {
                        index = iCount;
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
            return index;
        }// int GetPreWordIndex()

        /// <summary>
        /// 获得插入点前第一个单词的起始位置
        /// </summary>
        /// <param name="myElement">指定的元素对象</param>
        /// <returns></returns>
        public int GetPreWordIndex(DomElement myElement)
        {
            //intSelectStart = this.FixIndex( intSelectStart );
            int index = -1;
            if (myElement == null || this.Contains(myElement) == false)
                return -1;
            for (int iCount = this.IndexOf(myElement) - 1; iCount >= 0; iCount--)
            {
                if (this[iCount] is DomCharElement)
                {
                    if (char.IsLetter((this[iCount] as DomCharElement).CharValue))
                    {
                        index = iCount;
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
            }//for
            return index;
        }// int GetPreWordIndex()


        /// <summary>
        /// 获得插入点前的单词
        /// </summary>
        /// <returns>获得的单词，若不存在则返回空引用</returns>
        public string GetPreWord()
        {
            int index = this.GetPreWordIndex();
            System.Text.StringBuilder myStr = new System.Text.StringBuilder();
            DomCharElement myChar = null;
            if (index >= 0)
            {
                for (int iCount = index; iCount < this.SelectionStartIndex ; iCount++)
                {
                    myChar = this[iCount] as DomCharElement;
                    if (myChar != null)
                    {
                        if (char.IsLetter(myChar.CharValue))
                        {
                            myStr.Append(myChar.CharValue);
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
            }
            if (myStr.Length == 0)
            {
                return null;
            }
            else
            {
                return myStr.ToString();
            }
        }// string GetPreWord()

        /// <summary>
        /// 获得指定元素前的单词
        /// </summary>
        /// <param name="myElement">指定的元素对象</param>
        /// <returns>获得的单词，若不存在则返回空引用</returns>
        public string GetPreWord(DomElement myElement)
        {
            int index = this.GetPreWordIndex(myElement);
            System.Text.StringBuilder myStr = new System.Text.StringBuilder();
            DomCharElement myChar = null;
            if (index >= 0)
            {
                for (int iCount = index; iCount < this.Count; iCount++)
                {
                    myChar = this[iCount] as DomCharElement;
                    if (myChar != null)
                    {
                        if (char.IsLetter(myChar.CharValue))
                        {
                            myStr.Append(myChar.CharValue);
                        }
                        else
                        {
                            break;
                        }
                    }//if
                    else
                    {
                        break;
                    }
                }//for
            }
            if (myStr.Length == 0)
            {
                return null;
            }
            else
            {
                return myStr.ToString();
            }
        }// string GetPreWord()


        ///// <summary>
        ///// 获得所有被选中的元素列表
        ///// </summary>
        //public XTextElementList SelectedElements
        //{
        //    get
        //    {
        //        XTextElementList list = new XTextElementList();
        //        if( this.Selection.Length  != 0 )
        //        {
        //            int end = this.AbsSelectEnd ;
        //            for( int iCount = this.AbsSelectStart ; iCount <= end ; iCount ++ )
        //            {
        //                list.Add( this[ iCount ] );
        //            }
        //        }
        //        return list ;
        //    }
        //}
//
//		/// <summary>
//		/// 在当前插入点处插入若干个元素
//		/// </summary>
//		/// <param name="list">要插入的元素的列表</param>
//		public void InsertElements( XTextElementList list )
//		{
//			if( list == null || list.Count == 0 )
//				return ;
//			XTextElement element = this.CurrentElement ;
//			int index = 0 ;
//			if( element != null )
//				index = this.IndexOf( element );
//			if( index < 0 )
//				index = 0 ;
//			if( index > this.Count )
//				index = this.Count ;
//			foreach( XTextElement e in list )
//			{
//				e.OwnerDocument = this.myOwnerDocument ;
//				e.Parent = this ;
//			}
//			myElements.InsertRange( index , list );
//			this.UpdateAllElements();
//			RefreshContent( index );
//			int index2 = -1 ;
//			for( int iCount = list.Count - 1; iCount >= 0 ; iCount -- )
//			{
//				index2 = myContent.IndexOf( list[ iCount ] );
//				if( index2 >= 0 )
//				{
//					break;
//				}
//			}
//			if( index2 >= 0 )
//			{
//				myContent.AutoClearSelection = true ;
//				myContent.MoveSelectStart( index2 + 1 );
//			}
//		}

		private DomParagraphFlagElement[] GetPEOFs()
		{
			DomParagraphFlagElement pe1 = null;
			DomParagraphFlagElement pe2 = null;
            if (this.Selection.Length == 0)
            {
                return null;
            }
            DomElement e1 = this.Selection.FirstElement;
            DomElement e2 = this.Selection.LastElement;
			if( e1 != null && e2 != null )
			{
				pe1 = e1.OwnerParagraphEOF ;
				pe2 = e2.OwnerParagraphEOF ;
				if( pe1 != pe2 )
				{
					return new DomParagraphFlagElement[]{ pe1 ,pe2 } ;
				}
			}
			return null;
		}
	
		/// <summary>
		/// 删除当前元素前一个元素
		/// </summary>
        /// <param name="raiseEvent">是否触发事件</param>
		/// <returns>若删除成功,则返回新的插入点的位置,若操作失败则返回-1</returns>
        public int DeletePreElement( bool raiseEvent )
        {
            if (this.Count == 0)
            {
                return -1;
            }
            DomElement myElement = this.CurrentElement;
            if (myElement.ContentElement.PrivateContent.FirstElement
                == myElement)
            {
                // 若当前位置为文本块的第一个位置，则不能跨区域删除上一个文本块的最后一个文档元素
                return -1;
            }
            myElement = this.PreElement;
            if (myElement == null)
            {
                return -1;
            }
            DomContentElement ce = myElement.ContentElement;
            if (this.OwnerDocument.DocumentControler.CanDelete(myElement) == false)
            {
                // 元素不能删除
                return -1;
            }
            DomParagraphFlagElement pe1 = GetParagraphEOFElement(myElement);
            DomParagraphFlagElement pe2 = GetParagraphEOFElement(this.CurrentElement);
            DomContainerElement c = myElement.Parent;
            int index = ce.PrivateContent.IndexOf(myElement);// c.Elements.IndexOf(myElement);
            DomElement currentElementBack = this.CurrentElement;
            ContentChangedEventArgs cea = new ContentChangedEventArgs();
            if (InnerRemoveElement(c, myElement , raiseEvent , ref cea ))
            {
                this.LineEndFlag = false;
                _OwnerDocument.Modified = true;
                this.SetParagraphSettings(pe1, pe2);
                ce.UpdateContentElements(true);
                ce.RefreshPrivateContent(index);
                if (myElement is DomParagraphFlagElement)
                {
                    this.DocumentContentElement.SetSelection(this.Selection.StartIndex  - 1, 0);
                    //return this.Selection.StartIndex ;
                }
                else
                {
                    if (this.Contains(myElement))
                    {
                        // 要删除的元素还在文档中，说明是逻辑删除
                        int newIndex = this.IndexOf(myElement);
                        this.DocumentContentElement.SetSelection(newIndex, 0);
                    }
                    else if (currentElementBack != null && this.Contains( currentElementBack ))
                    {
                        int newIndex = this.IndexOf(currentElementBack);
                        this.DocumentContentElement.SetSelection(newIndex, 0);
                    }
                    else
                    {
                        this.DocumentContentElement.SetSelection(this.Selection.StartIndex - 1, 0);
                    }
                    //return this.Selection.StartIndex ;
                }
                c.RaiseBubbleOnContentChanged(cea);
                return this.Selection.StartIndex;
            }
            return -1;
        }

        /// <summary>
        /// 判断是否应该是逻辑删除元素
        /// </summary>
        /// <param name="element">文档元素对象</param>
        /// <returns>true:逻辑删除；false:物理删除。</returns>
        private bool IsLogicDelete(DomElement element)
        {
            DocumentContentStyle style = element.Style;
            DomDocument document = element.OwnerDocument;
            bool logicDelete = false;
            if (document.Options.SecurityOptions.EnablePermission
                && document.Options.SecurityOptions.EnableLogicDelete)
            {
                // 判断是否执行逻辑删除
                if (element.Style.CreatorIndex == document.UserHistories.CurrentIndex)
                {
                    // 若要删除的元素的创建者序号等于文档中当前的创建者序号
                    // 说明元素是当前用户在当前登录期间输入的，此时应该是物理
                    // 删除而不是逻辑删除。
                    logicDelete = false;
                }
                else
                {
                    logicDelete = true;
                    if (document.Options.SecurityOptions.RealDeleteOwnerContent)
                    {
                        // 物理删除曾经是自己输入的内容,用户采用ID进行判断
                        UserHistoryInfo info1 = document.UserHistories.GetInfo(style.CreatorIndex);
                        UserHistoryInfo info2 = document.UserHistories.CurrentInfo;
                        if (info1 != null && info2 != null)
                        {
                            if (info1.ID == info2.ID)
                            {
                                // 物理删除
                                logicDelete = false;
                            }
                        }
                    }
                }
            }
            return logicDelete;
        }

        /// <summary>
        /// 内部的删除一个文档元素
        /// </summary>
        /// <param name="c">容器对象</param>
        /// <param name="element">要删除的元素对象</param>
        /// <param name="raiseEvent">是否触发文档ContentChanging , ContentChanged事件</param>
        /// <returns>操作是否成功</returns>
		private bool InnerRemoveElement(
            DomContainerElement c ,
            DomElement element , 
            bool raiseEvent  ,
            ref ContentChangedEventArgs changedArgs )
		{
            int index = c.Elements.FastIndexOf( element );//.IndexOf( element );
            if (raiseEvent)
            {
                ContentChangingEventArgs args = new ContentChangingEventArgs();
                args.Document = this.OwnerDocument;
                args.Element = c;
                args.ElementIndex = index;
                args.DeletingElements = new DomElementList();
                args.DeletingElements.Add(element);
                c.RaiseBubbleOnContentChanging(args);
                if (args.Cancel)
                {
                    // 用户取消操作
                    return false;
                }
            }
            //// 判断是否是逻辑删除
            //bool logicDelete = IsLogicDelete( element );
    
            XTextUndoReplaceElements undo = null;
            bool deleteResult = false;

            if ( IsLogicDelete( element ) )
            {
                // 逻辑删除
                DocumentContentStyle style = (DocumentContentStyle)element.Style.Clone();
                style.DisableDefaultValue = true;
                style.DeleterIndex = this.OwnerDocument.UserHistories.CurrentIndex;
                int si = this.OwnerDocument.ContentStyles.GetStyleIndex(style);
                if (this.OwnerDocument.CanLogUndo)
                {
                    // 记录撤销信息
                    this.OwnerDocument.UndoList.AddStyleIndex(
                        element,
                        element.StyleIndex,
                        si);
                }
                element.StyleIndex = si;
                deleteResult = true;
            }
            else
            {
                // 物理删除
                if (this.OwnerDocument.CanLogUndo)
                {
                    // 记录撤销操作信息
                    DomElementList list = new DomElementList();
                    list.Add(element);
                    undo = new XTextUndoReplaceElements(c, index, list, null);
                    undo.Document = this.OwnerDocument;
                    undo.InGroup = true;
                }
                deleteResult = c.RemoveChild(element);
            }
            if( deleteResult )
			{
                // 删除相关的高亮度显示区域
                this.OwnerDocument.HighlightManager.Remove( element );
                // 更新文档元素的版本号
                c.UpdateContentVersion();
                if (undo != null && this.OwnerDocument.CanLogUndo )
                {
                    this.OwnerDocument.UndoList.Add(undo);
                }
                if (changedArgs != null )
                {
                    // 触发事件
                    changedArgs.Document = this.OwnerDocument;
                    changedArgs.Element = c;
                    changedArgs.ElementIndex = index;
                    changedArgs.DeletedElements = new DomElementList();
                    changedArgs.DeletedElements.Add(element);
                }
				return true ;
			}
			return false;
		}

		/// <summary>
		/// 删除元素
		/// </summary>
        /// <param name="raiseEvent">是否触发事件</param>
		/// <returns>若删除成功,则返回新的插入点的位置,若操作是否则返回-1</returns>
        public int DeleteCurrentElement( bool raiseEvent )
        {
            if (this.Count == 0)
            {
                return -1;
            }
            DomElement myElement = this.CurrentElement;
            if (myElement == null)
            {
                return -1;
            }
            DomContentElement ce = myElement.ContentElement;
            //XTextDocumentContentElement dce = myElement.DocumentContentElement;
            if (this.OwnerDocument.DocumentControler.CanDelete(myElement) == false)
            {
                // 元素不能删除
                return -1;
            }
            if (ce.PrivateContent.LastElement == myElement)
            {
                // 文本块区域中最后一个段落符号不能删除
                return -1;
            }
            DomParagraphFlagElement cpe = myElement.OwnerParagraphEOF;
            DomContainerElement c = myElement.Parent;
            int index = ce.PrivateContent.IndexOf(myElement);
            DomElement elementBack = this.GetNextElement( myElement );
            if (elementBack == null)
            {
                elementBack = this.LastElement;
            }
            ContentChangedEventArgs cea = new ContentChangedEventArgs();
            if (InnerRemoveElement(c, myElement , raiseEvent , ref cea ))
            {
                this.LineEndFlag = false;
                DomElement e2 = this.SafeGet(this.Selection.StartIndex + 1);
                if (e2 != null)
                {
                    SetParagraphSettings(cpe, e2.OwnerParagraphEOF);
                }
                _OwnerDocument.Modified = true;
                ce.UpdateContentElements(true);
                ce.RefreshPrivateContent(index);
                c.RaiseBubbleOnContentChanged(cea);
                if (elementBack != null && this.Contains(elementBack))
                {
                    this.DocumentContentElement.SetSelection(this.IndexOf(elementBack), 0);
                }
                else
                {
                    this.DocumentContentElement.SetSelection(this.Selection.StartIndex, 0);
                }
                return this.Selection.StartIndex;
            }//if
            return -1;
        }

		private void SetParagraphSettings(
            DomParagraphFlagElement source ,
            DomParagraphFlagElement desc )
		{
			if( source == desc || source == null || desc == null )
				return ;
			if( this.OwnerDocument.CanLogUndo )
			{
                this.OwnerDocument.UndoList.AddStyleIndex(desc, desc.StyleIndex, source.StyleIndex);
			}
            desc.StyleIndex = source.StyleIndex;

			//source.CopyAttributesTo( desc );
		}

        /// <summary>
        /// 判断能否删除被选中的元素
        /// </summary>
        public bool CanDeleteSelection
        {
            get
            {
                if (this.Count == 0)
                {
                    return false;
                }
                if (this.SelectionLength == 0)
                {
                    return false;
                }
                DocumentControler controler = this.OwnerDocument.DocumentControler;
                foreach (DomElement element in this.Selection.ContentElements)
                {
                    if (controler.CanDelete(element) == false )
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 删除文档树状结构
        /// </summary>
        /// <param name="rootElement">要删除的根节点</param>
        /// <param name="startIndexs">用于记录容器元素刷新其内容的起始序号的字典</param>
        /// <param name="detect">检测是否可以执行删除元素操作,但不真的进行删除</param>
        /// <param name="fastMode">快速模式，仅对DOM树进行最基本的处理，不更新其他信息.</param>
        /// <returns>删除的文档元素个数</returns>
        private int InnerDeleteSelectionDomTree(
            DomContainerElement rootElement ,
            Dictionary<DomContentElement , int > startIndexs ,
            bool detect ,
            DomSelection specifySelection ,
            List<ContentChangedEventArgs> changedArgs )
        {
            DomContentElement ce = rootElement.ContentElement;
            if (rootElement is DomContentElement)
            {
                ce = (DomContentElement)rootElement;
            }
            DomElementList pce = ce.PrivateContent;
            DomElementList selectElements = specifySelection.ContentElements;
            DocumentControler controler = this.OwnerDocument.DocumentControler;
            // 获得开始删除和结束删除的区域的标记元素列表
            DomElementList flagElements = new DomElementList();
            // 首先获得所有子元素的选择状态
            SelectionPartialType[] selectionTypes = new SelectionPartialType[rootElement.Elements.Count];
            int startIndex = 0 ;
            DomElement element2 = selectElements[0];
            while (element2 != null)
            {
                if (element2.Parent == rootElement)
                {
                    startIndex = rootElement.Elements.IndexOf(element2);
                    if (startIndex < 0)
                    {
                        startIndex = 0;
                    }
                    break;
                }
                element2 = element2.Parent;
            }
            
            int endIndex = rootElement.Elements.Count - 1;
            element2 = selectElements.LastElement;
            while (element2 != null)
            {
                if (element2.Parent == rootElement)
                {
                    endIndex = rootElement.Elements.IndexOf(element2);
                    if (endIndex < 0)
                    {
                        endIndex = rootElement.Elements.Count - 1;
                    }
                    break;
                }
                element2 = element2.Parent;
            }

            for (int iCount = startIndex ; iCount <= endIndex ; iCount++)
            {
                DomElement element = rootElement.Elements[iCount];
                if (controler.CanDelete(element))
                {
                    if (selectElements.Contains(element))
                    {
                        selectionTypes[iCount] = SelectionPartialType.Both;
                    }
                    else if (element is DomContainerElement)
                    {
                        selectionTypes[iCount] = GetSelectionPartialType((DomContainerElement)element);
                    }
                    else
                    {
                        selectionTypes[iCount] = SelectionPartialType.None;
                    }
                    if (detect)
                    {
                        if (selectionTypes[iCount] == SelectionPartialType.Both
                            || selectionTypes[iCount] == SelectionPartialType.Partial)
                        {
                            // 仅仅做检测
                            return 1;
                        }
                    }
                }
                else
                {
                    selectionTypes[iCount] = SelectionPartialType.None;
                }
            }//for
            
            if (detect)
            {
                return 0;
            }

            // 修改所有选择状态为NoContent的对象
            for (int iCount = startIndex ; iCount <= endIndex ; iCount++)
            {
                if (selectionTypes[iCount] == SelectionPartialType.NoContent)
                {
                    bool preFlag = false;
                    // 向前搜索选中状态
                    SelectionPartialType preType = SelectionPartialType.None;
                    for (int iCount2 = iCount - 1; iCount2 >= endIndex ; iCount2--)
                    {
                        if (selectionTypes[iCount2] != SelectionPartialType.NoContent)
                        {
                            preType = selectionTypes[iCount2];
                            preFlag = true;
                            break;
                        }
                    }
                    // 向后搜索选中状态
                    bool nextFlag = false;
                    SelectionPartialType nextType = SelectionPartialType.None ;
                    for (int iCount2 = iCount + 1; iCount2 <= endIndex ; iCount2++)
                    {
                        if (selectionTypes[iCount2] != SelectionPartialType.NoContent)
                        {
                            nextType = selectionTypes[iCount2];
                            nextFlag = true;
                            break;
                        }
                    }
                    if (preType != SelectionPartialType.None 
                        && nextType != SelectionPartialType.None)
                    {
                        // 前后都处于选中状态,则本元素也被选中
                        selectionTypes[iCount] = SelectionPartialType.Both;
                    }
                    if (preFlag == false)
                    {
                        // 向前没找到则用后置状态
                        selectionTypes[iCount] = nextType;
                    }
                    else if (nextFlag == false)
                    {
                        // 向后没找到则使用前置状态
                        selectionTypes[iCount] = preType;
                    }
                    else
                    {
                        // 否则设置为不选中
                        selectionTypes[iCount] = SelectionPartialType.None;
                    }
                }
            }//for

            int result = 0;

            int startRefreshIndex = pce.Count;

            DomElement firstDeleteElement = null;
            DomElement lastDeleteElement = null;
            List<XTextUndoReplaceElements> undos = new List<XTextUndoReplaceElements>();
            Dictionary<DomElement, SelectionPartialType> selectionTypesTable 
                = new Dictionary<DomElement, SelectionPartialType>();
            for (int iCount = startIndex ; iCount <= endIndex ; iCount++)
            {
                selectionTypesTable[rootElement.Elements[iCount]] = selectionTypes[iCount];
            }
            
            // 允许执行逻辑删除
            bool useLogicDelete = this.OwnerDocument.Options.SecurityOptions.EnablePermission 
                && this.OwnerDocument.Options.SecurityOptions.EnableLogicDelete;
            // 上一个文档元素使用的用户内部编号。
            int lastCreatorIndex = -1;
            // 当前用户内部编号。
            int currentUserIndex = this.OwnerDocument.UserHistories.CurrentIndex ;

            for (int iCount = startIndex; 
                iCount < rootElement.Elements.Count && iCount <= endIndex ;
                iCount++)
            {
                DomElement element = rootElement.Elements[iCount];
                if (selectionTypesTable[element] == SelectionPartialType.Both)
                {
                    lastDeleteElement = rootElement.Elements[iCount];
                    if (firstDeleteElement == null)
                    {
                        firstDeleteElement = lastDeleteElement;
                    }
                    int index = pce.IndexOf(firstDeleteElement.FirstContentElement );
                    if (index >= 0 && startRefreshIndex > index)
                    {
                        startRefreshIndex = index; 
                    }
                    if (useLogicDelete)
                    {
                        int creatorIndex = lastDeleteElement.FirstContentElement.Style.CreatorIndex;
                        if (creatorIndex != lastCreatorIndex )
                        {
                            //// 元素创建者编号发生了改变
                            //if (creatorIndex != currentUserIndex && lastCreatorIndex != currentUserIndex)
                            //{
                            //    // 当元素的创建者编号等于当前创建者编号则进行物理删除，否则进行逻辑删除。
                            //    // 当两者的元素创建者编号都不等于当前创建者编号，都是进行逻辑删除
                            //    // 因此无需后续判断
                            //    continue;
                            //}

                            DomElement preElement = rootElement.Elements.GetPreElement(lastDeleteElement);
                            int deleted = DeleteElements(
                                rootElement, 
                                firstDeleteElement,
                                preElement , 
                                true ,
                                changedArgs );
                            result = result + Math.Abs( deleted );
                            if ( deleted > 0 )
                            {
                                endIndex = endIndex - deleted;
                                iCount = iCount - deleted;
                            }
                            lastDeleteElement = rootElement.Elements[iCount];
                            firstDeleteElement = lastDeleteElement;
                            lastCreatorIndex = creatorIndex;

                                                        //if (endNextElement != null)
                            //{
                            //    iCount = rootElement.Elements.IndexOf(endNextElement) - 1 ;
                            //}
                        }
                    }
                }
                else
                {
                    if (firstDeleteElement != null)
                    {
                        // 删除已识别的连片的处于全选状态的元素
                        int deleted = DeleteElements(
                            rootElement,
                            firstDeleteElement,
                            lastDeleteElement,
                            true ,
                            changedArgs );
                        if (deleted > 0)
                        {
                            result = result + deleted;
                            endIndex = endIndex - deleted;
                            iCount = iCount - deleted;
                        }
                        firstDeleteElement = null;
                        lastDeleteElement = null;

                        //iCount = rootElement.Elements.IndexOf(element);
                    }
                    if (selectionTypesTable[element] == SelectionPartialType.Partial)
                    {
                        // 深入子节点进行部分内容的删除
                        result = result + InnerDeleteSelectionDomTree(
                            ( DomContainerElement )element ,
                            startIndexs ,
                            detect ,
                            specifySelection,
                            changedArgs);
                    }
                }
            }//for
            if (firstDeleteElement != null)
            {
                int index = pce.IndexOf(firstDeleteElement);
                if (index >= 0 && startRefreshIndex > index)
                {
                    startRefreshIndex = index;
                }
                int deleted = DeleteElements(
                    rootElement,
                    firstDeleteElement,
                    lastDeleteElement,
                    true ,
                    changedArgs );
                result = result + Math.Abs( deleted );
            }
            if (startIndexs.ContainsKey(ce))
            {
                startIndexs[ce] = Math.Min(startIndexs[ce], startRefreshIndex);
            }
            else
            {
                startIndexs[ce] = startRefreshIndex;
            }
            return result ;
        }

        /// <summary>
        /// 删除元素
        /// </summary>
        /// <param name="container">容器元素</param>
        /// <param name="firstDeleteElement">删除区域的开始元素</param>
        /// <param name="lastDeleteElement">删除区域的结束元素</param>
        /// <param name="raiseEvent">是否触发事件</param>
        /// <param name="logicDelete">是否是逻辑删除</param>
        /// <returns>删除的元素个数,若等于0则没有删除任何元素，若大于0则物理删除了元素，若小于0则逻辑删除了元素</returns>
        private int DeleteElements(
            DomContainerElement container,
            DomElement firstDeleteElement,
            DomElement lastDeleteElement ,
            bool raiseEvent ,
            List<ContentChangedEventArgs> changedArgs )
        {
            try
            {
                int firstDeleteIndex = container.Elements.IndexOf(firstDeleteElement);
                int lastDeleteIndex = container.Elements.IndexOf(lastDeleteElement);
                DomParagraphFlagElement p1 = GetParagraphEOFElement(firstDeleteElement);
                DomParagraphFlagElement p2 = null;
                 
                if (container.Elements.LastElement != lastDeleteElement)
                {
                    p2 = GetParagraphEOFElement(container.Elements.GetNextElement(lastDeleteElement));
                }

                //container.Elements.BeginFastRemove();
                
                // 要删除的元素列表
                DomElementList deletedElements = container.Elements.GetRange(
                        firstDeleteIndex,
                        lastDeleteIndex - firstDeleteIndex + 1);
                if (raiseEvent)
                {
                    // 触发事件
                    ContentChangingEventArgs args = new ContentChangingEventArgs();
                    args.Document = this.OwnerDocument;
                    args.Element = container;
                    args.ElementIndex = firstDeleteIndex;
                    args.DeletingElements = deletedElements;
                    container.RaiseBubbleOnContentChanging(args);
                    if (args.Cancel)
                    {
                        // 用户取消操作
                        return 0 ;
                    }
                }
                bool logicDelete = IsLogicDelete( container.Elements[firstDeleteIndex]);
                //// 批量删除元素
                //if ( this.OwnerDocument.Options.SecurityOptions.EnablePermission 
                //    && this.OwnerDocument.Options.SecurityOptions.EnableLogicDelete )
                //{
                //    // 判断是否执行逻辑删除
                //    XTextElement element = container.Elements[firstDeleteIndex];
                //    if (element.Style.CreatorIndex == this.OwnerDocument.UserHistories.CurrentIndex)
                //    {
                //        // 若要删除的元素的创建者序号等于文档中当前的创建者序号
                //        // 说明元素是当前用户在当前登录期间输入的，此时应该是物理
                //        // 删除而不是逻辑删除。
                //        logicDelete = false;
                //    }
                //    else
                //    {
                //        logicDelete = true;
                //    }
                //}
                if( logicDelete )
                {
                    // 逻辑删除
                    for (int iCount = firstDeleteIndex; iCount <= lastDeleteIndex; iCount++)
                    {
                        DomElement element = container.Elements[iCount];
                        DocumentContentStyle style = ( DocumentContentStyle ) element.Style.Clone();
                        style.DisableDefaultValue = true;
                        style.DeleterIndex = this.OwnerDocument.UserHistories.CurrentIndex;
                        int si = this.OwnerDocument.ContentStyles.GetStyleIndex(style);
                        if (this.OwnerDocument.CanLogUndo)
                        {
                            // 记录撤销信息
                            this.OwnerDocument.UndoList.AddStyleIndex(
                                element,
                                element.StyleIndex,
                                si);
                        }
                        element.StyleIndex = si;
                    }//for
                }
                else
                {
                    // 物理删除
                    if (this.OwnerDocument.CanLogUndo)
                    {
                        // 记录撤销信息
                        this.OwnerDocument.UndoList.AddRemoveElements(
                            container,
                            container.Elements.IndexOf(firstDeleteElement),
                            deletedElements);
                    }
                    container.Elements.RemoveRange(
                        firstDeleteIndex,
                        lastDeleteIndex - firstDeleteIndex + 1);
                }
                // 删除相关的高亮度显示区域
                foreach (DomElement element in deletedElements)
                {
                    if ( (element is DomCharElement )== false )
                    {
                        this.OwnerDocument.HighlightManager.Remove( element );
                    }
                }
                // 更新容器元素的内容版本号
                container.UpdateContentVersion();

                // 设置删除的元素所在的文本行状态无效
                foreach (DomElement re in deletedElements)
                {
                    if (re.OwnerLine != null)
                    {
                        re.OwnerLine.InvalidateState = true;
                    }
                    else
                    {
                        // 若元素没有所属文本行,则该元素本身没有参与文本排版
                        // 此时检测其包含的文本文档元素所在的文本行无效.
                        if (re is DomContainerElement)
                        {
                            DomContainerElement c2 = (DomContainerElement)re;
                            DomElementList ces = new DomElementList();
                            c2.AppendContent(ces , false );
                            if (ces.Count > 0)
                            {
                                foreach (DomElement ce in ces)
                                {
                                    if (ce.OwnerLine != null)
                                    {
                                        ce.OwnerLine.InvalidateState = true;
                                    }
                                }
                            }
                        }
                    }
                }
                if (raiseEvent)
                {
                    // 触发事件
                    ContentChangedEventArgs args = new ContentChangedEventArgs();
                    args.Document = this.OwnerDocument;
                    args.Element = container;
                    args.ElementIndex = firstDeleteIndex;
                    args.DeletedElements = deletedElements;
                    changedArgs.Add(args);
                    //container.RaiseBubbleOnContentChanged(args);
                }
                if (p1 != p2 && p2 != null)
                {
                    SetParagraphSettings(p1, p2);
                }
                if (logicDelete)
                {
                    return 0 - deletedElements.Count;
                }
                else
                {
                    return deletedElements.Count;
                }
            }//try
            finally
            {
                //container.Elements.EndFastRemove();
            }
        }

        /// <summary>
        /// 内容选择状态
        /// </summary>
        private enum SelectionPartialType
        {
            /// <summary>
            /// 内容全部选中
            /// </summary>
            Both ,
            /// <summary>
            /// 部分选中
            /// </summary>
            Partial,
            /// <summary>
            /// 没有任何内容被选中
            /// </summary>
            None,
            /// <summary>
            /// 元素没有任何内容,因此等同于周围的选中状态
            /// </summary>
            NoContent 
        }

        /// <summary>
        /// 判断指定容器的元素全部被选中
        /// </summary>
        /// <param name="rootElement">容器元素</param>
        /// <returns>内容是否被全部选中</returns>
        private SelectionPartialType GetSelectionPartialType(DomContainerElement rootElement)
        {
            DomElementList cc = new DomElementList();
            rootElement.AppendContent(cc, true);
            if (cc.Count == 0)
            {
                // 没有任何内容,则
                return SelectionPartialType.NoContent;
            }
            bool include = false;
            bool exclude = false;
            foreach (DomElement element in cc)
            {
                if (this.Selection.ContentElements.Contains(element))
                {
                    include = true;
                }
                else
                {
                    exclude = true;
                }
                if (include && exclude)
                {
                    break;
                }
            }
            SelectionPartialType result = SelectionPartialType.None;
            if (include && exclude )
            {
                // 既存在被选中的又存在没有选中的元素
                result = SelectionPartialType.Partial;
            }
            else if (include && exclude == false)
            {
                result = SelectionPartialType.Both;
                 
            }
            else if (include == false && exclude)
            {
                result = SelectionPartialType.None;
            }
            return result;
        }

        /// <summary>
        /// 已删除文档树结构的方式来删除选中的元素
        /// </summary>
        /// <param name="raiseEvent">是否触发文档ContentChanging , ContentChanged事件</param>
        /// <param name="detect">检测是否可以执行删除元素操作,但不真的进行删除</param>
        /// <param name="fastMode">快速模式，不更新用户界面，不更新内容元素列表</param>
        /// <returns>删除的文档元素对象个数</returns>
        public int DeleteSelection(
            bool raiseEvent,
            bool detect,
            bool fastMode )
        {
            return DeleteSelection(raiseEvent, detect, fastMode, null);
        } 

        /// <summary>
        /// 已删除文档树结构的方式来删除选中的元素
        /// </summary>
        /// <param name="raiseEvent">是否触发文档ContentChanging , ContentChanged事件</param>
        /// <param name="detect">检测是否可以执行删除元素操作,但不真的进行删除</param>
        /// <param name="fastMode">快速模式，不更新用户界面，不更新内容元素列表</param>
        /// <returns>删除的文档元素对象个数</returns>
        public int DeleteSelection(
            bool raiseEvent ,
            bool detect ,
            bool fastMode ,
            DomSelection specifySelection )
        {
            if (this.Count == 0)
            {
                //本对象无任何元素
                return -1;
            }
            if (specifySelection == null)
            {
                specifySelection = this.Selection;
            }
            if ( specifySelection.Length == 0)
            {
                // 没有包括任何元素
                return -1;
            }
            
            DomContainerElement rootElement = null;
            DomElementList parents1 = WriterUtils.GetParentList(
                specifySelection.ContentElements.FirstElement);
            DomElementList parents2 = WriterUtils.GetParentList(
                specifySelection.ContentElements.LastElement);
            for (int iCount = 0; iCount <= parents1.Count; iCount++)
            {
                if (parents2.Contains(parents1[iCount]))
                {
                    rootElement = ( DomContainerElement ) parents1[iCount];
                    
                    break;
                }
            }

            if ((rootElement is DomContentElement) == false
                && rootElement.Parent != null )
            {
                if (GetSelectionPartialType(rootElement) == SelectionPartialType.Both)
                {
                    rootElement = rootElement.Parent;
                }
            }
            Dictionary<DomContentElement , int > startIndexs 
                = new Dictionary<DomContentElement,int>();
            int selectionIndex = this.IndexOf(specifySelection.ContentElements[0]);
            DomElement selectionStartElement = this.SafeGet(this.Selection.AbsEndIndex );
            if (selectionStartElement == null)
            {
                selectionStartElement = this.LastElement;
            }
            List<ContentChangedEventArgs> changedArgs = new List<ContentChangedEventArgs>();
            int result = InnerDeleteSelectionDomTree(
                rootElement,
                startIndexs ,
                detect ,
                specifySelection ,
                changedArgs );
            if (detect)
            {
                // 仅仅做检测,不进行后续处理
                return result;
            }
            
            if (result > 0 && fastMode == false)
            {
                this.LineEndFlag = false;
                _OwnerDocument.Modified = true;
                bool refreshPage = false;
                foreach (DomContentElement ce in startIndexs.Keys)
                {
                    int index = startIndexs[ce];
                    ce.UpdateContentElements(false);
                    ce.RefreshPrivateContent(index, -1, true);
                    if (ce._NeedRefreshPage)
                    {
                        refreshPage = true;
                    }
                }//foreach
                this.DocumentContentElement.UpdateContentElements(false);
                this.DocumentContentElement.FillContent();
                if (changedArgs.Count > 0)
                {
                    // 集中触发文档内容发生改变事件
                    for (int iCount = changedArgs.Count - 1; iCount >= 0; iCount--)
                    {
                        ContentChangedEventArgs args = changedArgs[iCount];
                        DomContainerElement c = args.Element as DomContainerElement;
                        if (c != null)
                        {
                            c.RaiseBubbleOnContentChanged(args);
                        }
                    }//for
                }
                if (refreshPage)
                {
                    this.OwnerDocument.PageRefreshed = false;
                    this.OwnerDocument.RefreshPages();

                    if (this.OwnerDocument.EditorControl != null)
                    {
                        this.OwnerDocument.EditorControl.UpdatePages();
                        this.OwnerDocument.EditorControl.UpdateTextCaret();
                        this.OwnerDocument.EditorControl.Invalidate();
                    }
                }
                else
                {
                    if (this.OwnerDocument != null && this.OwnerDocument.EditorControl != null)
                    {
                        this.OwnerDocument.EditorControl.UpdateTextCaret();
                        this.OwnerDocument.EditorControl.Update();
                    }
                }
                if (specifySelection == this.Selection)
                {
                    if (selectionStartElement != null && this.Contains(selectionStartElement))
                    {
                        this.SetSelection(this.IndexOf(selectionStartElement), 0);
                    }
                    else
                    {
                        this.SetSelection(selectionIndex, 0);
                    }
                }
            }
            return result;
            //return 0;
        }

		/// <summary>
		/// 删除选中的元素(旧的删除功能函数)
		/// </summary>
        /// <param name="raiseEvent">是否触发文档ContentChanging , ContentChanged事件</param>
		/// <returns>若删除成功则返回新插入点的位置,若操作失败则返回-1</returns>
        public int DeleteSelectionOld(bool raiseEvent)
		{
            if (this.Count == 0)
            {
                //本对象无任何元素
                return -1;
            }
            if (this.SelectionLength == 0)
            {
                // 没有包括任何元素
                return -1;
            }
            
            DocumentControler controler = this.OwnerDocument.DocumentControler;
            // 查找第一个要删除的元素和最后一个要删除的元素
            DomElement firstDeleteElement = null;
            DomElement lastDeleteElement = null;
            DomContainerElement container = null;
            // 此处根据元素的能否删除的特性对选择区域中的可删除的元素区域进行分离
            // 该列表中偶数序号的元素是每一个连续的可删除区域的第一个元素，奇数序号的元素是可删除区域的最后一个元素。
            DomElementList flagElements = new DomElementList();
            foreach (DomElement element in this.Selection.ContentElements )
            {
                if (controler.CanDelete(element))
                {
                    if (firstDeleteElement == null
                        || element.Parent != container )
                    {
                        firstDeleteElement = element;
                        container = element.Parent;
                    }
                    lastDeleteElement = element;
                }
                else
                {
                    if (firstDeleteElement != null)
                    {
                        if (firstDeleteElement != null)
                        {
                            flagElements.Add(firstDeleteElement);
                            flagElements.Add(lastDeleteElement);
                        }
                        firstDeleteElement = null;
                        lastDeleteElement = null;
                    }
                }
            }//foreach
            if (firstDeleteElement != null)
            {
                flagElements.Add(firstDeleteElement);
                flagElements.Add(lastDeleteElement);
            }
            if ( flagElements.Count == 0 )
            {
                // 没有任何元素可以删除，则退出
                return -1;
            }

            int selectionIndex = this.IndexOf(flagElements[0]);
            bool bolChanged = false ;
            Dictionary<DomContentElement, int> startIndexs 
                = new Dictionary<DomContentElement, int>();
            for (int iCount = 0; iCount < flagElements.Count; iCount += 2)
            {
                firstDeleteElement = flagElements[iCount];
                lastDeleteElement = flagElements[iCount + 1];
                DomContentElement ce = firstDeleteElement.ContentElement;
                int index = ce.PrivateContent.IndexOf( firstDeleteElement );
                // 获得最前的内容编号
                if (startIndexs.ContainsKey(ce))
                {
                    if (startIndexs[ce] > index)
                    {
                        startIndexs[ce] = index;
                    }
                }
                else
                {
                    startIndexs[ce] = index;
                }
                container = firstDeleteElement.Parent;
                try
                {
                    int firstDeleteIndex = container.Elements.IndexOf(firstDeleteElement);
                    int lastDeleteIndex = container.Elements.IndexOf(lastDeleteElement);
                    DomParagraphFlagElement p1 = GetParagraphEOFElement(firstDeleteElement);
                    DomParagraphFlagElement p2 = null;

                    DomElementList removeElements = container.Elements.GetRange(
                        firstDeleteIndex ,
                        lastDeleteIndex - firstDeleteIndex + 1 );

                    if (container.Elements.LastElement != lastDeleteElement)
                    {
                        p2 = GetParagraphEOFElement( container.Elements.GetNextElement( lastDeleteElement ));
                    }

                    //container.Elements.BeginFastRemove();
                    if (this.OwnerDocument.CanLogUndo)
                    {
                        this.OwnerDocument.UndoList.AddRemoveElements(
                            container,
                            container.Elements.IndexOf(firstDeleteElement),
                            removeElements);
                    }
                    // 要删除的元素列表
                    DomElementList deletedElements = container.Elements.GetRange(
                            firstDeleteIndex,
                            lastDeleteIndex - firstDeleteIndex + 1);
                    if (raiseEvent)
                    {
                        // 触发事件
                        ContentChangingEventArgs args = new ContentChangingEventArgs();
                        args.Document = this.OwnerDocument;
                        args.Element = container;
                        args.ElementIndex = firstDeleteIndex;
                        args.DeletingElements = deletedElements;
                        container.RaiseBubbleOnContentChanging(args);
                        if (args.Cancel)
                        {
                            // 用户取消操作
                            continue;
                        }
                    }
                    // 批量删除元素
                    container.Elements.RemoveRange(
                        firstDeleteIndex,
                        lastDeleteIndex - firstDeleteIndex + 1);
                    // 更新容器元素的内容版本号
                    container.UpdateContentVersion();
                    // 设置删除的元素所在的文本行状态无效
                    foreach (DomElement re in removeElements)
                    {
                        if (re.OwnerLine != null)
                        {
                            re.OwnerLine.InvalidateState = true;
                        }
                    }
                    if (raiseEvent)
                    {
                        // 触发事件
                        ContentChangedEventArgs args = new ContentChangedEventArgs();
                        args.Document = this.OwnerDocument;
                        args.Element = container;
                        args.ElementIndex = firstDeleteIndex;
                        args.DeletedElements = deletedElements;
                        container.RaiseBubbleOnContentChanged(args);
                    }

                    bolChanged = true;
                    if (p1 != p2 && p2 != null)
                    {
                        SetParagraphSettings(p1, p2);
                    }
                }//try
                finally
                {
                    //container.Elements.EndFastRemove();
                }
            }//for
            if (bolChanged)
            {
                this.LineEndFlag = false;
                _OwnerDocument.Modified = true;
                foreach (DomContentElement ce in startIndexs.Keys)
                {
                    int index = startIndexs[ce];
                    ce.UpdateContentElements(false);
                    ce.RefreshPrivateContent(index);
                }//foreach
                this.DocumentContentElement.UpdateContentElements(false);
                this.SetSelection(selectionIndex, 0);
                return this.Selection.StartIndex;
            }
            else
            {
                return -1;
            }

//            int StartIndex = this.IndexOf(firstDeleteElement);
//            int EndIndex = this.IndexOf(lastDeleteElement);
//            int iLen = EndIndex - StartIndex+1;
//            bool bolChanged = false;
//            XTextElementList myList = this.SelectedElements ;
//            XTextElementList myRemoveList = new XTextElementList();

////			XTextUndoBatchRemoveElement undo = null;
////			if( myOwnerDocument.CanLogUndo )
////			{
////				undo = new XTextUndoBatchRemoveElement();
////				undo.Document = this.myOwnerDocument ;
////			}

//            XTextParagraphFlagElement cpe = this.OwnerDocument.GetParagraphEOFElement(firstDeleteElement);
//            XTextContentElement ce = this.CurrentElement.ContentElement;
//            int startPCIndex = ce.PrivateContent.IndexOf(this.CurrentElement);
//            //XTextElement LastDelete = null;
//            XTextElement FirstDelete = null;
//            XTextContainerElement LastContainer = null ;
//            try
//            {
//                for( int iCount = myList.Count - 1 ; iCount >= 0 ; iCount -- )
//                {
//                    XTextElement element = myList[ iCount ];
//                    if( myOwnerDocument.DocumentControler.CanDelete( element ))
//                    {
//                        XTextContainerElement c = element.Parent ;
//                        if( c != LastContainer )
//                        {
//                            if( LastContainer != null )
//                            {
//                                LastContainer.Elements.EndFastRemove();
//                            }
//                            LastContainer = c ;
//                            LastContainer.Elements.BeginFastRemove();
//                        }
//                        //int index = c.Elements.IndexOf( element );
//                        if( InnerRemoveElement( c , element ))
//                        {
//                            if (FirstDelete == null)
//                            {
//                                FirstDelete = element;
//                            }
//                            //LastDelete = element ;
//                            bolChanged = true ;
//                        }
//                    }
//                }//for
//            }
//            finally
//            {
//                if( LastContainer != null )
//                {
//                    LastContainer.Elements.EndFastRemove();
//                }
//            }
//            if( FirstDelete != null )
//            {
//                FirstDelete = FirstDelete.LastContentElement ;
//                FirstDelete = this.GetNextElement( FirstDelete );
//            }
//            if( FirstDelete != null )
//            {
//                //XTextParagraphEOF pe = LastDelete.OwnerParagraph.PEOF ;
//                SetParagraphSettings( cpe , FirstDelete.OwnerParagraphEOF );
//            }
////			if( undo != null )
////				myOwnerDocument.UndoList.Add( undo );
//            if( bolChanged )
//            {
//                myOwnerDocument.Modified = true ;
//                ce.UpdateContentElements();
//                ce.RefreshPrivateContent(startIndex);

//                //this.DocumentContentElement.UpdateContentElements();
				
//                //myOwnerDocument.RefreshContent( StartIndex ); //////////

//                this.SetSelection( StartIndex , 0 );
//                return this.intSelectionStart ;
//            }
//            return -1 ;
		}
//
//		private bool bolLineEndFlag		= false;
//		
//		public bool LineEndFlag
//		{
//			get{ return bolLineEndFlag ;}
//			set{ bolLineEndFlag = value;}
//		}

		/// <summary>
		/// 获得当前元素
		/// </summary>
		public DomElement CurrentElement
		{
			get
			{
                if (this.Count == 0)
                {
                    return null;
                }
                else
                {
                    if (this.Selection.StartIndex >= 0 && this.Selection.StartIndex < this.Count)
                    {
                        return SafeGet(this.Selection.StartIndex);
                    }
                    else
                    {
                        return SafeGet(this.Count - 1);
                    }
                }
			}
            set
            {
                if (this.Contains(value))
                {
                    this.MoveSelectStart(this.IndexOf(value));
                }
                //intSelectionStart = this.FixElementIndex(intSelectionStart);
            }
		}

        /// <summary>
        /// 当前段落结束标记对象
        /// </summary>
        public DomParagraphFlagElement CurrentParagraphEOF
        {
            get
            {
                return GetParagraphEOFElement(this.CurrentElement);
            }
        }

		/// <summary>
		/// 获得当前选中的元素,若文档中选择了一个元素则该属性返回这个元素,
		/// 若没有选中元素或选中多个元素则返回空
		/// </summary>
		public DomElement CurrentSelectElement
		{
			get
			{
                if (this.Count == 0 || Math.Abs( this.Selection.Length ) != 1 )
                {
                    return null;
                }
                else
                {
                    return this.Selection.FirstElement;
                }
			}
            //set
            //{
            //    if( Contains( value))
            //    {
            //        this.SetSelection( this.IndexOf( value)+1 ,-1);
            //    }
            //}
		}
         
		/// <summary>
		/// 获得当前位置的前一个元素
		/// </summary>
		public DomElement PreElement
		{
			get
			{
                int index = this.SelectionStartIndex;
                if (this.Count > 0
                    && index > 0
                    && index < this.Count)
                {
                    return this[index - 1];
                }
                else
                {
                    return null;
                }
			}
		}

		/// <summary>
		/// 当前文本行对象
		/// </summary>
		public DomContentLine CurrentLine
		{
			get
			{
                if (this.Count == 0)
                {
                    return null;
                }
                int index = this.SelectionStartIndex;
                if (index >= 0 && index < this.Count)
                {
                    DomContentLine myLine = this[index].OwnerLine;
                    if (this._LineEndFlag
                        && this.DocumentContentElement.Lines.IndexOf(myLine) > 0)
                    {
                        return this.DocumentContentElement.Lines[
                            this.DocumentContentElement.Lines.IndexOf(myLine) - 1];
                    }
                    else
                    {
                        return myLine;
                    }
                }
                else
                {
                    return this.LastElement.OwnerLine;
                }
			}
		}

		/// <summary>
		/// 当前行的上一个文本行对象
		/// </summary>
        public DomContentLine GetPreLine(bool ignoreHeaderRow)
        {
            DomContentLine myLine = this.CurrentLine;
             
            if (this.DocumentContentElement.Lines.IndexOf(myLine) > 0)
            {
                for (int iCount = this.SelectionStartIndex - 1; iCount >= 0; iCount--)
                {
                    if (this[iCount].OwnerLine != myLine)
                    {
                        return this[iCount].OwnerLine;
                    }
                }
                return null;
            }
            else
            {
                return myLine;
            }
        }

		/// <summary>
		/// 获得当前行的下一个文本行对象
		/// </summary>
        public DomContentLine GetNextLine(bool ignoreHeaderRow)
        {
            DomContentLine myLine = this.CurrentLine;
             
            if (this.DocumentContentElement.Lines.IndexOf(myLine)
                < this.DocumentContentElement.Lines.Count - 1)
            {
                for (int iCount = this.SelectionStartIndex + 1; iCount < this.Count; iCount++)
                {
                    if (this[iCount].OwnerLine != myLine)
                    {
                        return this[iCount].OwnerLine;
                    }
                }
                return null;
            }
            return myLine;
        }

        /// <summary>
        /// 获得指定元素所在段落的样式
        /// </summary>
        /// <param name="element">元素对象</param>
        /// <returns>段落样式对象</returns>
        public DomParagraphFlagElement GetParagraphEOFElement(DomElement element)
        {
            if (element == null)
            {
                return null;
            }
            int index = this.IndexOf(element);
            if (index >= 0)
            {
                for (int iCount = index; iCount < this.Count; iCount++)
                {
                    if (this[iCount] is DomParagraphFlagElement)
                    {
                        return (DomParagraphFlagElement)this[iCount];
                    }
                }
            }
            return null;
        }

		/// <summary>
		/// 选择所有的元素
		/// </summary>
		public void SelectAll()
		{
			if( this.Count >= 1 )
			{
                if (this.OwnerDocument.DocumentControler.FormView == FormViewMode.Strict)
                {
                    int index = this.FixIndexForStrictFormViewMode( this.SelectionStartIndex , FixIndexDirection.Both , true );

                    int index1 = this.FixIndexForStrictFormViewMode(0, FixIndexDirection.Forward, true);
                    int index2 = this.FixIndexForStrictFormViewMode(this.Count - 1, FixIndexDirection.Back, true);
                    
                }
                else
                {
                    this.SetSelection(this.Count - 1, 1 - this.Count);
                }
			}
		}

        /// <summary>
        /// 移动位置
        /// </summary>
        /// <param name="target"></param>
        public void MoveTo(MoveTarget target)
        {
            //this.AutoClearSelection = true;
            switch (target)
            {
                case MoveTarget.DocumentHome:
                    {
                        this.MoveSelectStart(
                            this.FixIndexForStrictFormViewMode(0, FixIndexDirection.Both, true));
                    }
                    break;
          
                case MoveTarget.ParagraphHome:
                    {
                        int index = 0;
                        for (int iCount = this.SelectionStartIndex ; iCount >= 0; iCount--)
                        {
                            if (this[iCount] is DomParagraphFlagElement)
                            {
                                index = iCount + 1;
                                break;
                            }
                        }
                        this.MoveSelectStart(this.FixIndexForStrictFormViewMode(index, FixIndexDirection.Both, true));
                    }
                    break;
                case MoveTarget.Home :
                    this.MoveHome();
                    break;
                case MoveTarget.End :
                    this.MoveEnd();
                    break;
                case MoveTarget.ParagraphEnd:
                    {
                        int index = this.SelectionStartIndex;
                        for (int iCount = this.SelectionStartIndex; iCount <= this.Count; iCount++)
                        {
                            if (this[iCount] is DomParagraphFlagElement)
                            {
                                index = iCount - 1;
                            }
                        }
                        this.MoveSelectStart(this.FixIndexForStrictFormViewMode(index, FixIndexDirection.Both, true));
                    }
                    break;
               
                case MoveTarget.DocumentEnd:
                    {
                        int index = this.Count - 2;
                        index = this.FixElementIndex(index);
                        this.MoveSelectStart(this.FixIndexForStrictFormViewMode(index, FixIndexDirection.Both, true));
                    }
                    break;
            }
        }

		/// <summary>
		/// 将插入点向上移动一行
		/// </summary>
		public void MoveUpOneLine()
		{
			ClearLineEndFlag();
			DomContentLine myLine = this.GetPreLine( true ) ;
			if( myLine != null )
			{
				if( _LastXPos <= 0 )
				{
					DomElement StartElement = this[ this.FixElementIndex( this.SelectionStartIndex) ] ;
					_LastXPos = StartElement.AbsLeft ;
				}
                DomElement curElement = null;
				foreach( DomElement myElement in myLine )
				{
					if( myElement.AbsLeft >= _LastXPos )
					{
                        curElement = myElement;
                        break;
					}
				}
                if (curElement == null)
                {
                    curElement = myLine.LastElement;
                }
                int index = this.IndexOf(curElement);
                if (index >= 0)
                {
                    index = FixIndexForStrictFormViewMode(index, FixIndexDirection.Forward , true );
                    this.MoveSelectStart(index);
                }
			}
			//XTextLine myLine = this.PreElement 
		}

		/// <summary>
		/// 将插入点向下移动一行
		/// </summary>
		public void MoveDownOneLine()
		{
			ClearLineEndFlag();
			DomContentLine myLine = this.GetNextLine( true );
			if( myLine != null )
			{
				if( _LastXPos <= 0 )
				{
					DomElement StartElement = this[ this.FixElementIndex( this.SelectionStartIndex )] ;
					_LastXPos = StartElement.AbsLeft ;
				}
                DomElement curElement = null;
				foreach( DomElement element in myLine )
				{
					if( !(element is DomParagraphListItemElement)
                        && element.AbsLeft >= _LastXPos )
					{
                        curElement = element;
                        break;
					}
				}
                if (curElement == null)
                {
                    curElement = myLine.LastElement;
                }
                int index = this.IndexOf(curElement);
                if (index >= 0)
                {
                    index = FixIndexForStrictFormViewMode(index, FixIndexDirection.Back , true );
                    this.MoveSelectStart(index);
                }
				//this.MoveSelectStart( myLine.LastElement );
			}
		}

		/// <summary>
		/// 将插入点先左移动一个元素
		/// </summary>
		public void MoveLeft()
		{
			ClearLineEndFlag();
			_LastXPos = -1 ;
            if (this.AutoClearSelection && this.SelectionLength != 0)
            {
                // 当需要清除选择状态是，移动插入点到选择区域的第一个元素之前
                int newIndex = this.SelectionStartIndex;
                if (this.SelectionLength < 0)
                {
                    newIndex = this.SelectionStartIndex + this.SelectionLength;
                }
                if (newIndex < 0 )
                {
                    newIndex = 0;
                }
                newIndex = FixIndexForStrictFormViewMode(newIndex, FixIndexDirection.Forward , true );
                this.MoveSelectStart(newIndex);
            }
            else
            {
                if (this.SelectionStartIndex  > 0 )
                {
                    int index = this.SelectionStartIndex - 1;
                    index = FixIndexForStrictFormViewMode(index, FixIndexDirection.Forward, true);
                    this.MoveSelectStart( index );
                }
            }
		}

		/// <summary>
		/// 将插入点向右移动一个元素
		/// </summary>
		public void MoveRight()
		{
			ClearLineEndFlag();
			_LastXPos = -1 ;
            if (this.AutoClearSelection && this.SelectionLength != 0)
            {
                // 当需要清除选择状态是，移动插入点到选择区域的最后一个元素之后
                int newIndex = this.SelectionStartIndex;
                if (this.SelectionLength > 0)
                {
                    newIndex = this.SelectionStartIndex + this.SelectionLength ;
                }
                if (newIndex >= this.Count)
                {
                    newIndex = this.Count - 1;
                }
                newIndex = FixIndexForStrictFormViewMode(newIndex, FixIndexDirection.Back, true);
                this.MoveSelectStart(newIndex);
            }
            else
            {
                if (this.SelectionStartIndex < this.Count - 1)
                {
                    int index = this.SelectionStartIndex + 1;
                    index = FixIndexForStrictFormViewMode(index, FixIndexDirection.Back, true);
                    this.MoveSelectStart( index );
                }
            }
		}

		/// <summary>
		/// 将插入点移动到当前行的最后一个元素处
		/// </summary>
		public void MoveEnd()
		{
			//bolLineEndFlag = false;
			try
			{
				DomContentLine myLine = this.CurrentLine ;
				if( myLine != null && _LineEndFlag == false )
				{
					_LastXPos = -1 ;
					this.CurrentElement = myLine.LastElement ;
					if( this.OwnerDocument.DocumentControler.IsNewLine( myLine.LastElement ))
					{
                        int index = this.IndexOf(myLine.LastElement);
                        if (index >= 0)
                        {
                            index = FixIndexForStrictFormViewMode(index, FixIndexDirection.Back, true);
                            this.MoveSelectStart(index);
                        }
					}
					else
					{
                        int index = this.IndexOf(myLine.LastElement) + 1;
                        int newIndex = FixIndexForStrictFormViewMode(index, FixIndexDirection.Back, true);
                        if (index != newIndex)
                        {
                            this.MoveSelectStart(newIndex);
                        }
                        else
                        {
                            this.MoveSelectStart( index );
                            _LineEndFlag = true;
                        }
						//myOwnerDocument.update
					}
				}
			}
			catch{}
		}

		/// <summary>
		/// 移动插入点到当前行的行首
		/// </summary>
		public void MoveHome()
		{
			ClearLineEndFlag();
			DomContentLine myLine = this.CurrentLine ;
			if( myLine != null )
			{
				_LastXPos = -1 ;
				int FirstIndex = this.IndexOf( myLine.FirstElement );
				int FirstNBlank = 0 ;
				foreach( DomElement myElement in myLine )
				{
					DomCharElement myChar = myElement as DomCharElement ;
					if( myChar == null || char.IsWhiteSpace( myChar.CharValue ) == false )
					{
						FirstNBlank = myLine.IndexOf( myElement );
						break;
					}
				}
				if( FirstNBlank == 0 || this.Selection.StartIndex == ( FirstIndex + FirstNBlank ))
				{
                    FirstIndex = FixIndexForStrictFormViewMode(FirstIndex, FixIndexDirection.Forward, true);
					this.MoveSelectStart( FirstIndex );
				}
				else
				{
                    int index = FirstIndex + FirstNBlank;
                    index = FixIndexForStrictFormViewMode(index, FixIndexDirection.Forward, true);
					this.MoveSelectStart( index );
				}
			}
		}

		/// <summary>
		/// 移动当前插入点的位置
		/// </summary>
		/// <param name="index">插入点的新的位置</param>
		/// <returns>操作是否成功</returns>
		public bool MoveSelectStart( int index)
		{
			ClearLineEndFlag();
			index = FixElementIndex( index );
			int NewLength = 0 ;
			if( this._AutoClearSelection == false )
			{
				NewLength = this.SelectionLength +  ( this.SelectionStartIndex - index );
//				if( this.bolLineEndFlag )
//					NewLength -- ;
			}
            
			return SetSelection( index , NewLength );
//				index, 
//				( bolAutoClearSelection ? 0: intSelectionLength + intSelectionStart - index ));
		}

		/// <summary>
		/// 将插入点移动到指定元素前面
		/// </summary>
		/// <param name="element">指定的元素</param>
		/// <returns>操作是否成功</returns>
		public bool MoveSelectStart( DomElement element )
		{
			int index = this.IndexOf( element );
            if (index >= 0)
            {
                return MoveSelectStart(index);
            }
			return false;
		}

		/// <summary>
		/// 水平移动插入点指定距离
		/// </summary>
		/// <param name="Step">移动距离</param>
		public void MoveStep( float Step )
		{
			ClearLineEndFlag();
			_LineEndFlag = false;
			DomElement element = this.CurrentElement ;
			if( element != null )
			{
				this.MoveTo( element.AbsLeft , element.AbsTop + Step );
			}
		}

		private void ClearLineEndFlag()
		{
			if( this._LineEndFlag )
			{
				this._LineEndFlag = false;
				//this.intSelectionStart -- ;
			}
		}

        /// <summary>
        /// 获得文档中指定位置下的文档行对象
        /// </summary>
        /// <param name="x">指定位置的X坐标</param>
        /// <param name="y">指定位置的Y坐标</param>
        /// <param name="strict">是否进行严格判断</param>
        /// <returns>找到的文档行对象,若未找到则返回空引用</returns>
        public DomContentLine GetLineAt(float x, float y , bool strict )
        {
            if (this._OwnerDocument == null)
            {
                return null;
            }
            // 查找指定位置下的容器元素对象
            DomContentElement contentElement = this.DocumentContentElement;
            DomElementList ces = this.DocumentContentElement.ContentElements;
            
            for (int iCount = ces.Count - 1; iCount >= 0; iCount--)
            {
                if (ces[iCount].AbsBounds.Contains(x, y))
                {
                    
                    contentElement = (DomContentElement)ces[iCount];
                    
                    break;
                }
            }
            DomContentLine currentLine = null;
            if (strict)
            {
                // 进行严格判断
                x = x - contentElement.AbsLeft;
                y = y - contentElement.AbsTop;
                foreach (DomContentLine line in contentElement.PrivateLines)
                {
                    RectangleF bounds = new RectangleF(
                        line.Left, 
                        line.Top, 
                        line.Width, 
                        line.ContentHeight + line.AdditionHeight);
                    if (bounds.Contains(x, y))
                    {
                        currentLine = line;
                        break;
                    }
                }
            }
            else
            {
                // 进行非严格判断
                // 确定当前行,指定的Y坐标在当前行低边缘上面
                foreach (DomContentLine myLine in contentElement.PrivateLines)
                {
                    if (myLine.AbsTop + myLine.ViewHeight > y)
                    {
                        int index = contentElement.PrivateLines.IndexOf(myLine);
                        if (index > 0)
                        {
                            DomContentLine preLine = contentElement.PrivateLines[index - 1];
                            float pos = (preLine.AbsTop + preLine.ViewHeight + myLine.AbsTop) / 2;
                            if ( y > pos )
                            {
                                currentLine = myLine;
                            }
                            else
                            {
                                currentLine = preLine;
                            }
                        }
                        else
                        {
                            currentLine = myLine;
                        }
                        break;
                    }
                }//foreach

                // 若没有找到当前行则设置最后一行为当前行
                if (currentLine == null)
                {
                    currentLine = contentElement.PrivateLines.LastLine;
                }
            }
             
            return currentLine;
        }

		/// <summary>
		/// 获得文档中指定位置下的元素对象
		/// </summary>
		/// <param name="x">指定位置的X坐标</param>
		/// <param name="y">指定位置的Y坐标</param>
        /// <param name="strict">是否进行严格判断</param>
		/// <returns>找到的元素,若未找到则返回空引用</returns>
		public DomElement GetElementAt( float x , float y , bool strict)
		{
            if (this._OwnerDocument == null)
            {
                return null;
            }
            // 确定当前行
			DomContentLine CurrentLine = GetLineAt( x , y , strict );
			// 若没有找到当前行则函数处理失败，立即返回
            if (CurrentLine == null)
            {
                return null;
            }
            // 修正X坐标值
			x = x - CurrentLine.AbsLeft ;//- CurrentLine.Margin ;
            if (strict)
            {
                // 进行严格判断
                if( x >= 0 && x <= CurrentLine.Width )
                {
                    foreach (DomElement element in CurrentLine)
                    {
                        if (element is DomParagraphListItemElement)
                        {
                            // 段落列表元素不能选择
                            continue;
                        }
                        if (x >= element.Left && x <= element.Left + element.Width + element.WidthFix )
                        {
                            return element;
                        }
                    }//foreach
                }
                return null;
            }
            else
            {
                // 进行非严格判断
                if (x < 0)
                {
                    return CurrentLine.FirstElement;
                }
                foreach (DomElement myElement in CurrentLine)
                {
                    if (myElement is DomParagraphListItemElement)
                    {
                        // 段落列表元素不能选择
                        continue;
                    }
                    if (x < myElement.Left + myElement.Width)
                    {
                        return myElement;
                    }
                }
                return CurrentLine.LastElement;
            }
		}


		private float _LastXPos = -1;
		/// <summary>
		/// 将插入点尽量移动到指定位置
		/// </summary>
		/// <param name="x">指定位置的X坐标</param>
		/// <param name="y">指定位置的Y坐标</param>
        public void MoveTo(float x, float y)
        {
            if (this.OwnerDocument == null)
            {
                return;
            }
            _LastXPos = -1;
            // 确定当前行,指定的Y坐标在当前行低边缘上面
            DomContentLine currentLine = GetLineAt(x, y, false);
            // 若最后还是没有找到当前行则函数处理失败，立即返回
            if (currentLine == null)
            {
                return;
            }
            bool bolFlag = false;
            int index = 0;
            x = x - currentLine.AbsLeft;//- CurrentLine.Margin ;

            // 确定当前元素,当前元素是当前行中右边缘大于指定的Ｘ坐标的元素
            DomElement currentElement = null;
            foreach (DomElement myElement in currentLine)
            {
                if (myElement is DomParagraphListItemElement)
                {
                    // 段落列表元素不能选择
                    continue ;
                }
                if (x < myElement.Left + myElement.Width)
                {
                    if (x > (myElement.Left + myElement.Width / 2))
                    {
                        continue;
                    }
                    currentElement = myElement;
                    break;
                }
            }
            if (currentElement == null)
            {
                // 若没找到当前元素则表示当前位置已向右超出当前行的范围
                // 若当前行已换行符结尾则设置该换行符为当前元素
                // 否则设置为当前行最后一个元素的下一个元素,并设置行尾标志
                if (this.AutoClearSelection == true
                    && currentLine.HasLineEndElement)
                {
                    index = this.IndexOf(currentLine.LastElement);
                }
                else
                {
                    index = this.IndexOf(currentLine.LastElement) + 1;
                    if (currentLine.LastElement is DomParagraphFlagElement)
                    {
                        index = this.IndexOf(currentLine.LastElement);
                        bolFlag = false;
                    }
                    else
                    {
                        if (index >= this.Count - 1)
                        {
                            bolFlag = false;
                            index = this.IndexOf(currentLine.LastElement);
                        }
                        else
                        {
                            bolFlag = true;
                        }
                    }
                }
            }
            else
            {
                // 若找到当前元素则设置当前位置到当前元素
                index = this.IndexOf(currentElement);
                bolFlag = false;
            }
            if (index < 0)
            {
                return;
            }
            // 修正当前元素序号
            if (index > this.Count)
            {
                index = this.Count - 1;
                bolFlag = false;
            }
            if (index < 0)
            {
                index = 0;
                bolFlag = false;
            }
            this.LineEndFlag = bolFlag;

            index = FixElementIndex(index);
            int newLength = 0;
            if (this.AutoClearSelection == false)
            {
                newLength = this.Selection.NativeLength + (this.Selection.NativeStartIndex - index);
            }

            // 根据严格的表单模式来修正插入点的位置
            int fixIndex = this.FixIndexForStrictFormViewMode(index , FixIndexDirection.Both , true );
            if (fixIndex != index)
            {
                index = fixIndex;
                newLength = 0;
            }
            if (newLength != 0)
            {
                if (this.OwnerDocument.DocumentControler.FormView == Controls.FormViewMode.Strict)
                {
                    DomContainerElement c1 = null ;
                    int ei1 = 0;
                    GetPositonInfo(index, out c1, out ei1, false);
                    DomContainerElement c2 = null;
                    int ei2 = 0;
                    GetPositonInfo(index + newLength, out c2, out ei2, false);
                    DomElement root = WriterUtils.GetRootElement(c1, c2);
                    
                    {
                        // 选择区域头尾元素不处于同一个文本输入域，则清除选择区域
                        newLength = 0;
                    }
                }
            }
            SetSelection(index, newLength);
            //this.MoveSelectStart( index );

            //if( bolLineEndFlag != bolFlag )
            {
                //this.MoveSelectStart( index );
                if (_OwnerDocument.EditorControl != null)
                {
                    _OwnerDocument.EditorControl.UpdateTextCaret();
                }
            }
            //			else
            //			{
            //				//this.MoveSelectStart( index );
            //			}
        }


         
        internal enum FixIndexDirection
        {
            /// <summary>
            /// 向前修正
            /// </summary>
            Forward,
            /// <summary>
            /// 向后修正
            /// </summary>
            Back,
            /// <summary>
            /// 所有的方向
            /// </summary>
            Both
        }

        /// <summary>
        /// 根据表单视图模式修正当前插入点的位置
        /// </summary>
        /// <returns>操作是否修改了插入点的位置</returns>
        internal bool FixCurrentIndexForStrictFormViewMode()
        {
            if (this.OwnerDocument.DocumentControler.FormView == FormViewMode.Strict)
            {
                int index = FixIndexForStrictFormViewMode(this.SelectionStartIndex, FixIndexDirection.Both, true);
                if (index != this.SelectionStartIndex)
                {
                    this.SetSelection(index, 0);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 根据表单视图状态修正插入点位置
        /// </summary>
        /// <param name="index">要修正的插入点位置编号</param>
        /// <param name="direction">修正方向</param>
        /// <returns>修正后的插入点位置</returns>
        internal int FixIndexForStrictFormViewMode(int index , FixIndexDirection direction , bool enableSetAutoClearSelectionFlag )
        {
             
            return index;
        }

		/// <summary>
		/// 设置选择区域大小
		/// </summary>
		/// <param name="NewSelectStart">新的选择区域开始的序号</param>
		/// <param name="NewSelectLength">新选择区域的长度</param>
		/// <returns>操作是否成功</returns>
		internal bool SetSelection( int newSelectionStart , int newSelectionLength)
		{
            FixRange(ref newSelectionStart, ref newSelectionLength);
            return this.DocumentContentElement.SetSelection(newSelectionStart, newSelectionLength);
		}
 
		/// <summary>
		/// 选择插入点所在的段落
		/// </summary>
		/// <returns>操作是否成功</returns>
        public bool SelectParagraph()
        {
            if (this.Count == 0)
            {
                return false;
            }
            bool strictFormView =
                this.OwnerDocument.DocumentControler.FormView == FormViewMode.Strict;
            DomElement element = this.CurrentElement;
            if (element != null)
            {
                int index = this.IndexOf(element);
                int startIndex = 0;
                int endIndex = this.Count - 1;
                for (int start = index - 1; start >= 0; start--)
                {
                    if (this[start] is DomParagraphFlagElement)
                    {
                        startIndex = start+1;
                        break;
                    }
                     
                }
                for (int end = index; end < this.Count; end++)
                {
                    if (this[end] is DomParagraphFlagElement)
                    {
                        endIndex = end;
                        break;
                    }
                     
                }
                this._LineEndFlag = false;
                this.SetSelection(startIndex, endIndex - startIndex + 1);
                this._AutoClearSelection = true;
                return true;
            }
            return false;
        }

		/// <summary>
		/// 选择插入点处的连续的字母和数字
		/// </summary>
		/// <returns>操作是否成功</returns>
		public bool SelectWord()
		{
            if (this.Count == 0)
            {
                return false;
            }
            DomContainerElement container = null;
			int start = -1 ;
            for (int iCount = this.SelectionStartIndex ; iCount >= 0; iCount--)
            {
                if (container == null)
                {
                    container = this[iCount].Parent;
                }
                DomCharElement c = this[iCount] as DomCharElement;
                if (c == null)
                {
                    break;
                }
                if (c.Parent == container)
                {
                    if (char.IsLetter(c.CharValue) || char.IsDigit(c.CharValue))
                    {
                        start = iCount;
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
            }//for
			if( start >= 0 )
			{
				int end = -1 ;
                for (int iCount = this.SelectionStartIndex ; iCount < this.Count; iCount++)
                {
                    if (container == null)
                    {
                        container = this[iCount].Parent;
                    }
                    DomCharElement c = this[iCount] as DomCharElement;
                    if (c == null)
                    {
                        break;
                    }
                    if (c.Parent == container)
                    {
                        if (char.IsLetter(c.CharValue) || char.IsDigit(c.CharValue))
                        {
                            end = iCount;
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
                }//for
				if( end != - 1 )
				{
					this._LineEndFlag = false;
					this.SetSelection( start , end - start + 1 );
					return true ;
				}
			}
			return false;
		}


        ///// <summary>
        ///// 获得指定元素前后相邻的元素对象，这些元素的某些方面相同。
        ///// </summary>
        ///// <param name="element">指定的元素</param>
        ///// <param name="callBack">比较对象的委托</param>
        ///// <returns></returns>
        //public XTextRange CreateRange(XTextElement element, CompareHandler callBack)
        //{
        //    int index = this.IndexOf(element);
        //    int startIndex = index;
        //    int endIndex = index;
        //    //向前搜索
        //    for (int iCount = index; iCount > 0; iCount--)
        //    {
        //        if (callBack(element, this[iCount]) == 0)
        //        {
        //            startIndex = iCount;
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //    // 向后搜索
        //    for (int iCount = index; iCount < this.Count; iCount++)
        //    {
        //        if (callBack(element, this[iCount]) == 0)
        //        {
        //            endIndex = iCount;
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //    return new XTextRange(
        //        this.OwnerDocument ,
        //        startIndex,
        //        endIndex - startIndex + 1);
        //}
         

        /// <summary>
        /// 查找字符串
        /// </summary>
        /// <param name="StartIndex">查找开始序号</param>
        /// <param name="strFind">要查找的字符串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <param name="setSelection">是否根据查找结果设置新的选择区域</param>
        /// <returns>找到的位置</returns>
        public int SearchString(int StartIndex, string strFind, bool ignoreCase, bool setSelection)
        {
            if (strFind == null || strFind.Length == 0)
            {
                return -1;
            }
            System.Text.StringBuilder myStr = new System.Text.StringBuilder();
            if (StartIndex < 0)
            {
                StartIndex = 0;
            }
            for (int iCount = StartIndex; iCount < this.Count; iCount++)
            {
                DomCharElement c = this[iCount] as DomCharElement;
                if (c == null)
                {
                    myStr.Append('\0');
                }
                else
                {
                    myStr.Append(c.CharValue);
                }
            }
            string text = myStr.ToString();
            int index = 0;
            if (ignoreCase)
            {
                index = text.IndexOf(strFind, StringComparison.CurrentCultureIgnoreCase);
            }
            else
            {
                index = text.IndexOf(strFind);
            }
            if (index >= 0)
            {
                index = index + StartIndex;
                if (setSelection)
                {
                    this.SetSelection(index, strFind.Length);
                }
                return index;
            }
            else
            {
                return -1;
            }
        }
	}
}