/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using DCSoft.Common ;
using DCSoft.Data ;
using System.Text;
using System.Data;

namespace DCSoft.DataSourceDom
{
	/// <summary>
	/// DataSourceUtil 的摘要说明。
	/// </summary>
	public sealed class DataSourceUtil
	{
        //public static string ValidateDataSourceDocument(DataSourceDocument document)
        //{
        //}

        //private static void ValidateDataSourceElement(DataSourceElement rootElement, StringBuilder output)
        //{
        //    if (System.Xml.XmlReader.IsName(rootElement.Name) == false)
        //    {
        //        output.AppendLine(string.Format(DataSourceStrings.InvalidateNodeName_Path, rootElement.FullPath));
        //    }
        //    if (DataSourceUtil.HasContent(rootElement.FieldName) == false && rootElement.Parent is DataSourceElement)
        //    {
        //        output.AppendLine( string.Format( DataSourceStrings.InvalidateRequireFieldName_Path , rootElement.FullPath ));
        //    }
        //    //foreach( 
        //}

        public static string[] ValueToStrings(object[] Values)
        {
            if (Values == null)
                return null;
            string[] texts = new string[Values.Length];
            for (int iCount = 0; iCount < Values.Length; iCount++)
            {
                object v = Values[iCount];
                if (v == null || DBNull.Value.Equals(v))
                    texts[iCount] = null;
                else
                    texts[iCount] = Convert.ToString(v);
            }
            return texts;
        }

        public static int IndexOfColumn(System.Data.IDataReader reader, string FieldName)
        {
            if (FieldName == null)
                return -1;
            FieldName = FieldName.Trim();
            if (FieldName.Length == 0)
                return -1;
            for (int iCount = 0; iCount < reader.FieldCount; iCount++)
            {
                string name = reader.GetName(iCount);
                if (SQLHelper.MatchFieldName(FieldName, name))
                {
                    return iCount;
                }
            }
            if (StringConvertHelper.IsInteger(FieldName))
                return StringConvertHelper.ToInt32Value(FieldName, -1);
            return -1;
        }

        public static int IndexOfColumn2(System.Data.DataTable table, string FieldName)
		{
			if( FieldName == null )
				return -1;
			FieldName = FieldName.Trim();
			if( FieldName.Length == 0 )
				return -1;
			foreach( System.Data.DataColumn col in table.Columns )
			{
				if( string.Compare( col.ColumnName , FieldName , true ) == 0 )
				{
					return table.Columns.IndexOf( col );
				}
			}
			return -1 ;
		}
		public static int IndexOfColumn( System.Data.DataTable table , string FieldName )
		{
			System.Collections.ArrayList names = new System.Collections.ArrayList();
			foreach( System.Data.DataColumn col in table.Columns )
			{
				names.Add( col.ColumnName );
			}
			return SQLHelper.FieldIndexOf( names , FieldName );
		}

