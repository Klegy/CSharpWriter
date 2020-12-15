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
using System.Collections;

namespace DCSoft.Drawing
{
	/// <summary>
	/// 几何以及数学运算的通用例程模块
	/// </summary>
	public sealed class MathCommon
	{

        /// <summary>
        /// 修正元素的大小，使得能完全的放置在一个容器中而不被剪切掉
        /// </summary>
        /// <param name="containerSize">容器的大小</param>
        /// <param name="elementSize">元素的原始大小</param>
        /// <param name="keepWidthHeightRate">是否保持元素的宽度高度比率</param>
        /// <returns>修正后的元素大小</returns>
        public static SizeF FixSize(SizeF containerSize, SizeF elementSize, bool keepWidthHeightRate)
        {
            if (elementSize.Width <= 0 || elementSize.Height <= 0)
            {
                // 元素宽度或者高度出现0值
                if (elementSize.Width <= 0)
                {
                    elementSize.Width = Math.Min(containerSize.Width, elementSize.Width);
                }
                if (elementSize.Height <= 0)
                {
                    elementSize.Height = Math.Min(containerSize.Height, elementSize.Height);
                }
                return elementSize;
            }
            if (elementSize.Width > containerSize.Width
                || elementSize.Height > containerSize.Height)
            {
                // 元素的宽度或者高度大于容器则需要进行修正
                if (keepWidthHeightRate)
                {
                    // 计算缩小比例
                    double zoomRate = Math.Min(
                        containerSize.Width / elementSize.Width,
                        containerSize.Height / elementSize.Height);
                    SizeF result = new SizeF(
                        (float)(elementSize.Width * zoomRate),
                        (float)(elementSize.Height * zoomRate));
                    return result;
                }
                else
                {
                    SizeF result = new SizeF(
                        Math.Min(elementSize.Width, containerSize.Width),
                        Math.Min(elementSize.Height, containerSize.Height));
                    return result;
                }
            }
            else
            {
                // 无需修正，返回原值
                return elementSize;
            }
        }

        public static System.Collections.ArrayList GetBorderLines(bool[] Links, System.Drawing.PointF[] points)
        {
            if (Links == null || Links.Length <= 1)
                throw new ArgumentNullException("Links");
            if (points == null || points.Length <= 1)
                throw new ArgumentNullException("points");
            if (Links.Length != points.Length)
                throw new ArgumentException("Array length error.");

            System.Collections.ArrayList list = new System.Collections.ArrayList();
            int len = Links.Length;
            System.Collections.ArrayList CurrentLine = new System.Collections.ArrayList();
            CurrentLine.Add(points[0]);
            bool LastLink = true;
            for (int iCount = 0; iCount < len; iCount++)
            {
                bool link = Links[iCount];
                if (LastLink != link)
                {
                    if (CurrentLine.Count > 1)
                    {
                        list.Add(CurrentLine);
                        CurrentLine = new System.Collections.ArrayList();
                    }
                    else
                    {
                        CurrentLine.Clear();
                    }
                    CurrentLine.Add(points[iCount]);
                }
                if (link)
                {
                    CurrentLine.Add(points[ ( iCount + 1 ) % len ]);
                }
                LastLink = link;
            }
            if (CurrentLine.Count > 1)
            {
                list.Add(CurrentLine);
            }
            for (int iCount = 0; iCount < list.Count; iCount++)
            {
                CurrentLine = (System.Collections.ArrayList)list[iCount];
                list[iCount] = (System.Drawing.PointF[])CurrentLine.ToArray(typeof(System.Drawing.PointF));
            }
            return list;
        }

		/// <summary>
		/// 将一个线段变粗,返回一个多边行
		/// </summary>
		/// <param name="x1">线段起点X坐标</param>
		/// <param name="y1">线段起点Y坐标</param>
		/// <param name="x2">线段终点X坐标</param>
		/// <param name="y2">线段终点Y坐标</param>
		/// <param name="width">线条变化的宽度</param>
		/// <returns>定义多边行4个顶点坐标</returns>
		public static System.Drawing.Point[] ThickLine( int x1 , int y1 , int x2  , int y2 , int width )
		{
			int dx = 0 ;
			int dy = 0 ;
			if( x1 == x2 )
			{
				dx = width ;
			}
			else if( y1 == y2 )
			{
				dy = width ;
			}
			else
			{
				double a = (y2 - y1) / (double)( x2 - x1 );
				double angle = System.Math.Atan( a );
				angle = angle + Math.PI / 2 ;
				dy = ( int ) ( Math.Sin( angle ) * width ) ;
				dx = ( int ) ( Math.Cos( angle ) * width ) ;
			}
			return new System.Drawing.Point[]
				{
					new System.Drawing.Point( x1 - dx , y1 - dy ),
					new System.Drawing.Point( x1 + dx , y1 + dy ),
					new System.Drawing.Point( x2 + dx , y2 + dy ),
					new System.Drawing.Point( x2 - dx , y2 - dy )
				};
		}

		public static System.Drawing.Point[] GetNearestPoint( 
			System.Drawing.Rectangle rect1 ,
			System.Drawing.Rectangle rect2 )
		{
			System.Drawing.Point c1 = new System.Drawing.Point( rect1.Left + rect1.Width / 2 , rect1.Top + rect1.Height / 2 );
			System.Drawing.Point c2 = new System.Drawing.Point( rect2.Left + rect2.Width / 2 , rect2.Top + rect2.Height / 2 );
			System.Drawing.Point[] ps1 = new System.Drawing.Point[]
				{
					new System.Drawing.Point( c1.X , rect1.Top ),
					new System.Drawing.Point( rect1.Right , c1.Y ),
					new System.Drawing.Point( c1.X , rect1.Bottom ),
					new System.Drawing.Point( rect1.X , c1.Y )
				};
			System.Drawing.Point[] ps2 = new System.Drawing.Point[]
				{
					new System.Drawing.Point( c2.X , rect2.Top ),
					new System.Drawing.Point( rect2.Right , c2.Y ),
					new System.Drawing.Point( c2.X , rect2.Bottom ),
					new System.Drawing.Point( rect2.X , c2.Y )
				};
			int min = - 1;
			System.Drawing.Point result1 = System.Drawing.Point.Empty ;
			System.Drawing.Point result2 = System.Drawing.Point.Empty ;
			for( int iCount1 = 0 ; iCount1 < ps1.Length ; iCount1 ++ )
			{
				System.Drawing.Point p1 = ps1[ iCount1 ] ;
				for( int iCount2 = 0 ; iCount2 < ps2.Length ; iCount2 ++ )
				{
					System.Drawing.Point p2 = ps2[ iCount2 ] ;
					int d = ( p1.X - p2.X ) * ( p1.X - p2.X ) + ( p1.Y - p2.Y ) * ( p1.Y - p2.Y );
					if( min == -1 || min > d )
					{
						min = d ;
						result1 = p1 ;
						result2 = p2 ;
					}
				}
			}
			return new System.Drawing.Point[]{ result1 , result2 };
		}

