/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using DCSoft.CSharpWriter.Undo;
using System.Collections.Generic;
using System.Collections;

namespace DCSoft.CSharpWriter.Dom.Undo
{
	/// <summary>
	/// 撤销操作列表
	/// </summary>
	public class XTextDocumentUndoList : XUndoList
	{
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="document"></param>
        public XTextDocumentUndoList(DomDocument document)
        {
            this.Document = document ;
        }

        protected override void OnClear()
        {
            if (this.Count > 0)
            {
                base.OnClear();
            }
        }

		/// <summary>
		/// 强制使用撤销对象组操作
		/// </summary>
		protected override bool ForceUseGroup
		{
			get
			{
				return true ;
			}
		}

		/// <summary>
		/// 创建一个撤销对象组对象
		/// </summary>
		/// <returns>创建的对象</returns>
		protected override XUndoGroup CreateGroup()
		{
			XTextDocumentUndoGroup group = new XTextDocumentUndoGroup( myDocument );
			group.OldSelectionStart = this.intOldSelectionStart ;
			group.OldSelectionLength = this.intOldSelectionLength ;
			group.NewSelectionStart = this.intNewSelectionStart ;
			group.NewSelectionLength = this.intNewSelectionLength ;
			return group ;
		}

        private bool _NeedRefreshDocument = false;
        /// <summary>
        /// 是否需要刷新整个文档
        /// </summary>
        public bool NeedRefreshDocument
        {
            get { return _NeedRefreshDocument; }
            set { _NeedRefreshDocument = value; }
        }

		private DomElementList myRefreshElements = new DomElementList();
		/// <summary>
		/// 状态发生改变的元素列表
		/// </summary>
		internal DomElementList RefreshElements
		{
			get{ return myRefreshElements ;}
		}

        private DomElementList _ContentChangedContainer = new DomElementList();
        /// <summary>
        /// 内容发生改变的容器元素对象
        /// </summary>
        public DomElementList ContentChangedContainer
        {
            get { return _ContentChangedContainer; }
        }

        internal Dictionary<DomContentElement, int> _ContentRefreshInfos = new Dictionary<DomContentElement, int>();

        public void AddContentRefreshInfo(DomContentElement contentElement, int startIndex)
        {
            if (_ContentRefreshInfos.ContainsKey(contentElement) )
            {
                _ContentRefreshInfos[contentElement] = Math.Min(_ContentRefreshInfos[contentElement], startIndex);
            }
            else
            {
                _ContentRefreshInfos[contentElement] = startIndex;
            }
        }

        //private XTextContentElement _EffectContentElement = null;
        ///// <summary>
        ///// 操作涉及到的文档元素对象
        ///// </summary>
        //public XTextContentElement EffectContentElement
        //{
        //    get
        //    {
        //        return _EffectContentElement; 
        //    }
        //    set
        //    {
        //        _EffectContentElement = value; 
        //    }
        //}

        //private int intRefreshStartIndex = int.MinValue  ;
        ///// <summary>
        ///// 文档重新刷新的开始序号
        ///// </summary>
        //internal int RefreshStartIndex 
        //{
        //    get
        //    {
        //        return intRefreshStartIndex ;
        //    }
        //    set
        //    {
        //        if (intRefreshStartIndex == int.MinValue)
        //        {
        //            intRefreshStartIndex = value;
        //        }
        //        else if (intRefreshStartIndex > value)
        //        {
        //            intRefreshStartIndex = value;
        //        }
        //    }
        //}
		/// <summary>
		/// 开始登记操作时的文档选择区域开始序号
		/// </summary>
		private int intOldSelectionStart = 0 ;
		/// <summary>
		/// 开始登记操作时的文档选择区域长度
		/// </summary>
		private int intOldSelectionLength = 0 ;
		/// <summary>
		/// 结束登记操作时的文档选择区域开始序号
		/// </summary>
		private int intNewSelectionStart = 0 ;
		/// <summary>
		/// 结束登记操作时的文档选择区域长度
		/// </summary>
		private int intNewSelectionLength = 0 ;
	
