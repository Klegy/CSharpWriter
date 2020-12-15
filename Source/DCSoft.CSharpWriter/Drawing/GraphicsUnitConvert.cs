/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Drawing ;

namespace DCSoft.Drawing
{
	/// <summary>
	/// 绘图单位转换
	/// </summary>
	public sealed class GraphicsUnitConvert
	{
		private static float fDpi = 96 ;
        /// <summary>
        /// 屏幕使用的DPI值
        /// </summary>
		public static float Dpi
		{
			get{ return fDpi ;}
			set{ fDpi = value;}
		}
        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="vValue">长度值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
		public static double Convert(
			double vValue ,
			System.Drawing.GraphicsUnit OldUnit ,
			System.Drawing.GraphicsUnit  NewUnit )
		{
			if( OldUnit == NewUnit )
				return vValue ;
			else
				return vValue * GetRate( NewUnit , OldUnit );
		}

        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="vValue">长度值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static float Convert(
			float vValue ,
			System.Drawing.GraphicsUnit OldUnit ,
			System.Drawing.GraphicsUnit  NewUnit )
		{
            if (OldUnit == NewUnit)
            {
                return vValue;
            }
            else
            {
                return (float)(vValue * GetRate(NewUnit, OldUnit));
            }
		}

        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="vValue">长度值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static int Convert(
			int vValue ,
			System.Drawing.GraphicsUnit OldUnit ,
			System.Drawing.GraphicsUnit  NewUnit )
		{
			if( OldUnit == NewUnit )
				return vValue ;
			else
				return ( int ) ( vValue * GetRate( NewUnit , OldUnit ) );
		}

        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="p">长度值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static System.Drawing.Point Convert(
			System.Drawing.Point p ,
			System.Drawing.GraphicsUnit OldUnit , 
			System.Drawing.GraphicsUnit  NewUnit )
		{
			if( OldUnit == NewUnit )
				return p ;
			else
			{
				double rate = GetRate( NewUnit , OldUnit );
				return new System.Drawing.Point( 
					( int ) ( p.X * rate ) ,
					( int ) ( p.Y * rate ));
			}
		}

        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="p">长度值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static System.Drawing.PointF Convert(
            System.Drawing.PointF p,
            System.Drawing.GraphicsUnit OldUnit,
            System.Drawing.GraphicsUnit NewUnit)
        {
            if (OldUnit == NewUnit)
            {
                return p;
            }
            else
            {
                double rate = GetRate(NewUnit, OldUnit);
                return new System.Drawing.PointF(
                    (float)(p.X * rate),
                    (float)(p.Y * rate));
            }
        }

        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="x">X坐标值</param>
        /// <param name="y">Y坐标值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static System.Drawing.Point Convert(
			int x, 
			int y ,
			System.Drawing.GraphicsUnit OldUnit ,
			System.Drawing.GraphicsUnit NewUnit )
		{
			if( OldUnit == NewUnit )
				return new System.Drawing.Point( x , y );
			else
			{
				double rate = GetRate( NewUnit , OldUnit );
				return new System.Drawing.Point(
					( int ) ( x * rate ) , 
					( int ) ( y * rate ));
			}
		}


        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="size">旧值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static System.Drawing.Size Convert(
			System.Drawing.Size size ,
			System.Drawing.GraphicsUnit OldUnit ,
			System.Drawing.GraphicsUnit NewUnit )
		{
			if( OldUnit == NewUnit )
				return size ;
			else
			{
				double rate = GetRate( NewUnit , OldUnit );
				return new System.Drawing.Size(
					( int ) ( size.Width * rate ) , 
					( int ) ( size.Height * rate ));
			}
		}

        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="size">旧值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static System.Drawing.SizeF Convert(
			System.Drawing.SizeF size , 
			System.Drawing.GraphicsUnit OldUnit ,
			System.Drawing.GraphicsUnit NewUnit )
		{
			if( OldUnit == NewUnit )
				return size ;
			else
			{
				double rate = GetRate( NewUnit , OldUnit );
				return new System.Drawing.SizeF(
					( float ) ( size.Width * rate ) , 
					( float ) ( size.Height * rate ));
			}
		}

        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="rect">旧值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static System.Drawing.Rectangle Convert(
			System.Drawing.Rectangle rect , 
			System.Drawing.GraphicsUnit OldUnit ,
			System.Drawing.GraphicsUnit  NewUnit )
		{
			if( OldUnit == NewUnit )
			{
				return rect ;
			}
			else
			{
				double rate = GetRate( NewUnit , OldUnit );
				return new System.Drawing.Rectangle(
					( int ) ( rect.X * rate ) ,
					( int ) ( rect.Y * rate ) ,
					( int ) ( rect.Width * rate ) ,
					( int ) ( rect.Height * rate ));
			}
		}

        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="rect">旧值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static System.Drawing.RectangleF Convert(
			System.Drawing.RectangleF rect , 
			System.Drawing.GraphicsUnit OldUnit ,
			System.Drawing.GraphicsUnit  NewUnit )
		{
			if( OldUnit == NewUnit )
			{
				return rect ;
			}
			else
			{
				double rate = GetRate( NewUnit , OldUnit );
				return new System.Drawing.RectangleF(
					( float ) ( rect.X * rate ) ,
					( float ) ( rect.Y * rate ) ,
					( float ) ( rect.Width * rate ) ,
					( float ) ( rect.Height * rate ));
			}
		}

