/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;

namespace DCSoft.WinForms
{
    /// <summary>
    /// 拖拽点样式
    /// </summary>
    public enum DragPointStyle
    {
        /// <summary>
        /// 无效拖拽点
        /// </summary>
        None = - 2 ,
        /// <summary>
        /// 进行移动的拖拽点，可改变图形位置
        /// </summary>
        Move = -1,
        /// <summary>
        /// 左上角拖拽点
        /// </summary>
        LeftTop = 0 ,
        /// <summary>
        /// 上边中央拖拽点
        /// </summary>
        TopCenter = 1,
        /// <summary>
        /// 右上角拖拽点
        /// </summary>
        TopRight = 2,
        /// <summary>
        /// 右边中央拖拽点
        /// </summary>
        RightCenter = 3,
        /// <summary>
        /// 右下角拖拽点
        /// </summary>
        RightBottom = 4,
        /// <summary>
        /// 底边中央拖拽点
        /// </summary>
        BottomCenter = 5,
        /// <summary>
        /// 左下角拖拽点
        /// </summary>
        LeftBottom = 6,
        /// <summary>
        /// 左边中央拖拽点
        /// </summary>
        LeftCenter = 7
    }
	/// <summary>
	/// 绘制和辅助处理8个点的控制矩形的对象
	/// </summary>
	public class DragRectangle
	{
		/// <summary>
		/// 无作为的初始化对象
		/// </summary>
		public DragRectangle()
		{
		}
		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="vBounds">矩形对象</param>
		/// <param name="vInnerRect">是否是内置拖拽控制点</param>
		public DragRectangle( System.Drawing.Rectangle vBounds , bool vInnerRect )
		{
			myBounds = vBounds ;
			bolInnerDragRect = vInnerRect ;
			this.Refresh();
		}

		private System.Drawing.Rectangle myBounds = System.Drawing.Rectangle.Empty ;
		private System.Drawing.Rectangle[] myDragRect = new System.Drawing.Rectangle[8];
		/// <summary>
		/// 拖拽控制点的大小
		/// </summary>
		public static int DragRectSize = 6 ;
		private bool bolInnerDragRect = false;
		
		private bool bolCanResize = true;
		private bool bolCanMove = true;
		private bool bolFocus = true;
		private bool bolBoundsBorder = true;

		

		/// <summary>
		/// 是否显示对象边框
		/// </summary>
		public bool BoundsBorder
		{
			get{ return bolBoundsBorder ;}
			set{ bolBoundsBorder = value;}
		}
		/// <summary>
		/// 是否获得焦点(当前对象)
		/// </summary>
		public bool Focus
		{
			get{ return bolFocus ;}
			set{ bolFocus = value;}
		}

		/// <summary>
		/// 是否是内置拖拉矩形
		/// </summary>
		public bool InnerDragRect
		{
			get{ return bolInnerDragRect ;}
			set{ bolInnerDragRect  =value;}
		}

		/// <summary>
		/// 对象边框
		/// </summary>
		public System.Drawing.Rectangle Bounds
		{
			get{ return myBounds;}
			set{ myBounds = value; this.Refresh() ;}
		}
		/// <summary>
		/// 对象的拖拉举行
		/// </summary>
		public System.Drawing.Rectangle[] DragRect
		{
			get{ return myDragRect;}
		}
		/// <summary>
		/// 是否能改变大小
		/// </summary>
		public bool CanResize
		{
			get{ return bolCanResize;}
			set{ bolCanResize = value;}
		}
		/// <summary>
		/// 是否能移动对象
		/// </summary>
		public bool CanMove
		{
			get{ return bolCanMove ;}
			set{ bolCanMove = value;}
		}
		private bool bolCanReWidth = true;
		/// <summary>
		/// 能否改变宽度
		/// </summary>
		public bool CanReWidth
		{
			get{ return bolCanReWidth ;}
			set{ bolCanReWidth = value;}
		}
		private bool bolCanReHeight = true;
		/// <summary>
		/// 能否改变高度
		/// </summary>
		public bool CanReHeight
		{
			get{ return bolCanReHeight;}
			set{ bolCanReHeight = value;}
		}
		/// <summary>
		/// 控制点矩形的填充色
		/// </summary>
		public System.Drawing.Color DragRectBackColor
		{
			get
			{
                if (bolFocus)
                {
                    if (this.bolCanResize)
                        return System.Drawing.Color.Blue;
                    else
                        return System.Drawing.Color.Black;
                }
                else
                {
                    return System.Drawing.Color.White;
                }
			}
		}
		/// <summary>
		/// 控制点矩形的边框颜色
		/// </summary>
		public System.Drawing.Color DragRectBorderColor
		{
			get
			{
				if( bolFocus )
					return System.Drawing.Color.LightGray ;
				else
				{
					if( this.bolCanResize )
						return System.Drawing.Color.Blue ;
					else
						return System.Drawing.Color.Black ;
				}
			}
		}

