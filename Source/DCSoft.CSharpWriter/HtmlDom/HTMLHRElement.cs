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
	/// 水平线元素
	/// </summary>
	public class HTMLHRElement : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回"hr"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Hr ; }
		}
		/// <summary>
		/// Sets or retrieves the alignment of the object relative to the display or table. 
		/// </summary>
		public string Align
		{
			get{ return GetAttribute( StringConstAttributeName.Align );}
			set{ SetAttribute( StringConstAttributeName.Align , value);}
		}
		/// <summary>
		/// Sets or retrieves the color to be used by the object. 
		/// </summary>
		public string Color
		{
			get{ return GetAttribute( StringConstAttributeName.Color );}
			set{ SetAttribute( StringConstAttributeName.Color , value);}
		}
		/// <summary>
		/// Sets or retrieves whether the horizontal rule is drawn with 3-D shading.
		/// </summary>
		public bool NoShade
		{
			get{ return GetBoolAttribute( StringConstAttributeName.Noshade ); }
			set{ SetBoolAttribute( StringConstAttributeName.Noshade , value );}
		}
		/// <summary>
		/// Sets or retrieves the height of the hr object.
		/// </summary>
		public string Size
		{
			get{ return GetAttribute( StringConstAttributeName.Size );}
			set{ SetAttribute( StringConstAttributeName.Size , value);}
		}
	}//public class HTMLHRElement : HTMLElement
}