		/// <summary>
		/// 将一个长度从旧单位换算成新单位使用的比率
		/// </summary>
		/// <param name="NewUnit">新单位</param>
		/// <param name="OldUnit">旧单位</param>
		/// <returns>比率数</returns>
		public static double GetRate(
			System.Drawing.GraphicsUnit NewUnit ,
			System.Drawing.GraphicsUnit OldUnit )
		{
			return GetUnit( OldUnit ) / GetUnit( NewUnit )  ;
		}

        /// <summary>
        /// 获得指定度量单位下的DPI值
        /// </summary>
        /// <param name="unit">指定的度量单位</param>
        /// <returns>DPI值</returns>
        public static double GetDpi(System.Drawing.GraphicsUnit unit)
		{
			switch( unit )
			{
				case System.Drawing.GraphicsUnit.Display :
					return fDpi ;
				case System.Drawing.GraphicsUnit.Document :
					return 300 ;
				case System.Drawing.GraphicsUnit.Inch :
					return 1 ;
				case System.Drawing.GraphicsUnit.Millimeter :
					return 25.4 ;
				case System.Drawing.GraphicsUnit.Pixel :
					return fDpi ;
				case System.Drawing.GraphicsUnit.Point :
					return 72 ;
			}
			return fDpi ;
		}

		/// <summary>
		/// 获得一个单位占据的英寸数
		/// </summary>
		/// <param name="unit">单位类型</param>
		/// <returns>英寸数</returns>
		public static double GetUnit( System.Drawing.GraphicsUnit unit )
		{
			switch( unit )
			{
				case System.Drawing.GraphicsUnit.Display :
					return 1 / fDpi ;
				case System.Drawing.GraphicsUnit.Document :
					return 1 / 300.0 ;
				case System.Drawing.GraphicsUnit.Inch :
					return 1 ;
				case System.Drawing.GraphicsUnit.Millimeter :
					return 1 / 25.4 ;
				case System.Drawing.GraphicsUnit.Pixel :
					return 1 / fDpi ;
				case System.Drawing.GraphicsUnit.Point :
					return 1 / 72.0 ;
				default:
					throw new System.NotSupportedException("Not support " + unit.ToString());
			}
		}

