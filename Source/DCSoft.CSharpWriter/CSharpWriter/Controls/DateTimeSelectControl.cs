/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Reflection;

namespace DCSoft.CSharpWriter.Controls
{
    [System.ComponentModel.ToolboxItem( false )]
    public partial class DateTimeSelectControl : UserControl
    {
        public DateTimeSelectControl()
        {
            InitializeComponent();
            _ControlDefaultSize = this.Size;
        }

        private Size _ControlDefaultSize = Size.Empty;

        private DateTime _DateTimeValue = DateTime.Now;
        /// <summary>
        /// 演示的事件日期数据
        /// </summary>
        public DateTime DateTimeValue
        {
            get { return _DateTimeValue; }
            set { _DateTimeValue = value; }
        }

        private void DateTimeSelectControl_Load(object sender, EventArgs e)
        {
            for (int iCount = 0; iCount < 24; iCount++)
            {
                cboHour.Items.Add(iCount.ToString());
            }
            for (int iCount = 0; iCount < 60; iCount++)
            {
                cboMinute.Items.Add(iCount.ToString());
                cboSecend.Items.Add(iCount.ToString());
            }
            myMonthCalendar.SetDate(_DateTimeValue);
            cboHour.Text = _DateTimeValue.Hour.ToString();
            cboMinute.Text = _DateTimeValue.Minute.ToString();
            cboSecend.Text = _DateTimeValue.Second.ToString();
            _Modified = false;
        }

        private IWindowsFormsEditorService _EditorService = null;
        /// <summary>
        /// 编辑器服务对象
        /// </summary>
        public IWindowsFormsEditorService EditorService
        {
            get { return _EditorService; }
            set { _EditorService = value; }
        }

        private bool _Modified = false;
        /// <summary>
        /// 数据是否发生改变
        /// </summary>
        public bool Modified
        {
            get { return _Modified; }
            set { _Modified = value; }
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size size = myMonthCalendar.GetPreferredSize(proposedSize);
            return new Size(size.Width, 266);

        }

        
         

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime dtm = myMonthCalendar.SelectionStart;
            _DateTimeValue = new DateTime(
                dtm.Year,
                dtm.Month,
                dtm.Day,
                Convert.ToInt32(cboHour.Text),
                Convert.ToInt32(cboMinute.Text),
                Convert.ToInt32(cboSecend.Text));
            _Modified = true;
            if (_EditorService != null)
            {
                _EditorService.CloseDropDown();
            }
        }

        private void myMonthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            _Modified = true;
        }

        private void cboHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Modified = true;
        }

        private void cboMinute_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Modified = true;
        }

        private void cboSecend_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Modified = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _Modified = false;
            if (_EditorService != null)
            {
                _EditorService.CloseDropDown();
            }
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (e.KeyCode == Keys.Escape)
            {
                // 按下ESC键取消操作
                btnCancel_Click(null, null);
            }
        }

        private void DateTimeSelectControl_Resize(object sender, EventArgs e)
        {
        }
        
        
    }
}