		/// <summary>
		/// 获得数据比例,返回值在0-1.0之间
		/// </summary>
		/// <param name="max">取值区间的最大值</param>
		/// <param name="min">取值区间的最小值</param>
		/// <param name="Value">要计算的数值</param>
		/// <returns>数据在取值区间的比例</returns>
		public static double GetRate( double max , double min , double Value )
		{
			if( double.IsNaN( max ) || double.IsNaN( min ) || double.IsNaN( Value ))
				return 0 ;
			if( max <= min )
				return 0 ;
			if( Value <= min )
				return 0 ;
			if( Value >= max )
				return 1 ;
			return ( Value - min ) / ( max - min );
		}
		/// <summary>
		/// 对若干条线段进行矩形区域剪切处理
		/// </summary>
		/// <remarks>本函数修改点数组,使之包含在矩形区域中,若线段不在矩形区域中,
		/// 则设置起点和终点坐标为( int.MinValue , int.MinValue )</remarks>
		/// <param name="ClipRectangle">剪切矩形</param>
		/// <param name="Lines">线段起点和终点的坐标</param>
		public static void RectangleClipLines( 
			System.Drawing.Rectangle ClipRectangle ,
			System.Drawing.Point[] LinesPoints )
		{
			if( ClipRectangle.IsEmpty )
				throw new System.ArgumentException("ClipRectangle is Empty" , "ClipRectangle");
			if( LinesPoints == null )
				throw new System.ArgumentNullException("LinesPoints");
			if( LinesPoints.Length == 0 )
				throw new System.ArgumentException("LinesPoints is empty" , "LinesPoints");
			// 点数组必须是二的倍数
			if( ( LinesPoints.Length % 2 ) != 0 )
				throw new System.ArgumentException("LinesPoints is error" , "LinesPoints");

			System.Drawing.Point BlankPoint = new System.Drawing.Point( int.MinValue , int.MinValue );

			int left = ClipRectangle.Left ;
			int top = ClipRectangle.Top ;
			int right = ClipRectangle.Right ;
			int bottom = ClipRectangle.Bottom ;

			for( int iCount = 0 ; iCount < LinesPoints.Length ; iCount += 2 )
			{
				System.Drawing.Point p1 = LinesPoints[ iCount ] ;
				System.Drawing.Point p2 = LinesPoints[ iCount + 1 ] ;

				bool c1 = ClipRectangle.Contains( p1 );

				// 若两点重合
				if( p1.Equals( p2 ))
				{
					if( c1 == false )
					{
						LinesPoints[ iCount ] = BlankPoint ;
						LinesPoints[ iCount + 1] = BlankPoint ;
					}
					continue ;
				}
				bool c2 = ClipRectangle.Contains( p2 );
				// 两个端点都在矩形内部则不需要处理
				if( c1 && c2 )
					continue ;

				if( p1.X == p2.X )
				{
					// 垂直线
					if( p1.X >= left && p1.X <= right )
					{
						LinesPoints[ iCount ].Y = FixToRange( p1.Y , top , bottom ) ;
						LinesPoints[ iCount + 1].Y = FixToRange( p2.Y , top , bottom ) ;
					}
					else
					{
						LinesPoints[ iCount ] = BlankPoint ;
						LinesPoints[ iCount + 1] = BlankPoint ;
					}
				}
				else if( p1.Y == p2.Y )
				{
					// 水平线
					if( p1.Y >= top && p1.Y <= bottom )
					{
						LinesPoints[ iCount ].X = FixToRange( p1.X , left , right ) ;
						LinesPoints[ iCount + 1].X = FixToRange( p2.X , left ,  right );
					}
					else
					{
						LinesPoints[ iCount ] = BlankPoint ;
						LinesPoints[ iCount + 1 ] = BlankPoint ;
					}
				}
				else
				{
					// 斜线
					double[] ps = GetLineEquationParameter( p1.X , p1.Y , p2.X , p2.Y );
					//int index = 0 ;
					double a = ps[0] ;
					double b = ps[1] ;

					if( p1.X < left )
					{
						p1.X = left ;
						p1.Y = ( int ) ( a * p1.X + b );
					}
					else if( p1.X > right )
					{
						p1.X = right ;
						p1.Y = ( int ) ( a * p1.X + b );
					}
					if( p1.Y < top )
					{
						p1.Y = top ;
						p1.X = ( int ) ( ( p1.Y - b ) / a );
					}
					else if( p1.Y > bottom )
					{
						p1.Y = bottom ;
						p1.X = ( int ) ( ( p1.Y - b ) / a );
					}

					if( p2.X < left )
					{
						p2.X = left ;
						p2.Y = ( int ) ( a * p2.X + b );
					}
					else if( p2.X > right )
					{
						p2.X = right ;
						p2.Y = ( int ) ( a * p2.X + b );
					}
					if( p2.Y < top )
					{
						p2.Y = top ;
						p2.X = ( int ) ( ( p2.Y - b ) / a );
					}
					else if( p2.Y > bottom )
					{
						p2.Y = bottom ;
						p2.X = ( int ) ( ( p2.Y - b ) / a );
					}

					bool flag = false;
					if( p1.X >= left && p1.X <= right )
					{
						if( p1.Y >= top && p1.Y <= bottom )
						{
							if( p2.X >= left && p2.X <= right )
							{
								if( p2.Y >= top && p2.Y <= bottom )
								{
									flag = true;
								}
							}		
						}
					}
					if( flag )
					{
						LinesPoints[ iCount ] = p1 ;
						LinesPoints[ iCount + 1 ] = p2 ;
					}
					else
					{
						LinesPoints[ iCount ] = BlankPoint ;
						LinesPoints[ iCount + 1 ] = BlankPoint ;
					}
				}
			}//for( int iCount = 0 ; iCount < LinesPoints.Length ; iCount += 2 )
		}

        

