/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;

namespace DCSoft.Common
{
	/// <summary>
	/// 将字符串转换为各种类型数值的通用例程
	/// </summary>
	/// <remarks>本模块不依赖其他代码文件。编制袁永福</remarks>
	public sealed class StringConvertHelper
	{
		private static bool bolConvertFalseFlag = false;
		/// <summary>
		/// 最后一次转换是否失败标志
		/// </summary>
		public static bool ConvertFalseFlag
		{
			get{ return bolConvertFalseFlag ;}
		}
		/// <summary>
		/// 判断一个字符串是否可以转换为整数
		/// </summary>
		/// <param name="strData">要测试的字符串</param>
		/// <returns>若字符串可转换为数字则返回true 否则返回false ，字符串对象为空时也返回false</returns>
		public static bool IsInteger( string strData)
		{
			if( strData != null && strData.Length > 0 )
			{
				bool bolBack = bolConvertFalseFlag ;
				ToInt32Value( strData , 0 );
				bool bolResult = bolConvertFalseFlag ;
				bolConvertFalseFlag = bolBack ;
				return ! bolResult ;
			}
			return false;
		}

		/// <summary>
		/// 判断一个字符串是否全部由数字字符组成
		/// </summary>
		/// <param name="strData">要测试的字符串</param>
		/// <returns>若全部由数字字符组成则返回true 否则返回false ，字符串对象为空时也返回false</returns>
		public static bool IsIntegerString( string strData )
		{
			if( strData != null && strData.Length > 0 )
			{
				for(int iCount = 0 ; iCount < strData.Length ; iCount ++)
				{
					char c = strData[ iCount ];
					if( c > '9' || c < '0' )
						return false;
				}
				return true;
			}
			else
				return false;
		}

		/// <summary>
		/// 判断一个字符串是否可转换为数值,本函数不会设置 ConvertFalseFlag
		/// </summary>
		/// <param name="strData">字符串数据</param>
		/// <returns>是否可转换为数值</returns>
		public static bool IsNumeric( string strData )
		{
			bool bolB = bolConvertFalseFlag ;
			double dblValue = ToDoubleValue( strData , double.NaN );
			bolConvertFalseFlag = bolB ;
			return ( ! double.IsNaN( dblValue ));
		}

		/// <summary>
		/// 将yyyyMMddHHmmss 格式的字符串转化为时间数据
		/// </summary>
		/// <param name="strData">原始字符串</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换后的时间数据</returns>
		public static System.DateTime ToDBDateTime(string strData , System.DateTime DefaultValue)
		{
			bolConvertFalseFlag = true;
			try
			{
				if( IsIntegerString( strData ) == false )
				{
					return DefaultValue ;
				}
				else
				{
					if( strData.Length < 4 )
						return DefaultValue ;
					int year = 1 ;
					int month = 1 ;
					int day = 1 ;
					int hour = 0 ;
					int minutes = 0 ;
					int secend = 0 ;
					// 年数
					year = Convert.ToInt32( strData.Substring( 0 , 4 ));
					if( strData.Length >= 6 )
					{
						// 月份数
						month = Convert.ToInt32( strData.Substring( 4 , 2 ));
						if( month <= 0 || month > 12 )
							return DefaultValue ;
					}	
					if( strData.Length >= 8 )
					{
						// 天数
						day = Convert.ToInt32( strData.Substring( 6 , 2 ));
						if( day <= 0 || day > DateTime.DaysInMonth( year , month ) )
							return DefaultValue ;
					}
					if( strData.Length >= 10 )
					{
						// 小时数
						hour = Convert.ToInt32( strData.Substring( 8 , 2 ));
						if( hour > 60 )
							return DefaultValue ;
					}
					if( strData.Length >= 12 )
					{
						// 分钟
						minutes = Convert.ToInt32( strData.Substring( 10 , 2 ));
						if( minutes > 60 )
							return DefaultValue ;
					}
					if( strData.Length >= 14 )
					{
						// 秒
						secend = Convert.ToInt32( strData.Substring( 12 , 2 ));
						if( secend > 60 )
							return DefaultValue ;
					}
					DateTime dtm = new DateTime( year , month , day , hour , minutes , secend );
					bolConvertFalseFlag = false;
					return dtm ;
				}
			}
			catch
			{
				return DefaultValue;
			}
		}
		/// <summary>
		/// 可指定进制数的将字符串转换为整数
		/// </summary>
		/// <param name="strData">字符串数据</param>
		/// <param name="StartIndex">分析区域在字符串中的起始位置</param>
		/// <param name="Radix">进制数,在2和36之间</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换后的数值</returns>
		public static int ToInt32ValueExt( string strData , int StartIndex , int Radix , int DefaultValue)
		{
			try
			{
				bolConvertFalseFlag = true;
				// 检查参数,若参数不正确则返回默认值
				if( strData == null || strData.Length == 0 )
					return DefaultValue ;
				if( StartIndex < 0 || StartIndex >= strData.Length )
					return DefaultValue ;
				if( Radix < 1 || Radix > 36 )
					return DefaultValue ;
				int intValueCount = 0 ;
				bool bolNegative = false ;
				bool bolNumFlag = false;
				int intLen = strData.Length ;
				for(int iCount = StartIndex ; iCount < intLen ; iCount ++)
				{
					char c = strData[iCount];
					if( ! char.IsWhiteSpace( c ))
					{
						bool bolFlag = false;
						int intValue = 0 ;
						if( c >= '0' && c <= '9')
						{
							intValue = c - '0' ;
							bolFlag = true;
						}
						else if( Radix > 10 )
						{
							if( c >= 'A' && c <= 'Z')
							{
								intValue = c - 'A' + 10;
								bolFlag = true;
							}
							else if( c >= 'a' && c <='z')
							{
								intValue = c - 'a'+ 10;
								bolFlag = true;
							}
						}
						if( bolFlag )
						{
							if( intValue < Radix )
							{
								intValueCount = intValueCount * Radix + intValue ;
								bolNumFlag = true;
							}
							else
								break;
						}
						else
						{
							if( bolNumFlag )
								break;
							if( c == '-' )
								bolNegative = true;
							else
								return DefaultValue ;
						}
					}//if
					else
					{
						if( bolNumFlag )
							break;
					}
				}//for
				if( bolNegative )
					intValueCount = 0 - intValueCount ;
				bolConvertFalseFlag = false;
				return intValueCount ;
			}
			catch
			{
				return DefaultValue ;
			}
		}//public static int ToInt32ValueExt( string strData , int StartIndex , int Radix , int DefaultValue)

		/// <summary>
		/// 将一个字符串转换为整数,若转换失败则返回默认值
		/// </summary>
		/// <param name="strData">字符串</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换结果</returns>
		public static int ToInt32Value(string strData , int DefaultValue)
		{
			return ToInt32ValueExt( strData , 0 , 10 , DefaultValue );
		}
		/// <summary>
		/// 将一个对象转换为整数,若转换失败则返回默认值
		/// </summary>
		/// <param name="vData">对象</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换结果</returns>
		public static int ToInt32Value( object vData , int DefaultValue )
		{
			bolConvertFalseFlag = true;
			if( vData == null) 
				return DefaultValue ;
			if( vData is string )
			{
				return ToInt32ValueExt( (string) vData , 0 , 10 , DefaultValue );
			}
			else
			{
				try
				{
					int ResultValue = Convert.ToInt32( vData );
					bolConvertFalseFlag = false;
					return ResultValue ;
				}
				catch
				{
					return DefaultValue ;
				}
			}
		}

		/// <summary>
		/// 将一个字符串转换为一个字节数据,若转换失败则返回默认值
		/// </summary>
		/// <param name="strData">字符串</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换结果</returns>
		public static byte ToByteValue( string strData , byte DefaultValue )
		{
			return (byte) ToInt32ValueExt( strData , 0 , 10 , (int) DefaultValue );
		}
		/// <summary>
		/// 将一个对象转换为字节数据,若转换失败则返回默认值
		/// </summary>
		/// <param name="vData">对象</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换结果</returns>
		public static byte ToByteValue( object vData , byte DefaultValue )
		{
			bolConvertFalseFlag = true;
			if( vData == null) 
				return DefaultValue ;
			if( vData is string )
				return (byte) ToInt32ValueExt( (string) vData , 0 , 10 , (int) DefaultValue );
			else
			{
				try
				{
					byte ResultValue = Convert.ToByte( vData );
					bolConvertFalseFlag = false;
					return ResultValue ;
				}
				catch
				{
					return DefaultValue ;
				}
			}
		}
		/// <summary>
		/// 将一个字符串转换为一个字符数据,若转换失败则返回默认值
		/// </summary>
		/// <param name="strData">字符串</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换结果</returns>
		public static char ToCharValue( string strData , char DefaultValue )
		{
			if( strData == null || strData.Length == 0 )
				return DefaultValue ;
			return strData[0];
		}
		/// <summary>
		/// 将一个对象转换为一个字符数据,若转换失败则返回默认值
		/// </summary>
		/// <param name="vData">对象</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换结果</returns>
		public static char ToCharValue( object vData , char DefaultValue )
		{
			bolConvertFalseFlag = true;
			if( vData == null) 
				return DefaultValue ;
			if( vData is string )
				return ToCharValue( (string ) vData , DefaultValue );
			else
			{
				try
				{
					char ResultValue = Convert.ToChar( vData );
					bolConvertFalseFlag = false;
					return ResultValue ;
				}
				catch
				{
					return DefaultValue ;
				}
			}
		}

		/// <summary>
		/// 将对象转换为一个枚举数据,若转换失败则返回默认值
		/// </summary>
		/// <param name="strData">字符串</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换结果</returns>
		public static System.Enum ToEnumValue( string strData , System.Enum DefaultValue )
		{
			bolConvertFalseFlag = true;
			if( IsBlankString( strData ))
				return DefaultValue ;
			try
			{
				int iValue = ToInt32Value( strData , 0 );
				if( bolConvertFalseFlag )
				{
					System.Enum ResultValue = ( System.Enum ) System.Enum.Parse( DefaultValue.GetType() , strData , true );
					bolConvertFalseFlag = false;
					return ResultValue ;
				}
				else
				{
					System.Enum ResultValue = ( System.Enum ) System.Enum.ToObject( DefaultValue.GetType() , iValue );
					bolConvertFalseFlag = true;
					return ResultValue ;
				}
			}
			catch
			{
				return DefaultValue ;
			}
		}

		/// <summary>
		/// 将一个对象转换为一个枚举数据,若转换失败则返回默认值
		/// </summary>
		/// <param name="vData">对象</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换结果</returns>
		public static System.Enum ToEnumValue( object vData , System.Enum DefaultValue )
		{
			bolConvertFalseFlag = true;
			if( vData == null) 
				return DefaultValue ;
			if( vData is string )
				return ToEnumValue( (string) vData , DefaultValue );
			else
			{
				try
				{
					System.Enum ResultValue = (System.Enum) System.Enum.ToObject( DefaultValue.GetType(), vData );
					bolConvertFalseFlag = false;
					return ResultValue ;
				}
				catch
				{
					return DefaultValue ;
				}
			}
		}

