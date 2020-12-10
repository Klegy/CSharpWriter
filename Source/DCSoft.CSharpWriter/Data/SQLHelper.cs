/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
//using DCSoft.Common;
using System.Collections;
using System.Collections.Specialized;
namespace DCSoft.Data
{
	/// <summary>
	/// SQLHelper 的摘要说明。
	/// </summary>
    /// <remarks>编制 袁永福</remarks>
	public sealed class SQLHelper
	{
		/// <summary>
		/// 根据字段列表输出查询SQL语句
		/// </summary>
		/// <param name="FieldNames">字段列表</param>
		/// <returns>输出的SQL语句</returns>
        public static string BuildSelectSQL(System.Collections.ICollection FieldNames)
        {
            StringCollection myFields = new StringCollection();
            StringCollection myTables = new StringCollection();
            foreach (string strFieldName in FieldNames)
            {
                if (myFields.Contains(strFieldName.Trim()) == false)
                {
                    myFields.Add(strFieldName.Trim());
                }
                int index = strFieldName.IndexOf('.');
                if (index > 0)
                {
                    string table = strFieldName.Substring(0, index).Trim();
                    if (myTables.Contains(table) == false)
                    {
                        myTables.Add(table);
                    }
                }
            }//foreach
            //myFields.RemoveBlankString();
            //myTables.RemoveBlankString();
            System.Text.StringBuilder myStr = new System.Text.StringBuilder();
            myStr.Append(" Select " + Environment.NewLine);
            for (int iCount = 0; iCount < myFields.Count; iCount++)
            {
                string item = (string)myFields[iCount];
                if (item.Length > 0)
                {
                    myStr.Append("     " + item );
                }
                if (iCount < myFields.Count - 1)
                {
                    myStr.Append(" ," + Environment.NewLine);
                }
            }
            myStr.Append( Environment.NewLine + " From" + Environment.NewLine);
            for (int iCount = 0; iCount < myTables.Count; iCount++)
            {
                string item = (string)myTables[iCount];
                if (item.Length > 0)
                {
                    myStr.Append("     " + item);
                }
                if (iCount < myTables.Count - 1)
                {
                    myStr.Append(" ," + Environment.NewLine);
                }
            }
            //myStr.Append( myTables.ToString( "    " , " , \r\n"));
            return myStr.ToString();
        }

        //public static string BuildUpdateSQL(System.Collections.ICollection FieldNames)
        //{
        //    SingleStringCollection myFields = new SingleStringCollection();
        //    SingleStringCollection myTables = new SingleStringCollection();
        //    foreach (string strFieldName in FieldNames)
        //    {
        //        myFields.Add(strFieldName);
        //        int index = strFieldName.IndexOf('.');
        //        if (index > 0)
        //        {
        //            myTables.Add(strFieldName.Substring(0, index).Trim());
        //        }
        //    }//foreach
        //    myFields.RemoveBlankString();
        //    myTables.RemoveBlankString();
        //    if (myTables.Count != 1)
        //    {
        //        return null;
        //    }
        //    System.Text.StringBuilder myStr = new System.Text.StringBuilder();
        //    myStr.Append("Update " + myTables[0] + " Set ");
        //    foreach (string strFieldName in myFields )
        //    {
        //        if (myStr.Length > 0)
        //        {
        //            myStr.Append(" , \r\n");
        //        }
        //        myStr.Append("    " + strFieldName 
        //    }
        //    myStr.Append(" Select \r\n");
        //    myStr.Append(myFields.ToString("    ", " , \r\n"));
        //    myStr.Append("\r\n From \r\n");
        //    myStr.Append(myTables.ToString("    ", " , \r\n"));
        //    return myStr.ToString();
        //}

		public static bool MatchFieldName( string name1 , string name2 )
		{
			if( name1 == null || name2 == null )
				return false;
			if( string.Compare( name1 , name2 , true ) == 0 )
				return true ;
			int index = name1.IndexOf('.');
			if( index > 0 )
			{
				name1 = name1.Substring( index + 1 ).Trim();
				if( name1.Length == 0 )
					return false;
			}

			index = name2.IndexOf('.');
			if( index > 0 )
			{
				name2 = name2.Substring( index + 1 ).Trim();
				if( name2.Length == 0 )
					return false;
			}

			if( string.Compare( name1 , name2 , true ) == 0 )
				return true ;

			return false ;
		}

		/// <summary>
		/// 获得字段名在字段名称列表中的从0开始的序号
		/// </summary>
		/// <param name="FieldNames">字段名称列表</param>
		/// <param name="FieldName">指定的字段名称</param>
		/// <returns>从0开始的序号,若未找到则返回-1</returns>
		public static int FieldIndexOf( System.Collections.ICollection FieldNames , string FieldName )
		{
			if( FieldName == null || FieldNames == null )
				return -1;
			FieldName = FieldName.Trim();
			if( FieldName.Length == 0 )
				return -1;

            int index = 0 ;
			
            bool isInteger = true;
            foreach (char c in FieldName)
            {
                if ("0123456789".IndexOf(c) < 0)
                {
                    isInteger = false;
                    break;
                }
            }
            if (isInteger)
            {
                // 若字段名全是数字内容则认为是直接指明字段序号
                index = Convert.ToInt32(FieldName);
                if (index >= 0 && index < FieldNames.Count)
                    return index;
                else
                    return -1;
            }

            foreach( string vName in FieldNames )
			{
				if( string.Compare( vName , FieldName , true ) == 0 )
					return index ;
				index ++ ;
			}
			index = FieldName.IndexOf('.');
			if( index > 0 )
				FieldName = FieldName.Substring( index + 1 ).Trim() ;
			if( FieldName.Length == 0 )
				return -1;
			index = 0 ;
			foreach( string vName in FieldNames )
			{
				if( string.Compare( vName , FieldName  , true ) == 0 )
					return index ;
				int index2 = vName.IndexOf('.');
				if( index2 >= 0 )
				{
					string name2 = vName.Substring( index2 + 1 ).Trim() ;
					if( string.Compare( name2 , FieldName , true ) == 0 )
						return index ;
				}
				index ++ ;
			}
			return -1;
		}

		private SQLHelper(){}
	}
}