		/// <summary>
		/// 计算指定矩形的拖拽控制矩形
		/// </summary>
		/// <remarks>
		/// 拖拽矩形主要用于有用户参与的图形化用户界面,在一个矩形区域的的4个顶
		/// 点和边框中间点共有8个控制点,用户使用鼠标拖拽操作来拖动这8个控制点
		/// 可以用于改变矩形区域的位置和大小,这些控制点可以在区域区域的内部,
		/// 也可在矩形区域的外部,拖拽矩形有8个,分别编号从0至7
		/// <pre>
		///               内拖拽矩形
		/// ┌─────────────────┐
		/// │■0            1■             2■│
		/// │                                  │
		/// │                                  │
		/// │                                  │
		/// │                                  │
		/// │■7                            3■│
		/// │                                  │
		/// │                                  │
		/// │                                  │
		/// │                                  │
		/// │■6           5■              4■│
		/// └─────────────────┘
		/// 
		///              外拖拽矩形
		///              
		/// ■               ■                  ■
		///   ┌────────────────┐
		///   │0            1                 2│
		///   │                                │
		///   │                                │
		///   │                                │
		///   │                                │
		/// ■│7                              3│■ 
		///   │                                │
		///   │                                │
		///   │                                │
		///   │                                │
		///   │6             5               4 │
		///   └────────────────┘
		/// ■                ■                 ■
		/// </pre>
		/// </remarks>
		public void Refresh()
		{
			if( bolInnerDragRect)
			{
				myDragRect[0] = new System.Drawing.Rectangle( 
					myBounds.X ,
					myBounds.Y , 
					DragRectSize ,
					DragRectSize );
				myDragRect[1] = new System.Drawing.Rectangle( 
					myBounds.X + (int)((myBounds.Width - DragRectSize)/2) , 
					myBounds.Y , 
					DragRectSize ,
					DragRectSize );
				myDragRect[2] = new System.Drawing.Rectangle( 
					myBounds.Right - DragRectSize , 
					myBounds.Y , 
					DragRectSize ,
					DragRectSize );
				myDragRect[3] = new System.Drawing.Rectangle(
					myBounds.Right - DragRectSize , 
					myBounds.Y + (int)(( myBounds.Height - DragRectSize)/2)  , 
					DragRectSize , 
					DragRectSize );
				myDragRect[4] = new System.Drawing.Rectangle( 
					myBounds.Right - DragRectSize ,
					myBounds.Bottom - DragRectSize , 
					DragRectSize ,
					DragRectSize );
				myDragRect[5] = new System.Drawing.Rectangle( 
					myBounds.X + (int)((myBounds.Width - DragRectSize)/2) ,
					myBounds.Bottom - DragRectSize ,
					DragRectSize ,
					DragRectSize );
				myDragRect[6] = new System.Drawing.Rectangle( 
					myBounds.X  , 
					myBounds.Bottom - DragRectSize , 
					DragRectSize ,
					DragRectSize );
				myDragRect[7] = new System.Drawing.Rectangle(
					myBounds.X  , 
					myBounds.Y + (int)(( myBounds.Height - DragRectSize)/2 ) , 
					DragRectSize ,
					DragRectSize );
			}
			else
			{
				myDragRect[0] = new System.Drawing.Rectangle( 
					myBounds.X - DragRectSize ,
					myBounds.Y - DragRectSize ,
					DragRectSize , 
					DragRectSize );
				myDragRect[1] = new System.Drawing.Rectangle(
					myBounds.X + (int)((myBounds.Width - DragRectSize)/2) , 
					myBounds.Y - DragRectSize , 
					DragRectSize ,
					DragRectSize );
				myDragRect[2] = new System.Drawing.Rectangle( 
					myBounds.Right  ,
					myBounds.Y - DragRectSize ,
					DragRectSize ,
					DragRectSize );
				myDragRect[3] = new System.Drawing.Rectangle(
					myBounds.Right  ,
					myBounds.Y + (int)(( myBounds.Height - DragRectSize)/2)  , 
					DragRectSize , 
					DragRectSize );
				myDragRect[4] = new System.Drawing.Rectangle( 
					myBounds.Right  , 
					myBounds.Bottom  ,
					DragRectSize ,
					DragRectSize );
				myDragRect[5] = new System.Drawing.Rectangle( 
					myBounds.X + (int)((myBounds.Width - DragRectSize)/2) ,
					myBounds.Bottom  ,
					DragRectSize ,
					DragRectSize );
				myDragRect[6] = new System.Drawing.Rectangle( 
					myBounds.X - DragRectSize ,
					myBounds.Bottom  ,
					DragRectSize ,
					DragRectSize );
				myDragRect[7] = new System.Drawing.Rectangle( 
					myBounds.X - DragRectSize  ,
					myBounds.Y + (int)(( myBounds.Height - DragRectSize)/2 ) , 
					DragRectSize , 
					DragRectSize );
			}
		}//public void Refresh()

