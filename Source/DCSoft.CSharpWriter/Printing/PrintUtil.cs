/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;

namespace DCSoft.Printing
{
	/// <summary>
	/// 打印相关的例程
	/// </summary>
	internal sealed class PrintUtil
	{
		/// <summary>
		/// 将图形坐标单位转换为图元坐标单位
		/// </summary>
		/// <param name="unit">图形坐标单位</param>
		/// <returns>图元坐标单位</returns>
		public static System.Drawing.Imaging.MetafileFrameUnit ConvertUnit( System.Drawing.GraphicsUnit unit )
		{
			if( unit == System.Drawing.GraphicsUnit.Document )
				return System.Drawing.Imaging.MetafileFrameUnit.Document ;
			if( unit == System.Drawing.GraphicsUnit.Inch )
				return System.Drawing.Imaging.MetafileFrameUnit.Inch  ;
			if( unit == System.Drawing.GraphicsUnit.Millimeter )
				return System.Drawing.Imaging.MetafileFrameUnit.Millimeter ;
			if( unit == System.Drawing.GraphicsUnit.Pixel )
				return System.Drawing.Imaging.MetafileFrameUnit.Pixel ;
			if( unit == System.Drawing.GraphicsUnit.Point )
				return System.Drawing.Imaging.MetafileFrameUnit.Point ;
			return System.Drawing.Imaging.MetafileFrameUnit.Pixel ;
		}

		/// <summary>
		/// 本对象不可实例化
		/// </summary>
		private PrintUtil(){}
	}
}