/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
namespace DCSoft.CSharpWriter.Commands
{
    partial class dlgWordCount
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgWordCount));
            this.label1 = new System.Windows.Forms.Label();
            this.lblPages = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblWords = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblChars = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCharsNOWhitespace = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblParagraphs = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblLines = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblImages = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblPages
            // 
            resources.ApplyResources(this.lblPages, "lblPages");
            this.lblPages.Name = "lblPages";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lblWords
            // 
            resources.ApplyResources(this.lblWords, "lblWords");
            this.lblWords.Name = "lblWords";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lblChars
            // 
            resources.ApplyResources(this.lblChars, "lblChars");
            this.lblChars.Name = "lblChars";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // lblCharsNOWhitespace
            // 
            resources.ApplyResources(this.lblCharsNOWhitespace, "lblCharsNOWhitespace");
            this.lblCharsNOWhitespace.Name = "lblCharsNOWhitespace";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // lblParagraphs
            // 
            resources.ApplyResources(this.lblParagraphs, "lblParagraphs");
            this.lblParagraphs.Name = "lblParagraphs";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // lblLines
            // 
            resources.ApplyResources(this.lblLines, "lblLines");
            this.lblLines.Name = "lblLines";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // lblImages
            // 
            resources.ApplyResources(this.lblImages, "lblImages");
            this.lblImages.Name = "lblImages";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dlgWordCount
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblImages);
            this.Controls.Add(this.lblLines);
            this.Controls.Add(this.lblParagraphs);
            this.Controls.Add(this.lblCharsNOWhitespace);
            this.Controls.Add(this.lblChars);
            this.Controls.Add(this.lblWords);
            this.Controls.Add(this.lblPages);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgWordCount";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.dlgWordCount_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPages;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblWords;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblChars;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCharsNOWhitespace;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblParagraphs;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblLines;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblImages;
        private System.Windows.Forms.Button btnClose;
    }
}