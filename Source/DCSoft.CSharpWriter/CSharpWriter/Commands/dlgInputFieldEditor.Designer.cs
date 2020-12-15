namespace DCSoft.CSharpWriter.Commands
{
    partial class dlgInputFieldEditor
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
            this.chkReadonly = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtValidate = new System.Windows.Forms.TextBox();
            this.btnBrowseValidate = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSettings = new System.Windows.Forms.TextBox();
            this.btnBrowseSettings = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBinding = new System.Windows.Forms.TextBox();
            this.btnBrowseBinding = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAttributes = new System.Windows.Forms.TextBox();
            this.btnBrowseAttributes = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDisplayFormat = new System.Windows.Forms.TextBox();
            this.btnBrowseDisplayFormat = new System.Windows.Forms.Button();
            this.chkUserEditable = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtToolTip = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBackgroundText = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAcceptElementType = new System.Windows.Forms.TextBox();
            this.btnBrowseAcceptElementType = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.cboEnableHighlight = new System.Windows.Forms.ComboBox();
            this.chkMultiParagraph = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSpecifyWidth = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpecifyWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "名称:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(97, 32);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(253, 21);
            this.txtName.TabIndex = 20;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // chkReadonly
            // 
            this.chkReadonly.AutoSize = true;
            this.chkReadonly.Location = new System.Drawing.Point(8, 148);
            this.chkReadonly.Name = "chkReadonly";
            this.chkReadonly.Size = new System.Drawing.Size(72, 16);
            this.chkReadonly.TabIndex = 30;
            this.chkReadonly.Text = "内容只读";
            this.chkReadonly.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 70;
            this.label2.Text = "数据校验格式:";
            // 
            // txtValidate
            // 
            this.txtValidate.Location = new System.Drawing.Point(97, 212);
            this.txtValidate.Name = "txtValidate";
            this.txtValidate.ReadOnly = true;
            this.txtValidate.Size = new System.Drawing.Size(172, 21);
            this.txtValidate.TabIndex = 80;
            // 
            // btnBrowseValidate
            // 
            this.btnBrowseValidate.Location = new System.Drawing.Point(275, 208);
            this.btnBrowseValidate.Name = "btnBrowseValidate";
            this.btnBrowseValidate.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseValidate.TabIndex = 90;
            this.btnBrowseValidate.Text = "浏览";
            this.btnBrowseValidate.UseVisualStyleBackColor = true;
            this.btnBrowseValidate.Click += new System.EventHandler(this.btnBrowseFormat_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(184, 435);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 220;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(275, 435);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 230;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 40;
            this.label3.Text = "输入样式:";
            // 
            // txtSettings
            // 
            this.txtSettings.Location = new System.Drawing.Point(97, 187);
            this.txtSettings.Name = "txtSettings";
            this.txtSettings.ReadOnly = true;
            this.txtSettings.Size = new System.Drawing.Size(172, 21);
            this.txtSettings.TabIndex = 50;
            // 
            // btnBrowseSettings
            // 
            this.btnBrowseSettings.Location = new System.Drawing.Point(275, 182);
            this.btnBrowseSettings.Name = "btnBrowseSettings";
            this.btnBrowseSettings.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseSettings.TabIndex = 60;
            this.btnBrowseSettings.Text = "浏览";
            this.btnBrowseSettings.UseVisualStyleBackColor = true;
            this.btnBrowseSettings.Click += new System.EventHandler(this.btnBrowseSettings_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 100;
            this.label4.Text = "绑定数据源:";
            // 
            // txtBinding
            // 
            this.txtBinding.Location = new System.Drawing.Point(97, 237);
            this.txtBinding.Name = "txtBinding";
            this.txtBinding.ReadOnly = true;
            this.txtBinding.Size = new System.Drawing.Size(172, 21);
            this.txtBinding.TabIndex = 110;
            // 
            // btnBrowseBinding
            // 
            this.btnBrowseBinding.Location = new System.Drawing.Point(275, 234);
            this.btnBrowseBinding.Name = "btnBrowseBinding";
            this.btnBrowseBinding.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseBinding.TabIndex = 120;
            this.btnBrowseBinding.Text = "浏览";
            this.btnBrowseBinding.UseVisualStyleBackColor = true;
            this.btnBrowseBinding.Click += new System.EventHandler(this.btnBrowseBinding_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 265);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 130;
            this.label5.Text = "自定义属性:";
            // 
            // txtAttributes
            // 
            this.txtAttributes.Location = new System.Drawing.Point(97, 262);
            this.txtAttributes.Name = "txtAttributes";
            this.txtAttributes.ReadOnly = true;
            this.txtAttributes.Size = new System.Drawing.Size(172, 21);
            this.txtAttributes.TabIndex = 140;
            // 
            // btnBrowseAttributes
            // 
            this.btnBrowseAttributes.Location = new System.Drawing.Point(275, 260);
            this.btnBrowseAttributes.Name = "btnBrowseAttributes";
            this.btnBrowseAttributes.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseAttributes.TabIndex = 150;
            this.btnBrowseAttributes.Text = "浏览";
            this.btnBrowseAttributes.UseVisualStyleBackColor = true;
            this.btnBrowseAttributes.Click += new System.EventHandler(this.btnBrowseAttributes_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "编号:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(97, 5);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(253, 21);
            this.txtID.TabIndex = 6;
            this.txtID.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 291);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 160;
            this.label7.Text = "输出格式:";
            // 
            // txtDisplayFormat
            // 
            this.txtDisplayFormat.Location = new System.Drawing.Point(97, 287);
            this.txtDisplayFormat.Name = "txtDisplayFormat";
            this.txtDisplayFormat.ReadOnly = true;
            this.txtDisplayFormat.Size = new System.Drawing.Size(172, 21);
            this.txtDisplayFormat.TabIndex = 170;
            // 
            // btnBrowseDisplayFormat
            // 
            this.btnBrowseDisplayFormat.Location = new System.Drawing.Point(275, 286);
            this.btnBrowseDisplayFormat.Name = "btnBrowseDisplayFormat";
            this.btnBrowseDisplayFormat.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseDisplayFormat.TabIndex = 180;
            this.btnBrowseDisplayFormat.Text = "浏览";
            this.btnBrowseDisplayFormat.UseVisualStyleBackColor = true;
            this.btnBrowseDisplayFormat.Click += new System.EventHandler(this.btnBrowseDisplayFormat_Click);
            // 
            // chkUserEditable
            // 
            this.chkUserEditable.AutoSize = true;
            this.chkUserEditable.Location = new System.Drawing.Point(86, 148);
            this.chkUserEditable.Name = "chkUserEditable";
            this.chkUserEditable.Size = new System.Drawing.Size(168, 16);
            this.chkUserEditable.TabIndex = 35;
            this.chkUserEditable.Text = "用户可以直接编辑修改内容";
            this.chkUserEditable.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "提示文本:";
            // 
            // txtToolTip
            // 
            this.txtToolTip.Location = new System.Drawing.Point(97, 59);
            this.txtToolTip.Name = "txtToolTip";
            this.txtToolTip.Size = new System.Drawing.Size(253, 21);
            this.txtToolTip.TabIndex = 22;
            this.txtToolTip.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "背景文本:";
            // 
            // txtBackgroundText
            // 
            this.txtBackgroundText.Location = new System.Drawing.Point(97, 86);
            this.txtBackgroundText.Name = "txtBackgroundText";
            this.txtBackgroundText.Size = new System.Drawing.Size(253, 21);
            this.txtBackgroundText.TabIndex = 24;
            this.txtBackgroundText.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 317);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 12);
            this.label10.TabIndex = 190;
            this.label10.Text = "可包含的内容:";
            // 
            // txtAcceptElementType
            // 
            this.txtAcceptElementType.Location = new System.Drawing.Point(97, 312);
            this.txtAcceptElementType.Name = "txtAcceptElementType";
            this.txtAcceptElementType.ReadOnly = true;
            this.txtAcceptElementType.Size = new System.Drawing.Size(172, 21);
            this.txtAcceptElementType.TabIndex = 200;
            // 
            // btnBrowseAcceptElementType
            // 
            this.btnBrowseAcceptElementType.Location = new System.Drawing.Point(275, 312);
            this.btnBrowseAcceptElementType.Name = "btnBrowseAcceptElementType";
            this.btnBrowseAcceptElementType.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseAcceptElementType.TabIndex = 210;
            this.btnBrowseAcceptElementType.Text = "浏览";
            this.btnBrowseAcceptElementType.UseVisualStyleBackColor = true;
            this.btnBrowseAcceptElementType.Click += new System.EventHandler(this.btnBrowseAcceptElementType_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 372);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 12);
            this.label12.TabIndex = 190;
            this.label12.Text = "高亮度状态:";
            // 
            // cboEnableHighlight
            // 
            this.cboEnableHighlight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEnableHighlight.FormattingEnabled = true;
            this.cboEnableHighlight.Items.AddRange(new object[] {
            "默认",
            "允许",
            "禁止"});
            this.cboEnableHighlight.Location = new System.Drawing.Point(97, 369);
            this.cboEnableHighlight.Name = "cboEnableHighlight";
            this.cboEnableHighlight.Size = new System.Drawing.Size(172, 20);
            this.cboEnableHighlight.TabIndex = 231;
            // 
            // chkMultiParagraph
            // 
            this.chkMultiParagraph.AutoSize = true;
            this.chkMultiParagraph.Location = new System.Drawing.Point(266, 148);
            this.chkMultiParagraph.Name = "chkMultiParagraph";
            this.chkMultiParagraph.Size = new System.Drawing.Size(84, 16);
            this.chkMultiParagraph.TabIndex = 30;
            this.chkMultiParagraph.Text = "允许多段落";
            this.chkMultiParagraph.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 395);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 12);
            this.label14.TabIndex = 190;
            this.label14.Text = "固定宽度:";
            // 
            // txtSpecifyWidth
            // 
            this.txtSpecifyWidth.Location = new System.Drawing.Point(97, 395);
            this.txtSpecifyWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtSpecifyWidth.Name = "txtSpecifyWidth";
            this.txtSpecifyWidth.Size = new System.Drawing.Size(172, 21);
            this.txtSpecifyWidth.TabIndex = 232;
            // 
            // dlgInputFieldEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 473);
            this.Controls.Add(this.txtSpecifyWidth);
            this.Controls.Add(this.cboEnableHighlight);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnBrowseSettings);
            this.Controls.Add(this.btnBrowseAcceptElementType);
            this.Controls.Add(this.btnBrowseDisplayFormat);
            this.Controls.Add(this.btnBrowseAttributes);
            this.Controls.Add(this.btnBrowseBinding);
            this.Controls.Add(this.btnBrowseValidate);
            this.Controls.Add(this.chkUserEditable);
            this.Controls.Add(this.chkMultiParagraph);
            this.Controls.Add(this.chkReadonly);
            this.Controls.Add(this.txtSettings);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAcceptElementType);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtDisplayFormat);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtAttributes);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBinding);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtValidate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtBackgroundText);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtToolTip);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgInputFieldEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文本输入域属性";
            this.Load += new System.EventHandler(this.dlgInputFieldProperties_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtSpecifyWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox chkReadonly;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtValidate;
        private System.Windows.Forms.Button btnBrowseValidate;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSettings;
        private System.Windows.Forms.Button btnBrowseSettings;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBinding;
        private System.Windows.Forms.Button btnBrowseBinding;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAttributes;
        private System.Windows.Forms.Button btnBrowseAttributes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDisplayFormat;
        private System.Windows.Forms.Button btnBrowseDisplayFormat;
        private System.Windows.Forms.CheckBox chkUserEditable;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtToolTip;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBackgroundText;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtAcceptElementType;
        private System.Windows.Forms.Button btnBrowseAcceptElementType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboEnableHighlight;
        private System.Windows.Forms.CheckBox chkMultiParagraph;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown txtSpecifyWidth;
    }
}