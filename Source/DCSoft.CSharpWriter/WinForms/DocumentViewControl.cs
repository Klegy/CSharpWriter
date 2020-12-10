/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
#define CaptureMouseMove
#define ReversibleDraw 

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCSoft.Drawing ;

//using DCSoft.Dom ;

#if CaptureMouseMove || ReversibleDraw

using DCSoft.WinForms.Native;

#endif

namespace DCSoft.WinForms
{
	/// <summary>
	/// 文档视图控件
	/// </summary>
    [System.ComponentModel.ToolboxItem(false)]
	public class DocumentViewControl : BorderUserControl
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public DocumentViewControl()
		{
			//this.myScaleViewer.BindViewControl = this ;
		}

#if ! DOTNET11

        private bool bolFixedBackground = false;
        /// <summary>
        /// 固定背景
        /// </summary>
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue(false)]
        //[System.ComponentModel.Browsable( false )]
        //[System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool FixedBackground
        {
            get
            {
                return bolFixedBackground; 
            }
            set
            {
                bolFixedBackground = value; 
            }
        }

        private System.Drawing.Image myLogonImage = null;
        /// <summary>
        /// 标志图片
        /// </summary>
        [System.ComponentModel.Category("Appearance")]
        [DefaultValue( null )]
        //[System.ComponentModel.Browsable(false)]
        //[System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Drawing.Image LogonImage
        {
            get
            {
                return myLogonImage; 
            }
            set
            {
                myLogonImage = value; 
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (this.DesignMode)
            {
                base.OnPaintBackground(e);
            }

            //return;
            if (this.IsUpdating)
                return;
            base.OnPaintBackground(e);
            if (myLogonImage != null)
            {
                int x = this.ClientSize.Width - myLogonImage.Width;
                int y = this.ClientSize.Height - myLogonImage.Height;
                if (e.ClipRectangle.IntersectsWith(
                    new Rectangle(
                        x,
                        y,
                        myLogonImage.Width,
                        myLogonImage.Height)))
                {
                    e.Graphics.DrawImage(
                        myLogonImage,
                        x, y , myLogonImage.Width , myLogonImage.Height );
                    //e.Graphics.DrawImageUnscaled(
                    //    myLogonImage,
                    //    x,
                    //    y);
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (bolFixedBackground)
            {
                LockWindowUpdate(this.Handle);
                base.OnMouseWheel(e);
                LockWindowUpdate(IntPtr.Zero);
                this.Invalidate();
            }
            else
            {
                base.OnMouseWheel(e);
            }
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            //System.Console.WriteLine(se.Type + " " + this.AutoScrollPosition.Y );
            if (bolFixedBackground)
            {
                if (se.Type == ScrollEventType.First)
                {
                    LockWindowUpdate(this.Handle);
                }
                else if (se.Type == ScrollEventType.ThumbTrack)
                {
                    LockWindowUpdate(IntPtr.Zero);
                    this.Refresh();
                    LockWindowUpdate(this.Handle);
                }
                else if (se.Type == ScrollEventType.ThumbPosition)
                {
                    LockWindowUpdate(IntPtr.Zero);
                    //LockWindowUpdate(this.Handle);
                    //this.Invalidate();
                    //LockWindowUpdate(IntPtr.Zero);
                    //this.Refresh();
                }
                else
                {
                    LockWindowUpdate(IntPtr.Zero);
                    this.Invalidate();
                }
            }
            base.OnScroll(se);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool LockWindowUpdate(IntPtr hWnd);

#endif

		/// <summary>
		/// 鼠标拖拽滚动时使用手形鼠标光标
		/// </summary>
		protected bool bolDragUseHandCursor = true ;
		/// <summary>
		/// 鼠标拖拽滚动时使用手形鼠标光标
		/// </summary>
		[System.ComponentModel.DefaultValue( true )]
        [System.ComponentModel.Browsable( false )]
        [System.ComponentModel.DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public virtual bool DragUseHandCursor
		{
			get
			{
				return bolDragUseHandCursor ;
			}
			set
			{
				bolDragUseHandCursor = value;
			}
		}

		protected System.Windows.Forms.Cursor myDefaultCursor = System.Windows.Forms.Cursors.Default ;
		/// <summary>
		/// 控件默认鼠标光标
		/// </summary>
		[System.ComponentModel.Browsable( false )]
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public System.Windows.Forms.Cursor XDefaultCursor
		{
			get
			{
				if( this.bolMouseDragScroll )
				{
					if( this.bolDragUseHandCursor )
					{
						return XCursors.HandDragUp ;
					}
					else
					{
						return myDefaultCursor ;
					}
				}
				else
					return myDefaultCursor ;
			}
			set
			{
				myDefaultCursor = value;
			}
		}

		public virtual void Zoom( float rate )
		{
			fXZoomRate *= rate ;
			fYZoomRate *= rate ;
			if( fXZoomRate <= 0 )
				fXZoomRate = 1f ;
			if( fYZoomRate <= 0 )
				fYZoomRate = 1f ;
			this.UpdateViewBounds();
			this.Invalidate();
		}

        public virtual void SetZoomRate(float rate)
        {
            if (rate <= 0)
                rate = 1f;
            fXZoomRate = rate;
            fYZoomRate = rate;
            this.UpdateViewBounds();
            this.Invalidate();
        }

		protected float fXZoomRate = 1.0f;
		/// <summary>
		/// X方向缩放率
		/// </summary>
		[System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public float XZoomRate
		{
			get
            {
                return fXZoomRate ;
            }
			set
			{
				if( fXZoomRate != value )
				{
					fXZoomRate = value;
					this.UpdateViewBounds();
					this.Invalidate();
				}
			}
		}

		protected float fYZoomRate = 1.0f ;
		/// <summary>
		/// Y方向缩放率
		/// </summary>
		[System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public float YZoomRate
		{
			get
            {
                return fYZoomRate ;
            }
			set
			{
				if( fYZoomRate != value )
				{
					fYZoomRate = value;
					this.UpdateViewBounds();
					this.Invalidate();
				}
			}
		}

        


		protected void CheckZoomRate()
		{
			if( this.fXZoomRate <= 0 || this.fYZoomRate <= 0 )
				throw new System.InvalidOperationException("Bad zoom rate value");
		}
		/// <summary>
		/// 绘图单位
		/// </summary>
		protected System.Drawing.GraphicsUnit intGraphicsUnit = System.Drawing.GraphicsUnit.Pixel ;
		/// <summary>
		/// 绘图单位
		/// </summary>
		[System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public System.Drawing.GraphicsUnit GraphicsUnit
		{
			get
            {
                return intGraphicsUnit ; 
            }
			set
			{
				intGraphicsUnit = value; 
				this.UpdateViewBounds();
				this.Invalidate();
			}
		}

		[System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual System.Drawing.Size ViewAutoScrollMinSize
		{
			get
			{
				System.Drawing.Size size = this.AutoScrollMinSize ;
				size = GraphicsUnitConvert.Convert(
					size ,
					System.Drawing.GraphicsUnit.Pixel ,
					this.intGraphicsUnit );
				size.Width = ( int ) ( size.Width * this.fXZoomRate );
				size.Height = ( int ) ( size.Height * this.fYZoomRate );
				return size ;
			}
			set
			{
				System.Drawing.Size size = GraphicsUnitConvert.Convert(
					value , 
					this.intGraphicsUnit , 
					System.Drawing.GraphicsUnit.Pixel );
				size.Width = ( int ) ( size.Width / this.fXZoomRate );
				size.Height = ( int ) ( size.Height / this.fYZoomRate );
				this.AutoScrollMinSize = size ;
			}
		}

        /// <summary>
        /// 横向的客户区图形度量单位和文档视图度量单位的比率
        /// </summary>
		[System.ComponentModel.Browsable(false)]
		public double ClientToViewXRate
		{
			get
			{
				double rate = GraphicsUnitConvert.GetRate( 
					this.intGraphicsUnit , 
					System.Drawing.GraphicsUnit.Pixel );
				rate /= this.fXZoomRate ;
				return rate ;
			}
		}
		[System.ComponentModel.Browsable(false)]
		public double ClientToViewYRate
		{
			get
			{
				double rate = GraphicsUnitConvert.GetRate( 
					this.intGraphicsUnit , 
					System.Drawing.GraphicsUnit.Pixel );
				rate /= this.fYZoomRate ;
				return rate ;
			}
		}
		[System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
		public virtual System.Drawing.Point ViewAutoScrollPosition
		{
			get
			{
				System.Drawing.Point p = this.AutoScrollPosition ;
				p = GraphicsUnitConvert.Convert(
					p ,
					System.Drawing.GraphicsUnit.Pixel , 
					this.intGraphicsUnit );
				p.X = ( int ) ( p.X * this.fXZoomRate );
				p.Y = ( int ) ( p.Y * this.fYZoomRate );
				return p ;
			}
			set
			{
				System.Drawing.Point p = GraphicsUnitConvert.Convert( 
					value , 
					this.intGraphicsUnit ,
					System.Drawing.GraphicsUnit.Pixel );
				p.X = ( int ) ( p.X / this.fXZoomRate );
				p.Y = ( int ) ( p.Y / this.fYZoomRate );
				this.SetAutoScrollPosition( p );
			}
		}

		[System.ComponentModel.Browsable(false)]
		public virtual System.Drawing.Size ViewClientSize
		{
			get
			{
				System.Drawing.Size size = GraphicsUnitConvert.Convert( 
					this.ClientSize , 
					System.Drawing.GraphicsUnit.Pixel , 
					this.intGraphicsUnit );
				size.Width = ( int ) ( size.Width / this.fXZoomRate );
				size.Height = ( int ) ( size.Height / this.fYZoomRate );
				return size ;
			}
		}
		[System.ComponentModel.Browsable(false)]
		public virtual System.Drawing.Rectangle ViewClientRectangle
		{
			get
			{
				System.Drawing.Rectangle rect = GraphicsUnitConvert.Convert( this.ClientRectangle ,
					System.Drawing.GraphicsUnit.Pixel ,
					this.intGraphicsUnit );
				rect.X = ( int ) ( rect.X / this.fXZoomRate );
				rect.Y = ( int ) ( rect.Y / this.fYZoomRate );
				rect.Width = ( int ) ( rect.Width / this.fXZoomRate );
				rect.Height = ( int ) ( rect.Height / this.fYZoomRate );
				return rect;
			}
		}

		/// <summary>
		/// 从控件客户区到视图区的转换对象
		/// </summary>
		protected TransformBase myTransform = new SimpleRectangleTransform();

        private System.Drawing.PointF myViewOffset = System.Drawing.PointF.Empty;
        /// <summary>
        /// 视图区域偏移量
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(
                DesignerSerializationVisibility.Hidden)]
        public System.Drawing.PointF ViewOffset
        {
            get
            {
                return myViewOffset; 
            }
            set
            {
                myViewOffset = value; 
            }
        }

		/// <summary>
		/// 内部的从控件客户区到视图区的转换对象
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		public TransformBase Transform
		{
			get
            {
                return this.myTransform ;
            }
		}

		/// <summary>
		/// 刷新坐标转换对象
		/// </summary>
        protected virtual void RefreshScaleTransform()
        {
            SimpleRectangleTransform transform = this.myTransform as SimpleRectangleTransform;
            if (transform == null)
                return;

            System.Drawing.Rectangle rect = this.ClientRectangle;
            transform.SourceRect = rect;
            System.Drawing.Point p = this.AutoScrollPosition;
            //rect.Offset( this.AutoScrollPosition.X ,   this.AutoScrollPosition.Y );
            //transform.SourceRect = rect ;

            float xrate = (float)this.ClientToViewXRate;
            float yrate = (float)this.ClientToViewYRate;

            System.Drawing.RectangleF rect2 = new System.Drawing.RectangleF(
               -p.X * xrate,
               -p.Y * yrate,
               rect.Width * xrate,
               rect.Height * yrate);

            rect2.Offset(this.ViewOffset);
            transform.DescRectF = rect2;
        }

		/// <summary>
		/// 将客户区坐标转换为视图区坐标
		/// </summary>
		/// <param name="x">客户区点X坐标</param>
		/// <param name="y">客户区点Y坐标</param>
		/// <returns>转换后的视图点坐标</returns>
		public virtual System.Drawing.Point ClientPointToView( int x , int y )
		{
			this.RefreshScaleTransform();
			return this.Transform.TransformPoint( x , y );
		}
		/// <summary>
		/// 将客户区坐标转换为视图区坐标
		/// </summary>
		/// <param name="p">客户区点坐标</param>
		/// <returns>视图区点坐标</returns>
		public virtual System.Drawing.Point ClientPointToView( System.Drawing.Point p )
		{
			this.RefreshScaleTransform();
			return this.Transform.TransformPoint( p );
		}
		//		
		//		public virtual System.Drawing.Rectangle ClipRectangleToView( System.Drawing.Rectangle rect )
		//		{
		//			this.RefreshScaleTransform();
		//			return myTransform.TransformRectangle( rect );
		//		}

		/// <summary>
		/// 将视图区坐标转换为客户区坐标
		/// </summary>
		/// <param name="x">视图区X坐标</param>
		/// <param name="y">视图区Y坐标</param>
		/// <returns>客户区坐标</returns>
		public virtual System.Drawing.Point ViewPointToClient( int x , int y )
		{
			this.RefreshScaleTransform();
			return myTransform.UnTransformPoint( x , y );
		}

		/// <summary>
		/// 将视图区坐标转换为客户区坐标
		/// </summary>
		/// <param name="p">视图区坐标</param>
		/// <returns>客户区坐标</returns>
		public System.Drawing.Point ViewPointToClient( System.Drawing.Point p )
		{
            return ViewPointToClient(p.X, p.X);
		}

		/// <summary>
		/// 鼠标在控件上的位置
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		public virtual System.Drawing.Point ClientMousePosition
		{
            get
            {
                System.Drawing.Point p = System.Windows.Forms.Control.MousePosition;
                p = this.PointToClient(p);
                if (this.ClientRectangle.Contains(p))
                {
                    return p;
                }
                else
                {
                    return System.Drawing.Point.Empty;
                }
            }
		}

		/// <summary>
		/// 鼠标在视图区中的坐标
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		public virtual System.Drawing.Point ViewMousePosition
		{
			get
			{
				System.Drawing.Point p = this.ClientMousePosition ;
				if( p.IsEmpty == false )
					return this.ClientPointToView( p );
				else
					return System.Drawing.Point.Empty ;
			}
		}

		protected System.Drawing.Rectangle myViewBounds
            = System.Drawing.Rectangle.Empty ;
		/// <summary>
		/// 整个视图区域的矩形区域
		/// </summary>
		[System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
		public System.Drawing.Rectangle ViewBounds
		{
			get
			{
				return myViewBounds ;
			}
			set
			{
				if( myViewBounds.Equals( value ) == false )
				{
					myViewBounds = value;
					this.UpdateViewBounds();
				}
			}
		}

        private System.Drawing.Size myViewBoundsMarginSize = new Size(20, 20);
        /// <summary>
        /// 视图内边距大小
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.ComponentModel.DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public System.Drawing.Size ViewBoundsMarginSize
        {
            get 
            {
                return myViewBoundsMarginSize; 
            }
            set
            {
                myViewBoundsMarginSize = value; 
            }
        }

       	public virtual void UpdateViewBounds()
		{
			System.Drawing.Size size = new Size(
                myViewBounds.Right - ( int ) this.ViewOffset.X ,
                myViewBounds.Bottom - ( int ) this.ViewOffset.Y );
			this.RefreshScaleTransform();
			size = myTransform.UnTransformSize( size );
            this.AutoScrollMinSize = new Size(
                size.Width + myViewBoundsMarginSize.Width ,
                size.Height + myViewBoundsMarginSize.Height );// size;
            this.Invalidate();
		}

		public virtual System.Windows.Forms.MouseEventArgs CreateViewMouseEventArgs()
		{
			System.Drawing.Point p = this.ViewMousePosition ;
			return new System.Windows.Forms.MouseEventArgs(
                System.Windows.Forms.Control.MouseButtons , 
                0 ,
                p.X ,
                p.Y , 
                0 );
		}

		protected bool bolMouseDragScroll = false;
		/// <summary>
		/// 使用鼠标拖拽滚动模式标记
		/// </summary>
		[System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
		public bool MouseDragScroll
		{
			get
            {
                return this.bolMouseDragScroll ;
            }
			set
            {
                this.bolMouseDragScroll = value;
            }
		}
		protected System.Drawing.Point myMouseDragPoint = System.Drawing.Point.Empty ;

		protected override void OnXScroll()
		{
			base.OnXScroll ();
			this.RefreshScaleTransform();
		}

		/// <summary>
		/// 已重载：鼠标按键按下事件处理
		/// </summary>
		/// <param name="e">事件参数</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown( e );
			if( this.bolMouseDragScroll )
			{
				this.myMouseDragPoint = new Point( e.X , e.Y );
			}

			if( this.myTransform.ContainsSourcePoint( e.X , e.Y ))
			{
				System.Drawing.Point p = this.ClientPointToView( e.X , e.Y );
				OnViewMouseDown(
                    new System.Windows.Forms.MouseEventArgs(
                        e.Button ,
                        e.Clicks ,
                        p.X ,
                        p.Y ,
                        e.Delta ));
			}
			if( this.MouseDragScroll )
			{
				if( this.bolDragUseHandCursor )
				{
					this.Cursor = XCursors.HandDragDown ;
				}
				//return ;
			}
		}
		protected void Control_OnMouseDown( MouseEventArgs e )
		{
			base.OnMouseDown( e );
		}

		public event System.Windows.Forms.MouseEventHandler ViewMouseDown = null;
		/// <summary>
		/// 鼠标按键在视图区中按下事件处理
		/// </summary>
		/// <param name="e">事件参数</param>
		protected virtual void OnViewMouseDown( MouseEventArgs e )
		{
			if( ViewMouseDown != null )
				ViewMouseDown( this , e );
		}

        /// <summary>
        /// 在OnMouseMove方法中是否执行了OnViewMouseMove方法的标记
        /// </summary>
        protected bool bolOnViewMouseMoveFlag = false;

		/// <summary>
		/// 已重载:鼠标移动事件处理
		/// </summary>
		/// <param name="e">事件参数</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
            bolOnViewMouseMoveFlag = false;
			base.OnMouseMove( e );
			if( this.bolScrolling )
			{
				System.Console.WriteLine( this.bolScrolling );
				return ;
			}
			if( this.MouseDragScroll )
			{
				if( System.Windows.Forms.Control.MouseButtons == System.Windows.Forms.MouseButtons.Left )
				{
					if( this.bolDragUseHandCursor )
					{
						this.Cursor = XCursors.HandDragDown ;
					}
					if( this.myMouseDragPoint.IsEmpty == false )
					{
						int dx = e.X - this.myMouseDragPoint.X ;
						int dy = e.Y - this.myMouseDragPoint.Y ;
						this.myMouseDragPoint = new Point( e.X , e.Y );
						this.SetAutoScrollPosition( new Point(
							-dx - this.AutoScrollPosition.X ,
							-dy - this.AutoScrollPosition.Y ));
						this.OnXScroll();
						return ;
					}
				}
				else
				{
					if( this.bolDragUseHandCursor )
					{
						this.Cursor = XCursors.HandDragUp ;
					}
					myMouseDragPoint = System.Drawing.Point.Empty ;
				}
			}
			
			if( this.myTransform.ContainsSourcePoint( e.X , e.Y ))
			{
				System.Drawing.Point p = this.ClientPointToView ( e.X , e.Y );
                bolOnViewMouseMoveFlag = true;
				OnViewMouseMove( new System.Windows.Forms.MouseEventArgs( e.Button , e.Clicks , p.X , p.Y , e.Delta ));
			}
			else
			{
				this.Cursor = this.XDefaultCursor ;
			}
//			if( this.bolMouseDragScroll )
//			{
//				this.Cursor = this.DefaultCursor ;// DCSoft.XCursors.HandDragUp ;
//			}
		}

		public event System.Windows.Forms.MouseEventHandler ViewMouseMove = null;
		/// <summary>
		/// 鼠标在视图区中的移动事件处理
		/// </summary>
		/// <param name="e">事件参数</param>
		protected virtual void OnViewMouseMove(MouseEventArgs e)
		{
			if( ViewMouseMove != null )
				ViewMouseMove( this , e );
		}

		/// <summary>
		/// 已重载:鼠标按键松开事件处理
		/// </summary>
		/// <param name="e">事件参数</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp( e );

			this.myMouseDragPoint = System.Drawing.Point.Empty ;
			if( this.myTransform.ContainsSourcePoint( e.X , e.Y ))
			{
				System.Drawing.Point p = this.ClientPointToView( e.X , e.Y );
				OnViewMouseUp ( new System.Windows.Forms.MouseEventArgs( e.Button , e.Clicks , p.X , p.Y , e.Delta ));
			}
		}
		/// <summary>
		/// 视图区域中的鼠标按键松开事件
		/// </summary>
		public event System.Windows.Forms.MouseEventHandler ViewMouseUp = null;
		/// <summary>
		/// 鼠标在视图区中的按键松开事件处理
		/// </summary>
		/// <param name="e">事件参数</param>
		protected virtual void OnViewMouseUp(MouseEventArgs e)
		{
			if( ViewMouseUp != null )
				ViewMouseUp( this , e );
		}

		
		/// <summary>
		/// 已重载:处理鼠标单击事件
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClick(EventArgs e)
		{
			base.OnClick (e);
			System.Drawing.Point p = this.ViewMousePosition ;
			OnViewClick( new System.Windows.Forms.MouseEventArgs( System.Windows.Forms.Control.MouseButtons , 1 , p.X , p.Y , 0 ));
		}
		/// <summary>
		/// 鼠标在视图中的单击事件
		/// </summary>
		public event System.Windows.Forms.MouseEventHandler ViewClick = null;
		/// <summary>
		/// 处理鼠标在视图区域中的单击事件
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnViewClick( System.Windows.Forms.MouseEventArgs e )
		{
			if( ViewClick != null )
				ViewClick( this , e );
		}

        /// <summary>
        /// 已重载:处理鼠标双击事件
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            Point p = this.ClientPointToView(e.X, e.Y);
            OnViewMouseDoubleClick(new MouseEventArgs(e.Button, e.Clicks, p.X, p.Y, e.Delta));
        }

        public event MouseEventHandler ViewMouseDoubleClick = null;

        protected virtual void OnViewMouseDoubleClick(MouseEventArgs e)
        {
            if (ViewMouseDoubleClick != null)
            {
                ViewMouseDoubleClick(this, e);
            }
        }


        ///// <summary>
        ///// 已重载:处理鼠标双击事件
        ///// </summary>
        ///// <param name="e">事件参数</param>
        //protected override void OnDoubleClick(EventArgs e)
        //{
        //    base.OnDoubleClick (e);
        //    System.Drawing.Point p = this.ViewMousePosition ;
        //    OnViewDoubleClick( new System.Windows.Forms.MouseEventArgs( System.Windows.Forms.Control.MouseButtons , 1 , p.X , p.Y , 0 ));
        //}
        ///// <summary>
        ///// 鼠标在视图区中双击事件
        ///// </summary>
        //public event System.Windows.Forms.MouseEventHandler ViewDoubleClick = null;
        ///// <summary>
        ///// 处理鼠标在视图区域中的双击事件
        ///// </summary>
        ///// <param name="e">事件参数</param>
        //protected virtual void OnViewDoubleClick( System.Windows.Forms.MouseEventArgs e )
        //{
        //    if( ViewDoubleClick != null )
        //        ViewDoubleClick( this , e );
        //}


		/// <summary>
		/// 已重载:重新绘制视图的事件处理
		/// </summary>
		/// <param name="e">事件参数</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint( e );
            if (this.DesignMode == false)
            {
                this.CheckZoomRate();
                this.RefreshScaleTransform();
                PaintEventArgs e2 = CreatePaintEventArgs(
                    e,
                    this.Transform as SimpleRectangleTransform);
                if (e2 != null)
                {
                    OnViewPaint(
                        e2,
                        this.Transform as SimpleRectangleTransform);
                }
            }
			//TransformPaint( e , this.myTransform as SimpleRectangleTransorm );
		}

        protected virtual PaintEventArgs CreatePaintEventArgs(
            PaintEventArgs e ,
            SimpleRectangleTransform trans)
        {
            if (trans == null)
            {
                return null ;
            }

            System.Drawing.Rectangle rect = e.ClipRectangle;
            rect.Offset(-1, -1);
            rect.Width += 2;
            rect.Height += 2;
            rect = System.Drawing.Rectangle.Intersect(trans.SourceRect, rect);
            if (rect.IsEmpty)
            {
                return null ;
            }

            System.Drawing.RectangleF rectf = trans.TransformRectangleF(
                rect.Left,
                rect.Top,
                rect.Width,
                rect.Height);// this.ClipRectangleToView( e.ClipRectangle );
            rect.X = (int)Math.Floor(rectf.Left);
            rect.Y = (int)Math.Floor(rectf.Top);
            //rect.Width = ( int ) System.Math.Ceiling( rectf.Width );
            //rect.Height = ( int ) System.Math.Ceiling( rectf.Height );
            rect.Width = (int)System.Math.Ceiling(rectf.Right) - rect.Left;
            rect.Height = (int)System.Math.Ceiling(rectf.Bottom) - rect.Top;

            e.Graphics.PageUnit = this.GraphicsUnit;
            e.Graphics.ResetTransform();

            //e.Graphics.TranslateTransform( trans.SourceRect.Left , trans.SourceRect.Top );
            e.Graphics.ScaleTransform(this.XZoomRate, this.YZoomRate);
            double rate = this.ClientToViewXRate * this.XZoomRate;

            e.Graphics.TranslateTransform(
                (float)(trans.SourceRect.Left * rate - trans.DescRectF.X),
                (float)(trans.SourceRect.Top * rate - trans.DescRectF.Y));

            if (trans.XZoomRate < 1)
            {
                rect.Width = rect.Width + (int)System.Math.Ceiling(1 / trans.XZoomRate);
            }
            if (trans.YZoomRate < 1)
            {
                rect.Height = rect.Height + (int)System.Math.Ceiling(1 / trans.YZoomRate);
            }
            rect.Height += 6;
            System.Windows.Forms.PaintEventArgs e2 =
                new System.Windows.Forms.PaintEventArgs(
                e.Graphics,
                rect);

            e2.Graphics.ResetClip();
            int widthFix = GraphicsUnitConvert.Convert(20, GraphicsUnit.Pixel, e2.Graphics.PageUnit);
            e2.Graphics.SetClip(new System.Drawing.Rectangle(
                rect.Left - widthFix ,
                rect.Top - widthFix ,
                rect.Width + widthFix * 2 ,
                rect.Height + widthFix  ));
            return e2;
        }

        //protected virtual void TransformPaint( PaintEventArgs e , SimpleRectangleTransform trans )
        //{
        //    if (trans == null)
        //    {
        //        return;
        //    }

        //    System.Drawing.Rectangle rect = e.ClipRectangle ;
        //    rect.Offset( -1, -1 );
        //    rect.Width += 2 ;
        //    rect.Height += 2 ;
        //    rect = System.Drawing.Rectangle.Intersect( trans.SourceRect , rect );
        //    if (rect.IsEmpty)
        //    {
        //        return;
        //    }

        //    System.Drawing.RectangleF rectf = trans.TransformRectangleF( 
        //        rect.Left ,
        //        rect.Top ,
        //        rect.Width ,
        //        rect.Height );// this.ClipRectangleToView( e.ClipRectangle );
        //    rect.X = ( int ) Math.Floor( rectf.Left );
        //    rect.Y = ( int ) Math.Floor( rectf.Top );
        //    //rect.Width = ( int ) System.Math.Ceiling( rectf.Width );
        //    //rect.Height = ( int ) System.Math.Ceiling( rectf.Height );
        //    rect.Width = ( int ) System.Math.Ceiling( rectf.Right ) - rect.Left ;
        //    rect.Height = ( int ) System.Math.Ceiling( rectf.Bottom ) - rect.Top ;

        //    e.Graphics.PageUnit = this.intGraphicsUnit ;
        //    e.Graphics.ResetTransform();

        //    //e.Graphics.TranslateTransform( trans.SourceRect.Left , trans.SourceRect.Top );
        //    e.Graphics.ScaleTransform( this.fXZoomRate , this.fYZoomRate );
        //    double rate = this.ClientToViewXRate * this.fXZoomRate ;

        //    e.Graphics.TranslateTransform(
        //        ( float ) ( trans.SourceRect.Left * rate - trans.DescRectF.X ),  
        //        ( float ) ( trans.SourceRect.Top * rate - trans.DescRectF.Y ) );

        //    if (trans.XZoomRate < 1)
        //    {
        //        rect.Width = rect.Width + (int)System.Math.Ceiling(1 / trans.XZoomRate);
        //    }
        //    if (trans.YZoomRate < 1)
        //    {
        //        rect.Height = rect.Height + (int)System.Math.Ceiling(1 / trans.YZoomRate);
        //    }
        //    rect.Height += 3;
        //    System.Windows.Forms.PaintEventArgs e2 = 
        //        new System.Windows.Forms.PaintEventArgs(
        //        e.Graphics ,
        //        rect ) ;

        //    e2.Graphics.ResetClip();
        //    e2.Graphics.SetClip( new System.Drawing.Rectangle(
        //        rect.Left ,
        //        rect.Top ,
        //        rect.Width + 1 ,
        //        rect.Height + 1 ));

        //    OnViewPaint( e2 , trans );
        //}

		/// <summary>
		/// 绘制视图的事件
		/// </summary>
		public event System.Windows.Forms.PaintEventHandler ViewPaint = null;
		/// <summary>
		/// 重新绘制视图的事件处理
		/// </summary>
		/// <param name="e">事件参数</param>
        protected virtual void OnViewPaint(PaintEventArgs e, SimpleRectangleTransform trans)
        {
            if (ViewPaint != null)
            {
                ViewPaint(this, e);
            }
        }

		
		/// <summary>
		/// 获得控件可视区域中内容的BMP图片对象
		/// </summary>
		/// <returns>BMP图片对象</returns>
		public System.Drawing.Bitmap GetVisibileContentBitmap()
		{
			System.Drawing.Bitmap bmp = new System.Drawing.Bitmap( this.ClientSize.Width , this.ClientSize.Height );
			using( System.Drawing.Graphics g = System.Drawing.Graphics.FromImage( bmp ))
			{
				g.Clear( this.BackColor );
				g.PageUnit = this.intGraphicsUnit ;
				this.OnPaint( new System.Windows.Forms.PaintEventArgs( g , this.ClientRectangle ));
			}
			return bmp;
		}


		public virtual System.Drawing.Graphics CreateViewGraphics( )
		{
            System.Drawing.Graphics g = this.CreateGraphics();
			g.PageUnit = this.intGraphicsUnit ;
			return g ;
		}

		protected System.Drawing.Bitmap CreateContentBitmap( float rate , System.Drawing.Color BmpBackColor )
		{
			SimpleRectangleTransform trans = this.myTransform as SimpleRectangleTransform ;
			if( trans == null )
				return null;

			System.Drawing.Size size = this.AutoScrollMinSize ;
			size.Width = ( int )( size.Width * rate );
			size.Height = ( int ) ( size.Height * rate );
			if( size.Width <= 0 || size.Height <= 0 )
				return null;
			System.Drawing.Bitmap bmp = new Bitmap( size.Width , size.Height );
			float rate2 = rate ;

			float rateback = this.fXZoomRate ;
			try
			{
				using( System.Drawing.Graphics g = System.Drawing.Graphics.FromImage( bmp ))
				{
					g.Clear( BmpBackColor );
					g.PageUnit = this.intGraphicsUnit ;
					g.ScaleTransform( rate2 , rate2 );
					g.TranslateTransform( - trans.DescRectF.X , - trans.DescRectF.Y );
					System.Windows.Forms.PaintEventArgs e = new PaintEventArgs( g , trans.DescRect );
					this.fXZoomRate = rate ;
					this.OnViewPaint( e , trans );
					this.fXZoomRate = rateback ;
				}
				return bmp ;
			}
			catch( Exception ext )
			{
				this.fXZoomRate = rateback ;
				throw ext ;
			}
		}
		//
		//		public void ViewInvertRect( System.Drawing.Rectangle rect )
		//		{
		//			rect = this.myTransform.UnTransformRectangle( rect );
		//			
		//		}

        private UpdateLock _UpdateLock = new UpdateLock();
        /// <summary>
        /// 开始更新内容，锁定用户界面
        /// </summary>
        public virtual void BeginUpdate()
        {

            _UpdateLock.BeginUpdate();
        }

        /// <summary>
        /// 结束更新内容，解锁用户界面
        /// </summary>
        public virtual void EndUpdate()
        {
            _UpdateLock.EndUpdate();
        }

        /// <summary>
        /// 是否正在更新内容，锁定用户界面
        /// </summary>
        [Browsable(false)]
        public bool IsUpdating
        {
            get
            {
                return _UpdateLock.Updating;
            }
        }

        //private int intUpdateLevel = 0 ;
        ///// <summary>
        ///// 开始更新文档,不响应用户界面
        ///// </summary>
        //public virtual void BeginUpdate()
        //{
        //    intUpdateLevel ++ ;
        //}
        ///// <summary>
        ///// 结束更新文档,恢复响应用户界面
        ///// </summary>
        //public virtual void EndUpdate()
        //{
        //    intUpdateLevel -- ;
        //    if( intUpdateLevel <= 0 )
        //    {
        //        intUpdateLevel = 0 ;
        //        this.ViewInvalidate( System.Drawing.Rectangle.Empty );
        //    }
        //}
        ///// <summary>
        ///// 正在更新文档,此时用户界面暂时冻结
        ///// </summary>
        //[System.ComponentModel.Browsable( false )]
        //public bool IsUpdating
        //{
        //    get
        //    {
        //        return intUpdateLevel > 0 ;
        //    }
        //}

		protected System.Drawing.Rectangle myInvalidateRect = System.Drawing.Rectangle.Empty ;
//		/// <summary>
//		/// 追加无效区域
//		/// </summary>
//		/// <param name="myRect"></param>
//		public virtual void AddInvalidateRect( System.Drawing.Rectangle ViewRect )
//		{
//			if( ViewRect.IsEmpty )
//				return ;
//			if( myInvalidateRect.IsEmpty )
//				myInvalidateRect = ViewRect ;
//			else
//				myInvalidateRect = System.Drawing.Rectangle.Union( myInvalidateRect , ViewRect );
//		}

		/// <summary>
		/// 根据当前控件的更新状态来修正坐标来修正无效矩形
		/// </summary>
		/// <param name="ViewRect">无效矩形</param>
		/// <returns>修正后的无效矩形</returns>
		protected virtual System.Drawing.Rectangle FixViewInvalidateRect( System.Drawing.Rectangle ViewRect )
		{
			if(myInvalidateRect.IsEmpty )
			{
				myInvalidateRect = ViewRect ;
			}
			else if( ViewRect.IsEmpty == false )
			{
				myInvalidateRect = System.Drawing.Rectangle.Union( myInvalidateRect , ViewRect );
			}
			if( this.IsUpdating )
			{
				return System.Drawing.Rectangle.Empty ;
			}
			else
			{
				System.Drawing.Rectangle rect = myInvalidateRect ;
				myInvalidateRect = System.Drawing.Rectangle.Empty ;
				return rect ;
			}
		}

		/// <summary>
		/// 使用视图坐标来指定区域无效
		/// </summary>
		/// <param name="ViewBounds">无效区域</param>
		public virtual void ViewInvalidate( System.Drawing.Rectangle ViewBounds )
		{
            if (this.IsUpdating)
                return;
			ViewBounds = this.FixViewInvalidateRect( ViewBounds );
			if( ! ViewBounds.IsEmpty )
			{
				this.RefreshScaleTransform();
				System.Drawing.Rectangle rect = myTransform.UnTransformRectangle( ViewBounds );
				//System.Console.WriteLine( rect.Width + " " + rect.Height );
				this.Invalidate( rect );
			}
		}

#if CaptureMouseMove

		protected class MyCapturer : MouseCapturer 
		{
			public BorderUserControl Control = null;
			protected override CaptureMouseMoveEventArgs CreateArgs( )
			{
				DocumentViewControl ctl = (  DocumentViewControl ) base.BindControl ;
				CaptureMouseMoveEventArgs e = base.CreateArgs( );
				e.StartPosition = ctl.myTransform.TransformPoint( e.StartPosition );
				e.CurrentPosition = ctl.myTransform.TransformPoint( e.CurrentPosition );
				return e ;
			}
			public int DragStyle = 0 ;
			public System.Drawing.Rectangle[] Rects = null;
			public System.Drawing.Rectangle ViewClipRectangle = System.Drawing.Rectangle.Empty ;
		}

		/// <summary>
		/// 进行鼠标拖拽操作
		/// </summary>
		/// <param name="DrawFunction">鼠标拖拽期间的回调函数委托</param>
		/// <param name="ClipRectangle">使用视图坐标的剪切矩形</param>
		/// <returns>点坐标数组,包含开始拖拽和结束拖拽时鼠标的视图坐标位置</returns>
		public virtual System.Drawing.Point[] CaptureMouseMove(
			CaptureMouseMoveEventHandler DrawFunction , 
			System.Drawing.Rectangle ClipRectangle ,
			object Tag )
		{
            if (this.bolMouseDragScroll)
            {
                return null;
            }
			//			if( this.myLockContentBmp != null )
			//				myLockContentBmp.Dispose();
			//			myLockContentBmp = this.GetVisibileContentBitmap();

			this.RefreshScaleTransform();
			
			MyCapturer mc = new MyCapturer( );
			mc.Tag = Tag ;
			mc.BindControl = this ;
			if( ClipRectangle.IsEmpty == false )
			{
				ClipRectangle = myTransform.UnTransformRectangle( ClipRectangle );
				mc.ClipRectangle = ClipRectangle ;
			}
			mc.ReversibleShape = ReversibleShapeStyle.Custom ;
            if (DrawFunction != null)
            {
                mc.Draw += DrawFunction;
            }
			if( mc.CaptureMouseMove())
			{
			
				System.Drawing.Point p1 = mc.StartPosition ;
				p1 = myTransform.TransformPoint( p1 );

				System.Drawing.Point p2 = mc.EndPosition ;
				p2 = myTransform.TransformPoint( p2 );

				return new System.Drawing.Point[]{ p1 , p2 };
			}//if
			return null;
		}
		
		/// <summary>
		/// 使用鼠标拖拽获得一个矩形,但可逆图形是椭圆
		/// </summary>
		/// <param name="ClipRectangle">剪切矩形</param>
		/// <returns>获得的矩形区域</returns>
		public System.Drawing.Point[] GetCaptureMouseEllipse( System.Drawing.Rectangle ClipRectangle )
		{
			return CaptureMouseMove(
				new CaptureMouseMoveEventHandler( this.myCaptueEllipse ) ,
				ClipRectangle ,
				null );
		}

		/// <summary>
		/// 使用鼠标拖拽获得一个矩形
		/// </summary>
		/// <param name="ClipRectangle">剪切矩形</param>
		/// <returns>获得的矩形区域</returns>
		public System.Drawing.Point[] GetCaptureMouseRectangle( System.Drawing.Rectangle ClipRectangle )
		{
			return CaptureMouseMove(
				new CaptureMouseMoveEventHandler( this.myCaptureRectangle ) ,
				ClipRectangle ,
				null );
		}
		/// <summary>
		/// 使用鼠标拖拽获得一个线段
		/// </summary>
		/// <param name="ClipRectangle">剪切矩形</param>
		/// <returns>线段的两个端点坐标组成的数组</returns>
		public System.Drawing.Point[] GetCaptureMouseLine( System.Drawing.Rectangle ClipRectangle )
		{
			return CaptureMouseMove(
				new CaptureMouseMoveEventHandler( this.myCaptureLine ) ,
				ClipRectangle ,
				null );
		}
		private void myCaptueEllipse( object sender , CaptureMouseMoveEventArgs e )
		{
			System.Drawing.Rectangle rect = RectangleCommon.GetRectangle(
				e.StartPosition  , 
				e.CurrentPosition );
			if( rect.IsEmpty == false )
				this.ReversibleViewDrawEllipse( rect , e.ResumeView );
		}
		private void myCaptureRectangle( object sender , CaptureMouseMoveEventArgs e )
		{
			System.Drawing.Rectangle rect = RectangleCommon.GetRectangle(
				e.StartPosition  , 
				e.CurrentPosition );
            if (rect.IsEmpty == false)
            {
                ReversibleViewDrawRect(rect, e.ResumeView);
            }
		}
		private void myCaptureLine( object sender , CaptureMouseMoveEventArgs e )
		{
			ReversibleViewDrawLine( e.StartPosition , e.CurrentPosition );
		}

#endif

#if ReversibleDraw

		#region 绘制可逆图形的成员群 ******************************************

		protected System.Drawing.Bitmap myLockContentBmp = null;

		/// <summary>
		/// 使用视图坐标绘制一个连续的可逆线段
		/// </summary>
		/// <param name="ps">视图坐标点数组</param>
		public void ReversibleViewDrawLines( System.Drawing.Point[] ps , System.Drawing.Graphics g )
		{
			if( ps == null || ps.Length <= 1 )
			{
				return ;
			}
			ReversibleDrawer drawer = null ;
			if( g == null )
				drawer = ReversibleDrawer.FromHwnd( this.Handle );
			else
				drawer = new ReversibleDrawer( g );
			drawer.PenStyle = intReversibleLineStyle ;
			drawer.PenColor = System.Drawing.Color.Red ;
			this.RefreshScaleTransform();
			System.Drawing.Point[] ps2 = new Point[ ps.Length ] ;
			for( int iCount = 0 ; iCount < ps.Length ; iCount ++ )
			{
				ps2[ iCount ] = myTransform.UnTransformPoint( ps[ iCount ] );
			}
			drawer.DrawLines( ps2 );
			drawer.Dispose();
		}

		/// <summary>
		/// 使用视图坐标绘制一个连续的可逆线段
		/// </summary>
		/// <param name="ps">视图坐标点数组</param>
		public void ReversibleViewDrawLines( System.Drawing.Point[] ps )
		{
			if( ps == null || ps.Length <= 1 )
			{
				return ;
			}
			using( ReversibleDrawer drawer
					   = ReversibleDrawer.FromHwnd( this.Handle ))
			{
				drawer.PenStyle = intReversibleLineStyle ;
				drawer.PenColor = System.Drawing.Color.Red ;
				this.RefreshScaleTransform();
				System.Drawing.Point[] ps2 = new Point[ ps.Length ] ;
				for( int iCount = 0 ; iCount < ps.Length ; iCount ++ )
				{
					ps2[ iCount ] = myTransform.UnTransformPoint( ps[ iCount ] );
				}
				drawer.DrawLines( ps2 );
			}
		}

		/// <summary>
		/// 使用视图坐标绘制一个可逆线段
		/// </summary>
		/// <param name="x1">线段起点X坐标</param>
		/// <param name="y1">线段起点Y坐标</param>
		/// <param name="x2">线段终点X坐标</param>
		/// <param name="y2">线段终点Y坐标</param>
		public void ReversibleViewDrawLine( 
			int x1 , 
			int y1 ,
			int x2 ,
			int y2 )
		{
			using( ReversibleDrawer drawer
					   = ReversibleDrawer.FromHwnd( this.Handle ))
			{
				drawer.PenStyle = intReversibleLineStyle ;
				drawer.PenColor = System.Drawing.Color.Red ;
				this.RefreshScaleTransform();
				System.Drawing.Point p1 = myTransform.UnTransformPoint( x1 , y1 );
				System.Drawing.Point p2 = myTransform.UnTransformPoint( x2 , y2 );
				
				drawer.DrawLine( p1 , p2 );
			}
		}

		/// <summary>
		/// 使用视图坐标绘制一个可逆线段边框
		/// </summary>
		/// <param name="p1">线段起点坐标</param>
		/// <param name="p2">线段终点坐标</param>
		public virtual void ReversibleViewDrawLine( 
			System.Drawing.Point p1 ,
			System.Drawing.Point p2 )
		{
			ReversibleViewDrawLine( p1.X , p1.Y , p2.X , p2.Y );
		}
		/// <summary>
		/// 使用视图坐标绘制一个可逆矩形
		/// </summary>
		/// <param name="rect">矩形坐标</param>
		public void ReversibleViewDrawRect( System.Drawing.Rectangle rect , bool ResumeView )
		{
			ReversibleViewDrawRect( rect.Left , rect.Top , rect.Width , rect.Height , ResumeView );
		}

		/// <summary>
		/// 使用视图坐标绘制一个可逆矩形边框
		/// </summary>
		/// <param name="left">矩形的左端位置</param>
		/// <param name="top">矩形的顶端位置</param>
		/// <param name="width">矩形的宽度</param>
		/// <param name="height">矩形的高度</param>
		public virtual void ReversibleViewDrawRect( 
			int left , 
			int top ,
			int width ,
			int height ,
			bool ResumeView )
		{
            this.RefreshScaleTransform();
            System.Drawing.Rectangle rect
                = this.myTransform.UnTransformRectangle(left, top, width, height);
            //rect.Location = this.PointToScreen(rect.Location);
            //System.Windows.Forms.ControlPaint.DrawReversibleFrame(rect, System.Drawing.Color.White, FrameStyle.Dashed );
            //using (System.Drawing.Graphics g = this.CreateGraphics())
            //{
            //    using(ReversibleDrawer drawer = new ReversibleDrawer( g ))
            //    {
            //        drawer.PenStyle = intReversibleLineStyle;
            //        drawer.PenColor = System.Drawing.Color.White;
            //        drawer.LineWidth = 1;

            //        drawer.DrawRectangle(rect);
            //        //g.Flush(System.Drawing.Drawing2D.FlushIntention.Flush);
            //    }
            //}
            using (ReversibleDrawer drawer
                       = ReversibleDrawer.FromHwnd(this.Handle))
            {
                drawer.PenStyle = intReversibleLineStyle;
                drawer.PenColor = System.Drawing.Color.White;
                drawer.LineWidth = 1;

                drawer.DrawRectangle(rect);
            }
		}

		private PenStyle intReversibleLineStyle = PenStyle.PS_SOLID ;
		/// <summary>
		/// 可逆线条样式
		/// </summary>
        [System.ComponentModel.DefaultValue( PenStyle.PS_SOLID )]
		public virtual PenStyle ReversibleLineStyle
		{
			get
			{
				return intReversibleLineStyle ;
			}
			set
			{
				intReversibleLineStyle = value;
			}
		}

		/// <summary>
		/// 使用绘图坐标填充一个可逆矩形区域
		/// </summary>
		/// <param name="rect">矩形区域</param>
		public void ReversibleViewFillRect( System.Drawing.Rectangle rect )
		{
			ReversibleViewFillRect( rect.Left , rect.Top , rect.Width , rect.Height );
		}
		/// <summary>
		/// 使用绘图坐标填充一个可逆矩形区域
		/// </summary>
		/// <param name="left">矩形的左端位置</param>
		/// <param name="top">矩形的顶端位置</param>
		/// <param name="width">矩形的宽度</param>
		/// <param name="height">矩形的高度</param>
		public void ReversibleViewFillRect( int left , int top , int width , int height )
		{
			using( ReversibleDrawer drawer
					   = ReversibleDrawer.FromHwnd( this.Handle ))
			{
				this.RefreshScaleTransform();
				System.Drawing.RectangleF rect = this.myTransform.UnTransformRectangleF( left , top , width , height );

				drawer.FillRectangle( System.Drawing.Rectangle.Ceiling( rect ));
			}
		}

		/// <summary>
		/// 使用绘图坐标填充一个可逆矩形区域
		/// </summary>
		/// <param name="rect">矩形区域</param>
		public void ReversibleViewFillRect( System.Drawing.Rectangle rect , System.Drawing.Graphics g )
		{
			ReversibleViewFillRect( rect.Left , rect.Top , rect.Width , rect.Height , g );
		}
		/// <summary>
		/// 使用绘图坐标填充一个可逆矩形区域
		/// </summary>
		/// <param name="left">矩形的左端位置</param>
		/// <param name="top">矩形的顶端位置</param>
		/// <param name="width">矩形的宽度</param>
		/// <param name="height">矩形的高度</param>
		public void ReversibleViewFillRect( int left , int top , int width , int height , System.Drawing.Graphics g )
		{
			IntPtr hdc = g.GetHdc();
			using( ReversibleDrawer drawer = ReversibleDrawer.FromHDC( hdc ))
			{
				this.RefreshScaleTransform();
				System.Drawing.Rectangle rect = this.myTransform.UnTransformRectangle( left , top , width , height );
                rect.Location = this.ViewPointToClient(left , top );
				drawer.FillRectangle( rect );
			}
			g.ReleaseHdc( hdc );
		}

		/// <summary>
		/// 使用视图坐标绘制一个可逆椭圆
		/// </summary>
		/// <param name="rect">椭圆外切矩形坐标</param>
		public void ReversibleViewDrawEllipse( System.Drawing.Rectangle rect , bool ResumeView )
		{
			ReversibleViewDrawEllipse( rect.Left , rect.Top , rect.Width , rect.Height , ResumeView );
		}

		/// <summary>
		/// 使用视图坐标绘制一个可逆矩形边框
		/// </summary>
		/// <param name="left">矩形的左端位置</param>
		/// <param name="top">矩形的顶端位置</param>
		/// <param name="width">矩形的宽度</param>
		/// <param name="height">矩形的高度</param>
		public virtual void ReversibleViewDrawEllipse( 
			int left , 
			int top ,
			int width ,
			int height ,
			bool ResumeView )
		{
			using( ReversibleDrawer drawer
					   = ReversibleDrawer.FromHwnd( this.Handle ))
			{
				drawer.PenStyle = intReversibleLineStyle ;
				drawer.PenColor = System.Drawing.Color.White ;
				drawer.LineWidth = 1 ;
				this.RefreshScaleTransform();
				System.Drawing.Rectangle rect 
					= this.myTransform.UnTransformRectangle( left , top , width , height );

				drawer.DrawEllipse( rect );
			}
		}

		#endregion

#endif

		#region 处理自动滚动的成员 ********************************************

		/// <summary>
		/// 移动滚动定位位置
		/// </summary>
		/// <param name="p">滚动改变量</param>
		public void MoveScrollPos( System.Drawing.Point p)
		{
			MoveScrollPos(p.X ,p.Y );
		}
		/// <summary>
		/// 移动滚动定位位置
		/// </summary>
		/// <param name="dx">横向滚动量</param>
		/// <param name="dy">纵向滚动量</param>
		public virtual void MoveScrollPos( int dx , int dy )
		{
			if( dx != 0 || dy != 0 )
			{
				System.Drawing.Point p = this.AutoScrollPosition ;
				this.SetAutoScrollPosition( new System.Drawing.Point(   dx - p.X  , dy - p.Y ));
			}
		}

		/// <summary>
		/// 滚动控件,保证指定区域在控件显示区域,参数采用视图坐标
		/// </summary>
		/// <param name="rect">指定的区域</param>
		/// <returns>操作是否导致滚动</returns>
		public virtual bool ScrollToView( System.Drawing.Rectangle rect )
		{
			return ScrollToView(
                rect.Left ,
                rect.Top ,
                rect.Width ,
                rect.Height ,
                ScrollToViewStyle.Normal );
		}

		/// <summary>
		/// 滚动控件,保证指定区域在控件显示区域中,参数采用视图坐标
		/// </summary>
		/// <param name="x">指定区域的左端位置</param>
		/// <param name="y">指定区域的顶端位置</param>
		/// <param name="width">指定区域的宽度</param>
		/// <param name="height">指定区域的高度</param>
		/// <returns>操作是否导致滚动</returns>
        public bool ScrollToView(int x, int y, int width, int height)
        {
            return ScrollToView(
                x,
                y,
                width, 
                height,
                ScrollToViewStyle.Normal);
        }

        public bool ScrollToView(Rectangle rect, ScrollToViewStyle style)
        {
            return ScrollToView(rect.Left , rect.Top , rect.Width , rect.Height , style);
        }

        public bool ScrollToView(
            int left,
            int top,
            int width,
            int height,
            ScrollToViewStyle style)
        {
            if (style == ScrollToViewStyle.Normal)
            {
                // 滚动视图，使得指定区域显示在可视区域中。
                System.Drawing.Rectangle rect = this.Transform.UnTransformRectangle(
                    left,
                    top,
                    width,
                    height);
                rect.Location = this.ViewPointToClient(left, top);
               
                if (rect.IsEmpty == false)
                {
                    //rect.Offset(  - this.AutoScrollPosition.X , - this.AutoScrollPosition.Y );
                    return InnerScrollToView(rect.Left, rect.Top, rect.Width, rect.Height);
                }
                else
                {
                    return false;
                }
            }
            else if (style == ScrollToViewStyle.Top)
            {
                // 滚动视图，使得指定区域显示在可视区域的顶端
                System.Drawing.Point p = this.ViewPointToClient(left, top);// this.Transform.UnTransformPoint(left, top);
                if (p.IsEmpty == false)
                {
                    p = new Point(
                        p.X - this.ClientSize.Width / 2 - this.AutoScrollPosition.X,
                        p.Y - this.ClientSize.Height / 2 - this.AutoScrollPosition.Y);
                    this.SetAutoScrollPosition(p);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (style == ScrollToViewStyle.Middle)
            {
                // 滚动视图，使得指定区域显示在可视区域的中间
                int cx = left + width / 2;
                int cy = top + height / 2;
                System.Drawing.Point p = this.ViewPointToClient(cx, cy);// myTransform.UnTransformPoint(cx, cy);
                if (p.IsEmpty == false)
                {
                    p = new Point(
                        p.X - this.ClientSize.Width / 2 - this.AutoScrollPosition.X,
                        p.Y - this.ClientSize.Height / 2 - this.AutoScrollPosition.Y);
                    this.SetAutoScrollPosition(p);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (style == ScrollToViewStyle.Bottom)
            {
                // 滚动视图，使得指定区域显示在可视区域的底部。
                System.Drawing.Point p = this.ViewPointToClient(left, top + height); // this.Transform.UnTransformPoint(left, top + height);
                if (p.IsEmpty == false)
                {
                    p = new Point(
                        p.X - this.ClientSize.Width / 2 - this.AutoScrollPosition.X,
                        p.Y - this.ClientSize.Height / 2 - this.AutoScrollPosition.Y);
                    this.SetAutoScrollPosition(p);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 滚动客户区，使得指定矩形显示在可视区域中，这个矩形不进行视图坐标转换
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public bool ScrollClientBoundsToView(
            int left,
            int top,
            int width,
            int height,
            ScrollToViewStyle style)
        {
            if (style == ScrollToViewStyle.Normal)
            {
                // 滚动视图，使得指定区域显示在可视区域中。
                return InnerScrollToView(left, top, width, height);
            }
            else if (style == ScrollToViewStyle.Top)
            {
                // 滚动视图，使得指定区域显示在可视区域的顶端
                int cx = left ;
                int cy = top ;
                Point p = new Point(cx, cy);
                p = new Point(
                    p.X - this.AutoScrollPosition.X,
                    p.Y - this.AutoScrollPosition.Y);
                this.SetAutoScrollPosition(p);
                return true;
            }
            else if (style == ScrollToViewStyle.Middle)
            {
                // 滚动视图，使得指定区域显示在可视区域的中间
                int cx = left + width / 2;
                int cy = top + height / 2;
                Point p = new Point( cx , cy );
                p = new Point(
                    p.X - this.ClientSize.Width / 2 - this.AutoScrollPosition.X,
                    p.Y - this.ClientSize.Height / 2 - this.AutoScrollPosition.Y);
                this.SetAutoScrollPosition(p);
                return true;
            }
            else if (style == ScrollToViewStyle.Bottom)
            {
                // 滚动视图，使得指定区域显示在可视区域的底部。
                System.Drawing.Point p = new Point(left, top + height);
                p = new Point(
                    p.X - this.AutoScrollPosition.X,
                    p.Y - this.ClientSize.Height  - this.AutoScrollPosition.Y);
                this.SetAutoScrollPosition(p);
                return true;
            }
            return false;
        }

        ///// <summary>
        ///// 滚动控件,使得指定区域显示在控件中央,参数采用视图坐标
        ///// </summary>
        ///// <param name="x">指定区域的左端位置</param>
        ///// <param name="y">指定区域的顶端位置</param>
        ///// <param name="width">指定区域的宽度</param>
        ///// <param name="height">指定区域的高度</param>
        ///// <returns>操作是否导致滚动</returns>
        //public bool ScrollToCenter(int x, int y, int width, int height)
        //{
        //    int cx = x + width / 2;
        //    int cy = y + height / 2;
        //    System.Drawing.Point p = myTransform.UnTransformPoint(cx, cy);
        //    if (p.IsEmpty == false)
        //    {
        //        p = new Point(
        //            p.X - this.ClientSize.Width / 2 - this.AutoScrollPosition.X,
        //            p.Y - this.ClientSize.Height / 2 - this.AutoScrollPosition.Y);
        //        this.SetAutoScrollPosition(p);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

		private bool bolScrollFlag = false;
		/// <summary>
		/// 进行滚动的标志
		/// </summary>
		[System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
		public bool ScrollFlag
		{
			get
			{
				return bolScrollFlag;
			}
			set
			{
				bolScrollFlag = value;
			}
		}

		/// <summary>
		/// 滚动控件,保证指定区域在控件显示区域中
		/// </summary>
		/// <param name="x">指定区域的左端位置</param>
		/// <param name="y">指定区域的顶端位置</param>
		/// <param name="width">指定区域的宽度</param>
		/// <param name="height">指定区域的高度</param>
		/// <returns>操作是否导致滚动</returns>
		protected bool InnerScrollToView( int x , int y , int width , int height )
		{
			bool bolResult = false;
			if( width >=0 && height >=0 )
			{
				//if( x < 0 ) x = 0 ;
				//if( y < 0 ) y = 0 ;
				int dx = 0 ;
				int dy = 0 ;
				System.Drawing.Point myPoint = new System.Drawing.Point(x,y);
                System.Drawing.Rectangle clientRect = this.ClientRectangle;
                if (clientRect.Width <= 0 || clientRect.Height <= 0)
                {
                    //控件客户区不可见，直接返回。
                    return false;
                }
                clientRect.Location = this.PointToScreen(clientRect.Location);
                clientRect = Rectangle.Intersect(clientRect, System.Windows.Forms.Screen.GetWorkingArea(this));
                if (clientRect.IsEmpty == false)
                {
                    clientRect.Location = this.PointToClient(clientRect.Location);
                }

				//System.Drawing.Size clientSize = this.ClientSize ;

				System.Drawing.Point ScrollPos = this.AutoScrollPosition ;

                if (myPoint.X > clientRect.Right - width + 3)
                {
                    if ( clientRect.Right - width + 3 > 0)
                    {
                        dx = myPoint.X - clientRect.Right + width + 3;
                    }
                }
                if (width > clientRect.Width)
                {
                    if (myPoint.X > 3)
                    {
                        dx = myPoint.X - 3;
                    }
                    if (myPoint.X + width < clientRect.Width)
                    {
                        dx = myPoint.X - clientRect.Width + width + 3;
                    }
                }
                else
                {
                    if (myPoint.X < clientRect.Left )
                    {
                        dx = myPoint.X - 3 - clientRect.Left ;
                    }
                }

				if(myPoint.Y > clientRect.Bottom - height + 3  )
				{
                    if (clientRect.Bottom - height + 3 > 0)
                    {
                        dy = myPoint.Y - clientRect.Bottom + height + 3;
                    }
                    else
                    {
                        if (myPoint.Y + height < clientRect.Top )
                        {
                            dy = myPoint.Y - clientRect.Bottom + height + 3;
                        }
                        else if (myPoint.Y > clientRect.Bottom)
                        {
                            dy = myPoint.Y - 3;
                        }
                    }
				}
				if( height > clientRect.Bottom )
				{
                    if (myPoint.Y > 3)
                    {
                        dy = myPoint.Y - 3;
                    }
                    if (myPoint.Y + height < clientRect.Bottom)
                    {
                        dy = myPoint.Y - clientRect.Bottom + height + 3;
                    }
				}
				else
				{
                    if (myPoint.Y < clientRect.Top )
                    {
                        dy = myPoint.Y - 3 - clientRect.Top ;
                    }
				}
				
				if( dx != 0 || dy != 0)
				{
					bolScrollFlag = true;
					//System.Console.WriteLine( "DX:" + dx + " DY:" + dy );
					System.Drawing.Point p = new System.Drawing.Point(
						dx - this.AutoScrollPosition.X  , 
						dy - this.AutoScrollPosition.Y  );
					SetAutoScrollPosition( p );
					bolResult = true;
				}
			}
			return bolResult ;
		}
		public void SetAutoScrollPosition( System.Drawing.Point p )
		{
			if( this.IsUpdating )
			{
				return ;
			}
			System.Drawing.Point OldP = new Point( 
                - this.AutoScrollPosition.X , - this.AutoScrollPosition.Y );

            if (OldP.Equals(p))
            {
                return;
            }
            if (DCSoft.Common.StackTraceHelper.CheckRecursion())
            {
                return;
            }
			//DCSoft.Common.StackTraceHelper.OutputStackTrace();
			//System.Console.Write("Scroll   ----   x:" + Convert.ToString( p.X + this.AutoScrollPosition.X ) + " y:" + Convert.ToString( p.Y + this.AutoScrollPosition.Y ));
			bolScrolling = true;
			//			if( true )
			//			{
			//				for( int iCount = 1 ; iCount <= 4 ; iCount ++ )
			//				{
			//					System.Drawing.Point np = new Point( OldP.X + ( p.X - OldP.Y ) * iCount / 4 , OldP.Y + ( p.Y - OldP.Y ) * iCount / 4 );
			//					this.AutoScrollPosition = np ;
			//					this.Update();
			//					System.Threading.Thread.Sleep( 50 );
			//				}
			//			}
			//			else
		{
 
            if (this.FixedBackground)
            {
                LockWindowUpdate(this.Handle);
                this.AutoScrollPosition = p;
                LockWindowUpdate(IntPtr.Zero);
            }
            else
            {
                this.AutoScrollPosition = p;
            }
 
			this.Update();
		}
			bolScrolling = false;
			this.OnXScroll();
			//System.Console.WriteLine("End Scroll");
		}
		/// <summary>
		/// 视图正在滚动中
		/// </summary>
		protected bool bolScrolling = false;

		#endregion

	}
}