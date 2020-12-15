/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Xml.Serialization ;
using System.ComponentModel ;
using DCSoft.Drawing ;
using DCSoft.Common;
using System.Drawing;
using DCSoft.CSharpWriter.Html;
using DCSoft.CSharpWriter.Script;

namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 文档元素基础类型
	/// </summary>
	/// <remarks>
	/// 本类型是文本文档对象模型的最基础的类型,任何其他的文本文档对象类型都是从该类型
	/// 派生的,本类型定义了所有文本文档对象所需要的通用程序,并定义了一些常用例程.
	/// 编制 袁永福 2012-1-12
	/// </remarks>
    [System.Xml.Serialization.XmlType("Element")]
    [Serializable]
    [System.Runtime.InteropServices.ComVisible(true)]
    public abstract class DomElement : IXDependencyPropertyLoggable, IDisposable
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public DomElement()
		{
		}

        ///// <summary>
        ///// 更新文档DOM状态
        ///// </summary>
        //public virtual void UpdateDomStatus()
        //{
        //}

        /// <summary>
        /// 元素类型
        /// </summary>
        [Browsable( false )]
        public virtual ElementType _ElementType
        {
            get
            {
                return WriterUtils.GetElementType(this.GetType());
            }
        }



        private string _ID = null;
        /// <summary>
        /// 对象编号
        /// </summary>
        [DefaultValue( null)]
        public string ID
        {
            get
            {
                return _ID; 
            }
            set
            {
                _ID = value; 
            }
        }

        /// <summary>
        /// 影子文档元素。
        /// </summary>
        /// <remarks>某些元素具有影子元素，当修改和删除本元素等价于修改和删除影子元素</remarks>
        [Browsable( false )]
        public virtual DomElement ShadowElement
        {
            get { return null; }
        }
             

        private DomDocument _OwnerDocument = null;
		/// <summary>
		/// 对象所属的文档对象
		/// </summary>
        [Browsable( false )]
        [DefaultValue(null)]
        [XmlIgnore()]
		public virtual DomDocument OwnerDocument
		{
			get
            {
                return _OwnerDocument ;
            }
			set
            {
                _OwnerDocument = value;
            }
		}

        /// <summary>
        /// 元素所属的文档级内容元素对象
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public DomDocumentContentElement DocumentContentElement
        {
            get
            {
                DomElement element = this;
                while (element != null)
                {
                    if (element is DomDocumentContentElement)
                    {
                        return (DomDocumentContentElement)element;
                    }
                    element = element.Parent;
                }
                return null;
            }
        }

        /// <summary>
        /// 元素所属的内容元素对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public DomContentElement ContentElement
        {
            get
            {
                DomElement element = this;
                while (element != null)
                {
                    if (element is DomContentElement)
                    {
                        return (DomContentElement)element;
                    }
                    element = element.Parent;
                }
                return null;
            }
        }

        /// <summary>
        /// 修改DOM结构信息
        /// </summary>
        public virtual void FixDomState()
        {
            if (this.Elements != null && this is DomContainerElement)
            {
                foreach (DomElement element in this.Elements)
                {
                    element._Parent = (DomContainerElement)this;
                    element._OwnerDocument = this.OwnerDocument;
                    element.FixDomState();
                }
            }
        }
         
		/// <summary>
		/// 元素在视图结构中编号
		/// </summary>
		/// <remarks>
		/// 当文档包含大量的元素时,XTextContent中包含了大量的元素,此时它的 IndexOf 函数执行
		/// 缓慢,此处用该属性来预先设置到元素在 XTextContent中的序号,以此来代替 IndexOf 函数
		/// </remarks>
		internal int _ViewIndex = -1 ;
		/// <summary>
		/// 元素在视图结构中编号
		/// </summary>
		[Browsable( false )]
        public int ViewIndex
		{
			get
            {
                return _ViewIndex ;
            }
		}

        /// <summary>
        /// 元素是否显示
        /// </summary>
        [XmlIgnore ]
        [Browsable( false )]
        public virtual bool Visible
        {
            get
            {
                if (this.OwnerDocument == null)
                {
                    return true;
                }
                else
                {
                    return this.OwnerDocument.IsVisible(this);
                }
            }
            set
            {
            }
        }

        /// <summary>
        /// 设置容器元素的可见性
        /// </summary>
        /// <param name="visible">新的可见性</param>
        /// <param name="fastMode">是否启用快速模式，当使用快速模式时，
        /// 只更新DOM结构，不更新用户界面视图</param>
        /// <returns>操作是否成功</returns>
        public virtual bool EditorSetVisible(bool visible , bool fastMode )
        {
            bool result = false;
            bool oldVisible = this.Visible;
            if (oldVisible != visible)
            {
                this.Visible = visible;
                bool visible2 = this.Visible;
                if (visible2 == visible)
                {
                    this.InvalidateView();
                    // 成功的修改了元素的可见性
                    if (visible)
                    {
                        this.OwnerDocument.HighlightManager.InvalidateHighlightInfo(this);
                    }
                    else
                    {
                        this.OwnerDocument.HighlightManager.Remove(this);
                    }
                    result = true;
                    DomElement fc = this.FirstContentElement;
                    DomElement lc = this.LastContentElement;
                    DomContentElement content = this.ContentElement;
                    int startIndex = 0;
                    DomElement preElement = content.PrivateContent.GetPreElement(fc);
                    if (oldVisible)
                    {
                        startIndex = content.PrivateContent.IndexOf(fc);
                    }
                    this.UpdateContentVersion();
                    DomDocumentContentElement dce = this.DocumentContentElement;
                    DomElement currentElementBack = dce.CurrentElement;
                    content.UpdateContentElements(true);
                    if (oldVisible == false)
                    {
                        startIndex = content.PrivateContent.IndexOf(fc);
                    }
                    else
                    {
                        if (preElement != null && content.PrivateContent.Contains( preElement ))
                        {
                            startIndex = content.PrivateContent.IndexOf(preElement);
                        }
                    }
                    content.RefreshPrivateContent(startIndex, -1 , fastMode);
                    if (fastMode == false)
                    {
                        if (currentElementBack != null)
                        {
                            int index = dce.Content.IndexOf(currentElementBack);
                            if (index >= 0)
                            {

                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 创建者权限许可等级
        /// </summary>
        [Browsable( false )]
        public int CreatorPermessionLevel
        {
            get
            {
                int index = this.Style.CreatorIndex;
                return this.OwnerDocument.UserHistories.GetPermissionLevel(index);
            }
        }

        /// <summary>
        /// 逻辑删除者权限许可等级
        /// </summary>
        [Browsable( false )]
        public int DeleterPermissionLevel
        {
            get
            {
                int index = this.Style.DeleterIndex;
                return this.OwnerDocument.UserHistories.GetPermissionLevel(index);
            }
        }

        /// <summary>
        /// 元素是否是逻辑删除的
        /// </summary>
        [Browsable( false )]
        public bool IsLogicDeleted
        {
            get
            {
                DomElement element = this;
                while (element != null)
                {
                    if (element.Style.DeleterIndex >= 0)
                    {
                        return true;
                    }
                    element = element.Parent;
                }
                return false ;
            }
        }

		/// <summary>
		/// 父对象
		/// </summary>
		private DomContainerElement _Parent = null;
		/// <summary>
		/// 父对象
		/// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [XmlIgnore()]
        public virtual DomContainerElement Parent
		{
			get
            {
                return _Parent ;
            }
			set
            {
                _Parent = value;
                if (_Parent != null)
                {
                    this.OwnerDocument = _Parent.OwnerDocument;
                }
            }
		}

        /// <summary>
        /// 判断指定元素是否是本元素的父节点或者更高层次的父节点。
        /// </summary>
        /// <param name="element">要判断的元素</param>
        /// <returns>是否是父节点或者更高级的父节点</returns>
        public virtual bool IsParentOrSupParent(DomElement element)
        {
            DomElement parent = this.Parent;
            while (parent != null)
            {
                if (parent == element)
                {
                    return true;
                }
                parent = parent.Parent;
            }
            return false;
        }

        /// <summary>
        /// 元素在父节点的子节点列表中的序号
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public virtual int ElementIndex
        {
            get
            {
                if (_Parent != null)
                {
                    return _Parent.Elements.IndexOf(this);
                }
                else
                {
                    return -1;
                }
            }
        }

		/// <summary>
		/// 对象所在的文本行对象
		/// </summary>
        [NonSerialized]
		private DomContentLine _OwnerLine = null;
		/// <summary>
		/// 对象所在的文本行对象
		/// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        [XmlIgnore()]
        public DomContentLine OwnerLine
		{
			get
            {
                return _OwnerLine ;
            }
			set
            {
                _OwnerLine = value;
            }
		}

		/// <summary>
		/// 对象在文本行中的从1开始的列号
		/// </summary>
		[Browsable(false)]
        public int ColumnIndex
		{
			get
			{
                if (_OwnerLine != null)
                {
                    return _OwnerLine.IndexOf(this) + 1 ;
                }
                else
                {
                    return -1;
                }
			}
		}

        /// <summary>
		/// 判断是否本元素或者子孙元素处于选择状态
		/// </summary>
		public virtual bool HasSelection
		{
			get
			{
                if (this.DocumentContentElement == null)
                {
                    return false;
                }
                else
                {
                    return this.DocumentContentElement.IsSelected(this);
                }
			}
		}


        /// <summary>
        /// 绘制文档内容使用的样式
        /// </summary>
        [Browsable( false )]
        [XmlIgnore()]
        public virtual DocumentContentStyle RuntimeStyle
        {
            get
            {
                if (this.OwnerDocument == null)
                {
                    return null;
                }
                else
                {
                    return ( DocumentContentStyle ) this.OwnerDocument.ContentStyles.GetRuntimeStyle( this.StyleIndex );
                }
            }
            //set
            //{
            //    this.StyleIndex = this.OwnerDocument.ContentStyles.GetStyleIndex(value);
            //}
        }

        [Browsable( false )]
        [XmlIgnore()]
        public DocumentContentStyle Style
        {
            get
            {
                if (this.OwnerDocument == null)
                {
                    return null;
                }
                else
                {
                    return  ( DocumentContentStyle) this.OwnerDocument.ContentStyles.GetStyle(this.StyleIndex);
                }
            }
            set
            {
                this.StyleIndex = this.OwnerDocument.ContentStyles.GetStyleIndex(value);
            }
        }


        private int _StyleIndex = -1;
        /// <summary>
        /// 使用的样式编号
        /// </summary>
        [DefaultValue(-1)]
        [XmlAttribute()]
        public virtual int StyleIndex
        {
            get
            {
                return _StyleIndex; 
            }
            set
            {
                if (_StyleIndex != value)
                {
                    _StyleIndex = value;
                    _SizeInvalid = true;
                }
            }
        }

        /// <summary>
        /// 元素样式发生改变事件
        /// </summary>
        public virtual void OnStyleChanged()
        {
        }

		/// <summary>
		/// 子对象列表
		/// </summary>
		[XmlIgnore()]
        [Browsable( false )]
        public virtual DomElementList Elements
		{
			get
            {
                return null;
            }
            set
            {
            }
		}

        

		/// <summary>
		/// 元素中第一个显示在文档内容中的元素 
		/// </summary>
		[Browsable( false )]
        public virtual DomElement FirstContentElement
		{
			get
            {
                return this ;
            }
		}

		/// <summary>
		/// 元素中最后一个显示在文档内容中的元素 
		/// </summary>
		[Browsable( false )]
        public virtual DomElement LastContentElement
		{
			get
            {
                return this ;
            }
		}

		/// <summary>
		/// 声明元素的视图无效,需要重新绘制
		/// </summary>
        public virtual void InvalidateView()
        {
            if (_OwnerDocument != null)
            {
                _OwnerDocument.InvalidateElementView(this);
            }
        }

        /// <summary>
        /// 声明元素的高亮度显示信息无效
        /// </summary>
        public virtual void InvalidateHighlightInfo()
        {
            if (this.OwnerDocument != null)
            {
                this.OwnerDocument.HighlightManager.InvalidateHighlightInfo(this);
            }
        }
		/// <summary>
		/// 元素大小无效标记
		/// </summary>
		private bool _SizeInvalid = true;
		/// <summary>
		/// 元素大小无效标记
		/// </summary>
		/// <remarks>若设置该属性,则元素的大小发生改变,系统需要从该元素
        /// 开始重新进行文档排版和分页</remarks>
		[XmlIgnore()]
        public virtual bool SizeInvalid
		{
			get
            {
                return _SizeInvalid ;
            }
			set
            {
                _SizeInvalid = value;
            }
		}

		/// <summary>
		/// 元素视图无效标记
		/// </summary>
		private bool _ViewInvalid = true ;
		/// <summary>
		/// 元素视图无效标记
		/// </summary>
		/// <remarks>若设置该属性,则元素的显示样式无效,系统需要重新
        /// 绘制该元素</remarks>
		[XmlIgnore()]
        public bool ViewInvalid
		{
			get
            {
                return _ViewInvalid ;
            }
			set
            {
                _ViewInvalid = value;
            }
		}
		/// <summary>
		/// 对象左端位置
		/// </summary>
		private float _Left = 0 ;
		/// <summary>
		/// 对象左端位置
		/// </summary>
        [XmlIgnore()]
        public float Left
		{
			get
            {
                return _Left ;
            }
			set
            {
                _Left = value;
            }
		}
		
		/// <summary>
		/// 对象顶端位置
		/// </summary>
		private float _Top = 0 ;
		/// <summary>
		/// 对象顶端位置
		/// </summary>
        [XmlIgnore()]
        public float Top
		{
			get
            {
                return _Top ;
            }
			set
            {
                _Top = value;
            }
		}
		/// <summary>
		/// 对象宽度
		/// </summary>
		private float _Width = 0 ;
		/// <summary>
		/// 对象宽度
		/// </summary>
        [XmlIgnore()]
        public virtual float Width
		{
			get
            {
                return _Width ;
            }
			set
            {
                _Width = value;
            }
		}

        /// <summary>
        /// 对象客户区的宽度
        /// </summary>
        [XmlIgnore]
        [Browsable( false )]
        public virtual float ClientWidth
        {
            get
            {
                DocumentContentStyle rs = this.RuntimeStyle;
                if (rs == null)
                {
                    return this.Width;
                }
                else
                {
                    return this.Width - rs.PaddingLeft - rs.PaddingRight;
                }
            }
        }

		/// <summary>
		/// 对象高度
		/// </summary>
		private float _Height = 0 ;
		/// <summary>
		/// 对象高度
		/// </summary>
        [XmlIgnore()]
        public virtual float Height
		{
			get
            {
                return _Height ;
            }
			set
            {
                _Height = value;
            }
		}

        /// <summary>
        /// 专为编辑器提供的对象大小属性
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public virtual System.Drawing.SizeF EditorSize
        {
            get
            {
                return new System.Drawing.SizeF(this.Width, this.Height);
            }
            set
            {
                this.Width = value.Width;
                this.Height = value.Height;
            }
        }

        /// <summary>
		/// 对象右边缘位置
		/// </summary>
		public float Right
		{
			get
            {
                return _Left + _Width ;
            }
		}
		/// <summary>
		/// 对象底端位置
		/// </summary>
		public float Bottom
		{
			get
            {
                return _Top + _Height ;
            }
		}

		/// <summary>
		/// 对象宽度修正值
		/// </summary>
		private float _WidthFix = 0 ;
		/// <summary>
		/// 对象宽度修正值
		/// </summary>
        [XmlIgnore()]
        public float WidthFix
		{
			get
            {
                return _WidthFix ;
            }
			set
            {
                _WidthFix = value;
            }
		}
		/// <summary>
		/// 对象边界
		/// </summary>
        [XmlIgnore()]
		public System.Drawing.RectangleF Bounds
		{
			get
            {
                return new System.Drawing.RectangleF( 
                    _Left ,
                    _Top ,
                    _Width ,
                    _Height ) ;
            }
			set
			{
				_Left = value.Left ;
				_Top = value.Top ;
				_Width = value.Width ;
				_Height = value.Height ;
			}
		}

		/// <summary>
		/// 对象在文档中的绝对坐标位置
		/// </summary>
        [Browsable( false )]
        public virtual float AbsLeft
		{
            get
            {
                if (_OwnerLine == null)
                {
                    if (this.Parent == null)
                    {
                        return _Left;
                    }
                    else
                    {
                        return _Left + this.Parent.Left;
                    }
                }
                else
                {
                    return _Left + _OwnerLine.AbsLeft;
                }
            }
		}

		/// <summary>
		/// 对象在文档中的绝对坐标位置
		/// </summary>
		[Browsable( false )]
        public virtual float AbsTop
		{
			get
			{
                if (_OwnerLine == null)
                {
                    if (this.Parent == null)
                    {
                        return _Top;
                    }
                    else
                    {
                        return _Top + this.Parent.AbsTop ;
                    }
                }
                else
                {
                    return _Top + this._OwnerLine.AbsTop;
                }
			}
		}
		
        [Browsable( false )]
        public System.Drawing.RectangleF AbsBounds
		{
            get
            {
                return new System.Drawing.RectangleF(
                    this.AbsLeft,
                    this.AbsTop,
                    this.Width,
                    this.Height);
            }
		}

        /// <summary>
        /// 文档元素在视图中占据的边界
        /// </summary>
        [Browsable( false )]
        public System.Drawing.RectangleF ViewBounds
        {
            get
            {
                if (this.OwnerLine == null)
                {
                    return new System.Drawing.RectangleF(
                        this.AbsLeft,
                        this.AbsTop,
                        _Width + _WidthFix,
                        this.Height);
                }
                else
                {
                    return new System.Drawing.RectangleF(
                        this.AbsLeft,
                        this.AbsTop,
                        _Width + _WidthFix,
                        this.OwnerLine.Height);
                }
            }
        }


        [Browsable(false)]
        internal DomParagraphFlagElement OwnerParagraphEOF
        {
            get
            {
                if (this.OwnerDocument == null)
                    return null;
                else
                    return this.OwnerDocument.GetParagraphEOFElement(this);
            }
        }

        /// <summary>
		/// 沿着DOM树逐级向上查找指定类型的父容器
		/// </summary>
		/// <param name="ParentType">父容器类型</param>
        /// <param name="includeThis">查找时是否包含元素本身</param>
		/// <returns>找到的父容器对象</returns>
		public virtual DomElement GetOwnerParent( Type ParentType , bool includeThis )
		{
			DomElement c = includeThis ? this : this.Parent ;
			while( c != null )
			{
                Type ct = c.GetType();
                if (ct.Equals(ParentType) || ct.IsSubclassOf( ParentType ))
                {
                    return c;
                }
				c = c.Parent ;
			}
			return null;
		}
		/// <summary>
		/// 获得同一层次中的上一个元素
		/// </summary>
		public virtual DomElement PreviousElement
		{
			get
			{
				DomContainerElement c = this.Parent ;
				if( c != null )
				{
					int index = c.Elements.IndexOf( this );
                    if (index > 0)
                    {
                        return c.Elements[index - 1];
                    }
				}
				return null;
			}
		}
		/// <summary>
		/// 获得元素的同一层次的下一个元素
		/// </summary>
		public virtual DomElement NextElement
		{
			get
			{
				DomContainerElement c = this.Parent ;
				if( c != null )
				{
					int index = c.Elements.IndexOf( this );
                    if (index < c.Elements.Count - 1)
                    {
                        return c.Elements[index + 1];
                    }
				}
				return null;
			}
		}

        [NonSerialized()]
        private int _ContentVersion = 0;
        /// <summary>
        /// 元素内容版本号，当元素内容发生任何改变时，该属性值都会改变
        /// </summary>
        [Browsable(false)]
        public int ContentVersion
        {
            get
            {
                return _ContentVersion;
            }
        }

        /// <summary>
        /// 修改元素内容版本号，这会导致所有父级元素的ContentVersion的值的改变。
        /// </summary>
        public virtual void UpdateContentVersion()
        {
            DomElement parent = this ;
            while (parent != null)
            {
                parent._ContentVersion++;
                parent = parent.Parent;
            }
        }

        /// <summary>
        /// 处理文档用户界面事件
        /// </summary>
        /// <param name="args"></param>
        public virtual void HandleDocumentEvent(DocumentEventArgs args)
        {
            switch (args.Style)
            {
                case DocumentEventStyles.GotFocus:
                    // 触发获得焦点事件
                    OnGotFocus(EventArgs.Empty);
                    break;
                case DocumentEventStyles.LostFocus:
                    // 触发失去焦点事件
                    OnLostFocus(EventArgs.Empty);
                    break;
                case DocumentEventStyles.MouseEnter:
                    // 触发鼠标进入事件
                    OnMouseEnter(EventArgs.Empty);
                    break;
                case DocumentEventStyles.MouseLeave:
                    // 触发鼠标离开事件
                    OnMouseLeave(EventArgs.Empty);
                    break;
            }
        }


        /// <summary>
        /// 触发获得输入焦点事件
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        public virtual void OnGotFocus(EventArgs args)
        {
            if (this.Events != null && this.Events.HasGotFocus)
            {
                this.Events.RaiseGotFocus(this, args);
            }
        }

        /// <summary>
        /// 触发失去输入焦点事件
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        public virtual void OnLostFocus(EventArgs args)
        {
            if (this.Events != null && this.Events.HasLostFocus)
            {
                this.Events.RaiseLostFocus(this, args);
            }
        }

        /// <summary>
        /// 判断元素是否获得输入焦点
        /// </summary>
        [Browsable(false)]
        public virtual bool Focused
        {
            get
            {
                DomDocumentContentElement dce = this.DocumentContentElement;
                return dce.CurrentElement == this;
            }
        }

        /// <summary>
        /// 获得输入焦点
        /// </summary>
        public virtual void Focus()
        {
            DomElement firstElement = this.FirstContentElement;
            if (firstElement != null)
            {
                DomDocumentContentElement dce = this.DocumentContentElement;
                dce.SetSelection(firstElement.ViewIndex, 0);
            }
        }


        /// <summary>
        /// 处理鼠标进入事件
        /// </summary>
        /// <param name="args">事件参数</param>
        public virtual void OnMouseEnter(EventArgs args)
        {
            if (this.Events != null && this.Events.HasMouseEnter)
            {
                this.Events.RaiseMouseEnter(this, args);
            }
        }

        /// <summary>
        /// 处理鼠标离开事件
        /// </summary>
        /// <param name="args">事件参数</param>
        public virtual void OnMouseLeave(EventArgs args)
        {
            if (this.Events != null && this.Events.HasMouseLeave)
            {
                this.Events.RaiseMouseLeave(this, args);
            }
        }
         
		/// <summary>
		/// 复制对象
		/// </summary>
		/// <param name="Deeply">是否深入复制子元素</param>
		/// <returns>复制品</returns>
		public virtual DomElement Clone( bool Deeply )
		{
            DomElement element = ( DomElement ) this.MemberwiseClone();
            return element;
		}

		/// <summary>
		/// 输出元素包含的文本数据
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "";
		}

        /// <summary>
        /// 输出纯文本数据
        /// </summary>
        /// <returns>纯文本数据</returns>
        public virtual string ToPlaintString()
        {
            return this.ToString();
        }

        /// <summary>
        /// 输出调试用的文本数据
        /// </summary>
        /// <returns>文本数据</returns>
        public virtual string ToDebugString()
        {
            return this.GetType().Name + "(" + this.ID + ")";
        }

        /// <summary>
        /// 输出HTML代码
        /// </summary>
        /// <param name="writer">XML书写器</param>
        public virtual void WriteHTML(WriterHtmlDocumentWriter writer)
        {
        }

        /// <summary>
        /// 输出RTF文档
        /// </summary>
        /// <param name="writer">RTF文档书写器</param>
        public virtual void WriteRTF(DCSoft.CSharpWriter.RTF.RTFContentWriter writer)
        {
        }

        ///// <summary>
        ///// 输出文档
        ///// </summary>
        ///// <param name="writer">文档书写器</param>
        //public virtual void WriteDocument( DocumentContentWriter writer )
        //{
           
        //}

        public virtual void Draw(DocumentPaintEventArgs args)
        {
            args.Render.RefreshView(this, args);
        }

        public virtual void DrawContent(DocumentPaintEventArgs args)
        {
            args.Render.DrawContent(this, args);
        }

        /// <summary>
        /// 计算元素大小
        /// </summary>
        /// <param name="args">参数</param>
        public virtual void RefreshSize( DocumentPaintEventArgs args )
        {
            args.Render.RefreshSize(this, args.Graphics);

        }

        /// <summary>
        /// 高亮度信息对象列表
        /// </summary>
        /// <returns>获得的列表</returns>
        public virtual HighlightInfoList GetHighlightInfos()
        {
            return null;
        }

        /// <summary>
        /// 创建包含元素内容的图片对象
        /// </summary>
        /// <returns>图片对象</returns>
        public virtual System.Drawing.Image CreateContentImage()
        {
            if (this.Width <= 0 || this.Height <= 0)
            {
                return null;
            }
            System.Drawing.SizeF size = new System.Drawing.SizeF( this.Width , this.Height );
            size = GraphicsUnitConvert.Convert(
                size , 
                this.OwnerDocument.DocumentGraphicsUnit ,
                System.Drawing.GraphicsUnit.Pixel );
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(
                (int)Math.Ceiling( size.Width) ,
                (int) Math.Ceiling( size.Height));
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.PageUnit = this.OwnerDocument.DocumentGraphicsUnit;
                RectangleF bounds = this.AbsBounds ;
                g.TranslateTransform( - bounds.Left , - bounds.Top );
                DocumentPaintEventArgs args = new DocumentPaintEventArgs(g, Rectangle.Empty);
                args.Document = this.OwnerDocument;
                args.PageClipRectangle = Rectangle.Ceiling( bounds );
                args.Render = this.OwnerDocument.Render;
                args.RenderStyle = DocumentRenderStyle.Bitmap ;
                args.Style = this.RuntimeStyle;
                args.Type = this.DocumentContentElement.ContentPartyStyle;
                args.ViewBounds = Rectangle.Ceiling( bounds );
                args.ActiveMode = true;
                args.Bounds = bounds;
                args.Element = this;
                args.ForCreateImage = true;

                this.Draw(args);
            }//using
            return bmp;
        }

        /// <summary>
        /// 文档加载后的处理
        /// </summary>
        /// <param name="format">加载文档的格式</param>
        public virtual void AfterLoad(FileFormat format)
        {
           
        }


        #region IXDependencyPropertyLoggable 成员

        IXDependencyPropertyLogger IXDependencyPropertyLoggable.PropertyLogger
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
         
        /// <summary>
        /// 销毁对象
        /// </summary>
        public virtual void Dispose()
        {
            
        }

        /// <summary>
        /// 表示对象内容的纯文本数据
        /// </summary>
        [Browsable( false )]
        [XmlIgnore()]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        public virtual string Text
        {
            get
            {
                return ToString();
            }
            set
            {
            }
        }

        [NonSerialized]
        private ElementEventList _Events = null;
        /// <summary>
        /// 元素事件列表
        /// </summary>
        [Browsable( false )]
        [XmlIgnore()]
        public virtual ElementEventList Events
        {
            get
            {
                return _Events; 
            }
            set
            {
                _Events = value; 
            }
        }
    }//public abstract class XTextElement
}