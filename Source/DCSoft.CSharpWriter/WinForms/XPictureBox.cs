/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Drawing;
using System.Windows.Forms;
using DCSoft.WinForms.Native;

namespace DCSoft.WinForms
{
	/// <summary>
	/// 增强的图片显示框
	/// </summary>
    [System.ComponentModel.ToolboxItem( false )]
	public class XPictureBox : System.Windows.Forms.UserControl 
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public XPictureBox()
		{
			this.SetStyle( System.Windows.Forms.ControlStyles.Selectable , true );
			this.SetStyle( System.Windows.Forms.ControlStyles.ResizeRedraw , true );
			this.AutoScroll  = true;
			this.BackColor = System.Drawing.Color.Black ;
		}

        private XPictureBoxBehaviorStyle intBehaviorStyle = XPictureBoxBehaviorStyle.DragScroll;
        /// <summary>
        /// 操作模式
        /// </summary>
        [System.ComponentModel.Category("Behavior") ]
        [System.ComponentModel.DefaultValue( XPictureBoxBehaviorStyle.DragScroll )]
        public XPictureBoxBehaviorStyle BehaviorStyle
        {
            get
            {
                return intBehaviorStyle; 
            }
            set 
            {
                intBehaviorStyle = value;
                if (intBehaviorStyle == XPictureBoxBehaviorStyle.None || intBehaviorStyle == XPictureBoxBehaviorStyle.DragSelect)
                    this.Cursor = Cursors.Default;
                else
                    this.Cursor = Cursors.SizeAll;
            }
        }

        //private bool bolEnableSelectArea = false;
        ///// <summary>
        ///// 是否允许选择指定区域的图像
        ///// </summary>
        //[System.ComponentModel.Category("Behavior")]
        //[System.ComponentModel.DefaultValue( false )]
        //public bool EnableSelectArea
        //{
        //    get
        //    {
        //        return bolEnableSelectArea; 
        //    }
        //    set
        //    {
        //        bolEnableSelectArea = value; 
        //    }
        //}

        public System.Drawing.Bitmap GetSelectedBmp()
        {
            if (this.Image == null)
                return null;
            if (mySelectionBounds.IsEmpty == false)
            {
                mySelectionBounds = System.Drawing.Rectangle.Intersect(mySelectionBounds, new Rectangle(0, 0, this.Image.Width, this.Image.Height));
                if (mySelectionBounds.IsEmpty == false)
                {
                    System.Drawing.Bitmap bmp = new Bitmap(mySelectionBounds.Width, mySelectionBounds.Height);
                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
                    {
                        g.DrawImage(this.Image, - mySelectionBounds.X, - mySelectionBounds.Y);
                    }
                    return bmp;
                }
            }
            return null;
        }



