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
	/// 页面设置预览界面对象
	/// </summary>
	public class PageSettingPreview
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public PageSettingPreview()
		{
		}

		private System.Drawing.Printing.PaperSize myPaperSize = null;
		/// <summary>
		/// 纸张大小
		/// </summary>
		public System.Drawing.Printing.PaperSize PaperSize
		{
			get{ return myPaperSize ;}
			set{ myPaperSize = value;}
		}
		private System.Drawing.Printing.Margins myMargins = new System.Drawing.Printing.Margins( 100 , 100 , 100 , 100 );
		/// <summary>
		/// 页边距
		/// </summary>
		public System.Drawing.Printing.Margins Margins
		{
			get{ return myMargins ;}
			set{ myMargins = value;}
		}
		private bool bolLandscape = false;
		/// <summary>
		/// 横向打印标记
		/// </summary>
		public bool Landscape
		{
			get{ return bolLandscape ;}
			set{ bolLandscape = value;}
		}
		public void SetPagetSettings( System.Drawing.Printing.PageSettings ps )
		{
			myPaperSize = ps.PaperSize ;
			myMargins = ps.Margins ;
			bolLandscape = ps.Landscape ;
		}

		private System.Drawing.Rectangle myBounds = System.Drawing.Rectangle.Empty ;
		/// <summary>
		/// 对象边界矩形
		/// </summary>
		public System.Drawing.Rectangle Bounds
		{
			get{ return myBounds ;}
			set{ myBounds = value;}
		}
		
		/// <summary>
		/// 绘制页面设置预览区域
		/// </summary>
		/// <param name="sender">事件发送者</param>
		/// <param name="e">绘图事件参数对象</param>
		public void OnPaint(object sender, System.Windows.Forms.PaintEventArgs e )
		{
			int PageWidth = ( bolLandscape == false ? myPaperSize.Width : myPaperSize.Height );
			int PageHeight= ( bolLandscape == false ? myPaperSize.Height : myPaperSize.Width  );
			double Rate = 1 ;
			if( ( PageWidth / (double)myBounds.Width ) > ( PageHeight / (double)myBounds.Height ))
				Rate =  ( 1.1 * PageWidth / ( double ) myBounds.Width ) ;
			else
				Rate =  ( 1.1 * PageHeight /( double ) myBounds.Height );
			System.Drawing.Rectangle rect = new System.Drawing.Rectangle( 0 , 0 , ( int ) ( PageWidth / Rate ) , ( int) ( PageHeight / Rate ));
			rect.X = myBounds.Left + ( myBounds.Width - rect.Width ) /2 ;
			rect.Y = myBounds.Top + ( myBounds.Height - rect.Height ) / 2 ;
			if( rect.IntersectsWith( e.ClipRectangle ) )
			{
				e.Graphics.FillRectangle( System.Drawing.Brushes.White , rect );
				e.Graphics.DrawRectangle( System.Drawing.Pens.Black , rect );
				System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle( 
					(int)(rect.Left + myMargins.Left / Rate ),
					(int) ( rect.Top + myMargins.Top / Rate ),
					(int) ( rect.Width - ( myMargins.Left + myMargins.Right ) / Rate ),
					(int) ( rect.Height - ( myMargins.Top + myMargins.Bottom ) / Rate ));
				e.Graphics.DrawRectangle( System.Drawing.Pens.Red , rect2 );
			}
		}
	}//public class PageSettingPreview : VirtualControlBase
}