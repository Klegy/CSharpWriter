/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using DCSoft.WinForms;
using DCSoft.Printing ;
 
using DCSoft.CSharpWriter.Dom;
using DCSoft.CSharpWriter.Commands;
using DCSoft.CSharpWriter.Dom.Undo;
using DCSoft.Drawing;
using System.Windows.Forms;
using System.Drawing;
using DCSoft.WinForms.Native;
using System.ComponentModel ;
using System.ComponentModel.Design ;
using System.Collections ;
using System.Collections.Generic ;
using DCSoft.Common;
using DCSoft.CSharpWriter.Undo;
using System.Text;
using DCSoft.CSharpWriter.Security;
using DCSoft.CSharpWriter.Printing;
using DCSoft.CSharpWriter.Data;
using DCSoft.CSharpWriter.Script;

namespace DCSoft.CSharpWriter.Controls
{
    /// <summary>
    /// 文本文档编辑控件
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [System.ComponentModel.ToolboxItem(true)]
    [System.Drawing.ToolboxBitmap(typeof(CSWriterControl))]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class CSWriterControl : TextPageViewControl
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public CSWriterControl()
        {
        }

        /// <summary>
        /// 应用程序数据基础路径
        /// </summary>
        [DefaultValue( null )]
        [Category("Data")]
        public string ApplicationDataBaseUrl
        {
            get
            {
                return this.AppHost.ApplicationDataBaseUrl ;
            }
            set
            {
                this.AppHost.ApplicationDataBaseUrl = value;
            }
        }

        private bool _AutoSetDocumentDefaultFont = true;
        /// <summary>
        /// 自动设置文档的默认字体
        /// </summary>
        /// <remarks>若该属性值为true，则编辑器会自动将控件的字体和前景色设置
        /// 到文档的默认字体和文本颜色。修改本属性值不会立即更新文档视图，
        /// 此时需要调用“UpdateDefaultFont( true )”来更新文档视图。</remarks>
        [DefaultValue(true )]
        [Category("Appearance")]
        public bool AutoSetDocumentDefaultFont
        {
            get
            {
                return _AutoSetDocumentDefaultFont; 
            }
            set
            {
                if (_AutoSetDocumentDefaultFont != value)
                {
                    _AutoSetDocumentDefaultFont = value;
                    //UpdateDefaultFont(true);
                }
            }
        }

        /// <summary>
        /// 更新文档的默认字体
        /// </summary>
        /// <param name="updateUI">是否更新用户界面</param>
        public void UpdateDefaultFont( bool updateUI )
        {
            if (this.AutoSetDocumentDefaultFont)
            {
                EditorSetDefaultFont(new XFontValue(this.Font), this.ForeColor, updateUI);
            }
        }

        /// <summary>
        /// 编辑器调用的设置文档的默认字体和颜色
        /// </summary>
        /// <param name="font">默认字体</param>
        /// <param name="color">默认文本颜色</param>
        /// <param name="updateUI">是否更新用户界面</param>
        internal void EditorSetDefaultFont(XFontValue font, Color color, bool updateUI)
        {
            if (this._Document != null)
            {
                // 设置文档的默认字体
                ViewEffects effects = this._Document.SetDefaultFont(font, color);
                if (updateUI && this.IsHandleCreated)
                {
                    if (effects == ViewEffects.Display)
                    {
                        this.Invalidate();
                    }
                    else if (effects == ViewEffects.Layout)
                    {
                        this.RefreshDocument();
                    }
                }
            }
        }

        private bool _AllowDragContent = false;
        /// <summary>
        /// 能否直接拖拽文档内容
        /// </summary>
        [System.ComponentModel.DefaultValue( false )]
        [Category("Behavior")]
        public bool AllowDragContent
        {
            get
            {
                return _AllowDragContent; 
            }
            set
            {
                _AllowDragContent = value; 
            }
        }
         
        //private bool _IEHost = false;
        ///// <summary>
        ///// 应用程序宿主是否为IE浏览器
        ///// </summary>
        //[Browsable( false )]
        //[DefaultValue( false )]
        //public bool IEHost
        //{
        //    get
        //    {
        //        return _IEHost; 
        //    }
        //    set
        //    {
        //        _IEHost = value; 
        //    }
        //}

        private FormList _ToolWindows = new FormList();
        /// <summary>
        /// 配套的工具窗体列表
        /// </summary>
        [Browsable( false)]
        public FormList ToolWindows
        {
            get
            {
                if (this.IsDisposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }
                return _ToolWindows; 
            }
        }
         

        private System.Windows.Forms.ToolTip _Tooltip = null;

        /// <summary>
        /// 判断指定名称功能命令是否可用
        /// </summary>
        /// <param name="commandName">功能命令名称</param>
        /// <returns>功能命令是否可用</returns>
        public bool IsCommandEnabled(string commandName)
        {
            return this.CommandControler.IsCommandEnabled(commandName);
        }

        /// <summary>
        /// 判断指定名称的功能命令是否处于勾选状态
        /// </summary>
        /// <param name="commandName">功能命令名称</param>
        /// <returns>功能命令是否处于勾选状态</returns>
        public bool IsCommandChecked(string commandName)
        {
            return this.CommandControler.IsCommandChecked(commandName);
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="commandName">命令文本</param>
        /// <param name="showUI">是否允许显示用户界面</param>
        /// <param name="parameter">用户参数</param>
        /// <returns>执行命令后的结果</returns>
        public object ExecuteCommand(
            string commandName,
            bool showUI,
            object parameter)
        {
            CheckHandle();
            object result = this.CommandControler.ExecuteCommand(
                commandName,
                showUI,
                parameter);
            this.DocumentControler.ClearSnapshot();
            return result;
        }

        public event WriterCommandEventHandler AfterExecuteCommand = null;
        /// <summary>
        /// 执行编辑器命令后的事件
        /// </summary>
        /// <param name="args">事件参数</param>
        public virtual void OnAfterExecuteCommand( WriterCommandEventArgs args)
        {
            if (AfterExecuteCommand != null)
            {
                AfterExecuteCommand(this, args);
            }
        }

        /// <summary>
        /// 自定义处理命令错误的事件
        /// </summary>
        public event CommandEventHandler CommandError = null;
        /// <summary>
        /// 触发自定义的错误事件
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="exp"></param>
        public virtual void OnCommandError(
            WriterCommand cmd, 
            WriterCommandEventArgs cmdArgs , 
            Exception exp)
        {
            if (CommandError != null)
            {
                CommandErrorEventArgs args = new CommandErrorEventArgs();
                args.WriterControl = this;
                args.CommandName = cmd.Name;
                args.CommmandParameter = cmdArgs.Parameter;
                args.Document = args.Document;
                args.Exception = exp;
                CommandError(this, args);
            }
            else
            {
                string msg = cmd.Name;
                if (exp != null)
                {
                    msg = msg + ":" + exp.Message;
                }
                ErrorReporter.ReportError(this, this.GetType(), msg , exp);
                //throw exp;
            }
        }

        private DateTime _LastUIEventTime = DateTime.Now;
        /// <summary>
        /// 最后一次用户界面事件的发生时间
        /// </summary>
        /// <remarks>这里的用户界面事件包括鼠标键盘事件、OLE拖拽事件，
        /// 应用程序可以根据这个属性值来实现超时锁定用户界面的功能。</remarks>
        [Browsable( false )]
        public DateTime LastUIEventTime
        {
            get
            {
                return _LastUIEventTime; 
            }
        }

        private bool _Readonly = false;
        /// <summary>
        /// 文档内容是否只读
        /// </summary>
        [DefaultValue(false)]
        [Category("Behavior")]
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

        private bool _HeaderFooterReadonly = false;
        /// <summary>
        /// 页眉页脚是否只读
        /// </summary>
        [DefaultValue(false)]
        [Category("Behavior")]
        public bool HeaderFooterReadonly
        {
            get
            {
                return _HeaderFooterReadonly; 
            }
            set 
            {
                _HeaderFooterReadonly = value; 
            }
        }
         
        /// <summary>
        /// 控件是否处于调试模式
        /// </summary>
        internal bool InDesignMode
        {
            get
            {
                if (this.DesignMode)
                {
                    return true;
                }
                
                Control c = this.Parent ;
                while (c != null)
                {
                    if (c.Site != null && c.Site.DesignMode  ) 
                    {
                        return true;
                    }
                    c = c.Parent;
                }
                
                return false;
            }
        }
         
        private FormViewMode _FormView = FormViewMode.Disable ;
        /// <summary>
        /// 表单视图模式
        /// </summary>
        [DefaultValue(FormViewMode.Disable )]
        [Category("Behavior")]
        public FormViewMode FormView
        {
            get
            {
                return _FormView; 
            }
            set
            {
                _FormView = value;
                this.DocumentControler.FormView = _FormView;
            }
        }

        /// <summary>
        /// 文档内容改变标记
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Modified
        {
            get
            {
                return this.Document.Modified;
            }
            set
            {
                this.Document.Modified = value;
            }
        }

        private object _ServerObject = null;
        /// <summary>
        /// 服务器对象
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object ServerObject
        {
            get
            {
                return _ServerObject;
            }
            set
            {
                _ServerObject = value;
                if (this._Document != null)
                {
                    this._Document.ServerObject = _ServerObject;
                }
            }
        }
         
        private DocumentOptions _DocumentOptions = new DocumentOptions();
        /// <summary>
        /// 文档设置
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DocumentOptions DocumentOptions
        {
            get
            {
                //if (_DocumentOptions == null && this.Document != null)
                //{
                //    return this.Document.Options;
                //}
                return _DocumentOptions; 
            }
            set
            {
                _DocumentOptions = value;
                if (_Document != null)
                {
                    _Document.Options = _DocumentOptions;
                }
            }
        }

        /// <summary>
        /// 控件字体发生改变事件，更新文档的默认字体
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (this._Document != null)
            {
                if (this.AutoSetDocumentDefaultFont)
                {
                    UpdateDefaultFont(true);
                }
                this.UpdateTextCaret();
            }
        }

        /// <summary>
        /// 文档基础路径
        /// </summary>
        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public string DocumentBaseUrl
        {
            get
            {
                return this.Document.BaseUrl;
            }
            set
            {
                this.Document.BaseUrl = value;
            }
        }

        /// <summary>
        /// 文档对象
        /// </summary>
        private DomDocument _Document = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DomDocument Document
        {
            get
            {
                if (_Document == null)
                {
                    _Document = new DomDocument();
                    _Document.Options = this.DocumentOptions;
                    _Document.EditorControl = this;
                    _Document.DocumentControler = this._DocumentControler;
                    _DocumentControler.Document = _Document;
                    if (this.AutoSetDocumentDefaultFont)
                    {
                        UpdateDefaultFont(false);
                    }
                    _Document.Clear();
                }
                else
                {
                    _Document.Options = this.DocumentOptions;
                    _Document.EditorControl = this;
                    _Document.DocumentControler = this._DocumentControler;
                    _DocumentControler.Document = _Document;
                }
                _Document.ServerObject = this.ServerObject;
                //if (this.DocumentOptions != null)
                //{
                //    _Document.Options = this.DocumentOptions;
                //}
                return _Document;
            }
            set
            {
                _Document = value;
                if (_Document != null)
                {
                    if (this.DocumentOptions != null)
                    {
                        _Document.Options = this.DocumentOptions;
                    }
                    _Document.EditorControl = this;
                    _Document.DocumentControler = this._DocumentControler;
                    _DocumentControler.Document = _Document;
                    _Document.ServerObject = this.ServerObject;
                    _Document.Options = this.DocumentOptions;
                    _Document.Body.FixElements();
                }
            }
        }

        /// <summary>
        /// 当前文档内容版本号，对文档内容的任何修改都会使得该版本号增加
        /// </summary>
        [Browsable(false)]
        public int DocumentContentVersion
        {
            get
            {
                return this.Document.ContentVersion;
            }
        }
         
