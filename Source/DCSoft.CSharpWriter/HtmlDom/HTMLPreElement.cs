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
	/// 文本显示区域对象
	/// </summary>
	public class HTMLPreElement : HTMLContainer
	{
		/// <summary>
		/// 对象标签名称,返回"pre"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Pre ; }
		}
		/// <summary>
		/// 对象是否不可用
		/// </summary>
		public bool Disabled
		{
			get{ return HasAttribute( StringConstAttributeName.Disabled );}
			set
			{
				if( value)
					SetAttribute( StringConstAttributeName.Disabled , "1");
				else
					RemoveAttribute( StringConstAttributeName.Disabled );
			}
		}
		/// <summary>
		/// 宽度
		/// </summary>
		public string Width
		{
			get{ return GetAttribute( StringConstAttributeName.Width );}
			set{ SetAttribute( StringConstAttributeName.Width , value);}
		}
		/// <summary>
		/// 是否换行
		/// </summary>
		public string Wrap
		{
			get{ return GetAttribute( StringConstAttributeName.Wrap );}
			set{ SetAttribute( StringConstAttributeName.Wrap , value);}
		}
	}//public class HTMLPreElement : HTMLContainer
}