		/// <summary>
		/// 将一个字符串转换为时间数据,若转换失败则返回默认值
		/// </summary>
		/// <param name="strData">字符串</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换结果</returns>
		public static System.DateTime ToDateTimeValue( string strData , System.DateTime DefaultValue )
		{
			bolConvertFalseFlag = true;
			if( IsBlankString( strData ))
				return DefaultValue ;
			if( IsIntegerString(strData ))
				return ToDBDateTime( strData , DefaultValue );
			try
			{
				if( strData.IndexOf("-")> 0 )
				{
					strData = strData.Replace('-' , ' ');
					int[] values = ToInt32Values( strData );
					DateTime dtm = DefaultValue ;

					if( values == null || values.Length == 0 )
					{
						return DefaultValue ;
					}

					if( values.Length == 1 )
					{
						dtm = new DateTime( values[0] , 1 , 1 );
						bolConvertFalseFlag = false;
					}
					else if( values.Length == 2 )
					{
						dtm = new DateTime( values[0] , values[1] , 1 );
						bolConvertFalseFlag = false;
					}
					else if( values.Length == 3 )
					{
						dtm = new DateTime( values[0] , values[1] , values[2] );
						bolConvertFalseFlag = false;
					}
					else if( strData.IndexOf(':') > 0 )
					{
						if( values.Length == 4 )
						{
							dtm = new DateTime( values[0] , values[1] , values[2] , values[3] ,0, 0 );
							bolConvertFalseFlag = false;
						}
						else if( values.Length == 5 )
						{
							dtm = new DateTime( values[0] , values[1] , values[2] , values[3] , values[4] , 0 );
							bolConvertFalseFlag = false;
						}
						else if( values.Length == 6 )
						{
							dtm = new DateTime( values[0] , values[1] , values[2] , values[3] , values[4] , values[5] );
							bolConvertFalseFlag = false;
						}
					}
					if( bolConvertFalseFlag == false )
						return dtm ;
				}
				System.DateTime ResultValue = Convert.ToDateTime( strData );
				bolConvertFalseFlag = false;
				return ResultValue ;
			}
			catch( Exception ext )
			{
				System.Console.Write( ext.ToString());
				return DefaultValue ;
			}
		}
		/// <summary>
		/// 将一个对象转换为一个时间数据,若转换失败则返回默认值
		/// </summary>
		/// <param name="vData">对象</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换结果</returns>
		public static System.DateTime ToDateTimeValue( object vData , System.DateTime DefaultValue )
		{
			bolConvertFalseFlag = true;
			if( vData == null)
				return DefaultValue ;
			if( vData is string )
				return ToDateTimeValue( (string ) vData , DefaultValue );
			try
			{
				System.DateTime ResultValue = Convert.ToDateTime( vData );
				bolConvertFalseFlag = false;
				return ResultValue ;
			}
			catch
			{
				return DefaultValue ;
			}
		}

		public static System.DateTime ToDateTimeValue( string strData , System.DateTime DefaultValue , string strFormat )
		{
			bolConvertFalseFlag = true;
			if( IsBlankString( strData ))
				return DefaultValue ;
			try
			{
				System.DateTime ResultValue = DateTime.ParseExact( 
					strData ,
					strFormat ,
					System.Globalization.CultureInfo.CurrentCulture ); 
				bolConvertFalseFlag = false;
				return ResultValue ;
			}
			catch
			{
				return DefaultValue ;
			}
		}

		/// <summary>
		/// 将一个字符串转换为单精度浮点数,若转换失败则返回默认值
		/// </summary>
		/// <param name="strData">字符串</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换结果</returns>
		public static float ToSingleValue( string strData , float DefaultValue)
		{
			return (float ) ToDoubleValue( strData , (double) DefaultValue );
		}
		/// <summary>
		/// 将一个对象转换为一个单精度浮点数,若转换失败则返回默认值
		/// </summary>
		/// <param name="vData">对象</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换结果</returns>
		public static float ToSingleValue( object vData , float DefaultValue )
		{
			return ( float ) ToDoubleValue( vData , (double) DefaultValue );
		}
		/// <summary>
		/// 将一个字符串转换为十进制数,若转换失败则返回默认值
		/// </summary>
		/// <param name="strData">字符串</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换结果</returns>
		public static decimal ToDecimalValue( string strData , decimal DefaultValue )
		{
			double v = ToDoubleValue( strData , decimal.ToDouble( DefaultValue ));
			return new decimal( v );
		}
		/// <summary>
		/// 将一个对象转换为一个十进制数,若转换失败则返回默认值
		/// </summary>
		/// <param name="vData">对象</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换结果</returns>
		public static decimal ToDecimalValue( object vData , decimal DefaultValue )
		{
			double v = ToDoubleValue( vData , decimal.ToDouble( DefaultValue ));
			return new decimal( v );
		}

		/// <summary>
		/// 将一个字符串转换为双精度浮点数,若转换失败则返回默认值
		/// </summary>
		/// <param name="strData">字符串</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换结果</returns>
		public static double ToDoubleValue( string strData , double DefaultValue)
		{
			bolConvertFalseFlag = true;
			try
			{
				if( IsBlankString( strData ))
					return DefaultValue;
				if( string.Compare( strData , "nan" , true )== 0 )
					return DefaultValue ;
				char[] myChars = strData.ToCharArray();
				double dbl = 0 ;
				bool bolNegative = false ;
				bool Digi = false;
				int intLen = myChars.Length ;
				int pow = 0 ;
				bool Reading = false;
				for(int index = 0 ; index < intLen ; index ++ )
				{
					char c = myChars[index];
					if( c >= 48 && c <= 57 )
					{
						dbl = dbl * 10 + c - 48 ;
						if( Digi )
							pow ++ ;
						Reading = true;
					}
					else if( c == '-' )
					{
						if( Reading )
							break;
						bolNegative = true;
					}
					else if( c == '.' )
					{
						if( Digi )
							break;
						Digi = true;
					}
					else
						break;
				}
				if( Reading == false )
				{
					return DefaultValue ;
				}
				if( bolNegative ) 
					dbl =  - dbl ;
				if( pow > 0 )
					dbl = dbl / System.Math.Pow( 10 , pow );
				bolConvertFalseFlag = false;
				return dbl ;
			}
			catch
			{
				return DefaultValue;
			}
		}

		/// <summary>
		/// 将一个对象转换为一个双精度浮点数,若转换失败则返回默认值
		/// </summary>
		/// <param name="vData">对象</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换结果</returns>
		public static double ToDoubleValue( object vData , double DefaultValue )
		{
			bolConvertFalseFlag = true;
			if( vData == null)
				return DefaultValue ;
			if( vData is string )
				return ToDoubleValue( (string) vData , DefaultValue );
			try
			{
				double ResultValue = Convert.ToDouble( vData );
				bolConvertFalseFlag = false;
				return ResultValue ;
			}
			catch
			{
				return DefaultValue ;
			}
		}

		/// <summary>
		/// 将一个对象转换为字符串
		/// </summary>
		/// <param name="obj">对象</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>字符串</returns>
		public static string ToStringValue(object obj ,string DefaultValue )
		{
			try
			{
				bolConvertFalseFlag = false;
				if( obj == null)
					return DefaultValue ;
				else
					return obj.ToString();
			}
			catch
			{
				bolConvertFalseFlag = true;
				return DefaultValue;
			}
		}

		/// <summary>
		/// 将一个字符串转换为布尔类型的值,若转换失败则返回默认值
		/// </summary>
		/// <param name="strData">待处理的字符串</param>
		/// <param name="DefaultValue">若转换失败则返回的默认值</param>
		/// <returns>转换结果</returns>
		public static bool ToBoolValue(string strData,bool DefaultValue)
		{
			bolConvertFalseFlag = true;
			if(strData == null)
				return DefaultValue ;
			strData = strData.Trim();
			bolConvertFalseFlag = false;
			if( DefaultValue )
			{
				if( strData == "0" || string.Compare( "false" , strData , true ) == 0)
					return false;
				else
					return true;
			}
			else
			{
				if( strData == "1" || string.Compare("true" , strData , true ) == 0 )
					return true;
				else
					return false;
			}
		}
		/// <summary>
		/// 将一个对象转换为布尔值,若转换失败则返回默认值
		/// </summary>
		/// <param name="obj">对象</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>字符串</returns>
		public static bool ToBoolValue( object vData , bool DefaultValue )
		{
			bolConvertFalseFlag = true;
			if( vData == null)
				return DefaultValue ;
			if( vData is string )
				return ToBoolValue( (string) vData , DefaultValue );
			try
			{
				bool ResultValue = Convert.ToBoolean( vData );
				bolConvertFalseFlag = false;
				return ResultValue ;
			}
			catch
			{
				return DefaultValue ;
			}
		}
		public static System.Drawing.Color ToColorValueAbs(string strText )
		{
			return ToColorValue( strText , System.Drawing.Color.Black );
		}
		/// <summary>
		/// 将 #xxxxxx 字符串转换为一个颜色值
		/// </summary>
		/// <param name="strText">#xxxxxx 格式的字符串</param>
		/// <param name="DefaultValue">若转换失败则使用的默认值</param> 
		/// <returns>转换结果</returns>
		public static System.Drawing.Color  ToColorValue(string strText, System.Drawing.Color  DefaultValue )
		{
			bolConvertFalseFlag = true;
			if( HasContent( strText ))
			{
				strText = strText.ToUpper().Trim();
				if(strText.StartsWith("#") )
				{
					const string c_HexList		= "0123456789ABCDEF";
					string strValue = GetSubString( strText , c_HexList , 1 );
					int intA = 255 ;
					int intR = 0 ;
					int intG = 0 ;
					int intB = 0 ;
					if( strValue.Length == 6 )
					{
						intR = ( c_HexList.IndexOf( strValue[0] ) << 4 )+ c_HexList.IndexOf( strValue[1]) ;
						intG = ( c_HexList.IndexOf( strValue[2] ) << 4 )+ c_HexList.IndexOf( strValue[3]) ;
						intB = ( c_HexList.IndexOf( strValue[4] ) << 4 )+ c_HexList.IndexOf( strValue[5]) ;
					}
					else if( strValue.Length == 8 )
					{
						intA = ( c_HexList.IndexOf( strValue[0] ) << 4 )+ c_HexList.IndexOf( strValue[1]) ;
						intR = ( c_HexList.IndexOf( strValue[2] ) << 4 )+ c_HexList.IndexOf( strValue[3]) ;
						intG = ( c_HexList.IndexOf( strValue[4] ) << 4 )+ c_HexList.IndexOf( strValue[5]) ;
						intB = ( c_HexList.IndexOf( strValue[6] ) << 4 )+ c_HexList.IndexOf( strValue[7]) ;
					}
					else
						return DefaultValue ;
					bolConvertFalseFlag = false;
 
					return System.Drawing.Color.FromArgb( intA , intR , intG , intB );
 
				}
				else
				{
					try
					{

						if( IsNumeric( strText ))
						{
							int iValue = ToInt32Value( strText , int.MinValue );
 
							System.Drawing.Color vColor = System.Drawing.Color.FromArgb( iValue );
							vColor = System.Drawing.Color.FromArgb( 255 , vColor );
							bolConvertFalseFlag = false;
							return vColor ;
 
						}
 
						System.Drawing.Color vColor2 = System.Drawing.Color.FromName( strText );
						if( vColor2.A == 0 && vColor2.R == 0 && vColor2.G == 0 && vColor2.B == 0 )
						{
							return DefaultValue ;
						}
						bolConvertFalseFlag = false;
						return vColor2 ;
 
					}
					catch
					{
						return DefaultValue ;
					}
				}
			}
			return DefaultValue;
		}
		//
		//		/// <summary>
		//		/// 将 #xxxxxx 字符串转换为一个颜色值
		//		/// </summary>
		//		/// <param name="strText">#xxxxxx 格式的字符串</param>
		//		/// <param name="DefaultValue">若转换失败则使用的默认值</param> 
		//		/// <returns>转换结果</returns>
		//		public static System.Drawing.Color  ToColorValue(string strText, System.Drawing.Color  DefaultValue )
		//		{
		//			bolConvertFalseFlag = true;
		//			if( HasContent( strText ))
		//			{
		//				strText = strText.ToUpper().Trim();
		//				if(strText.StartsWith("#") )
		//				{
		//					const string c_HexList		= "0123456789ABCDEF";
		//					string strValue = GetSubString( strText , c_HexList , 1 );
		//					int intA = 255 ;
		//					int intR = 0 ;
		//					int intG = 0 ;
		//					int intB = 0 ;
		//					if( strValue.Length == 6 )
		//					{
		//						intR = ( c_HexList.IndexOf( strValue[0] ) << 4 )+ c_HexList.IndexOf( strValue[1]) ;
		//						intG = ( c_HexList.IndexOf( strValue[2] ) << 4 )+ c_HexList.IndexOf( strValue[3]) ;
		//						intB = ( c_HexList.IndexOf( strValue[4] ) << 4 )+ c_HexList.IndexOf( strValue[5]) ;
		//					}
		//					else if( strValue.Length == 8 )
		//					{
		//						intA = ( c_HexList.IndexOf( strValue[0] ) << 4 )+ c_HexList.IndexOf( strValue[1]) ;
		//						intR = ( c_HexList.IndexOf( strValue[2] ) << 4 )+ c_HexList.IndexOf( strValue[3]) ;
		//						intG = ( c_HexList.IndexOf( strValue[4] ) << 4 )+ c_HexList.IndexOf( strValue[5]) ;
		//						intB = ( c_HexList.IndexOf( strValue[6] ) << 4 )+ c_HexList.IndexOf( strValue[7]) ;
		//					}
		//					else
		//						return DefaultValue ;
		//					bolConvertFalseFlag = false;
		//					return System.Drawing.Color.FromArgb( intA , intR , intG , intB );
		//				}
		//				else
		//				{
		//					try
		//					{
		//						if( IsNumeric( strText ))
		//						{
		//							int iValue = ToInt32Value( strText , int.MinValue );
		//							System.Drawing.Color vColor = System.Drawing.Color.FromArgb( iValue );
		//							vColor = System.Drawing.Color.FromArgb( 255 , vColor );
		//							bolConvertFalseFlag = false;
		//							return vColor ;
		//						}
		//						System.Drawing.Color vColor2 = System.Drawing.Color.FromName( strText );
		//						if( vColor2.A == 0 && vColor2.R == 0 && vColor2.G == 0 && vColor2.B == 0 )
		//						{
		//							return DefaultValue ;
		//						}
		//						bolConvertFalseFlag = false;
		//						return vColor2 ;
		//					}
		//					catch
		//					{
		//						return DefaultValue ;
		//					}
		//				}
		//			}
		//			return DefaultValue;
		//		}

