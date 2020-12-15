/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DCSoft.Common
{
    /// <summary>
    /// 数据校验规则编辑对话框
    /// </summary>
    /// <remarks>编制 袁永福 </remarks>
    public partial class dlgValueValidateStyleEditor : Form
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public dlgValueValidateStyleEditor()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        private ValueValidateStyle myValidateStyle = new ValueValidateStyle();

        public ValueValidateStyle ValidateStyle
        {
            get { return myValidateStyle; }
            set { myValidateStyle = value; }
        }

        private void dlgValueValidateStyleEditor_Load(object sender, EventArgs e)
        {
            if (myValidateStyle == null)
            {
                myValidateStyle = new ValueValidateStyle();
            }
            this.txtValueName.Text = myValidateStyle.ValueName;
            this.txtCustomMessage.Text = myValidateStyle.CustomMessage;
            this.chkRequire.Checked = myValidateStyle.Required;
            rdoDate.Checked = false;
            rdoNumeric.Checked = false;
            rdoText.Checked = false;
            rdoTime.Checked = false;
            if (myValidateStyle.ValueType == ValueTypeStyle.Text)
            {
                rdoText.Checked = true;
                chkMaxLength.Checked = myValidateStyle.CheckMaxValue;
                chkMinLength.Checked = myValidateStyle.CheckMinValue;
                txtMaxLength.Value = myValidateStyle.MaxLength;
                txtMinLength.Value = myValidateStyle.MinLength;
            }
            else if (myValidateStyle.ValueType == ValueTypeStyle.Numeric 
                || myValidateStyle.ValueType == ValueTypeStyle.Integer )
            {
                rdoNumeric.Checked = true;
                chkInteger.Checked = myValidateStyle.ValueType == ValueTypeStyle.Integer;
                chkMaxValue.Checked = myValidateStyle.CheckMaxValue;
                if (double.IsNaN(myValidateStyle.MaxValue) == false)
                {
                    txtMaxValue.Text = myValidateStyle.MaxValue.ToString();
                }
                chkMinValue.Checked = myValidateStyle.CheckMinValue;
                if (double.IsNaN(myValidateStyle.MinValue) == false)
                {
                    txtMinValue.Text = myValidateStyle.MinValue.ToString();
                }
            }
            else if (myValidateStyle.ValueType == ValueTypeStyle.Date
                || myValidateStyle.ValueType == ValueTypeStyle.DateTime)
            {
                rdoDate.Checked = true;
                dtpMaxDate.Value = myValidateStyle.DateTimeMaxValue;
                dtpMaxTime.Value = myValidateStyle.DateTimeMaxValue;
                chkMaxDateTime.Checked = myValidateStyle.CheckMaxValue;

                dtpMinDate.Value = myValidateStyle.DateTimeMinValue;
                dtpMinTime.Value = myValidateStyle.DateTimeMinValue;
                chkMinDateTime.Checked = myValidateStyle.CheckMinValue;
            }
            else if (myValidateStyle.ValueType == ValueTypeStyle.Time)
            {
                rdoTime.Checked = true;

                dtpMaxTime2.Value = new DateTime(1900, 1, 1).Add(myValidateStyle.DateTimeMaxValue.TimeOfDay);
                chkMaxTime.Checked = !myValidateStyle.CheckMaxValue;

                chkMinTime.Checked = !myValidateStyle.CheckMinValue;
                dtpMinTime2.Value = new DateTime(1900, 1, 1).Add(myValidateStyle.DateTimeMinValue.TimeOfDay);    
            }
            SetControlState(this.rdoText, this.grbTextStyle);
            SetControlState(this.rdoDate, this.grpDateTimeStyle);
            SetControlState(this.rdoTime, this.grpTimeStyle);
            SetControlState(this.rdoNumeric, this.grbNumericStyle);
        }

        private void txtMinLength_ValueChanged(object sender, EventArgs e)
        {

        }

        private void rdoText_CheckedChanged(object sender, EventArgs e)
        {
            SetControlState(( RadioButton ) sender , this.grbTextStyle  );
        }

        private void rdoNumeric_CheckedChanged(object sender, EventArgs e)
        {
            SetControlState((RadioButton)sender , this.grbNumericStyle );
        }

        private void rdoDate_CheckedChanged(object sender, EventArgs e)
        {
            SetControlState((RadioButton)sender , this.grpDateTimeStyle );
        }

        private void rdoTime_CheckedChanged(object sender, EventArgs e)
        {
            SetControlState((RadioButton)sender , this.grpTimeStyle );
        }

        private void SetControlState(System.Windows.Forms.RadioButton rdo , Control containerControl )
        {
            if (rdo.Checked)
            {
                foreach (Control ctl in containerControl.Controls)
                {
                    ctl.Enabled = true;
                }
                if (rdo == rdoText)
                {
                    txtMaxLength.Enabled = chkMaxLength.Checked;
                    txtMinLength.Enabled = chkMinLength.Checked;
                }
                if (rdo == rdoNumeric)
                {
                    txtMaxValue.Enabled = chkMaxValue.Checked && chkMaxValue.Enabled;
                    txtMinValue.Enabled = chkMinValue.Checked && chkMinValue.Enabled;
                }
                else if (rdo == rdoDate)
                {
                    dtpMinDate.Enabled = chkMinDateTime.Checked && chkMinDateTime.Enabled;
                    dtpMinTime.Enabled = chkMinDateTime.Checked && chkMinDateTime.Enabled;
                    dtpMaxDate.Enabled = chkMaxDateTime.Checked && chkMaxDateTime.Enabled;
                    dtpMaxTime.Enabled = chkMaxDateTime.Checked && chkMaxDateTime.Enabled;
                }
                else if (rdo == rdoTime)
                {
                    dtpMinTime2.Enabled = chkMinTime.Checked && chkMinTime.Enabled;
                    dtpMaxTime2.Enabled = chkMaxTime.Checked && chkMaxTime.Enabled;
                }
            }
            else
            {
                foreach (System.Windows.Forms.Control ctl in containerControl.Controls)
                {
                    if (ctl != rdo)
                    {
                        ctl.Enabled = false;
                    }
                }
            }
        }

        private void chkMaxValue_CheckedChanged(object sender, EventArgs e)
        {
            txtMaxValue.Enabled = chkMaxValue.Checked && chkMaxValue.Enabled ;
        }

        private void chkMinValue_CheckedChanged(object sender, EventArgs e)
        {
            txtMinValue.Enabled = chkMinValue.Checked && chkMinValue.Enabled ;
        }

        private void chkMinDateTime_CheckedChanged(object sender, EventArgs e)
        {
            dtpMinDate.Enabled = chkMinDateTime.Checked && chkMinDateTime.Enabled ;
            dtpMinTime.Enabled = chkMinDateTime.Checked && chkMinDateTime.Enabled ;
        }

        private void chkMaxDateTime_CheckedChanged(object sender, EventArgs e)
        {
            dtpMaxDate.Enabled = chkMaxDateTime.Checked && chkMaxDateTime.Enabled ;
            dtpMaxTime.Enabled = chkMaxDateTime.Checked && chkMaxDateTime.Enabled ;
        }

        private void chkMinTime_CheckedChanged(object sender, EventArgs e)
        {
            dtpMinTime2.Enabled = chkMinTime.Checked && chkMinTime.Enabled ;
        }

        private void chkMaxTime_CheckedChanged(object sender, EventArgs e)
        {
            dtpMaxTime2.Enabled = chkMaxTime.Checked && chkMaxTime.Enabled ;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            myValidateStyle.ValueName = txtValueName.Text.Trim();
            myValidateStyle.CustomMessage = txtCustomMessage.Text.Trim();
            myValidateStyle.Required = chkRequire.Checked;
            if (rdoText.Checked)
            {
                myValidateStyle.ValueType = ValueTypeStyle.Text;
                myValidateStyle.MaxLength = Convert.ToInt32( txtMaxLength.Value);
                myValidateStyle.MinLength = Convert.ToInt32(txtMinLength.Value);
                
            }
            else if (rdoNumeric.Checked)
            {
                myValidateStyle.ValueType = chkInteger.Checked ? ValueTypeStyle.Integer : ValueTypeStyle.Numeric;
                myValidateStyle.CheckMaxValue = chkMaxValue.Checked;
                myValidateStyle.CheckMinValue = chkMinValue.Checked;

                if (chkMaxValue.Checked)
                {
                    double dbl = 0;
                    if (double.TryParse(txtMaxValue.Text, out dbl))
                    {
                        myValidateStyle.MaxValue = dbl;
                    }
                    else
                    {
                        txtMaxValue.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    myValidateStyle.MaxValue = double.NaN;
                }
                if (chkMinValue.Checked)
                {
                    double dbl = 0;
                    if (double.TryParse(txtMinValue.Text, out dbl))
                    {
                        myValidateStyle.MinValue = dbl;
                    }
                    else
                    {
                        txtMinValue.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    myValidateStyle.MinValue = double.NaN;
                }
            }
            else if (rdoDate.Checked)
            {
                myValidateStyle.ValueType = ValueTypeStyle.DateTime;
                if (chkMaxDateTime.Checked)
                {
                    myValidateStyle.DateTimeMaxValue = dtpMaxDate.Value.Date.Add(dtpMaxTime.Value.TimeOfDay);
                }
                else
                {
                    myValidateStyle.DateTimeMaxValue = ValueValidateStyle.NullDateTime;
                }
                if (chkMinDateTime.Checked)
                {
                    myValidateStyle.DateTimeMinValue = dtpMinDate.Value.Date.Add(dtpMinTime.Value.TimeOfDay);
                }
                else
                {
                    myValidateStyle.DateTimeMinValue = ValueValidateStyle.NullDateTime;
                }
            }
            else if (rdoTime.Checked)
            {
                myValidateStyle.ValueType = ValueTypeStyle.Time;
                if (chkMaxTime.Checked)
                {
                    myValidateStyle.DateTimeMaxValue = dtpMaxTime2.Value;
                }
                else
                {
                    myValidateStyle.DateTimeMaxValue = ValueValidateStyle.NullDateTime;
                }
                if (chkMinTime.Checked)
                {
                    myValidateStyle.DateTimeMinValue = dtpMinTime2.Value;
                }
                else
                {
                    myValidateStyle.DateTimeMinValue = ValueValidateStyle.NullDateTime;
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkMaxLength_CheckedChanged(object sender, EventArgs e)
        {
            txtMaxLength.Enabled = chkMaxLength.Checked;
        }

        private void chkMinLength_CheckedChanged(object sender, EventArgs e)
        {
            txtMinLength.Enabled = chkMinLength.Checked;
        }
    }

    /// <summary>
    /// 数据校验样式编辑器
    /// </summary>
    public class ValueValidateStyleEditor : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object Value)
        {
            ValueValidateStyle style = Value as ValueValidateStyle;
            if (style == null)
                style = new ValueValidateStyle();
            else
                style = style.Clone();
            using (dlgValueValidateStyleEditor dlg = new dlgValueValidateStyleEditor())
            {
                dlg.ValidateStyle = style ;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return dlg.ValidateStyle;
                }
            }
            return Value;
        }
    }
}