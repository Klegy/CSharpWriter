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
using System.Text ;
using System.IO;

namespace DCSoft.Common
{
	/// <summary>
	/// 文件处理类
	/// </summary>
    /// <remarks>编制 袁永福</remarks>
	public sealed class FileHelper
	{

		/// <summary>
		/// 判断一个文件存在而且是标记为只读的
		/// </summary>
		/// <param name="strFileName">文件名</param>
		/// <returns>是否只读</returns>
		public static bool IsReadonlyFile( string strFileName )
		{
			if( strFileName == null || strFileName.Length == 0 )
				return false;
			if( File.Exists( strFileName ) == false )
				return false;
			if( ( File.GetAttributes( strFileName ) & FileAttributes.ReadOnly ) 
				== FileAttributes.ReadOnly )
			{
				return true ;
			}
			return false;
		}

		/// <summary>
		/// 获得路径的长路径名
		/// </summary>
		/// <param name="ShortPath">路径名,可以是短路径名</param>
		/// <returns>获得的长路径名</returns>
		public static string GetLongPath( string ShortPath )
		{
			StringBuilder buffer = new StringBuilder(260);
			StringBuilder temp = new StringBuilder();
			if (0 != GetLongPathName ( ShortPath , buffer, (uint) buffer.Capacity))
				return buffer.ToString ();
			else
				return null;
		}

		/// <summary>
		/// 获得路径的短路径名
		/// </summary>
		/// <param name="Path">路径名,可以是长路径名</param>
		/// <returns>获得的短路径名</returns>
		public static string GetShortPath( string Path )
		{
			StringBuilder buffer = new StringBuilder(260);
			if (0 != GetShortPathName (Path, buffer, (uint) buffer.Capacity))
				return buffer.ToString ();
			else
				return null;
		}
		/// <summary>
		/// 查找文件
		/// </summary>
		/// <param name="SearchPath">开始查找的目录</param>
		/// <param name="SearchPattern">文件名匹配字符串</param>
		/// <param name="Deeply">是否进行深入子孙目录查找文件</param>
        /// <param name="ReturnAbsPath">是否返回绝对路径名</param>
		/// <returns>查找到的文件名组成的数组</returns>
		public static string[] SearchFiles(
            string SearchPath ,
            string SearchPattern ,
            bool Deeply ,
            bool ReturnAbsPath )
		{
			if( SearchPath == null )
				return null;
			if( ! System.IO.Directory.Exists( SearchPath ))
				return null;
			if( SearchPattern == null || SearchPattern.Length == 0 )
				SearchPattern = "*.*";
			System.Collections.ArrayList list = new System.Collections.ArrayList();
			InnerSearchFiles( SearchPath , SearchPattern , list , Deeply );
			if( ReturnAbsPath == false )
			{
				for( int iCount = 0 ; iCount < list.Count ; iCount ++ )
				{
					string name = ( string ) list[ iCount ] ;
					name = name.Substring( SearchPath.Length ) ;
					if( name.StartsWith("\\") || name.StartsWith("/"))
						name = name.Substring( 1 );
					list[ iCount ] = name ;
				}
			}
			return ( string [] ) list.ToArray( typeof( string ));
		}

