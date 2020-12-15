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
    partial class dlgDocumentBorderBackground
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgDocumentBorderBackground));
            DCSoft.WinForms.ImageListBoxItem imageListBoxItem1 = new DCSoft.WinForms.ImageListBoxItem();
            DCSoft.WinForms.ImageListBoxItem imageListBoxItem2 = new DCSoft.WinForms.ImageListBoxItem();
            DCSoft.WinForms.ImageListBoxItem imageListBoxItem3 = new DCSoft.WinForms.ImageListBoxItem();
            DCSoft.WinForms.ImageListBoxItem imageListBoxItem4 = new DCSoft.WinForms.ImageListBoxItem();
            DCSoft.WinForms.ImageListBoxItem imageListBoxItem5 = new DCSoft.WinForms.ImageListBoxItem();
            DCSoft.WinForms.ImageListBoxItem imageListBoxItem6 = new DCSoft.WinForms.ImageListBoxItem();
            DCSoft.WinForms.ImageListBoxItem imageListBoxItem7 = new DCSoft.WinForms.ImageListBoxItem();
            DCSoft.WinForms.ImageListBoxItem imageListBoxItem8 = new DCSoft.WinForms.ImageListBoxItem();
            DCSoft.WinForms.ImageListBoxItem imageListBoxItem9 = new DCSoft.WinForms.ImageListBoxItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBorderWidth = new System.Windows.Forms.NumericUpDown();
            this.chkRightBorder = new System.Windows.Forms.CheckBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.chkCenterVerticalBorder = new System.Windows.Forms.CheckBox();
            this.chkLeftBorder = new System.Windows.Forms.CheckBox();
            this.chkBottomBorder = new System.Windows.Forms.CheckBox();
            this.chkMiddleHorizontalBorder = new System.Windows.Forms.CheckBox();
            this.chkTopBorder = new System.Windows.Forms.CheckBox();
            this.picBorderPreview = new System.Windows.Forms.PictureBox();
            this.btnBorderColor = new DCSoft.WinForms.ColorButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstBorderStyle = new DCSoft.WinForms.ImageListBox();
            this.lstBorderSettings = new DCSoft.WinForms.ImageListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboBorderApplyRange = new System.Windows.Forms.ComboBox();
            this.lblBorderApplyRange = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBackgroundColor = new DCSoft.WinForms.ColorButton();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBorderWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBorderPreview)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txtBorderWidth);
            this.groupBox1.Controls.Add(this.chkRightBorder);
            this.groupBox1.Controls.Add(this.chkCenterVerticalBorder);
            this.groupBox1.Controls.Add(this.chkLeftBorder);
            this.groupBox1.Controls.Add(this.chkBottomBorder);
            this.groupBox1.Controls.Add(this.chkMiddleHorizontalBorder);
            this.groupBox1.Controls.Add(this.chkTopBorder);
            this.groupBox1.Controls.Add(this.picBorderPreview);
            this.groupBox1.Controls.Add(this.btnBorderColor);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lstBorderStyle);
            this.groupBox1.Controls.Add(this.lstBorderSettings);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtBorderWidth
            // 
            resources.ApplyResources(this.txtBorderWidth, "txtBorderWidth");
            this.txtBorderWidth.Name = "txtBorderWidth";
            this.txtBorderWidth.ValueChanged += new System.EventHandler(this.txtBorderWidth_ValueChanged);
            // 
            // chkRightBorder
            // 
            resources.ApplyResources(this.chkRightBorder, "chkRightBorder");
            this.chkRightBorder.ImageList = this.imageList1;
            this.chkRightBorder.Name = "chkRightBorder";
            this.chkRightBorder.UseVisualStyleBackColor = true;
            this.chkRightBorder.CheckedChanged += new System.EventHandler(this.chkTopBorder_CheckedChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Red;
            this.imageList1.Images.SetKeyName(0, "BorderFlagBottom.bmp");
            this.imageList1.Images.SetKeyName(1, "BorderFlagCenterVertical.bmp");
            this.imageList1.Images.SetKeyName(2, "BorderFlagLeft.bmp");
            this.imageList1.Images.SetKeyName(3, "BorderFlagMiddleHorizontal.bmp");
            this.imageList1.Images.SetKeyName(4, "BorderFlagNone.bmp");
            this.imageList1.Images.SetKeyName(5, "BorderFlagRight.bmp");
            this.imageList1.Images.SetKeyName(6, "BorderFlagTop.bmp");
            // 
            // chkCenterVerticalBorder
            // 
            resources.ApplyResources(this.chkCenterVerticalBorder, "chkCenterVerticalBorder");
            this.chkCenterVerticalBorder.ImageList = this.imageList1;
            this.chkCenterVerticalBorder.Name = "chkCenterVerticalBorder";
            this.chkCenterVerticalBorder.UseVisualStyleBackColor = true;
            this.chkCenterVerticalBorder.CheckedChanged += new System.EventHandler(this.chkTopBorder_CheckedChanged);
            // 
            // chkLeftBorder
            // 
            resources.ApplyResources(this.chkLeftBorder, "chkLeftBorder");
            this.chkLeftBorder.ImageList = this.imageList1;
            this.chkLeftBorder.Name = "chkLeftBorder";
            this.chkLeftBorder.UseVisualStyleBackColor = true;
            this.chkLeftBorder.CheckedChanged += new System.EventHandler(this.chkTopBorder_CheckedChanged);
            // 
            // chkBottomBorder
            // 
            resources.ApplyResources(this.chkBottomBorder, "chkBottomBorder");
            this.chkBottomBorder.ImageList = this.imageList1;
            this.chkBottomBorder.Name = "chkBottomBorder";
            this.chkBottomBorder.UseVisualStyleBackColor = true;
            this.chkBottomBorder.CheckedChanged += new System.EventHandler(this.chkTopBorder_CheckedChanged);
            // 
            // chkMiddleHorizontalBorder
            // 
            resources.ApplyResources(this.chkMiddleHorizontalBorder, "chkMiddleHorizontalBorder");
            this.chkMiddleHorizontalBorder.ImageList = this.imageList1;
            this.chkMiddleHorizontalBorder.Name = "chkMiddleHorizontalBorder";
            this.chkMiddleHorizontalBorder.UseVisualStyleBackColor = true;
            this.chkMiddleHorizontalBorder.CheckedChanged += new System.EventHandler(this.chkTopBorder_CheckedChanged);
            // 
            // chkTopBorder
            // 
            resources.ApplyResources(this.chkTopBorder, "chkTopBorder");
            this.chkTopBorder.ImageList = this.imageList1;
            this.chkTopBorder.Name = "chkTopBorder";
            this.chkTopBorder.UseVisualStyleBackColor = true;
            this.chkTopBorder.CheckedChanged += new System.EventHandler(this.chkTopBorder_CheckedChanged);
            // 
            // picBorderPreview
            // 
            resources.ApplyResources(this.picBorderPreview, "picBorderPreview");
            this.picBorderPreview.BackColor = System.Drawing.Color.White;
            this.picBorderPreview.Name = "picBorderPreview";
            this.picBorderPreview.TabStop = false;
            this.picBorderPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.picBorderPreview_Paint);
            // 
            // btnBorderColor
            // 
            resources.ApplyResources(this.btnBorderColor, "btnBorderColor");
            this.btnBorderColor.Name = "btnBorderColor";
            this.btnBorderColor.UseVisualStyleBackColor = true;
            this.btnBorderColor.ValueChanged += new System.EventHandler(this.btnBorderColor_ValueChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lstBorderStyle
            // 
            resources.ApplyResources(this.lstBorderStyle, "lstBorderStyle");
            this.lstBorderStyle.BackColor = System.Drawing.SystemColors.Window;
            this.lstBorderStyle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            imageListBoxItem1.Image = ((System.Drawing.Image)(resources.GetObject("imageListBoxItem1.Image")));
            imageListBoxItem1.ImageIndex = 0;
            resources.ApplyResources(imageListBoxItem1, "imageListBoxItem1");
            imageListBoxItem2.Image = ((System.Drawing.Image)(resources.GetObject("imageListBoxItem2.Image")));
            imageListBoxItem2.ImageIndex = 0;
            resources.ApplyResources(imageListBoxItem2, "imageListBoxItem2");
            imageListBoxItem3.Image = ((System.Drawing.Image)(resources.GetObject("imageListBoxItem3.Image")));
            imageListBoxItem3.ImageIndex = 0;
            resources.ApplyResources(imageListBoxItem3, "imageListBoxItem3");
            imageListBoxItem4.Image = ((System.Drawing.Image)(resources.GetObject("imageListBoxItem4.Image")));
            imageListBoxItem4.ImageIndex = 0;
            resources.ApplyResources(imageListBoxItem4, "imageListBoxItem4");
            imageListBoxItem5.Image = ((System.Drawing.Image)(resources.GetObject("imageListBoxItem5.Image")));
            imageListBoxItem5.ImageIndex = 0;
            resources.ApplyResources(imageListBoxItem5, "imageListBoxItem5");
            this.lstBorderStyle.Items.Add(imageListBoxItem1);
            this.lstBorderStyle.Items.Add(imageListBoxItem2);
            this.lstBorderStyle.Items.Add(imageListBoxItem3);
            this.lstBorderStyle.Items.Add(imageListBoxItem4);
            this.lstBorderStyle.Items.Add(imageListBoxItem5);
            this.lstBorderStyle.Name = "lstBorderStyle";
            this.lstBorderStyle.SelectedIndexChagned += new System.EventHandler(this.lstBorderStyle_SelectedIndexChagned);
            // 
            // lstBorderSettings
            // 
            resources.ApplyResources(this.lstBorderSettings, "lstBorderSettings");
            this.lstBorderSettings.BackColor = System.Drawing.SystemColors.Window;
            this.lstBorderSettings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            imageListBoxItem6.Image = ((System.Drawing.Image)(resources.GetObject("imageListBoxItem6.Image")));
            imageListBoxItem6.ImageIndex = 0;
            resources.ApplyResources(imageListBoxItem6, "imageListBoxItem6");
            imageListBoxItem7.Image = ((System.Drawing.Image)(resources.GetObject("imageListBoxItem7.Image")));
            imageListBoxItem7.ImageIndex = 0;
            resources.ApplyResources(imageListBoxItem7, "imageListBoxItem7");
            imageListBoxItem8.Image = ((System.Drawing.Image)(resources.GetObject("imageListBoxItem8.Image")));
            imageListBoxItem8.ImageIndex = 0;
            resources.ApplyResources(imageListBoxItem8, "imageListBoxItem8");
            imageListBoxItem9.Image = ((System.Drawing.Image)(resources.GetObject("imageListBoxItem9.Image")));
            imageListBoxItem9.ImageIndex = 0;
            resources.ApplyResources(imageListBoxItem9, "imageListBoxItem9");
            this.lstBorderSettings.Items.Add(imageListBoxItem6);
            this.lstBorderSettings.Items.Add(imageListBoxItem7);
            this.lstBorderSettings.Items.Add(imageListBoxItem8);
            this.lstBorderSettings.Items.Add(imageListBoxItem9);
            this.lstBorderSettings.Name = "lstBorderSettings";
            this.lstBorderSettings.SelectedIndexChagned += new System.EventHandler(this.lstBorderSettings_SelectedIndexChagned);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cboBorderApplyRange
            // 
            resources.ApplyResources(this.cboBorderApplyRange, "cboBorderApplyRange");
            this.cboBorderApplyRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBorderApplyRange.FormattingEnabled = true;
            this.cboBorderApplyRange.Name = "cboBorderApplyRange";
            this.cboBorderApplyRange.SelectedIndexChanged += new System.EventHandler(this.cboBorderApplyRange_SelectedIndexChanged);
            // 
            // lblBorderApplyRange
            // 
            resources.ApplyResources(this.lblBorderApplyRange, "lblBorderApplyRange");
            this.lblBorderApplyRange.Name = "lblBorderApplyRange";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.btnBackgroundColor);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // btnBackgroundColor
            // 
            resources.ApplyResources(this.btnBackgroundColor, "btnBackgroundColor");
            this.btnBackgroundColor.Name = "btnBackgroundColor";
            this.btnBackgroundColor.UseVisualStyleBackColor = true;
            this.btnBackgroundColor.ValueChanged += new System.EventHandler(this.btnBackgroundColor_ValueChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dlgDocumentBorderBackground
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cboBorderApplyRange);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblBorderApplyRange);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgDocumentBorderBackground";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.dlgBorderBackground_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBorderWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBorderPreview)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private WinForms.ImageListBox lstBorderSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private WinForms.ImageListBox lstBorderStyle;
        private DCSoft.WinForms.ColorButton btnBorderColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkTopBorder;
        private System.Windows.Forms.PictureBox picBorderPreview;
        private System.Windows.Forms.CheckBox chkRightBorder;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox chkCenterVerticalBorder;
        private System.Windows.Forms.CheckBox chkLeftBorder;
        private System.Windows.Forms.CheckBox chkBottomBorder;
        private System.Windows.Forms.CheckBox chkMiddleHorizontalBorder;
        private System.Windows.Forms.ComboBox cboBorderApplyRange;
        private System.Windows.Forms.Label lblBorderApplyRange;
        private System.Windows.Forms.GroupBox groupBox2;
        private DCSoft.WinForms.ColorButton btnBackgroundColor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown txtBorderWidth;
        private System.Windows.Forms.Label label7;
    }
}