		/// <summary>
		/// 绘制拖拉矩形
		/// </summary>
		/// <param name="g">绘制对象</param>
		/// <param name="Rect">矩形区域</param>
		/// <param name="vFocus">是否是焦点绘制模式</param>
		public static void DrawDragRect( 
			System.Drawing.Graphics g , 
			System.Drawing.Rectangle Rect , 
			bool vFocus ,
			bool CanResize )
		{
			System.Drawing.Color color = System.Drawing.Color.Blue ;
			if( vFocus )
			{
				if( CanResize )
					color = System.Drawing.Color.Blue ;
				else
					color = System.Drawing.Color.Black ;
			}
			else
				color = System.Drawing.Color.White ;
			using( System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush( color ))
			{
				g.FillRectangle( myBrush , Rect );
			}
			if( vFocus )
				color = System.Drawing.Color.White ;
			else
			{
				if( CanResize )
					 color = System.Drawing.Color.Blue ;
				 else
					 color = System.Drawing.Color.Black ;
			}
			using( System.Drawing.Pen myPen = new System.Drawing.Pen ( color ))
			{
				g.DrawRectangle( myPen , Rect );
			}
		}
		/// <summary>
		/// 根据指定的位置获得控制矩形区域,指定的位置将是控制矩形区域的中心
		/// </summary>
		/// <param name="p">指定的位置</param>
		/// <returns>控制矩形区域</returns>
		public static System.Drawing.Rectangle GetDragRect( System.Drawing.Point p )
		{
			return new System.Drawing.Rectangle(
				p.X - DragRectSize / 2 ,
				p.Y - DragRectSize /2 , 
				DragRectSize , 
				DragRectSize );
		}