        /// <summary>
        /// 进行单位换算
        /// </summary>
        /// <param name="Value">旧值</param>
        /// <param name="OldUnit">旧单位</param>
        /// <param name="NewUnit">新单位</param>
        /// <returns>换算结果</returns>
        public static double Convert(double Value, LengthUnit OldUnit, LengthUnit NewUnit)
        {
            if (OldUnit == NewUnit)
                return Value;
            else
                return Value * GetUnit(OldUnit) / GetUnit( NewUnit );
        }

        /// <summary>
        /// 获得一个单位占据的英寸数
        /// </summary>
        /// <param name="unit">单位类型</param>
        /// <returns>英寸数</returns>
        public static double GetUnit( LengthUnit unit)
        {
            switch (unit)
            {
                case LengthUnit.Document :
                    return 1 / 300.0;
                case LengthUnit.Inch :
                    return 1;
                case LengthUnit.Millimeter :
                    return 1 / 25.4;
                case LengthUnit.Pixel :
                    return 1 / fDpi;
                case LengthUnit.Point :
                    return 1 / 72.0;
                case LengthUnit.Centimerter :
                    return 1 / 2.54;
                case LengthUnit.Twips :
                    return 1 / 1440.0;
                default:
                    throw new System.NotSupportedException("Not support " + unit.ToString());
            }
        }


        /// <summary>
		/// 将指定单位的指定长度转化为 Twips 单位
		/// </summary>
		/// <param name="Value">长度</param>
		/// <param name="unit">度量单位</param>
		/// <returns>转化的 Twips 数</returns>
		public static int ToTwips( int Value , System.Drawing.GraphicsUnit unit )
		{
			double v = GetUnit( unit );
			return ( int ) ( Value * v * 1440 );
		}

        /// <summary>
        /// 将指定单位的指定长度转化为 Twips 单位
        /// </summary>
        /// <param name="Value">长度</param>
        /// <param name="unit">度量单位</param>
        /// <returns>转化的 Twips 数</returns>
        public static int ToTwips(float Value, System.Drawing.GraphicsUnit unit)
        {
            double v = GetUnit(unit);
            return (int)(Value * v * 1440);
        }

		/// <summary>
		/// 将指定的Twips值转化为指定单位的数值
		/// </summary>
		/// <param name="Twips">Twips值</param>
		/// <param name="unit">要转化的目标单位</param>
		/// <returns>转化的长度值</returns>
		public static int FromTwips( int Twips , System.Drawing.GraphicsUnit unit )
		{
			double v = GetUnit( unit );
			return ( int ) ( Twips / ( v * 1440 ));
		}

        /// <summary>
        /// 将指定的Twips值转化为指定单位的数值
        /// </summary>
        /// <param name="Twips">Twips值</param>
        /// <param name="unit">要转化的目标单位</param>
        /// <returns>转化的长度值</returns>
        public static double FromTwips(double twips, System.Drawing.GraphicsUnit unit)
        {
            double v = GetUnit(unit);
            return twips / (v * 1440.0);
        }

