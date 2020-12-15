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
	/// li 对象(Denotes one item in a list.)
	/// </summary>
	public class HTMLLIElement : HTMLContainer
	{
		/// <summary>
		/// 对象标签名称,返回"li"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.LI ; }
		}
		/// <summary>
		/// 对象是否可用
		/// </summary>
		public bool Disabled
		{
			get{ return GetBoolAttribute( StringConstAttributeName.Disabled );}
			set{ SetBoolAttribute( StringConstAttributeName.Disabled ,value);}
		}
		/// <summary>
		/// Sets or retrieves advisory information (a ToolTip) for the object. 
		/// </summary>
		public string Title
		{
			get{ return GetAttribute( StringConstAttributeName.Title );}
			set{ SetAttribute( StringConstAttributeName.Title , value );}
		}
		/// <summary>
		/// Sets or retrieves the style of the list.
		/// </summary>
		public string Type
		{
			get{ return GetAttribute( StringConstAttributeName.Type );}
			set{ SetAttribute( StringConstAttributeName.Type , value );}
		}
		/// <summary>
		/// 加载标签前的处理
		/// </summary>
		/// <param name="vTagName"></param>
		/// <returns></returns>
		protected override bool BeforeLoadTag(string vTagName)
		{
			return vTagName != StringConstTagName.LI ;
		}
	}//public class HTMLLIElement : HTMLContainer
}