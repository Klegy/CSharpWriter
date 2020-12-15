/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
namespace DCSoft.CSharpWriter.Commands
{
    partial class dlgParagraphFormatcs
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
            this.txtLeftIndent = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFirstLineIndent = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSpacingBefore = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSpacingAfter = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLineSpacing = new System.Windows.Forms.NumericUpDown();
            this.cboLineSpacingStyle = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblBang = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtLeftIndent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstLineIndent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpacingBefore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpacingAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLineSpacing)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "Left indent:            chars";
            // 
            // txtLeftIndent
            // 
            this.txtLeftIndent.DecimalPlaces = 1;
            this.txtLeftIndent.Location = new System.Drawing.Point(97, 16);
            this.txtLeftIndent.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtLeftIndent.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.txtLeftIndent.Name = "txtLeftIndent";
            this.txtLeftIndent.Size = new System.Drawing.Size(56, 21);
            this.txtLeftIndent.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "First indent:           CM";
            // 
            // txtFirstLineIndent
            // 
            this.txtFirstLineIndent.DecimalPlaces = 2;
            this.txtFirstLineIndent.Location = new System.Drawing.Point(289, 16);
            this.txtFirstLineIndent.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtFirstLineIndent.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.txtFirstLineIndent.Name = "txtFirstLineIndent";
            this.txtFirstLineIndent.Size = new System.Drawing.Size(54, 21);
            this.txtFirstLineIndent.TabIndex = 40;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 12);
            this.label3.TabIndex = 50;
            this.label3.Text = "Space before:           lines";
            // 
            // txtSpacingBefore
            // 
            this.txtSpacingBefore.DecimalPlaces = 1;
            this.txtSpacingBefore.Location = new System.Drawing.Point(97, 53);
            this.txtSpacingBefore.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtSpacingBefore.Name = "txtSpacingBefore";
            this.txtSpacingBefore.Size = new System.Drawing.Size(56, 21);
            this.txtSpacingBefore.TabIndex = 60;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(204, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 12);
            this.label4.TabIndex = 70;
            this.label4.Text = "Space after:            lines";
            // 
            // txtSpacingAfter
            // 
            this.txtSpacingAfter.DecimalPlaces = 1;
            this.txtSpacingAfter.Location = new System.Drawing.Point(289, 53);
            this.txtSpacingAfter.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtSpacingAfter.Name = "txtSpacingAfter";
            this.txtSpacingAfter.Size = new System.Drawing.Size(54, 21);
            this.txtSpacingAfter.TabIndex = 80;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 90;
            this.label5.Text = "LS:";
            // 
            // txtLineSpacing
            // 
            this.txtLineSpacing.DecimalPlaces = 2;
            this.txtLineSpacing.Location = new System.Drawing.Point(218, 98);
            this.txtLineSpacing.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtLineSpacing.Name = "txtLineSpacing";
            this.txtLineSpacing.Size = new System.Drawing.Size(55, 21);
            this.txtLineSpacing.TabIndex = 120;
            // 
            // cboLineSpacingStyle
            // 
            this.cboLineSpacingStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLineSpacingStyle.FormattingEnabled = true;
            this.cboLineSpacingStyle.Items.AddRange(new object[] {
            "Simple line",
            "1.5 spacing",
            "2 spacing",
            "Min",
            "Fixed",
            "Multi-spacing"});
            this.cboLineSpacingStyle.Location = new System.Drawing.Point(49, 98);
            this.cboLineSpacingStyle.Name = "cboLineSpacingStyle";
            this.cboLineSpacingStyle.Size = new System.Drawing.Size(111, 20);
            this.cboLineSpacingStyle.TabIndex = 100;
            this.cboLineSpacingStyle.SelectedIndexChanged += new System.EventHandler(this.cboLineSpacingStyle_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(166, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 110;
            this.label6.Text = "Value:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(135, 157);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 130;
            this.btnOK.Text = "&Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(246, 157);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 140;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblBang
            // 
            this.lblBang.AutoSize = true;
            this.lblBang.Location = new System.Drawing.Point(279, 102);
            this.lblBang.Name = "lblBang";
            this.lblBang.Size = new System.Drawing.Size(17, 12);
            this.lblBang.TabIndex = 141;
            this.lblBang.Text = "Pa";
            this.lblBang.Visible = false;
            // 
            // dlgParagraphFormatcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 194);
            this.Controls.Add(this.lblBang);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cboLineSpacingStyle);
            this.Controls.Add(this.txtLineSpacing);
            this.Controls.Add(this.txtSpacingAfter);
            this.Controls.Add(this.txtSpacingBefore);
            this.Controls.Add(this.txtFirstLineIndent);
            this.Controls.Add(this.txtLeftIndent);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgParagraphFormatcs";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Paragraph format";
            this.Load += new System.EventHandler(this.dlgParagraphFormatcs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtLeftIndent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstLineIndent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpacingBefore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpacingAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLineSpacing)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtLeftIndent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtFirstLineIndent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtSpacingBefore;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txtSpacingAfter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown txtLineSpacing;
        private System.Windows.Forms.ComboBox cboLineSpacingStyle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblBang;
    }
}