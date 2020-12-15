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
using System.Windows.Forms;
using System.Drawing ;
//using EMRCommon;
using DCSoft.WinForms.Native;
using DCSoft.Common;

namespace DCSoft.WinForms
{
    [System.ComponentModel.ToolboxItem( false )]
    [System.Drawing.ToolboxBitmap( typeof( XTreeListBox ))]
    public class XTreeListBox : 
        System.Windows.Forms.ContainerControl ,
        IMessageFilter
    {
        /// <summary>
		/// 文本框收缩和展开控制框的大小,目前设为8
		/// </summary>
		public const int ExpendBoxSize	= 8;

        /// <summary>
        /// 初始化对象数据
        /// </summary>
        public XTreeListBox()
        {
            this.BackColor = System.Drawing.SystemColors.Window;
            intDefaultItemHeight = (int)this.Font.GetHeight() + 3;
            // 添加列表面板
            pnlList = new System.Windows.Forms.UserControl();
            pnlList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            pnlList.AutoScroll = true;
            pnlList.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlList.Visible = true;
            pnlList.Font = this.Font;
            pnlList.Paint += new System.Windows.Forms.PaintEventHandler(pnlList_Paint);
            pnlList.MouseMove += new System.Windows.Forms.MouseEventHandler(pnlList_MouseMove);
            pnlList.MouseDown += new System.Windows.Forms.MouseEventHandler(pnlList_MouseDown);
            pnlList.MouseLeave += new EventHandler(pnlList_MouseLeave);
            pnlList.DoubleClick += new EventHandler(pnlList_DoubleClick);
            this.Controls.Add(pnlList);
            // 添加拼音码标题
            lblSpell = new System.Windows.Forms.Label();
            lblSpell.Dock = System.Windows.Forms.DockStyle.Top;
            lblSpell.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            lblSpell.BackColor = System.Drawing.SystemColors.Control;
            lblSpell.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblSpell.Font = new System.Drawing.Font("宋体", 10);
            // 添加项目值编辑按钮
            btnEditItem = new Label();
            btnEditItem.Size = new Size(16, 14);
            btnEditItem.Visible = false;
            btnEditItem.Font = new System.Drawing.Font("Arial", 9);
            btnEditItem.BorderStyle = BorderStyle.FixedSingle;
            btnEditItem.BackColor = System.Drawing.Color.Gainsboro ;
            btnEditItem.ForeColor = System.Drawing.Color.Black;
            btnEditItem.Text = "...";
            btnEditItem.MouseEnter += delegate(object sender, EventArgs args)
            {
                btnEditItem.BackColor = System.Drawing.Color.WhiteSmoke;
            };
            btnEditItem.MouseLeave += delegate(object sender, EventArgs args)
            {
                btnEditItem.BackColor = System.Drawing.Color.Gainsboro;
            };
            btnEditItem.Click += new EventHandler(btnEditItem_Click);
            
            pnlList.Controls.Add(btnEditItem);
            //this.ImeMode	= System.Windows.Forms.ImeMode.Off ;
            this.Controls.Add(lblSpell);
        }



        /// <summary>
        /// 标题控件的文本内容
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public string TitleControlText
        {
            get
            {
                return this.lblSpell.Text;
            }
            set
            {
                this.lblSpell.Text = value;
                this.lblSpell.Refresh();
            }
        }
         
		//*****************************************************************************************
		//*****************************************************************************************
		//*****************************************************************************************

		/// <summary>
		/// 列表元素的集合
		/// </summary>
		private List<XTreeListBoxItem > myItems = new List<XTreeListBoxItem>();
        /// <summary>
		/// 列表项目
		/// </summary>
		public List<XTreeListBoxItem> Items
		{
			get
            {
                return myItems ;
            }
			set
			{
                if( value != myItems )
				{
					myItems = value ;
                    if (myItems == null)
                    {
                        myItems = new List<XTreeListBoxItem>();
                    }
                    this.CalculateViewSize();
                    this.UpdateAutoScrollMinSize();
				}
			}
		}

        ///// <summary>
        ///// 退出等待用户操作的标记
        ///// </summary>
        //private bool	bolExitLoop				= true;	 
        ///// <summary>
        ///// 用户确认操作的标记
        ///// </summary>
        //private bool	bolUserAccept			= false;

		
        ///// <summary>
        ///// 自动关闭列表
        ///// </summary>
        //protected bool	bolAutoClose			= true;

        /// <summary>
        /// 显示列表内容的控件
        /// </summary>
        private System.Windows.Forms.UserControl pnlList = null;

        /// <summary>
		/// 用于显示拼音码的控件
		/// </summary>
		private System.Windows.Forms.Label lblSpell = null;
        /// <summary>
        /// 编辑项目值的按钮控件
        /// </summary>
        private System.Windows.Forms.Label btnEditItem = null;

        /// <summary>
        /// 树状节点前的扩展点矩形所占据的宽度
        /// </summary>
		private int innerExpendHandleRectWidth	= 0 ;
        /// <summary>
        /// 快捷键的显示宽度
        /// </summary>
		private int innerShortCutWidth			= 0  ;
        /// <summary>
        /// 图标的显示宽度
        /// </summary>
		private int innerIconWidth				= 0 ;


        private bool _SelectionModified = false;
        /// <summary>
        /// 列表框选择的内容是否改变标识
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public bool SelectionModified
        {
            get
            {
                return _SelectionModified; 
            }
            set
            {
                _SelectionModified = value; 
            }
        }

        private bool _ShowEditItemButton = false;
        /// <summary>
        /// 是否显示编辑列表项目的小按钮
        /// </summary>
        [System.ComponentModel.DefaultValue( false )]
        public bool ShowEditItemButton
        {
            get 
            {
                return _ShowEditItemButton;
            }
            set 
            {
                _ShowEditItemButton = value;
            }
        }

        private bool _EditingItem = false;
        /// <summary>
        /// 用户正在编辑列表项目数值
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public bool EditingItem
        {
            get
            {
                return _EditingItem; 
            }
        }

        void btnEditItem_Click(object sender, EventArgs e)
        {
            XTreeListBoxItem item = (XTreeListBoxItem)btnEditItem.Tag;
            XTreeListBoxEventArgs args = new XTreeListBoxEventArgs();
            args.ListItem = item;
            try
            {
                _EditingItem = true;
                OnEditItem(args);
            }
            finally
            {
                _EditingItem = false;
            }
        }


        /// <summary>
        /// 编辑列表项目事件
        /// </summary>
        public event MyTreeListBoxEventHandler EditItem = null;

        protected virtual void OnEditItem(XTreeListBoxEventArgs e)
        {
            try
            {
                _EditingItem = true;
                if (EditItem != null)
                {
                    EditItem(this, e);
                    this.Invalidate();
                }
            }
            finally
            {
                _EditingItem = false;
            }
        }

		/// <summary>
		/// 列表当前项目改变事件
		/// </summary>
		public event System.EventHandler SelectedIndexChanged = null;
        /// <summary>
        /// 触发列表当前项目改变事件
        /// </summary>
        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(this, e);
            }
        }
		 
        /// <summary>
        /// 用户确认选择操作事件
        /// </summary>
        public event EventHandler UserAcceptSelection = null;
        /// <summary>
        /// 触发用户确认选择操作事件
        /// </summary>
        protected virtual void OnUserAcceptSelection(EventArgs args)
        {
            if (UserAcceptSelection != null)
            {
                UserAcceptSelection(this, args);
            }
        }

        /// <summary>
        /// 用户取消操作事件
        /// </summary>
        public event EventHandler UserCancel = null;

        /// <summary>
        /// 触发用户取消操作事件
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnUserCancel(EventArgs args)
        {
            if (UserCancel != null)
            {
                UserCancel(this, args);
            }
        }

        /// <summary>
        /// 列表内容发生改变事件
        /// </summary>
        public event System.EventHandler ListContentChanged = null;
        /// <summary>
        /// 触发列表内容发生改变事件
        /// </summary>
        protected virtual void OnListContentChanged(EventArgs e)
        {
            if (ListContentChanged != null)
            {
                ListContentChanged(this, e);
            }
        }

		#region 对象的属性定义群 **********************************************
 
         
		/// <summary>
		/// 是否显示展开收缩控制框
		/// </summary>
		private bool bolShowExpendHandleRect = true;
		/// <summary>
		/// 是否显示展开收缩控制框
		/// </summary>
        [System.ComponentModel.DefaultValue( true )]
		public bool ShowExpendHandleRect
		{
			get
            {
                return bolShowExpendHandleRect;
            }
			set
            {
                bolShowExpendHandleRect = value;
            }
		}

        
		/// <summary>
		/// 各个层次之间的距离
		/// </summary>
		private int intIndent = 9 ;
		/// <summary>
		/// 各级层次之间的距离
		/// </summary>
        [System.ComponentModel.DefaultValue( 9 )]
		public int Indent
		{
			get
            {
                return intIndent;
            }
			set
            {
                intIndent = value;
            }
		}

        
		/// <summary>
		/// 列表中最多可同时显示的项目个数
		/// </summary>
		private int intVisibleItems = 15 ;
		/// <summary>
		/// 列表中最多可同时显示的项目个数
		/// </summary>
        [System.ComponentModel.DefaultValue( 15 )]
		public int VisibleItems
		{
			get
            {
                return intVisibleItems ;
            }
			set
            {
                intVisibleItems = value;
            }
		}

        
		/// <summary>
		/// 列表标题
		/// </summary>
		private string strTitle = null;
		/// <summary>
		/// 列表标题
		/// </summary>
        [System.ComponentModel.DefaultValue( null )]
		public string Title
		{
			get
            {
                return strTitle;
            }
			set
			{
				if( value != null && strTitle != value)
				{
					strTitle = value;
					this.lblSpell.Text = strTitle ;
				}
			}
		}

        /// <summary>
		/// 当列表是多选时,各个项目文本之间的分隔字符
		/// </summary>
		private char chrItemSpliter = ',';
		/// <summary>
		/// 当列表是多选时,各个项目文本之间的分隔字符
		/// </summary>
        [System.ComponentModel.DefaultValue( ',' )]
		public char ItemSpliter
		{
			get
            {
                return chrItemSpliter ;
            }
			set
            {
                chrItemSpliter = value;
            }
		}

        /// <summary>
		/// 用于提供列表前面的图标的图形列表控件
		/// </summary>
		private System.Windows.Forms.ImageList myImageList = null;
		/// <summary>
		/// 列表示使用的图象列表
		/// </summary>
        [System.ComponentModel.DefaultValue(null)]
        [System.ComponentModel.Category("Appearance")]
        public System.Windows.Forms.ImageList ImageList
		{
			get
            {
                return myImageList ;
            }
			set
            {
                myImageList = value;
            }
		}
        
		/// <summary>
		/// 列表元素对列表左端的间距
		/// </summary>
		private int	intLeftMargin = 8 ;
		/// <summary>
		/// 列表项目的左边距
		/// </summary>
        [System.ComponentModel.DefaultValue( 8 )]
        [System.ComponentModel.Category("Appearance")]
		public int LeftMargin
		{
			get
            { 
                return intLeftMargin ;
            }
			set
            { 
                intLeftMargin = value;
            }
		}
		
        /// <summary>
		/// 列表元素最列表右段的间距
		/// </summary>
		private int	intRightMargin = 8 ;
        /// <summary>
		/// 列表项目的右边距
		/// </summary>
        [System.ComponentModel.DefaultValue( 8 )]
        [System.ComponentModel.Category("Appearance")]
		public int RightMargin
		{
			get
            { 
                return intRightMargin ;
            }
			set
            {
                intRightMargin = value;
            }
		}

		/// <summary>
		/// 显示快捷键字符的标记
		/// </summary>
		private bool bolShowShortCut	= true;
        /// <summary>
		/// 是否显示快捷字符
		/// </summary>
        [System.ComponentModel.DefaultValue( true )]
        [System.ComponentModel.Category("Appearance")]
		public bool ShowShortCut 
		{
			get
            { 
                return bolShowShortCut ;
            }
			set
            {
                bolShowShortCut = value;
            }
		}


		/// <summary>
		/// 默认列表项目高度
		/// </summary>
		private int	intDefaultItemHeight = 14 ; 
        /// <summary>
		/// 项目的默认高度,新增的列表项目将设置为该高度
		/// </summary>
        [System.ComponentModel.DefaultValue( 14 )]
        [System.ComponentModel.Category("Appearance")]
		public int DefaultItemHeight
		{
			get
            { 
                return  intDefaultItemHeight ;
            }
			set
            {
                intDefaultItemHeight = value;
            }
		}

        /// <summary>
        /// 获得项目的高度
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private int GetItemHeight(XTreeListBoxItem item)
        {
            if (item.Height > 0)
                return item.Height;
            else
                return this.DefaultItemHeight;
        }

        /// <summary>
        /// 获得列表项目显示的文本
        /// </summary>
        /// <param name="item">列表项目对象</param>
        /// <returns>列表文本</returns>
        public virtual string GetItemDisplayText(XTreeListBoxItem item)
        {
            return item.Text;
        }

		/// <summary>
		/// 是否允许多选的标记
		/// </summary>
		private bool bolMultiSelect = false;
        /// <summary>
		/// 是否允许多选
		/// </summary>
        [System.ComponentModel.DefaultValue( false )]
        [System.ComponentModel.Category("Behavior")]
		public virtual bool MultiSelect
		{
			get
            { 
                return bolMultiSelect ;
            }
			set
			{
				bolMultiSelect = value;
				if( this.Visible)
                {
                    this.Invalidate();
                }
			}
		}

		#endregion

		#region 列表项目列表操作的函数群 **************************************

        public event XTreeListBoxLoadChildItemsEventHandler LoadChildItems = null;

		/// <summary>
		/// 加载子列表元素
		/// </summary>
		/// <param name="myItem">父列表元素对象</param>
		/// <param name="myChildItems">保存加载的子列表的数组对象</param>
        protected virtual void OnLoadChildItems(
            XTreeListBoxItem myItem, 
            List<XTreeListBoxItem> myChildItems)
		{
            if (LoadChildItems != null)
            {
                XTreeListBoxLoadChildItemsEventArgs args = new XTreeListBoxLoadChildItemsEventArgs(myItem);
                args.ChildItems = new List<XTreeListBoxItem>();
                LoadChildItems(this, args);
                if (args.ChildItems != null && args.ChildItems.Count > 0)
                {
                    myChildItems.AddRange(args.ChildItems);
                }
            }
		}

		/// <summary>
		/// 是否存在有快捷字符的项目
		/// </summary>
		/// <returns></returns>
        [System.ComponentModel.Browsable( false )]
		private bool HasShortCutCharItem
		{
            get
            {
			    foreach(XTreeListBoxItem myItem in this.Items)
                {
				    if( char.IsWhiteSpace( myItem.ShortCutChar)==false)
                    {
					    return true;
                    }
                }
			    return false ;
            }
		}

		/// <summary>
		/// 是否存在有子列表的项目
		/// </summary>
		/// <returns></returns>
        [System.ComponentModel.Browsable( false )]
		private bool HasItemsItem
		{
            get
            {
			    foreach( XTreeListBoxItem myItem in this.Items)
                {
				    if( myItem.HasItems )// .Items != null && myItem.Items.Count > 0 )
                    {
					    return true;
                    }
                }
			    return false;
            }
		}

		/// <summary>
		/// 是否存在带图标的项目
		/// </summary>
		/// <returns></returns>
        [System.ComponentModel.Browsable( false )]
		private bool HasIconItem
		{
            get
            {
			    foreach(XTreeListBoxItem myItem in this.Items)
                {
				    if( myItem.Icon != null)
                    {
					    return true;
                    }
                }
			    return false;
		    }
        }

		/// <summary>
		/// 是否存在被选中的项目
		/// </summary>
		/// <returns></returns>
        [System.ComponentModel.Browsable( false )]
		public bool HasSelectedItem
		{
            get
            {
			    foreach( XTreeListBoxItem myItem in this.Items)
                {
				    if( myItem.Selected )
                    {
					    return true;
                    }
                }
			    return false;
            }
		}

        /// <summary>
        /// 将选中的项目移到最上面显示
        /// </summary>
		public void UpgradeSelection()
		{
			if( this.MultiSelect && this.Items.Count >  14 )
			{
				XTreeListBoxItem[] mySelectedItems = this.SelectedItems ;
				if( mySelectedItems != null && mySelectedItems.Length > 1 )
				{
                    foreach (XTreeListBoxItem item in mySelectedItems)
                    {
                        this.Items.Remove(item);
                    }
					this.Items.InsertRange( 0 , mySelectedItems );
                    this.CalculateViewSize();
					this.SelectedIndex = 0 ;
					this.Refresh();
				}
			}
		}


		/// <summary>
		/// 刷新列表项目的拼音码
		/// </summary>
		/// <param name="ResetAll">是否设置所有项目的拼音码，若为false则只设置不存在的拼音码的项目</param>
		public void RefreshChineseSpell(bool ResetAll)
		{
			if( ResetAll)
			{
				foreach(XTreeListBoxItem myItem in this.Items)
				{
					myItem.ChineseSpell =
                        StringConvertHelper.ToChineseSpell( myItem.Text);
				}
			}
			else
			{
				foreach( XTreeListBoxItem myItem in this.Items )
				{
					if(myItem.ChineseSpell == null
                        || myItem.ChineseSpell.Length == 0 )
                    {
						myItem.ChineseSpell =
                            StringConvertHelper.ToChineseSpell(myItem.Text);
				    }
                }
			}
		}

		/// <summary>
		/// 根据项目文本内容查找项目
		/// </summary>
		/// <param name="strText">需要查找的文本</param>
		/// <param name="StartsWith">是否匹配文本开头</param>
		/// <returns>找到的项目</returns>
		public XTreeListBoxItem GetItemByText( string strText,bool StartsWith)
		{
			if( strText == null)
            {
				return null;
            }
			foreach(XTreeListBoxItem myItem in this.Items)
			{
				if( myItem.Text != null 
                    && myItem.Style == XTreeListBoxItemStyle.Content )
				{
					if( myItem.Text == strText )
                    {
						return myItem ;
                    }
					if( StartsWith && myItem.Text.StartsWith( strText ))
                    {
						return myItem ;
				    }
                }
			}
			return null;
		}

		
		/// <summary>
		/// 获得和指定拼音码接近的第一个列表项目
		/// </summary>
		/// <param name="strSpell">指定的拼音码</param>
		/// <returns>获得的列表项目,若没有找到则返回空引用</returns>
		public XTreeListBoxItem GetItemByChineseSpell( string strSpell )
		{
			foreach( XTreeListBoxItem myItem in this.Items )
			{
				if( myItem.ChineseSpell != null 
                    && myItem.ChineseSpell.StartsWith( strSpell )
                    && myItem.Style == XTreeListBoxItemStyle.Content )
				{
					return myItem ;
				}
			}
			return null;
		}


		public XTreeListBoxItem AddSpliter()
		{
			XTreeListBoxItem myItem = this.AddItem(null);
			myItem.Style = XTreeListBoxItemStyle.Spliter  ;
			return myItem;

		}

		/// <summary>
		/// 添加项目
		/// </summary>
		/// <param name="strText">新增的列表项目的文本内容</param>
		/// <returns>新增的列表项目对象</returns>
		public XTreeListBoxItem AddItem(string strText)
		{
			XTreeListBoxItem item = CreateListItem( strText );// new ListItem();
            item.Top = this.ViewSize.Height;
			this.Items.Add(item);
            // 更新视图高度
            this._ViewSize.Height += GetItemHeight(item);
			if( this.isUpdateing == false )
			{
                this.UpdateAutoScrollMinSize();
				pnlList.Invalidate();
			}
			return item ;
		}

		/// <summary>
		/// 创建一个指定文本内容的列表项目对象
		/// </summary>
		/// <param name="strText">指定的列表项目的文本内容</param>
		/// <returns>创建的列表项目对象</returns>
		public XTreeListBoxItem CreateListItem ( string strText )
		{
			XTreeListBoxItem item = new XTreeListBoxItem();
			item.Text		= strText ;
			item.ForeColor	= this.ForeColor ;
			item.BackColor	= this.BackColor ;
			return item ;
		}

		/// <summary>
		/// 利用字符串数组添加多个项目
		/// </summary>
		/// <param name="strItems">字符串数组</param>
		public void AddStrings(string[] strItems)
		{
            if (strItems == null)
            {
                throw new ArgumentNullException("strItems");
            }
			foreach(string strItem in strItems)
			{
				XTreeListBoxItem NewItem	= new XTreeListBoxItem();
				NewItem.Text		= strItem ;
				NewItem.ForeColor	= this.ForeColor ;
				NewItem.BackColor	= this.BackColor ;
				NewItem.Top			= this.ViewSize.Height ;
				this.Items.Add( NewItem );
                // 更新视图高度
                this._ViewSize.Height += GetItemHeight(NewItem);
			}
			if( this.isUpdateing == false )
			{
                this.UpdateAutoScrollMinSize();
				pnlList.Invalidate();
			}
		}

		/// <summary>
		/// 利用对象数组添加多个项目
		/// </summary>
		/// <param name="objItems">对象数组</param>
		public void AddObjects(object[] objItems)
		{
            if (objItems == null)
            {
                throw new ArgumentNullException("objItems");
            }
			foreach(object obj in objItems)
			{
				XTreeListBoxItem NewItem	= new XTreeListBoxItem();
				NewItem.ForeColor	= this.ForeColor ;
				NewItem.BackColor	= this.BackColor ;
				NewItem.Top			= this._ViewSize.Height ;
                if (obj == null)
                {
                    NewItem.Text = "<NULL>";
                }
                else
                {
                    NewItem.Text = obj.ToString();
                    NewItem.Tag = obj;
                }
				this.Items.Add( NewItem );
                // 更新视图高度
                this._ViewSize.Height += GetItemHeight(NewItem);
			}
			if(  this.isUpdateing == false )
			{
                this.UpdateAutoScrollMinSize();
				pnlList.Invalidate();
			}
		}

		/// <summary>
		/// 删除列表项目
		/// </summary>
        /// <param name="item">列表项目对象</param>
		public void RemoveItem(XTreeListBoxItem item)
		{
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            this.Items.Remove(item);
            this.CalculateViewSize();
            if (intSelectedIndex >= this.Items.Count)
            {
                intSelectedIndex = this.Items.Count - 1;
                this.OnSelectedIndexChanged(EventArgs.Empty);
            }
			if( this.isUpdateing == false )
			{
                this.UpdateAutoScrollMinSize();
				pnlList.Invalidate();
			}
		}

		/// <summary>
		/// 清空项目
		/// </summary>
		public void Clear()
		{
            btnEditItem.Visible = false;
			this.Items.Clear();
			this._ViewSize = System.Drawing.Size.Empty ;
			intSelectedIndex = -1 ;
			if( this.isUpdateing == false )
			{
                this.UpdateAutoScrollMinSize();
				pnlList.Invalidate();
			}
		}
 

		/// <summary>
		/// 获得指定坐标所在的项目对象
		/// </summary>
		/// <param name="x">指定点的X坐标</param>
		/// <param name="y">指定点的Y坐标</param>
		/// <returns>指定点下的列表项目对象,若没有则返回空引用</returns>
		public XTreeListBoxItem HitTest(int x , int y )
		{
			if( x >=0 
                && x < pnlList.ClientSize.Width 
                && y >=0 
                && y <= pnlList.ClientSize.Height)
			{
				y = y - pnlList.AutoScrollPosition.Y ;
				
				foreach(XTreeListBoxItem myItem in this.Items)
				{
                    int itemHeight = GetItemHeight(myItem);
					if(  y <= myItem.Top + itemHeight
                        && myItem.Style == XTreeListBoxItemStyle.Content )
					{
						return myItem ;
					}
				}
			}
			return null;
		}// ListItem HitTest



		#endregion

		#region 当前列表项目操作的函数群 **************************************

		private void InnerSetSelectedIndex(int index)
		{
			if( index >=0 && index < this.Items.Count )
			{
				intSelectedIndex = index ;
				if( this.MultiSelect == false)
				{
					foreach( XTreeListBoxItem myItem in this.Items)
					{
						myItem.Selected = false;
					}
					XTreeListBoxItem mySelectedItem = this.Items[intSelectedIndex];
					mySelectedItem.Selected = true ;
				}
			}
		}



        private bool bolUserSelecting = false;

        /// <summary>
        /// 当前选中项目编号
        /// </summary>
        private int intSelectedIndex = -1;	 
		/// <summary>
		/// 设置,返回用户当前选择的项目的从0开始的序号
		/// </summary>
		/// <remarks>
        /// 当设置当前选择的项目的编号时,若该编号有效则根据需要滚动列表以保证该项目显示在客户区
		/// 此外还试图调用 SelectIndexChanged委托
        /// </remarks>
        [System.ComponentModel.Browsable( false )]
		public int SelectedIndex 
		{
			get
            {
                return intSelectedIndex ;
            }
			set
			{
				bolUserSelecting = true;
				if( value < 0 || value >= this.Items.Count )
				{
					intSelectedIndex = -1 ;
					if( this.MultiSelect == false)
					{
                        foreach (XTreeListBoxItem myItem in this.Items)
                        {
                            myItem.Selected = false;
                        }
					}
					pnlList.Invalidate();
				}
				else
				{
					if( intSelectedIndex != value)
					{
						XTreeListBoxItem OldItem = this.SelectedItem ;
						InnerSetSelectedIndex( value );
						if( intUpdateLevel <= 0 )
						{
							XTreeListBoxItem myItem = this.Items[intSelectedIndex];
                            ScrollToView(myItem);
							this.Invalidate( OldItem );
							this.Invalidate( this.SelectedItem );
							//pnlList.Refresh();
						}
                        this.SelectionModified = true;
                        OnSelectedIndexChanged(EventArgs.Empty);
					}
				}
				bolUserSelecting = false;
			}
		}

        /// <summary>
        /// 滚动列表，使得元素显示在可见区域中
        /// </summary>
        /// <param name="item">列表元素对象</param>
        public void ScrollToView(XTreeListBoxItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (intUpdateLevel <= 0)
            {
                Rectangle bounds = GetItemBounds(item);
                if ( bounds.Top < 0)
                {
                    pnlList.AutoScrollPosition = new System.Drawing.Point(
                        0,
                        (int)( bounds.Top - pnlList.AutoScrollPosition.Y));
                }
                else if ( bounds.Bottom > pnlList.ClientSize.Height)
                {
                    pnlList.AutoScrollPosition = new System.Drawing.Point(
                        0,
                        (int)( bounds.Bottom - pnlList.ClientSize.Height - pnlList.AutoScrollPosition.Y));
                }
            }
        }

		/// <summary>
		/// 设置指定序号的项目为当前项目并滚动列表使该项目位于列表中央
		/// </summary>
		/// <param name="index">从0开始的序号</param>
		public void SetSelectIndexMiddle(int index)
		{
			if( this.Items != null && index >=0 && index < this.Items.Count )
			{
				bolUserSelecting = true;
				InnerSetSelectedIndex( index );
				XTreeListBoxItem myItem = (XTreeListBoxItem)this.Items[index];
				//int vTop = myItem.Top ;
                int itemHeight = GetItemHeight(myItem);
				pnlList.AutoScrollPosition = new System.Drawing.Point(
                    0 ,
                    (int)( myItem.Top - ( pnlList.ClientSize.Height - itemHeight ) /2 ) );
				pnlList.Invalidate();
                this.SelectionModified = true;
                OnSelectedIndexChanged(EventArgs.Empty);
				bolUserSelecting = false;
			}
		}

		/// <summary>
		/// 设置,返回所有选择的元素的列表
		/// </summary>
        [System.ComponentModel.Browsable( false )]
		public XTreeListBoxItem[] SelectedItems
		{
			get
			{
				List<XTreeListBoxItem > myList = new List<XTreeListBoxItem >();
				foreach( XTreeListBoxItem myItem in this.Items )
                {
					if( myItem.Selected )
                    {
						myList.Add( myItem );
                    }
                }
				return myList.ToArray() ;
			}
            set
            {
                bool changed = false;
                foreach (XTreeListBoxItem myItem in this.Items)
                {
                    myItem.Selected = false;
                    if (value != null)
                    {
                        foreach (XTreeListBoxItem item2 in value)
                        {
                            if (item2 == myItem)
                            {
                                myItem.Selected = true;
                                this.SelectionModified = true;
                                changed = true;
                                break;
                            }
                        }//foreach
                    }//if
                }//foreach
                if (changed)
                {
                    this.OnSelectedIndexChanged(EventArgs.Empty);
                }
            }
		}

		/// <summary>
		/// 返回选中项目的附件对象集合
		/// </summary>
        [System.ComponentModel.Browsable( false )]
		public object[] SelectedTags
		{
			get
			{
				System.Collections.ArrayList myList 
                    = new System.Collections.ArrayList();
				foreach( XTreeListBoxItem myItem in this.Items )
                {
					if( myItem.Selected 
                        && myItem.Style == XTreeListBoxItemStyle.Content)
                    {
						myList.Add( myItem.Tag );
                    }
                }
				return myList.ToArray() ;
			}
		}

		/// <summary>
		/// 返回用户选中的列表项目的附属对象
		/// </summary>
        [System.ComponentModel.Browsable( false )]
		public object SelectedTag
		{
			get
			{
				if( this.SelectedItem != null)
                {
					return this.SelectedItem.Tag ;
                }
				else
                {
					return null;
                }
			}
		}
 
		/// <summary>
		/// 设置,获得当前选中的项目
		/// </summary>
        [System.ComponentModel.Browsable( false )]
		public XTreeListBoxItem SelectedItem
		{
			get
			{
                if (intSelectedIndex >= 0
                    && intSelectedIndex < this.Items.Count)
                {
                    return this.Items[intSelectedIndex];
                }
                else
                {
                    return null;
                }
			}
			set
			{
				this.SelectedIndex = this.Items.IndexOf(value);
			}
		}

        /// <summary>
        /// 选择的字符串数值
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public string SelectedValue
        {
            get
            {
                if (this.MultiSelect)
                {
                    StringBuilder myStr = new StringBuilder();
                    foreach (XTreeListBoxItem myItem in this.Items)
                    {
                        if (myItem.Selected)
                        {
                            if (myStr.Length > 0)
                            {
                                myStr.Append(this.ItemSpliter);
                            }
                            if (myItem.Value != null)
                            {
                                myStr.Append(Convert.ToString(myItem.Value));
                            }
                        }
                    }
                    return myStr.ToString();
                }
                else
                {
                    if (intSelectedIndex >= 0
                        && intSelectedIndex < this.Items.Count)
                    {
                        return Convert.ToString( this.Items[intSelectedIndex].Value );
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            set
            {
                if (value != null)
                {
                    if (this.MultiSelect)
                    {
                        XTreeListBoxItem FirstItem = null;
                        string[] strTexts = value.Split(this.ItemSpliter);
                        if (strTexts != null)
                        {
                            foreach (XTreeListBoxItem myItem in this.Items)
                            {
                                myItem.Selected = false;
                                foreach (string strText in strTexts)
                                {
                                    if (myItem.Value != null)
                                    {
                                        string v = Convert.ToString(myItem.Value);
                                        if ( v == strText )
                                        {
                                            this.SelectionModified = true;
                                            myItem.Selected = true;
                                            if (FirstItem == null)
                                            {
                                                FirstItem = myItem;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                            if (FirstItem != null)
                            {
                                this.SetSelectIndexMiddle(
                                    this.Items.IndexOf(FirstItem));
                            }
                        }
                    }
                    else
                    {
                        foreach (XTreeListBoxItem item in this.Items)
                        {
                            if (item.Value != null)
                            {
                                string v = Convert.ToString(item.Value);
                                if (v == value)
                                {
                                    this.SelectionModified = true;
                                    this.SetSelectedIndex(this.Items.IndexOf(item));
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

		/// <summary>
		/// 返回当前项目的文本和查找指定的文本的项目为当前项目
		/// </summary>
		/// <remarks>
        /// 若设置当前项目文本,若找到匹配的项目则滚动列表使新选中的列表显示在列表中央
        /// </remarks>
        [System.ComponentModel.Browsable( false )]
		public string SelectedText 
		{
			get
			{
				if( this.MultiSelect )
				{
					System.Text.StringBuilder myStr 
                        = new System.Text.StringBuilder();
					foreach( XTreeListBoxItem myItem in this.Items )
					{
						if( myItem.Selected)
						{
							if( myStr.Length > 0 )
                            {
								myStr.Append( this.ItemSpliter);
                            }
							myStr.Append( myItem.Text );
						}
					}
					return myStr.ToString();
				}
				else
				{
                    if (intSelectedIndex >= 0 
                        && intSelectedIndex < this.Items.Count)
                    {
                        return this.Items[intSelectedIndex].Text;
                    }
                    else
                    {
                        return null;
                    }
				}
			}
			set
			{
				if( value != null)
				{
					if( this.MultiSelect )
					{
                        XTreeListBoxItem FirstItem = null;
						string[] strTexts = value.Split(this.ItemSpliter);
						if( strTexts != null)
						{
							foreach( XTreeListBoxItem myItem in this.Items)
							{
								myItem.Selected = false;
								foreach(string strText in strTexts)
								{
                                    if (IsMatchSelection(myItem, strText))
									{
                                        this.SelectionModified = true;
										myItem.Selected = true;
										if( FirstItem == null)
                                        {
											FirstItem = myItem ;
                                        }
										break;
									}
								}
							}
							if( FirstItem != null)
							{
								this.SetSelectIndexMiddle(
                                    this.Items.IndexOf( FirstItem ) );
							}
						}
					}
					else
					{
						XTreeListBoxItem myItem = this.GetItemByText( value, false);
						if( myItem != null)
                        {
                            this.SelectionModified = true;
							this.SetSelectIndexMiddle(
                                this.Items.IndexOf(myItem));
                        }
					}
				}
			}
		}

        /// <summary>
        /// 判断指定的文本是否命中指定的列表元素
        /// </summary>
        /// <param name="item">列表元素</param>
        /// <param name="text">文本值</param>
        /// <returns>是否命中</returns>
        protected virtual bool IsMatchSelection(
            XTreeListBoxItem item,
            string text)
        {
            return item.Text == text;
        }

		/// <summary>
		/// 设置返回当前项目的拼音码
		/// </summary>
        [System.ComponentModel.Browsable( false )]
		public string SelectedSpell
		{
			get
			{
				if( intSelectedIndex >= 0 && intSelectedIndex < this.Items.Count )
					return this.Items[intSelectedIndex].ChineseSpell ;
				else
					return null;
			}
			set
			{
				this.SelectedItem = this.GetItemByChineseSpell( value );
			}
		}

		/// <summary>
		/// 根据文本设置当前列表项目
		/// </summary>
		/// <param name="strText">指定的文本</param>
		/// <param name="StartsWidth">是否匹配项目文本开头</param>
        public void SetSelectedText(string strText, bool StartsWidth)
        {
            XTreeListBoxItem myItem = GetItemByText(strText, StartsWidth);
            if (myItem != null)
            {
                this.SelectedIndex = this.Items.IndexOf(myItem);
            }
        }

		/// <summary>
		/// 内部的设置当前项目编号的函数,以便实现循环设置效果
		/// </summary>
		/// <remarks>本函数出入的项目编号为任意整数,本函数首先使用模运算修正项目编号,然后
		/// 设置当前列表项目的编号为该编号</remarks>
		/// <param name="index">项目编号</param>
		private void SetSelectedIndex( int index)
		{
			if( this.Items.Count > 0 )
			{
				if( index < 0 ) index = 0 ;
                if (index >= this.Items.Count)
                {
                    index = this.Items.Count - 1;
                }
				//index = index % this.Items.Count ;
				//if(index < 0 )
				//	index += this.Items.Count ;
				this.SelectedIndex = index ;
			}
		}

		#endregion

		#region 用户界面相关的函数群 **************************************************************

        private System.Drawing.Size _ViewSize = System.Drawing.Size.Empty;
        /// <summary>
        /// 视图区域大小
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public System.Drawing.Size ViewSize
        {
            get
            {
                return _ViewSize; 
            }
        }

        /// <summary>
        /// 计算视图区域大小
        /// </summary>
        /// <returns>视图区域大小</returns>
        public Size CalculateViewSize()
        {
            System.Drawing.Size result = System.Drawing.Size.Empty;

            if (this.IsDisposed)
            {
                // 若对象已经销毁了则不进行计算
                return result;
            }

            // 计算列表视图区域的高度
            int viewHeight = 0;
            foreach (XTreeListBoxItem myItem in this.Items)
            {
                // 计算列表项目的位置
                myItem.Top = viewHeight;
                viewHeight += GetItemHeight(myItem);
            }// foreach
            // 获得视图高度
            result.Height = viewHeight;
            if (result.Height < 20)
            {
                result.Height = 20;
            }


            // 计算快捷字符的显示宽度
            innerShortCutWidth = 0;
            if (this.ShowShortCut
                && this.HasShortCutCharItem)
            {
                using (System.Drawing.Graphics myGraph = this.CreateGraphics())
                {
                    innerShortCutWidth =
                        (int)(myGraph.MeasureString("(H)", this.Font).Width) + 4;
                }
            }
            // 计算图标的宽度
            innerIconWidth = (this.HasIconItem ? 19 : 0);
            // 计算缩放点的宽度
            innerExpendHandleRectWidth =
                (this.ShowExpendHandleRect && this.HasItemsItem ? ExpendBoxSize + 4 : 0);

			// 计算单位文本的宽度
			int intWidthUnit = 0 ;
			using( System.Drawing.Graphics myGraph = pnlList.CreateGraphics())
			{
				intWidthUnit = (int) myGraph.MeasureString(
                    "H" , 
                    this.Font,
                    1000,
                    System.Drawing.StringFormat.GenericTypographic).Width ;
			}

			// 查找所有列表项目中字节长度最长的文本及最深的层次
			int maxItemWidth		= 0 ;
			System.Text.Encoding myEncode = System.Text.Encoding.GetEncoding(936);
            using (Graphics g = this.pnlList.CreateGraphics())
            {
                foreach (XTreeListBoxItem myItem in this.Items)
                {
                    if (myItem.Style == XTreeListBoxItemStyle.Content)
                    {
                        string txt = GetItemDisplayText(myItem);
                        if (txt == null || txt.Length == 0)
                        {
                            txt = " ";
                        }
                        //int itemWidth = myEncode.GetByteCount(txt)
                        //    * intWidthUnit + myItem.Layer * this.Indent;
                        float width2 = g.MeasureString(txt, pnlList.Font, 10000, StringFormat.GenericTypographic).Width;
                        int itemWidth = (int ) width2 + myItem.Layer * this.Indent;
                        if (itemWidth > maxItemWidth)
                        {
                 
                            maxItemWidth = itemWidth;
                        }
                    }
                }//foreach
            }
			int maxWidth = intLeftMargin + innerIconWidth + innerExpendHandleRectWidth + maxItemWidth + intWidthUnit + 3 ;
            if (this.ShowEditItemButton)
            {
                maxWidth = maxWidth + 20;
            }
            //maxWidth = maxWidth + 20;
            if (maxWidth < 150)
            {
                maxWidth = 150;
            }
            result.Width = maxWidth;
            this._ViewSize = result;
            return result;
        }

        private void UpdateAutoScrollMinSize()
        {
            this.pnlList.AutoScrollMinSize = new Size(10, this.ViewSize.Height);
        }

        ///// <summary>
        ///// 重新计算所有元素的位置和视图区域的高度,并计算列表元素的内部的图形对象位置
        ///// </summary>
        //private void RefreshViewHeight()
        //{
        //    // 计算快捷字符的显示宽度
        //    InnerShortCutWidth = 0 ;
        //    if( this.ShowShortCut 
        //        && this.HasShortCutCharItem )
        //    {
        //        using(System.Drawing.Graphics myGraph = this.CreateGraphics())
        //        {
        //            InnerShortCutWidth =
        //                (int)(myGraph.MeasureString("(H)",this.Font).Width ) + 4 ;
        //        }
        //    }
        //    // 计算图标的宽度
        //    InnerIconWidth = ( this.HasIconItem ? 19 : 0 );
        //    // 计算缩放点的宽度
        //    InnerExpendHandleRectWidth =
        //        ( this.ShowExpendHandleRect && this.HasItemsItem ? ExpendBoxSize + 4 : 0 );
        //    // 计算列表视图区域的高度
        //    intViewHeight = 0 ;
        //    foreach( MyTreeListItem myItem in this.Items)
        //    {
        //        // 计算列表项目的位置
        //        myItem.Top				= intViewHeight ;
        //        intViewHeight			+= myItem.Height ;
        //    }// foreach



        //}// void RefreshViewHeight()

		private int GetItemLeft( XTreeListBoxItem myItem)
		{
			return this.LeftMargin + myItem.Layer * this.Indent ;
		}

        /// <summary>
        /// 获得控件推荐的大小
        /// </summary>
        /// <param name="proposedSize">限制大小</param>
        /// <returns>控件的大小</returns>
        public override Size GetPreferredSize(Size proposedSize)
        {
            if (this.IsDisposed)
            {
                // 对象已经销毁了
                return Size.Empty;
            }
            System.Drawing.Size result = this.CalculateViewSize();
            if (result.Height > pnlList.Height)
            {
                int vScrollWidth = SystemMetricsClass.CYVSCROLL;
                result.Width += vScrollWidth;
            }
            int widthFix = this.Width - pnlList.ClientSize.Width;
            int heightFix = this.Height - pnlList.ClientSize.Height;
            result.Width += widthFix;
            result.Height += heightFix;
            if (this.ShowEditItemButton)
            {
                result.Width += btnEditItem.Width;
            }
            if (proposedSize.IsEmpty)
            {
                return result;
            }
            else
            {
                if (proposedSize.Width > 0 && proposedSize.Width < result.Width)
                {
                    result.Width = proposedSize.Width;
                }
                if (proposedSize.Height > 0 && proposedSize.Height < result.Height)
                {
                    result.Height = proposedSize.Height;
                }
                return result;   
            }
        }
         

        ///// <summary>
        ///// 根据列表内容自动设置列表的大小
        ///// </summary>
        ///// <remarks>
        ///// 本函数计算所有列表项目的宽度,然后根据最大宽度设置本列表的宽度,然后根据所有列表
        ///// 项目的高度和设置列表的高度,并设置列表的高度不超过250
        ///// 本函数为了提高速度,并不直接计算列表项目文本的宽度,而是计算列表项目文本的字节长度在乘
        ///// 上宽度单位来计算的,此处宽度单位为字母'H'在当前字体下的无GDI+修正空白的宽度
        ///// </remarks>
        //public void SetDefaultSize()
        //{
        //    this.RefreshViewHeight();
        //    pnlList.AutoScrollMinSize = new System.Drawing.Size(10, intViewHeight );
        //    int intFix = this.Height - pnlList.ClientSize.Height ;
        //    int DefaultHeight = this.DefaultItemHeight * this.VisibleItems + intFix ;
        //    this.Height =( intViewHeight + intFix > DefaultHeight ? DefaultHeight : intViewHeight + intFix );
        //    //pnlList.AutoScrollMinSize = System.Drawing.Size.Empty ;
        //    int intWidthFix =this.Width - pnlList.ClientSize.Width +  intRightMargin + this.InnerIconWidth + this.InnerShortCutWidth  ;
        //    if( pnlList.ClientSize.Height < intViewHeight )
        //        intWidthFix -= Windows32.User32.GetSystemMetrics((int) Windows32.SystemMetricsConst.SM_CXVSCROLL );
			
        //    // 计算单位文本的宽度
        //    int intWidthUnit = 0 ;
        //    using( System.Drawing.Graphics myGraph = pnlList.CreateGraphics())
        //    {
        //        intWidthUnit = (int) myGraph.MeasureString( "H" , pnlList.Font,1000,System.Drawing.StringFormat.GenericTypographic).Width ;
        //    }

        //    // 查找所有列表项目中字节长度最长的文本及最深的层次
        //    int intMaxItemWidth		= 0 ;
        //    System.Text.Encoding myEncode = System.Text.Encoding.GetEncoding(936);
        //    foreach(MyTreeListItem myItem in this.Items)
        //    {
        //        if( myItem.Style == 0 )
        //        {
        //            int intItemWidth = myEncode.GetByteCount( myItem.Text ) * intWidthUnit + myItem.Layer * intIndent ;
        //            if( intItemWidth > intMaxItemWidth )
        //                intMaxItemWidth = intItemWidth ;
        //        }
        //    }//foreach
        //    int MaxWidth = intLeftMargin + intMaxItemWidth + intWidthFix + intWidthUnit + 3 ;
        //    intWidthFix = this.Width - lblSpell.Width ;
        //    if( MaxWidth < lblSpell.PreferredWidth + intWidthFix )
        //        MaxWidth = lblSpell.PreferredWidth + intWidthFix ;
        //    if( MaxWidth < 150 ) 
        //        MaxWidth = 150 ;
        //    if( intViewHeight > pnlList.ClientSize.Height)
        //    {
        //        this.Width = MaxWidth + Windows32.User32.GetSystemMetrics( (int)Windows32.SystemMetricsConst.SM_CXVSCROLL ); 
        //    }
        //    else
        //    {
        //        this.Width = MaxWidth ;
        //    }
        //}


        /// <summary>
        /// 更新用户界面的操作层次
        /// </summary>
        private int intUpdateLevel = 0;

		/// <summary>
		/// 开始更新内容,调用此函数后,以后对列表的操作并不会更新用户界面
		/// </summary>
		public void BeginUpdate()
		{
			intUpdateLevel ++ ;
		}
		/// <summary>
		/// 结束更新内容,调用此函数后会刷新用户界面
		/// </summary>
		public void EndUpdate()
		{
			intUpdateLevel -- ;
			if( intUpdateLevel <= 0 )
			{
				intUpdateLevel = 0 ;
                this.CalculateViewSize();
                this.UpdateAutoScrollMinSize();
				pnlList.Invalidate();
                this.OnListContentChanged(EventArgs.Empty);
			}
		}
		/// <summary>
		/// 对象是否正在更新内容
		/// </summary>
		/// <returns></returns>
        public bool isUpdateing
        {
            get
            {
                return (intUpdateLevel > 0);
            }
        }

        /// <summary>
        /// 声明指定项目的用户界面无效
        /// </summary>
        /// <param name="myItem">列表项目对象</param>
		public void Invalidate( XTreeListBoxItem myItem)
		{
            if (myItem != null)
            {
                pnlList.Invalidate( GetItemBounds( myItem ));
            }
		}
		
		/// <summary>
		/// 获得指定列表项目前面的缩放矩形对象,若对象为空或不存在缩放特性则返回空矩形对象
		/// </summary>
		/// <param name="myItem"></param>
		/// <returns></returns>
		private System.Drawing.Rectangle GetItemExpendRect(XTreeListBoxItem myItem )
		{
            if (myItem == null)
            {
                throw new ArgumentNullException("myItem");
            }
			if( this.ShowExpendHandleRect 
                && myItem != null
                && myItem.HasItems )
			{
				return XGUICommon.StaticGetExpendHandleRect2(
                    this.LeftMargin + myItem.Layer * intIndent ,
					myItem.Top + this.pnlList.AutoScrollPosition.Y ,
                    GetItemHeight( myItem ) ) ;
			}
			else
            {
				return System.Drawing.Rectangle.Empty ;
	    	}
        }

        /// <summary>
        /// 获得指定列表项目的边界
        /// </summary>
        /// <param name="item">列表项目对象</param>
        /// <returns>获得的边界矩形</returns>
        public Rectangle GetItemBounds(XTreeListBoxItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            Rectangle result = Rectangle.Empty;
            result.X = 0;
            result.Y = item.Top + pnlList.AutoScrollPosition.Y ;
            result.Width = this.ClientSize.Width;
            result.Height = GetItemHeight(item);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlList_Paint( object sender , PaintEventArgs e)
        {
 	        if( this.Items != null && this.isUpdateing == false )
			{
				//System.Drawing.RectangleF ClipRect = e.ClipRectangle ; 
				//ClipRect.Offset( 0 - pnlList.AutoScrollPosition.X , 0 - pnlList.AutoScrollPosition.Y );
				System.Drawing.Graphics myGraph = e.Graphics ;

				// 字体的高度
				float TextHeight = this.Font.GetHeight() + 1; 

				using( System.Drawing.StringFormat ShortCutFormat 
                    = new System.Drawing.StringFormat())
				{
					System.Drawing.Brush myTextBrush = System.Drawing.SystemBrushes.WindowText  ;
					ShortCutFormat.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show ;
					System.Drawing.Rectangle DrawClipRect = e.ClipRectangle ;
					//ClipRect.X -= pnlList.AutoScrollPosition.X ;
					//ClipRect.Y -= pnlList.AutoScrollPosition.Y ;
					System.Drawing.SolidBrush myBackBrush = null;
					int ClientWidth = pnlList.ClientSize.Width ;
					for( int iCount = 0 ;iCount < this.Items.Count ; iCount ++ )
					{
						// 设置当前要绘制的列表项目
						XTreeListBoxItem myItem = this.Items[iCount];
						// 计算列表项目在列表视图中的顶端位置
                        int intItemTop = myItem.Top + pnlList.AutoScrollPosition.Y;
						// 若当前列表项目全部位于绘制区域的下面则提前结束绘制
						if( intItemTop > DrawClipRect.Bottom )
                        {
							break;
                        }
                        int itemHeight = GetItemHeight(myItem);
						// 若当前列表项目和当前绘制区域纵向有相交的部分则绘制列表项目
						if( intItemTop + itemHeight >= DrawClipRect.Top 
                            && intItemTop <= DrawClipRect.Bottom  )
						{
							// 计算列表项目在视图列表中的左端位置
							int intItemLeft = this.LeftMargin + myItem.Layer * this.Indent ;
							// 计算列表项目的文本内容在视图中的左端位置
							int intItemTextLeft = 
                                intItemLeft + innerIconWidth + innerExpendHandleRectWidth ;
							// 计算列表项目文字的边框
							System.Drawing.RectangleF TextRect = new System.Drawing.RectangleF(
                                intItemTextLeft  ,
                                intItemTop + ( itemHeight - TextHeight) / 2, 
								ClientWidth - intItemTextLeft + 111  , 
								TextHeight );
							if( myItem.Style == XTreeListBoxItemStyle.Content )
							{
								// 绘制缩放控制矩形
								if( this.innerExpendHandleRectWidth > 0 )
								{
									if( myItem.HasItems )
									{
                                        Rectangle rect = XGUICommon.StaticGetExpendHandleRect2(
                                                intItemLeft,
                                                intItemTop,
                                                itemHeight);
										XGUICommon.StaticDrawExpendHandle(
                                            myGraph , 
											rect , 
											myItem.Expended ); 
									}
								}
								// 绘制图标
								if( myItem.Icon != null)
                                {
									myGraph.DrawImage(
                                        myItem.Icon ,
                                        intItemLeft + innerExpendHandleRectWidth  ,
                                        TextRect.Top , 
                                        16,
                                        16);
								}
								if( myItem.Selected ) 
								{
									// 若列表项目被选中则高亮度显示文本
									myGraph.FillRectangle(
                                        System.Drawing.SystemBrushes.Highlight , 
										TextRect.Left , 
										TextRect.Top - 1 ,
										TextRect.Width ,
										TextRect.Height  );
									if( char.IsWhiteSpace( myItem.ShortCutChar ) ==false)
                                    {
										myGraph.DrawString(
                                            "(&" + myItem.ShortCutChar + ")" , 
											pnlList.Font , 
											System.Drawing.SystemBrushes.HighlightText ,
											TextRect.Left  , 
											TextRect.Top ,
											ShortCutFormat );
                                    }
									TextRect.X += innerShortCutWidth ;
									//TextRect.Width	-= InnerShortCutWidth ;
									myGraph.DrawString(
                                        GetItemDisplayText( myItem ),
                                        this.Font ,
                                        System.Drawing.SystemBrushes.HighlightText ,
                                        TextRect );
								}
								else
								{
									// 绘制没有选中的列表项目的文本
									if( char.IsWhiteSpace( myItem.ShortCutChar ) ==false)
                                    {
										myGraph.DrawString(
                                            "(&" + myItem.ShortCutChar + ")" , 
											this.Font , 
											myTextBrush ,
											TextRect.Left  , 
											TextRect.Top ,
											ShortCutFormat );
                                    }
									TextRect.X		+= innerShortCutWidth  ;
									//TextRect.Width	-= InnerShortCutWidth ;
									myGraph.DrawString( 
                                        GetItemDisplayText( myItem ) , 
                                        this.Font ,
                                        myTextBrush  ,
                                        TextRect );
								}
                                //if( this.MultiSelect
                                //    && iCount == intSelectedIndex)
                                //{
                                //    myGraph.DrawRectangle(
                                //        System.Drawing.Pens.Red ,
                                //        intItemLeft ,
                                //        intItemTop  ,
                                //        ClientWidth - intItemLeft - 2 ,
                                //        itemHeight -1 );
                                //}
                                //if (this.MultiSelect)
                                //{
                                //    if (myItem == this.SelectedItem)
                                //    {
                                //        myGraph.DrawRectangle(
                                //            System.Drawing.Pens.Red ,
                                //            intItemLeft,
                                //            intItemTop,
                                //            ClientWidth - intItemLeft - 2,
                                //            itemHeight - 1);
                                //    }
                                //}
                                if (myItem == this.HoverListBoxItem)
                                {
                                    myGraph.DrawRectangle(
                                        this.MultiSelect ? System.Drawing.Pens.Red : System.Drawing.Pens.Blue ,
                                        intItemLeft,
                                        intItemTop,
                                        ClientWidth - intItemLeft - 2,
                                        itemHeight - 1);
                                }
							}
							else
							{
								if( myItem.Style == XTreeListBoxItemStyle.Spliter )
								{
                                    // 绘制分隔条项目
                                    using(System.Drawing.Pen myPen =
                                        new System.Drawing.Pen( System.Drawing.Color.Black , 2 ))
									{
										int intMiddle = intItemTop + (int)( itemHeight / 2.0) ;
										myGraph.DrawLine(
                                            myPen ,
                                            intItemLeft + 5  ,
                                            intMiddle ,
                                            ClientWidth - 10 ,
                                            intMiddle );
									}
								}
							}
						}
					}// for
					if( myBackBrush != null)
                    {
						myBackBrush.Dispose();
                    }
				}// using 
			}// if
		}// pnlList_Paint

        /// <summary>
		/// 展开/收缩指定的列表项目
		/// </summary>
		/// <param name="myItem">要展开或收缩的列表项目</param>
		/// <returns>本操作是否造成列表项目的改变</returns>
		private bool ExpendItem( XTreeListBoxItem myItem )
		{
			if( myItem != null && myItem.HasItems )
			{
				bool bolItemsChanged = false;
				if( myItem.Expended )
				{
					int intSubItemCount = 0 ;
					XTreeListBoxItem LastParent = myItem ;
					for(int iCount = this.Items.IndexOf( myItem ) + 1 ;
                        iCount < this.Items.Count ; 
                        iCount ++ )
					{
						XTreeListBoxItem myListItem = this.Items[iCount];
						if( myListItem.Parent != LastParent )
						{
							LastParent = myListItem.Parent ;
                            while (LastParent != null && LastParent != myItem)
                            {
                                LastParent = LastParent.Parent;
                            }
                            if (LastParent != myItem)
                            {
                                break;
                            }
						}
						intSubItemCount ++ ;
					}//for
					if( intSubItemCount > 0 )
					{
						this.Items.RemoveRange(
                            this.Items.IndexOf( myItem ) +1 , 
                            intSubItemCount );
						bolItemsChanged = true;
					}
				}
				else
				{
					List<XTreeListBoxItem> myChildItems
                        = new List<XTreeListBoxItem>();
					this.OnLoadChildItems( myItem , myChildItems );
					if( myChildItems.Count > 0 )
					{
						foreach(XTreeListBoxItem mySubItem in myChildItems)
						{
							mySubItem.Layer = myItem.Layer + 1;
							mySubItem.Parent = myItem ;
						}
						this.Items.InsertRange( 
                            this.Items.IndexOf( myItem) +1 ,
                            myChildItems);
						bolItemsChanged = true;
					}
					else
					{
						myItem.HasItems = false;
					}
				}
				myItem.Expended = ! myItem.Expended ;
                if (bolItemsChanged)
                {
                    this.RefreshChineseSpell(false);
                    this.CalculateViewSize();
                    this.UpdateAutoScrollMinSize();
                    this.OnListContentChanged(EventArgs.Empty);
                    pnlList.Refresh();
                }
                else
                {
                    this.Invalidate(myItem);
                }
				return  bolItemsChanged ;
			}
			return false;
		}

		private void pnlList_DoubleClick(object sender, EventArgs e)
		{
			if( this.SelectedItem != null)
			{
				System.Drawing.Rectangle ExpendRect = this.GetItemExpendRect( this.SelectedItem );
				if( ExpendRect.IsEmpty == false)
				{
					System.Drawing.Point p = System.Windows.Forms.Control.MousePosition ;
					p = pnlList.PointToClient( p );
                    if (p.X <= ExpendRect.Left + innerExpendHandleRectWidth)
                    {
                        return;
                    }
					//if( ExpendRect.Contains( p )) return ;
				}
                OnUserAcceptSelection(EventArgs.Empty);
			}
		}

        private Point lastMousePosition = new Point(-100, -100);

        private bool _MouseMoveChangeSelectedIndex = true;
        /// <summary>
        /// 仅仅使用鼠标移动就能修改选择项目
        /// </summary>
        [System.ComponentModel.DefaultValue( true )]
        [System.ComponentModel.Category("Behavior")]
        public bool MouseMoveChangeSelectedIndex
        {
            get
            {
                return _MouseMoveChangeSelectedIndex; 
            }
            set
            {
                _MouseMoveChangeSelectedIndex = value; 
            }
        }

        private XTreeListBoxItem _HoverListBoxItem = null;
        /// <summary>
        /// 鼠标悬停下的列表项目
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public XTreeListBoxItem HoverListBoxItem
        {
            get
            {
                if (_HoverListBoxItem != null)
                {
                    if (this.Items.Contains(_HoverListBoxItem) == false)
                    {
                        _HoverListBoxItem = null;
                    }
                }
                return _HoverListBoxItem; 
            }
            set
            {
                if (_HoverListBoxItem != value)
                {
                    if (_HoverListBoxItem != null)
                    {
                        this.Invalidate(_HoverListBoxItem);
                    }
                    _HoverListBoxItem = value;
                    if (_HoverListBoxItem != null)
                    {
                        this.Invalidate(_HoverListBoxItem);
                        this.ScrollToView(_HoverListBoxItem);
                    }
                }
            }
        }


        private void pnlList_MouseLeave(object sender, EventArgs e)
        {
            this.HoverListBoxItem = null;
        }

		private void pnlList_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(bolUserSelecting == false )
			{
                if (lastMousePosition.X == e.X && lastMousePosition.Y == e.Y)
                {
                    // 过滤掉重复的鼠标移动消息
                    return;
                }
                lastMousePosition = new Point(e.X, e.Y);
                //if (DateTime.Now.Second >45)
                //{
                //    System.Console.WriteLine("");
                //}
                bool mback = this.SelectionModified;
				XTreeListBoxItem myItem = this.HitTest(e.X , e.Y );

                if (this.MultiSelect)
                {
                    this.HoverListBoxItem = myItem;
                }
                else
                {
                    if (this.MouseMoveChangeSelectedIndex == false)
                    {
                        this.HoverListBoxItem = myItem;
                    }
                    else
                    {
                        this.SelectedItem = myItem;
                    }
                }
                if (myItem == null || myItem.Style == XTreeListBoxItemStyle.Spliter)
                {
                    return;
                }

                //if (myItem != null)
                //{
                //    if( this.MouseMoveChangeSelectedIndex )
                //    {
                //        this.SelectedItem = myItem;
                //    }
                //}
                //else
                //{
                //    this.SelectedIndex = -1;
                //}
                if (this.ShowEditItemButton)
                {
                    btnEditItem.Left = pnlList.ClientSize.Width - btnEditItem.Width - 1;
                    btnEditItem.Top = myItem.Top
                        + (GetItemHeight(myItem) - btnEditItem.Height) / 2
                        + pnlList.AutoScrollPosition.Y;
                    btnEditItem.Visible = true;
                    btnEditItem.Tag = myItem;
                }
                else
                {
                    btnEditItem.Visible = false;
                }
                this.SelectionModified = mback;
			}
		}

        private bool _ExpandSubItemsWhenClickItem = true;
        /// <summary>
        /// 如果列表项目有子项目，则鼠标点击项目时将扩展或收缩子项目
        /// </summary>
        [System.ComponentModel.DefaultValue( true )]
        public bool ExpandSubItemsWhenClickItem
        {
            get
            {
                return _ExpandSubItemsWhenClickItem; 
            }
            set
            {
                _ExpandSubItemsWhenClickItem = value; 
            }
        }

		private void pnlList_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            if (this.Visible == false)
            {
                return;
            }
			// 若列表多选且用户右击则认为用户确定选择
			if( e.Button == System.Windows.Forms.MouseButtons.Right  
                && this.MultiSelect )
			{
                OnUserAcceptSelection(EventArgs.Empty);
				return ;
			}
			
			XTreeListBoxItem myItem = this.HitTest(e.X , e.Y);
			if( myItem != null && myItem.Style == 0 )
			{
				if (this.ExpandSubItemsWhenClickItem)
                {
                    if (myItem.HasItems)
                    {
                        // 若点中展开收缩点则展开或收缩列表项目
                        ExpendItem(myItem);
                        return;
                    }
                }

				System.Drawing.Rectangle ExpendRect = this.GetItemExpendRect( myItem );
				if( ExpendRect.IsEmpty == false 
                    && e.X <= ExpendRect.Left + innerExpendHandleRectWidth   )
				{
                    if (ExpendRect.Contains(e.X, e.Y))
                    {
                        ExpendItem(myItem);
                    }
					return ;
				}
				if( this.MultiSelect)
				{
					myItem.Selected  = ! myItem.Selected ;
                    if (myItem.Selected)
                    {
                        this.SelectedIndex = this.Items.IndexOf(myItem);
                    }
                    this.SelectionModified = true;
                    this.OnSelectedIndexChanged(EventArgs.Empty);
					pnlList.Refresh();
				}
				else
				{
                    this.SelectionModified = true;
					this.SelectedIndex = this.Items.IndexOf( myItem );
					pnlList.Refresh();
                    OnUserAcceptSelection(EventArgs.Empty);
				}
			}
		}

		//		protected override void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)
		//		{
		//			this.AutoScrollPosition = new System.Drawing.Point(0,0- e.Delta  - this.AutoScrollPosition.Y );
		////		}
		//		protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
		//		{
		//			base.OnKeyDown (e);
		//		}

         
		#endregion


		#region IMessageFilter 成员 ***************************************************************
		/// <summary>
		/// 本函数实现 System.Windows.Forms.IMessageFilter接口的PreFilterMessage函数,用于对当前应用程序的消息进行预处理
		/// </summary>
		/// <remarks>本过滤器处理了鼠标按钮按下时间和键盘事件以及鼠标滚轮事件,
		/// 若鼠标在本列表边框外部按下则表示认为用户想要关闭列表,此时立即关闭本列表窗体
		/// 此外本函数还处理汉语拼音辅助定位列表元素
		/// 用户可用上下光标键或者上下翻页键来滚动列表,用空格或回车来进行选择</remarks>
		/// <param name="m">当前消息队列中的消息对象</param>
		/// <returns>true 当前消息其他程序不可处理(本消息被吃掉了) false本消息还可被其他程序处理</returns>
		public bool PreFilterMessage(ref System.Windows.Forms.Message m)
		{
			bool eatMessage = false;
			if( this.IsDisposed  
                || this.Disposing 
                || this.Visible == false  )
			{
				// 若对象被销毁了或列表不显示则删除本消息过滤器
				return false;
			}
			// 获得窗体的绝对位置和大小
			System.Drawing.Rectangle BoundsRect = this.Bounds ;
            //if (m.Msg == (int)Msgs.WM_MOUSEMOVE
            //    || m.Msg == (int)Msgs.WM_LBUTTONDOWN
            //    || m.Msg == (int)Msgs.WM_LBUTTONUP
            //    || m.Msg == (int)Msgs.WM_LBUTTONDBLCLK
            //    || m.Msg == (int)Msgs.WM_RBUTTONUP
            //    || m.Msg == (int)Msgs.WM_RBUTTONDBLCLK)
            //{
            //    // 鼠标事件
            //    System.Drawing.Point mousePos = new System.Drawing.Point((int)m.LParam);
            //    if (pnlList.Bounds.Contains(mousePos))
            //    {
            //        User32.SendMessage(
            //            (int)pnlList.Handle,
            //            m.Msg,
            //            (uint)m.WParam,
            //            (uint)m.LParam);
            //    }
            //    return true;
            //}
            
			if ( m.Msg == (int)Msgs.WM_MOUSEWHEEL)
			{
                // 鼠标滚轮事件
                System.Drawing.Point mousePos = new System.Drawing.Point((int)m.LParam);
				mousePos = this.PointToClient(mousePos);
				if( pnlList.Bounds.Contains(mousePos))
				{
                    User32.SendMessage(
                        pnlList.Handle ,
                        m.Msg ,
                        (uint)m.WParam ,
                        (uint)m.LParam);
				}
                return true;
			}
            if (m.Msg == (int)Msgs.WM_LBUTTONDOWN)
            {
                System.Console.WriteLine("");
                //return false;
            }
            //if (MouseMessageHelper.IsMouseMessage(m.Msg))
            //{
            //    if (m.HWnd == pnlList.Handle)
            //    {
            //        // 不过滤列表框的鼠标事件
            //        return false;
            //    }
            //    //Point p = MouseMessageHelper.GetScreenMousePosition(m);
            //    //p = pnlList.PointToClient(p);
            //    //if (pnlList.ClientRectangle.Contains(p))
            //    {
            //        if (m.Msg == MouseMessageHelper.WM_LBUTTONDOWN
            //            || m.Msg == MouseMessageHelper.WM_LBUTTONUP
            //            || m.Msg == MouseMessageHelper.WM_LBUTTONDBLCLK
            //            || m.Msg == MouseMessageHelper.WM_RBUTTONDOWN
            //            || m.Msg == MouseMessageHelper.WM_RBUTTONUP
            //            || m.Msg == MouseMessageHelper.WM_RBUTTONDBLCLK
            //            || m.Msg == MouseMessageHelper.WM_MOUSEMOVE)
            //        {
            //            Point p2 = MouseMessageHelper.GetScreenMousePosition(m);
            //            p2 = pnlList.PointToClient(p2);
            //            if (pnlList.ClientRectangle.Contains(p2))
            //            {
            //                User32.SendMessage(
            //                    (int)pnlList.Handle,
            //                    m.Msg,
            //                    (uint)m.WParam,
            //                    (uint) MouseMessageHelper.GetLParam( p2 ));
            //            }
            //        }
            //    }
            //    return true;
            //}

            if (m.Msg == (int)Msgs.WM_KEYDOWN 
                && m.WParam.ToInt32() ==  ( int ) VirtualKeys.VK_BACK )
            {
                m.Msg = (int)Msgs.WM_CHAR;
                m.WParam = new IntPtr( (int)Keys.Back);
            }
			// 键盘字符消息
            if (m.Msg == (int)Msgs.WM_CHAR && lblSpell.Visible)
			{
				bool bolChanged = false;
                if (strTitle != null && lblSpell.Text == strTitle)
                {
                    lblSpell.Text = "";
                }
                switch ((int)m.WParam)
				{
					case (int) System.Windows.Forms.Keys.Back :
						if( lblSpell.Text.Length > 0 )
						{
							lblSpell.Text = lblSpell.Text.Substring(0,lblSpell.Text.Length - 1);
							bolChanged = true;
						}
						else
						{
							if(	OnBackSpace() == false)
							{
                                OnUserCancel(EventArgs.Empty);
							}
						}
						break;
					default:
						if( char.IsWhiteSpace( (char)m.WParam) == false)
						{
							lblSpell.Text = lblSpell.Text + ((char)m.WParam).ToString().ToUpper();
							bolChanged = true;
						}
						break;
				}
                if (bolChanged)
                {
                    UpdateChineseSpell(this.lblSpell.Text);
                }
                if (strTitle != null && lblSpell.Text == "")
                {
                    lblSpell.Text = strTitle;
                }
                return true;
			}

			// 键盘按键按下消息
			if( m.Msg == (int) Msgs.WM_KEYDOWN )
			{
				XTreeListBoxItem currentItem = this.SelectedItem ;
				eatMessage = true;
				switch((int)m.WParam)
				{
                    //case (int)VirtualKeys.VK_BACK:
                    //    // 退格键
                    //    if (lblSpell.Visible)
                    //    {
                            
                    //    }
                    //    eatMessage = true;
                    //    break;
                    case (int)VirtualKeys.VK_CONTROL: // Ctl键，进行列表项目的展开或者收缩
						if( this.ShowExpendHandleRect )
						{
							XTreeListBoxItem myItem = this.SelectedItem ;
							this.ExpendItem( myItem );
						}
						break;
					case (int)VirtualKeys.VK_LEFT :
						eatMessage = false;
						if( this.ShowExpendHandleRect && currentItem != null )
						{
							if( currentItem.HasItems && currentItem.Expended )
							{
								ExpendItem( currentItem );
								eatMessage = true;
							}
							else if( currentItem.Parent != null)
							{
								this.SelectedItem = currentItem.Parent ;
								eatMessage = true;
							}
						}
                        eatMessage = true;////////////////
						break;
					case (int)VirtualKeys.VK_RIGHT :
						eatMessage = false;
						if( this.ShowExpendHandleRect && currentItem != null )
						{
							if( currentItem.HasItems && currentItem.Expended == false)
							{
								ExpendItem( currentItem );
								eatMessage = true;
							}
							else
							{
								int index = this.SelectedIndex + 1 ;
								if( this.SelectedIndex >= 0 && this.SelectedIndex < this.Items.Count -1 )
								{
									XTreeListBoxItem NextItem = ( XTreeListBoxItem ) this.Items[this.SelectedIndex + 1] ;
									if( NextItem.Parent == this.SelectedItem )
									{
										this.SelectedItem = NextItem ;
										eatMessage = true;
									}
								}
							}
						}
                        eatMessage = true;////////////////
						break;
					case (int)VirtualKeys.VK_UP:				// 上移一个项目
                        KeyMoveItem(-1);
						break;
					case (int)VirtualKeys.VK_DOWN:			// 下移一个项目
                        KeyMoveItem(1);
						break;
					case (int) System.Windows.Forms.Keys.PageDown :		// 上翻页
                        KeyMoveItem(10);
						break;
					case (int)System.Windows.Forms.Keys.PageUp :		// 下翻页
                        KeyMoveItem(-10);
						break;
                    case (int) Keys.Home :// 快速移动到第一个元素
                        KeyMoveItem(-100000);
                        break;
                    case ( int) Keys.End :// 快速移动到最后一个元素
                        KeyMoveItem(100000);
                        break;
					case (int)VirtualKeys.VK_SPACE :			// 空格键,表示确定选择
						eatMessage = true;
						if( this.MultiSelect )
						{
                            XTreeListBoxItem myItem = this.HoverListBoxItem;
							if( myItem != null)
							{
								myItem.Selected = !myItem.Selected ;
                                OnSelectedIndexChanged(EventArgs.Empty);
								this.Invalidate( myItem );
							}
						}
						else
						{
                            OnUserAcceptSelection(EventArgs.Empty);
						}
						break;
					case (int)VirtualKeys.VK_RETURN:			// 回车键,表示确定选择
                        OnUserAcceptSelection(EventArgs.Empty);
						eatMessage = true;
						break;
					case (int)VirtualKeys.VK_ESCAPE:			// ESC 键,表示取消选择
						// User wants to exit the menu, so set the flag to exit the message loop but 
						// let the message get processed. This way the key press is thrown away.
                        OnUserCancel(EventArgs.Empty);
						eatMessage = true;
						break;
                    default:		
						eatMessage = false;
						if( bolShowShortCut )
						{
							// 检测快捷键设置
							int intwParam = m.WParam.ToInt32();
							foreach(XTreeListBoxItem myItem in this.Items)
							{
								if(  char.IsWhiteSpace( myItem.ShortCutChar )== false)
								{
									if( (int) myItem.ShortCutChar == intwParam )
									{
										this.SelectedItem = myItem ;
                                        OnUserAcceptSelection(EventArgs.Empty);
										eatMessage = true ;
										break;
									}//if
								}//if
							}//foreach
						}//if
						// 其他键消息不作处理
						break;
				}
			}
			// 处理消息		
			return eatMessage;
		}

        private void KeyMoveItem(int step)
        {
            if (this.MultiSelect)
            {
                if (this.HoverListBoxItem == null)
                {
                    this.HoverListBoxItem = this.Items[0];
                }
                else
                {
                    int index = this.Items.IndexOf(this.HoverListBoxItem) + step ;
                    if (index < 0)
                    {
                        index = 0;
                    }
                    else if (index >= this.Items.Count)
                    {
                        index = this.Items.Count - 1;
                    }
                    this.HoverListBoxItem = this.Items[index];
                }
            }
            else
            {
                int index = this.SelectedIndex + step;
                if (index < 0)
                {
                    index = 0;
                }
                else if (index >= this.Items.Count)
                {
                    index = this.Items.Count - 1;
                }
                SetSelectedIndex( index );
            }
        }

        /// <summary>
        /// 当前用户输入的拼音码
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.ComponentModel.DesignerSerializationVisibility( System.ComponentModel.DesignerSerializationVisibility.Hidden )]
        public string SpellText
        {
            get
            {
                return lblSpell.Text;
            }
            set
            {
                lblSpell.Text = value;
            }
        }

        /// <summary>
        /// 用户输入的拼音码发生改变事件
        /// </summary>
        public event System.ComponentModel.CancelEventHandler SpellTextChanged = null;

		/// <summary>
		/// 根据当前输入的拼音码来设置当前元素
		/// </summary>
		protected virtual void UpdateChineseSpell( string strSpell )
		{
            if (SpellTextChanged != null)
            {
                // 调用事件
                System.ComponentModel.CancelEventArgs args = new System.ComponentModel.CancelEventArgs();
                args.Cancel = false ;
                SpellTextChanged(this, args);
                if (args.Cancel)
                {
                    // 用户取消后续操作
                    return;
                }
            }

			bolUserSelecting = true;
			foreach(XTreeListBoxItem myItem in this.Items )
			{
				if( myItem.Layer == 0 )
				{
					if( myItem.ChineseSpell != null 
						&& ( myItem.ChineseSpell.StartsWith( strSpell )
                        || myItem.Text.StartsWith( strSpell) ))
					{
                        if (this.MultiSelect)
                        {
                            this.HoverListBoxItem = myItem;
                        }
                        else
                        {
                            SetSelectIndexMiddle(this.Items.IndexOf(myItem));
                        }
						//SetSelectIndexMiddle( this.Items.IndexOf( myItem ));
						break;
					}
				}
			}
			bolUserSelecting = false;
		}

		/// <summary>
		/// 需要重载,处理退格键
		/// </summary>
		/// <remarks>当允许用户进行拼音码输入时,用户可以输入字母或者退格键来修改当前的拼音码
		/// 当当前拼音码为空时,用户还是输入的退格键,此时将调用该函数进行退格键处理</remarks>
		/// <returns>用户是否处理了BackSpace键</returns>
		protected virtual bool OnBackSpace()
		{
			return false;
		}

		#endregion
         
		
		protected override void Dispose(bool disposing)
		{
			if( myImageList != null)
			{
				myImageList.Dispose();
				myImageList = null;
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// 是否显示拼音输入框
		/// </summary>
        [System.ComponentModel.DefaultValue( true )]
		public bool ShowSpell
		{
			get
            {
                return lblSpell.Visible ;
            }
			set
			{
				lblSpell.Visible = value;
                if (value)
                {
                    this.pnlList.BorderStyle = BorderStyle.Fixed3D;
                }
                else
                {
                    this.pnlList.BorderStyle = BorderStyle.None;
                }
			}
		}
    }


    /// <summary>
    /// 弹出式列表项目,用于表示弹出式列表中的一个列表项目
    /// </summary>
    [Serializable]
    public class XTreeListBoxItem
    {
        /// <summary>
        /// 列表项目的类型
        /// </summary>
        public XTreeListBoxItemStyle Style = XTreeListBoxItemStyle.Content;
        /// <summary>
        /// 列表项目的文本
        /// </summary>
        public string Text = null;
        /// <summary>
        /// 列表项目的数据
        /// </summary>
        public object Value = null;
        /// <summary>
        /// 显示列表项目数据时使用的格式化字符串
        /// </summary>
        public string ValueFormatString = null;
        /// <summary>
        /// 列表项目的图标编号
        /// </summary>
        public int ImageIndex = -1;
        /// <summary>
        /// 列表项目是否选中标志
        /// </summary>
        public bool Selected = false;
        /// <summary>
        /// 提示文本
        /// </summary>
        public string ToolTip = null;
        /// <summary>
        /// 文本前景色
        /// </summary>
        public System.Drawing.Color ForeColor
            = System.Drawing.SystemColors.WindowText;
        /// <summary>
        /// 文本背景色
        /// </summary>
        public System.Drawing.Color BackColor
            = System.Drawing.SystemColors.Window;
        /// <summary>
        /// 项目包含的对象
        /// </summary>
        public object Tag = null;
        /// <summary>
        /// 图标图片对象
        /// </summary>
        public System.Drawing.Image Icon = null;
        /// <summary>
        /// 汉语拼音码
        /// </summary>
        public string ChineseSpell = null;
        /// <summary>
        /// 快捷键字符
        /// </summary>
        public char ShortCutChar = ' ';
        /// <summary>
        /// 下级列表元素的集合
        /// </summary>
        public List<XTreeListBoxItem> Items = null;
        /// <summary>
        /// 是否拥有下级元素
        /// </summary>
        public bool HasItems = true;
        /// <summary>
        /// 本节点是否已经展开
        /// </summary>
        public bool Expended = false;
        /// <summary>
        /// 项目高度
        /// </summary>
        public int Height = 0;
        /// <summary>
        /// 扩展数据
        /// </summary>
        public object ExternalValue = null;

        ///// <summary>
        ///// 列表项目在列表中的左端位置
        ///// </summary>
        //internal int Left = 0;
        /// <summary>
        /// 项目在视图中的顶端位置
        /// </summary>
        internal int Top = 0;
        
        /// <summary>
        /// 本节点所在层次
        /// </summary>
        internal int Layer = 0;
        /// <summary>
        /// 父列表项目
        /// </summary>
        internal XTreeListBoxItem Parent = null;
    }// public class ListItem 

    /// <summary>
    /// 列表项目样式
    /// </summary>
    public enum XTreeListBoxItemStyle
    {
        /// <summary>
        /// 正常内容列表
        /// </summary>
        Content,
        /// <summary>
        /// 拆分条
        /// </summary>
        Spliter
    }

    public delegate void MyTreeListBoxEventHandler(
        object sender ,
        XTreeListBoxEventArgs args );

    public class XTreeListBoxEventArgs : EventArgs
    {
        private XTreeListBoxItem _ListItem = null;

        public XTreeListBoxItem ListItem
        {
            get { return _ListItem; }
            set { _ListItem = value; }
        }
    }

    public delegate void XTreeListBoxLoadChildItemsEventHandler(
        object sender ,
        XTreeListBoxLoadChildItemsEventArgs args );


    public class XTreeListBoxLoadChildItemsEventArgs : EventArgs
    {
        public XTreeListBoxLoadChildItemsEventArgs(XTreeListBoxItem parentItem)
        {
            _ParentItem = parentItem;
        }

        private XTreeListBoxItem _ParentItem = null;

        public XTreeListBoxItem ParentItem
        {
            get { return _ParentItem; }
        }

        private List<XTreeListBoxItem> _ChildItems = new List<XTreeListBoxItem>();

        public List<XTreeListBoxItem> ChildItems
        {
            get { return _ChildItems; }
            set { _ChildItems = value; }
        }
    }
}