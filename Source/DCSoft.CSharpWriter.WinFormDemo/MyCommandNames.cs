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

namespace DCSoft.CSharpWriter.WinFormDemo
{

    public class MyCommandNames
    {
        public const string OpenXMLDemo = "openxmldemo";
        public const string OpenRTFDemo = "openrtfdemo";
        public const string OpenFormViewDemo = "OpenFormViewDemo";
        public const string New = "new";
        public const string Open = "open";
        public const string Save = "save";
        public const string SaveAs = "saveas";
        public const string SaveWeb = "saveweb";
        public const string Settings = "settings";
        public const string Find = "find";
        public const string DragMove = "dragmove";
        public const string Link = "link";
        public const string Bookmark = "bookmark";
        public const string ShowBookmark = "showbookmark";
        public const string WordCount = "wordcount";
        public const string About = "about";
    }
}
