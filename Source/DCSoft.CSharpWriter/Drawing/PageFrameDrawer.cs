/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Drawing ;

namespace DCSoft.Drawing
{
	/// <summary>
	/// 页面边框绘制对象
	/// </summary>
	public class PageFrameDrawer
	{
		/// <summary>
		/// 绘制页面边框
		/// </summary>
		/// <param name="bounds">页面边框</param>
		/// <param name="m">页边距对象</param>
		/// <param name="g">图形绘制对象</param>
		/// <param name="ClipRectangle">剪切矩形</param>
		/// <param name="HightlightingBorder">是否高亮度显示边框</param>
		/// <param name="FillBackground">是否填充背景</param>
		public static void DrawPageFrame(
			System.Drawing.Rectangle bounds ,
			System.Drawing.Printing.Margins m ,
			System.Drawing.Graphics g , 
			System.Drawing.Rectangle ClipRectangle ,
			bool HightlightingBorder , 
			System.Drawing.Color PageBackColor )
		{
			PageFrameDrawer drawer = new PageFrameDrawer();
			drawer.Bounds = bounds ;
			drawer.Margins = m ;
			if( HightlightingBorder )
				drawer.BorderColor = System.Drawing.Color.Blue ;
			else
				drawer.BorderColor = System.Drawing.Color.Black ;
			drawer.BorderWidth = 5 ;
            drawer.BackColor = PageBackColor;
			drawer.DrawPageFrame( g , ClipRectangle );
		}
		/// <summary>
		/// 初始化对象
		/// </summary>
		public PageFrameDrawer()
		{
		}

		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="bounds">页面边框</param>
		/// <param name="m">页边距对象</param>
		public PageFrameDrawer(
			System.Drawing.Rectangle bounds ,
			System.Drawing.Printing.Margins m )
		{
			this.myBounds = bounds ;
			this.Margins = m ;
		}

		private System.Drawing.Rectangle myBounds 
            = System.Drawing.Rectangle.Empty ;
		/// <summary>
		/// 对象边框
		/// </summary>
		public System.Drawing.Rectangle Bounds
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
	
        private int intLeftMargin = 20;
        /// <summary>
		/// 左页边距
		/// </summary>
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

        private int intTopMargin = 30;
        /// <summary>
		/// 顶页边距
		/// </summary>
		public int TopMargin
		{
			get
            {
                return intTopMargin ;
            }
			set
            {
                intTopMargin = value;
            }
		}

        private int intRightMargin = 20;
        /// <summary>
		/// 右页边距
		/// </summary>
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
        
        private int intBottomMargin = 40;
        /// <summary>
		/// 底页边距
		/// </summary>
		public int BottomMargin
		{
			get
            {
                return intBottomMargin ;
            }
			set
            {
                intBottomMargin = value;
            }
		}

		/// <summary>
		/// 页边距对象
		/// </summary>
		public System.Drawing.Printing.Margins Margins
		{
			get
			{
				return new System.Drawing.Printing.Margins( 
					this.intLeftMargin , 
					this.intRightMargin ,
					this.intTopMargin ,
					this.intBottomMargin );
			}
			set
			{
				this.intLeftMargin = value.Left ;
				this.intTopMargin = value.Top ;
				this.intRightMargin = value.Right ;
				this.intBottomMargin = value.Bottom ;
			}
		}

		private int intMarginLineLength = 60 ;
		/// <summary>
		/// 边距线长度
		/// </summary>
		public int MarginLineLength
		{
			get
            {
                return intMarginLineLength ;
            }
			set
            {
                intMarginLineLength = value;
            }
		}

		private bool bolDrawMargin = true;
		/// <summary>
		/// 是否绘制边距线
		/// </summary>
		public bool DrawMargin
		{
			get
            {
                return bolDrawMargin ;
            }
			set
            {
                bolDrawMargin = value;
            }
		}