		public static System.Data.DataRow[] CrossDataTable( 
			System.Data.DataTable table , 
			System.Data.DataRow[] rows ,
			string RowFieldName ,
			string ColFieldName )
		{
			int index1 = IndexOfColumn( table , RowFieldName );
			if( index1 < 0 )
			{
				throw new ArgumentException("不存在栏目 " + RowFieldName );
			}
			int index2 = IndexOfColumn( table , ColFieldName );
			if( index2 < 0 )
			{
				throw new ArgumentException("不存在栏目 " + ColFieldName );
			}
			return CrossDataTable(
				table ,
				rows ,
				index1 ,
				index2 );
		}

//		public static System.Data.DataRow[] CrossDataTable2( 
//			System.Data.DataTable table ,
//			System.Data.DataRow[] rows ,
//			int RowFieldIndex ,
//			int ColFieldIndex )
//		{
//			System.Collections.ArrayList RowValues = new System.Collections.ArrayList();
//			System.Collections.ArrayList ColValues = new System.Collections.ArrayList();
//			System.Data.DataColumn rc = table.Columns[ RowFieldIndex ] ;
//			System.Data.DataColumn cc = table.Columns[ ColFieldIndex ] ;
//			System.Collections.ArrayList Keys = new System.Collections.ArrayList();
//			foreach( System.Data.DataRow row in rows )
//			{
//				object rv = row[ rc ] ;
//				if( rv == null || DBNull.Value.Equals( rv ))
//					continue ;
//				object cv = row[ cc ] ;
//				if( cv == null || DBNull.Value.Equals( cv ))
//					continue ;
//				bool find = false;
//				int len = Keys.Count ;
//				if( len > 0 )
//				{
//					for( int iCount = 0 ; iCount < len ; iCount += 3 )
//					{
//						if( rv.Equals( Keys[ iCount ] )
//							&& cv.Equals( Keys[ iCount + 1 ] ))
//						{
//							System.Collections.ArrayList list = Keys[ iCount + 2 ] as System.Collections.ArrayList ;
//							if( list == null )
//							{
//								list = new System.Collections.ArrayList();
//								list.Add( Keys[ iCount + 2 ] );
//								Keys[ iCount + 2 ] = list ;
//							}
//							else
//							{
//								list.Add( row );
//							}
//							find = true ;
//						}
//					}
//				}
//				if( find == false )
//				{
//					Keys.Add( rv );
//					Keys.Add( cv );
//					Keys.Add( row );
//				}
//				bool find = false;
//				int len = RowValues.Count ;
//				if( len > 0 )
//				{
//					for( int iCount = 0 ; iCount < len ; iCount += 2 )
//					{
//						object v = RowValues[ iCount ] ;
//						if( rv.Equals( v ))
//						{
//							System.Collections.ArrayList list = RowValues[ iCount + 1 ] ;
//							if( list == null )
//							{
//								list = new System.Collections.ArrayList();
//								list.Add( RowValues[ iCount + 1 ] );
//								RowValues[ iCount + 1 ] = list ;
//							}
//							list.Add( row );
//							find = true ;
//							break;
//						}
//					}
//				}
//				if( find == false )
//				{
//					RowValues.Add( rv );
//					RowValues.Add( row );
//				}
//				
//				find = false;
//				foreach( object v in ColValues )
//				{
//					if( cv.Equals( v ))
//					{
//						find = true ;
//						break;
//					}
//				}
//				if( find == false )
//				{
//					ColValues.Add( cv );
//				}
//			}//foreach
//			System.Collections.ArrayList result = new System.Collections.ArrayList();
//			int len2 = RowValues.Count ;
//			if( len2 > 0 )
//			{
//				for( int iCount = 0 ; iCount < len2 ; iCount += 2 )
//				{
//					object rv = RowValues[ iCount ] ;
//					object v = RowValues[ iCount + 1 ] ;
//					byte[] flag = new byte[ ColValues ];
//					System.Collections.ArrayList list = new System.Collections.ArrayList();
//					if( v is System.Data.DataRow )
//					{
//						list.Add( v );
//					}
//					else
//					{
//						list.AddRange( ( System.Collections.ArrayList ) v );
//					}
//					
//				}
//			}
//			return null ;
//		}
		public static System.Data.DataRow[] CrossDataTable(
			System.Data.DataTable table ,
			System.Data.DataRow[] rows ,
			int RowFieldIndex ,
			int ColFieldIndex )
		{
			System.Collections.ArrayList result = new System.Collections.ArrayList();
			
			object[] RowValues = GetGroupValues( table , rows , RowFieldIndex );
			object[] ColValues = GetGroupValues( table , rows , ColFieldIndex );
			System.Collections.ArrayList myRows = new System.Collections.ArrayList( rows );
			foreach( object RowValue in RowValues )
			{
				foreach( object ColValue in ColValues )
				{
					bool flag = false;
					for( int iCount = 0 ; iCount < myRows.Count ; iCount ++ )
					{
						System.Data.DataRow row = ( System.Data.DataRow ) myRows[ iCount ] ;
						object rv = row[ RowFieldIndex ] ;
						object cv = row[ ColFieldIndex ] ;
						if( EqualsValue( RowValue , rv ) && EqualsValue( ColValue , cv ))
						{
							result.Add( row );
							myRows.RemoveAt( iCount );
							iCount -- ;
							flag = true ;
						}
					}
					if( flag == false )
					{
						System.Data.DataRow NewRow = table.NewRow();
						NewRow[ RowFieldIndex ] = RowValue ;
						NewRow[ ColFieldIndex ] = ColValue ;
						result.Add( NewRow );
					}
				}
			}
			return ( System.Data.DataRow[] ) result.ToArray( typeof( System.Data.DataRow ));
		}

