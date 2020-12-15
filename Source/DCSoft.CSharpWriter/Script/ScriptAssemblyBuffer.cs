/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
﻿using System;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.Collections;

namespace DCSoft.Script
{
    /// <summary>
    /// assembly buffer for script engine
    /// </summary>
    public class ScriptAssemblyBuffer
    {
        /// <summary>
        /// initialize instance
        /// </summary>
        public ScriptAssemblyBuffer()
        {
            strBufferPath = System.IO.Path.Combine(
                System.IO.Path.GetTempPath(), 
                "XDesignerScriptAssemblyBuffer");
        }

        private System.AppDomain myAppDomain = System.AppDomain.CurrentDomain;
        /// <summary>
        /// appdomain to load assembly
        /// </summary>
        public System.AppDomain AppDomain
        {
            get
            {
                return myAppDomain; 
            }
            set
            {
                myAppDomain = value; 
            }
        }

        private string strBufferPath = null;
        /// <summary>
        /// directory for save file
        /// </summary>
        public string BufferPath
        {
            get
            {
                return strBufferPath; 
            }
            set
            {
                strBufferPath = value; 
            }
        }

        private int intMaxByteSize = 5000 * 1024;
        /// <summary>
        /// max size of buffer
        /// </summary>
        public int MaxByteSize
        {
            get
            {
                return intMaxByteSize; 
            }
            set
            {
                intMaxByteSize = value; 
            }
        }

        private System.Security.Cryptography.MD5CryptoServiceProvider myMD5
            = new System.Security.Cryptography.MD5CryptoServiceProvider();
        private string GetMD5(string txt)
        {
            byte[] bs = System.Text.Encoding.Unicode.GetBytes(txt);
            bs = myMD5.ComputeHash(bs);
            return Convert.ToBase64String(bs);
        }

        public void AddAssembly(string ScriptText, System.Reflection.Assembly asm, byte[] bsData)
        {
            AssemblyItem item = new AssemblyItem();
            item.MD5 = GetMD5(ScriptText);
            item.Assembly = asm;
            item.AssemblyData = bsData;
            item.ReferenceCount = 1;
            myItems.Add(item);
            CheckBuffer();
        }

        public void Clear()
        {
            myItems.Clear();
        }

        public void Save()
        {
            foreach (AssemblyItem item in myItems)
            {
                if (item.Saved == false
                    && item.AssemblyData != null 
                    && item.AssemblyData.Length > 0)
                {
                    SaveAssemblyData(item.MD5, item.AssemblyData);
                }
            }
        }

        public System.Reflection.Assembly GetAssembly(string ScriptText)
        {
            string md5 = GetMD5(ScriptText);
            foreach (AssemblyItem item in myItems)
            {
                if (item.MD5 == md5)
                {
                    item.ReferenceCount++;
                    item.LastTime = DateTime.Now;
                    return item.Assembly;
                }
            }
            byte[] bs = ReadAssemblyData(md5);
            if (bs != null)
            {
                AssemblyItem NewItem = new AssemblyItem();
                NewItem.MD5 = md5;
                NewItem.AssemblyData = bs;
                NewItem.Assembly = myAppDomain.Load(bs);
                NewItem.Saved = true;
                NewItem.ByteSize = bs.Length;
                myItems.Add(NewItem);
                CheckBuffer();
                return NewItem.Assembly;
            }
            return null;
        }

         

        private void CheckBuffer()
        {
            if (intMaxByteSize <= 0)
                return;
            while (myItems.Count > 1)
            {
                int size = 0;
                foreach (AssemblyItem item in myItems)
                {
                    size += item.ByteSize;
                }
                if (size > intMaxByteSize)
                {
                    AssemblyItem MinItem = null;
                    foreach (AssemblyItem item in myItems)
                    {
                        if (MinItem == null)
                        {
                            MinItem = item;
                        }
                        else
                        {
                            if (MinItem.LastTime > item.LastTime)
                            {
                                MinItem = item;
                            }
                        }
                    }
                    myItems.Remove(MinItem);
                    // buffer
                    if (strBufferPath != null && System.IO.Path.IsPathRooted(strBufferPath))
                    {
                        if (MinItem.Saved == false
                            && MinItem.AssemblyData != null
                            && MinItem.AssemblyData.Length > 0)
                        {
                            SaveAssemblyData(MinItem.MD5, MinItem.AssemblyData);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public virtual byte[] ReadAssemblyData(string md5)
        {
            if (strBufferPath != null && System.IO.Path.IsPathRooted(strBufferPath))
            {
                string fileName = System.IO.Path.Combine(strBufferPath, md5);
                if (System.IO.File.Exists(fileName))
                {
                    byte[] bs = null;
                    using (System.IO.FileStream stream = new System.IO.FileStream(
                        fileName,
                        System.IO.FileMode.Open,
                        System.IO.FileAccess.Read))
                    {
                        bs = new byte[stream.Length];
                        stream.Read(bs, 0, bs.Length);
                    }
                    return bs;
                }
            }
            return null;
        }

        public virtual void SaveAssemblyData(string md5, byte[] bsData)
        {
            // buffer
            if (strBufferPath != null && System.IO.Path.IsPathRooted(strBufferPath))
            {
                if (System.IO.Directory.Exists(strBufferPath) == false)
                {
                    System.IO.Directory.CreateDirectory(strBufferPath);
                }
                using (System.IO.FileStream file = new System.IO.FileStream(
                    System.IO.Path.Combine(
                    strBufferPath,
                    md5),
                    System.IO.FileMode.Create,
                    System.IO.FileAccess.Write))
                {
                    file.Write(
                        bsData,
                        0,
                        bsData.Length);
                }
            }
        }

        private ArrayList myItems = new ArrayList();

        private class AssemblyItem
        {
            /// <summary>
            /// MD5 flag for source code
            /// </summary>
            public string MD5 = null;
            /// <summary>
            /// assembly instance
            /// </summary>
            public System.Reflection.Assembly Assembly = null;
            /// <summary>
            /// binary for assembly
            /// </summary>
            public byte[] AssemblyData = null;
            /// <summary>
            /// time when load assembly 
            /// </summary>
            public DateTime LoadTime = DateTime.Now;
            /// <summary>
            /// time when assembly last use
            /// </summary>
            public DateTime LastTime = DateTime.Now;
            /// <summary>
            /// binary size
            /// </summary>
            public int ByteSize = 0;
            /// <summary>
            /// total of use assmebly
            /// </summary>
            public int ReferenceCount = 0;
            /// <summary>
            /// assembly saved
            /// </summary>
            public bool Saved = false;
        }

    }
}
