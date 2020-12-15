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
using System.Collections.Generic;

namespace DCSoft.Drawing
{
	/// <summary>
	/// 复合矩形区域坐标转换关系列表
	/// </summary>
	public class MultiRectangleTransform : TransformBase , System.Collections.ICollection
	{
		/// <summary>
		/// 无作为的初始化对象
		/// </summary>
		public MultiRectangleTransform()
		{
            intItemsVersion = intGlobalItemsVersion ++;
		}

        protected List<SimpleRectangleTransform> myItems = new List<SimpleRectangleTransform>();

        private static int intGlobalItemsVersion = 0;

        private int intItemsVersion = 0;
        public int ItemsVersion
        {
            get { return intItemsVersion; }
        }

//		protected bool bolEnable = true;
//		/// <summary>
//		/// 对象是否可用
//		/// </summary>
//		public bool Enable
//		{
//			get{ return bolEnable ;}
//			set{ bolEnable = value;}
//		}
//		protected int intOffsetX = 0 ;
//		/// <summary>
//		/// 横向位置修正量
//		/// </summary>
//		public int OffsetX
//		{
//			get{ return intOffsetX ;}
//			set{ intOffsetX = value;}
//		}
//		protected int intOffsetY = 0 ;
//		/// <summary>
//		/// 纵向位置修正量
//		/// </summary>
//		public int OffsetY
//		{
//			get{ return intOffsetY ;}
//			set{ intOffsetY = value;}
//		}
//		/// <summary>
//		/// 位置修正量
//		/// </summary>
//		public System.Drawing.Point Offset
//		{
//			get
//			{
//				return new System.Drawing.Point( 
//					this.intOffsetX ,
//					this.intOffsetY ); 
//			}
//			set
//			{
//				this.intOffsetX = value.X ;
//				this.intOffsetY = value.Y ;
//			}
//		}

//		protected bool bolCheckEnable = false;
//		/// <summary>
//		/// 是否检查转换项目是否有效
//		/// </summary>
//		public bool CheckEnable
//		{
//			get{ return bolCheckEnable ;}
//			set{ bolCheckEnable = value;}
//		}
		protected double dblRate = 1 ;
		/// <summary>
		/// 缩放比例
		/// </summary>
		public double Rate
		{
			get{ return dblRate ;}
			set{ dblRate = value;}
		}

		protected System.Drawing.Point mySourceOffsetBack = System.Drawing.Point.Empty ;
        /// <summary>
        /// 移动所有的来源矩形
        /// </summary>
        /// <param name="dx">X轴移动量</param>
        /// <param name="dy">Y轴移动量</param>
        /// <param name="Remark">是否记录</param>
		public void OffsetSource( int dx , int dy , bool Remark )
		{
            if (dx != 0 || dy != 0)
            {
                if (Remark)
                {
                    mySourceOffsetBack.Offset(dx, dy);
                }
                foreach (SimpleRectangleTransform item in this)
                {
                    System.Drawing.RectangleF rect = item.SourceRectF;
                    rect.Offset(dx, dy);
                    item.SourceRectF = rect;

                    Rectangle rect2 = item._PartialAreaSourceBounds;
                    rect2.Offset(dx, dy);
                    item._PartialAreaSourceBounds = rect2;
                }//foreach
            }
		}

		public void ClearSourceOffset()
		{
			if( mySourceOffsetBack.IsEmpty == false )
			{
				foreach( SimpleRectangleTransform item in this )
				{
					System.Drawing.RectangleF rect = item.SourceRectF ;
					rect.Offset( - mySourceOffsetBack.X , - mySourceOffsetBack.Y );
					item.SourceRectF = rect ;

                    Rectangle  rect2 = item._PartialAreaSourceBounds;
                    rect2.Offset(-mySourceOffsetBack.X, -mySourceOffsetBack.Y);
                    item._PartialAreaSourceBounds = rect2;

				}
			}
			mySourceOffsetBack = System.Drawing.Point.Empty ;
		}
		
//		public void OffsetDesc( int dx , int dy )
//		{
//			foreach( SimpleRectangleTransform item in this )
//			{
//				System.Drawing.Rectangle rect = item.DescRect ;
//				rect.Offset( dx , dy );
//				item.DescRect = rect ;
//			}
//		}

