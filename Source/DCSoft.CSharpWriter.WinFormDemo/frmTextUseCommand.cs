/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCSoft.CSharpWriter;
using DCSoft.CSharpWriter.Controls;
using DCSoft.CSharpWriter.Commands;
using DCSoft.CSharpWriter.Dom;
 
using DCSoft.Drawing;
using DCSoft.CSharpWriter.Data;
using DCSoft.CSharpWriter.Security;
using DCSoft.Common;
using DCSoft.Data;
using System.IO;

namespace DCSoft.CSharpWriter.WinFormDemo
{
    /// <summary>
    /// Demo of DCSoft.CSharpWriter
    /// </summary>
    public partial class frmTextUseCommand : Form
    {
        public frmTextUseCommand()
        {
            InitializeComponent();
            //myEditControl.Enabled = false;
            myEditControl.ServerObject = new ServerObjectSample(this);
            myEditControl.DoubleBuffering = false;
            //myEditControl.ViewMode = Printing.PageViewMode.Normal;
            //myEditControl.Document.Options.EditOptions.AutoEditElementValue = true;
            //myEditControl.IsAdministrator = true;
            //myEditControl.Readonly = true;
            myEditControl.KeyPress += new KeyPressEventHandler(myEditControl_KeyPress);
             //myEditControl.IsAdministrator = true;
            //myEditControl.DocumentControler.DataFilter = new MyDataFilter();
            //myEditControl.HeaderFooterReadonly = true;
            myEditControl.AllowDragContent = true;
            //DocumentContentStyle style = myEditControl.Document.Style;
            //myEditControl.AutoSetDocumentDefaultFont = false;
              
        }

        private void frmTextUseCommand_Load(object sender, EventArgs e)
        {
            //DCSoft.CSharpWriter.Controls.TextWindowsFormsEditorHost.PopupFormSizeFix = new System.Drawing.Size(40, 20);
            myEditControl.Font = new Font(System.Windows.Forms.Control.DefaultFont.Name, 12);

            // 设置编辑器界面双缓冲
            myEditControl.DoubleBuffering = true;// _StartOptions.DoubleBuffering;
            // 初始化设置命令执行器
            myEditControl.CommandControler = myCommandControler;
            //myEditControl.CommandControler.UpdateBindingControlStatus();
            myCommandControler.Start();

            myEditControl.DocumentOptions = new DocumentOptions();
            // 设置文档处于调试模式
            myEditControl.DocumentOptions.BehaviorOptions.DebugMode = true;

            // Without permission control
            myEditControl.DocumentOptions.SecurityOptions.EnableLogicDelete = false;
            myEditControl.DocumentOptions.SecurityOptions.EnablePermission = false;
            myEditControl.DocumentOptions.SecurityOptions.ShowLogicDeletedContent = false;
            myEditControl.DocumentOptions.SecurityOptions.ShowPermissionMark = false;
            
            myEditControl.AutoUserLogin = false;

            btnDemoFiles_Click(null, null);
             
        }
         

        /// <summary>
        /// Handle after load document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myEditControl_DocumentLoad(object sender, EventArgs e)
        {
            
        } 

