/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
namespace DCSoft.CSharpWriter.Commands
{
    partial class dlgElementTypeEditor
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
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.chkText = new System.Windows.Forms.CheckBox();
            this.chkField = new System.Windows.Forms.CheckBox();
            this.chkInputField = new System.Windows.Forms.CheckBox();
            this.chkTable = new System.Windows.Forms.CheckBox();
            this.chkObject = new System.Windows.Forms.CheckBox();
            this.chkLineBreak = new System.Windows.Forms.CheckBox();
            this.chkPageBreak = new System.Windows.Forms.CheckBox();
            this.chkParagraphFlag = new System.Windows.Forms.CheckBox();
            this.chkCheckBox = new System.Windows.Forms.CheckBox();
            this.chkImage = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(12, 12);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(72, 16);
            this.chkAll.TabIndex = 0;
            this.chkAll.Text = "所有类型";
            this.chkAll.UseVisualStyleBackColor = true;
            // 
            // chkText
            // 
            this.chkText.AutoSize = true;
            this.chkText.Location = new System.Drawing.Point(28, 34);
            this.chkText.Name = "chkText";
            this.chkText.Size = new System.Drawing.Size(72, 16);
            this.chkText.TabIndex = 0;
            this.chkText.Text = "文本类型";
            this.chkText.UseVisualStyleBackColor = true;
            // 
            // chkField
            // 
            this.chkField.AutoSize = true;
            this.chkField.Location = new System.Drawing.Point(28, 56);
            this.chkField.Name = "chkField";
            this.chkField.Size = new System.Drawing.Size(84, 16);
            this.chkField.TabIndex = 0;
            this.chkField.Text = "文本域类型";
            this.chkField.UseVisualStyleBackColor = true;
            // 
            // chkInputField
            // 
            this.chkInputField.AutoSize = true;
            this.chkInputField.Location = new System.Drawing.Point(28, 78);
            this.chkInputField.Name = "chkInputField";
            this.chkInputField.Size = new System.Drawing.Size(84, 16);
            this.chkInputField.TabIndex = 0;
            this.chkInputField.Text = "输入域类型";
            this.chkInputField.UseVisualStyleBackColor = true;
            // 
            // chkTable
            // 
            this.chkTable.AutoSize = true;
            this.chkTable.Location = new System.Drawing.Point(28, 100);
            this.chkTable.Name = "chkTable";
            this.chkTable.Size = new System.Drawing.Size(72, 16);
            this.chkTable.TabIndex = 0;
            this.chkTable.Text = "表格类型";
            this.chkTable.UseVisualStyleBackColor = true;
            // 
            // chkObject
            // 
            this.chkObject.AutoSize = true;
            this.chkObject.Location = new System.Drawing.Point(28, 122);
            this.chkObject.Name = "chkObject";
            this.chkObject.Size = new System.Drawing.Size(72, 16);
            this.chkObject.TabIndex = 0;
            this.chkObject.Text = "对象类型";
            this.chkObject.UseVisualStyleBackColor = true;
            // 
            // chkLineBreak
            // 
            this.chkLineBreak.AutoSize = true;
            this.chkLineBreak.Location = new System.Drawing.Point(139, 34);
            this.chkLineBreak.Name = "chkLineBreak";
            this.chkLineBreak.Size = new System.Drawing.Size(48, 16);
            this.chkLineBreak.TabIndex = 0;
            this.chkLineBreak.Text = "换行";
            this.chkLineBreak.UseVisualStyleBackColor = true;
            // 
            // chkPageBreak
            // 
            this.chkPageBreak.AutoSize = true;
            this.chkPageBreak.Location = new System.Drawing.Point(139, 56);
            this.chkPageBreak.Name = "chkPageBreak";
            this.chkPageBreak.Size = new System.Drawing.Size(60, 16);
            this.chkPageBreak.TabIndex = 0;
            this.chkPageBreak.Text = "分页符";
            this.chkPageBreak.UseVisualStyleBackColor = true;
            // 
            // chkParagraphFlag
            // 
            this.chkParagraphFlag.AutoSize = true;
            this.chkParagraphFlag.Location = new System.Drawing.Point(139, 78);
            this.chkParagraphFlag.Name = "chkParagraphFlag";
            this.chkParagraphFlag.Size = new System.Drawing.Size(48, 16);
            this.chkParagraphFlag.TabIndex = 0;
            this.chkParagraphFlag.Text = "段落";
            this.chkParagraphFlag.UseVisualStyleBackColor = true;
            // 
            // chkCheckBox
            // 
            this.chkCheckBox.AutoSize = true;
            this.chkCheckBox.Location = new System.Drawing.Point(139, 100);
            this.chkCheckBox.Name = "chkCheckBox";
            this.chkCheckBox.Size = new System.Drawing.Size(78, 16);
            this.chkCheckBox.TabIndex = 0;
            this.chkCheckBox.Text = "单/复选框";
            this.chkCheckBox.UseVisualStyleBackColor = true;
            // 
            // chkImage
            // 
            this.chkImage.AutoSize = true;
            this.chkImage.Location = new System.Drawing.Point(139, 122);
            this.chkImage.Name = "chkImage";
            this.chkImage.Size = new System.Drawing.Size(48, 16);
            this.chkImage.TabIndex = 0;
            this.chkImage.Text = "图片";
            this.chkImage.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(40, 170);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(139, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dlgElementTypeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 208);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkImage);
            this.Controls.Add(this.chkCheckBox);
            this.Controls.Add(this.chkParagraphFlag);
            this.Controls.Add(this.chkPageBreak);
            this.Controls.Add(this.chkLineBreak);
            this.Controls.Add(this.chkObject);
            this.Controls.Add(this.chkTable);
            this.Controls.Add(this.chkInputField);
            this.Controls.Add(this.chkField);
            this.Controls.Add(this.chkText);
            this.Controls.Add(this.chkAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgElementTypeEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "接受的元素类型";
            this.Load += new System.EventHandler(this.dlgElementTypeEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckBox chkText;
        private System.Windows.Forms.CheckBox chkField;
        private System.Windows.Forms.CheckBox chkInputField;
        private System.Windows.Forms.CheckBox chkTable;
        private System.Windows.Forms.CheckBox chkObject;
        private System.Windows.Forms.CheckBox chkLineBreak;
        private System.Windows.Forms.CheckBox chkPageBreak;
        private System.Windows.Forms.CheckBox chkParagraphFlag;
        private System.Windows.Forms.CheckBox chkCheckBox;
        private System.Windows.Forms.CheckBox chkImage;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}