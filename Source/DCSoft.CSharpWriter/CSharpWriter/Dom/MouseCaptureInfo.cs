/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;

namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// MouseCaptureInfo 的摘要说明。
	/// </summary>
	public class MouseCaptureInfo
	{
		public MouseCaptureInfo( DocumentEventArgs args )
		{
			intButton = args.Button ;
			intStartX = args.X ;
			intStartY = args.Y ;
			intLastX = args.X ;
			intLastY = args.Y ;
		}
		private System.Windows.Forms.MouseButtons intButton = System.Windows.Forms.MouseButtons.None ;
		public System.Windows.Forms.MouseButtons Button
		{
			get{ return intButton ;}
			set{ intButton = value;}
		}

		private int intStartX = 0 ;
		public int StartX
		{
			get{ return intStartX ;}
			set{ intStartX = value;}
		}

		private int intStartY = 0 ;
		public int StartY
		{
			get{ return intStartY ;}
			set{ intStartY = value;}
		}

		private int intLastX = 0 ;
		public int LastX 
		{
			get{ return intLastX ;}
			set{ intLastX = value;}
		}

		private int intLastY = 0 ;
		public int LastY
		{
			get{ return intLastY ;}
			set{ intLastY = value;}
		}

	}
}