        private System.Drawing.Color intMarginLineColor
            = System.Drawing.Color.FromArgb(170, 170, 170);
		/// <summary>
		/// 边距线颜色
		/// </summary>
		public System.Drawing.Color MarginLineColor
		{
			get
            {
                return this.intMarginLineColor ;
            }
			set
            {
                this.intMarginLineColor = value;
            }
		}

		private System.Drawing.Color intBackColor = System.Drawing.Color.White ;
		/// <summary>
		/// 背景色
		/// </summary>
		public System.Drawing.Color BackColor
		{
			get
            {
                return intBackColor ;
            }
			set
            {
                intBackColor = value;
            }
		}

		private System.Drawing.Color intBorderColor = System.Drawing.Color.Black ;
		/// <summary>
		/// 边框线颜色
		/// </summary>
		public System.Drawing.Color BorderColor
		{
			get
            {
                return intBorderColor ;
            }
			set
            {
                intBorderColor = value;
            }
		}

		private int intBorderWidth = 1 ;
		/// <summary>
		/// 边框线宽度
		/// </summary>
		public int BorderWidth
		{
			get
            {
                return intBorderWidth ;
            }
			set
            {
                intBorderWidth = value;
            }
		}

        private System.Drawing.Image myBackgroundImage = null;
        /// <summary>
        /// 页面背景图片
        /// </summary>
        public System.Drawing.Image BackgroundImage
        {
            get
            {
                return myBackgroundImage; 
            }
            set
            {
                myBackgroundImage = value; 
            }
        }

        //public System.Drawing.Rectangle ClientBounds
        //{
        //    get
        //    {
        //        new System.Drawing.Rectangle(
        //                    myBounds.Left + this.intLeftMargin,
        //                    myBounds.Top + this.intTopMargin,
        //                    myBounds.Width - this.intLeftMargin - this.intRightMargin,
        //                    myBounds.Height - this.intTopMargin - this.intBottomMargin);
        //    }
        //}

        private System.Drawing.Drawing2D.GraphicsPath CreateShape()
        {
            return ShapeDrawer.CreateRoundRectanglePath(
                new RectangleF(
                    myBounds.Left, 
                    myBounds.Top, 
                    myBounds.Width, 
                    myBounds.Height),
                30);
        }

