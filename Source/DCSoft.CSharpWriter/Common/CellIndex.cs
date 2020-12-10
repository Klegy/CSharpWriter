/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;

namespace DCSoft.Common
{
	/// <summary>
	/// 单元格序号对象
	/// </summary>
	/// <remarks>本对象用于管理类似Excel单元格序号的信息,
	/// 位置字符串前面是一个以上连续的字母,后面是一个以上连续的数字字符,
	/// 字母部分表示列号,而数字部分表示行号,比如字符串"B1"表示第1行第2列,
	/// 本对象的行号和列号都是从1开始计数的。
    /// 编制 袁永福</remarks>
	public class CellIndex : System.IComparable
	{
		#region 构造函数 **************************************************************************

		/// <summary>
		/// 无作为的初始化对象
		/// </summary>
		public CellIndex()
		{
		}
		/// <summary>
		/// 初始化对象并设置行号和列号
		/// </summary>
		/// <param name="vRowIndex">行号</param>
		/// <param name="vColIndex">列号</param>
		public CellIndex( int vRowIndex , int vColIndex )
		{
			intRowIndex = vRowIndex ;
			intColIndex = vColIndex ;
		}
		/// <summary>
		/// 初始化对象并设置位置字符串
		/// </summary>
		/// <param name="vCellIndex">位置字符串</param>
		public CellIndex( string vCellIndex )
		{
			this.CellIndexString = vCellIndex ;
		}

		#endregion

		#region 对象属性 **************************************************************************

		private int intRowIndex = 1 ;
		/// <summary>
		/// 从1开始的行号
		/// </summary>
		public int RowIndex
		{
			get
            {
                return intRowIndex ;
            }
			set
            {
                intRowIndex = value;
            }
		}
		/// <summary>
		/// 行号字符串,实际是行号的字符串表达形式
		/// </summary>
		public string RowIndexString
		{
			get
            {
                return intRowIndex.ToString();
            }
			set
            {
                intRowIndex = Convert.ToInt32( value );
            }
		}
		private int intColIndex = 1 ;
		/// <summary>
		/// 从1开始的列号
		/// </summary>
		public int ColIndex
		{
			get
            {
                return intColIndex ;
            }
			set
            {
                intColIndex = value;
            }
		}
		/// <summary>
		/// 列号字符串,返回表示列号的由字母组成的字符串
		/// </summary>
		public string ColIndexString
		{
			get
            {
                return GetColWord( this.intColIndex );
            }
			set
            {
                intColIndex = GetColIndex( value );
            }
		}

        private int intWidth = 0;
        /// <summary>
        /// 区间宽度
        /// </summary>
        public int Width
        {
            get
            {
                return intWidth;
            }
            set
            {
                intWidth = value;
            }
        }

        private int intHeight = 0;
        /// <summary>
        /// 区间高度
        /// </summary>
        public int Height
        {
            get
            {
                return intHeight; 
            }
            set
            {
                intHeight = value; 
            }
        }

		/// <summary>
		/// 位置字符串，比如“A12”或“A12:B16”。
		/// </summary>
		public string CellIndexString
		{
			get
			{
                if (intWidth == 0 && intHeight == 0 && bolRangeStyle == false )
                {
                    return GetCellIndex(this.intRowIndex, this.intColIndex);
                }
                else
                {
                    int row = Math.Min(intRowIndex, intRowIndex + intHeight);
                    int col = Math.Min(intColIndex, intColIndex + intWidth);
                    int row2 = row + Math.Abs(intHeight);
                    int col2 = col + Math.Abs(intWidth);
                    return GetCellIndex(row, col) + ":" + GetCellIndex(row2, col2);
                }
			}
			set
			{
                int index = value.IndexOf(":");
                if (index >= 0)
                {
                    bolRangeStyle = true;
                    string cell = value.Substring(0, index).Trim();
                    string cell2 = value.Substring(index + 1).Trim();

                    intRowIndex = GetRowIndex(cell);
                    intColIndex = GetColIndex(cell);

                    int row2 = GetRowIndex(cell2);
                    int col2 = GetColIndex(cell2);

                    intHeight = Math.Abs(row2 - intRowIndex);
                    intWidth = Math.Abs(col2 - intColIndex);

                    if (intRowIndex > row2)
                        intRowIndex = row2;
                    if (intColIndex > col2)
                        intColIndex = col2;
                }
                else
                {
                    bolRangeStyle = false;
                    this.intRowIndex = GetRowIndex(value);
                    this.intColIndex = GetColIndex(value);
                    intWidth = 0;
                    intHeight = 0;
                }
			}
		}