		private static string GetSubString( string strText , string strIncludeChars , int StartIndex )
		{
			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
			for(int iCount = StartIndex ; iCount < strText.Length ; iCount ++ )
			{
				char c = strText[ iCount ];
				if( strIncludeChars.IndexOf( c ) >= 0 )
					myStr.Append( c );
				else
					break;
			}
			return myStr.ToString();
		}

		/// <summary>
		/// 将对象转换为一个颜色值,若转换失败则返回默认值
		/// </summary>
		/// <param name="vData">对象</param>
		/// <param name="DefaultValue">默认值</param>
		/// <returns>转换结果</returns>
		public static System.Drawing.Color ToColorValue( object vData , System.Drawing.Color DefaultValue )
		{
			bolConvertFalseFlag = true;
			if( vData == null)
				return DefaultValue ;
			if( vData is string )
				return ToColorValue( (string) vData , DefaultValue );
			try
			{
				if( vData is System.Drawing.Color )
				{
					bolConvertFalseFlag = false;
					return ( System.Drawing.Color ) vData ;
				}
				int iValue = Convert.ToInt32( vData );
				System.Drawing.Color c = System.Drawing.Color.FromArgb( iValue );
				c = System.Drawing.Color.FromArgb( 255, c );
				bolConvertFalseFlag = false;
				return c ;
			}
			catch
			{
				return DefaultValue ;
			}
		}

		/// <summary>
		/// 将Base64字符串转换为字节数组
		/// </summary>
		/// <param name="strData">字符串</param>
		/// <returns>字节数组</returns>
		public static byte[] ToBinary( string strData )
		{
			bolConvertFalseFlag = true;
			try
			{
				if( strData != null && strData.Length > 0 )
				{
					byte[] bs = Convert.FromBase64String( strData );
					bolConvertFalseFlag = false;
					return bs ;
				}
			}
			catch
			{
				return null;
			}
			return null;
		}
		/// <summary>
		/// 将对象转换为一个字节数组
		/// </summary>
		/// <param name="vData">对象</param>
		/// <returns>转换结果</returns>
		public static byte[] ToBinary( object vData )
		{
			bolConvertFalseFlag = true;
			if( vData == null)
				return null;
			if( vData is string )
				return ToBinary( (string ) vData );
			if( vData is byte[])
			{
				bolConvertFalseFlag = false;
				return (byte[]) vData ;
			}
			return null;
		}
		/// <summary>
		/// 将对象转换为字符串
		/// </summary>
		/// <param name="vValue">数值</param>
		/// <returns>转换后的字符串</returns>
		public static string ToString( byte[] vValue )
		{
			if( vValue == null)
				return null;
			else
				return Convert.ToBase64String( vValue );
		}

		/// <summary>
		/// 将对象转换为字符串
		/// </summary>
		/// <param name="vValue">数值</param>
		/// <returns>转换后的字符串</returns>
		public static string ToString( int vValue)
		{
			return vValue.ToString();
		}
		/// <summary>
		/// 将对象转换为字符串
		/// </summary>
		/// <param name="vValue">数值</param>
		/// <returns>转换后的字符串</returns>
		public static string ToString( System.DateTime dtm )
		{
			return dtm.ToString("yyyyMMddHHmmss");
		}
		/// <summary>
		/// 将对象转换为字符串
		/// </summary>
		/// <param name="vValue">数值</param>
		/// <returns>转换后的字符串</returns>
		public static string ToString( double vValue )
		{
			if( double.IsNaN( vValue ))
				return "NaN";
			return vValue.ToString();
		}
		/// <summary>
		/// 将十进制数据转换为字符串
		/// </summary>
		/// <param name="vValue">数据</param>
		/// <returns>转换后的字符串</returns>
		public static string ToString( decimal vValue )
		{
			return vValue.ToString();
		}
		/// <summary>
		/// 将对象转换为字符串
		/// </summary>
		/// <param name="vValue">数值</param>
		/// <returns>转换后的字符串</returns>
		public static string ToString( float vValue )
		{
			if( float.IsNaN( vValue ))
				return "NaN";
			return vValue.ToString();
		}
		/// <summary>
		/// 将对象转换为字符串
		/// </summary>
		/// <param name="vValue">数值</param>
		/// <returns>转换后的字符串</returns>
		public static string ToString( byte vValue )
		{
			return vValue.ToString();
		}
		/// <summary>
		/// 将对象转换为字符串
		/// </summary>
		/// <param name="vValue">数值</param>
		/// <returns>转换后的字符串</returns>
		public static string ToString( System.Enum vValue)
		{
			return Convert.ToString( Convert.ToInt32( vValue ));
		}
		/// <summary>
		/// 将对象转换为字符串
		/// </summary>
		/// <param name="vValue">数值</param>
		/// <returns>转换后的字符串</returns>
		public static string ToString( char vValue )
		{
			return new string( vValue , 1 );
		}
		/// <summary>
		/// 将对象转换为字符串
		/// </summary>
		/// <param name="vValue">数值</param>
		/// <returns>转换后的字符串</returns>
		public static string ToString( System.Drawing.Color vValue)
		{
			if( vValue.A != 255 )
				return "#" + vValue.A.ToString("X2") + Convert.ToInt32( vValue.ToArgb() & 0xffffff).ToString("X6");
			else
				return "#" + Convert.ToInt32( vValue.ToArgb() & 0xffffff).ToString("X6");
		}
		/// <summary>
		/// 将对象转换为字符串
		/// </summary>
		/// <param name="vValue">数值</param>
		/// <returns>转换后的字符串</returns>
		public static string ToString( string vValue )
		{
			return vValue ;
		}
		/// <summary>
		/// 将点坐标数组转换为字符串
		/// </summary>
		/// <param name="ps">点坐标数组</param>
		/// <returns>转换后的字符串</returns>
		public static string ToString( System.Drawing.Point[] ps )
		{
			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
			for(int iCount = 0 ; iCount < ps.Length ; iCount ++)
			{
				if( iCount > 0 )
					myStr.Append( "," );
				myStr.Append( ps[iCount].X.ToString());
				myStr.Append( "," );
				myStr.Append( ps[iCount].Y.ToString());
			}
			return myStr.ToString();
		}
		/// <summary>
		/// 将矩形转换为字符串
		/// </summary>
		/// <param name="rect">矩形区域</param>
		/// <returns>转换后的字符串</returns>
		public static string ToString( System.Drawing.Rectangle rect )
		{
			return rect.Left + "," + rect.Top + "," + rect.Width + "," + rect.Height ;
		}
		/// <summary>
		/// 将对象转换为字符串
		/// </summary>
		/// <param name="vValue">数值</param>
		/// <returns>转换后的字符串</returns>
		public static string ToString( bool vValue )
		{
			if( vValue )
				return "1";
			else
				return "0";
		}
		//		/// <summary>
		//		/// 将一个颜色值转换为 #XXXXXX 格式的字符串
		//		/// </summary>
		//		/// <param name="intValue">整数值</param>
		//		/// <returns>转换后的字符串</returns>
		//		public static string ColorToHtml(System.Drawing.Color  myColor)
		//		{
		//			return "#" + Convert.ToInt32(myColor.ToArgb() & 0xffffff).ToString("X6");
		//		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <remarks>例如参数传入的字符串为"((-28703,-11500), (-13149,14500))",
		/// 则函数返回矩形结构(-28703,-11500,-13149,14500)</remarks>
		/// <param name="strText"></param>
		/// <returns></returns>
		public static System.Drawing.Rectangle AnalyseRectString(string strText )
		{
			int[] intValues = ToInt32Values( strText );
			if( intValues != null && intValues.Length == 4 )
				return new System.Drawing.Rectangle( intValues[0] , intValues[1] , intValues[2] - intValues[0] , intValues[3] - intValues[1]);
			else
				return System.Drawing.Rectangle.Empty ;
		}

		/// <summary>
		/// 处理指定的字符串，将其中包含的所有的整数数值提取出来组成一个整数数组
		/// </summary>
		/// <param name="strText">要处理的字符串</param>
		/// <returns>整数数组</returns>
		public static int[] ToInt32Values( string strText )
		{
			if( strText == null || strText.Length == 0 )
				return null;
			System.Collections.ArrayList myList = new System.Collections.ArrayList();
			int iData = 0 ;
			bool Reading = false;
			bool bolNegative = false;
			for(int iCount = 0 ; iCount < strText.Length ; iCount ++)
			{
				bool bolAdd = ( iCount == strText.Length -1 );
				char c = strText[iCount];
				if( c >= '0' && c <= '9' )
				{
					iData = iData * 10 + c - '0';
					Reading = true;
				}
				else if( c == '-')
				{
					if( Reading )
					{
						bolAdd = true;
						iCount -- ;
					}
					else
						bolNegative = true;
				}
				else
				{
					bolAdd = true;
				}//else
				if( bolAdd )
				{
					if( Reading )
					{
						if( bolNegative )
							iData = 0 - iData ;
						myList.Add( iData );
						iData = 0 ;
					}
					bolNegative = false;
					Reading = false;
				}
			}//for
			if( myList.Count > 0 )
			{
				return ( int[]) myList.ToArray( typeof( int ));
			}
			else
				return null;
		}//public static int[] ToInt32Values( string strText )

		/// <summary>
		/// 将字符串解析生成一个双精度浮点数数组,若未解析出任何数据则返回空引用
		/// </summary>
		/// <param name="strText">文本字符串</param>
		/// <returns>双精度浮点数数组</returns>
		public static double[] ToDoubleValues( string strText )
		{
			if( strText == null || strText.Length == 0 )
				return null;

			System.Collections.ArrayList myList = new System.Collections.ArrayList();
			double dblValue = 0 ;
			bool Reading = false;
			bool bolNegative = false;
			bool bolPoint = false;
			int intPower = 0 ;
			for(int iCount = 0 ; iCount < strText.Length ; iCount ++)
			{
				bool bolAdd = ( iCount == strText.Length -1 );

				char c = strText[iCount];
				if( c >= '0' && c <= '9')
				{
					dblValue = dblValue * 10 + c - '0';
					Reading = true;
					if( bolPoint )
						intPower ++ ;
				}
				else if( c == '.' )
				{
					if( Reading && bolPoint )
					{
						bolAdd = true;
						iCount -- ;
					}
					else
					{
						if( bolPoint )
						{
							bolNegative = false;
						}
						bolPoint = true;
					}
				}
				else if( c == '-')
				{
					if( Reading )
					{
						bolAdd = true;
						iCount -- ;
					}
					else
					{
						bolNegative = true;
						if( ! Reading )
							bolPoint = false;
					}
				}
				else
				{
					if( Reading )
					{
						bolNegative = ( c == '-' );
						bolAdd = true;
					}
				}//else
				if( bolAdd )
				{
					if( Reading )
					{
						if( bolNegative )
							dblValue = 0 - dblValue ;
						if( intPower > 0)
							dblValue = dblValue / System.Math.Pow( 10 , intPower );
						myList.Add( dblValue );
						dblValue = 0 ;
					}
					intPower = 0 ;
					bolNegative = false;
					bolPoint = false;
					Reading = false;
				}
			}//for
			if( myList.Count > 0 )
			{
				return ( double[]) myList.ToArray( typeof( double));
			}
			else
				return null;
		}

