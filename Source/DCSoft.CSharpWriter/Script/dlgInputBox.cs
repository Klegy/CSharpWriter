/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DCSoft.Script
{
	/// <summary>
	/// InputBox 的摘要说明。
	/// </summary>
	public class dlgInputBox : System.Windows.Forms.Form
	{
        /// <summary>
        /// Show a input dialog and return user input text , if cancel by user , return null.
        /// </summary>
        /// <param name="ParentWindow">parent form</param>
        /// <param name="strTitle">description</param>
        /// <param name="strCaption">caption</param>
        /// <param name="strDefaultValue">initialize value</param>
        /// <returns>text that user input , if canceld , return null</returns>
        public static string InputBox(System.Windows.Forms.IWin32Window ParentWindow, string strTitle, string strCaption, string strDefaultValue)
        {
            using (dlgInputBox dlg = new dlgInputBox())
            {
                dlg.lblTitle.Text = strTitle;
                dlg.Text = strCaption;
                dlg.TextValue = strDefaultValue;
                if (dlg.ShowDialog(ParentWindow) == System.Windows.Forms.DialogResult.OK)
                {
                    return dlg.txtInput.Text;
                }
            }
            return null;
        }

		/// <summary>
        /// Show a input dialog and return user input text , if cancel by user , return null.
		/// </summary>
        /// <param name="strTitle">description</param>
        /// <param name="strCaption">caption</param>
        /// <param name="strDefaultValue">initialize value</param>
        /// <returns>text that user input , if canceld , return null</returns>
		public static string InputBox( string strTitle , string strCaption , string strDefaultValue)
		{
			using( dlgInputBox dlg = new dlgInputBox())
			{
				dlg.lblTitle.Text	= strTitle ;
				dlg.Text			= strCaption ;
				dlg.TextValue		= strDefaultValue ;
				if( dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					return dlg.txtInput.Text ;
				}
			}
			return null;
		}
         
		internal System.Windows.Forms.Label lblTitle;
		internal System.Windows.Forms.TextBox txtInput;
		internal System.Windows.Forms.Button cmdOK;
		internal System.Windows.Forms.Button cmdCancel;

		public string ErrorValueMsg = "Input error! please retry.";
		private string strTextValue = "";

		public string InputTitle
		{
			get{ return this.lblTitle.Text ;}
			set{ this.lblTitle.Text = value;}
		}
		
		/// <summary>
		/// text value
		/// </summary>
		public string TextValue
		{
			get{ return strTextValue ;}
			set{ strTextValue = value;}
		}

		private System.ComponentModel.Container components = null;

		public dlgInputBox()
		{
			//
			//
			InitializeComponent();

			//
			//
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel ;
		}

		/// <summary>
		/// 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgInputBox));
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // txtInput
            // 
            resources.ApplyResources(this.txtInput, "txtInput");
            this.txtInput.Name = "txtInput";
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // dlgInputBox
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgInputBox";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.dlgInputBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			strTextValue = this.txtInput.Text ;
			this.DialogResult = System.Windows.Forms.DialogResult.OK ;
			this.Close();
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel ;
			this.Close();
		}

		private void txtInput_KeyDown(object sender, KeyEventArgs e)
		{
			if( e.KeyCode == System.Windows.Forms.Keys.Enter )
				cmdOK_Click(null,null);
		}

		private void dlgInputBox_Load(object sender, System.EventArgs e)
		{
			this.txtInput.Text = strTextValue ;
		}

		//		public void SetPasswordMode(bool bolSet )
		//		{
		//			if( bolSet )
		//				this.txtInput.PasswordChar = '*';
		//			else
		//				this.txtInput.PasswordChar = ' ';
		//		}
	}
}