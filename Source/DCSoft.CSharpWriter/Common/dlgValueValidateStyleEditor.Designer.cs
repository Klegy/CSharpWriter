/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
namespace DCSoft.Common
{
    partial class dlgValueValidateStyleEditor
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgValueValidateStyleEditor));
            this.label1 = new System.Windows.Forms.Label();
            this.txtValueName = new System.Windows.Forms.TextBox();
            this.chkRequire = new System.Windows.Forms.CheckBox();
            this.grbTextStyle = new System.Windows.Forms.GroupBox();
            this.txtMinLength = new System.Windows.Forms.NumericUpDown();
            this.txtMaxLength = new System.Windows.Forms.NumericUpDown();
            this.rdoText = new System.Windows.Forms.RadioButton();
            this.grbNumericStyle = new System.Windows.Forms.GroupBox();
            this.txtMaxValue = new System.Windows.Forms.TextBox();
            this.txtMinValue = new System.Windows.Forms.TextBox();
            this.chkInteger = new System.Windows.Forms.CheckBox();
            this.chkMinValue = new System.Windows.Forms.CheckBox();
            this.chkMaxValue = new System.Windows.Forms.CheckBox();
            this.rdoNumeric = new System.Windows.Forms.RadioButton();
            this.grpDateTimeStyle = new System.Windows.Forms.GroupBox();
            this.chkMaxDateTime = new System.Windows.Forms.CheckBox();
            this.chkMinDateTime = new System.Windows.Forms.CheckBox();
            this.dtpMaxTime = new System.Windows.Forms.DateTimePicker();
            this.dtpMinTime = new System.Windows.Forms.DateTimePicker();
            this.dtpMaxDate = new System.Windows.Forms.DateTimePicker();
            this.dtpMinDate = new System.Windows.Forms.DateTimePicker();
            this.rdoDate = new System.Windows.Forms.RadioButton();
            this.grpTimeStyle = new System.Windows.Forms.GroupBox();
            this.chkMaxTime = new System.Windows.Forms.CheckBox();
            this.chkMinTime = new System.Windows.Forms.CheckBox();
            this.dtpMaxTime2 = new System.Windows.Forms.DateTimePicker();
            this.dtpMinTime2 = new System.Windows.Forms.DateTimePicker();
            this.rdoTime = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCustomMessage = new System.Windows.Forms.TextBox();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.chkMaxLength = new System.Windows.Forms.CheckBox();
            this.chkMinLength = new System.Windows.Forms.CheckBox();
            this.grbTextStyle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxLength)).BeginInit();
            this.grbNumericStyle.SuspendLayout();
            this.grpDateTimeStyle.SuspendLayout();
            this.grpTimeStyle.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtValueName
            // 
            resources.ApplyResources(this.txtValueName, "txtValueName");
            this.txtValueName.Name = "txtValueName";
            // 
            // chkRequire
            // 
            resources.ApplyResources(this.chkRequire, "chkRequire");
            this.chkRequire.Name = "chkRequire";
            this.chkRequire.UseVisualStyleBackColor = true;
            // 
            // grbTextStyle
            // 
            this.grbTextStyle.Controls.Add(this.txtMinLength);
            this.grbTextStyle.Controls.Add(this.txtMaxLength);
            this.grbTextStyle.Controls.Add(this.chkMaxLength);
            this.grbTextStyle.Controls.Add(this.chkMinLength);
            resources.ApplyResources(this.grbTextStyle, "grbTextStyle");
            this.grbTextStyle.Name = "grbTextStyle";
            this.grbTextStyle.TabStop = false;
            // 
            // txtMinLength
            // 
            resources.ApplyResources(this.txtMinLength, "txtMinLength");
            this.txtMinLength.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.txtMinLength.Name = "txtMinLength";
            this.txtMinLength.ValueChanged += new System.EventHandler(this.txtMinLength_ValueChanged);
            // 
            // txtMaxLength
            // 
            resources.ApplyResources(this.txtMaxLength, "txtMaxLength");
            this.txtMaxLength.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.txtMaxLength.Name = "txtMaxLength";
            // 
            // rdoText
            // 
            resources.ApplyResources(this.rdoText, "rdoText");
            this.rdoText.Name = "rdoText";
            this.rdoText.TabStop = true;
            this.rdoText.UseVisualStyleBackColor = true;
            this.rdoText.CheckedChanged += new System.EventHandler(this.rdoText_CheckedChanged);
            // 
            // grbNumericStyle
            // 
            this.grbNumericStyle.Controls.Add(this.txtMaxValue);
            this.grbNumericStyle.Controls.Add(this.txtMinValue);
            this.grbNumericStyle.Controls.Add(this.chkInteger);
            this.grbNumericStyle.Controls.Add(this.chkMinValue);
            this.grbNumericStyle.Controls.Add(this.chkMaxValue);
            resources.ApplyResources(this.grbNumericStyle, "grbNumericStyle");
            this.grbNumericStyle.Name = "grbNumericStyle";
            this.grbNumericStyle.TabStop = false;
            // 
            // txtMaxValue
            // 
            resources.ApplyResources(this.txtMaxValue, "txtMaxValue");
            this.txtMaxValue.Name = "txtMaxValue";
            // 
            // txtMinValue
            // 
            resources.ApplyResources(this.txtMinValue, "txtMinValue");
            this.txtMinValue.Name = "txtMinValue";
            // 
            // chkInteger
            // 
            resources.ApplyResources(this.chkInteger, "chkInteger");
            this.chkInteger.Name = "chkInteger";
            this.chkInteger.UseVisualStyleBackColor = true;
            // 
            // chkMinValue
            // 
            resources.ApplyResources(this.chkMinValue, "chkMinValue");
            this.chkMinValue.Name = "chkMinValue";
            this.chkMinValue.UseVisualStyleBackColor = true;
            this.chkMinValue.CheckedChanged += new System.EventHandler(this.chkMinValue_CheckedChanged);
            // 
            // chkMaxValue
            // 
            resources.ApplyResources(this.chkMaxValue, "chkMaxValue");
            this.chkMaxValue.Name = "chkMaxValue";
            this.chkMaxValue.UseVisualStyleBackColor = true;
            this.chkMaxValue.CheckedChanged += new System.EventHandler(this.chkMaxValue_CheckedChanged);
            // 
            // rdoNumeric
            // 
            resources.ApplyResources(this.rdoNumeric, "rdoNumeric");
            this.rdoNumeric.Name = "rdoNumeric";
            this.rdoNumeric.TabStop = true;
            this.rdoNumeric.UseVisualStyleBackColor = true;
            this.rdoNumeric.CheckedChanged += new System.EventHandler(this.rdoNumeric_CheckedChanged);
            // 
            // grpDateTimeStyle
            // 
            this.grpDateTimeStyle.Controls.Add(this.chkMaxDateTime);
            this.grpDateTimeStyle.Controls.Add(this.chkMinDateTime);
            this.grpDateTimeStyle.Controls.Add(this.dtpMaxTime);
            this.grpDateTimeStyle.Controls.Add(this.dtpMinTime);
            this.grpDateTimeStyle.Controls.Add(this.dtpMaxDate);
            this.grpDateTimeStyle.Controls.Add(this.dtpMinDate);
            resources.ApplyResources(this.grpDateTimeStyle, "grpDateTimeStyle");
            this.grpDateTimeStyle.Name = "grpDateTimeStyle";
            this.grpDateTimeStyle.TabStop = false;
            // 
            // chkMaxDateTime
            // 
            resources.ApplyResources(this.chkMaxDateTime, "chkMaxDateTime");
            this.chkMaxDateTime.Name = "chkMaxDateTime";
            this.chkMaxDateTime.UseVisualStyleBackColor = true;
            this.chkMaxDateTime.CheckedChanged += new System.EventHandler(this.chkMaxDateTime_CheckedChanged);
            // 
            // chkMinDateTime
            // 
            resources.ApplyResources(this.chkMinDateTime, "chkMinDateTime");
            this.chkMinDateTime.Name = "chkMinDateTime";
            this.chkMinDateTime.UseVisualStyleBackColor = true;
            this.chkMinDateTime.CheckedChanged += new System.EventHandler(this.chkMinDateTime_CheckedChanged);
            // 
            // dtpMaxTime
            // 
            this.dtpMaxTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            resources.ApplyResources(this.dtpMaxTime, "dtpMaxTime");
            this.dtpMaxTime.Name = "dtpMaxTime";
            this.dtpMaxTime.ShowUpDown = true;
            // 
            // dtpMinTime
            // 
            this.dtpMinTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            resources.ApplyResources(this.dtpMinTime, "dtpMinTime");
            this.dtpMinTime.Name = "dtpMinTime";
            this.dtpMinTime.ShowUpDown = true;
            // 
            // dtpMaxDate
            // 
            resources.ApplyResources(this.dtpMaxDate, "dtpMaxDate");
            this.dtpMaxDate.Name = "dtpMaxDate";
            // 
            // dtpMinDate
            // 
            resources.ApplyResources(this.dtpMinDate, "dtpMinDate");
            this.dtpMinDate.Name = "dtpMinDate";
            // 
            // rdoDate
            // 
            resources.ApplyResources(this.rdoDate, "rdoDate");
            this.rdoDate.Name = "rdoDate";
            this.rdoDate.TabStop = true;
            this.rdoDate.UseVisualStyleBackColor = true;
            this.rdoDate.CheckedChanged += new System.EventHandler(this.rdoDate_CheckedChanged);
            // 
            // grpTimeStyle
            // 
            this.grpTimeStyle.Controls.Add(this.chkMaxTime);
            this.grpTimeStyle.Controls.Add(this.chkMinTime);
            this.grpTimeStyle.Controls.Add(this.dtpMaxTime2);
            this.grpTimeStyle.Controls.Add(this.dtpMinTime2);
            resources.ApplyResources(this.grpTimeStyle, "grpTimeStyle");
            this.grpTimeStyle.Name = "grpTimeStyle";
            this.grpTimeStyle.TabStop = false;
            // 
            // chkMaxTime
            // 
            resources.ApplyResources(this.chkMaxTime, "chkMaxTime");
            this.chkMaxTime.Name = "chkMaxTime";
            this.chkMaxTime.UseVisualStyleBackColor = true;
            this.chkMaxTime.CheckedChanged += new System.EventHandler(this.chkMaxTime_CheckedChanged);
            // 
            // chkMinTime
            // 
            resources.ApplyResources(this.chkMinTime, "chkMinTime");
            this.chkMinTime.Name = "chkMinTime";
            this.chkMinTime.UseVisualStyleBackColor = true;
            this.chkMinTime.CheckedChanged += new System.EventHandler(this.chkMinTime_CheckedChanged);
            // 
            // dtpMaxTime2
            // 
            this.dtpMaxTime2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            resources.ApplyResources(this.dtpMaxTime2, "dtpMaxTime2");
            this.dtpMaxTime2.Name = "dtpMaxTime2";
            this.dtpMaxTime2.ShowUpDown = true;
            // 
            // dtpMinTime2
            // 
            this.dtpMinTime2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            resources.ApplyResources(this.dtpMinTime2, "dtpMinTime2");
            this.dtpMinTime2.Name = "dtpMinTime2";
            this.dtpMinTime2.ShowUpDown = true;
            // 
            // rdoTime
            // 
            resources.ApplyResources(this.rdoTime, "rdoTime");
            this.rdoTime.Name = "rdoTime";
            this.rdoTime.TabStop = true;
            this.rdoTime.UseVisualStyleBackColor = true;
            this.rdoTime.CheckedChanged += new System.EventHandler(this.rdoTime_CheckedChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtCustomMessage
            // 
            resources.ApplyResources(this.txtCustomMessage, "txtCustomMessage");
            this.txtCustomMessage.Name = "txtCustomMessage";
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // chkMaxLength
            // 
            resources.ApplyResources(this.chkMaxLength, "chkMaxLength");
            this.chkMaxLength.Name = "chkMaxLength";
            this.chkMaxLength.UseVisualStyleBackColor = true;
            this.chkMaxLength.CheckedChanged += new System.EventHandler(this.chkMaxLength_CheckedChanged);
            // 
            // chkMinLength
            // 
            resources.ApplyResources(this.chkMinLength, "chkMinLength");
            this.chkMinLength.Name = "chkMinLength";
            this.chkMinLength.UseVisualStyleBackColor = true;
            this.chkMinLength.CheckedChanged += new System.EventHandler(this.chkMinLength_CheckedChanged);
            // 
            // dlgValueValidateStyleEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.rdoNumeric);
            this.Controls.Add(this.grbNumericStyle);
            this.Controls.Add(this.rdoTime);
            this.Controls.Add(this.grpTimeStyle);
            this.Controls.Add(this.rdoText);
            this.Controls.Add(this.rdoDate);
            this.Controls.Add(this.grpDateTimeStyle);
            this.Controls.Add(this.grbTextStyle);
            this.Controls.Add(this.chkRequire);
            this.Controls.Add(this.txtCustomMessage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtValueName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgValueValidateStyleEditor";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.dlgValueValidateStyleEditor_Load);
            this.grbTextStyle.ResumeLayout(false);
            this.grbTextStyle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxLength)).EndInit();
            this.grbNumericStyle.ResumeLayout(false);
            this.grbNumericStyle.PerformLayout();
            this.grpDateTimeStyle.ResumeLayout(false);
            this.grpDateTimeStyle.PerformLayout();
            this.grpTimeStyle.ResumeLayout(false);
            this.grpTimeStyle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtValueName;
        private System.Windows.Forms.CheckBox chkRequire;
        private System.Windows.Forms.GroupBox grbTextStyle;
        private System.Windows.Forms.NumericUpDown txtMinLength;
        private System.Windows.Forms.NumericUpDown txtMaxLength;
        private System.Windows.Forms.RadioButton rdoText;
        private System.Windows.Forms.GroupBox grbNumericStyle;
        private System.Windows.Forms.RadioButton rdoNumeric;
        private System.Windows.Forms.CheckBox chkMinValue;
        private System.Windows.Forms.CheckBox chkMaxValue;
        private System.Windows.Forms.CheckBox chkInteger;
        private System.Windows.Forms.GroupBox grpDateTimeStyle;
        private System.Windows.Forms.RadioButton rdoDate;
        private System.Windows.Forms.DateTimePicker dtpMinDate;
        private System.Windows.Forms.CheckBox chkMaxDateTime;
        private System.Windows.Forms.CheckBox chkMinDateTime;
        private System.Windows.Forms.DateTimePicker dtpMaxTime;
        private System.Windows.Forms.DateTimePicker dtpMinTime;
        private System.Windows.Forms.DateTimePicker dtpMaxDate;
        private System.Windows.Forms.GroupBox grpTimeStyle;
        private System.Windows.Forms.CheckBox chkMaxTime;
        private System.Windows.Forms.CheckBox chkMinTime;
        private System.Windows.Forms.DateTimePicker dtpMaxTime2;
        private System.Windows.Forms.DateTimePicker dtpMinTime2;
        private System.Windows.Forms.RadioButton rdoTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCustomMessage;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.TextBox txtMaxValue;
        private System.Windows.Forms.TextBox txtMinValue;
        private System.Windows.Forms.CheckBox chkMaxLength;
        private System.Windows.Forms.CheckBox chkMinLength;

    }
}