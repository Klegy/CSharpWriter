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
	/// 脚本对象
	/// </summary>
	public class HTMLScriptElement : HTMLElement
	{
		/// <summary>
		/// 对象标签名称,返回"script"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Script ; }
		}
		private string strText ;
		/// <summary>
		/// Sets or retrieves the status of the script.
		/// </summary>
		public string Defer
		{
			get{ return GetAttribute( StringConstAttributeName.Defer );}
			set{ SetAttribute( StringConstAttributeName.Defer , value);}
		}
		/// <summary>
		/// Sets or retrieves the event for which the script is written. 
		/// </summary>
		public string Event
		{
			get{ return GetAttribute( StringConstAttributeName.Event );}
			set{ SetAttribute( StringConstAttributeName.Event , value);}
		}
		/// <summary>
		/// Sets or retrieves the object that is bound to the event script. 
		/// </summary>
		public string HTMLFor
		{
			get{ return GetAttribute( StringConstAttributeName.HTMLFor );}
			set{ SetAttribute( StringConstAttributeName.HTMLFor , value);}
		}
		/// <summary>
		/// 使用的脚本语言
		/// </summary>
		public string Language
		{
			get{ return GetAttribute( StringConstAttributeName.Language );}
			set{ SetAttribute( StringConstAttributeName.Language ,value);}
		}
		/// <summary>
		/// 脚本文件来源
		/// </summary>
		public string Src
		{
			get{ return GetAttribute( StringConstAttributeName.Src );}
			set{ SetAttribute( StringConstAttributeName.Src , value);}
		}
		/// <summary>
		/// Sets or retrieves the MIME type for the associated scripting engine. 
		/// </summary>
		public string Type
		{
			get{ return GetAttribute( StringConstAttributeName.Type );}
			set{ SetAttribute( StringConstAttributeName.Type , value);}
		}
		/// <summary>
		/// Retrieves or sets the text of the object as a string. 
		/// </summary>
		public override string Text
		{
			get{ return strText ;}
			set{ strText =value;}
		}
		/// <summary>
		/// 内部方法
		/// </summary>
		protected override bool HasText
		{
			get{ return true; }
		}
		/// <summary>
		/// 读取HTML代码,忽略HTML注释
		/// </summary>
		/// <param name="myReader">HTML文本读取器</param>
		/// <returns>操作是否成功</returns>
		internal override bool InnerRead(HTMLTextReader myReader)
		{
			strText = myReader.ReadToEndTag( this.TagName );
			if( strText != null)
			{
				int index = strText.IndexOf("<!--") ;
				if( index >= 0 )
					strText = strText.Substring( index + 4);
				index = strText.LastIndexOf("-->");
				if( index >= 0 )
					strText = strText.Substring( 0 , index );
			}
			//myReader.MoveAfter('>');
			return true ;
		}
		/// <summary>
		/// 向XML书写器输出对象数据
		/// </summary>
		/// <remarks>若设置了OwnerDocument.WriteOptions.ScriptWriteCData
		/// 则脚本代码放置在 CDATA 块中</remarks>
		/// <param name="myWriter">XML书写器</param>
		/// <returns>操作是否成功</returns>
		protected override bool InnerWrite(System.Xml.XmlWriter myWriter)
		{
			if( HTMLTextReader.isBlankString( strText ))
				myWriter.WriteString( " " );
			else
			{
				if( myOwnerDocument.WriteOptions.ScriptWriteCData )
				{
					string vText = strText.Replace("<![CDATA[","");
					vText = vText.Replace("]]" ,"");
					myWriter.WriteString("//");
					myWriter.WriteCData( vText + "//" );
				}
				else
				{
					string vText = strText + " ";
					if( vText.IndexOf("--") >= 0 )
					{
						vText = "因保存需要,将所有的\"--\"转换为 \"@@\"符号\r\n" + vText.Replace("--" , "@@");
					}
					myWriter.WriteComment( vText );
				}
				//myWriter.WriteString( strText );
			}
			return true;
		}
	}//public class HTMLScriptElement : HTMLElement
}