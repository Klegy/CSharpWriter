/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using Microsoft.Win32 ;
namespace DCSoft.WinForms.Native
{
	/// <summary>
	/// IE的帮助类,可读取注册表中IE的相关信息
	/// </summary>
    /// <remarks>编写 袁永福</remarks>
	public sealed class IEHelper
	{
        ///// <summary>
        ///// 获得HTML文档配套的子文件目录名
        ///// </summary>
        ///// <param name="strFileName">文件名</param>
        ///// <returns>目录名</returns>
        //public static string GetHtmlFilesPath(string strFileName)
        //{
        //    if (System.IO.Path.IsPathRooted(strFileName) == false)
        //    {
        //        strFileName = System.IO.Path.Combine(System.Environment.CurrentDirectory, strFileName);
        //    }
        //    string path = System.IO.Path.GetDirectoryName(strFileName);
        //    string name = System.IO.Path.GetFileNameWithoutExtension(strFileName);
        //    path = System.IO.Path.Combine(path, name + ".files");
        //    return path;
        //}

        ///// <summary>
        ///// 创建HTML文档配套的子文件目录
        ///// </summary>
        ///// <param name="strFileName">HTML文件名</param>
        ///// <returns>目录名</returns>
        //public static string CreateHtmlFilesPath(string strFileName)
        //{
        //    string path = GetHtmlFilesPath(strFileName);
        //    if (System.IO.Directory.Exists(strFileName) == false)
        //    {
        //        System.IO.Directory.CreateDirectory(strFileName);
        //    }
        //    return path;
        //}

		/// <summary>
		/// 用IE打开指定的URL地址
		/// </summary>
		/// <param name="strURL">URL地址</param>
		/// <returns>新打开的IE进程对象</returns>
		public static System.Diagnostics.Process Run( string strURL )
		{
//			return System.Diagnostics.Process.Start( strURL );
////			return null;
			System.Diagnostics.ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo();
			si.FileName = ExecutablePath ;
			si.Arguments = "\"" + strURL + "\"";
			System.Diagnostics.Process p = System.Diagnostics.Process.Start( si );
			return p ;
		}
		/// <summary>
		/// 用IE打开指定的文件
		/// </summary>
		/// <param name="strFileName">文件名</param>
		/// <returns>新打开的IE进程对象</returns>
		public static System.Diagnostics.Process OpenFile( string strFileName )
		{
			System.Diagnostics.ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo();
			si.FileName = ExecutablePath ;
			si.Arguments = "\"file://" + strFileName + "\"" ;
			System.Diagnostics.Process p = System.Diagnostics.Process.Start( si );
			return p;
		}
		/// <summary>
		/// IE可执行文件路径
		/// </summary>
		public static string ExecutablePath
		{
			get
			{
				return GetRegString(
					Registry.LocalMachine , 
					@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\IEXPLORE.EXE" ,
					null);
			}
		}
		/// <summary>
		/// 额外版本信息
		/// </summary>
		public static string MinorVersion
		{
			get
			{
				return GetRegString( Registry.LocalMachine , RootName2 , "MinorVersion");
			}
		}
		/// <summary>
		/// 建立号
		/// </summary>
		public static string Build
		{
			get
			{
				return GetRegString( Registry.LocalMachine , RootName , "Build" );
			}
		}
		/// <summary>
		/// 获得IE的版本信息
		/// </summary>
		public static System.Version Version
		{
			get
			{
				string v = GetRegString( Registry.LocalMachine , RootName , "Version"); ;
				if( v == null)
					return new System.Version();
				else
					return new System.Version( v );
			}
		}
		/// <summary>
		/// IE版本字符串
		/// </summary>
		public static string VersionString
		{
			get
			{
				return GetRegString( Registry.LocalMachine , RootName , "Version");
			}
		}
		/// <summary>
		/// 返回VML的版本信息
		/// </summary>
		public static System.Version VMLVersion
		{
			get
			{
				string vValue = GetRegString( 
					Registry.LocalMachine ,
					RootName + "\\Version Vector" ,
					"VML" );
				if( vValue == null)
					return new System.Version();
				else
					return new System.Version( vValue );
			}
		}
		/// <summary>
		/// 主页
		/// </summary>
		public static string LocalPage
		{
			get
			{
				return GetRegString( Registry.CurrentUser , RootName3 + "\\Main" , "Local Page");
			}
		}
		/// <summary>
		/// 起始页地址
		/// </summary>
		public static string StartPage
		{
			get
			{
				return GetRegString( Registry.CurrentUser , RootName3 + "\\Main" , "Start Page");
			}
		}
		/// <summary>
		/// 是否禁止脚本调试
		/// </summary>
		public static bool DisableScriptDebugger
		{
			get
			{
				return GetRegString( Registry.CurrentUser , RootName3 + "\\Main" , "Disable Script Debugger") == "yes" ;
			}
		}
		/// <summary>
		/// IE窗体标题
		/// </summary>
		public static string WindowTitle
		{
			get
			{
				return GetRegString( Registry.CurrentUser ,  RootName3 + "\\Main" , "Window Title");
			}
		}

		/// <summary>
		/// 获得用户曾经手工输入的URL字符串列表
		/// </summary>
		public static string[] TypedURLs
		{
			get
			{
				string[] strValues = null;
				RegistryKey key = Registry.CurrentUser.OpenSubKey( RootName3 + "\\TypedURLs");
				if( key != null)
				{
					strValues = key.GetValueNames();
					if( strValues != null)
					{
						for(int iCount = 0 ; iCount < strValues.Length ; iCount ++)
						{
							strValues[ iCount ] = Convert.ToString(key.GetValue( strValues[ iCount ]));
						}
					}
					key.Close();
				}
				return strValues ;
			}
		}

		#region 内部成员 **************************************************************************
		
		private const string RootName = @"SOFTWARE\Microsoft\Internet Explorer";
		private const string RootName2 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings";
		private const string RootName3 = @"Software\Microsoft\Internet Explorer";
		
		private static string GetRegString( RegistryKey RootKey , string Path , string KeyName )
		{
			object vValue = GetRegValue( RootKey , Path , KeyName );
			if( vValue == null)
				return null;
			else
				return Convert.ToString( vValue );
		}

		private static object GetRegValue( RegistryKey RootKey , string Path , string KeyName )
		{
			RegistryKey key = RootKey.OpenSubKey( Path );
			if( key == null)
				return null;
			else
			{
				object vValue = key.GetValue( KeyName );
				key.Close();
				return vValue ;
			}
		}
		private IEHelper(){}

		#endregion
 
	}//public sealed class IEHelper
}