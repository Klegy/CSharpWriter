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
	/// 下标对象
	/// </summary>
	public class HTMLSubElement : HTMLContainer 
	{
		/// <summary>
		/// 对象标签名称,返回"sub"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Sub ;}
		}
		/// <summary>
		/// 对象是否不可用
		/// </summary>
		public bool Disabled
		{
			get{ return GetBoolAttribute( StringConstAttributeName.Disabled );}
			set{ SetBoolAttribute( StringConstAttributeName.Disabled , value);}
		}
	}//public class HTMLSubElement : HTMLContainer 
}