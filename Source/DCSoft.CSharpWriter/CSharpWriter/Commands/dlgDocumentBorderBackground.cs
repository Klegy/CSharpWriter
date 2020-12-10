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
using System.Drawing.Drawing2D;

namespace DCSoft.CSharpWriter.Commands
{
    public partial class dlgDocumentBorderBackground : Form
    {
        public dlgDocumentBorderBackground()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private BorderBackgroundCommandParameter _CommandParameter = null;
        /// <summary>
        /// 命令参数对象
        /// </summary>
        public BorderBackgroundCommandParameter CommandParameter
        {
            get { return _CommandParameter; }
            set { _CommandParameter = value; }
        }

        private bool _CompleMode = true;
        /// <summary>
        /// 完整模式，显示对话框中所有的用户界面
        /// </summary>
        public bool CompleMode
        {
            get
            {
                return _CompleMode; 
            }
            set
            {
                _CompleMode = value; 
            }
        }

        private bool _ValueModified = false;
        /// <summary>
        /// 数值发生改变标记
        /// </summary>
        public bool ValueModified
        {
            get { return _ValueModified; }
            set { _ValueModified = value; }
        }

        private Bitmap _PreviewImage = null;

        private void dlgBorderBackground_Load(object sender, EventArgs e)
        {
            if (this.CompleMode == false)
            {
                chkMiddleHorizontalBorder.Visible = false;
                chkCenterVerticalBorder.Visible = false;
                lblBorderApplyRange.Visible = false;
                cboBorderApplyRange.Visible = false;
            }
            _PreviewImage = ( Bitmap ) picBorderPreview.Image;
            picBorderPreview.Image = null;
            _PreviewImage.MakeTransparent(Color.White);

            if (_CommandParameter == null)
            {
                _CommandParameter = new BorderBackgroundCommandParameter();
            }
            _HandingEvent = true;
            lstBorderSettings.SelectedIndex = (int )_CommandParameter.BorderSettingsStyle;
            switch (_CommandParameter.BorderStyle)
            {
                case DashStyle.Dash :
                    lstBorderStyle.SelectedIndex = 0;
                    break;
                case DashStyle.DashDot :
                    lstBorderStyle.SelectedIndex = 1;
                    break;
                case DashStyle.DashDotDot :
                    lstBorderStyle.SelectedIndex = 2;
                    break;
                case DashStyle.Dot :
                    lstBorderStyle.SelectedIndex = 3;
                    break;
                case DashStyle.Solid :
                    lstBorderStyle.SelectedIndex = 4;
                    break;
            }
            
            chkBottomBorder.Checked = _CommandParameter.BottomBorder;
            chkCenterVerticalBorder.Checked = _CommandParameter.CenterVerticalBorder;
            chkLeftBorder.Checked = _CommandParameter.LeftBorder;
            chkMiddleHorizontalBorder.Checked = _CommandParameter.MiddleHorizontalBorder;
            chkRightBorder.Checked = _CommandParameter.RightBorder;
            chkTopBorder.Checked = _CommandParameter.TopBorder;

             
            {
                cboBorderApplyRange.Items.Add(WriterStrings.BorderApplyRangeText);
                cboBorderApplyRange.Items.Add(WriterStrings.BorderApplyRangeParagraph);
                if (_CommandParameter.ApplyRange == BorderCommandApplyRange.Text)
                {
                    cboBorderApplyRange.SelectedIndex = 0;
                }
                else
                {
                    cboBorderApplyRange.SelectedIndex = 1;
                }
            }

            btnBorderColor.CenterColor = _CommandParameter.BorderColor;
            btnBackgroundColor.CenterColor = _CommandParameter.BackgroundColor;
            txtBorderWidth.Value = (decimal)_CommandParameter.BorderWidth;
            _HandingEvent = false;
            this.ValueModified = false;
        }

        /// <summary>
        /// 正在处理用户界面事件
        /// </summary>
        private bool _HandingEvent = false;

        private void lstBorderSettings_SelectedIndexChagned(object sender, EventArgs e)
        {
            if (this._HandingEvent)
            {
                return;
            }
            this._HandingEvent = true;
            this.ValueModified = true;
            switch (lstBorderSettings.SelectedIndex)
            {
                case 0:
                    chkBottomBorder.Checked = false;
                    chkCenterVerticalBorder.Checked = false;
                    chkLeftBorder.Checked = false;
                    chkMiddleHorizontalBorder.Checked = false;
                    chkRightBorder.Checked = false;
                    chkTopBorder.Checked = false;
                    break;
                case 1:
                    chkBottomBorder.Checked = true ;
                    chkCenterVerticalBorder.Checked = false;
                    chkLeftBorder.Checked = true ;
                    chkMiddleHorizontalBorder.Checked = false;
                    chkRightBorder.Checked = true ;
                    chkTopBorder.Checked = true ;
                    break;
                case 2:
                    chkBottomBorder.Checked = true;
                    chkCenterVerticalBorder.Checked = true;
                    chkLeftBorder.Checked = true;
                    chkMiddleHorizontalBorder.Checked = true;
                    chkRightBorder.Checked = true;
                    chkTopBorder.Checked = true;
                    break;
            }
            this._HandingEvent = false;
            picBorderPreview.Invalidate();
        }

