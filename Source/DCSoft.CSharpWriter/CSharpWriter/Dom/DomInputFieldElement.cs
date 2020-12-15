using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel ;
using DCSoft.Data ;
using DCSoft.CSharpWriter.Data;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 纯文本数据输入域
    /// </summary>
    [Serializable()]
    [System.Xml.Serialization.XmlType("XInputField")]
    [System.Diagnostics.DebuggerDisplay("Input Name:{Name}")]
    [Editor(
        typeof(DCSoft.CSharpWriter.Commands.XTextInputFieldElementEditor),
        typeof(ElementEditor))]
    public class DomInputFieldElement : DomInputFieldElementBase
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomInputFieldElement()
        {
            this.AcceptChildElementTypes2 = ElementType.Text;
        }
        private InputFieldSettings _FieldSettings = null;
        /// <summary>
        /// 输入域设置
        /// </summary>
        [DefaultValue( null )]
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

        public override void AfterLoad(FileFormat format)
        {
            if (this.FieldSettings != null)
            {
                this.FieldSettings.FixListSourceSettings();
            }
            base.AfterLoad(format);
        }
         
        /// <summary>
        /// 输入域获得输入焦点
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        public override void OnGotFocus(  EventArgs args)
        {
            base.OnGotFocus( args);
            if (this.OwnerDocument.Options.EditOptions.AutoEditElementValue
                && this.DocumentContentElement.Selection.Length == 0 )
            {
                if (this.OwnerDocument.EditorControl.EditorHost.CurrentEditContext == null
                    || this.OwnerDocument.EditorControl.EditorHost.CurrentEditContext.Element != this
                    || this.OwnerDocument.EditorControl.EditorHost.CurrentEditContext.EditStyle 
                                != Controls.ElementValueEditorEditStyle.Modal)
                {
                    if (this.FieldSettings != null
                        && this.FieldSettings.EditStyle == InputFieldEditStyle.DropdownList
                        && this.FieldSettings.ListSource != null
                        && this.FieldSettings.ListSource.IsEmpty == false 
                        && this.OwnerDocument.EditorControl != null)
                    {
                        this.OwnerDocument.EditorControl.BeginEditElementValue(this, false);
                    }
                    else if ( this.FieldSettings != null 
                        && this.FieldSettings.EditStyle == InputFieldEditStyle.Date)
                    {
                        this.OwnerDocument.EditorControl.BeginEditElementValue(this, false);
                    }
                }
            }
        }

        /// <summary>
        /// 输入域失去输入焦点
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        public override void OnLostFocus( EventArgs args)
        {
            base.OnLostFocus(args);
            if (this.OwnerDocument.EditorControl != null
                && this.OwnerDocument.EditorControl.EditorHost != null
                && this.OwnerDocument.EditorControl.EditorHost.CurrentEditContext != null
                && this.OwnerDocument.EditorControl.EditorHost.CurrentEditContext.Element == this)
            {
                this.OwnerDocument.EditorControl.CancelEditElementValue();
            }
        }

        public override string GetDisplayText(string Value)
        {
            if ( this.FieldSettings != null 
                && this.FieldSettings.ListSource != null
                && this.FieldSettings.ListSource.IsEmpty == false)
            {
                IListSourceProvider provider = null;
                WriterAppHost host = this.OwnerDocument.AppHost;
                if (host != null)
                {
                    provider = host.Services.ListSourceProvider ;
                    if (provider != null)
                    {
                        ListItemCollection items = ListSourceInfo.GetRuntimeListItems(
                            this.OwnerDocument.AppHost,
                            this.FieldSettings.ListSource,
                            provider);
                        
                        if (items != null)
                        {
                            return items.ValueToText(Value);
                        }
                    }
                }
            }
            return base.GetDisplayText(Value);
        }

        /// <summary>
        /// 处理文档内容发生改变事件
        /// </summary>
        /// <param name="args">参数</param>
        public override void OnContentChanged(ContentChangedEventArgs args)
        {
            base.OnContentChanged(args);
            if (this.FieldSettings != null)
            {
                if (this.FieldSettings.EditStyle == InputFieldEditStyle.Date
                    || this.FieldSettings.EditStyle == InputFieldEditStyle.DateTime)
                {
                    DateTime dtm = DateTime.Now;
                    bool flag = false;
                    if (this.DisplayFormat != null && string.IsNullOrEmpty(this.DisplayFormat.Format) == false)
                    {
                        flag = DateTime.TryParseExact(this.Text, this.DisplayFormat.Format, null, System.Globalization.DateTimeStyles.AssumeLocal, out dtm);
                    }
                    else
                    {
                        flag = DateTime.TryParse(this.Text, out dtm);
                    }
                    if (flag)
                    {
                        if (this.FieldSettings.EditStyle == InputFieldEditStyle.Date)
                        {
                            this.InnerValue = dtm.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            this.InnerValue = dtm.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    }
                    else
                    {
                        this.InnerValue = this.Text;
                    }
                }
            }
            else
            {
                this.InnerValue = this.Text;
            }
            //if (string.IsNullOrEmpty(this.NextFieldVisibleExpression) == false )
            //{
            //    bool nextFieldVisible = this.OwnerDocument.ExpressionExecuter.ExecuteBoolean(
            //        this,
            //        this.NextFieldVisibleExpression,
            //        true);
            //    XTextFieldElement nextField = (XTextFieldElement)this.OwnerDocument.GetNextElement(
            //        this,
            //        typeof(XTextFieldElement));
            //    if (nextField != null)
            //    {
            //        if ( nextField.Visible != nextFieldVisible)
            //        {
            //            // 设置下一个域元素的可见性
            //            if (args.LoadingDocument)
            //            {
            //                nextField.Visible = nextFieldVisible;
            //            }
            //            else
            //            {
            //                nextField.EditorSetVisible(nextFieldVisible);
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="Deeply">是否深度复制</param>
        /// <returns>复制品</returns>
        public override DomElement Clone(bool Deeply)
        {
            DomInputFieldElement field =( DomInputFieldElement ) base.Clone(Deeply);
            if (this._FieldSettings != null)
            {
                field._FieldSettings = this._FieldSettings.Clone();
            }
            return field;
        }
    }
}