        /// <summary>
        /// 将CSS样式的长度字符串转换为数值
        /// </summary>
        /// <param name="CSSLength">CSS样式的长度字符串</param>
        /// <param name="unit">要转换为单位</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns>转换后的数值</returns>
        public static double ParseCSSLength(string CSSLength , GraphicsUnit unit , double DefaultValue )
        {
            CSSLength = CSSLength.Trim();
            int len = CSSLength.Length  ;
            double num = 0 ;
            GraphicsUnit OldUnit = unit ;
            for ( int iCount = 0 ; iCount < len; iCount++)
            {
                char c = CSSLength[iCount];
                if( "-.0123456789".IndexOf( c ) < 0 )
                {
                    if( iCount > 0 )
                    {
                        if( double.TryParse( CSSLength.Substring( 0 , iCount ) , System.Globalization.NumberStyles.Any , null , out num ))
                        {
                            string strUnit = CSSLength.Substring( iCount ).Trim().ToLower();
                            switch( strUnit )
                            {
                                case "cm" :// 厘米单位
                                    return Convert( num , GraphicsUnit.Millimeter , unit ) * 10 ;
                                case "mm" :// 毫米单位
                                    return Convert( num , GraphicsUnit.Millimeter , unit );
                                case "in" :// 英寸单位
                                    return Convert( num , GraphicsUnit.Inch , unit );
                                case "pt" :// 点单位(1/72英寸)
                                    return Convert( num , GraphicsUnit.Point , unit );
                                case "pc" :// pica单位
                                    return Convert( num , GraphicsUnit.Point , unit ) * 12 ;
                                case "px":// 像素单位
                                    return Convert(num, GraphicsUnit.Pixel, unit);
                            }
                        }
                    }
                }
            }
            if (double.TryParse(CSSLength, System.Globalization.NumberStyles.Any, null, out num))
            {
                return Convert( num , GraphicsUnit.Pixel , unit );
            }
            return DefaultValue ;
        }

        /// <summary>
        /// 将长度转换为CSS中的长度字符串
        /// </summary>
        /// <param name="Value">长度</param>
        /// <param name="unit">长度单位</param>
        /// <param name="cssUnit">CSS单位</param>
        /// <returns>CSS样式的长度字符串</returns>
        public static string ToCSSLength(double Value, GraphicsUnit unit , CssLengthUnit cssUnit )
        {
            double v = 0 ;
            string strUnit = "";
            switch (cssUnit)
            {
                case CssLengthUnit.Centimeters :
                    v = Convert(Value, unit, GraphicsUnit.Millimeter) / 10;
                    strUnit = "cm";
                    break;
                case CssLengthUnit.Millimeters :
                    v = Convert(Value, unit , GraphicsUnit.Millimeter);
                    strUnit = "mm";
                    break;
                case CssLengthUnit.Inches :
                    v = Convert(Value, unit, GraphicsUnit.Inch);
                    strUnit = "in";
                    break;
                case CssLengthUnit.Picas :
                    v = Convert(Value, unit, GraphicsUnit.Point) / 12;
                    strUnit = "pc";
                    break;
                case CssLengthUnit.Pixels :
                    v = Convert(Value, unit, GraphicsUnit.Pixel);
                    strUnit = "px";
                    break;
                case CssLengthUnit.Points :
                    v = Convert(Value, unit, GraphicsUnit.Point);
                    strUnit = "pt";
                    break;
            }
            return v.ToString("0.0000") + strUnit;
        }

        /// <summary>
        /// 对象不可实例化
        /// </summary>
        private GraphicsUnitConvert()
		{
		}
	}

    /// <summary>
    /// 长度单位
    /// </summary>
    public enum LengthUnit
    {
        /// <summary>
        /// 文档单位
        /// </summary>
        Document ,
        /// <summary>
        /// 英尺单位
        /// </summary>
        Inch ,
        /// <summary>
        /// 毫米单位
        /// </summary>
        Millimeter ,
        /// <summary>
        /// 像素单位
        /// </summary>
        Pixel ,
        /// <summary>
        /// 点单位
        /// </summary>
        Point ,
        /// <summary>
        /// 厘米单位
        /// </summary>
        Centimerter ,
        /// <summary>
        /// Twips单位
        /// </summary>
        Twips
    }
    /// <summary>
    /// CSS长度单位
    /// </summary>
    public enum CssLengthUnit
    {
        /// <summary>
        /// 厘米
        /// </summary>
        Centimeters ,
        /// <summary>
        /// 毫米
        /// </summary>
        Millimeters,
        /// <summary>
        /// 英寸
        /// </summary>
        Inches,
        /// <summary>
        /// 点
        /// </summary>
        Points,
        /// <summary>
        /// Picas
        /// </summary>
        Picas ,
        /// <summary>
        /// 像素
        /// </summary>
        Pixels ,
    }
}