		/// <summary>
		/// 保存一个文件数据到一个流中
		/// </summary>
		/// <param name="strFileName">文件名</param>
		/// <param name="OutputStream">保存数据的输出流</param>
		/// <returns>操作的字节数</returns>
		public static int SaveFileToStream( string strFileName , System.IO.Stream OutputStream )
		{
			if( strFileName == null 
				|| OutputStream == null )
				return 0 ;

			if( System.IO.File.Exists( strFileName ) == false )
				return 0 ;

			using( System.IO.FileStream FileStream = new System.IO.FileStream(
					   strFileName ,
					   System.IO.FileMode.Open ,
					   System.IO.FileAccess.Read ))
			{
				int iCount = CopyStream( FileStream , OutputStream );
				FileStream.Close();
				return iCount ;
			}
		}
		/// <summary>
		/// 保存流中的数据到一个文件中
		/// </summary>
		/// <param name="InputStream">流对象</param>
		/// <param name="strFileName">保存数据的文件名</param>
		/// <returns>保存的字节数</returns>
		public static int SaveStreamToFile( System.IO.Stream InputStream , string strFileName )
		{
			if( InputStream == null 
				|| strFileName == null )
				return 0 ;
			using( System.IO.FileStream FileStream = new System.IO.FileStream( 
					   strFileName , 
					   System.IO.FileMode.Create , 
					   System.IO.FileAccess.Write ))
			{
				int iCount = CopyStream( InputStream , FileStream );
				FileStream.Close();
				return iCount ;
			}
		}

		/// <summary>
		/// 安全的不触发异常的删除文件
		/// </summary>
		/// <param name="strFileName">文件名</param>
		/// <returns>文件是否删除</returns>
		public static bool SafeDelete( string strFileName )
		{
			if( strFileName == null || strFileName.Length == 0 )
			{
				return false;
			}
			try
			{
				if( System.IO.File.Exists( strFileName ))
				{
					System.IO.File.SetAttributes( strFileName , System.IO.FileAttributes.Normal );
					System.IO.File.Delete( strFileName );
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// 将一个路径字符串拆成目录名和文件名,文件名支持通配符,函数输出一个包含两个字符串的数组
		/// 其中第一个字符串为目录名,第二个字符串为文件名
		/// </summary>
		/// <param name="strPath">路径字符串</param>
		/// <returns>输出的字符串数组</returns>
		public static string[] SplitPattern( string strPath )
		{
			string strDir = null;
			string strPattern = null;
			int index = strPath.LastIndexOfAny("/\\".ToCharArray());
			if( index > 0 )
			{
				strDir = strPath.Substring( 0 , index ) + System.IO.Path.DirectorySeparatorChar ;
				strPattern = strPath.Substring( index + 1 );
			}
			else
			{
				strDir = null;
				strPattern = strPath ;
			}
			if( strPath.IndexOf('*') >= 0 || strPath.IndexOf('?')>= 0 )
			{
				
			}
			else
			{
				if( System.IO.Directory.Exists( strPath ))
				{
					strDir = strPath ;
					strPattern = "*";
				}
			}
			return new string[]{ strDir , strPattern };
		}

		/// <summary>
		/// 格式化输出字节大小数据
		/// </summary>
		/// <param name="ByteSize">字节数</param>
		/// <returns>输出的字符串</returns>
		public static string FormatByteSize( int ByteSize )
		{
			byte[] buffer = new byte[30];
			StrFormatByteSizeA( ByteSize , buffer , buffer.Length );
			for(int iCount = 0 ; iCount < buffer.Length ; iCount ++)
			{
                if (buffer[iCount] == 0)
                {
                    return System.Text.Encoding.Default.GetString(buffer, 0, iCount);
                }
			}
			return null;
		}

		/// <summary>
		/// 格式化输出字节大小数据
		/// </summary>
		/// <param name="ByteSize">字节数</param>
		/// <returns>输出的字符串</returns>
		public static string FormatByteSize( long ByteSize )
		{
			byte[] buffer = new byte[30];
			StrFormatByteSize64( ByteSize , buffer , buffer.Length );
			for(int iCount = 0 ; iCount < buffer.Length ; iCount ++ )
			{
				if( buffer[iCount] == 0 )
					return System.Text.Encoding.GetEncoding(936).GetString( buffer , 0 , iCount );
			}
			return null;
		}

		/// <summary>
		/// 从指定文件读取二进制数据,返回获得的字节数组,若文件不存在或读取失败则返回空引用
		/// </summary>
		/// <param name="strFile">文件名</param>
		/// <returns>获得字节数组,若读取失败则返回空引用</returns>
		public static byte[] LoadBinaryFile( string strFile )
		{
			try
			{
				if( strFile != null && System.IO.File.Exists( strFile ))
				{
					System.IO.FileInfo info = new System.IO.FileInfo( strFile );
					if( info.Length == 0 )
						return null;
					using( System.IO.FileStream myStream = info.Open(
                        System.IO.FileMode.Open ,
                        System.IO.FileAccess.Read ))
					{
						byte[] byts = new byte[ myStream.Length ];
						myStream.Read( byts , 0 , byts.Length );
						myStream.Close();
						return byts ;
					}
				}
			}
			catch
			{
			}
			return null;
		}//public static byte[] LoadBinaryFile( string strFile )

		/// <summary>
		/// 向文件保存二进制数据
		/// </summary>
		/// <param name="strFile">文件名</param>
		/// <param name="byts">字节数组</param>
		/// <returns>保存是否成功</returns>
		public static bool SaveBinaryFile( string strFile , byte[] byts )
		{
			try
			{
				if( strFile != null)
				{
					using( System.IO.FileStream myStream = new System.IO.FileStream(
                        strFile ,
                        System.IO.FileMode.Create , 
                        System.IO.FileAccess.Write ))
					{
						myStream.Write( byts , 0 , byts.Length );
						myStream.Close();
						return true;
					}
				}
			}
			catch
			{
			}
			return false;
		}

        /// <summary>
        /// 使用指定的文本编码格式读取一个文本文件的内容
        /// </summary>
        /// <param name="strFileName">文件名</param>
        /// <param name="encoding">文本编码格式</param>
        /// <returns>读取的文件内容</returns>
        public static string LoadTextFile(string strFileName, System.Text.Encoding encoding)
        {
            if (strFileName == null)
                throw new ArgumentNullException("strFileName");
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            if (System.IO.File.Exists(strFileName))
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(
                    strFileName, encoding))
                {
                    string txt = reader.ReadToEnd();
                    return txt;
                }
            }
            return null;
        }

        /// <summary>
        /// 使用指定的文本编码格式将文本内容保存到文件中
        /// </summary>
        /// <param name="strFileName">文件名</param>
        /// <param name="strText">文本内容</param>
        /// <param name="encoding">文本编码格式</param>
        /// <returns>操作是否成功</returns>
        public static bool SaveTextFile(
            string strFileName,
            string strText ,
            System.Text.Encoding encoding)
        {
            if (strFileName == null)
                throw new ArgumentNullException("strFileName");
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            if( strText == null )
                strText = "";
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(
                strFileName,
                false,
                encoding))
            {
                writer.Write(strText);
            }
            return true;
        }

