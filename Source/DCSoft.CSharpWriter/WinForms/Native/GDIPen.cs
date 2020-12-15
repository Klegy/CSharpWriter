/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Runtime.InteropServices ;
namespace DCSoft.WinForms.Native
{
	/// <summary>
	/// 画笔样式
	/// </summary>
	public enum PenStyle
	{
		/// <summary>
		/// The pen is solid.
		/// </summary>
		PS_SOLID            = 0 ,
		/// <summary>
		/// The pen is dashed. This style is valid only when the pen width is one or less in device units.
		/// </summary>
		PS_DASH             = 1 ,      /* -------  */
		/// <summary>
		/// The pen is dotted. This style is valid only when the pen width is one or less in device units.
		/// </summary>
		PS_DOT              = 2 ,      /* .......  */
		/// <summary>
		/// The pen has alternating dashes and dots. This style is valid only when the pen width is one or less in device units.
		/// </summary>
		PS_DASHDOT          = 3 ,      /* _._._._  */
		/// <summary>
		/// The pen has alternating dashes and double dots. This style is valid only when the pen width is one or less in device units.
		/// </summary>
		PS_DASHDOTDOT       = 4 ,      /* _.._.._  */
		/// <summary>
		/// The pen is invisible.
		/// </summary>
		PS_NULL             = 5 ,
		/// <summary>
		/// The pen is solid. When this pen is used in any GDI drawing function that takes a bounding rectangle, the dimensions of the figure are shrunk so that it fits entirely in the bounding rectangle, taking into account the width of the pen. This applies only to geometric pens.
		/// </summary>
		PS_INSIDEFRAME      = 6 
	}

	/// <summary>
	/// 基于GDI的画笔对象
	/// </summary>
    /// <remarks>编写 袁永福</remarks>
    [Serializable()]
	public class GDIPen : GDIObject
	{
		/// <summary>
		/// 空的画笔对象
		/// </summary>
		public static readonly GDIPen NullPen = new GDIPen( PenStyle.PS_NULL , 1 , System.Drawing.Color.Black );

        /// <summary>
        /// 初始化对象，默认为黑色画笔
        /// </summary>
        public GDIPen()
        {
            intStyle = PenStyle.PS_SOLID;
            intWidth = 1;
            intColor = System.Drawing.Color.Black;
        }

		/// <summary>
		/// 初始化一个指定颜色的宽度为1的实线画笔对象
		/// </summary>
		/// <param name="color">颜色</param>
		public GDIPen( System.Drawing.Color color ): this( PenStyle.PS_SOLID , 1 , color )
		{
		}

		/// <summary>
		/// 初始化对象，创建一个实线画笔
		/// </summary>
		/// <param name="width">线宽</param>
		/// <param name="color">颜色</param>
		public GDIPen( int width , System.Drawing.Color color ) : this( PenStyle.PS_SOLID , width , color)
		{
		}

		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="style">画笔类型</param>
		/// <param name="width">线宽</param>
		/// <param name="color">颜色</param>
		public GDIPen( PenStyle style , int width , System.Drawing.Color color )
		{
			intStyle = style ;
			this.intWidth = width ;
			this.intColor = color ;
		}

		private PenStyle intStyle = PenStyle.PS_SOLID  ;
		/// <summary>
		/// 画笔样式
		/// </summary>
		public PenStyle Style
		{
			get
            {
                return intStyle ;
            }
			set
			{
				if( intStyle != value )
				{
					intStyle = value ;
					this.Dispose();
				}
			}
		}
		private int intWidth = 1 ;
		/// <summary>
		/// 线条宽度
		/// </summary>
		public int Width
		{
			get
            {
                return intWidth ;
            }
            set
            {
                if (intWidth != value)
                {
                    intWidth = value;
                    this.Dispose();
                }
            }
		}

		private System.Drawing.Color intColor = System.Drawing.Color.Black ;
		/// <summary>
		/// 颜色
		/// </summary>
		public System.Drawing.Color Color
		{
			get
            {
                return intColor ;
            }
			set
			{
				if( intColor != value )
				{
					intColor = value ;
					this.Dispose();
				}
			}
		}

        protected override void CheckHandle()
        {
            if (intHandle == IntPtr.Zero)
            {
                this.intHandle = CreatePen((int)intStyle, intWidth, System.Drawing.ColorTranslator.ToWin32(intColor));
            }
        }

		/// <summary>
		/// 在一个设备上下文绘制线段
		/// </summary>
		/// <param name="hdc">设备上下文句柄</param>
		/// <param name="x1">起点X坐标</param>
		/// <param name="y1">起点Y坐标</param>
		/// <param name="x2">终点X坐标</param>
		/// <param name="y2">终点Y坐标</param>
		public void DrawLine( IntPtr hdc , int x1 , int y1 , int x2 , int y2 )
		{
            CheckHandle();
			IntPtr h = this.SelectTo( hdc );
			MoveToEx( hdc , x1 , y1 , 0 );
			LineTo( hdc , x2 , y2 );
			if( h.ToInt32() != 0 )
			{
				this.UnSelect( hdc , h );
			}
		}

		/// <summary>
		/// 在一个设备上下文上绘制多条线段
		/// </summary>
		/// <param name="hdc">设备上下文句柄</param>
		/// <param name="ps">点数组</param>
		public void DrawLines( IntPtr hdc , System.Drawing.Point[] ps )
		{
            CheckHandle();
			IntPtr h = this.SelectTo( hdc );
			POINT[] ps2 = new POINT[ ps.Length ];
			for( int iCount = 0 ; iCount < ps.Length ; iCount ++ )
			{
				ps2[iCount].x = ps[ iCount ].X ;
				ps2[iCount].y = ps[ iCount ].Y ;
			}
			Polyline( hdc , ps2 , ps2.Length );
			if( h.ToInt32() != 0 )
			{
				this.UnSelect( hdc , h );
			}
		}

		/// <summary>
		/// 在一个设备上下文绘制线段
		/// </summary>
		/// <param name="hdc">设备上下文句柄</param>
		/// <param name="p1">起点坐标</param>
		/// <param name="p2">终点坐标</param>
		public void DrawLine( IntPtr hdc , System.Drawing.Point p1 , System.Drawing.Point p2 )
		{
            CheckHandle();
			DrawLine( hdc , p1.X , p1.Y , p2.X , p2.Y );
		}

		#region 声明Win32API函数 ******************************************************************
		
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		private static extern IntPtr CreatePen( int PenStyle , int Width , int Color );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		private static extern bool LineTo( System.IntPtr hDC , int X , int Y );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		private static extern bool Polyline( System.IntPtr hDC , POINT[] ps , int len );

		[StructLayout(LayoutKind.Sequential)]
		private struct POINT
		{
			public int x;
			public int y;
		}

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		private static extern bool MoveToEx ( System.IntPtr hDC , int X , int Y , int lpPoint );

		#endregion

 	}//public class GDIPen : GDIObject
}