        private DocumentControler _DocumentControler = new DocumentControler();
        /// <summary>
        /// 文档控制器
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DocumentControler DocumentControler
        {
            get
            {
                if (_DocumentControler == null)
                {
                    _DocumentControler = new DocumentControler();
                }
                if (_DocumentControler.EditorControl != this)
                {
                    _DocumentControler.EditorControl = this;
                }
                if (_DocumentControler.Document != this.Document)
                {
                    _DocumentControler.Document = this.Document;
                }
                return _DocumentControler;
            }
            set
            {
                _DocumentControler = value;
                if (_DocumentControler != null)
                {
                    _DocumentControler.Document = this.Document;
                    _DocumentControler.EditorControl = this;
                    _DocumentControler.IsAdministrator = this._IsAdministrator;
                    _DocumentControler.FormView = this._FormView;
                }
            }
        }

        private bool _IsAdministrator = false;
        /// <summary>
        /// 是否以管理员模式运行
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAdministrator
        {
            get
            {
                if (this._DocumentControler == null)
                {
                    return _IsAdministrator;
                }
                else
                {
                    return this.DocumentControler.IsAdministrator;
                }
            }
            set
            {
                _IsAdministrator = value;
                if (_DocumentControler != null)
                {
                    this.DocumentControler.IsAdministrator = value;
                }
            }
        }

        ///// <summary>
        ///// 当前访问权限等级
        ///// </summary>
        //[System.ComponentModel.Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        //public int CurrentAccessLevel
        //{
        //    get
        //    {
        //        return this.DocumentControler.CurrentAccessLevel;
        //    }
        //    set
        //    {
        //        this.DocumentControler.CurrentAccessLevel = value;
        //    }
        //}

        ///// <summary>
        ///// 启用AccessLevel权限控制
        ///// </summary>
        //[System.ComponentModel.Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public bool EnableAccessLevel
        //{
        //    get
        //    {
        //        return this.DocumentControler.EnableAccessLevel;
        //    }
        //    set
        //    {
        //        this.DocumentControler.EnableAccessLevel = value;
        //    }
        //}

        ///// <summary>
        ///// 是否隐藏高权限的文档内容
        ///// </summary>
        //[System.ComponentModel.Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public bool HideHightLevelContent
        //{
        //    get
        //    {
        //        return this.DocumentControler.HideHightLevelContent;
        //    }
        //    set
        //    {
        //        this.DocumentControler.HideHightLevelContent = value;
        //    }
        //}

