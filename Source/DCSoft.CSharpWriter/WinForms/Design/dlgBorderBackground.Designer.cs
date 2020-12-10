/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
namespace DCSoft.WinForms.Design
{
    partial class dlgBorderBackground
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
            DCSoft.WinForms.ImageListBoxItem imageListBoxItem1 = new DCSoft.WinForms.ImageListBoxItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgBorderBackground));
            DCSoft.WinForms.ImageListBoxItem imageListBoxItem2 = new DCSoft.WinForms.ImageListBoxItem();
            DCSoft.WinForms.ImageListBoxItem imageListBoxItem3 = new DCSoft.WinForms.ImageListBoxItem();
            DCSoft.WinForms.ImageListBoxItem imageListBoxItem4 = new DCSoft.WinForms.ImageListBoxItem();
            DCSoft.WinForms.ImageListBoxItem imageListBoxItem5 = new DCSoft.WinForms.ImageListBoxItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBackgroundColor = new DCSoft.WinForms.ColorButton();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBorderWidth = new System.Windows.Forms.NumericUpDown();
            this.btnBorderColor = new DCSoft.WinForms.ColorButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstBorderStyle = new DCSoft.WinForms.ImageListBox();
            this.chkRightBorder = new System.Windows.Forms.CheckBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.chkLeftBorder = new System.Windows.Forms.CheckBox();
            this.chkBottomBorder = new System.Windows.Forms.CheckBox();
            this.chkTopBorder = new System.Windows.Forms.CheckBox();
            this.picBorderPreview = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
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
            this.groupBox1.Controls.Add(this.btnBackgroundColor);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtBorderWidth);
            this.groupBox1.Controls.Add(this.btnBorderColor);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lstBorderStyle);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(185, 299);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置";
            // 
            // btnBackgroundColor
            // 
            this.btnBackgroundColor.Location = new System.Drawing.Point(83, 265);
            this.btnBackgroundColor.Name = "btnBackgroundColor";
            this.btnBackgroundColor.Size = new System.Drawing.Size(88, 23);
            this.btnBackgroundColor.TabIndex = 1;
            this.btnBackgroundColor.UseVisualStyleBackColor = true;
            this.btnBackgroundColor.ValueChanged += new System.EventHandler(this.btnBackgroundColor_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 270);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "背景色:";
            // 
            // txtBorderWidth
            // 
            this.txtBorderWidth.Location = new System.Drawing.Point(83, 238);
            this.txtBorderWidth.Name = "txtBorderWidth";
            this.txtBorderWidth.Size = new System.Drawing.Size(88, 21);
            this.txtBorderWidth.TabIndex = 7;
            this.txtBorderWidth.ValueChanged += new System.EventHandler(this.txtBorderWidth_ValueChanged);
            // 
            // btnBorderColor
            // 
            this.btnBorderColor.Location = new System.Drawing.Point(83, 208);
            this.btnBorderColor.Name = "btnBorderColor";
            this.btnBorderColor.Size = new System.Drawing.Size(88, 23);
            this.btnBorderColor.TabIndex = 3;
            this.btnBorderColor.UseVisualStyleBackColor = true;
            this.btnBorderColor.ValueChanged += new System.EventHandler(this.btnBorderColor_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 240);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "边框线宽度:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "边框线颜色:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "边框线样式:";
            // 
            // lstBorderStyle
            // 
            this.lstBorderStyle.AutoScroll = true;
            this.lstBorderStyle.AutoScrollMinSize = new System.Drawing.Size(1, 145);
            this.lstBorderStyle.BackColor = System.Drawing.SystemColors.Window;
            this.lstBorderStyle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            imageListBoxItem1.Image = ((System.Drawing.Image)(resources.GetObject("imageListBoxItem1.Image")));
            imageListBoxItem1.ImageIndex = 0;
            imageListBoxItem1.Text = "划线";
            imageListBoxItem2.Image = ((System.Drawing.Image)(resources.GetObject("imageListBoxItem2.Image")));
            imageListBoxItem2.ImageIndex = 0;
            imageListBoxItem2.Text = "点划线";
            imageListBoxItem3.Image = ((System.Drawing.Image)(resources.GetObject("imageListBoxItem3.Image")));
            imageListBoxItem3.ImageIndex = 0;
            imageListBoxItem3.Text = "双点划线";
            imageListBoxItem4.Image = ((System.Drawing.Image)(resources.GetObject("imageListBoxItem4.Image")));
            imageListBoxItem4.ImageIndex = 0;
            imageListBoxItem4.Text = "虚线";
            imageListBoxItem5.Image = ((System.Drawing.Image)(resources.GetObject("imageListBoxItem5.Image")));
            imageListBoxItem5.ImageIndex = 0;
            imageListBoxItem5.Text = "实线";
            this.lstBorderStyle.Items.Add(imageListBoxItem1);
            this.lstBorderStyle.Items.Add(imageListBoxItem2);
            this.lstBorderStyle.Items.Add(imageListBoxItem3);
            this.lstBorderStyle.Items.Add(imageListBoxItem4);
            this.lstBorderStyle.Items.Add(imageListBoxItem5);
            this.lstBorderStyle.Location = new System.Drawing.Point(6, 48);
            this.lstBorderStyle.Name = "lstBorderStyle";
            this.lstBorderStyle.Size = new System.Drawing.Size(165, 154);
            this.lstBorderStyle.TabIndex = 1;
            this.lstBorderStyle.SelectedIndexChagned += new System.EventHandler(this.lstBorderStyle_SelectedIndexChagned);
            // 
            // chkRightBorder
            // 
            this.chkRightBorder.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRightBorder.ImageIndex = 5;
            this.chkRightBorder.ImageList = this.imageList1;
            this.chkRightBorder.Location = new System.Drawing.Point(141, 194);
            this.chkRightBorder.Name = "chkRightBorder";
            this.chkRightBorder.Size = new System.Drawing.Size(33, 28);
            this.chkRightBorder.TabIndex = 5;
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
            // chkLeftBorder
            // 
            this.chkLeftBorder.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkLeftBorder.ImageIndex = 2;
            this.chkLeftBorder.ImageList = this.imageList1;
            this.chkLeftBorder.Location = new System.Drawing.Point(55, 194);
            this.chkLeftBorder.Name = "chkLeftBorder";
            this.chkLeftBorder.Size = new System.Drawing.Size(33, 28);
            this.chkLeftBorder.TabIndex = 5;
            this.chkLeftBorder.UseVisualStyleBackColor = true;
            this.chkLeftBorder.CheckedChanged += new System.EventHandler(this.chkTopBorder_CheckedChanged);
            // 
            // chkBottomBorder
            // 
            this.chkBottomBorder.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBottomBorder.ImageIndex = 0;
            this.chkBottomBorder.ImageList = this.imageList1;
            this.chkBottomBorder.Location = new System.Drawing.Point(15, 155);
            this.chkBottomBorder.Name = "chkBottomBorder";
            this.chkBottomBorder.Size = new System.Drawing.Size(33, 28);
            this.chkBottomBorder.TabIndex = 5;
            this.chkBottomBorder.UseVisualStyleBackColor = true;
            this.chkBottomBorder.CheckedChanged += new System.EventHandler(this.chkTopBorder_CheckedChanged);
            // 
            // chkTopBorder
            // 
            this.chkTopBorder.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkTopBorder.ImageIndex = 6;
            this.chkTopBorder.ImageList = this.imageList1;
            this.chkTopBorder.Location = new System.Drawing.Point(15, 87);
            this.chkTopBorder.Name = "chkTopBorder";
            this.chkTopBorder.Size = new System.Drawing.Size(33, 28);
            this.chkTopBorder.TabIndex = 5;
            this.chkTopBorder.UseVisualStyleBackColor = true;
            this.chkTopBorder.CheckedChanged += new System.EventHandler(this.chkTopBorder_CheckedChanged);
            // 
            // picBorderPreview
            // 
            this.picBorderPreview.BackColor = System.Drawing.Color.White;
            this.picBorderPreview.Image = ((System.Drawing.Image)(resources.GetObject("picBorderPreview.Image")));
            this.picBorderPreview.Location = new System.Drawing.Point(54, 87);
            this.picBorderPreview.Name = "picBorderPreview";
            this.picBorderPreview.Size = new System.Drawing.Size(120, 100);
            this.picBorderPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBorderPreview.TabIndex = 4;
            this.picBorderPreview.TabStop = false;
            this.picBorderPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.picBorderPreview_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.picBorderPreview);
            this.groupBox2.Controls.Add(this.chkTopBorder);
            this.groupBox2.Controls.Add(this.chkRightBorder);
            this.groupBox2.Controls.Add(this.chkBottomBorder);
            this.groupBox2.Controls.Add(this.chkLeftBorder);
            this.groupBox2.Location = new System.Drawing.Point(191, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 299);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "预览:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(177, 318);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(267, 318);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dlgBorderBackground
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 354);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgBorderBackground";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "边框和底纹";
            this.Load += new System.EventHandler(this.dlgBorderBackground_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBorderWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBorderPreview)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private WinForms.ImageListBox lstBorderStyle;
        private DCSoft.WinForms.ColorButton btnBorderColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkTopBorder;
        private System.Windows.Forms.PictureBox picBorderPreview;
        private System.Windows.Forms.CheckBox chkRightBorder;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox chkLeftBorder;
        private System.Windows.Forms.CheckBox chkBottomBorder;
        private System.Windows.Forms.GroupBox groupBox2;
        private DCSoft.WinForms.ColorButton btnBackgroundColor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown txtBorderWidth;
        private System.Windows.Forms.Label label7;
    }
}