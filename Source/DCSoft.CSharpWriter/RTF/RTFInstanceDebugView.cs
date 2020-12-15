/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
/*
 * 
 *   DCSoft RTF DOM v1.0
 *   Author : Yuan yong fu.
 *   Email  : yyf9989@hotmail.com
 *   blog site:http://www.cnblogs.com/xdesigner.
 * 
 */


using System;
using System.Text;

namespace DCSoft.RTF
{
    /// <summary>
    /// debug information dispalyer at design time
    /// </summary>
    public class RTFInstanceDebugView
    {
        /// <summary>
        /// initialize instance
        /// </summary>
        /// <param name="instance"></param>
        public RTFInstanceDebugView(object instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");
            myInstance = instance;
        }

        private object myInstance = null;

        /// <summary>
        /// output debug item at design time
        /// </summary>
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.RootHidden)]
        public object Items
        {
            get
            {
                if (myInstance is System.Collections.IEnumerable)
                {
                    System.Collections.CollectionBase list = (System.Collections.CollectionBase)myInstance;
                    object[] items = new object[list.Count];
                    int iCount = 0;
                    foreach (object obj in list)
                    {
                        items[iCount] = obj;
                        iCount++;
                    }
                    return items;
                }
                else if (myInstance is RTFColorTable)
                {
                    RTFColorTable table = (RTFColorTable)myInstance;
                    object[] items = new object[table.Count];
                    for (int iCount = 0; iCount < table.Count; iCount++)
                    {
                        items[iCount] = table[iCount];
                    }
                    return items;
                }
                else if (myInstance is RTFDocumentInfo)
                {
                    RTFDocumentInfo info = (RTFDocumentInfo)myInstance;
                    return info.StringItems;
                }
                else
                {
                    return myInstance;
                }
            }
        }
    }
}