		/// <summary>
		/// 使用GB2312编码格式读取一个文本文件的内容
		/// </summary>
		/// <param name="strFile">文件名</param>
		/// <returns>读取的文件内容，若文件不存在或发生错误则返回空引用</returns>
		public static string LoadAnsiFile( string strFile)
		{
			System.IO.StreamReader myReader = null;
			try
			{
				if( System.IO.File.Exists( strFile ))
				{
					myReader = new System.IO.StreamReader(
                        strFile ,
                        System.Text.Encoding.GetEncoding(936));
					string strText = myReader.ReadToEnd();
					myReader.Close();
					return strText ;
				}
			}
			catch
			{
				if( myReader != null)
					myReader.Close();
			}
			return null;
		}//public static string LoadAnsiFile( string strFile)

		/// <summary>
		/// 使用GB2312编码格式保存字符串到一个文件中
		/// </summary>
		/// <param name="strFile">文件名</param>
		/// <param name="strText">字符串数据</param>
		/// <returns>操作是否成功</returns>
		public static bool SaveAnsiFile( string strFile , string strText)
		{
			using( System.IO.StreamWriter myWriter = new System.IO.StreamWriter(
                strFile ,
                false ,
                System.Text.Encoding.GetEncoding(936)))
			{
				myWriter.Write( strText );
				myWriter.Close();
				return true;
			}
		}
		/// <summary>
		/// 使用UTF8编码格式保存文本到一个文件中
		/// </summary>
		/// <param name="strFileName">文件名</param>
		/// <param name="strText">文本内容</param>
		/// <returns>操作是否成功</returns>
		public static bool SaveUTF8File( string strFileName , string strText )
		{
			using( System.IO.StreamWriter myWriter = new System.IO.StreamWriter(
                strFileName , 
                false ,
                System.Text.Encoding.UTF8 ))
			{
				myWriter.Write( strText );
				myWriter.Close();
				return true;
			}
		}
		/// <summary>
		/// 使用指定编码格式从一个文本文件中读取所有行的文本内容
		/// </summary>
		/// <param name="strFile">文件名</param>
		/// <param name="myEncoding">编码格式对象</param>
		/// <returns>读取的文本行组成的字符串数组</returns>
		public static string[] ReadLines(
            string strFile ,
            System.Text.Encoding myEncoding )
		{
			using( System.IO.StreamReader myReader = new System.IO.StreamReader( strFile , myEncoding  ))
			{
				System.Collections.ArrayList myList = new System.Collections.ArrayList();
				string strLine = myReader.ReadLine();
				while( strLine != null )
				{
					myList.Add( strLine );
					strLine = myReader.ReadLine();
				}
				return (string[]) myList.ToArray( typeof( string ));
			}
		}
		/// <summary>
		/// 使用指定编码向一个文件写入多行文本数据
		/// </summary>
		/// <param name="strFile">文件名</param>
		/// <param name="myEncoding">编码格式对象</param>
		/// <param name="strLines">保存多行文本数据的字符串数组</param>
		public static void SaveLines( 
            string strFile ,
            System.Text.Encoding myEncoding ,
            string[] strLines )
		{
			using( System.IO.StreamWriter myWriter = new System.IO.StreamWriter( strFile , false , myEncoding ))
			{
				foreach( string strLine in strLines )
					myWriter.WriteLine( strLine );
			}
		}
		/// <summary>
		/// 检测文件名是否是合法的文件名
		/// </summary>
		/// <param name="strFileName">文件名字符串</param>
		/// <returns>文件名是否合法</returns>
		public static bool CheckFileNameInValidChar( string strFileName )
		{
			if( strFileName == null 
				|| strFileName.Length == 0 
				|| strFileName.Length > 255 )
				return false;
			// 在Windows操作系统文件名中不可出现的字符列表
			const string InValidChars = "\\/:*?\"<>|";
			// 检测文件名对于Windows操作系统是否合法
			foreach( char c in strFileName)
			{
				if( InValidChars.IndexOf( c ) >= 0 )
				{
					return false;
				}
			}//foreach
			return true;
		}

