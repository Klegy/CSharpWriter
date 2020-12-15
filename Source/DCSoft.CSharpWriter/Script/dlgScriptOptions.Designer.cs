/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
namespace DCSoft.Script
{
    partial class dlgScriptOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgScriptOptions));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvwAssembly = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.cmdDeleteAssembly = new System.Windows.Forms.Button();
            this.cmdBrowseAdd = new System.Windows.Forms.Button();
            this.cmdAddGACAssembly = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtImports = new System.Windows.Forms.TextBox();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvwAssembly);
            this.groupBox1.Controls.Add(this.cmdDeleteAssembly);
            this.groupBox1.Controls.Add(this.cmdBrowseAdd);
            this.groupBox1.Controls.Add(this.cmdAddGACAssembly);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lvwAssembly
            // 
            this.lvwAssembly.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvwAssembly.FullRowSelect = true;
            this.lvwAssembly.GridLines = true;
            this.lvwAssembly.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwAssembly.HideSelection = false;
            resources.ApplyResources(this.lvwAssembly, "lvwAssembly");
            this.lvwAssembly.MultiSelect = false;
            this.lvwAssembly.Name = "lvwAssembly";
            this.lvwAssembly.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwAssembly.UseCompatibleStateImageBehavior = false;
            this.lvwAssembly.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // cmdDeleteAssembly
            // 
            resources.ApplyResources(this.cmdDeleteAssembly, "cmdDeleteAssembly");
            this.cmdDeleteAssembly.Name = "cmdDeleteAssembly";
            this.cmdDeleteAssembly.UseVisualStyleBackColor = true;
            this.cmdDeleteAssembly.Click += new System.EventHandler(this.cmdDeleteAssembly_Click);
            // 
            // cmdBrowseAdd
            // 
            resources.ApplyResources(this.cmdBrowseAdd, "cmdBrowseAdd");
            this.cmdBrowseAdd.Name = "cmdBrowseAdd";
            this.cmdBrowseAdd.UseVisualStyleBackColor = true;
            this.cmdBrowseAdd.Click += new System.EventHandler(this.cmdBrowseAdd_Click);
            // 
            // cmdAddGACAssembly
            // 
            resources.ApplyResources(this.cmdAddGACAssembly, "cmdAddGACAssembly");
            this.cmdAddGACAssembly.Name = "cmdAddGACAssembly";
            this.cmdAddGACAssembly.UseVisualStyleBackColor = true;
            this.cmdAddGACAssembly.Click += new System.EventHandler(this.cmdAddGACAssembly_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtImports);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // txtImports
            // 
            this.txtImports.AcceptsReturn = true;
            resources.ApplyResources(this.txtImports, "txtImports");
            this.txtImports.Name = "txtImports";
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
            // dlgScriptOptions
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgScriptOptions";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.dlgScriptOptions_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdDeleteAssembly;
        private System.Windows.Forms.Button cmdBrowseAdd;
        private System.Windows.Forms.Button cmdAddGACAssembly;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtImports;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ListView lvwAssembly;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}