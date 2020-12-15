/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System ;
using System.Collections.Generic;

namespace DCSoft.HtmlDom
{
	/// <summary>
	/// 样式表元素
	/// </summary>
	public class HTMLStyleElement : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回"style"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstAttributeName.Style ;}
		}

        public override string InnerText
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }
		private string strText ;
		/// <summary>
		/// 对象文本
		/// </summary>
		public override string Text
		{
			get{ return strText ; }
			set{ strText = value; }
		}
		/// <summary>
		/// 所有的样式对象集合
		/// </summary>
        private List<HTMLNameStyleItem> myItems = new List<HTMLNameStyleItem>();
		/// <summary>
		/// 所有的样式对象集合
		/// </summary>
        public List<HTMLNameStyleItem> Items
		{
			get{ return myItems ;}
		}
		/// <summary>
		/// 获得指定序号的样式对象
		/// </summary>
		public HTMLNameStyleItem this[int index]
		{
			get{return (HTMLNameStyleItem) myItems[index] ;}
		}
		/// <summary>
		/// 获得指定名称的样式对象,名称不区分大小写
		/// </summary>
		public HTMLNameStyleItem this[ string strName]
		{
			get
			{
				foreach( HTMLNameStyleItem myItem in myItems )
				{
					if( string.Compare( myItem.Name , strName , true ) == 0 )
						return myItem ;
				}
				return null;
			}
		}
		/// <summary>
		/// 添加样式对象
		/// </summary>
		/// <param name="item"></param>
		public void Add( HTMLNameStyleItem item )
		{
			myItems.Add( item );
		}
		/// <summary>
		/// 删除指定的样式对象
		/// </summary>
		/// <param name="item">样式对象</param>
		public void Remove( HTMLNameStyleItem item )
		{
			myItems.Remove( item );
		}
		/// <summary>
		/// 删除指定名称的样式对象
		/// </summary>
		/// <param name="name">样式名称</param>
		public void Remove( string name )
		{
			HTMLNameStyleItem item = this[ name ];
			if( item != null)
				myItems.Remove( item );
		}
		/// <summary>
		/// 内部方法
		/// </summary>
		protected override bool HasText
		{
			get{ return true; }
		}

		/// <summary>
		/// 加载对象数据
		/// </summary>
		/// <param name="myReader"></param>
		/// <returns></returns>
		internal override bool InnerRead(HTMLTextReader myReader)
		{
			myItems.Clear();
			strText = myReader.ReadToEndTag( this.TagName );
			if( strText != null)
			{
				strText = strText.Replace("<!--" , "");
				strText = strText.Replace("-->" , "");
				int index = 0 ;
				int index2 = strText.IndexOf("{");
				while( index2 >= 0 )
				{
					int index3= strText.IndexOf( "}" , index2 );
					if( index3 > index2 )
					{
						string strName = strText.Substring( index , index2 - index ) ;
						string strValue = strText.Substring( index2 + 1 , index3 - index2 - 1 );
						HTMLNameStyleItem NewItem = new HTMLNameStyleItem();
						NewItem.Name = strName.Trim();
						NewItem.CSSString = strValue ;
						myItems.Add( NewItem );
					}
					index = index3 +1 ;
					index2 = strText.IndexOf("{" , index );
				}
			}
			//myReader.MoveAfter('>');
			return true ;
		}
		/// <summary>
		/// 将对象数据输出到一个XML书写器中
		/// </summary>
		/// <param name="myWriter">XML书写器</param>
		/// <returns>操作是否成功</returns>
		protected override bool InnerWrite(System.Xml.XmlWriter myWriter)
		{
			if( myOwnerDocument.WriteOptions.FormatStyleText )
			{
				foreach( HTMLNameStyleItem myItem in myItems )
				{
					myWriter.WriteString( System.Environment.NewLine + myItem.Name + "{" );
					for(int iCount = 0 ; iCount < myItem.Attributes.Count ; iCount ++)
					{
						HTMLAttribute attr = ( HTMLAttribute ) myItem.Attributes[iCount] ;
						if( iCount > 0 )
							myWriter.WriteString(";");
						myWriter.WriteString( System.Environment.NewLine + "    " + attr.Name + ":" + attr.Value  );
					}
					if( myItem.Attributes.Count > 0 )
						myWriter.WriteString( System.Environment.NewLine + "    ");
					myWriter.WriteString("}");
				}
			}
			else
			{
				foreach( HTMLNameStyleItem myItem in myItems )
				{
					myWriter.WriteString( System.Environment.NewLine + myItem.Name + "{" + myItem.CSSString + "}" );
				}
			}
			if( myItems.Count > 0 )
				myWriter.WriteString( System.Environment.NewLine );
			return true;
		}
	}//public class HTMLStyleElement : HTMLElement
}