/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCSoft.CSharpWriter.Dom;
using System.Drawing.Design;

namespace DCSoft.CSharpWriter.Commands
{
    public partial class dlgElementTypeEditor : Form
    {
        public dlgElementTypeEditor()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private ElementType _InputElementType = ElementType.All;
        /// <summary>
        /// 输入输出的元素类型
        /// </summary>
        public ElementType InputElementType
        {
            get { return _InputElementType; }
            set { _InputElementType = value; }
        }

        private void dlgElementTypeEditor_Load(object sender, EventArgs e)
        {
            if (_InputElementType == ElementType.All)
            {
                chkAll.CheckState = CheckState.Checked;
            }
            else if (_InputElementType == ElementType.None)
            {
                chkAll.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkAll.CheckState = CheckState.Indeterminate;
            }
            this.chkCheckBox.Checked = ( _InputElementType & ElementType.CheckBox ) == ElementType.CheckBox ;
            this.chkField.Checked = (_InputElementType & ElementType.Field) == ElementType.Field;
            this.chkImage.Checked = (_InputElementType & ElementType.Image) == ElementType.Image;
            this.chkInputField.Checked = (_InputElementType & ElementType.InputField) == ElementType.InputField;
            this.chkLineBreak.Checked = (_InputElementType & ElementType.LineBreak) == ElementType.LineBreak;
            this.chkObject.Checked = (_InputElementType & ElementType.Object) == ElementType.Object;
            this.chkPageBreak.Checked = (_InputElementType & ElementType.PageBreak) == ElementType.PageBreak;
            this.chkParagraphFlag.Checked = (_InputElementType & ElementType.ParagraphFlag) == ElementType.ParagraphFlag;
            this.chkTable.Checked = (_InputElementType & ElementType.Table) == ElementType.Table;
            this.chkText.Checked = (_InputElementType & ElementType.Text ) == ElementType.Text;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (chkAll.CheckState == CheckState.Checked )
            {
                _InputElementType = ElementType.All;
            }
            else
            {
                _InputElementType = ElementType.None;
                if (this.chkCheckBox.Checked)
                {
                    _InputElementType = _InputElementType | ElementType.CheckBox;
                }
                if (this.chkField.Checked)
                {
                    _InputElementType = _InputElementType | ElementType.Field;
                }
                if (this.chkImage.Checked)
                {
                    _InputElementType = _InputElementType | ElementType.Image;
                }
                if (this.chkInputField.Checked)
                {
                    _InputElementType = _InputElementType | ElementType.InputField;
                }
                if (this.chkLineBreak.Checked)
                {
                    _InputElementType = _InputElementType | ElementType.LineBreak;
                }
                if (this.chkObject.Checked)
                {
                    _InputElementType = _InputElementType | ElementType.Object;
                }
                if (this.chkPageBreak.Checked)
                {
                    _InputElementType = _InputElementType | ElementType.PageBreak;
                }
                if (this.chkParagraphFlag.Checked)
                {
                    _InputElementType = _InputElementType | ElementType.ParagraphFlag;
                }
                if (this.chkTable.Checked)
                {
                    _InputElementType = _InputElementType | ElementType.Table;
                }
                if (this.chkText.Checked)
                {
                    _InputElementType = _InputElementType | ElementType.Text;
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public class ElementTypeEditor : UITypeEditor
        {
            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return UITypeEditorEditStyle.Modal;
            }

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                using (dlgElementTypeEditor dlg = new dlgElementTypeEditor())
                {
                    dlg.InputElementType = (ElementType)value;
                    if (dlg.ShowDialog(null) == DialogResult.OK)
                    {
                        return dlg.InputElementType;
                    }
                }
                return value;
            }
        }
    }
}
