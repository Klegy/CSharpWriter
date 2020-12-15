/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DCSoft.Common ;
using System.Drawing.Printing;
using DCSoft.WinForms;


namespace DCSoft.Printing
{
	/// <summary>
	/// 页面设置对话框
	/// </summary>
	public class dlgPageSetup : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblTop;
		private System.Windows.Forms.NumericUpDown txtTopMargin;
		private System.Windows.Forms.NumericUpDown txtBottomMargin;
		private System.Windows.Forms.Label lblBottom;
		private System.Windows.Forms.Label lblLeft;
		private System.Windows.Forms.NumericUpDown txtLeftMargin;
		private System.Windows.Forms.NumericUpDown txtRightMargin;
		private System.Windows.Forms.Label lblRight;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label lblPageSize;
		private System.Windows.Forms.Label lblWidth;
		private System.Windows.Forms.Label lblHeight;
		private System.Windows.Forms.ComboBox cboPage;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.PictureBox picPreview;
		private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.RadioButton rdoLandscape;
		private System.Windows.Forms.RadioButton rdoLandscape2;
		private System.Windows.Forms.NumericUpDown txtWidth;
		private System.Windows.Forms.NumericUpDown txtHeight;
        private Button cmdUseWordDefault;
        private Button button1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public dlgPageSetup()
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel ;
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgPageSetup));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoLandscape = new System.Windows.Forms.RadioButton();
            this.rdoLandscape2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTopMargin = new System.Windows.Forms.NumericUpDown();
            this.txtLeftMargin = new System.Windows.Forms.NumericUpDown();
            this.txtBottomMargin = new System.Windows.Forms.NumericUpDown();
            this.lblLeft = new System.Windows.Forms.Label();
            this.txtRightMargin = new System.Windows.Forms.NumericUpDown();
            this.lblRight = new System.Windows.Forms.Label();
            this.lblBottom = new System.Windows.Forms.Label();
            this.lblTop = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.NumericUpDown();
            this.lblWidth = new System.Windows.Forms.Label();
            this.cboPage = new System.Windows.Forms.ComboBox();
            this.lblPageSize = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.NumericUpDown();
            this.lblHeight = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdUseWordDefault = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTopMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLeftMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBottomMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRightMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.AccessibleDescription = null;
            this.groupBox2.AccessibleName = null;
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.BackgroundImage = null;
            this.groupBox2.Controls.Add(this.rdoLandscape);
            this.groupBox2.Controls.Add(this.rdoLandscape2);
            this.groupBox2.Font = null;
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // rdoLandscape
            // 
            this.rdoLandscape.AccessibleDescription = null;
            this.rdoLandscape.AccessibleName = null;
            resources.ApplyResources(this.rdoLandscape, "rdoLandscape");
            this.rdoLandscape.BackColor = System.Drawing.SystemColors.Control;
            this.rdoLandscape.BackgroundImage = null;
            this.rdoLandscape.Font = null;
            this.rdoLandscape.Name = "rdoLandscape";
            this.rdoLandscape.UseVisualStyleBackColor = false;
            this.rdoLandscape.CheckedChanged += new System.EventHandler(this.rdoLandscape_CheckedChanged);
            // 
            // rdoLandscape2
            // 
            this.rdoLandscape2.AccessibleDescription = null;
            this.rdoLandscape2.AccessibleName = null;
            resources.ApplyResources(this.rdoLandscape2, "rdoLandscape2");
            this.rdoLandscape2.BackColor = System.Drawing.SystemColors.Control;
            this.rdoLandscape2.BackgroundImage = null;
            this.rdoLandscape2.Font = null;
            this.rdoLandscape2.Name = "rdoLandscape2";
            this.rdoLandscape2.UseVisualStyleBackColor = false;
            this.rdoLandscape2.CheckedChanged += new System.EventHandler(this.rdoLandscape2_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackgroundImage = null;
            this.groupBox1.Controls.Add(this.txtTopMargin);
            this.groupBox1.Controls.Add(this.txtLeftMargin);
            this.groupBox1.Controls.Add(this.txtBottomMargin);
            this.groupBox1.Controls.Add(this.lblLeft);
            this.groupBox1.Controls.Add(this.txtRightMargin);
            this.groupBox1.Controls.Add(this.lblRight);
            this.groupBox1.Controls.Add(this.lblBottom);
            this.groupBox1.Controls.Add(this.lblTop);
            this.groupBox1.Font = null;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtTopMargin
            // 
            this.txtTopMargin.AccessibleDescription = null;
            this.txtTopMargin.AccessibleName = null;
            resources.ApplyResources(this.txtTopMargin, "txtTopMargin");
            this.txtTopMargin.DecimalPlaces = 2;
            this.txtTopMargin.Font = null;
            this.txtTopMargin.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.txtTopMargin.Name = "txtTopMargin";
            this.txtTopMargin.Tag = "Pager TopMargin";
            this.txtTopMargin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtTopMargin.ValueChanged += new System.EventHandler(this.txtTopMargin_ValueChanged);
            this.txtTopMargin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTopMargin_KeyPress);
            // 
            // txtLeftMargin
            // 
            this.txtLeftMargin.AccessibleDescription = null;
            this.txtLeftMargin.AccessibleName = null;
            resources.ApplyResources(this.txtLeftMargin, "txtLeftMargin");
            this.txtLeftMargin.DecimalPlaces = 2;
            this.txtLeftMargin.Font = null;
            this.txtLeftMargin.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.txtLeftMargin.Name = "txtLeftMargin";
            this.txtLeftMargin.Tag = "Pager LeftMargin";
            this.txtLeftMargin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtLeftMargin.ValueChanged += new System.EventHandler(this.txtLeftMargin_ValueChanged);
            this.txtLeftMargin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLeftMargin_KeyPress);
            // 
            // txtBottomMargin
            // 
            this.txtBottomMargin.AccessibleDescription = null;
            this.txtBottomMargin.AccessibleName = null;
            resources.ApplyResources(this.txtBottomMargin, "txtBottomMargin");
            this.txtBottomMargin.DecimalPlaces = 2;
            this.txtBottomMargin.Font = null;
            this.txtBottomMargin.Maximum = new decimal(new int[] {
            600000,
            0,
            0,
            0});
            this.txtBottomMargin.Name = "txtBottomMargin";
            this.txtBottomMargin.Tag = "Pager BottomMargin";
            this.txtBottomMargin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtBottomMargin.ValueChanged += new System.EventHandler(this.txtBottomMargin_ValueChanged);
            this.txtBottomMargin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBottomMargin_KeyPress);
            // 
            // lblLeft
            // 
            this.lblLeft.AccessibleDescription = null;
            this.lblLeft.AccessibleName = null;
            resources.ApplyResources(this.lblLeft, "lblLeft");
            this.lblLeft.Font = null;
            this.lblLeft.Name = "lblLeft";
            // 
            // txtRightMargin
            // 
            this.txtRightMargin.AccessibleDescription = null;
            this.txtRightMargin.AccessibleName = null;
            resources.ApplyResources(this.txtRightMargin, "txtRightMargin");
            this.txtRightMargin.DecimalPlaces = 2;
            this.txtRightMargin.Font = null;
            this.txtRightMargin.Maximum = new decimal(new int[] {
            600000,
            0,
            0,
            0});
            this.txtRightMargin.Name = "txtRightMargin";
            this.txtRightMargin.Tag = "Pager RightMargin";
            this.txtRightMargin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtRightMargin.ValueChanged += new System.EventHandler(this.txtRightMargin_ValueChanged);
            this.txtRightMargin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRightMargin_KeyPress);
            // 
            // lblRight
            // 
            this.lblRight.AccessibleDescription = null;
            this.lblRight.AccessibleName = null;
            resources.ApplyResources(this.lblRight, "lblRight");
            this.lblRight.Font = null;
            this.lblRight.Name = "lblRight";
            // 
            // lblBottom
            // 
            this.lblBottom.AccessibleDescription = null;
            this.lblBottom.AccessibleName = null;
            resources.ApplyResources(this.lblBottom, "lblBottom");
            this.lblBottom.Font = null;
            this.lblBottom.Name = "lblBottom";
            // 
            // lblTop
            // 
            this.lblTop.AccessibleDescription = null;
            this.lblTop.AccessibleName = null;
            resources.ApplyResources(this.lblTop, "lblTop");
            this.lblTop.Font = null;
            this.lblTop.Name = "lblTop";
            // 
            // txtWidth
            // 
            this.txtWidth.AccessibleDescription = null;
            this.txtWidth.AccessibleName = null;
            resources.ApplyResources(this.txtWidth, "txtWidth");
            this.txtWidth.DecimalPlaces = 2;
            this.txtWidth.Font = null;
            this.txtWidth.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.txtWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Tag = "Page Width";
            this.txtWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtWidth.ValueChanged += new System.EventHandler(this.txtWidth_ValueChanged);
            // 
            // lblWidth
            // 
            this.lblWidth.AccessibleDescription = null;
            this.lblWidth.AccessibleName = null;
            resources.ApplyResources(this.lblWidth, "lblWidth");
            this.lblWidth.Font = null;
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Tag = "Page Width";
            // 
            // cboPage
            // 
            this.cboPage.AccessibleDescription = null;
            this.cboPage.AccessibleName = null;
            resources.ApplyResources(this.cboPage, "cboPage");
            this.cboPage.BackgroundImage = null;
            this.cboPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPage.Font = null;
            this.cboPage.Name = "cboPage";
            this.cboPage.SelectedIndexChanged += new System.EventHandler(this.cboPage_SelectedIndexChanged);
            // 
            // lblPageSize
            // 
            this.lblPageSize.AccessibleDescription = null;
            this.lblPageSize.AccessibleName = null;
            resources.ApplyResources(this.lblPageSize, "lblPageSize");
            this.lblPageSize.Font = null;
            this.lblPageSize.Name = "lblPageSize";
            // 
            // txtHeight
            // 
            this.txtHeight.AccessibleDescription = null;
            this.txtHeight.AccessibleName = null;
            resources.ApplyResources(this.txtHeight, "txtHeight");
            this.txtHeight.DecimalPlaces = 2;
            this.txtHeight.Font = null;
            this.txtHeight.Maximum = new decimal(new int[] {
            600000,
            0,
            0,
            0});
            this.txtHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Tag = "Page Height";
            this.txtHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtHeight.ValueChanged += new System.EventHandler(this.txtHeight_ValueChanged);
            // 
            // lblHeight
            // 
            this.lblHeight.AccessibleDescription = null;
            this.lblHeight.AccessibleName = null;
            resources.ApplyResources(this.lblHeight, "lblHeight");
            this.lblHeight.Font = null;
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Tag = "Page Height";
            // 
            // groupBox3
            // 
            this.groupBox3.AccessibleDescription = null;
            this.groupBox3.AccessibleName = null;
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.BackgroundImage = null;
            this.groupBox3.Controls.Add(this.picPreview);
            this.groupBox3.Font = null;
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // picPreview
            // 
            this.picPreview.AccessibleDescription = null;
            this.picPreview.AccessibleName = null;
            resources.ApplyResources(this.picPreview, "picPreview");
            this.picPreview.BackColor = System.Drawing.SystemColors.Control;
            this.picPreview.BackgroundImage = null;
            this.picPreview.Font = null;
            this.picPreview.ImageLocation = null;
            this.picPreview.Name = "picPreview";
            this.picPreview.TabStop = false;
            this.picPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.picPreview_Paint);
            // 
            // cmdOK
            // 
            this.cmdOK.AccessibleDescription = null;
            this.cmdOK.AccessibleName = null;
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.BackgroundImage = null;
            this.cmdOK.Font = null;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.AccessibleDescription = null;
            this.cmdCancel.AccessibleName = null;
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.BackgroundImage = null;
            this.cmdCancel.Font = null;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdUseWordDefault
            // 
            this.cmdUseWordDefault.AccessibleDescription = null;
            this.cmdUseWordDefault.AccessibleName = null;
            resources.ApplyResources(this.cmdUseWordDefault, "cmdUseWordDefault");
            this.cmdUseWordDefault.BackgroundImage = null;
            this.cmdUseWordDefault.Font = null;
            this.cmdUseWordDefault.Name = "cmdUseWordDefault";
            this.cmdUseWordDefault.Click += new System.EventHandler(this.cmdUseWordDefault_Click);
            // 
            // button1
            // 
            this.button1.AccessibleDescription = null;
            this.button1.AccessibleName = null;
            resources.ApplyResources(this.button1, "button1");
            this.button1.BackgroundImage = null;
            this.button1.Font = null;
            this.button1.Name = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dlgPageSetup
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmdUseWordDefault);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.cboPage);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.lblPageSize);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgPageSetup";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.dlgPageSetup_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTopMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLeftMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBottomMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRightMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		internal string ErrorString = "Paget setting error";
		private XPageSettings myPageSettings = null;
		/// <summary>
		/// 页面设置对象
		/// </summary>
		public XPageSettings PageSettings
		{
			get{ return myPageSettings ;}
			set{ myPageSettings = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dlgPageSetup_Load(object sender, System.EventArgs e)
		{
			this.cboPage.Items.Clear();
			this.cboPage.Items.AddRange( Enum.GetNames( typeof( System.Drawing.Printing.PaperKind )));

            RefreshView();
			//this.cboPage.Items.AddRange( mySetting.PaperSizes );
		}

        private void RefreshView()
        {
            if (myPageSettings != null)
            {
                for (int iCount = 0; iCount < cboPage.Items.Count; iCount++)
                {
                    PaperKind kind = (PaperKind)Enum.Parse(typeof(PaperKind), (string)cboPage.Items[iCount]);
                    if (myPageSettings.PaperKind == kind)
                    {
                        cboPage.SelectedIndex = iCount;
                        break;
                    }
                }

                this.txtLeftMargin.Value = Convert.ToDecimal(MeasureConvert.HundredthsInchToMillimeter(myPageSettings.LeftMargin));
                this.txtTopMargin.Value = Convert.ToDecimal(MeasureConvert.HundredthsInchToMillimeter(myPageSettings.TopMargin));
                this.txtRightMargin.Value = Convert.ToDecimal(MeasureConvert.HundredthsInchToMillimeter(myPageSettings.RightMargin));
                this.txtBottomMargin.Value = Convert.ToDecimal(MeasureConvert.HundredthsInchToMillimeter(myPageSettings.BottomMargin));
                this.rdoLandscape.Checked = !myPageSettings.Landscape;
                this.rdoLandscape2.Checked = myPageSettings.Landscape;
            }
            if (cboPage.SelectedIndex >= 0)
            {
                setPagerSize((PaperKind)Enum.Parse(typeof(PaperKind), cboPage.Text));
            }
        }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private bool RefreshPageSettings()
		{
			if( myPageSettings != null )
			{
                myPageSettings.PaperKind = ( PaperKind )Enum.Parse( typeof( PaperKind ) , cboPage.Text );
                if (myPageSettings.PaperKind == PaperKind.Custom)
                {
                	myPageSettings.PaperWidth = (int) ( MeasureConvert.MillimeterToHundredthsInch( Convert.ToDouble( this.txtWidth.Value) ) );
					myPageSettings.PaperHeight = (int) ( MeasureConvert.MillimeterToHundredthsInch(Convert.ToDouble(this.txtHeight.Value) ));
				}
			
				myPageSettings.LeftMargin = (int) MeasureConvert.MillimeterToHundredthsInch( Convert.ToDouble(this.txtLeftMargin.Value ));
				myPageSettings.TopMargin = (int) MeasureConvert.MillimeterToHundredthsInch(Convert.ToDouble( this.txtTopMargin.Value ));
				myPageSettings.RightMargin = (int) MeasureConvert.MillimeterToHundredthsInch( Convert.ToDouble(this.txtRightMargin.Value ));
				myPageSettings.BottomMargin = (int) MeasureConvert.MillimeterToHundredthsInch( Convert.ToDouble(this.txtBottomMargin.Value) );
				myPageSettings.Landscape = this.rdoLandscape2.Checked ;
				return true ;
			}
			else
				return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void picPreview_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if( RefreshPageSettings() == false )
			{
				using( System.Drawing.StringFormat f = new StringFormat())
				{
					f.Alignment = System.Drawing.StringAlignment.Center ;
					f.LineAlignment = System.Drawing.StringAlignment.Center ;
					e.Graphics.DrawString( ErrorString , this.Font , System.Drawing.Brushes.Red , new System.Drawing.RectangleF( 0 , 0 , picPreview.Width , picPreview.Height ) , f );
				}
				return ;
			}
			PageSettingPreview myPreview = new PageSettingPreview();
			myPreview.Landscape = myPageSettings.Landscape ;
            myPreview.Margins = myPageSettings.Margins;
			myPreview.PaperSize = myPageSettings.PaperSize ;
			myPreview.Bounds = this.picPreview.ClientRectangle ;
			myPreview.OnPaint( sender , e );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboPage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            if (cboPage.SelectedIndex >= 0)
            {
                this.setPagerSize((PaperKind)Enum.Parse(typeof(PaperKind), cboPage.Text));
                this.picPreview.Invalidate();
            }
		}

		/// <summary>
		/// 设置Pagesize是否可用
		/// </summary>
        private void setPagerSize(PaperKind kind)
        {
            this.txtWidth.Enabled = (kind == System.Drawing.Printing.PaperKind.Custom);
            this.txtHeight.Enabled = (kind == System.Drawing.Printing.PaperKind.Custom);
            if (kind == System.Drawing.Printing.PaperKind.Custom)
            {
                decimal width =  Convert.ToDecimal(MeasureConvert.HundredthsInchToMillimeter(myPageSettings.PaperWidth));
                decimal height = Convert.ToDecimal(MeasureConvert.HundredthsInchToMillimeter(myPageSettings.PaperHeight));

                if (width < this.txtWidth.Minimum)
                {
                    this.txtWidth.Value = this.txtWidth.Minimum;
                }
                else
                {
                    this.txtWidth.Value = width;
                }

                if (height < this.txtHeight.Minimum)
                {
                    this.txtHeight.Value = this.txtHeight.Minimum;
                }
                else
                {
                    this.txtHeight.Value = height;
                }
            }
            else
            {
                Size size = XPageSettings.GetStandardPaperSize(kind);
                if (size.IsEmpty == false)
                {
                    this.txtWidth.Value = Convert.ToDecimal(MeasureConvert.HundredthsInchToMillimeter(size.Width));
                    this.txtHeight.Value = Convert.ToDecimal(MeasureConvert.HundredthsInchToMillimeter(size.Height));
                }
            }
        }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private bool validateInput()
		{
			if (rdoLandscape.Checked)
			{
				if (txtWidth.Value < txtLeftMargin.Value + txtRightMargin.Value)
				{
                    MessageBox.Show(
                        this, 
                        PrintingResources.MargeMoreThanPageWidth,
                        this.Text);
					txtWidth.Focus();
					return false;
				}

				if (txtHeight.Value < txtTopMargin.Value + txtBottomMargin.Value)
				{
                    MessageBox.Show(
                        this,
                        PrintingResources.MargeMoreThanPageHeight,
                        this.Text);
					txtWidth.Focus();
					return false;
				}
			}
			else
			{
				if (txtHeight.Value < txtLeftMargin.Value + txtRightMargin.Value)
				{
                    MessageBox.Show(
                        this,
                        PrintingResources.MargeMoreThanPageWidth,
                        this.Text);
					txtWidth.Focus();
					return false;
				}

				if (txtWidth.Value < txtTopMargin.Value + txtBottomMargin.Value)
				{
                    MessageBox.Show(
                        this,
                        PrintingResources.MargeMoreThanPageHeight,
                        this.Text);
					txtWidth.Focus();
					return false;
				}
			}
			return true;
		}
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			if (!validateInput())
			{
				return;
			}
            if (this.RefreshPageSettings())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(this, ErrorString, this.Text);
            }
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void txtWidth_ValueChanged(object sender, System.EventArgs e)
		{
			this.picPreview.Invalidate();
		}

		private void txtHeight_ValueChanged(object sender, System.EventArgs e)
		{
			this.picPreview.Invalidate();
		}

		private void rdoLandscape_CheckedChanged(object sender, System.EventArgs e)
		{
			this.picPreview.Invalidate();
		}

		private void rdoLandscape2_CheckedChanged(object sender, System.EventArgs e)
		{
			this.picPreview.Invalidate();
		}

		private void txtTopMargin_ValueChanged(object sender, System.EventArgs e)
		{
			this.picPreview.Invalidate();
		}

		private void txtBottomMargin_ValueChanged(object sender, System.EventArgs e)
		{
			this.picPreview.Invalidate();
		}

		private void txtLeftMargin_ValueChanged(object sender, System.EventArgs e)
		{
			this.picPreview.Invalidate();
		}

		private void txtRightMargin_ValueChanged(object sender, System.EventArgs e)
		{
			this.picPreview.Invalidate();
		}

		private void txtTopMargin_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			this.picPreview.Invalidate();
		}

		private void txtBottomMargin_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			this.picPreview.Invalidate();
		}

		private void txtLeftMargin_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			this.picPreview.Invalidate();
		}

		private void txtRightMargin_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			this.picPreview.Invalidate();
		}

        private void cmdUseWordDefault_Click(object sender, EventArgs e)
        {
            XPageSettings.WordDefault.CopyTo(myPageSettings);
            this.RefreshView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XPageSettings.IEDefault.CopyTo(myPageSettings);
            this.RefreshView();
        }
	}//public class dlgPageSetup : System.Windows.Forms.Form
}