		/// <summary>
		/// 使用指定图形绘制对象从指定位置开始绘制页面框架
		/// </summary>
		/// <param name="g">图形绘制对象</param>
		/// <param name="ClipRectangle">剪切矩形</param>
		public void DrawPageFrame(
			System.Drawing.Graphics g , 
			System.Drawing.Rectangle ClipRectangle )
		{
			using( RectangleDrawer drawer = new RectangleDrawer() )
			{
				drawer.Bounds = this.myBounds ;
                drawer.RoundRadio = 9;
				if( this.intBorderColor.A != 0 && this.intBorderWidth > 0 )
					drawer.BorderPen = new System.Drawing.Pen(
                        this.intBorderColor ,
                        this.intBorderWidth );
				if( this.intBackColor.A != 0 )
					drawer.FillBrush = new System.Drawing.SolidBrush( this.intBackColor );
				if( drawer.Draw( g , ClipRectangle ))
				{
                    if (myBackgroundImage != null)
                    {
                        // 绘制页面背景图片
                        Rectangle ir = new Rectangle(
                            myBounds.Left ,
                            myBounds.Top ,
                            myBounds.Width  ,
                            myBounds.Height );
                        if (ClipRectangle.IsEmpty == false )
                        {
                            ir = System.Drawing.Rectangle.Intersect(ir, ClipRectangle);
                        }
                        if (ir.IsEmpty == false)
                        {
                            using (TextureBrush brush = new TextureBrush(myBackgroundImage))
                            {
                                brush.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
                                float rate = (float)GraphicsUnitConvert.GetRate(
                                    g.PageUnit,
                                    System.Drawing.GraphicsUnit.Pixel);
                                brush.TranslateTransform(
                                    myBounds.Left ,
                                    myBounds.Top );
                                brush.ScaleTransform(rate, rate);
                                g.FillRectangle(brush, ir);
                            }//using
                            drawer.DrawBorder(g, ClipRectangle);
                        }//if

                        //Rectangle ir = new Rectangle(
                        //    myBounds.Left + this.LeftMargin,
                        //    myBounds.Top + this.TopMargin,
                        //    myBounds.Width - this.LeftMargin - this.RightMargin,
                        //    myBounds.Height - this.TopMargin - this.BottomMargin);
                        //if (ClipRectangle.IsEmpty == false)
                        //{
                        //    ir = System.Drawing.Rectangle.Intersect(ir, ClipRectangle);
                        //}
                        //if (ir.IsEmpty == false)
                        //{
                        //    using (TextureBrush brush = new TextureBrush(myBackgroundImage))
                        //    {
                        //        brush.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
                        //        float rate = (float)GraphicsUnitConvert.GetRate(
                        //            g.PageUnit,
                        //            System.Drawing.GraphicsUnit.Pixel);
                        //        brush.TranslateTransform(
                        //            myBounds.Left + this.LeftMargin,
                        //            myBounds.Top + this.TopMargin);
                        //        brush.ScaleTransform(rate, rate);
                        //        g.FillRectangle(brush, ir);
                        //    }//using
                        //}//if

                    }
                    if( this.bolDrawMargin 
						&& this.intMarginLineColor.A != 0 
						&& this.intMarginLineLength > 0 )
					{
						System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
							myBounds.Left + this.intLeftMargin - 1,
							myBounds.Top + this.intTopMargin ,
							myBounds.Width - this.intLeftMargin - this.intRightMargin + 2,
							myBounds.Height - this.intTopMargin - this.intBottomMargin );

						System.Drawing.Point[] ps = new System.Drawing.Point[ 16 ];
						ps[0] = rect.Location ;
						ps[1].X = rect.Left - intMarginLineLength ;
						ps[1].Y = rect.Top ;
				
						ps[2] = ps[0];
						ps[3].X = rect.Left ;
						ps[3].Y = rect.Top - intMarginLineLength ;

						ps[4].X = rect.Right ;
						ps[4].Y = rect.Top ;
						ps[5].X = rect.Right + intMarginLineLength;
						ps[5].Y = rect.Top ;

						ps[6] = ps[4];
						ps[7].X = rect.Right ;
						ps[7].Y = rect.Top - intMarginLineLength ;

						ps[8].X = rect.Right ;
						ps[8].Y = rect.Bottom ;
						ps[9].X = rect.Right + intMarginLineLength ;
						ps[9].Y = rect.Bottom ;

						ps[10] = ps[8];
						ps[11].X = rect.Right ;
						ps[11].Y = rect.Bottom + intMarginLineLength ;

						ps[12].X = rect.Left ;
						ps[12].Y = rect.Bottom ;
						ps[13].X = rect.Left ;
						ps[13].Y = rect.Bottom + intMarginLineLength ;

						ps[14] = ps[12];
						ps[15].X = rect.Left - intMarginLineLength;
						ps[15].Y = rect.Bottom ;

						MathCommon.RectangleClipLines( myBounds , ps );
						using( System.Drawing.Pen p = new System.Drawing.Pen( 
                            this.intMarginLineColor , 1 ))
						{
							for( int iCount = 0 ;iCount < ps.Length ; iCount += 2 )
							{
								g.DrawLine( p , ps[ iCount ] , ps[iCount +1 ] );
							}
						}//using( System.Drawing.Pen p = new System.Drawing.Pen( this.intMarginLineColor , 1 ))
					}//if( this.bolDrawMargin && this.intMarginLineColor.A != 0 )
				}//if( drawer.Draw( g , ClipRectangle ))
			}//using( RectangleDrawer drawer = new RectangleDrawer() )
		}//public void DrawPageFrame()
	}//public class PageFrameDrawer
}