/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using DCSoft.Printing ;
using DCSoft.WinForms;


using DCSoft.CSharpWriter.Dom.Undo ;
using DCSoft.CSharpWriter.RTF;

using System.Xml.Serialization;

using System.ComponentModel;
using DCSoft.Drawing;
using DCSoft.Common;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using DCSoft.CSharpWriter.Commands;
using System.IO;
using DCSoft.CSharpWriter.Data;
using System.Text;
using DCSoft.CSharpWriter.Script;
using DCSoft.CSharpWriter.Security;


namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 文本文档对象
	/// </summary>
	/// <remarks>
	/// XTextDocument 是文本文档对象模型的顶级对象,是外部程序访问文档对象模型的入口点,
	/// 它包含了一些控制文本文档整体的成员.包括文档对象的组织,文档视图内容的绘制,用户界面事件处理.
	/// 编制 袁永福 2012-4-12
	/// </remarks>
    [System.Xml.Serialization.XmlType("XTextDocument")]
    [Serializable]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class DomDocument : DomContainerElement, IPageDocument 
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public DomDocument()
		{
            base.OwnerDocument = this ;
            base.Parent = this;
            this.AppendChildElement(new XTextDocumentHeaderElement());
            this.AppendChildElement(new XTextDocumentBodyElement());
            this.AppendChildElement(new XTextDocumentFooterElement());
            _UndoList = new XTextDocumentUndoList( this );
			this.ContentStyles.Default.Spacing = 1;
            this.ContentStyles.Default.LineSpacing = 4;
            this.ContentStyles.Default.SpacingBeforeParagraph = 9f;
            //this.Elements.Add(this.CreateParagraphEOF());
		}


        #region 一些属性无效 ***********************************************
        /// <summary>
        /// 方法无效
        /// </summary>
        private new void EditorDelete(bool logUndo)
        {
            throw new NotSupportedException("EditorDelete");
        }

        
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        new public DomDocumentContentElement DocumentContentElement { get { return null; } }
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        new public DomContainerElement ContentElement { get { return null; } }
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        new public DomContentLine OwnerLine { get { return null; } }
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        new public DomParagraphFlagElement OwnerParagraphEOF { get { return null; } }
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        new public DomElement PreviousElement { get { return null; } }
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        new public DocumentContentStyle RuntimeStyle { get { throw new NotSupportedException("Document.RuntimeStyle"); } }
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        new public DocumentContentStyle Style {  get { throw new NotSupportedException("Document.Style"); } }
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        [System.Xml.Serialization.XmlIgnore]
        new private int StyleIndex { get { throw new NotSupportedException("Document.StyleIndex"); }}
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        new public int ViewIndex { get { return 0; } }
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        new public int ColumnIndex { get { return 0; } }
        [System.ComponentModel.Browsable(false)]
        [System.Obsolete()]
        new public int ElementIndex { get { return 0; } }
        
        #endregion

        #region VBA脚本相关的代码

        private string _ScriptText = null;
        /// <summary>
        /// VBA脚本代码
        /// </summary>
        [DefaultValue( null)]
        [Category("Behavior")]
        public string ScriptText
        {
            get
            {
                return _ScriptText; 
            }
            set
            {
                _ScriptText = value; 
            }
        }

        [NonSerialized]
        private DocumentScriptEngine _ScriptEngine = null;
        /// <summary>
        /// 脚本引擎
        /// </summary>
        [Browsable( false )]
        [System.Xml.Serialization.XmlIgnore]
        public DocumentScriptEngine ScriptEngine
        {
            get
            {
                if (_ScriptEngine == null)
                {
                    _ScriptEngine = new DocumentScriptEngine();
                    _ScriptEngine.Document = this;
                }
                return _ScriptEngine; 
            }
            set
            {
                _ScriptEngine = value; 
            }
        }

        public string GetDebugText()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("Title:" + this.Info.Title);
            str.AppendLine("FileName:" + this.FileName);
            str.AppendLine("Body Elements:" + (this.Body.Elements == null ? "NULL" : this.Body.Elements.Count.ToString()));
            string txt = this.BodyText;
            if (txt != null && txt.Length > 100)
            {
                txt = txt.Substring(0, 100);
                txt = txt.Replace("\r", " ");
                txt = txt.Replace("\n", " ");
            }
            str.AppendLine("Body XElements:" + (this.Body.ElementsForSerialize == null ? "NULL" : this.Body.ElementsForSerialize.Count.ToString()));
            str.AppendLine("Body Text:" + txt );
            return str.ToString();
        }
 

        #endregion

        

        private bool _Initializing = false;

        /// <summary>
        /// 文档正在初始化中，某些操作不能执行
        /// </summary>
        [XmlIgnore()]
        [Browsable(false)]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public bool Initializing
        {
            get
            {
                return _Initializing; 
            }
            set 
            {
                _Initializing = value; 
            }
        }

        private string _SpecialTag = null;
        /// <summary>
        /// 特殊的附加数据,这种数据能保存在复制粘贴过程中生成的临时XML文档，当不包含在其保存的XML文档。
        /// </summary>
        /// <remarks>
        /// 这是一种特殊的附加数据,可用于复制粘贴操作时的额外处理,由于复制粘贴操作中
        /// 被复制的文档内容会创建一个小的文档复制品然后进行XML序列化,此时该值也会
        /// 随着被包含进去,粘贴时就会跟着还原出来,此时可用于额外控制.
        /// 例如，若设置本标记为病历号，则可以通过这个属性来实现不同病历之间数据不能复制粘贴的功能。
        /// </remarks>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlElement]
        public string SpecialTag
        {
            get
            {
                return _SpecialTag; 
            }
            set
            {
                _SpecialTag = value; 
            }
        }

        /// <summary>
        /// 当前编辑器组件版本号
        /// </summary>
        [Browsable( false )]
        public static Version CurrentEditorVersion
        {
            get
            {
                return typeof(DomDocument).Assembly.GetName().Version;
            }
        }

        private Version _EditorVersion = new Version();
        /// <summary>
        /// 最后编辑该文档使用的编辑器组件的版本号
        /// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        public Version EditorVersion
        {
            get
            {
                return _EditorVersion; 
            }
            set
            {
                _EditorVersion = value; 
            }
        }
         
        [XmlAttribute]
        [Category("Data")]
        public string EditorVersionString
        {
            get
            {
                return _EditorVersion.ToString(); 
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _EditorVersion = new Version();
                }
                else
                {
                    _EditorVersion = new Version(value);
                }
            }
        }

        [NonSerialized()]
        private DocumentOptions _Options = null;
        /// <summary>
        /// 文档相关的配置项目,该对象不参与二进制和XML序列化.
        /// </summary>
        [XmlIgnore()]
        [Browsable( false )]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden )]
        public DocumentOptions Options
        {
            get
            {
                if (_Options == null)
                {
                    _Options = new DocumentOptions();
                    //_Options.LoadConfig();
                }
                return _Options; 
            }
            set
            {
                _Options = value; 
            }
        }

        private DocumentInfo _Info = new DocumentInfo();
        /// <summary>
        /// 文档相关信息
        /// </summary>
        public DocumentInfo Info
        {
            get
            {
                return _Info;
            }
            set 
            { 
                _Info = value;
            }
        }

		/// <summary>
		/// 对象所属文档就是自己
		/// </summary>
        [Browsable( false )]
        [XmlIgnore()]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden )]
		public override DomDocument OwnerDocument
		{
			get
			{
				return this ;
			}
			set
			{
			}
		}

		/// <summary>
		/// 文档对象没有父节点
		/// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public override DomContainerElement Parent
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		/// <summary>
		/// 文档正处于打印状态
		/// </summary>
        [NonSerialized()]
		internal bool _Printing = false;
		/// <summary>
		/// 文档正处于打印状态
		/// </summary>
        [Browsable(false)]
        [XmlIgnore]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public bool Printing
		{
			get
            {
                return _Printing ;
            }
            set
            {
                _Printing = value;
            }
		}

		/// <summary>
		/// 文档内容修改标记
		/// </summary>
		[NonSerialized]
        private bool _Modified = false;
		/// <summary>
		/// 文档内容修改标记
		/// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public bool Modified
		{
			get
            {
                return _Modified ;
            }
			set
            {
                _Modified = value;
            }
		}

        private string _FileName = null;
        /// <summary>
        /// 文件名
        /// </summary>
        [XmlIgnore()]
        [ReadOnly( true )]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public string FileName
        {
            get
            {
                return _FileName; 
            }
            set
            {
                _FileName = value; 
            }
        }

        private string _BaseUrl = null;
        /// <summary>
        /// 基础URL路径
        /// </summary>
        [XmlIgnore()]
        [ReadOnly(true)]
        [Browsable(false )]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public string BaseUrl
        {
            get
            {
                return _BaseUrl; 
            }
            set
            {
                _BaseUrl = value; 
            }
        }

		#region 管理文档元素内容的成员群 **************************************

        [NonSerialized]
        private WriterAppHost _AppHost = null;
        /// <summary>
        /// 应用程序宿主
        /// </summary>
        /// <remarks>
        /// 本属性首先使用文档对象自己的宿主对象，若没有则使用文档对象绑定的编
        /// 辑器控件的宿主对象，若没有则使用默认宿主对象。</remarks>
        [Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public WriterAppHost AppHost
        {
            get
            {
                if (_AppHost != null)
                {
                    return _AppHost;
                }
                else if (this.EditorControl != null)
                {
                    return this.EditorControl.AppHost;
                }
                else
                {
                    return WriterAppHost.Default;
                }
            }
            set
            {
                _AppHost = value; 
            }
        }

        [NonSerialized]
        private DocumentControler _DocumentControler = null;
        /// <summary>
        /// 文档控制器
        /// </summary>
        /// <remarks>
        /// 本属性内部首先使用其绑定的编辑器控件的控制器，若没有使用
        /// 文档自己的控制器，若没有则创建一个新的控制器。</remarks>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public DocumentControler DocumentControler
        {
            get
            {
                if (this.EditorControl != null)
                {
                    return this.EditorControl.DocumentControler;
                }
                if (_DocumentControler == null)
                {
                    _DocumentControler = new DocumentControler();
                }
                _DocumentControler.Document = this;
                return _DocumentControler; 
            }
            set
            {
                _DocumentControler = value; 
            }
        }

        /// <summary>
        /// 更新文档元素的状态，包括OwernDocument、Parent、Content、PrivateContent属性
        /// </summary>
        public void UpdateElementState()
        { 
            this.Enumerate( delegate( object sender , ElementEnumerateEventArgs args )
                {
                    DomElement element = args.Element;
                    element.Parent = args.Parent;
                    element.OwnerDocument = this;
                    if (element is DomContentElement)
                    {
                        ((DomContentElement)element).UpdateContentElements(false);
                    }
                } ,
                false );
        }

        /// <summary>
        /// 页眉对象
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.ComponentModel.DefaultValue( null)]
        [System.Xml.Serialization.XmlIgnore()]
        public XTextDocumentHeaderElement Header
        {
            get
            {
                foreach (DomElement element in this.Elements)
                {
                    if (element is XTextDocumentHeaderElement)
                    {
                        return (XTextDocumentHeaderElement)element;
                    }
                }
                XTextDocumentHeaderElement header = new XTextDocumentHeaderElement();
                header.IsEmpty = true;
                //header.FixElements();
                this.AppendChildElement(header);
                //header.Elements.Add(new XTextParagraphFlagElement());
                return header;
            }
        }

        /// <summary>
        /// 文档正文对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DefaultValue(null)]
        [System.Xml.Serialization.XmlIgnore()]
        public XTextDocumentBodyElement Body
        {
            get
            {
                foreach (DomElement element in this.Elements)
                {
                    if (element is XTextDocumentBodyElement)
                    {
                        return (XTextDocumentBodyElement)element;
                    }
                }
                XTextDocumentBodyElement body = new XTextDocumentBodyElement();
                this.AppendChildElement(body);
                //body.FixElements();
                //body.Elements.Add(new XTextParagraphFlagElement());// this.CreateParagraphEOF());
                return body;
            }
        }

        /// <summary>
        /// 文档正文纯文本内容
        /// </summary>
        [Browsable( false )]
        [DefaultValue( null )]
        [XmlElement]
        public string BodyText
        {
            get
            {
                return this.Body.Text;
            }
            set
            {
            }
        }

        /// <summary>
        /// 页脚对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DefaultValue(null)]
        [System.Xml.Serialization.XmlIgnore()]
        public XTextDocumentFooterElement Footer
        {
            get
            {
                foreach (DomElement element in this.Elements)
                {
                    if (element is XTextDocumentFooterElement)
                    {
                        return (XTextDocumentFooterElement)element;
                    }
                }
                XTextDocumentFooterElement footer = new XTextDocumentFooterElement();
                //footer.Elements.Add(this.CreateParagraphEOF());
                footer.IsEmpty = true;
                this.AppendChildElement(footer);
                //footer.Elements.Add(new XTextParagraphFlagElement());
                return footer;
            }
        }

        [NonSerialized()]
        private DomDocumentContentElement _CurrentContentElement = null;
        /// <summary>
        /// 当前插入点所在的文档内容块对象，它是Header,Body或Footer中的某个。
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DefaultValue(null)]
        [System.Xml.Serialization.XmlIgnore()]
        public DomDocumentContentElement CurrentContentElement
        {
            get
            {
                if (_CurrentContentElement == null)
                {
                    _CurrentContentElement = this.Body;
                }
                return _CurrentContentElement; 
            }
            set 
            {
                _CurrentContentElement = value; 
            }
        }

        /// <summary>
        /// 当前文档内容模块样式
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public PageContentPartyStyle CurrentContentPartyStyle
        {
            get
            {
                if (this.CurrentContentElement == this.Header)
                {
                    return PageContentPartyStyle.Header;
                }
                else if (this.CurrentContentElement == this.Footer)
                {
                    return PageContentPartyStyle.Footer;
                }
                return PageContentPartyStyle.Body;
            }
        }

        /// <summary>
        /// 当前内容
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public DomContent Content
        {
            get
            {
                return this.CurrentContentElement.Content;
            }
        }

        /// <summary>
        /// 当前被选择的内容
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public DomSelection Selection
        {
            get
            {
                return this.CurrentContentElement.Selection;
            }
        }
         
        /// <summary>
        /// 当前元素
        /// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public DomElement CurrentElement
        {
            get
            {
                return this.CurrentContentElement.CurrentElement ;
            }
        }

        /// <summary>
        /// 获得指定类型的当前文档元素
        /// </summary>
        /// <param name="elementType">指定的文档元素类型</param>
        /// <returns>获得的文档元素对象</returns>
        public DomElement GetCurrentElement(Type elementType)
        {
            if (this.CurrentElement == null)
            {
                return null;
            }
            else
            {
                return this.CurrentElement.GetOwnerParent(elementType, true);
            }
        }

        /// <summary>
        /// 获得当前元素具有相同样式的区域
        /// </summary>
        /// <returns>获得的区域</returns>
        public ContentRange GetCurrentRangeWithSameStyle()
        {
            return CreateRange(this.CurrentElement, delegate(object obj1, object obj2)
                {
                    DomElement e1 = (DomElement)obj1;
                    DomElement e2 = (DomElement)obj2;
                    if (e1.GetType() == e2.GetType()
                        && e1.StyleIndex == e2.StyleIndex)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                });
        }

        /// <summary>
        /// 获得指定元素前后相邻的元素对象，这些元素的某些方面相同。
        /// </summary>
        /// <param name="element">指定的元素</param>
        /// <param name="callBack">比较对象的委托</param>
        /// <returns></returns>
        public ContentRange CreateRange(DomElement element , CompareHandler callBack )
        {
            DomDocumentContentElement ce = element.DocumentContentElement;
            DomContent content = ce.Content;
            int index = content.IndexOf(element);
            int startIndex = index;
            int endIndex = index;
            //向前搜索
            for( int iCount = index ;iCount > 0;iCount -- )
            {
                if( callBack( element , content[ iCount ] ) == 0 )
                {
                    startIndex = iCount ;
                }
                else
                {
                    break;
                }
            }//for
            // 向后搜索
            for (int iCount = index; iCount < content.Count; iCount++)
            {
                if (callBack(element, content[iCount]) == 0)
                {
                    endIndex = iCount;
                }
                else
                {
                    break;
                }
            }//for
            return new ContentRange( 
                this.CurrentContentElement ,
                startIndex  ,
                endIndex - startIndex + 1 );
        }

        /// <summary>
        /// 当前段落对象
        /// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public DomParagraphFlagElement CurrentParagraphEOF
        {
            get
            {
                return this.CurrentContentElement.CurrentParagraphEOF;
            }
        }
 
		#endregion
         

        //[NonSerialized()]
        //internal XTextElementList myContentForSerialize = null;
        ///// <summary>
        ///// 为进行XML序列化而使用的元素列表,系统内部使用，外部不要使用。
        ///// </summary>
        //[Browsable(false)]
        //[XmlArray("Content")]
        //[System.Xml.Serialization.XmlArrayItem("String", typeof(XTextStringElement))]
        //[System.Xml.Serialization.XmlArrayItem("Char", typeof(XTextCharElement))]
        //[System.Xml.Serialization.XmlArrayItem("Image", typeof(XTextImageElement))]
        //[System.Xml.Serialization.XmlArrayItem("LineBreak", typeof(XTextLineBreakElement))]
        //[System.Xml.Serialization.XmlArrayItem("Object", typeof(XTextObjectElement))]
        //[System.Xml.Serialization.XmlArrayItem("Bookmark", typeof(XTextBookmark))]
        //[System.Xml.Serialization.XmlArrayItem("PFlag", typeof(XTextParagraphFlagElement))]
        //[XmlArrayItem("Paragraph" , typeof( XTextParagraphElement ))]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //[EditorBrowsable(EditorBrowsableState.Advanced)]
        //public XTextElementList ContentForSerialize
        //{
        //    get
        //    {
        //        if (myContentForSerialize == null)
        //        {
        //            myContentForSerialize = this.CreateParagraphs(this.Content);
        //        }
        //        return myContentForSerialize;
        //    }
        //    set
        //    {
        //        myContentForSerialize = value;
        //        if (myContentForSerialize != null)
        //        {
        //            myContentForSerialize.OwnerElement = this;
        //        }
        //    }
        //}
         
        /// <summary>
        /// 加载文档后的处理
        /// </summary>
        public override void AfterLoad(FileFormat format)
        {
            if (format == FileFormat.RTF)
            {
                this.Options.ViewOptions.RichTextBoxCompatibility = true;
            }
            else
            {
                this.Options.ViewOptions.RichTextBoxCompatibility = false;
            }
            foreach (DocumentContentStyle style in this.ContentStyles.Styles)
            {
                XDependencyObject.ApplyDefaultValuePropertyNames(
                    style,
                    style.DefaultValuePropertyNames);
            }
            this.ContentStyles.Styles.FixFontName();
            //WriterUtils.SplitElements(this.Elements, true);
            base.AfterLoad(format);
            this.FixDomState();

            
            //foreach (DocumentContentStyle style in this.ContentStyles.Styles)
            //{
            //    XFontValue f = style.Font;
            //    if (f.FixFontName())
            //    {
            //        // 修复错误的字体名称
            //        style.FontName = f.Name;
            //    }
            //}//foreach
            
            
            XTextDocumentBodyElement body = this.Body;
            //body.Elements.Clear();
            for (int iCount = this.Elements.Count - 1; iCount >= 0; iCount--)
            {
                DomElement element = this.Elements[iCount];
                if (element is DomDocumentContentElement)
                {
                }
                else
                {
                    this.Elements.RemoveAtRaw(iCount);
                    //body.AppendChildElement(element);
                    body.Elements.Insert(0, element);
                    element.Parent = body;
                    element.OwnerDocument = this;
                }
            }//for

            this.CurrentContentElement = this.Body;
            
            DomElement element2 = this.Header;
            element2 = this.Footer;
            
            foreach (DomDocumentContentElement element in this.Elements)
            {
                element.OwnerDocument = this.OwnerDocument;
                element.Parent = this;
                element.AfterLoad(format);
                element.FixElements();
                element.UpdateContentElements(true);
                element.SetSelection(0, 0);
            }//foreach

            // 触发文档加载事件
            this.OnDocumentLoad(EventArgs.Empty);
            // 触发所有元素的Load事件
            this.Enumerate(delegate(object sender, ElementEnumerateEventArgs args)
                {
                    if (args.Element.Events != null && args.Element.Events.HasLoad)
                    {
                        // 触发元素的Load事件
                        args.Element.Events.RaiseLod(this, EventArgs.Empty);
                    }
                },
                true);

            if (this.GlobalEvents != null && this.GlobalEvents.HasLoad)
            {
                // 触发全局文档加载事件
                this.GlobalEvents.RaiseLod(this, EventArgs.Empty);
            }
            // 触发所有容器元素的 ContentChanged事件
            this.Enumerate(delegate(object sender, ElementEnumerateEventArgs args)
                {
                    if (args.Element is DomContainerElement)
                    {
                        ContentChangedEventArgs args2 = new ContentChangedEventArgs();
                        args2.Document= this ;
                        args2.ElementIndex = 0;
                        args2.Element = (DomContainerElement)args.Element;
                        args2.LoadingDocument = true;
                        DomContainerElement c = (DomContainerElement)args.Element;
                        c.OnContentChanged( args2);
                    }
                },
                false);
        }

        /// <summary>
        /// 文档加载事件
        /// </summary>
        [field:NonSerialized]
        public event EventHandler DocumentLoad = null;
        /// <summary>
        /// 触发文档的DocumentLoad事件。本方法内部还会调用文档绑定的编辑器控件的OnDocumentLoad方法。
        /// </summary>
        /// <param name="args">事件参数</param>
        public virtual void OnDocumentLoad(EventArgs args)
        {
            // 触发脚本
            if (this.Options.BehaviorOptions.EnableScript)
            {
                this.ScriptEngine.ExecuteSub(
                    this,
                    DocumentScriptEngine.Document_DocumentLoad);
            }

            if (DocumentLoad != null)
            {
                DocumentLoad(this, args);
            }
            if (this.EditorControl != null)
            {
                this.EditorControl.OnDocumentLoad(args);
            }
        }

		public virtual void OnLinkClick( DomElement sender ,string link )
		{
			if( this.EditorControl != null )
			{
				this.EditorControl.OnLinkClick(sender , link );
			}
		}

        /// <summary>
        /// 文档内容发生改变事件,当用户修改了文档的任何内容时就会触发该事件。
        /// </summary>
        [field:NonSerialized]
        public event System.EventHandler DocumentContentChanged = null;
        /// <summary>
        /// 触发文档内容发生改变事件.本方法内部还会调用文档对象绑定的编辑器控件的OnDocumentContentChanged方法。
        /// </summary>
        /// <param name="args"></param>
        public virtual void OnDocumentContentChanged( )
        {
            // 触发脚本
            if (this.Options.BehaviorOptions.EnableScript)
            {
                this.ScriptEngine.ExecuteSub(
                    this,
                    DocumentScriptEngine.Document_DocumentContentChanged);
            }
            this._HoverElement = null;
            if (this.EditorControl != null)
            {
                this.EditorControl.OnDocumentContentChanged(EventArgs.Empty);
            }
            if (DocumentContentChanged != null)
            {
                DocumentContentChanged(this, EventArgs.Empty );
            }
        }

        /// <summary>
        /// 文档选择状态发生改变后的事件,包括选择区域改变或插入点位置的改变。
        /// </summary>
        [field:NonSerialized]
        public event EventHandler SelectionChanged = null;
        /// <summary>
        /// 文档内容状态发生改变处理,本方法还会调用文档对象绑定的编辑器控件的OnSelectionChanged方法。
        /// </summary>
        public void OnSelectionChanged( )
        {
            // 触发脚本
            if (this.Options.BehaviorOptions.EnableScript)
            {
                this.ScriptEngine.ExecuteSub(
                    this,
                    DocumentScriptEngine.Document_SelectionChanged);
            }
            DomDocumentContentElement ce = this.CurrentContentElement;
            if (this.EditorControl != null)
            {
                this.EditorControl.OnSelectionChanged(EventArgs.Empty);
            }
            if (SelectionChanged != null)
            {
                SelectionChanged(this , EventArgs.Empty );
            }

        }

        ///// <summary>
        ///// 只触发选择内容发生改变事件,但不更新文档的一些信息
        ///// </summary>
        //public virtual void RaiseSelectedChangedEvent()
        //{
        //    if (_EditorControl != null)
        //    {
        //        _EditorControl.OnSelectionChanged(EventArgs.Empty);
        //    }
        //    if (SelectionChanged != null)
        //    {
        //        SelectionChanged(this, null);
        //    }
        //}

		/// <summary>
		/// 文档选择状态正在发生改变事件
		/// </summary>
        [field: NonSerialized]
        public event SelectionChangingEventHandler SelectionChanging = null;
        

		/// <summary>
        /// 文档内容选择状态发生改变处理,本方法还会调用文档绑定的编辑器控件的OnSelectionChanging方法。
		/// </summary>
        /// <param name="args">事件参数</param>
        public virtual void OnSelectionChanging( SelectionChangingEventArgs args )
		{
            DomDocumentContentElement ce = this.CurrentContentElement;
            if (this.EditorControl != null)
            {
                this.EditorControl.OnSelectionChanging( args );
            }
            if (SelectionChanging != null)
            {
                SelectionChanging(this, args );
            }
		}

        /// <summary>
        /// 处理编辑器控件获得焦点事件
        /// </summary>
        public virtual void OnControlGotFocus()
        {
            if (this.CurrentElement != null)
            {
                DocumentEventArgs args = new DocumentEventArgs(
                            this.OwnerDocument,
                            this.CurrentElement ,
                            DocumentEventStyles.GotFocus);
                this.BubbleHandleElementEvent(this.CurrentElement, args);
            }
        }

        /// <summary>
        /// 处理编辑控件失去焦点事件
        /// </summary>
        public virtual void OnControlLostFocus()
        {
            if (this.CurrentElement != null)
            {
                DocumentEventArgs args = new DocumentEventArgs(
                            this.OwnerDocument,
                            this.CurrentElement,
                            DocumentEventStyles.LostFocus);
                this.BubbleHandleElementEvent(this.CurrentElement, args);
            }
        }
         
		#region 加载和保存文档内容的成员群 ************************************

        /// <summary>
        /// 输出RTF文档
        /// </summary>
        /// <param name="writer">RTF文档书写器</param>
        public override void WriteRTF(DCSoft.CSharpWriter.RTF.RTFContentWriter writer)
        {
            writer.WriteStartDocument( this );
            writer.ClipRectangle = new RectangleF(0, 0, this.Body.Width, this.Body.Height);
            this.Body.WriteRTF(writer);
            writer.WriteEndDocument();
        }

        /// <summary>
        /// 正在进行反序列化操作的标记
        /// </summary>
        internal bool _Deserializing = true;

        /// <summary>
        /// 从另外一个文本文档复制文档内容
        /// </summary>
        /// <param name="sourceDocument">文档内容来源</param>
        /// <remarks>
        /// 本方法一般用于执行XML反序列化时，将文档内容从临时文档复制到本文档。
        /// </remarks>
        public virtual void CopyContent(DomDocument sourceDocument , bool copyElements )
        {
            if (sourceDocument != null)
            {
                if (copyElements)
                {
                    this.Elements.Clear();
                    foreach (DomElement element in sourceDocument.Elements)
                    {
                        this.Elements.Add(element);
                    }
                    if (sourceDocument._ElementsForSerialize != null)
                    {
                        this._ElementsForSerialize = sourceDocument._ElementsForSerialize.Clone();
                    }
                }
                if (sourceDocument.Attributes != null)
                {
                    this.Attributes = sourceDocument.Attributes.Clone();
                }
                 
                this._UndoList = null;
                this._CurrentContentElement = null;
                 this._HighlightManager = null;
                this._HoverElement = null;
                this._DocumentControler = null;
                //this._EditorControl = null;
                this._EditorCurrentStyle = null;
                this._GlobalEvents = null;
                this._GlobalPages = null;
                this._MouseCapture = null;
                this._PageIndex = 0;
                this._PageRefreshed = false;
                this._Pages = new PrintPageCollection();
                this._RawPageIndex = 0;
                this._ScriptEngine = null;
                //if( sourceDocument._UndoList != null )
                //{
                //    this._UndoList = new XTextDocumentUndoList( this );
                //}
                if (sourceDocument.Info != null)
                {
                    this.Info = sourceDocument.Info.Clone();
                }
                if (sourceDocument.PageSettings != null)
                {
                    this._PageSettings = sourceDocument._PageSettings.Clone();
                }
                if (sourceDocument._ContentStyles != null)
                {
                    this._ContentStyles =( DocumentContentStyleContainer ) sourceDocument._ContentStyles.Clone();
                }
                if (sourceDocument._UserHistories != null)
                {
                    this._UserHistories = sourceDocument._UserHistories.Clone();
                }
                if (sourceDocument._Options != null)
                {
                    this._Options = sourceDocument._Options.Clone();
                }
                this.ScriptText = sourceDocument.ScriptText;
            }
        }
        /// <summary>
        /// 以指定的格式从指定名称的文件中加载文档
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="format">文件格式</param>
        public virtual void Load(string fileName, FileFormat format)
        {
            long tickCount = 0;
            if (this.Options.BehaviorOptions.DebugMode)
            {
                tickCount = DCSoft.Common.CountDown.GetTickCountExt();
                System.Diagnostics.Debug.WriteLine( string.Format(
                    WriterStrings.Loading_FileName_Format , fileName , format ));
            }

            if (format == FileFormat.Text)
            {
                DocumentLoader.LoadTextFile(fileName, this);
            }
            else if (format == FileFormat.RTF)
            {
                DocumentLoader.LoadRTFFile(fileName, this);
            }
            else if (format == FileFormat.XML)
            {
                DocumentLoader.LoadXmlFile(fileName, this);
            }
            
            this.FileName = fileName;
            this.BaseUrl = WriterUtils.GetBaseURL(fileName);
            this.Modified = false;
            if (this.UndoList != null)
            {
                this.UndoList.Clear();
            }
            if (this.Options.BehaviorOptions.DebugMode)
            {
                tickCount = DCSoft.Common.CountDown.GetTickCountExt() - tickCount;
                System.Diagnostics.Debug.WriteLine("Load Tick Count:" +  Convert.ToString( ( double ) tickCount / 10000.0 ));
                System.Diagnostics.Debug.WriteLine("Document loaded:" + this.Info.Title);
                System.Diagnostics.Debug.WriteLine("File name      :" + this.FileName);
                System.Diagnostics.Debug.WriteLine("Creation time  :" + this.Info.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
                System.Diagnostics.Debug.WriteLine("Description    :" + this.Info.Description);
            }
            //this.OnSelectionChanged();
        }

        /// <summary>
        /// 以指定的格式从指定名称的文件中加载文档
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="format">文件格式</param>
        public virtual void Load( TextReader reader, FileFormat format)
        {
            long tickCount = 0;
            string fn = this.FileName;
            if (this.Options.BehaviorOptions.DebugMode)
            {
                tickCount = DCSoft.Common.CountDown.GetTickCountExt();
                System.Diagnostics.Debug.WriteLine(string.Format(
                    WriterStrings.Loading_FileName_Format, "TextReader", format));
            }

            if (format == FileFormat.Text)
            {
                DocumentLoader.LoadTextFile(reader, this);
            }
            else if (format == FileFormat.RTF)
            {
                DocumentLoader.LoadRTFFile(reader, this);
            }
            else if (format == FileFormat.XML)
            {
                DocumentLoader.LoadXmlFile(reader, this);
            }
            
            this.Modified = false;
            if (this.UndoList != null)
            {
                this.UndoList.Clear();
            }
            if (this.Options.BehaviorOptions.DebugMode)
            {
                tickCount = DCSoft.Common.CountDown.GetTickCountExt() - tickCount;
                System.Diagnostics.Debug.WriteLine("Load Tick Count:" + Convert.ToString((double)tickCount / 10000.0));
                System.Diagnostics.Debug.WriteLine("Document loaded:" + this.Info.Title);
                System.Diagnostics.Debug.WriteLine("File name      :" + fn);
                System.Diagnostics.Debug.WriteLine("Creation time  :" + this.Info.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
                System.Diagnostics.Debug.WriteLine("Description    :" + this.Info.Description);
            }
            //this.OnSelectionChanged();
        }

        /// <summary>
        /// 以指定的格式从文件流中加载文档。
        /// </summary>
        /// <param name="stream">文件流对象</param>
        /// <param name="format">指定的格式</param>
        public virtual void Load(System.IO.Stream stream, FileFormat format)
        {
            string fn = this.FileName;
            long tickCount = 0;
            if (this.Options.BehaviorOptions.DebugMode)
            {
                tickCount = DCSoft.Common.CountDown.GetTickCountExt();
                System.Diagnostics.Debug.WriteLine(string.Format(
                    WriterStrings.Loading_FileName_Format, "Stream", format));
            }
            if (format == FileFormat.Text)
            {
                DocumentLoader.LoadTextFile(stream, this);
            }
            else if (format == FileFormat.RTF)
            {
                DocumentLoader.LoadRTFFile(stream, this);
            }
            else if (format == FileFormat.XML)
            {
                DocumentLoader.LoadXmlFile(stream, this);
            }
            
            this.Modified = false;
            if (this.UndoList != null)
            {
                this.UndoList.Clear();
            }
            if (this.Options.BehaviorOptions.DebugMode)
            {
                tickCount = DCSoft.Common.CountDown.GetTickCountExt() - tickCount;
                System.Diagnostics.Debug.WriteLine("Document loaded:" + this.Info.Title);
                System.Diagnostics.Debug.WriteLine("Load Tick Count:" + Convert.ToString((double)tickCount / 10000.0));
                System.Diagnostics.Debug.WriteLine("File name      :" + fn);
                System.Diagnostics.Debug.WriteLine("Creation time  :" + this.Info.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
                System.Diagnostics.Debug.WriteLine("Description    :" + this.Info.Description);
            }
            //this.OnSelectionChanged();
        }

        /// <summary>
        /// 更新用户历史记录的时间
        /// </summary>
        public void UpdateUserInfoSaveTime()
        {
            if (this.UserHistories.Count > 0)
            {
                UserHistoryInfo info = this.UserHistories[this.UserHistories.Count - 1];
                info.SavedTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 以指定的格式将文档保存在文件中
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="format">文档格式</param>
        public virtual void Save(string fileName, FileFormat format)
        {
            UpdateUserInfoSaveTime();
            if (format == FileFormat.RTF)
            {
                DocumentSaver.SaveRTFFile(fileName, this);
            }
            else if (format == FileFormat.Text)
            {
                DocumentSaver.SaveTextFile(fileName, this);
            }
            else if (format == FileFormat.XML)
            {
                string back = this._SpecialTag;
                this._SpecialTag = null;
                try
                {
                    DocumentSaver.SaveXmlFile(fileName, this);
                }
                finally
                {
                    this._SpecialTag = back;
                }
            }
          
            this.FileName = fileName;
            this.Modified = false;
            this.OnSelectionChanged();
        }

        /// <summary>
        /// 以指定的格式将文档保存在文件流中。
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="format">文件格式</param>
        public virtual void Save(System.IO.Stream stream, FileFormat format)
        {
            if (format == FileFormat.RTF)
            {
                DocumentSaver.SaveRTFFile(stream, this);
            }
            else if (format == FileFormat.Text)
            {
                DocumentSaver.SaveTextFile(stream, this);
            }
            else if (format == FileFormat.XML)
            {
                string back = this._SpecialTag;
                this._SpecialTag = null;
                try
                {
                    DocumentSaver.SaveXmlFile(stream, this);
                }
                finally
                {
                    this._SpecialTag = back;
                }
            }
            this.Modified = false;
            this.OnSelectionChanged();
        }

        /// <summary>
        /// 以指定的格式将文档保存在文件流中。
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="format">文件格式</param>
        public virtual void Save(System.IO.TextWriter writer , FileFormat format)
        {
            if (format == FileFormat.RTF)
            {
                DocumentSaver.SaveRTFFile(writer, this);
            }
            else if (format == FileFormat.Text)
            {
                DocumentSaver.SaveTextFile(writer, this);
            }
            else if (format == FileFormat.XML)
            {
                string back = this._SpecialTag;
                this._SpecialTag = null;
                try
                {
                    DocumentSaver.SaveXmlFile(writer, this);
                }
                finally
                {
                    this._SpecialTag = back;
                }
            }
            this.Modified = false;
            this.OnSelectionChanged();
        }

		#endregion
         

        /// <summary>
        /// 创建画布对象
        /// </summary>
        /// <returns>创建的画布对象</returns>
		public virtual System.Drawing.Graphics CreateGraphics()
		{
            if (this.EditorControl == null || this.EditorControl.IsHandleCreated == false )
            {
                System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(new IntPtr(0));
                g.PageUnit = this._DocumentGraphicsUnit;
                return g;
            }
            else
            {
                return this.EditorControl.CreateViewGraphics();
            }
		}


		/// <summary>
		/// 清空文档内容
		/// </summary>
		public virtual void Clear()
		{
			this.Modified = false;
            //this.intHeaderHeight = 0 ;
            //this.intFooterHeight = 0 ;
            // 设置页面方式
            this.PageSettings = new XPageSettings();

            this.FileName = null;
            this.ContentStyles.Clear();
            //if (this.DefaultFont != null)
            //{
            //    this.ContentStyles.Default.Font = this.DefaultFont;
            //}
            this.ClearContent();
        	using( System.Drawing.Graphics g = this.CreateGraphics())
			{
				this.RefreshSize( g );
			}
			this.ExecuteLayout();
			this.OnSelectionChanged();
            this.OnDocumentContentChanged();
		}

        internal void ClearContent()
        {
            //myElements.Add( this.myEOFElement );
            if (this.UndoList != null)
            {
                this.UndoList.Clear();
            }
            if (this.UserHistories != null)
            {
                this.UserHistories.Clear();
            }
            // 清空脚本
            this.ScriptText = null;
            if (this.ScriptEngine != null)
            {
                this.ScriptEngine.Close();
                this.ScriptEngine = null;
            }

            this.ContentStyles.Clear();
            if (this.DefaultFont != null)
            {
                this.ContentStyles.Default.Font = this.DefaultFont;
            }
            this._EditorCurrentStyle = null;
            this.Elements.Clear();
            this._CurrentContentElement = null;
            DomDocumentContentElement ce = this.Body;
            ce.FixElements();
            //ce.AppendChildElement(this.CreateParagraphEOF());
            ce.UpdateContentElements(true);
            ce.SetSelection(0, 0);

            ce = this.Header;
            ce.FixElements();
            //DocumentContentStyle style = new DocumentContentStyle();
            
            ce.UpdateContentElements(true);
            ce.SetSelection(0, 0);

            ce = this.Footer;
            ce.FixElements();
            ce.UpdateContentElements(true);
            ce.SetSelection(0, 0);
            this.FixDomState();
        }

        /// <summary>
        /// 提交所有用户的修改记录。删除被逻辑删除的内容，清除用户修改痕迹。
        /// </summary>
        /// <returns>操作是否修改了文档内容</returns>
        internal bool CommitUserTrace()
        {
            bool result = DeleteLogicDeletedElement(this);
            for (int iCount = this.ContentStyles.Styles.Count - 1; iCount >= 0; iCount--)
            {
                DocumentContentStyle style = ( DocumentContentStyle ) this.ContentStyles.Styles[iCount];
                if (style.DeleterIndex >= 0)
                {
                    this.ContentStyles.Styles.RemoveAt(iCount);
                    result = true;
                }
                else if (style.CreatorIndex >= 0)
                {
                    style.CreatorIndex = -1;
                    result = true;
                }
            }//for
            if (this.UserHistories.Count > 0)
            {
                // 清除用户登录记录
                this.UserHistories.Clear();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 删除被逻辑删除的内容
        /// </summary>
        /// <param name="rootElement"></param>
        /// <returns></returns>
        private bool DeleteLogicDeletedElement(DomContainerElement rootElement)
        {
            bool result = false;
            for (int iCount = rootElement.Elements.Count - 1; iCount >= 0; iCount--)
            {
                DomElement element = rootElement.Elements[iCount];
                DocumentContentStyle style = element.Style;
                if (style.DeleterIndex >= 0)
                {
                    // 删除被标记为逻辑删除的元素
                    rootElement.Elements.RemoveAt(iCount);
                    result = true;
                }
                else if (element is DomContainerElement)
                {
                    DeleteLogicDeletedElement((DomContainerElement)element);
                }
            }
            return result;
        }

        #region 撤销/重复操作相关的成员群 *************************************

        /// <summary>
        /// 撤销操作信息列表
        /// </summary>
        [NonSerialized()]
        private XTextDocumentUndoList _UndoList = null ;
		/// <summary>
		/// 开始记录撤销操作信息
		/// </summary>
		/// <returns>操作是否成功</returns>
		public bool BeginLogUndo()
		{
			if( _UndoList != null )
			{
				return _UndoList.BeginLog();
			}
			return false;
		}
		/// <summary>
		/// 当前是否可以记录撤销操作信息
		/// </summary>
		public bool CanLogUndo
		{
            get
            {
                if (_UndoList != null)
                {
                    return _UndoList.CanLog;
                }
                else
                {
                    return false;
                }
            }
		}
		/// <summary>
		/// 撤销信息列表
		/// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public XTextDocumentUndoList UndoList
		{
			get
            {
                return _UndoList ;
            }
		}

		/// <summary>
		/// 完成记录撤销操作信息
		/// </summary>
        /// <remarks>操作是否保存了新的撤销信息</remarks>
		public bool EndLogUndo()
		{
			if( _UndoList != null )
			{
                if (_UndoList.EndLog())
                {
                    // 重复/撤销操信息列表内容发生改变，需要更新用户界面
                    if (this.EditorControl != null && this.EditorControl.CommandControler != null)
                    {
                        this.EditorControl.CommandControler.UpdateBindingControlStatus(StandardCommandNames.Undo);
                        this.EditorControl.CommandControler.UpdateBindingControlStatus(StandardCommandNames.Redo);
                    }
                    return true;
                }
			}
            return false;
		}

        public void CancelLogUndo()
        {
            if (_UndoList != null)
            {
                _UndoList.CancelLog();
            }
        }
		#endregion

         
        /// <summary>
        /// 创建多个文本元素
        /// </summary>
        /// <param name="strText">文本内容</param>
        /// <param name="paragraphStyle">段落样式</param>
        /// <param name="textStyle">文本样式</param>
        /// <returns>创建的字符元素对象列表</returns>
        public DomElementList CreateTextElements(
            string strText,
            DocumentContentStyle paragraphStyle,
            DocumentContentStyle textStyle )
        {
            return CreateTextElements(
                strText, 
                paragraphStyle, 
                textStyle, 
                this.Options.SecurityOptions.EnablePermission);
        }

        /// <summary>
        /// 创建多个文本元素
        /// </summary>
        /// <param name="strText">文本内容</param>
        /// <param name="paragraphStyle">段落样式</param>
        /// <param name="textStyle">文本样式</param>
        /// <param name="enablePermission">是否启用授权标记</param>
        /// <returns>创建的字符元素对象列表</returns>
	    public DomElementList CreateTextElements(
            string strText ,
            DocumentContentStyle paragraphStyle ,
            DocumentContentStyle textStyle ,
            bool enablePermission)
		{
            if( strText == null || strText.Length == 0 )
            {
                return null ;
            }
            DomElementList result = new DomElementList();
            if (enablePermission)
            {
                if (textStyle != null)
                {
                    textStyle = (DocumentContentStyle)textStyle.Clone();
                    textStyle.CreatorIndex = this.UserHistories.CurrentIndex;
                }
                else
                {
                    textStyle = new DocumentContentStyle();
                    textStyle.CreatorIndex = this.UserHistories.CurrentIndex;
                }
                if (paragraphStyle != null)
                {
                    paragraphStyle = (DocumentContentStyle)paragraphStyle.Clone();
                    paragraphStyle.CreatorIndex = this.UserHistories.CurrentIndex;
                }
                else
                {
                    paragraphStyle = new DocumentContentStyle();
                    paragraphStyle.CreatorIndex = this.UserHistories.CurrentIndex;
                }
            }
            else
            {
                if (textStyle != null && textStyle.CreatorIndex >= 0 )
                {
                    textStyle = (DocumentContentStyle)textStyle.Clone();
                    textStyle.CreatorIndex = -1;
                }
                if (paragraphStyle != null && paragraphStyle.CreatorIndex >= 0 )
                {
                    paragraphStyle = (DocumentContentStyle)paragraphStyle.Clone();
                    paragraphStyle.CreatorIndex = -1;
                }
            }


            int si = this.ContentStyles.GetStyleIndex( textStyle );
            int psi = this.ContentStyles.GetStyleIndex(paragraphStyle);
            using (System.Drawing.Graphics g = this.CreateGraphics())
            {
                foreach (char c in strText)
                {
                    if (c == '\r')
                    {
                        DomParagraphFlagElement pe = new DomParagraphFlagElement();
                        pe.OwnerDocument = this;
                        pe.Parent = this;
                        pe.StyleIndex = psi;
                        this.Render.RefreshSize(pe, g);
                        result.Add(pe);
                    }
                    else if (c == '\n')
                    {
                    }
                    else
                    {
                        DomCharElement c2 = new DomCharElement();
                        c2.CharValue = c;
                        c2.OwnerDocument = this;
                        c2.Parent = this;
                        c2.StyleIndex = si;
                        this.Render.RefreshSize(c2, g);
                        result.Add(c2);
                    }
                }//foreach
            }//using
            return result;
		}

        /// <summary>
        /// 导入文档元素
        /// </summary>
        /// <param name="elements">要导入的文档元素</param>
        public void ImportElements(DomElementList elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }
            if (elements.Count == 0)
            {
                return;
            }
            if (elements.LastElement is DomParagraphFlagElement)
            {
                DomParagraphFlagElement p = (DomParagraphFlagElement)elements.LastElement;
                if (p.AutoCreate)
                {
                    // 删除最后一个自动生成的段落标记对象
                    elements.RemoveAt(elements.Count - 1);
                }
            }
            Dictionary<int, int> styleIndexMap = new Dictionary<int, int>();
            DomDocument sourceDocument = elements[0].OwnerDocument;
            
            // 创建文档内容样式映射列表
            using (System.Drawing.Graphics g = this.CreateGraphics())
            {
                int oldIndex = 0;
                foreach (DocumentContentStyle style in sourceDocument.ContentStyles.Styles)
                {
                    int newIndex = this.ContentStyles.GetStyleIndex(style);
                    styleIndexMap[ oldIndex ] = newIndex;
                    DocumentContentStyle newStyle = ( DocumentContentStyle ) this.ContentStyles.GetStyle(newIndex);
                    newStyle.UpdateState(g);
                    oldIndex++;
                }//foreach
            }//using

            foreach (DomElement element in elements)
            {
                element.OwnerDocument = this;
                element.FixDomState();
            }//using

            foreach (DomElement element in elements)
            {
                element.AfterLoad(FileFormat.XML);
            }
            WriterUtils.Enumerate(elements, delegate(object sender, ElementEnumerateEventArgs args)
                {
                    if (styleIndexMap.ContainsKey(args.Element.StyleIndex))
                    {
                        // 更新文档内容样式编号
                        args.Element.StyleIndex = styleIndexMap[args.Element.StyleIndex];
                    }
                    args.Element.OwnerDocument = this;
                });
        }

		/// <summary>
		/// 在当前位置插入一个元素
		/// </summary>
		/// <param name="element">要插入的元素对象</param>
		public void InsertElement( DomElement element )
		{
			DomElementList list = new DomElementList();
			list.Add( element );
            InsertElements(list, true);
			//InsertElementsBefore( this.CurrentElement , list , true );
		}
         

        /// <summary>
        /// 插入多个元素到文档中
        /// </summary>
        /// <param name="newElements">要插入的新元素</param>
        /// <param name="updateContent">是否更新文档视图</param>
        /// <returns>插入的元素个数</returns>
        public int InsertElements(DomElementList newElements, bool updateContent)
        {
            
            DomElement element = this.CurrentElement;
             
            DomDocumentContentElement dce = element.DocumentContentElement;
            DomContainerElement container = null;
            int index = 0;
            dce.Content.GetPositonInfo(
                dce.Content.IndexOf(element),
                out container,
                out index,
                dce.Content.LineEndFlag );
            if (container is DomContentElement
                && index == container.Elements.Count )
            {
                // 若容器元素是文本块元素，则最后一个元素固定保证为一个段落符号，
                // 因此当插入点位置在元素列表最后则进行修正
                index = container.Elements.Count - 1;
            }
            ReplaceElementsArgs args = new ReplaceElementsArgs(
                    container,
                    index,
                    0,
                    newElements,
                    true,
                    updateContent,
                    true);
            return this.ReplaceElements( args );
        }
         

        /// <summary>
        /// 对元素设置逻辑删除标记
        /// </summary>
        /// <param name="elements">元素对象</param>
        /// <param name="startIndex">开始删除的起始位置</param>
        /// <param name="length">要删除的区域的长度</param>
        /// <param name="addCreatorIndex">添加模式 true:添加创建者标记, false:添加删除者标记</param>
        /// <param name="logUndo">是否记录撤销信息</param>
        internal void MarkPermission(DomElementList elements, int startIndex, int length , bool addCreatorIndex , bool logUndo )
        {
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }
            if (startIndex < 0 || startIndex >= elements.Count)
            {
                throw new ArgumentOutOfRangeException("startIndex=" + startIndex);
            }
            if (length < 0 || startIndex + length > elements.Count)
            {
                throw new ArgumentOutOfRangeException("length=" + length);
            }
            int endIndex = startIndex + length - 1 ;
            for (int iCount = startIndex; iCount <= endIndex; iCount++)
            {
                DomElement element = elements[ iCount ] ;
                DocumentContentStyle style = (DocumentContentStyle)element.Style.Clone();
                style.DisableDefaultValue = false;
                if (addCreatorIndex)
                {
                    style.CreatorIndex = this.UserHistories.CurrentIndex;
                }
                else
                {
                    style.DeleterIndex = this.UserHistories.CurrentIndex;
                }
                int si = this.ContentStyles.GetStyleIndex(style);
                if ( logUndo && this.CanLogUndo)
                {
                    this.UndoList.AddStyleIndex(element, element.StyleIndex, si);
                }
                element.StyleIndex = si;
            }//for
        }

        /// <summary>
        /// 替换元素
        /// </summary>
        /// <param name="container">容器对象</param>
        /// <param name="startIndex">开始区域</param>
        /// <param name="deleteLength">要替换的元素的区域长度</param>
        /// <param name="newElements">新元素列表</param>
        /// <param name="logUndo">是否记录撤销操作信息</param>
        /// <param name="updateContent">是否更新文档视图</param>
        /// <param name="raiseEvent">是否触发事件</param>
        /// <returns>操作的元素个数</returns>
        public int ReplaceElements(ReplaceElementsArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }
            if (args.Container == null)
            {
                throw new ArgumentNullException("container");
            }

            //XTextDocumentContentElement dce = container.DocumentContentElement;
            DomContentElement ce = args.Container.ContentElement;
            if (ce == null)
            {
                throw new ArgumentNullException("ContentElement");
            }

            //XTextContent content = dce.Content;
            //if (content == null)
            //{
            //    throw new ArgumentNullException("Content");
            //}

            //int selectionPos = content.AbsSelectStart;

            DomElementList elements = args.Container.Elements;
            DomElementList oldElements = new DomElementList();
            if (elements.Count > 0)
            {
                // 元素列表中有内容
                if (args.StartIndex >= 0 && args.StartIndex + args.DeleteLength <= elements.Count)
                {
                    if (args.DeleteLength > 0)
                    {
                        for (int iCount = 0; iCount < args.DeleteLength; iCount++)
                        {
                            DomElement old = elements[iCount + args.StartIndex];
                            if (this.DocumentControler.CanDelete(old , args.AccessFlags ) == false )
                            {
                                // 出现只读元素，退出替换元素操作
                                return 0;
                            }
                            oldElements.Add(elements[iCount + args.StartIndex]);
                        }//for
                    }
                }//if
            }
            if (args.NewElements != null && args.NewElements.Count > 0)
            {
                foreach (DomElement element in args.NewElements)
                {
                    if (this.DocumentControler.CanInsert(
                        args.Container, 
                        args.StartIndex, 
                        element ,
                        args.AccessFlags) == false)
                    {
                        // 出现不能插入的元素，退出替换元素操作
                        return 0;
                    }
                }//foreach
            }
            int privateStartContentIndex = 0;
            DomElement nextElement = null;
            if (args.UpdateContent)
            {
                // 更新文档内容，重新排版

                // 获得更新区域的第一个文档内容元素
                DomElement startElement = null;
                if (elements.Count == 0)
                {
                    startElement = args.Container.FirstContentElement;
                }
                else if (args.DeleteLength > 0)
                {
                    startElement = oldElements.FirstContentElement;
                }
                else if (args.StartIndex == elements.Count)
                {
                    // 插入位置在最后
                    startElement = elements[args.StartIndex - 1].FirstContentElement;
                }
                else
                {
                    startElement = elements[args.StartIndex].FirstContentElement;
                }
                if (startElement == null
                    || ce.PrivateContent.Contains(startElement) == false)
                {
                    startElement = args.Container.FirstContentElement;
                }
                if (startElement == null
                    || ce.PrivateContent.Contains(startElement) == false)
                {
                    startElement = ce.PrivateContent[0];
                }

                // 获得更新区域中最后一个文档内容元素
                DomElement endElement = null;
                if (elements.Count == 0)
                {
                    endElement = args.Container.LastContentElement;
                }
                else if (args.DeleteLength > 0)
                {
                    endElement = oldElements.LastContentElement;
                }
                else if (args.StartIndex == elements.Count)
                {
                    // 插入位置在最后
                    endElement = elements[args.StartIndex - 1].LastContentElement;
                }
                else
                {
                    endElement = elements[args.StartIndex].LastContentElement;
                }
                if (endElement == null
                    || ce.PrivateContent.Contains(endElement) == false)
                {
                    endElement = args.Container.LastContentElement;
                }

                nextElement = ce.PrivateContent.LastElement;
                if (endElement != null)
                {
                    nextElement = ce.PrivateContent.GetNextElement(endElement);
                }
                privateStartContentIndex = ce.PrivateContent.IndexOf(startElement);
                if (privateStartContentIndex >= 0)
                {
                    // 设置无效行
                    DomElement tempElement = ce.PrivateContent.SafeGet(privateStartContentIndex - 1);
                    int startLineIndex = 0;
                    int endLineIndex = ce.PrivateLines.Count - 1;
                    if (tempElement != null && tempElement.OwnerLine != null)
                    {
                        startLineIndex = ce.PrivateLines.IndexOf(tempElement.OwnerLine);
                        if (tempElement.OwnerLine.LastElement == tempElement)
                        {
                            startLineIndex++;
                        }
                    }
                    int index3 = ce.PrivateContent.IndexOf(endElement);
                    if (index3 >= 0)
                    {
                        tempElement = ce.PrivateContent.SafeGet(index3 + 1);
                        if (tempElement != null && tempElement.OwnerLine != null)
                        {
                            endLineIndex = ce.PrivateLines.IndexOf(tempElement.OwnerLine);
                        }
                    }
                    for (int iCount = startLineIndex; iCount <= endLineIndex; iCount++)
                    {
                        ce.PrivateLines[iCount].InvalidateState = true;
                    }
                }//if
            }
            DomElementList deletedElement = elements.GetRange(args.StartIndex, args.DeleteLength);
            if (args.RaiseEvent)
            {
                // 触发文档内容正在发生改变事件
                ContentChangingEventArgs args2 = new ContentChangingEventArgs();
                args2.Document = this;
                args2.Element = args.Container;
                args2.ElementIndex = args.StartIndex;
                args2.DeletingElements = deletedElement;
                args2.InsertingElements = args.NewElements;
                args.Container.RaiseBubbleOnContentChanging(args2);
                if (args2.Cancel)
                {
                    // 用户取消操作
                    return 0;
                }
            }
            bool logicDelete = false;
            if ( args.DisablePermission )
            {
                // 没有临时禁止掉授权控制
                if (this.Options.SecurityOptions.EnablePermission
                    && this.Options.SecurityOptions.EnableLogicDelete)
                {
                    // 判断是否进行逻辑删除
                    for (int iCount = 0; iCount < args.DeleteLength; iCount++)
                    {
                        DomElement element = elements[args.StartIndex + iCount];
                        if (element.Style.CreatorIndex != this.UserHistories.CurrentIndex)
                        {
                            logicDelete = true;
                            break;
                        }
                    }
                }
            }
            int result = 0;
            if (args.DeleteLength > 0)
            {
                if ( logicDelete )
                {
                    // 逻辑删除,添加删除标记
                    MarkPermission(elements, args.StartIndex, args.DeleteLength, false , true );
                }
                else
                {
                    // 物理删除旧元素
                    if (args.DeleteLength == elements.Count)
                    {
                        elements.Clear();
                    }
                    else
                    {
                        elements.RemoveRange(args.StartIndex, args.DeleteLength);
                    }
                }
                result += args.DeleteLength;
            }
            if (args.NewElements != null && args.NewElements.Count > 0)
            {
                // 插入新元素
                if (args.DisablePermission)
                {
                    // 临时禁止掉了授权控制，删除新增元素上面的授权信息
                    foreach (DomElement element in args.NewElements )
                    {
                        DocumentContentStyle style = ( DocumentContentStyle ) element.Style.Clone();
                        style.CreatorIndex = -1;
                        style.DeleterIndex = -1;
                        element.StyleIndex = this.ContentStyles.GetStyleIndex(style);
                    }
                }
                elements.InsertRange(args.StartIndex, args.NewElements);
                foreach (DomElement element in args.NewElements)
                {
                    element.Parent = args.Container;
                    element.OwnerDocument = this;
                }
                if (this.Options.SecurityOptions.EnablePermission)
                {
                    // 标记授权信息
                    MarkPermission(args.NewElements, 0, args.NewElements.Count, true, false);
                }
                result += args.NewElements.Count;
                // 更新容器元素的内容版本号
                args.Container.UpdateContentVersion();
            }
            
            // 记录操作日志
            if (args.LogUndo && this.CanLogUndo)
            {
                if ( logicDelete )
                {
                    this.UndoList.AddReplaceElements(
                        args.Container,
                        args.StartIndex,
                        null ,// 处于逻辑删除模式，此时元素并不是真正的删除，因此不记录删除元素的撤销信息
                        args.NewElements);
                }
                else
                {
                    this.UndoList.AddReplaceElements(
                        args.Container,
                        args.StartIndex,
                        oldElements,
                        args.NewElements);
                }
            }

            this._Modified = true;
            if (args.UpdateContent)
            {
                DomElement currentElementBack = this.CurrentElement;
                if (args.NewElements != null && args.NewElements.Count > 0)
                {
                    
                }
                args.Container.ContentElement.UpdateContentElements(true);
                ce.RefreshPrivateContent(
                        privateStartContentIndex,
                        ce.PrivateContent.IndexOf(nextElement),
                        false );
                // 确认新的插入点的位置
                DomDocumentContentElement dce = args.Container.DocumentContentElement;
                dce.RefreshGlobalLines();
                if (args.ChangeSelection)
                {
                    // 设置新的插入点位置
                    if (currentElementBack != null)
                    {
                        dce.Content.AutoClearSelection = true;
                        dce.Content.LineEndFlag = false;

                        int newSelectionPosition = currentElementBack.ViewIndex;
                        if (args.NewElements != null)
                        {
                            DomElement lastContentElement = args.NewElements.LastContentElement;
                            if (lastContentElement != null && dce.Content.Contains(lastContentElement))
                            {
                                newSelectionPosition = lastContentElement.ViewIndex + 1;
                            }
                        }
                        dce.Content.MoveSelectStart(newSelectionPosition);
                    }
                }
                else
                {
                    // 尽量使用当前的插入点的位置
                    dce.Content.SetSelection(dce.Selection.StartIndex, dce.Selection.Length);
                }
            }
            if (args.RaiseEvent)
            {
                // 触发文档内容发生改变后事件
                ContentChangedEventArgs args2 = new ContentChangedEventArgs();
                args2.Document = this;
                args2.Element = args.Container;
                args2.ElementIndex = args.StartIndex;
                if (args.DeleteLength > 0)
                {
                    args2.DeletedElements = deletedElement;
                }
                args2.InsertedElements = args.NewElements;
                args.Container.RaiseBubbleOnContentChanged(args2);
            }
            return result;
        }

        /// <summary>
        /// 创建段落对象
        /// </summary>
        /// <param name="elements">元素对象列表</param>
        /// <returns>创建的段落对象列表</returns>
        public DomElementList CreateParagraphs( IEnumerable elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }
            DomElementList result = new DomElementList();
            DomParagraphElement p = new DomParagraphElement();
            p.OwnerDocument = this;
            p.Parent = this;
            result.Add(p);
            foreach (DomElement element in elements)
            {
                if (element is DomParagraphFlagElement)
                {
                    DomParagraphFlagElement eof = (DomParagraphFlagElement)element;
                    p.StyleIndex = eof.StyleIndex;
                    //p.myEOFElement = eof;
                    p.Elements.AddRaw(element);

                    p = new DomParagraphElement();
                    p.OwnerDocument = this;
                    p.Parent = this;
                    result.AddRaw(p);
                }
                else
                {
                    p.Elements.AddRaw(element);
                }
            }//foreach
            foreach (DomParagraphElement p2 in result)
            {
                DomElementList list =  WriterUtils.MergeElements(
                    p2.Elements ,
                    false );
                //if (list.LastElement is XTextParagraphEOF)
                //{
                //    list.RemoveAt(list.Count - 1);
                //}
                p2.Elements.Clear();
                p2.Elements.AddRange(list);
            }
            return result;
        }

		/// <summary>
		/// 文档绑定的控件
		/// </summary>
        [NonSerialized()]
		private DCSoft.CSharpWriter.Controls.CSWriterControl _EditorControl = null;
		/// <summary>
		/// 文档绑定的控件
		/// </summary>
        [XmlIgnore()]
        [Browsable( false )]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden )]
        public DCSoft.CSharpWriter.Controls.CSWriterControl EditorControl
		{
			get
            {
                return _EditorControl ;
            }
			set
            {
                _EditorControl = value;
            }
		}
         
        /// <summary>
        /// 获得文档中指定编号的元素对象,查找时ID值区分大小写的。
        /// </summary>
        /// <param name="id">指定的编号</param>
        /// <returns>找到的元素对象</returns>
        public DomElement GetElementById(string id)
        {
            if (string.Compare( id , WriterConst.Header , true )== 0)
            {
                // 返回页眉
                return this.Header;
            }
            else if (string.Compare(id, WriterConst.Body, true) == 0)
            {
                // 返回正文
                return this.Body;
            }
            else if (string.Compare(id, WriterConst.Footer, true) == 0)
            {
                // 返回页脚
                return this.Footer;
            }
            DomElement idElement = null;
            DomElement nameElement = null;
            this.Enumerate(delegate(object sender, ElementEnumerateEventArgs args)
                {
                    if (args.Element.ID == id)
                    {
                        idElement = args.Element;
                        args.Cancel = true;
                    }
                    
                },
            false);
            if (idElement != null)
            {
                return idElement;
            }
            else
            {
                return nameElement;
            }
        }

        /// <summary>
        /// 获得文档中指定类型的下一个元素
        /// </summary>
        /// <param name="startElement">开始查找的起始元素</param>
        /// <param name="nextElementType">要查找的元素的类型</param>
        /// <returns>找到的元素</returns>
        public DomElement GetNextElement(DomElement startElement, Type nextElementType)
        {
            if (startElement == null)
            {
                throw new ArgumentNullException("startElement");
            }
            if (nextElementType == null)
            {
                throw new ArgumentNullException("nextElementType");
            }
            DomElement result = null;
            bool ready = false;
            this.Enumerate(delegate(object sender, ElementEnumerateEventArgs args)
                {
                    if (ready)
                    {
                        if (nextElementType.IsInstanceOfType(args.Element))
                        {
                            result = args.Element;
                            args.Cancel = true;
                        }
                    }
                    else if (args.Element == startElement)
                    {
                        ready = true;
                    }
                });
            return result;
        }

        /// <summary>
        /// 获得文档中所有的指定类型的文档元素列表
        /// </summary>
        /// <param name="elementType">元素类型</param>
        /// <returns>获得的元素列表</returns>
        public DomElementList GetSpecifyElements(Type elementType)
        {
            DomElementList result = new DomElementList();
            GlobalGetElements(this, result, elementType);
            return result;
        }

        internal void GlobalGetElements(
            DomContainerElement rootElement, 
            DomElementList list, 
            Type elementType)
        {
            foreach (DomElement element in rootElement.Elements)
            {
                if (elementType.IsInstanceOfType(element))
                {
                    list.Add(element);
                }
                if (element is DomContainerElement)
                {
                    GlobalGetElements((DomContainerElement)element, list, elementType);
                }
            }//foreach
        }
         
        /// <summary>
        /// 文档所有的XML字符串
        /// </summary>
        [XmlIgnore()]
        [Browsable(false)]
        public string XMLText
        {
            get
            {
                StringWriter writer = new StringWriter();
                XmlSerializer ser = DocumentSaver.GetDocumentXmlSerializer(this.GetType());
                ser.Serialize(writer, this);
                string xml = writer.ToString();
                return xml;
            }
            set
            {
                StringReader reader = new StringReader( value );
                DocumentLoader.LoadXmlFile(reader, this);
            }
        }


		/// <summary>
		/// 获得文档的RTF文本代码
		/// </summary>
		/// <param name="IncludeSelectionOnly">是否只包含选择区域</param>
		/// <returns>获得的RTF文本代码字符串</returns>
		public string GetRTFText( bool IncludeSelectionOnly )
		{
			System.IO.StringWriter writer = new System.IO.StringWriter();
			RTFContentWriter w = new RTFContentWriter( );
			w.IncludeSelectionOnly = IncludeSelectionOnly ;
			w.Open( writer );
            w.Document = this;
            w.CollectionDocumentsStyle();
			this.WriteRTF( w );
			w.Close();
			return writer.ToString();
		}

		/// <summary>
		/// 文档的所有的RTF文本代码
		/// </summary>
		[XmlIgnore()]
        [Browsable( false)]
        public string RTFText
		{
			get
			{
				return GetRTFText( false );
			}
            set
            {
                if (value == null
                    || value.IndexOf("{") < 0 
                    || value.IndexOf("}") < 0 )
                {
                    this.Text = value;
                }
                else
                {
                    RTFLoader loader = new RTFLoader();
                    loader.EnableDocumentSetting = true;
                    loader.LoadRTFText(value);
                    loader.FillTo(this);
                }
            }
		}

		/// <summary>
		/// 文档所有的文本内容
		/// </summary>
        [XmlIgnore()]
        [Browsable( false )]
		public override string Text
		{
			get
			{
                return this.Body.PrivateContent.GetInnerText();
    		}
			set
			{
                this.Clear();
                DomElementList newElements = this.CreateTextElements(
                        value,
                        null , 
                        null );
                if (newElements != null && newElements.Count > 0)
                {
                    this.ClearContent();
                    this.Body.Elements.Clear();
                    this.Body.Elements.AddRange(newElements);
                    this.Body.UpdateContentElements(true);
                }
                //this.FixElements();
			}
		}

        private string ExecuteVariable( string txt )
		{
			DCSoft.Common.VariableString str = new DCSoft.Common.VariableString( txt );
			str.SetVariable( "pageindex" , Convert.ToString( this._PageIndex + 1 ));
			str.SetVariable( "pagecount" , this._Pages.Count.ToString());
			System.DateTime dtm = System.DateTime.Now ;
			str.SetVariable( "year" , dtm.Year.ToString());
			str.SetVariable( "month" , dtm.Month.ToString());
			str.SetVariable( "day" , dtm.Day.ToString());
			str.SetVariable( "hour" , dtm.Hour.ToString());
			str.SetVariable( "minute" , dtm.Minute.ToString());
			str.SetVariable( "secend" , dtm.Second.ToString());
			return str.Execute();
		}

		
		/// <summary>
		/// 声明指定元素的视图无效,需要重新绘制
		/// </summary>
		/// <param name="element">文本元素对象</param>
		public virtual void InvalidateElementView( DomElement element )
		{
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
			if( this.EditorControl != null 
				&& element != null)
			{
                System.Drawing.RectangleF rect = element.AbsBounds ;
                rect.Width = rect.Width + element.WidthFix;
                if (element.OwnerLine != null)
                {
                    rect.Y = element.OwnerLine.AbsTop  ;
                    rect.Height = element.OwnerLine.Height;
                }
				this.EditorControl.ViewInvalidate(
                    rect ,
                    element.DocumentContentElement.ContentPartyStyle );
			}
		}

        public virtual void InvalidateView(System.Drawing.RectangleF bounds)
        {
            if (this.EditorControl != null)
            {
                this.EditorControl.ViewInvalidate(bounds , this.CurrentContentPartyStyle );
            }
        }

        public virtual void InvalidateView(ContentRange range)
        {
            if (range == null)
            {
                return;
                //throw new ArgumentNullException("range");
            }
            if (this.EditorControl != null)
            {
                foreach (DomElement element in range)
                {
                    this.InvalidateElementView(element);
                }//foreach
            }
        }

        /// <summary>
        /// 创建一个文档元素
        /// </summary>
        /// <param name="elementType">指定的文档元素类型</param>
        /// <returns>创建的文档元素</returns>
        public virtual DomElement CreateElement(Type elementType)
        {
            if (elementType == null)
            {
                throw new ArgumentNullException("elementType");
            }
            if (typeof(DomElement).IsAssignableFrom( elementType ) == false)
            {
                throw new ArgumentOutOfRangeException(elementType.FullName);
            }
            DomElement element = ( DomElement ) System.Activator.CreateInstance(elementType);
            if (element != null)
            {
                element.OwnerDocument = this;
                element.Parent = null;
                element.StyleIndex = -1;
            }
            return element;
        }

        ///// <summary>
        ///// 创建一个书签对象
        ///// </summary>
        ///// <returns>创建的书签对象</returns>
        //public XTextBookmark CreateBookmark()
        //{
        //    XTextBookmark mark = new XTextBookmark();
        //    mark.OwnerDocument = this ;
        //    mark.Parent = this ;
        //    return mark ;
        //}
 
 
		/// <summary>
		/// 根据一个字符串创建若干个字符文本元素
		/// </summary>
		/// <param name="strText">字符串</param>
		/// <returns>创建的字符文本元素组成的列表</returns>
		public virtual DomElementList CreateChars( string strText )
		{
            if (strText == null
                || strText.Length == 0)
            {
                return null;
            }
			DomElementList list = new DomElementList();
			foreach( char c in strText )
			{
				DomCharElement c2 = CreateChar( c );
                if (c2 != null)
                {
                    list.Add(c2);
                }
			}
			return list ;
		}

        /// <summary>
        /// 根据一个字符串创建若干个字符文本元素
        /// </summary>
        /// <param name="strText">字符串</param>
        /// <returns>创建的字符文本元素组成的列表</returns>
        public virtual DomElementList CreateChars(string strText , int styleIndex )
        {
            if (strText == null
                || strText.Length == 0)
            {
                return null;
            }
            DomElementList list = new DomElementList();
            foreach (char c in strText)
            {
                DomCharElement c2 = CreateChar(c , styleIndex );
                if (c2 != null)
                {
                    list.Add(c2);
                }
            }
            return list;
        }

		/// <summary>
		/// 创建一个换行对象
		/// </summary>
		/// <returns>创建的对象</returns>
		public virtual DomLineBreakElement CreateLineBreak()
		{
            return (DomLineBreakElement)CreateElement(typeof(DomLineBreakElement));
		}

		public virtual DomParagraphFlagElement CreateParagraphEOF()
		{
            return (DomParagraphFlagElement)CreateElement(typeof(DomParagraphFlagElement));
		}

		/// <summary>
		/// 根据一个字符创建一个字符文本元素
		/// </summary>
		/// <param name="v">字符串数据</param>
		/// <returns>创建的字符文本元素</returns>
		public virtual DomCharElement CreateChar( char v )
		{
			return CreateChar(v, this.ContentStyles.GetStyleIndex(this.CurrentStyle ));
		}

        public virtual DomCharElement CreateChar(char v, int styleIndex)
        {
            if (v == '\n' || v == '\r')
            {
                return null;
            }
            if (v < 32 && v != '\t')
            {
                // 出现不可接收的字符
                return null;
            }

            DomCharElement myChar = new DomCharElement();
            myChar.CharValue = v;
            myChar.StyleIndex = styleIndex;
            myChar.OwnerDocument = this;
            myChar.Parent = this;
            return myChar;
        }

        private UserHistoryInfoList _UserHistories = new UserHistoryInfoList();
        /// <summary>
        /// 用户历史记录列表
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        [System.Xml.Serialization.XmlArrayItem("History" , typeof( UserHistoryInfo ))]
        public UserHistoryInfoList UserHistories
        {
            get
            {
                if (_UserHistories == null)
                {
                    _UserHistories = new UserHistoryInfoList();
                }
                return _UserHistories; 
            }
            set
            {
                _UserHistories = value; 
            }
        }

        /// <summary>
        /// 创建文档样式对象
        /// </summary>
        /// <returns>创建的对象</returns>
        public virtual DocumentContentStyle CreateDocumentContentStyle()
        {
            return new DocumentContentStyle();
        }

        //private XFontValue _DefaultFont = new XFontValue();
        /// <summary>
        /// 默认字体
        /// </summary>
        [System.ComponentModel.Category("Appearance")]
        [Browsable( false )]
        [XmlIgnore]
        public XFontValue DefaultFont
        {
            get 
            {
                return this.DefaultStyle.Font;
            }
            set
            {
                this.DefaultStyle.Font = value;
            }
        }

        private DocumentContentStyleContainer _ContentStyles 
            = new DocumentContentStyleContainer();

        /// <summary>
        /// 文档样式容器
        /// </summary>
        public DocumentContentStyleContainer ContentStyles
        {
            get 
            {
                if (_ContentStyles == null)
                {
                    _ContentStyles = new DocumentContentStyleContainer();
                }
                _ContentStyles.Document = this;
                return _ContentStyles; 
            }
            set
            {
                _ContentStyles = value;
                if (_ContentStyles != null)
                {
                    _ContentStyles.Document = this;
                }
            }
        }

        /// <summary>
        /// 删除没有使用到的文档内容样式对象
        /// </summary>
        /// <remarks>
        /// 在文档编辑过程中，可能会产生没有任何文档元素使用到的文档样式，
        /// 此时可以使用本方法来删除没有用的样式，减少文档数据量。
        /// </remarks>
        public void DeleteUselessStyle()
        {
            // 获得文档中所有被使用过的样式编号
            ContentStyleList styles = this.ContentStyles.Styles;
            // 累计文档中各个编号的样式引用次数,偶数位是样式编号，奇数为是样式引用次数
            int[] references = new int[styles.Count];
            for (int iCount = 0; iCount < styles.Count; iCount++)
            {
                references[iCount] = 0;
            }
            this.Enumerate(delegate(object sender, ElementEnumerateEventArgs args)
                {
                    int si = args.Element.StyleIndex;
                    if (si >= 0 && si < styles.Count)
                    {
                        references[si]++;
                    }
                },
                false );
            // 建立映射列表,数组序号是旧映射编号，数组元素内容是新样式编号
            int step = 0;
            for (int iCount = 0; iCount < references.Length ; iCount++)
            {
                if (references[iCount] == 0)
                {
                    step++;
                    references[iCount] = -1;
                }
                else
                {
                    references[iCount] = iCount - step;
                }
            }//for

            // 更新文档元素的样式编号
            this.Enumerate(delegate(object sender, ElementEnumerateEventArgs args)
                {
                    int si = args.Element.StyleIndex;
                    if (si >= 0 && si < styles.Count)
                    {
                        args.Element.StyleIndex = references[si];
                    }
                    else
                    {
                        args.Element.StyleIndex = -1;
                    }
                },
                false );
            // 删除没用的样式
            for (int iCount = references.Length - 1; iCount >= 0; iCount--)
            {
                if (references[iCount] < 0)
                {
                    styles.RemoveAt(iCount);
                }
            }//for
        }

        //public virtual void SetElementStyle(XTextElement element, DocumentContentStyle style)
        //{
        //    if (element == null)
        //    {
        //        throw new ArgumentNullException("element");
        //    }
        //    if (style == null)
        //    {
        //        throw new ArgumentNullException("style");
        //    }
        //    element.StyleIndex = this.ContentStyles.GetStyleIndex(style);
        //}

        ///// <summary>
        ///// 用户指定的当前样式
        ///// </summary>
        //[NonSerialized()]
        //internal DocumentContentStyle _UserSpecifyStyle = null;

        /// <summary>
        /// 在编辑器中使用的当前样式
        /// </summary>
        [NonSerialized]
        private DocumentContentStyle _EditorCurrentStyle = null;

        /// <summary>
        /// 在编辑器中使用的当前样式
        /// </summary>
        internal DocumentContentStyle EditorCurrentStyle
        {
            get
            {
                if (_EditorCurrentStyle == null)
                {
                    _EditorCurrentStyle = ( DocumentContentStyle ) this.CurrentStyle.Clone();
                    //_EditorCurrentStyle.Merge(this.DefaultStyle);
                }
                _EditorCurrentStyle.DisableDefaultValue = true;
                //if (this.Options.SecurityOptions.EnablePermission == false )
                //{
                _EditorCurrentStyle.CreatorIndex = -1;
                _EditorCurrentStyle.DeleterIndex = -1;
                //}
                return _EditorCurrentStyle; 
            }
            set
            {
                _EditorCurrentStyle = value;
                if (_EditorCurrentStyle != null )
                {
                    _EditorCurrentStyle.CreatorIndex = -1;
                    _EditorCurrentStyle.DeleterIndex = -1;
                }
            }
        }

        

        /// <summary>
        /// 当前文档样式
        /// </summary>
        [Browsable( false )]
        [XmlIgnore()]
        public DocumentContentStyle CurrentStyle
        {
            get
            {
                if (this.CurrentElement == null)
                {
                    return (DocumentContentStyle)this.ContentStyles.Default;
                }
                else
                {
                    DomElement element = this.CurrentElement;
                    DomContentElement ce = element.ContentElement;
                    if (this.CurrentContentElement.Selection.Length == 0)
                    {
                        DomElement preElement = ce.PrivateContent.GetPreElement(element);
                        DocumentContentStyle result = element.RuntimeStyle;
                        if (preElement != null
                            && (preElement is DomParagraphFlagElement) == false)
                        {
                            result = preElement.RuntimeStyle;
                        }
                        return result;
                    }
                    else
                    {
                        element = this.CurrentContentElement.Selection.ContentElements[0];
                        DocumentContentStyle rs = element.RuntimeStyle;
                        return rs;
                    }
                }
            }
        }


        /// <summary>
        /// 当前段落样式
        /// </summary>
        [Browsable( false )]
        [XmlIgnore()]
        public DocumentContentStyle CurrentParagraphStyle
        {
            get
            {
                return GetParagraphStyle(this.CurrentElement);
            }
        }

        /// <summary>
        /// 获得指定元素所在段落的样式
        /// </summary>
        /// <param name="element">元素对象</param>
        /// <returns>段落样式对象</returns>
        public DomParagraphFlagElement GetParagraphEOFElement(DomElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            if (element is DomParagraphFlagElement)
            {
                return (DomParagraphFlagElement)element;
            }
            DomContentElement ce = element.ContentElement;
            if (ce != null)
            {
                DomElementList pc = ce.PrivateContent;
                if (pc.IndexOf(element) < 0)
                {
                    throw new InvalidOperationException("element not in content");
                    //System.Console.WriteLine("");
                }
                int index = pc.IndexOf(element);
                if (index < 0)
                {
                    System.Console.Write("");
                }
                for ( ; index < pc.Count; index++)
                {
                    if ( pc[index] is DomParagraphFlagElement)
                    {
                        return (DomParagraphFlagElement)pc[index];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获得指定元素所在段落的样式
        /// </summary>
        /// <param name="element">元素对象</param>
        /// <returns>段落样式对象</returns>
        public DocumentContentStyle GetParagraphStyle(DomElement element)
        {
            if (element == null)
            {
                return ( DocumentContentStyle ) this.ContentStyles.Default;
            }
            DomParagraphFlagElement eof = this.GetParagraphEOFElement(element);
            if( eof == null )
            {
                return (DocumentContentStyle)this.ContentStyles.Default;
            }
            else
            {
                return eof.RuntimeStyle ;
            }
        }

        /// <summary>
        /// 默认的文档样式
        /// </summary>
        [Browsable( false )]
        [XmlIgnore()]
        public DocumentContentStyle DefaultStyle
        {
            get
            {
                return ( DocumentContentStyle ) this.ContentStyles.Default;
            }
            set
            {
                this.ContentStyles.Default = value;
            }
        }

        /// <summary>
        /// 设置默认字体
        /// </summary>
        /// <param name="font">默认字体</param>
        /// <param name="color">默认文本颜色</param>
        /// <returns>对视图的影响</returns>
        public ViewEffects SetDefaultFont(XFontValue font, Color color)
        {
            // 控件字体发生改变标记
            bool fc = this.ContentStyles.Default.Font.EqualsValue(font) == false;
            // 控件颜色发生改变标记
            bool cc = this.ContentStyles.Default.Color != color ;
            if (fc == true)
            {
                this.ContentStyles.Default.Font = font.Clone();
                this.ContentStyles.ClearRuntimeStyleList();
            }
            if (cc == true)
            {
                this.ContentStyles.Default.Color = color;
                this.ContentStyles.ClearRuntimeStyleList();
            }
            if (fc || cc)
            {
                //this.Modified = true;
                this.OnDocumentContentChanged();
                this.OnSelectionChanged();
            }
            if( fc == true )
            {
                return ViewEffects.Layout ;
            }
            else if( cc == true )
            {
                return ViewEffects.Display ;
            }
            else
            {
                return  ViewEffects.None ;
            }
        }
           
        [NonSerialized]
		internal MouseCaptureInfo _MouseCapture = null;
		 
        [NonSerialized]
        private ElementEventList _GlobalEvents = null;
        /// <summary>
        /// 全局文档事件列表
        /// </summary>
        [Browsable( false )]
        [XmlIgnore()]
        public virtual ElementEventList GlobalEvents
        {
            get
            {
                return _GlobalEvents; 
            }
            set
            {
                _GlobalEvents = value; 
            }
        }

        ///// <summary>
        ///// 触发全局的内容正在发生改变事件
        ///// </summary>
        ///// <param name="sender">参数</param>
        ///// <param name="args">参数</param>
        //public virtual void OnGlobalContentChanging(object sender, ContentChangingEventArgs args)
        //{
        //    if (this.GlobalEvents != null && this.GlobalEvents.HasContentChanging)
        //    {
        //        this.GlobalEvents.RaiseContentChanging(sender, args);
        //    }
        //}

        ///// <summary>
        ///// 触发全局的内容已经发生改变事件
        ///// </summary>
        ///// <param name="sender">参数</param>
        ///// <param name="args">参数</param>
        //public virtual void OnGlobalContentChanged(object sender, ContentChangedEventArgs args)
        //{
        //    if (this.GlobalEvents != null && this.GlobalEvents.HasContentChanged)
        //    {
        //        this.GlobalEvents.RaiseContentChanged(sender, args);
        //    }
        //    //// 文档内容发生改变,执行表达式
        //    //if (args.Element is XTextCheckBoxElement
        //    //    || args.Element is XTextInputFieldElement )
        //    //{
        //    //    XTextElementList effectElements = new XTextElementList();
        //    //    this.Enumerate(delegate(object sender, ElementEnumerateEventArgs args)
        //    //        {
                        
        //    //        });
        //    //}
        //}

        

        [NonSerialized]
        private HighlightManager _HighlightManager = null ;
        /// <summary>
        /// 文档视图中高亮度显示区域管理器
        /// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public HighlightManager HighlightManager
        {
            get
            {
                if (_HighlightManager == null)
                {
                    _HighlightManager = new HighlightManager();
                }
                _HighlightManager.Document = this;
                return _HighlightManager; 
            }
            set
            {
                _HighlightManager = value; 
            }
        }


        /// <summary>
        /// 方法无效
        /// </summary>
        /// <returns></returns>
        new private HighlightInfoList GetHighlightInfos()
        {
            return null;
        }


        [NonSerialized()]
        private DomElement _HoverElement = null;
        /// <summary>
        /// 鼠标悬停的元素
        /// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public DomElement HoverElement
        {
            get
            {
                return _HoverElement;
            }
            set
            {
                _HoverElement = value;
                this.HighlightManager.HoverHighlightInfo = null;
                //if (_HoverElement != null)
                //{
                //    _HoverHighlightInfo = GetHighlightInfo(_HoverElement);
                //}
            }
        }

        /// <summary>
        /// 判断鼠标是否悬停在对象上面
        /// </summary>
        /// <param name="element">文档元素</param>
        /// <returns>是否悬停</returns>
        public bool IsHover(DomElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            if (this.HoverElement == null)
            {
                return false;
            }
            else
            {
                DomElement e = this.HoverElement;
                while (e != null)
                {
                    if (e == element)
                    {
                        return true;
                    }
                    e = e.Parent;
                }
                return false;
            }
        }

        /// <summary>
        /// 鼠标悬停元素改变事件处理
        /// </summary>
        /// <param name="oldHoverElement"></param>
        /// <param name="newHoverElement"></param>
        protected virtual void OnHoverElementChanged(
            DomElement oldHoverElement,
            DomElement newHoverElement)
        {
            if (this.EditorControl != null)
            {
                //if (oldHoverElement is XTextContainerElement)
                //{
                //    HighlightInfoList infos = ((XTextContainerElement)oldHoverElement).GetHighlightInfos();
                //    if (infos != null && infos.Count > 0)
                //    {
                //        foreach (HighlightInfo info in infos)
                //        {
                //            this.InvalidateView(info.Range);
                //        }
                //    }
                //}
                //if (newHoverElement is XTextContainerElement)
                //{
                //    HighlightInfoList infos = ((XTextContainerElement)newHoverElement).GetHighlightInfos();
                //    if (infos != null && infos.Count > 0)
                //    {
                //        foreach (HighlightInfo info in infos)
                //        {
                //            this.InvalidateView(info.Range);
                //        }
                //    }
                //}
                this.EditorControl.OnHoverElementChanged(oldHoverElement, newHoverElement);
            }
        }

        /// <summary>
        /// 处理用户界面事件
        /// </summary>
        /// <param name="args">事件参数</param>
        public override void HandleDocumentEvent(DocumentEventArgs args)
        {
            if (args.Style == DocumentEventStyles.MouseDown)
            {
                if (_EditorControl != null)
                {
                    _EditorControl.UseAbsTransformPoint = true;
                }
                DomElement element = GetElementAt(args.X, args.Y, true);// this.Content.GetElementAt(args.X, args.Y, true);
                if (element != null)
                {
                    // 冒泡方式触发文档元素事件
                    BubbleHandleElementEvent(element, args);

                    //XTextElementList list = GetEventPathElements(element);
                    //for (int iCount = list.Count - 1; iCount >= 0; iCount--)
                    //{
                    //    XTextElement e2 = list[iCount];
                    //    e2.HandleDocumentEvent(args);
                    //}
                    //element.HandleDocumentEvent(args);
                    //if (args.CancelBubble)
                    //{
                    //    return;
                    //}
                }
                //if (args.CancelBubble == false)
                //{
                //    _MouseCapture = new MouseCaptureInfo(args);
                //    this.Content.AutoClearSelection = !args.ShiftKey;
                //    this.Content.MoveTo(args.X, args.Y);
                //    //myBindControl.MoveTo( args.X , args.Y );
                //    if (this.EditorControl != null)
                //    {
                //        this.EditorControl.UpdateTextCaret();
                //        this.EditorControl.UseAbsTransformPoint = true;
                //    }
                //}
            }
            else if (args.Style == DocumentEventStyles.MouseMove)
            {
                if (args.Button == System.Windows.Forms.MouseButtons.None)
                {
                    _MouseCapture = null;
                }
                if (_MouseCapture != null)
                {
                    _MouseCapture.LastX = args.X;
                    _MouseCapture.LastY = args.Y;
                    this.Content.AutoClearSelection = false;
                    this.Content.MoveTo(args.X, args.Y);
                    this.Content.AutoClearSelection = true;
                    //myBindControl.MoveTo( args.X , args.Y );
                    this.EditorControl.MoveCaretWithScroll = false;
                    this.EditorControl.UpdateTextCaret();
                    this.EditorControl.MoveCaretWithScroll = true;
                }
                else
                {
                    if (args.Button == System.Windows.Forms.MouseButtons.None )
                    {
                        // 当不是严格命中文档,则鼠标光标实际上是在文档范围之外,此时当前鼠标悬浮的元素为空引用.
                        DomElement element = args.StrictMatch ? GetElementAt(args.X, args.Y, true) : null ;// this.Content.GetElementAt(args.X, args.Y, true);
                        if (element != this._HoverElement)
                        {
                            DomElementList parents1 = this.HoverElement == null ?
                                new DomElementList() : WriterUtils.GetParentList(this.HoverElement);
                            if (this.HoverElement != null)
                            {
                                parents1.Insert(0, this.HoverElement);
                            }
                            DomElementList parents2 = element == null ? 
                                new DomElementList() : WriterUtils.GetParentList(element);
                            if (element != null)
                            {
                                parents2.Insert(0, element);
                            }
                            // 触发鼠标离开文档元素事件
                            foreach (DomElement element2 in parents1)
                            {
                                if (parents2.Contains(element2) == false)
                                {
                                    DocumentEventArgs args2 = args.Clone();
                                    args2.intStyle = DocumentEventStyles.MouseLeave;
                                    args2.Element = element2;
                                    element2.HandleDocumentEvent(args2);
                                    if (args2.CancelBubble)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }//foreach
                            // 触发鼠标进入文档元素事件
                            foreach (DomElement element2 in parents2)
                            {
                                if (parents1.Contains(element2) == false)
                                {
                                    DocumentEventArgs args2 = args.Clone();
                                    args2.intStyle = DocumentEventStyles.MouseEnter;
                                    args2.Element = element2;
                                    element2.HandleDocumentEvent(args2);
                                    if (args2.CancelBubble)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }//foreach
                        }
                        if (element != this._HoverElement)
                        {
                            DomElement back = this._HoverElement;
                            this.HoverElement = element;
                            this.OnHoverElementChanged(back, element);
                        }
                        // 触发元素的鼠标移动事件
                        BubbleHandleElementEvent(element, args);
                    }//if
                    //args.Cursor = args.Cursor ;
                }
            }
            else if (args.Style == DocumentEventStyles.MouseUp)
            {
                _EditorControl.UseAbsTransformPoint = false;
                _MouseCapture = null;
                DomElement element = GetElementAt(args.X, args.Y, true);// this.Content.GetElementAt(args.X, args.Y, true);
                if (element != null)
                {
                    BubbleHandleElementEvent(element, args);
                    //element.HandleDocumentEvent(args);
                }
            }
            else if (args.Style == DocumentEventStyles.MouseLeave)
            {
                if (this.HoverElement != null)
                {
                    BubbleHandleElementEvent(this.HoverElement, args);
                    DomElement back = this.HoverElement;
                    this.HoverElement = null;
                    this.OnHoverElementChanged(back, null);
                }
            }
            else if (args.Style == DocumentEventStyles.KeyDown)
            {
                DomElement element = null;
                if (Math.Abs(this.Selection.Length) == 1)
                {
                    element = this.Selection.ContentElements[0];
                }
                else
                {
                    DomContainerElement container = null;
                    int index = 0;
                    this.Content.GetCurrentPositionInfo(out container, out index);
                    element = container;
                }
                BubbleHandleElementEvent(element, args);
            }
            else
            {
                base.HandleDocumentEvent(args);
            }
        }

        /// <summary>
        /// 进行冒泡式事件处理
        /// </summary>
        /// <param name="elements">要处理事件的元素列表</param>
        /// <param name="args">事件参数</param>
        private void BubbleHandleElementEvent(DomElementList elements, DocumentEventArgs args)
        {
            int x = args.X;
            int y = args.Y;
            args.CancelBubble = false;
            int endIndex = elements.Count - 1;
            // 首先过滤掉已经被逻辑删除的元素
            for (int iCount = 0; iCount < elements.Count; iCount++)
            {
                if (elements[iCount].Style.DeleterIndex >= 0 )
                {
                    endIndex = iCount - 1;
                    break;
                }
            }
            for (int iCount = 0; iCount <= endIndex; iCount++)//list.Count - 1; iCount >= 0; iCount--)
            {
                DomElement item = elements[iCount];
                if (args.Style == DocumentEventStyles.MouseDown
                       || args.Style == DocumentEventStyles.MouseMove
                       || args.Style == DocumentEventStyles.MouseUp)
                {
                    args.intX = (int)(x - item.AbsLeft);
                    args.intY = (int)(y - item.AbsTop);
                }
                args.Element = item;
                item.HandleDocumentEvent(args);
                if (args.CancelBubble)
                {
                    break;
                }
            }//for
            args.intX = x;
            args.intY = y;
        }

        private void BubbleHandleElementEvent(DomElement element, DocumentEventArgs args)
        {
            // 获得事件冒泡顺序列表
            DomElementList list = new DomElementList();
            DomElement item = element;
            while (item != null && item != this)
            {
                list.Add(item);
                item = item.Parent;
            }//while
            BubbleHandleElementEvent(list, args);
        }

        /// <summary>
        /// 获得文档视图中指定位置处的文档元素对象
        /// </summary>
        /// <param name="x">指定的X坐标</param>
        /// <param name="y">指定的Y坐标</param>
        /// <param name="strict">是否严格匹配</param>
        /// <returns>获得的文档元素</returns>
        public virtual DomElement GetElementAt(float x, float y , bool strict)
        {
            // 先搜索文档内容
            
            DomElement element = this.Content.GetElementAt(x, y , strict );
              
            return element ;
        }
         
        
        [NonSerialized]
        private DocumentContentRender _Render = new DocumentContentRender();

        /// <summary>
        /// 绘制文档内容的视图对象
        /// </summary>
        [Browsable( false )]
        [XmlIgnore()]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DocumentContentRender Render
        {
            get
            {
                if (_Render == null)
                {
                    _Render = new DocumentContentRender();
                }
                _Render.Document = this;
                return _Render;
            }
            set
            {
                _Render = value;
            }
        }

		public void RefreshSize( System.Drawing.Graphics g )
		{
            this.Width = this.PageSettings.ViewPaperWidth;
			g.PageUnit = this.DocumentGraphicsUnit ;
			g.TextRenderingHint =  System.Drawing.Text.TextRenderingHint.AntiAlias ;
            this.ContentStyles.UpdateState(g);
            DocumentContentRender view = this.Render;
            DocumentPaintEventArgs args = new DocumentPaintEventArgs(g, Rectangle.Empty);
            args.Graphics.PageUnit = this.DocumentGraphicsUnit;
            args.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.ContentStyles.UpdateState(args.Graphics);
            args.Render = this.Render;
            this.Render.ClearCharSizeBuffer();
            base.RefreshSize(args);
            this.Render.ClearCharSizeBuffer();
		}
         
        /// <summary>
        /// 采用视图单位的标准的页面高度
        /// </summary>
        internal float GetStandartPapeViewHeight( XPageSettings ps )
        {
            float height = ps.ViewClientHeight ;
            if (this.Header.Height > ps.ViewHeaderHeight)
            {
                height = height - ( this.Header.Height - ps.ViewHeaderHeight );
            }
            if( this.Footer.Height > ps.ViewFooterHeight )
            {
                height = height - ( this.Footer.Height - ps.ViewFooterHeight );  
            }
            return height;
        }

        [NonSerialized]
        private PageViewMode _PageViewMode = PageViewMode.Page;
        /// <summary>
        /// 页面视图方式
        /// </summary>
        [Browsable( false )]
        [XmlIgnore]
        public PageViewMode PageViewMode
        {
            get
            {
                return _PageViewMode; 
            }
            set
            {
                _PageViewMode = value; 
            }
        }

		/// <summary>
		/// 重新进行分页
		/// </summary>
		public void RefreshPages(  )
		{
            if (DCSoft.Common.StackTraceHelper.CheckRecursion())
            {
                // 若检测到递归则退出处理
                return ;
            }
            foreach (DomDocumentContentElement ce in this.Elements)
            {
                ce.Top = 0;
                float topCount = ce.Top;
                foreach (DomContentLine line in ce.Lines)
                {
                    line._OwnerPage = null;
                    if (line[0] is DomPageBreakElement)
                    {
                        ((DomPageBreakElement)line[0]).Handled = false;
                    }
                }
                //ce.Height = topCount;
            }//foreach

            DomDocumentContentElement body = this.Body;
            // 处理文档正文中的表格,删除由于标题行而添加的临时表格行
            List<DomContentElement> ceHasHeaderRow = new List<DomContentElement>();
            
            if (ceHasHeaderRow.Count > 0)
            {
                if (ceHasHeaderRow.Contains(body) == false)
                {
                    ceHasHeaderRow.Add(body);
                }
                foreach (DomContentElement ce in ceHasHeaderRow)
                {
                    ce.UpdateLinePosition(ce.ContentVertialAlign, false, false);
                }
            }

            body.Height = 0;
            if (body.PrivateLines.Count > 0)
            {
                body.Height = body.PrivateLines.LastLine.Bottom - body.PrivateLines[0].Top ;
            }
            this.PageSettings.ViewUnit = this.DocumentGraphicsUnit;
			this.Pages.Clear();
			//myPages.Reset( g );

            this.Pages.Top = (int)body.Top;
			int bodyHeight = ( int ) body.Height ;
			int LastPos = _Pages.Top ;
			//this.Pages.MinPageHeight = 15 ;
            _RawPageIndex = 0;
            if (this.EditorControl != null)
            {
                this.PageViewMode = this.EditorControl.ViewMode;
            }
            PageLineInfo info = new PageLineInfo();
            info.PageSettings = this.PageSettings;
            info.StdPageContentHeight = (int )this.PageSettings.ViewClientHeight;
            info.MinPageContentHeight = 50;

			while( this.Pages.Height < ( int ) body.Height )
			{
                PrintPage page = new PrintPage(
                    this,
                    this.PageSettings.Clone(), 
                    this.Pages,
                    0 ,
                    0 );
                page.Height = (int)GetStandartPapeViewHeight(this.PageSettings);
                if (page.Height < 50)
                {
                    // 若标准页高小于50则页面设置可能错误，退出处理
                    break;
                }
                page.DocumentHeight = ( int )this.Height;

                info.LastPosition = page.Top;
                info._CurrentPoistion = (int)page.Bottom;
                info.PageViewMode = this.PageViewMode;
                body.FixPageLine(info);
                page.Height = info.CurrentPoistion - page.Top;

                if (page.Height < info.MinPageContentHeight )
                {
                    page.Height = page.ViewStandardHeight;
                }
				LastPos = page.Bottom ;
				foreach( DomContentLine line in body.Lines )
				{
                    if (line.AbsTop < info.CurrentPoistion )
                    {
                        if (line.OwnerPage == null)
                        {
                            line._OwnerPage = page;
                        }
                   
                    }
                    else
                    {
                    }
				}//foreach
				this.Pages.Add( page );
			}//while
			if( this.Pages.Count > 0 )
			{
                body.GlobalUpdateLineIndex();
				this.Pages.LastPage.Height = 
					(int)( this.Pages.LastPage.Height - ( this.Pages.Height - body.Height ));
			}
            // 保存总页数
            this.Info.NumOfPage = this.Pages.Count;

            // 调整部分文档内容的排版，使得不会出现文档行跨页显示
            foreach (DomContentElement ce in this.Body.ContentElements)
            {
                RectangleF ceBounds = ce.AbsBounds;
                if (ce.PrivateLines.Count == 0)
                {
                    System.Console.WriteLine("");
                }
                float contentTop = ce.PrivateLines[0].AbsTop;
                float contentBottom = contentTop + ce.ContentHeight;
                
                foreach (PrintPage page in this.Pages)
                {
                    if (page.Top > contentTop && page.Top < contentBottom)
                    {
                        foreach (DomContentLine line in ce.PrivateLines)
                        {
                            if (page.Top > line.AbsTop +1 && page.Top < line.AbsBottom - 1 )
                            {
                                // 分页线跨过了文档行，需要调整文档行的位置
                                float dy = float.NaN;
                                if ( line.AbsBottom - page.Top < contentTop - ceBounds.Top)
                                {
                                    // 向上移动文档行
                                    dy = line.AbsBottom - page.Top;
                                }
                                else if (page.Top - line.AbsTop < ceBounds.Bottom - contentBottom)
                                {
                                    // 向下移动文档行
                                    dy = page.Top - line.AbsTop;
                                }
                                if (float.IsNaN(dy) == false)
                                {
                                    foreach (DomContentLine line2 in ce.PrivateLines)
                                    {
                                        line2.Top = line2.Top + dy;
                                    }
                                    goto NextCE;
                                }
                            }
                        }//foreach
                    }
                }//foreach
            NextCE: ;
            }//foreach
            this.PageRefreshed = true;
            if (this.Info != null)
            {
                this.Info.NumOfPage = this.Pages.Count;
            }
           
		}

		/// <summary>
		/// 对整个文档执行重新排版操作
		/// </summary>
		public override void ExecuteLayout()
		{
            ArrayList list = new ArrayList(this.Elements);
            foreach (DomDocumentContentElement ce in list)
            {
                ce.ExecuteLayout();
            }//foreach
		}

   	    /// <summary>
		/// 元素是否处于选择状态
		/// </summary>
		/// <param name="element">元素对象</param>
		/// <returns>是否选择</returns>
		public virtual bool IsSelected( DomElement element )
		{
            DomDocumentContentElement ce = element.DocumentContentElement;
            return ce.IsSelected(element);
		}

        /// <summary>
        /// 元素是否可见
        /// </summary>
        /// <param name="element">元素对象</param>
        /// <returns>是否可见</returns>
        public virtual bool IsVisible(DomElement element)
        {
            if (this.Options.SecurityOptions.ShowLogicDeletedContent == false)
            {
                if (element.Style.DeleterIndex >= 0)
                {
                    // 被逻辑删除了，因此不可见。
                    return false;
                }
            }
            return true;
        }

        public float PixelToDocumentUnit( float Value )
		{
			return GraphicsUnitConvert.Convert(
				Value , 
				System.Drawing.GraphicsUnit.Pixel ,
				this._DocumentGraphicsUnit );
		}

		public System.Drawing.Size PixelToDocumentUnit( System.Drawing.Size Value )
		{
			return GraphicsUnitConvert.Convert(
				Value ,
				System.Drawing.GraphicsUnit.Pixel ,
				this._DocumentGraphicsUnit );
		}

		public int ToPixel( int Value )
		{
			return GraphicsUnitConvert.Convert(
				Value ,
				this._DocumentGraphicsUnit ,
				System.Drawing.GraphicsUnit.Pixel );
		}

		public System.Drawing.Size ToPixel( System.Drawing.Size Value )
		{
			return GraphicsUnitConvert.Convert(
				Value ,
				this._DocumentGraphicsUnit ,
				System.Drawing.GraphicsUnit.Pixel );
		}

		#region IPageDocument 成员

	
		/// <summary>
		/// 文档坐标单位
		/// </summary>
		private System.Drawing.GraphicsUnit _DocumentGraphicsUnit 
			= System.Drawing.GraphicsUnit.Document  ;
		/// <summary>
		/// 文档坐标单位
		/// </summary>
		public System.Drawing.GraphicsUnit DocumentGraphicsUnit
		{
			get
			{
				return _DocumentGraphicsUnit ;
			}
			set
			{
				_DocumentGraphicsUnit = value;
			}
		}

        ///// <summary>
        ///// 一个像素的长度单位转换为文档视图单位
        ///// </summary>
        //[Browsable( false )]
        //public float PixelToDocumentGraphicsUnit
        //{
        //    get
        //    {
        //        return (float)GraphicsUnitConvert.Convert(1.0, GraphicsUnit.Pixel, this.DocumentGraphicsUnit);
        //    }
        //}

        private XPageSettings _PageSettings = new XPageSettings();
        /// <summary>
        /// 页面设置信息对象
        /// </summary>
        [System.ComponentModel.Category("Layout")]
        [System.ComponentModel.RefreshProperties(
            System.ComponentModel.RefreshProperties.All)]
        public XPageSettings PageSettings
        {
            get
            {
                if (_PageSettings == null)
                {
                    _PageSettings = new XPageSettings();
                    this.PageRefreshed = false;
                }
                return _PageSettings;
            }
            set
            {
                _PageSettings = value;
                if (_PageSettings == null)
                {
                    _PageSettings = new XPageSettings();
                }
                this.PageRefreshed = false;
            }
        }


        /// <summary>
        /// 页面集合
        /// </summary>
        [NonSerialized]
        private PrintPageCollection _Pages = new PrintPageCollection();
        /// <summary>
        /// 页面集合
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public PrintPageCollection Pages
        {
            get
            {
                if (_Pages == null)
                {
                    _Pages = new PrintPageCollection();
                }
                return _Pages;
            }
            set
            {
                _Pages = value;
            }
        }

        [NonSerialized()]
        private PrintPageCollection _GlobalPages = null;
        /// <summary>
        /// 全局页面集合
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public PrintPageCollection GlobalPages
        {
            get
            {
                if (_GlobalPages == null)
                    return this.Pages;
                else
                    return _GlobalPages;
            }
            set
            {
                _GlobalPages = value;
            }
        }

        [NonSerialized()]
        private bool _PageRefreshed = false;
        /// <summary>
        /// 文档已经执行的排版和分页操作
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public bool PageRefreshed
        {
            get
            {
                return _PageRefreshed;
            }
            set
            {
                _PageRefreshed = value;
            }
        }

        [NonSerialized]
        private int _RawPageIndex = 0;
        /// <summary>
        /// 内置的从0开始的页码号,不受PageIndexFix和多文档处理的影响.
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public int RawPageIndex
        {
            get
            {
                return _RawPageIndex;
            }
            set
            {
                _RawPageIndex = value;
            }
        }

        /// <summary>
        /// 显示的页码修正值
        /// </summary>
        private int _PageIndexfix = 0;
        /// <summary>
        /// 显示的页码修正值
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public int PageIndexfix
        {
            get
            {
                return _PageIndexfix;
            }
            set
            {
                _PageIndexfix = value;
            }
        }


        /// <summary>
        /// 从1开始计算的当前打印的页面序号
        /// </summary>
        [NonSerialized]
        private int _PageIndex = 1;
        /// <summary>
        /// 从1开始计算的当前打印的页面序号
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public int PageIndex
        {
            get
            {
                return _PageIndex;
            }
            set
            {
                if (_PageIndex != value)
                {
                    _PageIndex = value;
                }
            }
        }

        /// <summary>
        /// 绘制文档内容
        /// </summary>
        /// <param name="args">参数</param>
        public virtual void DrawContent(PageDocumentPaintEventArgs args)
        {
            this.PageIndex = args.PageIndex;
            DomDocumentContentElement ce = this.Body;
            switch (args.ContentStyle)
            {
                case PageContentPartyStyle.Body :
                    ce = this.Body;
                    break;
                case PageContentPartyStyle.Header :
                    ce = this.Header;
                    if (ce.HasContentElement == false)
                    {
                        if (this.CurrentContentElement != ce)
                        {
                            return;
                        }
                    }
                    break;
                case PageContentPartyStyle.Footer :
                    ce = this.Footer;
                    if (ce.HasContentElement == false)
                    {
                        if (this.CurrentContentElement != ce)
                        {
                            return;
                        }
                    }
                    break;
            }//switch

            DocumentPaintEventArgs args2 = new DocumentPaintEventArgs(
                args.Graphics,
                args.ClipRectangle);
            args2.Document = this;
            args2.Type = args.ContentStyle;
            args2.ActiveMode = (ce == this.CurrentContentElement);
            args2.DocumentContentElement = ce;
            args2.Render = this.Render;
            if (args.RenderMode == ContentRenderMode.Print)
            {
                args2.RenderStyle = DocumentRenderStyle.Print;
            }
            else
            {
                if (this.Printing)
                {
                    args2.RenderStyle = DocumentRenderStyle.Print;
                }
                else
                {
                    args2.RenderStyle = DocumentRenderStyle.Paint;
                }
            }
            //args.ViewBoundsF = e.AbsBounds;
            args2.Element = ce;
            args2.Style = ce.RuntimeStyle;
            args2.Graphics.TextRenderingHint = this.Options.ViewOptions.TextRenderStyle;
            args2.PageClipRectangle = args.ContentBounds;
            ce.DrawContent(args2);
            if (ce == this.Header && this.Options.ViewOptions.ShowHeaderBottomLine )
            {
                if (ce.Content.Count > 0)
                {
                    // 当页眉有内容时显示下边框线
                    RectangleF bounds = ce.AbsBounds ;
                    args.Graphics.DrawLine(Pens.Black, ce.Left, ce.Bottom, ce.Right, ce.Bottom);
                }
            }
        }

		#endregion

        public override string ToString()
        {
            return "Document:" + this.Info.Title;
        }

        #region 文档参数相关代码群 *********************************************

        [NonSerialized]
        private object _ServerObject = null;
        /// <summary>
        /// 服务器对象
        /// </summary>
        [Browsable( false )]
        [XmlIgnore()]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        public object ServerObject
        {
            get
            {
                return _ServerObject; 
            }
            set
            {
                _ServerObject = value; 
            }
        }


        #endregion
         

        #region 编辑器直接调用的方法 *******************************************************

        /// <summary>
        /// 设置文档默认样式
        /// </summary>
        /// <param name="newStyle">新默认样式</param>
        /// <param name="logUndo">是否记录撤销操作信息</param>
        internal void EditorSetDefaultStyle(DocumentContentStyle newStyle, bool logUndo)
        {
            if (newStyle == null)
            {
                throw new ArgumentNullException("newStyle");
            }
            if (logUndo)
            {
                if (this.BeginLogUndo())
                {
                    this.UndoList.Add(new XTextUndoSetDocumentProperty(
                        this ,
                        "DefaultStyle", 
                        this.DefaultStyle.Clone(),
                        newStyle));
                    this.EndLogUndo();
                }
            }
            this.ContentStyles.Default = newStyle;
            //this.ContentStyles.RefreshRuntimeStyleList();
            this.Modified = true;
            if (this.EditorControl != null)
            {
                this.EditorControl.RefreshDocument();
            }
            //this.OnSelectionChanged();
        }

        /// <summary>
        /// 在编辑器中设置段落样式
        /// </summary>
        /// <param name="newStyles">新段落样式</param>
        /// <param name="logUndo">是否记录撤销信息</param>
        /// <returns>操作的段落元素对象列表</returns>
        internal DomElementList EditorSetParagraphStyle(
            Dictionary<DomElement, int> newStyleIndexs,
            bool logUndo )
        {
            if (newStyleIndexs == null)
            {
                throw new ArgumentNullException("newStyleIndexs");
            }

            //XTextElementList modifiedElements = new XTextElementList();
            //XTextContentElement ce = null;
           
            XTextUndoSetElementStyle undo = null;
            if (logUndo)
            {
                undo = new XTextUndoSetElementStyle();
                undo.Document = this;
                undo.ParagraphStyle = true;
            }

            List<DomContentLine> invalidateLines = new List<DomContentLine>();
            Dictionary<DomContentElement, DomElementList> modifiedElements
                = new Dictionary<DomContentElement,DomElementList>();
            DomElementList result = new DomElementList();
            foreach (DomParagraphFlagElement p in newStyleIndexs.Keys )
            {
                if (this.DocumentControler.CanModify(p) == false)
                {
                    // 不能修改属性
                    continue;
                }
                DomContentElement ce = p.ContentElement;

                if (undo != null)
                {
                    undo.AddInfo(p, p.StyleIndex, newStyleIndexs[p]);
                }
                // 旧的段落样式
                DocumentContentStyle oldStyle = p.RuntimeStyle;
                p.StyleIndex = newStyleIndexs[p];
                // 段落样式被修改了，不再是自动创建的了。
                p.AutoCreate = false;
                p.UpdateContentVersion();
                result.Add(p);
                // 新的段落样式
                DocumentContentStyle newStyle = p.RuntimeStyle;
                // 比较一些重要的段落样式属性，看看是否可以避免文档内容重新分行
                bool refreshLine = true ;
                //if (oldStyle.LeftIndent == newStyle.LeftIndent
                //    && oldStyle.FirstLineIndent == newStyle.FirstLineIndent
                //    && oldStyle.BulletedList == newStyle.BulletedList
                //    && oldStyle.NumberedList == newStyle.NumberedList
                //    && oldStyle.Align == newStyle.Align )
                //{
                //    refreshLine = false;
                //}
                if (modifiedElements.ContainsKey(ce) == false )
                {
                    modifiedElements[ce] = new DomElementList();
                }

                modifiedElements[ce].Add(p.FirstContentElement);
                modifiedElements[ce].Add(p);
                
                if (refreshLine)
                {
                    // 设置段落所在的文本行状态无效，需要放弃。
                    DomContentLine line = p.FirstContentElement.OwnerLine;
                    DomContentLine line2 = p.OwnerLine;
                    if (line != null && line2 != null)
                    {
                        int endIndex = ce.PrivateLines.IndexOf(line2);
                        for (int iCount = ce.PrivateLines.IndexOf(line);
                            iCount <= endIndex;
                            iCount++)
                        {
                            invalidateLines.Add(ce.PrivateLines[iCount]);
                            ce.PrivateLines[iCount].InvalidateState = true;
                        }//for
                    }
                }
                else
                {
                    
                }
            }//foreach

            if (modifiedElements.Count > 0)
            {
                if (logUndo )
                {
                    if (this.BeginLogUndo())
                    {
                        this.UndoList.Add(undo);
                        this.EndLogUndo();
                    }
                }

                //this.UpdateContentVersion();
                this.Modified = true;
                bool flag = false;
                foreach (DomContentElement ce in modifiedElements.Keys)
                {
                    ce.RefreshParagraphState(null);
                    ce.RefreshContentByElements(modifiedElements[ce], true , false);
                    if (ce.DocumentContentElement == this.CurrentContentElement)
                    {
                        flag = true;
                    }
                    ContentChangedEventArgs args = new ContentChangedEventArgs();
                    args.Document = this;
                    args.Element = ce;
                    ce.RaiseBubbleOnContentChanged(args);
                }
                if (this.EditorControl != null)
                {
                    this.EditorControl.UpdateTextCaret();
                }
                if ( flag )
                {
                    if (this.EditorControl != null)
                    {
                        this.EditorControl.Invalidate();
                    }
                    this.OnSelectionChanged();
                }
            }
            return result;
        }

        /// <summary>
        /// 在编辑器中设置元素样式
        /// </summary>
        /// <param name="newStyleIndexs">新元素样式编号</param>
        /// <param name="logUndo">是否记录撤销信息</param>
        /// <returns>操作修改的元素列表</returns>
        public DomElementList EditorSetElementStyle(
            Dictionary<DomElement, int> newStyleIndexs,
            bool logUndo)
        {
            Dictionary<DomContentElement, DomElementList> modifiedElements
                = new Dictionary<DomContentElement, DomElementList>();
            XTextUndoSetElementStyle undo = null;
            if (logUndo)
            {
                undo = new XTextUndoSetElementStyle();
                undo.Document = this;
                undo.ParagraphStyle = false;
            }
            DomElementList result = new DomElementList();
            List<DomContainerElement> containers = new List<DomContainerElement>();
            using (System.Drawing.Graphics g = this.CreateGraphics())
            {
                foreach (DomElement element in newStyleIndexs.Keys)
                {
                    if (this.DocumentControler.CanModify(element) == false)
                    {
                        // 不能修改元素属性
                        continue;
                    }
                    DomContentElement ce = element.ContentElement;
                    if (undo != null)
                    {
                        undo.AddInfo(element, element.StyleIndex, newStyleIndexs[element]);
                    }
                    element.StyleIndex = newStyleIndexs[element];
                    if (containers.Contains(element.Parent) == false)
                    {
                        containers.Add(element.Parent);
                    }
                    // 触发元素样式发生改变事件
                    element.OnStyleChanged();
                    element.UpdateContentVersion();
                    if (element is DomContentElement)
                    {
                        DomContentElement ce2 = (DomContentElement)element;
                        ce2.UpdateLinePosition(ce.ContentVertialAlign, false, false);
                    }
                    element.SizeInvalid = true;
                    result.Add(element);
                    if (ce.PrivateContent.Contains(element.FirstContentElement))
                    {
                        if (modifiedElements.ContainsKey(ce))
                        {
                            modifiedElements[ce].Add(element.FirstContentElement);
                        }
                        else
                        {
                            DomElementList list = new DomElementList();
                            list.Add(element.FirstContentElement);
                            modifiedElements[ce] = list;
                        }
                    }
                    this.Render.RefreshSize(element, g);
                }//foreach
            }

            if (modifiedElements.Count > 0)
            {
                if (logUndo)
                {
                    if (this.BeginLogUndo())
                    {
                        this.UndoList.Add(undo);
                        this.EndLogUndo();
                    }
                }
                //this.UpdateContentVersion();
                this.Modified = true;
                bool refreshPage = false;
                foreach (DomContentElement ce in modifiedElements.Keys)
                {
                    ce.RefreshContentByElements(modifiedElements[ce], true, true);
                    if (ce._NeedRefreshPage)
                    {
                        refreshPage = true;
                    }
                    
                }
                if (containers.Count > 0)
                {
                    foreach (DomContainerElement c in containers)
                    {
                        // 触发容器元素内容发生改变事件
                        ContentChangedEventArgs cde = new ContentChangedEventArgs();
                        cde.Document = this;
                        cde.Element = c;
                        c.RaiseBubbleOnContentChanged(cde);
                    }
                }
                if (refreshPage)
                {
                    // 需要刷新分页
                    this.PageRefreshed = false;
                    this.RefreshPages();
                    if (this.EditorControl != null)
                    {
                        this.EditorControl.UpdatePages();
                        this.EditorControl.UpdateTextCaret();
                        this.EditorControl.Invalidate();
                    }
                }
                else
                {
                    if (this.EditorControl != null)
                    {
                        this.EditorControl.UpdateTextCaret();
                        this.EditorControl.Invalidate();
                    }
                }
                this.OnSelectionChanged();
            }
            return result;
        }


        #endregion

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="Deeply">是否</param>
        /// <returns></returns>
        public override DomElement Clone(bool Deeply)
        {
            DomDocument doc = (DomDocument)base.Clone(Deeply);
            doc.CopyContent(this , false );
            return doc;
        } 
    }
}