        /// <summary>
        /// 修整文件名
        /// </summary>
        /// <param name="strFileName">原始文件名</param>
        /// <param name="InvalidReplaceChar">替换掉错误字符的字符</param>
        /// <returns>修整后的文件名</returns>
        public static string FixFileName( string strFileName , char InvalidReplaceChar)
        {
            if (strFileName == null || strFileName.Length == 0)
            {
                return strFileName;
            }
            const string InValidChars = "\\/:*?\"<>|";
            if (InValidChars.IndexOf(InvalidReplaceChar) >= 0)
            {
                throw new ArgumentNullException("InvalidReplaceChar");
            }
            System.Text.StringBuilder myStr = new StringBuilder();
            foreach (char c in strFileName)
            {
                if (InValidChars.IndexOf(c) >= 0)
                {
                    if (InvalidReplaceChar > 0)
                    {
                        myStr.Append(InvalidReplaceChar);
                    }
                }
                else
                {
                    myStr.Append(c);
                }
            }
            return myStr.ToString();
        }

		/// <summary>
		/// 获得文件名的大写形式的扩展名,若没有扩展名则返回空引用
		/// </summary>
		/// <remarks>文件扩展名就是文件名字符串中最后一个斜杠字符(包括/\)
		/// 后面最后一个点号后面的部分</remarks>
		/// <param name="strFileName">文件名</param>
		/// <returns>文件扩展名</returns>
		public static string GetExtension( string strFileName)
		{
			if( strFileName != null && strFileName.Length > 0 )
			{
				int index = strFileName.LastIndexOf('.');
				int index2 = strFileName.LastIndexOfAny("/\\".ToCharArray());
				if( index >= 0 && index > index2 )
				{
					string ext = strFileName.Substring( index + 1).Trim().ToUpper();
					if( ext.Length > 0 )
						return ext ;
				}
			}
			return null;
		}
		
