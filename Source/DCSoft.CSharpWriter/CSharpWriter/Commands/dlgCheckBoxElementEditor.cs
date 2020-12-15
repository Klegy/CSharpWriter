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
    public partial class dlgCheckBoxElementEditor : Form
    {
        public dlgCheckBoxElementEditor()
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

        //private XTextCheckBoxElementProperties _ElementProperties = null;

        //public XTextCheckBoxElementProperties ElementProperties
        //{
        //    get { return _ElementProperties; }
        //    set { _ElementProperties = value; }
        //}

        private void dlgCheckBoxElementProperties_Load(object sender, EventArgs e)
        {
            if (this.SourceEventArgs != null && this.SourceEventArgs.Element is DomCheckBoxElement )
            {
                DomCheckBoxElement chk = (DomCheckBoxElement)this.SourceEventArgs.Element;
                txtName.Text = chk.Name;
                txtValue.Text = chk.CheckedValue;
                chkCheckBox.Checked = (chk.ControlStyle == CheckBoxControlStyle.CheckBox);
                rdoRadio.Checked = (chk.ControlStyle == CheckBoxControlStyle.RadioBox);
                chkChecked.Checked = chk.Checked;
                chkDeleteable.Checked = chk.Deleteable;
                txtGroupName.Text = chk.GroupName;
                chkEnabled.Checked = chk.Enabled;
                txtTag.Text = chk.Tag;
                txtTooltip.Text = chk.ToolTip;
                //if (chk.EventExpressions != null)
                //{
                //    foreach (EventExpressionInfo item in chk.EventExpressions)
                //    {
                //        if (item.EventName == WriterConst.EventName_ContentChanged
                //            && item.Target == EventExpressionTarget.Custom)
                //        {
                //            string exp = item.Expression;
                //            if (Evaluant.Calculator.Expression.EqualsExpression(item.Expression, "this.Checked=true"))
                //            {
                //                txtCheckedCascadeContainerName.Text = item.CustomTargetName;
                //            }
                //            else if (Evaluant.Calculator.Expression.EqualsExpression(item.Expression, "this.Checked=false"))
                //            {
                //                txtUnCheckedCascadeContainerName.Text = item.CustomTargetName;
                //            }
                //        }//if
                //    }//foreach
                //    txtEventExpressions.Tag = chk.EventExpressions;
                //    txtEventExpressions.Text = chk.EventExpressions.ToString();
                //}
            }
            chkCheckBox.CheckedChanged += new EventHandler(chkCheckBox_CheckedChanged);
            rdoRadio.CheckedChanged += new EventHandler(rdoRadio_CheckedChanged);
           
            //if (_ElementProperties == null)
            //{
            //    _ElementProperties = new XTextCheckBoxElementProperties();
            //}
            //txtName.Text = _ElementProperties.Name;
            //txtValue.Text = _ElementProperties.CheckedValue;
            //chkCheckBox.Checked = ( _ElementProperties.ControlStyle == CheckBoxControlStyle.CheckBox );
            //rdoRadio.Checked = (_ElementProperties.ControlStyle == CheckBoxControlStyle.RadioBox);
            //chkChecked.Checked = _ElementProperties.Checked;
            //chkDeleteable.Checked = _ElementProperties.Deleteable;
            //txtGroupName.Text = _ElementProperties.GroupName;
            //chkEnabled.Checked = _ElementProperties.Enabled;
            //txtTag.Text = _ElementProperties.Tag;
            //txtTooltip.Text = _ElementProperties.ToolTip;
            //txtCheckedCascadeContainerName.Text = _ElementProperties.CheckedCascadeContainerName;
            //txtUnCheckedCascadeContainerName.Text = _ElementProperties.UnCheckedCascadeContainerName;
            //if (_ElementProperties.EventExpressions != null)
            //{
            //    txtEventExpressions.Text = _ElementProperties.EventExpressions.ToString();
            //    txtEventExpressions.Tag = _ElementProperties.EventExpressions;
            //}
        }

        void rdoRadio_CheckedChanged(object sender, EventArgs e)
        {
            chkCheckBox.Checked = !rdoRadio.Checked;
        }

        void chkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            rdoRadio.Checked = !chkCheckBox.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SourceEventArgs != null)
            {
                bool result = false;
                DomCheckBoxElement chk = (DomCheckBoxElement)this.SourceEventArgs.Element;
                DomDocument document = this.SourceEventArgs.Document;
                bool logUndo = this.SourceEventArgs.LogUndo && document.CanLogUndo  ;
                string txt = null;
                //EventExpressionInfoList expresses = (EventExpressionInfoList ) txtEventExpressions.Tag;
                //string txt = txtCheckedCascadeContainerName.Text.Trim();
                //if ( string.IsNullOrEmpty( txt ) == false )
                //{
                //    // 设置勾选级联对象设置
                //    if (expresses == null)
                //    {
                //        expresses = new EventExpressionInfoList();
                //    }
                //    EventExpressionInfo matchItem = null;
                //    foreach (EventExpressionInfo item in expresses)
                //    {
                //        if (item.EventName == WriterConst.EventName_ContentChanged
                //            && Evaluant.Calculator.Expression.EqualsExpression(item.Expression, "this.Checked=true"))
                //        {
                //            matchItem = item;
                //            break;
                //        }
                //    }//foreach

                //    if (matchItem == null)
                //    {
                //        matchItem = new EventExpressionInfo();
                //        matchItem.EventName = WriterConst.EventName_ContentChanged;
                //        matchItem.Expression = "this.Checked=true";
                //        expresses.Add(matchItem);
                //    }
                //    matchItem.Target = EventExpressionTarget.Custom;
                //    matchItem.CustomTargetName = txt;
                //}
                //txt = txtUnCheckedCascadeContainerName.Text.Trim();
                //if (string.IsNullOrEmpty( txt ) == false)
                //{
                //    // 设置未勾选级联对象
                //    if (expresses == null)
                //    {
                //        expresses = new EventExpressionInfoList();
                //    }
                //    EventExpressionInfo matchItem = null;
                //    foreach (EventExpressionInfo item in expresses)
                //    {
                //        if (item.EventName == WriterConst.EventName_ContentChanged
                //            && Evaluant.Calculator.Expression.EqualsExpression(item.Expression, "this.Checked=false"))
                //        {
                //            matchItem = item;
                //            break;
                //        }
                //    }//foreach

                //    if (matchItem == null)
                //    {
                //        matchItem = new EventExpressionInfo();
                //        matchItem.EventName = WriterConst.EventName_ContentChanged;
                //        matchItem.Expression = "this.Checked=false";
                //        expresses.Add(matchItem);
                //    }
                //    matchItem.Target = EventExpressionTarget.Custom;
                //    matchItem.CustomTargetName = txt;
                //}

                //if (expresses != null)
                //{
                //    if ( logUndo )
                //    {
                //        document.UndoList.AddProperty(
                //            "EventExpressions",
                //            chk.EventExpressions,
                //            expresses,
                //            chk);
                //    }
                //    chk.EventExpressions = expresses;
                //    result = true;
                //}
                CheckBoxControlStyle cs = CheckBoxControlStyle.CheckBox;
                if (chkCheckBox.Checked)
                {
                    cs = CheckBoxControlStyle.CheckBox;
                }
                else
                {
                    cs = CheckBoxControlStyle.RadioBox;
                }
                if (chk.ControlStyle != cs)
                {
                    if (logUndo )
                    {
                        document.UndoList.AddProperty("ControlStyle", chk.ControlStyle, cs, chk);
                    }
                    chk.ControlStyle = cs;
                    result = true;
                }
                if (chk.Checked != chkCheckBox.Checked )
                {
                    if (logUndo )
                    {
                        document.UndoList.AddProperty("Checked", chk.Checked, chkCheckBox.Checked , chk);
                    }
                    chk.Checked = chkCheckBox.Checked ;
                    result = true;
                }
                if (chk.Enabled != chkEnabled.Checked )
                {
                    if (logUndo )
                    {
                        document.UndoList.AddProperty("Enabled", chk.Enabled, chkEnabled.Checked , chk);
                    }
                    chk.Enabled = chkEnabled.Checked ;
                    result = true;
                }
                
                txt = txtTag.Text;
                if (txt.Length == 0)
                {
                    txt = null;
                }
                if (chk.Tag != txt )
                {
                    if (logUndo )
                    {
                        document.UndoList.AddProperty("Tag", chk.Tag, txt , chk);
                    }
                    chk.Tag = txt;
                    result = true;
                }
                txt = txtTooltip.Text;
                if (txt.Length == 0)
                {
                    txt = null;
                }
                if (chk.ToolTip != txt )
                {
                    if (logUndo )
                    {
                        document.UndoList.AddProperty("ToolTip", chk.ToolTip, txt , chk);
                    }
                    chk.ToolTip = txt ;
                    result = true;
                }
                txt = txtValue.Text;
                if (txt.Length == 0)
                {
                    txt = null;
                }
                if (chk.CheckedValue != txt )
                {
                    if (logUndo )
                    {
                        document.UndoList.AddProperty("CheckedValue", chk.CheckedValue, txt , chk);
                    }
                    chk.CheckedValue = txt ;
                    result = true;
                }
                //if (chk.UnCheckedValue !=  this.UnCheckedValue)
                //{
                //    if (logUndo && document.CanLogUndo)
                //    {
                //        document.UndoList.AddProperty("UnCheckedValue", chk.UnCheckedValue, this.CheckedValue, chk);
                //    }
                //    chk.UnCheckedValue = this.CheckedValue;
                //    result = true;
                //}
                if (chk.Deleteable != chkDeleteable.Checked )
                {
                    if (logUndo )
                    {
                        document.UndoList.AddProperty("Deleteable", chk.Deleteable, chkDeleteable.Checked , chk);
                    }
                    chk.Deleteable = chkDeleteable.Checked ;
                    result = true;
                }
                txt = txtName.Text;
                if (txt.Length == 0)
                {
                    txt = null;
                }
                if (chk.Name != txt )
                {
                    if (logUndo )
                    {
                        document.UndoList.AddProperty("Name", chk.Name, txt , chk);
                    }
                    chk.Name = txt ;
                    result = true;
                }
                txt = txtGroupName.Text;
                if (txt.Length == 0)
                {
                    txt = null;
                }
                if (chk.GroupName != txt )
                {
                    if (logUndo )
                    {
                        document.UndoList.AddProperty("GroupName", chk.GroupName, txt , chk);
                    }
                    chk.GroupName = txt ;
                    result = true;
                }
                if (this.SourceEventArgs.Method == ElementEditMethod.Edit)
                {
                    if (result)
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        ContentChangedEventArgs args = new ContentChangedEventArgs();
                        args.Document = chk.OwnerDocument;
                        args.Element = chk;
                        args.LoadingDocument = false;
                        chk.Parent.RaiseBubbleOnContentChanged(args);
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

         
    }
}