		public static int FixToRange( int vValue , int min , int max )
		{
            if (vValue < min)
            {
                return min;
            }
            if (vValue > max)
            {
                return max;
            }
			return vValue ;
		}

		/// <summary>
		/// 获得两个区间的交集
		/// </summary>
		/// <param name="a1">区间1的起点</param>
		/// <param name="b1">区间1的终点</param>
		/// <param name="a2">区间2的起点</param>
		/// <param name="b2">区间2的终点</param>
		/// <returns>区间的交集的起点和终点,若没有交集则返回空引用</returns>
		public static int[] IntersectRange( int a1 , int b1 , int a2 , int b2 )
		{
			int temp = 0 ;
			if( a1 > b1 )
			{
				temp = a1;
				a1 = b1; 
				b1 = temp ;
			}
			if( a2 > b2 )
			{
				temp = a2 ;
				a2 = b2 ;
				b2 = temp ;
			}

			int maxa = Math.Max( a1 , a2 );
			int minb = Math.Min( b1 , b2 );
			if( maxa >= minb )
				return new int[]{ maxa , minb } ;
			else
				return null;
		}

		/// <summary>
		/// 判断两个区间是否存在交集
		/// </summary>
		/// <param name="a1">区间1的起点</param>
		/// <param name="b1">区间1的终点</param>
		/// <param name="a2">区间2的起点</param>
		/// <param name="b2">区间2的终点</param>
		/// <returns>两个区间是否存在交集</returns>
		public static bool IntersectWithRange( int a1 , int b1 , int a2 , int b2 )
		{
			int temp = 0 ;
			if( a1 > b1 )
			{
				temp = a1;
				a1 = b1; 
				b1 = temp ;
			}
			if( a2 > b2 )
			{
				temp = a2 ;
				a2 = b2 ;
				b2 = temp ;
			}

			int maxa = Math.Max( a1 , a2 );
			int minb = Math.Min( b1 , b2 );
			if( maxa >= minb )
				return true ;
			else
				return false;
		}

//		/// <summary>
//		/// 对整数值进行冒泡排序
//		/// </summary>
//		/// <param name="Values">要进行排序的整数值</param>
//		/// <returns>排序后的各个元素的在序列中序号</returns>
//		public static int[] BubbleSort( int[] Values )
//		{
//			int length = Values.Length ;
//			int[] indexs = new int[ length ] ;
//			for(int iCount = 0 ; iCount < length ; iCount ++ )
//			{
//				indexs[ iCount ] = iCount ;
//			}
//			int temp = 0 ;
//			int f = length ;
//			while( f > 0 )
//			{
//				int k = f -1 ;
//				f = 0 ;
//				for( int j = 0 ; j < k ; j ++ )
//				{
//					if( Values[ indexs[ j ] ]>  Values[ indexs[ j + 1 ] ])
//					{
//						temp = indexs[ j ] ;
//						indexs[ j ] = indexs[ j + 1 ] ;
//						indexs[ j + 1 ] = temp ;
//						f = j ;
//					}
//				}
//			}
//			return indexs ;
//		}

		/// <summary>
		/// 从指定点出发获得最近的一个点序号
		/// </summary>
		/// <param name="o">原点</param>
		/// <param name="ps">点坐标数组</param>
		/// <param name="Direction">方向,0:左边 1:上边 2:右边 3:下边</param>
		/// <returns>距离原点最近的点的序号,若没有则返回-1</returns>
		public static int GetNearestPoint(
			System.Drawing.Point o ,
			System.Drawing.Point[] ps ,
			int Direction )
		{
			int MinDis = int.MaxValue ;
			int MinPDis = int.MaxValue ;
			int MinIndex = -1 ;
			for(int iCount = 0 ; iCount < ps.Length ; iCount ++ )
			{
				System.Drawing.Point p = ps[ iCount ];
				if( p.X == o.X && p.Y == o.Y )
					continue ;
				double angle = Angle ( o.X , o.Y , p.X , p.Y );
				bool bolFlag = false;
				int dis = 0 ;
				int pdis = 0 ;
				pdis = ( p.X - o.X ) * ( p.X - o.X ) + ( p.Y - o.Y ) * ( p.Y - o.Y );
				switch( Direction )
				{
					case 0:
						bolFlag = ( angle <= 45 || angle >= 315 );
						dis = p.X - o.X ;
						break;
					case 1:
						bolFlag = ( angle >= 45 && angle <= 135 );
						dis = p.Y - o.Y ;
						break;
					case 2:
						bolFlag = ( angle >= 135 && angle <= 225 );
						dis = p.X - o.X ;
						break;
					case 3:
						bolFlag = ( angle >= 225 && angle <= 315 );
						dis = p.Y - o.Y ;
						break;
				}
				if( bolFlag )
				{
					dis = System.Math.Abs( dis );
					if( dis <= MinDis )
					{
						if( dis < MinDis || pdis < MinPDis )
						{
							MinDis = dis ;
							MinPDis = pdis ;
							MinIndex = iCount ;
						}
					}
				}
			}
			if( MinIndex == - 1 )
			{
				for(int iCount = 0 ; iCount < ps.Length ; iCount ++ )
				{
					System.Drawing.Point p = ps[ iCount ];
					if( p.X == o.X && p.Y == o.Y )
						continue ;
					int dis = -1 ;
					switch( Direction )
					{
						case 0:
							if( p.X > o.X )
								dis = p.X - o.X ;
							break;
						case 1:
							if( p.Y > o.Y )
								dis = p.Y - o.Y ;
							break;
						case 2:
							if( p.X < o.X )
								dis = o.X - p.X ;
							break;
						case 3:
							if( p.Y < o.Y )
								dis = o.Y - p.Y ;
							break;
					}
					if( dis > 0 )
					{
						if( dis < MinDis )
						{
							MinDis = dis ;
							MinIndex = iCount ;
						}
					}
				}
			}
			return MinIndex ;
		}