        private System.Drawing.Drawing2D.DashStyle _LineDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        /// <summary>
        /// 边框线虚线样式
        /// </summary>
        public System.Drawing.Drawing2D.DashStyle LineDashStyle
        {
            get { return _LineDashStyle; }
            set { _LineDashStyle = value; }
        }
		/// <summary>
		/// 绘制拖拽矩形,本函数根据主矩形区域计算8个拖拽矩形区域并用指定的颜色
		/// 填充和绘制边框,本函数不绘制主矩形区域
		/// </summary>
        /// <param name="myGraph">图像绘制对象</param>
        public void RefreshView(System.Drawing.Graphics myGraph)
        {
            if (myGraph != null)
            {
                if (bolBoundsBorder)
                {
                    using (System.Drawing.Pen myPen =
                               new System.Drawing.Pen(System.Drawing.Color.Black))
                    {
                        myPen.DashStyle = this.LineDashStyle;
                        myGraph.DrawRectangle(myPen, myBounds);
                    }
                }
                using (System.Drawing.SolidBrush myBrush =
                           new System.Drawing.SolidBrush(this.DragRectBackColor))
                {
                    myGraph.FillRectangles(myBrush, this.myDragRect);
                }
                using (System.Drawing.Pen myPen =
                          new System.Drawing.Pen(this.DragRectBorderColor))
                {
                    myGraph.DrawRectangles(myPen, myDragRect);
                }
            }
        }// void DrawDragRect()

		/// <summary>
		/// 判断指定拖拽点是否可用
		/// </summary>
		/// <param name="point">拖拽点样式</param>
		/// <returns>是否可用</returns>
		private bool DragRectEnable( DragPointStyle point )
		{
			switch( point )
			{
				case  DragPointStyle.Move :
					return bolCanMove ;
				case  DragPointStyle.LeftTop :
					return bolCanMove && bolCanResize ;
				case DragPointStyle.TopCenter :
					return bolCanMove && bolCanResize ;
				case DragPointStyle.TopRight :
					return bolCanMove && bolCanResize ;
				case  DragPointStyle.RightCenter :
					return bolCanResize ;
				case DragPointStyle.RightBottom :
					return bolCanResize ;
				case DragPointStyle.BottomCenter :
					return bolCanResize ;
				case DragPointStyle.LeftBottom :
					return bolCanMove && bolCanResize ;
				case DragPointStyle.LeftCenter :
					return bolCanMove && bolCanResize;
				default:
					return false;
			}
		}//bool DragRectEnable()
		
		/// <summary>
		/// 计算指定坐标在对象中的区域 -1:在对象区域中,0-7:在对象的某个拖拉矩形中,-2:不在对象中或不需要进行拖拽操作
		/// </summary>
		/// <param name="x">X坐标</param>
		/// <param name="y">Y坐标</param>
		/// <returns>控制号</returns>
        public DragPointStyle DragHit(int x, int y)
        {
            if (bolCanResize == false && myBounds.Contains(x, y))
                return DragPointStyle.Move;
            for (int iCount = 0; iCount < 8; iCount++)
            {
                if (myDragRect[iCount].Contains(x, y))
                {
                    if (this.DragRectEnable((DragPointStyle)iCount))
                        return (DragPointStyle)iCount;
                    else
                        return DragPointStyle.None;
                }
            }
            if (myBounds.Contains(x, y) && bolCanMove)
            {
                return DragPointStyle.Move;
            }
            else
            {
                return DragPointStyle.None;
            }
        }

		/// <summary>
		/// 移动矩形
		/// </summary>
		/// <param name="dx">横向移动量</param>
		/// <param name="dy">纵向移动量</param>
		/// <param name="DragStyle">移动方式</param>
		/// <param name="SourceRect">原始矩形</param>
		/// <returns>移动后的矩形</returns>
		public static System.Drawing.Rectangle CalcuteDragRectangle( 
			int dx , 
			int dy , 
			DragPointStyle DragStyle ,
			System.Drawing.Rectangle SourceRect )
		{
			// 中间
			if(DragStyle == DragPointStyle.Move )
				SourceRect.Offset(dx,dy);
			// 左边
			if(DragStyle == DragPointStyle.LeftTop 
                || DragStyle == DragPointStyle.LeftCenter
                || DragStyle == DragPointStyle.LeftBottom )
			{
				SourceRect.Offset(dx,0);
				SourceRect.Width = SourceRect.Width - dx;
			}
			// 顶边
			if(DragStyle == DragPointStyle.LeftTop
                || DragStyle == DragPointStyle.TopCenter
                || DragStyle == DragPointStyle.TopRight )
			{
				SourceRect.Offset(0,dy);
				SourceRect.Height = SourceRect.Height -dy;
			}
			// 右边
			if(DragStyle == DragPointStyle.TopRight
                || DragStyle == DragPointStyle.RightCenter
                || DragStyle == DragPointStyle .RightBottom )
			{
				SourceRect.Width = SourceRect.Width + dx;
			}
			// 底边
			if(DragStyle == DragPointStyle.RightBottom
                || DragStyle == DragPointStyle.BottomCenter 
                || DragStyle == DragPointStyle.LeftBottom )
			{
				SourceRect.Height = SourceRect.Height + dy;
			}
			return SourceRect ;
		}

