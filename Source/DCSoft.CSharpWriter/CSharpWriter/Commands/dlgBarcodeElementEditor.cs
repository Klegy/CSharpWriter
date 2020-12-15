using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCSoft.Barcode;
using DCSoft.CSharpWriter.Dom;

namespace DCSoft.CSharpWriter.Commands
{
    public partial class dlgBarcodeElementEditor : Form
    {
        public dlgBarcodeElementEditor()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private ElementEditEventArgs _SourceEventArgs = null;

        public ElementEditEventArgs SourceEventArgs
        {
            get { return _SourceEventArgs; }
            set { _SourceEventArgs = value; }
        }

        //private XTextBarcodeFieldElementProperties _InputProperties = null;

        //public XTextBarcodeFieldElementProperties InputProperties
        //{
        //    get { return _InputProperties; }
        //    set { _InputProperties = value; }
        //}

        private void dlgBarcodeElementProperties_Load(object sender, EventArgs e)
        {
            foreach (object item in Enum.GetValues(typeof(DCSoft.Barcode.BarcodeStyle)))
            {
                cboBarcodeStyle.Items.Add(item);
            }
            if (this.SourceEventArgs != null)
            {
                DomBarcodeFieldElement field = (DomBarcodeFieldElement)this.SourceEventArgs.Element;
                txtID.Text = field.ID;
                txtName.Text = field.Name;
                cboBarcodeStyle.Text = field.BarcodeStyle.ToString();
                cboTextAlignment.SelectedIndex = (int)field.TextAlignment;
                chkShowText.Checked = field.ShowText;
                txtMinWidth.Value = (decimal)field.MinBarWidth;
                if (this.SourceEventArgs.Method == ElementEditMethod.Edit)
                {
                    txtInitalizeText.Text = field.Text;
                }
                else
                {
                    txtInitalizeText.Text = field.InitalizeText;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SourceEventArgs != null &&
                this.SourceEventArgs.Element is DomBarcodeFieldElement )
            {
                bool result = false;
                DomBarcodeFieldElement field = (DomBarcodeFieldElement)this.SourceEventArgs.Element ;
                bool logUndo = this.SourceEventArgs.LogUndo 
                    && this.SourceEventArgs.Document.CanLogUndo;

                DomDocument document = this.SourceEventArgs.Document;

                string txt = this.txtID.Text.Trim();
                if (txt.Length == 0)
                {
                    txt = null;
                }
                if (txt != field.ID)
                {
                    if (logUndo )
                    {
                        document.UndoList.AddProperty("ID", field.ID, txt, field);
                    }
                    field.ID = txt;
                    result = true;
                }
                txt = this.txtName.Text.Trim();
                if (txt.Length == 0)
                {
                    txt = null;
                }
                if (txt != field.Name)
                {
                    if (logUndo )
                    {
                        document.UndoList.AddProperty("Name", field.Name, txt, field );
                    }
                    field.Name = txt;
                    result = true;
                }
                BarcodeStyle bs = (BarcodeStyle)Enum.Parse(typeof(BarcodeStyle), cboBarcodeStyle.Text);

                if (bs != field.BarcodeStyle)
                {
                    if (logUndo && document.CanLogUndo)
                    {
                        document.UndoList.AddProperty("BarcodeStyle", field.BarcodeStyle, bs, field );
                    }
                    field.BarcodeStyle = bs;
                    result = true;
                }
                StringAlignment ta = (StringAlignment)cboTextAlignment.SelectedIndex;
                if (ta != field.TextAlignment)
                {
                    if (logUndo && document.CanLogUndo)
                    {
                        document.UndoList.AddProperty(
                            "TextAlignment",
                            field.TextAlignment,
                            ta,
                            field );
                    }
                    field.TextAlignment = ta;
                    result = true;
                }
                if ( chkShowText.Checked != field.ShowText)
                {
                    if (logUndo && document.CanLogUndo)
                    {
                        document.UndoList.AddProperty("ShowText", field.ShowText, chkShowText.Checked , field );
                    }
                    field.ShowText = chkShowText.Checked;
                    result = true;
                }
                float mbw = (float)txtMinWidth.Value;
                if (mbw != field.MinBarWidth)
                {
                    if (logUndo && document.CanLogUndo)
                    {
                        document.UndoList.AddProperty("MinBarWidth", field.MinBarWidth, mbw, field );
                    }
                    field.MinBarWidth = mbw;
                    result = true;
                }

                if (txtInitalizeText.Text != field.Text)
                {
                    field.OwnerDocument = this.SourceEventArgs.Document;
                    if (this.SourceEventArgs.Method == ElementEditMethod.Edit)
                    {
                        field.SetEditorTextExt(txtInitalizeText.Text, DomAccessFlags.Normal, false, false);
                        //XTextElementList list = this.SourceEventArgs.Document.CreateTextElements(txtInitalizeText.Text, null, field.Style);
                        //if (logUndo && document.CanLogUndo )
                        //{
                        //    document.UndoList.AddProperty("Elements", field.Elements, list, field);
                        //}
                        //field.Elements = list;
                        result = true;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtInitalizeText.Text) == false)
                        {
                            field.SetInnerTextFast(txtInitalizeText.Text);
                            result = true;
                        }
                    }
                }
                if (this.SourceEventArgs.Method == ElementEditMethod.Edit)
                {
                     
                    if (result)
                    {
                        document.Modified = true;
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        ContentChangedEventArgs args = new ContentChangedEventArgs();
                        args.Document = field.OwnerDocument;
                        args.Element = field;
                        args.LoadingDocument = false;
                        field.RaiseBubbleOnContentChanged(args);
                    }
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboBarcodeStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            picPreview.Refresh();
        }

        private void cboTextAlignment_SelectedIndexChanged(object sender, EventArgs e)
        {
            picPreview.Refresh();
        }

        private void chkShowText_CheckedChanged(object sender, EventArgs e)
        {
            picPreview.Refresh();
        }

        private void txtMinWidth_ValueChanged(object sender, EventArgs e)
        {
            picPreview.Refresh();
        }


        private void txtInitalizeText_TextChanged(object sender, EventArgs e)
        {
            picPreview.Refresh();
        }

        private void picPreview_Paint(object sender, PaintEventArgs e)
        {
            BarcodeDrawer drawer = new BarcodeDrawer();
            drawer.Font = ( Font ) this.Font.Clone();
            drawer.ShowText = chkShowText.Checked ;
            drawer.Style = (BarcodeStyle)Enum.Parse(typeof(BarcodeStyle), cboBarcodeStyle.Text);
            if (string.IsNullOrEmpty(txtInitalizeText.Text))
            {
                drawer.Text = BarcodeDrawer.GetSampleValue(drawer.Style);
            }
            else
            {
                drawer.Text = txtInitalizeText.Text;
            }
            drawer.TextAlignment = (StringAlignment)cboTextAlignment.SelectedIndex;
            drawer.MinBarWidth = DCSoft.Drawing.GraphicsUnitConvert.Convert((float)txtMinWidth.Value, GraphicsUnit.Document, GraphicsUnit.Pixel);
            drawer.Encode();
            Rectangle rect = new Rectangle(
                (int)(picPreview.ClientSize.Width - drawer.Width) / 2,
                10,
                (int)drawer.Width,
                picPreview.ClientSize.Height - 20);
            drawer.Draw(e.Graphics, rect);
        }


    }
}