        ///// <summary>
        ///// 获得圆弧上平均分布的点的坐标
        ///// </summary>
        ///// <param name="x">圆心X坐标</param>
        ///// <param name="y">圆心Y坐标</param>
        ///// <param name="r">圆半径</param>
        ///// <param name="StartAngle">圆弧开始角度</param>
        ///// <param name="EndAngle">圆弧结束角度</param>
        ///// <param name="PointCount">点个数</param>
        ///// <returns>点结构体数组</returns>
        //public static System.Drawing.Point[] GetPiePoints(
        //    System.Drawing.RectangleF bounds ,
        //    double StartAngle , 
        //    double EndAngle , 
        //    int PointCount )
        //{
        //    if( PointCount < 3 )
        //        return null;
        //    System.Drawing.Point[] ps = new System.Drawing.Point[ PointCount ];
        //    XDesignerDrawer.EllipseObject obj = new XDesignerDrawer.EllipseObject( bounds );

        //    double dblStep = ( EndAngle - StartAngle ) / ( PointCount - 1 ) ;
        //    //double cx = bounds.Left + bounds.Width / 2 ;
        //    //double cy = bounds.Top + bounds.Height / 2 ;

        //    for(int iCount = 0 ; iCount < PointCount ; iCount ++ )
        //    {
        //        double angle = StartAngle + dblStep * iCount ;
        //        System.Drawing.PointF p = obj.PeripheraPoint2( angle );
        //        ps[ iCount ].X = ( int ) p.X ;
        //        ps[ iCount ].Y = ( int ) p.Y ;
        //    }
        //    return ps ;
        //}

		/// <summary>
		/// 计算两点间的距离
		/// </summary>
		/// <param name="x1">起点的X坐标</param>
		/// <param name="y1">起点的Y坐标</param>
		/// <param name="x2">终点的X坐标</param>
		/// <param name="y2">终点的Y坐标</param>
		/// <returns>两点的距离</returns>
		public static int Distance( int x1 , int y1 , int x2 , int y2 )
		{
			if( x1 == x2 )
				return ( int) System.Math.Abs( y1 - y2 );
			else if( y1 == y2 )
				return ( int ) System.Math.Abs( x1 - x2 );
			else
				return (int) System.Math.Sqrt( ( x1 - x2 ) * ( x1 - x2 ) + ( y1 - y2 ) * ( y1 - y2 ));
		}
		/// <summary>
		/// 将指定的值修正在指定的区间中
		/// </summary>
		/// <param name="iValue">数值</param>
		/// <param name="iMax">区间最大值</param>
		/// <param name="iMin">区间最小值</param>
		/// <returns>修正后的值</returns>
		public static int FixValue( int iValue , int iMax , int iMin)
		{
			if( iValue > iMax )
				iValue = iMax ;
			if( iValue < iMin )
				iValue = iMin ;
			return iValue;
		}

		public static int[] CalcuteAlignPosition( 
			int[] Widths , 
			int Spacing ,
			int ContentWidth ,
			System.Drawing.StringAlignment Align , 
			bool SplitBlank )
		{
			if( Align == System.Drawing.StringAlignment.Near )
				return CalcuteAlignPosition( Widths , Spacing , ContentWidth , 0 , SplitBlank);
			if( Align == System.Drawing.StringAlignment.Center )
				return CalcuteAlignPosition( Widths , Spacing , ContentWidth , 1 , SplitBlank);
			if( Align == System.Drawing.StringAlignment.Far )
				return CalcuteAlignPosition( Widths , Spacing , ContentWidth , 2 , SplitBlank);
			return null ;
		}

		/// <summary>
		/// 根据宽度值和对齐方式计算位置,参数Align意义为 0:左对齐 1:居中对齐 2:右对齐 3:两边对齐
		/// </summary>
		/// <param name="Widths">各个对象的宽度值</param>
		/// <param name="Spacing">各个对象的间距</param>
		/// <param name="ContentWidth">容器的宽度</param>
		/// <param name="Align">对齐方式</param>
		/// <param name="SplitBlank">是否平均分隔空白</param>
		/// <returns>各个对象的位置</returns>
		public static int[] CalcuteAlignPosition(
			int[] Widths , 
			int Spacing , 
			int ContentWidth , 
			int Align , 
			bool SplitBlank )
		{
			if( Widths.Length == 0 )
				return null;
			
			
			int[] Result = new int[ Widths.Length ];
			if( Widths.Length == 1 )
			{
				if( Align == 0 )
					Result[0] = 0 ;
				else if( Align == 1 )
					Result[0] = ( ContentWidth - Widths[0]) / 2 ;
				else if( Align == 2 )
					Result[0] = ContentWidth - Widths[0];
				else if( Align == 3 )
					Result[0] = ( ContentWidth - Widths[0]) / 2 ;
				return Result ;
			}
			
			int TotalWidth = 0 ;
			for(int iCount = 0 ; iCount < Widths.Length ; iCount ++)
				TotalWidth = TotalWidth + Widths[iCount] + Spacing ;
			TotalWidth -= Spacing ;

			int PosCount = 0 ;

			if( Align == 0 ) // 左对齐
				PosCount = 0 ;
			else if( Align == 1 )// 居中对齐
			{
				if( SplitBlank )
					PosCount = 0 ;
				else
					PosCount = ( ContentWidth - TotalWidth ) / 2 ;
			}
			else if( Align == 2 ) // 右对齐
			{
				if( SplitBlank )
					PosCount = 0 ;
				else
					PosCount = ContentWidth - TotalWidth ;
			}
			else if( Align == 3 ) // 两边对齐
			{
				PosCount = 0 ;
				SplitBlank = true;
			}

			for(int iCount = 0 ; iCount < Widths.Length ; iCount ++)
			{
				Result[iCount] = PosCount ;
				PosCount = PosCount + Widths[ iCount ] + Spacing ;
			}

			if( SplitBlank )
			{
				int TotalDis = ContentWidth - TotalWidth ;
				if( TotalDis > 0 )
				{
					int iStep = (int) System.Math.Ceiling( TotalDis / ( double) ( Widths.Length - 1));
					int StepCount = 0 ;
					for(int iCount = 0 ; iCount < Result.Length ; iCount ++)
					{
						Result[iCount] += StepCount ;
						StepCount += iStep ;
						if( StepCount > TotalDis )
							StepCount = TotalDis ;
					}
				}
			}

			return Result ;
		}//public static int[] CalcuteAlignPosition( int[] Widths , int Spacing , int ContentWidth , int Align )
	
