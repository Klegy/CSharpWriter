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
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DCSoft.CSharpWriter.Controls
{
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IWriterControlComEvents
    {
        [DispId(12341)]
        void ComEventSelectionChanged();
        [DispId(12342)]
        void ComEventDocumentLoad();
        [DispId(12343)]
        void ComEventDocumentContentChanged();
        [DispId(12344)]
        void ComEventStatusTextChanged();
    }

    /// <summary>
    /// 无参数无返回值委托类型
    /// </summary>
    public delegate void VoidEventHandler();

    /// <summary>
    /// Import the IObjectSaftety COM Interface. 
    /// See http://msdn.microsoft.com/en-us/library/aa768224(VS.85).aspx
    /// </summary>
    [ComImport]
    [Guid("CB5BDC81-93C1-11CF-8F20-00805F2CD064")] // This is the only Guid that cannot be modifed in this file
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IObjectSafety
    {
        [PreserveSig]
        int GetInterfaceSafetyOptions(ref Guid riid, out int pdwSupportedOptions, out int pdwEnabledOptions);

        [PreserveSig]
        int SetInterfaceSafetyOptions(ref Guid riid, int dwOptionSetMask, int dwEnabledOptions);
    }

}
