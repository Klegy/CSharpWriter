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
	/// HTML标签元素基础类,其他所有的HTML标签元素都是本类的子类
	/// </summary>
	public abstract class HTMLElement
	{
		/// <summary>
		/// 属性列表
		/// </summary>
		protected HTMLAttributeList myAttributes = new HTMLAttributeList();
		/// <summary>
		/// 父节点
		/// </summary>
		protected HTMLContainer myParent ;
		/// <summary>
		/// 所属文档对象
		/// </summary>
		protected HTMLDocument myOwnerDocument ;
		/// <summary>
		/// 样式控制对象
		/// </summary>
		protected HTMLStyle myStyle ;
		/// <summary>
		/// 对象的样式控制表对象
		/// </summary>
		public HTMLStyle Style
		{
			get
			{
				if( myStyle == null)
				{
					myStyle = new HTMLStyle();
					myStyle.OwnerElement = this ;
				}
				return myStyle;
			}
		}
		/// <summary>
		/// 元素样式信息字符串
		/// </summary>
		public string StyleString
		{
			get
			{
				if( myStyle == null)
					return null;
				else
					return myStyle.CSSString ;
			}
			set
			{
				if( value == null)
					myStyle = null;
				else
					myStyle.CSSString = value;
			}
		}
		/// <summary>
		/// 元素内部的纯文本信息
		/// </summary>
		public virtual string InnerText
		{
			get{ return this.Text ;}
            set { this.Text = value ;}
		}
		/// <summary>
		/// 对象所属文档对象
		/// </summary>
		public HTMLDocument OwnerDocument
		{
			get{ return myOwnerDocument;}
			set{ myOwnerDocument = value;}
		}
		/// <summary>
		///所有属性列表 
		/// </summary>
		public HTMLAttributeList Attributes
		{
			get{ return myAttributes ;}
		}
		/// <summary>
		/// 获得指定名称的属性值,属性名称不区分大小写
		/// </summary>
		/// <param name="strName">属性名称</param>
		/// <returns>属性值,若该属性不存在则返回空引用</returns>
		public string GetAttribute( string strName )
		{
			return myAttributes.GetAttribute( strName );
		}
        public string GetAttributeTrim(string name)
        {
            string txt = this.Attributes.GetAttribute(name);
            if (txt == null)
            {
                return txt;
            }
            else
            {
                return txt.Trim();
            }
        }
		/// <summary>
		/// 设置指定名称的属性值,属性名称不区分大小写
		/// </summary>
		/// <param name="strName">属性名</param>
		/// <param name="strValue">属性值,若为空引用则删除该属性</param>
		public void SetAttribute( string strName , string strValue)
		{
			if( strValue == null || strValue.Trim().Length == 0 )
			{
				myAttributes.RemoveAttribute( strName );
				return ;
			}
			myAttributes.SetAttribute( strName , strValue );
		}
		/// <summary>
		/// 删除指定名称的属性,属性名称不区分大小写
		/// </summary>
		/// <param name="strName">属性名称</param>
		public void RemoveAttribute( string strName )
		{
			myAttributes.RemoveAttribute( strName );
		}
		/// <summary>
		/// 是否存在指定名称的属性,属性名称不区分大小写
		/// </summary>
		/// <param name="strName">属性名称</param>
		/// <returns>是否存在该名称的属性</returns>
		public bool HasAttribute( string strName)
		{
			return myAttributes.HasAttribute( strName );
		}
		/// <summary>
		/// 获得布尔类型的属性值
		/// </summary>
		/// <param name="strName">属性名称</param>
		/// <returns>属性值</returns>
		public bool GetBoolAttribute( string strName)
		{
			return myAttributes.HasAttribute( strName );
		}
		/// <summary>
		/// 设置布尔类型的属性值
		/// </summary>
		/// <param name="strName">属性名</param>
		/// <param name="vValue">属性值</param>
		public void SetBoolAttribute( string strName , bool vValue )
		{
			if( vValue)
				myAttributes.SetAttribute( strName , "1");
			else
				myAttributes.RemoveAttribute( strName );
		}
		/// <summary>
		/// 删除所有的属性
		/// </summary>
		public void ClearAttribute()
		{
			myAttributes.Clear();
			myStyle = null;
		}

		/// <summary>
		/// 添加子元素
		/// </summary>
		/// <param name="e">要添加的子元素对象</param>
		/// <returns>操作是否成功</returns>
		public virtual bool AppendChild( HTMLElement e )
		{
			return false;
		}
		/// <summary>
		/// 子节点集合
		/// </summary>
		public virtual HTMLElementList ChildNodes
		{
			get{ return null ;}
		}

//		
//		public virtual string DisplayText
//		{
//			get{ return null;}
//		}

		#region 定义元素事件信息 ******************************************************************

		/// <summary>
		/// 鼠标单击事件
		/// </summary>
		public string OnClick
		{
			get{ return this.GetAttribute( StringConstAttributeName.OnClick );}
			set{ this.SetAttribute( StringConstAttributeName.OnClick , value );}
		}
		/// <summary>
		/// 鼠标按下事件
		/// </summary>
		public string OnMouseDown
		{
			get{ return this.GetAttribute( StringConstAttributeName.OnMouseDown );}
			set{ this.SetAttribute( StringConstAttributeName.OnMouseDown , value );}
		}
		/// <summary>
		/// 鼠标进入事件
		/// </summary>
		public string OnMouseEnter
		{
			get{ return this.GetAttribute( StringConstAttributeName.OnMouseEnter );}
			set{ this.SetAttribute( StringConstAttributeName.OnMouseEnter , value );}
		}
		/// <summary>
		/// 鼠标悬停事件
		/// </summary>
		public string OnMouseOver
		{
			get{ return this.GetAttribute( StringConstAttributeName.OnMouseOver );}
			set{ this.SetAttribute( StringConstAttributeName.OnMouseOver , value );}
		}
		/// <summary>
		/// 鼠标离开事件
		/// </summary>
		public string OnMouseLeave
		{
			get{ return this.GetAttribute( StringConstAttributeName.OnMouseLeave );}
			set{ this.SetAttribute( StringConstAttributeName.OnMouseLeave , value );}
		}
		/// <summary>
		/// 鼠标按键松开事件
		/// </summary>
		public string OnMouseUp
		{
			get{ return this.GetAttribute( StringConstAttributeName.OnMouseUp );}
			set{ this.SetAttribute( StringConstAttributeName.OnMouseUp , value );}
		}
		/// <summary>
		/// 鼠标滚轮事件
		/// </summary>
		public string OnMouseWheel
		{
			get{ return this.GetAttribute( StringConstAttributeName.OnMouseWheel );}
			set{ this.SetAttribute( StringConstAttributeName.OnMouseWheel , value );}
		}
		/// <summary>
		/// 对象数据改变事件
		/// </summary>
		public string OnBlur
		{
			get{ return this.GetAttribute( StringConstAttributeName.OnBlur );}
			set{ this.SetAttribute( StringConstAttributeName.OnBlur , value );}
		}
		
		#endregion

		#region 元素特定的属性值 ******************************************************************

		/// <summary>
		/// Sets or retrieves the accelerator key for the object
		/// </summary>
		public string AccessKey
		{
			get{ return GetAttribute( StringConstAttributeName.AccessKey );}
			set{ SetAttribute( StringConstAttributeName.AccessKey , value);}
		}

		/// <summary>
		/// 样式类的名称
		/// </summary>
		public string ClassName
		{
			get{ return this.GetAttribute(StringConstAttributeName.ClassName );}
			set{ this.SetAttribute(StringConstAttributeName.ClassName , value);}
		}
		/// <summary>
		/// 对象编号
		/// </summary>
		public string ID
		{
			get{ return this.GetAttribute( StringConstAttributeName.ID );}
			set{ this.SetAttribute( StringConstAttributeName.ID , value );}
		}

		#endregion

		/// <summary>
		/// 检查指定的容器元素,判断当前元素是否可以为该元素的子元素
		/// </summary>
		/// <param name="vParent">容器元素</param>
		/// <returns>是否可为该元素的子元素</returns>
		internal virtual bool CheckParent( HTMLContainer vParent )
		{
			return true;
		}
		/// <summary>
		/// 父对象
		/// </summary>
		public HTMLContainer Parent
		{
			get
			{
				if( myParent == this )
					return null;
				else
					return myParent;
			}
			set{ myParent = value; if( value != null) this.myOwnerDocument = value.OwnerDocument ;}
		}
		/// <summary>
		/// 获得前一个元素
		/// </summary>
		public HTMLElement PreviousSibling
		{
			get
			{
				if( myParent == null)
					return null;
				else
					return myParent.GetPreElement( this );
			}
		}
		/// <summary>
		/// 获得后一个元素
		/// </summary>
		public HTMLElement NextSibling
		{
			get
			{
				if( myParent == null )
					return null;
				else
					return myParent.GetNextElement( this );
			}
		}
		/// <summary>
		/// 对象标签名称
		/// </summary>
		public virtual string TagName
		{
			get{ return null;}
		}

        /// <summary>
        /// 获得修正后的对象标签名称，去掉两边的空格并设置为小写
        /// </summary>
        public string FixTagName
        {
            get
            {
                string tg = this.TagName;
                if (tg != null)
                {
                    return tg.Trim().ToLower();
                }
                else
                {
                    return tg;
                }
            }
        }
		/// <summary>
		/// 对象数值
		/// </summary>
		public virtual string Value
		{
			get{ return null;}
			set{ ;}
		}
		/// <summary>
		/// 内部使用:是否有内部的文本
		/// </summary>
		protected virtual bool HasText 
		{
			get{ return false;}
		}
		/// <summary>
		/// 对象文本
		/// </summary>
		public virtual string Text
		{
			get{ return null;}
			set{;}
		}
		/// <summary>
		/// 从一个文本流中加载对象数据
		/// </summary>
		/// <param name="myReader">HTML文本读取器</param>
		/// <returns>操作是否成功</returns>
		internal virtual bool Read( HTMLTextReader myReader)
		{
			ReadAttribute( myReader );
			if( ! myReader.EOF )
				return InnerRead( myReader );
			else
				return false;
		}

		/// <summary>
		/// 读取元素所有的属性值
		/// </summary>
		/// <param name="myReader">HTML文本读取器</param>
		internal virtual void ReadAttribute( HTMLTextReader myReader)
		{
			string strName = null;
			string strValue = null;
			while( ! myReader.EOF )
			{
				myReader.SkipWhiteSpace();
				// 若遇到元素结束标记则退出
				if( myReader.StartWidth("/>"))
				{
					myReader.MoveStep(2);
					break;
				}
				if( myReader.Peek == '>' || myReader.Peek == '<' )
				{
					myReader.MoveNext();
					break;
				}
				// 读取属性名
				strName = myReader.ReadWord( );
				if( strName == null)
				{
					break;
				}
				strName = strName.ToLower();
				// 读取属性值
                myReader.SkipWhiteSpace();
				if( myReader.EOF || myReader.Peek !='=')
					strValue = "1" ;
				else
				{
					myReader.MoveNext();
					strValue = myReader.ReadQuotMarkText();
				}
				// 保存属性
				if( System.Xml.XmlReader.IsName( strName ))
					this.SetAttribute( strName , strValue );
			}//while
			// 设置样式控制表属性
			if( HasAttribute( StringConstAttributeName.Style ))
			{
				myStyle = new HTMLStyle();
				myStyle.CSSString = GetAttribute( StringConstAttributeName.Style );
			}
			else
				myStyle = null;
		}
		/// <summary>
		/// 加载内容
		/// </summary>
		/// <param name="myReader">HTML文本读取器</param>
		/// <returns>操作是否成功</returns>
		internal virtual bool InnerRead( HTMLTextReader myReader)
		{
			if( this.HasText )
			{
				// 加载文本内容
				string vText = myReader.ReadString();
				if( myReader.StartWidth("</" + this.TagName ))
					myReader.MoveAfter('>');
				this.Text = vText ;
			}
			return true;
		}
		/// <summary>
		/// 向文本流中填写对象数据
		/// </summary>
		/// <param name="myWriter">XML书写器</param>
		/// <returns>操作是否成功</returns>
		public virtual bool Write( System.Xml.XmlWriter myWriter)
		{
			if( myOwnerDocument.WriteOptions.EnableOutput( this ) )
			{
				this.SetAttribute( StringConstAttributeName.Style , this.StyleString );
				myWriter.WriteStartElement( this.TagName );
				this.WriteAllAttributes( myWriter );
				this.WriteXSLAttribute( myWriter );
				InnerWrite( myWriter );
				myWriter.WriteEndElement();
			}
			else
			{
				if( myOwnerDocument.WriteOptions.InnerWriteWhenDisable )
					this.InnerWrite( myWriter );
			}
			return true;
		}
		/// <summary>
		/// 向XML书写器书写所有的属性值
		/// </summary>
		/// <param name="myWriter">XML书写器</param>
		public void WriteAllAttributes( System.Xml.XmlWriter myWriter )
		{
			if( myOwnerDocument.WriteOptions.AttributeOutputXSL )
			{
				foreach( HTMLAttribute a in myAttributes)
				{
					if( a.IsXSLAttribute == false)
						myWriter.WriteAttributeString( a.Name , a.FixValue );
				}
			}
			else
			{
				foreach( HTMLAttribute a in myAttributes)
					myWriter.WriteAttributeString( a.Name , a.Value );
			}
		}

		/// <summary>
		/// 向XML书写器属性所有的生成属性的XSLT代码
		/// </summary>
		/// <param name="myWriter">XML书写器</param>
		/// <remarks>
		/// 例如对于属性 aname="xsl:myname" 则此函数输出
		/// ＜xsl:attribute name="aname"＞
		///		＜xsl:value-of select="myname" /＞
		///	＜/xsl:attribute＞
		/// </remarks>
		public void WriteXSLAttribute( System.Xml.XmlWriter myWriter )
		{
			if( myOwnerDocument.WriteOptions.AttributeOutputXSL )
			{
				foreach( HTMLAttribute a in myAttributes)
				{
					if( a.IsXSLAttribute )
					{
						myWriter.WriteStartElement( StringConstXSLT.Attribute );
						myWriter.WriteAttributeString( StringConstAttributeName.Name , a.Name );
						myWriter.WriteStartElement( StringConstXSLT.Value_of );
						myWriter.WriteAttributeString( StringConstXSLT.Select , a.XSLSource );
						myWriter.WriteEndElement();
						myWriter.WriteEndElement();
					}
				}
			}
		}

		/// <summary>
		/// 填写内容的方法,若填写了内容则返回true,若没有填写任何内容则返回false
		/// </summary>
		/// <param name="myWriter"></param>
		/// <returns></returns>
		protected virtual bool InnerWrite( System.Xml.XmlWriter myWriter)
		{
			if( this.HasText )
			{
				if( this.Text != null )
					myWriter.WriteString( this.Text );
			}
			return false ;
		}
	}//public class HTMLElement
}