		public System.Drawing.Rectangle SourceBounds
		{
			get
			{
				System.Drawing.Rectangle rect = System.Drawing.Rectangle.Empty ;
				foreach( SimpleRectangleTransform item in this )
				{
					if( rect.IsEmpty )
						rect = item.SourceRect ;
					else
						rect = System.Drawing.Rectangle.Union( rect , item.SourceRect );
				}
				return rect ;
			}
		}

		public System.Drawing.Rectangle DescBounds
		{
			get
			{
				System.Drawing.Rectangle rect = System.Drawing.Rectangle.Empty ;
				foreach( SimpleRectangleTransform item in this )
				{
					if( rect.IsEmpty )
						rect = item.DescRect ;
					else
						rect = System.Drawing.Rectangle.Union( rect , item.DescRect );
				}
				return rect ;
			}
		}

		/// <summary>
		/// 返回指定序号的转换对应关系
		/// </summary>
		public SimpleRectangleTransform this[ int index ]
		{
			get{ return ( SimpleRectangleTransform ) myItems[ index ] ; }
		}
		/// <summary>
		/// 获得具有指定原始区域矩形边框的转换对应关系,若未找到则返回空引用
		/// </summary>
		public SimpleRectangleTransform this[ System.Drawing.Rectangle rect ]
		{
			get
			{
				foreach( SimpleRectangleTransform item in this )
				{
					if( item.SourceRect.Equals( rect ))
						return item ;
				}
				return null;
			}
		}
		/// <summary>
		/// 获得指定点所在的对应关系
		/// </summary>
		public SimpleRectangleTransform this[ int x , int y ]
		{
			get
			{
                foreach (SimpleRectangleTransform item in this)
                {
                    if (item.SourceRect.Contains(x, y) && item.Enable)
                    {
                        return item;
                    }
                }
				return null;
			}
		}
		/// <summary>
		/// 获得指定点所在的对应关系
		/// </summary>
		public SimpleRectangleTransform this[ System.Drawing.Point p ]
		{
			get
            {
                return this[ p.X , p.Y ];
            }
		}

        public SimpleRectangleTransform GetByDescPoint(float x, float y)
        {
            return GetItemByPoint(x, y, false , false , false );
        }

        public SimpleRectangleTransform GetByDescPointAbsolute( float x , float y )
        {
            return GetItemByPoint(x , y , false, true , false );
        }

        public SimpleRectangleTransform GetBySourcePoint(float x, float y)
        {
            return GetItemByPoint(x, y, true, false , false );
        }

        public SimpleRectangleTransform GetBySourcePointAbsolute(float x, float y)
        {
            return GetItemByPoint(x, y, true, true , false );
        }

        /// <summary>
        /// 根据转换后的坐标位置查找转换信息对象
        /// </summary>
        /// <param name="x">点X坐标</param>
        /// <param name="y">点Y坐标</param>
        /// <param name="compatibility">是否启用兼容模式。若启用兼容模式，
        /// 如果没有找到和指定点精确匹配的坐标转换信息对象，
        /// 则尽量查找距离点最近的坐标转换信息对象</param>
        /// <param name="onlyIncludeEnabledItem">只对可用的对象进行处理</param>
        /// <param name="useSourceRect">模式，True:匹配源矩形；False:匹配目标矩形。</param>
        /// <returns>找到的坐标转换信息对象</returns>
        public SimpleRectangleTransform GetItemByPoint(
            float x,
            float y,
            bool useSourceRect ,
            bool compatibility,
            bool onlyIncludeEnabledItem )
        {
            if (this.Count == 0)
            {
                // 列表为空，没法获得值
                return null;
            }
            foreach (SimpleRectangleTransform item in this.myItems)
            {
                if (onlyIncludeEnabledItem && item.Enable == false)
                {
                    continue;
                }

                if (useSourceRect)
                {
                    if (item.SourceRectF.Contains(x, y))
                    {
                        return item;
                    }
                }
                else
                {
                    if (item.DescRectF.Contains(x, y))
                    {
                        return item;
                    }
                }
            }//foreach
            if (compatibility)
            {
                // 寻找距离最近的目标矩形区域
                float minLen = 0;
                int index = 0;
                for (int iCount = 0; iCount < this.myItems.Count; iCount++)
                {
                    SimpleRectangleTransform item = myItems[iCount];
                    if (onlyIncludeEnabledItem && item.Enable == false)
                    {
                        continue;
                    }

                    RectangleF rect = useSourceRect ? item.SourceRectF : item.DescRectF;
                    if (rect.Contains(x, y))
                    {
                        return myItems[iCount];
                    }
                    float len = RectangleCommon.GetDistance(x, y, rect);
                    if ( iCount == 0 || len < minLen)
                    {
                        minLen = len;
                        index = iCount;
                    }
                }//for
                return myItems[index];
            }
            else
            {
                return null;
            }
        }

