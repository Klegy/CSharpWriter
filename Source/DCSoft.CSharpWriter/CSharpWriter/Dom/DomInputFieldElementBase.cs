using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.Common;
using DCSoft.CSharpWriter.Controls;
using System.ComponentModel;
using DCSoft.CSharpWriter.Data;
using DCSoft.Data ;

using DCSoft.Drawing;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 基础的纯文本数据输入域
    /// </summary>
    [Serializable()]
    [System.Xml.Serialization.XmlType("XInputFieldBase")]
    [System.Diagnostics.DebuggerDisplay("Base Input Name:{Name}")]
    public class DomInputFieldElementBase : DomFieldElementBase 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomInputFieldElementBase()
        {
            //_AcceptChildElementTypes = ElementType.Text;
        }

        private float _SpecifyWidth = 0f;
        /// <summary>
        /// 输入域指定宽度,若大于0则输入域宽度不小于该值，而且当内容很多时，自动变宽。
        /// </summary>
        [DefaultValue( 0f)]
        public float SpecifyWidth
        {
            get
            {
                return _SpecifyWidth; 
            }
            set
            {
                _SpecifyWidth = value; 
            }
        }

        /// <summary>
        /// 修正结束元素的宽度
        /// </summary>
        internal virtual bool FixEndElementWidth()
        {
            if (this.SpecifyWidth > 0)
            {
                DomElement ee = this.EndElement;
                DomElementList list = new DomElementList();
                this.AppendContent(list, false);
                if (list.Contains(ee))
                {
                    float totalWidth = 0f;
                    float borderWidth = this.OwnerDocument.PixelToDocumentUnit(
                        DomFieldBorderElement.StandardPixelWidth );
                    foreach (DomElement element in list)
                    {
                        if (element is DomFieldBorderElement)
                        {
                            totalWidth = totalWidth + borderWidth;
                        }
                        else
                        {
                            totalWidth = totalWidth + element.Width;
                        }
                    }
                    if (this.SpecifyWidth > totalWidth)
                    {
                        float w = this.SpecifyWidth - totalWidth;
                        DomFieldBorderElement eb = (DomFieldBorderElement)this.EndElement;
                        eb.Width = borderWidth + w;
                        return true;
                    }
                }
            }
            return false;
        }


        private bool _Readonly = false;
        /// <summary>
        /// 内容只读
        /// </summary>
        [System.ComponentModel.DefaultValue(false )]
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

        /// <summary>
        /// 内容是否可编辑
        /// </summary>
        [Browsable( false )]
        public override bool ContentEditable
        {
            get
            {
                return this.Readonly == false;
            }
        }

        private ElementType _AcceptChildElementTypes = ElementType.Text ;
        /// <summary>
        /// 能接收的子元素类型
        /// </summary>
        [DefaultValue( ElementType.Text )]
        [System.Xml.Serialization.XmlElement]
        public ElementType AcceptChildElementTypes2
        {
            get
            {
                return _AcceptChildElementTypes;
            }
            set
            {
                _AcceptChildElementTypes = value;
            }
        }

        /// <summary>
        /// 能接收的子元素类型
        /// </summary>
        [Browsable( false )]
        public override ElementType AcceptChildElementTypes
        {
            get
            {
                return _AcceptChildElementTypes;
            }
        }
         
        //internal string NextFieldVisibleExpression
        //{
        //    get
        //    {
        //        if (this.EventExpressions != null)
        //        {
        //            foreach (EventExpressionInfo item in this.EventExpressions)
        //            {
        //                if (item.EventName == WriterConst.EventName_ContentChanged
        //                    && item.Target == EventExpressionTarget.NextElement)
        //                {
        //                    this.NextFieldVisibleExpression = item.Expression;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    set
        //    {
        //        if (string.IsNullOrEmpty(this.NextFieldVisibleExpression) == false)
        //        {
        //            if (expresses == null)
        //            {
        //                expresses = new EventExpressionInfoList();
        //            }
        //            EventExpressionInfo info = null;
        //            foreach (EventExpressionInfo item in expresses)
        //            {
        //                if (item.EventName == WriterConst.EventName_ContentChanged
        //                    && item.Target == EventExpressionTarget.NextElement)
        //                {
        //                    info = item;
        //                    break;
        //                }
        //            }
        //            if (info == null)
        //            {
        //                info = new EventExpressionInfo();
        //                info.EventName = WriterConst.EventName_ContentChanged;
        //                info.Target = EventExpressionTarget.NextElement;
        //                expresses.Add(info);
        //            }
        //            info.Expression = this.NextFieldVisibleExpression;
        //        }
        //    }
        //}
        

        private bool _UserEditable = true;
        /// <summary>
        /// 用户可以直接修改文本域中的内容
        /// </summary>
        [System.ComponentModel.DefaultValue( true )]
        public bool UserEditable
        {
            get
            {
                return _UserEditable; 
            }
            set
            {
                _UserEditable = value; 
            }
        }

        private string _Name = null;
        /// <summary>
        /// 对象名称
        /// </summary>
        [System.ComponentModel.DefaultValue( null)]
        public string Name
        {
            get
            {
                return _Name; 
            }
            set
            {
                _Name = value; 
            }
        }

        private bool _AcceptTab = false;
        /// <summary>
        /// 能否接受制表符
        /// </summary>
        [DefaultValue( false )]
        public bool AcceptTab
        {
            get
            {
                return _AcceptTab; 
            }
            set
            {
                _AcceptTab = value; 
            }
        }

        private bool _MultiParagraph = false;
        /// <summary>
        /// 能否接受多个段落
        /// </summary>
        [DefaultValue( false )]
        public bool MultiParagraph
        {
            get
            {
                return _MultiParagraph; 
            }
            set
            {
                _MultiParagraph = value; 
            }
        }

        private ValueFormater _DisplayFormat = null;
        /// <summary>
        /// 显示的格式化对象
        /// </summary>
        [DefaultValue( null)]
        public ValueFormater DisplayFormat
        {
            get
            {
                return _DisplayFormat; 
            }
            set
            {
                _DisplayFormat = value; 
            }
        }

        private XDataBinding _ValueBinding = null;
        /// <summary>
        /// 内容绑定对象
        /// </summary>
        [DefaultValue( null)]
        public XDataBinding ValueBinding
        {
            get
            {
                return _ValueBinding; 
            }
            set
            {
                _ValueBinding = value; 
            }
        }

        private ValueValidateStyle _ValidateStyle = null;
        /// <summary>
        /// 数据验证样式
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        public ValueValidateStyle ValidateStyle
        {
            get
            {
                return _ValidateStyle; 
            }
            set
            {
                _ValidateStyle = value; 
            }
        }

        /// <summary>
        /// 文档加载后的处理
        /// </summary>
        /// <param name="format">文档存储格式</param>
        public override void AfterLoad(FileFormat format)
        {
            base.AfterLoad(format);
            if (this.ValueBinding != null && this.ValueBinding.AutoUpdate)
            {
                UpdateDataBinding( true );
            }
        }

        [NonSerialized]
        private int _DataBindingContentVersion = 0;
        /// <summary>
        /// 最后一次执行数据源绑定时的内容版本号
        /// </summary>
        [Browsable( false )]
        public int DataBindingContentVersion
        {
            get
            {
                return _DataBindingContentVersion; 
            }
        }

        /// <summary>
        /// 将数值转换为文本
        /// </summary>
        /// <param name="Value">数值</param>
        /// <returns>转换后的文本</returns>
        public virtual string GetDisplayText(string Value)
        {
            if (this.DisplayFormat != null && this.DisplayFormat.IsEmpty == false)
            {
                Value = this.DisplayFormat.Execute( Value );
            }
            return Value;
        }

        /// <summary>
        /// 更新数据源
        /// </summary>
        /// <param name="fastMode">快速模式</param>
        /// <returns>操作是否导致了文档内容发生改变</returns>
        public virtual bool UpdateDataBinding(bool fastMode )
        {
            if (this.ValueBinding != null )
            {
                DomDocument doc = this.OwnerDocument;
                object Value = doc.DataBindingProvider.DomReadValue(
                    this.OwnerDocument.AppHost ,
                    this.OwnerDocument ,
                    this,
                    this.ValueBinding,
                    false);
                if (Value == null || DBNull.Value.Equals(Value))
                {
                    this.InnerValue = "";
                    string txt = GetDisplayText("");
                    if (fastMode || this.Parent == null )
                    {
                        DomElementList elements = this.SetInnerTextFast(txt);
                        if (elements != null && elements.Count > 0)
                        {
                            // 删除元素上的授权信息
                            foreach (DomElement element in elements)
                            {
                                DocumentContentStyle style = (DocumentContentStyle)element.Style.Clone();
                                style.CreatorIndex = -1;
                                style.DeleterIndex = -1;
                                element.StyleIndex = this.OwnerDocument.ContentStyles.GetStyleIndex(style);
                            }
                        }
                    }
                    else
                    {
                        this.SetEditorTextExt(txt, DomAccessFlags.None , true );
                    }
                }
                else
                {
                    this.InnerValue = Convert.ToString( Value );
                    string txt = GetDisplayText( Convert.ToString(Value) );
                    if (fastMode || this.Parent == null )
                    {
                        DomElementList elements = this.SetInnerTextFast(txt);
                        if (elements != null && elements.Count > 0)
                        {
                            // 删除元素上的授权信息
                            foreach (DomElement element in elements)
                            {
                                DocumentContentStyle style = ( DocumentContentStyle ) element.Style.Clone();
                                style.CreatorIndex = -1;
                                style.DeleterIndex = -1;
                                element.StyleIndex = this.OwnerDocument.ContentStyles.GetStyleIndex(style);
                            }
                        }
                    }
                    else
                    {
                        this.SetEditorTextExt(txt, DomAccessFlags.None, true);
                    }
                }
                this._DataBindingContentVersion = this.ContentVersion;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 保持字段域数据到数据源中
        /// </summary>
        /// <returns>操作是否成功</returns>
        public virtual bool WriteDataSource()
        {
            if (this.ValueBinding != null && this.ValueBinding.Readonly == false )
            {
                if (this.ValidateStyle != null && this.ValidateStyle.IsEmpty == false)
                {
                    CancelEventArgs args = new CancelEventArgs();
                    ValueValidateResult result = this.OnValidating(args, false);
                    if (args.Cancel)
                    {
                        return false;
                    }
                    if (result != null)
                    {
                        // 数据校验不通过，无法填充数据源
                        return false;
                    }
                }
                DomDocument document = this.OwnerDocument;
                string v = this.InnerValue;
                if (string.IsNullOrEmpty(v))
                {
                    v = this.Text;
                }
                return document.DataBindingProvider.DomWriteValue(
                    this.OwnerDocument.AppHost ,
                    this.OwnerDocument ,
                    this,
                    this.ValueBinding,
                    v ,
                    false);
            }
            else
            {
                return false;
            }
        }


        private string _InnerValue = null;
        /// <summary>
        /// 内置的数值
        /// </summary>
        [DefaultValue(null)]
        public string InnerValue
        {
            get
            {
                return _InnerValue;
            }
            set
            {
                _InnerValue = value;
            }
        }

        private string _ToolTip = null;
        /// <summary>
        /// 提示文本
        /// </summary>
        [DefaultValue( null)]
        public string ToolTip
        {
            get
            {
                return _ToolTip; 
            }
            set
            {
                _ToolTip = value; 
            }
        }


        private string _BackgroundText = null;
        /// <summary>
        /// 背景文本
        /// </summary>
        [DefaultValue( null)]
        public string BackgroundText
        {
            get
            {
                return _BackgroundText; 
            }
            set
            {
                _BackgroundText = value;
                _backgroundTextElements = null;
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
                bool hasVisibleElement = false;
                if (this.Elements != null && this.Elements.Count > 0)
                {
                    foreach (DomElement element in this.Elements)
                    {
                        if (element.Visible)
                        {
                            hasVisibleElement = true;
                            break;
                        }
                    }
                }
                if (hasVisibleElement)
                {
                    // 添加可见的元素
                    return base.AppendContent(content, privateMode);
                }
                else
                {
                    int result = 0;
                    if (this.StartElement != null)
                    {
                        content.Add(this.StartElement);
                        result++;
                    }
                    if (string.IsNullOrEmpty(this.BackgroundText) == false)
                    {
                        if (_backgroundTextElements == null)
                        {
                            //DocumentContentStyle style = (DocumentContentStyle)this.Style.Clone();
                            //style.Color = this.OwnerDocument.Options.ViewOptions.BackgroundTextColor;
                            _backgroundTextElements = this.OwnerDocument.CreateTextElements(
                                this.BackgroundText, null, this.StartElement.Style);
                        }
                        foreach (DomElement e in _backgroundTextElements)
                        {
                            e.OwnerDocument = this.OwnerDocument;
                            e.Parent = this;
                            e.StyleIndex = this.StyleIndex;
                        }
                        content.AddRange(_backgroundTextElements);
                        result += _backgroundTextElements.Count;
                    }
                    if (this.EndElement != null)
                    {
                        content.Add(this.EndElement);
                        result++;
                    }
                    return result;
                }
            }
        }

         
        [NonSerialized]
        private ValueValidateResult _LastValidateResult = null ;
        /// <summary>
        /// 处理字段内容发生改变事件
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        public override void OnContentChanged( ContentChangedEventArgs args)
        {
            bool raiseValidate = false;
            DocumentValueValidateMode validateMode = this.OwnerDocument.Options.EditOptions.ValueValidateMode;
            if (validateMode == DocumentValueValidateMode.Dynamic)
            {
                // 实时的进行数据校验
                raiseValidate = true;
            }
            else if( args.UndoRedoCause )
            {
                // 由于执行重复/撤销操作而引起的该事件
                if (validateMode == DocumentValueValidateMode.LostFocus)
                {
                    raiseValidate = true;
                }
            }
            if (raiseValidate)
            {
                // 执行数据校验
                CancelEventArgs args2 = new CancelEventArgs();
                args2.Cancel = false;
                this.OnValidating(args2, args.LoadingDocument);
                if (args2.Cancel == false)
                {
                    this.OnValidated();
                }
            }
            UpdateToolTip();
          
            base.OnContentChanged( args);
        }

        /// <summary>
        /// 处理获得输入焦点事件
        /// </summary>
        /// <param name="args">事件参数</param>
        public override void OnGotFocus(EventArgs args)
        {
            if (this.OwnerDocument.Options.ViewOptions.FieldFocusedBackColor.A != 0)
            {
                // 在失去输入焦点时进行数据校验
                this.OwnerDocument.HighlightManager.InvalidateHighlightInfo(this);
                this.InvalidateView();
            }
            base.OnGotFocus(args);
        }

        /// <summary>
        /// 处理失去输入焦点事件
        /// </summary>
        /// <param name="args">事件参数</param>
        public override void OnLostFocus(EventArgs args)
        {
            if (this.OwnerDocument.Options.ViewOptions.FieldFocusedBackColor.A != 0)
            {
                this.OwnerDocument.HighlightManager.InvalidateHighlightInfo(this);
                this.InvalidateView();
            }
            if (this.OwnerDocument.Options.EditOptions.ValueValidateMode 
                == DocumentValueValidateMode.LostFocus)
            {
                CancelEventArgs args2 = new CancelEventArgs();
                args2.Cancel = false;
                this.OnValidating(args2, false);
                if (args2.Cancel == false)
                {
                    this.OnValidated();
                }
            }
            base.OnLostFocus(args);
        }

        /// <summary>
        /// 处理鼠标进入事件
        /// </summary>
        /// <param name="args">事件参数</param>
        public override void OnMouseEnter(EventArgs args)
        {
            if (this.OwnerDocument.Options.ViewOptions.FieldHoverBackColor.A != 0)
            {
                this.OwnerDocument.HighlightManager.InvalidateHighlightInfo(this);
                this.InvalidateView();
            }
            UpdateToolTip();
            //if (this.OwnerDocument.EditorControl != null)
            //{
            //    this.OwnerDocument.EditorControl.UpdateToolTip(false);
            //}
            base.OnMouseEnter(args);
        }

        /// <summary>
        /// 处理鼠标离开事件
        /// </summary>
        /// <param name="args">事件参数</param>
        public override void OnMouseLeave(EventArgs args)
        {
            if (this.OwnerDocument.Options.ViewOptions.FieldHoverBackColor.A != 0)
            {
                this.OwnerDocument.HighlightManager.InvalidateHighlightInfo( this );
                this.InvalidateView();
            }
            base.OnMouseLeave(args);
        }

        private EnableState _EnableHighlight = EnableState.Enabled;
        /// <summary>
        /// 是否允许高亮度显示状态
        /// </summary>
        [DefaultValue( EnableState.Enabled )]
        public EnableState EnableHighlight
        {
            get
            {
                return _EnableHighlight; 
            }
            set
            {
                _EnableHighlight = value; 
            }
        }

        /// <summary>
        /// 获得高亮度显示区域列表
        /// </summary>
        /// <returns></returns>
        public override HighlightInfoList GetHighlightInfos()
        {
            if (this.EnableHighlight == EnableState.Disabled)
            {
                return null;
            }
            System.Drawing.Color c = System.Drawing.Color.Transparent;
            if (_LastValidateResult != null )
            {
                c = this.OwnerDocument.Options.ViewOptions.FieldInvalidateValueBackColor;
            }
            else if( this.EnableHighlight == EnableState.Enabled )
            {
                if (this.Focused
                    && this.OwnerDocument.Options.ViewOptions.FieldFocusedBackColor.A != 0)
                {
                    c = this.OwnerDocument.Options.ViewOptions.FieldFocusedBackColor;
                }
                else if (this.OwnerDocument.IsHover(this) 
                    && this.OwnerDocument.Options.ViewOptions.FieldHoverBackColor.A != 0)
                {
                    c = this.OwnerDocument.Options.ViewOptions.FieldHoverBackColor;
                }
                else if (this.OwnerDocument.Options.ViewOptions.FieldBackColor.A != 0)
                {
                    //bool find = false;
                    DomElement parent = this.Parent;
                    while (parent != null)
                    {
                        if (parent is DomInputFieldElementBase)
                        {
                            //find = true;
                            return null ;// 使用父文本输入域的高亮度显示区域
                            //break;
                        }
                        parent = parent.Parent;
                    }
                    c = this.OwnerDocument.Options.ViewOptions.FieldBackColor;
                }
            }
            if (c.A != 0)
            {
                HighlightInfo info = new HighlightInfo(
                    new DomRange(
                        this.DocumentContentElement,
                        this.FirstContentElement,
                        this.LastContentElement),
                   c,
                   System.Drawing.Color.Empty);
                info.ActiveStyle = HighlightActiveStyle.Static;
                info.OwnerElement = this;
                HighlightInfoList list = new HighlightInfoList();
                list.Add(info);
                return list;
            }
            return null;
        }

        /// <summary>
        /// 触发数据验证结束事件
        /// </summary>
        public virtual void OnValidated()
        {
            if (this.Events != null && this.Events.HasValidated)
            {
                this.Events.RaiseValidated(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 更新提示文本
        /// </summary>
        protected void UpdateToolTip( )
        {
            if (this.OwnerDocument != null && this.OwnerDocument.EditorControl != null)
            {
                // 添加元素提示文本
                if (_LastValidateResult == null )
                {
                    // 设置正常的提示文本
                    if (this.OwnerDocument.EditorControl != null)
                    {
                        if (string.IsNullOrEmpty(this.ToolTip))
                        {
                            this.OwnerDocument.EditorControl.ToolTips.Remove(this);
                        }
                        else
                        {
                            this.OwnerDocument.EditorControl.ToolTips.Add(
                                this,
                                this.ToolTip,
                                ToolTipStyle.ToolTip,
                                ToolTipLevel.Normal);
                        }
                    }
                }
                else
                {
                    // 显示错误提示文本
                    this.OwnerDocument.EditorControl.ToolTips.Add(
                        this,
                        _ValidateStyle.Message ,
                        ToolTipStyle.ToolTip,
                        ToolTipLevel.Warring);
                }
            }
        }

        /// <summary>
        /// 进行数据验证
        /// </summary>
        public virtual ValueValidateResult OnValidating(CancelEventArgs args, bool loadingDocument)
        {
            if (this.OwnerDocument.Options.EditOptions.ValueValidateMode
                == DocumentValueValidateMode.None)
            {
                // 禁止数据校验
                return null ;
            }
            if (this.Events != null && this.Events.HasValidating)
            {
                // 触发用户定义的数据校验事件
                this.Events.RaiseValidating(this, args);
                if (args.Cancel)
                {
                    return null ;
                }
            }
            if (this.ValidateStyle != null
                && this.ValidateStyle.IsEmpty == false)
            {
                if (this.ValidateStyle.ContentVersion == this.ContentVersion)
                {
                    // 此处使用ContentVersion进行判断用来减少判断次数.
                    args.Cancel = _LastValidateResult != null;
                    return _LastValidateResult;
                }
                this.ValidateStyle.ContentVersion = this.ContentVersion;
                args.Cancel = true;
                this.ValidateStyle.Value = this.Text ;
                bool result = this.ValidateStyle.Validate();

                if (this.OwnerDocument.EditorControl != null)
                {
                    // 添加元素提示文本
                    UpdateToolTip();
                    if (loadingDocument == false)
                    {
                        this.OwnerDocument.EditorControl.UpdateToolTip(false);
                    }
                }
                if (result)
                {
                    _LastValidateResult = null;
                }
                else
                {
                    _LastValidateResult = new ValueValidateResult();
                    _LastValidateResult.Element = this;
                    _LastValidateResult.Message = this.ValidateStyle.Message;
                }
                if (loadingDocument == false)
                {
                    this.InvalidateView();
                }
            }
            else
            {
                _LastValidateResult = null;
            }
            if (this.OwnerDocument.Options.BehaviorOptions.DebugMode && _LastValidateResult != null )
            {
                if (string.IsNullOrEmpty(_LastValidateResult.Message) == false)
                {
                    string msg = string.Format(
                            WriterStrings.ValueInvalidate_Source_Value_Result,
                            this.ToDebugString(),
                            this.Text,
                            _LastValidateResult.Message);
                    System.Diagnostics.Debug.WriteLine( msg );
                }
            }
            return _LastValidateResult;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="Deeply">是否深度复制</param>
        /// <returns>复制品</returns>
        public override DomElement Clone(bool Deeply)
        {
            DomInputFieldElementBase field = ( DomInputFieldElementBase )  base.Clone(Deeply);
            if (this._ValidateStyle != null)
            {
                field._ValidateStyle = this._ValidateStyle.Clone();
            }
            if (this._ValueBinding != null)
            {
                field._ValueBinding = this._ValueBinding.Clone();
            }
            if (this._DisplayFormat != null)
            {
                field._DisplayFormat = this._DisplayFormat.Clone();
            }
            
            field._LastValidateResult = null;
            return field; 
        }


        public override string ToDebugString()
        {
            if (string.IsNullOrEmpty(this.ID) == false)
            {
                return "Field[" + this.ID + "]";
            }
            if (string.IsNullOrEmpty(this.Name) == false)
            {
                return "Field[" + this.Name + "]";
            }
            return "Field";
        }
    }
}