        ///// <summary>
        ///// 高权限的文档内容是否只读
        ///// </summary>
        //[System.ComponentModel.Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public bool ReadonlyHightLevelContent
        //{
        //    get
        //    {
        //        return this.DocumentControler.ReadonlyHightLevelContent;
        //    }
        //    set
        //    {
        //        this.DocumentControler.ReadonlyHightLevelContent = value;
        //    }
        //}

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
                if (_AppHost == null)
                {
                    _AppHost = WriterAppHost.Default;
                }
                return _AppHost;
            }
            set
            {
                _AppHost = value;
            }
        }

        private ElementToolTipList _ToolTips = new ElementToolTipList();
        /// <summary>
        /// 元素提示文本信息列表
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ElementToolTipList ToolTips
        {
            get
            {
                return _ToolTips;
            }
            set
            {
                _ToolTips = value;
            }
        }

        private int _ToolTipsVersion = 0;
        /// <summary>
        /// 根据元素提示文本信息列表来更新用户界面
        /// </summary>
        /// <param name="ignoreCheckVersion">是否忽略提示信息版本检测</param>
        public void UpdateToolTip(bool ignoreCheckVersion)
        {
            if (ignoreCheckVersion == false)
            {
                if (this.ToolTips.Version == _ToolTipsVersion)
                {
                    return;
                }
            }
            _ToolTipsVersion = this.ToolTips.Version;

            // 显示提示文本
            DomElement element = this.HoverElement;
            ElementToolTip tip = null;
            while (element != null)
            {
                tip = this.ToolTips[element];
                if (tip != null)
                {
                    break;
                }
                element = element.Parent;
            }
            if (tip == null)
            {
                _Tooltip.SetToolTip(this, null);
            }
            else if (tip.Style == ToolTipStyle.ToolTip)
            {
                if (tip.Level == ToolTipLevel.Normal)
                {
                    _Tooltip.ToolTipIcon = ToolTipIcon.Info;
                }
                else if (tip.Level == ToolTipLevel.Warring)
                {
                    _Tooltip.ToolTipIcon = ToolTipIcon.Warning;
                }
                else if (tip.Level == ToolTipLevel.Error)
                {
                    _Tooltip.ToolTipIcon = ToolTipIcon.Error;
                }
                if( tip.Disposable )
                {
                    this.ToolTips.Remove(tip);
                    //this.ToolTips.IncreateVersion();
                }
                if (string.IsNullOrEmpty(tip.Title))
                {
                    _Tooltip.ToolTipTitle = WriterStrings.TipTitle;
                }
                else
                {
                    _Tooltip.ToolTipTitle = tip.Title;
                }
                _Tooltip.SetToolTip(this, tip.Text );
            }
        }

        /// <summary>
        /// 将插入点移动到指定位置
        /// </summary>
        /// <param name="target">移动的目标</param>
        public void MoveTo(MoveTarget target)
        {
            if (this.Document != null)
            {
                this.Document.Content.AutoClearSelection = true;
                this.Document.Content.MoveTo(target);
            }
        }

        /// <summary>
        /// 当前元素样式
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DocumentContentStyle CurrentStyle
        {
            get
            {
                return this.Document.CurrentStyle;
            }
        }

        /// <summary>
        /// 当前插入点所在的元素
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DomElement CurrentElement
        {
            get
            {
                return this.Document.CurrentElement;
            }
        }

        /// <summary>
        /// 获得当前插入点所在的指定类型的文档元素对象。
        /// </summary>
        /// <param name="elementType">指定的文档元素类型</param>
        /// <returns>获得的文档元素对象</returns>
        public DomElement GetCurrentElement(Type elementType)
        {
            if (elementType == null)
            {
                throw new ArgumentNullException("elementType");
            }
            if (elementType.Equals(typeof(DomElement))
                || elementType.IsSubclassOf(typeof(DomElement)))
            {
                return this.Document.GetCurrentElement(elementType);
            }
            else
            {
                throw new ArgumentOutOfRangeException(elementType.FullName + " Not XTex");
            }
        }

        /// <summary>
        /// 鼠标悬停的元素
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DomElement HoverElement
        {
            get
            {
                return this.Document.HoverElement;
            }
        }

        /// <summary>
        /// 当前文本行
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DomContentLine CurrentLine
        {
            get
            {
                return this.Document.CurrentContentElement.CurrentLine;
            }
        }

        /// <summary>
        /// 获得指定ID号的文档元素对象,查找时ID值区分大小写的。
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>找到的文档元素对象</returns>
        public DomElement GetElementById(string id)
        {
            return this.Document.GetElementById(id);
        }

        /// <summary>
        /// 获得文档中所有的指定类型的文档元素列表
        /// </summary>
        /// <param name="elementType">元素类型</param>
        /// <returns>获得的元素列表</returns>
        public DomElementList GetSpecifyElements(Type elementType)
        {
            if (this.Document == null)
            {
                return null;
            }
            else
            {
                return this.Document.GetSpecifyElements(elementType);
            }
        }

        /// <summary>
        /// 高亮度显示区域
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public HighlightInfo HighlightRange
        {
            get
            {
                return this.Document.HighlightManager.HighlightRange;
            }
            set
            {
                DomDocument document = this.Document;
                if (HighlightInfo.Compare(document.HighlightManager.HighlightRange, value) == false)
                {
                    if (document.HighlightManager.HighlightRanges != null)
                    {
                        foreach (HighlightInfo item in document.HighlightManager.HighlightRanges)
                        {
                            this.Document.InvalidateView(item.Range);
                        }
                    }
                    document.HighlightManager.HighlightRange = value;
                    if (document.HighlightManager.HighlightRange != null)
                    {
                        foreach (HighlightInfo item in document.HighlightManager.HighlightRanges)
                        {
                            this.Document.InvalidateView(item.Range);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 高亮度显示区域
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public HighlightInfoList HighlightRanges
        {
            get
            {
                return this.Document.HighlightManager.HighlightRanges;
            }
            set
            {
                DomDocument document = this.Document;
                if (document.HighlightManager.HighlightRanges != value)
                {
                    if (document.HighlightManager.HighlightRanges != null)
                    {
                        foreach (HighlightInfo item in document.HighlightManager.HighlightRanges)
                        {
                            this.Document.InvalidateView(item.Range);
                        }
                    }
                    document.HighlightManager.HighlightRanges = value;
                    foreach (HighlightInfo item in document.HighlightManager.HighlightRanges)
                    {
                        this.Document.InvalidateView(item.Range);
                    }
                }
            }
        }
        /// <summary>
        /// 设置指定的区域视图无效
        /// </summary>
        /// <param name="range">文档区域</param>
        public virtual void Invalidate(DomRange range)
        {
            if (range == null)
            {
                return;
                //throw new ArgumentNullException("range");
            }
            foreach (DomElement element in range)
            {
                this.Document.InvalidateElementView(element);
            }//foreach
        }

        private DomContentRender _ContentRender = new DomContentRender();
        /// <summary>
        /// 绘制文档内容的视图对象
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DomContentRender ContentRender
        {
            get
            {
                if (_ContentRender == null)
                {
                    _ContentRender = new DomContentRender();
                }
                _ContentRender.Document = this.Document;
                return _ContentRender;
            }
            set
            {
                _ContentRender = value;
            }
        }

        private CSWriterCommandControler _CommandControler = null;
        /// <summary>
        /// 命令控制器对象
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CSWriterCommandControler CommandControler
        {
            get
            {
                if (_CommandControler == null)
                {
                    _CommandControler = new CSWriterCommandControler();
                }
                _CommandControler.CommandContainer = this.AppHost.CommandContainer;
                _CommandControler.EditControl = this;
                _CommandControler.Document = this.Document;
                return _CommandControler;
            }
            set
            {
                _CommandControler = value;
                if (_CommandControler != null)
                {
                    _CommandControler.CommandContainer = this.AppHost.CommandContainer;
                    _CommandControler.EditControl = this;
                    _CommandControler.Document = this.Document;
                }
            }
        }

        /// <summary>
        /// 控件加载时的处理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (this.InDesignMode == false)
            {
                this.DocumentOptions.LoadConfig();
                 
                this.DocumentControler = new DocumentControler();
                this.myTransform = new DocumentViewTransform();

                if (this.Document == null)
                {
                    this.Document = new DomDocument();
                }
                this.Pages = this.Document.Pages;
                this.MouseDragScroll = false;
                _Tooltip = new System.Windows.Forms.ToolTip();
                
                this.ClearContent();
                //this.RefreshDocument();
            }
            base.OnLoad(e);
        }

        private bool _AutoUserLogin = false;
        /// <summary>
        /// 每打开文档时自动进行用户登录
        /// </summary>
        [DefaultValue( false )]
        [Category("Behavior")]
        public bool AutoUserLogin
        {
            get
            {
                return _AutoUserLogin; 
            }
            set
            {
                _AutoUserLogin = value; 
            }
        }

        private UserLoginInfo _AutoUserLoginInfo = null;
        /// <summary>
        /// 执行自动登录时使用的用户登录信息
        /// </summary>
        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public UserLoginInfo AutoUserLoginInfo
        {
            get
            {
                return _AutoUserLoginInfo; 
            }
            set
            {
                _AutoUserLoginInfo = value; 
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="userName">用户名</param>
        /// <param name="permissionLevel">用户等级</param>
        /// <returns>操作是否成功</returns>
        public bool UserLogin(string userID, string userName, int permissionLevel)
        {
            UserLoginInfo info = new UserLoginInfo();
            info.ID = userID;
            info.Name = userName;
            info.PermissionLevel = permissionLevel;
            return UserLogin(info, true);
        }

        /// <summary>
        /// 执行用户登录操作
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        /// <param name="updateUI">是否更新用户界面</param>
        /// <returns>操作是否成功</returns>
        public bool UserLogin(UserLoginInfo loginInfo , bool updateUI )
        {
            if (loginInfo == null)
            {
                throw new ArgumentNullException("loginInfo");
            }
             
            if (this.Document.UserHistories.CurrentInfo != null)
            {
                this.Document.UserHistories.CurrentInfo.SavedTime = DateTime.Now;
            }

            this.Document.UserHistories.Add(new UserHistoryInfo( loginInfo ));
            // 登录成功后会删除撤销信息
            if (this.Document.UndoList != null)
            {
                this.Document.UndoList.EndLog();
                this.Document.UndoList.Clear();
            }
            if (updateUI)
            {
                this.RefreshDocument();
            }
            else
            {
                this.Document.OnSelectionChanged();
            }
            return true;
        }

        /// <summary>
        /// 从指定的文件地址中加载文档
        /// </summary>
        /// <param name="strUrl">文件地址</param>
        /// <returns>是否成功加载文档</returns>
        public bool LoadDocument(
            string strUrl,
            FileFormat style)
        {
            CheckHandle();
            if (string.IsNullOrEmpty(strUrl))
            {
                throw new ArgumentNullException("strUrl");
            }
            this.Document.Load(strUrl, style);
            //this.OnDocumentLoad(EventArgs.Empty);
            this.RefreshDocument();
            this.Invalidate();
            return true;
        }

        /// <summary>
        /// 从指定的文件地址中加载文档
        /// </summary>
        /// <param name="strUrl">文件地址</param>
        /// <returns>是否成功加载文档</returns>
        public bool LoadDocument(
            System.IO.Stream stream,
            FileFormat style)
        {
            CheckHandle();
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            this.Document.Load(stream, style);
            //this.OnDocumentLoad(EventArgs.Empty);
            this.RefreshDocument();
            this.Document.Modified = false;
            this.Invalidate();
            return true;
        }

        /// <summary>
        /// 从指定的文件地址中加载文档
        /// </summary>
        /// <param name="reader">文件地址</param>
        /// <returns>是否成功加载文档</returns>
        public bool LoadDocument(
            System.IO.TextReader reader ,
            FileFormat style)
        {
            CheckHandle();
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            this.Document.Load(reader, style);
            //this.OnDocumentLoad(EventArgs.Empty);
            this.RefreshDocument();
            this.Document.Modified = false;
            this.Invalidate();
            return true;
        }
        /// <summary>
        /// 文档加载事件
        /// </summary>
        public event EventHandler DocumentLoad = null;
        /// <summary>
        /// 触发文档加载事件，触发此事件时，文档已经加载成功，但尚未显示出来。
        /// </summary>
        /// <param name="args">事件参数</param>
        public virtual void OnDocumentLoad(EventArgs args)
        {
            AddLastEventNames("DocumentLoad");

            if (this.AutoUserLogin && this.AutoUserLoginInfo != null )
            {
                // 执行用户自动登录
                this.UserLogin(this.AutoUserLoginInfo, true);
            }
            if (DocumentLoad != null)
            {
                DocumentLoad(this, args);
            }
        }

        /// <summary>
        /// 更新用户历史记录的时间
        /// </summary>
        public void UpdateUserInfoSaveTime()
        {
            this.Document.UpdateUserInfoSaveTime();
        }

        /// <summary>
        /// 保存文档到指定名称的文件中
        /// </summary>
        /// <param name="strUrl">文件名</param>
        /// <param name="style">文件格式</param>
        /// <returns>操作是否成功</returns>
        public bool SaveDocument(string strUrl, FileFormat style)
        {
            this.Document.Save(strUrl, style);
            return true;
        }

        /// <summary>
        /// 保存文档到指定的流中
        /// </summary>
        /// <param name="stream">文档流</param>
        /// <param name="style">文件格式</param>
        /// <returns>操作是否成功</returns>
        public bool SaveDocument(System.IO.Stream stream, FileFormat style)
        {
            this.Document.Save(stream, style);
            return true;
        }

        /// <summary>
        /// 清空文档内容
        /// </summary>
        public void ClearContent()
        {
            this.EnableJumpPrint = false;
            this.JumpPrintPosition = 0;
            this.MouseDragScroll = false;
            if (this.AutoSetDocumentDefaultFont)
            {
                this.UpdateDefaultFont(false);
            }
            this.Document.Clear();
            this.RefreshDocument();
            this.Invalidate();
        }

        public override Graphics CreateViewGraphics()
        {
            Graphics g = base.CreateViewGraphics();
            return g;
        }

        /// <summary>
        /// 刷新文档
        /// </summary>
        public void RefreshDocument()
        {
            if (DCSoft.Common.StackTraceHelper.CheckRecursion())
            {
                // 本方法不能递归调用
                return;
            }
            this.intGraphicsUnit = this.Document.DocumentGraphicsUnit;
            if (this.AutoSetDocumentDefaultFont)
            {
                bool mod = this.Document.Modified;
                this.UpdateDefaultFont(false);
                this.Document.Modified = mod;
            }
            using (System.Drawing.Graphics g = this.CreateViewGraphics())
            {
                foreach (DomDocumentContentElement dce in this.Document.Elements)
                {
                    dce.UpdateContentElements(false);
                }
                this.Pages = this.Document.Pages;
                this.Document.RefreshSize(g);
                this.Document.ExecuteLayout();// .RefreshLines();
                this.Document.RefreshPages();
                UpdatePages();
                this.Document.Content.FixCurrentIndexForStrictFormViewMode();
                this.UpdateTextCaret();
                this.Document.OnSelectionChanged();
            }
        }


        public override void UpdatePages()
        {
            if (this.Document != null)
            {
                //this.Pages.Clear();
                //this.Pages.AddRange(this.Document.Pages);
                this.Pages = this.Document.Pages;
                base.UpdatePages();


                PageContentPartyStyle style = PageContentPartyStyle.Body;
                if (this.Document.CurrentContentElement == this.Document.Body)
                {
                    style = PageContentPartyStyle.Body;
                }
                else if (this.Document.CurrentContentElement == this.Document.Header)
                {
                    style = PageContentPartyStyle.Header;
                }
                else if (this.Document.CurrentContentElement == this.Document.Footer)
                {
                    style = PageContentPartyStyle.Footer;
                }
                foreach (SimpleRectangleTransform item in this.PagesTransform)
                {
                    item.Enable = (item.ContentStyle == style);
                }//foreach
                if (_CurrentTransformItem != null)
                {
                    // 更新当前转换信息对象
                    bool match = false;
                    foreach (SimpleRectangleTransform item in this.PagesTransform)
                    {
                        if (item.DocumentObject == _CurrentTransformItem.DocumentObject
                            && item.PageIndex == _CurrentTransformItem.PageIndex
                            && item.ContentStyle == item.ContentStyle)
                        {
                            _CurrentTransformItem = item;
                            match = true;
                            break;
                        }
                    }//foreach
                    if (match == false)
                    {
                        _CurrentTransformItem = null;
                    }
                }
                foreach (SimpleRectangleTransform item in this.PagesTransform)
                {
                    if (item.ContentStyle == PageContentPartyStyle.Header
                        || item.ContentStyle == PageContentPartyStyle.Footer)
                    {
                        item.Enable = item == _CurrentTransformItem;
                    }
                }
            }
        }

        /// <summary>
        /// 控件数据
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get
            {
                if (this.Document != null)
                {
                    return this.Document.Text;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                CheckHandle();
                if (this.Document != null)
                {
                    this.Document.Text = value;
                }
                this.RefreshDocument();
                this.Invalidate();
            }
        }

        /// <summary>
        /// RTF文本
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string RTFText
        {
            get
            {
                if (this.Document == null)
                {
                    return "";
                }
                else
                {
                    return this.Document.RTFText;
                }
            }
            set
            {
                CheckHandle();
                if (this.Document != null)
                {
                    this.Document.RTFText = value;
                }
                this.RefreshDocument();
                this.Invalidate();
            }
        }

        /// <summary>
        /// XML文本
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string XMLText
        {
            get
            {
                if (this.Document == null)
                {
                    return null;
                }
                else
                {
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    DocumentSaver.SaveXmlFile(writer, this.Document);
                    string xml = writer.ToString();
                    return xml;
                }
            }
            set
            {
                CheckHandle();
                System.IO.StringReader reader = new System.IO.StringReader(value);
                DocumentLoader.LoadXmlFile(reader, this.Document);
                string txt = this.Document.GetDebugText();
                this.RefreshDocument();
                //MessageBox.Show( txt + "\r\n###" + this.Document.GetDebugText());
                this.Invalidate();
            }
        }

        /// <summary>
        /// 检测窗体句柄，确保已经创建了句柄
        /// </summary>
        private void CheckHandle()
        {
            if (this.IsHandleCreated == false)
            {
                this.CreateHandle();
            }
        }

        /// <summary>
        /// 单独执行一段外界注入的VB脚本代码
        /// </summary>
        /// <param name="scriptText">脚本代码</param>
        /// <returns>执行结果</returns>
        public object ExecuteVBScript( string scriptText  )
        {
            CheckHandle();
            object result = null;
            if (this.Document.Options.BehaviorOptions.EnableScript)
            {
                DocumentScriptEngine engine = new DocumentScriptEngine();
                string functionName = "F" + System.Environment.TickCount;
                string txt = "Function " + functionName + "\r\n" + scriptText + "\r\n End Function";
                engine.ScriptText = txt;
                engine.Document = this.Document;
                result = engine.Execute(functionName, null, false);
                engine.Dispose();
            }
            return result;
        }

        
         

        internal JumpPrintInfo _JumpPrint = new JumpPrintInfo();

        /// <summary>
        /// 是否允许续打
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(
            System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool EnableJumpPrint
        {
            get
            {
                return _JumpPrint.Enabled;
            }
            set
            {
                _JumpPrint.Enabled = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// 续打位置
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(
            System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int JumpPrintPosition
        {
            get
            {
                return _JumpPrint.NativePosition;
            }
            set
            {
                _JumpPrint.NativePosition = value;
                _JumpPrint.Page = null;
                foreach (SimpleRectangleTransform item in this.PagesTransform)
                {
                    if (value >= item.DescRectF.Top && value <= item.DescRectF.Bottom)
                    {
                        _JumpPrint.Page = (PrintPage)item.PageObject;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 当前鼠标悬浮的元素改变事件
        /// </summary>
        public event EventHandler HoverElementChanged = null;
        /// <summary>
        /// 触发鼠标悬停的文档元素改变事件。
        /// </summary>
        /// <param name="oldHoverElement">旧的鼠标悬停元素</param>
        /// <param name="newHoverElement">新的鼠标悬停元素</param>
        public virtual void OnHoverElementChanged(
            DomElement oldHoverElement,
            DomElement newHoverElement)
        {
            //if (this.ToolTips.Contains(newHoverElement) == false )
            //{
            //    if (newHoverElement is XTextCheckBoxElement)
            //    {
            //        XTextCheckBoxElement chk = ( 
            //    }
            //}
            if ( this.Document.Options.SecurityOptions.ShowPermissionTip)
            {
                if (newHoverElement != null)
                {
                    // 显示授权相关的提示信息
                    StringBuilder str = new StringBuilder();
                    UserHistoryInfo info2 = newHoverElement.OwnerDocument.UserHistories.GetInfo(newHoverElement.Style.CreatorIndex);
                    if (info2 != null)
                    {
                        str.Append(string.Format(
                            WriterStrings.CreatorTip_Name_Time,
                            info2.Name,
                            info2.SaveTimeString));
                    }
                    info2 = newHoverElement.OwnerDocument.UserHistories.GetInfo(newHoverElement.Style.DeleterIndex);
                    if (info2 != null)
                    {
                        if (str.Length > 0)
                        {
                            str.Append(Environment.NewLine);
                        }
                        str.Append(string.Format(
                            WriterStrings.DeleterTip_Name_Time,
                            info2.Name,
                            info2.SaveTimeString));
                    }
                    if (str.Length > 0)
                    {
                        this.ToolTips.Add(
                            newHoverElement,
                            str.ToString(),
                            ToolTipStyle.ToolTip, 
                            ToolTipLevel.Normal ).Disposable = true ;
                    }
                }
            }
            this.UpdateToolTip(true);
            HighlightInfo info = this.Document.HighlightManager[oldHoverElement];
            if (info != null && info.ActiveStyle == HighlightActiveStyle.Hover)
            {
                this.Invalidate(info.Range);
            }
            info = this.Document.HighlightManager[newHoverElement];
            if (info != null && info.ActiveStyle == HighlightActiveStyle.Hover)
            {
                this.Invalidate(info.Range);
            }
            if (HoverElementChanged != null)
            {
                HoverElementChanged(this, EventArgs.Empty);
            }
        }


        /// <summary>
        /// 点击链接事件
        /// </summary>
        public event LinkClickEventHandler LinkClick = null;
        /// <summary>
        /// 触发点击链接事件
        /// </summary>
        /// <param name="link">链接目标</param>
        public virtual void OnLinkClick(
            DomElement element,
            string link)
        {
            if (LinkClick != null)
            {
                LinkClick(this, new LinkClickEventArgs(_Document, element, link));
            }
        }

        public override Point ViewPointToClient(int x, int y)
        {
            if (_CurrentTransformItem == null)
            {
                return base.ViewPointToClient(x, y);
            }
            else
            {
                return _CurrentTransformItem.UnTransformPoint(x, y);
            }
        }

        public override Point ClientPointToView(int x, int y)
        {
            if (_CurrentTransformItem == null)
            {
                return base.ClientPointToView(x, y);
            }
            else
            {
                return _CurrentTransformItem.TransformPoint(x, y);
            }
        }

        //private UpdateLock _UpdateLock = new UpdateLock();
        ///// <summary>
        ///// 开始更新内容，锁定用户界面
        ///// </summary>
        //public void BeginUpdate()
        //{
            
        //    _UpdateLock.BeginUpdate();
        //}

        ///// <summary>
        ///// 结束更新内容，解锁用户界面
        ///// </summary>
        //public void EndUpdate()
        //{
        //    _UpdateLock.EndUpdate();
        //}

        ///// <summary>
        ///// 是否正在更新内容，锁定用户界面
        ///// </summary>
        //[Browsable( false )]
        //public bool IsUpdating
        //{
        //    get
        //    {
        //        return _UpdateLock.Updating;
        //    }
        //}

        /// <summary>
        /// 滚动视图到光标位置
        /// </summary>
        public void ScrollToCaret()
        {
            if (_Document == null || this.IsUpdating )
            {
                return;
            }
            CheckHandle();
            DomDocumentContentElement ce = this.Document.CurrentContentElement;

            DomElement element = ce.Content.CurrentElement;
            if (element != null)
            {
                if (ce.Content.LineEndFlag)
                {
                    DomElement e2 = ce.Content.GetPreElement(element);
                    if (e2 == null)
                    {
                        e2 = element;
                    }
                    if (e2 != null)
                    {
                        this.ScrollToView(
                            (int)(e2.AbsLeft + e2.Width - 1),
                            (int)(e2.AbsTop + e2.Height),
                            (this.InsertMode ? DefaultCaretWidth : DefaultBroadCaretWidth),
                            (int)e2.Height);
                    }
                }
                else
                {
                    this.ScrollToView(
                        (int)element.AbsLeft,
                        (int)(element.AbsTop + element.Height),
                        (this.InsertMode ? DefaultCaretWidth : DefaultBroadCaretWidth),
                        (int)element.Height);
                }
            }
        }

        private bool _HideCaretWhenHasSelection = true;
        /// <summary>
        /// 当选择了文档内容时隐藏光标
        /// </summary>
        [DefaultValue( true )]
        [Category("Appearance")]
        public bool HideCaretWhenHasSelection
        {
            get
            {
                return _HideCaretWhenHasSelection; 
            }
            set
            {
                _HideCaretWhenHasSelection = value; 
            }
        }

        /// <summary>
        /// 根据指定的文档元素对象更新光标
        /// </summary>
        /// <param name="myElement">指定的文档元素对象</param>
        public void UpdateTextCaret(DomElement element)
        {
            if (this.IsUpdating)
            {
                // 正在更新用户界面，无法更新光标
                return;
            }
            if (this.Document == null )
            {
                // 文档内容为空，无法更新光标。
                return;
            }
            if (element != null)
            {
                DomDocumentContentElement ce = this.Document.CurrentContentElement;
                if (ce.Content.LineEndFlag)
                {
                    DomElement e2 = ce.Content.GetPreElement(
                        element);
                    if (e2 == null)
                    {
                        e2 = element;
                    }
                    if (e2 != null)
                    {
                        float curHeight = e2.Height;
                        if (e2.OwnerLine != null)
                        {
                            if (e2.OwnerLine.AdditionHeight < 0)
                            {
                                curHeight = Math.Min(
                                    curHeight,
                                    e2.OwnerLine.Height + e2.OwnerLine.AdditionHeight);
                            }
                        }
                        if (this.HideCaretWhenHasSelection && this.Selection.Length != 0  )
                        {
                            base.HideOwnerCaret();
                            base.ScrollToView((int)(e2.AbsLeft ),
                                (int)(e2.AbsTop + curHeight),
                                (int)(e2.Width ),
                                (int) curHeight);
                        }
                        else
                        {
                            if (this.Focused || this.ForceShowCaret )
                            {
                                base.ShowCaret();
                            }
                            base.MoveTextCaretTo(
                                (int)(e2.AbsLeft + e2.Width - 1),
                                (int)(e2.AbsTop + curHeight ),
                                (int) curHeight);
                        }
                    }
                }
                else
                {
                    DomElement e2 = element;
                    DomContentLine line = element.OwnerLine;
                    if (line != null)
                    {
                        e2 = line.GetPreElement(element);
                        if (e2 == null)
                        {
                            e2 = element;
                        }
                    }

                    if (this.HideCaretWhenHasSelection && this.Selection.Length != 0)
                    {
                        // 当选择了内容时禁用光标则隐藏光标。
                        base.HideOwnerCaret();
                        if (this.Focused)
                        {
                            base.ScrollToView(
                                (int)(element.AbsLeft),
                                (int)(element.AbsTop),
                                (int)(element.Width),
                                (int)element.Height);
                        }
                    }
                    else
                    {
                        if (this.Focused || this.ForceShowCaret)
                        {
                            // 若控件获得焦点或者强制显示光标则显示光标
                            base.ShowCaret();
                        }
                        
                        float curHeight = e2.Height;
                        if (e2.OwnerLine != null)
                        {
                            if (e2.OwnerLine.AdditionHeight < 0)
                            {
                                curHeight = Math.Min(
                                    curHeight,
                                    e2.OwnerLine.Height + e2.OwnerLine.AdditionHeight);
                            }
                        }
                        base.MoveTextCaretTo(
                            (int)element.AbsLeft,
                            (int)(e2.AbsTop + curHeight ),
                            (int)curHeight);
                    }
                }
            }
        }

        /// <summary>
        /// 根据当前元素更新光标
        /// </summary>
        public void UpdateTextCaret()
        {
            if (this.Document == null)
            {
                return;
            }
            UpdateTextCaret(this.Document.CurrentElement);
        }

        /// <summary>
        /// 根据当前元素更新光标，而且不会造成用户视图区域的滚动
        /// </summary>
        public void UpdateTextCaretWithoutScroll()
        {
            if (this.IsUpdating)
            {
                return;
            }
            if (this.Document == null)
            {
                return;
            }
            bool back = this.MoveCaretWithScroll;
            this.MoveCaretWithScroll = false;
            UpdateTextCaret(this.Document.CurrentElement);
            this.MoveCaretWithScroll = back;
        }


        /// <summary>
        /// 选中文档所有内容
        /// </summary>
        public void SelectAll()
        {
            this.Document.Content.SelectAll();
            UpdateTextCaret();
        }

        /// <summary>
        /// 剪切
        /// </summary>
        public void Cut()
        {
            this.DocumentControler.Cut();
        }
        /// <summary>
        /// 复制
        /// </summary>
        public void Copy()
        {
            this.DocumentControler.Copy();
        }
        /// <summary>
        /// 粘贴
        /// </summary>
        public void Paste()
        {
            this.DocumentControler.Paste();
        }
        /// <summary>
        /// 撤销操作
        /// </summary>
        public void Undo()
        {
            if (this.Document.UndoList != null)
            {
                XUndoEventArgs e = new XUndoEventArgs();
                this.Document.UndoList.Undo(e);
            }
        }
        /// <summary>
        /// 重复操作
        /// </summary>
        public void Redo()
        {
            if (this.Document.UndoList != null)
            {
                XUndoEventArgs e = new XUndoEventArgs();
                this.Document.UndoList.Redo(e);
            }
        }
        /// <summary>
        /// 删除选择区域
        /// </summary>
        public void DeleteSelection()
        {
            this.DocumentControler.Delete();
        }

        /// <summary>
        /// 获得从1开始计算的当前列号
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public int CurrentColumnIndex
        {
            get
            {
                if (this.Document.Content.CurrentElement != null)
                {
                    return this.Document.Content.CurrentElement.ColumnIndex;
                }
                else
                {
                    return -1;
                }
            }
        }
        /// <summary>
        /// 当前行号
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public int CurrentLineIndex
        {
            get
            {
                if (this.Document.Content.CurrentLine == null)
                {
                    return -1;
                }
                else
                {
                    return this.Document.Content.CurrentLine.GlobalIndex;
                }
            }
        }
        /// <summary>
        /// 获得从1开始计算的当前文本行在页中的序号
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public int CurrentLineIndexInPage
        {
            get
            {
                if (this.Document.Content.CurrentLine == null)
                {
                    return -1;
                }
                else
                {
                    return this.Document.Content.CurrentLine.IndexInPage;
                }
            }
        }
        /// <summary>
        /// 文档选择的部分
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public DomSelection Selection
        {
            get
            {
                return this.Document.Selection;
            }
        }

        /// <summary>
        /// 打印整个文档
        /// </summary>
        public void PrintDocument()
        {
            CheckHandle();
            DocumentPrinter printer = new DocumentPrinter(this.Document);
            printer.JumpPrint = this._JumpPrint;
            printer.CurrentPage = this.CurrentPage;
            printer.PrintRange = System.Drawing.Printing.PrintRange.AllPages;
            printer.PrintDocument(true);
            this.RefreshDocument();
        }

        /// <summary>
        /// 打印当前页
        /// </summary>
        public void PrintCurrentPage()
        {
            CheckHandle();
            DocumentPrinter printer = new DocumentPrinter(this.Document);
            printer.JumpPrint = this._JumpPrint;
            printer.CurrentPage = this.CurrentPage;
            printer.PrintRange = System.Drawing.Printing.PrintRange.CurrentPage;
            printer.PrintDocument(true);
            this.RefreshDocument();
        }

        /// <summary>
        /// 文档内容发生改变事件
        /// </summary>
        public event System.EventHandler DocumentContentChanged = null;
        /// <summary>
        /// 触发文档内容发生改变事件
        /// </summary>
        /// <param name="args">事件参数</param>
        public virtual void OnDocumentContentChanged(EventArgs args)
        {
            AddLastEventNames("DocumentContentChanged");
            if (DocumentContentChanged != null)
            {
                DocumentContentChanged(this, args);
            }
        }

        private List<string> _LastEventNames = null;
        /// <summary>
        /// 获得收集到的事件名称列表，各个事件名称之间用逗号分开。
        /// </summary>
        /// <remarks>目前支持的事件名称有DocumentContentChanged、DocumentLoad、
        /// SelectionChanged、SelectionChanging、StatusTextChanged。
        /// 当编辑器控件嵌入在HTML页面中运行时，JavaScript可能无法响应控件事件，此时可以
        /// 调用定时器定期调用这个函数来获得已经触发的事件名称，然后进行事件处理。</remarks>
        /// <returns>事件名称列表。</returns>
        public string GetLastEventNames()
        {
            StringBuilder str = new StringBuilder();
            if (_LastEventNames.Count > 0)
            {
                foreach (string name in _LastEventNames)
                {
                    if (str.Length > 0)
                    {
                        str.Append(",");
                    }
                    str.Append(name);
                }
                _LastEventNames.Clear();
            }
            return str.ToString();
        }

        /// <summary>
        /// 添加事件名称到最后事件名称列表中
        /// </summary>
        /// <param name="name">事件名称</param>
        private void AddLastEventNames(string name)
        {
            if (_LastEventNames == null)
            {
                _LastEventNames = new List<string>();
            }
            if (_LastEventNames.Contains(name) == false)
            {
                _LastEventNames.Add(name);
            }
        }

        /// <summary>
        /// 文档选择状态发生改变后的事件
        /// </summary>
        public event EventHandler SelectionChanged = null;
        /// <summary>
        /// 触发文档选择状态发生改变后的事件
        /// </summary>
        /// <param name="args">事件参数</param>
        public virtual void OnSelectionChanged(EventArgs args)
        {
            AddLastEventNames("SelectionChanged");
            if (SelectionChanged != null)
            {
                SelectionChanged(this, args);
            }
            if (this.CommandControler != null)
            {
                this.CommandControler.EditControl = this;
                DomElement element = this.CurrentElement;
                if (element != null)
                {
                    this.CommandControler.Document = element.OwnerDocument;
                }
                if (this.CommandControler.IsExecutingCommand == false)
                {
                    this.CommandControler.UpdateBindingControlStatus();
                }
            }
        }

        /// <summary>
        ///  表示当前插入点位置信息的字符串
        /// </summary>
        [Browsable( false )]
        public string PositionInfoText
        {
            get
            {
                DomContentLine line = this.Document.CurrentContentElement.CurrentLine;
                if (line != null && line.OwnerPage != null)
                {
                    string txt =
                        string.Format( WriterStrings.LineInfo_PageIndex_LineIndex_ColumnIndex  ,
                        Convert.ToString(line.OwnerPage.PageIndex),
                        Convert.ToString(this.CurrentLineIndexInPage ),
                        Convert.ToString(this.CurrentColumnIndex));
                    return txt ;
                     
                }
                return null;
            }
        }

        /// <summary>
        /// 文档选择状态正在发生改变事件
        /// </summary>
        public event SelectionChangingEventHandler SelectionChanging = null;
        /// <summary>
        /// 触发文档选择状态发生改变事件
        /// </summary>
        /// <param name="args">事件参数</param>
        public virtual void OnSelectionChanging(SelectionChangingEventArgs args)
        {
            AddLastEventNames("SelectionChanging");
            if (SelectionChanging != null)
            {
                SelectionChanging(this, args);
            }
        }

        //protected override void OnMouseClick(MouseEventArgs e)
        //{
        //    base.OnMouseClick(e);
        //    if (this.Document != null)
        //    {
        //        if (e.Clicks == 2)
        //        {
        //            this.Document.Content.SelectWord();
        //        }
        //        else if (e.Clicks == 3)
        //        {
        //            this.Document.Content.SelectParagraph();
        //        }
        //    }
        //}

        ///// <summary>
        ///// 处理视图中鼠标双击事件
        ///// </summary>
        ///// <param name="e">事件参数</param>
        //protected override void OnViewMouseDoubleClick(MouseEventArgs e)
        //{
        //    if (this.Document != null)
        //    {
        //        this.Document.CurrentContentElement.Content.SelectWord();
        //    }
        //}

        ///// <summary>
        ///// 处理视图中鼠标双击事件
        ///// </summary>
        ///// <param name="e">事件参数</param>
        //protected override void OnViewDoubleClick(System.Windows.Forms.MouseEventArgs e)
        //{
        //    if( this.Document != null )
        //    {
        //        this.Document.CurrentContentElement.Content.SelectWord();
        //    }
        //}


        ///// <summary>
        ///// 处理视图中鼠标按键按下事件
        ///// </summary>
        ///// <param name="e">事件参数</param>
        //protected override void OnViewMouseDown(System.Windows.Forms.MouseEventArgs e)
        //{
        //    if( e.Button == System.Windows.Forms.MouseButtons.Right 
        //        && this.ContextMenu != null )
        //    {
        //        return ;
        //    }
        //    if( this.Document != null )
        //    {
        //        DocumentEventArgs args = DocumentEventArgs.CreateMouseDown( this.Document , e );
        //        this.Document.HandleDocumentEvent(args );
        //        this.Cursor = args.Cursor ;
        //        _Tooltip.SetToolTip( this , args.Tooltip );
        //        XTextElement element = this.Document.Content.GetElementAt(e.X, e.Y , true );
        //        if (element != null)
        //        {
        //            this.BeginEditElementValue(element);
        //        }
        //    }
        //    base.OnViewMouseDown (e);
        //    //this.MoveTo( e.X , e.Y );
        //}

        /// <summary>
        /// 正在编辑文档元素数值
        /// </summary>
        [Browsable(false)]
        public bool IsEditingElementValue
        {
            get
            {
                return (this.EditorHost != null && this.EditorHost.CurrentEditContext != null);
            }
        }

        /// <summary>
        /// 取消当前的编辑元素内容的操作
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool CancelEditElementValue()
        {
            if (this.EditorHost != null && this.EditorHost.CurrentEditContext != null)
            {
                this.EditorHost.CancelEditValue();
                return true;
            }
            return false;
        }
         
         
        /// <summary>
        /// 处理键盘按键按下事件
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            this._LastUIEventTime = DateTime.Now;
            base.OnKeyDown(e);
            if (e.Handled)
            {
                // 该事件已经被处理了
                return;
            }

            if (this._Document != null)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    if (this.IsEditingElementValue)
                    {
                        // 按下ESC键取消操作
                        this.CancelEditElementValue();
                        e.Handled = true;
                        return;
                    }
                }
                DocumentEventArgs args = DocumentEventArgs.CreateKeyDown(this.Document, e);
                if (this.Document != null)
                {
                    // 优先让文档对象处理键盘按键事件
                    this.Document.HandleDocumentEvent(args);
                }
                if (args.CancelBubble == false)
                {
                    // 文档对象处理完毕，并允许进行后续事件时，调用快捷键命令
                    WriterCommand cmd = this.AppHost.CommandContainer.Active(
                        this,
                        this.Document,
                        e);
                    //System.Console.WriteLine( e.KeyCode + " " + e.Control );
                    if (cmd != null)
                    {
                        WriterCommandEventArgs args2 = new WriterCommandEventArgs(
                            this,
                            this.Document,
                            WriterCommandEventMode.Invoke);
                        args2.UIElement = this;
                        args2.UIEventArgs = e;
                        args2.AltKey = e.Alt;
                        args2.ShiftKey = e.Shift;
                        args2.CtlKey = e.Control;
                        args2.KeyCode = e.KeyCode;
                        cmd.Invoke(args2);
                        this.DocumentControler.ClearSnapshot();
                        if (this.CommandControler != null)
                        {
                            // 刷新控件状态
                            if (args2.RefreshLevel == UIStateRefreshLevel.Current)
                            {
                                this.CommandControler.UpdateBindingControlStatus(cmd.Name);
                            }
                            else if (args2.RefreshLevel == UIStateRefreshLevel.All)
                            {
                                this.CommandControler.UpdateBindingControlStatus();
                            }
                        }
                        e.Handled = true;
                    }//if
                }//if
            }
        }

        /// <summary>
        /// 指示可以使用输入法
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        protected override bool CanEnableIme
        {
            get
            {
                return true;
            }
        }


        //private ContentStateControler _ContentStateControler = new ContentStateControler();
        ///// <summary>
        ///// 文档内容状态控制器
        ///// </summary>
        //[System.ComponentModel.Browsable(false)]
        //[System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        //public ContentStateControler ContentStateControler
        //{
        //    get
        //    {
        //        if (_ContentStateControler == null)
        //        {
        //            _ContentStateControler = new ContentStateControler();
        //        }
        //        _ContentStateControler.Document = this.Document;
        //        return _ContentStateControler; 
        //    }
        //    set
        //    {
        //        _ContentStateControler = value; 
        //    }
        //}

        /// <summary>
        /// 处理键盘字符事件
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            this._LastUIEventTime = DateTime.Now;
            base.OnKeyPress(e);
            if (e.Handled)
            {
                // 该事件已经被处理了.
                return;
            }
            if (e.KeyChar == '\r'
                && Control.ModifierKeys == Keys.Shift)
            {
                return;
            }
            if (this._Document != null)
            {
                if (e.KeyChar == '\t' && Control.ModifierKeys != Keys.Control)
                {
                    
                    if (this.Document.Options.EditOptions.TabKeyToFirstLineIndent)
                    {
                        // 对于Tab字符试图设置当前段落的首行缩进
                        if (this.Selection.Length == 0)
                        {
                            DomElement element = this.CurrentElement;
                            DomContentElement ce = element.ContentElement;
                            DomElement preElement = ce.PrivateContent.GetPreElement(element);
                            if (preElement == null || preElement is DomParagraphFlagElement)
                            {
                                // 当前元素是段落第一个元素
                                DomParagraphFlagElement flag = element.OwnerParagraphEOF;
                                WriterCommand cmd = this.AppHost.CommandContainer.GetCommand(StandardCommandNames.FirstLineIndent);
                                if (cmd != null)
                                {
                                    // 执行设置首行缩进命令
                                    WriterCommandEventArgs args = new WriterCommandEventArgs();
                                    args.Document = element.OwnerDocument;
                                    args.EditorControl = this;
                                    args.Host = this.AppHost;
                                    args.Mode = WriterCommandEventMode.QueryState;
                                    cmd.Invoke(args);
                                    if (args.Enabled)
                                    {
                                        args.Parameter = true;
                                        args.Mode = WriterCommandEventMode.Invoke;
                                        cmd.Invoke(args);
                                        this.DocumentControler.ClearSnapshot();
                                    }
                                    e.Handled = true;
                                    return;
                                }
                            }
                        }
                    }
                }

                if (this.Readonly == false
                    && this.DocumentControler.CanInsertElementAtCurrentPosition(
                        typeof( DomElement ) , 
                        DomAccessFlags.Normal ))
                {
                    // 若当前位置能插入字符则插入字符
                    this.DocumentControler.InsertChar(e.KeyChar);
                }

                //this.Document.OnKeyPress( 
                //    DocumentEventArgs.CreateKeyPress( this.Document , e ));
            }
            e.Handled = true;
        }

        /// <summary>
        /// 处理键盘按钮松开事件
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
        {
            this._LastUIEventTime = DateTime.Now;
            base.OnKeyUp(e);
            if (e.Handled)
            {
                // 该事件已经被处理了
                return;
            }
            if (this._Document != null)
            {
                this.Document.HandleDocumentEvent(
                    DocumentEventArgs.CreateKeyUp(
                        this.Document,
                        e));
            }
        }

        /// <summary>
        /// 处理鼠标移动事件
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            this._LastUIEventTime = DateTime.Now;
            base.OnMouseMove(e);
            if (this._Document == null)
            {
                // 没有文档，不处理事件
                return;
            }
            if (this._JumpPrint.Enabled)
            {
                this.Cursor = Cursors.Default;
                return;
            }
            if (e.Button != System.Windows.Forms.MouseButtons.None)
            {
                if (this.MouseDragScroll)
                {
                    this.UseAbsTransformPoint = true;
                    base.OnMouseMove(e);
                    this.UseAbsTransformPoint = false;
                    return;
                }
            }
            MyHandleMouseEvent(e, DocumentEventStyles.MouseMove);
        }

        /// <summary>
        /// 处理鼠标按键按下事件
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            this._LastUIEventTime = DateTime.Now;
            if (this._Document == null)
            {
                // 无文档，不处理事件
                base.OnMouseDown(e);
                return;
            }
            if (this._JumpPrint.Enabled)
            {
                // 设置续打位置
                JumpPrintInfo infoBack = this._JumpPrint.Clone();
                _JumpPrint.Page = null;
                _JumpPrint.Position = 0;
                foreach (SimpleRectangleTransform item in this.PagesTransform)
                {
                    if (item.ContentStyle == PageContentPartyStyle.Body
                        && item.SourceRectF.Top <= e.Y
                        && item.SourceRectF.Bottom >= e.Y)
                    {
                        PrintPage page = (PrintPage)item.PageObject;
                        DomDocument doc = (DomDocument)page.Document;
                        int pos = item.TransformPoint(e.X, e.Y).Y;
                        if (pos >= 0)
                        {
                            this._JumpPrint.Page = page;
                            this._JumpPrint.NativePosition = pos;
                            JumpPrintInfo info = doc.GetJumpPrintInfo(pos);
                            if (info != null)
                            {
                                this._JumpPrint.Page = info.Page;
                                this._JumpPrint.Position = info.Position;
                            }
                            else
                            {
                                this._JumpPrint.Page = page;
                                this._JumpPrint.Position = pos;
                            }
                        }
                        break;
                    }
                }//foreach
                if (this._JumpPrint.Page  != infoBack.Page 
                    || this._JumpPrint.Position != infoBack.Position )
                {
                    this.Invalidate();
                }
            }//if (myJumpPrint.Enabled)
            else
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    if (this.ContextMenu != null || this.ContextMenuStrip != null)
                    {
                        return;
                    }
                }
                MyHandleMouseEvent(e, DocumentEventStyles.MouseDown);
            }
            if (this.Focused == false)
            {
                this.Focus();
            }
        }

        /// <summary>
        /// 正在拖拽控件文档内容标记
        /// </summary>
        private bool _DoDragContenting = false;

        /// <summary>
        /// 处理鼠标事件
        /// </summary>
        /// <param name="e">事件参数</param>
        /// <param name="style">事件类型</param>
        private void MyHandleMouseEvent(MouseEventArgs e, DocumentEventStyles style)
        {
            if (this._Document == null)
            {
                return;
            }
            this.RefreshScaleTransform();
            if (this.HeaderFooterReadonly == false)
            {
                foreach (SimpleRectangleTransform item2 in this.PagesTransform)
                {
                    if (item2.PartialAreaSourceBounds.Contains(e.X, e.Y) 
                        && item2.Enable == false )
                    {
                        if (item2.ContentStyle == PageContentPartyStyle.Header
                            || item2.ContentStyle == PageContentPartyStyle.Footer)
                        {
                            // 命中未激活页眉页脚区域，可能用户打算双击激活页眉页脚区域
                            // 因此退出处理。
                            return;
                        }
                    }//if
                }//foreach
            }//if
            SimpleRectangleTransform item = this.PagesTransform.GetItemByPoint(
                        e.X,
                        e.Y,
                        true,
                        true,
                        true);
            if (item != null)
            {
                DomDocument document = (DomDocument)item.DocumentObject;
                Point p = new Point(e.X, e.Y);
                p = RectangleCommon.MoveInto(p, item.SourceRect);
                if (p.Y == item.SourceRect.Bottom)
                {
                    p.Y = item.SourceRect.Bottom - 2 ;
                }
                p = item.TransformPoint(p.X, p.Y);
                DocumentEventArgs args = DocumentEventArgs.CreateMouseDown(
                    document,
                    new MouseEventArgs(e.Button, e.Clicks, p.X, p.Y, e.Delta));
                args._StrictMatch = item.SourceRect.Contains(e.X, e.Y);
                args.intStyle = style;
                // 调用文档对象的事件处理过程
                document.HandleDocumentEvent(args);
                this.UpdateToolTip(false);
                this.Cursor = args.Cursor;
                if (args.CancelBubble == false)
                {
                    if (style == DocumentEventStyles.MouseDown
                        && e.Button == MouseButtons.Left )
                    {
                        if (e.Clicks == 1)
                        {
                            bool handled = false;
                            if (this.AllowDragContent && this.Selection.Length != 0)
                            {
                                foreach (DomElement element in this.Selection)
                                {
                                    RectangleF rect = element.AbsBounds;
                                    rect.Width = rect.Width + element.WidthFix;
                                    if (rect.Contains(p.X, p.Y))
                                    {
                                        // 开始拖拽文档内容
                                        if (DCSoft.WinForms.Native.MouseCapturer.DragDetect(this.Handle))
                                        {
                                            this.Cursor = Cursors.Default;
                                            System.Windows.Forms.DataObject obj = this._DocumentControler.CreateSelectionDataObject( );
                                            DragDropEffects allowEff = DragDropEffects.Copy;
                                            if (this.Readonly == false )
                                            {
                                                allowEff = allowEff | DragDropEffects.Move;
                                            }
                                            DragDropEffects effect = DragDropEffects.None;
                                            try
                                            {
                                                this._DoDragContenting = true;
                                                effect = this.DoDragDrop(
                                                    obj,
                                                    allowEff);
                                            }
                                            finally
                                            {
                                                this._DoDragContenting = false;
                                            }
                                            if (effect == DragDropEffects.None)
                                            {
                                                this.UpdateTextCaret();
                                            }
                                            else if ((effect & DragDropEffects.Move) == DragDropEffects.Move)
                                            {
                                                this.DeleteSelection();
                                            }
                                            handled = true;
                                        }
                                        break;
                                    }//if
                                }//foreach
                            }
                            if (handled == false)
                            {
                                document._MouseCapture = new MouseCaptureInfo(args);
                                document.Content.AutoClearSelection = !args.ShiftKey;
                                document.Content.MoveTo(args.X, args.Y);
                                //myBindControl.MoveTo( args.X , args.Y );

                                this.UpdateTextCaret();
                                this.UseAbsTransformPoint = true;
                            }
                        }
                        else if (e.Clicks == 2)
                        {
                            DomElement element = document.Content.GetElementAt(p.X, p.Y, true);
                            if (element != null)
                            {
                                if (item.SourceRect.Contains(e.X, e.Y))
                                {
                                     
                                }
                            }
                        }
                    }
                    else if (style == DocumentEventStyles.MouseMove)
                    {
                        //_Tooltip.SetToolTip(this, args.Tooltip);
                        this.Cursor = args.Cursor;
                    }
                    else if (style == DocumentEventStyles.MouseUp)
                    {

                    }
                }
            }
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            this._LastUIEventTime = DateTime.Now;
            base.OnMouseUp(e);
            if (this._Document == null)
            {
                // 无文档，不处理事件
                return;
            }
            if (this.InDesignMode == false && this.EnableJumpPrint == false )
            {
                // 检测鼠标三击事件
                if (this.Document != null && this.IsEditingElementValue == false)
                {
                    int click = MouseClickCounter.Instance.Count(e.X, e.Y);
                    if (click == 2)
                    {
                        //if (this.CurrentElement != null)
                        //{
                        //    if (this.BeginEditElementValue(this.CurrentElement, false) )
                        //    {
                        //        return;
                        //    }
                        //}
                        this.Document.Content.SelectWord();
                    }
                    else if (click == 3)
                    {
                        //if (this.FormView == FormViewMode.Strict)
                        //{

                        //}
                        //else
                        {
                            this.Document.Content.SelectParagraph();
                        }
                    }
                    //else
                    //{
                    //    MyHandleMouseEvent(e, DocumentEventStyles.MouseUp);
                    //}
                }
                //if ( MouseClickCounter.Instance.Count( e.X , e.Y ) == 3 )// ThreeClickChecker.Instance.Check(e.X, e.Y))
                //{
                //    this.OnThreeClick(new System.EventArgs());
                //}
            }
        }

        /// <summary>
        /// 处理鼠标光标离开事件
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            this._LastUIEventTime = DateTime.Now;
            base.OnMouseLeave(e);
            if (this.InDesignMode == false)
            {
                if (this._JumpPrint.Enabled == false)
                {
                    if (this._Document != null)
                    {
                        DocumentEventArgs args = new DocumentEventArgs(
                            this.Document,
                            this.Document,
                            DocumentEventStyles.MouseLeave);
                        this.Document.HandleDocumentEvent(args);
                    }
                }
            }
        }

        ///// <summary>
        ///// 处理鼠标连续点击三击事件
        ///// </summary>
        ///// <param name="e">事件参数</param>
        //protected virtual void OnThreeClick( EventArgs e )
        //{
        //    //if (this.Document != null)
        //    //{
        //    //    // 鼠标连续点击三次，则选择当前段落
        //    //    this.Document.Content.SelectParagraph();
        //    //}
        //}

        /// <summary>
        /// 处理控件视图滚动事件
        /// </summary>
        /// <param name="se">事件参数</param>
        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            if (this._Document != null)
            {
                if (this.IsEditingElementValue)
                {
                    // 文档内容发生滚动时取消元素值编辑操作
                    this.EditorHost.CancelEditValue();
                }
            }
        }

        /// <summary>
        /// 处理鼠标滚轮事件
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            this._LastUIEventTime = DateTime.Now;
            base.OnMouseWheel(e);
            if (this._Document != null)
            {
                if (this.IsEditingElementValue)
                {
                    // 文档内容发生滚动时取消元素值编辑操作
                    this.EditorHost.CancelEditValue();
                }
            }
        }


        /// <summary>
        /// 当前获得光标焦点的坐标转换信息对象
        /// </summary>
        private SimpleRectangleTransform _CurrentTransformItem = null;

        internal SimpleRectangleTransform GetTransformItemByDescPoint(float x, float y)
        {
            if (_CurrentTransformItem != null && _CurrentTransformItem.DescRectF.Contains(x, y))
            {
                return _CurrentTransformItem;
            }
            return this.PagesTransform.GetByDescPoint(x, y);
        }

        /// <summary>
        /// 处理鼠标双击事件
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        { 
            base.OnMouseDoubleClick(e);
        
            if (this._Document == null)
            {
                // 无文档，不处理消息
                return;
            }
            this._LastUIEventTime = DateTime.Now;
            bool handled = false;
            int x = e.X;// -this.AutoScrollPosition.X;
            int y = e.Y;// -this.AutoScrollPosition.Y;
            this._CurrentTransformItem = null;
            foreach (SimpleRectangleTransform item in this.PagesTransform)
            {
                DomDocument document = (DomDocument)item.DocumentObject;
                bool contains = item.PartialAreaSourceBounds.Contains(x, y);
                //if (contains == false)
                //{
                //    if (item.ContentStyle == PageContentPartyStyle.Body)
                //    {
                //        // 若为正文区域，则使用整个正文页面矩形进一步判断
                //        PrintPage page = (PrintPage)item.PageObject;
                //        Rectangle rect = page.ClientBounds;
                //        rect.X = rect.X + page.ClientMargins.Left + this.AutoScrollPosition.X;
                //        rect.Y = rect.Y + page.ClientMargins.Top + this.AutoScrollPosition.Y;
                //        rect.Width = rect.Width - page.ClientMargins.Left - page.ClientMargins.Right;
                //        rect.Height = rect.Height - page.ClientMargins.Top - page.ClientMargins.Bottom;
                //        contains = rect.Contains(x, y);
                //    }
                //}
                if (contains == false)
                {
                    continue;
                }
                if (this.HeaderFooterReadonly)
                {
                    // 页眉页脚内容只读
                    if (item.ContentStyle == PageContentPartyStyle.Header
                        || item.ContentStyle == PageContentPartyStyle.Footer)
                    {
                        continue;
                    }
                }
                DomDocumentContentElement newContentElement = null;
                switch (item.ContentStyle)
                {
                    case PageContentPartyStyle.Body:
                        newContentElement = document.Body;
                        break;
                    case PageContentPartyStyle.Header:
                        newContentElement = document.Header;
                        _CurrentTransformItem = item;
                        break;
                    case PageContentPartyStyle.Footer:
                        newContentElement = document.Footer;
                        _CurrentTransformItem = item;
                        break;
                }
                if (document.CurrentContentElement != newContentElement)
                {
                    DomDocumentContentElement oldContentElement = document.CurrentContentElement;
                    document.CurrentContentElement = newContentElement;
                    document.CurrentContentElement.RaiseFocusEvent(
                        oldContentElement.CurrentElement,
                        newContentElement.CurrentElement);
                }
                if (item.Enable == false)
                {
                    // 若命中的区域是不可用的则更新文档状态
                    document.CurrentContentElement.FixElements();
                    this.UpdatePages();////////////////////
                    //this.RefreshScaleTransform();
                    this.Invalidate();
                    this.UpdateTextCaret();
                    handled = true;
                }
                break;
            }//foreach
            if (handled == false)
            {
                base.OnMouseDoubleClick(e);
            }
        }

        /// <summary>
        /// 控件大小改变事件处理
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.InDesignMode == false && this.IsHandleCreated )
            {
                if (this._Document != null)
                {
                    this.RefreshScaleTransform();
                    this.UpdateTextCaret();
                }
            }
        }
         
        /// <summary>
        /// 处理绘制用户界面事件
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (this.InDesignMode)
            {
                // 控件处于设计模式
                RectangleF rect = new RectangleF(
                    0,
                    0,
                    this.ClientSize.Width,
                    this.ClientSize.Height);
                using (StringFormat f = new StringFormat())
                {
                    // 显示控件的名称、类型和版本号。
                    f.Alignment = StringAlignment.Center;
                    f.LineAlignment = StringAlignment.Center;
                    string text = this.Name
                        + System.Environment.NewLine + this.GetType().FullName
                        + System.Environment.NewLine + "Version : " + this.ProductVersion;

                    e.Graphics.DrawString(
                        text ,
                        this.Font,
                        Brushes.Red,
                        rect,
                        f);
                }//using
            }
            else
            {
                if (this.IsFreezeUI)
                {
                    // 绘制冻结的用户界面
                    this.DrawFreezeUI(e);
                    return;
                }
                if (this.IsUpdating)
                {
                    return;
                }
                if (this._Document == null)
                {
                    // 绘制无文档字样
                    RectangleF rect = new RectangleF(
                    0,
                    0,
                    this.ClientSize.Width,
                    this.ClientSize.Height);
                    using (StringFormat f = new StringFormat())
                    {
                        f.Alignment = StringAlignment.Center;
                        f.LineAlignment = StringAlignment.Center;
                        e.Graphics.DrawString(
                            WriterStrings.NoDocument,
                            this.Font,
                            Brushes.Red,
                            rect,
                            f);
                    }//using
                }
                else
                {
                    this.HeaderFooterFlagVisible = HeaderFooterFlagVisible.None;
                    if (this.Document.CurrentContentPartyStyle == PageContentPartyStyle.Header
                        || this.Document.CurrentContentPartyStyle == PageContentPartyStyle.Footer)
                    {
                        this.HeaderFooterFlagVisible = HeaderFooterFlagVisible.HeaderFooter;
                    }
                    //this.HeaderFooterFlagVisible = HeaderFooterFlagVisible.HeaderFooter;
                    _SelectionAreaRectangles = new List<Rectangle>();
                    base.OnPaint(e);
                    if (_SelectionAreaRectangles.Count > 0)
                    {
                        DocumentViewOptions vop = this.Document.Options.ViewOptions;
                        if (vop.SelectionHighlight == SelectionHighlightStyle.Invert)
                        {
                            // 反色高亮度显示选择区域
                            IntPtr hdc = e.Graphics.GetHdc();
                            using (ReversibleDrawer drawer = ReversibleDrawer.FromHDC(hdc))
                            {
                                // 绘制可逆矩形
                                foreach (SimpleRectangleTransform item in this.PagesTransform)
                                {
                                    if (item.Enable
                                        && item.ContentStyle == this.Document.CurrentContentPartyStyle)
                                    {
                                        foreach (Rectangle rect in _SelectionAreaRectangles)
                                        {
                                            Rectangle rect2 = Rectangle.Intersect(rect, item.DescRect);
                                            if (rect2.IsEmpty == false)
                                            {
                                                rect2 = item.UnTransformRectangle(rect2);
                                                Rectangle rect3 = Rectangle.Intersect(rect2, e.ClipRectangle);
                                                if (rect3.IsEmpty == false)
                                                {
                                                    drawer.FillRectangle(rect3);
                                                }//if
                                            }//if
                                        }//foreach
                                    }//if
                                }//foreach
                            }//using
                            e.Graphics.ReleaseHdc(hdc);
                        }
                        else if (vop.SelectionHighlight == SelectionHighlightStyle.MaskColor)
                        {
                            // 使用遮盖色高亮度显示被选择的区域
                            using (SolidBrush b = new SolidBrush(vop.SelectionHightlightMaskColor))
                            {
                                // 绘制可逆矩形
                                foreach (SimpleRectangleTransform item in this.PagesTransform)
                                {
                                    if (item.Enable
                                        && item.ContentStyle == this.Document.CurrentContentPartyStyle)
                                    {
                                        foreach (Rectangle rect in _SelectionAreaRectangles)
                                        {
                                            Rectangle rect2 = Rectangle.Intersect(rect, item.DescRect);
                                            if (rect2.IsEmpty == false)
                                            {
                                                rect2 = item.UnTransformRectangle(rect2);
                                                Rectangle rect3 = Rectangle.Intersect(rect2, e.ClipRectangle);
                                                if (rect3.IsEmpty == false)
                                                {
                                                    e.Graphics.FillRectangle(b, rect3);
                                                }//if
                                            }//if
                                        }//foreach
                                    }//if
                                }//foreach
                            }//using
                        }//if

                        //foreach (Rectangle rect in _ReversibleViewRectangles)
                        //{
                        //    base.ReversibleViewFillRect(rect , e.Graphics );
                        //}
                    }
                    _SelectionAreaRectangles = null;
                    if (this.Document.Options.ViewOptions.ShowPageLine
                        && this.Document.Pages.Count > 1)
                    {
                        // 显示分页线
                        base.DrawPageLines(e);
                    }
                    if (this.EnableJumpPrint)
                    {
                        // 绘制续打模式下的不打印区域覆盖矩形
                        base.DrawJumpPrintArea(
                            e.Graphics,
                            e.ClipRectangle,
                            this._JumpPrint,
                            System.Drawing.Color.FromArgb(100, 0, 0, 255));
                    }//if
                }//else
            }//else
        }
         
        /// <summary>
        /// 可逆矩形列表
        /// </summary>
        private List<Rectangle> _SelectionAreaRectangles = null;

        /// <summary>
        /// 添加被选择区域矩形
        /// </summary>
        /// <param name="rect">矩形</param>
        public void AddSelectionAreaRectangle(Rectangle rect)
        {
            if (_SelectionAreaRectangles != null)
            {
                for (int iCount = _SelectionAreaRectangles.Count - 1; iCount >= 0; iCount--)
                {
                    Rectangle item = _SelectionAreaRectangles[iCount];
                    if (item.Contains(rect))
                    {
                        return;
                    }
                    Rectangle iRect = Rectangle.Intersect(item, rect);
                    if (iRect.IsEmpty == false)
                    {
                        if (iRect.Equals(item))
                        {
                            _SelectionAreaRectangles.RemoveAt(iCount);
                            //break;
                        }
                        else if (iRect.Equals(rect))
                        {
                            return;
                        }
                        else if (iRect.Top == item.Top && iRect.Height == item.Height)
                        {
                            if (iRect.Left == item.Left)
                            {
                                item.Width = item.Right - iRect.Right;
                                item.X = iRect.Right;
                                _SelectionAreaRectangles[iCount] = item;
                            }
                            else if (iRect.Right == item.Right)
                            {
                                item.Width = iRect.Left - item.Left;
                                _SelectionAreaRectangles[iCount] = item;
                            }
                        }
                        else if (iRect.Left == item.Left && iRect.Width == item.Width)
                        {
                            if (iRect.Top == item.Top)
                            {
                                item.Height = item.Bottom - iRect.Bottom;
                                item.Y = iRect.Bottom;
                                _SelectionAreaRectangles[iCount] = item;
                            }
                            else if (iRect.Bottom == item.Bottom)
                            {
                                item.Height = iRect.Top - item.Top;
                                _SelectionAreaRectangles[iCount] = item;
                            }
                        }
                        else if (iRect.Top == rect.Top && iRect.Height == rect.Height)
                        {
                            if (iRect.Left == rect.Left)
                            {
                                rect.Width = rect.Right - iRect.Right;
                                rect.X = iRect.Right;
                            }
                            else if (iRect.Right == rect.Right)
                            {
                                rect.Width = iRect.Left - rect.Left;
                            }
                        }
                         
                        else if (iRect.Left == rect.Left && iRect.Width == rect.Width)
                        {
                            if (iRect.Top == rect.Top)
                            {
                                rect.Height = rect.Bottom - iRect.Bottom;
                                rect.Y = iRect.Bottom;
                            }
                            else if (iRect.Bottom == rect.Bottom)
                            {
                                rect.Height = iRect.Top - rect.Top;
                            }
                        }
                    }
                }//foreach
                if (rect.IsEmpty == false)
                {
                    _SelectionAreaRectangles.Add(rect);
                }
            }
        }

        /// <summary>
        /// 声明指定区域无效，需要重新绘制。无效矩形采用视图坐标。
        /// </summary>
        /// <param name="rect">无效矩形</param>
        public void ViewInvalidate(RectangleF rect, PageContentPartyStyle party)
        {
            DomDocument doc = this.Document;
            if (doc != null)
            {
                //SimpleRectangleTransform maxItem = null;
                //float maxArea = 0;
                foreach (SimpleRectangleTransform item in this.PagesTransform)
                {
                    if (item.ContentStyle == party)
                    {
                        RectangleF irect = RectangleF.Intersect(rect, item.DescRectF);
                        if (irect.Width > 2 && irect.Height > 2)// item.DescRectF.Contains(rect.Left, rect.Top))
                        {
                            System.Drawing.PointF p = item.UnTransformPointF(rect.Location);
                            if (item.SourceRectF.Contains(p))// p.IsEmpty == false)
                            {
                                RectangleF rect2 = new RectangleF(
                                    p,
                                    item.UnTransformSizeF(rect.Size));
                                if (rect2.Width > 0 && rect2.Height > 0)
                                {
                                    rect2.Offset(-2, -2);
                                    rect2.Width += 4;
                                    rect2.Height += 4;
                                    Rectangle rect3 = Rectangle.Ceiling(rect2);
                                    this.Invalidate(rect3);
                                }
                            }
                        }//if
                    }//if
                }//foreach
            }
        }


        ///// <summary>
        ///// 处理绘制文档内容事件
        ///// </summary>
        ///// <param name="e">绘制参数</param>
        ///// <param name="trans">坐标转化对象</param>
        //protected override void OnViewPaint(
        //    System.Windows.Forms.PaintEventArgs e, 
        //    SimpleRectangleTransform trans)
        //{
        //    if (this.IsUpdating )
        //    {
        //        return;
        //    }
        //    this.Document.PageIndex = trans.PageIndex ;
        //    if (trans.ContentStyle == PageContentPartyStyle.Header )
        //    {
        //        this.Document.DrawHead(e.Graphics, trans.DescRect);
        //    }
        //    else if (trans.ContentStyle == PageContentPartyStyle.Body )
        //    {
        //        this.Document.DrawDocument(e.Graphics, e.ClipRectangle);
        //    }
        //    else if (trans.ContentStyle == PageContentPartyStyle.Footer )
        //    {
        //        this.Document.DrawFooter(e.Graphics, trans.DescRect);
        //    }
        //}

        /// <summary>
        /// 处理控件获得焦点事件,刷新光标
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (this._Document == null)
            {
                // 无文档，不处理事件
                return;
            }
            this.UpdateTextCaret();
            if (this.IsEditingElementValue == false)
            {
                // 正在编辑文档元素内容时可能会弹出窗体导致控件失去或获得焦点
                // 对这种情况不需处理
                if (this.Document != null)
                {
                    this.Document.OnControlGotFocus();
                }
            }
        }

        /// <summary>
        /// 处理控件失去焦点事件
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (this._Document != null)
            {
                if (this.IsEditingElementValue == false)
                {
                    // 正在编辑文档元素内容时可能会弹出窗体导致控件失去或获得焦点
                    // 对这种情况不需处理
                    if (this.Document != null)
                    {
                        this.Document.OnControlLostFocus();
                    }
                }
            }
        }

        //		protected override void OnImeModeChanged(EventArgs e)
        //		{
        //			this.UpdateTextCaret();
        //			base.OnImeModeChanged (e);
        //		}

        /// <summary>
        /// 保存输入法一次输入的字符的缓存区
        /// </summary>
        private StringBuilder _IME_CompositionString = null;
        //private Msgs _LastMsg = Msgs.WM_NULL; 

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            DCSoft.WinForms.Native.Msgs msg = (Msgs)m.Msg;
            if (msg == Msgs.WM_IME_CHAR)
            {
                // 记录输入法输入的字符
                if (_IME_CompositionString == null)
                {
                    _IME_CompositionString = new StringBuilder();
                }
                _IME_CompositionString.Append((char)m.WParam);
                return;
            }
            else
            {
                if (_IME_CompositionString != null && _IME_CompositionString.Length > 0)
                {
                    // 提交输入法输入的文本内容
                    string text = _IME_CompositionString.ToString();

                    _IME_CompositionString = null;

                    if (this.Readonly == false
                        && this.DocumentControler.CanInsertElementAtCurrentPosition(
                            typeof(DomCharElement),
                            DomAccessFlags.Normal))
                    {
                        // 将输入法输入的文本整体插入到文档中
                        this.DocumentControler.InsertString(text, true, InputValueSource.UI);
                    }
                }
            }

            //if (msg == Msgs.WM_IME_STARTCOMPOSITION)
            //{
            //    // 输入法开始提交输入的文本
            //    _IME_CompositionString = new StringBuilder();
            //    return;
            //}
            //else if (msg == Msgs.WM_IME_NOTIFY)
            //{
            //    // 输入法开始提交输入的文本
            //    if (_IME_CompositionString == null)
            //    {
            //        _IME_CompositionString = new StringBuilder();
            //    }
            //}
            //else if (msg == Msgs.WM_IME_CHAR)
            //{
            //    // 输入法提交一个字符
            //    if (_IME_CompositionString != null)
            //    {
            //        _IME_CompositionString.Append((char)m.WParam);
            //        return;
            //    }
            //}
            //else if (msg == Msgs.WM_IME_ENDCOMPOSITION)
            //{
            //    // 输入法结束提交一次输入。
            //    if (_IME_CompositionString != null)
            //    {
            //        string text = _IME_CompositionString.ToString();

            //        _IME_CompositionString = null;

            //        if (this.Readonly == false
            //            && this.DocumentControler.CanInsertElementAtCurrentPosition(
            //                typeof(XTextCharElement),
            //                DomAccessFlags.Normal))
            //        {
            //            // 将输入法输入的文本整体插入到文档中
            //            this.DocumentControler.InsertString(text, true, InputValueSource.UI);
            //        }
            //    }
            //    return;
            //}
            
            if (m.Msg == Win32Imm.WM_IME_NOTIFY
                && m.WParam.ToInt32() == Win32Imm.IMN_OPENSTATUSWINDOW)
            {
                // 处理输入法
                this.UpdateTextCaret();
                //System.Console.WriteLine( System.Environment.TickCount + "=" +  m.Msg.ToString("X") + "   " + m.WParam  );
            }
            //			if( m.HWnd == this.Handle && m.Msg >= 0x281 && m.Msg <= 0x291 )
            //			{
            //				System.Console.WriteLine( System.Environment.TickCount + "=" +  m.Msg.ToString("X") + "   " + m.WParam + " :" + m.LParam  );
            //			}
            ////			if( m.Msg == Windows32.Win32Imm.WM_IME_NOTIFY )
            ////			{
            ////				System.Console.WriteLine( m.WParam );
            ////				this.UpdateTextCaret();
            ////				//return ;
            ////			}
            base.WndProc(ref m);
        }


        #region 处理OLE拖拽事件 ***********************************************
 
        private Point ClientPointToViewAbs(Point p)
        {
            MultiPageTransform transform = (MultiPageTransform)this.Transform;
            PointF p2 = transform.AbsTransformPoint(p.X, p.Y);
            return new Point((int)p2.X, (int)p2.Y);
        }

         /// <summary>
        /// 处理OLE拖拽进入事件
        /// </summary>
        /// <param name="drgevent">事件参数</param>
        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            this._LastUIEventTime = DateTime.Now;
            base.OnDragEnter(drgevent);
            if (this._Document == null)
            {
                // 无文档，不处理事件
                return;
            }
            drgevent.Effect = DragDropEffects.None;
            MyHandleDragEvent(drgevent, 0);
        }
        /// <summary>
        /// 处理OLE拖拽经过事件
        /// </summary>
        /// <param name="drgevent">事件参数</param>
        protected override void OnDragOver(System.Windows.Forms.DragEventArgs drgevent)
        {
            this._LastUIEventTime = DateTime.Now;
            base.OnDragOver(drgevent);
            if (this._Document == null)
            {
                // 无文档，不处理事件
                return;
            }
            drgevent.Effect = System.Windows.Forms.DragDropEffects.None;
            MyHandleDragEvent(drgevent, 0);
        }
        /// <summary>
        /// 处理OLE拖拽反馈事件
        /// </summary>
        /// <param name="gfbevent">事件参数</param>
        protected override void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
        {
            this._LastUIEventTime = DateTime.Now;
            gfbevent.UseDefaultCursors = true;
            base.OnGiveFeedback(gfbevent);
        }
        /// <summary>
        /// 处理OLE拖拽完成事件
        /// </summary>
        /// <param name="drgevent">事件参数</param>
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            if (this._Document == null)
            {
                // 无文档，不处理事件
                base.OnDragDrop(drgevent);
                return;
            }
            this.ForceShowCaret = false;
            MyHandleDragEvent(drgevent, 1);
            base.OnDragDrop(drgevent);
        }

        /// <summary>
        /// 内部的处理OLE拖拽事件
        /// </summary>
        /// <param name="drgevent"></param>
        /// <param name="style"></param>
        private void MyHandleDragEvent(DragEventArgs drgevent, int style)
        {
            _LastUIEventTime = DateTime.Now;
            if (this.PagesTransform != null)
            {
                SimpleRectangleTransform item = this.PagesTransform.GetItemByPoint(
                    drgevent.X,
                    drgevent.Y,
                    true,
                    true,
                    true);
                if (item != null)
                {
                    Point p2 = new Point(drgevent.X, drgevent.Y);
                    p2 = this.PointToClient(p2);
                    PointF p = new PointF(p2.X, p2.Y);
                    p = RectangleCommon.MoveInto(p, item.SourceRectF);
                    p = item.TransformPointF(p.X, p.Y);
                    DomDocument document = (DomDocument)item.DocumentObject;
                    DomElement element = document.Content.GetElementAt(p.X, p.Y, false);
                    if (element != null)
                    {
                        int index = document.Content.FixIndexForStrictFormViewMode(
                            document.Content.IndexOf(element),
                            DomContent.FixIndexDirection.Both,
                            false);
                        if (index >= 0)
                        {
                            element = document.Content[index];
                        }
                        else
                        {
                            drgevent.Effect = DragDropEffects.None;
                            return;
                        }
                        this.ForceShowCaret = true;
                        this.UseAbsTransformPoint = true;
                        if (this._DoDragContenting)
                        {
                            bool back = this.HideCaretWhenHasSelection;
                            this.HideCaretWhenHasSelection = false;
                            this.UpdateTextCaret(element);
                            this.HideCaretWhenHasSelection = back;
                            if (document.Selection.Contains(element))
                            {
                                drgevent.Effect = DragDropEffects.None;
                                this.SetStatusText(null) ;
                                return;
                            }
                        }
                        else
                        {
                            document.Content.AutoClearSelection = true;
                            document.Content.MoveTo(p.X, p.Y);
                            element = document.CurrentElement;
                        }
                        this.UseAbsTransformPoint = false;
                        
                        if (document.DocumentControler.CanDragDrop(element, drgevent, p.X, p.Y))
                        {
                            if ((drgevent.KeyState & 4) == 4
                                    && (drgevent.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
                            {
                                SetStatusText(WriterStrings.WhereToMove);
                                drgevent.Effect = DragDropEffects.Move;
                            }
                            else if ((drgevent.KeyState & 8) == 8
                                && (drgevent.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                            {
                                SetStatusText(WriterStrings.WhereToCopy);
                                drgevent.Effect = DragDropEffects.Copy;
                            }
                            else if ((drgevent.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
                            {
                                SetStatusText(WriterStrings.WhereToMove);
                                drgevent.Effect = DragDropEffects.Move;
                            }
                            else if ((drgevent.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                            {
                                SetStatusText(WriterStrings.WhereToCopy);
                                drgevent.Effect = DragDropEffects.Copy;
                            }
                            else
                            {
                                SetStatusText(null);
                                drgevent.Effect = DragDropEffects.None;
                            }

                            if (style == 1)
                            {
                                if (this._DoDragContenting
                                    && ((drgevent.Effect & DragDropEffects.Move) == DragDropEffects.Move))
                                {
                                    this.DeleteSelection();
                                    drgevent.Effect = DragDropEffects.Copy;
                                }
                                document.Content.AutoClearSelection = true;
                                this.ForceShowCaret = false;

                                this.UseAbsTransformPoint = true;
                                document.Content.SetSelection(element.ViewIndex, 0);
                                //document.Content.MoveTo(ViewX, ViewY);
                                this.UseAbsTransformPoint = false;
                                SetStatusText(null);
                                document.DocumentControler.DragDrop(element, drgevent, p.X, p.Y);
                                this.Update();
                            }
                        }
                        else
                        {
                            SetStatusText(null);
                            drgevent.Effect = DragDropEffects.None;
                            //if (style == 1)
                            //{
                            //    document.Content.MoveTo(p.X, p.Y);
                            //}
                        }
                    }
                }
            }
        }

        #endregion

        private string _StatusText = null;
        /// <summary>
        /// 状态文本
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        public string StatusText
        {
            get
            {
                return _StatusText; 
            }
            set
            {
                _StatusText = value; 
            }
        }

        /// <summary>
        /// 设置状态栏文本，并触发事件
        /// </summary>
        /// <param name="text">新状态栏文本</param>
        public void SetStatusText(string text)
        {
            if (_StatusText != text)
            {
                _StatusText = text;
                AddLastEventNames("StatusTextChanged");
                if (StatusTextChanged != null)
                {
                    StatusTextChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 状态栏文本发生改变事件
        /// </summary>
        public event EventHandler StatusTextChanged = null ;

        /// <summary>
        /// 销毁对象
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (_ToolWindows != null)
            {
                _ToolWindows.DisposeAllForm();
                _ToolWindows = null;
            }
            if (_EditorHost != null)
            {
                _EditorHost.Dispose();
                _EditorHost = null;
            }
            //if (_SearchDialog != null)
            //{
            //    _SearchDialog.Dispose();
            //    _SearchDialog = null;
            //}
            base.Dispose(disposing);
        }

        private TextWindowsFormsEditorHost _EditorHost = new TextWindowsFormsEditorHost();

        /// <summary>
        /// 编辑器宿主对象
        /// </summary>
        [Browsable(false)]
        public TextWindowsFormsEditorHost EditorHost
        {
            get
            {
                if (_EditorHost == null)
                {
                    _EditorHost = new TextWindowsFormsEditorHost();
                }
                _EditorHost.EditControl = this;
                _EditorHost.Document = this.Document;
                return _EditorHost;
            }
        }

        /// <summary>
        /// 设置弹出式列表的最佳位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="height"></param>
        internal Rectangle GetCompositionRect(float x, float y, float height)
        {
            height = GraphicsUnitConvert.Convert(
                height,
                this.GraphicsUnit,
                System.Drawing.GraphicsUnit.Pixel);
            System.Drawing.Point pos = this.ViewPointToClient((int)x, (int)y);
            //pos = this.PointToScreen( pos );
            return new System.Drawing.Rectangle(pos.X, pos.Y, 10, (int)height);
            //pos = myPopupList.GetPopupPos( pos.X , pos.Y ,height);
            //myPopupList.Location = pos ;
        }

        /// <summary>
        /// 显示关于对话框
        /// </summary>
        public void ShowAboutDialog()
        {
            using (dlgAbout dlg = new dlgAbout())
            {
                dlg.ShowDialog(this);
            }
        }
 
          
    }//public class TextDocumentEditor : XDesignerPrinting.PageScrollableControl
}