		public int Add( SimpleRectangleTransform item )
		{
            intItemsVersion = intGlobalItemsVersion++;
			myItems.Add( item );
            return myItems.Count - 1;
		}
		/// <summary>
		/// 添加一个转换关系
		/// </summary>
		/// <param name="SourceRect">原始区域矩形边框</param>
		/// <param name="DescRect">目标区域矩形边框</param>
		/// <remarks>新增的转换关系</remarks>
		public SimpleRectangleTransform Add( 
			System.Drawing.Rectangle SourceRect ,
			System.Drawing.Rectangle DescRect )
		{
            intItemsVersion = intGlobalItemsVersion++;
			SimpleRectangleTransform NewItem = new SimpleRectangleTransform( SourceRect , DescRect );
			myItems.Add( NewItem );
			return NewItem ;
		}
		/// <summary>
		/// 添加一个转换关系
		/// </summary>
		/// <param name="SourceLeft">原始区域边框左端位置</param>
		/// <param name="SourceTop">原始区域边框顶端位置</param>
		/// <param name="SourceWidth">原始区域边框宽度</param>
		/// <param name="SourceHeight">原始区域边框高度</param>
		/// <param name="DescLeft">目标区域边框左端位置</param>
		/// <param name="DescTop">目标区域边框顶端位置</param>
		/// <param name="DescWidth">目标区域边框宽度</param>
		/// <param name="DescHeight">目标区域边框高度</param>
		public SimpleRectangleTransform Add( 
			int SourceLeft , 
			int SourceTop , 
			int SourceWidth , 
			int SourceHeight , 
			int DescLeft , 
			int DescTop , 
			int DescWidth , 
			int DescHeight )
		{
            intItemsVersion = intGlobalItemsVersion++;
			SimpleRectangleTransform NewItem = new SimpleRectangleTransform( 
				new System.Drawing.Rectangle( SourceLeft , SourceTop , SourceWidth , SourceHeight ) ,
				new System.Drawing.Rectangle( DescLeft , DescTop , DescWidth , DescHeight ));
			myItems.Add( NewItem );
			return NewItem ;
		}

		/// <summary>
		/// 判断指定坐标的点是否有相应的原始区域
		/// </summary>
		/// <param name="x">点X坐标</param>
		/// <param name="y">点Y坐标</param>
		/// <returns>是否有相应的原始区域</returns>
		public override bool ContainsSourcePoint( int x , int y )
		{
			return this[ x , y ] != null;
		}
		/// <summary>
		/// 判断指定坐标的点是否有相应的原始区域
		/// </summary>
		/// <param name="p">点坐标</param>
		/// <returns>是否有相应的原始区域</returns>
		public bool Contains( System.Drawing.Point p )
		{
			return this[ p.X , p.Y ]!= null;
		}

		public int TransformY( int y )
		{
			foreach( SimpleRectangleTransform item in this )
			{
				if( item.Enable )
				{
					System.Drawing.Rectangle rect = item.SourceRect ;
					if( y >= rect.Top && y <= rect.Bottom )
					{
						return item.TransformPoint( rect.Left , y ).Y ;
					}
				}
			}
			return y ;
		}
		public int UnTransformY( int y )
		{
			foreach( SimpleRectangleTransform item in this )
			{
				if( item.Enable )
				{
					System.Drawing.Rectangle rect = item.DescRect ;
					if( y >= rect.Top && y <= rect.Bottom )
					{
						return item.UnTransformPoint( item.DescRect.Left , y ).Y ;
					}
				}
			}
			return 0 ;
		}

		/// <summary>
		/// 将原始点根据命中的转换关系转换为目标区域中的点坐标,若未相应的转换关系则返回原始坐标值
		/// </summary>
		/// <param name="p">原始点坐标</param>
		/// <returns>处理后的目标点坐标</returns>
		public System.Drawing.Point Transform( System.Drawing.Point p )
		{
			return TransformPoint( p.X , p.Y );
		}

