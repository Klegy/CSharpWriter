/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.ComponentModel;
using System.Xml.Serialization;
using DCSoft.CSharpWriter.Html;
using DCSoft.CSharpWriter.Script;
using System.Text;
using System.Drawing;

namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 容器元素对象
	/// </summary>
	/// <remarks>
	/// 本类型是从XTextElement上派生的容器文本文档元素类型,它能包含其他的文本文档元素,
	/// 还可以包含其他的容器元素.是文本文档对象模型中比较基础的类型.
	/// 编制 袁永福 2007-3-21
	/// </remarks>
    [Serializable()]
	public class DomContainerElement : DomElement
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public DomContainerElement()
		{
			_Elements = new DomElementList();
			_Elements.OwnerElement = this ;
		}

        /// <summary>
        /// 文档内容可编辑
        /// </summary>
        [Browsable( false )]
        public virtual bool ContentEditable
        {
            get
            {
                return true;
            }
        }
        ///// <summary>
        ///// 对象在文档对象模型中的序号
        ///// </summary>
        //[Browsable(false)]
        //public int DomIndex
        //{
        //    get
        //    {
        //        int result = 0;
        //        XTextElement element = this ;
        //        XTextElement parent = this.Parent;
        //        while (parent != null)
        //        {
        //            result = result + parent.Elements.IndexOf(element);
        //            element = parent;
        //            parent = element.Parent;
        //        }
        //        return result;

        //        return _DomIndex;
        //    }
        //}

        /// <summary>
        /// 返回子元素背景样式
        /// </summary>
        public virtual DocumentContentStyle GetContentBackgroundStyle(DomElement childElement)
        {
            DocumentContentStyle rs = (DocumentContentStyle)this.RuntimeStyle;
            if ( rs != null && rs.HasVisibleBackground)
            {
                return rs;
            }
            else
            {
                return null;
            }
            //return null;
        }


		/// <summary>
		/// 对象所属文档对象
		/// </summary>
		[XmlIgnore ()]
        [Browsable( false )]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        public override DomDocument OwnerDocument
		{
			get
			{
				return base.OwnerDocument ;
			}
            set
            {
                base.OwnerDocument = value;
                foreach (DomElement element in this.Elements)
                {
                    element.OwnerDocument = value;
                }
                //if (myEOFElement != null)
                //{
                //    myEOFElement.OwnerDocument = value;
                //}
            }
		}

        private DomExpressionList _Expressions = null;
        /// <summary>
        /// 表达式列表
        /// </summary>
        [DefaultValue( null )]
        [System.Xml.Serialization.XmlArrayItem( "Expression" , typeof( DomExpression ))]
        public virtual DomExpressionList Expressions
        {
            get
            {
                return _Expressions; 
            }
            set
            {
                _Expressions = value; 
            }
        }
        

        private XAttributeList _Attributes = new XAttributeList();
        /// <summary>
        /// 用户自定义属性列表
        /// </summary>
        [DefaultValue( null)]
        [XmlArrayItem("Attribute" , typeof( DomAttribute ))]
        public XAttributeList Attributes
        {
            get
            {
                return _Attributes; 
            }
            set
            {
                _Attributes = value; 
            }
        }
         
        ///// <summary>
        ///// 刷新对象包含的段落对象集合
        ///// </summary>
        //public virtual void RefreshParagraphs( )
        //{
        //    //myParagraphs = WriterUtils.RefreshParagraphs(
        //    //    this ,
        //    //    this.myElements ,
        //    //    true );
        //}

		/// <summary>
		/// 子元素列表
		/// </summary>
		private DomElementList _Elements  = null ;
		/// <summary>
		/// 子元素列表
		/// </summary>
        [XmlIgnore()]
        [Browsable( false )]
		public override DomElementList Elements
		{
			get
			{
				return _Elements ;
			}
            set
            {
                _Elements = value;
                if (_Elements != null)
                {
                    _Elements.OwnerElement = this;
                    foreach (DomElement e in _Elements)
                    {
                        e.Parent = this;
                    }
                }
            }
		}

        [NonSerialized()]
        internal DomElementList _ElementsForSerialize = null;
        
        /// <summary>
        /// 为XML序列化/反序列化的子元素列表
        /// </summary>
        [System.ComponentModel.Browsable(false )]
        [System.Xml.Serialization.XmlArray("XElements")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual DomElementList ElementsForSerialize
        {
            get
            {
                if (_ElementsForSerialize == null )
                {
                    //myElementForSerialize = new XTextElementList();
                    if ( this.OwnerDocument == null || this.OwnerDocument._Deserializing )
                    {
                        _ElementsForSerialize = new DomElementList();
                    }
                    else
                    {
                        _ElementsForSerialize = WriterUtils.MergeElements(this.Elements, false);
                    }
                }
                return _ElementsForSerialize;
            }
            set 
            {
                _ElementsForSerialize = value;
                if (_ElementsForSerialize != null)
                {
                    _ElementsForSerialize.OwnerElement = this;
                }
            }
        }

        /// <summary>
        /// 文档加载后的处理
        /// </summary>
        /// <param name="format">文档存储格式</param>
        public override void AfterLoad(FileFormat format)
        {
            if (FixElementsForSerialize(false))
            {
                //WriterUtils.SplitElements(this.Elements);
            }
            WriterUtils.SplitElements(this.Elements, false);
            //if (_ElementsForSerialize != null && _ElementsForSerialize.Count > 0)
            //{
            //    this.Elements.Clear();
            //    foreach (XTextElement element in _ElementsForSerialize)
            //    {
            //        if (element is XTextParagraphElement)
            //        {
            //            this.Elements.AddRange(element.Elements);
            //        }
            //        else
            //        {
            //            this.Elements.Add(element);
            //        }
            //    }//foreach
            //    foreach (XTextElement element in this.Elements)
            //    {
            //        element.OwnerDocument = this.OwnerDocument ;
            //        element.Parent = this;
            //    }
            //    WriterUtils.SplitElements(this.Elements);
            //}
            foreach (DomElement element in this.Elements)
            {
                element.Parent = this;
                element.AfterLoad(format);
            }
            base.AfterLoad(format);
        }

        internal bool FixElementsForSerialize( bool deeply )
        {
            if (_ElementsForSerialize != null && _ElementsForSerialize.Count > 0)
            {
                this.Elements.Clear();
                foreach (DomElement element in _ElementsForSerialize)
                {
                    if (element is DomParagraphElement)
                    {
                        this.Elements.AddRange(element.Elements);
                    }
                    //else if (element is XTextStringElement)
                    //{
                    //    XTextStringElement str = (XTextStringElement)element;
                    //    XTextElementList list = str.SplitChars();
                    //    this.Elements.AddRange(list);
                    //}
                    else
                    {
                        this.Elements.Add(element);
                    }
                }//foreach
                _ElementsForSerialize = null;
                //WriterUtils.SplitElements(this.Elements);
                foreach (DomElement element in this.Elements)
                {
                    element.OwnerDocument = this.OwnerDocument;
                    element.Parent = this;
                    if (deeply)
                    {
                        if (element is DomContainerElement)
                        {
                            ((DomContainerElement)element).FixElementsForSerialize(deeply);
                        }
                    }
                }
                return true;
            }
            return false;
        }

		/// <summary>
		/// 子孙元素中第一个显示在文档内容中的元素
		/// </summary>
		[Browsable( false )]
        public override DomElement FirstContentElement
		{
			get
			{
                foreach (DomElement element in _Elements)
                {
                    if (element is DomContainerElement)
                    {
                        DomContainerElement c = (DomContainerElement)element;
                        DomElement fc = c.FirstContentElement;
                        if (fc != null)
                        {
                            return fc;
                        }
                    }
                    else
                    {
                        return element;
                    }
                }
				return null;
			}
		}
		/// <summary>
		/// 子孙元素中第一个显示在文档内容中的元素
		/// </summary>
		[Browsable( false )]
        public override DomElement LastContentElement
		{
			get
			{
                for (int iCount = _Elements.Count - 1; iCount >= 0; iCount--)
                {
                    DomElement element = _Elements[iCount];
                    if (element is DomContainerElement)
                    {
                        DomContainerElement c = (DomContainerElement)element;
                        DomElement e = c.LastContentElement;
                        if (e != null)
                        {
                            return e;
                        }
                    }
                    else
                    {
                        return element;
                    }
                }
				return null;
			}
		}

        /// <summary>
        /// 判断是否包含被用户选择的内容
        /// </summary>
        [Browsable( false )]
		public override bool HasSelection
		{
            get
            {
                DomElement first = this.FirstContentElement;
                DomElement last = this.LastContentElement;
                DomDocumentContentElement ce = this.DocumentContentElement;
                int start = ce.Selection.AbsStartIndex ;
                int end =  ce.Selection.AbsEndIndex ;
                if (first.ViewIndex <= end && last.ViewIndex >= start)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
		}

        /// <summary>
        /// 对整个内容执行重新排版操作
        /// </summary>
        public virtual void ExecuteLayout()
        {
        }

        /// <summary>
        /// 允许接收的子元素类型
        /// </summary>
        [Browsable( false )]
        [XmlIgnore]
        public virtual ElementType AcceptChildElementTypes
        {
            get
            {
                return ElementType.All;
            }
        }

		/// <summary>
		/// 添加子元素
		/// </summary>
		/// <param name="element">新添加的元素</param>
		public virtual bool AppendChildElement( DomElement element )
		{
			if( element != null )
			{
				if( _Elements.Contains( element ) == false )
				{
					_Elements.Add( element );
				}
				element.Parent = this ;
				element.OwnerDocument = this.OwnerDocument ;
				return true ;
			}
			return false;
		}

		/// <summary>
		/// 删除子元素
		/// </summary>
		/// <param name="element">要删除的子元素</param>
		/// <returns>操作是否成功</returns>
		public virtual bool RemoveChild( DomElement element )
		{
			if( element != null )
			{
				_Elements.Remove( element );
				return true ;
			}
			return false;
		}


		/// <summary>
		/// 将对象内容添加到文档内容中
		/// </summary>
		/// <param name="content">文档内容对象</param>
        /// <returns>添加的文档元素个数</returns>
		public virtual int AppendContent( DomElementList content , bool privateMode )
		{
            int result = 0;
			foreach( DomElement element in this.Elements )
			{
				element.Parent = this ;
                if (  element.Visible )
                {
                     
                    if (element is DomContainerElement)
                    {
                        DomContainerElement c = (DomContainerElement)element;
                        result = result + c.AppendContent(content , privateMode );
                    }
                    else
                    {
                        content.Add(element);
                        result++;
                    }
                }//if
			}//foreach
            return result;
		}

        public override void WriteHTML(WriterHtmlDocumentWriter writer)
		{
			WriteContentHTML( writer );
		}

		protected virtual void WriteContentHTML( WriterHtmlDocumentWriter writer )
		{
			DomElementList list = WriterUtils.MergeElements( 
                this.Elements ,
                true );
			if( list != null && list.Count > 0 )
			{
				foreach( DomElement element in list )
				{
					if( writer.IncludeSelectionOndly == false
                        || element.HasSelection )
					{
						element.WriteHTML( writer );
					}
				}
			}
		}

        /// <summary>
        /// 输出RTF文档
        /// </summary>
        /// <param name="writer">RTF文档书写器</param>
        public override void WriteRTF(DCSoft.CSharpWriter.RTF.RTFContentWriter writer)
        {
            DomElementList list = WriterUtils.MergeParagraphs(
                this.Elements,
                writer.IncludeSelectionOnly);
            if (list != null && list.Count > 0)
            {
                foreach (DomElement element in list)
                {
                     
                    //if (writer.IncludeSelectionOnly == false
                    //    || element.HasSelection)
                    {
                        element.WriteRTF(writer);
                    }
                }//foreach
            }
        }

        //public virtual void WriteCotentDocument( DocumentContentWriter writer )
        //{
        //    XTextElementList list = WriterUtils.MergeParagraphs(
        //        this.Elements ,
        //        writer.IncludeSelectionOnly );
        //    if( list != null && list.Count > 0 )
        //    {
        //        foreach( XTextElement element in list )
        //        {
        //            //if (writer.IncludeSelectionOnly == false
        //            //    || element.HasSelection)
        //            if (this is XTextTableCellElement)
        //            {
                        
        //            }
        //            {
        //                element.WriteRTF( writer );
        //            }
        //        }//foreach
        //    }
        //}

        /// <summary>
        /// 返回预览对象内容的字符串
        /// </summary>
        /// <returns></returns>
        public virtual string PreviewString
        {
            get
            {
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                foreach (DomElement element in this.Elements)
                {
                    if ((element is DomParagraphFlagElement) == false)
                    {
                        str.Append(element.ToString());
                        if (str.Length > 20)
                        {
                            break;
                        }
                    }
                }
                return "Para:" + str.ToString();
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="Deeply">是否复制子孙节点</param>
        /// <returns>复制品</returns>
        public override DomElement Clone(bool Deeply)
        {
            DomContainerElement c = (DomContainerElement)base.Clone(Deeply);
            if (this._Attributes != null)
            {
                c._Attributes = this._Attributes.Clone();
            }
            //if (this._Expressions != null)
            //{
            //    c._Expressions = this._Expressions.Clear 
            //}
            c._Elements = new DomElementList();
            c._ElementsForSerialize = null;
            if (Deeply)
            {
                if (_Elements != null)
                {
                    c._Elements = new DomElementList();
                    foreach (DomElement element in _Elements)
                    {
                        DomElement newElement = element.Clone(Deeply);
                        c._Elements.Add(newElement);
                        newElement.Parent = c;
                    }//foreach
                }//if
            }
            return c;
        }

        /// <summary>
        /// 销毁对象
        /// </summary>
        public override void Dispose()
        {
            foreach (DomElement element in this.Elements)
            {
                element.Dispose();
            }
        }

        /// <summary>
        /// 遍历子孙文档元素
        /// </summary>
        /// <param name="handler">遍历过程的委托对象</param>
        public void Enumerate(ElementEnumerateEventHandler handler)
        {
            Enumerate(handler, false);
        }

        /// <summary>
        /// 遍历子孙元素
        /// </summary>
        /// <param name="includeSelfNode">是否包含节点本身</param>
        /// <param name="handler">遍历过程的委托对象</param>
        public void Enumerate(ElementEnumerateEventHandler handler ,bool includeSelfNode )
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            ElementEnumerateEventArgs args = new ElementEnumerateEventArgs();
            if (includeSelfNode)
            {
                args._Parent = this.Parent;
                args._Element = this;
                handler(this, args);
                if (args.Cancel || args.CancelChild)
                {
                    return;
                }
            }
            MyEnumerate(handler, args);
        }

        private void MyEnumerate( ElementEnumerateEventHandler handler , ElementEnumerateEventArgs args )
        {
            foreach (DomElement element in this.Elements)
            {
                args._Parent = this;
                args._Element = element;
                args.CancelChild = false;
                handler( this , args);
                if (args.Cancel )
                {
                    break;
                }
                if (args.CancelChild == false )
                {
                    if (element is DomContainerElement)
                    {
                        ((DomContainerElement)element).MyEnumerate(handler , args );
                        if (args.Cancel)
                        {
                            break;
                        }
                    }
                }
                args.CancelChild = false;
            }
        }

        public override void RefreshSize(DocumentPaintEventArgs args)
        {
            if (this.Elements != null)
            {
                foreach (DomElement e in this.Elements)
                {
                    e.RefreshSize(args);
                }
            }
        }

        #region 事件处理 ************************************************

        ///// <summary>
        ///// 处理文档事件
        ///// </summary>
        ///// <param name="args">事件参数</param>
        //public override void HandleDocumentEvent(DocumentEventArgs args)
        //{
        //    switch (args.Style)
        //    {
        //        case DocumentEventStyles.GotFocus :
        //            // 触发获得焦点事件
        //            OnGotFocus(EventArgs.Empty);
        //            break;
        //        case  DocumentEventStyles.LostFocus :
        //            // 触发失去焦点事件
        //            OnLostFocus(EventArgs.Empty);
        //            break;
        //        case DocumentEventStyles.MouseEnter :
        //            // 触发鼠标进入事件
        //            OnMouseEnter(EventArgs.Empty);
        //            break;
        //        case DocumentEventStyles.MouseLeave :
        //            // 触发鼠标离开事件
        //            OnMouseLeave(EventArgs.Empty);
        //            break;
        //        default :
        //            base.HandleDocumentEvent(args);
        //            break;
        //    }
        //}


        /// <summary>
        /// 以冒泡方式出发文档容器元素的OnContentChanging事件
        /// </summary>
        /// <param name="args">事件参数</param>
        public void RaiseBubbleOnContentChanging(ContentChangingEventArgs args)
        {
            DomContainerElement parent = this;
            while (parent != null)
            {
                parent.OnContentChanging(args);
                if (args.CancelBubble)
                {
                    break;
                }
                parent = parent.Parent;
            }
        }

        /// <summary>
        /// 触发内容正在改变事件
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        public virtual void OnContentChanging( ContentChangingEventArgs args)
        {
            if (this.Events != null && this.Events.HasContentChanging )
            {
                this.Events.RaiseContentChanging(this , args);
            }
            //if (args.Cancel == false && this.OwnerDocument != null )
            //{
            //    // 触发文档全局内容修改前事件
            //    this.OwnerDocument.OnGlobalContentChanging(this, args);
            //}
        }

        /// <summary>
        /// 以冒泡方式出发文档容器元素的OnContentChanged事件
        /// </summary>
        /// <param name="args">事件参数</param>
        public void RaiseBubbleOnContentChanged(ContentChangedEventArgs args)
        {
            DomContainerElement parent = this;
            while (parent != null)
            {
                parent.OnContentChanged(args);
                if (args.CancelBubble)
                {
                    break;
                }
                parent = parent.Parent;
            }
        }

        /// <summary>
        /// 触发内容已经改变事件
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        public virtual void OnContentChanged(ContentChangedEventArgs args)
        {
            if (this.Events != null && this.Events.HasContentChanged)
            {
                this.Events.RaiseContentChanged(this, args);
            }
            //if (this.OwnerDocument != null)
            //{
            //    // 触发文档全局的内容修改后事件
            //    this.OwnerDocument.OnGlobalContentChanged(this, args);
            //}
        }


        /// <summary>
        /// 判断元素是否获得输入焦点
        /// </summary>
        [Browsable( false )]
        public override bool Focused
        {
            get
            {
                DomDocumentContentElement dce = this.DocumentContentElement ;
                if (this.OwnerDocument.CurrentContentElement == dce)
                {
                    if (dce == this)
                    {
                        // 文档级容器对象一直是拥有焦点的
                        return true;
                    }
                    DomElement element = dce.CurrentElement;
                    while (element != null)
                    {
                        if (this.FirstContentElement != element)
                        {
                            if (element == this)
                            {
                                
                                return true;
                            }
                        }
                        element = element.Parent;
                    }//while
                }
                return false ;
            }
        }

        /// <summary>
        /// 获得输入焦点
        /// </summary>
        public override void Focus()
        {
            DomElement firstElement = this.FirstContentElement;
            if (firstElement != null)
            {
                DomDocumentContentElement dce = this.DocumentContentElement;
                dce.SetSelection(firstElement.ViewIndex, 0);
            }
        }


        ///// <summary>
        ///// 返回文本内容，不包含被逻辑删除的部分。
        ///// </summary>
        //[Browsable(false)]
        //[XmlIgnore()]
        //public virtual string TextWithoutLogicDeleted
        //{
        //    get
        //    {
        //        StringBuilder str = new StringBuilder();
        //        if (this.Elements != null)
        //        {
        //            foreach (XTextElement element in this.Elements)
        //            {
        //                if (element.Visible)
        //                {
        //                    if (element.Style.DeleterIndex < 0)
        //                    {
        //                        if (element is XTextContainerElement)
        //                        {
        //                            str.Append(((XTextContainerElement)element).Text);
        //                        }
        //                        else
        //                        {
        //                            str.Append(element.Text);
        //                        }
        //                    }
        //                }
        //            }//foreach
        //        }
        //        return str.ToString();
        //    }
        //}

        /// <summary>
        /// 返回文本内容，不包含被逻辑删除的部分。
        /// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get
            {
                StringBuilder str = new StringBuilder();
                if (this.Elements != null)
                {
                    foreach (DomElement element in this.Elements)
                    {
                        if (element.Visible)
                        {
                            if (element.Style.DeleterIndex < 0)
                            {
                                if (element is DomContainerElement)
                                {
                                    str.Append(((DomContainerElement)element).Text );
                                }
                                else
                                {
                                    str.Append(element.Text);
                                }
                            }
                        }
                    }//foreach
                }
                return str.ToString();

                //StringBuilder str = new StringBuilder();
                //foreach (XTextElement element in this.Elements)
                //{
                //    if (element.Visible)
                //    {
                //        str.Append(element.Text);
                //    }
                //}
                //return str.ToString();
            }
            set
            {
                if (this.Parent == null)
                {
                    this.SetInnerTextFast(value);
                    return;
                }
                if (string.IsNullOrEmpty(value))
                {
                    ReplaceElementsArgs args = new ReplaceElementsArgs(
                            this,
                            0,
                            this.Elements.Count,
                            null,
                            true,
                            true,
                            true);
                    args.ChangeSelection = false;
                    this.OwnerDocument.ReplaceElements(args);
                }
                else
                {
                    DomElementList list = this.OwnerDocument.CreateTextElements(value, null, this.Style);
                    if (list != null && list.Count > 0)
                    {
                        ReplaceElementsArgs args = new ReplaceElementsArgs(
                            this,
                            0,
                            this.Elements.Count,
                            list,
                            true,
                            true,
                            true);
                        args.AccessFlags = DomAccessFlags.None;
                        args.ChangeSelection = false;
                        this.OwnerDocument.ReplaceElements(args);
                    }
                }
            }
        }

        /// <summary>
        /// 在编辑器中设置获得对象文本值,这个操作会被系统记录，能进行重复和撤销操作。
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public string EditorText
        {
            get
            {
                return this.Text ;
            }
            set
            {
                if (this.OwnerDocument != null)
                {
                    string oldText = this.Text;
                    if (oldText == null || oldText.ToString() != value)
                    {
                        // 为了提高效率，只有出现不同的文本才进行文本替换操作
                        this.OwnerDocument.BeginLogUndo();

                        //XTextElementList list = this.OwnerDocument.CreateTextElements(value, null, this.Style);
                        //if (list != null && list.Count > 0)
                        //{
                        //    XTextDocument.ReplaceElementsArgs args = new XTextDocument.ReplaceElementsArgs(
                        //        this,
                        //        0,
                        //        this.Elements.Count,
                        //        list,
                        //        true,
                        //        true,
                        //        true);
                        //    args.AccessFlags = DomAccessFlags.None ;
                        //    this.OwnerDocument.ReplaceElements(args);
                        //}

                        this.Text = value;
                        this.OwnerDocument.EndLogUndo();
                        this.OwnerDocument.OnSelectionChanged();
                        this.OwnerDocument.OnDocumentContentChanged();
                    }
                }
                else
                {
                    this.SetInnerTextFast(value);
                }
            }
        }

        /// <summary>
        /// 在编辑器中设置/获得对象文本值,这个操作会被系统记录，能进行重复和撤销操作,而且不受用户界面层只读的限制。
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public string EditorTextExt
        {
            get
            {
                return this.Text ;
            }
            set
            {
                SetEditorTextExt(value , DomAccessFlags.Normal , false );
            }
        }

        
        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="newText">新文本</param>
        /// <param name="flags">标记</param>
        /// <param name="disablePermissioin">禁止权限控制</param>
        /// <returns>操作是否修改了对象内容</returns>
        public bool SetEditorTextExt(string newText, DomAccessFlags flags, bool disablePermissioin)
        {
            return SetEditorTextExt(newText, flags, disablePermissioin, true);
        }

        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="newText">新文本</param>
        /// <param name="flags">标记</param>
        /// <param name="disablePermissioin">禁止权限控制</param>
        /// <returns>操作是否修改了对象内容</returns>
        public bool SetEditorTextExt(string newText , DomAccessFlags flags , bool disablePermissioin , bool updateContent )
        {
            bool result = false;
            if (this.OwnerDocument != null)
            {
                string oldText = this.Text;
                if (oldText == null || oldText != newText)
                {
                    // 为了提高效率，只有出现不同的文本才进行文本替换操作
                    bool innerLogUndo = this.OwnerDocument.CanLogUndo;
                    if (innerLogUndo == false)
                    {
                        this.OwnerDocument.BeginLogUndo();
                    }
                    if (string.IsNullOrEmpty(newText))
                    {
                        // 直接设置空内容
                        ReplaceElementsArgs args = new ReplaceElementsArgs(
                                this,
                                0,
                                this.Elements.Count,
                                null,
                                true,
                                true,
                                true);
                        args.DisablePermission = disablePermissioin;
                        args.AccessFlags = flags;
                        args.ChangeSelection = false;
                        args.UpdateContent = updateContent;
                        if (args.DisablePermission == false
                                && this.OwnerDocument.Options.SecurityOptions.EnablePermission)
                        {
                            // 很多时候，本属性是为下拉列表文本输入域调用的，此时输入域前面的文本可能是被
                            // 当前用户输入的，后面的元素已经被逻辑删除了，此时应该应该是执行物理删除，
                            // 在此进行判断，修正删除区间。
                            for (int iCount = 0; iCount < this.Elements.Count; iCount++)
                            {
                                DomElement element = this.Elements[iCount];
                                if (element.Style.DeleterIndex >= 0)
                                {
                                    bool fix = true;
                                    for (int iCount2 = iCount; iCount2 < this.Elements.Count; iCount2++)
                                    {
                                        if (this.Elements[iCount2].Style.DeleterIndex < 0)
                                        {
                                            fix = false;
                                            break;
                                        }
                                    }
                                    if (fix)
                                    {
                                        args.DeleteLength = iCount;
                                    }
                                    break;
                                }
                            }//for
                        }
                        result = this.OwnerDocument.ReplaceElements(args) != 0;
                    }
                    else
                    {
                        // 设置文本内容
                        DomElementList list = this.OwnerDocument.CreateTextElements(newText, null, this.Style);
                        if (list != null && list.Count > 0)
                        {
                            ReplaceElementsArgs args = new ReplaceElementsArgs(
                                this,
                                0,
                                this.Elements.Count,
                                list,
                                true,
                                true,
                                true);
                            args.DisablePermission = disablePermissioin;
                            args.AccessFlags = flags;
                            args.ChangeSelection = false;
                            args.UpdateContent = updateContent;
                            if (args.DisablePermission == false
                                && this.OwnerDocument.Options.SecurityOptions.EnablePermission)
                            {
                                // 很多时候，本属性是为下拉列表文本输入域调用的，此时输入域前面的文本可能是被
                                // 当前用户输入的，后面的元素已经被逻辑删除了，此时应该应该是执行物理删除，
                                // 在此进行判断，修正删除区间。
                                for (int iCount = 0; iCount < this.Elements.Count; iCount++)
                                {
                                    DomElement element = this.Elements[iCount];
                                    if (element.Style.DeleterIndex >= 0)
                                    {
                                        bool fix = true;
                                        for (int iCount2 = iCount; iCount2 < this.Elements.Count; iCount2++)
                                        {
                                            if (this.Elements[iCount2].Style.DeleterIndex < 0)
                                            {
                                                fix = false;
                                                break;
                                            }
                                        }
                                        if (fix)
                                        {
                                            args.DeleteLength = iCount;
                                        }
                                        break;
                                    }
                                }//for
                            }
                            result = this.OwnerDocument.ReplaceElements(args) != 0;
                        }
                    }
                    if (innerLogUndo == false)
                    {
                        if (result)
                        {
                            this.OwnerDocument.EndLogUndo();
                        }
                        else
                        {
                            this.OwnerDocument.CancelLogUndo();
                        }
                    }
                    if (result)
                    {
                        this.OwnerDocument.OnDocumentContentChanged();
                        this.OwnerDocument.OnSelectionChanged();
                    }
                }
            }
            else
            {
                DomElementList list = this.SetInnerTextFast(newText);
                result = list != null && list.Count > 0;
            }
            return result;
        }

        /// <summary>
        /// 快速设置元素的文本内容
        /// </summary>
        /// <param name="txt">文本</param>
        /// <returns>创建的元素对象列表</returns>
        public DomElementList SetInnerTextFast(string txt)
        {
            if (this.Elements == null)
            {
                this.Elements = new DomElementList();
            }
            DomElementList list = this.OwnerDocument.CreateTextElements(txt, null, this.Style);
            if (list != null && list.Count > 0)
            {
                this.Elements.Clear();
                foreach (DomElement e in list)
                {
                    e.Parent = this;
                    e.OwnerDocument = this.OwnerDocument;
                    this.Elements.Add(e);
                }
            }
            return list;
        }

        #endregion

        private bool _Visible = true;
        /// <summary>
        ///  元素是否可见
        /// </summary>
        [Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public override bool Visible
        {
            get
            {
                if (this.OwnerDocument != null)
                {
                    return _Visible && this.OwnerDocument.IsVisible(this);
                }
                else
                {
                    return _Visible;
                }
            }
            set
            {
                _Visible = value;
            }
        }

        ///// <summary>
        ///// 设置容器元素的可见性
        ///// </summary>
        ///// <param name="visible">新的可见性</param>
        ///// <returns>操作是否成功</returns>
        //public bool EditorSetVisible(bool visible)
        //{
        //    bool result = false;
        //    bool oldVisible = this.Visible;
        //    if( oldVisible != visible )
        //    {
        //        this.Visible = visible;
        //        bool visible2 = this.Visible;
        //        if (visible2 == visible)
        //        {
        //            // 成功的修改了元素的可见性
        //            if (visible)
        //            {
        //                this.OwnerDocument.HighlightManager.InvalidateHighlightInfo(this);
        //            }
        //            else
        //            {
        //                this.OwnerDocument.HighlightManager.Remove(this);
        //            }
        //            result = true;
        //            XTextElement fc = this.FirstContentElement;
        //            XTextElement lc = this.LastContentElement;
        //            XTextContentElement content = this.ContentElement;
        //            int startIndex = 0;
        //            if (oldVisible)
        //            {
        //                startIndex = content.PrivateContent.IndexOf(fc);
        //            }
        //            this.UpdateContentVersion();
        //            content.UpdateContentElements(true);
        //            if (oldVisible == false)
        //            {
        //                startIndex = content.PrivateContent.IndexOf(fc);
        //            }
        //            content.RefreshPrivateContent(startIndex, -1, false);
        //        }
        //    }
        //    return result;
        //}

        /// <summary>
        /// 声明容器元素内容无效,需要重新排版
        /// </summary>
        public void EditorInvalidateContent()
        {
            DomContentElement ce = this.ContentElement;
             
            DomElement currentElementBack = this.DocumentContentElement.CurrentElement;
            //XTextElementList contentElements = new XTextElementList();
            //this.AppendContent(contentElements , true );

            DomElement firstElement = ce.PrivateContent.GetPreElement(this.FirstContentElement);
            if (firstElement == null)
            {
                firstElement = this.FirstContentElement;
            }
            DomElement lastElement = ce.PrivateContent.GetNextElement(this.LastContentElement);
            if (lastElement == null)
            {
                lastElement = ce.PrivateContent.LastElement;
            }
            // 声明所经过的文本行无效
            int endIndex = ce.PrivateContent.IndexOf(lastElement);
            for (int iCount = ce.PrivateContent.IndexOf(firstElement); iCount <= endIndex; iCount++)
            {
                if (ce.PrivateContent[iCount].OwnerLine != null)
                {
                    ce.PrivateContent[iCount].OwnerLine.InvalidateState = true;
                }
            }
            this.UpdateContentVersion();
            ce.UpdateContentElements(true);
            ce.RefreshPrivateContent(
                        ce.PrivateContent.IndexOf( firstElement ),
                        ce.PrivateContent.IndexOf( lastElement ) ,
                        false);
            // 确认新的插入点的位置
            DomDocumentContentElement dce = this.DocumentContentElement;
            dce.RefreshGlobalLines();
            if (currentElementBack != null)
            {
                dce.Content.AutoClearSelection = true;
                dce.Content.LineEndFlag = false;

                int newSelectionPosition = currentElementBack.ViewIndex;
                dce.Content.MoveSelectStart(newSelectionPosition);
            }
        }

        /// <summary>
        /// 在编辑器中删除整个对象
        /// </summary>
        /// <param name="logUndo">是否记录撤销操作信息</param>
        public bool EditorDelete(bool logUndo)
        {
            if (this is DomDocumentContentElement)
            {
                return false ;
            }

            DomContainerElement container = this.Parent;
            int index = container.Elements.IndexOf(this);
            DomDocument document = this.OwnerDocument;
            if (logUndo)
            {
                document.BeginLogUndo();
            }
            int result = document.ReplaceElements(new ReplaceElementsArgs(
                container,
                index,
                1,
                null,
                logUndo,
                true,
                true));
            if (logUndo)
            {
                document.EndLogUndo();
            }
            return result != 0;
        }

        /// <summary>
        /// 设置文档内容的样式
        /// </summary>
        /// <param name="newStyle">新样式</param>
        /// <param name="logUndo">是否记录撤销操作信息</param>
        public virtual void EditorSetContentStyle(DocumentContentStyle newStyle, bool logUndo)
        {
            if (newStyle == null)
            {
                throw new ArgumentNullException("newStyle");
            }
            DomElementList list = new DomElementList();
            list.Add(this);
            this.AppendContent(list, true);
            if (logUndo)
            {
                this.OwnerDocument.BeginLogUndo();
            }
            bool result = DomSelection.SetElementStyle(newStyle, this.OwnerDocument, list);
            if (logUndo)
            {
                this.OwnerDocument.EndLogUndo();
            }
            if (result)
            {
                this.OwnerDocument.EditorCurrentStyle = null;
                this.OwnerDocument.OnSelectionChanged();
                this.OwnerDocument.OnDocumentContentChanged();
            }
        }

        /// <summary>
        /// 修复DOM结构状态
        /// </summary>
        public override void FixDomState()
        {
            base.FixDomState();
            foreach (DomElement element in this.Elements)
            {
                element.FixDomState();
            }
            this._ElementsForSerialize = null;
        }
         
    }//public class XTextElementContainer : XTextElement
}