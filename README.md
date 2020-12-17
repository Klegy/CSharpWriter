# CSharpWriter
<img src="https://raw.githubusercontent.com/dcsoft-yyf/CSharpWriter/main/snapshort.png"/>
##Background
<br/ > Many people use standard RichTextBox control to handle RTF style text . but RichTextBox control
        has lots limited. for example ,It can not use in NO-GUI application,RTF file format is hard to parse,
        hard to change party of document content by programming.
##What is CSharpWriter
<br/ >CSharpWriter(short name CSWriter) is a new RTF style document process engine without RichTextBox control. it is more powerfull than RichTextBox.We holpe it can replace OpenOffice in .NET World.It support :
    <br />1.RTF style document content. like text color,back color,font.
    <br />2.DOM,you can create document in memory without create GUI control. so CSWriter can used in console /asp.net application.
    <br />3.Independent. CSWriter is independent to other components. it draw all content and gui using system.drawing.graphics. handle mouse and keyboard events by itself. you can use it without technology limited.
    <br />4.Easy to use. you can place a control on your winform. it provide thousands OOP APIs.
    <br />5.Lite weight control. CSWriter is implemented from System.Windows.Forms.UserControl. it run in application UI thread, use less memory , You can catch any unhandled exception.
    <br />6.Support XML format. the result file is more easy to parse then RTF format. use DOM,you can create you owner file format.
    <br />7.CSWriter is developing.If you have any idea about CSWriter ,you can write to [28348092@qq.com] or [yyf9989@hotmail.com] , or visit site[https://github.com/dcsoft-yyf/CSharpWriter].

    <b>How to use CSharpWriter in WinForm.NET</b>

    <p>    Just like others winform control.you can use CSWriter easy. in VS.NET IDE,open winform designer,at toolbox left side,choose "choose items..." . and select file DCSoft.CSharpWriter.dll ,than you can see CSWriterControl on toolbox,just like the following:</p>
    <br /><img src="https://raw.githubusercontent.com/dcsoft-yyf/CSharpWriter/main/about.files/dcimg_39226.jpg" />
    <br />   You can drag a CSWriterControl and a CSWriterCommandControler to the form ,change it's name as 'myEditControl' and 'myCommandControler'.
    Next you can add some button/toolstripbutton or menu item on the form,select a button ,at PropertyGrid,you can see a property item name "at myCommandControler's CommandName",Just like the following:
    <br /><img src="https://raw.githubusercontent.com/dcsoft-yyf/CSharpWriter/main/about.files/dcimg_39227.jpg" />
    <br />click button at the end,then show a dialog like this:
    <br /><img src="https://raw.githubusercontent.com/dcsoft-yyf/CSharpWriter/main/about.files/dcimg_39228.jpg" />
    <br />  And you can select a command whitch binding to the button.Then you design a form like this:
    <br /><img src="https://raw.githubusercontent.com/dcsoft-yyf/CSharpWriter/main/about.files/dcimg_39229.jpg" />
    <br /> Open the source editor, At the form_load event source code,add the following code:
    <pre>private void frmTextUseCommand_Load(object sender, EventArgs e)
{
    myEditControl.CommandControler = myCommandControler;
    myCommandControler.Start();
}</pre>
    <p>
        After do this,you can run application ,click button or menu item to use CSWriter. look ,it is so easy.
        <br /> CSWriter support a lot of commands.You can open a file by code "myEditControl.ExecuteCommand("FileOpen", true, null)", save a file by code"myEditControl.ExecuteCommand("FileSaveAs", true,null)", Inser a string at current position by code "myEditControl.ExecuteCommand("InsertString",false,"Some text content")".
        <br /> Those commands defind in WriterCommandModuleBrowse.cs,WriterCommandModuleEdit.cs,WriterCommandModuleFile.cs,WriterCommandModuleFormat.cs,WriterCommandModuleInsert.cs,WriterCommandModuleSecurity.cs,WriterCommandModuleTools.cs in directory "CSharpWriter\Commands".You can add your owner command in those files.For example,this code define command "InsertString":
    </p>
    <pre>[WriterCommandDescription("InsertString")]
protected void InsertString(object sender, WriterCommandEventArgs args)
{
    if (args.Mode == WriterCommandEventMode.QueryState)
    {
        args.Enabled = args.DocumentControler != null
            && args.DocumentControler.CanInsertElementAtCurrentPosition(
            typeof(DomCharElement));
    }
    else if (args.Mode == WriterCommandEventMode.Invoke)
    {
        args.Result = 0;
        InsertStringCommandParameter parameter = null;
        if (args.Parameter is InsertStringCommandParameter)
        {
            parameter = (InsertStringCommandParameter)args.Parameter;
        }
        else
        {
            parameter = new InsertStringCommandParameter();
            if (args.Parameter != null)
            {
                parameter.Text = Convert.ToString(args.Parameter);
            }
        }
        if (args.ShowUI)
        {
            using (dlgInputString dlg = new dlgInputString())
            {
                dlg.InputText = parameter.Text;
                if (dlg.ShowDialog(args.EditorControl) == DialogResult.OK)
                {
                    parameter.Text = dlg.InputText;
                }
                else
                {
                    return;
                }
            }
        }
        if (string.IsNullOrEmpty(parameter.Text) == false)
        {
            args.Result = args.DocumentControler.InsertString(parameter.Text);
        }
    }
}</pre>
    <p>
        In thess code, the "QueryState" party is detect wheather the command is Enable currenttly.System will call this party to set Button/MenuItem.Enable =true or false. the "Invoke"party is define the body of command.
    </p>
    <h3> File format</h3>
    Currenttly, CSWriter save load or save XML file format.In the futury , it will support RTF , HTML(MHT),PDF,BMP , ODF(Open doucument format) file format.and you can add your owner file format.
    <h3>Deploy</h3>
    CSWriter is a lite GUI Control component.All part include in single file"DCSoft.CSharpWriter.dll".independent any third part component.It run on MS.NET framework 2.0 SP2 or later. Notice , Must add SP2.becase CSWriterControl use property "CanEnableIme" which define in SP2(no defind in .NET2.0 without SP2).
    <br /> When you need update CSWriter, overwrite dll file , and re-compile your source code and finished.
    <h3>Support</h3>
    CSWriter provide technology support.If you have some question or idea, you can write email  [28348092@qq.com] or [yyf9989@hotmail.com] , or visit site[https://github.com/dcsoft-yyf/CSharpWriter].
    <h3>Compatibility Warring</h3>
    In this version, the with of whitespace is too small,In the futrue , the wdith of whitespace maby increase,this will change doucment layout.
    <h3>Route map</h3>
    This is the route map:
    <br /><img src="https://raw.githubusercontent.com/dcsoft-yyf/CSharpWriter/main/about.files/dcimg_39230.jpg" />

    <br />>>For programmer, curenttly CSWriter is support WinForm.NET,Console,Windows Service. CSWriter provider a type CSWriterControl base System.Windows.Forms.UserControl,Paint document content by itselft, handle mouse and keyboard event by itself.Without any third part component.
    <br />>>COM-Interop,CSWriter will support COM-Interop using System.Runtime.InteropServices,it will supoort COM-base developing,such as VB6,VC6,PowerBuidler,Delphi7(XE) or host in IE as ActiveControl.
    <br />>>For Web programmer,In the future,CSWriter will support WEB developing,such as ASP.NET WebForm/MVC and ASP.NET Core. and alse can work with JAVA/PHP/Node.js application.It will support web browser compatibility,etc IE/Edge/Chrome/Safari/Firefox.One document in different web browsers,have same view, same content layout and same print result.
    <br /> >>For GUI Operator, CSWriter just like a MS Word style.You can type any char, support English,Arabic,Tibetan.You can change text font, color,backcolor,paragraph format.In the futrue, it will support table.
    <br />>>Performance.We are working hard to performance optimization.In the future, You can load document with 1000 pages completed no more then 10 seconds.handle table with 1000 rows smooth.
    <h3>Inside CSharpWriter</h3>
    <br />>>DOM,the core of CSWriter. Dom description any details of document. every part of document has map to a class instance. programmer can new or delete Dom elements ,modify element''s property ,those can update document view ,just like JavaScript update HTML view by modify HTML DOM. DOM also is expendable,in the future ,it will add new type element class ,support new behavior. create an unlimited cswriter.
    <br />This is element implement tree in DOM:
    <br /><img src="https://raw.githubusercontent.com/dcsoft-yyf/CSharpWriter/main/about.files/dcimg_39231.jpg" />
    <br /> DOM define a set of classes,just like HTML DOM,each type of class map to a type content in document.for example, DomDocument class map to the total document, It is the primary enterpoint for programming; DomImageElement map to a image in document,DomCharElement map to a character in document.
    <br />This is DOM element reference tree:
    <br /><img src="https://raw.githubusercontent.com/dcsoft-yyf/CSharpWriter/main/about.files/dcimg_39232.jpg" />
    <br />CSWriter build a DOM tree in memory,every document element instance placed in the tree.[Document] is the single enterpoint to access DOM tree.You can insert/delete element in the tree,and the system can update view quickly.just like javascript access HTMLDom.
