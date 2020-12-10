/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;

namespace DCSoft.Printing
{
	/// <summary>
	/// 页面区域信息
	/// </summary>
	public class PageRangeInfo
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public PageRangeInfo()
		{
		}

		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="range">包含页面区域信息的文本</param>
		public PageRangeInfo( string range )
		{
			Parse( range );
		}

		private int intFromPage = 0 ;
		/// <summary>
		/// 获取或设置要打印的第一页的从0开始计算的页码。 
		/// </summary>
		public int FromPage
		{
			get
			{
				return intFromPage ;
			}
			set
			{
				intFromPage = value;
				CheckData();
			}
		}

		private int intToPage = 0 ;
		/// <summary>
		/// 获取或设置要打印的最后一页的从0开始计算的页码。
		/// </summary>
		public int ToPage
		{
			get
			{
				return intToPage ;
			}
			set
			{
				intToPage = value;
				CheckData();
			}
		}

		private void CheckData()
		{
			if( intFromPage < 0 )
				intFromPage = - intFromPage ;
			if( intToPage < 0 )
				intToPage = - intToPage ;
			if( intFromPage > intToPage )
			{
				int temp = intFromPage ;
				intFromPage = intToPage ;
				intToPage = temp ;
			}
		}
		/// <summary>
		/// 判断指定的页码属于页码区域
		/// </summary>
		/// <param name="pageIndex">指定的从0开始计算的页码序号</param>
		/// <returns>该页码属于页码区域</returns>
		public bool Contains( int pageIndex )
		{
			return pageIndex >= intFromPage && pageIndex <= intToPage ;
		}

		/// <summary>
		/// 解析字符串设置对象数据
		/// </summary>
		/// <param name="range"></param>
		public void Parse( string range )
		{
			intFromPage = 0 ;
			intToPage = 0 ;
			if( range != null && range.Trim().Length > 0 )
			{
				range = range.Trim();
				System.Collections.ArrayList Values = new System.Collections.ArrayList();
				int v = -1 ;
				foreach( char c in range )
				{
					int index = "0123456789".IndexOf( c );
					if( index >= 0 )
					{
						if( v == -1 )
							v = 0 ;
						v = v * 10 + index ;
					}
					else
					{
						if( v >= 0 )
						{
							Values.Add( v );
							v = -1 ;
						}
					}
				}//foreach
				if( v >= 0 )
					Values.Add( v );


				if( Values.Count == 0 )
				{
					intFromPage = 0 ;
					intToPage = 0 ;
				}
				else if( Values.Count == 1 )
				{
					intFromPage = ( int ) Values[0] ;
					intToPage = intFromPage ;
				}
				else
				{
					intFromPage = ( int ) Values[ 0] ;
					intToPage = ( int ) Values[ 1] ;
				}
				CheckData();
			}
		}

		public override string ToString()
		{
			if( intFromPage == intToPage )
				return intFromPage.ToString();
			else
				return intFromPage.ToString() + "-" + intToPage.ToString();
		}



	}
}
