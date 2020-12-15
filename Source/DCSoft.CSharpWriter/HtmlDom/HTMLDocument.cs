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
	/// HTML文档对象
	/// </summary>
	public class HTMLDocument : HTMLContainer
	{
        /// <summary>
        /// 显示关于对话框
        /// </summary>
        public static void About()
        {
            string txt = @"XDesignerHTMLDom 是 DCSoft 于2006年5月16号发布的实现了HTML文档对象模型的库，特点为：
   1 运行在微软 DOTNET1.1 的环境下，不依赖其他非标准库。
   2 可解析静态HTML并生成文档对象模型树，不支持动态HTML。
   3 可处理某些劣质的HTML文档。
   4 不严格遵守HTML相关国际标准。
   5 可将结构保存到XML或XSLT文件中。
   6 一般可用于对静态HTML文档的结构化分析处理。
更多信息请访问 http://xdesigner.cnblogs.com";
            System.Windows.Forms.MessageBox.Show(null, txt, "关于XDesignerHTMLDom", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }
        
        /// <summary>
		/// 初始化对象
		/// </summary>
		public HTMLDocument()
		{
			myOwnerDocument = this ;
			myWriteOptions.OwnerDocument = this ;
		}

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="bs">HTML文档内容</param>
        /// <param name="contentEncoding">HTTP响应中指定的编码格式</param>
        public HTMLDocument(byte[] bs, string contentEncoding)
        {
            myOwnerDocument = this;
            myWriteOptions.OwnerDocument = this;
            if (bs != null)
            {
                System.Text.Encoding myEncode = System.Text.Encoding.GetEncoding(936);
                if (contentEncoding != null && contentEncoding.Trim().Length > 0)
                {
                    myEncode = System.Text.Encoding.GetEncoding(contentEncoding);
                }
                string html = myEncode.GetString(bs);
                try
                {
                    this.CharSet = myEncode.BodyName;
                    this.LoadHTML(html);
                }
                catch ( HTMLChangeCharSetException)
                {
                    try
                    {
                        myEncode = System.Text.Encoding.GetEncoding(this.CharSet);
                    }
                    catch
                    {
                        myEncode = System.Text.Encoding.GetEncoding(936);
                    }
                    html = myEncode.GetString(bs);
                    this.LoadHTML(html);
                }
            }
        }

		/// <summary>
		/// 输出格式控制
		/// </summary>
		protected HTMLWriteOptions myWriteOptions = new HTMLWriteOptions();

		/// <summary>
		/// HTML输出控制对象
		/// </summary>
		public HTMLWriteOptions WriteOptions
		{
			get{ return myWriteOptions ;}
			set{ myWriteOptions = value; if( value != null) value.OwnerDocument = this ;}
		}

		private bool bolLoadingFlag = false;

		private string strBaseURL = null;
		/// <summary>
		/// 文档基础地址
		/// </summary>
		public string BaseURL
		{
			get{ return strBaseURL ;}
			set{ strBaseURL = value;}
		}

        ///// <summary>
        ///// 获得绝对路径
        ///// </summary>
        ///// <param name="url">相对路径</param>
        ///// <returns>绝对路径</returns>
        //public string GetAbsoluteUrl(string url)
        //{
        //    Uri uri = new Uri(url);
        //    if (uri.IsAbsoluteUri)
        //    {
        //        if (uri.Scheme == Uri.UriSchemeFile)
        //        {
        //            return uri.LocalPath;
        //        }
        //        else
        //        {
        //            return uri.AbsoluteUri;
        //        }
        //    }
        //    else
        //    {
        //        Uri result = new Uri( new Uri( this.BaseURL ) , url , true );
        //        if (result.Scheme == Uri.UriSchemeFile)
        //        {
        //            return result.LocalPath;
        //        }
        //        else
        //        {
        //            return result.AbsoluteUri;
        //        }
        //    }
        //}

		private string strLocation ;

		/// <summary>
		/// 文档路径
		/// </summary>
		public string Location
		{
			get{ return strLocation;}
			set{ strLocation = value;}
		}

		/// <summary>
		/// 正在加载文档标记
		/// </summary>
		internal bool LoadingFlag
		{
			get{ return bolLoadingFlag ;}
		}

		/// <summary>
		/// 对象标签名称,返回"html"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.HTML ; }
		}

        public HTMLElementList AllStyles
        {
            get
            {
                HTMLElementList list = new HTMLElementList();
                foreach (HTMLElement element in this.AllElements )
                {
                    if (element is HTMLStyleElement)
                    {
                        list.Add(element);
                    }
                }
                return list;
            }
        }
		/// <summary>
		/// 返回包含所有内容的元素列表
		/// </summary>
		public HTMLElementList AllElements
		{
			get
			{
				HTMLElementList list = new HTMLElementList();
				this.InnerGetAllElement( list );
				return list ;
			}
		}
		/// <summary>
		/// 获得指定编号的元素对象,ID号区分大小写
		/// </summary>
		/// <param name="vID">指定的ID号</param>
		/// <returns>找到的指定ID号的元素,若未找到则返回空引用</returns>
		public HTMLElement GetElementById( string vID)
		{
			if( this.Body != null)
				return this.Body.InnerGetElementById( vID );
			else
				return null;
		}
		/// <summary>
		/// 获得指定名称的元素对象
		/// </summary>
		/// <param name="vName">元素名称</param>
		/// <returns>保存找到的元素的列表对象</returns>
		public HTMLElementList GetElementsByName( string vName)
		{
			HTMLElementList myList = new HTMLElementList();
			if( this.Body != null)
				this.Body.InnerGetElementsByName( vName , myList );
			return myList ;
		}
		/// <summary>
		/// 获得指定标签名称的元素对象
		/// </summary>
		/// <param name="vName">元素标签名称</param>
		/// <returns>保存找到的元素的列表对象</returns>
		public HTMLElementList GetElementsByTagName( string vName)
		{
			HTMLElementList myList = new HTMLElementList();
			if( this.Body != null)
				this.Body.InnerGetElementsByTagName( vName , myList );
			return myList ;
		}
		/// <summary>
		/// 文档体
		/// </summary>
		public HTMLBodyElement Body
		{
			get
			{
				foreach( object o in myChildNodes )
					if( o is HTMLBodyElement )
						return ( HTMLBodyElement ) o ;
				return null;
			}
		}
		/// <summary>
		/// 文档头对象
		/// </summary>
		public HTMLHeadElement Head
		{
			get
			{
				foreach( object o in myChildNodes)
					if( o is HTMLHeadElement )
						return (HTMLHeadElement)o;
				return null;
			}
		}

		/// <summary>
		/// 文档标题
		/// </summary>
		public string Title
		{
			get
			{
				HTMLHeadElement vHead = this.Head ;
				if( vHead != null)
				{
					foreach( HTMLElement e in vHead.ChildNodes )
					{
						if( e is HTMLTitleElement )
							return e.Value ;
					}//foreach
				}
				return strLocation ;
			}
			set
			{
				HTMLHeadElement vHead = this.Head ;
				if( vHead != null)
				{
					foreach( HTMLElement e in vHead.ChildNodes )
					{
						if( e is HTMLTitleElement )
						{
							e.Value = value ;
							break;
						}
					}//foreach
				}//if
			}//set
		}
		/// <summary>
		/// 文档内所有的纯文本信息
		/// </summary>
		public override string InnerText
		{
			get
			{
				if( this.Body == null)
					return null;
				System.Text.StringBuilder myStr = new System.Text.StringBuilder();
				this.Body.GetDisplayText( myStr );
				string strText = myStr.ToString();
				strText = System.Web.HttpUtility.HtmlDecode( strText );
				return strText ;
			}
		}

//		public static string HtmlDecode( string strText )
//		{
//			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
//			int TextLen = strText.Length ;
//			for( int iCount = 0 ; iCount < TextLen ; iCount ++)
//			{
//				char c = strText[iCount];
//				if( c == '&')
//				{
//					int index = strText.IndexOf( ';', iCount + 1 );
//					if( index > 0 )
//					{
//						string strItem = strText.Substring( iCount + 1 , index - iCount - 1 );
//						if( strItem.Length > 0 )
//						{
//							if( strItem[0] == '#' )
//							{
//								try
//								{
//									if( strItem[1] == 'x' || strItem[1] == 'X')
//										c = ( char ) int.Parse( strItem.Substring( 2 ) , System.Globalization.NumberStyles.AllowHexSpecifier );
//									else
//										c = (char) int.Parse( strItem.Substring( 1 ));
//									myStr.Append( c );
//								}
//								catch
//								{
//									iCount ++ ;
//								}
//							}
//							else
//							{
//								iCount = index ;
//
//							}
//						}
//					}
//					else
//						myStr.Append( c );
//				}
//				else
//					myStr.Append( c );
//			}//for
//			return myStr.ToString();
//		}

		/// <summary>
		/// 已重载:HTML文档只接收 head , body , script, style 直属子元素
		/// </summary>
		/// <param name="strName"></param>
		/// <returns></returns>
		internal override bool CheckChildTagName(string strName)
		{
			return true;
		}

		private bool PrivateCheckChildTagName(string strName)
		{
			return strName == StringConstTagName.Head 
				|| strName == StringConstTagName.Body 
				|| strName == StringConstTagName.Script
				|| strName == StringConstTagName.Style
				|| strName == StringConstTagName.FrameSet
				|| strName == StringConstTagName.NoFrames 
				|| strName == StringConstTagName.NoScript
				|| strName == StringConstTagName.Comment 
				|| strName == StringConstTagName.XML ;
		}

		/// <summary>
		/// 从一个字节数组加载对象
		/// </summary>
		/// <param name="bsByte">字节数组</param>
		/// <returns>操作是否成功</returns>
		public bool LoadBinary( byte[] bsByte )
		{
			bool bolResult = false;
            this.CharSet = System.Text.Encoding.Default.WebName  ;
			System.Text.Encoding myEncod = System.Text.Encoding.GetEncoding( this.CharSet );
			string strHTML = myEncod.GetString( bsByte );
			try
			{
				bolResult =this.LoadHTML( strHTML );
			}
			catch( HTMLChangeCharSetException )
			{
				myEncod = System.Text.Encoding.GetEncoding( this.CharSet );
				strHTML = myEncod.GetString( bsByte );
				bolResult = this.LoadHTML( strHTML );
			}
			return bolResult ;
		}

        /// <summary>
        /// 从指定流中加载内容
        /// </summary>
        /// <param name="stream">流对象</param>
        /// <returns>操作是否成功</returns>
        public bool Load(System.IO.Stream stream)
        {
            this.Attributes.Clear();
            this.ChildNodes.Clear();
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            byte[] bs = new byte[1024];
            while (true)
            {
                int len = stream.Read(bs, 0, bs.Length);
                if (len <= 0)
                {
                    break;
                }
                else
                {
                    ms.Write(bs, 0, len);
                }
            }//while
            ms.Close();
            bs = ms.ToArray();
            return LoadBinary(bs);
        }


		/// <summary>
		/// 从指定的URL使用指定的编码格式加载文档
		/// </summary>
		/// <param name="strUrl">URL地址</param>
		/// <returns>操作是否成功</returns>
        public bool LoadUrl(string strUrl)
        {
            myAttributes.Clear();
            myChildNodes.Clear();
            Uri uri = new Uri(strUrl);
            byte[] bs = null;
            if (uri.Scheme == Uri.UriSchemeFile)
            {
                // 本地文件
                string fileName = uri.LocalPath;
                if (System.IO.File.Exists(fileName) == false)
                {
                    throw new System.IO.FileNotFoundException(fileName);
                }
                System.IO.FileInfo info = new System.IO.FileInfo(fileName);
                bs = new byte[info.Length];
                using (System.IO.FileStream stream = 
                    new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    stream.Read(bs, 0, bs.Length);
                }
            }
            else
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    bs = client.DownloadData(strUrl);
                }
            }
            if (bs == null || bs.Length == 0)
            {
                return false;
            }
            if (this.LoadBinary(bs))
            {
                if (strUrl.EndsWith("/"))
                {
                    strBaseURL = strUrl;
                }
                else
                {
                    int index = strUrl.LastIndexOf("/");
                    strBaseURL = strUrl.Substring(0, index + 1);
                    if (string.Compare(strBaseURL, "http://", true) == 0
                        || string.Compare(strBaseURL, "https://", true) == 0
                        || string.Compare(strBaseURL, "ftp://", true) == 0)
                    {
                        strBaseURL = strUrl;
                    }
                    if (strBaseURL.EndsWith("/") == false)
                    {
                        strBaseURL = strBaseURL + "/";
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

		/// <summary>
		/// 根据文档基础路径和指定的相对路径获得绝对路径
		/// </summary>
		/// <param name="strURL">相对路径</param>
		/// <returns>绝对路径</returns>
		public string GetAbsoluteURL( string strURL )
		{
			string url2 = strURL.ToLower();
			if( url2.StartsWith("http://") || url2.StartsWith("https://") || url2.StartsWith("ftp://"))
				return CombineUrl( strURL );
			else
				return CombineUrl( strBaseURL + strURL );
		}

		/// <summary>
		/// 内部方法
		/// </summary>
		/// <param name="strURL"></param>
		/// <returns></returns>
		public static string CombineUrl( string strURL )
		{
			int index = strURL.IndexOf("//");
			string strPrefix = null;
			if( index > 0 )
				strPrefix = strURL.Substring( 0 , index + 2 );
            if (index >= 0)
            {
                strURL = strURL.Substring(index + 2);
            }
			string[] strItems = strURL.Split("/\\".ToCharArray());
			System.Collections.ArrayList myItems = new System.Collections.ArrayList();
			foreach( string strItem in strItems )
			{
				if( strItem.Trim().Length != 0 )
					myItems.Add( strItem );
			}
			for(int iCount = 0 ; iCount < myItems.Count ; iCount ++)
			{
				if( "..".Equals( myItems[iCount]))
				{
					if( iCount > 0 )
					{
						myItems.RemoveAt( iCount );
						myItems.RemoveAt( iCount -1 );
						iCount -= 2 ;
					}
				}
			}//for
			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
			foreach( string strItem in myItems )
			{
				if( myStr.Length > 0 )
					myStr.Append("/");
				myStr.Append( strItem );
			}
			if( strPrefix != null )
				myStr.Insert( 0 , strPrefix );
			return myStr.ToString();
		}
//
//		/// <summary>
//		/// 从指定的URL使用指定的编码格式加载文档
//		/// </summary>
//		/// <param name="strUrl">URL地址</param>
//		/// <param name="encoding">文本编码格式</param>
//		/// <returns>操作是否成功</returns>
//		public bool LoadUrl( string strUrl , System.Text.Encoding encoding )
//		{
//			myAttributes.Clear();
//			myChildNodes.Clear();
//			string strHTML = null;
//			System.Net.HttpWebRequest myReq = ( System.Net.HttpWebRequest ) System.Net.WebRequest.Create( strUrl );
//			myReq.Method = "GET" ;
//			System.Net.WebResponse myResp = myReq.GetResponse();
//			System.IO.Stream myStream = myResp.GetResponseStream();
//			System.IO.MemoryStream myOutStream = new System.IO.MemoryStream();
//			byte[] bs = new byte[ 512 ];
//			while( true )
//			{
//				int len = myStream.Read( bs , 0 , bs.Length );
//				if( len == 0 )
//					break;
//				myOutStream.Write( bs , 0 , len );
//			}
//			myStream.Close();
//			myResp.Close();
//			myReq.Abort();
//
//			bs = myOutStream.ToArray();
//			myOutStream.Close();
//			if( bs == null && bs.Length == 0 )
//				return false;
//			strHTML = encoding.GetString( bs );
//			return this.LoadHTML( strHTML );
//		}

		/// <summary>
		/// 加载指定的HTML文档
		/// </summary>
		/// <param name="strFile">HTML文件名</param>
		/// <returns>操作是否成功</returns>
		public bool Load( string strFile)
		{
			bolLoadingFlag = true;
			System.IO.StreamReader myReader = new System.IO.StreamReader( strFile , System.Text.Encoding.Default );
			string strText = myReader.ReadToEnd();
			myReader.Close();
			HTMLTextReader TxtReader = new HTMLTextReader( strText );
			bool bolResult = Read( TxtReader );
			bolLoadingFlag = false;
			strLocation = strFile ;
			return bolResult ;
		}
		/// <summary>
		/// 加载指定的HTML字符串
		/// </summary>
		/// <param name="strHTML">HTML字符串</param>
		/// <returns>操作是否成功</returns>
		public bool LoadHTML( string strHTML)
		{
			bolLoadingFlag = true;
			HTMLTextReader TxtReader = new HTMLTextReader( strHTML );
			bool bolResult = Read( TxtReader );
			bolLoadingFlag = false;
			return bolResult ;
		}

		/// <summary>
		/// 文档字符编码格式
		/// </summary>
		private string strCharSet = "gb2312";
		/// <summary>
		/// 文档字符编码格式
		/// </summary>
		public string CharSet
		{
			get{ return strCharSet ;}
			set{ strCharSet = value;}
		}
		
		/// <summary>
		/// 添加子元素
		/// </summary>
		/// <param name="e">要添加的子元素</param>
		/// <returns>操作是否成功</returns>
		public override bool AppendChild(HTMLElement e)
		{
			if( e == this )
				return true;
			if( this.PrivateCheckChildTagName( e.TagName ))
				return base.AppendChild (e);
			else
			{
				foreach( HTMLElement element in myChildNodes )
				{
					if( element is HTMLContainer )
					{
						HTMLContainer c= ( HTMLContainer ) element ;
						if( c.InnerAppendChild( e ))
							return true;
					}
				}
			}
			return false;
		}


 		/// <summary>
		/// 已重载:从HTML文本读取器读取数据加载对象
		/// </summary>
		/// <param name="myReader"></param>
		/// <returns></returns>
		internal override bool Read( HTMLTextReader myReader)
		{
			myAttributes.Clear();
			myChildNodes.Clear();
			HTMLElement element = new HTMLHeadElement();
			element.Parent = this ;
			element.OwnerDocument = this ;
			myChildNodes.Add( element );

			element = new HTMLBodyElement();
			element.Parent = this ;
			element.OwnerDocument = this ;
			myChildNodes.Add( element );
			return base.InnerRead( myReader );
//
//			myReader.ReadUntil("<html");
//			myReader.MoveStep(5);
//			return base.Read( myReader );
		}

		/// <summary>
		/// 将对象数据输出到一个XML文件中
		/// </summary>
		/// <param name="strFileName">XML文件名</param>
		/// <returns>操作是否成功</returns>
		public bool Write( string strFileName )
		{
			using( System.IO.FileStream myStream = new System.IO.FileStream( strFileName , System.IO.FileMode.Create , System.IO.FileAccess.Write ))
			{
				System.Xml.XmlTextWriter myWriter = new System.Xml.XmlTextWriter( myStream , System.Text.Encoding.UTF8 );
				myWriter.Indentation = 4 ;
				myWriter.IndentChar = ' ';
				myWriter.Formatting = System.Xml.Formatting.Indented ;
				bool bolResult = Write( myWriter );
				myWriter.Close();
				return bolResult ;
			}
		}

		public System.Xml.XmlDocument CreateXMLDocument()
		{
			System.IO.StringWriter writer = new System.IO.StringWriter();
			System.Xml.XmlTextWriter xmlwriter = new System.Xml.XmlTextWriter( writer );
			this.Write( xmlwriter );
			xmlwriter.Close();
			string xml = writer.ToString();
			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
			doc.LoadXml( xml );
			return doc ;
		}
		/// <summary>
		/// 将对象数据输出到一个XML书写器中
		/// </summary>
		/// <remarks>若设置了WriteOptions.DocumentOutputXSL则会输出
		/// ＜xsl:stylesheet version="1.0"＞＜xsl:template match="/*"＞标记 </remarks>
		/// <param name="myWriter">XML书写器</param>
		/// <returns>操作是否成功</returns>
		public override bool Write(System.Xml.XmlWriter myWriter)
		{
			if( this.myWriteOptions.DocumentOutputXSL  )
			{
				myWriter.WriteStartElement( StringConstXSLT.Stylesheet );
				myWriter.WriteAttributeString( StringConstXSLT.NSName , StringConstXSLT.NSValue );
				myWriter.WriteAttributeString( StringConstXSLT.Version , "1.0");
 
				myWriter.WriteStartElement( StringConstXSLT.Template );
				myWriter.WriteAttributeString( StringConstXSLT.Match , "/*");
				bool bolResult = base.Write( myWriter );
				myWriter.WriteEndElement();
				myWriter.WriteEndElement();
				return bolResult ;
			}
			else
				return base.Write( myWriter );
		}


		/// <summary>
		/// 根据标签名称创建HTML元素对象
		/// </summary>
		/// <param name="TagName">标签名称</param>
		/// <returns>HTML元素对象</returns>
		public HTMLElement CreateElement( string TagName )
		{
			return InnerCreateElement( TagName , null);
		}
		/// <summary>
		/// 创建元素对象实例
		/// </summary>
		/// <param name="TagName">HTML标签名称</param>
		/// <param name="vParent">创建的元素的父节点</param>
		/// <returns>创建的HTML元素对象</returns>
		internal HTMLElement InnerCreateElement( string TagName , HTMLContainer vParent )
		{
			if( HTMLTextReader.isBlankString( TagName )  )
				return null;
			HTMLElement NewElement = null ;
			TagName = TagName.ToLower().Trim();
			switch( TagName )
			{
				case StringConstTagName.HTML :
					if( vParent == null || vParent == this )
						NewElement = this ;
					else
						NewElement = new HTMLDivElement();
					break;
				case StringConstTagName.Head :
					if( vParent is HTMLDocument )
					{
						NewElement = this.Head ;
						if( NewElement == null)
							NewElement = new HTMLHeadElement();
					}
					break;
				case StringConstTagName.Body :
					if( vParent is HTMLDocument )
					{
						NewElement = this.Body ;
						if( NewElement == null )
							NewElement = new HTMLBodyElement();
					}
					else
					{
						NewElement = new HTMLDivElement();
					}
					break;
				case StringConstTagName.IEDevicerect :
					NewElement = new HTMLIEDeviceRect();
					break;
				case StringConstTagName.IEHeaderFooter :
					NewElement = new HTMLIEHeaderfooter();
					break;
				case StringConstTagName.IELayoutrect :
					NewElement = new HTMLIELayoutrect();
					break;
				case StringConstTagName.IETemplatePrinter :
					NewElement = new HTMLIETemplateprinter();
					break;
				case StringConstTagName.H1 :
					NewElement = new HTMLHnElement();
					((HTMLHnElement) NewElement ).Level = 1 ;
					break;
				case StringConstTagName.H2 :
					NewElement = new HTMLHnElement();
					((HTMLHnElement) NewElement ).Level = 2 ;
					break;
				case StringConstTagName.H3 :
					NewElement = new HTMLHnElement();
					((HTMLHnElement) NewElement ).Level = 3 ;
					break;
				case StringConstTagName.H4 :
					NewElement = new HTMLHnElement();
					((HTMLHnElement) NewElement ).Level = 4 ;
					break;
				case StringConstTagName.H5 :
					NewElement = new HTMLHnElement();
					((HTMLHnElement) NewElement ).Level = 5 ;
					break;
				case StringConstTagName.H6 :
					NewElement = new HTMLHnElement();
					((HTMLHnElement) NewElement ).Level = 6 ;
					break;
				
				case StringConstTagName.Input :
					NewElement = new HTMLInputElement();
					break;
				case StringConstTagName.TextArea :
					NewElement = new HTMLTextAreaElement();
					break;
				case StringConstTagName.Select :
					NewElement = new HTMLSelectElement();
					break;
				case StringConstTagName.Option :
					NewElement = new HTMLOptionElement();
					break;
				case StringConstTagName.Form :
					NewElement = new HTMLFormElement();
					break;
				case StringConstTagName.A :
					NewElement = new HTMLAElement();
					break ;
				case StringConstTagName.B :
					NewElement = new HTMLBElement();
					break;
				case StringConstTagName.Pre :
					NewElement = new HTMLPreElement();
					break;
				case StringConstTagName.Span :
					NewElement = new HTMLSpanElement();
					break;
				case StringConstTagName.Div :
					NewElement = new HTMLDivElement();
					break;
				case StringConstTagName.P :
					NewElement = new HTMLPElement();
					break;
				case StringConstTagName.Br :
					NewElement = new HTMLBRElement();
					break;
				case StringConstTagName.Applet :
					NewElement = new HTMLAppletElement();
					break;
				case StringConstTagName.TextNode :
					NewElement = new HTMLTextNodeElement();
					break;
				case StringConstTagName.Object :
					NewElement = new HTMLObjectElement();
					break;
				case StringConstTagName.Script :
					NewElement = new HTMLScriptElement();
					break;
				case StringConstTagName.Link :
					NewElement = new HTMLLinkElement();
					break;
				case StringConstTagName.Font :
					NewElement = new HTMLFontElement();
					break;
				case StringConstTagName.Meta :
					NewElement = new HTMLMetaElement();
					break;
				case StringConstTagName.BGSound :
					NewElement = new HTMLBGSoundElement();
					break;
				case StringConstTagName.Param :
					NewElement = new HTMLParamElement();
					break;
				case StringConstTagName.Comment :
					NewElement = new HTMLCommentElement();
					break;
				case StringConstTagName.Hr :
					NewElement = new HTMLHRElement();
					break;
				case StringConstTagName.Table :
					NewElement = new HTMLTableElement();
					break;
				case StringConstTagName.TBody :
					NewElement = new HTMLTBodyElement();
					break;
				case StringConstTagName.Tr :
					NewElement = new HTMLTRElement();
					break;
				case StringConstTagName.Td :
					NewElement = new HTMLTDElement();
					break;
				case StringConstTagName.Col :
					NewElement = new HTMLColElement();
					break;
				case StringConstTagName.Style :
					NewElement = new HTMLStyleElement();
					break;
				case StringConstTagName.Img :
					NewElement = new HTMLImgElement();
					break;
				case StringConstTagName.Title :
					NewElement = new HTMLTitleElement();
					break;
				case StringConstTagName.UL :
					NewElement = new HTMLULElement();
					break;
				case StringConstTagName.LI :
					NewElement = new HTMLLIElement();
					break;
				case StringConstTagName.Map :
					NewElement = new HTMLMapElement();
					break;
				case StringConstTagName.Area :
					NewElement = new HTMLAreaElement();
					break;
				case StringConstTagName.HTAApplication :
					NewElement = new HTMLHTAApplicationElement();
					break;
				case StringConstTagName.FrameSet :
					NewElement = new HTMLFrameSetElement();
					break;
				case StringConstTagName.Frame :
					NewElement = new HTMLFrameElement();
					break;
				case StringConstTagName.Label :
					NewElement = new HTMLLabelElement();
					break;
				case StringConstTagName.Marquee :
					NewElement = new HTMLMarqueeElement();
					break;
				case StringConstTagName.IFrame :
					NewElement = new HTMLIFrameElement();
					break;
				case StringConstTagName.XML :
					NewElement = new HTMLXMLElement();
					break;
				case StringConstTagName.Sup :
					NewElement = new HTMLSupElement();
					break;
				case StringConstTagName.Sub :
					NewElement = new HTMLSubElement();
					break;
				case StringConstTagName.Nobr :
					NewElement = new HTMLNobarElement();
					break;
				default:
					NewElement = new HTMLUnknowElement();
					( ( HTMLUnknowElement) NewElement ).SetTagName( TagName );
					break;
			}
			if( NewElement != null)
			{
				NewElement.OwnerDocument = this ;
				NewElement.Parent = vParent ;
			}
			return NewElement ;
		}
	}//public class HTMLDocument : HTMLContainer
}