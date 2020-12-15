/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;

namespace DCSoft.HtmlDom
{
	/// <summary>
	/// HTML的css样式Display属性可选值字符串常量列表
	/// </summary>
	public sealed class StringConstDisplay
	{
		/// <summary>
		/// Object is rendered as a block element. 
		/// </summary>
		public const string block 				="block" ; 
		/// <summary>
		///  Object is not rendered. 
		/// </summary>
		public const string none 				="none" ; 
		/// <summary>
		/// Default. Object is rendered as an inline element sized by the dimensions of the content. 
		/// </summary>
		public const string inline 				="inline" ;  
		/// <summary>
		/// Object is rendered inline, but the contents of the object are rendered as a block element. Adjacent inline elements are rendered on the same line, space permitting. 
		/// </summary>
		public const string inline_block 		="inline-block" ;  
		/// <summary>
		/// Internet Explorer 6 and later. Object is rendered as a block element, and a list-item marker is added. 
		/// </summary>
		public const string list_item   		="list-item" ;    
		/// <summary>
		/// Table header is always displayed before all other rows and row groups, and after any top captions. The header is displayed on each page spanned by a table. 
		/// </summary>
		public const string table_header_group 	="table-header-group" ;  
		/// <summary>
		///  Table footer is always displayed after all other rows and row groups, and before any bottom captions. The footer is displayed on each page spanned by a table. 
		/// </summary>
		public const string table_footer_group 	="table-footer-group" ; 

		private StringConstDisplay(){}
	}//public sealed class HTMLDisplayStringConst
}