        private bool bolRangeStyle = false;
        /// <summary>
        /// 是否是区间样式得单元格坐标方式。
        /// </summary>
        /// <remarks>
        /// 比如以“A12”初始化对象或设置CellIndexString属性值时本属性为false，
        /// 以“A12:B18”初始化对象或设置CellIndexString属性值时本属性为true,
        /// 注意以“A12:A12”初始化对象或设置CellIndexStrings属性值时虽然对象的
        /// Width和Height属性值为0，只包括一个单元格，但本属性值仍然为true。
        /// </remarks>
        public bool RangeStyle
        {
            get
            {
                return bolRangeStyle;
            }
        }

        /// <summary>
        /// 判断指定的位置是否包含在本对象定义的区域中
        /// </summary>
        /// <param name="row">行号</param>
        /// <param name="col">列号</param>
        /// <returns>是否在本对象定义的区域中</returns>
        public bool Contains(int row, int col)
        {
            return (row >= intRowIndex
                && row <= intRowIndex + intHeight
                && col >= intColIndex
                && col <= intColIndex + intWidth);
        }

		#endregion

		#region 对象函数 **************************************************************************

		/// <summary>
		/// 移动位置
		/// </summary>
		/// <param name="dRow">移动的行数</param>
		/// <param name="dCol">移动的列数</param>
		public void Offset( int dRow , int dCol )
		{
			intRowIndex += dRow ;
			intColIndex += dCol ;
		}
		/// <summary>
		/// 已重载:返回位置字符串
		/// </summary>
		/// <returns>位置字符串</returns>
		public override string ToString()
		{
			return this.CellIndexString ;
		}


        public int CompareTo(object obj)
        {
            CellIndex index = obj as CellIndex;
            if (index != null)
            {
                if (this.RowIndex < index.RowIndex)
                    return -1;
                else if (this.RowIndex == index.RowIndex)
                    return this.ColIndex - index.ColIndex;
                else
                    return 1;
            }
            return 0;
        }

