/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
namespace DCSoft.CSharpWriter.WinFormDemo
{
    partial class frmTextUseCommand
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTextUseCommand));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mOpenUrl = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mDocumentDefaultFont = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPageSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.mRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSpecifyPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.mEditImageShape = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mJumpPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.mPageViewMode = new System.Windows.Forms.ToolStripMenuItem();
            this.mNormalViewMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.mCleanViewMode = new System.Windows.Forms.ToolStripMenuItem();
            this.mComplexViewMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.mFormViewMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            this.mDebugOutWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            this.mDesignMode = new System.Windows.Forms.ToolStripMenuItem();
            this.mInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.mInsertImage = new System.Windows.Forms.ToolStripMenuItem();
            this.mInsertInputField = new System.Windows.Forms.ToolStripMenuItem();
            this.mInsertParameter = new System.Windows.Forms.ToolStripMenuItem();
            this.mInsertCheckBox = new System.Windows.Forms.ToolStripMenuItem();
            this.mInsertMedicalExpression = new System.Windows.Forms.ToolStripMenuItem();
            this.mInsertBarcode = new System.Windows.Forms.ToolStripMenuItem();
            this.mInsertPageInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.mInsertXML = new System.Windows.Forms.ToolStripMenuItem();
            this.mInsertFileContent = new System.Windows.Forms.ToolStripMenuItem();
            this.mInsertContentLink = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.mConvertFieldToContent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
            this.mDeleteField = new System.Windows.Forms.ToolStripMenuItem();
            this.mFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.mParagraphFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.mFont = new System.Windows.Forms.ToolStripMenuItem();
            this.mTextColor = new System.Windows.Forms.ToolStripMenuItem();
            this.mBackColor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.mSup = new System.Windows.Forms.ToolStripMenuItem();
            this.mSub = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mAlignLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.mCenterAlign = new System.Windows.Forms.ToolStripMenuItem();
            this.mRightAlign = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.mNumerList = new System.Windows.Forms.ToolStripMenuItem();
            this.mBulleteList = new System.Windows.Forms.ToolStripMenuItem();
            this.mFirstIndent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
            this.mFieldFixedWidth = new System.Windows.Forms.ToolStripMenuItem();
            this.mTable = new System.Windows.Forms.ToolStripMenuItem();
            this.mInsertTable = new System.Windows.Forms.ToolStripMenuItem();
            this.mDeleteTable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.mCellAlign = new System.Windows.Forms.ToolStripMenuItem();
            this.mAlignTopLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.mAlignTopCenter = new System.Windows.Forms.ToolStripMenuItem();
            this.mAlignTopRight = new System.Windows.Forms.ToolStripMenuItem();
            this.mAlignMiddleLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.mAlignMiddleCenter = new System.Windows.Forms.ToolStripMenuItem();
            this.mAlignMiddleRight = new System.Windows.Forms.ToolStripMenuItem();
            this.mAlignBottomLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.mAlignBottomCenter = new System.Windows.Forms.ToolStripMenuItem();
            this.mAlignBottomRight = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.mInsertRowUp = new System.Windows.Forms.ToolStripMenuItem();
            this.mInsertRowDown = new System.Windows.Forms.ToolStripMenuItem();
            this.mDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mInsertColumnLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.mInsertColumnRight = new System.Windows.Forms.ToolStripMenuItem();
            this.mDeleteColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mMergeCell = new System.Windows.Forms.ToolStripMenuItem();
            this.mSplitCell = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.mHeaderRow = new System.Windows.Forms.ToolStripMenuItem();
            this.mTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mWordCount = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.menuConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mDocumentValueValidate = new System.Windows.Forms.ToolStripMenuItem();
            this.mEditVBAScript = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnDemoFiles = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton19 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.cboFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.btnColor = new System.Windows.Forms.ToolStripButton();
            this.btnBackColor = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton17 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton18 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton13 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton14 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton15 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton16 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblAbout = new System.Windows.Forms.ToolStripStatusLabel();
            this.myImageList = new System.Windows.Forms.ImageList(this.components);
            this.myCommandControler = new DCSoft.CSharpWriter.Commands.CSWriterCommandControler(this.components);
            this.cmRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmCut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.cmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmColor = new System.Windows.Forms.ToolStripMenuItem();
            this.cmFont = new System.Windows.Forms.ToolStripMenuItem();
            this.cmAlignLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.cmAlignCenter = new System.Windows.Forms.ToolStripMenuItem();
            this.cmAlignRight = new System.Windows.Forms.ToolStripMenuItem();
            this.cmProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.myEditControl = new DCSoft.CSharpWriter.Controls.CSWriterControl();
            this.cmEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myCommandControler)).BeginInit();
            this.cmEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuView,
            this.mInsert,
            this.mFormat,
            this.mTable,
            this.mTools,
            this.menuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(811, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewFile,
            this.menuOpen,
            this.mOpenUrl,
            this.menuSave,
            this.menuSaveAs,
            this.toolStripSeparator2,
            this.mDocumentDefaultFont,
            this.menuPageSettings,
            this.menuPrint,
            this.toolStripMenuItem7,
            this.toolStripMenuItem10,
            this.toolStripSeparator3,
            this.menuClose});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(39, 21);
            this.menuFile.Text = "&File";
            // 
            // menuNewFile
            // 
            this.myCommandControler.SetCommandName(this.menuNewFile, "FileNew");
            this.menuNewFile.Image = ((System.Drawing.Image)(resources.GetObject("menuNewFile.Image")));
            this.menuNewFile.Name = "menuNewFile";
            this.menuNewFile.Size = new System.Drawing.Size(180, 22);
            this.menuNewFile.Text = "&New...";
            // 
            // menuOpen
            // 
            this.myCommandControler.SetCommandName(this.menuOpen, "FileOpen");
            this.menuOpen.Image = ((System.Drawing.Image)(resources.GetObject("menuOpen.Image")));
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.Size = new System.Drawing.Size(180, 22);
            this.menuOpen.Text = "&Open...";
            // 
            // mOpenUrl
            // 
            this.myCommandControler.SetCommandName(this.mOpenUrl, "FileOpenUrl");
            this.mOpenUrl.Name = "mOpenUrl";
            this.mOpenUrl.Size = new System.Drawing.Size(180, 22);
            this.mOpenUrl.Text = "Open url...";
            // 
            // menuSave
            // 
            this.myCommandControler.SetCommandName(this.menuSave, "FileSave");
            this.menuSave.Image = ((System.Drawing.Image)(resources.GetObject("menuSave.Image")));
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new System.Drawing.Size(180, 22);
            this.menuSave.Text = "&Save...";
            // 
            // menuSaveAs
            // 
            this.myCommandControler.SetCommandName(this.menuSaveAs, "FileSaveAs");
            this.menuSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("menuSaveAs.Image")));
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(180, 22);
            this.menuSaveAs.Text = "&Save as...";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // mDocumentDefaultFont
            // 
            this.myCommandControler.SetCommandName(this.mDocumentDefaultFont, "DocumentDefaultFont");
            this.mDocumentDefaultFont.Name = "mDocumentDefaultFont";
            this.mDocumentDefaultFont.Size = new System.Drawing.Size(180, 22);
            this.mDocumentDefaultFont.Text = "Default font ...";
            // 
            // menuPageSettings
            // 
            this.myCommandControler.SetCommandName(this.menuPageSettings, "FilePageSettings");
            this.menuPageSettings.Image = ((System.Drawing.Image)(resources.GetObject("menuPageSettings.Image")));
            this.menuPageSettings.Name = "menuPageSettings";
            this.menuPageSettings.Size = new System.Drawing.Size(180, 22);
            this.menuPageSettings.Text = "Page settings...";
            // 
            // menuPrint
            // 
            this.myCommandControler.SetCommandName(this.menuPrint, "FilePrint");
            this.menuPrint.Image = ((System.Drawing.Image)(resources.GetObject("menuPrint.Image")));
            this.menuPrint.Name = "menuPrint";
            this.menuPrint.Size = new System.Drawing.Size(180, 22);
            this.menuPrint.Text = "&Print...";
            // 
            // toolStripMenuItem7
            // 
            this.myCommandControler.SetCommandName(this.toolStripMenuItem7, "FileCleanPrint");
            this.toolStripMenuItem7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem7.Image")));
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem7.Text = "Clean print...";
            // 
            // toolStripMenuItem10
            // 
            this.myCommandControler.SetCommandName(this.toolStripMenuItem10, "ViewXMLSource");
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem10.Text = "View xml source...";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // menuClose
            // 
            this.menuClose.Name = "menuClose";
            this.menuClose.Size = new System.Drawing.Size(180, 22);
            this.menuClose.Text = "&Exit";
            this.menuClose.Click += new System.EventHandler(this.menuClose_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mUndo,
            this.mRedo,
            this.toolStripSeparator4,
            this.mCut,
            this.mCopy,
            this.mPaste,
            this.menuSpecifyPaste,
            this.mDelete,
            this.toolStripSeparator5,
            this.mSelectAll,
            this.toolStripSeparator6,
            this.mSearch,
            this.toolStripSeparator17,
            this.mEditImageShape});
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(42, 21);
            this.menuEdit.Text = "&Edit";
            // 
            // mUndo
            // 
            this.myCommandControler.SetCommandName(this.mUndo, "Undo");
            this.mUndo.Image = ((System.Drawing.Image)(resources.GetObject("mUndo.Image")));
            this.mUndo.Name = "mUndo";
            this.mUndo.Size = new System.Drawing.Size(162, 22);
            this.mUndo.Text = "&Undo";
            // 
            // mRedo
            // 
            this.myCommandControler.SetCommandName(this.mRedo, "Redo");
            this.mRedo.Image = ((System.Drawing.Image)(resources.GetObject("mRedo.Image")));
            this.mRedo.Name = "mRedo";
            this.mRedo.Size = new System.Drawing.Size(162, 22);
            this.mRedo.Text = "&Redo";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(159, 6);
            // 
            // mCut
            // 
            this.myCommandControler.SetCommandName(this.mCut, "Cut");
            this.mCut.Image = ((System.Drawing.Image)(resources.GetObject("mCut.Image")));
            this.mCut.Name = "mCut";
            this.mCut.Size = new System.Drawing.Size(162, 22);
            this.mCut.Text = "Cut";
            // 
            // mCopy
            // 
            this.myCommandControler.SetCommandName(this.mCopy, "Copy");
            this.mCopy.Image = ((System.Drawing.Image)(resources.GetObject("mCopy.Image")));
            this.mCopy.Name = "mCopy";
            this.mCopy.Size = new System.Drawing.Size(162, 22);
            this.mCopy.Text = "&Copy";
            // 
            // mPaste
            // 
            this.myCommandControler.SetCommandName(this.mPaste, "Paste");
            this.mPaste.Image = ((System.Drawing.Image)(resources.GetObject("mPaste.Image")));
            this.mPaste.Name = "mPaste";
            this.mPaste.Size = new System.Drawing.Size(162, 22);
            this.mPaste.Text = "&Paste";
            // 
            // menuSpecifyPaste
            // 
            this.myCommandControler.SetCommandName(this.menuSpecifyPaste, "SpecifyPaste");
            this.menuSpecifyPaste.Image = ((System.Drawing.Image)(resources.GetObject("menuSpecifyPaste.Image")));
            this.menuSpecifyPaste.Name = "menuSpecifyPaste";
            this.menuSpecifyPaste.Size = new System.Drawing.Size(162, 22);
            this.menuSpecifyPaste.Text = "Specify paste...";
            // 
            // mDelete
            // 
            this.myCommandControler.SetCommandName(this.mDelete, "Delete");
            this.mDelete.Image = ((System.Drawing.Image)(resources.GetObject("mDelete.Image")));
            this.mDelete.Name = "mDelete";
            this.mDelete.Size = new System.Drawing.Size(162, 22);
            this.mDelete.Text = "&Delete";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(159, 6);
            // 
            // mSelectAll
            // 
            this.myCommandControler.SetCommandName(this.mSelectAll, "SelectAll");
            this.mSelectAll.Name = "mSelectAll";
            this.mSelectAll.Size = new System.Drawing.Size(162, 22);
            this.mSelectAll.Text = "Select &All";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(159, 6);
            // 
            // mSearch
            // 
            this.myCommandControler.SetCommandName(this.mSearch, "SearchReplace");
            this.mSearch.Image = ((System.Drawing.Image)(resources.GetObject("mSearch.Image")));
            this.mSearch.Name = "mSearch";
            this.mSearch.Size = new System.Drawing.Size(162, 22);
            this.mSearch.Text = "&Find";
            this.mSearch.Visible = false;
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(159, 6);
            // 
            // mEditImageShape
            // 
            this.myCommandControler.SetCommandName(this.mEditImageShape, "EditImageAdditionShape");
            this.mEditImageShape.Image = ((System.Drawing.Image)(resources.GetObject("mEditImageShape.Image")));
            this.mEditImageShape.Name = "mEditImageShape";
            this.mEditImageShape.Size = new System.Drawing.Size(162, 22);
            this.mEditImageShape.Text = "编辑图片";
            // 
            // menuView
            // 
            this.menuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mJumpPrint,
            this.toolStripSeparator18,
            this.mPageViewMode,
            this.mNormalViewMode,
            this.toolStripSeparator21,
            this.mCleanViewMode,
            this.mComplexViewMode,
            this.toolStripSeparator23,
            this.mFormViewMode,
            this.toolStripSeparator25,
            this.mDebugOutWindow,
            this.toolStripSeparator28,
            this.mDesignMode});
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(47, 21);
            this.menuView.Text = "&View";
            // 
            // mJumpPrint
            // 
            this.myCommandControler.SetCommandName(this.mJumpPrint, "JumpPrintMode");
            this.mJumpPrint.Name = "mJumpPrint";
            this.mJumpPrint.Size = new System.Drawing.Size(212, 22);
            this.mJumpPrint.Text = "Jump print";
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(209, 6);
            // 
            // mPageViewMode
            // 
            this.myCommandControler.SetCommandName(this.mPageViewMode, "PageViewMode");
            this.mPageViewMode.Name = "mPageViewMode";
            this.mPageViewMode.Size = new System.Drawing.Size(212, 22);
            this.mPageViewMode.Text = "Page view mode";
            // 
            // mNormalViewMode
            // 
            this.myCommandControler.SetCommandName(this.mNormalViewMode, "NormalViewMode");
            this.mNormalViewMode.Name = "mNormalViewMode";
            this.mNormalViewMode.Size = new System.Drawing.Size(212, 22);
            this.mNormalViewMode.Text = "Normal view mode";
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(209, 6);
            // 
            // mCleanViewMode
            // 
            this.myCommandControler.SetCommandName(this.mCleanViewMode, "CleanViewMode");
            this.mCleanViewMode.Name = "mCleanViewMode";
            this.mCleanViewMode.Size = new System.Drawing.Size(212, 22);
            this.mCleanViewMode.Text = "Clean view mode";
            // 
            // mComplexViewMode
            // 
            this.myCommandControler.SetCommandName(this.mComplexViewMode, "ComplexViewMode");
            this.mComplexViewMode.Name = "mComplexViewMode";
            this.mComplexViewMode.Size = new System.Drawing.Size(212, 22);
            this.mComplexViewMode.Text = "Track view mode";
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(209, 6);
            // 
            // mFormViewMode
            // 
            this.myCommandControler.SetCommandName(this.mFormViewMode, "FormViewMode");
            this.mFormViewMode.Name = "mFormViewMode";
            this.mFormViewMode.Size = new System.Drawing.Size(212, 22);
            this.mFormViewMode.Text = "Form view mode";
            // 
            // toolStripSeparator25
            // 
            this.toolStripSeparator25.Name = "toolStripSeparator25";
            this.toolStripSeparator25.Size = new System.Drawing.Size(209, 6);
            // 
            // mDebugOutWindow
            // 
            this.myCommandControler.SetCommandName(this.mDebugOutWindow, "DebugOutputWindow");
            this.mDebugOutWindow.Image = ((System.Drawing.Image)(resources.GetObject("mDebugOutWindow.Image")));
            this.mDebugOutWindow.Name = "mDebugOutWindow";
            this.mDebugOutWindow.Size = new System.Drawing.Size(212, 22);
            this.mDebugOutWindow.Text = "Debug out";
            // 
            // toolStripSeparator28
            // 
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            this.toolStripSeparator28.Size = new System.Drawing.Size(209, 6);
            // 
            // mDesignMode
            // 
            this.myCommandControler.SetCommandName(this.mDesignMode, "DesignMode");
            this.mDesignMode.Image = ((System.Drawing.Image)(resources.GetObject("mDesignMode.Image")));
            this.mDesignMode.Name = "mDesignMode";
            this.mDesignMode.Size = new System.Drawing.Size(212, 22);
            this.mDesignMode.Text = "Design time view mode";
            // 
            // mInsert
            // 
            this.mInsert.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mInsertImage,
            this.mInsertInputField,
            this.mInsertParameter,
            this.mInsertCheckBox,
            this.mInsertMedicalExpression,
            this.mInsertBarcode,
            this.mInsertPageInfo,
            this.mInsertXML,
            this.mInsertFileContent,
            this.mInsertContentLink,
            this.toolStripMenuItem1,
            this.toolStripMenuItem13,
            this.mConvertFieldToContent,
            this.toolStripSeparator30,
            this.mDeleteField});
            this.mInsert.Name = "mInsert";
            this.mInsert.Size = new System.Drawing.Size(53, 21);
            this.mInsert.Text = "&Insert";
            // 
            // mInsertImage
            // 
            this.myCommandControler.SetCommandName(this.mInsertImage, "InsertImage");
            this.mInsertImage.Image = ((System.Drawing.Image)(resources.GetObject("mInsertImage.Image")));
            this.mInsertImage.Name = "mInsertImage";
            this.mInsertImage.Size = new System.Drawing.Size(251, 22);
            this.mInsertImage.Text = "Insert image...";
            // 
            // mInsertInputField
            // 
            this.myCommandControler.SetCommandName(this.mInsertInputField, "InsertInputField");
            this.mInsertInputField.Name = "mInsertInputField";
            this.mInsertInputField.Size = new System.Drawing.Size(251, 22);
            this.mInsertInputField.Text = "Insert input field....";
            // 
            // mInsertParameter
            // 
            this.myCommandControler.SetCommandName(this.mInsertParameter, "InsertParameter");
            this.mInsertParameter.Image = ((System.Drawing.Image)(resources.GetObject("mInsertParameter.Image")));
            this.mInsertParameter.Name = "mInsertParameter";
            this.mInsertParameter.Size = new System.Drawing.Size(251, 22);
            this.mInsertParameter.Text = "Insert parameter...";
            // 
            // mInsertCheckBox
            // 
            this.myCommandControler.SetCommandName(this.mInsertCheckBox, "InsertCheckBox");
            this.mInsertCheckBox.Image = ((System.Drawing.Image)(resources.GetObject("mInsertCheckBox.Image")));
            this.mInsertCheckBox.Name = "mInsertCheckBox";
            this.mInsertCheckBox.Size = new System.Drawing.Size(251, 22);
            this.mInsertCheckBox.Text = "Insert check box...";
            // 
            // mInsertMedicalExpression
            // 
            this.myCommandControler.SetCommandName(this.mInsertMedicalExpression, "InsertMedicalExpression");
            this.mInsertMedicalExpression.Name = "mInsertMedicalExpression";
            this.mInsertMedicalExpression.Size = new System.Drawing.Size(251, 22);
            this.mInsertMedicalExpression.Text = "Insert medical expression";
            // 
            // mInsertBarcode
            // 
            this.myCommandControler.SetCommandName(this.mInsertBarcode, "InsertBarcode");
            this.mInsertBarcode.Image = ((System.Drawing.Image)(resources.GetObject("mInsertBarcode.Image")));
            this.mInsertBarcode.Name = "mInsertBarcode";
            this.mInsertBarcode.Size = new System.Drawing.Size(251, 22);
            this.mInsertBarcode.Text = "Inser barcode...";
            // 
            // mInsertPageInfo
            // 
            this.myCommandControler.SetCommandName(this.mInsertPageInfo, "InsertPageInfo");
            this.mInsertPageInfo.Name = "mInsertPageInfo";
            this.mInsertPageInfo.Size = new System.Drawing.Size(251, 22);
            this.mInsertPageInfo.Text = "Insert pageinfo...";
            // 
            // mInsertXML
            // 
            this.myCommandControler.SetCommandName(this.mInsertXML, "InsertXML");
            this.mInsertXML.Name = "mInsertXML";
            this.mInsertXML.Size = new System.Drawing.Size(251, 22);
            this.mInsertXML.Text = "Insert by xml ...";
            // 
            // mInsertFileContent
            // 
            this.myCommandControler.SetCommandName(this.mInsertFileContent, "InsertFileContent");
            this.mInsertFileContent.Name = "mInsertFileContent";
            this.mInsertFileContent.Size = new System.Drawing.Size(251, 22);
            this.mInsertFileContent.Text = "Insert by file...";
            // 
            // mInsertContentLink
            // 
            this.myCommandControler.SetCommandName(this.mInsertContentLink, "InsertContentLink");
            this.mInsertContentLink.Name = "mInsertContentLink";
            this.mInsertContentLink.Size = new System.Drawing.Size(251, 22);
            this.mInsertContentLink.Text = "Insert content link...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(248, 6);
            // 
            // toolStripMenuItem13
            // 
            this.myCommandControler.SetCommandName(this.toolStripMenuItem13, "ConvertContentToField");
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(251, 22);
            this.toolStripMenuItem13.Text = "Conver content to input field...";
            // 
            // mConvertFieldToContent
            // 
            this.myCommandControler.SetCommandName(this.mConvertFieldToContent, "ConvertFieldToContent");
            this.mConvertFieldToContent.Name = "mConvertFieldToContent";
            this.mConvertFieldToContent.Size = new System.Drawing.Size(251, 22);
            this.mConvertFieldToContent.Text = "Convert input field to content";
            // 
            // toolStripSeparator30
            // 
            this.toolStripSeparator30.Name = "toolStripSeparator30";
            this.toolStripSeparator30.Size = new System.Drawing.Size(248, 6);
            // 
            // mDeleteField
            // 
            this.myCommandControler.SetCommandName(this.mDeleteField, "DeleteField");
            this.mDeleteField.Name = "mDeleteField";
            this.mDeleteField.Size = new System.Drawing.Size(251, 22);
            this.mDeleteField.Text = "Delete field";
            // 
            // mFormat
            // 
            this.mFormat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mParagraphFormat,
            this.mFont,
            this.mTextColor,
            this.mBackColor,
            this.toolStripSeparator7,
            this.mSup,
            this.mSub,
            this.toolStripSeparator8,
            this.mAlignLeft,
            this.mCenterAlign,
            this.mRightAlign,
            this.toolStripSeparator9,
            this.mNumerList,
            this.mBulleteList,
            this.mFirstIndent,
            this.toolStripSeparator26,
            this.mFieldFixedWidth});
            this.mFormat.Name = "mFormat";
            this.mFormat.Size = new System.Drawing.Size(61, 21);
            this.mFormat.Text = "&Format";
            // 
            // mParagraphFormat
            // 
            this.myCommandControler.SetCommandName(this.mParagraphFormat, "ParagraphFormat");
            this.mParagraphFormat.Name = "mParagraphFormat";
            this.mParagraphFormat.Size = new System.Drawing.Size(227, 22);
            this.mParagraphFormat.Text = "Paragraph format...";
            // 
            // mFont
            // 
            this.myCommandControler.SetCommandName(this.mFont, "Font");
            this.mFont.Image = ((System.Drawing.Image)(resources.GetObject("mFont.Image")));
            this.mFont.Name = "mFont";
            this.mFont.Size = new System.Drawing.Size(227, 22);
            this.mFont.Text = "Font...";
            // 
            // mTextColor
            // 
            this.myCommandControler.SetCommandName(this.mTextColor, "Color");
            this.mTextColor.Name = "mTextColor";
            this.mTextColor.Size = new System.Drawing.Size(227, 22);
            this.mTextColor.Text = "Text color";
            // 
            // mBackColor
            // 
            this.myCommandControler.SetCommandName(this.mBackColor, "BorderBackgroundFormat");
            this.mBackColor.Name = "mBackColor";
            this.mBackColor.Size = new System.Drawing.Size(227, 22);
            this.mBackColor.Text = "Background ...";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(224, 6);
            // 
            // mSup
            // 
            this.myCommandControler.SetCommandName(this.mSup, "Superscript");
            this.mSup.Image = ((System.Drawing.Image)(resources.GetObject("mSup.Image")));
            this.mSup.Name = "mSup";
            this.mSup.Size = new System.Drawing.Size(227, 22);
            this.mSup.Text = "Superscript";
            // 
            // mSub
            // 
            this.myCommandControler.SetCommandName(this.mSub, "Subscript");
            this.mSub.Image = ((System.Drawing.Image)(resources.GetObject("mSub.Image")));
            this.mSub.Name = "mSub";
            this.mSub.Size = new System.Drawing.Size(227, 22);
            this.mSub.Text = "Subscript";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(224, 6);
            // 
            // mAlignLeft
            // 
            this.myCommandControler.SetCommandName(this.mAlignLeft, "AlignLeft");
            this.mAlignLeft.Image = ((System.Drawing.Image)(resources.GetObject("mAlignLeft.Image")));
            this.mAlignLeft.Name = "mAlignLeft";
            this.mAlignLeft.Size = new System.Drawing.Size(227, 22);
            this.mAlignLeft.Text = "Align left";
            // 
            // mCenterAlign
            // 
            this.myCommandControler.SetCommandName(this.mCenterAlign, "AlignCenter");
            this.mCenterAlign.Image = ((System.Drawing.Image)(resources.GetObject("mCenterAlign.Image")));
            this.mCenterAlign.Name = "mCenterAlign";
            this.mCenterAlign.Size = new System.Drawing.Size(227, 22);
            this.mCenterAlign.Text = "Align center";
            // 
            // mRightAlign
            // 
            this.myCommandControler.SetCommandName(this.mRightAlign, "AlignRight");
            this.mRightAlign.Image = ((System.Drawing.Image)(resources.GetObject("mRightAlign.Image")));
            this.mRightAlign.Name = "mRightAlign";
            this.mRightAlign.Size = new System.Drawing.Size(227, 22);
            this.mRightAlign.Text = "Align right";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(224, 6);
            // 
            // mNumerList
            // 
            this.myCommandControler.SetCommandName(this.mNumerList, "NumberedList");
            this.mNumerList.Image = ((System.Drawing.Image)(resources.GetObject("mNumerList.Image")));
            this.mNumerList.Name = "mNumerList";
            this.mNumerList.Size = new System.Drawing.Size(227, 22);
            this.mNumerList.Text = "numbered list";
            // 
            // mBulleteList
            // 
            this.myCommandControler.SetCommandName(this.mBulleteList, "BulletedList");
            this.mBulleteList.Image = ((System.Drawing.Image)(resources.GetObject("mBulleteList.Image")));
            this.mBulleteList.Name = "mBulleteList";
            this.mBulleteList.Size = new System.Drawing.Size(227, 22);
            this.mBulleteList.Text = "Bulleted list";
            // 
            // mFirstIndent
            // 
            this.myCommandControler.SetCommandName(this.mFirstIndent, "FirstLineIndent");
            this.mFirstIndent.Image = ((System.Drawing.Image)(resources.GetObject("mFirstIndent.Image")));
            this.mFirstIndent.Name = "mFirstIndent";
            this.mFirstIndent.Size = new System.Drawing.Size(227, 22);
            this.mFirstIndent.Text = "First indent for paragraph";
            // 
            // toolStripSeparator26
            // 
            this.toolStripSeparator26.Name = "toolStripSeparator26";
            this.toolStripSeparator26.Size = new System.Drawing.Size(224, 6);
            // 
            // mFieldFixedWidth
            // 
            this.myCommandControler.SetCommandName(this.mFieldFixedWidth, "FieldFixedWidth");
            this.mFieldFixedWidth.Name = "mFieldFixedWidth";
            this.mFieldFixedWidth.Size = new System.Drawing.Size(227, 22);
            this.mFieldFixedWidth.Text = "fixe input field width";
            // 
            // mTable
            // 
            this.mTable.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mInsertTable,
            this.mDeleteTable,
            this.toolStripSeparator20,
            this.mCellAlign,
            this.toolStripSeparator16,
            this.mInsertRowUp,
            this.mInsertRowDown,
            this.mDeleteRow,
            this.toolStripMenuItem3,
            this.mInsertColumnLeft,
            this.mInsertColumnRight,
            this.mDeleteColumn,
            this.toolStripMenuItem2,
            this.mMergeCell,
            this.mSplitCell,
            this.toolStripSeparator19,
            this.mHeaderRow});
            this.mTable.Name = "mTable";
            this.mTable.Size = new System.Drawing.Size(52, 21);
            this.mTable.Text = "&Table";
            // 
            // mInsertTable
            // 
            this.myCommandControler.SetCommandName(this.mInsertTable, "Table_InsertTable");
            this.mInsertTable.Image = ((System.Drawing.Image)(resources.GetObject("mInsertTable.Image")));
            this.mInsertTable.Name = "mInsertTable";
            this.mInsertTable.Size = new System.Drawing.Size(202, 22);
            this.mInsertTable.Text = "Insert table...";
            // 
            // mDeleteTable
            // 
            this.myCommandControler.SetCommandName(this.mDeleteTable, "Table_DeleteTable");
            this.mDeleteTable.Name = "mDeleteTable";
            this.mDeleteTable.Size = new System.Drawing.Size(202, 22);
            this.mDeleteTable.Text = "Delete table";
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(199, 6);
            // 
            // mCellAlign
            // 
            this.mCellAlign.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mAlignTopLeft,
            this.mAlignTopCenter,
            this.mAlignTopRight,
            this.mAlignMiddleLeft,
            this.mAlignMiddleCenter,
            this.mAlignMiddleRight,
            this.mAlignBottomLeft,
            this.mAlignBottomCenter,
            this.mAlignBottomRight});
            this.mCellAlign.Name = "mCellAlign";
            this.mCellAlign.Size = new System.Drawing.Size(202, 22);
            this.mCellAlign.Text = "Align";
            // 
            // mAlignTopLeft
            // 
            this.myCommandControler.SetCommandName(this.mAlignTopLeft, "AlignTopLeft");
            this.mAlignTopLeft.Image = ((System.Drawing.Image)(resources.GetObject("mAlignTopLeft.Image")));
            this.mAlignTopLeft.Name = "mAlignTopLeft";
            this.mAlignTopLeft.Size = new System.Drawing.Size(159, 22);
            this.mAlignTopLeft.Text = "Top left";
            // 
            // mAlignTopCenter
            // 
            this.myCommandControler.SetCommandName(this.mAlignTopCenter, "AlignTopCenter");
            this.mAlignTopCenter.Image = ((System.Drawing.Image)(resources.GetObject("mAlignTopCenter.Image")));
            this.mAlignTopCenter.Name = "mAlignTopCenter";
            this.mAlignTopCenter.Size = new System.Drawing.Size(159, 22);
            this.mAlignTopCenter.Text = "Top center";
            // 
            // mAlignTopRight
            // 
            this.myCommandControler.SetCommandName(this.mAlignTopRight, "AlignTopRight");
            this.mAlignTopRight.Image = ((System.Drawing.Image)(resources.GetObject("mAlignTopRight.Image")));
            this.mAlignTopRight.Name = "mAlignTopRight";
            this.mAlignTopRight.Size = new System.Drawing.Size(159, 22);
            this.mAlignTopRight.Text = "Top right";
            // 
            // mAlignMiddleLeft
            // 
            this.myCommandControler.SetCommandName(this.mAlignMiddleLeft, "AlignMiddleLeft");
            this.mAlignMiddleLeft.Image = ((System.Drawing.Image)(resources.GetObject("mAlignMiddleLeft.Image")));
            this.mAlignMiddleLeft.Name = "mAlignMiddleLeft";
            this.mAlignMiddleLeft.Size = new System.Drawing.Size(159, 22);
            this.mAlignMiddleLeft.Text = "Middle left";
            // 
            // mAlignMiddleCenter
            // 
            this.myCommandControler.SetCommandName(this.mAlignMiddleCenter, "AlignMiddleCenter");
            this.mAlignMiddleCenter.Image = ((System.Drawing.Image)(resources.GetObject("mAlignMiddleCenter.Image")));
            this.mAlignMiddleCenter.Name = "mAlignMiddleCenter";
            this.mAlignMiddleCenter.Size = new System.Drawing.Size(159, 22);
            this.mAlignMiddleCenter.Text = "Middle center";
            // 
            // mAlignMiddleRight
            // 
            this.myCommandControler.SetCommandName(this.mAlignMiddleRight, "AlignMiddleRight");
            this.mAlignMiddleRight.Image = ((System.Drawing.Image)(resources.GetObject("mAlignMiddleRight.Image")));
            this.mAlignMiddleRight.Name = "mAlignMiddleRight";
            this.mAlignMiddleRight.Size = new System.Drawing.Size(159, 22);
            this.mAlignMiddleRight.Text = "Middle right";
            // 
            // mAlignBottomLeft
            // 
            this.myCommandControler.SetCommandName(this.mAlignBottomLeft, "AlignBottomLeft");
            this.mAlignBottomLeft.Image = ((System.Drawing.Image)(resources.GetObject("mAlignBottomLeft.Image")));
            this.mAlignBottomLeft.Name = "mAlignBottomLeft";
            this.mAlignBottomLeft.Size = new System.Drawing.Size(159, 22);
            this.mAlignBottomLeft.Text = "Bottom left";
            // 
            // mAlignBottomCenter
            // 
            this.myCommandControler.SetCommandName(this.mAlignBottomCenter, "AlignBottomCenter");
            this.mAlignBottomCenter.Image = ((System.Drawing.Image)(resources.GetObject("mAlignBottomCenter.Image")));
            this.mAlignBottomCenter.Name = "mAlignBottomCenter";
            this.mAlignBottomCenter.Size = new System.Drawing.Size(159, 22);
            this.mAlignBottomCenter.Text = "Bottom center";
            // 
            // mAlignBottomRight
            // 
            this.myCommandControler.SetCommandName(this.mAlignBottomRight, "AlignBottomRight");
            this.mAlignBottomRight.Image = ((System.Drawing.Image)(resources.GetObject("mAlignBottomRight.Image")));
            this.mAlignBottomRight.Name = "mAlignBottomRight";
            this.mAlignBottomRight.Size = new System.Drawing.Size(159, 22);
            this.mAlignBottomRight.Text = "Bottom right";
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(199, 6);
            // 
            // mInsertRowUp
            // 
            this.myCommandControler.SetCommandName(this.mInsertRowUp, "Table_InsertRowUp");
            this.mInsertRowUp.Image = ((System.Drawing.Image)(resources.GetObject("mInsertRowUp.Image")));
            this.mInsertRowUp.Name = "mInsertRowUp";
            this.mInsertRowUp.Size = new System.Drawing.Size(202, 22);
            this.mInsertRowUp.Text = "Insert row to up";
            // 
            // mInsertRowDown
            // 
            this.myCommandControler.SetCommandName(this.mInsertRowDown, "Table_InsertRowDown");
            this.mInsertRowDown.Image = ((System.Drawing.Image)(resources.GetObject("mInsertRowDown.Image")));
            this.mInsertRowDown.Name = "mInsertRowDown";
            this.mInsertRowDown.Size = new System.Drawing.Size(202, 22);
            this.mInsertRowDown.Text = "Insert row to down";
            // 
            // mDeleteRow
            // 
            this.myCommandControler.SetCommandName(this.mDeleteRow, "Table_DeleteRow");
            this.mDeleteRow.Image = ((System.Drawing.Image)(resources.GetObject("mDeleteRow.Image")));
            this.mDeleteRow.Name = "mDeleteRow";
            this.mDeleteRow.Size = new System.Drawing.Size(202, 22);
            this.mDeleteRow.Text = "Delete row";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(199, 6);
            // 
            // mInsertColumnLeft
            // 
            this.myCommandControler.SetCommandName(this.mInsertColumnLeft, "Table_InsertColumnLeft");
            this.mInsertColumnLeft.Image = ((System.Drawing.Image)(resources.GetObject("mInsertColumnLeft.Image")));
            this.mInsertColumnLeft.Name = "mInsertColumnLeft";
            this.mInsertColumnLeft.Size = new System.Drawing.Size(202, 22);
            this.mInsertColumnLeft.Text = "Insert colum to left";
            // 
            // mInsertColumnRight
            // 
            this.myCommandControler.SetCommandName(this.mInsertColumnRight, "Table_InsertColumnRight");
            this.mInsertColumnRight.Image = ((System.Drawing.Image)(resources.GetObject("mInsertColumnRight.Image")));
            this.mInsertColumnRight.Name = "mInsertColumnRight";
            this.mInsertColumnRight.Size = new System.Drawing.Size(202, 22);
            this.mInsertColumnRight.Text = "Insert column to right";
            // 
            // mDeleteColumn
            // 
            this.myCommandControler.SetCommandName(this.mDeleteColumn, "Table_DeleteColumn");
            this.mDeleteColumn.Image = ((System.Drawing.Image)(resources.GetObject("mDeleteColumn.Image")));
            this.mDeleteColumn.Name = "mDeleteColumn";
            this.mDeleteColumn.Size = new System.Drawing.Size(202, 22);
            this.mDeleteColumn.Text = "Delete column";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(199, 6);
            // 
            // mMergeCell
            // 
            this.myCommandControler.SetCommandName(this.mMergeCell, "Table_MergeCell");
            this.mMergeCell.Image = ((System.Drawing.Image)(resources.GetObject("mMergeCell.Image")));
            this.mMergeCell.Name = "mMergeCell";
            this.mMergeCell.Size = new System.Drawing.Size(202, 22);
            this.mMergeCell.Text = "Merge cell";
            // 
            // mSplitCell
            // 
            this.myCommandControler.SetCommandName(this.mSplitCell, "Table_SplitCell");
            this.mSplitCell.Image = ((System.Drawing.Image)(resources.GetObject("mSplitCell.Image")));
            this.mSplitCell.Name = "mSplitCell";
            this.mSplitCell.Size = new System.Drawing.Size(202, 22);
            this.mSplitCell.Text = "Split cell";
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(199, 6);
            // 
            // mHeaderRow
            // 
            this.myCommandControler.SetCommandName(this.mHeaderRow, "Table_HeaderRow");
            this.mHeaderRow.Name = "mHeaderRow";
            this.mHeaderRow.Size = new System.Drawing.Size(202, 22);
            this.mHeaderRow.Text = "Header row";
            // 
            // mTools
            // 
            this.mTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mWordCount,
            this.toolStripSeparator10,
            this.menuConfig,
            this.mDocumentValueValidate,
            this.mEditVBAScript});
            this.mTools.Name = "mTools";
            this.mTools.Size = new System.Drawing.Size(52, 21);
            this.mTools.Text = "&Tools";
            // 
            // mWordCount
            // 
            this.myCommandControler.SetCommandName(this.mWordCount, "WordCount");
            this.mWordCount.Image = ((System.Drawing.Image)(resources.GetObject("mWordCount.Image")));
            this.mWordCount.Name = "mWordCount";
            this.mWordCount.Size = new System.Drawing.Size(157, 22);
            this.mWordCount.Text = "Word count";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(154, 6);
            // 
            // menuConfig
            // 
            this.myCommandControler.SetCommandName(this.menuConfig, "DocumentOptions");
            this.menuConfig.Name = "menuConfig";
            this.menuConfig.Size = new System.Drawing.Size(157, 22);
            this.menuConfig.Text = "options...";
            // 
            // mDocumentValueValidate
            // 
            this.myCommandControler.SetCommandName(this.mDocumentValueValidate, "DocumentValueValidate");
            this.mDocumentValueValidate.Name = "mDocumentValueValidate";
            this.mDocumentValueValidate.Size = new System.Drawing.Size(157, 22);
            this.mDocumentValueValidate.Text = "Value validate";
            // 
            // mEditVBAScript
            // 
            this.myCommandControler.SetCommandName(this.mEditVBAScript, "EditVBAScript");
            this.mEditVBAScript.Image = ((System.Drawing.Image)(resources.GetObject("mEditVBAScript.Image")));
            this.mEditVBAScript.Name = "mEditVBAScript";
            this.mEditVBAScript.Size = new System.Drawing.Size(157, 22);
            this.mEditVBAScript.Text = "Edit script";
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(47, 21);
            this.menuHelp.Text = "&Help";
            // 
            // mAbout
            // 
            this.myCommandControler.SetCommandName(this.mAbout, "AboutControl");
            this.mAbout.Name = "mAbout";
            this.mAbout.Size = new System.Drawing.Size(120, 22);
            this.mAbout.Text = "About...";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDemoFiles,
            this.toolStripSeparator24,
            this.toolStripButton1,
            this.toolStripButton19,
            this.toolStripButton2,
            this.toolStripSeparator11,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripSeparator12,
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolStripSeparator13,
            this.cboFontSize,
            this.toolStripComboBox1,
            this.toolStripButton8,
            this.toolStripButton9,
            this.toolStripButton10,
            this.toolStripButton11,
            this.btnColor,
            this.btnBackColor,
            this.toolStripButton17,
            this.toolStripButton18,
            this.toolStripSeparator14,
            this.toolStripButton12,
            this.toolStripButton13,
            this.toolStripButton14,
            this.toolStripSeparator15,
            this.toolStripButton15,
            this.toolStripButton16});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(811, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnDemoFiles
            // 
            this.btnDemoFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDemoFiles.Image = ((System.Drawing.Image)(resources.GetObject("btnDemoFiles.Image")));
            this.btnDemoFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDemoFiles.Name = "btnDemoFiles";
            this.btnDemoFiles.Size = new System.Drawing.Size(83, 22);
            this.btnDemoFiles.Text = "Demo files";
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton1, "FileNew");
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "New";
            // 
            // toolStripButton19
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton19, "FileOpen");
            this.toolStripButton19.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton19.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton19.Image")));
            this.toolStripButton19.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton19.Name = "toolStripButton19";
            this.toolStripButton19.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton19.Text = "Open...";
            // 
            // toolStripButton2
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton2, "FileSave");
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Save";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton3, "Cut");
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Cut";
            // 
            // toolStripButton4
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton4, "Copy");
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Copy";
            // 
            // toolStripButton5
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton5, "Paste");
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "Paste";
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton6
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton6, "Undo");
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "Undo";
            // 
            // toolStripButton7
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton7, "Redo");
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton7.Text = "Redo";
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
            // 
            // cboFontSize
            // 
            this.cboFontSize.AutoSize = false;
            this.myCommandControler.SetCommandName(this.cboFontSize, "FontName");
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Size = new System.Drawing.Size(130, 25);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.AutoSize = false;
            this.myCommandControler.SetCommandName(this.toolStripComboBox1, "FontSize");
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(55, 25);
            // 
            // toolStripButton8
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton8, "Font");
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton8.Text = "Font";
            // 
            // toolStripButton9
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton9, "Bold");
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton9.Image")));
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton9.Text = "Bold";
            // 
            // toolStripButton10
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton10, "Italic");
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton10.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton10.Image")));
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton10.Text = "Italic";
            // 
            // toolStripButton11
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton11, "Underline");
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton11.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton11.Image")));
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton11.Text = "Underline";
            // 
            // btnColor
            // 
            this.myCommandControler.SetCommandName(this.btnColor, "Color");
            this.btnColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnColor.Image = ((System.Drawing.Image)(resources.GetObject("btnColor.Image")));
            this.btnColor.ImageTransparentColor = System.Drawing.Color.Red;
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(23, 22);
            this.btnColor.Text = "Color";
            // 
            // btnBackColor
            // 
            this.myCommandControler.SetCommandName(this.btnBackColor, "BackColor");
            this.btnBackColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBackColor.Image = ((System.Drawing.Image)(resources.GetObject("btnBackColor.Image")));
            this.btnBackColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(23, 22);
            this.btnBackColor.Text = "Back color";
            // 
            // toolStripButton17
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton17, "Superscript");
            this.toolStripButton17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton17.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton17.Image")));
            this.toolStripButton17.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton17.Name = "toolStripButton17";
            this.toolStripButton17.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton17.Text = "Superscript";
            // 
            // toolStripButton18
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton18, "Subscript");
            this.toolStripButton18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton18.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton18.Image")));
            this.toolStripButton18.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton18.Name = "toolStripButton18";
            this.toolStripButton18.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton18.Text = "Subscript";
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton12
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton12, "AlignLeft");
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton12.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton12.Image")));
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton12.Text = "Align left";
            // 
            // toolStripButton13
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton13, "AlignCenter");
            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton13.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton13.Image")));
            this.toolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton13.Name = "toolStripButton13";
            this.toolStripButton13.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton13.Text = "Align center";
            // 
            // toolStripButton14
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton14, "AlignRight");
            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton14.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton14.Image")));
            this.toolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton14.Name = "toolStripButton14";
            this.toolStripButton14.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton14.Text = "Align right";
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton15
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton15, "NumberedList");
            this.toolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton15.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton15.Image")));
            this.toolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton15.Name = "toolStripButton15";
            this.toolStripButton15.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton15.Text = "Numbered list";
            // 
            // toolStripButton16
            // 
            this.myCommandControler.SetCommandName(this.toolStripButton16, "BulletedList");
            this.toolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton16.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton16.Image")));
            this.toolStripButton16.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton16.Name = "toolStripButton16";
            this.toolStripButton16.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton16.Text = "Bulleted list";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblPosition,
            this.lblAbout});
            this.statusStrip1.Location = new System.Drawing.Point(0, 387);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(811, 26);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblStatus.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(367, 21);
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = false;
            this.lblPosition.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblPosition.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(250, 21);
            this.lblPosition.Text = "Position";
            this.lblPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAbout
            // 
            this.lblAbout.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblAbout.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(179, 21);
            this.lblAbout.Text = "CSharpWriter winform demo";
            // 
            // myImageList
            // 
            this.myImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("myImageList.ImageStream")));
            this.myImageList.TransparentColor = System.Drawing.Color.White;
            this.myImageList.Images.SetKeyName(0, "");
            this.myImageList.Images.SetKeyName(1, "");
            this.myImageList.Images.SetKeyName(2, "");
            this.myImageList.Images.SetKeyName(3, "");
            this.myImageList.Images.SetKeyName(4, "");
            this.myImageList.Images.SetKeyName(5, "");
            this.myImageList.Images.SetKeyName(6, "");
            this.myImageList.Images.SetKeyName(7, "");
            this.myImageList.Images.SetKeyName(8, "");
            this.myImageList.Images.SetKeyName(9, "");
            this.myImageList.Images.SetKeyName(10, "");
            this.myImageList.Images.SetKeyName(11, "");
            this.myImageList.Images.SetKeyName(12, "");
            this.myImageList.Images.SetKeyName(13, "");
            this.myImageList.Images.SetKeyName(14, "");
            this.myImageList.Images.SetKeyName(15, "");
            this.myImageList.Images.SetKeyName(16, "");
            this.myImageList.Images.SetKeyName(17, "");
            this.myImageList.Images.SetKeyName(18, "");
            this.myImageList.Images.SetKeyName(19, "");
            this.myImageList.Images.SetKeyName(20, "");
            this.myImageList.Images.SetKeyName(21, "");
            this.myImageList.Images.SetKeyName(22, "");
            this.myImageList.Images.SetKeyName(23, "");
            this.myImageList.Images.SetKeyName(24, "");
            this.myImageList.Images.SetKeyName(25, "");
            this.myImageList.Images.SetKeyName(26, "");
            this.myImageList.Images.SetKeyName(27, "");
            this.myImageList.Images.SetKeyName(28, "");
            this.myImageList.Images.SetKeyName(29, "");
            this.myImageList.Images.SetKeyName(30, "");
            this.myImageList.Images.SetKeyName(31, "");
            this.myImageList.Images.SetKeyName(32, "");
            // 
            // cmRedo
            // 
            this.myCommandControler.SetCommandName(this.cmRedo, "Redo");
            this.cmRedo.Image = ((System.Drawing.Image)(resources.GetObject("cmRedo.Image")));
            this.cmRedo.Name = "cmRedo";
            this.cmRedo.Size = new System.Drawing.Size(196, 22);
            this.cmRedo.Text = "Redo";
            // 
            // cmUndo
            // 
            this.myCommandControler.SetCommandName(this.cmUndo, "Undo");
            this.cmUndo.Image = ((System.Drawing.Image)(resources.GetObject("cmUndo.Image")));
            this.cmUndo.Name = "cmUndo";
            this.cmUndo.Size = new System.Drawing.Size(196, 22);
            this.cmUndo.Text = "Undo";
            // 
            // cmCut
            // 
            this.myCommandControler.SetCommandName(this.cmCut, "Cut");
            this.cmCut.Image = ((System.Drawing.Image)(resources.GetObject("cmCut.Image")));
            this.cmCut.Name = "cmCut";
            this.cmCut.Size = new System.Drawing.Size(196, 22);
            this.cmCut.Text = "Cut";
            // 
            // cmCopy
            // 
            this.myCommandControler.SetCommandName(this.cmCopy, "Copy");
            this.cmCopy.Image = ((System.Drawing.Image)(resources.GetObject("cmCopy.Image")));
            this.cmCopy.Name = "cmCopy";
            this.cmCopy.Size = new System.Drawing.Size(196, 22);
            this.cmCopy.Text = "Copy";
            // 
            // cmPaste
            // 
            this.myCommandControler.SetCommandName(this.cmPaste, "Paste");
            this.cmPaste.Image = ((System.Drawing.Image)(resources.GetObject("cmPaste.Image")));
            this.cmPaste.Name = "cmPaste";
            this.cmPaste.Size = new System.Drawing.Size(196, 22);
            this.cmPaste.Text = "Paste";
            // 
            // cmDelete
            // 
            this.myCommandControler.SetCommandName(this.cmDelete, "Delete");
            this.cmDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmDelete.Image")));
            this.cmDelete.Name = "cmDelete";
            this.cmDelete.Size = new System.Drawing.Size(196, 22);
            this.cmDelete.Text = "Delete";
            // 
            // cmColor
            // 
            this.myCommandControler.SetCommandName(this.cmColor, "Color");
            this.cmColor.Name = "cmColor";
            this.cmColor.Size = new System.Drawing.Size(196, 22);
            this.cmColor.Text = "Color";
            // 
            // cmFont
            // 
            this.myCommandControler.SetCommandName(this.cmFont, "Font");
            this.cmFont.Image = ((System.Drawing.Image)(resources.GetObject("cmFont.Image")));
            this.cmFont.Name = "cmFont";
            this.cmFont.Size = new System.Drawing.Size(196, 22);
            this.cmFont.Text = "Font...";
            // 
            // cmAlignLeft
            // 
            this.myCommandControler.SetCommandName(this.cmAlignLeft, "AlignLeft");
            this.cmAlignLeft.Image = ((System.Drawing.Image)(resources.GetObject("cmAlignLeft.Image")));
            this.cmAlignLeft.Name = "cmAlignLeft";
            this.cmAlignLeft.Size = new System.Drawing.Size(196, 22);
            this.cmAlignLeft.Text = "Align left";
            // 
            // cmAlignCenter
            // 
            this.myCommandControler.SetCommandName(this.cmAlignCenter, "AlignCenter");
            this.cmAlignCenter.Image = ((System.Drawing.Image)(resources.GetObject("cmAlignCenter.Image")));
            this.cmAlignCenter.Name = "cmAlignCenter";
            this.cmAlignCenter.Size = new System.Drawing.Size(196, 22);
            this.cmAlignCenter.Text = "Align center";
            // 
            // cmAlignRight
            // 
            this.myCommandControler.SetCommandName(this.cmAlignRight, "AlignRight");
            this.cmAlignRight.Image = ((System.Drawing.Image)(resources.GetObject("cmAlignRight.Image")));
            this.cmAlignRight.Name = "cmAlignRight";
            this.cmAlignRight.Size = new System.Drawing.Size(196, 22);
            this.cmAlignRight.Text = "Align right";
            // 
            // cmProperties
            // 
            this.myCommandControler.SetCommandName(this.cmProperties, "ElementProperties");
            this.cmProperties.Name = "cmProperties";
            this.cmProperties.Size = new System.Drawing.Size(196, 22);
            this.cmProperties.Text = "Element properties...";
            // 
            // myEditControl
            // 
            this.myEditControl.AllowDrop = true;
            this.myEditControl.AutoScroll = true;
            this.myEditControl.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.myEditControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.myEditControl.ContextMenuStrip = this.cmEdit;
            this.myEditControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myEditControl.Location = new System.Drawing.Point(0, 50);
            this.myEditControl.Name = "myEditControl";
            this.myEditControl.Size = new System.Drawing.Size(811, 337);
            this.myEditControl.TabIndex = 4;
            this.myEditControl.DocumentLoad += new System.EventHandler(this.myEditControl_DocumentLoad);
            this.myEditControl.HoverElementChanged += new System.EventHandler(this.myEditControl_HoverElementChanged);
            this.myEditControl.DocumentContentChanged += new System.EventHandler(this.myEditControl_DocumentContentChanged);
            this.myEditControl.SelectionChanged += new System.EventHandler(this.myEditControl_SelectionChanged);
            this.myEditControl.StatusTextChanged += new System.EventHandler(this.myEditControl_StatusTextChanged);
            // 
            // cmEdit
            // 
            this.cmEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmRedo,
            this.cmUndo,
            this.toolStripMenuItem4,
            this.cmCut,
            this.cmCopy,
            this.cmPaste,
            this.cmDelete,
            this.toolStripMenuItem5,
            this.cmColor,
            this.cmFont,
            this.toolStripMenuItem6,
            this.cmAlignLeft,
            this.cmAlignCenter,
            this.cmAlignRight,
            this.toolStripMenuItem8,
            this.cmProperties});
            this.cmEdit.Name = "cmEdit";
            this.cmEdit.Size = new System.Drawing.Size(197, 292);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(193, 6);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(193, 6);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(193, 6);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(193, 6);
            // 
            // frmTextUseCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 413);
            this.Controls.Add(this.myEditControl);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmTextUseCommand";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSharpWriter winform.net demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTextUseCommand_FormClosing);
            this.Load += new System.EventHandler(this.frmTextUseCommand_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myCommandControler)).EndInit();
            this.cmEdit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem menuView;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuNewFile;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuPageSettings;
        private System.Windows.Forms.ToolStripMenuItem menuPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuClose;
        private System.Windows.Forms.ToolStripMenuItem mUndo;
        private System.Windows.Forms.ToolStripMenuItem mRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mCut;
        private System.Windows.Forms.ToolStripMenuItem mCopy;
        private System.Windows.Forms.ToolStripMenuItem mPaste;
        private System.Windows.Forms.ToolStripMenuItem mDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mSearch;
        private System.Windows.Forms.ToolStripMenuItem mJumpPrint;
        private System.Windows.Forms.ToolStripMenuItem mInsert;
        private System.Windows.Forms.ToolStripMenuItem mFormat;
        private System.Windows.Forms.ToolStripMenuItem mTools;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblPosition;
        private System.Windows.Forms.ToolStripStatusLabel lblAbout;
        private System.Windows.Forms.ImageList myImageList;
        private DCSoft.CSharpWriter.Commands.CSWriterCommandControler myCommandControler;
        private System.Windows.Forms.ToolStripMenuItem mFont;
        private System.Windows.Forms.ToolStripMenuItem mTextColor;
        private System.Windows.Forms.ToolStripMenuItem mBackColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem mSup;
        private System.Windows.Forms.ToolStripMenuItem mSub;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem mAlignLeft;
        private System.Windows.Forms.ToolStripMenuItem mCenterAlign;
        private System.Windows.Forms.ToolStripMenuItem mRightAlign;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem mNumerList;
        private System.Windows.Forms.ToolStripMenuItem mBulleteList;
        private System.Windows.Forms.ToolStripMenuItem mFirstIndent;
        private System.Windows.Forms.ToolStripMenuItem mWordCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem menuConfig;
        private System.Windows.Forms.ToolStripMenuItem mAbout;
        private System.Windows.Forms.ToolStripMenuItem mInsertImage;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.ToolStripButton toolStripButton11;
        private System.Windows.Forms.ToolStripButton toolStripButton17;
        private System.Windows.Forms.ToolStripButton toolStripButton18;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripButton toolStripButton12;
        private System.Windows.Forms.ToolStripButton toolStripButton13;
        private System.Windows.Forms.ToolStripButton toolStripButton14;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripButton toolStripButton15;
        private System.Windows.Forms.ToolStripButton toolStripButton16;
        private System.Windows.Forms.ToolStripButton toolStripButton19;
        private System.Windows.Forms.ToolStripMenuItem mTable;
        private System.Windows.Forms.ToolStripMenuItem mInsertTable;
        private System.Windows.Forms.ToolStripMenuItem mDeleteTable;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripMenuItem mInsertRowUp;
        private System.Windows.Forms.ToolStripMenuItem mInsertRowDown;
        private System.Windows.Forms.ToolStripMenuItem mDeleteRow;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mInsertColumnLeft;
        private System.Windows.Forms.ToolStripMenuItem mInsertColumnRight;
        private System.Windows.Forms.ToolStripMenuItem mDeleteColumn;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mMergeCell;
        private System.Windows.Forms.ToolStripMenuItem mSplitCell;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mInsertInputField;
        private System.Windows.Forms.ToolStripMenuItem mDeleteField;
        private System.Windows.Forms.ToolStripComboBox cboFontSize;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ContextMenuStrip cmEdit;
        private System.Windows.Forms.ToolStripMenuItem cmRedo;
        private System.Windows.Forms.ToolStripMenuItem cmUndo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem cmCut;
        private System.Windows.Forms.ToolStripMenuItem cmCopy;
        private System.Windows.Forms.ToolStripMenuItem cmPaste;
        private System.Windows.Forms.ToolStripMenuItem cmDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem cmColor;
        private System.Windows.Forms.ToolStripMenuItem cmFont;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem cmAlignLeft;
        private System.Windows.Forms.ToolStripMenuItem cmAlignCenter;
        private System.Windows.Forms.ToolStripMenuItem cmAlignRight;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem cmProperties;
        private System.Windows.Forms.ToolStripMenuItem mInsertCheckBox;
        private System.Windows.Forms.ToolStripMenuItem mParagraphFormat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripMenuItem mEditImageShape;
        private System.Windows.Forms.ToolStripMenuItem mInsertParameter;
        private System.Windows.Forms.ToolStripMenuItem mDocumentValueValidate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        private System.Windows.Forms.ToolStripMenuItem mPageViewMode;
        private System.Windows.Forms.ToolStripMenuItem mNormalViewMode;
        private System.Windows.Forms.ToolStripButton btnColor;
        private System.Windows.Forms.ToolStripButton btnBackColor;
        private System.Windows.Forms.ToolStripMenuItem mEditVBAScript;
        private System.Windows.Forms.ToolStripMenuItem menuSpecifyPaste;
        internal DCSoft.CSharpWriter.Controls.CSWriterControl myEditControl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
        private System.Windows.Forms.ToolStripMenuItem mCellAlign;
        private System.Windows.Forms.ToolStripMenuItem mAlignTopLeft;
        private System.Windows.Forms.ToolStripMenuItem mAlignTopCenter;
        private System.Windows.Forms.ToolStripMenuItem mAlignTopRight;
        private System.Windows.Forms.ToolStripMenuItem mAlignMiddleLeft;
        private System.Windows.Forms.ToolStripMenuItem mAlignMiddleCenter;
        private System.Windows.Forms.ToolStripMenuItem mAlignMiddleRight;
        private System.Windows.Forms.ToolStripMenuItem mAlignBottomLeft;
        private System.Windows.Forms.ToolStripMenuItem mAlignBottomCenter;
        private System.Windows.Forms.ToolStripMenuItem mAlignBottomRight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStripMenuItem mCleanViewMode;
        private System.Windows.Forms.ToolStripMenuItem mComplexViewMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
        private System.Windows.Forms.ToolStripMenuItem mFormViewMode;
        private System.Windows.Forms.ToolStripMenuItem mInsertMedicalExpression;
        private System.Windows.Forms.ToolStripMenuItem mInsertBarcode;
        private System.Windows.Forms.ToolStripMenuItem mHeaderRow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator25;
        private System.Windows.Forms.ToolStripMenuItem mDebugOutWindow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator26;
        private System.Windows.Forms.ToolStripMenuItem mFieldFixedWidth;
        private System.Windows.Forms.ToolStripMenuItem mInsertXML;
        private System.Windows.Forms.ToolStripMenuItem mInsertPageInfo;
        private System.Windows.Forms.ToolStripMenuItem mInsertFileContent;
        private System.Windows.Forms.ToolStripMenuItem mInsertContentLink;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem mDocumentDefaultFont;
        private System.Windows.Forms.ToolStripMenuItem mOpenUrl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator28;
        private System.Windows.Forms.ToolStripMenuItem mDesignMode;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem mConvertFieldToContent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator30;
        private System.Windows.Forms.ToolStripDropDownButton btnDemoFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
    }
}