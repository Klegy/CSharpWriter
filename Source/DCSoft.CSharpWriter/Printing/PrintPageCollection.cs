/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using DCSoft.Common;

namespace DCSoft.Printing
{
	/// <summary>
	/// 打印页集合对象
	/// </summary>
    [DocumentComment()]
    [Serializable()]
	public class PrintPageCollection : System.Collections.CollectionBase
	{
		private int intTop = 0 ;
         

		/// <summary>
		/// 自定义绘制页眉页脚
		/// </summary>
		protected bool bolCustomDrawHeadFooter = false;
		/// <summary>
		/// 自定义绘制页眉页脚
		/// </summary>
		public bool CustomDrawHeadFooter
		{
			get{ return bolCustomDrawHeadFooter ;}
			set{ bolCustomDrawHeadFooter = value;}
		}
         
		public PrintPageCollection()
		{
		}
 
		private System.Drawing.GraphicsUnit intGraphicsUnit 
			= System.Drawing.GraphicsUnit.Document ;
		/// <summary>
		/// 度量单位
		/// </summary>
		public System.Drawing.GraphicsUnit GraphicsUnit
		{
			get{ return intGraphicsUnit ;}
			set{ intGraphicsUnit = value;}
		}

        /// <summary>
        /// 准备清空列表
        /// </summary>
        protected override void OnClear()
        {
            foreach (PrintPage page in this)
            {
                page.Document = null;
                page.OwnerPages = null;
                page.PageSettings = null;
            }
            base.OnClear();
        }
         

        private int intMinPageHeight = 50;
		/// <summary>
		/// 最小页高
		/// </summary>
		public int MinPageHeight
		{
			get
            {
                return intMinPageHeight ;
            }
			set
            {
                intMinPageHeight = value;
            }
		}
         
		/// <summary>
		/// 对象的顶端位置
		/// </summary>
		public int Top 
		{
			get{ return intTop ;}
			set{ intTop = value;}
		}
        
		public bool ContainsTop( int vTop )
		{
			int iCount = intTop ;
			foreach( PrintPage page in this)
			{
				iCount = iCount + page.Height ;
				if( iCount > vTop )
					return true;
			}
			return false;
		}

		/// <summary>
		/// 获得从0开始的序号
		/// </summary>
		/// <param name="myPage"></param>
		/// <returns></returns>
		public int IndexOf( PrintPage myPage)
		{
			return this.InnerList.IndexOf( myPage);
		}
         
		/// <summary>
		/// 获得第一页
		/// </summary>
		public PrintPage FirstPage 
		{
			get
			{
				if( this.Count > 0 )
					return this[0];
				else
					return null;
			}
		}
		/// <summary>
		/// 获得最后一页
		/// </summary>
		public PrintPage LastPage
		{
			get
			{
				if( this.Count > 0)
					return  this[ this.Count -1 ];
				else
					return null;
			}
		}
         

		/// <summary>
		/// 添加页对象
		/// </summary>
		/// <param name="myPage"></param>
		public int Add( PrintPage myPage)
		{
            return this.List.Add(myPage);
            //if( ! this.InnerList.Contains( myPage ) )
            //    this.InnerList.Add( myPage );
		}

        public void AddRange(PrintPageCollection pages)
        {
            this.InnerList.AddRange(pages);
        }
		/// <summary>
		/// 删除指定页
		/// </summary>
		/// <param name="myPage"></param>
		public void Remove( PrintPage myPage)
		{
			this.List.Remove( myPage );
		}

		/// <summary>
		/// 判断是否存在指定的页对象
		/// </summary>
		/// <param name="page">页对象</param>
		/// <returns>是否存在页对象</returns>
		public bool Contains( PrintPage page )
		{
			return this.List.Contains( page );
		}
		/// <summary>
		/// 获得最大的页宽
		/// </summary>
		/// <returns></returns>
		public int GetPageMaxWidth()
		{
			int MaxWidth = 0 ;
			foreach(PrintPage myPage in this)
			{
				if( myPage.Width > MaxWidth )
					MaxWidth = myPage.Width ;
			}
			return MaxWidth  ;
		}

		/// <summary>
		/// 所有页面的高度
		/// </summary>
		public int Height
		{
			get
			{
				int intHeight = 0 ;
				foreach( PrintPage myPage in this )
				{
					intHeight += myPage.Height ;
				}
				return intHeight ;
			}
		}
        
		/// <summary>
		/// 返回指定的从0开始的序号的页对象
		/// </summary>
		public PrintPage this[int index]
		{
			get{ return ( PrintPage ) this.List[ index ];}
		}
		/// <summary>
		/// 返回指定位置处的页面对象
		/// </summary>
		/// <param name="y">指定的垂直位置</param>
		/// <returns>页面对象</returns>
		public PrintPage GetPage( int y )
		{
			foreach( PrintPage page in this )
			{
				if( y >= page.ClientBounds.Top && y <= page.ClientBounds.Bottom )
					return page;
			}
			return null;
		}

        public PrintPage GetPageByViewPosition(int y)
        {
            foreach (PrintPage page in this)
            {
                if (y >= page.Top && y < page.Bottom)
                    return page;
            }
            return null;
        }
	}
}