/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.CSharpWriter
{
    /// <summary>
    /// 定义一些常量
    /// </summary>
    internal class WriterConst
    {
        public readonly static DateTime NullDate = new DateTime(1900, 1, 1);
        public const int WriterApplicationID = 4;
        public const int License_OldXML = 1;

        public const string FS_Default = "default";
        public const string FS_KBLibrary = "kblibrary";
        public const string FS_Document = "document";
        public const string FS_Template = "template";
        public const string FS_KBListItem = "kblistitem";

        public const string Header = "Header";
        public const string Footer = "Footer";
        public const string Body = "Body";

        public const string FieldCode_Page = "Page";
        public const string FieldCode_NumPages = "NumPages";

        public const string EventName_ContentChanged = "ContentChanged";

        public const string PropertyName_Visible = "Visible";
    }
}
