/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing ;
using DCSoft.Printing ;
using DCSoft.Drawing;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 绘制文档内容的委托类型
    /// </summary>
    /// <param name="sender">参数</param>
    /// <param name="args">参数</param>
    public delegate void DocumentPaintEventHandler(
        object sender ,
        DocumentPaintEventArgs args );

    public class DocumentPaintEventArgs : EventArgs , ICloneable
    {
        /// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="g">绘图对象</param>
		/// <param name="clipRectangle">剪切矩形</param>
		public DocumentPaintEventArgs(
            System.Drawing.Graphics  g ,
            System.Drawing.Rectangle clipRectangle)
		{
             
			this.myGraphics = g ;
			this.myClipRectangle = clipRectangle ;
			this.myViewBounds = clipRectangle ;
			this.myDrawRectangles = new System.Drawing.Rectangle[]{ clipRectangle };
		}
         
        private DomDocument myDocument = null;
        /// <summary>
        /// 相关的文档对象
        /// </summary>
        public DomDocument Document
        {
            get
            {
                return myDocument;
            }
            set
            {
                myDocument = value;
            }
        }

        private DomDocumentContentElement _DocumentContentElement = null;
        /// <summary>
        /// 内容元素对象
        /// </summary>
        public DomDocumentContentElement DocumentContentElement
        {
            get { return _DocumentContentElement; }
            set { _DocumentContentElement = value; }
        }
        private bool _ActiveMode = true;
        /// <summary>
        /// 要绘制的内容处于激活模式
        /// </summary>
        public bool ActiveMode
        {
            get 
            {
                return _ActiveMode; 
            }
            set
            {
                _ActiveMode = value; 
            }
        }
        private DomElement myElement = null;
        /// <summary>
        /// 相关的文档元素对象
        /// </summary>
        public DomElement Element
        {
            get
            {
                return myElement;
            }
            set
            {
                myElement = value;
            }
        }


        private PageContentPartyStyle intType = PageContentPartyStyle.Body ;
        /// <summary>
        /// 文档内容类型
        /// </summary>
        public PageContentPartyStyle Type
        {
            get
            {
                return intType;
            }
            set
            {
                intType = value ;
            }
        }

        private DocumentContentStyle _Style = null;
        /// <summary>
        /// 绘制文档内容使用的样式
        /// </summary>
        public DocumentContentStyle Style
        {
            get
            {
                return _Style;
            }
            set
            {
                _Style = value ;
            }
        }

        private bool _Cancel = false;
        /// <summary>
        /// 用户取消操作
        /// </summary>
        public bool Cancel
        {
            get
            {
                return _Cancel; 
            }
            set
            {
                _Cancel = value; 
            }
        }

        private DocumentContentRender _Render = null;
        /// <summary>
        /// 图形内容呈现器
        /// </summary>
        public DocumentContentRender Render
        {
            get
            {
                return _Render; 
            }
            set
            {
                _Render = value; 
            }
        }

        private DocumentRenderStyle intRenderStyle = DocumentRenderStyle.Paint;
        /// <summary>
        /// 正在呈现的文档样式
        /// </summary>
        public DocumentRenderStyle RenderStyle
        {
            get
            {
                return intRenderStyle; 
            }
            set
            {
                intRenderStyle = value; 
            }
        }

		private bool bolForCreateImage = false;
		/// <summary>
		/// 正在为创建图片而绘制图形
		/// </summary>
		public bool ForCreateImage
		{
			get
			{
				return bolForCreateImage ;
			}
			set
			{
				bolForCreateImage = value;
			}
		}

		/// <summary>
		/// 绘图对象
		/// </summary>
		protected System.Drawing.Graphics myGraphics = null;
		/// <summary>
		/// 绘图对象
		/// </summary>
		public System.Drawing.Graphics Graphics
		{
			get
            {
                return myGraphics;
            }
			set
            {
                myGraphics = value;
            }
		}

		private int[] intPageLinePositions = null;
		/// <summary>
		/// 分页线位置
		/// </summary>
		public int[] PageLinePositions
		{
			get
			{
				return intPageLinePositions ;
			}
			set
			{
				intPageLinePositions = value;
			}
		}
		
		private bool MatchPageLine( int pos )
		{
			if( intPageLinePositions != null )
			{
				for( int iCount = intPageLinePositions.Length - 1 ; iCount >= 0 ; iCount -- )
				{
					if( intPageLinePositions[ iCount ] == pos )
					{
						return true ;
					}
				}
			}
			return false;
		}

        private System.Drawing.Rectangle myPageClipRectangle
            = System.Drawing.Rectangle.Empty;
        /// <summary>
        /// 页面剪切矩形
        /// </summary>
        public System.Drawing.Rectangle PageClipRectangle
        {
            get
            {
                return myPageClipRectangle; 
            }
            set
            {
                myPageClipRectangle = value; 
            }
        }


		/// <summary>
		/// 剪切矩形
		/// </summary>
		protected System.Drawing.Rectangle myClipRectangle 
            = System.Drawing.Rectangle.Empty ;
		/// <summary>
		/// 剪切矩形
		/// </summary>
		public System.Drawing.Rectangle ClipRectangle
		{
			get
            {
                return myClipRectangle ;
            }
			set
            {
                myClipRectangle = value;
            }
		}


        /// <summary>
        /// 浮点数剪切矩形
        /// </summary>
        public RectangleF ClipRectangleF
        {
            get
            {
                return new RectangleF(
                    myClipRectangle.Left,
                    myClipRectangle.Top,
                    myClipRectangle.Width,
                    myClipRectangle.Height);
            }
        }


        private System.Drawing.RectangleF _Bounds = RectangleF.Empty;

        public System.Drawing.RectangleF Bounds
        {
            get { return _Bounds; }
            set { _Bounds = value; }
        }
		/// <summary>
		/// 绘图区域的矩形数组
		/// </summary>
		protected System.Drawing.Rectangle[] myDrawRectangles = null;
		/// <summary>
		/// 绘图区域的矩形数组
		/// </summary>
		public System.Drawing.Rectangle[] DrawRectangles
		{
			get
            {
                return myDrawRectangles;
            }
			set
            {
                myDrawRectangles = value;
            }
		}

		protected float fScaleRate = 1.0f ;
		/// <summary>
		/// 缩放比率
		/// </summary>
		public float ScaleRate
		{
			get
            {
                return fScaleRate ;
            }
			set
            {
                fScaleRate = value;
            }
		}

        //private bool bolCancel = false;
        ///// <summary>
        ///// 取消后续操作
        ///// </summary>
        //public bool Cancel
        //{
        //    get
        //    {
        //        return bolCancel;
        //    }
        //    set
        //    {
        //        bolCancel = value;
        //    }
        //}

		/// <summary>
		/// 判断指定矩形是否和绘图区域相交
		/// </summary>
		/// <param name="rect">表示指定区域的矩形</param>
		/// <returns>指定区域是否和绘图区域相交</returns>
		public bool IntersectsWithDrawRects( System.Drawing.Rectangle rect )
		{
			if( myDrawRectangles != null)
			{
				for(int iCount = 0 ; iCount < myDrawRectangles.Length ; iCount ++)
					if( myDrawRectangles[iCount].IntersectsWith( rect ))
						return true;
			}
			return false;
		}

		public bool IntersectsWithDrawRects( int y1 , int y2 )
		{
			System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
				myViewBounds.Left ,
				y1 ,
				myViewBounds.Width , 
				y2 - y1 );
			return IntersectsWithDrawRects( rect );
		}

		/// <summary>
		/// 填充需要绘图的区域
		/// </summary>
		/// <param name="b">填充使用的画刷对象</param>
		public void FillDrawRects( System.Drawing.Brush b )
		{
			FillDrawRects( b , this.myViewBounds );
		}
		/// <summary>
		/// 填充需要绘图的区域
		/// </summary>
		/// <param name="b">填充使用的画刷对象</param>
		/// <param name="rect">要填充的区域</param>
		public void FillDrawRects( System.Drawing.Brush b , System.Drawing.Rectangle rect )
		{
			if( myDrawRectangles != null && myGraphics != null && b != null)
			{
				for(int iCount = 0 ; iCount < myDrawRectangles.Length ; iCount ++)
				{
					System.Drawing.Rectangle r = System.Drawing.Rectangle.Intersect( myDrawRectangles[iCount] , rect );
					if( InvalidSize( r.Width , r.Height ))// r.Width >= 0 && r.Height >= 0 )
					{
						if( r.Width == 0 )
						{
							r.Width = 1 ;
						}
						if( r.Height == 0 )
						{
							r.Height = 1 ;
						}
						myGraphics.FillRectangle( b , r );
					}
						//myGraphics.FillRectangle( b , r.Left , r.Top , r.Width + 1 , r.Height + 1 );
				}
			}
		}

		/// <summary>
		/// 判断指定大小的区域是否表示有效的大小
		/// </summary>
		/// <param name="width">区域宽度</param>
		/// <param name="height">区域高度</param>
		/// <returns>是否表示有效大小</returns>
		private bool InvalidSize( int width , int height )
		{
			if( width > 0 && height >= 0 )
				return true;
			if( height > 0 && width >= 0 )
				return true;
			return false;
		}
		/// <summary>
		/// 绘制边框
		/// </summary>
		/// <param name="p">绘制边框使用的画笔对象</param>
		public void DrawDrawRects( System.Drawing.Pen p )
		{
			DrawDrawRects( p , this.myViewBounds , true , true , true , true );
		}
		/// <summary>
		/// 绘制边框
		/// </summary>
		/// <param name="p">绘制边框使用的画笔对象</param>
		/// <param name="rect">边框的矩形对象</param>
		/// <param name="LeftBorder">是否绘制左边框</param>
		/// <param name="TopBorder">是否绘制顶边框</param>
		/// <param name="RightBorder">是否绘制右边框</param>
		/// <param name="BottomBorder">是否绘制底边框</param>
		public void DrawDrawRects( 
			System.Drawing.Pen p , 
			System.Drawing.Rectangle rect , 
			bool LeftBorder , 
			bool TopBorder , 
			bool RightBorder , 
			bool BottomBorder )
		{
			if( myDrawRectangles != null && myGraphics != null && p != null )
			{
				for(int iCount = 0 ; iCount < myDrawRectangles.Length ; iCount ++)
				{
					System.Drawing.Rectangle r = System.Drawing.Rectangle.Intersect( myDrawRectangles[iCount] , rect );
					if( InvalidSize( r.Width , r.Height ))
					{
						if( LeftBorder )
						{
							if( r.Left == rect.Left )
								myGraphics.DrawLine( p , r.Left , r.Top , r.Left , r.Bottom );
						}
						if( TopBorder )
						{
							int top = r.Top ;
//							if( FixPageLine && intPageLinePosition > top && intPageLinePosition < r.Bottom )
//							{
//								top = intPageLinePosition ;
//							}
							if( top == rect.Top || MatchPageLine( top ))
							{
								myGraphics.DrawLine( p , r.Left , top , r.Right , top );
							}
						}
						if( RightBorder )
						{
							if( r.Right == rect.Right )
								myGraphics.DrawLine( p , r.Right , r.Top  , r.Right , r.Bottom );
						}
						if( BottomBorder )
						{
							int bottom = r.Bottom ;
//							if( FixPageLine && intPageLinePosition < bottom && intPageLinePosition > r.Top )
//							{
//								bottom = intPageLinePosition ;
//							}
							if( bottom == rect.Bottom || MatchPageLine( bottom ))
							{
								myGraphics.DrawLine( p , r.Left , bottom , r.Right , bottom );
							}
						}
					}
				}
			}
		}
		/// <summary>
		/// 对象的区域
		/// </summary>
		protected System.Drawing.Rectangle myViewBounds 
            = System.Drawing.Rectangle.Empty ;
		/// <summary>
		/// 对象的区域
		/// </summary>
		public System.Drawing.Rectangle ViewBounds
		{
			get
            {
                return myViewBounds;
            }
			set
            {
                myViewBounds = value;
            }
		}
		/// <summary>
		/// 对象区域
		/// </summary>
		public System.Drawing.RectangleF ViewBoundsF
		{
			get
			{ 
				return new System.Drawing.RectangleF(
					 myViewBounds.Left , 
					 myViewBounds.Top ,
					 myViewBounds.Width ,
					 myViewBounds.Height );
			}
		}
		/// <summary>
		/// 复制对象的一个复本
		/// </summary>
		/// <returns>复制的对象</returns>
		object System.ICloneable.Clone()
		{
			DocumentPaintEventArgs a = new DocumentPaintEventArgs( this.myGraphics , this.myViewBounds );
		    a._Render = this._Render;
            a._ActiveMode = this._ActiveMode;
            a._Bounds = this._Bounds;
            a._Cancel = this._Cancel;
            a._DocumentContentElement = this._DocumentContentElement;
            a._Render = this._Render;
            a._Style = this._Style;
            a.bolForCreateImage = this.bolForCreateImage;
            a.fScaleRate = this.fScaleRate;
            a.intPageLinePositions = this.intPageLinePositions;
            a.intRenderStyle = this.intRenderStyle;
            a.intType = this.intType;
            a.myClipRectangle = this.myClipRectangle;
            a.myDocument = this.myDocument;
            a.myDrawRectangles = this.myDrawRectangles;
            a.myElement = this.myElement;
            a.myGraphics = this.myGraphics;
            a.myPageClipRectangle = this.myPageClipRectangle;
            a.myViewBounds = this.myViewBounds;
           
            if( this.myDrawRectangles != null)
			{
				a.myDrawRectangles = ( System.Drawing.Rectangle[]) myDrawRectangles.Clone();
			}
            //a.bolCancel = this.bolCancel;
			return a ;
		}

        /// <summary>
        /// 复制对象的一个复本
        /// </summary>
        /// <returns>复制的对象</returns>
        public DocumentPaintEventArgs Clone()
        {
            return (DocumentPaintEventArgs)((ICloneable)this).Clone();
        }
    }

    ///// <summary>
    ///// 文档内容类型
    ///// </summary>
    //public enum DocumentContentType
    //{
    //    /// <summary>
    //    /// 无样式
    //    /// </summary>
    //    None,
    //    /// <summary>
    //    /// 文档正文
    //    /// </summary>
    //    Body,
    //    /// <summary>
    //    /// 页眉
    //    /// </summary>
    //    Header,
    //    /// <summary>
    //    /// 页脚
    //    /// </summary>
    //    Footer
    //}

    /// <summary>
    /// 正在呈现的的文档样式
    /// </summary>
    public enum DocumentRenderStyle
    {
        /// <summary>
        /// 正在WinForm用户界面上绘制图形
        /// </summary>
        Paint ,
        /// <summary>
        /// 正在创建包含对象内容的位图
        /// </summary>
        Bitmap ,
        /// <summary>
        /// 正在打印
        /// </summary>
        Print,
        /// <summary>
        /// 正在输出HTML文档
        /// </summary>
        Html ,
        /// <summary>
        /// 正在输出PDF文档
        /// </summary>
        PDF ,
        /// <summary>
        /// 正在输出RTF文档
        /// </summary>
        RTF,
    }
}
