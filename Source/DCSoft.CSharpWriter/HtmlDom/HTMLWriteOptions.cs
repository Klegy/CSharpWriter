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
	/// HTML输出设置信息对象
	/// </summary>
	public class HTMLWriteOptions
	{
		/// <summary>
		/// HTML文档对象是否输出为XSLT代码
		/// </summary>
		/// <remarks>当设置了此选项,则输出的 [html] 标签外还套上
		/// [xsl:stylesheet][xsl:template match="/*"] 的标签
		/// </remarks>
		public bool DocumentOutputXSL = false;

		/// <summary>
		/// INPUT元素是否输出为XSLT代码
		/// </summary>
		/// <remarks>当设置了此选项,则
		/// 对于 ＜input type="text|hidden" ＞时会内嵌 
		/// ＜xsl:attribute name="value"＞
		///		＜xsl:value-of select="对象名称"/＞
		///	＜/xsl:attribute＞
		/// 对于 ＜input type="checkbox|radio"＞则内嵌 
		/// ＜xsl:if test="对象名称='对象值'＞
		///		＜xsl:attribute name="checked"＞1＜/xsl:attribute＞
		///	＜/xsl:if＞
		/// </remarks>
		public bool InputOutputXSL = false;

		/// <summary>
		/// Select 元素是否输出为XSLT代码
		/// </summary>
		/// <remarks>当设置了此选项,而且 select 标签的 xslsource 属性不为空则对于 select 标签输出为
		/// ＜select＞
		///		＜xsl:variable name="selectvalue"＞
		///			＜xsl:value-of select="对象名称" /＞
		/// 	＜/xsl:variable＞
		///		＜xsl:for-each select="对象xslsource属性值"＞
		///			＜option＞
		///				＜xsl:attribute name="对象valuexslsource属性值"＞
		///					＜xsl:value-of select="" /＞
		/// 			＜/xsl:attribute＞
		///				＜xsl:if test="$selectvalue=对象valuexslsource属性值"＞
		///					＜xsl:attribute name="selected"＞1＜/xsl:attribute＞
		///				＜/xsl:if＞
		///				＜xsl:value-of select="对象textxslsource属性值" /＞
		/// 		＜/option＞
		///		＜/xsl:for-each＞
		/// ＜/select＞
		/// </remarks>
		public bool SelectOutputXSL = false;

		/// <summary>
		/// TD元素是否输出为XSLT代码
		/// </summary>
		/// <remarks>
		/// 若设置该属性且 td 标签 xslsource 属性不为空
		/// 则在 td 标签外还套上[xsl:for-each select="xslsource属性值"] 标签
		/// </remarks>
		public bool TDOutputXSL = false;

		/// <summary>
		/// TR元素是否输出为XSLT代码
		/// </summary>
		/// <remarks>
		/// 若设置该属性且 tr 标签 xslsource 属性不为空
		/// 则在 tr 标签外还套上[xsl:for-each select="xslsource属性值"] 标签
		/// </remarks>
		public bool TROutputXSL = false;
		
		/// <summary>
		/// 输出的纯文本是否压缩空白字符
		/// </summary>
		public bool NormalizeSpace = false;

		/// <summary>
		/// 输出的纯文本外是否包含SPAN标签
		/// </summary>
		public bool AddSpan = false ;

		/// <summary>
		/// 是否输出空白字符
		/// </summary>
		public bool WriteWhitespace = true;

		/// <summary>
		/// 纯文本是否输出为XSLT代码
		/// </summary>
		public bool TextOutPutXSL = false;

		/// <summary>
		/// 纯文本中数值域前缀
		/// </summary>
		public string TextFieldPrefix = "[";

		/// <summary>
		/// 纯文本中数值域后缀
		/// </summary>
		public string TextFieldEndfix = "]";

		/// <summary>
		/// 元素属性值是否输出为XSLT代码
		/// </summary>
		public bool AttributeOutputXSL = false;

		/// <summary>
		/// option元素是否输出为XSLT代码
		/// </summary>
		public bool OptionOutputXSL = false;

		/// <summary>
		/// TextArea 元素是否输出为XSLT代码
		/// </summary>
		public bool TextAreaOutputXSL = false;

		/// <summary>
		/// 是否输出注释
		/// </summary>
		public bool CommentOutput = true;

		/// <summary>
		/// 是否将注释输出为XSLT代码
		/// </summary>
		/// <remarks>若设置该设置则注释输出为 [xsl:comment]对象文本[/xsl:comment] </remarks>
		public bool CommentOutputXSL = false;

		/// <summary>
		/// 保存Scritpt内容时使用CData
		/// </summary>
		public bool ScriptWriteCData = true;

		/// <summary>
		/// 是否格式化 Style 标签内容
		/// </summary>
		/// <remarks>若设置了该属性则输出 style 标签内容时,将各个样式成员进行换行格式处理,否则所有的样式成员全部放在一行 </remarks>
		public bool FormatStyleText = true;

		/// <summary>
		/// 对象所属HTML文档对象
		/// </summary>
		protected HTMLDocument myOwnerDocument = null;
		/// <summary>
		/// 对象所属文档对象
		/// </summary>
		public HTMLDocument OwnerDocument
		{
			get{ return myOwnerDocument ;}
			set{ myOwnerDocument = value;}
		}
		/// <summary>
		/// 禁止输出的标签名称列表
		/// </summary>
		protected string[] strDisableOutputTagNames = null;
		/// <summary>
		/// 禁止输出的标签名称列表
		/// </summary>
		public string[] DisableOutputTagNames
		{
			get{ return strDisableOutputTagNames ;}
			set
			{
				System.Collections.ArrayList myList = new System.Collections.ArrayList();
				if( value != null)
				{
					foreach( string strItem in value)
					{
						if( strItem != null)
						{
							string strV  = strItem.Trim().ToLower();
							if( strV.Length > 0 )
								myList.Add( strV );
						}
					}
				}
				if( myList.Count > 0 )
					strDisableOutputTagNames =(string[]) myList.ToArray( typeof( string ));
				else
					strDisableOutputTagNames = null;
			}
		}
		/// <summary>
		/// 当元素禁止输出时是否输出其子节点
		/// </summary>
		public bool InnerWriteWhenDisable = true;
		/// <summary>
		/// 内部方法:判断指定的元素标签能否输出
		/// </summary>
		/// <param name="vElement">元素对象</param>
		/// <returns>能否输出</returns>
		public virtual bool EnableOutput( HTMLElement vElement )
		{
			if( strDisableOutputTagNames != null && vElement != null )
			{
				string vTagName = vElement.TagName ;
				foreach( string vName in strDisableOutputTagNames )
				{
					if( string.Compare( vName , vTagName , true ) == 0 )
						return false;
				}
			}
			return true;
		}
		/// <summary>
		/// 内部方法:修正XSL名称
		/// </summary>
		/// <param name="strSource"></param>
		/// <returns></returns>
		public string FixXSLSource( string strSource )
		{
			if( strSource != null && strSource.IndexOf('【') >= 0 && strSource.IndexOf('】')> 0 )
			{
				strSource = strSource.Replace('【','[');
				strSource = strSource.Replace('】',']');
			}
			return strSource ;
		}

	}//public class HTMLWriteOptions
}