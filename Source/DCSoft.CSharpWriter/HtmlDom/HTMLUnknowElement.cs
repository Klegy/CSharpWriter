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
	/// 不可识别的对象
	/// </summary>
	public class HTMLUnknowElement : HTMLContainer
	{
//		public override bool Write(System.Xml.XmlWriter myWriter)
//		{
//			return base.Write (myWriter);
//		}

		private string strTagName ;
		/// <summary>
		/// 对象标签名称,根据解析结果输出对象标签名称
		/// </summary>
		public override string TagName
		{
			get{ return strTagName ;}
		}
		internal void SetTagName( string v)
		{
			strTagName = v;
		}
	}//public class HTMLUnknowElement : HTMLContainer
}