		public static object[] GetGroupValues( System.Data.DataTable table , System.Data.DataRow[] rows , int FieldIndex )
		{
			System.Collections.ArrayList list = new System.Collections.ArrayList();
			foreach( System.Data.DataRow row in rows )
			{
				object key = row[ FieldIndex ] ;
				bool find = false;
				foreach( object v in list )
				{
					if( EqualsValue( key , v ))
					{
						find = true ;
						break;
					}
				}
				if( find == false )
				{
					list.Add( key );
				}
			}
			return list.ToArray();
		}

		public static bool EqualsValue( object v1 , object v2 )
		{
			if( v1 == v2 )
				return true ;
			if( v1 != null )
			{
				return v1.Equals( v2 );
			}
			if( v2 != null )
			{
				return v2.Equals( v1 );
			}
			return false;
		}

//		/// <summary>
//		/// 对一个数据表针对指定的字段进行分组
//		/// </summary>
//		/// <param name="table">数据表对象</param>
//		/// <param name="Rows">参与分组的数据行对象列表</param>
//		/// <param name="FieldIndex">字段序号</param>
//		/// <returns>分组结果</returns>
//		public static System.Collections.Hashtable GroupDataTable( System.Data.DataTable table , System.Collections.IEnumerable Rows , int FieldIndex )
//		{
//			if( Rows == null )
//			{
//				Rows = table.Rows ;
//			}
//			System.Collections.Hashtable result = new System.Collections.Hashtable();
//			foreach( System.Data.DataRow row in Rows )
//			{
//				object key = row[ FieldIndex ] ;
//				System.Collections.ArrayList list = result[ key ] as System.Collections.ArrayList ;
//				if( list == null )
//				{
//					list = new System.Collections.ArrayList();
//					result[ key ] = list ;
//				}
//				list.Add( row );
//			}
//			foreach( object key in result.Keys )
//			{
//				System.Collections.ArrayList list = ( System.Collections.ArrayList ) result[ key ]  ;
//				result[ key ] = list.ToArray( typeof( System.Data.DataRow ));
//			}
//			return result ;
//		}

