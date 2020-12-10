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
	/// 字符串格式处理类
	/// </summary>
    /// <remarks>编制 袁永福</remarks>
	public sealed class StringFormatHelper
	{
        
        public static string AddLineCount(string MultilineText , int StartLineIndex )
        {
            if (MultilineText == null)
                return null;
            System.IO.StringReader reader = new System.IO.StringReader(MultilineText);
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            string strLine = reader.ReadLine();
            while (strLine != null)
            {
                list.Add(strLine);
                strLine = reader.ReadLine();
            }
            reader.Close();
            System.Text.StringBuilder myStr = new System.Text.StringBuilder();
            string format = new string('0', (int)(Math.Ceiling(Math.Log10(list.Count + StartLineIndex ))));
            string newLine = Environment.NewLine ;
            for (int iCount = 0; iCount < list.Count; iCount++)
            {
                if (myStr.Length > 0)
                    myStr.Append(newLine);
                int index = iCount + StartLineIndex;
                myStr.Append(index.ToString(format));
                myStr.Append(":");
                myStr.Append((string)list[iCount]);
            }
            return myStr.ToString();
        }

        /// <summary>
        /// 将一个多行字符转化为单行字符组成的字符串
        /// </summary>
        /// <param name="strText">多行文本</param>
        /// <returns>字符串数组</returns>
        public static string[] GetLines(string strText)
		{
			if( strText == null || strText.Length == 0 )
				return null;
			System.IO.StringReader reader = new System.IO.StringReader( strText );
			System.Collections.ArrayList list = new System.Collections.ArrayList();
			string strLine = reader.ReadLine();
			while( strLine != null )
			{
				list.Add( strLine );
				strLine = reader.ReadLine();
			}
			reader.Close();
			return ( string [] ) list.ToArray( typeof( string ));
		}

		/// <summary>
		/// 将一个多行字符串转换为单行字符串
		/// </summary>
		/// <param name="strText">原始字符串</param>
		/// <returns>处理后的字符串</returns>
		public static string ToSingleLine( string strText )
		{
			if( strText == null || strText.Length == 0 )
				return strText ;
			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
			foreach( char c in strText )
			{
				if( c != '\r' && c != '\n')
					myStr.Append( c );
			}
			return myStr.ToString();
		}

		/// <summary>
		/// 删除一个字符串中指定的字符
		/// </summary>
		/// <param name="strText">字符串</param>
		/// <param name="vChar">指定的字符</param>
		/// <returns>处理后的字符串</returns>
		public static string RemoveChar( string strText , char vChar )
		{
			if( strText == null || strText.Length == 0 )
				return strText ;
			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
			foreach( char c in strText )
				if( c != vChar )
					myStr.Append( c );
			return myStr.ToString();
		}
		/// <summary>
		/// 若存在指定的前缀字符串则删除前缀字符串
		/// </summary>
		/// <param name="strText">要处理的字符串</param>
		/// <param name="strPrefix">要删除的前缀字符串</param>
		/// <returns>处理后的字符串</returns>
		public static string RemovePrefix( string strText , string strPrefix )
		{
			if( strText == null)
				return strText ;
			if( strText.StartsWith( strPrefix ))
				return strText.Substring( strPrefix.Length );
			else
				return strText ;
		}
		/// <summary>
		/// 若存在指定的后缀字符串则删除后缀字符串
		/// </summary>
		/// <param name="strText">要处理的字符串</param>
		/// <param name="strEndfix">要删除的后缀字符串</param>
		/// <returns>处理后的字符串</returns>
		public static string RemoveEndfix( string strText , string strEndfix )
		{
			if( strText == null || strText.EndsWith( strEndfix ) == false )
				return strText ;
			return strText.Substring( 0 , strText.Length - strEndfix.Length );
		}

		/// <summary>
		/// 对一个多行文本的每行添加指定的前缀和后缀
		/// </summary>
		/// <param name="strText">多行文本</param>
		/// <param name="strPrefix">前缀</param>
		/// <param name="strEndfix">后缀</param>
		/// <returns>处理后的字符串</returns>
		public static string LinesAddfix( string strText , string strPrefix , string strEndfix ) 
		{
			if( strText == null || strText.Length == 0 )
				return strText ;

			System.IO.StringReader myReader = new System.IO.StringReader( strText );
			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
			string strLine = myReader.ReadLine();
			while( strLine != null)
			{
				if( strLine.Length > 0 )
				{
					if( strPrefix != null)
						myStr.Append( strPrefix );
				}
				myStr.Append( strLine );
				if( strLine.Length > 0 )
				{
					if( strEndfix != null)
						myStr.Append( strEndfix );
				}
				strLine = myReader.ReadLine();
				if( strLine == null)
					break;
				myStr.Append( System.Environment.NewLine );
			}
			myReader.Close();
			return myStr.ToString();
		}

		/// <summary>
		/// 计算所有的字符串的最大的字节长度，并可设置所有的字符串为相同的字节长度
		/// </summary>
		/// <param name="myList">保存字符串的数组</param>
		/// <param name="FixStyle">修正模式 0:不修正, 1:在原始字符产前面添加填充字符 2:在字符串后面添加填充字符</param>
		/// <param name="FillChar">修正时填充的字符,其Asc码从0到255</param>
		/// <returns>字符串最大的字节长度</returns>
		public static int FixStringByteLength( System.Collections.ArrayList myList , int FixStyle, char FillChar)
		{
			System.Text.Encoding myEncode = System.Text.Encoding.GetEncoding(936);
			int MaxLen = 0 ;
			foreach( string strItem in myList )
			{
				if( strItem != null)
				{
					int len = myEncode.GetByteCount( strItem );
					if( MaxLen < len )
						MaxLen = len ;
				}
			}
			if( FixStyle == 1 || FixStyle == 2 )
			{
				for(int iCount = 0 ; iCount < myList.Count ; iCount ++)
				{
					string strItem = (string) myList[iCount];
					if( strItem == null)
						strItem = "";
					int len = myEncode.GetByteCount( strItem );
					if( len != MaxLen )
					{
						if( FixStyle == 1 )
							strItem = new string( FillChar , MaxLen - len ) + strItem ;
						else
							strItem = strItem + new string( FillChar , MaxLen - len );
						myList[iCount] = strItem ;
					}
				}//for
			}
			return MaxLen ;
		}//public static int FixStringByteLength( System.Collections.ArrayList myList , int FixStyle, char FillChar)


		/// <summary>
		/// 清除一个字符串中的空白字符
		/// </summary>
		/// <param name="strText">原始字符串</param>
		/// <param name="intMaxLength">输出结果的最长长度,为0表示无限制</param>
		/// <param name="bolHtml">是否模仿HTML对空白字符的处理</param>
		/// <returns>没有空白字符的字符串</returns>
		public static string ClearWhiteSpace(string strText , int intMaxLength , bool bolHtml )
		{
			if( strText == null)
				return null;
			else
			{
				bool bolPreIsWhiteSpace = false;
				System.Text.StringBuilder myStr = new System.Text.StringBuilder();
				int iCount = 0 ;
				foreach( char vChar in strText)
				{
					if( char.IsWhiteSpace( vChar ) )
						bolPreIsWhiteSpace = true;
					else
					{
						if( bolHtml && bolPreIsWhiteSpace == true )
						{
							myStr.Append(" ");
						}
						myStr.Append( vChar );
						if( intMaxLength > 0 )
						{
							iCount ++ ;
							if( iCount > intMaxLength )
								break;
						}
						 
						bolPreIsWhiteSpace = false ;
					}
				}
				return myStr.ToString();
			}
		}
		
		
		/// <summary>
		/// 清除一段文本中所有的空白行
		/// </summary>
		/// <param name="strData">文本</param>
		/// <returns>处理后的文本</returns>
		public static string ClearBlankLine(string strData)
		{
			if(strData == null)
				return null;
			else
			{
				System.IO.StringReader myReader = new System.IO.StringReader(strData);
				System.Text.StringBuilder myStr = new System.Text.StringBuilder();
				string strLine = myReader.ReadLine();
				bool bolFirst = true;
				while(strLine != null)
				{
					foreach(System.Char myChr in strLine)
					{
						if(System.Char.IsWhiteSpace(myChr)==false)
						{
							if( !bolFirst )
								myStr.Append("\r\n");
							myStr.Append(strLine);
							bolFirst = false;
							break;
						}
					}
					strLine = myReader.ReadLine();
				}
				myReader.Close();
				return myStr.ToString();
			}
		}
		
		public static string RemoveBlank( string strText )
		{
			if( strText == null)
				return strText ;
			if( strText.Length == 0 )
				return strText ;
			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
			foreach( char myChar in strText )
			{
				if( char.IsWhiteSpace( myChar ) == false )
					myStr.Append( myChar );
			}
			return myStr.ToString();
		}
		
		/// <summary>
		/// 压缩所有的空白字符(将连续的空白字符压缩为一个空格)
		/// </summary>
		/// <param name="strData"></param>
		/// <returns></returns>
		public static string NormalizeSpace( string strData )
		{
			if( strData == null)
				return null;
			char[] chrOut = new char[ strData.Length ];
			int iStep = 0 ;
			bool bolFlag = false;
			for(int iCount = 0 ; iCount < strData.Length ; iCount ++)
			{
				if( char.IsWhiteSpace( strData[iCount]) )
				{
					if( bolFlag == false)
					{
						bolFlag = true ;
						chrOut[iStep] = ' ';
						iStep ++ ;
					}
				}
				else
				{
					bolFlag = false;
					chrOut[iStep] = strData[iCount];
					iStep ++ ;
				}
			}
			if( iStep == 0)
				return "";
			else
				return new string( chrOut , 0 , iStep );
		}//public static string NormalizeSpace( string strData )
		
		/// <summary>
		/// 判断一个字符串对象是否是空字符串
		/// </summary>
		/// <param name="strData">字符串</param>
		/// <returns>若字符串为空或者全部有空白字符组成则返回True,否则返回false</returns>
		public static bool IsBlankString(string strData)
		{
			if(strData == null)
				return true;
			else
			{
				for(int iCount = 0 ; iCount < strData.Length ; iCount++)
				{
					if(Char.IsWhiteSpace( strData[iCount])==false )
						return false;
				}
				return true;
			}
		}//public static bool isBlankString()

		/// <summary>
		/// 判断一个字符串是否存在空白字符
		/// </summary>
		/// <param name="strData">字符串</param>
		/// <returns>是否存在空白字符</returns>
		public static bool HasBlank( string strData )
		{
			if( strData == null)
				return false;
			for(int iCount = 0 ; iCount < strData.Length ; iCount ++)
			{
				if( char.IsWhiteSpace( strData[iCount] ))
					return true;
			}
			return false;
		}

		/// <summary>
		/// 判断一个字符串是否有内容,本函数和isBlankString相反
		/// </summary>
		/// <param name="strData">字符串对象</param>
		/// <returns>若字符串不为空且存在非空白字符则返回True 否则返回False</returns>
		public static bool HasContent( string strData )
		{
			if( strData != null && strData.Length > 0 )
			{
				foreach(char c in strData )
				{
					if( Char.IsWhiteSpace( c ) == false)
						return true;
				}
			}
			return false;
		}// bool HasContent()

		/// <summary>
		/// 判断一个字符串是否为空引用或者长度为零
		/// </summary>
		/// <param name="strData">字符串对象</param>
		/// <returns>是否为空引用或长度为0</returns>
		public static bool IsEmpty( string strData )
		{
			return ( strData == null || strData.Length == 0 );
		}
		
		/// <summary>
		/// 测试一个字符串中所有的字符都是字母或者数字
		/// </summary>
		/// <param name="strData">供测试的字符串</param>
		/// <returns>若字符串所有字符都是字母或数字则返回true ,否则返回 false</returns>
		public static bool IsLetterOrDigit(string strData )
		{
			if (strData != null)
			{
				for(int iCount = 0 ; iCount < strData.Length ; iCount++)
				{
					if (System.Char.IsLetterOrDigit(strData,iCount)==false)
						return false;
				}
				return true;
			}
			else
				return false;
		}

		/// <summary>
		/// 格式化字符串，进行分组处理
		/// </summary>
		/// <param name="strBase64">纯的Base64编码字符串</param>
		/// <param name="GroupSize">一组编码的字符个数</param>
		/// <param name="LineGroupCount">每行文本的编码组个数</param>
		/// <returns>格式化后的字符串</returns>
		public static string GroupFormatString(string strData , int GroupSize , int LineGroupCount)
		{
			if( strData == null || strData.Length == 0 || ( GroupSize <=0 && LineGroupCount <= 0 ) )
				return strData ;

			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
			int iSize = strData.Length ;
			int iCount = 0 ;
			LineGroupCount *= GroupSize ;
			while(true)
			{
				myStr.Append(" ");
				if(iCount + GroupSize < iSize)
				{
					myStr.Append(strData.Substring(iCount,GroupSize));
				}
				else
				{
					myStr.Append(strData.Substring(iCount));
					break;
				}
				iCount += GroupSize ;
				if(iCount % LineGroupCount == 0 )
					myStr.Append("\r\n");
			}
			return myStr.ToString();
		}
		private StringFormatHelper(){}
	}//public sealed class StringFormatHelper
}