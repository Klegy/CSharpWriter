/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Runtime.InteropServices ;
namespace DCSoft.WinForms.Native
{
	/// <summary>
	/// GDI对象基础类
	/// </summary>
    /// <remarks>编写 袁永福</remarks>
    [Serializable()]
	public abstract class GDIObject : System.IDisposable
	{
		/// <summary>
		/// 对象句柄
		/// </summary>
        [NonSerialized()]
		protected System.IntPtr intHandle = IntPtr.Zero ;
		/// <summary>
		/// 对象句柄
		/// </summary>
		public System.IntPtr Handle
		{
			get
            {
                CheckHandle();
                return intHandle ;
            }
		}

        private bool _IsDisposed = false;

        /// <summary>
        /// 对象已经被销毁标记
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public bool IsDisposed
        {
            get
            {
                return _IsDisposed; 
            }
        }

		/// <summary>
		/// 销毁对象
		/// </summary>
		public virtual void Dispose()
		{
			if( intHandle != IntPtr.Zero )
			{
				DeleteObject( intHandle );
				intHandle = IntPtr.Zero ;
                _IsDisposed = true;
			}
		}

        /// <summary>
        /// 检查GDI句柄状态，若无效则创建GDI句柄
        /// </summary>
        protected virtual void CheckHandle()
        {
            if (intHandle == IntPtr.Zero )
            {
                throw new Exception("句柄无效");
            }
        }

		/// <summary>
		/// 选择对象到一个设备上下文
		/// </summary>
		/// <param name="hdc">设备上下文句柄</param>
		/// <returns>替换的对象的句柄</returns>
		public IntPtr SelectTo( IntPtr hdc )
		{
            CheckHandle();
			return SelectObject( hdc , intHandle );
		}

		/// <summary>
		/// 选择对象到一个设备上下文
		/// </summary>
		/// <param name="hdc">设备上下文句柄</param>
		/// <returns>替换的对象的句柄</returns>
		public IntPtr SelectTo( int hdc )
		{
            CheckHandle();
			return SelectObject( new IntPtr( hdc ) , intHandle );
		}

		/// <summary>
		/// 取消选择对象到设备上下文
		/// </summary>
		/// <param name="hdc">设备上下文句柄</param>
		/// <param name="handle">替换的对象的句柄</param>
		/// <returns>操作是否成功</returns>
		public bool UnSelect( IntPtr hdc , IntPtr handle )
		{
            CheckHandle();
			IntPtr h = SelectObject( hdc , handle );
			return h == this.intHandle ;
		}

		/// <summary>
		/// 取消选择对象到设备上下文
		/// </summary>
		/// <param name="hdc">设备上下文句柄</param>
		/// <param name="handle">替换的对象的句柄</param>
		/// <returns>操作是否成功</returns>
		public bool UnSelect( int hdc , IntPtr handle )
		{
            CheckHandle();
			IntPtr h = SelectObject( new IntPtr( hdc ) , handle );
			return h == this.intHandle ;
		}

		#region 声明Win32API函数 ******************************************************************
		
		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		private static extern int DeleteObject(System.IntPtr hObject);

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		private static extern IntPtr SelectObject(System.IntPtr hDC, System.IntPtr hObject);

		#endregion
	}//public abstract class BGDIObject : System.IDisposable
}