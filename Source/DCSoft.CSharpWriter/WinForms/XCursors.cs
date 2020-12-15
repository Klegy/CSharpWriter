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
	/// 扩展鼠标光标对象,本对象提供了一些非标准的鼠标光标对象
	/// </summary>
	public sealed class XCursors
	{
        /// <summary>
        /// 格式刷光标对象
        /// </summary>
        public static System.Windows.Forms.Cursor FormatBrush
        {
            get
            {
                return GetCursor("FormatBrush.cur");
            }
        }

        /// <summary>
        /// 指向右方的光标对象
        /// </summary>
        public static System.Windows.Forms.Cursor Right
        {
            get
            {
                return GetCursor("Right.cur");
            }
        }

		/// <summary>
		/// 手型拖拽松开光标对象
		/// </summary>
		public static System.Windows.Forms.Cursor HandDragUp
		{
			get{ return GetCursor( "HandDragUp.cur"); }
		}
		/// <summary>
		/// 手型拖拽按下光标对象
		/// </summary>
		public static System.Windows.Forms.Cursor HandDragDown
		{
			get{ return GetCursor( "HandDragDown.cur" );}
		}
		/// <summary>
		/// 黑色的鼠标光标对象
		/// </summary>
		public static System.Windows.Forms.Cursor Black
		{
			get{ return GetCursor( "Black.cur" );}
		}
		/// <summary>
		/// 向右的鼠标光标对象
		/// </summary>
		public static System.Windows.Forms.Cursor RightArrow
		{
			get{ return GetCursor( "RightArrow.cur" );}
		}
		/// <summary>
		/// 缩小鼠标光标对象
		/// </summary>
		public static System.Windows.Forms.Cursor ZoomIn
		{
			get{ return GetCursor( "ZoomIn.cur");}
		}
		/// <summary>
		/// 放大鼠标光标对象
		/// </summary>
		public static System.Windows.Forms.Cursor ZoomOut
		{
			get{ return GetCursor( "ZoomOut.cur" );}
		}
		/// <summary>
		/// 获得复制拖拽鼠标光标对象
		/// </summary>
		public static System.Windows.Forms.Cursor DragCopy
		{
			get{ return GetCursor( "DragCopy.cur");}
		}
		/// <summary>
		/// 获得拖拽连接鼠标光标对象
		/// </summary>
		public static System.Windows.Forms.Cursor DragLink
		{
			get{ return GetCursor( "DragLink.cur" );}
		}
		/// <summary>
		/// 获得拖拽移动鼠标光标对象
		/// </summary>
		public static System.Windows.Forms.Cursor DragMove
		{
			get{ return GetCursor( "DragMove.cur" );}
		}
		/// <summary>
		/// 获得禁止拖拽鼠标光标对象
		/// </summary>
		public static System.Windows.Forms.Cursor DragNo
		{
			get{ return GetCursor( "DragNo.cur" );}
		}
		/// <summary>
		/// 获得拖拽页面鼠标光标对象
		/// </summary>
		public static System.Windows.Forms.Cursor DragPage
		{
			get{ return GetCursor( "DragPage.cur" );}
		}
		/// <summary>
		/// 获得禁止拖拽页面鼠标光标对象
		/// </summary>
		public static System.Windows.Forms.Cursor DragPageNo
		{
			get{ return GetCursor( "DragPageNo.cur");}
		}

		#region 内部成员 ******************************************************
		
		/// <summary>
		/// 获得指定名称的光标对象
		/// </summary>
		/// <param name="strName">光标名称</param>
		/// <returns>获得的光标对象</returns>
		private static System.Windows.Forms.Cursor GetCursor( string strName )
		{
			System.Windows.Forms.Cursor cur = myCursors[ strName ] as System.Windows.Forms.Cursor ;
			if( cur == null )
			{
				System.Reflection.Assembly asm = typeof( XCursors ).Assembly ;
				string[] names = asm.GetManifestResourceNames();
				foreach( string name in names )
				{
					if( name == strName || name.EndsWith( strName ))
					{
						using( System.IO.Stream stream = asm.GetManifestResourceStream( name ))
						{
							cur = new System.Windows.Forms.Cursor( stream );
							myCursors[ strName ] = cur ;
							break;
						}
					}
				}
			}
			return cur ;
		}
		/// <summary>
		/// 内部缓存光标对象的列表
		/// </summary>
		private static System.Collections.Hashtable myCursors = new System.Collections.Hashtable();
		/// <summary>
		/// 对象不能实例化
		/// </summary>
		private XCursors()
		{
		}

		#endregion
		
	}//public sealed class XCursors
}