		/// <summary>
		/// 开始登记撤销信息
		/// </summary>
		/// <returns>操作是否成功</returns>
		public override bool BeginLog()
		{
			if( base.BeginLog())
			{
				myRefreshElements.Clear();
                intOldSelectionStart = this.Document.CurrentContentElement.Selection.StartIndex;
                intOldSelectionLength = this.Document.CurrentContentElement.Selection.Length;
				return true ;
			}
			return false ;
		}
        /// <summary>
        /// 取消登记撤销信息操作
        /// </summary>
        public override void CancelLog()
        {
            base.CancelLog();
            myRefreshElements.Clear();
        }

		/// <summary>
		/// 结束登记撤销信息
		/// </summary>
		public override bool EndLog()
		{
			myRefreshElements.Clear();
			//this.intRefreshStartIndex = int.MinValue ;
            if (this.Document != null
                && this.Document.CurrentContentElement != null
                && this.Document.CurrentContentElement.Selection != null)
            {
                intNewSelectionStart = this.Document.CurrentContentElement.Selection.StartIndex;
                intNewSelectionLength = this.Document.CurrentContentElement.Selection.Length;
            }
            else
            {
                intNewSelectionStart = 0;
                intNewSelectionLength = 0;
            }
			return base.EndLog ();
		}

        //protected override void OnStateChanged()
        //{
        //    base.OnStateChanged();
        //    if( this.Document != null && this.Document
        //}

		/// <summary>
		/// 本对象所属的文档对象
		/// </summary>
		protected DomDocument myDocument = null;
		/// <summary>
		/// 本对象所属的文档对象
		/// </summary>
		public DomDocument Document
		{
			get
            {
                return myDocument ;
            }
			set
            {
                myDocument = value;
            }
		}

		/// <summary>
		/// 添加一个插入元素操作信息
		/// </summary>
		/// <param name="c">容器元素</param>
		/// <param name="index">插入的序号</param>
		/// <param name="element">插入的元素</param>
		public void AddInsertElement( DomContainerElement c , int index , DomElement element )
		{
			if( CanLog )
			{
                DomElementList list = new DomElementList();
                list.Add( element );
                XTextUndoReplaceElements undo = new XTextUndoReplaceElements( c , index , null , list );
                undo.Document = this.Document;
                undo.InGroup = true;
                this.Add(undo);
                //XTextUndoInsertElement undo = new XTextUndoInsertElement();
                //undo.Document = this.myDocument ;
                //undo.Element = element ;
                //undo.Container = c ;
                //undo.Index = index ;
                //this.Add( undo );
			}
		}
		/// <summary>
		/// 添加删除一个元素的操作信息
		/// </summary>
		/// <param name="c">容器元素</param>
		/// <param name="index">子元素在容器元素中的序号</param>
		/// <param name="element">子元素对象</param>
		public void AddRemoveElement( DomContainerElement c , int index , DomElement element )
		{
			if( CanLog )
			{
                DomElementList list = new DomElementList();
                list.Add(element);
                XTextUndoReplaceElements undo = new XTextUndoReplaceElements(c, index, list, null);
                undo.Document = this.Document;
                undo.InGroup = true;
                this.Add(undo);

                //XTextUndoRemoveElement undo = new XTextUndoRemoveElement();
                //undo.Document = this.myDocument ;
                //undo.Element = element ;
                //undo.Container = c ;
                //undo.Index = index ;
                //this.Add( undo );
			}
		}
		
		/// <summary>
		/// 添加一个插入多个元素的撤销信息
		/// </summary>
		/// <param name="c">容器对象</param>
		/// <param name="index">开始插入区域序号</param>
		/// <param name="list">插入的元素</param>
		public void AddInsertElements( 
			DomContainerElement c ,
			int index ,
			DomElementList list )
		{
			if( CanLog )
			{
                XTextUndoReplaceElements undo = new XTextUndoReplaceElements(c, index, null, list);
                undo.Document = this.Document;
                undo.InGroup = true;
                this.Add(undo);

                //XTextUndoInsertElements undo = new XTextUndoInsertElements( );
                //undo.Document = this.myDocument ;
                //undo.Container = c ;
                //undo.Index = index ;
                //undo.Items.AddRange( list );
                //this.Add( undo );
			}
		}

