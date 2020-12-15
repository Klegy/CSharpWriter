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
	/// XML数据岛对象
	/// </summary>
	public class HTMLXMLElement : HTMLElement
	{
		/// <summary>
		/// 加载的XML文档对象
		/// </summary>
		protected System.Xml.XmlDocument myXMLDocument = new System.Xml.XmlDocument();
		/// <summary>
		/// 对象标签名称,返回"xml"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.XML  ;}
		}
		/// <summary>
		/// XML来源
		/// </summary>
		public string Src
		{
			get{ return base.GetAttribute( StringConstAttributeName.Src );}
			set{ base.SetAttribute( StringConstAttributeName.Src , value);}
		}
		/// <summary>
		/// 加载成功的XML文档对象
		/// </summary>
		public System.Xml.XmlDocument XMLDocument
		{
			get{ return myXMLDocument ;}
			set{ myXMLDocument = value;}
		}
		/// <summary>
		/// 加载的原始XML字符串
		/// </summary>
		protected string strSourceXML = null;
		/// <summary>
		/// 加载的原始XML字符串
		/// </summary>
		public string SourceXML
		{
			get{ return strSourceXML ;}
			set{ strSourceXML = value;}
		}
		/// <summary>
		/// 加载的错误信息
		/// </summary>
		protected string strLoadErrorMsg = null;

		/// <summary>
		/// 从HTML读取器中加载对象数据
		/// </summary>
		/// <param name="myReader"></param>
		/// <returns></returns>
		internal override bool InnerRead(HTMLTextReader myReader)
		{
			strLoadErrorMsg = null;
			strSourceXML = myReader.ReadToEndTag( this.TagName );
			try
			{
				myXMLDocument.RemoveAll();
				System.Xml.XmlNamespaceManager nsm = new System.Xml.XmlNamespaceManager( myXMLDocument.NameTable );
				foreach( HTMLAttribute attr in myOwnerDocument.Attributes )
				{
					string vName = attr.Name;
					if( vName.ToLower().StartsWith( StringConstAttributeName.XMLNS ))
					{
						int index = vName.IndexOf(":");
						if(index > 0 )
						{
							string NsName = vName.Substring( index + 1 ) ;
							nsm.AddNamespace( NsName , attr.Value );
						}
					}
				}
				System.Xml.XmlParserContext pc = new System.Xml.XmlParserContext( myXMLDocument.NameTable , nsm , null , System.Xml.XmlSpace.None );
				System.Xml.XmlTextReader myXMLReader = new System.Xml.XmlTextReader( strSourceXML , System.Xml.XmlNodeType.Element , pc );
				myXMLDocument.Load( myXMLReader );
				myXMLReader.Close();
			}
			catch(Exception ext)
			{
				myXMLDocument.RemoveAll();
				strLoadErrorMsg = "加载XML数据岛信息错误 - "  + ext.Message ;
			}
			return true;
		}

		/// <summary>
		/// 保存对象数据到XML书写器中
		/// </summary>
		/// <param name="myWriter">XML书写器</param>
		/// <returns>操作是否成功</returns>
		protected override bool InnerWrite(System.Xml.XmlWriter myWriter)
		{
			if( myXMLDocument.DocumentElement == null)
				myWriter.WriteString( this.strLoadErrorMsg );
			else
				myXMLDocument.WriteContentTo( myWriter );
			return true;
		}
	}//public class HTMLXMLElement : HTMLElement
}