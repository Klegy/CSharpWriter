/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DCSoft.Data;

namespace DCSoft.Data.WinForms
{
	/// <summary>
	/// 数据源数据格式化编辑器控件
	/// </summary>
	public class dlgFormatDesigner : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblSample;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox lstStyle;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboFormat;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public dlgFormatDesigner()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();
            this.AcceptButton = this.cmdOK;
            this.CancelButton = this.cmdCancel;
			this.DialogResult = DialogResult.Cancel ;

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

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgFormatDesigner));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSample = new System.Windows.Forms.Label();
            this.cboFormat = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstStyle = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSample);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lblSample
            // 
            resources.ApplyResources(this.lblSample, "lblSample");
            this.lblSample.Name = "lblSample";
            // 
            // cboFormat
            // 
            this.cboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            resources.ApplyResources(this.cboFormat, "cboFormat");
            this.cboFormat.Name = "cboFormat";
            this.cboFormat.SelectedIndexChanged += new System.EventHandler(this.cboFormat_SelectedIndexChanged);
            this.cboFormat.TextChanged += new System.EventHandler(this.cboFormat_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lstStyle
            // 
            resources.ApplyResources(this.lstStyle, "lstStyle");
            this.lstStyle.Name = "lstStyle";
            this.lstStyle.SelectedIndexChanged += new System.EventHandler(this.lstStyle_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // dlgFormatDesigner
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cboFormat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstStyle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgFormatDesigner";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.ctlFormat_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private bool bolModified = false;
		/// <summary>
		/// 数据是否改变标志
		/// </summary>
		public bool Modified 
		{
			get
			{
				return bolModified ;
			}
		}

        private ValueFormater myInputFormater = new ValueFormater();
        public ValueFormater InputFormater
        {
            get
            {
                return myInputFormater;
            }
            set
            {
                myInputFormater = value;
            }
        }
         
		private System.Collections.Hashtable myFormats = new Hashtable();
		private void ctlFormat_Load(object sender, System.EventArgs e)
		{
            myFormats[ValueFormatStyle.Currency] = DataStrings.FormatSample_Currency.Split('|');
            myFormats[ValueFormatStyle.Numeric] = 
				new string[]{
								"0.00",
								"#.00",
								"c" ,
								//"d",
								"e",
								"f",
								"g" ,
								"r",
                                "FormatedSize"
								//"X"
							};
            myFormats[ValueFormatStyle.DateTime] = DataStrings.FormatSample_DateTime.Split('|');
            myFormats[ValueFormatStyle.String] =
				new string[]{
								"trim",
								"normalizespace",
								"htmltext",
								"left,1",
                                "right,1",
                                "lower",
                                "upper"
							};
            myFormats[ValueFormatStyle.Boolean] = DataStrings.FormatSample_Boolean.Split('|');

            myFormats[ValueFormatStyle.Percent] =
                new string[] { "0" , "1" , "2" , "3" , "4" };


            lstStyle.Items.AddRange(Enum.GetNames(typeof(ValueFormatStyle)));
            if (myInputFormater == null)
            {
                myInputFormater = new ValueFormater();
            }
            lstStyle.Text = myInputFormater.Style.ToString();
            cboFormat.Text = myInputFormater.Format;
            this.bolModified = false ;
		}

		private void lstStyle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//strInputValue = this.lstStyle.Text + ":" + this.cboFormat.Text ;
			this.bolModified = true ;
			this.cboFormat.Items.Clear();
            ValueFormatStyle style = (ValueFormatStyle)Enum.Parse(typeof(ValueFormatStyle), lstStyle.Text, true);
			string[] formats = myFormats[ style ] as string[] ;
			if( formats != null )
			{
				cboFormat.Items.AddRange( formats );
			}
		}

		private void cboFormat_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateSample();
		}
		

		private void cboFormat_TextChanged(object sender, System.EventArgs e)
		{
			UpdateSample();
		}

		private void UpdateSample()
		{
			if( this.lstStyle.SelectedIndex < 0 )
			{
				this.lblSample.Text = "";
			}
			else
			{
				ValueFormater formater = new ValueFormater();
                try
                {

                    formater.Style = (ValueFormatStyle)Enum.Parse(typeof(ValueFormatStyle), lstStyle.Text, true);
                    formater.Format = cboFormat.Text;
                    string strText = "";
                    if (formater.Style == ValueFormatStyle.String)
                    {
                        strText = "\"" + formater.Execute( DataStrings.SampleText ) + "\"";
                    }
                    else if (formater.Style == ValueFormatStyle.Currency)
                    {
                        strText = formater.Execute("123456.789");
                    }
                    else if (formater.Style == ValueFormatStyle.DateTime)
                    {
                        strText = formater.Execute(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else if (formater.Style == ValueFormatStyle.Numeric)
                    {
                        strText = formater.Execute("123456.789");
                    }

                    else if (formater.Style == ValueFormatStyle.Boolean)
                    {
                        strText = "True:" + formater.Execute("true") + "  False:" + formater.Execute("false");
                    }
                    else if (formater.Style == ValueFormatStyle.Percent)
                    {
                        strText = formater.Execute("123.45678");
                    }
                    this.lblSample.Text = strText;
                }
                catch (Exception ext)
                {
                    lblSample.Text = "#" + ext.Message;
                }
			}
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
            if (myInputFormater == null)
                myInputFormater = new ValueFormater();
            if (lstStyle.SelectedIndex >= 0)
            {
                myInputFormater.Style = (ValueFormatStyle)Enum.Parse(typeof(ValueFormatStyle), lstStyle.Text, true);
                myInputFormater.Format = cboFormat.Text;
            }
            else
            {
                myInputFormater.Style = ValueFormatStyle.None;
            }
			this.DialogResult = DialogResult.OK ;
			this.Close();
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}//public class ctlFormatDesigner : System.Windows.Forms.UserControl
}