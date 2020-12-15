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
using DCSoft.CSharpWriter.Dom;

namespace DCSoft.CSharpWriter.Commands
{
    public partial class dlgWordCount : Form
    {
        public dlgWordCount()
        {
            InitializeComponent();
        }

        private WordCountResult _CountResult = null;

        public WordCountResult CountResult
        {
            get { return _CountResult; }
            set { _CountResult = value; }
        }

        private void dlgWordCount_Load(object sender, EventArgs e)
        {
            if (_CountResult == null)
            {
                _CountResult = new WordCountResult();
            }
            lblChars.Text = _CountResult.Chars.ToString();
            lblCharsNOWhitespace.Text = _CountResult.CharsNoWhitespace.ToString();
            lblImages.Text = _CountResult.Images.ToString();
            lblLines.Text = _CountResult.Lines.ToString();
            lblPages.Text = _CountResult.Pages.ToString();
            lblParagraphs.Text = _CountResult.Paragraphs.ToString();
            lblWords.Text = _CountResult.Words.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