		/// <summary>
		/// 添加一个删除多个元素的撤销信息
		/// </summary>
		/// <param name="c">容器对象</param>
		/// <param name="index">删除区域开始编号</param>
		/// <param name="list">删除的元素</param>
		public void AddRemoveElements(
			DomContainerElement c ,
			int index ,
			DomElementList list )
		{
			if( CanLog )
			{
                XTextUndoReplaceElements undo = new XTextUndoReplaceElements(c, index, list, null);
                undo.Document = this.Document;
                undo.InGroup = true;
                this.Add(undo);

                //XTextUndoRemoveElements undo = new XTextUndoRemoveElements( );
                //undo.Document = this.myDocument ;
                //undo.Container = c ;
                //undo.Index = index ;
                //undo.Items.AddRange( list );
                //this.Add( undo );
			}
		}

        /// <summary>
        /// 添加一个替换多个元素的撤销信息
        /// </summary>
        /// <param name="container">容器对象</param>
        /// <param name="index">操作区域开始编号</param>
        /// <param name="oldElements">旧元素列表</param>
        /// <param name="newElements">新元素列表</param>
        public void AddReplaceElements(
            DomContainerElement container,
            int index, 
            DomElementList oldElements,
            DomElementList newElements)
        {
            if (this.CanLog)
            {
                XTextUndoReplaceElements undo = new XTextUndoReplaceElements(
                    container, 
                    index,
                    oldElements,
                    newElements);
                undo.Document = this.Document;
                undo.InGroup = true;
                this.Add(undo);
            }
        }

        /// <summary>
        /// 添加一个项目
        /// </summary>
        /// <param name="element">文档元素</param>
        /// <param name="oldStyleIndex">旧的样式编号</param>
        /// <param name="newStyleIndex">新的样式编号</param>
        public void AddStyleIndex(DomElement element, int oldStyleIndex, int newStyleIndex)
        {
            XTextUndoStyleIndex undo = new XTextUndoStyleIndex(element, oldStyleIndex, newStyleIndex);
            undo.Document = myDocument;
            undo.InGroup = true;
            this.Add(undo);
        }

		/// <summary>
		/// 添加一个项目
		/// </summary>
		/// <param name="style">动作类型</param>
		/// <param name="vOldValue">旧数据</param>
		/// <param name="vNewValue">新数据</param>
		/// <param name="element">元素对象</param>
		public void AddProperty( 
			XTextUndoStyles style , 
			object vOldValue ,
			object vNewValue ,
			DomElement element )
		{
			XTextUndoProperty undo = new XTextUndoProperty(
                style ,
                vOldValue ,
                vNewValue ,
                element );
			undo.Document = myDocument ;
			undo.InGroup = true ;
			this.Add( undo );
		}

		/// <summary>
		/// 添加设置对象属性值的撤销信息
		/// </summary>
		/// <param name="PropertyName">属性名称,不区分大小写</param>
		/// <param name="OldValue">旧的属性值</param>
		/// <param name="NewValue">新的属性值</param>
		/// <param name="ObjectInstance">对象实例</param>
		public void AddProperty( 
			string PropertyName ,
			object OldValue , 
			object NewValue , 
			object ObjectInstance )
		{
			if( PropertyName == null || PropertyName.Length == 0 )
				throw new System.ArgumentNullException("PropertyName");
			if( ObjectInstance == null )
				throw new System.ArgumentNullException("ObjectInstance");
			System.Type t = ObjectInstance.GetType();
			System.Reflection.PropertyInfo p = t.GetProperty( 
				PropertyName , 
				System.Reflection.BindingFlags.IgnoreCase | 
				System.Reflection.BindingFlags.Instance | 
				System.Reflection.BindingFlags.Public );
            if (p == null)
            {
                throw new System.Exception(string.Format(WriterStrings.MissProperty_Name, PropertyName));
            }
            System.Reflection.ParameterInfo[] ps = p.GetIndexParameters();
            if (ps != null && ps.Length > 0)
            {
                throw new System.Exception(string.Format(WriterStrings.PropertyCannotHasParameter_Name, PropertyName));
            }
            if (p.CanWrite == false)
				throw new System.Exception( string.Format( WriterStrings.PropertyIsReadonly_Name , PropertyName ));
			Type pt = p.PropertyType ;
			if( OldValue != null )
			{
				Type vt = OldValue.GetType();
				if( vt.Equals( pt ) == false && vt.IsSubclassOf( pt ) == false )
					throw new System.Exception("旧数据值类型不匹配");
			}
			if( NewValue != null )
			{
				Type vt = NewValue.GetType();
				if( vt.Equals( pt ) == false && vt.IsSubclassOf( pt ) == false )
				{
					throw new System.Exception("新数值类型不匹配");
				}
			}

