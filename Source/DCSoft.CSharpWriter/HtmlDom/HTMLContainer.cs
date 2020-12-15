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
	/// 带有子对象的HTML元素
	/// </summary>
	public abstract class HTMLContainer : HTMLElement , System.Collections.IEnumerable
	{
		/// <summary>
		/// 子元素集合
		/// </summary>
		protected HTMLElementList myChildNodes = new HTMLElementList();
		/// <summary>
		/// 子节点集合
		/// </summary>
		public override HTMLElementList ChildNodes
		{
			get{ return myChildNodes;}
		}
		/// <summary>
		/// 添加子元素
		/// </summary>
		/// <param name="e">要添加的子元素对象</param>
		/// <returns>操作是否成功</returns>
		public override bool AppendChild( HTMLElement e )
		{
			if( myChildNodes.Contains( e ))
				return true;
			if( InnerAppendChild ( e ))
				return true;
 			else
			{
				if( myParent != null && myParent != this )
					myParent.AppendChild( e );
				return false;
			}
		}
		/// <summary>
		/// 内部的添加子元素
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		internal bool InnerAppendChild( HTMLElement e )
		{
			if( this.CheckChildTagName( e.TagName ) && e.CheckParent( this ) )
			{
				myChildNodes.Add( e );
				e.Parent = this ;
				e.OwnerDocument = myOwnerDocument ;
				if( e is IHTMLFormElement )
				{
					HTMLElement frm = this ;
					while( ( frm != null ) && ( !( frm is HTMLFormElement ) ))
					{
						frm = frm.Parent ;
					}
					if( frm is HTMLFormElement )
					{
						( ( HTMLFormElement ) frm).AppendElement( ( IHTMLFormElement) e);
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// 删除子元素
		/// </summary>
		/// <param name="e">要删除的元素</param>
		/// <returns>操作是否成功</returns>
		public virtual bool RemoveChild( HTMLElement e)
		{
			myChildNodes.Remove( e );
			return true;
		}
		/// <summary>
		/// 删除所有的子元素
		/// </summary>
		public virtual void RemoveAllChild()
		{
			myChildNodes.Clear();
		}
		/// <summary>
		/// 获得第一个子元素
		/// </summary>
		public HTMLElement FirstChild
		{
			get{ return myChildNodes.FirstElement ;}
		}
		/// <summary>
		/// 获得最后一个子元素
		/// </summary>
		public HTMLElement LastChild
		{
			get{ return myChildNodes.LastElement ;}
		}
		/// <summary>
		/// 获得指定子元素前面的元素
		/// </summary>
		/// <param name="e">指定的子元素</param>
		/// <returns>该子元素前面的元素</returns>
		internal HTMLElement GetPreElement( HTMLElement e)
		{
			return myChildNodes.GetPreElement( e );
		}
		/// <summary>
		/// 获得指定元素后面的元素
		/// </summary>
		/// <param name="e">指定的子元素</param>
		/// <returns>该子元素后面的子元素</returns>
		internal HTMLElement GetNextElement( HTMLElement e)
		{
			return myChildNodes.GetNextElement( e );
		}
		/// <summary>
		/// 获得所有指定标签名称的元素
		/// </summary>
		/// <param name="vTagName">指定的标签名称</param>
		/// <param name="myList">保存获得到元素的列表</param>
		/// <returns>获得到元素的个数</returns>
		internal virtual int InnerGetElementsByTagName( string vTagName , HTMLElementList myList )
		{
			int iCount = 0 ;
			foreach( HTMLElement e in myChildNodes)
			{
				if( e.TagName == vTagName )
				{
					myList.Add( e );
					iCount ++ ;
				}
				if( e is HTMLContainer )
				{
					iCount += ( ( HTMLContainer)e).InnerGetElementsByTagName( vTagName , myList) ;
				}
			}
			return iCount ;
		}
		/// <summary>
		/// 获得所有指定名称的元素
		/// </summary>
		/// <param name="vName">名称</param>
		/// <param name="myList">保存获得到元素的列表</param>
		/// <returns>获得到元素的个数</returns>
		internal virtual int InnerGetElementsByName( string vName , HTMLElementList myList )
		{
			int iCount = 0 ;
			foreach( HTMLElement e in myChildNodes)
			{
				if( e.GetAttribute(StringConstAttributeName.Name ) == vName )
				{
					myList.Add( e );
					iCount ++ ;
				}
				if( e is HTMLContainer )
				{
					iCount += ( ( HTMLContainer)e).InnerGetElementsByName( vName , myList) ;
				}
			}
			return iCount ;
		}

		internal virtual void InnerGetAllElement( HTMLElementList list )
		{
			foreach( HTMLElement element in myChildNodes )
			{
				list.Add( element );
				if( element is HTMLContainer )
				{
					( ( HTMLContainer ) element ).InnerGetAllElement( list );
				}
			}
		}

		/// <summary>
		/// 获得指定编号的元素
		/// </summary>
		/// <param name="vID">编号</param>
		/// <returns>元素对象,若没找到则返回空引用</returns>
		internal virtual HTMLElement InnerGetElementById( string vID )
		{
			HTMLElement myElement = null;
			foreach( HTMLElement e in myChildNodes)
			{
				if( e.ID == vID )
					return e;
				if( e is HTMLContainer )
				{
					myElement = ( ( HTMLContainer)e).InnerGetElementById( vID ) ;
					if( myElement != null)
						return myElement ;
				}
			}
			return null;
		}
//		/// <summary>
//		/// 获得指定序号的子节点
//		/// </summary>
//		public HTMLElement this[int index]
//		{
//			get{ return ( HTMLElement ) myChildNodes[ index] ;}
//		}
		/// <summary>
		/// 判断指定的标签名称是否可以为子节点的名称
		/// </summary>
		/// <param name="strName"></param>
		/// <returns></returns>
		internal virtual bool CheckChildTagName( string strName )
		{
			if( strName == StringConstTagName.Tr 
				|| strName == StringConstTagName.Td 
				|| strName == StringConstTagName.LI )
				return false;
			else
				return true;
		}

		/// <summary>
		/// 返回对象内部所有的纯文本信息
		/// </summary>
		public override string InnerText
		{
			get
			{
				System.Text.StringBuilder myStr = new System.Text.StringBuilder();
				this.GetDisplayText( myStr );
				string strText = myStr.ToString();
				strText = System.Web.HttpUtility.HtmlDecode( strText );
				return strText ;
			}
			set
			{
				HTMLTextNodeElement txt = new HTMLTextNodeElement();
				txt.Text = value ;
				if( txt.CheckParent( this ) )
				{
					txt.Text = value ;
					this.myChildNodes.Clear();
					this.AppendChild( txt );
				}
			}
		}

		/// <summary>
		/// 将对象内部所有的纯文本信息填写到一个字符串对象中
		/// </summary>
		/// <param name="myStr">供填写文本的字符串缓冲区</param>
		public virtual void GetDisplayText( System.Text.StringBuilder myStr )
		{
			foreach( HTMLElement element in myChildNodes )
			{
				if( element is HTMLContainer )
				{
					( ( HTMLContainer ) element).GetDisplayText( myStr );
				}
				else
					myStr.Append( element.InnerText );
			}
		}

		/// <summary>
		/// 从HTML文本读取器读取数据并加载子元素
		/// </summary>
		/// <param name="myReader">HTML文本读取器</param>
		/// <returns>操作是否成功</returns>
		internal override bool InnerRead(HTMLTextReader myReader)
		{
			string strTagName ;
			while( ! myReader.EOF )
			{
				string strText = myReader.ReadString();
				if( strText != null && this.CheckChildTagName( StringConstTagName.TextNode ))
				{
					HTMLTextNodeElement txt = new HTMLTextNodeElement();
					txt.Text = strText ;
					AppendChild( txt );
				}
				if( myReader.EOF )
					break;
				if( myReader.NextChar == '!') 
				{
					if( myReader.StartWidth("<!--"))
					{
						myReader.MoveStep(4);
						strText = myReader.ReadUntil("-->");
						if( strText != null)
						{
							if( this.CheckChildTagName( StringConstTagName.Comment ))
							{
								HTMLCommentElement com = new HTMLCommentElement();
								com.Text = strText ;
								AppendChild( com );
							}
						}
						myReader.MoveStep(3);
						continue;
					}
					else
					{
						myReader.MoveAfter('>');
						myReader.SkipWhiteSpace();
						continue;
					}
				}
					//				else if( myReader.NextChar == '/')
					//				{
					//					if( myReader.StartWidth("</" + this.TagName ))
					//					{
					//						myReader.MoveAfter('>');
					//						break;
					//					}
					//					else
					//					{
					//						myReader.MoveAfter('>');
					//						continue;
					//					}
					//				}
				else if( myReader.NextChar == '/')
				{
					myReader.MoveStep(2);
					string vEndTag = myReader.PeekWord();
					myReader.MoveStep(-2);
					//					if( ( vEndTag != null ) && vEndTag.ToLower() == this.TagName )
					//					{
					//						myReader.MoveAfter('>');
					//						break;
					//					}
					if( ( vEndTag != null) && ( MeetEndTag( myReader , vEndTag.ToLower() ) ))
					{
						break;
					}
					else
					{
						myReader.MoveAfter('>');
						continue;
					}
				}
				else if( myReader.NextChar == '?')
				{
					myReader.MoveAfter('>');
					continue;
				}
				myReader.MoveNext();
				strTagName = myReader.ReadWord();
				if( strTagName != null)
				{
					strTagName = strTagName.ToLower();
					if( ! BeforeLoadTag( strTagName ) )
					{
						myReader.MovePreTo('<'); 
						break;
					}
					if( this.CheckChildTagName( strTagName ))
					{
						HTMLElement NewElement = myOwnerDocument.InnerCreateElement( strTagName , this );
						if( NewElement != null )
						{
							AppendChild( NewElement );
							NewElement.Read( myReader );
						}
						else
						{
							myReader.ReadToEndTag( strTagName );
						}
					}
				}
			}//while
			return true;
		}

		/// <summary>
		/// 解析HTML时遇到结束标签时的处理
		/// </summary>
		/// <param name="myReader"></param>
		/// <param name="vTagName"></param>
		/// <returns></returns>
		internal virtual bool MeetEndTag( HTMLTextReader myReader , string vTagName )
		{
			if( vTagName == this.TagName || this.CheckChildTagName( vTagName ) == false )
			{
				myReader.MoveAfter('>');
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 开始加载标签前的处理
		/// </summary>
		/// <param name="vTagName"></param>
		/// <returns></returns>
		protected virtual bool BeforeLoadTag( string vTagName)
		{
			return true;
		}

		/// <summary>
		/// 向XML书写器输出对象数据
		/// </summary>
		/// <param name="myWriter">XML书写器</param>
		/// <returns>操作是否成功</returns>
		protected override bool InnerWrite( System.Xml.XmlWriter myWriter)
		{
			if( myChildNodes.Count > 0 )
			{
				foreach( HTMLElement e in myChildNodes)
				{
					e.Write( myWriter );
				}
				return true;
			}
			else
			{
				if( this.MustHasEndTag )
					myWriter.WriteString(" ");
			}
			return false;
		}

		/// <summary>
		/// 返回所有子元素的枚举器
		/// </summary>
		/// <returns>子元素枚举器对象</returns>
		public System.Collections.IEnumerator GetEnumerator()
		{
			return myChildNodes.GetEnumerator();
		}

		/// <summary>
		/// 本对象是否需要完整的结束标签
		/// </summary>
		protected virtual bool MustHasEndTag
		{
			get{ return true;}
		}
	}//public class HTMLContainer : HTMLElement , System.Collections.IEnumerable
}