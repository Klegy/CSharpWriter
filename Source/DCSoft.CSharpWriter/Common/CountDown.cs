/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Runtime.InteropServices ;

namespace DCSoft.Common
{
	/// <summary>
	/// 倒计数对象
	/// </summary>
    /// <remarks>编制 袁永福</remarks>
	public class CountDown
	{
        private static CountDown myInstance = new CountDown();
        /// <summary>
        /// 对象唯一静态实例
        /// </summary>
        public static CountDown Instance
        {
            get
            {
                return myInstance;
            }
        }

		[DllImport( "kernel32.dll ")] 
		static extern bool QueryPerformanceCounter( ref long lpPerformanceCount); 
		[DllImport( "kernel32.dll ")] 
		static extern bool QueryPerformanceFrequency( ref long lpFrequency); 

		static long _f = 0; 

		/// <summary>
		/// 获得当前系统时间戳，单位为 0.1微毫秒(十万分之一秒)
		/// </summary>
		/// <returns></returns>
		public static long GetTickCountExt() 
		{ 
			long f = _f; 

			if (f == 0) 
			{ 
				if (QueryPerformanceFrequency(ref f)) 
				{ 
					_f = f; 
				} 
				else 
				{ 
					_f = -1; 
				} 
			} 
			if (f == -1) 
			{ 
				return Environment.TickCount * 10000; 
			} 
			long c = 0; 
			QueryPerformanceCounter(ref c); 
			return (long)(((double)c) * 1000 * 10000 / ((double)f)); 
		}

		/// <summary>
		/// 无作为的初始化对象
		/// </summary>
		public CountDown()
		{
			this.Reset();
		}
		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="vCount">毫秒为单位的总时间</param>
		public CountDown( int vCount )
		{
			intCount = vCount ;
			this.Reset();
		}
		/// <summary>
		/// 毫秒为单位的总的倒计数值
		/// </summary>
		protected int intCount = 0 ;
		/// <summary>
		/// 毫秒为单位的总的倒计数值
		/// </summary>
		public int Count
		{
			get{ return intCount ;}
			set{ intCount = value;}
		}
		/// <summary>
		/// 重置对象,开始计数
		/// </summary>
		protected int intStartTick = 0 ;

		private long intStartTickExt = GetTickCountExt() ;

		/// <summary>
		/// 重置对象,开始计数
		/// </summary>
		public void Reset()
		{
			intStartTick = System.Environment.TickCount ;
			intStartTickExt = GetTickCountExt();
		}
		/// <summary>
		/// 重置对象,设置新的计数总数,并开始计数
		/// </summary>
		/// <param name="vCount">新的计数总数</param>
		public void Reset( int vCount )
		{
			intCount = vCount ;
			intStartTick = System.Environment.TickCount ;
		}
		/// <summary>
		/// 已经消耗的毫秒数
		/// </summary>
		public int Spend
		{
			get
			{
				return System.Environment.TickCount - intStartTick ;
			}
		}
		/// <summary>
		/// 高精度的已消耗的时间数,以十万分之一秒为单位
		/// </summary>
		public long SpendExt
		{
			get
			{
				return GetTickCountExt() - intStartTickExt ;
			}
		}
		/// <summary>
		/// 剩余的毫秒数
		/// </summary>
		public int Remain
		{
			get
			{
				int intTick = System.Environment.TickCount ;
				return intCount - ( intTick - intStartTick );
			}
		}
		/// <summary>
		/// 休息当前线程来消耗掉所有剩余时间
		/// </summary>
		public void SleepToEnd()
		{
			int intRemain = this.Remain ;
			if( intRemain > 0 )
			{
				System.Threading.Thread.Sleep( intRemain );
			}
		}

        private System.Text.StringBuilder myLogString = new System.Text.StringBuilder();
        public void Log(string text)
        {
            myLogString.Append(Environment.NewLine + this.Spend + " : " + text);
        }

        public void LogExt(string text)
        {
            long spend = this.SpendExt;
            long mod = spend % 10000;
            spend = (spend - mod) / 10000;
            myLogString.Append(Environment.NewLine + spend + "." + mod + " : " + text);
        }

        public string LogText
        {
            get
            {
                return myLogString.ToString();
            }
        }

        public void ClearLog()
        {
            myLogString = new System.Text.StringBuilder();
        }

	}//public class CountDown
}