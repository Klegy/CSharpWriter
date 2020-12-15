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
	/// UL对象
	/// </summary>
	public class HTMLULElement : HTMLContainer
	{
		/// <summary>
		/// 对象标签名称,返回"ul"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.UL ;}
		}
		/// <summary>
		/// 对象是否不可用
		/// </summary>
		public bool Disabled
		{
			get{ return GetBoolAttribute( StringConstAttributeName.Disabled );}
			set{ SetBoolAttribute( StringConstAttributeName.Disabled ,value);}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public string Title
		{
			get{ return GetAttribute( StringConstAttributeName.Title );}
			set{ SetAttribute( StringConstAttributeName.Title , value );}
		}
		/// <summary>
		/// 类型
		/// </summary>
		public string Type
		{
			get{ return GetAttribute( StringConstAttributeName.Type );}
			set{ SetAttribute( StringConstAttributeName.Type , value );}
		}
		/// <summary>
		/// 已重载:UL对象只能接收LI子对象
		/// </summary>
		/// <param name="strName"></param>
		/// <returns></returns>
		internal override bool CheckChildTagName(string strName)
		{
			return strName == StringConstTagName.LI ;
		}
	}
}