		public static System.Drawing.Color Int32ToColor( int intColor)
		{
			return System.Drawing.Color.FromArgb(
				(intColor & 0xff0000) >> 16  , 
				( intColor & 0xff00)>> 8 , 
				( intColor & 0xff));
		}
		public static int ColorToInt32( System.Drawing.Color Color)
		{
			return Color.ToArgb() & 0xffffff ;
			//return Color.R << 16 + Color.G << 8 + Color.B ;
		}
		/// <summary>
		/// 计算一个点到一个线段或直线的距离,该距离大于等于0,若是计算点到线段的距离且点在线段所在直线的投影点不在该线段则返回-1
		/// </summary>
		/// <param name="x1">线段起点X坐标</param>
		/// <param name="y1">线段起点Y坐标</param>
		/// <param name="x2">线段终点X坐标</param>
		/// <param name="y2">线段终点X坐标</param>
		/// <param name="x">点的X坐标</param>
		/// <param name="y">点的Y坐标</param>
		/// <param name="ShortLine">true:计算点到线段的距离 false:计算点到直线的距离</param>
		/// <returns></returns>
		public static double DistanceToLine(
			double x1 , 
			double y1 , 
			double x2 ,
			double y2 , 
			double x , 
			double y , 
			bool ShortLine)
		{
			// 线段起点和终点重合则参数不正确
			if( x1 == x2 && y1 == y2 )
				return -1 ;
			// 将线段两个端点和指定点组成三角形,计算其边长
			double a = System.Math.Sqrt( ( x-x1) * ( x- x1) + (y-y1) * ( y-y1));
			double b = System.Math.Sqrt( ( x-x2) * ( x- x2) + (y-y2) * ( y-y2));
			//double c = System.Math.Sqrt( ( x1-x2) * ( x1- x2) + (y1-y2) * ( y1-y2));
			// 获得点在线段上的投影点坐标
			double xd = x1 + ( x2 - x1) * a / ( a + b );
			double yd = y1 + ( y2 - y1) * a / ( a + b );

			// 若计算点到线段的距离且投影点在线段外边则返回-1
			if( ShortLine )
			{
				if( x1 != x2 )
				{
					if( ( xd-x1) * (xd-x2)>=0)
						return -1 ;
				}
				else
				{
					if( ( yd-y1) * (yd-y2) >= 0)
						return -1;
				}
			}

			// 计算点和投影点的距离
			double ds = System.Math.Sqrt( (x - xd) * ( x-xd) + ( y-yd)*(y-yd));
			// 指定点和投影点间的距离就是点和线段间的距离
			return ds ;
		}//public static double DistanceToLine( double x1 , double y1 , double x2 , double y2 , double x , double y , bool ShortLine)

		/// <summary>
		/// 计算指定点靠近指定矩形的那个边框,函数返回0,1,2,3分别表示指定点靠近边框的上右下左的边框,若不靠近任何边框则返回-1
		/// </summary>
		/// <remarks>本函数根据矩形的边框所在直线将平面空间划分成9个部分
		/// 
		///    一   |     二       |   三
		/// --------------------------------
		///    八   |     九       |   四
		/// --------------------------------
		///    七   |     六       |   五
		/// 
		/// 只有在区域 2,4,6,8,9中的点才可能靠近矩形边框
		/// </remarks>
		/// <param name="vLeft">矩形区域的左端位置</param>
		/// <param name="vTop">矩形区域的顶端位置</param>
		/// <param name="vWidth">矩形区域的宽度</param>
		/// <param name="vHeight">矩形区域的高度</param>
		/// <param name="x">指定点的X坐标</param>
		/// <param name="y">指定点的Y坐标</param>
		/// <param name="MaxDistance">点到矩形区域边框的最大距离,若点和边框的距离超过该距离则点不算靠近边框,该参数必须大于等于0</param>
		/// <returns>边框的编号</returns>
		public static int CloseWithBorder(
			double vLeft , 
			double vTop , 
			double vWidth , 
			double vHeight ,
			double x ,
			double y , 
			double MaxDistance)
		{
			double vRight = vLeft + vWidth ;
			double vBottom = vTop + vHeight ;
			if( x >= vLeft && x <= vRight )
			{
				// 处于8,9,4区域
				if( System.Math.Abs( x - vLeft ) <= MaxDistance )
					return 3 ;
				if( System.Math.Abs( x - vRight ) <= MaxDistance )
					return 1 ;
			}
			if( y >= vTop && y <= vBottom )
			{
				// 处于2,9,6区域
				if( System.Math.Abs( y - vTop ) <= MaxDistance )
					return 0 ;
				if( System.Math.Abs( y - vBottom ) <= MaxDistance )
					return 2 ;
			}
			return -1 ;
		}//public static int CloseWithBorder()

