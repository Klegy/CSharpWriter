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
	/// 文档头对象
	/// </summary>
	public class HTMLHeadElement : HTMLContainer
	{
		/// <summary>
		/// 对象标签名称,返回"head"
		/// </summary>
		public override string TagName
		{
			get{return StringConstTagName.Head ;}
		}
		/// <summary>
		/// 内部方法,本对象子标签只能为 meta,link,script,style,hta:application,#comment,xml,并且最多只能有一个title
		/// </summary>
		/// <param name="strName"></param>
		/// <returns></returns>
		internal override bool CheckChildTagName(string strName)
		{
			if( strName == StringConstAttributeName.Title )
			{
				foreach( object obj in this.myChildNodes )
					if( obj is HTMLTitleElement )
						return false;
				return true;
			}
			else
				return strName == StringConstTagName.Meta 
					|| strName == StringConstTagName.Link 
					|| strName == StringConstTagName.Script 
					|| strName == StringConstTagName.Style 
					|| strName == StringConstTagName.HTAApplication 
					|| strName == StringConstTagName.Comment 
					|| strName == StringConstTagName.XML ;
		}
		/// <summary>
		/// 处理结束标签
		/// </summary>
		/// <param name="myReader"></param>
		/// <param name="vTagName"></param>
		/// <returns></returns>
		internal override bool MeetEndTag(HTMLTextReader myReader, string vTagName)
		{
			if( vTagName == StringConstTagName.Head )
			{
				myReader.MoveAfter('>');
				return true;
			}
			else
				return false;
		}
	}//public class HTMLHeadElement : HTMLContainer
}