		/// <summary>
		/// 修正文件夹字符串,保证字符串以文件夹分隔符结尾
		/// </summary>
		/// <param name="strDir">文件夹字符串</param>
		/// <returns>修正后的字符串</returns>
		public static string FixDirectoryName( string strDir)
		{
			if( strDir != null && strDir.Length > 0 )
			{
				if( strDir[ strDir.Length - 1] != System.IO.Path.DirectorySeparatorChar )
					strDir = strDir + System.IO.Path.DirectorySeparatorChar ;
			}
			return strDir ;
		}

		/// <summary>
		/// 获得没有目录和扩展名的简单文件名
		/// </summary>
		/// <param name="strPath">路径名</param>
		/// <returns>简单文件名</returns>
		public static string GetSimpleName( string strPath )
		{
			string strName = System.IO.Path.GetFileName( strPath );
			int index = strName.LastIndexOf('.');
			if( index >= 0 )
				return strName.Substring( 0 , index );
			else
				return strName ;
		}
		
		/// <summary>
		/// 进行文件通配符的判断,支持任意个*和?,字符串匹配不区分大小写
		/// </summary>
		/// <remarks>本函数调用了 SplitAny 函数</remarks>
		/// <param name="FileName">文件名</param>
		/// <param name="MatchPattern">包含通配符的字符串</param>
		/// <returns>文件名是否匹配通配符字符串</returns>
		public static bool MatchFileName( string FileName , string MatchPattern)
		{
			if( FileName == null || FileName.Length == 0 )
				return false;
			if( FileName != null)
			{
				FileName = System.IO.Path.GetFileName( FileName );
				FileName = FileName.ToUpper();
			}
			if( MatchPattern != null)
				MatchPattern = MatchPattern.ToUpper();

			string[] strItems = SplitAny( MatchPattern , "*?");
			if( strItems != null)
			{
				int index = 0 ;
				for(int iCount = 0 ; iCount < strItems.Length ; iCount ++)
				{
					string strItem = strItems[iCount];
					if( strItem == "*")
					{
						if( iCount == strItems.Length -1 )
							return true;
						index = FileName.IndexOf( strItems[iCount+1] , index );
						if( index < 0 )
							return false;
					}
					else if( strItem == "?" )
					{
						index ++ ;
					}
					else if( FileName.Length < index + strItem.Length 
						|| FileName.Substring( index , strItem.Length ) != strItem )
					{
						return false;
					}
					else
					{
						index += strItem.Length ;
					}
				}//foreach
				return FileName.Length == index ;
			}//if
			return true;
		}//public static bool MatchFileName( string FileName , string MatchPattern)

		#region 内部私有的成员 ****************************************************

		/// <summary>
		/// 将一个流中的所有的数据输出到另一个流中
		/// </summary>
		/// <param name="stream1">输出流</param>
		/// <param name="stream2">输入流</param>
		/// <returns>操作的字节数</returns>
		private static int CopyStream(
			System.IO.Stream stream1 ,
			System.IO.Stream stream2 )
		{
			byte[] bs = new byte[ 8 * 1024 ];
			int iCount = 0 ;
			while( true )
			{
				int len = stream1.Read( bs , 0 , bs.Length );
				if( len <= 0 )
					break;
				stream2.Write( bs , 0 , len );
				iCount += len ;
			}
			return iCount ;
		}

