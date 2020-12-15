/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System ;
namespace DCSoft.HtmlDom
{
	/// <summary>
	/// 内部使用,HTML字符串读取对象
	/// </summary>
	internal class HTMLTextReader
	{
		public static System.Collections.Specialized.NameValueCollection AnalyseValueString( string strText )
		{
			if( strText == null || strText.Length == 0 )
				return null;
			string[] items = strText.Split(';');
			System.Collections.Specialized.NameValueCollection myValues = new System.Collections.Specialized.NameValueCollection();
			foreach( string strItem in items )
			{
				int index = strItem.IndexOf('=');
				string strName = null;
				string strValue = null;
				if( index > 0 )
				{
					strName = strItem.Substring( 0 , index );
					strValue = strItem.Substring( index + 1 );
				}
				else
				{
					strName = strItem ;
					strValue = "";
				}
				strName = strName.Trim();
				strValue = strValue.Trim();
				myValues[ strName ] = strValue ;
			}
			return myValues ;
		}

		/// <summary>
		/// 分析一个字符串,该字符串包含特殊标记包含的数据源名称,本函数将该字符串分解为一个字符串数组,该
		/// 字符串数组序号为偶数的字符串为普通文本,奇数号字符串为特殊标记包含的字符串
		/// </summary>
		/// <param name="strText">供处理的原始字符串</param>
		/// <param name="strHead">标记的头字符串</param>
		/// <param name="strEnd">标记尾字符串</param>
		/// <returns>处理后的字符串</returns>
		public static string[] SplitVariableString(
			string strText, 
			string strHead,  
			string strEnd )
		{
			// 若原始字符串无效或者没有任何可用的参数则退出函数
			if(    strText 			== null 
				|| strHead			== null 
				|| strEnd 			== null 
				|| strHead.Length	== 0 
				|| strEnd.Length	== 0 
				|| strText.Length	== 0
				)
				return new string[]{ strText } ;
			
			int 	index = strText.IndexOf( strHead );
			// 若原始字符串没有变量标记则退出函数
			if(index < 0 ) 
				return new string[]{ strText } ;
			System.Collections.ArrayList myList = new System.Collections.ArrayList();

			string 	strKey ;
			int 	index2 ;
			int 	LastIndex = 0 ;
			do
			{	
				// 查找有 "[内容]" 样式的子字符串
				// 若没有找到 "[" 和 "]"的字符对则退出循环
				index2 = strText.IndexOf( strEnd ,  index + 1  );
				if(index2 > index)
				{
					// 若 "[" 符号后面出现 "]"符号则存在 "[]"字符对
					// 修正查找结果以保证 "[]"字符对中不出现字符 "["
					int index3 = index ;
					do
					{
						index = index3 ;
						index3 = strText.IndexOf(strHead, index3 + 1 );
					}while( index3 > index && index3 < index2 ) ;
				
					// 获得字符对夹着的子字符串,该子字符串为参数名
					// 若该参数名有效则向输出结果输出参数值
					// 否则不进行额外的处理
					strKey = strText.Substring(index + strHead.Length ,  index2 - index - strHead.Length  );
					if(LastIndex < index)
						myList.Add(  strText.Substring(LastIndex, index - LastIndex ) );
					else
						myList.Add( null );
					myList.Add( strKey );
					index = index2 +  strEnd.Length ;
					LastIndex = index ; 
				}
				else
				{
					break;
				}
			}while( index >=0 && index < strText.Length );
			// 添加处理过后剩余的字符串
			if(LastIndex < strText.Length   )
				myList.Add(  strText.Substring(LastIndex) );
			return ( string[] ) myList.ToArray( typeof( string ));
		}// End of function : string[] SplitVariableString()

