/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
namespace DCSoft.CSharpWriter.Controls
{
    partial class DateTimeSelectControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateTimeSelectControl));
            this.myMonthCalendar = new System.Windows.Forms.MonthCalendar();
            this.label1 = new System.Windows.Forms.Label();
            this.cboHour = new System.Windows.Forms.ComboBox();
            this.cboMinute = new System.Windows.Forms.ComboBox();
            this.cboSecend = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // myMonthCalendar
            // 
            resources.ApplyResources(this.myMonthCalendar, "myMonthCalendar");
            this.myMonthCalendar.Name = "myMonthCalendar";
            this.myMonthCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.myMonthCalendar_DateChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cboHour
            // 
            this.cboHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHour.FormattingEnabled = true;
            resources.ApplyResources(this.cboHour, "cboHour");
            this.cboHour.Name = "cboHour";
            this.cboHour.SelectedIndexChanged += new System.EventHandler(this.cboHour_SelectedIndexChanged);
            // 
            // cboMinute
            // 
            this.cboMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMinute.FormattingEnabled = true;
            resources.ApplyResources(this.cboMinute, "cboMinute");
            this.cboMinute.Name = "cboMinute";
            this.cboMinute.SelectedIndexChanged += new System.EventHandler(this.cboMinute_SelectedIndexChanged);
            // 
            // cboSecend
            // 
            this.cboSecend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSecend.FormattingEnabled = true;
            resources.ApplyResources(this.cboSecend, "cboSecend");
            this.cboSecend.Name = "cboSecend";
            this.cboSecend.SelectedIndexChanged += new System.EventHandler(this.cboSecend_SelectedIndexChanged);
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
            // DateTimeSelectControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cboSecend);
            this.Controls.Add(this.cboMinute);
            this.Controls.Add(this.cboHour);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.myMonthCalendar);
            this.Name = "DateTimeSelectControl";
            this.Load += new System.EventHandler(this.DateTimeSelectControl_Load);
            this.Resize += new System.EventHandler(this.DateTimeSelectControl_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar myMonthCalendar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboHour;
        private System.Windows.Forms.ComboBox cboMinute;
        private System.Windows.Forms.ComboBox cboSecend;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}