		/// <summary>
		/// 依据若干个分隔字符将一个字符串分隔为若干部分,分隔的部分包括分隔字符
		/// </summary>
		/// <remarks>例如字符串"abc*def?hk",若分隔字符为"*?",
		/// 则本函数处理返回的字符串数组为 "abc" , "*" , "def" ,
		///  "?" , "hk"</remarks>
		/// <param name="strText">要处理的字符串</param>
		/// <param name="Spliter">分隔字符组成的字符串</param>
		/// <returns>分隔后的字符串数组</returns>
		private static string[] SplitAny( string strText , string Spliter)
		{
			if( strText == null
                || strText.Length == 0 
                || Spliter == null 
                || Spliter.Length == 0 )
				return null;
			System.Collections.ArrayList myList = new System.Collections.ArrayList();
			int LastIndex = 0 ;
			for(int iCount = 0 ; iCount < strText.Length ; iCount ++ )
			{
				if( Spliter.IndexOf( strText[iCount]) >= 0 )
				{
					if( iCount > LastIndex )
						myList.Add( strText.Substring( LastIndex , iCount - LastIndex ));
					myList.Add( strText.Substring( iCount , 1 ));
					LastIndex = iCount + 1;
				}
			}
			if( LastIndex < strText.Length )
				myList.Add( strText.Substring( LastIndex ));
			if( myList.Count > 0 )
				return ( string[]) myList.ToArray( typeof( string ));
			else
				return null;
		}


		/// <summary>
		/// 比较两个字符串是否相等
		/// </summary>
		/// <param name="s1"></param>
		/// <param name="s2"></param>
		/// <returns></returns>
		private static bool EqualString( string s1 , string s2)
		{
			if( s1 == null && s2 == null)
				return true;
			if( s1 != null && s2 != null)
				return s1 == s2 ;
			else
				return false;
		}
		
		private static void InnerSearchFiles(
            string SearchPath ,
            string SearchPattern , 
            System.Collections.ArrayList list ,
            bool Deeply )
		{
			string[] FileNames = System.IO.Directory.GetFiles( SearchPath , SearchPattern );
			if( FileNames != null )
			{
				foreach( string FileName in FileNames )
				{
					if( FileName != "." || FileName != ".." )
						list.Add( FileName );
				}
			}
			if( Deeply )
			{
				string[] dirs = System.IO.Directory.GetDirectories( SearchPath );
				if( dirs != null )
				{
					foreach( string dir in dirs )
					{
						if( dir != "." || dir != ".." )
						{
							InnerSearchFiles( dir , SearchPattern , list , true );
						}
					}
				}
			}
		}

		[DllImport("kernel32.dll")]
		private static extern uint GetLongPathName (string shortPath,
			StringBuilder buffer, uint bufLength);


		[DllImport("kernel32.dll", SetLastError=true)]
		private static extern uint GetShortPathName ( string longPath,
			StringBuilder buffer, uint bufLength);

		[DllImport("kernel32.dll", CharSet=System.Runtime.InteropServices.CharSet.Auto)]
		private static extern int GetLongPathName( 
            string ShortPath ,
            string LongPath ,
            int Buffer );

		[System.Runtime.InteropServices.DllImport("shlwapi.dll" )]
		private static extern int StrFormatByteSizeA( int dw , byte[] buf , int bufSize );

		[System.Runtime.InteropServices.DllImport("shlwapi.dll" )]
		private static extern int StrFormatByteSize64( long lng , byte[] buf , int bufSize );

		/// <summary>
		/// 不对象不可实例化
		/// </summary>
		private FileHelper(){}

		#endregion

	}//public sealed class FileNameHelper
}