		/// <summary>
		/// 计算指定矩形的拖拽控制矩形
		/// </summary>
		/// <param name="myRect">主矩形区域</param>
		/// <param name="DragRectSize">拖拽矩形的大小</param>
		/// <param name="InnerDragRect">
		/// 拖拽矩形是否在主矩形内部,若为false则拖拽矩形外翻
		/// </param>
		/// <remarks>
		/// 拖拽矩形主要用于有用户参与的图形化用户界面,在一个矩形区域的的4个顶点
		/// 和边框中间点共有8个控制点,用户使用鼠标拖拽操作来拖动这8个控制点可以用
		/// 于改变矩形区域的位置和大小,这些控制点可以在区域区域的内部,也可在矩形
		/// 区域的外部,拖拽矩形有8个,分别编号从0至7
		/// <pre>
		///               内拖拽矩形
		/// ┌─────────────────┐
		/// │■0            1■             2■│
		/// │                                  │
		/// │                                  │
		/// │                                  │
		/// │                                  │
		/// │■7                            3■│
		/// │                                  │
		/// │                                  │
		/// │                                  │
		/// │                                  │
		/// │■6           5■              4■│
		/// └─────────────────┘
		/// 
		///              外拖拽矩形
		///              
		/// ■               ■                  ■
		///   ┌────────────────┐
		///   │0            1                 2│
		///   │                                │
		///   │                                │
		///   │                                │
		///   │                                │
		/// ■│7                              3│■ 
		///   │                                │
		///   │                                │
		///   │                                │
		///   │                                │
		///   │6             5               4 │
		///   └────────────────┘
		/// ■                ■                 ■
		/// </pre>
		/// </remarks>
		/// <returns>拖拽矩形的数组,有8个元素</returns>
		public static System.Drawing.Rectangle[] GetDragRects(
			System.Drawing.Rectangle myRect , 
			int DragRectSize ,
			bool InnerDragRect)
		{
			System.Drawing.Rectangle[] DragRects = new System.Drawing.Rectangle[8];
			if( InnerDragRect)
			{
				DragRects[0] = new System.Drawing.Rectangle(
					myRect.X , 
					myRect.Y , 
					DragRectSize , 
					DragRectSize );
				DragRects[1] = new System.Drawing.Rectangle(
					myRect.X + (int)((myRect.Width - DragRectSize)/2) ,
					myRect.Y ,
					DragRectSize , 
					DragRectSize );
				DragRects[2] = new System.Drawing.Rectangle( 
					myRect.Right - DragRectSize ,
					myRect.Y , 
					DragRectSize ,
					DragRectSize );
				DragRects[3] = new System.Drawing.Rectangle( 
					myRect.Right - DragRectSize ,
					myRect.Y + (int)(( myRect.Height - DragRectSize)/2)  ,
					DragRectSize , 
					DragRectSize );
				DragRects[4] = new System.Drawing.Rectangle(
					myRect.Right - DragRectSize ,
					myRect.Bottom - DragRectSize ,
					DragRectSize , 
					DragRectSize );
				DragRects[5] = new System.Drawing.Rectangle(
					myRect.X + (int)((myRect.Width - DragRectSize)/2) ,
					myRect.Bottom - DragRectSize ,
					DragRectSize , 
					DragRectSize );
				DragRects[6] = new System.Drawing.Rectangle( 
					myRect.X  ,
					myRect.Bottom - DragRectSize , 
					DragRectSize , 
					DragRectSize );
				DragRects[7] = new System.Drawing.Rectangle( 
					myRect.X  , 
					myRect.Y + (int)(( myRect.Height - DragRectSize)/2 ) , 
					DragRectSize , 
					DragRectSize );
			}
			else
			{
				DragRects[0] = new System.Drawing.Rectangle( 
					myRect.X - DragRectSize , 
					myRect.Y - DragRectSize ,
					DragRectSize ,
					DragRectSize );
				DragRects[1] = new System.Drawing.Rectangle(
					myRect.X + (int)((myRect.Width - DragRectSize)/2) ,
					myRect.Y - DragRectSize ,
					DragRectSize , 
					DragRectSize );
				DragRects[2] = new System.Drawing.Rectangle( 
					myRect.Right  ,
					myRect.Y - DragRectSize , 
					DragRectSize , 
					DragRectSize );
				DragRects[3] = new System.Drawing.Rectangle( 
					myRect.Right  ,
					myRect.Y + (int)(( myRect.Height - DragRectSize)/2)  ,
					DragRectSize ,
					DragRectSize );
				DragRects[4] = new System.Drawing.Rectangle( 
					myRect.Right  ,
					myRect.Bottom  , 
					DragRectSize , 
					DragRectSize );
				DragRects[5] = new System.Drawing.Rectangle( 
					myRect.X + (int)((myRect.Width - DragRectSize)/2) , 
					myRect.Bottom  , 
					DragRectSize ,
					DragRectSize );
				DragRects[6] = new System.Drawing.Rectangle(
					myRect.X - DragRectSize , 
					myRect.Bottom  ,
					DragRectSize ,
					DragRectSize );
				DragRects[7] = new System.Drawing.Rectangle(
					myRect.X - DragRectSize  , 
					myRect.Y + (int)(( myRect.Height - DragRectSize)/2 ) ,
					DragRectSize ,
					DragRectSize );
			}
			return DragRects ;
		}

