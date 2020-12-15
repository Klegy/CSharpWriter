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
	/// 标题行对象
	/// </summary>
	public class HTMLHnElement : HTMLContainer
	{
		/// <summary>
		/// 标题等级 从1到6
		/// </summary>
		protected int intLevel = 1 ;
		/// <summary>
		/// 标题等级 从1到6
		/// </summary>
		public int Level
		{
			get{ return intLevel ;}
			set{ if( value >= 1 && value <= 6 ) intLevel = value;}
		}
		/// <summary>
		/// 对象标签名称,返回标题等级返回 h1,h2,h3,h4,h5或h6
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.H + intLevel ; }
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
		/// Sets or retrieves the alignment of the object relative to the display or table. 
		/// </summary>
		public string Align
		{
			get{ return base.GetAttribute( StringConstAttributeName.Align );}
			set{ base.SetAttribute( StringConstAttributeName.Align , value );}
		}
	}//public class HTMLHnElement : HTMLContainer
}