			XTextUndoNameProperty undo = new XTextUndoNameProperty();
			undo.Document = myDocument ;
			undo.Property = p ;
			undo.InGroup = true ;
			undo.OldValue = OldValue ;
			undo.NewValue = NewValue ;
			undo.ObjectInstance = ObjectInstance ;
			this.Add( undo );
		}
          

        ///// <summary>
        ///// 添加创建者信息
        ///// </summary>
        ///// <param name="element">文档元素对象</param>
        ///// <param name="oldCreatorIndex">旧的创建者信息编号</param>
        //public void AddCreatorIndex(XTextElement element, int oldCreatorIndex)
        //{
        //    if (element != null && element.Style.CreatorIndex != oldCreatorIndex)
        //    {
        //        this.Add(new XTextUndoProperty(
        //            XTextUndoStyles.CreatorIndex,
        //            oldCreatorIndex,
        //            element.Style.CreatorIndex ,
        //            element));
        //    }
        //}

        ///// <summary>
        ///// 添加删除者信息
        ///// </summary>
        ///// <param name="element">文档元素对象</param>
        ///// <param name="oldDeleterIndex">旧的删除者信息编号</param>
        //public void AddDeleterIndex(XTextElement element, int oldDeleterIndex)
        //{
        //    if (element != null && element.Style.DeleterIndex != oldDeleterIndex)
        //    {
        //        this.Add(new XTextUndoProperty(
        //            XTextUndoStyles.DeleterIndex,
        //            oldDeleterIndex,
        //            element.Style.DeleterIndex,
        //            element));
        //    }
        //}
         
	}


    internal class XTextDocumentUndoGroup : XUndoGroup
    {
        public XTextDocumentUndoGroup(DomDocument doc)
        {
            myDocument = doc;
        }
        /// <summary>
        /// 本对象所属的文档对象
        /// </summary>
        protected DomDocument myDocument = null;
        /// <summary>
        /// 本对象所属的文档对象
        /// </summary>
        public DomDocument Document
        {
            get { return myDocument; }
            set { myDocument = value; }
        }

        public override void Undo(XUndoEventArgs args)
        {
            Execute(args, true);
        }

        public override void Redo( XUndoEventArgs args )
        {
            Execute(args, false);
        }