		/// <summary>
		/// 在一个整数数组查找指定的数据
		/// </summary>
		/// <param name="intValues">整数数组</param>
		/// <param name="intMatch">要查找的数据</param>
		/// <param name="SplitSearch">是否使用二分法查找</param>
		/// <returns>若找到则返回在数组中的序号，否则返回－1</returns>
		public static int SearchInt32Array(
			int[] intValues , 
			int intMatch ,
			bool SplitSearch )
		{
			if( intValues != null)
			{
				int StartIndex = 0 ;
				int EndIndex = intValues.Length -1 ;
				if( intValues.Length < 10 || SplitSearch == false)
				{
					for(int iCount = StartIndex ; iCount <= EndIndex ; iCount ++ )
					{
						if( intMatch == intValues[iCount])
							return iCount ;
					}
				}
				else
				{
					int MiddleIndex = 0 ;
					int Length = EndIndex - StartIndex ;
					while( Length > 10 )
					{
						MiddleIndex = StartIndex + Length / 2 ;
						if( intValues[MiddleIndex] == intMatch )
							return MiddleIndex ;
						else if( intValues[MiddleIndex] > intMatch )
							EndIndex = MiddleIndex ;
						else
							StartIndex = MiddleIndex ;
						Length = EndIndex - StartIndex ;
					}//while
					for(int iCount = StartIndex ; iCount <= EndIndex ; iCount ++ )
					{
						if( intMatch == intValues[iCount])
							return iCount ;
					}//if
				}//else
			}
			return -1 ;
		}//public static int SearchInt32Array()

		/// <summary>
		/// 计算两点的连线和X轴的夹角,返回角度,在0->360之间
		/// </summary>
		/// <param name="x1">第一个点的X坐标</param>
		/// <param name="y1">第一个点的Y坐标</param>
		/// <param name="x2">第二个点的X坐标</param>
		/// <param name="y2">第二个点的Y坐标</param>
		/// <returns>角度 0->360之间 </returns>
		public static double Angle(
			double x1 , 
			double y1 ,
			double x2 , 
			double y2 )
		{
			if( x1 == x2 )
			{
				// 在X坐标上
				if( y2 >= y1 )
					return 90 ; // 在X轴正半轴
				else
					return 270; // 在X轴负半轴
			}
			else
			{
				// 计算弧度
				double dblAngle = System.Math.Atan( ( y2-y1) / ( x2-x1));
				// 将弧度转换为角度
				dblAngle = 180.0 * dblAngle / System.Math.PI ;
				if( x2 >= x1 )
				{
					if( y2 >= y1 )
						return dblAngle ;		// 第一象限
					else
						return dblAngle + 360 ;	// 第4象限
				}
				else
					return dblAngle + 180 ;		// 第二,三象限
			}
		}//public static double Angle

		
		/// <summary>
		/// 设置标志位
		/// </summary>
		/// <param name="intAttributes">原始的标志数据</param>
		/// <param name="intValue">要设置的标志位的数据</param>
		/// <param name="bolSet">是否设置或者清除</param>
		/// <returns>修改后的标志数据</returns>
		public static int SetIntAttribute(int intAttributes , int intValue , bool bolSet)
		{
			return bolSet ? intAttributes | intValue : intAttributes & ~ intValue ;
		}

		/// <summary>
		/// 判断是否设置的标志位
		/// </summary>
		/// <param name="intAttributes">原始的的标志数据</param>
		/// <param name="intValue">需要判断的标志位的数据</param>
		/// <returns>是否进行了设置</returns>
		public static bool GetIntAttribute( int intAttributes , int intValue)
		{
			return ( intAttributes & intValue ) == intValue;
		}

		/// <summary>
		/// 判断指定值是否在某个区间内
		/// </summary>
		/// <param name="x1">区间一个端点</param>
		/// <param name="x2">区间另一个端点</param>
		/// <param name="x">指定的值</param>
		/// <returns>是否在区间内</returns>
		public static bool RangeContains( int x1 , int x2 , int x )
		{
			if( x1 < x2 )
				return ( x >= x1 && x <= x2 );
			else
				return ( x >= x2 && x <= x1 );
		}

		
		/// <summary>
		/// 判断指定值是否在某个区间内
		/// </summary>
		/// <param name="x1">区间一个端点</param>
		/// <param name="x2">区间另一个端点</param>
		/// <param name="x">指定的值</param>
		/// <returns>是否在区间内</returns>
		public static bool RangeContains( double x1 , double x2 , double x )
		{
			if( x1 < x2 )
				return ( x >= x1 && x <= x2 );
			else
				return ( x >= x2 && x <= x1 );
		}

		/// <summary>
		/// 进行逆时针旋转指定弧度的角度处理
		/// </summary>
		/// <param name="o">原点</param>
		/// <param name="p">处理的点</param>
		/// <param name="angle">旋转的角度(弧度)</param>
		/// <returns>处理后的点</returns>
		public static System.Drawing.Point RotatePoint( 
			System.Drawing.Point o , 
			System.Drawing.Point p ,
			double angle )
		{
			if( o.X == p.X && o.Y == p.Y )
				return p ;
			double l = (p.X - o.X) * ( p.X - o.X ) + ( p.Y - o.Y ) * ( p.Y - o.Y );
			l = System.Math.Sqrt( l );
			double alf = System.Math.Atan2( p.Y - o.Y , p.X - o.X );
			alf = alf - angle ;
			System.Drawing.Point p2 = System.Drawing.Point.Empty ;
			p2.X = (int)(o.X + l * System.Math.Cos( alf ));
			p2.Y = (int)(o.Y + l * System.Math.Sin( alf ));
			return p2 ;
		}