		/// <summary>
		/// 计算拖拉矩形上的鼠标光标位置
		/// </summary>
		/// <remarks>
		/// 鼠标设置如下
		/// 西北-东南          南北                东北-西南
		///	   ■               ■                  ■
		///     ┌────────────────┐
		///     │0            1                 2│
		///     │                                │
		///     │                                │
		///     │                                │
		///     │                                │
		///   ■│7 西-南                        3│■ 西-南
		///     │                                │
		///     │                                │
		///     │                                │
		///     │                                │
		///     │6             5               4 │
		///     └────────────────┘
		///   ■                ■                 ■
		/// 东北-西南          南北                   西北-东南
		/// </remarks>
		/// <param name="point">拖拽矩形的序号,从0至7</param>
		/// <returns>鼠标光标对象,若序号小于0或大于7则返回空引用</returns>
		public static System.Windows.Forms.Cursor GetMouseCursor( DragPointStyle point )
		{
			switch( point )
			{
				case DragPointStyle.Move : 
					return System.Windows.Forms.Cursors.Arrow ;
				case DragPointStyle.LeftTop :
					return System.Windows.Forms.Cursors.SizeNWSE ;
				case  DragPointStyle.TopCenter :
					return System.Windows.Forms.Cursors.SizeNS ;
				case DragPointStyle.TopRight :
					return System.Windows.Forms.Cursors.SizeNESW ;
				case DragPointStyle.RightCenter :
					return System.Windows.Forms.Cursors.SizeWE ;
				case DragPointStyle.RightBottom :
					return System.Windows.Forms.Cursors.SizeNWSE ;
				case  DragPointStyle.BottomCenter :
					return System.Windows.Forms.Cursors.SizeNS ;
				case DragPointStyle.LeftBottom :
					return System.Windows.Forms.Cursors.SizeNESW ;
				case DragPointStyle.LeftCenter :
					return System.Windows.Forms.Cursors.SizeWE ;
				default:
					return System.Windows.Forms.Cursors.Default ;
			}
			//return null;
		}
	}//public class DragRectangle
}