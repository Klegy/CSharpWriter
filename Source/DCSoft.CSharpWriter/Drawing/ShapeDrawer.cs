/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Drawing ;
using System.Drawing.Drawing2D ;

namespace DCSoft.Drawing
{
    /// <summary>
    /// 图示样式信息
    /// </summary>
    [System.ComponentModel.TypeConverter( typeof( ShapeSymbleStyleTypeConverter ))]
    [Serializable()]
    public class ShapeSymbleStyle
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ShapeSymbleStyle()
        {
        }

        private System.Drawing.Color intBorderColor = System.Drawing.Color.Black;
        /// <summary>
        /// 边框色
        /// </summary>
        [System.ComponentModel.DefaultValue( typeof( System.Drawing.Color ) , "Black")]
        public System.Drawing.Color BorderColor
        {
            get
            {
                return intBorderColor; 
            }
            set
            {
                intBorderColor = value; 
            }
        }

        private System.Drawing.Color intBackColor = System.Drawing.Color.White;
        /// <summary>
        /// 背景色
        /// </summary>
        [System.ComponentModel.DefaultValue( typeof( System.Drawing.Color ) , "White" )]
        public System.Drawing.Color BackColor
        {
            get
            {
                return intBackColor; 
            }
            set
            {
                intBackColor = value; 
            }
        }

        private int intSize = 50;
        /// <summary>
        /// 图示大小
        /// </summary>
        [System.ComponentModel.DefaultValue( 50 )]
        public int Size
        {
            get
            {
                return intSize;
            }
            set
            {
                intSize = value;
            }
        }

        private ShapeTypes intStyle = ShapeTypes.Rectangle;
        /// <summary>
        /// 图示样式
        /// </summary>
        [System.ComponentModel.DefaultValue( ShapeTypes.Rectangle )]
        public ShapeTypes Style
        {
            get
            {
                return intStyle;
            }
            set
            {
                intStyle = value;
            }
        }