		/// <summary>
		/// 进行逆时针旋转指定弧度的角度处理
		/// </summary>
		/// <param name="o">原点</param>
		/// <param name="p">处理的点</param>
		/// <param name="angle">旋转的角度(弧度)</param>
		/// <returns>处理后的点</returns>
		public static System.Drawing.PointF RotatePoint( 
			System.Drawing.PointF o , 
			System.Drawing.PointF p , 
			double angle )
		{
			if( o.X == p.X && o.Y == p.Y )
				return p ;
			double l = (p.X - o.X) * ( p.X - o.X ) + ( p.Y - o.Y ) * ( p.Y - o.Y );
			l = System.Math.Sqrt( l );
			double alf = System.Math.Atan2( p.Y - o.Y , p.X - o.X );
			alf = alf - angle ;
			System.Drawing.PointF p2 = System.Drawing.PointF.Empty ;
			p2.X = (float)(o.X + l * System.Math.Cos( alf ));
			p2.Y = (float)(o.Y + l * System.Math.Sin( alf ));
			return p2 ;
		}


//		/// <summary>
//		/// 根据两点坐标获得方框
//		/// </summary>
//		/// <param name="p1"></param>
//		/// <param name="p2"></param>
//		/// <returns></returns>
//		public static System.Drawing.Rectangle GetRectangle( System.Drawing.Point p1 , System.Drawing.Point p2 )
//		{
//			return GetRectangle( p1.X , p1.Y , p2.X , p2.Y );
//		}
//
//		/// <summary>
//		/// 根据两点坐标获得方框对象
//		/// </summary>
//		/// <param name="x1"></param>
//		/// <param name="y1"></param>
//		/// <param name="x2"></param>
//		/// <param name="y2"></param>
//		/// <returns></returns>
//		public static System.Drawing.Rectangle GetRectangle( int x1 , int y1 , int x2 , int y2)
//		{
//			System.Drawing.Rectangle myRect = System.Drawing.Rectangle.Empty ;
//			if( x1 < x2)
//			{
//				myRect.X 		= x1 ;
//				myRect.Width 	= x2 - x1 ;
//			}
//			else
//			{
//				myRect.X 		= x2;
//				myRect.Width	= x1 - x2 ;
//			}
//			if( y1 < y2)
//			{
//				myRect.Y 		= y1;
//				myRect.Height	= y2 - y1 ;
//			}
//			else
//			{
//				myRect.Y 		= y2;
//				myRect.Height	= y1 - y2;
//			}
//			if( myRect.Width < 0) myRect.Width = 0 ;
//			if( myRect.Height < 0) myRect.Height = 0 ;
//			return myRect ;
//		}

        /// <summary>
        /// 获得一个线段和矩形的交点
        /// </summary>
        /// <param name="rect">矩形区域</param>
        /// <param name="x1">线段起点X坐标</param>
        /// <param name="y1">线段起点Y坐标</param>
        /// <param name="x2">线段终点X坐标</param>
        /// <param name="y2">线段终点Y坐标</param>
        /// <returns>包含交点的点坐标数组</returns>
        public static System.Drawing.Point[] LineIntersects(
            System.Drawing.Rectangle rect,
            int x1, 
            int y1, 
            int x2, 
            int y2)
        {
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            System.Drawing.Point[] ps = new System.Drawing.Point[] { 
                new System.Drawing.Point( rect.Left , rect.Top ),
                new System.Drawing.Point( rect.Right , rect.Top ),
                new System.Drawing.Point( rect.Right , rect.Bottom ),
                new System.Drawing.Point( rect.Left , rect.Bottom ),
                new System.Drawing.Point( rect.Left , rect.Top )
                };
            for (int iCount = 0; iCount <= ps.Length - 2; iCount++)
            {
                System.Drawing.Point p = LineIntersectPoint(
                    ps[iCount].X,
                    ps[iCount].Y,
                    ps[ iCount + 1].X ,
                    ps[ iCount + 1 ].Y ,
                    x1, 
                    y1 , 
                    x2 , 
                    y2 ,
                    false );
                if (p.X != int.MaxValue && p.Y != int.MinValue)
                {
                    list.Add(p);
                }
            }
            return (System.Drawing.Point[])list.ToArray(typeof(System.Drawing.Point));
        }

        public static bool LineIntersectsWith(
			System.Drawing.Rectangle rect ,
			int x1 ,
			int y1 ,
			int x2 ,
			int y2)
		{
			if( rect.Contains( x1 ,y1 )) return true;
			if( rect.Contains( x2 , y2 )) return true;

			if( LineIntersectsWith(
				x1 ,
				y1 ,
				x2 ,
				y2 , 
				rect.Left ,
				rect.Top , 
				rect.Right , 
				rect.Top ))
				return true;

			if( LineIntersectsWith(
				x1 ,
				y1 ,
				x2 ,
				y2 , 
				rect.Right ,
				rect.Top ,
				rect.Right ,
				rect.Bottom ))
				return true;

			if( LineIntersectsWith(
				x1 ,
				y1 ,
				x2 ,
				y2 ,
				rect.Right ,
				rect.Bottom ,
				rect.Left ,
				rect.Bottom )) 
				return true;

			if( LineIntersectsWith(
				x1 ,
				y1 ,
				x2 , 
				y2 , 
				rect.Left , 
				rect.Bottom , 
				rect.Left , 
				rect.Top ))
				return true;

			return false ;
		}
		/// <summary>
		/// 判断两个线段是否相交
		/// </summary>
		/// <param name="x1">线段1的起点坐标</param>
		/// <param name="y1">线段1的起点坐标</param>
		/// <param name="x2">线段1的终点坐标</param>
		/// <param name="y2">线段1的终点坐标</param>
		/// <param name="x3">线段2的起点坐标</param>
		/// <param name="y3">线段2的起点坐标</param>
		/// <param name="x4">线段2的终点坐标</param>
		/// <param name="y4">线段2的终点坐标</param>
		/// <returns>true:两线段相交 false:两线段不相交</returns>
		public static bool LineIntersectsWith( 
			int x1 ,
			int y1 ,
			int x2 , 
			int y2 , 
			int x3 ,
			int y3 , 
			int x4 , 
			int y4)
		{
			System.Drawing.Point p = LineIntersectPoint( 
				x1 ,
				y1 ,
				x2 ,
				y2 ,
				x3 ,
				y3 ,
				x4 ,
				y4 , 
				false );

			if( p.X == int.MinValue && p.Y == int.MinValue )
				return false;
			else
			{
				return true;
			}
		}

