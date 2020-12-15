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
	/// 网页背景音乐对象(Enables an author to create pages with background sounds or soundtracks.)
	/// </summary>
	public class HTMLBGSoundElement : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回 "bgsound"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.BGSound ;}
		}
		/// <summary>
		/// Sets or retrieves the value indicating how the volume of the background sound is divided between the left and right speakers. 
		/// </summary>
		public string Balance
		{
			get{ return GetAttribute( StringConstAttributeName.Balance );}
			set{ SetAttribute( StringConstAttributeName.Balance , value);}
		}
		/// <summary>
		/// Sets or retrieves the URL of a sound to play. 
		/// </summary>
		public string Src
		{
			get{ return GetAttribute( StringConstAttributeName.Src );}
			set{ SetAttribute( StringConstAttributeName.Src , value);}
		}
		/// <summary>
		/// Sets or retrieves the number of times a sound or video clip will loop when activated. 
		/// </summary>
		public string Loop
		{
			get{ return GetAttribute( StringConstAttributeName.Loop );}
			set{ SetAttribute( StringConstAttributeName.Loop , value);}
		}
		/// <summary>
		/// Sets or retrieves the volume setting for the sound. 
		/// </summary>
		public string Volume
		{
			get{ return GetAttribute( StringConstAttributeName.Volume );}
			set{ SetAttribute( StringConstAttributeName.Volume , value);}
		}
	}//public class HTMLBGSoundElement : HTMLElement
}