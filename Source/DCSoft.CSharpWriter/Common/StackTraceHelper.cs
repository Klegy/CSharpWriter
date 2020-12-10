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
	/// 应用程序调用堆栈信息帮助类
	/// </summary>
    /// <remarks>编制 袁永福</remarks>
	public sealed class StackTraceHelper
	{
		/// <summary>
		/// 获得当前调用的方法的信息
		/// </summary>
		/// <returns>获得的方法的信息</returns>
		public static System.Reflection.MethodBase GetCurrentMethodInfo()
		{
			System.Diagnostics.StackTrace myTrace = new System.Diagnostics.StackTrace();
			if( myTrace.FrameCount < 2 )
				return null ;
			return myTrace.GetFrame( 1 ).GetMethod() ;
		}

        /// <summary>
        /// 判断当前是否运行在ASP.NET中
        /// </summary>
        /// <returns></returns>
        public static bool RuningASPNET()
        {
            System.Diagnostics.StackTrace myTrace = new System.Diagnostics.StackTrace();
            for (int iCount = 2; iCount < myTrace.FrameCount; iCount++)
            {
                System.Diagnostics.StackFrame frame = myTrace.GetFrame(iCount);
                Type t = frame.GetMethod().DeclaringType;
                if (t.IsSubclassOf(typeof(System.Web.UI.Control)))
                {
                    return true;
                }
                //else if (t.IsSubclassOf(typeof(System.Web.Services.WebService)))
                //{
                //    return true;
                //}
                else if (t.IsSubclassOf(typeof(System.Windows.Forms.Control)))
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 检查调用本方法的方法是否发生了递归
        /// </summary>
        /// <remarks>本函数是利用应用程序调用堆栈来判断是否存在递归</remarks>
        /// <returns>若发生了递归则返回true,否则返回false</returns>
        public static bool CheckRecursion()
		{
			System.Diagnostics.StackTrace myTrace = new System.Diagnostics.StackTrace();
			// 若堆栈小于三层则不可能出现递归
			if( myTrace.FrameCount < 3 )
				return false;
			System.IntPtr mh = myTrace.GetFrame( 1 ).GetMethod().MethodHandle.Value ;
			for( int iCount = 2 ; iCount < myTrace.FrameCount ; iCount ++ )
			{
				System.Reflection.MethodBase m = myTrace.GetFrame( iCount ).GetMethod();
				if( m.MethodHandle.Value == mh )
				{
					return true;
				}
			}
			return false;
		}
		public static void OutputStackTrace()
		{
			System.Diagnostics.StackTrace myTrace = new System.Diagnostics.StackTrace( true );
			System.Console.WriteLine( myTrace.ToString());
		}

        public static string StackTraceString
        {
            get
            {
                System.Diagnostics.StackTrace myTrace = new System.Diagnostics.StackTrace(true);
                return myTrace.ToString();
            }
        }


		public static void OutputStackTraceExt()
		{
			System.Diagnostics.StackTrace myTrace = new System.Diagnostics.StackTrace( true );
			System.Console.WriteLine("");
			for( int iCount = 0 ; iCount < myTrace.FrameCount ; iCount ++ )
			{
				System.Diagnostics.StackFrame frame = myTrace.GetFrame( iCount );
				System.Console.Write( iCount + " ");
				System.Console.Write( frame.GetMethod().ToString());
				if( frame.GetFileName() != null )
					System.Console.Write( frame.GetFileName());
				if( frame.GetFileLineNumber() >= 0 )
					System.Console.Write( " " + frame.GetFileLineNumber());
				System.Console.WriteLine("");
			}
		}

        public static string StackTraceExtString
        {
            get
            {
                System.Diagnostics.StackTrace myTrace = new System.Diagnostics.StackTrace(true);
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                for (int iCount = 0; iCount < myTrace.FrameCount; iCount++)
                {
                    System.Diagnostics.StackFrame frame = myTrace.GetFrame(iCount);
                    str.Append( System.Environment.NewLine + iCount + " ");
                    str.Append(frame.GetMethod().ToString());
                    if (frame.GetFileName() != null)
                        str.Append(frame.GetFileName());
                    if (frame.GetFileLineNumber() >= 0)
                        str.Append(" " + frame.GetFileLineNumber());
                    //str.Append(System.Environment.NewLine); 
                }
                return str.ToString();
            }
        }
         


//		/// <summary>
//		/// 检查调用本方法的方法是否发生了递归
//		/// </summary>
//		/// <remarks>本函数是利用应用程序调用堆栈来判断是否存在递归</remarks>
//		/// <param name="MaxCount">最多可以递归的次数</param>
//		/// <returns>若发生了递归则返回true,否则返回false</returns>
//		public static bool CheckRecursion( int MaxCount )
//		{
//			System.Diagnostics.StackTrace myTrace = new System.Diagnostics.StackTrace();
//			// 若堆栈小于三层则不可能出现递归
//			if( myTrace.FrameCount < 3 )
//				return false;
//			System.IntPtr mh = myTrace.GetFrame( myTrace.FrameCount - 2 ).GetMethod().MethodHandle.Value ;
//			for( int iCount = myTrace.FrameCount - 3 ; iCount >= 0 ; iCount -- )
//			{
//				System.Reflection.MethodBase m = myTrace.GetFrame( iCount ).GetMethod();
//				if( m.MethodHandle.Value == mh )
//				{
//					MaxCount -- ;
//					if( MaxCount <= 0 )
//						return true;
//				}
//			}
//			return false;
//		}

		/// <summary>
		/// 本对象不能实例化
		/// </summary>
		private StackTraceHelper()
		{
		}
	}//public sealed class CheckRecursion
}