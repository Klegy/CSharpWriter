namespace DCSoft.CSharpWriter.Commands
{
    partial class dlgCheckBoxElementEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkCheckBox = new System.Windows.Forms.CheckBox();
            this.rdoRadio = new System.Windows.Forms.RadioButton();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkDeleteable = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTag = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTooltip = new System.Windows.Forms.TextBox();
            this.btnBrowseEventExpressions = new System.Windows.Forms.Button();
            this.txtEventExpressions = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "名称：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(73, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(233, 21);
            this.txtName.TabIndex = 20;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkCheckBox);
            this.groupBox1.Controls.Add(this.rdoRadio);
            this.groupBox1.Location = new System.Drawing.Point(14, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 43);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "样式";
            // 
            // chkCheckBox
            // 
            this.chkCheckBox.AutoSize = true;
            this.chkCheckBox.Location = new System.Drawing.Point(107, 20);
            this.chkCheckBox.Name = "chkCheckBox";
            this.chkCheckBox.Size = new System.Drawing.Size(60, 16);
            this.chkCheckBox.TabIndex = 70;
            this.chkCheckBox.Text = "复选框";
            this.chkCheckBox.UseVisualStyleBackColor = true;
            // 
            // rdoRadio
            // 
            this.rdoRadio.AutoSize = true;
            this.rdoRadio.Location = new System.Drawing.Point(20, 20);
            this.rdoRadio.Name = "rdoRadio";
            this.rdoRadio.Size = new System.Drawing.Size(59, 16);
            this.rdoRadio.TabIndex = 60;
            this.rdoRadio.TabStop = true;
            this.rdoRadio.Text = "单选框";
            this.rdoRadio.UseVisualStyleBackColor = true;
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(14, 132);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(96, 16);
            this.chkChecked.TabIndex = 80;
            this.chkChecked.Text = "处于选择状态";
            this.chkChecked.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 110;
            this.label2.Text = "数值：";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(112, 161);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(194, 21);
            this.txtValue.TabIndex = 120;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(151, 333);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 240;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(234, 333);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 250;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkDeleteable
            // 
            this.chkDeleteable.AutoSize = true;
            this.chkDeleteable.Location = new System.Drawing.Point(234, 132);
            this.chkDeleteable.Name = "chkDeleteable";
            this.chkDeleteable.Size = new System.Drawing.Size(72, 16);
            this.chkDeleteable.TabIndex = 100;
            this.chkDeleteable.Text = "可以删除";
            this.chkDeleteable.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 30;
            this.label3.Text = "分组：";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(73, 33);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(233, 21);
            this.txtGroupName.TabIndex = 40;
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(121, 132);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(96, 16);
            this.chkEnabled.TabIndex = 90;
            this.chkEnabled.Text = "对象是否可用";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 130;
            this.label4.Text = "附加数据：";
            // 
            // txtTag
            // 
            this.txtTag.Location = new System.Drawing.Point(112, 188);
            this.txtTag.Name = "txtTag";
            this.txtTag.Size = new System.Drawing.Size(194, 21);
            this.txtTag.TabIndex = 140;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 150;
            this.label5.Text = "提示文本：";
            // 
            // txtTooltip
            // 
            this.txtTooltip.Location = new System.Drawing.Point(112, 215);
            this.txtTooltip.Name = "txtTooltip";
            this.txtTooltip.Size = new System.Drawing.Size(194, 21);
            this.txtTooltip.TabIndex = 160;
            // 
            // btnBrowseEventExpressions
            // 
            this.btnBrowseEventExpressions.Location = new System.Drawing.Point(312, 242);
            this.btnBrowseEventExpressions.Name = "btnBrowseEventExpressions";
            this.btnBrowseEventExpressions.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseEventExpressions.TabIndex = 190;
            this.btnBrowseEventExpressions.Text = "浏览";
            this.btnBrowseEventExpressions.UseVisualStyleBackColor = true;
            // 
            // txtEventExpressions
            // 
            this.txtEventExpressions.Location = new System.Drawing.Point(112, 242);
            this.txtEventExpressions.Name = "txtEventExpressions";
            this.txtEventExpressions.ReadOnly = true;
            this.txtEventExpressions.Size = new System.Drawing.Size(194, 21);
            this.txtEventExpressions.TabIndex = 180;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 246);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 12);
            this.label13.TabIndex = 170;
            this.label13.Text = "事件表达式：";
            // 
            // dlgCheckBoxElementEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 363);
            this.Controls.Add(this.btnBrowseEventExpressions);
            this.Controls.Add(this.txtEventExpressions);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.chkDeleteable);
            this.Controls.Add(this.chkChecked);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtTooltip);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTag);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtGroupName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgCheckBoxElementEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "复选框属性";
            this.Load += new System.EventHandler(this.dlgCheckBoxElementProperties_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkCheckBox;
        private System.Windows.Forms.RadioButton rdoRadio;
        private System.Windows.Forms.CheckBox chkChecked;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkDeleteable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTag;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTooltip;
        private System.Windows.Forms.Button btnBrowseEventExpressions;
        private System.Windows.Forms.TextBox txtEventExpressions;
        private System.Windows.Forms.Label label13;
    }
}