        private void Execute(XUndoEventArgs args, bool undo)
        {
            myDocument.UndoList.RefreshElements.Clear();
            myDocument.UndoList._ContentRefreshInfos.Clear();
            myDocument.UndoList.ContentChangedContainer.Clear();
            myDocument.UndoList.NeedRefreshDocument = false;
            if (undo)
            {
                base.Undo(args);
            }
            else
            {
                base.Redo(args);
            }
            if (myDocument.UndoList.NeedRefreshDocument)
            {
                // 需要刷新整个文档。
                if (this.Document.EditorControl != null)
                {
                    this.Document.EditorControl.RefreshDocument();
                    return;
                }
            }

            // 指定了要刷新的元素
            DomElementList RedrawElements = new DomElementList();

            Dictionary<DomContentElement, int> startIndexs
                = new Dictionary<DomContentElement, int>();

            if (this.Document.UndoList._ContentRefreshInfos.Count > 0)
            {
                // 直接获得要刷新排版的位置信息
                DomDocumentContentElement dce2 = null;
                foreach (DomContentElement ce in this.Document.UndoList._ContentRefreshInfos.Keys)
                {
                    dce2 = ce.DocumentContentElement;
                    ce.UpdateContentElements(false );
                    startIndexs[ce] = this.Document.UndoList._ContentRefreshInfos[ce];
                }
                dce2.UpdateContentElements(false);
            }//if

            if (this.Document.UndoList.RefreshElements.Count > 0)
            {
                // 获得要刷新排版的位置信息
                foreach (DomElement element in this.Document.UndoList.RefreshElements)
                {
                    DomContentElement ce = element.ContentElement;
                    if (startIndexs.ContainsKey(ce))
                    {
                        int index = ce.PrivateContent.IndexOf(element);
                        if (index >= 0 && index < startIndexs[ ce ])
                        {
                            for (int iCount = startIndexs[ce]; iCount >= index; iCount--)
                            {
                                DomElement element2 = ce.PrivateContent[iCount];
                                if (element2.OwnerLine != null)
                                {
                                    // 声明文本行无效
                                    element2.OwnerLine.InvalidateState = true;
                                }
                            }
                            startIndexs[ce] = index;
                        }
                    }
                    else
                    {
                        ce.UpdateContentElements(true);
                        int index = ce.PrivateContent.IndexOf(element.FirstContentElement );
                        if (index >= 0)
                        {
                            startIndexs[ce] = index;
                        }
                    }
                }//foreach

                using (System.Drawing.Graphics g = this.Document.CreateGraphics())
                {
                    foreach (DomElement element in this.Document.UndoList.RefreshElements)
                    {
                        DomContentElement ce = element.ContentElement;
                        int index2 = ce.PrivateContent.IndexOf(element.FirstContentElement);
                        if (index2 >= 0)
                        {
                            //element.SizeInvalid = true;
                            if (element.SizeInvalid || element.ViewInvalid)
                            {
                                RedrawElements.Add(element);
                            }
                            if (element.SizeInvalid)
                            {
                                this.Document.Render.RefreshSize(element, g);
                            }
                            //if (StartIndex == int.MinValue || StartIndex > index2)
                            //{
                            //    StartIndex = index2;
                            //}
                            //if (EndIndex == -1 || EndIndex < index2)
                            //{
                            //    EndIndex = index2;
                            //}
                        }//if
                    }//foreach
                }//using

            }//if

            DomDocumentContentElement dce = this.Document.Body;
            foreach (DomContentElement ce in startIndexs.Keys)
            {
                dce = ce.DocumentContentElement;
                //ce.UpdateContentElements();
                int index = startIndexs[ce];
                if (index > 0)
                {
                    //index--;
                }
                ce.RefreshPrivateContent(index, -1, true);
            }//foreach
            if (this.Document.PageRefreshed == false)
            {
                this.Document.RefreshPages();
                if (this.Document.EditorControl != null)
                {
                    this.Document.EditorControl.UpdatePages();
                    this.Document.EditorControl.Invalidate();
                }
            }
            dce.Content.AutoClearSelection = true;
            dce.Content.LineEndFlag = false;
            if (undo)
            {
                dce.SetSelection(intOldSelectionStart, 0);
            }
            else
            {
                dce.SetSelection(intNewSelectionStart, 0);
            }
            //myDocument.Content.MoveSelectStart( intOldSelectionStart );
            foreach (DomElement element in RedrawElements)
            {
                element.ViewInvalid = true;
                element.InvalidateView();
            }
            if (this.Document.UndoList.ContentChangedContainer.Count > 0)
            {
                // 触发文档内容修改事件
                foreach (DomContainerElement container in this.Document.UndoList.ContentChangedContainer)
                {
                    // 触发文档事件
                    ContentChangedEventArgs args2 = new ContentChangedEventArgs();
                    args2.UndoRedoCause = true;
                    args2.Document = this.Document;
                    args2.Element = container;
                    container.RaiseBubbleOnContentChanged(args2);
                }
                this.Document.HighlightManager.UpdateHighlightInfos();
                this.Document.OnDocumentContentChanged();
            }
            if (this.Document.EditorControl != null)
            {
                this.Document.EditorControl.UpdateTextCaret();
                this.Document.EditorControl.Update();
            }
        }

        private int intOldSelectionStart = 0;
        /// <summary>
        /// 开始登记操作时的文档选择区域开始序号
        /// </summary>
        public int OldSelectionStart
        {
            get { return intOldSelectionStart; }
            set { intOldSelectionStart = value; }
        }
        private int intOldSelectionLength = 0;
        /// <summary>
        /// 开始登记操作时的文档选择区域长度
        /// </summary>
        public int OldSelectionLength
        {
            get { return intOldSelectionLength; }
            set { intOldSelectionLength = value; }
        }

        private int intNewSelectionStart = 0;
        /// <summary>
        /// 结束登记操作时的文档选择区域开始序号
        /// </summary>
        public int NewSelectionStart
        {
            get { return intNewSelectionStart; }
            set { intNewSelectionStart = value; }
        }
        private int intNewSelectionLength = 0;
        /// <summary>
        /// 结束登记操作时的文档选择区域长度
        /// </summary>
        public int NewSelectionLength
        {
            get { return intNewSelectionLength; }
            set { intNewSelectionLength = value; }
        }
    }
}