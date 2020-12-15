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
	/// 更换HTML文档的字符编码格式的异常对象
	/// </summary>
	public class HTMLChangeCharSetException : System.Exception
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="txt"></param>
		public HTMLChangeCharSetException( string txt ) : base( txt )
		{
		}
	}

	/// <summary>
	/// Meta元素(Conveys hidden information about the document to the server and the client.)
	/// </summary>
	public class HTMLMetaElement : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回"meta"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Meta ;}
		}
		/// <summary>
		/// Sets or retrieves meta-information to associate with httpEquiv or name.
		/// </summary>
		public string Content
		{
			get{ return GetAttribute( StringConstAttributeName.Content );}
			set{ SetAttribute( StringConstAttributeName.Content , value);}
		}
		/// <summary>
		/// Sets or retrieves information used to bind the META tag's content to an HTTP response header.
		/// </summary>
		public string HttpEquiv
		{
			get{ return GetAttribute( StringConstAttributeName.HttpEquiv );}
			set{ SetAttribute( StringConstAttributeName.HttpEquiv , value);}
		}
		/// <summary>
		/// Sets or retrieves a scheme to be used in interpreting the value of a property specified for the object.
		/// </summary>
		public string Scheme
		{
			get{ return GetAttribute( StringConstAttributeName.Scheme );}
			set{ SetAttribute( StringConstAttributeName.Scheme , value);}
		}

		internal override bool Read(HTMLTextReader myReader)
		{
			if(  base.Read (myReader) )
			{
                if (this.HasAttribute("charset"))
                {
                    string v = this.GetAttribute("charset");
                    if (string.IsNullOrEmpty(this.OwnerDocument.CharSet) == false)
                    {
                        if (string.Compare(v, this.OwnerDocument.CharSet, true) != 0)
                        {
                            this.OwnerDocument.CharSet = v;
                            throw new HTMLChangeCharSetException( v );
                        }
                    }
                }
				if( this.HasAttribute("content"))
				{
					System.Collections.Specialized.NameValueCollection myValues = HTMLTextReader.AnalyseValueString( this.GetAttribute("content"));
					foreach( string strKey in myValues.Keys )
					{
						if( string.Compare( strKey , "charset" , true ) == 0 )
						{
							string strValue = myValues[ strKey ];
							if( strValue != null )
							{
								strValue = strValue.Trim() ;
								if( myOwnerDocument.CharSet != null )
								{
									if( string.Compare( strValue , myOwnerDocument.CharSet , true ) != 0 )
									{
										myOwnerDocument.CharSet = strValue ;
										throw new HTMLChangeCharSetException( strValue );
									}
								}
							}
						}
					}
				}
				return true;
			}
			else
			{
				return false;
			}
		}
	}//public class HTMLMetaElement : HTMLElement
}