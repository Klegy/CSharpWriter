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

namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 文件信息
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    public class VFileInfo : ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public VFileInfo()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="info">本地文件系统信息对象</param>
        public VFileInfo(System.IO.FileInfo info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            this.Name = info.Name;
            this.FullPath = info.FullName;
            this.Exists = info.Exists;
            this.BasePath = System.IO.Path.GetDirectoryName(info.FullName);
            if (info.Exists)
            {
                this.Length = info.Length;
                this.CreationTime = info.CreationTime;
                this.AccessTime = info.LastAccessTime;
                this.Readonly = info.IsReadOnly;
                this.Title = System.IO.Path.GetFileName(info.Name);
            }
        }

        private string _Name = null;
        /// <summary>
        /// 文件名
        /// </summary>
        [DefaultValue(null)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Format = null;
        /// <summary>
        /// 文件格式,可以为XML,RTF,HTML,OldXML
        /// </summary>
        [DefaultValue(null)]
        public string Format
        {
            get { return _Format; }
            set { _Format = value; }
        }

        private string _FullPath = null;
        /// <summary>
        /// 全路径名
        /// </summary>
        [DefaultValue(null)]
        public string FullPath
        {
            get { return _FullPath; }
            set { _FullPath = value; }
        }

        private string _BasePath = null;
        /// <summary>
        /// 基础路径
        /// </summary>
        [DefaultValue(null)]
        public string BasePath
        {
            get { return _BasePath; }
            set { _BasePath = value; }
        }


        private long _Length = 0;
        /// <summary>
        /// 文件长度
        /// </summary>
        [DefaultValue((long)0)]
        public long Length
        {
            get { return _Length; }
            set { _Length = value; }
        }

        private string _Title = null;
        /// <summary>
        /// 标题
        /// </summary>
        [DefaultValue(null)]
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private bool _Exists = true;
        /// <summary>
        /// 文件是否存在
        /// </summary>
        [DefaultValue(true)]
        public bool Exists
        {
            get { return _Exists; }
            set { _Exists = value; }
        }

        private DateTime _CreationTime = WriterConst.NullDate;
        /// <summary>
        /// 文件创建时间
        /// </summary>
        public DateTime CreationTime
        {
            get { return _CreationTime; }
            set { _CreationTime = value; }
        }

        private DateTime _AccessTime = WriterConst.NullDate;
        /// <summary>
        /// 文件访问时间
        /// </summary>
        public DateTime AccessTime
        {
            get { return _AccessTime; }
            set { _AccessTime = value; }
        }

        private bool _Readonly = false;
        /// <summary>
        /// 文件是只读的
        /// </summary>
        [DefaultValue(false)]
        public bool Readonly
        {
            get { return _Readonly; }
            set { _Readonly = value; }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public VFileInfo Clone()
        {
            return (VFileInfo)((ICloneable)this).Clone();
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