		public override System.Drawing.Point TransformPoint(int x, int y)
		{
			foreach( SimpleRectangleTransform item in this )
			{
				if( item.Enable && item.SourceRect.Contains( x , y  ) )
					return item.TransformPoint( x , y );
			}
			return System.Drawing.Point.Empty ;
			//return new System.Drawing.Point( x , y );
		}


		public override System.Drawing.Size TransformSize(int w, int h)
		{
			return new System.Drawing.Size( ( int ) ( w * dblRate ) , ( int ) ( h * dblRate ));
		}
		public override System.Drawing.Size TransformSize(System.Drawing.Size vSize)
		{
			return new System.Drawing.Size( ( int ) ( vSize.Width * dblRate ) , ( int ) ( vSize.Height * dblRate ));
		}
		public override System.Drawing.SizeF TransformSizeF(float w, float h)
		{
			return new System.Drawing.SizeF( ( float ) ( w * dblRate ) , ( float ) ( h * dblRate ));
		}
		public override System.Drawing.SizeF TransformSizeF(System.Drawing.SizeF vSize)
		{
			return new System.Drawing.SizeF( ( float ) ( vSize.Width * dblRate ) , ( float ) ( vSize.Height * dblRate ));
		}

		public override System.Drawing.Point UnTransformPoint(int x, int y)
		{
			System.Drawing.Point p  = System.Drawing.Point.Empty ;
			foreach( SimpleRectangleTransform item in this )
			{
				if( item.Enable && item.DescRect.Contains( x , y ))
				{
					p = item.UnTransformPoint( x , y );
					return p ;
				}
			}
			return p ;
		}

		public override System.Drawing.PointF TransformPointF(float x, float y)
		{
			foreach( SimpleRectangleTransform item in this )
			{
				if( item.SourceRectF.Contains( x ,y  ) && item.Enable )
					return item.TransformPointF( x , y );
			}
			return new System.Drawing.PointF( x , y );
		}

		public override System.Drawing.PointF UnTransformPointF(float x, float y)
		{
			System.Drawing.PointF p  = System.Drawing.PointF.Empty ;
			foreach( SimpleRectangleTransform item in this )
			{
				if( item.DescRectF.Contains( x , y ) && item.Enable )
				{
					p = item.UnTransformPointF( x , y );
					break;
				}
			}
			return p ;	
		}

		public override System.Drawing.Size UnTransformSize(int w, int h)
		{
			return new System.Drawing.Size( ( int ) ( w / dblRate ) , ( int ) ( h / dblRate ));
		}
		public override System.Drawing.Size UnTransformSize(System.Drawing.Size vSize)
		{
			return new System.Drawing.Size( ( int ) ( vSize.Width / dblRate ) , ( int ) ( vSize.Height / dblRate ));
		}
		public override System.Drawing.SizeF UnTransformSizeF(float w, float h)
		{
			return new System.Drawing.SizeF( ( float ) ( w / dblRate ) , ( float ) ( h / dblRate ));
		}
		public override System.Drawing.SizeF UnTransformSizeF(System.Drawing.SizeF vSize)
		{
			return new System.Drawing.SizeF( ( float ) ( vSize.Width / dblRate ) , ( float ) ( vSize.Height / dblRate ));
		}





		/// <summary>
		/// 当前转换关系项目
		/// </summary>
		protected SimpleRectangleTransform myCurrentItem = null;
		/// <summary>
		/// 当前转换关系项目
		/// </summary>
		public SimpleRectangleTransform CurrentItem
		{
			get{ return myCurrentItem ;}
		}

		public void Clear()
		{
            intItemsVersion = intGlobalItemsVersion++;
			myItems.Clear();
		}

		#region ICollection 成员

		public bool IsSynchronized
		{
			get
			{
                return false;
			}
		}

		public int Count
		{
			get
			{
				return myItems.Count ;
			}
		}

		public void CopyTo(Array array, int index)
		{
            throw new NotSupportedException("CopyTo");
		}

		public object SyncRoot
		{
			get
			{
                return null;
			}
		}

		#endregion

		#region IEnumerable 成员

		public System.Collections.IEnumerator GetEnumerator()
		{
			return myItems.GetEnumerator();
		}

		#endregion
	}//public class RectangleTransform : System.Collections.CollectionBase
}