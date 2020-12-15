/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;

namespace DCSoft.Drawing
{
	/// <summary>
	/// 图形样式
	/// </summary>
	/// <remarks>
	/// <img src="images/ShapeTypes.png" />
	/// </remarks>
    //[DCSoft.Common.DocumentComment()]
    [System.ComponentModel.Editor(
        "DCSoft.WinForms.Design.ShapeTypesEditor",
        typeof( System.Drawing.Design.UITypeEditor ))]
	public enum ShapeTypes
	{
		/// <summary>
		/// 矩形
		/// </summary>
		Rectangle ,
		/// <summary>
		/// 正方形
		/// </summary>
		Square,
		/// <summary>
		/// 椭圆
		/// </summary>
		Ellipse ,
		/// <summary>
		/// 正圆
		/// </summary>
		Circle,
		/// <summary>
		/// 菱形
		/// </summary>
		Diamond,
        ///// <summary>
        ///// 圆角矩形
        ///// </summary>
        //RoundedRectangle ,
        ///// <summary>
        ///// 圆角正方形
        ///// </summary>
        //RoundedSquare ,
		/// <summary>
		/// 向上的三角形
		/// </summary>
		TriangleUp,
		/// <summary>
		/// 向右的三角形
		/// </summary>
		TriangleRight,
		/// <summary>
		/// 向下的三角形
		/// </summary>
		TriangleDown,
		/// <summary>
		/// 向左的三角形
		/// </summary>
		TriangleLeft,
		/// <summary>
		/// 十字形
		/// </summary>
		Cross ,
		/// <summary>
		/// X交叉形
		/// </summary>
		XCross,
		/// <summary>
		/// 圆加上十字形
		/// </summary>
		CircleCross ,
		/// <summary>
		/// 圆加上X交叉形
		/// </summary>
		CircleXCross ,
		/// <summary>
		/// 默认状态
		/// </summary>
		Default,
		/// <summary>
		/// 不显示任何图形
		/// </summary>
		None
	}//public enum ShapeType
}