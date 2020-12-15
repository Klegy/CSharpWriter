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
	/// 滚动条幅对象
	/// </summary>
	public class HTMLMarqueeElement : HTMLContainer
	{
		/// <summary>
		/// 对象标签名称,返回"marquee"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Marquee ;}
		}
		/// <summary>
		/// Sets or retrieves how the text scrolls in the marquee. 
		/// </summary>
		public string Behavior
		{
			get{ return GetAttribute( StringConstAttributeName.Behavior );}
			set{ SetAttribute( StringConstAttributeName.Behavior , value);}
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
		/// Sets or retrieves the direction in which the text should scroll. 
		/// </summary>
		public string Direction
		{
			get{ return GetAttribute( StringConstAttributeName.Direction );}
			set{ SetAttribute( StringConstAttributeName.Direction , value);}
		}
		/// <summary>
		/// Sets or retrieves the status of the object. 
		/// </summary>
		public bool Disabled
		{
			get{ return GetBoolAttribute( StringConstAttributeName.Disabled );}
			set{ SetBoolAttribute( StringConstAttributeName.Disabled , value);}
		}
		/// <summary>
		/// Sets or retrieves the height of the object. 
		/// </summary>
		public string Height
		{
			get{ return GetAttribute( StringConstAttributeName.Height );}
			set{ SetAttribute( StringConstAttributeName.Height , value);}
		}
		/// <summary>
		/// Sets or retrieves the horizontal margin for the object. 
		/// </summary>
		public string HSpace
		{
			get{ return GetAttribute( StringConstAttributeName.HSpace );}
			set{ SetAttribute( StringConstAttributeName.HSpace , value);}
		}
		/// <summary>
		/// Sets or retrieves the number of times a marquee will play. 
		/// </summary>
		public string Loop
		{
			get{ return GetAttribute( StringConstAttributeName.Loop );}
			set{ SetAttribute( StringConstAttributeName.Loop , value);}
		}
		/// <summary>
		/// Sets or retrieves the number of pixels the text scrolls between each subsequent drawing of the marquee. 
		/// </summary>
		public string ScrollAmount
		{
			get{ return GetAttribute( StringConstAttributeName.ScrollAmount );}
			set{ SetAttribute( StringConstAttributeName.ScrollAmount , value);}
		}
		/// <summary>
		/// Sets or retrieves the speed of the marquee scroll. 
		/// </summary>
		public string ScrollDelay
		{
			get{ return GetAttribute( StringConstAttributeName.ScrollDelay );}
			set{ SetAttribute( StringConstAttributeName.ScrollDelay , value);}
		}
		/// <summary>
		/// Sets or retrieves the type of timeline associated with an element.
		/// </summary>
		public string TimeContainer
		{
			get{ return GetAttribute( StringConstAttributeName.TimeContainer );}
			set{ SetAttribute( StringConstAttributeName.TimeContainer , value);}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public string Title
		{
			get{ return GetAttribute( StringConstAttributeName.Title );}
			set{ SetAttribute( StringConstAttributeName.Title , value);}
		}
		/// <summary>
		/// Sets or retrieves whether the position of the marquee is calculated using the scrollDelay and scrollAmount properties and the actual time elapsed from the last clock tick. 
		/// </summary>
		public string TrueSpeed
		{
			get{ return GetAttribute( StringConstAttributeName.TrueSpeed );}
			set{ SetAttribute( StringConstAttributeName.TrueSpeed , value);}
		}
		/// <summary>
		/// Sets or retrieves the vertical margin for the object. 
		/// </summary>
		public string VSpace
		{
			get{ return GetAttribute( StringConstAttributeName.VSpace );}
			set{ SetAttribute( StringConstAttributeName.VSpace , value);}
		}
		/// <summary>
		/// Sets or retrieves the width of the object.
		/// </summary>
		public string Width
		{
			get{ return GetAttribute( StringConstAttributeName.Width );}
			set{ SetAttribute( StringConstAttributeName.Width , value);}
		}
		internal override bool CheckChildTagName(string strName)
		{
			return strName == StringConstTagName.B 
				|| strName == StringConstTagName.Span 
				|| strName == StringConstTagName.A 
				|| strName == StringConstTagName.I 
				|| strName == StringConstTagName.TextNode 
				|| strName == StringConstTagName.Font ;
		}
	}//public class HTMLMarqueeElement : HTMLContainer
}