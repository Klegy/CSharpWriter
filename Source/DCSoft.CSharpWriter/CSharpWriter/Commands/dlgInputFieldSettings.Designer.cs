namespace DCSoft.CSharpWriter.Commands
{
    partial class dlgInputFieldSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rdoText = new System.Windows.Forms.RadioButton();
            this.rdoDropdownList = new System.Windows.Forms.RadioButton();
            this.grbDropdownList = new System.Windows.Forms.GroupBox();
            this.chkMultiSelect = new System.Windows.Forms.CheckBox();
            this.rdoDate = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rdoDateTime = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtListSource = new System.Windows.Forms.TextBox();
            this.btnBrowseListSource = new System.Windows.Forms.Button();
            this.grbDropdownList.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdoText
            // 
            this.rdoText.AutoSize = true;
            this.rdoText.Location = new System.Drawing.Point(12, 12);
            this.rdoText.Name = "rdoText";
            this.rdoText.Size = new System.Drawing.Size(119, 16);
            this.rdoText.TabIndex = 50;
            this.rdoText.TabStop = true;
            this.rdoText.Text = "直接输入文本数据";
            this.rdoText.UseVisualStyleBackColor = true;
            // 
            // rdoDropdownList
            // 
            this.rdoDropdownList.AutoSize = true;
            this.rdoDropdownList.Location = new System.Drawing.Point(12, 34);
            this.rdoDropdownList.Name = "rdoDropdownList";
            this.rdoDropdownList.Size = new System.Drawing.Size(95, 16);
            this.rdoDropdownList.TabIndex = 60;
            this.rdoDropdownList.TabStop = true;
            this.rdoDropdownList.Text = "下拉列表方式";
            this.rdoDropdownList.UseVisualStyleBackColor = true;
            this.rdoDropdownList.CheckedChanged += new System.EventHandler(this.rdoDropdownList_CheckedChanged);
            // 
            // grbDropdownList
            // 
            this.grbDropdownList.Controls.Add(this.btnBrowseListSource);
            this.grbDropdownList.Controls.Add(this.txtListSource);
            this.grbDropdownList.Controls.Add(this.label1);
            this.grbDropdownList.Controls.Add(this.chkMultiSelect);
            this.grbDropdownList.Location = new System.Drawing.Point(3, 35);
            this.grbDropdownList.Name = "grbDropdownList";
            this.grbDropdownList.Size = new System.Drawing.Size(478, 90);
            this.grbDropdownList.TabIndex = 3;
            this.grbDropdownList.TabStop = false;
            // 
            // chkMultiSelect
            // 
            this.chkMultiSelect.AutoSize = true;
            this.chkMultiSelect.Location = new System.Drawing.Point(9, 21);
            this.chkMultiSelect.Name = "chkMultiSelect";
            this.chkMultiSelect.Size = new System.Drawing.Size(72, 16);
            this.chkMultiSelect.TabIndex = 70;
            this.chkMultiSelect.Text = "允许多选";
            this.chkMultiSelect.UseVisualStyleBackColor = true;
            // 
            // rdoDate
            // 
            this.rdoDate.AutoSize = true;
            this.rdoDate.Location = new System.Drawing.Point(12, 143);
            this.rdoDate.Name = "rdoDate";
            this.rdoDate.Size = new System.Drawing.Size(173, 16);
            this.rdoDate.TabIndex = 100;
            this.rdoDate.TabStop = true;
            this.rdoDate.Text = "日期格式，例如2012-5-15。";
            this.rdoDate.UseVisualStyleBackColor = true;
            this.rdoDate.CheckedChanged += new System.EventHandler(this.rdoDateTime_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(192, 210);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 130;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(287, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 140;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rdoDateTime
            // 
            this.rdoDateTime.AutoSize = true;
            this.rdoDateTime.Location = new System.Drawing.Point(12, 174);
            this.rdoDateTime.Name = "rdoDateTime";
            this.rdoDateTime.Size = new System.Drawing.Size(251, 16);
            this.rdoDateTime.TabIndex = 141;
            this.rdoDateTime.TabStop = true;
            this.rdoDateTime.Text = "日期时间格式，例如2012-5-15 16:35:19。";
            this.rdoDateTime.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 71;
            this.label1.Text = "列表内容:";
            // 
            // txtListSource
            // 
            this.txtListSource.Location = new System.Drawing.Point(74, 48);
            this.txtListSource.Name = "txtListSource";
            this.txtListSource.ReadOnly = true;
            this.txtListSource.Size = new System.Drawing.Size(309, 21);
            this.txtListSource.TabIndex = 72;
            // 
            // btnBrowseListSource
            // 
            this.btnBrowseListSource.Location = new System.Drawing.Point(389, 46);
            this.btnBrowseListSource.Name = "btnBrowseListSource";
            this.btnBrowseListSource.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseListSource.TabIndex = 73;
            this.btnBrowseListSource.Text = "浏览";
            this.btnBrowseListSource.UseVisualStyleBackColor = true;
            this.btnBrowseListSource.Click += new System.EventHandler(this.btnBrowseListSource_Click);
            // 
            // dlgInputFieldSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 242);
            this.Controls.Add(this.rdoDateTime);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.rdoDate);
            this.Controls.Add(this.rdoDropdownList);
            this.Controls.Add(this.rdoText);
            this.Controls.Add(this.grbDropdownList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgInputFieldSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "输入域设置";
            this.Load += new System.EventHandler(this.dlgInputFieldSettings_Load);
            this.grbDropdownList.ResumeLayout(false);
            this.grbDropdownList.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoText;
        private System.Windows.Forms.RadioButton rdoDropdownList;
        private System.Windows.Forms.GroupBox grbDropdownList;
        private System.Windows.Forms.CheckBox chkMultiSelect;
        private System.Windows.Forms.RadioButton rdoDate;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rdoDateTime;
        private System.Windows.Forms.Button btnBrowseListSource;
        private System.Windows.Forms.TextBox txtListSource;
        private System.Windows.Forms.Label label1;
    }
}