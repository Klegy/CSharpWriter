using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.Common;
using DCSoft.CSharpWriter.Dom;
using DCSoft.CSharpWriter.Data;
using DCSoft.Data;
using System.ComponentModel;


namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 文本输入域设置信息对象
    /// </summary>
    [TypeConverter( typeof( XTextInputFieldElementPropertiesTypeConverter ))]
    public class XTextInputFieldElementProperties : XTextElementProperties
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XTextInputFieldElementProperties()
        { 
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="field">要读取设置的文本输入域对象</param>
        public XTextInputFieldElementProperties(DomInputFieldElement field)
        {
            if (field != null)
            {
                ReadProperties(field);
            }
        }

        public override bool ReadProperties(DomElement element)
        {
            DomInputFieldElement field = (DomInputFieldElement)element;
            _ID = field.ID;
            _Name = field.Name;
            _Readonly = field.Readonly;
            _UserEditable = field.UserEditable;
            _ValidateStyle = field.ValidateStyle;
            _ValueBinding = field.ValueBinding;
            _FieldSettings = field.FieldSettings;
            _Attributes = field.Attributes;
            _DisplayFormat = field.DisplayFormat;
            _ToolTip = field.ToolTip;
            _BackgroundText = field.BackgroundText;
            _SpecifyWidth = field.SpecifyWidth;
            this.AcceptChildElementTypes2 = field.AcceptChildElementTypes2;
             
            this.EnableHighlight = field.EnableHighlight;
            this.MultiParagraph = field.MultiParagraph;
            
            return true;
        }

        private string _ID = null;
        /// <summary>
        /// 编号
        /// </summary>
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private float _SpecifyWidth = 0f;
        /// <summary>
        /// 输入域指定宽度,若大于0则输入域宽度不小于该值，而且当内容很多时，自动变宽。
        /// </summary>
        [DefaultValue(0f)]
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


        private bool _MultiParagraph = false;
        /// <summary>
        /// 能否接受多个段落
        /// </summary>
        [DefaultValue(false)]
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

        private EnableState _EnableHighlight = EnableState.Enabled;
        /// <summary>
        /// 是否允许高亮度显示状态
        /// </summary>
        [DefaultValue(EnableState.Enabled)]
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

        private string _NextFieldVisibleExpression = null;
        /// <summary>
        /// 控制下一个文本域可见性的数值
        /// </summary>
        /// <remarks>
        /// 若文本输入域的文本值等于本属性值,则设置下一个文本域的可见,否则不可见.
        /// 以此可以设置简单的级联输入域.
        /// </remarks>
        [DefaultValue(null)]
        public string NextFieldVisibleExpression
        {
            get
            {
                return _NextFieldVisibleExpression;
            }
            set
            {
                _NextFieldVisibleExpression = value;
            }
        }

        private ElementType _AcceptChildElementTypes = ElementType.All;
        /// <summary>
        /// 能接收的子元素类型
        /// </summary>
        [DefaultValue(ElementType.All)]
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


        private string _Name = null;
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private bool _Readonly = false;

        public bool Readonly
        {
            get { return _Readonly; }
            set { _Readonly = value; }
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

        private string _BackgroundText = null;
        /// <summary>
        /// 背景文本
        /// </summary>
        [DefaultValue(null)]
        public string BackgroundText
        {
            get
            {
                return _BackgroundText;
            }
            set
            {
                _BackgroundText = value;
            }
        }

        private bool _UserEditable = true;
        /// <summary>
        /// 用户可以直接修改文本域中的内容
        /// </summary>
        [System.ComponentModel.DefaultValue(true)]
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

        private ValueFormater _DisplayFormat = null;
        /// <summary>
        /// 显示的格式化对象
        /// </summary>
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


        private XAttributeList _Attributes = null;
        /// <summary>
        /// 自定义属性列表
        /// </summary>
        public XAttributeList Attributes
        {
            get { return _Attributes; }
            set { _Attributes = value; }
        }

        private string _Text = null;
        /// <summary>
        /// 文本值 
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        private string _InitalizeInnerValue = null;
        /// <summary>
        /// 初始化的表单值，本属性只能用于新增元素操作
        /// </summary>
        public string InitalizeInnerValue
        {
            get
            {
                return _InitalizeInnerValue; 
            }
            set
            {
                _InitalizeInnerValue = value; 
            }
        }
        

        private string _InitalizeText = null;
        /// <summary>
        /// 初始化的文本值，本属性只能用于新增元素操作
        /// </summary>
        public string InitalizeText
        {
            get 
            {
                return _InitalizeText; 
            }
            set
            {
                _InitalizeText = value; 
            }
        }

        //private string _Text = null;
        ///// <summary>
        ///// 文本值
        ///// </summary>
        //public string Text
        //{
        //    get 
        //    {
        //        return _Text; 
        //    }
        //    set
        //    {
        //        _Text = value; 
        //    }
        //}

        private InputFieldSettings _FieldSettings = null;
        /// <summary>
        /// 输入域设置
        /// </summary>
        public InputFieldSettings FieldSettings
        {
            get
            {
                return _FieldSettings;
            }
            set
            {
                _FieldSettings = value;
            }
        }

        private XDataBinding _ValueBinding = null;
        /// <summary>
        /// 数据源绑定
        /// </summary>
        public XDataBinding ValueBinding
        {
            get { return _ValueBinding; }
            set { _ValueBinding = value; }
        }

        private ValueValidateStyle _ValidateStyle = null;
        /// <summary>
        /// 数据校验格式
        /// </summary>
        public ValueValidateStyle ValidateStyle
        {
            get { return _ValidateStyle; }
            set { _ValidateStyle = value; }
        }

        internal bool _ValidateStyleModified = true;

        public override bool PromptNewElement(WriterCommandEventArgs args)
        {
            //using (dlgInputFieldProperties dlg = new dlgInputFieldProperties())
            //{
                
            //    dlg.ElementProperties = this;
            //    if (dlg.ShowDialog( args == null ? null : args.EditorControl) 
            //        == System.Windows.Forms.DialogResult.OK)
            //    {
            //        return true;
            //    }
            //}
            return false;
        }

        public override bool PromptEditProperties(WriterCommandEventArgs args)
        {
            if (PromptNewElement(args))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据对象设置创建输入域元素对象
        /// </summary>
        /// <param name="document">文档对象</param>
        /// <returns>创建的输入域元素对象</returns>
        public override DomElement CreateElement(DomDocument document)
        {
            DomInputFieldElement element = new DomInputFieldElement();
            element.Elements = new DomElementList();
            element.OwnerDocument = document;
            ApplyToElement(document ,  element , false );
            if (string.IsNullOrEmpty(this.InitalizeText) == false)
            {
                element.SetInnerTextFast(this.InitalizeText);
            }
            if (string.IsNullOrEmpty(this.InitalizeInnerValue) == false)
            {
                element.InnerValue = this.InitalizeInnerValue;
            }
            return element;
        }

        public override bool ApplyToElement(DomDocument document, DomElement element, bool logUndo)
        {
            DomInputFieldElement field = (DomInputFieldElement)element;
            if (_Name != null)
            {
                _Name = _Name.Trim();
            }
            bool result = false;
            if (field.ID != this.ID)
            {
                if (logUndo && document.CanLogUndo)
                {
                    document.UndoList.AddProperty("ID", field.ID, this.ID, field);
                }
                field.ID = this.ID;
                result = true;
            }
            //if (field.EventExpressions != this.EventExpressions)
            //{
            //    if (logUndo && document.CanLogUndo)
            //    {
            //        document.UndoList.AddProperty(
            //            "EventExpressions",
            //            field.EventExpressions,
            //            this.EventExpressions,
            //            field);
            //    }
            //    field.EventExpressions = this.EventExpressions;
            //    result = true;
            //}
            if (field.EnableHighlight != this.EnableHighlight)
            {
                if (logUndo && document.CanLogUndo)
                {
                    document.UndoList.AddProperty(
                        "EnableHighlight",
                        field.EnableHighlight,
                        this.EnableHighlight,
                        field);
                }
                field.EnableHighlight = this.EnableHighlight;
                result = true;
            }
            if (field.MultiParagraph != this.MultiParagraph)
            {
                if (logUndo && document.CanLogUndo)
                {
                    document.UndoList.AddProperty(
                        "MultiParagraph",
                        field.MultiParagraph,
                        this.MultiParagraph,
                        field);
                }
                field.MultiParagraph = this.MultiParagraph;
                result = true;
            }

            if (field.AcceptChildElementTypes2 != this.AcceptChildElementTypes2)
            {
                if (logUndo && document.CanLogUndo)
                {
                    document.UndoList.AddProperty(
                        "AcceptChildElementTypes2",
                        field.AcceptChildElementTypes2,
                        this.AcceptChildElementTypes2, field);
                }
                field.AcceptChildElementTypes2 = this.AcceptChildElementTypes2;
                result = true;
            }
            if ( field.Name != this.Name)
            {
                if ( logUndo && document.CanLogUndo)
                {
                    document.UndoList.AddProperty("Name", field.Name, this.Name, field);
                }
                field.Name = this.Name;
                result = true;
            }

            if (field.SpecifyWidth != this.SpecifyWidth)
            {
                if (logUndo && document.CanLogUndo)
                {
                    document.UndoList.AddProperty("SpecifyWidth", field.SpecifyWidth, this.SpecifyWidth, field);
                }
                field.SpecifyWidth = this.SpecifyWidth;
                result = true;
            }

            if (field.ToolTip != this.ToolTip)
            {
                if (logUndo && document.CanLogUndo)
                {
                    document.UndoList.AddProperty("ToolTip", field.ToolTip, this.ToolTip, field);
                }
                field.ToolTip = this.ToolTip;
                result = true;
            }

            if (field.BackgroundText != this.BackgroundText)
            {
                if (logUndo && document.CanLogUndo)
                {
                    document.UndoList.AddProperty(
                        "BackgroundText",
                        field.BackgroundText,
                        this.BackgroundText,
                        field);
                }
                field.BackgroundText = this.BackgroundText;
                result = true;
            }

            if ( field.Readonly != this.Readonly )
            {
                if (logUndo && document.CanLogUndo)
                {
                    document.UndoList.AddProperty("Readonly", field.Readonly, this.Readonly , field );
                }
                field.Readonly = this.Readonly;
                result = true;
            }
            if (field.UserEditable != this.UserEditable)
            {
                if (logUndo && document.CanLogUndo)
                {
                    document.UndoList.AddProperty("UserEditable", field.UserEditable, this.UserEditable, field);
                }
                field.UserEditable = this.UserEditable;
                result = true;
            }
            if ( field.ValidateStyle != this.ValidateStyle )
            {
                if ( logUndo && document.CanLogUndo)
                {
                    document.UndoList.AddProperty("ValidateStyle", field.ValidateStyle, this.ValidateStyle , field);
                }
                field.ValidateStyle = this.ValidateStyle ;
                result = true;
            }
            bool changeBinding = false;
            if (field.ValueBinding != this.ValueBinding)
            {
                if (logUndo && document.CanLogUndo)
                {
                    document.UndoList.AddProperty("ValueBinding", field.ValueBinding, this.ValueBinding, field);
                }
                field.ValueBinding = this.ValueBinding;
                changeBinding = true;
                result = true;
            }
            if (field.FieldSettings != this.FieldSettings)
            {
                if (logUndo && document.CanLogUndo)
                {
                    document.UndoList.AddProperty("FieldSettings", field.FieldSettings, this.FieldSettings, field);
                }
                field.FieldSettings = this.FieldSettings;
                result = true;
            }
            if (field.Attributes != this.Attributes)
            {
                if (logUndo && document.CanLogUndo)
                {
                    document.UndoList.AddProperty("Attributes", field.Attributes, this.Attributes, field);
                }
                field.Attributes = this.Attributes;
                result = true;
            }
            if (field.DisplayFormat != this.DisplayFormat)
            {
                if (logUndo && document.CanLogUndo)
                {
                    document.UndoList.AddProperty("DisplayFormat", field.DisplayFormat, this.DisplayFormat, field);
                }
                field.DisplayFormat = this.DisplayFormat;
                result = true;
            }
            if (changeBinding && field.ValueBinding != null)
            {
                field.UpdateDataBinding( false );
            }
            if (result)
            {
                ContentChangedEventArgs args = new ContentChangedEventArgs();
                args.Document = field.OwnerDocument;
                args.Element = field;
                args.LoadingDocument = false;
                field.RaiseBubbleOnContentChanged(args);
            }
            return result;
        }

    }

    /// <summary>
    /// 对象类型转换器
    /// </summary>
    public class XTextInputFieldElementPropertiesTypeConverter : TypeConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(value, attributes);
        }
    }
}
