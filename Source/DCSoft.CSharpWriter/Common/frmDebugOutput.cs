/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace DCSoft.Common
{
    /// <summary>
    /// 显示调试输出内容的窗口
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public partial class frmDebugOutput : Form 
    {
        public frmDebugOutput()
        {
            InitializeComponent();
        }

        private void frmDebugOutput_Load(object sender, EventArgs e)
        {
            _Listener = new MyListener();
            _Listener.frm = this;

            this.EnableListenDebugOutput = true;
        }

        private bool _ShowTimeStamp = true;

        public bool ShowTimeStamp
        {
            get { return _ShowTimeStamp; }
            set { _ShowTimeStamp = value; }
        }

        private string _TimeStampFormat = "HH:mm:ss.fff|";

        public string TimeStampFormat
        {
            get { return _TimeStampFormat; }
            set { _TimeStampFormat = value; }
        }

        private MyListener _Listener = null;
        /// <summary>
        /// 是否监听调试输出信息
        /// </summary>
        public bool EnableListenDebugOutput
        {
            get
            {
                return System.Diagnostics.Debug.Listeners.Contains(_Listener);
            }
            set
            {
                if (System.Diagnostics.Debug.Listeners.Contains(_Listener) != value)
                {
                    if (value)
                    {
                        System.Diagnostics.Debug.Listeners.Add(_Listener);
                    }
                    else
                    {
                        System.Diagnostics.Debug.Listeners.Remove(_Listener);
                    }
                }
            }
             
        }

        private class MyListener : System.Diagnostics.TraceListener
        {
            public frmDebugOutput frm = null;
            private bool isNewLine = false ;
            private void CheckNewLine()
            {
                if (isNewLine)
                {
                    if (frm._ShowTimeStamp && string.IsNullOrEmpty(frm._TimeStampFormat) == false)
                    {
                        frm.txtOutput.AppendText( Environment.NewLine + DateTime.Now.ToString(frm._TimeStampFormat));
                    }
                    isNewLine = false;
                }
            }
            public override void Write(string message)
            {
                if (frm != null && frm.txtOutput != null && frm.txtOutput.IsHandleCreated)
                {
                    CheckNewLine();
                    frm.txtOutput.AppendText(message);
                }
            }
            public override void WriteLine(string message)
            {
                if (frm != null && frm.txtOutput != null && frm.txtOutput.IsHandleCreated)
                {
                    CheckNewLine();
                    if (string.IsNullOrEmpty(message) == false)
                    {
                        frm.txtOutput.AppendText(message);
                    }
                    isNewLine = true;
                }
            }
        }

        private void frmDebugOutput_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.EnableListenDebugOutput = false;
        }

        private void btnWordWrap_Click(object sender, EventArgs e)
        {
            txtOutput.WordWrap = btnWordWrap.Checked;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOutput.Text = "";
        }
    }
}