        void myEditControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                 
            }
        }
         

        /// <summary>
        /// Demo of server object in document
        /// </summary>
        public class ServerObjectSample
        {
            public ServerObjectSample(frmTextUseCommand frm)
            {
                _Form = frm;
            }

            private frmTextUseCommand _Form = null;
            public frmTextUseCommand Form
            {
                get
                {
                    return _Form;
                }
            }

            public string FormTitle
            {
                get
                {
                    return _Form.Text;
                }
            }

            public string AppPath
            {
                get
                {
                    return Application.StartupPath;
                }
            }

            private string _Name = "Zhang san";

            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }

            private DateTime _Birthday = new DateTime( 1990 , 1 , 1);

            public DateTime Birthday
            {
                get { return _Birthday; }
                set { _Birthday = value; }
            }

            private string _Nation = "China";

            public string Nation
            {
                get { return _Nation; }
                set { _Nation = value; }
            }
        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        private void menuOpenXMLDemo_Click(object sender, EventArgs e)
        {
            HandleCommand(MyCommandNames.OpenXMLDemo);
        }

        private void menuOpenRTFDemo_Click(object sender, EventArgs e)
        {
            HandleCommand(MyCommandNames.OpenRTFDemo);
        }

        private void mOpenFormViewDemo_Click(object sender, EventArgs e)
        {
            HandleCommand(MyCommandNames.OpenFormViewDemo);
        }

        /// <summary>
        /// Current file name.
        /// </summary>
        private string strFileName = null;
         
        /// <summary>
        /// Handle commands
        /// </summary>
        /// <param name="Command">command name</param>
        public void HandleCommand(string Command)
        {
            if (Command == null)
                return;
            Command = Command.Trim();
            try
            {
                switch (Command)
                {
                    case MyCommandNames.OpenXMLDemo:
                        if (QuerySave())
                        {
                            this.strFileName = null;
                            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                            string name = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
                            name = name.Trim().ToLower();
                            name = "DCSoft.CSharpWriter.WinFormDemo.DemoFile.demo.xml";
                            myEditControl.ExecuteCommand("FormViewMode", false, false);
                            using (System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream(name))
                            {
                                myEditControl.LoadDocument(stream, FileFormat.XML);
                                stream.Close();
                            }
                            this.Cursor = System.Windows.Forms.Cursors.Default;
                        }
                        break;
                    case MyCommandNames.OpenFormViewDemo :
                        if (QuerySave())
                        {
                            this.strFileName = null;
                            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                            string name = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
                            name = name.Trim().ToLower();
                            name = "DCSoft.CSharpWriter.WinFormDemo.DemoFile.FormViewModeDemo.xml";
                            using (System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream(name))
                            {
                                myEditControl.LoadDocument(stream, FileFormat.XML);
                                stream.Close();
                            }
                            myEditControl.ExecuteCommand("FormViewMode", false, true);
                            myEditControl.AutoScrollPosition = new Point(0, 0);
                            myEditControl.UpdateTextCaret();
                            this.Cursor = System.Windows.Forms.Cursors.Default;
                        }
                        break;
                    case MyCommandNames.OpenRTFDemo:
                        if (QuerySave())
                        {
                            this.strFileName = null;
                            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                            string name = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
                            name = name.Trim().ToLower();
                            name = "DCSoft.CSharpWriter.WinFormDemo.DemoFile.demo.rtf";
                            using (System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream(name))
                            {
                                this.myEditControl.LoadDocument(stream, FileFormat.RTF);
                                stream.Close();
                            }
                            myEditControl.ExecuteCommand("FormViewMode", false, false);
                            this.Cursor = System.Windows.Forms.Cursors.Default;
                        }
                        break;
                    case MyCommandNames.New:
                        // new document
                        if (QuerySave())
                        {
                            this.myEditControl.ClearContent();
                            //myEditControl.Document.HeaderString = GetResourceString("_TEXTDEMO");
                            //myEditControl.Document.HeaderHeight = 100 ;
                            //myEditControl.Document.HeaderFont = new XFontValue( System.Windows.Forms.Control.DefaultFont.Name , 12 );
                            //myEditControl.Document.FooterString = "[%pageindex%]/[%pagecount%]";
                            //myEditControl.Document.FooterHeight = 100 ;
                            //myEditControl.Document.FooterFont = new XFontValue( System.Windows.Forms.Control.DefaultFont.Name , 12 );
                            myEditControl.RefreshDocument();
                            strFileName = null;
                        }
                        break;
                    case MyCommandNames.Open:
                        // open document
                        if (QuerySave())
                        {
                            using (System.Windows.Forms.OpenFileDialog dlg = new OpenFileDialog())
                            {
                                dlg.Filter = "*.xml|*.xml";
                                dlg.CheckFileExists = true;
                                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                {
                                    this.Update();
                                    this.OpenDocument(dlg.FileName);
                                }
                            }
                        }
                        break;
                    case MyCommandNames.Save:
                        // save document
                        SaveDocument(strFileName);
                        break;
                    case MyCommandNames.SaveAs:
                        // save document as
                        SaveDocument(null);
                        break;
                      
                    //case MyCommandNames.ShowBookmark:
                    //    // show or hide bookmark
                    //    myEditControl.Document.ViewOptions.ShowBookmark = !myEditControl.Document.ViewOptions.ShowBookmark;
                    //    //menuShowBookmark.Checked = myEditControl.Document.ShowBookmark ;
                    //    this.mShowBookmark.Checked = myEditControl.Document.ViewOptions.ShowBookmark;
                    //    myEditControl.Invalidate();
                    //    break;
                    //case MyCommandNames.WordCount:
                    //    this.ShowWordCount();
                    //    break;
                     
                    default:
#if DEBUG
                        this.Alert("Bad command:" + Command);
#endif
                        break;
                }
            }
            catch (Exception ext)
            {
                this.Alert( "error:"  + ext.ToString());
                this.Status = "error:"  + ext.Message;
            }
            this.UpdateFormText();
        }


        /// <summary>
        /// Show a message box
        /// </summary>
        /// <param name="msg">message text</param>
        private void Alert(string msg)
        {
            MessageBox.Show(this, msg, this.Text);
        }


        /// <summary>
        /// get or set main status text
        /// </summary>
        private string Status
        {
            get { return this.lblStatus.Text; }
            set { this.lblStatus.Text = value; this.Update(); }
        }


        /// <summary>
        /// open special file
        /// </summary>
        /// <param name="fileName">file name ,it can be xml,rtf or txt file</param>
        private void OpenDocument(string fileName)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                string name = fileName.Trim().ToLower();
                FileFormat style = FileFormat.XML;
                if (name.EndsWith(".xml"))
                {
                    style = FileFormat.XML;
                }
                else if (name.EndsWith(".rtf"))
                {
                    style = FileFormat.RTF;
                }
                else if (name.EndsWith(".txt"))
                {
                    style = FileFormat.Text;
                }
                this.Status = "Loading" + fileName;
                this.myEditControl.LoadDocument(fileName, style);
                this.strFileName = fileName;
                this.Status = "loaded " + fileName;
                UpdateFormText();
            }
            catch (Exception ext)
            {
                this.Alert(
                    string.Format(
                        "open file'{0}' error:'{1}'",
                    fileName,
                    ext.ToString()));
                this.Status = string.Format(
                        "open file'{0}' error:'{1}'",
                    fileName,
                    ext.Message);
            }
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        /// <summary>
        /// save document
        /// </summary>
        /// <param name="name">file name</param>
        /// <returns></returns>
        private bool SaveDocument(string name)
        {
            if (name == null)
            {
                using (System.Windows.Forms.SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "*.xml|*.xml";
                    dlg.CheckPathExists = true;
                    if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        name = dlg.FileName;
                    else
                        return false;
                }
                this.Update();
            }

            FileFormat style = FileFormat.XML;

            try
            {
                string name2 = name.Trim().ToLower();
                if (name2.EndsWith(".rtf"))
                    style = FileFormat.RTF;
                else
                    style = FileFormat.XML;
                this.Status = "Saving " + name;
                if (this.myEditControl.SaveDocument(name, style))
                {
                    strFileName = name;
                }
                this.Status = "Saved " + name;
                UpdateFormText();
                return true;
            }
            catch (Exception ext)
            {
                string txt = string.Format("Save file'{0}' error:'{1}'", name, ext.ToString());
                this.Alert(txt);
                this.Status = txt;
                return false;
            }
        }


        /// <summary>
        /// If document modified , show query message
        /// </summary>
        /// <returns>If return true , then can go no ; return false , cancel operation</returns>
        private bool QuerySave()
        {
            if (this.myEditControl.Document.Modified)
            {
                System.Windows.Forms.DialogResult result = MessageBox.Show(
                    this,
                    "File content changed ,save it?",
                    this.Text,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (SaveDocument(strFileName) == false)
                        return false;
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                    return true;
                else if (result == System.Windows.Forms.DialogResult.Cancel)
                    return false;
            }
            return true;
        }
         
        /// <summary>
        /// handle element hover event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myEditControl_HoverElementChanged(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Handle selection changed event in editor control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myEditControl_SelectionChanged(object sender, EventArgs e)
        {
            

            DomContentLine line = myEditControl.Document.CurrentContentElement.CurrentLine;
            if (line != null && line.OwnerPage != null)
            {
                string txt =
                   string.Format("Page:{0} Line:{1} Column:{2}",
                        Convert.ToString(line.OwnerPage.PageIndex),
                        Convert.ToString(myEditControl.CurrentLineIndexInPage ),
                        Convert.ToString(myEditControl.CurrentColumnIndex));
                if (myEditControl.Selection.Length != 0)
                {
                    txt = txt + string.Format(
                        " Selected{0}elements",
                        Math.Abs(myEditControl.Selection.Length));
                }
                this.lblPosition.Text = txt;
            }
            UpdateFormText();
             
        }

        private void UpdateFormText()
        {
            string text = "DCSoft.CSharpWriter";
            if (string.IsNullOrEmpty(this.myEditControl.Document.Info.Title) == false)
            {
                text = myEditControl.Document.Info.Title + "-" + text;
            }
            else if ( string.IsNullOrEmpty( this.myEditControl.Document.FileName ) == false )
            {
                text =myEditControl.Document.FileName + " - " + text ;
            }
            if (myEditControl.Document.Modified)
            {
                text = text + " *";
            }
            this.Text = text;
        }

        private void frmTextUseCommand_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (QuerySave() == false)
            {
                e.Cancel = true;
            }
        }
         

        /// <summary>
        /// 文档内容发生改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myEditControl_DocumentContentChanged(object sender, EventArgs e)
        {
            //System.Console.WriteLine("");
            //System.Diagnostics.Debug.WriteLine(
            //    System.Environment.TickCount + ":" + myEditControl.DocumentContentVersion);
            //XTextInputFieldElement field = myEditControl.Document.CurrentField as XTextInputFieldElement;

        }
         
         
        private void myEditControl_StatusTextChanged(object sender, EventArgs e)
        {
            lblStatus.Text = myEditControl.StatusText;
            this.statusStrip1.Refresh();
        }

        private void btnDemoFiles_Click(object sender, EventArgs e)
        {
            var ms = this.GetType().Assembly.GetManifestResourceStream("DCSoft.CSharpWriter.WinFormDemo.about.xml");
            this.myEditControl.LoadDocument(ms, FileFormat.XML);
        }
    }
}