		/// <summary>
		/// 判断两个区间是否相交
		/// </summary>
		/// <param name="a1">区间1起点</param>
		/// <param name="b1">区间1终点</param>
		/// <param name="a2">区间2起点</param>
		/// <param name="b2">区间2终点</param>
		/// <returns>是否相交</returns>
		public static bool RangeIntersect( int a1 , int b1 , int a2 , int b2 )
		{
			if( a1 <= b2 && a2 <= b1 )
				return true;
			else
				return false;
		}

		/// <summary>
		/// 获得直线的方程参数
		/// </summary>
		/// <param name="x1">线段的起点坐标</param>
		/// <param name="y1">线段的起点坐标</param>
		/// <param name="x2">线段的终点坐标</param>
		/// <param name="y2">线段的终点坐标</param>
		/// <returns>方程参数组成的数组</returns>
		/// <remarks>
		/// 一个直线的方程为 y = a * x + b ,本函数就是根据两个点的坐标计算出
		/// 直线方程的参数 a 和 b , 并将其放置到一个数组中.若直线垂直,则直线
		/// 方程就变成了 x = b ,此时函数返回一个空引用.
		/// </remarks>
		public static double[] GetLineEquationParameter( double x1 , double y1 , double x2 , double y2 )
		{
			if( double.IsNaN( x1 ))
				throw new System.ArgumentException("x1 is Nan");
			if( double.IsNaN( y1 ))
				throw new System.ArgumentException("y1 is Nan");
			if( double.IsNaN( x2 ))
				throw new System.ArgumentException("x2 is Nan");
			if( double.IsNaN( y2 ))
				throw new System.ArgumentException("y2 is Nan");

			if( x1 != x2 )
			{
				double a = ( y2 - y1 ) / ( x2 - x1 );
				double b = y1 - a * x1 ;
				return new double[]{ a , b };
			}
			else
				return null;
		}

		/// <summary>
		/// 获得两直线的交点,
        /// 若两线段无交点则返回(int.MinValue,int.MinValue),
        /// 若两线重合则返回(int.MaxValue,int.MaxValue)
		/// </summary>
		/// <param name="x1">线段1的起点坐标</param>
		/// <param name="y1">线段1的起点坐标</param>
		/// <param name="x2">线段1的终点坐标</param>
		/// <param name="y2">线段1的终点坐标</param>
		/// <param name="x3">线段2的起点坐标</param>
		/// <param name="y3">线段2的起点坐标</param>
		/// <param name="x4">线段2的终点坐标</param>
		/// <param name="y4">线段2的终点坐标</param>
		/// <param name="Beeline">若交点不在线段上，是否延长线段</param>
		/// <returns>两条线的交点</returns>
		public static System.Drawing.Point LineIntersectPoint( 
			int x1 ,
			int y1 ,
			int x2 , 
			int y2 , 
			int x3 , 
			int y3 , 
			int x4 , 
			int y4 , 
			bool Beeline )
		{
			double x = 0 , y = 0 ;
			double a1 = 0 , a2 = 0 , b1 = 0  , b2 = 0 ;
			if( x1 != x2 )
			{
				a1 = (y2 - y1) /(double)( x2 - x1 );
				b1 = y1 - a1 * x1 ;
			}
			if( x3 != x4 )
			{
				a2 = (y4 - y3) /(double)( x4 - x3 );
				b2 = y3 -a2 * x3 ;
			}
			if( x1 == x2 )
			{
				if( x3 == x4) // 两直线都垂直,因此两直线平行
				{
					if( x1 == x3 )
					{
						if( Beeline )
						{
							if( RangeIntersect( y1 , y2 , y3 , y4 ))
								return new System.Drawing.Point( int.MaxValue , int.MaxValue );
							else
								return new System.Drawing.Point( int.MinValue , int.MinValue );
						}
						return new System.Drawing.Point( int.MaxValue , int.MaxValue );
					}
					else
						return new System.Drawing.Point( int.MinValue , int.MinValue );
				}
				else
				{
					x = x1 ;
					y = a2 * x + b2 ;
				}
			}
			else
			{
				if( x3 == x4 )
				{
                    // 第二条线段垂直
					x = x3 ;
					y = a1 * x + b1 ;
                    //if (y < Math.Min(y3, y4) || y > Math.Max(y3, y4))
                    //{
                    //    if (Beeline)
                    //    {
                    //        return new System.Drawing.Point((int)x, (int)y);
                    //    }
                    //    else
                    //    {
                    //        return new System.Drawing.Point(int.MinValue, int.MinValue);
                    //    }
                    //}
				}
				else
				{
					if( a1 == a2 )
					{
						// 斜率相等,两直线平行
                        if (b1 == b2)
                        {
                            if (Beeline)
                            {
                                if (RangeIntersect(x1, x2, x3, x4))
                                    return new System.Drawing.Point(int.MaxValue, int.MaxValue);
                                else
                                    return new System.Drawing.Point(int.MinValue, int.MinValue);
                            }
                            return new System.Drawing.Point(int.MaxValue, int.MaxValue);
                        }
                        else
                        {
                            return new System.Drawing.Point(int.MinValue, int.MinValue);
                        }
					}
					else
					{
						x = ( b2 - b1 ) / ( a1 - a2 );
						y = a1 * x  + b1 ;
					}
				}
			}
            if (Beeline == false )
            {
                // 交点不在线段上
                if (RangeContains(x1, x2, x)
                    && RangeContains(x3, x4, x)
                    && RangeContains(y1, y2, y)
                    && RangeContains(y3, y4, y)
                    )
                    return new System.Drawing.Point((int)x, (int)y);
                else
                    return new System.Drawing.Point(int.MinValue, int.MinValue);
            }
            else
            {
                return new System.Drawing.Point((int)x, (int)y);
            }
		}//public System.Drawing.Point LineIntersectPoint( int x1 , int y1 , int x2 , int y2 , int x3 , int y3 , int x4 , int y4 , bool Beeline )

		private MathCommon(){}
	}//public class MathCommon
}