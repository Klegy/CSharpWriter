using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCSoft.CSharpWriter.Dom ;
using DCSoft.CSharpWriter.Data;

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 编辑文本输入域设置对话框
    /// </summary>
    public partial class dlgInputFieldSettings : Form
    {
        public dlgInputFieldSettings()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private InputFieldSettings _InputFieldSettings = null;
        /// <summary>
        /// 当前编辑的设置信息对象
        /// </summary>
        public InputFieldSettings InputFieldSettings
        {
            get { return _InputFieldSettings; }
            set { _InputFieldSettings = value; }
        }

        private void dlgInputFieldSettings_Load(object sender, EventArgs e)
        {
            if (_InputFieldSettings == null)
            {
                _InputFieldSettings = new InputFieldSettings();
            }
            rdoText.Checked = ( _InputFieldSettings.EditStyle == InputFieldEditStyle.Text );
            rdoDropdownList.Checked = ( _InputFieldSettings.EditStyle == InputFieldEditStyle.DropdownList);
            if (rdoDropdownList.Checked)
            {
                chkMultiSelect.Checked = _InputFieldSettings.MultiSelect;
                if (_InputFieldSettings.ListSource != null)
                {
                    txtListSource.Text = _InputFieldSettings.ListSource.ToString();
                    txtListSource.Tag = _InputFieldSettings.ListSource.Clone();
                }
            }
            rdoDate.Checked = _InputFieldSettings.EditStyle == InputFieldEditStyle.Date;
            rdoDateTime.Checked = _InputFieldSettings.EditStyle == InputFieldEditStyle.DateTime;
            rdoDropdownList_CheckedChanged(null, null);
        }

        private void SetControlEnabled(Control ctl, bool enabled)
        {
            ctl.Enabled = enabled;
            foreach (Control c2 in ctl.Controls)
            {
                c2.Enabled = enabled;
            }
        }

        private void rdoDropdownList_CheckedChanged(object sender, EventArgs e)
        {
            SetControlEnabled(grbDropdownList, rdoDropdownList.Checked);
        }

        private void rdoDateTime_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _InputFieldSettings.EditStyle = InputFieldEditStyle.Text ;
            if (rdoText.Checked)
            {
                _InputFieldSettings.EditStyle = InputFieldEditStyle.Text;
            }
            else if (rdoDropdownList.Checked)
            {
                _InputFieldSettings.EditStyle = InputFieldEditStyle.DropdownList;
                _InputFieldSettings.MultiSelect = chkMultiSelect.Checked;
                _InputFieldSettings.ListSource = (ListSourceInfo)txtListSource.Tag;
            }
            else if (rdoDate.Checked)
            {
                _InputFieldSettings.EditStyle = InputFieldEditStyle.Date;
            }
            else if (rdoDateTime.Checked)
            {
                _InputFieldSettings.EditStyle = InputFieldEditStyle.DateTime;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
         
        private void btnBrowseListSource_Click(object sender, EventArgs e)
        {
            using (dlgListSourceInfo dlg = new dlgListSourceInfo())
            {
                dlg.ListSource = (ListSourceInfo)txtListSource.Tag;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    txtListSource.Text = dlg.ListSource.ToString();
                    txtListSource.Tag = dlg.ListSource;
                }
            }
        }
    }
}
