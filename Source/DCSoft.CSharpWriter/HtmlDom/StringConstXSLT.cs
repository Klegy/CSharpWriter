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
	/// 一些XSLT标签和属性的名称列表
	/// </summary>
	public sealed class StringConstXSLT
	{
		public const string Stylesheet	= "xsl:stylesheet";
		public const string Template	= "xsl:template";
		public const string NSName		= "xmlns:xsl";
		public const string NSValue		= "http://www.w3.org/1999/XSL/Transform";
		public const string Version		= "version";
		public const string Match		= "match";
		public const string For_each	= "xsl:for-each";
		public const string Value_of	= "xsl:value-of";
		public const string IF			= "xsl:if";
		public const string Test		= "test";
		public const string Select		= "select";
		public const string Name		= "name";
		public const string Attribute	= "xsl:attribute";
		public const string Variable	= "xsl:variable";
		public const string Text		= "xsl:text";
		public const string Comment		= "xsl:comment";
		public const string Output		= "xsl:output";
		public const string Include		= "xsl:include";
		public const string Calltemplate = "xsl:call-template" ;
		public const string WithParam	= "xsl:with-param" ;
		public const string Disable_output_escaping = "disable-output-escaping";

		private StringConstXSLT(){}
	}//public sealed class StringConstXSLT
}