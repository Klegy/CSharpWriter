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
using DCSoft.Printing;
using DCSoft.CSharpWriter;

namespace DCSoft.CSharpWriter.Dom
{
    [Serializable()]
    [System.Diagnostics.DebuggerDisplay("Count={ Count }")]
    [System.Diagnostics.DebuggerTypeProxy(typeof(DCSoft.Common.ListDebugView))]
    public class DomDocumentList : List<DomDocument>
    {
        public DomDocumentList()
        {
        }

        public DomDocumentList( DomDocument document )
        {
            this.Add(document);
        }

        public DomDocument FirstDocument
        {
            get
            {
                if (this.Count > 0)
                {
                    return this[0];
                }
                else
                {
                    return null;
                }
            }
        }

        public DomDocument LastDocument
        {
            get
            {
                if (this.Count > 0)
                {
                    return this[this.Count - 1];
                }
                else
                {
                    return null;
                }
            }
        }

        public PrintPageCollection AllPages
        {
            get
            {
                PrintPageCollection ps = new PrintPageCollection();
                foreach (DomDocument doc in this)
                {
                    ps.AddRange(doc.Pages);
                }
                for (int iCount = 0; iCount < ps.Count; iCount++)
                {
                    ps[iCount].GlobalIndex = iCount;
                }
                return ps;
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public string Title
        {
            get
            {
                foreach (DomDocument document in this)
                {
                    if (string.IsNullOrEmpty(document.Info.Title) == false )
                    {
                        return document.Info.Title;
                    }
                }
                foreach (DomDocument document in this)
                {
                    if (string.IsNullOrEmpty(document.FileName) == false)
                    {
                        return System.IO.Path.GetFileNameWithoutExtension(document.FileName);
                    }
                }
                return "DCSoft.CSharpWriter document";
            }
        }
    }
}