        /// <summary>
        /// 判断本区域是否包含另外一个区域
        /// </summary>
        /// <param name="index">指定的区域</param>
        /// <returns>是否包含</returns>
        public bool Contains(CellIndex index)
        {
            if (index != null)
            {
                if (index.RowIndex >= this.RowIndex && index.ColIndex >= this.ColIndex)
                {
                    if (index.RowIndex + index.Height <= this.RowIndex + this.Height)
                    {
                        if (index.ColIndex + index.Width <= this.ColIndex + this.Width)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion

		#region 定义的一些静态成员 ****************************************************************

        /// <summary>
        /// 判断一个字符串是否是合法的表示区间的位置字符串，比如“A1”、“A1:C12”就符合这种格式。
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <returns></returns>
        public static bool IsCellIndex(string cellIndex)
        {
            if (cellIndex == null || cellIndex.Length == 0)
                return false;
            int index = cellIndex.IndexOf(":");
            if (index > 0)
            {
                string index1 = cellIndex.Substring(0, index).Trim();
                string index2 = cellIndex.Substring(index + 1).Trim();
                if (IsSimpleCellIndex(index1) && IsSimpleCellIndex(index2))
                {
                    return true;
                }
            }
            else
            {
                return IsSimpleCellIndex(cellIndex);
            }
            return false;
        }

		/// <summary>
		/// 根据从1开始计算的行号和列号获得位置字符串,行列号从1开始
		/// </summary>
		/// <param name="RowIndex">从1开始计算的行号</param>
		/// <param name="ColIndex">从1开始计算的列号</param>
		/// <returns>位置字符串</returns>
		public static string GetCellIndex( int RowIndex , int ColIndex ) 
		{
			return GetColWord( ColIndex ) + RowIndex.ToString();
		}

		/// <summary>
		/// 判断一个字符串是否是合法的位置字符串，比如“B12”就符合这种格式。
		/// </summary>
		/// <remarks>合法的字符串只能包含字母和数字,分为两个部分,前部分为连续的字母,
		/// 后部分为连续的数字,字母字符个数不得超过3个</remarks>
		/// <param name="CellIndex">字符串</param>
		/// <returns>若字符串为合法的位置字符串则返回true,否则返回false</returns>
		public static bool IsSimpleCellIndex( string CellIndex )
		{
			if( CellIndex == null || CellIndex.Length == 0 )
				return false;
			CellIndex = CellIndex.ToUpper();
			//bool bolBad = true;
			bool bolChar = true;
			int intCharCount = 0 ;
			int len = CellIndex.Length ;
			for(int iCount = 0 ; iCount < len ; iCount ++ )
			{
				char c = CellIndex[ iCount ];
				if( c >= 'A' && c <= 'Z' )
				{
					if( bolChar == false )
						return false;
					intCharCount ++ ;
				}
				else if( c >= '0' && c <= '9' )
					bolChar = false;
				else
					return false;
				if( intCharCount > 3 ) 
					return false;
			}
			if( bolChar )
				return false;
			else
				return true;
		}

		/// <summary>
		/// 将从1开始计算的列号转换为列字符串
		/// </summary>
		/// <param name="ColIndex">列号</param>
		/// <returns>列字符串</returns>
		public static string GetColWord( int ColIndex )
		{
			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
			string strWord = "0ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			while( true )
			{
				int index = ColIndex % 26 ;
				if( index == 0 )
				{
					myStr.Insert( 0 , "Z");
					index = 26 ;
				}
				else 
					myStr.Insert( 0 , strWord[ index ] );
				if( ColIndex <= 26 )
					break;
				ColIndex = ( ColIndex - index )  / 26 ;
			}
			return myStr.ToString();
		}

		/// <summary>
		/// 获得位置字符串表示的从1开始计算的行号
		/// </summary>
		/// <param name="CellIndex">位置字符串</param>
		/// <returns>行号</returns>
		public static int GetRowIndex( string CellIndex ) 
		{
			if( CellIndex == null || CellIndex.Length == 0 )
				return 1 ;
			int RowIndex = 0 ;
			foreach( char myChar in CellIndex )
			{
				if( char.IsNumber( myChar ) )
					RowIndex = RowIndex * 10 + myChar - '0';
			}
			return RowIndex ;
		}

		/// <summary>
		/// 获得位置字符串表示的从1开始计算的列号
		/// </summary>
		/// <param name="CellIndex">位置字符串</param>
		/// <returns>列号</returns>
		public static int GetColIndex( string CellIndex ) 
		{
			if( CellIndex == null || CellIndex.Length == 0 )
				return 1 ;
			int ColIndex = 0 ;
			CellIndex = CellIndex.ToUpper();
			foreach( char myChar in CellIndex )
			{
				if( char.IsLetter( myChar ))
					ColIndex = ColIndex * 26 + myChar - 'A' + 1 ;
			}
			return ColIndex ;
		}

		
		/// <summary>
		/// 获得位置字符串表示的从1开始计算的行号,若格式不对则返回-1
		/// </summary>
		/// <param name="CellIndex">位置字符串</param>
		/// <returns>行号</returns>
		public static int GetRowIndexWithError( string CellIndex ) 
		{
			if( CellIndex == null || CellIndex.Length == 0 )
				return -1 ;
			int RowIndex = 0 ;
			bool flag = false;
			foreach( char myChar in CellIndex )
			{
				if( char.IsNumber( myChar ) )
				{
					RowIndex = RowIndex * 10 + myChar - '0';
					flag = true ;
				}
			}
			if( flag )
			{
				return RowIndex ;
			}
			else
			{
				return -1 ;
			}
		}

		/// <summary>
		/// 获得位置字符串表示的从1开始计算的列号,若格式不对则返回-1
		/// </summary>
		/// <param name="CellIndex">位置字符串</param>
		/// <returns>列号</returns>
		public static int GetColIndexWithError( string CellIndex ) 
		{
			if( CellIndex == null || CellIndex.Length == 0 )
				return -1 ;
			int ColIndex = 0 ;
			bool flag = false ;
			CellIndex = CellIndex.ToUpper();
			foreach( char myChar in CellIndex )
			{
				if( char.IsLetter( myChar ))
				{
					ColIndex = ColIndex * 26 + myChar - 'A' + 1 ;
					flag = true ;
				}
			}
			if( flag )
			{
				return ColIndex ;
			}
			else
			{
				return -1 ;
			}
		}

		#endregion
    }//public class CellIndex
}