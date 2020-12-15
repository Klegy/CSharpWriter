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
	/// HTML文档体对象(Specifies the beginning and end of the document body.)
	/// </summary>
	public class HTMLBodyElement : HTMLContainer
	{
		/// <summary>
		/// 对象标签名称, 返回 "body"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Body ;}
		}
		/// <summary>
		/// Sets or retrieves the color of all active links in the element.
		/// </summary>
		public string ALink
		{
			get{ return GetAttribute( StringConstAttributeName.ALink );}
			set{ SetAttribute( StringConstAttributeName.ALink , value);}
		}
		/// <summary>
		/// Sets or retrieves the background picture tiled behind the text and graphics on the page. 
		/// </summary>
		public string BackGround
		{
			get{ return GetAttribute( StringConstAttributeName.BackGround );}
			set{ SetAttribute( StringConstAttributeName.BackGround , value);}
		}
		/// <summary>
		/// Deprecated. Sets or retrieves the background color behind the object. 
		/// </summary>
		public string BgColor
		{
			get{ return GetAttribute( StringConstAttributeName.BgColor );}
			set{ SetAttribute( StringConstAttributeName.BgColor , value);}
		}
		/// <summary>
		/// Sets or retrieves the bottom margin of the entire body of the page.
		/// </summary>
		public string BottomMargin
		{
			get{ return GetAttribute( StringConstAttributeName.BottomMargin );}
			set{ SetAttribute( StringConstAttributeName.BottomMargin , value);}
		}
		/// <summary>
		/// 对象是否可用
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
		/// Sets or retrieves the left margin for the entire body of the page, overriding the default margin. 
		/// </summary>
		public string LeftMargin
		{
			get{ return GetAttribute( StringConstAttributeName.LeftMargin ) ;}
			set{ SetAttribute( StringConstAttributeName.LeftMargin , value);}
		}
		/// <summary>
		/// Sets or retrieves the color of the document links for the object. 
		/// </summary>
		public string Link
		{
			get{ return GetAttribute( StringConstAttributeName.Link );}
			set{ SetAttribute( StringConstAttributeName.Link , value);}
		}
		/// <summary>
		/// Sets or retrieves whether the browser automatically performs wordwrap.
		/// </summary>
		public string NoWrap
		{
			get{ return GetAttribute( StringConstAttributeName.NoWrap );}
			set{ SetAttribute( StringConstAttributeName.NoWrap , value);}
		}
		/// <summary>
		/// Sets or retrieves the right margin for the entire body of the page. 
		/// </summary>
		public string RightMargin
		{
			get{ return GetAttribute( StringConstAttributeName.RightMargin );}
			set{ SetAttribute( StringConstAttributeName.RightMargin , value);}
		}
		/// <summary>
		/// Sets or retrieves the text (foreground) color for the document body. 
		/// </summary>
		public override string Text
		{
			get{ return GetAttribute( StringConstAttributeName.Text );}
			set{ SetAttribute( StringConstAttributeName.Text , value);}
		}
		/// <summary>
		/// Sets or retrieves advisory information (a ToolTip) for the object. 
		/// </summary>
		public string Title
		{
			get{ return GetAttribute( StringConstAttributeName.Title );}
			set{ SetAttribute( StringConstAttributeName.Title , value);}
		}
		/// <summary>
		/// Sets or retrieves the margin for the top of the page.
		/// </summary>
		public string TopMargin
		{
			get{ return GetAttribute( StringConstAttributeName.TopMargin );}
			set{ SetAttribute( StringConstAttributeName.TopMargin , value);}
		}
		/// <summary>
		/// Sets or retrieves the color of links in the object that have already been visited. 
		/// </summary>
		public string VLink
		{
			get{ return GetAttribute( StringConstAttributeName.VLink );}
			set{ SetAttribute( StringConstAttributeName.VLink , value);}
		}
		/// <summary>
		/// 内部方法,本类型可接收一切类型的子标签
		/// </summary>
		/// <param name="strName"></param>
		/// <returns></returns>
		internal override bool CheckChildTagName(string strName)
		{
			return true;
		}
 
	}//public class HTMLBodyElement : HTMLContainer
}