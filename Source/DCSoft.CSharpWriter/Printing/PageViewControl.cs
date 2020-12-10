/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using DCSoft.WinForms;
using DCSoft.Drawing ;
using DCSoft.Common;
using System.Drawing;
using System.Windows.Forms;

namespace DCSoft.Printing
{
    /// <summary>
	/// 带分页功能的视图控件
	/// </summary>
    [DocumentComment()]
    [System.ComponentModel.ToolboxItem(false)]
	public class PageViewControl : DocumentViewControl
	{
		//protected UpdateLock myUpdateLock = new UpdateLock();
		/// <summary>
		/// 无作为的初始化对象
		/// </summary>
		public PageViewControl()
		{
            base.myTransform = new MultiPageTransform();
			//myUpdateLock.BindControl = this ;
		}

		/// <summary>
		/// 页面显示模式
		/// </summary>
		private PageViewMode intViewMode = PageViewMode.Page ;
		/// <summary>
		/// 页面显示模式
		/// </summary>
		[System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DefaultValue( PageViewMode.Page )]
		public PageViewMode ViewMode
		{
			get
            {
                return intViewMode;
            }
			set
            {
                intViewMode = value;
            }
		}

		/// <summary>
		/// 当前页对象
		/// </summary>
		protected PrintPage myCurrentPage = null;
		/// <summary>
		/// 当前页对象
		/// </summary>
		[System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(
            System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public PrintPage CurrentPage
		{
			get
            {
                return myCurrentPage ;
            }
			set
			{
				myCurrentPage = value;
                if (myCurrentPage != null)
                {
                    if (this.ViewMode == PageViewMode.Page)
                    {
                        // 当前已分页模式显示,则需要滚动视图
                        int y = myCurrentPage.ClientBounds.Top;
                        this.SetAutoScrollPosition(
                            new System.Drawing.Point(this.AutoScrollPosition.X, y));
                    }
                }
			}
		}
        
        /// <summary>
        /// 当前页号
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(
            System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int CurrentPageIndex
        {
            get
            {
                if (myCurrentPage == null)
                {
                    return 0;
                }
                else
                {
                    return myCurrentPage.PageIndex;
                }
            }
            set
            {
                if (this.Pages != null && value >= 0 && value < this.Pages.Count)
                {
                    this.CurrentPage = this.Pages[value];
                }
            }
        }

		/// <summary>
		/// 更新当前页面对象
		/// </summary>
		/// <returns>操作是否改变了当前页面对象</returns>
		protected bool UpdateCurrentPage()
		{
			if( _Pages != null )
			{
				MultiPageTransform trans = ( MultiPageTransform ) this.myTransform ;

				PrintPage cpage = null;
				System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
					- this.AutoScrollPosition.X ,
					- this.AutoScrollPosition.Y ,
					this.ClientSize.Width , 
					this.ClientSize.Height );

				int MaxHeight = 0 ;
				foreach( PrintPage page in _Pages )
				{
					System.Drawing.Rectangle rect2 = System.Drawing.Rectangle.Intersect( 
                        page.ClientBounds , rect );
					if( ! rect2.IsEmpty )
					{
						if( MaxHeight < rect2.Height )
						{
							cpage = page;
							MaxHeight = rect2.Height ;
						}
					}
				}
				if( cpage != myCurrentPage )
				{
					myCurrentPage = cpage ;
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 当前页改变事件
		/// </summary>
		public event System.EventHandler CurrentPageChanged = null;
		/// <summary>
		/// 当前页改变事件处理
		/// </summary>
        protected virtual void OnCurrentPageChanged()
        {
            if (CurrentPageChanged != null)
            {
                CurrentPageChanged(this, null);
            }
        }

		/// <summary>
		/// 页面集合
		/// </summary>
		private PrintPageCollection _Pages = new PrintPageCollection() ;
		/// <summary>
		/// 页面集合
		/// </summary>
		[System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(
            System.ComponentModel.DesignerSerializationVisibility.Hidden )]
		public PrintPageCollection Pages
		{
			get
            {
                return _Pages ;
            }
			set
            {
                _Pages = value;
            }
		}

        //private ViewTransformMode _ViewTransformMode = ViewTransformMode.Normal;
        ///// <summary>
        ///// 视图坐标转换模式
        ///// </summary>
        //[System.ComponentModel.Browsable( false )]
        //public ViewTransformMode ViewTransformMode
        //{
        //    get
        //    {
        //        return _ViewTransformMode; 
        //    }
        //    set
        //    {
        //        _ViewTransformMode = value; 
        //    }
        //}

        private SimpleRectangleTransform _CurrentTransformItem = null;
        /// <summary>
        /// 当前转换信息对象
        /// </summary>
        protected SimpleRectangleTransform CurrentTransformItem
        {
            get
            {
                return _CurrentTransformItem; 
            }
            set
            {
                _CurrentTransformItem = value; 
            }
        }


        /// <summary>
        /// 将控件客户区域中的坐标转换为文档视图区中的坐标
        /// </summary>
        /// <param name="x">控件客户区坐标值</param>
        /// <param name="y">控件客户区坐标值</param>
        /// <param name="mode">转换模式</param>
        /// <returns>转换结果</returns>
        public PointF ClientPointToView(float x, float y, ViewTransformMode mode)
        {
            SimpleRectangleTransform item = GetTransformItem(x, y, mode);
            if (item != null)
            {
                return item.UnTransformPointF(x, y);
            }
            else
            {
                return new PointF(float.NaN, float.NaN);
            }
        }

        /// <summary>
        /// 获得指定客户区坐标点所在的坐标转换对象
        /// </summary>
        /// <param name="clientX">客户区X坐标</param>
        /// <param name="clientY">客户区Y坐标</param>
        /// <param name="mode">转换模式</param>
        /// <returns>获得的转换对象</returns>
        public SimpleRectangleTransform GetTransformItem(
            float clientX, 
            float clientY, 
            ViewTransformMode mode)
        {
            this.RefreshScaleTransform();
            SimpleRectangleTransform item = null;
            switch (mode)
            {
                case ViewTransformMode.Normal:
                    item = this.PagesTransform.GetByDescPoint(clientX, clientY );
                    break;
                case ViewTransformMode.Absolute:
                    item = this.PagesTransform.GetByDescPointAbsolute( clientX , clientY );
                    break;
                case Printing.ViewTransformMode.LimitedCurrentItem:
                    item = this.CurrentTransformItem;
                    break;
            }
            return item;
        }

        /// <summary>
        /// 将文档视图区域中的坐标转换为控件客户区中的坐标
        /// </summary>
        /// <param name="x">视图坐标值</param>
        /// <param name="y">视图坐标值</param>
        /// <param name="mode">转换模式</param>
        /// <returns>转换结果</returns>
        public PointF ViewPointToClient(float x, float y , ViewTransformMode mode )
        {
            this.RefreshScaleTransform();
            SimpleRectangleTransform item = null ;
            switch ( mode )
            {
                case ViewTransformMode.Normal :
                    item = this.PagesTransform.GetBySourcePoint( x , y );
                    break;
                case ViewTransformMode.Absolute :
                    item = this.PagesTransform.GetBySourcePointAbsolute( x , y );
                    break;
                case Printing.ViewTransformMode.LimitedCurrentItem :
                    item = this.CurrentTransformItem;
                    break;
            }
            if (item != null)
            {
                return item.TransformPointF(x, y);
            }
            else
            {
                return new PointF(float.NaN, float.NaN) ;
            }
        }

		//private System.Drawing.Printing.Margins myClientMargins = new System.Drawing.Printing.Margins();

		//private System.Drawing.Size myClientPageSize = System.Drawing.Size.Empty ;

        /// <summary>
        /// 刷新坐标转换信息
        /// </summary>
		protected override void RefreshScaleTransform()
		{
			MultiPageTransform trans = ( MultiPageTransform ) this.myTransform ;
			intGraphicsUnit = _Pages.GraphicsUnit ;
			trans.Rate = GraphicsUnitConvert.GetRate(
                intGraphicsUnit ,
                System.Drawing.GraphicsUnit.Pixel );

			//float rate = ( float )( 1.0 / this.ClientToViewXRate );
			//trans.Pages = myPages ;
			//trans.Refresh( rate , this.intPageSpacing );
			System.Drawing.Point sp = this.AutoScrollPosition ;
			trans.ClearSourceOffset();
			trans.OffsetSource( sp.X , sp.Y , true );
		}

		/// <summary>
		/// 分页坐标转换对象
		/// </summary>
		[System.ComponentModel.Browsable( false )]
		public MultiPageTransform PagesTransform
		{
			get
            {
                return ( MultiPageTransform ) this.Transform ;
            }
		}

        /// <summary>
		/// 根据分页信息更新页面排布
		/// </summary>
		public virtual void UpdatePages()
		{
            if (StackTraceHelper.CheckRecursion())
            {
                // 检查递归
                return;
            }
			float rate = ( float )( 1.0 / this.ClientToViewXRate );
            if (this.ViewMode == PageViewMode.Normal)
            {
                this.PagesTransform.Clear();
                this.PagesTransform.ClearSourceOffset();
                SimpleRectangleTransform item = new SimpleRectangleTransform();
                item.PageIndex = 0;
                item.DescRectF = new RectangleF(0, 0, this.Pages.GetPageMaxWidth(), this.Pages.Height);
                item.SourceRectF = new RectangleF(0, 0, item.DescRectF.Width * rate, item.DescRectF.Height * rate);
                item.PageObject = this.Pages[0];
                item.ContentStyle = PageContentPartyStyle.Body;
                item.Enable = true;
                item.DocumentObject = this.Pages[0].Document;
                this.PagesTransform.Add(item);
                this.PagesTransform.ClearSourceOffset();
                this.RefreshScaleTransform();
                Size msize = new Size(
                    ( int ) Math.Ceiling( item.SourceRectF.Width) ,
                    ( int ) Math.Ceiling( item.SourceRectF.Height ));
                if (this.AutoScrollMinSize.Equals(msize) == false)
                {
                    this.AutoScrollMinSize = msize;
                }
                this.CurrentPage = this.Pages[0];
                int topCount = 0;
                foreach (PrintPage page in this.Pages)
                {
                    page.ClientBounds = new Rectangle(
                        0,
                        (int ) (topCount * rate ), 
                        (int)( page.Bounds.Width * rate ),
                        (int) ( page.Bounds.Height * rate));
                    topCount = topCount + page.Bounds.Height;
                }
                //int ClientWidth = this.ClientSize.Width;
                //int x = 0;
                //if (ClientWidth <= this.AutoScrollMinSize.Width)
                //{
                //    x = 0;// this.PageSpacing / 2;
                //}
                //else
                //{
                //    x = (ClientWidth - this.AutoScrollMinSize.Width) / 2;
                //}

                //this.PagesTransform.OffsetSource(x, 0, false);
            }
            else
            {
                Size TotalSize = new Size(0, 0);

                foreach (PrintPage page in this.Pages)
                {
                    TotalSize.Height = (int)(TotalSize.Height
                        + this.PageSpacing
                        + page.PageSettings.ViewPaperHeight * rate);
                    if (TotalSize.Width < page.PageSettings.ViewPaperWidth * rate)
                    {
                        TotalSize.Width = (int)(page.PageSettings.ViewPaperWidth * rate);
                    }
                    //page.OwnerPages = this.Pages;
                }
                TotalSize.Width += this.PageSpacing * 2;
                TotalSize.Height += this.PageSpacing;
                foreach (PrintPage page in this.Pages)
                {
                    page.ClientLeftFix = (int)(TotalSize.Width
                        - page.PageSettings.ViewPaperWidth * rate) / 2;
                }
                if (this.AutoScrollMinSize.Equals(TotalSize) == false)
                {
                    this.AutoScrollMinSize = TotalSize;
                }

                MultiPageTransform trans = (MultiPageTransform)this.Transform;
                base.intGraphicsUnit = this.Pages.GraphicsUnit;

                trans.Pages = this.Pages;
                trans.Refresh(rate, this.PageSpacing);

                int ClientWidth = this.ClientSize.Width;
                int x = 0;
                if (ClientWidth <= TotalSize.Width)
                {
                    x = 0;// this.PageSpacing / 2;
                }
                else
                {
                    x = (ClientWidth - TotalSize.Width) / 2;
                }

                trans.OffsetSource(x, 0, false);

                this.RefreshScaleTransform();

                //System.Drawing.Rectangle rect = System.Drawing.Rectangle.Empty ;

                int topCount = this.PageSpacing;
                foreach (PrintPage page in this.Pages)
                {
                    System.Drawing.Rectangle clientRect = System.Drawing.Rectangle.Empty;
                    clientRect.X = x + page.ClientLeftFix;
                    clientRect.Y = topCount;
                    clientRect.Width = (int)(page.PageSettings.ViewPaperWidth * rate);
                    clientRect.Height = (int)(page.PageSettings.ViewPaperHeight * rate);
                    //page.ClientLocation = new Point(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);
                    page.ClientBounds = clientRect;
                    page.ClientMargins = new System.Drawing.Printing.Margins(
                        (int)(page.ViewLeftMargin * rate),
                        (int)(page.ViewRightMargin * rate),
                        (int)(page.ViewTopMargin * rate),
                        (int)(page.ViewBottomMargin * rate));
                    topCount = topCount + clientRect.Height + this.PageSpacing;
                }//foreach

                this.UpdateCurrentPage();
            }
		}


		/// <summary>
		/// 页背景色
		/// </summary>
		protected System.Drawing.Color intPageBackColor = System.Drawing.SystemColors.Window ;
		/// <summary>
		/// 页背景色
		/// </summary>
        [System.ComponentModel.DefaultValue( typeof( System.Drawing.Color ) , "Window" )]
        [System.ComponentModel.Category("Appearance")]
		public System.Drawing.Color PageBackColor
		{
			get
            {
                return intPageBackColor ;
            }
			set
            {
                if (intPageBackColor != value)
                {
                    intPageBackColor = value;
                    this.Invalidate();
                }
            }
		}

		/// <summary>
		/// 处于页面视图模式时各个页面间的距离，像素为单位
		/// </summary>
		protected int intPageSpacing = 20 ;
		/// <summary>
		/// 处于页面视图模式时各个页面间的距离，像素为单位
		/// </summary>
        [System.ComponentModel.DefaultValue( 20 )]
        [System.ComponentModel.Category("Appearance")]
		public int PageSpacing
		{
			get
            {
                return intPageSpacing ;
            }
			set
            {
                intPageSpacing = value;
            }
		}

		/// <summary>
		/// 总页数
		/// </summary>
        [System.ComponentModel.Browsable(false)]
        public int PageCount
        {
            get
            {
                if (_Pages != null)
                {
                    return _Pages.Count;
                }
                else
                {
                    return 0;
                }
            }
        }

		#region 无效矩形区域控制代码群 ****************************************
		
//		/// <summary>
//		/// 无效矩形区域
//		/// </summary>
//		protected RectangleCounter myInvalidateRect = new RectangleCounter();
//		/// <summary>
//		/// 无效矩形区域
//		/// </summary>
//		[System.ComponentModel.Browsable(false)]
//		public RectangleCounter InvalidateRect
//		{
//			get{ return myInvalidateRect ;}
//		}
//				 
//		/// <summary>
//		/// 追加无效区域
//		/// </summary>
//		/// <param name="myRect"></param>
//		public void AddInvalidateRect( System.Drawing.Rectangle myRect)
//		{
//			myInvalidateRect.Add( myRect );
//		}
//		/// <summary>
//		/// 追加无效区域
//		/// </summary>
//		/// <param name="x"></param>
//		/// <param name="y"></param>
//		/// <param name="width"></param>
//		/// <param name="height"></param>
//		public void AddInvalidateRect( int x , int y , int width , int height)
//		{
//			myInvalidateRect.Add( x , y , width , height );
//		}
//
//		/// <summary>
//		/// 更新无效区域
//		/// </summary>
//		public void UpdateInvalidateRect()
//		{
//			if( myInvalidateRect.IsEmpty == false)
//			{
//				this.ViewInvalidate( this.myInvalidateRect.Value );
//				this.myInvalidateRect.Clear();
//			}
//		}
		/// <summary>
		/// 使用视图坐标来声明无效区域,程序准备重新绘制无效文档
		/// </summary>
		/// <param name="ViewBounds">无效区域</param>
		public override void ViewInvalidate(System.Drawing.Rectangle ViewBounds)
		{
			ViewBounds = this.FixViewInvalidateRect( ViewBounds );
			if( ! ViewBounds.IsEmpty )
			{
				MultiPageTransform trans = this.myTransform as MultiPageTransform ;
				if( trans == null )
				{
					base.ViewInvalidate ( ViewBounds );
				}
				else
				{
					foreach( SimpleRectangleTransform item in trans )
					{
						System.Drawing.Rectangle rect = System.Drawing.Rectangle.Intersect( item.DescRect , ViewBounds );
						if( ! rect.IsEmpty )
						{
							rect = item.UnTransformRectangle( rect );
							this.Invalidate( rect );
						}
					}
				}
			}
		}

		#endregion

		/// <summary>
		/// 是否采用绝对的坐标转换
		/// </summary>
		[System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(
            System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public bool UseAbsTransformPoint
		{
			get
            {
                return this.PagesTransform.UseAbsTransformPoint;
            }
			set
            {
                this.PagesTransform.UseAbsTransformPoint = value;
            }
		}

		/// <summary>
		/// 设置或返回从1开始的当前页号
		/// </summary>
		[System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(
            System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public int PageIndex
		{
			get
			{
                if (myCurrentPage == null)
                {
                    return 0;
                }
                else
                {
                    return myCurrentPage.GlobalIndex;
                }
			}
			set
			{
				MoveToPage( value );
			}
		}

		/// <summary>
		/// 跳到第一页
		/// </summary>
		public void FirstPage()
		{
			MoveToPage( 0 );
		}
		/// <summary>
		/// 跳到下一页
		/// </summary>
		public void NextPage()
		{
            if (myCurrentPage == null)
            {
                MoveToPage(0);
            }
            else
            {
                MoveToPage(_Pages.IndexOf(myCurrentPage) + 1);
            }
		}
		/// <summary>
		/// 跳到上一页
		/// </summary>
		public void PrePage()
		{
            if (myCurrentPage == null)
            {
                MoveToPage(0);
            }
            else
            {
                MoveToPage(_Pages.IndexOf(myCurrentPage) - 1);
            }
		}
		/// <summary>
		/// 跳到最后一页
		/// </summary>
        public void LastPage()
        {
            if (_Pages != null)
            {
                MoveToPage(_Pages.Count - 1);
            }
        }
		/// <summary>
		/// 跳到指定页,页号从0开始计算。
		/// </summary>
		/// <param name="PageIndex">从0开始的页号</param>
        /// <returns>操作是否成功</returns>
		public bool MoveToPage( int PageIndex )
		{
			if( _Pages != null && PageIndex >= 0 && PageIndex < _Pages.Count )
			{
				PrintPage page = _Pages[ PageIndex ];
				this.SetAutoScrollPosition( new System.Drawing.Point( 0 , page.ClientBounds.Top ));
				myCurrentPage = page ;
				this.Invalidate();
                return true;
			}
            return false;
		}

		/// <summary>
		/// 页面滚动事件处理
		/// </summary>
		protected override void OnXScroll()
		{
			if( this.Pages != null && this.ViewMode == PageViewMode.Page )
			{
				PrintPage page = this.myCurrentPage ;
				if( this.UpdateCurrentPage())
				{
					using( System.Drawing.Graphics g = this.CreateGraphics())
					{
                        DrawPageFrame(page, g, System.Drawing.Rectangle.Empty , false );
						DrawPageFrame( this.CurrentPage , g , System.Drawing.Rectangle.Empty , false );
					}
//					if( page == null)
//						rect = cpage.ClientBounds ;
//					else
//						rect = System.Drawing.Rectangle.Union( cpage.ClientBounds ,  myCurrentPage.ClientBounds );
					this.OnCurrentPageChanged();
                    DrawHeadShadow();
				}
            }
            base.OnXScroll();
        }

        private void DrawHeadShadow()
        {
            //using (System.Drawing.Graphics g = this.CreateGraphics())
            //{
            //    using (System.Drawing.Drawing2D.LinearGradientBrush b = new System.Drawing.Drawing2D.LinearGradientBrush(
            //        new System.Drawing.Rectangle(0, 0, this.ClientSize.Width, 10),
            //        System.Drawing.Color.FromArgb(200, Color.Black),
            //        Color.FromArgb(30, Color.Black),
            //         System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            //    {
            //        g.FillRectangle(b, new Rectangle(0, 0, this.ClientSize.Width, 10));
            //    }
            //}
        }

		/// <summary>
		/// 绘制页面框架
		/// </summary>
		/// <param name="myPage">页面对象</param>
		/// <param name="g">图形绘制对象</param>
		/// <param name="Focused">该页是否是当前页</param>
		/// <param name="FillBackGround">是否填充背景</param>
		protected void DrawPageFrame( 
			PrintPage myPage , 
			System.Drawing.Graphics g ,
            System.Drawing.Rectangle ClipRectangle ,
			bool FillBackGround  )
		{
            if (myPage == null || _Pages.Contains(myPage) == false)
            {
                return;
            }
			System.Drawing.Rectangle bounds = myPage.ClientBounds ;
			bounds.Offset( this.AutoScrollPosition );
            // 绘制页面阴影
            //int ShadowSize = 5;
            //g.FillRectangle(System.Drawing.Brushes.Black, bounds.Right - 1 , bounds.Top + ShadowSize, ShadowSize, bounds.Height-1);
            //g.FillRectangle(System.Drawing.Brushes.Black , bounds.Left + ShadowSize, bounds.Bottom - 1 , bounds.Width-1, ShadowSize);

            //System.Drawing.Rectangle ShadowRect = bounds;
            //ShadowRect.Offset(10 , 10 );
            //using (System.Drawing.Pen p = new System.Drawing.Pen( System.Drawing.Color.Gray , 10 ))
            //{
            //    g.DrawRectangle(p, ShadowRect);
            //}
            PageFrameDrawer drawer = new PageFrameDrawer();
            drawer.BorderWidth = 1;
            drawer.Bounds = bounds;
            drawer.Margins = myPage.ClientMargins;
            if ( myPage == this.CurrentPage )
            {
                if ( this.Enabled )// info.Enabled )
                {
                    drawer.BorderColor = System.Drawing.ColorTranslator.FromHtml("#EEAA57");// System.Drawing.Color.Red;
                    drawer.BorderWidth = 3;
                }
                else
                {
                    drawer.BorderColor = System.Drawing.Color.LightGray ;
                    drawer.BorderWidth = 3;
                }
            }
            else
            {
                drawer.BorderWidth = 3;
                drawer.BorderColor = System.Drawing.Color.Black  ;
            }
            drawer.BackColor = FillBackGround ? this.PageBackColor : System.Drawing.Color.Transparent ;

            //this.FixedBackground = false;
 

            drawer.DrawPageFrame(g, ClipRectangle);

            
            
            //XDesignerDrawer.PageFrameDrawer.DrawPageFrame( 
            //    bounds ,
            //    this.myClientMargins ,
            //    g ,
            //    System.Drawing.Rectangle.Empty ,
            //    Focused ,
            //    FillBackGround ? this.PageBackColor : System.Drawing.Color.Transparent );
		}

		protected System.Drawing.Rectangle GetViewClipRectangle( System.Drawing.Rectangle rect )
		{
			double xrate = this.ClientToViewXRate ;
			double yrate = this.ClientToViewYRate ;
			rect.X = ( int ) ( rect.X * xrate );
			rect.Y =  ( int ) ( rect.Y * yrate );
			rect.Width = ( int ) ( rect.Width * xrate );
			rect.Height = ( int ) ( rect.Height * yrate );
			return rect ;
		}

		/// <summary>
		/// 根据指定坐标的位置获得续打位置
		/// </summary>
		/// <param name="x">X坐标值</param>
		/// <param name="y">Y坐标值</param>
		/// <returns>续打位置</returns>
		protected int GetJumpPrintPosition( int x , int y )
		{
			MultiPageTransform trans = ( MultiPageTransform ) this.myTransform ;
			if( trans.ContainsSourcePoint( x ,y ) )
			{
				int pos = this.myTransform.TransformPoint( x , y ).Y ;
				if( pos >= 0 )
					return pos ;
			}
			return 0 ;
		}

        public SimpleRectangleTransform GetTransformItemByViewPosition(float x, float y)
        {
            MultiPageTransform trans = (MultiPageTransform)this.myTransform;
            foreach (SimpleRectangleTransform item in trans)
            {
                if (item.DescRectF.Contains(x, y))
                {
                    return item;
                }
            }
            return null;
        }

        public SimpleRectangleTransform GetTransformItemByViewPosition( float y)
        {
            MultiPageTransform trans = (MultiPageTransform)this.myTransform;
            foreach (SimpleRectangleTransform item in trans)
            {
                if (item.DescRectF.Top >= y && item.DescRectF.Bottom >= y )
                {
                    return item;
                }
            }
            return null;
        }
         
        private HeaderFooterFlagVisible _HeaderFooterFlagVisible = HeaderFooterFlagVisible.None ;
        /// <summary>
        /// 是否显示页眉页脚标记
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.ComponentModel.DesignerSerializationVisibility(
            System.ComponentModel.DesignerSerializationVisibility.Hidden )]
        public virtual HeaderFooterFlagVisible HeaderFooterFlagVisible
        {
            get
            {
                return _HeaderFooterFlagVisible; 
            }
            set
            {
                _HeaderFooterFlagVisible = value; 
            }
        }

        /// <summary>
        /// 绘制分页线
        /// </summary>
        /// <param name="args"></param>
        protected void DrawPageLines(PaintEventArgs args)
        {
            if (this.Pages != null
                && this.Pages.Count > 0
                && this.ViewMode == PageViewMode.Normal )
            {
                using (Pen p = new Pen(Color.Black))
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    foreach (PrintPage page in this.Pages)
                    {
                        int pos = page.ClientBounds.Bottom + this.AutoScrollPosition.Y;
                        if (pos >= args.ClipRectangle.Top && pos <= args.ClipRectangle.Bottom)
                        {
                            args.Graphics.DrawLine( p ,  0, pos, this.ClientSize.Width, pos);
                        }
                    }
                }
            }
        }

		/// <summary>
		/// 已重载:绘制文档内容
		/// </summary>
		/// <param name="e">绘制事件参数</param>
		protected override void OnPaint( System.Windows.Forms.PaintEventArgs e)
		{
            //if (this.DesignMode)
            {
                base.OnPaint(e);
            }
            if (this.IsUpdating)
            {
                return;
            }
             
            System.Drawing.Rectangle clipRect = e.ClipRectangle;
            clipRect.Height += 1;
			System.Drawing.Point sp = this.AutoScrollPosition ;
			//int ax = - this.AutoScrollPosition.X ;
			//int ay = - this.AutoScrollPosition.Y ;
			this.RefreshScaleTransform();
            if (this.PagesTransform == null || this.PagesTransform.Count == 0)
            {
                // 没有任何内容可以绘制
                return;
            }
            if (this.ViewMode == PageViewMode.Normal)
            {
                using (SolidBrush b = new SolidBrush(this.PageBackColor))
                {
                    e.Graphics.FillRectangle(b, e.ClipRectangle);
                }
                SimpleRectangleTransform item = this.PagesTransform[0];
                PrintPage myPage = (PrintPage)item.PageObject;

                Rectangle rect = item.SourceRect;
                rect.Width = rect.Width + 30;
                rect = Rectangle.Intersect(
                    clipRect,
                    rect);

                if (rect.IsEmpty == false)
                {
                    // 保存状态
                    System.Drawing.Drawing2D.GraphicsState state2 = e.Graphics.Save();

                    //try
                    {
                        PaintEventArgs e2 = this.CreatePaintEventArgs(e, item);
                        if (e2 != null)
                        {
                            PageDocumentPaintEventArgs e3 = new PageDocumentPaintEventArgs(
                                e2.Graphics,
                                e2.ClipRectangle,
                                myPage.Document,
                                myPage,
                                item.ContentStyle);
                            e3.ContentBounds = item.DescRect;
                            e3.RenderMode = ContentRenderMode.Paint;
                            e3.PageIndex = myPage.PageIndex;
                            e3.NumberOfPages = this.Pages.Count;
                            //e3.EditMode = this.EditMode;
                            if (myPage.Document != null)
                            {
                                myPage.Document.DrawContent(e3);
                            }//if
                        }
                    }
                    //catch (Exception ext)
                    //{
                    //    System.Console.WriteLine(ext.ToString());
                    //}
                    // 恢复状态
                    e.Graphics.Restore(state2);
                }
                return;
            }
//
			MultiPageTransform trans = ( MultiPageTransform ) this.Transform ;
//			trans.ClearSourceOffset();
//			trans.OffsetSource( sp.X , sp.Y , true );

			System.Drawing.Graphics g = e.Graphics ;
            //System.Drawing.Drawing2D.GraphicsState stateBack = e.Graphics.Save();
			foreach( PrintPage myPage in this.Pages )
			{
				System.Drawing.Rectangle ClientBounds = myPage.ClientBounds  ;
				ClientBounds.Offset( sp );
                ClientBounds.Width = ClientBounds.Width + 20;
                //if( clipRect.Top <= ClientBounds.Bottom  + 5
                //    && clipRect.Bottom >= ClientBounds.Top )
                if (clipRect.IntersectsWith(
                    new Rectangle(
                        ClientBounds.Left,
                        ClientBounds.Top,
                        ClientBounds.Width + 5,
                        ClientBounds.Height + 5)))
				{
					//this.SetPageIndex( myPage.Index );

                    //e.Graphics.Restore(stateBack);
                    //e.Graphics.ResetClip();
                    DrawPageFrame(
                        myPage,
                        e.Graphics,
                        clipRect,
                        true);

                    for( int iCount = trans.Count -1 ; iCount >= 0 ; iCount -- )
                    {
                        SimpleRectangleTransform item = trans[ iCount ];
        				if( item.Visible && item.PageObject == myPage )
						{
                            // 显示页眉页脚标记文本
                            if (item.ContentStyle == PageContentPartyStyle.Header)
                            {
                                if (this.HeaderFooterFlagVisible == HeaderFooterFlagVisible.Header
                                    || this.HeaderFooterFlagVisible == HeaderFooterFlagVisible.HeaderFooter)
                                {
                                    // 绘制页眉标记
                                    //e.Graphics.Restore(stateBack);
                                    DrawHeaderFooterFlag(
                                        PrintingResources.Header,
                                        item.PartialAreaSourceBounds  ,
                                        e.Graphics );
                                }
                            }
                            else if (item.ContentStyle == PageContentPartyStyle.Footer)
                            {
                                if (this.HeaderFooterFlagVisible == HeaderFooterFlagVisible.Footer
                                    || this.HeaderFooterFlagVisible == HeaderFooterFlagVisible.HeaderFooter)
                                {
                                    // 绘制页脚标记
                                    //e.Graphics.Restore(stateBack);
                                    DrawHeaderFooterFlag(
                                        PrintingResources.Footer,
                                        item.PartialAreaSourceBounds ,
                                        e.Graphics);
                                }
                            }
                            Rectangle rect = item.SourceRect;
        
							rect = Rectangle.Intersect( 
								clipRect ,
								rect );

							if( rect.IsEmpty == false )
							{

                                System.Drawing.Drawing2D.GraphicsState state2 = e.Graphics.Save();

                                PaintEventArgs e2 = this.CreatePaintEventArgs(e, item);
                                if (e2 != null)
                                {
                          
                                    PageDocumentPaintEventArgs e3 = new PageDocumentPaintEventArgs(
                                        e2.Graphics,
                                        e2.ClipRectangle,
                                        myPage.Document,
                                        myPage,
                                        item.ContentStyle);
                                    e3.ContentBounds = item.DescRect;
                                    e3.RenderMode = ContentRenderMode.Paint;
                                    e3.PageIndex = myPage.PageIndex;
                                    e3.NumberOfPages = this.Pages.Count;
                                    //e3.EditMode = this.EditMode;
                                    if (myPage.Document != null)
                                    {
                                        myPage.Document.DrawContent(e3);
                                    }//if
                                }

                                e.Graphics.Restore(state2);

                                if (item.Enable == false)
                                {
                                    // 若区域无效则用白色半透明进行覆盖，以作标记
                                    using(System.Drawing.SolidBrush maskBrush 
                                        = new SolidBrush( Color.FromArgb( 140 , this.PageBackColor )))
                                    {
                                        e.Graphics.FillRectangle( 
                                            maskBrush ,
                                            rect.Left ,
                                            rect.Top ,
                                            rect.Width + 2 ,
                                            rect.Height + 2  );
                                    }
                                }
							}//if
                           // ClipRect.Height -= 1;
						}
					}//foreach
				}//if
			}//foreach
            DrawHeadShadow();
			//base.OnPaint( e );
            //e.Graphics.Flush(System.Drawing.Drawing2D.FlushIntention.Sync);
            //System.Threading.Thread.Sleep(100);
		}


        /// <summary>
        /// 绘制页眉页脚使用的字体对象
        /// </summary>
        private static Font _HeaderFooterFlagFont = null;
        //private static Size _HeaderFooterFlagSize = Size.Empty;
        /// <summary>
        /// 绘制页眉页脚标记
        /// </summary>
        /// <param name="flagText">标记文本</param>
        /// <param name="flagRect">边界区域</param>
        /// <param name="g">图形绘制对象</param>
        private void DrawHeaderFooterFlag(string flagText, Rectangle flagRect, Graphics g)
        {
            if (_HeaderFooterFlagFont == null)
            {
                _HeaderFooterFlagFont = new Font(
                    System.Windows.Forms.Control.DefaultFont.Name,
                    10);
            }
            SizeF size = g.MeasureString(
                flagText,
                _HeaderFooterFlagFont);
            Size _HeaderFooterFlagSize = new Size(
                (int)size.Width + 4,
                (int)size.Height + 4);
            // 绘制页眉页脚边界
            using (System.Drawing.Pen pen
                = new Pen(Color.FromArgb(155, 187, 227)))
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                //flagRect.Width = flagRect.Width;
                //flagRect.Height = flagRect.Height;
                g.DrawRectangle(pen, flagRect);
            }

            // 绘制页眉页脚文本
            flagRect.Offset(-_HeaderFooterFlagSize.Width, 0);
            flagRect.Width = _HeaderFooterFlagSize.Width;
                                       
            DrawLabel(
                g,
                _HeaderFooterFlagFont,
                flagText,
                Color.FromArgb(21, 66, 139),
                Color.FromArgb(216, 232, 245),
                Color.FromArgb(155, 187, 227),
                flagRect);
        }

        private void DrawLabel(
            System.Drawing.Graphics graphics,
            Font font,
            string text,
            Color textColor,
            Color backColor,
            Color borderColor,
            Rectangle bounds)
        {
            using (SolidBrush b = new SolidBrush(backColor))
            {
                graphics.FillRectangle(b, bounds);
            }
            if (text != null && text.Length > 0)
            {
                using (StringFormat format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    format.FormatFlags = StringFormatFlags.NoWrap;
                    using (SolidBrush b = new SolidBrush(textColor))
                    {
                        graphics.DrawString(
                            text,
                            font,
                            b,
                            new RectangleF(
                                bounds.Left,
                                bounds.Top,
                                bounds.Width,
                                bounds.Height),
                            format);
                    }//using
                }//using
            }
            using (Pen pen = new Pen(borderColor))
            {
                graphics.DrawRectangle(pen, bounds);
            }
        }

		/// <summary>
		/// 控件大小发生改变时的处理
		/// </summary>
		/// <param name="e">事件参数</param>
		protected override void OnResize(EventArgs e)
		{
            if (this.DesignMode == false && this.IsHandleCreated )
            {
                this.UpdatePages();
            }
			base.OnResize (e);
		}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
		/// <summary>
		/// 设置当前页号
		/// </summary>
		/// <param name="index"></param>
		protected virtual void SetPageIndex( int index )
		{
		}
	}//public class PageScrollableControl : DocumentViewControl

    /// <summary>
    /// 页眉页脚标记显示模式
    /// </summary>
    public enum HeaderFooterFlagVisible
    {
        /// <summary>
        /// 不显示
        /// </summary>
        None ,
        /// <summary>
        /// 只显示页眉
        /// </summary>
        Header ,
        /// <summary>
        /// 只显示页脚
        /// </summary>
        Footer ,
        /// <summary>
        /// 同时显示页眉和页脚
        /// </summary>
        HeaderFooter
    }

    /// <summary>
    /// 坐标转换模式
    /// </summary>
    public enum ViewTransformMode
    {
        /// <summary>
        /// 正常模式
        /// </summary>
        Normal ,
        /// <summary>
        /// 绝对模式
        /// </summary>
        Absolute,
        /// <summary>
        /// 限制为当前区域模式
        /// </summary>
        LimitedCurrentItem 
    }
}