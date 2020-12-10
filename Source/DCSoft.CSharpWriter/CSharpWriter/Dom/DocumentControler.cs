/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DCSoft.Common;
using DCSoft.CSharpWriter.Controls ;
using DCSoft.Drawing;
using DCSoft.CSharpWriter.RTF;
using System.ComponentModel;
using System.Windows.Forms;
using DCSoft.CSharpWriter.Security;
using System.Drawing;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 文档控制器
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class DocumentControler
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DocumentControler()
        {
        }
         

        private bool _IsAdministrator = false;
        /// <summary>
        /// 是否以管理员模式运行
        /// </summary>
        /// <remarks>已管理员模式运行,则程序在保证文档结构正常的情况下,
        /// 文档中的任何内容都可以删除,文档中的任何部位都能插入,不受授权控制、域只读的限制。
        /// 本属性受到文本编辑器控件的Readonly属性的限制.</remarks>
        public bool IsAdministrator
        {
            get
            { 
                return _IsAdministrator; 
            }
            set
            {
                _IsAdministrator = value; 
            }
        }

        //private int _CurrentAccessLevel = 0;
        ///// <summary>
        ///// 当前访问权限等级
        ///// </summary>
        //public int CurrentAccessLevel
        //{
        //    get
        //    {
        //        return _CurrentAccessLevel; 
        //    }
        //    set
        //    {
        //        _CurrentAccessLevel = value; 
        //    }
        //}

        //private bool _EnableAccessLevel = false;
        ///// <summary>
        ///// 启用AccessLevel权限控制
        ///// </summary>
        //[System.ComponentModel.Browsable(false)]
        //public bool EnableAccessLevel
        //{
        //    get
        //    {
        //        return _EnableAccessLevel;
        //    }
        //    set
        //    {
        //        _EnableAccessLevel = value;
        //    }
        //}

        //private bool _HideHightLevelContent = true;
        ///// <summary>
        ///// 是否隐藏高权限的文档内容
        ///// </summary>
        //public bool HideHightLevelContent
        //{
        //    get
        //    {
        //        return _HideHightLevelContent; 
        //    }
        //    set
        //    {
        //        _HideHightLevelContent = value; 
        //    }
        //}

        //private bool _ReadonlyHightLevelContent = true;
        ///// <summary>
        ///// 高权限的文档内容是否只读
        ///// </summary>
        //public bool ReadonlyHightLevelContent
        //{
        //    get
        //    {
        //        return _ReadonlyHightLevelContent; 
        //    }
        //    set
        //    {
        //        _ReadonlyHightLevelContent = value; 
        //    }
        //}
        

        private DomDocument _Document = null;
        /// <summary>
        /// 控制器操作的文档对象
        /// </summary>
        public DomDocument Document
        {
            get
            {
                return _Document; 
            }
            set
            {
                _Document = value; 
            }
        }

        private WriterAppHost _AppHost = null;
        /// <summary>
        /// 编辑器宿主对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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


        /// <summary>
        /// 数据过滤器
        /// </summary>
        public FilterValueEventHandler ValueFilter
        {
            get
            {
                return (FilterValueEventHandler)this.AppHost.Services.GetService(typeof(FilterValueEventHandler));
            }
            set
            {
                if (value == null)
                {
                    this.AppHost.Services.RemoveService(typeof(FilterValueEventHandler));
                }
                else
                {
                    this.AppHost.Services.AddService(typeof(FilterValueEventHandler), value);
                }
            }
        }

        [NonSerialized]
        private CSWriterControl _EditorControl = null;
        /// <summary>
        /// 控制器操作的文本编辑器控件对象
        /// </summary>
        public CSWriterControl EditorControl
        {
            get
            {
                return _EditorControl; 
            }
            set
            {
                _EditorControl = value; 
            }
        }

        /// <summary>
        /// 控件是否只读
        /// </summary>
        public bool EditorControlReadonly
        {
            get
            {
                return this.EditorControl != null && this.EditorControl.Readonly;
            }
        }

        /// <summary>
        /// 文档内容呈现器
        /// </summary>
        public DocumentContentRender  Render
        {
            get
            {
                if (_Document != null)
                {
                    return _Document.Render;
                }
                if (_EditorControl != null)
                {
                    return _EditorControl.ContentRender;
                }
                return null;
            }
        }


        private DomContent Content
        {
            get
            {
                return this.Document.Content;
            }
        }

        private DomDocumentContentElement DocumentContent
        {
            get
            {
                return this.Document.CurrentContentElement;
            }
        }

        private DomSelection Selection
        {
            get
            {
                return this.Document.Selection;
            }
        }

        public bool SetSeletion(int startIndex, int length)
        {
            return this.Document.CurrentContentElement.SetSelection(startIndex, length);
        }

        /// <summary>
        /// 判断父元素能否容纳指定的子元素
        /// </summary>
        /// <param name="parentElement">父元素</param>
        /// <param name="element">子元素</param>
        /// <returns>能否容纳子元素</returns>
        public virtual bool AcceptChildElement( DomElement parentElement , DomElement element , DomAccessFlags flags)
        {
            if (parentElement == null)
            {
                throw new ArgumentNullException("parentElement");
            }
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return AcceptChildElement( parentElement , element.GetType() , flags );
        }

        /// <summary>
        /// 判断父元素能否容纳指定类型的子元素
        /// </summary>
        /// <param name="parentElement">父元素对象</param>
        /// <param name="elementType">子元素列表</param>
        /// <returns>能否容纳子元素</returns>
        public virtual bool AcceptChildElement( DomElement parentElement , Type elementType , DomAccessFlags flags )
        {
            if (parentElement == null)
            {
                throw new ArgumentNullException("parentElement");
            }
            if (elementType == null)
            {
                throw new ArgumentNullException("elementType");
            }
            if (parentElement is DomContainerElement)
            {
                ElementType acceptType = ((DomContainerElement)parentElement).AcceptChildElementTypes;
                ElementType et = WriterUtils.GetElementType(elementType);
                if (et != ElementType.None)
                {
                    if ((acceptType & et) != et)
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }

        ///// <summary>
        ///// 查询元素是否可以删除
        ///// </summary>
        ///// <param name="element">元素对象</param>
        ///// <returns>是否可以删除</returns>
        //public virtual bool QueryDelete(XTextElement element)
        //{
        //    return true;
        //}


        /// <summary>
        /// 判断是否可以接受处理OLE拖拽事件
        /// </summary>
        /// <param name="element">处理的元素</param>
        /// <param name="e">拖拽事件参数</param>
        /// <param name="ViewX">使用视图坐标的拖拽位置的X坐标</param>
        /// <param name="ViewY">使用视图坐标的拖拽位置的Y坐标</param>
        /// <returns>是否可以接受拖拽来的数据</returns>
        public virtual bool CanDragDrop(
            DomElement element,
            System.Windows.Forms.DragEventArgs e,
            float ViewX,
            float ViewY)
        {
            //this.Content.AutoClearSelection = true;
            //this.EditorControl.ForceShowCaret = true;

            //if (this.EditorControl != null)
            //{
            //    this.EditorControl.UseAbsTransformPoint = true;
            //}
            //this.Content.MoveTo(ViewX, ViewY);
            //if (this.EditorControl != null)
            //{
            //    this.EditorControl.UseAbsTransformPoint = false;
            //}

            return CanInsertObject(
                this.Document.Content.IndexOf( element ),
                e.Data ,
                null , 
                DomAccessFlags.Normal );
        }
        /// <summary>
        /// 处理OLE拖拽事件
        /// </summary>
        /// <param name="element">处理的元素</param>
        /// <param name="e">拖拽事件参数</param>
        /// <param name="ViewX">使用视图坐标的拖拽位置的X坐标</param>
        /// <param name="ViewY">使用视图坐标的拖拽位置的Y坐标</param>
        /// <returns>操作是否成功</returns>
        public virtual void DragDrop(
            DomElement element,
            System.Windows.Forms.DragEventArgs e,
            float ViewX,
            float ViewY)
        {
            //this.Content.AutoClearSelection = true;
            //this.EditorControl.ForceShowCaret = false;

            //if (this.EditorControl != null)
            //{
            //    this.EditorControl.UseAbsTransformPoint = true;
            //}
            //this.Content.MoveTo(ViewX, ViewY);
            //if (this.EditorControl != null)
            //{
            //    this.EditorControl.UseAbsTransformPoint = false;
            //}

            //myContent.MoveTo( ViewX , ViewY );
            InsertObject(e.Data, null);
        }

        /// <summary>
        /// 目前支持的数据格式名称
        /// </summary>
        public static readonly string[] SupportDataFormats = new string[] 
            {
                DataObjectHelper.Format_FileNameW ,
                DataFormats.Rtf ,
                DataFormats.Bitmap ,
                DataFormats.Text ,
                XMLDataFormat 
            };

        /// <summary>
        /// 判断能否在文档的当前位置插入元素
        /// </summary>
        /// <param name="data">要插入的数据</param>
        /// <param name="specifyFormat">指定的数据格式</param>
        /// <returns>能否插入元素</returns>
        public virtual bool CanInsertObject(
            int specifyPosition ,
            System.Windows.Forms.IDataObject data ,
            string specifyFormat ,
            DomAccessFlags flags )
        {
            if (data == null)
            {
                return false;
            }
            if (this.EditorControlReadonly )
            {
                // 控件只读
                return false;
            }
            if (specifyFormat != null )
            {
                specifyFormat = specifyFormat.Trim();
                if (specifyFormat.Length == 0)
                {
                    specifyFormat = null;
                }
            }
            if (specifyFormat != null)
            {
                if (data.GetDataPresent(specifyFormat) == false)
                {
                    return false;
                }
            }
            // 判断当前位置能否插入元素
            if (this.Document == null)
            {
                return false;
            }
            //XTextElement element = this.Document.CurrentElement;
            //if (element == null)
            //{
            //    return false;
            //}
            DomDocumentContentElement dce = this.Document.CurrentContentElement;// element.DocumentContentElement;
            DomContainerElement container = null;
            if (specifyPosition < 0 || specifyPosition >= dce.Content.Count)
            {
                DomElement element = this.Document.CurrentElement;
                if (element == null)
                {
                    return false;
                }
                specifyPosition = dce.Content.IndexOf(element);
            }
            int index = 0;
            dce.Content.GetPositonInfo(
                specifyPosition ,
                out container,
                out index ,
                dce.Content.LineEndFlag );
            if (this.CanInsert(container, index, typeof( DomElement ) , flags ) == false)
            {
                return false;
            }
            
            DataObjectHelper helper = new DataObjectHelper(data);
            if (specifyFormat == DataObjectHelper.Format_FileNameW || specifyFormat == null)
            {
                
                if (specifyFormat != null)
                {
                    return false;
                }
            }
            if (specifyFormat == DataFormats.Rtf || specifyFormat == null)
            {
                if (helper.HasRtf)
                {
                    return true;
                }
                if (specifyFormat != null)
                {
                    return false;
                }
            }
             
            if (specifyFormat == DataFormats.Text || specifyFormat == null)
            {
                if (helper.HasText)
                {
                    return true;
                }
                if (specifyFormat != null)
                {
                    return false;
                }
            }
            if (specifyFormat == XMLDataFormat || specifyFormat == null)
            {
                if (data.GetDataPresent(XMLDataFormat))
                {
                    return true;
                }
                if (specifyFormat != null)
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 在文档的当前位置插入数据
        /// </summary>
        /// <param name="data">要插入的数据</param>
        /// <returns>操作是否成功</returns>
        public virtual bool InsertObject(System.Windows.Forms.IDataObject data , string specifyFormat )
        {
            if (data == null)
            {
                return false;
            }

            if (specifyFormat != null )
            {
                specifyFormat = specifyFormat.Trim();
                if (specifyFormat.Length == 0)
                {
                    specifyFormat = null;
                }
            }
            if (specifyFormat != null)
            {
                // 判断是否存在指定的数据格式
                if (data.GetDataPresent(specifyFormat) == false)
                {
                    return false;
                }
            }
            
            DataObjectHelper helper = new DataObjectHelper(data);
             
            if (specifyFormat == XMLDataFormat || specifyFormat == null)
            {
                if (data.GetDataPresent(XMLDataFormat))
                {
                    // 插入XML序列化的数据
                    if (this.CanInsertAtCurrentPosition)
                    {
                        string xml = (string)data.GetData(XMLDataFormat);
                        if (xml != null && xml.Length > 0)
                        {
                            System.IO.StringReader reader = new System.IO.StringReader(xml);
                            DomDocument document = DocumentLoader.LoadXmlFileWithCreateDocument(
                                reader,
                                this.Document );

                            //document.Body.UpdateContentElements(true);
                            DomElementList list = document.Body.Elements.Clone();
                            document.UpdateElementState();
                            if (list != null && list.Count > 0)
                            {
                                if (this.ValueFilter != null)
                                {
                                    // 调用数据过滤器
                                    FilterValueEventArgs args = new FilterValueEventArgs(
                                        InputValueSource.Clipboard , 
                                        InputValueType.Dom ,
                                        list );
                                    this.ValueFilter(this, args);
                                    if (args.Cancel)
                                    {
                                        // 用户取消操作
                                        return false;
                                    }
                                    list = args.Value as DomElementList ;
                                    if (list == null || list.Count == 0)
                                    {
                                        // 过滤掉了所有的数据，结束操作
                                        return false;
                                    }
                                }
                                if (list.LastElement is DomParagraphFlagElement)
                                {
                                    DomParagraphFlagElement flag = (DomParagraphFlagElement)list.LastElement;
                                    if (flag.AutoCreate == true)
                                    {
                                        list.RemoveAt(list.Count - 1);
                                    }
                                    if (list.Count == 0)
                                    {
                                        return false;
                                    }
                                }
                                this.Document.BeginLogUndo();
                                if (this.DocumentContent.HasSelection)
                                {
                                    this.Content.DeleteSelection(true, false, false);
                                }
                                this.Document.ImportElements(list);
                                this.InnerInsertElements(list, false);
                                this.Document.EndLogUndo();
                                this.Document.OnDocumentContentChanged();
                                return true;
                            }
                        }
                    }
                    return false;
                }
                if (specifyFormat != null)
                {
                    return false;
                }
            }
            if (specifyFormat == DataFormats.Rtf || specifyFormat == null)
            {
                if (helper.HasRtf)
                {
                    // 插入RTF文档
                    if (this.CanInsertAtCurrentPosition)
                    {
                        string rtf = helper.Rtf;
                        return InsertRTF(rtf, true , InputValueSource.Clipboard );
                    }
                    else
                    {
                        return false;
                    }
                }
                if (specifyFormat != null)
                {
                    return false;
                }
            }
            if (specifyFormat == DataFormats.Text || specifyFormat == null)
            {
                if (helper.HasText)
                {
                    // 插入纯文本
                    if (this.CanInsertAtCurrentPosition)
                    {
                        this.InsertString(helper.Text );
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (specifyFormat != null)
                {
                    return false;
                }
            }
            return false;
        }
         
        /// <summary>
        /// 在当前位置插入RTF文档
        /// </summary>
        /// <param name="rtfText">RTF文本</param>
        /// <returns>操作是否成功</returns>
        public bool InsertRTF(string rtfText)
        {
            return InsertRTF(rtfText, true, InputValueSource.Unknow );
        }

        /// <summary>
        /// 在当前位置插入RTF文档
        /// </summary>
        /// <param name="rtfText">RTF文本</param>
        /// <param name="logUndo">是否记录撤销操作</param>
        public virtual bool InsertRTF(string rtfText  , bool logUndo , InputValueSource inputSource )
        {
            if (this.ValueFilter != null)
            {
                // 调用数据过滤器
                FilterValueEventArgs args = new FilterValueEventArgs( inputSource , InputValueType.RTF, rtfText);
                this.ValueFilter(this, args);
                if (args.Cancel)
                {
                    // 取消操作
                    return false;
                }
                rtfText = args.Value as string;
                if (string.IsNullOrEmpty(rtfText))
                {
                    // 数据全部过滤掉了，操作失败
                    return false;
                }
            }
            if (rtfText == null || rtfText.Length == 0)
            {
                throw new ArgumentNullException("rtfText");
            }
            RTFLoader loader = new RTFLoader();
            loader.EnableDocumentSetting = false;
            loader.ImportTemplateGenerateParagraph = false;
            loader.LoadRTFText(rtfText);
            DomElementList list = loader.ImportContent(this.Document);
            if (list != null && list.Count > 0)
            {
                // 不导入页眉页脚部分
                for (int iCount = list.Count - 1; iCount >= 0; iCount--)
                {
                    if (list[iCount] is XTextDocumentFooterElement
                        || list[iCount] is XTextDocumentHeaderElement)
                    {
                        list.RemoveAt(iCount);
                    }
                }
                if ( list.Count > 0 && this.ValueFilter != null)
                {
                    // 调用数据过滤器
                    FilterValueEventArgs args = new FilterValueEventArgs(inputSource, InputValueType.Dom, list);
                    this.ValueFilter(this, args);
                    if (args.Cancel)
                    {
                        // 用户取消操作
                        return false;
                    }
                    list = args.Value as DomElementList;
                    if (list == null || list.Count == 0)
                    {
                        // 数据全部被过滤掉了
                        return false;
                    }
                }
                if (logUndo)
                {
                    this.Document.BeginLogUndo();
                }
                if (this.DocumentContent.HasSelection)
                {
                    this.Content.DeleteSelection(true, false, false);
                }
                this.InnerInsertElements(list, false);
                if (logUndo)
                {
                    this.Document.EndLogUndo();
                }
                this.Document.OnDocumentContentChanged();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <returns>本操作是否删除了元素</returns>
        public virtual bool DeleteSelectionOld()
        {
            int index = -1;
            this.Document.BeginLogUndo();
            if (this.DocumentContent.HasSelection)
            {
                index = this.Content.DeleteSelectionOld(true);
            }
            else
            {
                index = this.Content.DeleteCurrentElement(true);
            }
            this.Document.EndLogUndo();
            if (index >= 0)
            {
                if (index > 0)
                {
                    index--;
                }
                this.Document.OnDocumentContentChanged();
                //this.Document.CurrentContentElement.RefreshPrivateContent(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <returns>本操作是否删除了元素</returns>
        public virtual bool Delete()
        {
            int index = -1;
            this.Document.BeginLogUndo();
            if (this.DocumentContent.HasSelection)
            {
                index = this.Content.DeleteSelection(true, false, false);
            }
            else
            {
                index = this.Content.DeleteCurrentElement( true );
            }
            this.Document.EndLogUndo();
            if (index >= 0)
            {
                if (index > 0)
                {
                    index--;
                }
                this.Document.OnDocumentContentChanged();
                //this.Document.CurrentContentElement.RefreshPrivateContent(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 执行剪切操作
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool Cut()
        {
            if ( this.DocumentContent.HasSelection )
            {
                this.Copy();
                this.Delete();
                return true;
            }
            return false;
        }

        public bool CanCut()
        {
            if (this.DocumentContent.HasSelectionWithouLogicDeleted )
            {
                return this.CanDeleteSelection;
                //XTextElementList list = this.Document.Selection.ContentElements;
                //if (list != null && list.Count > 0)
                //{
                //    foreach (XTextElement element in this.Document.Selection.ContentElements)
                //    {
                //        if (this.CanDelete(element))
                //        {
                //            // 只要有一个元素可以被删除那就能执行剪切操作
                //            return true;
                //        }
                //    }
                //    return false;
                //}
                //else
                //{
                //    return true;
                //}
            }
            return false;
        }
        /// <summary>
        /// 判断能否执行粘贴操作
        /// </summary>
        public bool CanPaste
        {
            get
            {
                return CanInsertObject(
                    -1,
                    System.Windows.Forms.Clipboard.GetDataObject() ,
                    null,
                    DomAccessFlags.Normal  );
            }
        }

        ///// <summary>
        ///// 判断能否执行粘贴操作
        ///// </summary>
        //public bool CanSpecifyPaste( IDataObject data , string specifyFormat )
        //{
        //    get
        //    {
        //        return CanInsertObject( data , specifyFormat );
        //    }
        //}

        /// <summary>
        /// 执行粘贴操作
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool Paste()
        {
            return this.InsertObject(System.Windows.Forms.Clipboard.GetDataObject() ,null );
        }

        /// <summary>
        /// 执行粘贴操作
        /// </summary>
        /// <param name="specifyFormat">指定的数据格式</param>
        /// <returns>操作是否成功</returns>
        public bool Paste( string specifyFormat )
        {
            return this.InsertObject(System.Windows.Forms.Clipboard.GetDataObject(), specifyFormat);
        }



        /// <summary>
        /// 判断能否执行复制操作
        /// </summary>
        public bool CanCopy
        {
            get
            {
                return this.DocumentContent.HasSelectionWithouLogicDeleted  ;
            }
        }

        //private string BinaryDataFormat
        //{
        //    get
        //    {
        //        return "XTextDocumentBianry V:" + this.GetType().Assembly.GetName().Version;
        //    }
        //}

        private static string XMLDataFormat
        {
            get
            {
                return "DCWriterXML V:" + typeof( DocumentControler ).Assembly.GetName().Version;
            }
        }


        /// <summary>
        /// 执行复制操作
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool Copy()
        {
            System.Windows.Forms.DataObject obj = CreateSelectionDataObject( );
            if (obj != null)
            {
                System.Windows.Forms.Clipboard.SetDataObject(obj, true);
                return true;
            }
            else
            {
                return false;
            }
            //XTextSelection selection = this.Selection;
            //if ( selection != null && selection.Length != 0 )
            //{
            //    System.Windows.Forms.DataObject obj = new System.Windows.Forms.DataObject();
            //    // 设置纯文本数据
            //    obj.SetData(System.Windows.Forms.DataFormats.Text, selection.Text );
            //    // 设置RTF数据
            //    string rtf = selection.RTFText;// this.GetRTFText( true );
            //    if (rtf != null && rtf.Length > 0)
            //    {
            //        obj.SetData(System.Windows.Forms.DataFormats.Rtf, rtf);
            //    }
            //    // 设置图片数据
            //    if (selection.Length == 1 && selection.Mode == ContentRangeMode.Content
            //        && selection.ContentElements[0] is XTextImageElement)
            //    {
            //        XTextImageElement img = (XTextImageElement)selection.ContentElements[0];
            //        if (img.Image.Value != null)
            //        {
            //            obj.SetImage(img.Image.Value);
            //        }
            //    }
            //    // 设置XML数据
            //    using (XTextDocument selectionDocument = selection.CreateDocument())
            //    {
            //        selectionDocument.ScriptText = null;
            //        System.IO.StringWriter writer = new System.IO.StringWriter();
            //        DocumentSaver.SaveXmlFile(writer, selectionDocument);
            //        string xml = writer.ToString();
            //        obj.SetData(XMLDataFormat , xml );
            //    }
            //    // 设置HTML数据
            //    WriterHtmlDocumentWriter htmlWriter = new WriterHtmlDocumentWriter();
            //    htmlWriter.Documents.Add(this.Document);
            //    htmlWriter.Options.Indent = true;
            //    htmlWriter.Options.KeepLineBreak = true;
            //    htmlWriter.Options.UseClassAttribute = true;
            //    htmlWriter.IncludeSelectionOndly = true;
            //    htmlWriter.Options.OutputHeaderFooter = false;
            //    htmlWriter.WritingExcelHtml = false;
            //    htmlWriter.ViewStyle = WriterHtmlViewStyle.Normal;
            //    htmlWriter.Refresh();
            //    string html = htmlWriter.DocumentHtml;
            //    if (html != null && html.Length > 0)
            //    {
            //        obj.SetData(System.Windows.Forms.DataFormats.Html, html);
            //    }
            //    //				// 设置HTML数据
            //    //				XHtmlWriter hw = new XHtmlWriter( true );
            //    //				hw.IncludeSelectionOndly = true ;
            //    //				this.WriteHTML( hw );
            //    //				string html = hw.ToString();
            //    //				if( html != null && html.Length > 0 )
            //    //				{
            //    //					obj.SetData( System.Windows.Forms.DataFormats.Html , html );
            //    //				}
            //    System.Windows.Forms.Clipboard.SetDataObject(obj, true);
            //    return true;
            //}
            //return false;
        }

        internal System.Windows.Forms.DataObject CreateSelectionDataObject( )
        {
            DomSelection selection = this.Selection;
            if (selection != null && selection.Length != 0)
            {
                System.Windows.Forms.DataObject obj = new System.Windows.Forms.DataObject();
                // 设置纯文本数据
                obj.SetData(System.Windows.Forms.DataFormats.Text, selection.Text);
                // 设置RTF数据
                string rtf = selection.RTFText;// this.GetRTFText( true );
                if (rtf != null && rtf.Length > 0)
                {
                    obj.SetData(System.Windows.Forms.DataFormats.Rtf, rtf);
                }
                 
                // 设置XML数据
                using (DomDocument selectionDocument = selection.CreateDocument())
                {
                    //if (this.EditorControl != null)
                    //{
                    //    selectionDocument.EditorControlHandle = this.EditorControl.Handle.ToInt32();
                    //}
                    selectionDocument.ScriptText = null;
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    DocumentSaver.SaveXmlFile(writer, selectionDocument);
                    string xml = writer.ToString();
                    obj.SetData(XMLDataFormat, xml);
                }
                
                return obj;
            }
            return null;
        }

        /// <summary>
        /// 判断能否设置样式
        /// </summary>
        /// <returns></returns>
        public bool CanSetStyle
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 在文档中的当前未知插入文本
        /// </summary>
        /// <param name="strText">要插入的文本</param>
        /// <returns>插入的字符个数</returns>
        public int InsertString(string strText)
        {
            return InsertString(strText, true, InputValueSource.Unknow);
        }

        /// <summary>
        /// 执行在当前位置插入一个字符串的操作
        /// </summary>
        /// <param name="strText">要插入的字符串值</param>
        /// <param name="logUndo">是否记录操作过程</param>
        /// <returns>插入的元素个数</returns>
        public virtual int InsertString(string strText, bool logUndo , InputValueSource inputSource )
        {
            if (this.ValueFilter != null)
            {
                FilterValueEventArgs args = new FilterValueEventArgs(inputSource, InputValueType.Text, strText);
                this.ValueFilter(this, args);
                if (args.Cancel)
                {
                    // 用户取消操作
                    return 0;
                }
                strText = args.Value as string;
            }
            if (strText == null || strText.Length == 0)
            {
                return 0;
            }
            DomElementList newElements = this.Document.CreateTextElements(
                strText,
                (DocumentContentStyle)this.Document.CurrentParagraphStyle,
                ( DocumentContentStyle )this.Document.EditorCurrentStyle.Clone() );

            if (newElements == null || newElements.Count == 0)
            {
                return 0 ;
            }
            
             
            if (logUndo)
            {
                this.Document.BeginLogUndo();
            }
            //int oldSelectionLength = this.Selection.Length;
            if (this.DocumentContent.HasSelection )
            {
                this.Content.DeleteSelection(true, false, false);
                this.Document.Modified = true;
            }
            else
            {
                if (this.EditorControl != null)
                {
                    if (this.EditorControl.InsertMode == false)
                    {
                        this.Content.DeleteCurrentElement( true );
                        this.Document.Modified = true;
                    }
                }
            }
 
            DocumentContentStyle styleBack = this.Document.EditorCurrentStyle;
            int result = this.Document.InsertElements(newElements, true);
            if (logUndo)
            {
                this.Document.EndLogUndo();
            } 
            if (styleBack != this.Document.EditorCurrentStyle)
            {
                this.Document.EditorCurrentStyle = styleBack;
                this.Document.OnSelectionChanged();
            }
            this.Document.Modified = true;
            if (result > 0)
            {
                this.Document.OnDocumentContentChanged();
            }
            return result;
        }

        /// <summary>
        /// 插入一个换行元素
        /// </summary>
        public virtual DomLineBreakElement InsertLineBreak()
        {
            this.Document.BeginLogUndo();
            DomLineBreakElement br = this.Document.CreateLineBreak();
            using (System.Drawing.Graphics g = this.Document.CreateGraphics())
            {
                DocumentPaintEventArgs args = new DocumentPaintEventArgs(g, Rectangle.Empty);
                args.Document = this.Document;
                args.Render = this.Document.Render;
                args.Element = br;
                br.RefreshSize(args);
            }
            this.Document.InsertElement(br);
            this.Document.EndLogUndo();
            this.Document.OnDocumentContentChanged();
            return br;
        }
         
        public virtual void InsertChar(char vChar)
        {
            if (vChar == '\n')
            {
                return;
            }
            if (vChar < 32)
            {
                if (vChar != '\t' && vChar != '\r')
                {
                    // 出现不可接收的字符
                    return;
                }
            }

            if (vChar == '\r')
            {
                DomParagraphFlagElement Paragraph = 
                    this.Content.CurrentElement.OwnerParagraphEOF;
                if (Paragraph == null)
                {
                    throw new System.Exception("未找到所属段落");
                }
                DomContentLine line = this.Content.CurrentLine;
                int index = line.IndexOf(this.Content.CurrentElement);
                StringBuilder headerString = new StringBuilder();
                if (index > 0)
                {
                    // 追加前面的空格
                    for (int iCount = 0; iCount < index; iCount++)
                    {
                        if (line[iCount] is DomCharElement
                            && ((DomCharElement)line[iCount]).CharValue == ' ')
                        {
                            headerString.Append(" ");
                        }
                        else
                        {
                            headerString = new StringBuilder();
                            break;
                        }
                    }
                }
                headerString.Insert(0, vChar);
                this.InsertString(headerString.ToString(), true , InputValueSource.UI );
            }
            else
            {
                this.InsertString(vChar.ToString() , true , InputValueSource.UI );
            }
        }

        /// <summary>
        /// 在当前插入点插入一个元素
        /// </summary>
        /// <param name="element">要插入的元素</param>
        public void InsertElement(DomElement element)
        {
            if (element != null)
            {
                element.OwnerDocument = this.Document ;
                this.Document.BeginLogUndo();
                if (element.SizeInvalid)
                {
                    using (System.Drawing.Graphics g = this.Document.CreateGraphics())
                    {
                        DocumentPaintEventArgs args = new DocumentPaintEventArgs(
                            g,
                            System.Drawing.Rectangle.Empty);
                        args.Document = this.Document;
                        args.Element = element;
                        args.Render = this.Document.Render;
                        element.RefreshSize(args);
                    }
                }
                this.Document.InsertElement(element);
                this.Document.EndLogUndo();
                this.Document.OnDocumentContentChanged();
            }
        }

        /// <summary>
        /// 在当前插入点处插入若干个元素
        /// </summary>
        /// <param name="list">要插入的元素的列表</param>
        /// <remarks>插入的元素个数</remarks>
        public int InsertElements(DomElementList list)
        {
            return InnerInsertElements(list, true);
        }

        /// <summary>
        /// 在当前插入点处插入若干个元素
        /// </summary>
        /// <param name="logUndo">是否记录撤销操作信息</param>
        /// <param name="list">要插入的元素的列表</param>
        /// <returns>插入的元素个数</returns>
        private int InnerInsertElements(DomElementList list, bool logUndo)
        {
            if (list == null || list.Count == 0)
            {
                return 0;
            }
            if (logUndo)
            {
                this.Document.BeginLogUndo();
            }

            MeasureElementSize(list);
            
            int result = this.Document.InsertElements(list, true);
            this.Document.Modified = true;
            if (logUndo)
            {
                this.Document.EndLogUndo();
            }
            return result;
        }

        /// <summary>
        /// 计算文档元素的大小
        /// </summary>
        /// <param name="list">文档元素列表</param>
        public virtual void MeasureElementSize(DomElementList list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (list.Count == 0)
            {
                return;
            }
            using (System.Drawing.Graphics g = this.Document.CreateGraphics())
            {
                foreach (DomElement element in list)
                {
                    element.OwnerDocument = this.Document;

                    //if (element.SizeInvalid)
                    {
                        // 插入元素时全部计算大小
                        DocumentPaintEventArgs args = new DocumentPaintEventArgs(
                            g,
                            System.Drawing.Rectangle.Empty);
                        args.Render = this.Render;
                        args.Document = this.Document;
                        args.Element = element;
                        element.RefreshSize(args);

                        //this.Render.RefreshSize(element, g);
                    }
                    
                }//foreach
            }//using
        }

        /// <summary>
        /// 执行删除插入点前一个元素的操作
        /// </summary>
        /// <returns>本操作是否删除了元素</returns>
        public virtual bool Backspace()
        {
            int index = -1;
            this.Document.BeginLogUndo();
            if (this.DocumentContent.HasSelection == false)
            {
                // 没有选择任何内容，则删除当前位置的前一个元素
                DomElement element = this.Document.CurrentElement;
                DomParagraphFlagElement peof = element.OwnerParagraphEOF;
                if (element != null
                    && peof != null
                    && element == peof.ParagraphFirstContentElement)
                {
                    // 若当前插入点是在一个段落的最前面,则敲入退格键相当于设置段落的首行向前缩进
                    if (this.CanModify(peof, DomAccessFlags.CheckUserEditable))
                    {
                        DocumentContentStyle style =
                            (DocumentContentStyle)peof.Style.Clone();
                        style.DisableDefaultValue = true;
                        bool modify = false;
                        if (style.FirstLineIndent > 0)
                        {
                            // 取消段首缩进
                            modify = true;
                            style.FirstLineIndent = 0;
                        }
                        else if (style.NumberedList)
                        {
                            // 取消数字式列表
                            modify = true;
                            style.NumberedList = false;
                        }
                        else if (style.BulletedList)
                        {
                            // 取消原点式列表
                            modify = true;
                            style.BulletedList = false;
                        }
                        if (modify)
                        {
                            // 修改了段落设置
                            style.CreatorIndex = this.Document.UserHistories.CurrentIndex;
                            int styleIndex = this.Document.ContentStyles.GetStyleIndex(style);
                            if (this.Document.CanLogUndo)
                            {
                                this.Document.UndoList.AddProperty(
                                    "StyleIndex",
                                    peof.StyleIndex,
                                    styleIndex,
                                    peof);
                            }
                            peof.StyleIndex = styleIndex;
                            peof.UpdateContentVersion();
                            DomElementList list = new DomElementList();
                            list.Add(peof.ParagraphFirstContentElement );
                            list.Add(peof.LastContentElement);
                            this.Document.CurrentContentElement.RefreshContentByElements(
                                list,
                                true,
                                false);
                            //BindControl .UpdateInvalidateRect();
                            this.EditorControl.UpdateTextCaret();
                            //this.Document.CurrentStyle.FirstLineIndent = 0;
                            this.Document.OnSelectionChanged();
                            this.Document.EndLogUndo();
                            return true;
                        }//if
                    }//if
                }//if
            }
            if (this.DocumentContent.HasSelection)
            {
                // 若选择内容则删除用户选择的内容
                index = this.Content.DeleteSelection(true, false, false);
            }
            else
            {
                // 若没有选择内容则删除前一个元素
                index = this.Content.DeletePreElement( true );
            }
            if (index >= 0)
            {
                this.Document.EndLogUndo();
                this.Document.CurrentContentElement.RefreshPrivateContent(index);
                this.Document.OnSelectionChanged();
                this.Document.OnDocumentContentChanged();
                return true;
            }
            else
            {
                this.Document.EndLogUndo();
                return false;
            }
        }

        /// <summary>
        /// 判断能否修改当前段落或被选择的段落
        /// </summary>
        public virtual bool CanModifyParagraphs
        {
            get
            {
                DomElementList ps = this.Selection.ParagraphsEOFs;
                foreach (DomElement p in ps)
                {
                    if (CanModify(p ))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 判断能否修改被选择的内容
        /// </summary>
        public virtual bool CanModifySelection
        {
            get
            {
                if (this.EditorControlReadonly)
                {
                    return false;
                }
                if (this.Selection.Length == 0)
                {
                    DomContainerElement container = null;
                    int index = 0 ;
                    this.Document.Content.GetCurrentPositionInfo(out container, out index);
                    return CanModify(container );
                }
                else
                {
                    foreach (DomElement element in this.Selection.ContentElements)
                    {
                        if (CanModify(element ))
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
        }

        
        /// <summary>
        /// 判断指定的元素能否被修改
        /// </summary>
        /// <param name="element">元素对象</param>
        /// <returns>元素能否被修改</returns>
        public virtual bool CanModify(DomElement element )
        {
            return CanModify(element, DomAccessFlags.Normal);
        }

        /// <summary>
        /// 判断指定的元素能否被修改
        /// </summary>
        /// <param name="element">元素对象</param>
        /// <returns>元素能否被修改</returns>
        public virtual bool CanModify(DomElement element , DomAccessFlags flags )
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            if (HasFlag(flags, DomAccessFlags.CheckControlReadonly) )
            {
                // 检查控件是否只读
                if (this.EditorControlReadonly)
                {
                    return false;
                }
            }
             

            if (HasFlag(flags, DomAccessFlags.CheckLock))
            {
                // 检查锁定状态
                DomDocumentContentElement dce = element.DocumentContentElement;
                int index2 = 0;
                if (element is DomParagraphFlagElement)
                {
                    index2 = dce.Content.IndexOf(element);
                }
                else
                {
                    index2 = dce.Content.IndexOf(element.LastContentElement );
                }
                
            }

            if (HasFlag(flags, DomAccessFlags.CheckPermission))
            {
                // 检查授权控制
                if (this.IsAdministrator == false
                    && this.Document.Options.SecurityOptions.EnablePermission)
                {
                    // 执行权限控制
                    if (this.Document.UserHistories.CurrentPermissionLevel < element.CreatorPermessionLevel)
                    {
                        // 权限不够，不能修改。
                        return false;
                    }
                    if (element.Style.DeleterIndex >= 0)
                    {
                        // 元素已经被逻辑删除了,无法接着修改样式了。
                        return false;
                    }
                    //if ((flags & DomAccessFlags.LogicDelete) == DomAccessFlags.LogicDelete)
                    //{
                    //    // 执行逻辑删除
                    //    if (element.Style.DeleterIndex >= 0)
                    //    {
                    //        // 元素已经被逻辑删除了，无法接着删除。
                    //        return false;
                    //    }
                    //}
                }
            }

            DomElement parent = element ;
            while (parent != null)
            {
                 
                if (parent is DomContainerElement)
                {
                    if (HasFlag(flags, DomAccessFlags.CheckReadonly))
                    {
                        if (((DomContainerElement)parent).ContentEditable == false)
                        {
                            if (this.IsAdministrator == false)
                            {
                                return false;
                            }
                        }
                    }
                }
                 
                parent = parent.Parent;
            }//while
            return true;
        }

        ///// <summary>
        ///// 判断指定的容器元素器内容是只读的
        ///// </summary>
        ///// <param name="container"></param>
        ///// <returns></returns>
        //public virtual bool IsContainerReadonly(XTextContainerElement container)
        //{
        //    if (container is XTextInputFieldElement)
        //    {
        //        // 容器元素为文本域
        //        XTextInputFieldElement field = (XTextInputFieldElement)container;
        //        if (field.Readonly)
        //        {
        //            return false;
        //        }
        //    }
        //    return false;
        //}

        /// <summary>
        /// 判断指定的元素能否被删除
        /// </summary>
        /// <param name="element">指定的元素对象</param>
        /// <returns>元素能否删除</returns>
        public bool CanDelete(DomElement element)
        {
            return CanDelete(element, DomAccessFlags.Normal );
        }

        /// <summary>
        /// 判断指定的文档元素是否在输入域中
        /// </summary>
        /// <param name="element">文档元素对象</param>
        /// <returns>是否在输入域中</returns>
        private bool IsInFormField(DomElement element)
        {
            return false;
        }

        /// <summary>
        /// 判断指定元素能否被删除
        /// </summary>
        /// <param name="element">元素对象</param>
        /// <returns>元素能否删除</returns>
        public virtual bool CanDelete(DomElement element , DomAccessFlags flags )
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            if (HasFlag(flags, DomAccessFlags.CheckControlReadonly))
            {
                // 检查控件是否是只读的
                if (this.EditorControlReadonly)
                {
                    // 控件被设置为只读的
                    return false;
                }
            }
             
            //if (element.IsLogicDeleted)
            //{
            //    // 元素已经被逻辑删除了，无法再次删除
            //    return false;
            //}
            if ( this.IsAdministrator == false
                && this.Document.Options.SecurityOptions.EnablePermission)
            {
                // 执行权限控制
                if (this.Document.UserHistories.CurrentPermissionLevel < element.CreatorPermessionLevel)
                {
                    // 权限不够，不能删除。
                    return false;
                }
                if (this.Document.Options.SecurityOptions.EnableLogicDelete
                    && element.Style.DeleterIndex >= 0)
                {
                    // 文档允许逻辑删除而且元素已经给逻辑删除了，因此不能再次被逻辑删除。
                    return false;
                }
            }
            
            else if (element is DomParagraphFlagElement)
            {
                DomContentElement ce = element.ContentElement;
                if (ce.PrivateContent.LastElement == element)
                {
                    // 文本块元素中最后一个段落符号不能删除
                    return false;
                }
            }
            DomElement parent = element.Parent  ;
            while (parent != null)
            {
                 
                if (parent is DomContainerElement)
                {
                    if (HasFlag(flags, DomAccessFlags.CheckReadonly))
                    {
                        if (((DomContainerElement)parent).ContentEditable == false)
                        {
                            // 内容只读
                            if (this.IsAdministrator == false)
                            {
                                return false;
                            }
                        }
                    }
                }
                 
                parent = parent.Parent;
            }//while
            return true ;
        }

        /// <summary>
        /// 判断能否删除文档中选中的区域或者当前元素
        /// </summary>
        public virtual bool CanDeleteSelection
        {
            get
            {
                if ( this.EditorControlReadonly )
                {
                    // 控件只读
                    return false;
                }
                if (this.Selection.Length == 0)
                {
                    DomElement preElement = this.Content.SafeGet(this.Selection.StartIndex - 1);
                    DomElement endElement = this.Content.SafeGet(this.Selection.StartIndex );
                    if (preElement != null && CanDelete(preElement)
                        || (endElement != null && CanDelete(endElement)))
                    {
                        return true;
                    }
                }
                else
                {
                    int result = this.Content.DeleteSelection(false, true , false );
                    return result != 0;
                }
                return false;
            }
        }

        

        /// <summary>
        /// 判断能否在指定容器元素的指定序号处插入新的元素
        /// </summary>
        /// <param name="container">容器元素</param>
        /// <param name="index">指定序号</param>
        /// <param name="newElement">准备插入的新元素</param>
        /// <returns>能否插入元素</returns>
        public bool CanInsert(
            DomContainerElement container,
            int index,
            DomElement newElement )
        {
            return CanInsert(container, index, newElement, DomAccessFlags.CheckUserEditable);
        }


        /// <summary>
        /// 判断能否在指定容器元素的指定序号处插入新的元素
        /// </summary>
        /// <param name="container">容器元素</param>
        /// <param name="index">指定序号</param>
        /// <param name="newElement">准备插入的新元素</param>
        /// <returns>能否插入元素</returns>
        public virtual bool CanInsert(
            DomContainerElement container,
            int index,
            DomElement newElement,
            DomAccessFlags flags )
        {
            if (HasFlag(flags, DomAccessFlags.CheckControlReadonly))
            {
                if (this.EditorControlReadonly )
                {
                    // 控件是只读的
                    return false;
                }
            }
            if (this.AcceptChildElement(container, newElement , flags) == false)
            {
                return false;
            }
             
            return true;
        }

        /// <summary>
        /// 判断指定容器元素中指定位置能否插入指定类型的子元素
        /// </summary>
        /// <param name="container">容器元素</param>
        /// <param name="index">要插入子元素的序号</param>
        /// <param name="elementType">子元素类型</param>
        /// <returns>能否插入子元素</returns>
        public virtual bool CanInsert(
            DomContainerElement container,
            int index,
            Type elementType )
        {
            return CanInsert(
                container,
                index, 
                elementType, 
                DomAccessFlags.Normal );
        }

        /// <summary>
        /// 判断指定容器元素中指定位置能否插入指定类型的子元素
        /// </summary>
        /// <param name="container">容器元素</param>
        /// <param name="index">要插入子元素的序号</param>
        /// <param name="elementType">子元素类型</param>
        /// <returns>能否插入子元素</returns>
        public virtual bool CanInsert(
            DomContainerElement container,
            int index,
            Type elementType ,
            DomAccessFlags flags )
        {
            // 检查参数
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            if (elementType == null)
            {
                throw new ArgumentNullException("elementType");
            }
            //this.EditorControl.Document.UserHistories.CurrentInfo.PermissionLevel;

            if ( HasFlag( flags , DomAccessFlags.CheckControlReadonly ))
            {
                // 检查控件是否只读
                if (this.EditorControlReadonly)
                {
                    return false;
                }
            }
             
            if (container.IsLogicDeleted)
            {
                // 元素已经被逻辑删除了，无法插入任何内容。
                return false;
            }
            

            DomContainerElement element = container;
            if (this.AcceptChildElement(container, elementType , flags ) == false)
            {
                return false;
            }
            while (element != null)
            {
                if (element is DomContainerElement)
                {
                    // 内容只读
                    if (HasFlag(flags, DomAccessFlags.CheckReadonly))
                    {
                        if (((DomContainerElement)element).ContentEditable == false)
                        {
                            if (this.IsAdministrator == false)
                            {
                                return false;
                            }
                        }
                    }
                }
                 
                element = element.Parent;
            }//while
            return true;
        }

        ///// <summary>
        ///// 判断能否在指定位置处插入元素
        ///// </summary>
        ///// <param name="index"></param>
        ///// <param name="newElement"></param>
        ///// <returns></returns>
        //public virtual bool CanInsert(int index, XTextElement newElement)
        //{
        //    return true;
        //}

        //public virtual bool CanInsert(int index)
        //{
        //    return true;
        //}

        /// <summary>
        /// 判断能否在当前位置插入元素
        /// </summary>
        /// <returns></returns>
        public bool CanInsertAtCurrentPosition
        {
            get
            {
                return CanInsertElementAtCurrentPosition(
                    typeof( DomElement ) ,
                    DomAccessFlags.Normal );
            }
        }

        /// <summary>
        /// 判断能否在当前位置插入指定类型的元素
        /// </summary>
        /// <param name="newElementType">新插入元素的类型</param>
        /// <returns>能否插入新元素</returns>
        public bool CanInsertElementAtCurrentPosition(Type newElementType)
        {
            return CanInsertElementAtCurrentPosition(
                newElementType,
                DomAccessFlags.Normal );
        }

        /// <summary>
        /// 判断能否在当前位置插入指定类型的元素
        /// </summary>
        /// <param name="newElementType">新插入元素的类型</param>
        /// <returns>能否插入新元素</returns>
        public virtual bool CanInsertElementAtCurrentPosition(
            Type newElementType , 
            DomAccessFlags flags)
        {
            if ((flags & DomAccessFlags.CheckControlReadonly)
                == DomAccessFlags.CheckControlReadonly)
            {
                if (this.EditorControlReadonly)
                {
                    // 控件只读
                    return false;
                }
            }
            DomContainerElement container = null;
            int elementIndex = 0 ;
            this.Document.Content.GetPositonInfo(
                this.Document.Selection.StartIndex, 
                out container,
                out elementIndex,
                this.Document.Content.LineEndFlag);
            return CanInsert(container, elementIndex, newElementType , flags );
            //return this.AcceptChildElement(container, newElementType);
        }

        public virtual bool IsVisible(DomElement element)
        {
            return true;
        }


        /// <summary>
        /// 判断元素是否是强制分行
        /// </summary>
        /// <param name="element">元素对象</param>
        /// <returns>元素是否是强制分行</returns>
        public virtual bool IsNewLine(DomElement element)
        {
            //			if( element is XTextEOF )
            //				return false ;
            if (element is DomParagraphFlagElement)
            {
                return true;
            }
            if (element is DomLineBreakElement)
            {
                return true;
            }
            if (element is DomPageBreakElement)
            {
                return true;
            }
             
            return false;
            //			XTextChar c = element as XTextChar ;
            //			if( c == null )
            //				return false;
            //			if( c.Value == '\r' || c.Value == '\n' )
            //				return true;
            //			else
            //				return false;
        }

        /// <summary>
        /// 判断元素是否单独占据一行
        /// </summary>
        /// <param name="element">元素对象</param>
        /// <returns>元素是否单独占据一行</returns>
        public virtual bool OwnerHoleLine(DomElement element)
        {
             
            if (element is DomPageBreakElement)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断指定的字符是否是应为字母或者数字
        /// </summary>
        /// <param name="c">字符值</param>
        /// <returns>判断结果</returns>
        public virtual bool IsEnglishLetterOrDigit(char c)
        {
            if (c >= 'a' && c <= 'z')
            {
                return true;
            }
            if (c >= 'A' && c <= 'Z')
            {
                return true;
            }
            if (c >= '0' && c <= '9')
            {
                return true;
            }
            return false;
        }



        /// <summary>
        /// 判断元素是否可以出现在行首
        /// </summary>
        /// <param name="element">元素对象</param>
        /// <returns>元素是否可以出现在行首</returns>
        public virtual bool CanBeLineHead(DomElement element)
        {
            if (element is DomCharElement)
            {
                return CanBeLineHead(((DomCharElement)element).CharValue);
            }
             
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 判断元素是否可以出现在行尾
        /// </summary>
        /// <param name="element">元素对象</param>
        /// <returns>元素是否可以出现在行尾</returns>
        public virtual bool CanBeLineEnd(DomElement element)
        {
            if (element is DomCharElement)
            {
                return CanBeLineEnd(((DomCharElement)element).CharValue);
            }
             
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 判断指定字符能否放在行首
        /// </summary>
        /// <param name="c">字符值</param>
        /// <returns>判断结果</returns>
        public virtual bool CanBeLineHead(char c)
        {
            if (strTailSymbols == null)
            {
                return true;
            }
            return strTailSymbols.IndexOf(c) < 0 ;
        }

        /// <summary>
        /// 判断指定字符能否放置在行尾
        /// </summary>
        /// <param name="c">字符值</param>
        /// <returns>判断结果</returns>
        public virtual bool CanBeLineEnd(char c)
        {
            if (strHeadSymbols == null)
            {
                return true;
            }
            return strHeadSymbols.IndexOf(c) < 0 ;
        }

        private static string strTailSymbols = "!),.:;?]}¨·ˇˉ―‖’”…∶、。〃々〉》」』】〕〗！＂＇），．：；？］｀｜｝～￠";
        /// <summary>
        /// 后置标点
        /// </summary>
        public static string TailSymbols
        {
            get
            {
                return strTailSymbols; 
            }
            set
            {
                strTailSymbols = value; 
            }
        }

        private static string strHeadSymbols = "([{·‘“〈《「『【〔〖（．［｛￡￥";
        /// <summary>
        /// 前置标点
        /// </summary>
        public static string HeadSymbols
        {
            get
            {
                return strHeadSymbols; 
            }
            set
            {
                strHeadSymbols = value; 
            }
        }

        private DocumentControlerSnapshot _Snapshot = null;
        /// <summary>
        /// 文档状态快照
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public DocumentControlerSnapshot Snapshot
        {
            get
            {
                if (_Snapshot != null)
                {
                    if (_Snapshot.DocumentContentVersion != this.Document.ContentVersion
                        || _Snapshot.SelectionVerion != this.Document.Selection.SelectionVersion)
                    {
                        _Snapshot = null;
                    }
                }
                if (_Snapshot == null)
                {
                    _Snapshot = new DocumentControlerSnapshot();
                    _Snapshot.DocumentContentVersion = this.Document.ContentVersion;
                    _Snapshot.SelectionVerion = this.Document.Selection.SelectionVersion;
                    _Snapshot.OwnerControler = this;
                    //_Snapshot.CanDeleteSelection = this.CanDeleteSelection;
                    _Snapshot.CanModifySelection = this.CanModifySelection;
                    _Snapshot.CanModifyParagraphs = this.CanModifyParagraphs;
                    _Snapshot.CanDeleteSelection = this.CanDeleteSelection;
                }
                return _Snapshot; 
            }
        }

        public void ClearSnapshot()
        {
            _Snapshot = null;
        }

        private bool HasFlag(DomAccessFlags flags, DomAccessFlags specifyFlag)
        {
            return (flags & specifyFlag) == specifyFlag;
        }

    }

    public class DocumentControlerSnapshot
    {
        private DocumentControler _OwnerControler = null;

        public DocumentControler OwnerControler
        {
            get { return _OwnerControler; }
            set { _OwnerControler = value; }
        }

        private int _DocumentContentVersion = 0;
        /// <summary>
        /// 文档内容版本号
        /// </summary>
        public int DocumentContentVersion
        {
            get { return _DocumentContentVersion; }
            set { _DocumentContentVersion = value; }
        }

        private int _SelectionVerion = 0;
        /// <summary>
        /// 选择区域版本号
        /// </summary>
        public int SelectionVerion
        {
            get { return _SelectionVerion; }
            set { _SelectionVerion = value; }
        }

        //private Dictionary<Type, bool> _CanInsertElements = new Dictionary<Type, bool>();

        //public bool CanInsertElementAtCurrentPosition(Type elementType)
        //{
        //    if (_CanInsertElements.ContainsKey(elementType))
        //    {
        //        return _CanInsertElements[elementType];
        //    }
        //    else
        //    {
        //        bool flag = this.OwnerControler.CanInsertElementAtCurrentPosition(elementType);
        //        _CanInsertElements[elementType] = flag;
        //        return flag;
        //    }
        //}

        //public bool CanInsertAtCurrentPosition
        //{
        //    get
        //    {
        //        return CanInsertElementAtCurrentPosition(typeof(XTextElement));
        //    }
        //}

        private bool _CanDeleteSelection = true;
        /// <summary>
        /// 能否删除被选中的内容
        /// </summary>
        public bool CanDeleteSelection
        {
            get { return _CanDeleteSelection; }
            set { _CanDeleteSelection = value; }
        }

        private bool _CanModifySelection = true;
        /// <summary>
        /// 能否修改被选中的内容
        /// </summary>
        public bool CanModifySelection
        {
            get { return _CanModifySelection; }
            set { _CanModifySelection = value; }
        }

        private bool _CanModifyParagraphs = true;
        /// <summary>
        /// 能否修改被选中的段落
        /// </summary>
        public bool CanModifyParagraphs
        {
            get { return _CanModifyParagraphs; }
            set { _CanModifyParagraphs = value; }
        }
    }

    /// <summary>
    /// 内容状态
    /// </summary>
    public enum ContentStates
    {
        /// <summary>
        /// 只读
        /// </summary>
        Readonly = 0,
        /// <summary>
        /// 可插入
        /// </summary>
        Insertable = 1,
        /// <summary>
        /// 可删除
        /// </summary>
        Deleteable = 2,
        /// <summary>
        /// 可插入可删除
        /// </summary>
        InsertDeleteable = 3
    }

    /// <summary>
    /// 支持的数据格式
    /// </summary>
    [Flags]
    public enum DataFormatStyle
    {
        /// <summary>
        /// XML格式
        /// </summary>
        XML = 1,
        /// <summary>
        /// 二进制格式
        /// </summary>
        Binary = 2,
        /// <summary>
        /// 加密的二进制格式
        /// </summary>
        EncryptBianry = 4 ,
        /// <summary>
        /// HTML格式
        /// </summary>
        Html = 8 ,
        /// <summary>
        /// RTF格式
        /// </summary>
        RTF = 16,
        /// <summary>
        /// 纯文本格式
        /// </summary>
        Text = 32 ,
        /// <summary>
        /// 所有的格式
        /// </summary>
        All = 0xffffff
    }
}