		/// <summary>
		/// 压缩所有的空白字符(将连续的空白字符压缩为一个空格)
		/// </summary>
		/// <param name="strData"></param>
		/// <returns></returns>
		public static string NormalizeSpace( string strData )
		{
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
		/// 判断一个字符串对象是否是空字符串
		/// </summary>
		/// <param name="strData">字符串</param>
		/// <returns>若字符串为空或者全部有空白字符组成则返回True,否则返回false</returns>
		public static bool isBlankString(string strData)
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

		private string strText ;
		private string strLowerText ;
		private int intPosition ;
		private int intLength ;
		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="txt">要解析的HTML字符串</param>
		internal HTMLTextReader( string txt )
		{
			if( txt == null )
				txt = "";
			strText = txt ;
			intPosition = 0 ;
			intLength = strText.Length ;
			strLowerText = strText.ToLower();
		}
		/// <summary>
		/// 当前解析的字符的位置
		/// </summary>
		public int Position
		{
			get{ return intPosition ;}
			set{ intPosition = value ;}
		}
		/// <summary>
		/// 解析的字符串的长度
		/// </summary>
		public int Length
		{
			get{ return intLength ;}
		}
		/// <summary>
		/// 数据是否读取完毕
		/// </summary>
		public bool EOF
		{
			get{ return intPosition >= intLength ;}
		}
		/// <summary>
		/// 读取一个字符
		/// </summary>
		/// <returns></returns>
		public char Read()
		{
			intPosition ++ ;
			return strText[ intPosition - 1];
		}
		/// <summary>
		/// 获得当前字符
		/// </summary>
		public char Peek
		{
			get{ return strText[intPosition];}
		}
		
		/// <summary>
		/// 判断当前位置后面是否为指定的字符串,判断不区分大小写
		/// </summary>
		/// <param name="strText"></param>
		/// <returns></returns>
		public bool StartWidth( string strText)
		{
			if( intPosition + strText.Length < intLength )
			{
				string vText = strLowerText.Substring( intPosition ,strText.Length );
				return vText.Equals( strText );
			}
			else
				return false;
		}
		/// <summary>
		/// 获得前一个字符
		/// </summary>
		public char PreChar
		{
			get
			{
				if( intPosition > 0 )
					return strText[intPosition-1];
				else
					return '\0';
			}
		}
		/// <summary>
		/// 返回下一个字符
		/// </summary>
		public char NextChar
		{
			get
			{
				if( intPosition < intLength -1)
					return strText[ intPosition +1];
				else
					return '\0';
			}
		}
		/// <summary>
		/// 返回下下个字符
		/// </summary>
		public char SubNextChar
		{
			get
			{
				if( intPosition < intLength - 2 )
					return strText[ intPosition + 2];
				else
					return '\0';
			}
		}
		/// <summary>
		/// 获得下一个指定长度的字符串
		/// </summary>
		/// <param name="iLen"></param>
		/// <returns></returns>
		public string NextString( int iLen)
		{
			if( iLen + intPosition >= intLength )
				iLen = intLength - intPosition ;
			if( iLen > 0 )
				return strText.Substring( intPosition , iLen ) ;
			else
				return null;
		}
		/// <summary>
		/// 获得当前位置前指定长度的字符串
		/// </summary>
		/// <param name="iLen"></param>
		/// <returns></returns>
		public string PreString( int iLen)
		{
			if( iLen > intPosition )
				iLen = intPosition ;
			if( iLen > 0 )
				return strText.Substring( intPosition - iLen , iLen);
			else
				return null;
		}

		/// <summary>
		/// 读取指定长度的字符串
		/// </summary>
		/// <param name="vLen"></param>
		/// <returns></returns>
		public string Read(int vLen )
		{
			if( vLen > strText.Length - intPosition )
				vLen = strText.Length - intPosition ;
			string vText = strText.Substring( intPosition , vLen );
			intPosition += vLen ;
			return vText ;
		}
		/// <summary>
		/// 读取字符串到指定的位置
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public string ReadTo( int index)
		{
			if( this.EOF )
				return null;
			if( index > intLength )
				index = intLength ;
			if( index < intPosition )
				index = intPosition ;
			if( index == intPosition )
				return null;
			string vText = strText.Substring( intPosition , index - intPosition );
			intPosition = index ;
			return vText ;
		}
		/// <summary>
		/// 返回指定位置的字符
		/// </summary>
		public char this[int index]
		{
			get{ return strText[index] ;}
		}

		/// <summary>
		/// 移动当前位置到下一个位置
		/// </summary>
		public void MoveNext()
		{
			intPosition ++ ;
		}
		public void MovePre()
		{
			intPosition -- ;
		}
		/// <summary>
		/// 根据指定的长度移动当前位置
		/// </summary>
		/// <param name="iStep"></param>
		public void MoveStep( int iStep)
		{
			intPosition += iStep ;
		}

		
		/// <summary>
		/// 移动到指定的字符上
		/// </summary>
		/// <param name="c"></param>
		public void MovePreTo( char c)
		{
			while( intPosition >= 0 )
			{
				if( strText[intPosition] == c)
					break;
				intPosition --  ;
			}
		}

		/// <summary>
		/// 移动到指定的字符上
		/// </summary>
		/// <param name="c"></param>
		public void MoveTo( char c)
		{
			while( intPosition < intLength )
			{
				if( strText[intPosition] == c)
					break;
				intPosition ++ ;
			}
		}

		/// <summary>
		/// 移动到指定的字符中
		/// </summary>
		/// <param name="CharArray"></param>
		public void MoveTo( string CharArray)
		{
			while( intPosition < intLength)
			{
				if( CharArray.IndexOf( strText[intPosition] ) >= 0 )
					break;
				intPosition ++ ;
			}
		}
		/// <summary>
		/// 移动到指定的字符后面
		/// </summary>
		/// <param name="c"></param>
		public void MoveAfter( char c)
		{
			while( intPosition < intLength )
			{
				if( strText[intPosition] == c )
				{
					intPosition ++ ;
					break;
				}
				intPosition ++ ;
			}
		}
		/// <summary>
		/// 移动到指定的字符后面
		/// </summary>
		/// <param name="CharArray"></param>
		public void MoveAfter( string CharArray)
		{
			while( intPosition < intLength)
			{
				if( CharArray.IndexOf( strText[intPosition] ) >= 0 )
				{
					intPosition ++ ;
					break;
				}
				intPosition ++ ;
			}
		}
		/// <summary>
		/// 跳到所有空白字符的后面
		/// </summary>
		public void SkipWhiteSpace()
		{
			while( intPosition < intLength )
			{
				if( ! char.IsWhiteSpace( strText[intPosition] ) )
					break;
				intPosition ++ ;
			}
		}
		/// <summary>
		/// 跳到指定字符的后面
		/// </summary>
		/// <param name="vSkipChars"></param>
		/// <returns></returns>
		public char SkipChars( string vSkipChars)
		{
			bool bolFlag = false;
			for(int iCount = intPosition ; iCount < intLength ; iCount ++)
			{
				if( vSkipChars.IndexOf( strText[iCount] ) < 0 )
				{
					intPosition = iCount ;
					break;
				}
				bolFlag = true;
			}
			if( bolFlag )
				return strText[ intPosition -1 ];
			else
				return strText[ intPosition ];
		}

		public string PeekWord( )
		{
			return PeekWord( "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789:_.-" );
		}
		public string PeekWord( string vInclude)
		{
			 
			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
			for(int iCount = intPosition ; iCount < intLength ; iCount ++ )
			{
				if( vInclude.IndexOf( strText[iCount]) >= 0 )
					myStr.Append(strText[iCount]);
				else
					break;
			}
			if( myStr.Length == 0)
				return null;
			else
				return myStr.ToString();
		}
		/// <summary>
		/// 读取一个单词
		/// </summary>
		/// <returns></returns>
		public string ReadWord( )
		{
			return ReadWord( "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789:_.-");
		}

		/// <summary>
		/// 读取一个单词,
		/// </summary>
		/// <param name="vInclude">组成单词的字符列表</param>
		/// <returns></returns>
		public string ReadWord( string vInclude)
		{
			if( this.EOF )
				return null;
			int index = intPosition ;
			string strResult = null;
			for(; index < intLength ; index ++ )
			{
				if( vInclude.IndexOf( strText[index]) < 0 )
				{
					if( index > intPosition )
					{
						strResult = strText.Substring( intPosition , index - intPosition );
						intPosition = index   ;
						break;
					}
				}
			}//for
			if( index == intLength )
			{
				strResult = strText.Substring( intPosition ) ;
				intPosition = strText.Length ;
			}
			return strResult ;
		}
		/// <summary>
		/// 读取一个分号包围的字符串
		/// </summary>
		/// <returns></returns>
		public string ReadQuotMarkText()
		{
			int StartIndex = 0;
			int EndIndex = 0 ;
			string strResult = null;
			for(int iCount = intPosition ; iCount < intLength ; iCount ++ )
			{
				char c = strText[iCount];
				if( ! char.IsWhiteSpace( c ))
				{
					StartIndex = iCount ;
					if( c == '>')
					{
						strResult = strText.Substring( StartIndex + 1 , iCount - StartIndex );
						intPosition = iCount ;
						
					}
					else if( c == '\'' || c == '\"' )
					{
						EndIndex = strText.IndexOf( c , StartIndex +1 );
						if( EndIndex < 0 )
							EndIndex = intLength ;
						strResult = strText.Substring( StartIndex + 1 , EndIndex - StartIndex - 1 );
						intPosition = EndIndex + 1 ;
					}
					else
					{
						for(int iCount2 = iCount +1 ; iCount2 < intLength ;iCount2 ++)
						{
							if( char.IsWhiteSpace( strText[iCount2]) || strText[iCount2] == '>' )
							{
								EndIndex = iCount2 ;
								break;
							}
						}
						if( EndIndex == 0 )
							EndIndex = strText.Length - 1 ;
						strResult = strText.Substring( StartIndex , EndIndex - StartIndex );
						intPosition = EndIndex ;
					}//else
					return strResult ;
				}
			}//for
			intPosition = intLength ;
			return null;
		}

		/// <summary>
		/// 读取到指定结束标签
		/// </summary>
		/// <param name="vTagName"></param>
		/// <returns></returns>
		public string ReadToEndTag( string vTagName)
		{
			string strText = this.ReadUntil( "</" + vTagName );
			this.MoveAfter('>');
			return strText ;
		}

		public string ReadUntil( string vText)
		{
			vText = vText.ToLower();
			int index = this.strLowerText.IndexOf( vText , intPosition );
			return this.ReadTo( index >= 0 ? index : intLength );
		}
		/// <summary>
		/// 读取一个字符串直到出现字符 &lt;
		/// </summary>
		/// <returns></returns>
		public string ReadString()
		{
			if( this.EOF )
				return null;
			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
			while( intPosition < this.intLength )
			{
				char c = strText[ intPosition ];
				if( c == '<' )
				{
					char c2 = this.NextChar ;
					if( c2 == 0 )
					{
						myStr.Append( c );
						intPosition ++ ;
						break;
					}
					if( c2 != '>' && c2 != '<' && char.IsWhiteSpace( c2 ) == false )
						break;
				}
				myStr.Append( c );
				intPosition ++ ;
			}
			return myStr.ToString();

//			if( this.EOF )
//				return null;
//			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
//			while( intPosition < this.intLength )
//			{
//				char c = strText[ intPosition ];
//				if( c == '<' )
//				{
//					if( intPosition + 1 < intLength )
//					{
//						if( strText[ intPosition + 1 ] == '<')
//							myStr.Append( c );
//						else
//							break;
//					}
//					else
//						break;
//				}
//				else
//					myStr.Append( c );
//				intPosition ++ ;
//			}
//			return myStr.ToString();
			
			//int index = strText.IndexOf( '<' , intPosition );
			//return this.ReadTo( index >= 0 ? index : intLength );
		}
	}//public class HTMLTextReader
}