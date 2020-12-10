/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using DCSoft.CSharpWriter.Undo;
using DCSoft.CSharpWriter;
using DCSoft.CSharpWriter.Dom;

namespace DCSoft.CSharpWriter.Dom.Undo
{
	/// <summary>
	/// 撤销操作类型
	/// </summary>
	public enum XTextUndoStyles
	{
		/// <summary>
		/// 无样式
		/// </summary>
		None ,
        /// <summary>
        /// 设计器中的元素大小
        /// </summary>
        EditorSize ,
        /// <summary>
        /// 表格行高度
        /// </summary>
        TableRowSpecifyHeight,
        /// <summary>
        /// 创建者用户编号
        /// </summary>
        CreatorIndex,
        /// <summary>
        /// 删除者用户编号
        /// </summary>
        DeleterIndex ,
        ///// <summary>
        ///// 表格列宽度
        ///// </summary>
        //TableColumnWidth 
        ///// <summary>
        ///// 设置字体
        ///// </summary>
        //Font ,
        ///// <summary>
        ///// 字体名称
        ///// </summary>
        //FontName ,
        ///// <summary>
        ///// 字体大小
        ///// </summary>
        //FontSize ,
        ///// <summary>
        ///// 粗体
        ///// </summary>
        //FontBold ,
        ///// <summary>
        ///// 斜体
        ///// </summary>
        //FontItalic ,
        ///// <summary>
        ///// 下划线
        ///// </summary>
        //FontUnderline ,
        ///// <summary>
        ///// 删除线
        ///// </summary>
        //FontStrikeout ,
        ///// <summary>
        ///// 文本颜色
        ///// </summary>
        //ForeColor ,
        ///// <summary>
        ///// 背景色
        ///// </summary>
        //BackColor ,
        ///// <summary>
        ///// 编辑器中对象大小
        ///// </summary>
        //EditorSize ,
        ///// <summary>
        ///// 文本对齐方式
        ///// </summary>
        //Align ,
        ///// <summary>
        ///// 段落列表方式
        ///// </summary>
        //ParagraphListStyle ,
        ///// <summary>
        ///// 段落首行缩进
        ///// </summary>
        //FirstLineIndent ,
        ///// <summary>
        ///// 段落整体左缩进量
        ///// </summary>
        //LeftIndent ,
        ///// <summary>
        ///// 设置文本标记样式
        ///// </summary>
        //TextScriptStyle ,
        ///// <summary>
        ///// 设置超链接
        ///// </summary>
        //Link ,
        ///// <summary>
        ///// 段落设置
        ///// </summary>
        //ParagraphSettings
	}

    /// <summary>
    /// 重复/撤销设置元素的样式编号
    /// </summary>
    public class XTextUndoStyleIndex : XTextUndoBase
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XTextUndoStyleIndex()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="element">操作的文档元素</param>
        /// <param name="oldStyleIndex">旧样式编号</param>
        /// <param name="newStyleIndex">新样式编号</param>
        public XTextUndoStyleIndex(DomElement element, int oldStyleIndex, int newStyleIndex)
        {
            _Element = element;
            _OldStyleIndex = oldStyleIndex;
            _NewStyleIndex = newStyleIndex;
        }

        private DomElement _Element = null;

        public DomElement Element
        {
            get { return _Element; }
            set { _Element = value; }
        }

        private int _OldStyleIndex = -1;

        public int OldStyleIndex
        {
            get { return _OldStyleIndex; }
            set { _OldStyleIndex = value; }
        }

        private int _NewStyleIndex = -1;

        public int NewStyleIndex
        {
            get { return _NewStyleIndex; }
            set { _NewStyleIndex = value; }
        }

        public override void Undo(XUndoEventArgs args)
        {
            _Element.StyleIndex = _OldStyleIndex;
            this.OwnerList.RefreshElements.Add(_Element);
        }