        private Rectangle mySelectionBounds = Rectangle.Empty;
        /// <summary>
        /// 选择区域
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.ComponentModel.DesignerSerializationVisibility( System.ComponentModel.DesignerSerializationVisibility.Hidden )]
        public Rectangle SelectionBounds
        {
            get 
            {
                return mySelectionBounds; 
            }
            set 
            {
                mySelectionBounds = value; 
            }
        }

		private System.Drawing.Image myImage = null;
		/// <summary>
		/// 对象内显示的图片对象
		/// </summary>
        [System.ComponentModel.DefaultValue( null )]
		public System.Drawing.Image Image
		{
			get
            {
                return myImage ;
            }
			set
            {
                myImage = value;
                OnImageChanged () ;
            }
		}
		/// <summary>
		/// 从指定流中加载图片对象
		/// </summary>
		/// <param name="myStream">流对象</param>
		/// <returns>加载是否成功</returns>
		public bool LoadImage( System.IO.Stream myStream )
		{
			myImage = System.Drawing.Image.FromStream( myStream );
			OnImageChanged();
			return myImage != null;
		}
		/// <summary>
		/// 从指定的字节数组中加载图片对象
		/// </summary>
		/// <param name="bs">字节数组</param>
		/// <returns>加载是否成功</returns>
		public bool LoadImage( byte[] bs)
		{
			using( System.IO.MemoryStream myStream = new System.IO.MemoryStream( bs ))
			{
				return LoadImage( myStream );
			}
		}
		/// <summary>
		/// 从指定文件中加载图片对象
		/// </summary>
		/// <param name="strFileName">文件名</param>
		/// <returns>加载是否成功</returns>
		public bool LoadImage( string strFileName )
		{
			myImage = System.Drawing.Image.FromFile( strFileName );
			OnImageChanged();
			return myImage != null;
		}
		/// <summary>
		/// 保存图片到指定的文件中,并根据图片文件名扩展名来决定保存的图片文件格式
		/// </summary>
		/// <param name="strFileName">文件名</param>
		/// <returns>操作是否成功</returns>
		public bool SaveImage( string strFileName )
		{
			if( myImage == null)
				return false;
			string strExt = System.IO.Path.GetExtension( strFileName );
			if( strExt != null )
				strExt = strExt.Trim().ToLower();
			else
				strExt = ".bmp";
			myImage.Save( strFileName , GetFormat( strExt ) );
			return true;
		}

		public bool Copy()
		{
			if( myImage == null )
				return false;
			System.Windows.Forms.DataObject d = new System.Windows.Forms.DataObject();
            System.Drawing.Bitmap bmp = this.GetSelectedBmp();
            if (bmp == null)
            {
                bmp = new System.Drawing.Bitmap(this.myImage);
            }
			d.SetData( System.Windows.Forms.DataFormats.Bitmap , bmp );
			System.Windows.Forms.Clipboard.SetDataObject( d );
			return true;
		}
		public bool Paste()
		{
			System.Windows.Forms.IDataObject d =  System.Windows.Forms.Clipboard.GetDataObject();
			if( d.GetDataPresent( System.Windows.Forms.DataFormats.Bitmap ))
			{
				System.Drawing.Image img = ( System.Drawing.Image ) d.GetData( System.Windows.Forms.DataFormats.Bitmap );
				if( img != null )
				{
					myImage = img ;
                    mySelectionBounds = Rectangle.Empty;
					OnImageChanged();
					return true ;
				}
			}
			return false;
		}
		private System.Drawing.Imaging.ImageFormat GetFormat( string strExtension )
		{
			System.Drawing.Imaging.ImageFormat myFormat = null;
			switch( strExtension )
			{
				case ".png":
					myFormat = System.Drawing.Imaging.ImageFormat.Png ;
					break;
				case ".jpg":
					myFormat = System.Drawing.Imaging.ImageFormat.Jpeg ;
					break;
				case ".jpeg":
					myFormat = System.Drawing.Imaging.ImageFormat.Jpeg ;
					break;
				case ".emf":
					myFormat = System.Drawing.Imaging.ImageFormat.Emf ;
					break;
				case ".gif":
					myFormat = System.Drawing.Imaging.ImageFormat.Gif ;
					break;
				case ".tiff":
					myFormat = System.Drawing.Imaging.ImageFormat.Tiff ;
					break;
				default:
					myFormat = System.Drawing.Imaging.ImageFormat.Bmp ;
					break;
			}
			return myFormat ;
		}
		protected void OnImageChanged()
		{
			if( myImage == null)
				this.AutoScrollMinSize = new System.Drawing.Size( 0 , 0 );
			else
				this.AutoScrollMinSize = myImage.Size ;
			this.Invalidate();
			if( ImageChanged != null)
				ImageChanged( this , null );
		}
		/// <summary>
		/// 图片内容发生改变时的事件
		/// </summary>
		public event System.EventHandler ImageChanged = null;
        [System.ComponentModel.Browsable( false )]
		public System.Drawing.Rectangle ImageBounds
		{
			get
			{
				if( myImage != null )
				{
					System.Drawing.Rectangle rect = new System.Drawing.Rectangle( 0 , 0 , myImage.Size.Width , myImage.Size.Height );
					System.Drawing.Size cs = this.ClientSize ;
					if( cs.Width > rect.Width )
						rect.X = ( cs.Width - rect.Width ) /2 ;
					if( cs.Height > rect.Height )
						rect.Y = ( cs.Height - rect.Height ) / 2 ;
					return rect ;
				}
				return System.Drawing.Rectangle.Empty ;
			}
		}

 
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			if( myImage != null)
			{
				System.Drawing.Point sp = this.AutoScrollPosition ;
				System.Drawing.Rectangle rect = this.ImageBounds ;
				System.Drawing.Point ip = rect.Location ;
				System.Drawing.Rectangle ClipRect = e.ClipRectangle ;
				ClipRect.Offset( 0 - sp.X , 0 - sp.Y );
				rect = System.Drawing.Rectangle.Intersect( ClipRect , rect );
				if( rect.Width > 0 || rect.Height > 0 )
				{
					e.Graphics.DrawImage( myImage , 
						new System.Drawing.Rectangle
						( rect.Left + sp.X , 
						rect.Top + sp.Y , 
						rect.Width ,
						rect.Height ) ,
						new System.Drawing.Rectangle( rect.Left - ip.X  , rect.Top - ip.Y  , rect.Width , rect.Height ) , 
						System.Drawing.GraphicsUnit.Pixel );
//					rect = this.ImageBounds ;
//					rect.Offset( - sp.X , - sp.Y );
//					if( this.Focused )
//						System.Windows.Forms.ControlPaint.DrawFocusRectangle( e.Graphics , rect );
				}
				//string strText = "宽:" + myImage.Size.Width + " 高:" + myImage.Size.Height + " 格式:" + myImage.PixelFormat ;
			}
            if( this.BehaviorStyle == XPictureBoxBehaviorStyle.DragSelect )
            {
                using (ReversibleDrawer drawer = new ReversibleDrawer(e.Graphics))
                {
                    drawer.PenColor = Color.White;
                    drawer.PenStyle = PenStyle.PS_SOLID;
                    drawer.LineWidth = 1;

                    Rectangle rect2 = this.SelectionBounds;
                    if (rect2.IsEmpty == false)
                    {
                        rect2.Offset( this.AutoScrollPosition.X , this.AutoScrollPosition.Y );
                        rect2.Offset(this.ImageBounds.Location);
                        drawer.DrawRectangle(rect2);
                    }
                    if (myLastPoint != null)
                    {
                        drawer.DrawLine(0, myLastPoint.Y, this.ClientSize.Width, myLastPoint.Y);
                        drawer.DrawLine(myLastPoint.X, 0, myLastPoint.X, this.ClientSize.Height);
                        myLastPoint = null;
                    }
                }
            }
			base.OnPaint (e);
		}
		private myPointClass myDragPoint = null;
        //protected bool bolEnableDragMove = true;
        ///// <summary>
        ///// 是否允许鼠标拖拽移动图片
        ///// </summary>
        //public bool EnableDragMode
        //{
        //    get
        //    {
        //        return bolEnableDragMove ;
        //    }
        //    set
        //    { 
        //        bolEnableDragMove = value;
        //        if( value == false )
        //            myDragPoint = null;
        //    }
        //}

        private ReversibleDrawer CreateReversibleDrawer()
        {
            ReversibleDrawer drawer = ReversibleDrawer.FromHwnd(this.Handle);
            drawer.PenColor = Color.White;
            drawer.PenStyle = PenStyle.PS_SOLID;
            drawer.LineWidth = 1;
            return drawer;
        }

        private Rectangle GetBounds(Point p1, Point p2)
        {
            Rectangle rect = Rectangle.Empty;
            rect.X = Math.Min(p1.X, p2.X);
            rect.Y = Math.Min(p1.Y, p2.Y);
            rect.Width = Math.Max(p1.X, p2.X) - rect.Left;
            rect.Height = Math.Max(p1.Y, p2.Y) - rect.Top;
            return rect;
        }



        private myPointClass myLastPoint = null;

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            myLastPoint = null;
            
            if (this.BehaviorStyle == XPictureBoxBehaviorStyle.DragScroll)
            {
                if (this.ImageBounds.Contains(e.X - this.AutoScrollPosition.X, e.Y - this.AutoScrollPosition.Y))
                {
                    myDragPoint = new myPointClass();
                    myDragPoint.X = e.X;
                    myDragPoint.Y = e.Y;
                }
            }
            else if( this.BehaviorStyle == XPictureBoxBehaviorStyle.DragSelect )
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    MouseCapturer cap = new MouseCapturer(this);
                    cap.ReversibleShape = ReversibleShapeStyle.Rectangle;
                    this.Cursor = Cursors.Cross;
                    this.Capture = true;
                    this.Refresh();
                    if (cap.CaptureMouseMove())
                    {
                        mySelectionBounds = GetBounds(cap.StartPosition, cap.EndPosition);
                        mySelectionBounds.Offset( - this.AutoScrollPosition.X,  - this.AutoScrollPosition.Y);
                        Point p = this.ImageBounds.Location;
                        mySelectionBounds.Offset(-p.X, -p.Y);
                        myLastPoint = null;
                        this.Refresh();
                    }
                    this.Cursor = Cursors.Default;
                    this.Capture = false;
                }
            }
            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            if (this.BehaviorStyle == XPictureBoxBehaviorStyle.DragScroll)
            {
                if (myDragPoint != null)
                {
                    System.Drawing.Point p = new System.Drawing.Point(
                        0 - this.AutoScrollPosition.X,
                        0 - this.AutoScrollPosition.Y);
                    p.Offset(-e.X + myDragPoint.X, -e.Y + myDragPoint.Y);
                    myDragPoint.X = e.X;
                    myDragPoint.Y = e.Y;
                    this.AutoScrollPosition = p;
                }
                else
                {
                    if (myImage != null)
                    {
                        int x = e.X - this.AutoScrollPosition.X;
                        int y = e.Y - this.AutoScrollPosition.Y;
                        if (myImage.Size.Width > this.ClientSize.Width || myImage.Size.Height > this.ClientSize.Height)
                        {
                            if (this.ImageBounds.Contains(x, y))
                                this.Cursor = System.Windows.Forms.Cursors.SizeAll;
                            else
                                this.Cursor = System.Windows.Forms.Cursors.Default;
                        }
                        else
                            this.Cursor = System.Windows.Forms.Cursors.Default;
                    }
                }
            }
            else if (this.BehaviorStyle == XPictureBoxBehaviorStyle.DragSelect)
            {
                using (ReversibleDrawer drawer = this.CreateReversibleDrawer())
                {
                    if (myLastPoint != null)
                    {
                        drawer.DrawLine(0, myLastPoint.Y, this.ClientSize.Width, myLastPoint.Y);
                        drawer.DrawLine(myLastPoint.X, 0, myLastPoint.X, this.ClientSize.Height);
                    }
                    else
                    {
                        myLastPoint = new myPointClass();
                    }
                    if (this.ImageBounds.Contains(e.X, e.Y))
                    {
                        myLastPoint.X = e.X;
                        myLastPoint.Y = e.Y;
                        drawer.DrawLine(0, myLastPoint.Y, this.ClientSize.Width, myLastPoint.Y);
                        drawer.DrawLine(myLastPoint.X, 0, myLastPoint.X, this.ClientSize.Height);
                    }
                    else
                    {
                        myLastPoint = null;
                    }
                }
            }
            base.OnMouseMove(e);
        }

		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
            myLastPoint = null;
            this.Capture = false;
			myDragPoint = null;
			base.OnMouseUp (e);
		}

        protected override void OnMouseLeave(EventArgs e)
        {
            if (myLastPoint != null)
            {
                using (ReversibleDrawer drawer = this.CreateReversibleDrawer())
                {
                    drawer.DrawLine(0, myLastPoint.Y, this.ClientSize.Width, myLastPoint.Y);
                    drawer.DrawLine(myLastPoint.X, 0, myLastPoint.X, this.ClientSize.Height);
                    myLastPoint = null;
                }
            }
            base.OnMouseLeave(e);
        }

		private class myPointClass
		{
			public int X = 0 ;
			public int Y = 0 ;
		}
	}

    /// <summary>
    /// 图片框动作样式
    /// </summary>
    public enum XPictureBoxBehaviorStyle
    {
        /// <summary>
        /// 无样式
        /// </summary>
        None ,
        /// <summary>
        /// 鼠标拖拽滚动
        /// </summary>
        DragScroll ,
        /// <summary>
        /// 鼠标拖拽选择
        /// </summary>
        DragSelect
    }
}