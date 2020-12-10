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
	/// 数值,类型转换相关帮助类
	/// </summary>
    /// <remarks>编制 袁永福</remarks>
	public sealed class ValueTypeHelper
	{
		
		static ValueTypeHelper()
		{
			
		}

		public static double ObjectToDouble( object v , double DefaultValue )
		{
			if( v == null )
				return DefaultValue ;
			if( v is byte
				|| v is short
				|| v is int
				|| v is long
				|| v is decimal
				|| v is double )
			{
				return ( double ) v ;
			}
			return StringToDouble( Convert.ToString( v ) , DefaultValue );
		}

		public static double StringToDouble( string v , double DefaultValue )
		{
			if( v == null )
				return DefaultValue ;
			v = v.Trim();
			if( v.Length == 0 )
				return DefaultValue ;
			double result = 0 ;
			if( double.TryParse( v , System.Globalization.NumberStyles.Any , null , out result ) )
			{
				return result ;
			}
			return DefaultValue ;
		}

        private static System.Collections.Hashtable myDefaultValue = null;
        /// <summary>
        /// 获得指定类型的默认值
        /// </summary>
        /// <param name="ValueType">数据类型</param>
        /// <returns>默认值</returns>
        public static object GetDefaultValue(Type ValueType)
		{
            if (myDefaultValue == null)
            {
                myDefaultValue = new System.Collections.Hashtable();
                myDefaultValue[typeof(object)] = null;
                myDefaultValue[typeof(byte)] = (byte)0;
                myDefaultValue[typeof(sbyte)] = (sbyte)0;
                myDefaultValue[typeof(short)] = (short)0;
                myDefaultValue[typeof(ushort)] = (ushort)0;
                myDefaultValue[typeof(int)] = (int)0;
                myDefaultValue[typeof(uint)] = (uint)0;
                myDefaultValue[typeof(long)] = (long)0;
                myDefaultValue[typeof(ulong)] = (ulong)0;
                myDefaultValue[typeof(char)] = (char)0;
                myDefaultValue[typeof(float)] = (float)0;
                myDefaultValue[typeof(double)] = (double)0;
                myDefaultValue[typeof(decimal)] = (decimal)0;
                myDefaultValue[typeof(bool)] = false;
                myDefaultValue[typeof(string)] = null;
                myDefaultValue[typeof(DateTime)] = DateTime.MinValue;
                myDefaultValue[typeof(System.Drawing.Point)] = System.Drawing.Point.Empty;
                myDefaultValue[typeof(System.Drawing.PointF)] = System.Drawing.PointF.Empty;
                myDefaultValue[typeof(System.Drawing.Size)] = System.Drawing.Size.Empty;
                myDefaultValue[typeof(System.Drawing.SizeF)] = System.Drawing.SizeF.Empty;
                myDefaultValue[typeof(System.Drawing.Rectangle)] = System.Drawing.Rectangle.Empty;
                myDefaultValue[typeof(System.Drawing.RectangleF)] = System.Drawing.RectangleF.Empty;
                myDefaultValue[typeof(System.Drawing.Color)] = System.Drawing.Color.Transparent;
            }
			if( ValueType == null )
			{
				throw new ArgumentNullException("ValueType");
			}
			if( myDefaultValue.ContainsKey( ValueType ))
			{
				return myDefaultValue[ ValueType ] ;
			}
			if( ValueType.IsValueType )
			{
				return System.Activator.CreateInstance( ValueType );
			}
			return null;
		}

        public static bool TryConvertTo(object Value, Type NewType, ref object Result)
        {
            if (NewType == null)
            {
                throw new ArgumentNullException("NewType");
            }
            if (Value == null || DBNull.Value.Equals(Value))
            {
                if (NewType.IsClass)
                {
                    Result = null;
                    return true;
                }
                return false;
            }

            Type ValueType = Value.GetType();

            if (ValueType.Equals(NewType) || ValueType.IsSubclassOf(NewType))
            {
                Result = Value;
                return true;
            }

            try
            {
                bool IsStringValue = ValueType.Equals(typeof(string));
                if (NewType.Equals(typeof(string)))
                {
                    if (IsStringValue)
                        Result = (string)Value;
                    else
                        Result = Convert.ToString(Value);
                    return true;
                } 
                if (NewType.Equals(typeof(bool)))
                {
                    if( IsStringValue )
                    {
                        bool bol = false;
                        if ( TryParseBoolean((string)Value, out bol))
                        {
                            Result = bol;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    Result = Convert.ToBoolean(Value);
                    return true;
                }
                if (NewType.Equals(typeof(char)))
                {
                    if (IsStringValue)
                    {
                        char c = char.MinValue;
                        if ( TryParseChar((string)Value, out c))
                        {
                            Result = c;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    Result = Convert.ToChar(Value);
                    return true;
                }
                if (NewType.Equals(typeof(byte)))
                {
                    if (IsStringValue)
                    {
                        byte b = 0;
                        if (TryParseByte((string)Value, out b))
                        {
                            Result = b;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    Result = Convert.ToByte(Value);
                    return true;
                }
                if (NewType.Equals(typeof(sbyte)))
                {
                    if (IsStringValue)
                    {
                        sbyte sb = 0;
                        if ( TryParseSByte((string)Value, out sb))
                        {
                            Result = sb;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    Result = Convert.ToSByte(Value);
                    return true;
                }
                if (NewType.Equals(typeof(short)))
                {
                    if (IsStringValue)
                    {
                        short si = 0;
                        if (TryParseInt16((string)Value, out si))
                        {
                            Result = si;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    Result = Convert.ToInt16(Value);
                    return true;
                }
                if (NewType.Equals(typeof(ushort)))
                {
                    if (IsStringValue)
                    {
                        ushort us = 0;
                        if ( TryParseUInt16((string)Value, out us))
                        {
                            Result = us;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    Result = Convert.ToUInt16(Value);
                    return true;
                }
                if (NewType.Equals(typeof(int)))
                {
                    if (IsStringValue)
                    {
                        int i = 0;
                        if (TryParseInt32((string)Value, out i))
                        {
                            Result = i;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    Result = Convert.ToInt32(Value);
                    return true;
                }
                if (NewType.Equals(typeof(uint)))
                {
                    if (IsStringValue)
                    {
                        uint ui = 0;
                        if ( TryParseUInt32((string)Value, out ui))
                        {
                            Result = ui;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    Result = Convert.ToUInt32(Value);
                    return true;
                }
                if (NewType.Equals(typeof(long)))
                {
                    if (IsStringValue)
                    {
                        long lng = 0;
                        if( TryParseInt64( ( string ) Value , out lng ))
                        {
                            Result = lng ;
                            return true ;
                        }
                        else
                        {
                            return false ;
                        }
                    }
                    Result = Convert.ToInt64(Value);
                    return true ;
                }
                if (NewType.Equals(typeof(ulong)))
                {
                    if (IsStringValue)
                    {
                        ulong ulng = 0;
                        if ( TryParseUInt64((string)Value, out ulng))
                        {
                            Result = ulng;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    Result = Convert.ToUInt64(Value);
                    return true;
                }
                if (NewType.Equals(typeof(float)))
                {
                    if (IsStringValue)
                    {
                        float f = 0;
                        if (TryParseSingle((string)Value, out f))
                        {
                            Result = f;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    Result = Convert.ToSingle(Value);
                    return true;
                }
                if (NewType.Equals(typeof(double)))
                {
                    if (IsStringValue)
                    {
                        double v = 0;
                        if ( TryParseDouble((string)Value, out v))
                        {
                            Result = v;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    Result = Convert.ToDouble(Value);
                    return true;
                }
                if (NewType.Equals(typeof(decimal)))
                {
                    if (IsStringValue)
                    {
                        decimal v = 0;
                        if (TryParseDecimal((string)Value, out v))
                        {
                            Result = v;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    Result = Convert.ToDecimal(Convert.ToSingle(Value));// .ToDecimal( v );
                    return true;
                }
                if (NewType.Equals(typeof(DateTime)))
                {
                    if (IsStringValue)
                    {
                        DateTime v = DateTime.MinValue;
                        if (TryParseDateTime((string)Value, out v))
                        {
                            Result = v;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    Result = Convert.ToDateTime(Value);
                    return true;
                }
                if (NewType.Equals(typeof(TimeSpan)))
                {
                    if (IsStringValue)
                    {
                        TimeSpan v = TimeSpan.Zero;
                        if (TryParseTimeSpan((string)Value, out v))
                        {
                            Result = v;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    Result = new TimeSpan(Convert.ToInt64(Value));
                    return true;
                }
                if( NewType.Equals( typeof( byte[])))
                {
                    if( IsStringValue)
                    {
                        byte[] bs = null ;
                        try
                        {
                            bs = Convert.FromBase64String((string)Value);
                            Result = bs;
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                    return false ;
                }
                if (NewType.IsEnum)
                {
                    if (Enum.IsDefined(ValueType, Value))
                    {
                        if (IsStringValue)
                        {
                            Result = Enum.Parse(NewType, (string)Value, true);
                        }
                        else
                        {
                            Result = Enum.ToObject(NewType, Value);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(NewType);
                if (converter != null)
                {
                    if (converter.CanConvertFrom(Value.GetType()))
                    {
                        Result = converter.ConvertFrom(Value);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (Value is System.IConvertible)
                {
                    Result = ((System.IConvertible)Value).ToType(NewType, null);
                    return true;
                }

                Result = Convert.ChangeType(Value, NewType);
                return true;
            }
            catch
            {
                return false;
            }
        }

		public static object ConvertTo( object Value , Type NewType , object DefaultValue )
		{
			if( NewType == null )
			{
				throw new ArgumentNullException("NewType");
			}
			if( Value == null || DBNull.Value.Equals( Value ))
			{
				return DefaultValue ;
			}
            
            Type ValueType = Value.GetType();

			if( ValueType.Equals( NewType ) || ValueType.IsSubclassOf( NewType ))
			{
				return Value ;
			}

            if (NewType.Equals(typeof(string)))
            {
                return Convert.ToString(Value);
            }
            if (NewType.Equals(typeof(bool)))
            {
                if (Value is String)
                {
                    return bool.Parse((string)Value);
                }
                else
                {
                    return Convert.ToBoolean(Value);
                }
            }
            try
            {
                if (NewType.Equals(typeof(char)))
                {
                    return Convert.ToChar(Value);
                }
                if (NewType.Equals(typeof(byte)))
                {
                    return Convert.ToByte(Value);
                }
                if (NewType.Equals(typeof(sbyte)))
                {
                    return Convert.ToSByte(Value);
                }
                if (NewType.Equals(typeof(short)))
                {
                    return Convert.ToInt16(Value);
                }
                if (NewType.Equals(typeof(ushort)))
                {
                    return Convert.ToUInt16(Value);
                }
                if (NewType.Equals(typeof(int)))
                {
                    return Convert.ToInt32(Value);
                }
                if (NewType.Equals(typeof(uint)))
                {
                    return Convert.ToUInt32(Value);
                }
                if (NewType.Equals(typeof(long)))
                {
                    return Convert.ToInt64(Value);
                }
                if (NewType.Equals(typeof(ulong)))
                {
                    return Convert.ToUInt64(Value);
                }
                if (NewType.Equals(typeof(float)))
                {
                    return Convert.ToSingle(Value);
                }
                if (NewType.Equals(typeof(double)))
                {
                    return Convert.ToDouble(Value);
                }
                if (NewType.Equals(typeof(decimal)))
                {
                    decimal dec = Convert.ToDecimal(Convert.ToSingle(Value));// .ToDecimal( v );
                    return dec;
                }
                if( NewType.Equals( typeof( DateTime )))
                {
                    DateTime dtm = DateTime.MinValue ;
                    if (ValueType.Equals(typeof(string)))
                        dtm = DateTime.Parse((string)Value);
                    else
                        dtm = Convert.ToDateTime(Value);
                    return dtm;
                }
                if (NewType.Equals(typeof(TimeSpan)))
                {
                    TimeSpan span = TimeSpan.Zero;
                    if (ValueType.Equals(typeof(string)))
                        span = TimeSpan.Parse((string)Value);
                    else
                        span = TimeSpan.Parse(Convert.ToString(Value));
                    return span;
                }
                if (NewType.Equals(typeof(byte[])))
                {
                    if (ValueType.Equals(typeof(string)))
                    {
                        byte[] bs = Convert.FromBase64String((string)Value);
                        return bs;
                    }
                    return null;
                }

                if (NewType.IsEnum)
                {
                    if (Value is string)
                        return System.Enum.Parse(NewType, (string)Value);
                    else
                        return System.Enum.ToObject(NewType, Value);
                }

                System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(NewType);
                if (converter != null)
                {
                    if (converter.CanConvertFrom(Value.GetType()))
                    {
                        return converter.ConvertFrom(Value);
                    }
                    return DefaultValue;
                }
                if (Value is System.IConvertible)
                {
                    return ((System.IConvertible)Value).ToType(NewType, null);
                }

                return Convert.ChangeType(Value, NewType);
            }
            catch
            {
                return DefaultValue;
            }
		}

        public static object ConvertTo(object Value, Type NewType)
        {
            if (NewType == null)
            {
                throw new ArgumentNullException("NewType");
            }
            // 判断是否是空白字符串
            bool emptyString = false;
            if (Value is string)
            {
                string s = (string)Value;
                if (s == null || s.Trim().Length == 0)
                    emptyString = true;
            }

            if (Value == null || DBNull.Value.Equals(Value))
            {
                if (NewType.IsClass)
                {
                    return null;
                }
                else
                {
                    throw new ArgumentNullException("Value");
                }
            }

            Type ValueType = Value.GetType();

            if (ValueType.Equals(NewType) || ValueType.IsSubclassOf(NewType))
            {
                return Value;
            }

            if (NewType.Equals(typeof(string)))
            {
                return Convert.ToString(Value);
            }
            if (NewType.Equals(typeof(bool)))
            {
                if (Value is String)
                {
                    return bool.Parse((string)Value);
                }
                else
                {
                    if (emptyString)
                        return false;
                    else
                        return Convert.ToBoolean(Value);
                }
            }
            if (NewType.Equals(typeof(char)))
            {
                return Convert.ToChar(Value);
            }
            if (NewType.Equals(typeof(byte)))
            {
                if (emptyString)
                    return (byte)0;
                else
                    return Convert.ToByte(Value);
            }
            if (NewType.Equals(typeof(sbyte)))
            {
                if (emptyString)
                    return (sbyte)0;
                else
                    return Convert.ToSByte(Value);
            }
            if (NewType.Equals(typeof(short)))
            {
                if (emptyString)
                    return (short)0;
                else
                    return Convert.ToInt16(Value);
            }
            if (NewType.Equals(typeof(ushort)))
            {
                if (emptyString)
                    return (ushort)0;
                else 
                    return Convert.ToUInt16(Value);
            }
            if (NewType.Equals(typeof(int)))
            {
                if (emptyString)
                    return (int)0;
                else
                    return Convert.ToInt32(Value);
            }
            if (NewType.Equals(typeof(uint)))
            {
                if (emptyString)
                    return (uint)0;
                else
                    return Convert.ToUInt32(Value);
            }
            if (NewType.Equals(typeof(long)))
            {
                if (emptyString)
                    return (long)0;
                else
                    return Convert.ToInt64(Value);
            }
            if (NewType.Equals(typeof(ulong)))
            {
                if (emptyString)
                    return (ulong)0;
                else
                    return Convert.ToUInt64(Value);
            }
            if (NewType.Equals(typeof(float)))
            {
                if (emptyString)
                    return (float)0;
                else
                    return Convert.ToSingle(Value);
            }
            if (NewType.Equals(typeof(double)))
            {
                if (emptyString)
                    return (double)0;
                else
                    return Convert.ToDouble(Value);
            }
            if (NewType.Equals(typeof(decimal)))
            {
                if (emptyString)
                {
                    return decimal.Zero;
                }
                else
                {
                    decimal dec = Convert.ToDecimal(Convert.ToSingle(Value));// .ToDecimal( v );
                    return dec;
                }
            }
            if (NewType.Equals(typeof(DateTime)))
            {
                if (emptyString)
                {
                    return DateTime.MinValue;
                }
                else
                {
                    DateTime dtm = DateTime.MinValue;
                    if (ValueType.Equals(typeof(string)))
                        dtm = DateTime.Parse((string)Value);
                    else
                        dtm = Convert.ToDateTime(Value);
                    return dtm;
                }
            }
            if (NewType.Equals(typeof(TimeSpan)))
            {
                if (emptyString)
                {
                    return TimeSpan.Zero;
                }
                else
                {
                    TimeSpan span = TimeSpan.Zero;
                    if (ValueType.Equals(typeof(string)))
                        span = TimeSpan.Parse((string)Value);
                    else
                        span = TimeSpan.Parse(Convert.ToString(Value));
                    return span;
                }
            }
            if (NewType.IsEnum)
            {
                if (Value is string)
                    return System.Enum.Parse(NewType, (string)Value);
                else
                    return System.Enum.ToObject(NewType, Value);
            }

            System.ComponentModel.TypeConverter converter = 
                System.ComponentModel.TypeDescriptor.GetConverter(NewType);
            if (converter != null)
            {
                if (converter.CanConvertFrom(Value.GetType()))
                {
                    return converter.ConvertFrom(Value);
                }
                else
                {
                    throw new ArgumentException("Value");
                }
            }
            if (Value is System.IConvertible)
            {
                return ((System.IConvertible)Value).ToType(NewType, null);
            }
            return Convert.ChangeType(Value, NewType);
        }

        /// <summary>
        /// 将字符串值转换为枚举值，若转换失败则返回默认值
        /// </summary>
        /// <param name="Value">字符串值</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns>转换的枚举值</returns>
        public static Enum ParseEnum(string Value, Enum DefaultValue)
        {
            string[] names = Enum.GetNames(DefaultValue.GetType());
            foreach (string name in names)
            {
                if (string.Compare(name, Value, true) == 0)
                {
                    return (Enum)Enum.Parse(DefaultValue.GetType(), Value);
                }
            }
            return DefaultValue;
        }

        ///// <summary>
        ///// 试图将字符串解释成枚举类型的值
        ///// </summary>
        ///// <param name="Value">字符串值</param>
        ///// <param name="Result">获得的枚举值</param>
        ///// <returns>操作是否成功</returns>
        //public static bool TryParseEnum(string Value, out Enum Result)
        //{
        //    string[] names = Enum.GetNames(Result.GetType());
        //    foreach (string name in names)
        //    {
        //        if (string.Compare(name, Value, true) == 0)
        //        {
        //            Result = ( Enum ) Enum.Parse( Result.GetType() , Value );
        //            return true;
        //        }
        //    }
        //    return false;
        //}

		/// <summary>
		/// 试图将字符串值解释成时间长度值
		/// </summary>
		/// <param name="Value">字符串值</param>
		/// <param name="Result">获得时间长度值</param>
		/// <returns>操作是否成功</returns>
		public static bool TryParseTimeSpan( string Value , out TimeSpan Result )
		{

			return TimeSpan.TryParse( Value , out Result );

		}

		/// <summary>
		/// 试图将字符串值解释成日期数值
		/// </summary>
		/// <param name="Value">字符串值</param>
		/// <param name="Result">获得的日期数值</param>
		/// <returns>操作是否成功</returns>
		public static bool TryParseDateTime( string Value , out DateTime Result )
		{
            Result = DateTime.MinValue;

            if (Value == null || Value.Trim().Length == 0)
                return false;
            Value = Value.Trim();

			return DateTime.TryParse( Value , out Result );

		}

        /// <summary>
        /// 试图将字符串值解释成日期数值,其中支持全数字的字符串，例如20100302123543
        /// </summary>
        /// <param name="Value">字符串值</param>
        /// <param name="Result">获得的日期数值</param>
        /// <returns>操作是否成功</returns>
        public static bool TryParseDateTimeExt(string Value, out DateTime Result)
        {
            Result = DateTime.MinValue;
            if (Value == null || Value.Trim().Length == 0)
            {
                return false;
            }
            Value = Value.Trim();
            // 判断原始数据是否是纯数字
            bool isNumeric = true;
            foreach (char c in Value)
            {
                if ("0123456789".IndexOf(c) < 0)
                {
                    isNumeric = false;
                    break;
                }
            }
            if (isNumeric)
            {
                if (Value.Length < 4)
                    return false;
                int year = 1;
                int month = 1;
                int day = 1;
                int hour = 0;
                int minutes = 0;
                int secend = 0;
                // 年数
                year = Convert.ToInt32(Value.Substring(0, 4));
                if (Value.Length >= 6)
                {
                    // 月份数
                    month = Convert.ToInt32(Value.Substring(4, 2));
                    // 检查月份数是否合法
                    if (month <= 0 || month > 12)
                    {
                        return false;
                    }
                }
                if (Value.Length >= 8)
                {
                    // 天数
                    day = Convert.ToInt32(Value.Substring(6, 2));
                    // 检查天数是否合法
                    if (day <= 0 || day > DateTime.DaysInMonth(year, month))
                    {
                        return false;
                    }
                }
                if (Value.Length >= 10)
                {
                    // 小时数
                    hour = Convert.ToInt32(Value.Substring(8, 2));
                    // 检查小时数是否合法
                    if (hour > 24)
                        return false;
                }
                if (Value.Length >= 12)
                {
                    // 分钟
                    minutes = Convert.ToInt32(Value.Substring(10, 2));
                    // 检查分钟数是否合法
                    if (minutes > 60)
                        return false;
                }
                if (Value.Length >= 14)
                {
                    // 秒
                    secend = Convert.ToInt32(Value.Substring(12, 2));
                    // 检查秒数是否合法
                    if (secend > 60)
                        return false;
                }
                Result = new DateTime(
                    year, 
                    month, 
                    day, 
                    hour, 
                    minutes, 
                    secend);
                return true ;
            }
            return TryParseDateTime(Value, out Result);
        }

		/// <summary>
		/// 试图将字符串值解释成双精度浮点数
		/// </summary>
		/// <param name="Value">字符串值</param>
		/// <param name="Result">获得的双精度浮点数</param>
		/// <returns>操作是否成功</returns>
		public static bool TryParseDecimal( string Value , out decimal Result )
		{

			return Decimal.TryParse( Value , out Result );
		}

		
		/// <summary>
		/// 将字符串值解释成双精度浮点数
		/// </summary>
		/// <param name="Value">字符串值</param>
		/// <param name="Result">获得的双精度浮点数</param>
		/// <returns>操作是否成功</returns>
		public static bool TryParseDouble( string Value , out double Result )
		{
 
					return Double.TryParse( Value , out Result );
		}

		/// <summary>
		/// 试图将字符串解释成单精度浮点数
		/// </summary>
		/// <param name="Value">字符串值</param>
		/// <param name="Result">获得的单精度浮点数</param>
		/// <returns>操作是否成功</returns>
		public static bool TryParseSingle( string Value , out float Result )
		{
 
			return Single.TryParse( Value , out Result );
		}
 

		/// <summary>
		/// 试图将字符串解释成字符值
		/// </summary>
		/// <param name="Value">字符串值</param>
		/// <param name="Result">获得的字符值</param>
		/// <returns>操作是否成功</returns>
		public static bool TryParseChar( string Value , out char Result )
		{
			Result = char.MinValue  ;
			if( Value != null && Value.Length == 1 )
			{
				Result = Value[ 0 ] ;
				return true ;
			}
			return false ;
		}
 

		/// <summary>
		/// 试图将字符串解释成64位无符号整数值
		/// </summary>
		/// <param name="Value">字符串值</param>
		/// <param name="Result">获得的64位无符号整数值</param>
		/// <returns>操作是否成功</returns>
		public static bool TryParseUInt64( string Value , out ulong Result )
		{
 
			return UInt64.TryParse( Value , out Result );

		}

		/// <summary>
		/// 试图将字符串解释成64位有符号整数值
		/// </summary>
		/// <param name="Value">字符串值</param>
		/// <param name="Result">获得的64位有符号整数值</param>
		/// <returns>操作是否成功</returns>
		public static bool TryParseInt64( string Value , out long Result )
		{

			return Int64.TryParse( Value , out Result );

		}

		/// <summary>
		/// 试图将字符串解释成32位无符号整数值
		/// </summary>
		/// <param name="Value">字符串值</param>
		/// <param name="Result">获得的32位无符号整数值</param>
		/// <returns>操作是否成功</returns>
		public static bool TryParseUInt32( string Value , out uint Result )
		{
 
			return UInt32.TryParse( Value , out Result );
 
		}

		/// <summary>
		/// 试图将字符串解释成32位有符号整数值
		/// </summary>
		/// <param name="Value">字符串值</param>
		/// <param name="Result">获得的32位有符号整数值</param>
		/// <returns>操作是否成功</returns>
		public static bool TryParseInt32( string Value , out int Result )
		{
 
			return Int32.TryParse( Value , out Result );
 
		}

		/// <summary>
		/// 试图将字符串解释成16位无符号整数值
		/// </summary>
		/// <param name="Value">字符串值</param>
		/// <param name="Result">获得的16位无符号整数值</param>
		/// <returns>操作是否成功</returns>
		public static bool TryParseUInt16( string Value , out ushort Result )
		{
 
			return UInt16.TryParse( Value , out Result );
 
		}

		/// <summary>
		/// 试图将字符串解释成16位有符号整数值
		/// </summary>
		/// <param name="Value">字符串值</param>
		/// <param name="Result">获得的16位有符号整数值</param>
		/// <returns>操作是否成功</returns>
		public static bool TryParseInt16( string Value , out short Result )
		{
 
			return Int16.TryParse( Value , out Result );
 
		}

		/// <summary>
		/// 试图将字符串解释成单字节无符号整数值
		/// </summary>
		/// <param name="Value">字符串值</param>
		/// <param name="Result">获得的单字节无符号整数值</param>
		/// <returns>操作是否成功</returns>
		public static bool TryParseByte( string Value , out byte Result )
		{
 
			return Byte.TryParse( Value , out Result );
 
		}

		/// <summary>
		/// 试图将字符串解释成单字节有符号整数值
		/// </summary>
		/// <param name="Value">字符串值</param>
		/// <param name="Result">获得的单字节有符号整数值</param>
		/// <returns>操作是否成功</returns>
		public static bool TryParseSByte( string Value , out sbyte Result )
		{
 
			return SByte.TryParse( Value , out Result );
 
		}


		private static bool CheckChars( string Value , string chars )
		{
			if( Value != null && Value.Length > 0 )
			{
				foreach( char c in Value )
				{
					if( chars.IndexOf( c ) < 0 )
						return false ;
				}
			}
			return true ;
		}
		/// <summary>
		/// 试图将字符串解释成布尔类型值
		/// </summary>
		/// <param name="Value">字符串值</param>
		/// <param name="Result">获得布尔类型值</param>
		/// <returns>操作是否成功</returns>
		public static bool TryParseBoolean(string Value, out bool Result)
		{
			Result = false;
			if ( Value != null)
			{
				Value = Value.Trim();
                if (Value == "0")
                {
                    Result = false;
                    return true;
                }
                if (Value == "1")
                {
                    Result = true;
                    return true;
                }
				if ( String.Compare( "True" , Value , true ) == 0 )
				{
					Result = true;
					return true;
				}
				if ( String.Compare( "False" , Value , true ) == 0 )
				{
					Result = false;
					return true;
				}
			}
			return false;
		}

		private ValueTypeHelper(){}
	}
}