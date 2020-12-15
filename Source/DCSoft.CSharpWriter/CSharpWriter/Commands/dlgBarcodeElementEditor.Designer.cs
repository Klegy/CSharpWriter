namespace DCSoft.CSharpWriter.Commands
{
    partial class dlgBarcodeElementEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgBarcodeElementEditor));
            this.label1 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboBarcodeStyle = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboTextAlignment = new System.Windows.Forms.ComboBox();
            this.chkShowText = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMinWidth = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtInitalizeText = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtID
            // 
            resources.ApplyResources(this.txtID, "txtID");
            this.txtID.Name = "txtID";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cboBarcodeStyle
            // 
            this.cboBarcodeStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBarcodeStyle.FormattingEnabled = true;
            resources.ApplyResources(this.cboBarcodeStyle, "cboBarcodeStyle");
            this.cboBarcodeStyle.Name = "cboBarcodeStyle";
            this.cboBarcodeStyle.SelectedIndexChanged += new System.EventHandler(this.cboBarcodeStyle_SelectedIndexChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // cboTextAlignment
            // 
            this.cboTextAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTextAlignment.FormattingEnabled = true;
            this.cboTextAlignment.Items.AddRange(new object[] {
            resources.GetString("cboTextAlignment.Items"),
            resources.GetString("cboTextAlignment.Items1"),
            resources.GetString("cboTextAlignment.Items2")});
            resources.ApplyResources(this.cboTextAlignment, "cboTextAlignment");
            this.cboTextAlignment.Name = "cboTextAlignment";
            this.cboTextAlignment.SelectedIndexChanged += new System.EventHandler(this.cboTextAlignment_SelectedIndexChanged);
            // 
            // chkShowText
            // 
            resources.ApplyResources(this.chkShowText, "chkShowText");
            this.chkShowText.Name = "chkShowText";
            this.chkShowText.UseVisualStyleBackColor = true;
            this.chkShowText.CheckedChanged += new System.EventHandler(this.chkShowText_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picPreview);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.Color.White;
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.picPreview, "picPreview");
            this.picPreview.Name = "picPreview";
            this.picPreview.TabStop = false;
            this.picPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.picPreview_Paint);
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
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtMinWidth
            // 
            resources.ApplyResources(this.txtMinWidth, "txtMinWidth");
            this.txtMinWidth.Name = "txtMinWidth";
            this.txtMinWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMinWidth.ValueChanged += new System.EventHandler(this.txtMinWidth_ValueChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtInitalizeText
            // 
            resources.ApplyResources(this.txtInitalizeText, "txtInitalizeText");
            this.txtInitalizeText.Name = "txtInitalizeText";
            this.txtInitalizeText.TextChanged += new System.EventHandler(this.txtInitalizeText_TextChanged);
            // 
            // dlgBarcodeElementEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtMinWidth);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkShowText);
            this.Controls.Add(this.cboTextAlignment);
            this.Controls.Add(this.cboBarcodeStyle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtInitalizeText);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgBarcodeElementEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.dlgBarcodeElementProperties_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboBarcodeStyle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboTextAlignment;
        private System.Windows.Forms.CheckBox chkShowText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown txtMinWidth;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtInitalizeText;
    }
}