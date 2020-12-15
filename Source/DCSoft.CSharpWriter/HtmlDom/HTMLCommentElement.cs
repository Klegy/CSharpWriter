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
	/// HTML注释对象
	/// </summary>
	public class HTMLCommentElement : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回"#comment"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Comment  ; }
		}

        public override string InnerText
        {
            get
            {
                return strText;
            }
            set
            {
                strText = value;
            }
        }

		private string strText ;
		/// <summary>
		/// 注释内容
		/// </summary>
		public override string Text
		{
			get{ return strText ; }
			set{ strText = value ;}
		}
		/// <summary>
		/// 内部方法
		/// </summary>
		protected override bool HasText
		{
			get{ return true;}
		}
		/// <summary>
		/// 将注释文本填写到一个XML书写器中
		/// </summary>
		/// <remarks>
		/// 若 OwnerDocument.WriteOptions.CommentOutput设置为false则不输出注释
		/// 若设置了OwnerDocument.WriteOptions.CommentOutputXSL则对象输出为
		/// [xsl:comment]对象文本[/xsl:comment]
		/// </remarks>
		/// <param name="myWriter">XML书写器</param>
		/// <returns>操作是否成功</returns>
		public override bool Write(System.Xml.XmlWriter myWriter)
		{
			if( myOwnerDocument.WriteOptions.CommentOutput )
			{
				if( strText != null && strText.Length > 0 )
				{
					string vText = strText + " ";
					if( vText.IndexOf("--") >= 0)
					{
						vText = "因保存需要,将所有的\"- -\"转换为 \"@@\"符号\r\n" + vText.Replace("--" , "@@");
					}
					if( myOwnerDocument.WriteOptions.CommentOutputXSL )
					{
						myWriter.WriteStartElement( StringConstXSLT.Comment );
						myWriter.WriteCData( vText );
						myWriter.WriteEndElement();
					}
					else
						myWriter.WriteComment( vText );
				}
			}
			return true;
		}
	}//public class HTMLCommentElement : HTMLElement
}