		public static string FixDataSourceName ( string vName )
		{
			if( System.Xml.XmlReader.IsName( vName ))
				return vName ;
			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
			foreach( char c in vName )
			{
				if( System.Xml.XmlReader.IsName( c.ToString()))
					myStr.Append( c );
				else
					myStr.Append("_");
			}
			return myStr.ToString();
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
        /// 根据对象文本获得该对象绑定的字段名
        /// </summary>
        /// <param name="text">对象文本</param>
        /// <returns>获得的绑定的第一个字段名</returns>
        public static string GetBindingFieldName(string text)
        {
            string[] items = VariableString.AnalyseVariableString(text, "[", "]");
            if (items == null || items.Length <= 1)
                return null;
            for (int iCount = 0; iCount < items.Length; iCount++)
            {
                if ((iCount % 2) == 1)
                {
                    string name = items[iCount];
                    if (System.Xml.XmlReader.IsName(name))
                    {
                        return name;
                    }
                }
            }//for
            return null;
        }

        public static bool IsInBinding(string txt, int index)
		{
			if( txt == null )
				return false;
			if( index <= 0 || index > txt.Length - 1 )
				return false;

			bool start = false;
			for( int iCount = index -1 ; iCount >= 0 ; iCount -- )
			{
				char c = txt[ iCount ] ;
				if( c == ']' || c == '[' )
				{
					if( c == '[' )
						start = true;
					break;
				}
			}
			
			if( start == false )
				return false;

			bool end = false;
			for( int iCount = index ; iCount < txt.Length ; iCount ++ )
			{
				char c = txt[ iCount ] ;
				if( c == ']' || c == '[' )
				{
					if( c == ']' )
						end = true;
					break;
				}
			}

			return end ;
		}

		public static string[] GetVariableNames( string strText )
		{
			VariableString str = new VariableString( strText );
			return str.GetVariableNames();
		}

		public static string ExecuteVariableString( string strText , XParameterList vars )
		{
			VariableString str = new VariableString( strText );
			str.Variables = new DataSourceParameterVariableProvider( vars );
			return str.Execute();
		}

        public static string ExecuteVariableString(
            string strCommandText,
            System.Collections.ArrayList ParameterValues,
            IVariableProvider vars)
        {
            VariableString str = new VariableString(strCommandText);
            str.Variables = vars;
            return str.Execute(ParameterValues);
        }


        public static string ExecuteVariableString(
			string strCommandText , 
			System.Collections.ArrayList ParameterValues ,
			IVariableProvider vars ,
            ParameterStyle parameterStyle )
		{
			VariableString str = new VariableString( strCommandText );
			str.Variables = vars ;
            switch (parameterStyle)
            {
                case ParameterStyle.Default :
                    str.NamedParameter = true;
                    str.ParameterNamePrefix = "@";
                    return str.Execute( );
                case ParameterStyle.Macro :
                    str.NamedParameter = false ;
                    return str.Execute();
                case ParameterStyle.Anonymous :
                    str.ParameterNamePrefix = null;
                    str.NamedParameter = false;
                    return str.Execute(ParameterValues);
                case ParameterStyle.OracleStyle :
                    str.NamedParameter = true;
                    str.ParameterNamePrefix = ":";
                    return str.Execute(ParameterValues);
                case ParameterStyle.SQLServerStyle :
                    str.NamedParameter = true;
                    str.ParameterNamePrefix = "@";
                    return str.Execute(ParameterValues);
            }
            throw new ArgumentException("parameterStyle");
		}

        public static void DebugPrintCommandInfo( string source , System.Data.IDbCommand command )
        {
            System.Diagnostics.Debug.WriteLine( source + ":" + command.CommandText);
            foreach (IDataParameter p in command.Parameters)
            {
                System.Diagnostics.Debug.WriteLine(p.ParameterName + "=\"" + p.Value + "\"");
            }
        }

        public static void AppendParameterSQL(
            StringBuilder mySQL,
            string fieldName, 
            object Value , 
            ParameterStyle parameterStyle )
        {
            switch (parameterStyle)
            {
                case ParameterStyle.Anonymous:
                    mySQL.Append(" ? ");
                    break;
                case ParameterStyle.Default:
                    mySQL.Append(" ? ");
                    break;
                case ParameterStyle.Macro:
                    if (Value == null || DBNull.Value.Equals(Value))
                    {
                        mySQL.Append(" null ");
                    }
                    else if (Value is string)
                    {
                        string str = (string)Value;
                        if (str.Length == 0)
                        {
                            mySQL.Append(" null ");
                        }
                        else
                        {
                            if (str.IndexOf("'") >= 0)
                            {
                                str = str.Replace("'", "''");
                            }
                            mySQL.Append("'" + str + "'");
                        }
                    }
                    else if (Value is byte[])
                    {
                        byte[] bs = (byte[])Value;
                        if (bs.Length == 0)
                        {
                            mySQL.Append(" null ");
                        }
                        else
                        {
                            char[] hexs = "0123456789ABCDEF".ToCharArray();
                            mySQL.Append("0x");
                            for (int iCount = 0; iCount < bs.Length; iCount++)
                            {
                                byte b = bs[iCount];
                                mySQL.Append(hexs[b >> 4]);
                                mySQL.Append(hexs[b & 15]);
                            }
                        }
                    }
                    else if (Value is DateTime)
                    {
                        mySQL.Append("'" + Value.ToString() + "'");
                    }
                    else
                    {
                        mySQL.Append(Value.ToString());
                    }
                    break;
                case ParameterStyle.OracleStyle:
                    mySQL.Append(":" + fieldName);
                    break;
                case ParameterStyle.SQLServerStyle:
                    mySQL.Append("@" + fieldName);
                    break;
                default:
                    mySQL.Append("@" + fieldName);
                    break;
            }//switch
        }

		private DataSourceUtil()
		{
		}
	}
}
