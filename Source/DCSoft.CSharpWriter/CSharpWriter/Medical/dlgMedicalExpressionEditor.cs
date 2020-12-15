using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCSoft.CSharpWriter.Dom ;
using DCSoft.CSharpWriter.Commands;
using DCSoft.CSharpWriter;

namespace DCSoft.CSharpWriter.Medical
{
    public partial class dlgMedicalExpressionEditor : Form
    {
        public dlgMedicalExpressionEditor()
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

        private DocumentContentStyle _CurrentContentStyle = null;
        /// <summary>
        /// 当前文档内容样式
        /// </summary>
        public DocumentContentStyle CurrentContentStyle
        {
            get { return _CurrentContentStyle; }
            set { _CurrentContentStyle = value; }
        }

          

        private void dlgMedicalExpressionProperties_Load(object sender, EventArgs e)
        {
            if (this.SourceEventArgs != null)
            {
                XTextMedicalExpressionFieldElement field = (XTextMedicalExpressionFieldElement)this.SourceEventArgs.Element;
                txtName.Text = field.Name;
                cboStyle.SelectedIndex = (int)field.ExpressionStyle;
            }
        }

        private void cboStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            picPreview.Refresh();
        }

        private void picPreview_Paint(object sender, PaintEventArgs e)
        {
            MedicalExpressionRender render = new MedicalExpressionRender();
            XTextMedicalExpressionFieldElement field = null;
            if (this.SourceEventArgs != null)
            {
                field = (XTextMedicalExpressionFieldElement)this.SourceEventArgs.Element;
            }
            if (field == null)
            {
                render.Value1 = "Value1";
                render.Value2 = "Value2";
                render.Value3 = "Value3";
                render.Value4 = "Value4";
                if (_CurrentContentStyle != null)
                {
                    render.Font = _CurrentContentStyle.Font;
                    render.ForeColor = _CurrentContentStyle.Color;
                }
            }
            else
            {
                field.CheckShapeState( false );
                render.Value1 = field.ExpressionRender.Value1;
                render.Value2 = field.ExpressionRender.Value2;
                render.Value3 = field.ExpressionRender.Value3;
                render.Value4 = field.ExpressionRender.Value4;
                render.Font = field.Style.Font;
                render.ForeColor = field.Style.Color;
            }
            render.Style = (MedicalExpressionStyle)cboStyle.SelectedIndex;
            render.Render(e.Graphics, new RectangleF(0, 0, picPreview.Width, picPreview.Height));
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SourceEventArgs != null)
            {
                bool logUndo = this.SourceEventArgs.LogUndo && this.SourceEventArgs.Document.CanLogUndo;
                XTextMedicalExpressionFieldElement field = (XTextMedicalExpressionFieldElement)this.SourceEventArgs.Element;
                DomDocument document = this.SourceEventArgs.Document;
                bool modified = false;
                if (field.Name != this.Name)
                {
                    if (logUndo)
                    {
                        document.UndoList.AddProperty(
                            "Name",
                            field.Name,
                            this.Name,
                            field);
                    }
                    field.Name = this.Name;
                    modified = true;
                }
                MedicalExpressionStyle style = (MedicalExpressionStyle)cboStyle.SelectedIndex;
                if (field.ExpressionStyle != style)
                {
                    if (logUndo && document.CanLogUndo)
                    {
                        document.UndoList.AddProperty(
                            "ExpressionStyle",
                            field.ExpressionStyle,
                            style,
                            field);
                    }
                    field.ExpressionStyle = style;
                    modified = true;
                }
                if (this.SourceEventArgs.Method == ElementEditMethod.Edit)
                {
                    if (modified)
                    {
                        if (field.EditMode == false)
                        {
                            field.SizeInvalid = true;
                            DomContentElement ce = field.ContentElement;
                            field.CheckShapeState(true);
                            ce.RefreshPrivateContent(ce.PrivateContent.IndexOf(field));
                        }
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