        /// <summary>
        /// 绘制图形
        /// </summary>
        /// <param name="g">画布对象</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        public void Draw(System.Drawing.Graphics g, float x, float y)
        {
            using (System.Drawing.SolidBrush b = new SolidBrush(intBackColor))
            {
                using (Pen p = new Pen(intBorderColor))
                {
                    ShapeDrawer drawer = new ShapeDrawer();
                    drawer.Bounds = new RectangleF(x - intSize / 2, y - intSize / 2, intSize, intSize);
                    drawer.BorderPen = p;
                    drawer.FillBrush = b;
                    drawer.DrawAndFill(g);
                }
            }
        }
    }

	/// <summary>
	/// 图形绘制对象
	/// </summary>
	public class ShapeDrawer
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public ShapeDrawer()
		{
		}

		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="rect">矩形对象</param>
		/// <param name="type">图形类型</param>
		/// <param name="BorderColor">边框色</param>
		/// <param name="FillColor">背景色</param>
		public ShapeDrawer( 
			System.Drawing.RectangleF rect , 
			ShapeTypes type , 
			System.Drawing.Color BorderColor ,
			System.Drawing.Color FillColor )
		{
			this.myBounds = rect ;
			this.intType = type ;
			this.myBorderPen = new Pen( BorderColor );
			this.myFillBrush = new SolidBrush( FillColor );
		}

		private System.Drawing.RectangleF myBounds = System.Drawing.RectangleF.Empty ;
		/// <summary>
		/// 对象边框
		/// </summary>
		public System.Drawing.RectangleF Bounds
		{
			get
            { 
                return myBounds ;
            }
			set
            { 
                myBounds = value;
            }
		}
		/// <summary>
		/// 设置/返回对象左端位置
		/// </summary>
		public float Left
		{
			get
            {
                return myBounds.Left ;
            }
			set
            {
                myBounds.X = value;
            }
		}
		/// <summary>
		/// 设置/返回对象的顶端位置
		/// </summary>
		public float Top
		{
			get
            { 
                return myBounds.Top ;
            }
			set
            { 
                myBounds.Y  = value;
            }
		}
		/// <summary>
		/// 设置/返回对象宽度
		/// </summary>
		public float Width
		{
			get
            {
                return myBounds.Width ;
            }
			set
            {
                myBounds.Width = value;
            }
	    }

		/// <summary>
		/// 设置/返回对象高度
		/// </summary>
		public float Height
		{
			get
            {
                return myBounds.Height ;
            }
			set
            { 
                myBounds.Height = value;
            }
		}

        ///// <summary>
        ///// 返回对象右端位置
        ///// </summary>
        //public int Right
        //{
        //    get{ return myBounds.Right ;}
        //}
        ///// <summary>
        ///// 返回对象的底端位置
        ///// </summary>
        //public int Bottom
        //{
        //    get{ return myBounds.Bottom ;}
        //}

        private float fRoundRadio = 0;
        /// <summary>
        /// 圆角半径
        /// </summary>
        public float RoundRadio
        {
            get
            {
                return fRoundRadio;
            }
            set
            {
                fRoundRadio = value;
            }
        }

        //private void DrawOneShape(ShapeTypes type, Graphics g)
        //{
        //    if ((intType & type) == type)
        //        DrawShape(myBounds, this.fRoundRadio, g, myBorderPen, myFillBrush, type);
        //}
		private ShapeTypes intType = ShapeTypes.Rectangle ;
		/// <summary>
		/// 符号类型
		/// </summary>
		public ShapeTypes Type
		{
			get
            {
                return intType ;
            }
			set
            {
                intType = value;
            }
		}

		private System.Drawing.Pen myBorderPen = null;
		/// <summary>
		/// 绘制边框使用的画笔对象
		/// </summary>
		public System.Drawing.Pen BorderPen
		{
			get
            {
                return myBorderPen ;
            }
			set
            {
                myBorderPen = value;
            }
		}

		private System.Drawing.Brush myFillBrush = null;
		/// <summary>
		/// 填充对象使用的画刷对象
		/// </summary>
		public System.Drawing.Brush FillBrush
		{
			get
            {
                return myFillBrush ;
            }
			set
            {
                myFillBrush = value;
            }
		}


        /// <summary>
        /// 创新路径对象
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Drawing2D.GraphicsPath CreatePath()
        {
            System.Drawing.RectangleF rect = RectangleF.Empty;
            switch (intType)
            {
                case ShapeTypes.Default :
                    // 默认为矩形
                    return CreateRoundRectanglePath(myBounds, fRoundRadio);
                case ShapeTypes.Rectangle :
                    // 矩形
                    return CreateRoundRectanglePath(myBounds, fRoundRadio);
                case ShapeTypes.Square :
                    // 正方形
                    return CreateRoundRectanglePath(GetSquareRect(), fRoundRadio);
                case ShapeTypes.Ellipse :
                    // 椭圆
                    rect = myBounds;
                    GraphicsPath pathEllipse = new GraphicsPath();
                    pathEllipse.AddEllipse(myBounds);
                    return pathEllipse;
                case ShapeTypes.Circle :
                    // 正圆
                    GraphicsPath pathCircle = new GraphicsPath();
                    pathCircle.AddEllipse(GetSquareRect());
                    return pathCircle;
                case ShapeTypes.Diamond :
                    // 菱形
                    return CreateDiamondPath(myBounds);
                case ShapeTypes.TriangleUp :
                    // 向上的三角形
                    return CreateTrianglePath(myBounds, 0);
                case ShapeTypes.TriangleRight :
                    // 向右的三角形
                    return CreateTrianglePath(myBounds, 1);
                case ShapeTypes.TriangleDown :
                    // 向下的三角行
                    return CreateTrianglePath(myBounds, 2);
                case ShapeTypes.TriangleLeft :
                    // 向左的三角行
                    return CreateTrianglePath(myBounds, 3);
                case ShapeTypes.Cross :
                    // 交叉线
                    GraphicsPath crossPath = new GraphicsPath();
                    crossPath.AddLine(
                        myBounds.Left,
                        myBounds.Top + myBounds.Height / 2,
                        myBounds.Right,
                        myBounds.Top + myBounds.Height / 2);
                    crossPath.AddLine(
                        myBounds.Left + myBounds.Width / 2, 
                        myBounds.Top,
                        myBounds.Left + myBounds.Width / 2,
                        myBounds.Bottom);
                    return crossPath;
                case ShapeTypes.XCross :
                    GraphicsPath xcrossPath = new GraphicsPath();
                    xcrossPath.AddLine(
                        myBounds.Left, 
                        myBounds.Top,
                        myBounds.Right,
                        myBounds.Bottom);
                    xcrossPath.AddLine(
                        myBounds.Right,
                        myBounds.Top,
                        myBounds.Left,
                        myBounds.Bottom);
                    return xcrossPath;
                case ShapeTypes.CircleXCross :
                    // 圆交叉线
                    System.Drawing.RectangleF sr2 = GetSquareRect();
                    float r = sr2.Width / 2;
                    float d = (int)(r * Math.Sin(Math.PI / 4));
                    GraphicsPath cxPath = new GraphicsPath();
                    float cx = myBounds.Left + myBounds.Width / 2;
                    float cy = myBounds.Top + myBounds.Height / 2;
                    cxPath.AddLine(cx - d, cy - d, cx + d, cy + d);
                    cxPath.AddLine(cx + d, cy - d, cx - d, cy + d);
                    cxPath.AddEllipse(sr2);
                    return cxPath;
                case ShapeTypes.CircleCross :
                    // 45度角的圆交叉线
                    System.Drawing.RectangleF sr = GetSquareRect();
                    GraphicsPath ccPath = new GraphicsPath();
                    ccPath.AddLine(
                        sr.Left,
                        sr.Top + sr.Height / 2,
                        sr.Right,
                        sr.Top + sr.Height / 2);
                    ccPath.AddLine(
                        sr.Left + sr.Width / 2,
                        sr.Top,
                        sr.Left + sr.Width / 2,
                        sr.Bottom);
                    ccPath.AddEllipse( sr );
                    return ccPath;
            }
            return null;
        }

        /// <summary>
        /// 获得居中的正方形
        /// </summary>
        /// <returns></returns>
        private RectangleF GetSquareRect()
        {
            if (myBounds.Width > myBounds.Height)
            {
                return new RectangleF(
                    myBounds.Left + (myBounds.Width - myBounds.Height) / 2,
                    myBounds.Top,
                    myBounds.Height,
                    myBounds.Height);
            }
            else
            {
                return new RectangleF(
                    myBounds.Left,
                    myBounds.Top + (myBounds.Height - myBounds.Width) / 2,
                    myBounds.Width,
                    myBounds.Width);
            }
        }

        /// <summary>
        /// 绘制图形边框 
        /// </summary>
        /// <param name="g"></param>
        public void DrawBorder(Graphics g)
        {
            if (myBorderPen != null && g != null)
            {
                switch (intType)
                {
                    case ShapeTypes.Cross:
                        // 交叉线
                        g.DrawLine(
                            myBorderPen,
                            myBounds.Left,
                            myBounds.Top + myBounds.Height / 2,
                            myBounds.Right,
                            myBounds.Top + myBounds.Height / 2);
                        g.DrawLine(
                            myBorderPen,
                            myBounds.Left + myBounds.Width / 2,
                            myBounds.Top,
                            myBounds.Left + myBounds.Width / 2,
                            myBounds.Bottom);
                        break;
                    case ShapeTypes.XCross :
                        g.DrawLine(
                            myBorderPen ,
                            myBounds.Left,
                            myBounds.Top,
                            myBounds.Right,
                            myBounds.Bottom);
                        g.DrawLine(
                            myBorderPen ,
                            myBounds.Right,
                            myBounds.Top,
                            myBounds.Left,
                            myBounds.Bottom);
                        break;
                    case ShapeTypes.CircleXCross:
                        // 圆交叉线
                        System.Drawing.RectangleF sr2 = GetSquareRect();
                        float r = sr2.Width / 2;
                        float d = (int)(r * Math.Sin(Math.PI / 4));
                        GraphicsPath cxPath = new GraphicsPath();
                        float cx = myBounds.Left + myBounds.Width / 2;
                        float cy = myBounds.Top + myBounds.Height / 2;
                        g.DrawLine(myBorderPen, cx - d, cy - d, cx + d, cy + d);
                        g.DrawLine(myBorderPen, cx + d, cy - d, cx - d, cy + d);
                        g.DrawEllipse(myBorderPen, sr2);
                        break;
                    case ShapeTypes.CircleCross:
                        System.Drawing.RectangleF sr = GetSquareRect();
                        // 45度角的圆交叉线
                        g.DrawLine(
                            myBorderPen,
                            sr.Left,
                            sr.Top + sr.Height / 2,
                            sr.Right,
                            sr.Top + sr.Height / 2);
                        g.DrawLine(
                            myBorderPen,
                            sr.Left + sr.Width / 2,
                            sr.Top,
                            sr.Left + sr.Width / 2,
                            sr.Bottom);
                        g.DrawEllipse(myBorderPen, sr);
                        break;
                    default:
                        GraphicsPath path = this.CreatePath();
                        if (path != null)
                        {
                            g.DrawPath(myBorderPen, path);
                            path.Dispose();
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 填充图形
        /// </summary>
        /// <param name="g"></param>
        public void Fill(Graphics g)
        {
            if (myFillBrush != null && g != null)
            {
                switch (intType)
                {
                    case ShapeTypes.Cross:
                        // 交叉线
                        break;
                    case ShapeTypes.XCross :
                        break;
                    case ShapeTypes.CircleXCross:
                        // 圆交叉线
                        System.Drawing.RectangleF sr2 = GetSquareRect();
                        g.FillEllipse(myFillBrush, sr2);
                        break;
                    case ShapeTypes.CircleCross:
                        // 45度角的圆交叉线
                        System.Drawing.RectangleF sr = GetSquareRect();
                        g.FillEllipse(myFillBrush, sr);
                        break;
                    default:
                        GraphicsPath path = this.CreatePath();
                        if (path != null)
                        {
                            g.FillPath( myFillBrush , path );
                            path.Dispose();
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 绘制边框并填充内容
        /// </summary>
        /// <param name="g">画布对象</param>
        public void DrawAndFill(Graphics g)
        {
            if (g != null)
            {
                if (myFillBrush != null)
                {
                    Fill(g);
                }
                if (myBorderPen != null)
                {
                    DrawBorder(g);
                    //g.DrawPath( myBorderPen , path);
                }
            }
        }

        ///// <summary>
        ///// 绘制对象
        ///// </summary>
        ///// <param name="g">图形绘制对象</param>
        ///// <param name="ClipRect">剪切矩形</param>
        //public void Draw(Graphics g, Rectangle ClipRect)
        //{
        //    if( myFillBrush == null && myBorderPen == null )
        //        return ;
        //    DrawShape( myBounds , this.intRoundRadio , g , myBorderPen , myFillBrush , intType );
        //}//public override void Draw(Graphics g, Rectangle ClipRect)

        ///// <summary>
        ///// 绘制图形
        ///// </summary>
        ///// <param name="bounds">图形边界</param>
        ///// <param name="RoundRadio">圆角半径</param>
        ///// <param name="g">图形绘制对象</param>
        ///// <param name="pen">绘制线条的画笔对象</param>
        ///// <param name="brush">填充图形的画笔对象</param>
        ///// <param name="type">图形类型</param>
        //public static void DrawShape(
        //    System.Drawing.Rectangle bounds ,
        //    int RoundRadio ,
        //    Graphics g , 
        //    System.Drawing.Pen pen ,
        //    System.Drawing.Brush brush ,
        //    ShapeTypes type )
        //{
        //    if( brush == null && pen == null )
        //        return ;

        //    int w2 = bounds.Width / 2 ;
        //    int h2 = bounds.Height / 2 ;
        //    int cx = bounds.Left + w2 ;
        //    int cy = bounds.Top + h2 ;

        //    System.Drawing.Rectangle SquareRect = System.Drawing.Rectangle.Empty ;

        //    if( bounds.Width > bounds.Height )
        //        SquareRect = new Rectangle( 
        //            bounds.Left + ( bounds.Width - bounds.Height ) / 2 ,
        //            bounds.Top ,
        //            bounds.Height ,
        //            bounds.Height );
        //    else
        //        SquareRect = new Rectangle(
        //            bounds.Left ,
        //            bounds.Top + ( bounds.Height - bounds.Width ) / 2 ,
        //            bounds.Width , 
        //            bounds.Width );

        //    switch( type )
        //    {
        //        case ShapeTypes.Rectangle :
        //            if( brush != null )
        //                g.FillRectangle( brush , bounds );
        //            if( pen != null )
        //                g.DrawRectangle( pen , bounds );
        //            break;
        //        case ShapeTypes.Square :
        //            if( brush != null )
        //                g.FillRectangle( brush , SquareRect );
        //            if( pen != null )
        //                g.DrawRectangle( pen , SquareRect );
        //            break;
        //        case ShapeTypes.Ellipse :
        //            if( brush != null )
        //                g.FillEllipse( brush , bounds );
        //            if( pen != null )
        //                g.DrawEllipse( pen , bounds );
        //            break;
        //        case ShapeTypes.Circle :
        //            if( brush != null )
        //                g.FillEllipse( brush , SquareRect );
        //            if( pen != null )
        //                g.DrawEllipse( pen , SquareRect );
        //            break;
        //        case ShapeTypes.Diamond :
        //            using( GraphicsPath path = CreateDiamondPath( bounds ))
        //            {
        //                if( brush != null )
        //                    g.FillPath( brush , path );
        //                if( pen != null )
        //                    g.DrawPath( pen , path );
        //            }
        //            break;
        //        case ShapeTypes.RoundedRectangle :
        //            using( GraphicsPath path2 = CreateRoundRectanglePath( bounds , RoundRadio ))
        //            {
        //                if( brush != null )
        //                    g.FillPath( brush , path2 );
        //                if( pen != null )
        //                    g.DrawPath( pen , path2 );
        //            }
        //            break;
        //        case ShapeTypes.RoundedSquare :
        //            using( GraphicsPath path = CreateRoundRectanglePath( SquareRect , RoundRadio ))
        //            {
        //                if( brush != null )
        //                    g.FillPath( brush , path );
        //                if( pen != null )
        //                    g.DrawPath( pen , path );
        //            }
        //            break;
        //        case ShapeTypes.TriangleUp :
        //            using( GraphicsPath path = CreateTrianglePath( bounds , 0 ))
        //            {
        //                if( brush != null )
        //                    g.FillPath( brush , path );
        //                if( pen != null )
        //                    g.DrawPath( pen , path );
        //            }
        //            break;
        //        case ShapeTypes.TriangleRight :
        //            using( GraphicsPath path = CreateTrianglePath( bounds , 1 ))
        //            {
        //                if( brush != null )
        //                    g.FillPath( brush , path );
        //                if( pen != null )
        //                    g.DrawPath( pen , path );
        //            }
        //            break;
        //        case ShapeTypes.TriangleDown :
        //            using( GraphicsPath path = CreateTrianglePath( bounds , 2 ))
        //            {
        //                if( brush != null )
        //                    g.FillPath( brush , path );
        //                if( pen != null )
        //                    g.DrawPath( pen , path );
        //            }
        //            break;
        //        case ShapeTypes.TriangleLeft :
        //            using( GraphicsPath path = CreateTrianglePath( bounds , 3 ))
        //            {
        //                if( brush != null )
        //                    g.FillPath( brush , path );
        //                if( pen != null )
        //                    g.DrawPath( pen , path );
        //            }
        //            break;
        //        case ShapeTypes.Cross :
        //            if( pen != null )
        //            {
        //                g.DrawLine( 
        //                    pen ,
        //                    bounds.Left ,
        //                    bounds.Top + h2 ,
        //                    bounds.Right ,
        //                    bounds.Top + h2 );
        //                g.DrawLine( 
        //                    pen ,
        //                    bounds.Left + w2 ,
        //                    bounds.Top ,
        //                    bounds.Left + w2 ,
        //                    bounds.Bottom );
        //            }
        //            break;
        //        case ShapeTypes.XCross :
        //            if( pen != null )
        //            {
        //                g.DrawLine(
        //                    pen ,
        //                    bounds.Left , 
        //                    bounds.Top ,
        //                    bounds.Right ,
        //                    bounds.Bottom );
        //                g.DrawLine( 
        //                    pen , 
        //                    bounds.Right ,
        //                    bounds.Top , 
        //                    bounds.Left , 
        //                    bounds.Bottom );
        //            }
        //            break;
        //        case ShapeTypes.CircleCross :
        //            if( pen != null )
        //            {
        //                g.DrawLine( 
        //                    pen ,
        //                    SquareRect.Left ,
        //                    SquareRect.Top + SquareRect.Height / 2 ,
        //                    SquareRect.Right ,
        //                    SquareRect.Top + SquareRect.Height / 2 );
        //                g.DrawLine( 
        //                    pen ,
        //                    SquareRect.Left + SquareRect.Width / 2 ,
        //                    SquareRect.Top ,
        //                    SquareRect.Left + SquareRect.Width / 2 ,
        //                    SquareRect.Bottom );
        //                g.DrawEllipse( pen , SquareRect );
        //            }
        //            break;
        //        case ShapeTypes.CircleXCross :
        //            if( pen != null )
        //            {
        //                int r = SquareRect.Width / 2 ;
        //                int d = ( int ) ( r * Math.Sin( Math.PI / 4 ) );
        //                g.DrawLine(
        //                    pen ,
        //                    cx - d ,
        //                    cy - d ,
        //                    cx + d ,
        //                    cy + d );
        //                g.DrawLine(
        //                    pen ,
        //                    cx + d ,
        //                    cy - d ,
        //                    cx - d ,
        //                    cy + d );
        //                g.DrawEllipse( pen , SquareRect );
        //            }
        //            break;
        //    }//switch( intType )
        //}//public override void Draw(Graphics g, Rectangle ClipRect)

		/// <summary>
		/// 创建一个三角形路径
		/// </summary>
		/// <param name="rect">外切矩形</param>
		/// <param name="direction">三角形方向 0:向上 1:向右 2:向下 3:向左</param>
		/// <returns>创建路径对象</returns>
		public static GraphicsPath CreateTrianglePath( System.Drawing.RectangleF rect , int direction )
		{
			if( direction < 0 || direction > 3 )
				throw new System.ArgumentOutOfRangeException( "direction" , "0->3" );
			GraphicsPath path = new GraphicsPath();
			float w2 = rect.Width / 2 ;
			float h2 = rect.Height / 2 ;
			if( direction == 0 )
			{
				path.AddLine( rect.Left + w2 , rect.Top , rect.Right , rect.Bottom );
				path.AddLine( rect.Right , rect.Bottom , rect.Left , rect.Bottom );
				path.AddLine( rect.Left , rect.Bottom , rect.Left + w2 , rect.Top );
			}
			else if( direction == 1 )
			{
				path.AddLine( rect.Left , rect.Top , rect.Right , rect.Top + h2 );
				path.AddLine( rect.Right , rect.Top + h2 , rect.Left , rect.Bottom );
				path.AddLine( rect.Left , rect.Bottom , rect.Left , rect.Top );
			}
			else if( direction == 2 )
			{
				path.AddLine( rect.Left , rect.Top , rect.Right , rect.Top );
				path.AddLine( rect.Right , rect.Top , rect.Left + w2 , rect.Bottom );
				path.AddLine( rect.Left + w2 , rect.Bottom , rect.Left , rect.Top );
			}
			else if( direction == 3 )
			{
				path.AddLine( rect.Left , rect.Top + h2 , rect.Right , rect.Top );
				path.AddLine( rect.Right , rect.Top , rect.Right , rect.Bottom );
				path.AddLine( rect.Right , rect.Bottom , rect.Left , rect.Top + h2 );
			}
			return path ;
		}
		/// <summary>
		/// 创建一个菱形路径对象
		/// </summary>
		/// <param name="rect">菱形外切矩形</param>
		/// <returns>创建的路径对象</returns>
		public static GraphicsPath CreateDiamondPath( System.Drawing.RectangleF rect )
		{
			float w2 = rect.Width / 2 ;
			float h2 = rect.Height / 2 ;
			GraphicsPath path = new GraphicsPath();
			path.AddLine(
				rect.Left + w2 ,
				rect.Top ,
				rect.Right ,
				rect.Top + h2 );
			path.AddLine( 
				rect.Right ,
				rect.Top + h2 ,
				rect.Left + w2 ,
				rect.Bottom );
			path.AddLine(
				rect.Left + w2 ,
				rect.Bottom ,
				rect.Left ,
				rect.Top + h2 );
			path.AddLine(
				rect.Left ,
				rect.Top + h2 ,
				rect.Left + w2 ,
				rect.Top );
			return path ;
		}

		/// <summary>
		/// 创建一个圆角矩形路径对象
		/// </summary>
		/// <param name="rect">矩形对象</param>
		/// <param name="radio">圆角半径,若小于等于0则表示矩形</param>
		/// <returns>创建的路径对象</returns>
		public static GraphicsPath CreateRoundRectanglePath( System.Drawing.RectangleF rect , float radio )
		{
			// 圆角矩形由4个90度扇形和4个线段组成
			GraphicsPath path = new GraphicsPath();
			if( radio <= 0 )
			{
				path.AddRectangle( rect );
				return path ;
			}
			// 左上角圆角
			path.AddArc( rect.Left , rect.Top , radio , radio , 270 , -90);
			// 左边框
			path.AddLine( rect.Left , rect.Top + radio / 2 , rect.Left , rect.Bottom - radio / 2);

			// 左下圆角
			path.AddArc( rect.Left , rect.Bottom - radio , radio , radio , 180 , -90);
			// 底边框
			path.AddLine( rect.Left + radio /2 , rect.Bottom , rect.Right - radio /2 , rect.Bottom );

			// 右下圆角
			path.AddArc( rect.Right - radio , rect.Bottom - radio , radio , radio , 90 , -90 );
			// 右边框
			path.AddLine( rect.Right , rect.Bottom - radio / 2, rect.Right , rect.Top + radio / 2 );

			// 右上圆角
			path.AddArc( rect.Right - radio , rect.Top , radio , radio , 0 , -90 );
			// 顶边框
			path.AddLine( rect.Right - radio /2 , rect.Top , rect.Left + radio / 2 , rect.Top );
			path.CloseAllFigures();
			return path ;
		}
	}//public class ShapeDrawer : BaseDrawer
    /// <summary>
    /// 用于 ShapeSymbleStyle的类型转换器
    /// </summary>
    public class ShapeSymbleStyleTypeConverter : System.ComponentModel.TypeConverter
    {
        /// <summary>
        /// 是否支持获得对象属性列表
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>支持</returns>
        public override bool GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }
        /// <summary>
        /// 获得对象的属性
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="value">对象数据</param>
        /// <param name="attributes">特性</param>
        /// <returns>对象的属性列表</returns>
        public override System.ComponentModel.PropertyDescriptorCollection GetProperties(System.ComponentModel.ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return System.ComponentModel.TypeDescriptor.GetProperties(typeof(ShapeSymbleStyle), attributes);
        }
    }
}