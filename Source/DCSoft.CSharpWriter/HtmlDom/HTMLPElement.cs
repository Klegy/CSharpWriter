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
	/// 段落对象
	/// </summary>
	public class HTMLPElement : HTMLContainer
	{
		/// <summary>
		/// 对象标签名称,返回"p"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.P ; }
		}
		/// <summary>
		/// 文本对齐方式
		/// </summary>
		public string Align
		{
			get{ return GetAttribute( StringConstAttributeName.Align );}
			set{ SetAttribute( StringConstAttributeName.Align , value);}
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
		/// 返回对象内部的文本
		/// </summary>
		/// <param name="myStr"></param>
		public override void GetDisplayText(System.Text.StringBuilder myStr)
		{
			myStr.Append( System.Environment.NewLine );
			base.GetDisplayText (myStr);
		}
		/// <summary>
		/// 该对象不必结束标签
		/// </summary>
		protected override bool MustHasEndTag
		{
			get{ return false;}
		}
		/// <summary>
		/// 开始加载标签前的操作
		/// </summary>
		/// <param name="vTagName"></param>
		/// <returns></returns>
		protected override bool BeforeLoadTag(string vTagName)
		{
			return vTagName != StringConstTagName.P ;
		}
		/// <summary>
		/// 遇到结束标签
		/// </summary>
		/// <param name="myReader">HTML文本读取器</param>
		/// <param name="vTagName">当前标签名称</param>
		/// <returns></returns>
		internal override bool MeetEndTag(HTMLTextReader myReader, string vTagName)
		{
			if( myParent != null && vTagName == myParent.TagName )
				return true;
			else
				return false;
		}
	}//public class HTMLPElement : HTMLContainer
}