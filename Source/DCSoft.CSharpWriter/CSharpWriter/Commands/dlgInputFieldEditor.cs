using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCSoft.CSharpWriter.Dom ;
using DCSoft.Common;
using DCSoft.CSharpWriter.Commands ;
using DCSoft.CSharpWriter.Data;
using DCSoft.Data;


namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 文本输入域属性对话框
    /// </summary>
    public partial class dlgInputFieldEditor : Form
    {
        public dlgInputFieldEditor()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private ElementEditEventArgs _SourceEventArgs = null;

        public ElementEditEventArgs SourceEventArgs
        {
            get { return _SourceEventArgs; }
            set { _SourceEventArgs = value; }
        }

        //private XTextInputFieldElementProperties _ElementProperties = null;

        //public XTextInputFieldElementProperties ElementProperties
        //{
        //    get { return _ElementProperties; }
        //    set { _ElementProperties = value; }
        //}
        
        private void dlgInputFieldProperties_Load(object sender, EventArgs e)
        {
            if (this.SourceEventArgs != null)
            {
                DomInputFieldElement field = (DomInputFieldElement)this.SourceEventArgs.Element;
                txtName.Text = field.Name;
                txtID.Text = field.ID;
                txtBackgroundText.Text = field.BackgroundText;
                chkReadonly.Checked = field.Readonly;
                chkUserEditable.Checked = field.UserEditable;
                chkMultiParagraph.Checked = field.MultiParagraph;
                txtToolTip.Text = field.ToolTip;
                if (field.ValidateStyle != null)
                {
                    txtValidate.Text = field.ValidateStyle.ToStyleString();
                    txtValidate.Tag = field.ValidateStyle ;
                }
                if (field.FieldSettings != null)
                {
                    txtSettings.Text = field.FieldSettings.ToString();
                    txtSettings.Tag = field.FieldSettings ;
                }
                if (field.Attributes != null)
                {
                    txtAttributes.Text = field.Attributes.ToString();
                    txtAttributes.Tag = field.Attributes ;
                }
                if (field.DisplayFormat != null)
                {
                    txtDisplayFormat.Text = field.DisplayFormat.ToString();
                    txtDisplayFormat.Tag = field.DisplayFormat ;
                }
                
                if (field.ValueBinding != null)
                {
                    txtBinding.Text = field.ValueBinding.ToString();
                    txtBinding.Tag = field.ValueBinding;
                }
                txtAcceptElementType.Text = field.AcceptChildElementTypes2.ToString();
                txtAcceptElementType.Tag = field.AcceptChildElementTypes2;
                //txtNextFieldControlValue.Text  = field.FieldSettings
                cboEnableHighlight.SelectedIndex = (int)field.EnableHighlight;
                txtSpecifyWidth.Value = (decimal)field.SpecifyWidth;

                return;
            }
        } 

        //private bool _ValidateStyleModified = false;
        private void btnBrowseFormat_Click(object sender, EventArgs e)
        {
            using ( dlgValueValidateStyleEditor dlg = new dlgValueValidateStyleEditor())
            {
                dlg.ValidateStyle = (ValueValidateStyle)txtValidate.Tag;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    txtValidate.Tag = dlg.ValidateStyle.Clone();
                    txtValidate.Text = dlg.ValidateStyle.ToStyleString();
                    //_ValidateStyleModified = true;
                }
            }
        }


        private void btnBrowseBinding_Click(object sender, EventArgs e)
        {
            if (this.SourceEventArgs != null)
            {
                using (dlgXDataBinding dlg = new dlgXDataBinding())
                {
                    dlg.XDataBinding = (XDataBinding)txtBinding.Tag;
                    dlg.Document = this.SourceEventArgs.Document;
                    if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        txtBinding.Tag = dlg.XDataBinding.Clone();
                        txtBinding.Text = dlg.XDataBinding.ToString();
                        //_ValidateStyleModified = true;
                    }
                }
            }
        }

        private void btnBrowseSettings_Click(object sender, EventArgs e)
        {
            using (dlgInputFieldSettings dlg = new dlgInputFieldSettings())
            {
                dlg.InputFieldSettings = (InputFieldSettings)txtSettings.Tag;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    txtSettings.Tag = dlg.InputFieldSettings.Clone();
                    txtSettings.Text = dlg.InputFieldSettings.ToString();
                    //_ValidateStyleModified = true;
                }
            }
        }


        private void btnBrowseAttributes_Click(object sender, EventArgs e)
        {
            using (dlgAttributes dlg = new dlgAttributes())
            {
                dlg.InputAttributes = (XAttributeList)txtAttributes.Tag;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    txtAttributes.Tag = dlg.InputAttributes.Clone();
                    txtAttributes.Text = dlg.InputAttributes.ToString();
                    //_ValidateStyleModified = true;
                }
            }
        }


        private void btnBrowseDisplayFormat_Click(object sender, EventArgs e)
        {
            using (DCSoft.Data.WinForms.dlgFormatDesigner dlg = new DCSoft.Data.WinForms.dlgFormatDesigner())
            {
                dlg.InputFormater = (ValueFormater)txtDisplayFormat.Tag;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    txtDisplayFormat.Tag = dlg.InputFormater.Clone() ;
                    txtDisplayFormat.Text = dlg.InputFormater.ToString();
                    //_ValidateStyleModified = true;
                }
            }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SourceEventArgs != null &&
                this.SourceEventArgs.Element is  DomInputFieldElement )
            {
                DomInputFieldElement field = (DomInputFieldElement)this.SourceEventArgs.Element;
                if (field != null)
                {
                    DomDocument document = this.SourceEventArgs.Document;
                    bool logUndo = this.SourceEventArgs.LogUndo
                        && this.SourceEventArgs.Document.CanLogUndo;
                    bool modifed = false;
                    
                    string txt = txtID.Text.Trim();
                    if (txt.Length == 0)
                    {
                        txt = null;
                    }
                    if (field.ID != txt)
                    {
                        if (logUndo)
                        {
                            document.UndoList.AddProperty(
                                "ID",
                                field.ID,
                                txt,
                                field);
                        }
                        field.ID = txt;
                        modifed = true;
                    }
                    txt = txtName.Text.Trim();
                    if (txt.Length == 0)
                    {
                        txt = null;
                    }
                    if (field.Name != txt)
                    {
                        if (logUndo)
                        {
                            document.UndoList.AddProperty(
                                "Name",
                                field.Name, 
                                txt,
                                field);
                        }
                        field.Name = txt;
                        modifed = true;
                    }
                    txt = this.txtToolTip.Text.Trim();
                    if (txt.Length == 0)
                    {
                        txt = null;
                    }
                    if (field.ToolTip != txt)
                    {
                        if (logUndo)
                        {
                            document.UndoList.AddProperty(
                                "ToolTip",
                                field.ToolTip,
                                txt,
                                field);
                        }
                        field.ToolTip = txt;
                        modifed = true;
                    }
                    txt = this.txtBackgroundText.Text.Trim();
                    if (txt.Length == 0)
                    {
                        txt = null;
                    }
                    if (field.BackgroundText != txt)
                    {
                        if (logUndo)
                        {
                            document.UndoList.AddProperty(
                                "BackgroundText",
                                field.BackgroundText,
                                txt,
                                field);
                        }
                        field.BackgroundText = txt;
                        modifed = true;
                    }

                    if (field.Readonly != chkReadonly.Checked)
                    {
                        if (logUndo)
                        {
                            document.UndoList.AddProperty(
                                "Readonly", 
                                field.Readonly,
                                chkReadonly.Checked,
                                field);
                        }
                        field.Readonly = chkReadonly.Checked;
                        modifed = true;
                    }

                    if (field.UserEditable != chkUserEditable.Checked)
                    {
                        if (logUndo)
                        {
                            document.UndoList.AddProperty(
                                "UserEditable",
                                field.UserEditable,
                                chkUserEditable.Checked,
                                field);
                        }
                        field.UserEditable = chkUserEditable.Checked;
                        modifed = true;
                    }

                    if (field.Attributes != txtAttributes.Tag)
                    {
                        if (logUndo)
                        {
                            document.UndoList.AddProperty(
                                "Attributes", 
                                field.Attributes,
                                txtAttributes.Tag,
                                field);
                        }
                        field.Attributes = (XAttributeList)txtAttributes.Tag;
                        modifed = true;
                    }

                    if (field.ValidateStyle != txtValidate.Tag)
                    {
                        if (logUndo)
                        {
                            document.UndoList.AddProperty(
                                "ValidateStyle",
                                field.ValidateStyle,
                                txtValidate.Tag, 
                                field);
                        }
                        field.ValidateStyle = (ValueValidateStyle)txtValidate.Tag;
                        modifed = true;
                    }

                    if (field.FieldSettings != txtSettings.Tag)
                    {
                        if (logUndo)
                        {
                            document.UndoList.AddProperty(
                                "FieldSettings",
                                field.FieldSettings, 
                                txtSettings.Tag,
                                field);
                        }
                        field.FieldSettings = (InputFieldSettings)txtSettings.Tag;
                        modifed = true;
                    }

                    if (field.DisplayFormat != this.txtDisplayFormat.Tag)
                    {
                        if (logUndo)
                        {
                            document.UndoList.AddProperty(
                                "DisplayFormat",
                                field.DisplayFormat, 
                                ( ValueFormater ) txtDisplayFormat.Tag,
                                field);
                        }
                        field.DisplayFormat = (ValueFormater)txtDisplayFormat.Tag;
                        modifed = true;
                    }

                    if (field.AcceptChildElementTypes2 != (ElementType)txtAcceptElementType.Tag)
                    {
                        if (logUndo)
                        {
                            document.UndoList.AddProperty(
                                "DisplayFormat", 
                                field.AcceptChildElementTypes2,
                                txtAcceptElementType.Tag, 
                                field);
                        }
                        field.AcceptChildElementTypes2 = (ElementType)txtAcceptElementType.Tag;
                        modifed = true;
                    }

                    if (field.EnableHighlight != (EnableState)this.cboEnableHighlight.SelectedIndex)
                    {
                        if (logUndo)
                        {
                            document.UndoList.AddProperty(
                                "EnableHighlight", 
                                field.EnableHighlight, 
                                (EnableState)cboEnableHighlight.SelectedIndex,
                                field);
                        }
                        field.EnableHighlight = (EnableState)cboEnableHighlight.SelectedIndex;
                        modifed = true;
                    }

                    if (field.MultiParagraph != chkMultiParagraph.Checked)
                    {
                        if (logUndo)
                        {
                            document.UndoList.AddProperty(
                                "MultiParagraph",
                                field.MultiParagraph,
                                chkMultiParagraph.Checked,
                                field);
                        }
                        field.MultiParagraph = chkMultiParagraph.Checked;
                        modifed = true;
                    }



                    if (field.SpecifyWidth != (float)txtSpecifyWidth.Value)
                    {
                        if (logUndo)
                        {
                            document.UndoList.AddProperty(
                                "SpecifyWidth",
                                field.SpecifyWidth,
                                (float)txtSpecifyWidth.Value,
                                field);
                        }
                        field.SpecifyWidth = (float)txtSpecifyWidth.Value;
                        modifed = true;
                    }
                     
                    bool changeBinding = false;
                    if (field.ValueBinding != txtBinding.Tag)
                    {
                        if (logUndo)
                        {
                            document.UndoList.AddProperty(
                                "ValueBinding",
                                field.ValueBinding, 
                                txtBinding.Tag,
                                field);
                        }
                        field.ValueBinding = (XDataBinding)txtBinding.Tag;
                        changeBinding = true;
                        modifed = true;
                    }

                    if (changeBinding && field.ValueBinding != null)
                    {
                        field.UpdateDataBinding(false);
                    }

                    if (this.SourceEventArgs.Method == ElementEditMethod.Edit)
                    {
                        if (modifed)
                        {
                            DomContentElement ce = field.ContentElement;
                            ce.SetLinesInvalidateState(
                                field.StartElement.OwnerLine, 
                                field.EndElement.OwnerLine);
                            ce.UpdateContentElements(true);
                            ce.RefreshPrivateContent(field.FirstContentElement.ViewIndex);
                            ContentChangedEventArgs args = new ContentChangedEventArgs();
                            args.Document = field.OwnerDocument;
                            args.Element = field;
                            args.LoadingDocument = false;
                            field.RaiseBubbleOnContentChanged(args);
                        }
                    }
                    else
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
                this.Close();
                return;
            }

            //_ElementProperties.ID = txtID.Text.Trim();
            //_ElementProperties.Name = txtName.Text.Trim();
            //_ElementProperties.ToolTip = txtToolTip.Text.Trim();
            //_ElementProperties.BackgroundText = txtBackgroundText.Text.Trim();
            //_ElementProperties.Readonly = chkReadonly.Checked;
            //_ElementProperties.UserEditable = chkUserEditable.Checked;
            //_ElementProperties.ValidateStyle = (ValueValidateStyle)txtValidate.Tag;
            //_ElementProperties.FieldSettings = (InputFieldSettings)txtSettings.Tag;
            //_ElementProperties.ValueBinding = (XDataBinding)txtBinding.Tag;
            //_ElementProperties.Attributes = (XAttributeList)txtAttributes.Tag;
            //_ElementProperties.DisplayFormat = (ValueFormater)txtDisplayFormat.Tag;
            //_ElementProperties.AcceptChildElementTypes2 = (ElementType)txtAcceptElementType.Tag;
            //_ElementProperties.NextFieldVisibleExpression = txtNextFieldControlValue.Text.Trim();
            //_ElementProperties.EnableHighlight = (EnableState)cboEnableHighlight.SelectedIndex;
            //_ElementProperties.MultiParagraph = chkMultiParagraph.Checked;
            //_ElementProperties.EventExpressions = (EventExpressionInfoList)txtEventExpressions.Tag;
            //_ElementProperties.SpecifyWidth = (float)txtSpecifyWidth.Value;
            //this.DialogResult = System.Windows.Forms.DialogResult.OK;
            //this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBrowseAcceptElementType_Click(object sender, EventArgs e)
        {
            using (dlgElementTypeEditor dlg = new dlgElementTypeEditor())
            {
                dlg.InputElementType = (ElementType)txtAcceptElementType.Tag;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    txtAcceptElementType.Tag = dlg.InputElementType;
                    txtAcceptElementType.Text = dlg.InputElementType.ToString();
                }
            }
        }

         

    }
}
