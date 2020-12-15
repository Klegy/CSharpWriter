/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DCSoft.Printing
{
    public class dlgWaitPrintJob : Form
    {

        public dlgWaitPrintJob()
        {
            InitializeComponent();
            strStatus = lblStatus.Text;
            this.DialogResult = DialogResult.Cancel;
        }

        private string strStatus = null;
        private Label label1;
        private Label lblStatus;
        private Timer myTimer;
        private IContainer components;
        private Button btnCancel;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgWaitPrintJob));
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.myTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblStatus
            // 
            this.lblStatus.AccessibleDescription = null;
            this.lblStatus.AccessibleName = null;
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.Font = null;
            this.lblStatus.Name = "lblStatus";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = null;
            this.btnCancel.AccessibleName = null;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackgroundImage = null;
            this.btnCancel.Font = null;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // myTimer
            // 
            this.myTimer.Enabled = true;
            this.myTimer.Tick += new System.EventHandler(this.myTimer_Tick);
            // 
            // dlgWaitPrintJob
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label1);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgWaitPrintJob";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private PrintJob myPrintJob = null;

        /// <summary>
        /// 打印任务对象
        /// </summary>
        public PrintJob PrintJob
        {
            get 
            {
                return myPrintJob; 
            }
            set
            {
                myPrintJob = value; 
            }
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (myPrintJob != null)
            {
                myPrintJob.Cancel();
            }
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 定时器事件，不断的检测打印任务状态，若打印任务结束则关闭本对话框。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myTimer_Tick(object sender, EventArgs e)
        {
            if (myPrintJob != null)
            {
                lblStatus.Text = strStatus + myPrintJob.Status;
                if (myPrintJob.IsRunning == false)
                {
                    if (myPrintJob.IsSuccessStatus)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    this.Close();
                }
            }
        }
    }
}