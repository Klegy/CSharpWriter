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
	/// 字体对象
	/// </summary>
	public class HTMLFontElement : HTMLContainer
	{
		/// <summary>
		/// 对象标签,返回"font"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Font ;}
		}
		/// <summary>
		/// 文字颜色
		/// </summary>
		public string Color
		{
			get{ return GetAttribute( StringConstAttributeName.Color );}
			set{ SetAttribute( StringConstAttributeName.Color , value);}
		}
		/// <summary>
		/// 对象是否可用
		/// </summary>
		public bool Disabled
		{
			get{ return GetBoolAttribute( StringConstAttributeName.Disabled );}
			set{ SetBoolAttribute( StringConstAttributeName.Disabled , value);}
		}
		/// <summary>
		/// 字体名称
		/// </summary>
		public string Face
		{
			get{ return GetAttribute( StringConstAttributeName.Face );}
			set{ SetAttribute( StringConstAttributeName.Face , value);}
		}
		/// <summary>
		/// 字体大小
		/// </summary>
		public string Size
		{
			get{ return GetAttribute( StringConstAttributeName.Size );}
			set{ SetAttribute( StringConstAttributeName.Size , value);}
		}
	}//public class HTMLFontElement : HTMLContainer
}