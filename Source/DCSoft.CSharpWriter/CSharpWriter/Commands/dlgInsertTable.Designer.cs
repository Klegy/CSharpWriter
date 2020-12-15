namespace DCSoft.CSharpWriter.Commands
{
    partial class dlgInsertTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgInsertTable));
            this.label1 = new System.Windows.Forms.Label();
            this.txtRows = new System.Windows.Forms.NumericUpDown();
            this.txtColumns = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlPreview = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.txtRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtColumns)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtRows
            // 
            resources.ApplyResources(this.txtRows, "txtRows");
            this.txtRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtRows.Name = "txtRows";
            this.txtRows.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtRows.ValueChanged += new System.EventHandler(this.txtRows_ValueChanged);
            // 
            // txtColumns
            // 
            resources.ApplyResources(this.txtColumns, "txtColumns");
            this.txtColumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtColumns.Name = "txtColumns";
            this.txtColumns.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtColumns.ValueChanged += new System.EventHandler(this.txtColumns_ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
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
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.pnlPreview);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // pnlPreview
            // 
            resources.ApplyResources(this.pnlPreview, "pnlPreview");
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPreview_Paint);
            // 
            // dlgInsertTable
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.txtColumns);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRows);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgInsertTable";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.dlgInsertTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtColumns)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtRows;
        private System.Windows.Forms.NumericUpDown txtColumns;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnlPreview;
    }
}