        private void chkTopBorder_CheckedChanged(object sender, EventArgs e)
        {
            if (this._HandingEvent == false)
            {
                this._HandingEvent = true;
                this.ValueModified = true;
                picBorderPreview.Invalidate();
                if (chkBottomBorder.Checked
                    && chkLeftBorder.Checked
                    && chkRightBorder.Checked
                    && chkTopBorder.Checked
                    )
                {
                    if (chkCenterVerticalBorder.Checked
                        && chkMiddleHorizontalBorder.Checked)
                    {
                        lstBorderSettings.SelectedIndex = 2;
                    }
                    else if (chkMiddleHorizontalBorder.Checked == false
                        && chkCenterVerticalBorder.Checked == false)
                    {
                        lstBorderSettings.SelectedIndex = 1;
                    }
                    else
                    {
                        lstBorderSettings.SelectedIndex = 3;
                    }
                }
                else if (chkBottomBorder.Checked == false
                    && chkCenterVerticalBorder.Checked == false
                    && chkLeftBorder.Checked == false
                    && chkMiddleHorizontalBorder.Checked == false
                    && chkRightBorder.Checked == false
                    && chkTopBorder.Checked == false)
                {
                    lstBorderSettings.SelectedIndex = 0;
                }
                else
                {
                    lstBorderSettings.SelectedIndex = 3;
                }

                this._HandingEvent = false;
            }

        }

        private void picBorderPreview_Paint(object sender, PaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(btnBackgroundColor.CenterColor))
            {
                e.Graphics.FillRectangle(b, 14, 13, 106 - 14, 85 - 13);
            }
            e.Graphics.DrawImage(_PreviewImage, 0, 0);
            if (txtBorderWidth.Value > 0)
            {
                using (Pen pen = new System.Drawing.Pen(
                    this.btnBorderColor.CenterColor,
                    (float)txtBorderWidth.Value))
                {
                    if (chkBottomBorder.Checked)
                    {
                        e.Graphics.DrawLine( pen , 14, 85, 106, 85);
                    }
                    if (chkCenterVerticalBorder.Checked)
                    {
                        e.Graphics.DrawLine(pen , 60, 13, 60, 85);
                    }
                    if( chkLeftBorder.Checked )
                    {
                        e.Graphics.DrawLine(  pen , 14, 13, 14, 85);
                    }
                    if (chkMiddleHorizontalBorder.Checked)
                    {
                        e.Graphics.DrawLine( pen  , 14, 49, 106, 49);
                    }
                    if (chkRightBorder.Checked)
                    {
                        e.Graphics.DrawLine(pen, 106, 13, 106, 85);
                    }
                    if (chkTopBorder.Checked)
                    {
                        e.Graphics.DrawLine(pen, 14, 13, 106, 13);
                    }
                }
            }
        }


        void btnBorderColor_ValueChanged(object sender, System.EventArgs e)
        {
            this.ValueModified = true;
            picBorderPreview.Invalidate();
        }

        void btnBackgroundColor_ValueChanged(object sender, System.EventArgs e)
        {
            this.ValueModified = true;
            picBorderPreview.Invalidate();
        }
         

        private void txtBorderWidth_ValueChanged(object sender, EventArgs e)
        {
            this.ValueModified = true;
            picBorderPreview.Invalidate();
        }

        private void cboBorderApplyRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValueModified = true;
        }
         

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.ValueModified)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                _CommandParameter.BorderSettingsStyle = (BorderSettingsStyle)lstBorderSettings.SelectedIndex;
                switch (this.lstBorderStyle.SelectedIndex)
                {
                    case 0:
                        _CommandParameter.BorderStyle = DashStyle.Dash ;
                        break;
                    case 1:
                        _CommandParameter.BorderStyle = DashStyle.DashDot ;
                        break;
                    case 2:
                        _CommandParameter.BorderStyle = DashStyle.DashDotDot ;
                        break;
                    case 3:
                        _CommandParameter.BorderStyle = DashStyle.Dot;
                        break;
                    case 4:
                        _CommandParameter.BorderStyle = DashStyle.Solid ;
                        break;
                    default :
                        _CommandParameter.BorderStyle = DashStyle.Solid;
                        break;
                }
                _CommandParameter.TopBorder = chkTopBorder.Checked;
                _CommandParameter.MiddleHorizontalBorder = chkMiddleHorizontalBorder.Checked;
                _CommandParameter.BottomBorder = chkBottomBorder.Checked;
                _CommandParameter.LeftBorder = chkLeftBorder.Checked;
                _CommandParameter.CenterVerticalBorder = chkCenterVerticalBorder.Checked;
                _CommandParameter.RightBorder = chkRightBorder.Checked;
                _CommandParameter.BorderColor = btnBorderColor.CenterColor;
                _CommandParameter.BorderWidth = (int)txtBorderWidth.Value;
                _CommandParameter.BackgroundColor = btnBackgroundColor.CenterColor ;
                 
                {
                    if (cboBorderApplyRange.SelectedIndex == 0)
                    {
                        _CommandParameter.ApplyRange = BorderCommandApplyRange.Text;
                    }
                    else
                    {
                        _CommandParameter.ApplyRange = BorderCommandApplyRange.Paragraph;
                    }
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            this.Close();
        }

        private void lstBorderStyle_SelectedIndexChagned(object sender, EventArgs e)
        {
            this.ValueModified = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
