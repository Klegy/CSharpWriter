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
	/// 纯文本节点对象
	/// </summary>
	public class HTMLTextNodeElement : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回"#text"
		/// </summary>
		public override string TagName
		{
			get{ return  StringConstTagName.TextNode ;}
		}
		private string strText ;
		/// <summary>
		/// 对象文本
		/// </summary>
		public override string  Text
		{
			get{ return strText ;}
			set{ strText = value;}
		}
	
		/// <summary>
		/// 对象文本
		/// </summary>
		public override string InnerText
		{
			get
			{
				return strText ;
			}
		}

		/// <summary>
		/// 输出对象数据到一个XML书写器中
		/// </summary>
		/// <remarks>
		/// 若设置了OwnerDocument.WriteOptions.WriteWhitespace则输出两边的空白字符,否则不会输出两边的空白字符
		/// 若设置了OwnerDocument.WriteOptions.NormalizeSpace则对输出的字符进行 normalizespace 处理
		/// 若设置了OwnerDocument.WriteOptions.AddSpan且父标签还有其他元素则在外套上 span 标签
		/// 若设置了OwnerDocument.WriteOptions.TextOutPutXSL 则将所有限定标记组合(默认为"[]")包围的部分进行XSL输出
		/// OwnerDocument.WriteOptions.TextFieldPrefix和TextFieldEndfix指定了限定标记组合
		/// 例如 "aaa[bbb]ccc" 输出为 "aaa＜xsl:vlaue-of select="bbb" /＞ccc"
		/// </remarks>
		/// <param name="myWriter">XML书写器</param>
		/// <returns>操作是否成功</returns>
		public override bool Write(System.Xml.XmlWriter myWriter)
		{
			if( HTMLTextReader.HasContent( strText ))
			{
				string vText ;
				if( myOwnerDocument.WriteOptions.WriteWhitespace )
					vText = strText ;
				else
					vText = strText.Trim() ;
				if( myOwnerDocument.WriteOptions.NormalizeSpace )
					vText = HTMLTextReader.NormalizeSpace( vText );
				bool bolAddSpan = ( myOwnerDocument.WriteOptions.AddSpan && ( myParent == null || myParent.ChildNodes.Count != 1));
				if( myOwnerDocument.WriteOptions.TextOutPutXSL )
				{
					string[] strItems = HTMLTextReader.SplitVariableString( vText , myOwnerDocument.WriteOptions.TextFieldPrefix , myOwnerDocument.WriteOptions.TextFieldEndfix );
					if( bolAddSpan )
						myWriter.WriteStartElement( StringConstTagName.Span );
					for(int iCount = 0 ; iCount < strItems.Length ; iCount ++ )
					{
						string strItem = strItems[iCount];
						if( strItem != null )
						{
							if( (iCount % 2 ) == 1 && strItem != "")
							{
								if( strItem.IndexOf("\r\n") >= 0 )
									strItem = strItem.Replace("\r\n" , "");
								if( strItem.StartsWith("num:"))
								{
									strItem = strItem.Substring( 4 );
									myWriter.WriteStartElement( StringConstXSLT.IF );
									myWriter.WriteAttributeString( StringConstXSLT.Test , strItem + "!='0.0000'");
									myWriter.WriteStartElement( StringConstXSLT.Value_of );
									myWriter.WriteAttributeString( StringConstXSLT.Select , myOwnerDocument.WriteOptions.FixXSLSource( strItem));
									myWriter.WriteEndElement();
									myWriter.WriteEndElement();
								}
								else if( strItem.StartsWith("%:"))
								{
									strItem = strItem.Substring(2);
									myWriter.WriteStartElement( StringConstXSLT.Value_of );
									myWriter.WriteAttributeString( StringConstXSLT.Select , "format-number(" + myOwnerDocument.WriteOptions.FixXSLSource( strItem ) + " , '0.00')" );
									myWriter.WriteEndElement();
								}
								else if( strItem.StartsWith("call:"))
								{
									strItem = strItem.Substring( 5 );
									myWriter.WriteStartElement( StringConstXSLT.Calltemplate );
									myWriter.WriteAttributeString( StringConstXSLT.Name , strItem );
									myWriter.WriteEndElement();
								}
								else
								{
									myWriter.WriteStartElement( StringConstXSLT.Value_of );
									myWriter.WriteAttributeString( StringConstXSLT.Select , myOwnerDocument.WriteOptions.FixXSLSource( strItem));
									myWriter.WriteEndElement();
								}
							}
							else
							{
								int index = 0 ;
								int index2 = strItem.IndexOf(StringConstEntity.WhiteSpace );
								if( index2 >= 0 )
								{
									while( index2 >= 0 )
									{
										if( index2 > index )
											myWriter.WriteString( strItem.Substring( index , index2 - index ));
										myWriter.WriteStartElement( StringConstXSLT.Text );
										myWriter.WriteAttributeString( StringConstXSLT.Disable_output_escaping  , StringConstValue._Yes );
										myWriter.WriteString( StringConstEntity.WhiteSpace );
										myWriter.WriteEndElement();

										index = index2 + 6 ;
										index2 = strItem.IndexOf(StringConstEntity.WhiteSpace , index );
									}
									if( index < strItem.Length )
										myWriter.WriteString( strItem.Substring( index ));
								}
								else
									myWriter.WriteString( strItem );
							}
						}
					}//for
					if( bolAddSpan )
						myWriter.WriteEndElement();
				}
				else
				{
					if( bolAddSpan)
						myWriter.WriteElementString( StringConstTagName.Span , vText );
					else
						myWriter.WriteString( vText );
				}
			}
			return true;
		}

		internal override bool Read(HTMLTextReader myReader)
		{
			strText = myReader.ReadString();
			return true ;
		}
	}//public class HTMLTextNodeElement : HTMLElement
}