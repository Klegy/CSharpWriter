/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;

namespace DCSoft.CSharpWriter
{
	/// <summary>
	/// 文档格式样式
	/// </summary>
	public enum FileFormat
	{
        /// <summary>
        /// 无效值
        /// </summary>
        None,
		/// <summary>
		/// 标准的XML序列化格式
		/// </summary>
		XML ,
		/// <summary>
		/// RTF文档
		/// </summary>
		RTF ,
		/// <summary>
		/// 纯文本
		/// </summary>
		Text,
        /// <summary>
        /// HTML文档
        /// </summary>
        Html,
        ///// <summary>
        ///// 旧版本XML格式。
        ///// </summary>
        //OldXML
	}
}