        /// <summary>
        /// 将指定的数值转化为小写汉字字符串
        /// </summary>
        /// <param name="number">原始数据</param>
        /// <returns>转化后的字符串</returns>
        public static string ToSmallChineseNumber(double number)
        {
            string numList = "零一二三四五六七八九";
            string rmbList = "分角元十百千万十百千亿十百千万";
            string tempOutString = "";
            bool flag = false;
            if (number < 0)
            {
                flag = true;
                number = -number;
            }
            if (number < 0.01)
            {
                return "零元整";
            }
            if (number > 9999999999999.99)
            {
                return "数值过大,无法显示";
                //throw new System.ArgumentOutOfRangeException("number", 9999999999999.99, "数值超出范围");
            }

            //将小数转化为整数字符串 
            string tempNumberString = Convert.ToInt64(number * 100).ToString();
            int tempNmberLength = tempNumberString.Length;
            int i = 0;
            while (i < tempNmberLength)
            {
                int oneNumber = Int32.Parse(tempNumberString.Substring(i, 1));
                string oneNumberChar = numList.Substring(oneNumber, 1);
                string oneNumberUnit = rmbList.Substring(tempNmberLength - i - 1, 1);
                if (oneNumberChar != "零")
                    tempOutString += oneNumberChar + oneNumberUnit;
                else
                {
                    if (oneNumberUnit == "亿" || oneNumberUnit == "万" || oneNumberUnit == "元" || oneNumberUnit == "零")
                    {
                        while (tempOutString.EndsWith("零"))
                        {
                            tempOutString = tempOutString.Substring(0, tempOutString.Length - 1);
                        }
                    }
                    if (oneNumberUnit == "亿" || (oneNumberUnit == "万" && !tempOutString.EndsWith("亿")) || oneNumberUnit == "元")
                    {
                        tempOutString += oneNumberUnit;
                    }
                    else
                    {
                        bool tempEnd = tempOutString.EndsWith("亿");
                        bool zeroEnd = tempOutString.EndsWith("零");
                        if (tempOutString.Length > 1)
                        {
                            bool zeroStart = tempOutString.Substring(tempOutString.Length - 2, 2).StartsWith("零");
                            if (!zeroEnd && (zeroStart || !tempEnd))
                                tempOutString += oneNumberChar;
                        }
                        else
                        {
                            if (!zeroEnd && !tempEnd)
                                tempOutString += oneNumberChar;
                        }
                    }
                }
                i += 1;
            }

            while (tempOutString.EndsWith("零"))
            {
                tempOutString = tempOutString.Substring(0, tempOutString.Length - 1);
            }

            while (tempOutString.EndsWith("元"))
            {
                tempOutString = tempOutString + "整";
            }

            if (flag)
                tempOutString = "负" + tempOutString;

            return tempOutString;
        }//public static string ConvertToChineseNum( double number )
		 
		/// <summary>
		/// 将指定的数值转化为大写汉字字符串
		/// </summary>
		/// <param name="number">原始数据</param>
		/// <returns>转化后的字符串</returns>
		public static string ToBigChineseNumber( double number )
		{ 
			string numList="零壹贰叁肆伍陆柒捌玖"; 
			string rmbList = "分角圆拾佰仟万拾佰仟亿拾佰仟万"; 
			string tempOutString=""; 
			bool flag = false;
			if( number < 0 )
			{
				flag = true ;
				number = - number;
			}
            if (  number < 0.01 )
            {
                return "零圆整";
            }
            if (number > 9999999999999.99)
            {
                return "数值过大,无法显示";
                //throw new System.ArgumentOutOfRangeException("number", 9999999999999.99, "数值超出范围");
            }

			//将小数转化为整数字符串 
			string tempNumberString = Convert.ToInt64( number*100 ).ToString(); 
			int tempNmberLength = tempNumberString.Length; 
			int i=0; 
			while( i < tempNmberLength ) 
			{ 
				int oneNumber = Int32.Parse(tempNumberString.Substring(i,1)); 
				string oneNumberChar = numList.Substring(oneNumber,1); 
				string oneNumberUnit = rmbList.Substring(tempNmberLength-i-1,1); 
				if(oneNumberChar!="零") 
					tempOutString += oneNumberChar+oneNumberUnit; 
				else 
				{ 
					if(oneNumberUnit=="亿"||oneNumberUnit=="万"||oneNumberUnit=="圆"||oneNumberUnit=="零") 
					{ 
						while (tempOutString.EndsWith("零")) 
						{ 
							tempOutString=tempOutString.Substring(0,tempOutString.Length-1); 
						} 
					} 
					if(oneNumberUnit=="亿"||(oneNumberUnit=="万"&&!tempOutString.EndsWith("亿"))||oneNumberUnit=="圆") 
					{ 
						tempOutString+=oneNumberUnit; 
					} 
					else 
					{ 
						bool tempEnd=tempOutString.EndsWith("亿"); 
						bool zeroEnd=tempOutString.EndsWith("零"); 
						if(tempOutString.Length>1) 
						{ 
							bool zeroStart=tempOutString.Substring(tempOutString.Length-2,2).StartsWith("零"); 
							if(!zeroEnd&&(zeroStart||!tempEnd)) 
								tempOutString+=oneNumberChar; 
						} 
						else 
						{ 
							if(!zeroEnd&&!tempEnd) 
								tempOutString+=oneNumberChar; 
						} 
					} 
				} 
				i+=1; 
			} 

			while (tempOutString.EndsWith("零")) 
			{ 
				tempOutString=tempOutString.Substring(0,tempOutString.Length-1); 
			} 

			while(tempOutString.EndsWith("圆")) 
			{ 
				tempOutString=tempOutString+"整"; 
			} 

			if( flag )
				tempOutString = "负" + tempOutString ;

			return tempOutString; 
		}//public static string ConvertToChineseNum( double number )
 

		
		/// <summary>
		/// 获得一个字符串的汉语拼音码
		/// </summary>
		/// <param name="strText">字符串</param>
		/// <returns>汉语拼音码,该字符串只包含大写的英文字母</returns>
		public static string ToChineseSpell( string strText)
		{
            if (strText == null || strText.Length == 0)
            {
                return strText;
            }
			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
			int index = 0 ;
			foreach( char vChar in strText)
			{
				// 若是字母则直接输出
                if ((vChar >= 'a' && vChar <= 'z') || (vChar >= 'A' && vChar <= 'Z'))
                {
                    myStr.Append(char.ToUpper(vChar));
                }
                else
                {
                    index = (int)vChar - 19968;
                    if (index >= 0 && index < strChineseFirstPY.Length)
                    {
                        myStr.Append(strChineseFirstPY[index]);
                    }
                }
			}//foreach
			return myStr.ToString() ;
		}// public static string GetChineseSpell( string strText)

		#region 内部私有的成员 ****************************************************
		
		/// <summary>
		/// 判断一个字符串对象是否是空字符串
		/// </summary>
		/// <param name="strData">字符串</param>
		/// <returns>若字符串为空或者全部有空白字符组成则返回True,否则返回false</returns>
		private static bool IsBlankString(string strData)
		{
			if(strData == null)
				return true;
			else
			{
				for(int iCount = 0 ; iCount < strData.Length ; iCount++)
				{
					if(Char.IsWhiteSpace( strData[iCount])==false )
						return false;
				}
				return true;
			}
		}//public static bool isBlankString()

