using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel ;
using System.Xml.Serialization ;
using DCSoft.CSharpWriter.Data;
using System.IO;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 内容链接对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    [XmlType("XContentLinkField")]
    [Editor( typeof( DCSoft.CSharpWriter.Commands.XTextContentLinkElementEditor ),typeof( ElementEditor )) ]
    public class DomContentLinkFieldElement : DomFieldElementBase
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomContentLinkFieldElement()
        {
        }

        private bool _Readonly = false;
        /// <summary>
        /// 内容只读
        /// </summary>
        [DefaultValue( false )]
        public bool Readonly
        {
            get
            {
                return _Readonly; 
            }
            set
            {
                _Readonly = value; 
            }
        }

        [Browsable( false )]
        public override bool ContentEditable
        {
            get
            {
                return this.Readonly == false;
            }
        }

        private string _FileName = null;
        /// <summary>
        /// 文件名
        /// </summary>
        [DefaultValue( null )]
        public string FileName
        {
            get
            {
                return _FileName; 
            }
            set
            {
                _FileName = value;
                _backgroundTextElements = null;
            }
        }

        private string _TargetRange = null;
        /// <summary>
        /// 目标区域
        /// </summary>
        [DefaultValue( null )]
        public string TargetRange
        {
            get
            {
                return _TargetRange; 
            }
            set
            {
                _TargetRange = value; 
            }
        }

        private bool _AutoUpdate = false;
        /// <summary>
        /// 自动更新内容
        /// </summary>
        [DefaultValue( false )]
        public bool AutoUpdate
        {
            get
            {
                return _AutoUpdate; 
            }
            set
            {
                _AutoUpdate = value; 
            }
        }

        private bool _SaveContentToFile = true;
        /// <summary>
        /// 保存文件的时候也保存链接的内容
        /// </summary>
        [DefaultValue( true )]
        public bool SaveContentToFile
        {
            get 
            {
                return _SaveContentToFile; 
            }
            set
            {
                _SaveContentToFile = value; 
            }
        }

        /// <summary>
        /// 为XML序列化/反序列化的子元素列表
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlArray("XElements")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override DomElementList ElementsForSerialize
        {
            get
            {
                if (this.SaveContentToFile)
                {
                    return base.ElementsForSerialize;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                base.ElementsForSerialize = value;
            }
        }

        /// <summary>
        /// 添加内容元素
        /// </summary>
        /// <param name="content">内容列表</param>
        /// <param name="privateMode">私有模式</param>
        public override int AppendContent(DomElementList content, bool privateMode)
        {
            if (this.OwnerDocument.Printing)
            {
                //  若处于打印模式，则只添加可见元素
                return base.AppendContent(content, privateMode);
            }
            else
            {
                if (this.OwnerDocument.Options.BehaviorOptions.DesignMode)
                {
                    int result = 0;
                    if (this.StartElement != null)
                    {
                        content.Add(this.StartElement);
                        result++;
                    }
                    if (_backgroundTextElements == null)
                    {
                        // 创建背景元素
                        string bgText = this.FileName;
                        if (string.IsNullOrEmpty(bgText) == false)
                        {
                            _backgroundTextElements = this.OwnerDocument.CreateTextElements(
                                    bgText, null, this.StartElement.Style);
                            foreach (DomElement e in _backgroundTextElements)
                            {
                                e.OwnerDocument = this.OwnerDocument;
                                e.Parent = this;
                                //e.StyleIndex = this.StyleIndex;
                                e.FixDomState();
                            }
                        }
                        else
                        {
                            _backgroundTextElements = new DomElementList();
                        }
                    }
                    foreach (DomElement e in _backgroundTextElements)
                    {
                        e.StyleIndex = this.StyleIndex;
                    }
                    content.AddRange(_backgroundTextElements);
                    result += _backgroundTextElements.Count;
                    if (this.EndElement != null)
                    {
                        content.Add(this.EndElement);
                        result++;
                    }
                    return result;
                }
                else
                {
                    return base.AppendContent(content, privateMode);
                }
            }
        }

        /// <summary>
        /// 文档内容加载后的处理
        /// </summary>
        /// <param name="format">文件格式</param>
        public override void AfterLoad(FileFormat format)
        {
            if (this.AutoUpdate)
            {
                if (this.OwnerDocument.Options.BehaviorOptions.DesignMode == false)
                {
                    // 处于运行模式才加载
                    UpdateContent(true);
                }
            }
            base.AfterLoad(format);
        }

        public override void OnGotFocus(EventArgs args)
        {
            base.OnGotFocus(args);
            InvalidateBorderElement();
        }
        public override void OnLostFocus(EventArgs args)
        {
            base.OnLostFocus(args);
            InvalidateBorderElement();
        }

        public override void OnMouseEnter(EventArgs args)
        {
            base.OnMouseEnter(args);
            InvalidateBorderElement();
        }

        public override void OnMouseLeave(EventArgs args)
        {
            base.OnMouseLeave(args);
            InvalidateBorderElement();
        }
        private void InvalidateBorderElement()
        {
            this.StartElement.InvalidateView();
            this.EndElement.InvalidateView();
        }

        /// <summary>
        /// 更新链接的文档内容
        /// </summary>
        /// <param name="fastMode">快速更新</param>
        /// <returns>操作是否修改了文档内容</returns>
        public bool UpdateContent(bool fastMode)
        {
            IFileSystem fs = this.OwnerDocument.AppHost.FileSystems.Docuemnt ;
            Stream stream = null;
            WriterAppHost host = this.OwnerDocument.AppHost;
            try
            {
                if (host.Debuger != null)
                {
                    host.Debuger.DebugLoadingFile(this.FileName);
                }
                stream = fs.Open(host.Services , this.FileName);
            }
            catch (Exception ext)
            {
                System.Diagnostics.Debug.WriteLine(
                    string.Format(
                        WriterStrings.FailToLoad_FileName_Message, 
                        this.FileName,
                        ext.Message));
            }
            if (stream != null)
            {
                DomDocument document = DocumentLoader.FastLoadXMLFile( 
                    stream,
                    this.OwnerDocument.GetType() );
                if (host.Debuger != null)
                {
                    host.Debuger.DebugLoadFileComplete((int)stream.Length);
                }
                document.FixElementsForSerialize(true);
                document.FixDomState();
                document.Options = this.OwnerDocument.Options;
                DomElement sourceElement = document.Body;
                if (string.IsNullOrEmpty(this.TargetRange) == false)
                {
                    sourceElement = document.GetElementById(this.TargetRange);
                }
                if (sourceElement != null)
                {
                    DomElementList list = new DomElementList();
                    if (sourceElement is DomContainerElement)
                    {
                        DomContainerElement c = (DomContainerElement)sourceElement;
                        list.AddRange(sourceElement.Elements);
                        sourceElement.Elements.Clear();
                    }
                    else
                    {
                        list.Add(sourceElement);
                        DomContainerElement p = sourceElement.Parent;
                        if (p != null)
                        {
                            p.RemoveChild(sourceElement);
                        }
                    }
                    foreach (DomElement element in list)
                    {
                        element.OwnerDocument = this.OwnerDocument;
                    }
                    WriterUtils.SplitElements(list, true);
                    if (list.Count > 0)
                    {
                        this.OwnerDocument.ImportElements(list);
                        document.Dispose();
                        if (list.Count > 0)
                        {
                            this.Elements.Clear();
                            this.Elements.AddRange(list);
                            if (fastMode == false)
                            {
                                base.AfterLoad(FileFormat.XML);
                                this.UpdateContentVersion();
                                int startIndex = this.FirstContentElement.ViewIndex;
                                this.OwnerDocument.DocumentControler.MeasureElementSize(list);
                                this.ContentElement.RefreshPrivateContent(startIndex, -1, false);
                            }
                            return true;
                        }
                    }//if
                }//if
            }
            return false;
        }
    }
}
