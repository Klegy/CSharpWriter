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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using DCSoft.Drawing;

namespace DCSoft.WinForms.Design
{
    /// <summary>
    /// 边框背景样式编辑器
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    public partial class dlgBorderBackground : Form
    {
        public dlgBorderBackground()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private ContentStyle _ContentStyle = null;

        public ContentStyle ContentStyle
        {
            get { return _ContentStyle; }
            set { _ContentStyle = value; }
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
            if (_ContentStyle == null)
            {
                _ContentStyle = new ContentStyle();
            }
            _PreviewImage = ( Bitmap ) picBorderPreview.Image;
            picBorderPreview.Image = null;
            _PreviewImage.MakeTransparent(Color.White);
             
            _HandingEvent = true;
            switch ( _ContentStyle.BorderStyle )
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
            
            chkBottomBorder.Checked = _ContentStyle.BorderBottom ;
            chkLeftBorder.Checked = _ContentStyle.BorderLeft;
            chkRightBorder.Checked = _ContentStyle.BorderRight;
            chkTopBorder.Checked = _ContentStyle.BorderTop;


            btnBorderColor.CenterColor = _ContentStyle.BorderColor;
            btnBackgroundColor.CenterColor = _ContentStyle.BackgroundColor;
            txtBorderWidth.Value = (decimal)_ContentStyle.BorderWidth;
            _HandingEvent = false;
            this.ValueModified = false;
        }

        /// <summary>
        /// 正在处理用户界面事件
        /// </summary>
        private bool _HandingEvent = false;
         

        private void chkTopBorder_CheckedChanged(object sender, EventArgs e)
        {
            if (this._HandingEvent == false)
            {
                this._HandingEvent = true;
                this.ValueModified = true;
                picBorderPreview.Invalidate();
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
                        e.Graphics.DrawLine(pen, 14, 85, 106, 85);
                    }
                    if (chkLeftBorder.Checked)
                    {
                        e.Graphics.DrawLine(pen, 14, 13, 14, 85);
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
                switch (this.lstBorderStyle.SelectedIndex)
                {
                    case 0:
                        this._ContentStyle.BorderStyle = DashStyle.Dash ;
                        break;
                    case 1:
                        this._ContentStyle.BorderStyle = DashStyle.DashDot ;
                        break;
                    case 2:
                        this._ContentStyle.BorderStyle = DashStyle.DashDotDot ;
                        break;
                    case 3:
                        this._ContentStyle.BorderStyle = DashStyle.Dot;
                        break;
                    case 4:
                        this._ContentStyle.BorderStyle = DashStyle.Solid ;
                        break;
                    default :
                        this._ContentStyle.BorderStyle = DashStyle.Solid;
                        break;
                }
                this._ContentStyle.BorderTop = chkTopBorder.Checked;
                this._ContentStyle.BorderBottom = chkBottomBorder.Checked;
                this._ContentStyle.BorderLeft = chkLeftBorder.Checked;
                this._ContentStyle.BorderRight = chkRightBorder.Checked;
                this._ContentStyle.BorderColor = btnBorderColor.CenterColor;
                this._ContentStyle.BorderWidth = (int)txtBorderWidth.Value;
                this._ContentStyle.BackgroundColor = btnBackgroundColor.CenterColor ;
                 
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