		/// <summary>
		/// 判断一个字符串是否有内容,本函数和isBlankString相反
		/// </summary>
		/// <param name="strData">字符串对象</param>
		/// <returns>若字符串不为空且存在非空白字符则返回True 否则返回False</returns>
		private static bool HasContent( string strData )
		{
			if( strData != null && strData.Length > 0 )
			{
				foreach(char c in strData )
				{
					if( Char.IsWhiteSpace( c ) == false)
						return true;
				}
			}
			return false;
		}// bool HasContent()
		/// <summary>
		/// 汉字拼音首字母列表 本列表包含了20902个汉字,用于配合 ToChineseSpell 函数使用,本表收录的字符的Unicode编码范围为19968至40869
		/// </summary>
		private const string strChineseFirstPY =
			"YDYQSXMWZSSXJBYMGCCZQPSSQBYCDSCDQLDYLYBSSJGYZZJJFKCCLZDHWDWZJLJPFYYNWJJTMYHZWZHFLZPPQHGSCYYYNJQYXXGJ"
			+ "HHSDSJNKKTMOMLCRXYPSNQSECCQZGGLLYJLMYZZSECYKYYHQWJSSGGYXYZYJWWKDJHYCHMYXJTLXJYQBYXZLDWRDJRWYSRLDZJPC"
			+ "BZJJBRCFTLECZSTZFXXZHTRQHYBDLYCZSSYMMRFMYQZPWWJJYFCRWFDFZQPYDDWYXKYJAWJFFXYPSFTZYHHYZYSWCJYXSCLCXXWZ"
			+ "ZXNBGNNXBXLZSZSBSGPYSYZDHMDZBQBZCWDZZYYTZHBTSYYBZGNTNXQYWQSKBPHHLXGYBFMJEBJHHGQTJCYSXSTKZHLYCKGLYSMZ"
			+ "XYALMELDCCXGZYRJXSDLTYZCQKCNNJWHJTZZCQLJSTSTBNXBTYXCEQXGKWJYFLZQLYHYXSPSFXLMPBYSXXXYDJCZYLLLSJXFHJXP"
			+ "JBTFFYABYXBHZZBJYZLWLCZGGBTSSMDTJZXPTHYQTGLJSCQFZKJZJQNLZWLSLHDZBWJNCJZYZSQQYCQYRZCJJWYBRTWPYFTWEXCS"
			+ "KDZCTBZHYZZYYJXZCFFZZMJYXXSDZZOTTBZLQWFCKSZSXFYRLNYJMBDTHJXSQQCCSBXYYTSYFBXDZTGBCNSLCYZZPSAZYZZSCJCS"
			+ "HZQYDXLBPJLLMQXTYDZXSQJTZPXLCGLQTZWJBHCTSYJSFXYEJJTLBGXSXJMYJQQPFZASYJNTYDJXKJCDJSZCBARTDCLYJQMWNQNC"
			+ "LLLKBYBZZSYHQQLTWLCCXTXLLZNTYLNEWYZYXCZXXGRKRMTCNDNJTSYYSSDQDGHSDBJGHRWRQLYBGLXHLGTGXBQJDZPYJSJYJCTM"
			+ "RNYMGRZJCZGJMZMGXMPRYXKJNYMSGMZJYMKMFXMLDTGFBHCJHKYLPFMDXLQJJSMTQGZSJLQDLDGJYCALCMZCSDJLLNXDJFFFFJCZ"
			+ "FMZFFPFKHKGDPSXKTACJDHHZDDCRRCFQYJKQCCWJDXHWJLYLLZGCFCQDSMLZPBJJPLSBCJGGDCKKDEZSQCCKJGCGKDJTJDLZYCXK"
			+ "LQSCGJCLTFPCQCZGWPJDQYZJJBYJHSJDZWGFSJGZKQCCZLLPSPKJGQJHZZLJPLGJGJJTHJJYJZCZMLZLYQBGJWMLJKXZDZNJQSYZ"
			+ "MLJLLJKYWXMKJLHSKJGBMCLYYMKXJQLBMLLKMDXXKWYXYSLMLPSJQQJQXYXFJTJDXMXXLLCXQBSYJBGWYMBGGBCYXPJYGPEPFGDJ"
			+ "GBHBNSQJYZJKJKHXQFGQZKFHYGKHDKLLSDJQXPQYKYBNQSXQNSZSWHBSXWHXWBZZXDMNSJBSBKBBZKLYLXGWXDRWYQZMYWSJQLCJ"
			+ "XXJXKJEQXSCYETLZHLYYYSDZPAQYZCMTLSHTZCFYZYXYLJSDCJQAGYSLCQLYYYSHMRQQKLDXZSCSSSYDYCJYSFSJBFRSSZQSBXXP"
			+ "XJYSDRCKGJLGDKZJZBDKTCSYQPYHSTCLDJDHMXMCGXYZHJDDTMHLTXZXYLYMOHYJCLTYFBQQXPFBDFHHTKSQHZYYWCNXXCRWHOWG"
			+ "YJLEGWDQCWGFJYCSNTMYTOLBYGWQWESJPWNMLRYDZSZTXYQPZGCWXHNGPYXSHMYQJXZTDPPBFYHZHTJYFDZWKGKZBLDNTSXHQEEG"
			+ "ZZYLZMMZYJZGXZXKHKSTXNXXWYLYAPSTHXDWHZYMPXAGKYDXBHNHXKDPJNMYHYLPMGOCSLNZHKXXLPZZLBMLSFBHHGYGYYGGBHSC"
			+ "YAQTYWLXTZQCEZYDQDQMMHTKLLSZHLSJZWFYHQSWSCWLQAZYNYTLSXTHAZNKZZSZZLAXXZWWCTGQQTDDYZTCCHYQZFLXPSLZYGPZ"
			+ "SZNGLNDQTBDLXGTCTAJDKYWNSYZLJHHZZCWNYYZYWMHYCHHYXHJKZWSXHZYXLYSKQYSPSLYZWMYPPKBYGLKZHTYXAXQSYSHXASMC"
			+ "HKDSCRSWJPWXSGZJLWWSCHSJHSQNHCSEGNDAQTBAALZZMSSTDQJCJKTSCJAXPLGGXHHGXXZCXPDMMHLDGTYBYSJMXHMRCPXXJZCK"
			+ "ZXSHMLQXXTTHXWZFKHCCZDYTCJYXQHLXDHYPJQXYLSYYDZOZJNYXQEZYSQYAYXWYPDGXDDXSPPYZNDLTWRHXYDXZZJHTCXMCZLHP"
			+ "YYYYMHZLLHNXMYLLLMDCPPXHMXDKYCYRDLTXJCHHZZXZLCCLYLNZSHZJZZLNNRLWHYQSNJHXYNTTTKYJPYCHHYEGKCTTWLGQRLGG"
			+ "TGTYGYHPYHYLQYQGCWYQKPYYYTTTTLHYHLLTYTTSPLKYZXGZWGPYDSSZZDQXSKCQNMJJZZBXYQMJRTFFBTKHZKBXLJJKDXJTLBWF"
			+ "ZPPTKQTZTGPDGNTPJYFALQMKGXBDCLZFHZCLLLLADPMXDJHLCCLGYHDZFGYDDGCYYFGYDXKSSEBDHYKDKDKHNAXXYBPBYYHXZQGA"
			+ "FFQYJXDMLJCSQZLLPCHBSXGJYNDYBYQSPZWJLZKSDDTACTBXZDYZYPJZQSJNKKTKNJDJGYYPGTLFYQKASDNTCYHBLWDZHBBYDWJR"
			+ "YGKZYHEYYFJMSDTYFZJJHGCXPLXHLDWXXJKYTCYKSSSMTWCTTQZLPBSZDZWZXGZAGYKTYWXLHLSPBCLLOQMMZSSLCMBJCSZZKYDC"
			+ "ZJGQQDSMCYTZQQLWZQZXSSFPTTFQMDDZDSHDTDWFHTDYZJYQJQKYPBDJYYXTLJHDRQXXXHAYDHRJLKLYTWHLLRLLRCXYLBWSRSZZ"
			+ "SYMKZZHHKYHXKSMDSYDYCJPBZBSQLFCXXXNXKXWYWSDZYQOGGQMMYHCDZTTFJYYBGSTTTYBYKJDHKYXBELHTYPJQNFXFDYKZHQKZ"
			+ "BYJTZBXHFDXKDASWTAWAJLDYJSFHBLDNNTNQJTJNCHXFJSRFWHZFMDRYJYJWZPDJKZYJYMPCYZNYNXFBYTFYFWYGDBNZZZDNYTXZ"
			+ "EMMQBSQEHXFZMBMFLZZSRXYMJGSXWZJSPRYDJSJGXHJJGLJJYNZZJXHGXKYMLPYYYCXYTWQZSWHWLYRJLPXSLSXMFSWWKLCTNXNY"
			+ "NPSJSZHDZEPTXMYYWXYYSYWLXJQZQXZDCLEEELMCPJPCLWBXSQHFWWTFFJTNQJHJQDXHWLBYZNFJLALKYYJLDXHHYCSTYYWNRJYX"
			+ "YWTRMDRQHWQCMFJDYZMHMYYXJWMYZQZXTLMRSPWWCHAQBXYGZYPXYYRRCLMPYMGKSJSZYSRMYJSNXTPLNBAPPYPYLXYYZKYNLDZY"
			+ "JZCZNNLMZHHARQMPGWQTZMXXMLLHGDZXYHXKYXYCJMFFYYHJFSBSSQLXXNDYCANNMTCJCYPRRNYTYQNYYMBMSXNDLYLYSLJRLXYS"
			+ "XQMLLYZLZJJJKYZZCSFBZXXMSTBJGNXYZHLXNMCWSCYZYFZLXBRNNNYLBNRTGZQYSATSWRYHYJZMZDHZGZDWYBSSCSKXSYHYTXXG"
			+ "CQGXZZSHYXJSCRHMKKBXCZJYJYMKQHZJFNBHMQHYSNJNZYBKNQMCLGQHWLZNZSWXKHLJHYYBQLBFCDSXDLDSPFZPSKJYZWZXZDDX"
			+ "JSMMEGJSCSSMGCLXXKYYYLNYPWWWGYDKZJGGGZGGSYCKNJWNJPCXBJJTQTJWDSSPJXZXNZXUMELPXFSXTLLXCLJXJJLJZXCTPSWX"
			+ "LYDHLYQRWHSYCSQYYBYAYWJJJQFWQCQQCJQGXALDBZZYJGKGXPLTZYFXJLTPADKYQHPMATLCPDCKBMTXYBHKLENXDLEEGQDYMSAW"
			+ "HZMLJTWYGXLYQZLJEEYYBQQFFNLYXRDSCTGJGXYYNKLLYQKCCTLHJLQMKKZGCYYGLLLJDZGYDHZWXPYSJBZKDZGYZZHYWYFQYTYZ"
			+ "SZYEZZLYMHJJHTSMQWYZLKYYWZCSRKQYTLTDXWCTYJKLWSQZWBDCQYNCJSRSZJLKCDCDTLZZZACQQZZDDXYPLXZBQJYLZLLLQDDZ"
			+ "QJYJYJZYXNYYYNYJXKXDAZWYRDLJYYYRJLXLLDYXJCYWYWNQCCLDDNYYYNYCKCZHXXCCLGZQJGKWPPCQQJYSBZZXYJSQPXJPZBSB"
			+ "DSFNSFPZXHDWZTDWPPTFLZZBZDMYYPQJRSDZSQZSQXBDGCPZSWDWCSQZGMDHZXMWWFYBPDGPHTMJTHZSMMBGZMBZJCFZWFZBBZMQ"
			+ "CFMBDMCJXLGPNJBBXGYHYYJGPTZGZMQBQTCGYXJXLWZKYDPDYMGCFTPFXYZTZXDZXTGKMTYBBCLBJASKYTSSQYYMSZXFJEWLXLLS"
			+ "ZBQJJJAKLYLXLYCCTSXMCWFKKKBSXLLLLJYXTYLTJYYTDPJHNHNNKBYQNFQYYZBYYESSESSGDYHFHWTCJBSDZZTFDMXHCNJZYMQW"
			+ "SRYJDZJQPDQBBSTJGGFBKJBXTGQHNGWJXJGDLLTHZHHYYYYYYSXWTYYYCCBDBPYPZYCCZYJPZYWCBDLFWZCWJDXXHYHLHWZZXJTC"
			+ "ZLCDPXUJCZZZLYXJJTXPHFXWPYWXZPTDZZBDZCYHJHMLXBQXSBYLRDTGJRRCTTTHYTCZWMXFYTWWZCWJWXJYWCSKYBZSCCTZQNHX"
			+ "NWXXKHKFHTSWOCCJYBCMPZZYKBNNZPBZHHZDLSYDDYTYFJPXYNGFXBYQXCBHXCPSXTYZDMKYSNXSXLHKMZXLYHDHKWHXXSSKQYHH"
			+ "CJYXGLHZXCSNHEKDTGZXQYPKDHEXTYKCNYMYYYPKQYYYKXZLTHJQTBYQHXBMYHSQCKWWYLLHCYYLNNEQXQWMCFBDCCMLJGGXDQKT"
			+ "LXKGNQCDGZJWYJJLYHHQTTTNWCHMXCXWHWSZJYDJCCDBQCDGDNYXZTHCQRXCBHZTQCBXWGQWYYBXHMBYMYQTYEXMQKYAQYRGYZSL"
			+ "FYKKQHYSSQYSHJGJCNXKZYCXSBXYXHYYLSTYCXQTHYSMGSCPMMGCCCCCMTZTASMGQZJHKLOSQYLSWTMXSYQKDZLJQQYPLSYCZTCQ"
			+ "QPBBQJZCLPKHQZYYXXDTDDTSJCXFFLLCHQXMJLWCJCXTSPYCXNDTJSHJWXDQQJSKXYAMYLSJHMLALYKXCYYDMNMDQMXMCZNNCYBZ"
			+ "KKYFLMCHCMLHXRCJJHSYLNMTJZGZGYWJXSRXCWJGJQHQZDQJDCJJZKJKGDZQGJJYJYLXZXXCDQHHHEYTMHLFSBDJSYYSHFYSTCZQ"
			+ "LPBDRFRZTZYKYWHSZYQKWDQZRKMSYNBCRXQBJYFAZPZZEDZCJYWBCJWHYJBQSZYWRYSZPTDKZPFPBNZTKLQYHBBZPNPPTYZZYBQN"
			+ "YDCPJMMCYCQMCYFZZDCMNLFPBPLNGQJTBTTNJZPZBBZNJKLJQYLNBZQHKSJZNGGQSZZKYXSHPZSNBCGZKDDZQANZHJKDRTLZLSWJ"
			+ "LJZLYWTJNDJZJHXYAYNCBGTZCSSQMNJPJYTYSWXZFKWJQTKHTZPLBHSNJZSYZBWZZZZLSYLSBJHDWWQPSLMMFBJDWAQYZTCJTBNN"
			+ "WZXQXCDSLQGDSDPDZHJTQQPSWLYYJZLGYXYZLCTCBJTKTYCZJTQKBSJLGMGZDMCSGPYNJZYQYYKNXRPWSZXMTNCSZZYXYBYHYZAX"
			+ "YWQCJTLLCKJJTJHGDXDXYQYZZBYWDLWQCGLZGJGQRQZCZSSBCRPCSKYDZNXJSQGXSSJMYDNSTZTPBDLTKZWXQWQTZEXNQCZGWEZK"
			+ "SSBYBRTSSSLCCGBPSZQSZLCCGLLLZXHZQTHCZMQGYZQZNMCOCSZJMMZSQPJYGQLJYJPPLDXRGZYXCCSXHSHGTZNLZWZKJCXTCFCJ"
			+ "XLBMQBCZZWPQDNHXLJCTHYZLGYLNLSZZPCXDSCQQHJQKSXZPBAJYEMSMJTZDXLCJYRYYNWJBNGZZTMJXLTBSLYRZPYLSSCNXPHLL"
			+ "HYLLQQZQLXYMRSYCXZLMMCZLTZSDWTJJLLNZGGQXPFSKYGYGHBFZPDKMWGHCXMSGDXJMCJZDYCABXJDLNBCDQYGSKYDQTXDJJYXM"
			+ "SZQAZDZFSLQXYJSJZYLBTXXWXQQZBJZUFBBLYLWDSLJHXJYZJWTDJCZFQZQZZDZSXZZQLZCDZFJHYSPYMPQZMLPPLFFXJJNZZYLS"
			+ "JEYQZFPFZKSYWJJJHRDJZZXTXXGLGHYDXCSKYSWMMZCWYBAZBJKSHFHJCXMHFQHYXXYZFTSJYZFXYXPZLCHMZMBXHZZSXYFYMNCW"
			+ "DABAZLXKTCSHHXKXJJZJSTHYGXSXYYHHHJWXKZXSSBZZWHHHCWTZZZPJXSNXQQJGZYZYWLLCWXZFXXYXYHXMKYYSWSQMNLNAYCYS"
			+ "PMJKHWCQHYLAJJMZXHMMCNZHBHXCLXTJPLTXYJHDYYLTTXFSZHYXXSJBJYAYRSMXYPLCKDUYHLXRLNLLSTYZYYQYGYHHSCCSMZCT"
			+ "ZQXKYQFPYYRPFFLKQUNTSZLLZMWWTCQQYZWTLLMLMPWMBZSSTZRBPDDTLQJJBXZCSRZQQYGWCSXFWZLXCCRSZDZMCYGGDZQSGTJS"
			+ "WLJMYMMZYHFBJDGYXCCPSHXNZCSBSJYJGJMPPWAFFYFNXHYZXZYLREMZGZCYZSSZDLLJCSQFNXZKPTXZGXJJGFMYYYSNBTYLBNLH"
			+ "PFZDCYFBMGQRRSSSZXYSGTZRNYDZZCDGPJAFJFZKNZBLCZSZPSGCYCJSZLMLRSZBZZLDLSLLYSXSQZQLYXZLSKKBRXBRBZCYCXZZ"
			+ "ZEEYFGKLZLYYHGZSGZLFJHGTGWKRAAJYZKZQTSSHJJXDCYZUYJLZYRZDQQHGJZXSSZBYKJPBFRTJXLLFQWJHYLQTYMBLPZDXTZYG"
			+ "BDHZZRBGXHWNJTJXLKSCFSMWLSDQYSJTXKZSCFWJLBXFTZLLJZLLQBLSQMQQCGCZFPBPHZCZJLPYYGGDTGWDCFCZQYYYQYSSCLXZ"
			+ "SKLZZZGFFCQNWGLHQYZJJCZLQZZYJPJZZBPDCCMHJGXDQDGDLZQMFGPSYTSDYFWWDJZJYSXYYCZCYHZWPBYKXRYLYBHKJKSFXTZJ"
			+ "MMCKHLLTNYYMSYXYZPYJQYCSYCWMTJJKQYRHLLQXPSGTLYYCLJSCPXJYZFNMLRGJJTYZBXYZMSJYJHHFZQMSYXRSZCWTLRTQZSST"
			+ "KXGQKGSPTGCZNJSJCQCXHMXGGZTQYDJKZDLBZSXJLHYQGGGTHQSZPYHJHHGYYGKGGCWJZZYLCZLXQSFTGZSLLLMLJSKCTBLLZZSZ"
			+ "MMNYTPZSXQHJCJYQXYZXZQZCPSHKZZYSXCDFGMWQRLLQXRFZTLYSTCTMJCXJJXHJNXTNRZTZFQYHQGLLGCXSZSJDJLJCYDSJTLNY"
			+ "XHSZXCGJZYQPYLFHDJSBPCCZHJJJQZJQDYBSSLLCMYTTMQTBHJQNNYGKYRQYQMZGCJKPDCGMYZHQLLSLLCLMHOLZGDYYFZSLJCQZ"
			+ "LYLZQJESHNYLLJXGJXLYSYYYXNBZLJSSZCQQCJYLLZLTJYLLZLLBNYLGQCHXYYXOXCXQKYJXXXYKLXSXXYQXCYKQXQCSGYXXYQXY"
			+ "GYTQOHXHXPYXXXULCYEYCHZZCBWQBBWJQZSCSZSSLZYLKDESJZWMYMCYTSDSXXSCJPQQSQYLYYZYCMDJDZYWCBTJSYDJKCYDDJLB"
			+ "DJJSODZYSYXQQYXDHHGQQYQHDYXWGMMMAJDYBBBPPBCMUUPLJZSMTXERXJMHQNUTPJDCBSSMSSSTKJTSSMMTRCPLZSZMLQDSDMJM"
			+ "QPNQDXCFYNBFSDQXYXHYAYKQYDDLQYYYSSZBYDSLNTFQTZQPZMCHDHCZCWFDXTMYQSPHQYYXSRGJCWTJTZZQMGWJJTJHTQJBBHWZ"
			+ "PXXHYQFXXQYWYYHYSCDYDHHQMNMTMWCPBSZPPZZGLMZFOLLCFWHMMSJZTTDHZZYFFYTZZGZYSKYJXQYJZQBHMBZZLYGHGFMSHPZF"
			+ "ZSNCLPBQSNJXZSLXXFPMTYJYGBXLLDLXPZJYZJYHHZCYWHJYLSJEXFSZZYWXKZJLUYDTMLYMQJPWXYHXSKTQJEZRPXXZHHMHWQPW"
			+ "QLYJJQJJZSZCPHJLCHHNXJLQWZJHBMZYXBDHHYPZLHLHLGFWLCHYYTLHJXCJMSCPXSTKPNHQXSRTYXXTESYJCTLSSLSTDLLLWWYH"
			+ "DHRJZSFGXTSYCZYNYHTDHWJSLHTZDQDJZXXQHGYLTZPHCSQFCLNJTCLZPFSTPDYNYLGMJLLYCQHYSSHCHYLHQYQTMZYPBYWRFQYK"
			+ "QSYSLZDQJMPXYYSSRHZJNYWTQDFZBWWTWWRXCWHGYHXMKMYYYQMSMZHNGCEPMLQQMTCWCTMMPXJPJJHFXYYZSXZHTYBMSTSYJTTQ"
			+ "QQYYLHYNPYQZLCYZHZWSMYLKFJXLWGXYPJYTYSYXYMZCKTTWLKSMZSYLMPWLZWXWQZSSAQSYXYRHSSNTSRAPXCPWCMGDXHXZDZYF"
			+ "JHGZTTSBJHGYZSZYSMYCLLLXBTYXHBBZJKSSDMALXHYCFYGMQYPJYCQXJLLLJGSLZGQLYCJCCZOTYXMTMTTLLWTGPXYMZMKLPSZZ"
			+ "ZXHKQYSXCTYJZYHXSHYXZKXLZWPSQPYHJWPJPWXQQYLXSDHMRSLZZYZWTTCYXYSZZSHBSCCSTPLWSSCJCHNLCGCHSSPHYLHFHHXJ"
			+ "SXYLLNYLSZDHZXYLSXLWZYKCLDYAXZCMDDYSPJTQJZLNWQPSSSWCTSTSZLBLNXSMNYYMJQBQHRZWTYYDCHQLXKPZWBGQYBKFCMZW"
			+ "PZLLYYLSZYDWHXPSBCMLJBSCGBHXLQHYRLJXYSWXWXZSLDFHLSLYNJLZYFLYJYCDRJLFSYZFSLLCQYQFGJYHYXZLYLMSTDJCYHBZ"
			+ "LLNWLXXYGYYHSMGDHXXHHLZZJZXCZZZCYQZFNGWPYLCPKPYYPMCLQKDGXZGGWQBDXZZKZFBXXLZXJTPJPTTBYTSZZDWSLCHZHSLT"
			+ "YXHQLHYXXXYYZYSWTXZKHLXZXZPYHGCHKCFSYHUTJRLXFJXPTZTWHPLYXFCRHXSHXKYXXYHZQDXQWULHYHMJTBFLKHTXCWHJFWJC"
			+ "FPQRYQXCYYYQYGRPYWSGSUNGWCHKZDXYFLXXHJJBYZWTSXXNCYJJYMSWZJQRMHXZWFQSYLZJZGBHYNSLBGTTCSYBYXXWXYHXYYXN"
			+ "SQYXMQYWRGYQLXBBZLJSYLPSYTJZYHYZAWLRORJMKSCZJXXXYXCHDYXRYXXJDTSQFXLYLTSFFYXLMTYJMJUYYYXLTZCSXQZQHZXL"
			+ "YYXZHDNBRXXXJCTYHLBRLMBRLLAXKYLLLJLYXXLYCRYLCJTGJCMTLZLLCYZZPZPCYAWHJJFYBDYYZSMPCKZDQYQPBPCJPDCYZMDP"
			+ "BCYYDYCNNPLMTMLRMFMMGWYZBSJGYGSMZQQQZTXMKQWGXLLPJGZBQCDJJJFPKJKCXBLJMSWMDTQJXLDLPPBXCWRCQFBFQJCZAHZG"
			+ "MYKPHYYHZYKNDKZMBPJYXPXYHLFPNYYGXJDBKXNXHJMZJXSTRSTLDXSKZYSYBZXJLXYSLBZYSLHXJPFXPQNBYLLJQKYGZMCYZZYM"
			+ "CCSLCLHZFWFWYXZMWSXTYNXJHPYYMCYSPMHYSMYDYSHQYZCHMJJMZCAAGCFJBBHPLYZYLXXSDJGXDHKXXTXXNBHRMLYJSLTXMRHN"
			+ "LXQJXYZLLYSWQGDLBJHDCGJYQYCMHWFMJYBMBYJYJWYMDPWHXQLDYGPDFXXBCGJSPCKRSSYZJMSLBZZJFLJJJLGXZGYXYXLSZQYX"
			+ "BEXYXHGCXBPLDYHWETTWWCJMBTXCHXYQXLLXFLYXLLJLSSFWDPZSMYJCLMWYTCZPCHQEKCQBWLCQYDPLQPPQZQFJQDJHYMMCXTXD"
			+ "RMJWRHXCJZYLQXDYYNHYYHRSLSRSYWWZJYMTLTLLGTQCJZYABTCKZCJYCCQLJZQXALMZYHYWLWDXZXQDLLQSHGPJFJLJHJABCQZD"
			+ "JGTKHSSTCYJLPSWZLXZXRWGLDLZRLZXTGSLLLLZLYXXWGDZYGBDPHZPBRLWSXQBPFDWOFMWHLYPCBJCCLDMBZPBZZLCYQXLDOMZB"
			+ "LZWPDWYYGDSTTHCSQSCCRSSSYSLFYBFNTYJSZDFNDPDHDZZMBBLSLCMYFFGTJJQWFTMTPJWFNLBZCMMJTGBDZLQLPYFHYYMJYLSD"
			+ "CHDZJWJCCTLJCLDTLJJCPDDSQDSSZYBNDBJLGGJZXSXNLYCYBJXQYCBYLZCFZPPGKCXZDZFZTJJFJSJXZBNZYJQTTYJYHTYCZHYM"
			+ "DJXTTMPXSPLZCDWSLSHXYPZGTFMLCJTYCBPMGDKWYCYZCDSZZYHFLYCTYGWHKJYYLSJCXGYWJCBLLCSNDDBTZBSCLYZCZZSSQDLL"
			+ "MQYYHFSLQLLXFTYHABXGWNYWYYPLLSDLDLLBJCYXJZMLHLJDXYYQYTDLLLBUGBFDFBBQJZZMDPJHGCLGMJJPGAEHHBWCQXAXHHHZ"
			+ "CHXYPHJAXHLPHJPGPZJQCQZGJJZZUZDMQYYBZZPHYHYBWHAZYJHYKFGDPFQSDLZMLJXKXGALXZDAGLMDGXMWZQYXXDXXPFDMMSSY"
			+ "MPFMDMMKXKSYZYSHDZKXSYSMMZZZMSYDNZZCZXFPLSTMZDNMXCKJMZTYYMZMZZMSXHHDCZJEMXXKLJSTLWLSQLYJZLLZJSSDPPMH"
			+ "NLZJCZYHMXXHGZCJMDHXTKGRMXFWMCGMWKDTKSXQMMMFZZYDKMSCLCMPCGMHSPXQPZDSSLCXKYXTWLWJYAHZJGZQMCSNXYYMMPML"
			+ "KJXMHLMLQMXCTKZMJQYSZJSYSZHSYJZJCDAJZYBSDQJZGWZQQXFKDMSDJLFWEHKZQKJPEYPZYSZCDWYJFFMZZYLTTDZZEFMZLBNP"
			+ "PLPLPEPSZALLTYLKCKQZKGENQLWAGYXYDPXLHSXQQWQCQXQCLHYXXMLYCCWLYMQYSKGCHLCJNSZKPYZKCQZQLJPDMDZHLASXLBYD"
			+ "WQLWDNBQCRYDDZTJYBKBWSZDXDTNPJDTCTQDFXQQMGNXECLTTBKPWSLCTYQLPWYZZKLPYGZCQQPLLKCCYLPQMZCZQCLJSLQZDJXL"
			+ "DDHPZQDLJJXZQDXYZQKZLJCYQDYJPPYPQYKJYRMPCBYMCXKLLZLLFQPYLLLMBSGLCYSSLRSYSQTMXYXZQZFDZUYSYZTFFMZZSMZQ"
			+ "HZSSCCMLYXWTPZGXZJGZGSJSGKDDHTQGGZLLBJDZLCBCHYXYZHZFYWXYZYMSDBZZYJGTSMTFXQYXQSTDGSLNXDLRYZZLRYYLXQHT"
			+ "XSRTZNGZXBNQQZFMYKMZJBZYMKBPNLYZPBLMCNQYZZZSJZHJCTZKHYZZJRDYZHNPXGLFZTLKGJTCTSSYLLGZRZBBQZZKLPKLCZYS"
			+ "SUYXBJFPNJZZXCDWXZYJXZZDJJKGGRSRJKMSMZJLSJYWQSKYHQJSXPJZZZLSNSHRNYPZTWCHKLPSRZLZXYJQXQKYSJYCZTLQZYBB"
			+ "YBWZPQDWWYZCYTJCJXCKCWDKKZXSGKDZXWWYYJQYYTCYTDLLXWKCZKKLCCLZCQQDZLQLCSFQCHQHSFSMQZZLNBJJZBSJHTSZDYSJ"
			+ "QJPDLZCDCWJKJZZLPYCGMZWDJJBSJQZSYZYHHXJPBJYDSSXDZNCGLQMBTSFSBPDZDLZNFGFJGFSMPXJQLMBLGQCYYXBQKDJJQYRF"
			+ "KZTJDHCZKLBSDZCFJTPLLJGXHYXZCSSZZXSTJYGKGCKGYOQXJPLZPBPGTGYJZGHZQZZLBJLSQFZGKQQJZGYCZBZQTLDXRJXBSXXP"
			+ "ZXHYZYCLWDXJJHXMFDZPFZHQHQMQGKSLYHTYCGFRZGNQXCLPDLBZCSCZQLLJBLHBZCYPZZPPDYMZZSGYHCKCPZJGSLJLNSCDSLDL"
			+ "XBMSTLDDFJMKDJDHZLZXLSZQPQPGJLLYBDSZGQLBZLSLKYYHZTTNTJYQTZZPSZQZTLLJTYYLLQLLQYZQLBDZLSLYYZYMDFSZSNHL"
			+ "XZNCZQZPBWSKRFBSYZMTHBLGJPMCZZLSTLXSHTCSYZLZBLFEQHLXFLCJLYLJQCBZLZJHHSSTBRMHXZHJZCLXFNBGXGTQJCZTMSFZ"
			+ "KJMSSNXLJKBHSJXNTNLZDNTLMSJXGZJYJCZXYJYJWRWWQNZTNFJSZPZSHZJFYRDJSFSZJZBJFZQZZHZLXFYSBZQLZSGYFTZDCSZX"
			+ "ZJBQMSZKJRHYJZCKMJKHCHGTXKXQGLXPXFXTRTYLXJXHDTSJXHJZJXZWZLCQSBTXWXGXTXXHXFTSDKFJHZYJFJXRZSDLLLTQSQQZ"
			+ "QWZXSYQTWGWBZCGZLLYZBCLMQQTZHZXZXLJFRMYZFLXYSQXXJKXRMQDZDMMYYBSQBHGZMWFWXGMXLZPYYTGZYCCDXYZXYWGSYJYZ"
			+ "NBHPZJSQSYXSXRTFYZGRHZTXSZZTHCBFCLSYXZLZQMZLMPLMXZJXSFLBYZMYQHXJSXRXSQZZZSSLYFRCZJRCRXHHZXQYDYHXSJJH"
			+ "ZCXZBTYNSYSXJBQLPXZQPYMLXZKYXLXCJLCYSXXZZLXDLLLJJYHZXGYJWKJRWYHCPSGNRZLFZWFZZNSXGXFLZSXZZZBFCSYJDBRJ"
			+ "KRDHHGXJLJJTGXJXXSTJTJXLYXQFCSGSWMSBCTLQZZWLZZKXJMLTMJYHSDDBXGZHDLBMYJFRZFSGCLYJBPMLYSMSXLSZJQQHJZFX"
			+ "GFQFQBPXZGYYQXGZTCQWYLTLGWSGWHRLFSFGZJMGMGBGTJFSYZZGZYZAFLSSPMLPFLCWBJZCLJJMZLPJJLYMQDMYYYFBGYGYZMLY"
			+ "ZDXQYXRQQQHSYYYQXYLJTYXFSFSLLGNQCYHYCWFHCCCFXPYLYPLLZYXXXXXKQHHXSHJZCFZSCZJXCPZWHHHHHAPYLQALPQAFYHXD"
			+ "YLUKMZQGGGDDESRNNZLTZGCHYPPYSQJJHCLLJTOLNJPZLJLHYMHEYDYDSQYCDDHGZUNDZCLZYZLLZNTNYZGSLHSLPJJBDGWXPCDU"
			+ "TJCKLKCLWKLLCASSTKZZDNQNTTLYYZSSYSSZZRYLJQKCQDHHCRXRZYDGRGCWCGZQFFFPPJFZYNAKRGYWYQPQXXFKJTSZZXSWZDDF"
			+ "BBXTBGTZKZNPZZPZXZPJSZBMQHKCYXYLDKLJNYPKYGHGDZJXXEAHPNZKZTZCMXCXMMJXNKSZQNMNLWBWWXJKYHCPSTMCSQTZJYXT"
			+ "PCTPDTNNPGLLLZSJLSPBLPLQHDTNJNLYYRSZFFJFQWDPHZDWMRZCCLODAXNSSNYZRESTYJWJYJDBCFXNMWTTBYLWSTSZGYBLJPXG"
			+ "LBOCLHPCBJLTMXZLJYLZXCLTPNCLCKXTPZJSWCYXSFYSZDKNTLBYJCYJLLSTGQCBXRYZXBXKLYLHZLQZLNZCXWJZLJZJNCJHXMNZ"
			+ "ZGJZZXTZJXYCYYCXXJYYXJJXSSSJSTSSTTPPGQTCSXWZDCSYFPTFBFHFBBLZJCLZZDBXGCXLQPXKFZFLSYLTUWBMQJHSZBMDDBCY"
			+ "SCCLDXYCDDQLYJJWMQLLCSGLJJSYFPYYCCYLTJANTJJPWYCMMGQYYSXDXQMZHSZXPFTWWZQSWQRFKJLZJQQYFBRXJHHFWJJZYQAZ"
			+ "MYFRHCYYBYQWLPEXCCZSTYRLTTDMQLYKMBBGMYYJPRKZNPBSXYXBHYZDJDNGHPMFSGMWFZMFQMMBCMZZCJJLCNUXYQLMLRYGQZCY"
			+ "XZLWJGCJCGGMCJNFYZZJHYCPRRCMTZQZXHFQGTJXCCJEAQCRJYHPLQLSZDJRBCQHQDYRHYLYXJSYMHZYDWLDFRYHBPYDTSSCNWBX"
			+ "GLPZMLZZTQSSCPJMXXYCSJYTYCGHYCJWYRXXLFEMWJNMKLLSWTXHYYYNCMMCWJDQDJZGLLJWJRKHPZGGFLCCSCZMCBLTBHBQJXQD"
			+ "SPDJZZGKGLFQYWBZYZJLTSTDHQHCTCBCHFLQMPWDSHYYTQWCNZZJTLBYMBPDYYYXSQKXWYYFLXXNCWCXYPMAELYKKJMZZZBRXYYQ"
			+ "JFLJPFHHHYTZZXSGQQMHSPGDZQWBWPJHZJDYSCQWZKTXXSQLZYYMYSDZGRXCKKUJLWPYSYSCSYZLRMLQSYLJXBCXTLWDQZPCYCYK"
			+ "PPPNSXFYZJJRCEMHSZMSXLXGLRWGCSTLRSXBZGBZGZTCPLUJLSLYLYMTXMTZPALZXPXJTJWTCYYZLBLXBZLQMYLXPGHDSLSSDMXM"
			+ "BDZZSXWHAMLCZCPJMCNHJYSNSYGCHSKQMZZQDLLKABLWJXSFMOCDXJRRLYQZKJMYBYQLYHETFJZFRFKSRYXFJTWDSXXSYSQJYSLY"
			+ "XWJHSNLXYYXHBHAWHHJZXWMYLJCSSLKYDZTXBZSYFDXGXZJKHSXXYBSSXDPYNZWRPTQZCZENYGCXQFJYKJBZMLJCMQQXUOXSLYXX"
			+ "LYLLJDZBTYMHPFSTTQQWLHOKYBLZZALZXQLHZWRRQHLSTMYPYXJJXMQSJFNBXYXYJXXYQYLTHYLQYFMLKLJTMLLHSZWKZHLJMLHL"
			+ "JKLJSTLQXYLMBHHLNLZXQJHXCFXXLHYHJJGBYZZKBXSCQDJQDSUJZYYHZHHMGSXCSYMXFEBCQWWRBPYYJQTYZCYQYQQZYHMWFFHG"
			+ "ZFRJFCDPXNTQYZPDYKHJLFRZXPPXZDBBGZQSTLGDGYLCQMLCHHMFYWLZYXKJLYPQHSYWMQQGQZMLZJNSQXJQSYJYCBEHSXFSZPXZ"
			+ "WFLLBCYYJDYTDTHWZSFJMQQYJLMQXXLLDTTKHHYBFPWTYYSQQWNQWLGWDEBZWCMYGCULKJXTMXMYJSXHYBRWFYMWFRXYQMXYSZTZ"
			+ "ZTFYKMLDHQDXWYYNLCRYJBLPSXCXYWLSPRRJWXHQYPHTYDNXHHMMYWYTZCSQMTSSCCDALWZTCPQPYJLLQZYJSWXMZZMMYLMXCLMX"
			+ "CZMXMZSQTZPPQQBLPGXQZHFLJJHYTJSRXWZXSCCDLXTYJDCQJXSLQYCLZXLZZXMXQRJMHRHZJBHMFLJLMLCLQNLDXZLLLPYPSYJY"
			+ "SXCQQDCMQJZZXHNPNXZMEKMXHYKYQLXSXTXJYYHWDCWDZHQYYBGYBCYSCFGPSJNZDYZZJZXRZRQJJYMCANYRJTLDPPYZBSTJKXXZ"
			+ "YPFDWFGZZRPYMTNGXZQBYXNBUFNQKRJQZMJEGRZGYCLKXZDSKKNSXKCLJSPJYYZLQQJYBZSSQLLLKJXTBKTYLCCDDBLSPPFYLGYD"
			+ "TZJYQGGKQTTFZXBDKTYYHYBBFYTYYBCLPDYTGDHRYRNJSPTCSNYJQHKLLLZSLYDXXWBCJQSPXBPJZJCJDZFFXXBRMLAZHCSNDLBJ"
			+ "DSZBLPRZTSWSBXBCLLXXLZDJZSJPYLYXXYFTFFFBHJJXGBYXJPMMMPSSJZJMTLYZJXSWXTYLEDQPJMYGQZJGDJLQJWJQLLSJGJGY"
			+ "GMSCLJJXDTYGJQJQJCJZCJGDZZSXQGSJGGCXHQXSNQLZZBXHSGZXCXYLJXYXYYDFQQJHJFXDHCTXJYRXYSQTJXYEFYYSSYYJXNCY"
			+ "ZXFXMSYSZXYYSCHSHXZZZGZZZGFJDLTYLNPZGYJYZYYQZPBXQBDZTZCZYXXYHHSQXSHDHGQHJHGYWSZTMZMLHYXGEBTYLZKQWYTJ"
			+ "ZRCLEKYSTDBCYKQQSAYXCJXWWGSBHJYZYDHCSJKQCXSWXFLTYNYZPZCCZJQTZWJQDZZZQZLJJXLSBHPYXXPSXSHHEZTXFPTLQYZZ"
			+ "XHYTXNCFZYYHXGNXMYWXTZSJPTHHGYMXMXQZXTSBCZYJYXXTYYZYPCQLMMSZMJZZLLZXGXZAAJZYXJMZXWDXZSXZDZXLEYJJZQBH"
			+ "ZWZZZQTZPSXZTDSXJJJZNYAZPHXYYSRNQDTHZHYYKYJHDZXZLSWCLYBZYECWCYCRYLCXNHZYDZYDYJDFRJJHTRSQTXYXJRJHOJYN"
			+ "XELXSFSFJZGHPZSXZSZDZCQZBYYKLSGSJHCZSHDGQGXYZGXCHXZJWYQWGYHKSSEQZZNDZFKWYSSTCLZSTSYMCDHJXXYWEYXCZAYD"
			+ "MPXMDSXYBSQMJMZJMTZQLPJYQZCGQHXJHHLXXHLHDLDJQCLDWBSXFZZYYSCHTYTYYBHECXHYKGJPXHHYZJFXHWHBDZFYZBCAPNPG"
			+ "NYDMSXHMMMMAMYNBYJTMPXYYMCTHJBZYFCGTYHWPHFTWZZEZSBZEGPFMTSKFTYCMHFLLHGPZJXZJGZJYXZSBBQSCZZLZCCSTPGXM"
			+ "JSFTCCZJZDJXCYBZLFCJSYZFGSZLYBCWZZBYZDZYPSWYJZXZBDSYUXLZZBZFYGCZXBZHZFTPBGZGEJBSTGKDMFHYZZJHZLLZZGJQ"
			+ "ZLSFDJSSCBZGPDLFZFZSZYZYZSYGCXSNXXCHCZXTZZLJFZGQSQYXZJQDCCZTQCDXZJYQJQCHXZTDLGSCXZSYQJQTZWLQDQZTQCHQ"
			+ "QJZYEZZZPBWKDJFCJPZTYPQYQTTYNLMBDKTJZPQZQZZFPZSBNJLGYJDXJDZZKZGQKXDLPZJTCJDQBXDJQJSTCKNXBXZMSLYJCQMT"
			+ "JQWWCJQNJNLLLHJCWQTBZQYDZCZPZZDZYDDCYZZZCCJTTJFZDPRRTZTJDCQTQZDTJNPLZBCLLCTZSXKJZQZPZLBZRBTJDCXFCZDB"
			+ "CCJJLTQQPLDCGZDBBZJCQDCJWYNLLZYZCCDWLLXWZLXRXNTQQCZXKQLSGDFQTDDGLRLAJJTKUYMKQLLTZYTDYYCZGJWYXDXFRSKS"
			+ "TQTENQMRKQZHHQKDLDAZFKYPBGGPZREBZZYKZZSPEGJXGYKQZZZSLYSYYYZWFQZYLZZLZHWCHKYPQGNPGBLPLRRJYXCCSYYHSFZF"
			+ "YBZYYTGZXYLXCZWXXZJZBLFFLGSKHYJZEYJHLPLLLLCZGXDRZELRHGKLZZYHZLYQSZZJZQLJZFLNBHGWLCZCFJYSPYXZLZLXGCCP"
			+ "ZBLLCYBBBBUBBCBPCRNNZCZYRBFSRLDCGQYYQXYGMQZWTZYTYJXYFWTEHZZJYWLCCNTZYJJZDEDPZDZTSYQJHDYMBJNYJZLXTSST"
			+ "PHNDJXXBYXQTZQDDTJTDYYTGWSCSZQFLSHLGLBCZPHDLYZJYCKWTYTYLBNYTSDSYCCTYSZYYEBHEXHQDTWNYGYCLXTSZYSTQMYGZ"
			+ "AZCCSZZDSLZCLZRQXYYELJSBYMXSXZTEMBBLLYYLLYTDQYSHYMRQWKFKBFXNXSBYCHXBWJYHTQBPBSBWDZYLKGZSKYHXQZJXHXJX"
			+ "GNLJKZLYYCDXLFYFGHLJGJYBXQLYBXQPQGZTZPLNCYPXDJYQYDYMRBESJYYHKXXSTMXRCZZYWXYQYBMCLLYZHQYZWQXDBXBZWZMS"
			+ "LPDMYSKFMZKLZCYQYCZLQXFZZYDQZPZYGYJYZMZXDZFYFYTTQTZHGSPCZMLCCYTZXJCYTJMKSLPZHYSNZLLYTPZCTZZCKTXDHXXT"
			+ "QCYFKSMQCCYYAZHTJPCYLZLYJBJXTPNYLJYYNRXSYLMMNXJSMYBCSYSYLZYLXJJQYLDZLPQBFZZBLFNDXQKCZFYWHGQMRDSXYCYT"
			+ "XNQQJZYYPFZXDYZFPRXEJDGYQBXRCNFYYQPGHYJDYZXGRHTKYLNWDZNTSMPKLBTHBPYSZBZTJZSZZJTYYXZPHSSZZBZCZPTQFZMY"
			+ "FLYPYBBJQXZMXXDJMTSYSKKBJZXHJCKLPSMKYJZCXTMLJYXRZZQSLXXQPYZXMKYXXXJCLJPRMYYGADYSKQLSNDHYZKQXZYZTCGHZ"
			+ "TLMLWZYBWSYCTBHJHJFCWZTXWYTKZLXQSHLYJZJXTMPLPYCGLTBZZTLZJCYJGDTCLKLPLLQPJMZPAPXYZLKKTKDZCZZBNZDYDYQZ"
			+ "JYJGMCTXLTGXSZLMLHBGLKFWNWZHDXUHLFMKYSLGXDTWWFRJEJZTZHYDXYKSHWFZCQSHKTMQQHTZHYMJDJSKHXZJZBZZXYMPAGQM"
			+ "STPXLSKLZYNWRTSQLSZBPSPSGZWYHTLKSSSWHZZLYYTNXJGMJSZSUFWNLSOZTXGXLSAMMLBWLDSZYLAKQCQCTMYCFJBSLXCLZZCL"
			+ "XXKSBZQCLHJPSQPLSXXCKSLNHPSFQQYTXYJZLQLDXZQJZDYYDJNZPTUZDSKJFSLJHYLZSQZLBTXYDGTQFDBYAZXDZHZJNHHQBYKN"
			+ "XJJQCZMLLJZKSPLDYCLBBLXKLELXJLBQYCXJXGCNLCQPLZLZYJTZLJGYZDZPLTQCSXFDMNYCXGBTJDCZNBGBQYQJWGKFHTNPYQZQ"
			+ "GBKPBBYZMTJDYTBLSQMPSXTBNPDXKLEMYYCJYNZCTLDYKZZXDDXHQSHDGMZSJYCCTAYRZLPYLTLKXSLZCGGEXCLFXLKJRTLQJAQZ"
			+ "NCMBYDKKCXGLCZJZXJHPTDJJMZQYKQSECQZDSHHADMLZFMMZBGNTJNNLGBYJBRBTMLBYJDZXLCJLPLDLPCQDHLXZLYCBLCXZZJAD"
			+ "JLNZMMSSSMYBHBSQKBHRSXXJMXSDZNZPXLGBRHWGGFCXGMSKLLTSJYYCQLTSKYWYYHYWXBXQYWPYWYKQLSQPTNTKHQCWDQKTWPXX"
			+ "HCPTHTWUMSSYHBWCRWXHJMKMZNGWTMLKFGHKJYLSYYCXWHYECLQHKQHTTQKHFZLDXQWYZYYDESBPKYRZPJFYYZJCEQDZZDLATZBB"
			+ "FJLLCXDLMJSSXEGYGSJQXCWBXSSZPDYZCXDNYXPPZYDLYJCZPLTXLSXYZYRXCYYYDYLWWNZSAHJSYQYHGYWWAXTJZDAXYSRLTDPS"
			+ "SYYFNEJDXYZHLXLLLZQZSJNYQYQQXYJGHZGZCYJCHZLYCDSHWSHJZYJXCLLNXZJJYYXNFXMWFPYLCYLLABWDDHWDXJMCXZTZPMLQ"
			+ "ZHSFHZYNZTLLDYWLSLXHYMMYLMBWWKYXYADTXYLLDJPYBPWUXJMWMLLSAFDLLYFLBHHHBQQLTZJCQJLDJTFFKMMMBYTHYGDCQRDD"
			+ "WRQJXNBYSNWZDBYYTBJHPYBYTTJXAAHGQDQTMYSTQXKBTZPKJLZRBEQQSSMJJBDJOTGTBXPGBKTLHQXJJJCTHXQDWJLWRFWQGWSH"
			+ "CKRYSWGFTGYGBXSDWDWRFHWYTJJXXXJYZYSLPYYYPAYXHYDQKXSHXYXGSKQHYWFDDDPPLCJLQQEEWXKSYYKDYPLTJTHKJLTCYYHH"
			+ "JTTPLTZZCDLTHQKZXQYSTEEYWYYZYXXYYSTTJKLLPZMCYHQGXYHSRMBXPLLNQYDQHXSXXWGDQBSHYLLPJJJTHYJKYPPTHYYKTYEZ"
			+ "YENMDSHLCRPQFDGFXZPSFTLJXXJBSWYYSKSFLXLPPLBBBLBSFXFYZBSJSSYLPBBFFFFSSCJDSTZSXZRYYSYFFSYZYZBJTBCTSBSD"
			+ "HRTJJBYTCXYJEYLXCBNEBJDSYXYKGSJZBXBYTFZWGENYHHTHZHHXFWGCSTBGXKLSXYWMTMBYXJSTZSCDYQRCYTWXZFHMYMCXLZNS"
			+ "DJTTTXRYCFYJSBSDYERXJLJXBBDEYNJGHXGCKGSCYMBLXJMSZNSKGXFBNBPTHFJAAFXYXFPXMYPQDTZCXZZPXRSYWZDLYBBKTYQP"
			+ "QJPZYPZJZNJPZJLZZFYSBTTSLMPTZRTDXQSJEHBZYLZDHLJSQMLHTXTJECXSLZZSPKTLZKQQYFSYGYWPCPQFHQHYTQXZKRSGTTSQ"
			+ "CZLPTXCDYYZXSQZSLXLZMYCPCQBZYXHBSXLZDLTCDXTYLZJYYZPZYZLTXJSJXHLPMYTXCQRBLZSSFJZZTNJYTXMYJHLHPPLCYXQJ"
			+ "QQKZZSCPZKSWALQSBLCCZJSXGWWWYGYKTJBBZTDKHXHKGTGPBKQYSLPXPJCKBMLLXDZSTBKLGGQKQLSBKKTFXRMDKBFTPZFRTBBR"
			+ "FERQGXYJPZSSTLBZTPSZQZSJDHLJQLZBPMSMMSXLQQNHKNBLRDDNXXDHDDJCYYGYLXGZLXSYGMQQGKHBPMXYXLYTQWLWGCPBMQXC"
			+ "YZYDRJBHTDJYHQSHTMJSBYPLWHLZFFNYPMHXXHPLTBQPFBJWQDBYGPNZTPFZJGSDDTQSHZEAWZZYLLTYYBWJKXXGHLFKXDJTMSZS"
			+ "QYNZGGSWQSPHTLSSKMCLZXYSZQZXNCJDQGZDLFNYKLJCJLLZLMZZNHYDSSHTHZZLZZBBHQZWWYCRZHLYQQJBEYFXXXWHSRXWQHWP"
			+ "SLMSSKZTTYGYQQWRSLALHMJTQJSMXQBJJZJXZYZKXBYQXBJXSHZTSFJLXMXZXFGHKZSZGGYLCLSARJYHSLLLMZXELGLXYDJYTLFB"
			+ "HBPNLYZFBBHPTGJKWETZHKJJXZXXGLLJLSTGSHJJYQLQZFKCGNNDJSSZFDBCTWWSEQFHQJBSAQTGYPQLBXBMMYWXGSLZHGLZGQYF"
			+ "LZBYFZJFRYSFMBYZHQGFWZSYFYJJPHZBYYZFFWODGRLMFTWLBZGYCQXCDJYGZYYYYTYTYDWEGAZYHXJLZYYHLRMGRXXZCLHNELJJ"
			+ "TJTPWJYBJJBXJJTJTEEKHWSLJPLPSFYZPQQBDLQJJTYYQLYZKDKSQJYYQZLDQTGJQYZJSUCMRYQTHTEJMFCTYHYPKMHYZWJDQFHY"
			+ "YXWSHCTXRLJHQXHCCYYYJLTKTTYTMXGTCJTZAYYOCZLYLBSZYWJYTSJYHBYSHFJLYGJXXTMZYYLTXXYPZLXYJZYZYYPNHMYMDYYL"
			+ "BLHLSYYQQLLNJJYMSOYQBZGDLYXYLCQYXTSZEGXHZGLHWBLJHEYXTWQMAKBPQCGYSHHEGQCMWYYWLJYJHYYZLLJJYLHZYHMGSLJL"
			+ "JXCJJYCLYCJPCPZJZJMMYLCQLNQLJQJSXYJMLSZLJQLYCMMHCFMMFPQQMFYLQMCFFQMMMMHMZNFHHJGTTHHKHSLNCHHYQDXTMMQD"
			+ "CYZYXYQMYQYLTDCYYYZAZZCYMZYDLZFFFMMYCQZWZZMABTBYZTDMNZZGGDFTYPCGQYTTSSFFWFDTZQSSYSTWXJHXYTSXXYLBYQHW"
			+ "WKXHZXWZNNZZJZJJQJCCCHYYXBZXZCYZTLLCQXYNJYCYYCYNZZQYYYEWYCZDCJYCCHYJLBTZYYCQWMPWPYMLGKDLDLGKQQBGYCHJ"
			+ "XY" ;

		private StringConvertHelper(){}

		#endregion

	}//public class StringConvertHelper
}