        public override void Redo(XUndoEventArgs args)
        {
            _Element.StyleIndex = _NewStyleIndex;
            this.OwnerList.RefreshElements.Add(_Element);
        }


    }
	/// <summary>
	/// 重复/撤销设置元素属性的信息对象
	/// </summary>
	public class XTextUndoProperty : XTextUndoBase
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public XTextUndoProperty()
		{
		}

       
		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="style">动作类型</param>
		/// <param name="vOldValue">旧数值</param>
		/// <param name="vNewValue">新数值</param>
		/// <param name="element">元素对象</param>
		public XTextUndoProperty( 
			XTextUndoStyles style , 
			object vOldValue , 
			object vNewValue , 
			DomElement element )
		{
			this.intStyle = style ;
			this.objOldValue = vOldValue ;
			this.objNewValue = vNewValue ;
			this.myElement = element ;
		}
		/// <summary>
		/// 旧数值
		/// </summary>
		protected object objOldValue = null;
		/// <summary>
		/// 旧数值
		/// </summary>
		public object OldValue
		{
			get{ return objOldValue ;}
			set{ objOldValue = value;}
		}

		/// <summary>
		/// 新数值
		/// </summary>
		protected object objNewValue = null;
		/// <summary>
		/// 新数值
		/// </summary>
		public object NewValue
		{
			get{ return objNewValue ;}
			set{ objNewValue = value;}
		}

		/// <summary>
		/// 元素对象
		/// </summary>
		protected DomElement myElement = null;
		/// <summary>
		/// 元素对象
		/// </summary>
		public DomElement Element
		{
			get{ return myElement ;}
			set{ myElement = value;}
		}

		/// <summary>
		/// 样式
		/// </summary>
		protected XTextUndoStyles intStyle = 0 ;
		/// <summary>
		/// 样式
		/// </summary>
		public XTextUndoStyles Style
		{
			get{ return intStyle ;}
			set{ intStyle = value;}
		}

		/// <summary>
		/// 动作执行标记,若执行了动作,使得元素状态发生改变,
		/// 则该属性为 true , 若执行动作没有产生任何修改则该属性为 false 
		/// </summary>
		protected bool bolExecuteFlag = false;
		/// <summary>
		/// 动作执行标记,若执行了动作,使得元素状态发生改变,
		/// 则该属性为 true , 若执行动作没有产生任何修改则该属性为 false 
		/// </summary>
		public bool ExecuteFlag
		{
			get
            {
                return bolExecuteFlag ;
            }
			set
            {
                bolExecuteFlag  = value;
            }
		}

		/// <summary>
		/// 执行撤销操作
		/// </summary>
        public override void Undo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
		{
			Execute( true );
		}
		/// <summary>
		/// 执行重复操作
		/// </summary>
        public override void Redo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
		{
			Execute( false );
		}

		/// <summary>
		/// 执行动作
		/// </summary>
		/// <param name="undo">是否是撤销操作 true:撤销操作 false:重复操作</param>
		/// <returns>操作是否成功</returns>
		public virtual void Execute( bool undo )
		{
			bolExecuteFlag = false;
            switch (intStyle)
            {
                case XTextUndoStyles.EditorSize:
                    // 设置元素大小
                    {
                        System.Drawing.SizeF OldSize = (System.Drawing.SizeF)objOldValue;
                        System.Drawing.SizeF NewSize = (System.Drawing.SizeF)objNewValue;
                        if (OldSize.Width > 0
                            && OldSize.Height > 0
                            && NewSize.Width > 0
                            && NewSize.Height > 0
                            && OldSize.Equals(NewSize) == false)
                        {
                            if (undo)
                            {
                                myElement.EditorSize = OldSize;
                            }
                            else
                            {
                                myElement.EditorSize = NewSize;
                            }
                            myElement.SizeInvalid = true;
                            bolExecuteFlag = true;
                        }
                    }
                    break;
                 
                case XTextUndoStyles.CreatorIndex:
                    {
                        // 设置元素的创建者编号
                        int oldIndex = (int)objOldValue;
                        int newIndex = (int)objNewValue;
                        if (oldIndex != newIndex && myElement != null)
                        {
                            DocumentContentStyle style = (DocumentContentStyle)myElement.RuntimeStyle.Clone();
                            style.DisableDefaultValue = true;
                            if (undo)
                            {
                                style.CreatorIndex = oldIndex;
                            }
                            else
                            {
                                style.CreatorIndex = newIndex;
                            }
                            myElement.StyleIndex = myElement.OwnerDocument.ContentStyles.GetStyleIndex(style);
                            myElement.SizeInvalid = true;
                            bolExecuteFlag = true;
                        }
                    }
                    break;
                case XTextUndoStyles.DeleterIndex:
                    {
                        // 设置元素的创建者编号
                        int oldIndex = (int)objOldValue;
                        int newIndex = (int)objNewValue;
                        if (oldIndex != newIndex && myElement != null)
                        {
                            DocumentContentStyle style = (DocumentContentStyle)myElement.RuntimeStyle.Clone();
                            style.DisableDefaultValue = true;
                            if (undo)
                            {
                                style.DeleterIndex = oldIndex;
                            }
                            else
                            {
                                style.DeleterIndex = newIndex;
                            }
                            myElement.StyleIndex = myElement.OwnerDocument.ContentStyles.GetStyleIndex(style);
                            myElement.SizeInvalid = true;
                            bolExecuteFlag = true;
                        }
                    }
                    break;
            }
            if (bolExecuteFlag)
            {
                this.OwnerList.RefreshElements.Add(myElement.FirstContentElement);
                this.OwnerList.RefreshElements.Add(myElement.LastContentElement);
            }
		}
	}
}