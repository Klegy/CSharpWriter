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
using DCSoft.Drawing;

namespace DCSoft.CSharpWriter.Commands
{
    public partial class dlgParagraphFormatcs : Form
    {
        public dlgParagraphFormatcs()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private ParagraphFormatCommandParameter _CommandParameter = null;

        public ParagraphFormatCommandParameter CommandParameter
        {
            get { return _CommandParameter; }
            set { _CommandParameter = value; }
        }

        private void dlgParagraphFormatcs_Load(object sender, EventArgs e)
        {
            if (_CommandParameter == null)
            {
                _CommandParameter = new ParagraphFormatCommandParameter();
            }
            this.txtLeftIndent.Value = ( decimal )( GraphicsUnitConvert.ToTwips(_CommandParameter.LeftIndent , GraphicsUnit.Document) / 210.0 );
            txtFirstLineIndent.Value = (decimal)(GraphicsUnitConvert.Convert(_CommandParameter.FirstLineIndent, GraphicsUnit.Document, GraphicsUnit.Millimeter) / 10.0);
            txtSpacingBefore.Value = (decimal)(GraphicsUnitConvert.ToTwips(_CommandParameter.SpacingBefore, GraphicsUnit.Document) / 312.0);
            txtSpacingAfter.Value = (decimal)(GraphicsUnitConvert.ToTwips(_CommandParameter.SpacingAfter, GraphicsUnit.Document) / 312.0);
            cboLineSpacingStyle.SelectedIndex = (int)_CommandParameter.LineSpacingStyle;
            cboLineSpacingStyle_SelectedIndexChanged(null, null);
        }

        private void cboLineSpacingStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            LineSpacingStyle style = (LineSpacingStyle)cboLineSpacingStyle.SelectedIndex;
            switch (style)
            {
                case LineSpacingStyle.SpaceSingle :
                case LineSpacingStyle.Space1pt5 :
                case LineSpacingStyle.SpaceDouble :
                case LineSpacingStyle.SpaceExactly :
                    txtLineSpacing.Value = 0;
                    txtLineSpacing.Enabled = false;
                    txtLineSpacing.Increment = 1m;
                    lblBang.Visible = false ;
                    break;
                case LineSpacingStyle.SpaceSpecify :
                    txtLineSpacing.Enabled = true;
                    txtLineSpacing.Value = (decimal)(GraphicsUnitConvert.ToTwips(_CommandParameter.LineSpacing, GraphicsUnit.Document) / 20.0);
                    txtLineSpacing.Increment = 1m;
                    lblBang.Visible = true ;
                    break;
                case LineSpacingStyle.SpaceMultiple :
                    txtLineSpacing.Value = 3;
                    txtLineSpacing.Increment = 0.25m;
                    lblBang.Visible = false;
                    txtLineSpacing.Enabled = true;
                    break;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            _CommandParameter.FirstLineIndent = (float )GraphicsUnitConvert.Convert((double)txtFirstLineIndent.Value * 10.0, GraphicsUnit.Millimeter, GraphicsUnit.Document);
            _CommandParameter.LeftIndent =(float ) GraphicsUnitConvert.FromTwips((double)txtLeftIndent.Value * 210, GraphicsUnit.Document);
            _CommandParameter.SpacingBefore = (float)GraphicsUnitConvert.FromTwips((double)txtSpacingBefore.Value * 312 , GraphicsUnit.Document );
            _CommandParameter.SpacingAfter = (float)GraphicsUnitConvert.FromTwips((double)txtSpacingAfter.Value * 312 , GraphicsUnit.Document );
            _CommandParameter.LineSpacingStyle = (LineSpacingStyle)cboLineSpacingStyle.SelectedIndex;
            switch (_CommandParameter.LineSpacingStyle)
            {
                case LineSpacingStyle.SpaceSpecify :
                    _CommandParameter.LineSpacing = (float)(GraphicsUnitConvert.FromTwips((double )txtLineSpacing.Value * 20, GraphicsUnit.Document));
                    break;
                case LineSpacingStyle.SpaceMultiple :
                    _CommandParameter.LineSpacing = (float)txtLineSpacing.Value;
                    break;
                default :
                    _CommandParameter.LineSpacing = 0;
                    break;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
