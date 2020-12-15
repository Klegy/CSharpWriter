using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel ;
using System.Xml.Serialization ;
using System.Drawing ;
using DCSoft.Drawing;
using DCSoft.RTF;
using DCSoft.CSharpWriter.RTF;
using DCSoft.Common;
using DCSoft.CSharpWriter.Data;
 

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 复选框控件基础对象类型
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("CheckBox:Group={GroupName} , Checked={Checked}")]
    [System.Drawing.ToolboxBitmap(typeof(DomCheckBoxElementBase))]
    public class DomCheckBoxElementBase : DomObjectElement 
    {
        internal static Bitmap ImageCheckBoxChecked = null;
        internal static Bitmap ImageCheckBoxUnChecked = null;
        internal static Bitmap ImageRadioChecked = null;
        internal static Bitmap ImageRadioUnChecked = null;

        static DomCheckBoxElementBase()
        {
            ImageCheckBoxChecked = WriterResources.CheckBoxChecked;
            ImageCheckBoxChecked.MakeTransparent(Color.White);

            ImageCheckBoxUnChecked = WriterResources.CheckBoxUnChecked;
            ImageCheckBoxUnChecked.MakeTransparent(Color.White);

            ImageRadioChecked = WriterResources.RadioChecked;
            ImageRadioChecked.MakeTransparent(Color.White);

            ImageRadioUnChecked = WriterResources.RadioUnChecked;
            ImageRadioUnChecked.MakeTransparent(Color.White);
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomCheckBoxElementBase()
        {
        }

        private string _ToolTip = null;
        /// <summary>
        /// 提示文本
        /// </summary>
        [DefaultValue(null)]
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

        private XAttributeList _Attributes = new XAttributeList();
        /// <summary>
        /// 用户自定义属性列表
        /// </summary>
        [DefaultValue(null)]
        [XmlArrayItem("Attribute", typeof(DomAttribute))]
        public XAttributeList Attributes
        {
            get
            {
                return _Attributes;
            }
            set
            {
                _Attributes = value;
            }
        }

        private XDataBinding _ValueBinding = null;
        /// <summary>
        /// 内容绑定对象
        /// </summary>
        [DefaultValue(null)]
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

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="fastMode">是否是快速模式</param>
        /// <returns>操作是否修改了文档内容</returns>
        public virtual bool UpdateDataBinding( bool fastMode )
        {
            if (this.ValueBinding != null)
            {
                DomDocument doc = this.OwnerDocument;
                XDataBindingProvider bp = doc.DataBindingProvider;
                object Value = bp.DomReadValue(
                    this.OwnerDocument.AppHost , 
                    this.OwnerDocument ,
                    this,
                    this.ValueBinding,
                    false);
                bool newValue = false;
                if (Value == null || DBNull.Value.Equals(Value))
                {
                    newValue = false;
                }
                else
                {
                    bool v = false;
                    bool.TryParse(Convert.ToString(Value), out v);
                    newValue = v;
                }
                if (this.Checked != newValue)
                {
                    this.Checked = newValue;
                    if (fastMode == false)
                    {
                        this.InvalidateView();
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 保持字段域数据到数据源中
        /// </summary>
        /// <returns>操作是否成功</returns>
        public virtual bool WriteDataSource()
        {
            if (this.ValueBinding != null)
            {
                DomDocument document = this.OwnerDocument;
                XDataBindingProvider bp = document.DataBindingProvider;
                return bp.DomWriteValue(
                    this.OwnerDocument.AppHost ,
                    this.OwnerDocument ,
                    this, 
                    this.ValueBinding,
                    this.Text,
                    false);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 显示在用户界面上的图片
        /// </summary>
        internal Bitmap DisplayImage
        {
            get
            {
                if (this.ControlStyle == CheckBoxControlStyle.CheckBox)
                {
                    if (this.Checked)
                    {
                        return ImageCheckBoxChecked;
                    }
                    else
                    {
                        return ImageCheckBoxUnChecked;
                    }
                }
                else
                {
                    if (this.Checked)
                    {
                        return ImageRadioChecked;
                    }
                    else
                    {
                        return ImageRadioUnChecked;
                    }
                }
            }
        }

        /// <summary>
        /// 对象大小不能修改
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public override bool CanResize
        {
            get
            {
                return false;
            }
            set
            {

            }
        }

        private bool _Deleteable = false;
        /// <summary>
        /// 对象能否被删除
        /// </summary>
        [DefaultValue( false )]
        public bool Deleteable
        {
            get
            {
                return _Deleteable; 
            }
            set
            {
                _Deleteable = value; 
            }
        }

        private string _GroupName = null;
        /// <summary>
        /// 分组名称
        /// </summary>
        [DefaultValue( null )]
        public string GroupName
        {
            get
            {
                return _GroupName; 
            }
            set
            {
                _GroupName = value; 
            }
        }

        /// <summary>
        /// 获得文档中所有同组的复选框对象列表
        /// </summary>
        /// <returns></returns>
        public DomElementList GetElementsInSameGroup()
        {
            DomElementList result = new DomElementList();
            this.DocumentContentElement.Enumerate(delegate(object sender, ElementEnumerateEventArgs args)
                {
                    if (args.Element is DomCheckBoxElementBase)
                    {
                        DomCheckBoxElementBase chk = (DomCheckBoxElementBase)args.Element;
                        if (this.ControlStyle == chk.ControlStyle)
                        {
                            if (string.IsNullOrEmpty(this.GroupName)
                                && string.IsNullOrEmpty(chk.GroupName))
                            {
                                result.Add(chk);
                            }
                            else
                            {
                                if (this.GroupName == chk.GroupName )
                                {
                                    result.Add(chk);
                                }
                            }
                        }
                    }
                }, false);
            return result;
        }

        private CheckBoxControlStyle _ControlStyle = CheckBoxControlStyle.CheckBox;
        /// <summary>
        /// 控件样式
        /// </summary>
        [DefaultValue( CheckBoxControlStyle.CheckBox )]
        public CheckBoxControlStyle ControlStyle
        {
            get
            {
                return _ControlStyle; 
            }
            set
            {
                _ControlStyle = value; 
            }
        }

        private bool _Checked = false;
        /// <summary>
        /// 选中状态
        /// </summary>
        [DefaultValue( false )]
        public bool Checked
        {
            get
            {
                return _Checked; 
            }
            set
            {
                _Checked = value; 
            }
        }

        private string _CheckedValue = null;
        /// <summary>
        /// 勾选状态的值
        /// </summary>
        [DefaultValue( null )]
        public string CheckedValue
        {
            get
            {
                return _CheckedValue; 
            }
            set
            {
                _CheckedValue = value; 
            }
        }

        private string _UnCheckedValue = null;
        /// <summary>
        /// 没有勾选状态的值
        /// </summary>
        [DefaultValue( null )]
        public string UnCheckedValue
        {
            get 
            {
                return _UnCheckedValue; 
            }
            set
            {
                _UnCheckedValue = value; 
            }
        }

        //private string _Value = null;
        /// <summary>
        /// 对象数值
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public string Value
        {
            get
            {
                if (this.Checked)
                {
                    return this.CheckedValue;
                }
                else
                {
                    return this.UnCheckedValue;
                }
            }
            set
            {
                if (value == this.CheckedValue)
                {
                    this.Checked = true;
                }
                else
                {
                    this.Checked = false;
                }
            }
        }
         

        /// <summary>
        /// 处理文档事件
        /// </summary>
        /// <param name="args"></param>
        public override void HandleDocumentEvent(DocumentEventArgs args)
        {
            if (args.Style == DocumentEventStyles.MouseDown)
            {
                if (this.Enabled)
                {
                    if (this.OwnerDocument.DocumentControler.CanModify(
                        this,
                        DomAccessFlags.CheckUserEditable))
                    {
                        // 鼠标点击设置勾选状态。
                        this.DocumentContentElement.Content.CurrentElement = this;
                        this.EditorChecked = !this.Checked;
                        args.CancelBubble = true;
                        args.Cursor = System.Windows.Forms.Cursors.Arrow;
                    }
                }
            }
            else if (args.Style == DocumentEventStyles.MouseMove)
            {
                if (this.OwnerDocument.EditorControl != null)
                {
                    // 设置提示信息
                    if (string.IsNullOrEmpty(this.ToolTip))
                    {
                        this.OwnerDocument.EditorControl.ToolTips.Remove(this);
                    }
                    else
                    {
                        this.OwnerDocument.EditorControl.ToolTips.Add(this, this.ToolTip);
                    }
                    args.Cursor = System.Windows.Forms.Cursors.Arrow;
                }
            }
            else
            {
                base.HandleDocumentEvent(args);
            }
        }

        public override void OnMouseEnter(EventArgs args)
        {
            if (string.IsNullOrEmpty(this.GroupName) == false)
            {
                InvalidateHighlightInfo();
            }
            else
            {
                this.OwnerDocument.HighlightManager.Remove(this);
            }
            base.OnMouseEnter(args);
        }

        public override void OnMouseLeave(EventArgs args)
        {
            if (string.IsNullOrEmpty(this.GroupName) == false)
            {
                InvalidateHighlightInfo();
            }
            else
            {
                this.OwnerDocument.HighlightManager.Remove(this);
            }
            base.OnMouseLeave(args);
        }

        public override void OnGotFocus(EventArgs args)
        {
            InvalidateHighlightInfo();
            base.OnGotFocus(args);
        }

        public override void OnLostFocus(EventArgs args)
        {
            InvalidateHighlightInfo();
            base.OnLostFocus(args);
        }

        /// <summary>
        /// 声明同组的所有的复选框元素高亮度显示信息无效
        /// </summary>
        public override void InvalidateHighlightInfo()
        {
            if (string.IsNullOrEmpty(this.GroupName) == false)
            {
                if (this.OwnerDocument.Options.ViewOptions.FieldFocusedBackColor.A != 0)
                {
                    DomElementList chks = GetElementsInSameGroup();
                    foreach (DomElement element in chks)
                    {
                        this.OwnerDocument.HighlightManager.InvalidateHighlightInfo(element);
                        element.InvalidateView();
                    }
                }
            }
        }

        /// <summary>
        /// 编辑器中设置或获得选择状态
        /// </summary>
        [Browsable( false )]
        [XmlIgnore]
        public virtual bool EditorChecked
        {
            get
            {
                return this.Checked ;
            }
            set
            {
                if (this.Checked != value)
                {
                    if (this.OwnerDocument.DocumentControler.CanModify(this))
                    {
                        DomElementList chks = new DomElementList();
                        if ( value == true && this.ControlStyle == CheckBoxControlStyle.RadioBox)
                        {
                            chks = this.GetElementsInSameGroup();
                            if (value == true)
                            {
                                for (int iCount = chks.Count - 1; iCount >= 0; iCount--)
                                {
                                    DomCheckBoxElementBase chk = (DomCheckBoxElementBase)chks[iCount];
                                    if (this.OwnerDocument.DocumentControler.CanModify(chk) == false)
                                    {
                                        // 元素只读，不能修改。
                                        chks.RemoveAt(iCount);
                                    }
                                    else if (chk != this && chk.Checked == false )
                                    {
                                        // 不处理不处于勾选状态的单选框元素
                                        chks.RemoveAt(iCount);
                                    }
                                }
                            }
                        }
                        else
                        {
                            chks.Add(this);
                        }

                        // 触发元素值改变前事件
                        ContentChangingEventArgs args2 = new ContentChangingEventArgs();
                        args2.Document = this.OwnerDocument;
                        args2.Tag = this;
                        args2.Element = this;
                        foreach (DomCheckBoxElementBase chk in chks)
                        {
                            chk.OnContentChanging(this, args2);
                            if (args2.Cancel)
                            {
                                // 取消操作
                                return;
                            }
                        }
                        if (this.OwnerDocument.BeginLogUndo())
                        {
                            foreach (DomCheckBoxElementBase chk in chks)
                            {
                                this.OwnerDocument.UndoList.AddProperty(
                                    "InnerEditorChecked",
                                    chk.Checked,
                                    !chk.Checked,
                                    chk);
                            }
                            this.OwnerDocument.EndLogUndo();
                        }
                        foreach (DomCheckBoxElementBase chk in chks)
                        {
                            chk.Checked = !chk.Checked;
                            ContentChangedEventArgs args3 = new ContentChangedEventArgs();
                            args3.Document = this.OwnerDocument;
                            args3.Element = this;
                            args3.Tag = this;
                            chk.OnContentChanged(chk, args3);
                            chk.InvalidateView();
                        }
                        this.OwnerDocument.Modified = true;
                        this.OwnerDocument.OnDocumentContentChanged();
                    }
                }
            }
        }

        /// <summary>
        /// 编辑器中设置或获得选择状态，该属性内部使用，而且不会记录撤销操作信息
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public bool InnerEditorChecked
        {
            //get
            //{
            //    return this.Checked;
            //}
            set
            {
                if (this.Checked != value)
                {
                    if (this.OwnerDocument.DocumentControler.CanModify(this))
                    {
                        ContentChangingEventArgs args2 = new ContentChangingEventArgs();
                        args2.Document = this.OwnerDocument;
                        args2.Tag = this;
                        OnContentChanging(this, args2);
                        if (args2.Cancel)
                        {
                            return;
                        }
                        this.Checked = !this.Checked;
                        ContentChangedEventArgs args3 = new ContentChangedEventArgs();
                        args3.Document = this.OwnerDocument;
                        args3.Tag = this;
                        OnContentChanged(this, args3);
                        this.InvalidateView();
                    }
                }
            }
        }

        /// <summary>
        /// 获得高亮度显示区域
        /// </summary>
        /// <returns></returns>
        public override HighlightInfoList GetHighlightInfos()
        {
            DomElementList list = GetElementsInSameGroup();
            Color c = Color.Transparent;
            if (this.Focused)
            {
                c = this.OwnerDocument.Options.ViewOptions.FieldFocusedBackColor;
            }
            else if (this.OwnerDocument.IsHover(this))
            {
                c = this.OwnerDocument.Options.ViewOptions.FieldHoverBackColor;
            }
            if (c.A != 0)
            {
                HighlightInfoList result = new HighlightInfoList();
                foreach (DomElement element in list)
                {
                    HighlightInfo info = new HighlightInfo();
                    info.Range = new DomRange(element.DocumentContentElement, element.ViewIndex, 1);
                    info.BackColor = c;
                    info.OwnerElement = element;
                    result.Add(info);
                }//foreach
                return result;
            }
            else
            {
                return null;
            }
        }

        #region 事件处理 ************************************************

        //private EventExpressionInfoList _EventExpressions = null;
        ///// <summary>
        ///// 事件表达式列表
        ///// </summary>
        //[DefaultValue(null)]
        //[System.Xml.Serialization.XmlArrayItem("Expression", typeof(EventExpressionInfo))]
        //public EventExpressionInfoList EventExpressions
        //{
        //    get
        //    {
        //        return _EventExpressions;
        //    }
        //    set
        //    {
        //        _EventExpressions = value;
        //    }
        //}

        /// <summary>
        /// 触发内容正在改变事件
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        public virtual void OnContentChanging(object sender, ContentChangingEventArgs args)
        {
            if (this.Events != null && this.Events.HasContentChanging)
            {
                this.Events.RaiseContentChanging(sender, args);
            }
            //if (args.Cancel == false && this.OwnerDocument != null)
            //{
            //    // 触发文档全局事件
            //    this.OwnerDocument.OnGlobalContentChanging(sender, args);
            //}
        }

        /// <summary>
        /// 触发内容已经改变事件
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        public virtual void OnContentChanged(object sender, ContentChangedEventArgs args)
        {
            if (this.Events != null && this.Events.HasContentChanged)
            {
                this.Events.RaiseContentChanged(sender, args);
            }
             
        }

        #endregion

        public override void WriteRTF(RTFContentWriter writer)
        {
            //AttributeStringList attributes = new AttributeStringList();
            //attributes.SetValue("Type", this.GetType().FullName);
            //attributes.SetValue("ControlStyle", this.ControlStyle.ToString());
            //attributes.SetValue("Checked", this.Checked.ToString());
            //attributes.SetValue("Value", this.Value);
            //attributes.SetValue("Name", this.Name);
            writer.WriteEmbObject(this, this.DisplayImage);
        }

        /// <summary>
        /// 返回表示对象数据的文本
        /// </summary>
        public override string Text
        {
            get
            {
                if (this.ControlStyle == CheckBoxControlStyle.CheckBox)
                {
                    if (this.Checked)
                    {
                        return "■";
                    }
                    else
                    {
                        return "□";
                    }
                }
                else
                {
                    if (this.Checked)
                    {
                        return "●";
                    }
                    else
                    {
                        return "○";
                    }
                }
            }
            set
            {
            }
        }

        /// <summary>
        /// 返回表示对象的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            if (this.ControlStyle == CheckBoxControlStyle.CheckBox)
            {
                return "CheckBox:" + this.Checked;
            }
            else
            {
                return "Radio:" + this.Checked;
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="Deeply">是否深入复制，无效</param>
        /// <returns>复制的对象</returns>
        public override DomElement Clone(bool Deeply)
        {
            DomCheckBoxElementBase chk = ( DomCheckBoxElementBase ) base.Clone(Deeply);
            //if (this._EventExpressions != null)
            //{
            //    chk._EventExpressions = this._EventExpressions.Clone();
            //}
            if (this._ValueBinding != null)
            {
                chk._ValueBinding = this._ValueBinding.Clone();
            }
            if (this._Attributes != null)
            {
                chk._Attributes = this._Attributes.Clone();
            }
            return chk;
        }
    }

    /// <summary>
    /// 复选框控件样式
    /// </summary>
    public enum CheckBoxControlStyle
    {
        /// <summary>
        /// 复选框控件
        /// </summary>
        CheckBox ,
        /// <summary>
        /// 单选框控件
        /// </summary>
        RadioBox
    }
}
