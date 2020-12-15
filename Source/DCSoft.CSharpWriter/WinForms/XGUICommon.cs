/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;

namespace DCSoft.WinForms
{
	/// <summary>
	/// GUI通用例程集合
	/// </summary>
	public sealed class XGUICommon
	{
		/// <summary>
		/// 文本框收缩和展开控制框的大小,目前设为8
		/// </summary>
		public const int c_ExpendBoxSize	= 8;

		
		public static void StaticDrawExpendHandle( System.Drawing.Graphics g ,  System.Drawing.Rectangle BoxRect ,  bool bolExpended )
		{
			if( g != null)
			{
				g.FillRectangle(System.Drawing.SystemBrushes.Window  ,BoxRect);
				g.DrawRectangle(System.Drawing.SystemPens.WindowText ,BoxRect);
				g.DrawLine
					(System.Drawing.SystemPens.WindowText,
					BoxRect.X  + 2, 
					(int)(BoxRect.Y  + BoxRect.Height / 2.0)  ,
					(int) BoxRect.X  + BoxRect.Width  - 2 , 
					(int)(BoxRect.Y  + BoxRect.Height /2.0 )  );
				if( ! bolExpended )
				{
					g.DrawLine
						(System.Drawing.SystemPens.WindowText ,
						(int)(BoxRect.X  +  BoxRect.Width /2.0)  ,
						BoxRect.Y  + 2 , 
						(int)(BoxRect.X  + BoxRect.Width / 2.0)  ,
						BoxRect.Y  + BoxRect.Height  - 2);
				}
			}
		}

		
		/// <summary>
		/// 计算对象的收缩和展开点的矩形区域
		/// </summary>
		/// <remarks>
		/// 对象收缩和展开控制点在对象的左边,两者边框间有两个单位的距离,水平中轴线相重
		/// </remarks>
		/// <param name="x">对象的左端位置</param>
		/// <param name="y">对象的顶端位置</param>
		/// <param name="height">对象的高度</param>
		/// <returns>包含收缩点的矩形区域</returns>
		public static System.Drawing.Rectangle StaticGetExpendHandleRect(int x, int y , int height )
		{
			if( height <= c_ExpendBoxSize )
				height = c_ExpendBoxSize ;
			return  new System.Drawing.Rectangle
				(x - c_ExpendBoxSize - 2 ,
				y - (int)((height - c_ExpendBoxSize)/2.0) ,
				c_ExpendBoxSize ,
				c_ExpendBoxSize );
		}

		public static System.Drawing.Rectangle StaticGetExpendHandleRect2(int x , int y , int height)
		{
			return  new System.Drawing.Rectangle
				(x   ,
				y + (int)((height - c_ExpendBoxSize)/2.0) ,
				c_ExpendBoxSize ,
				c_ExpendBoxSize );
		}


		private XGUICommon()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
	}
}
