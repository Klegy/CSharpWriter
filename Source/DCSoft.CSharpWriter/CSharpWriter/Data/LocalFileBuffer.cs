using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;

namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 本地文件缓存
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    public class LocalFileBuffer
    {
        private static LocalFileBuffer _Instance = null;
        /// <summary>
        /// 对象唯一静态实例
        /// </summary>
        public static LocalFileBuffer Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new LocalFileBuffer();
                }
                return _Instance; 
            }
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        public LocalFileBuffer()
        {
        }

        private int _MaxFileNum = 0;
        /// <summary>
        /// 缓存的最大文件个数
        /// </summary>
        [DefaultValue( 0 )]
        public int MaxFileNum
        {
            get { return _MaxFileNum; }
            set { _MaxFileNum = value; }
        }

        private long _MaxFileSize = 0;
        /// <summary>
        /// 缓存的最大文件总大小
        /// </summary>
        [DefaultValue( ( long )  0 )]
        public long MaxFileSize
        {
            get { return _MaxFileSize; }
            set { _MaxFileSize = value; }
        }

        private string _WorkDirectory = System.IO.Path.Combine(
            System.Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData ) ,
            "DCLocalBuffer");
        /// <summary>
        /// 工作目录
        /// </summary>
        public string WorkDirectory
        {
            get
            {
                return _WorkDirectory; 
            }
            set
            {
                _WorkDirectory = value;
                _Files = null;
            }
        }

        private List<LocalFileBufferItem> _Files = null;
        /// <summary>
        /// 缓存的文件信息
        /// </summary>
        public List<LocalFileBufferItem> Files
        {
            get
            {
                if (_Files == null)
                {
 
                }
                return _Files;
            }
        }
    }

    /// <summary>
    /// 缓存文件信息对象
    /// </summary>
    [Serializable]
    public class LocalFileBufferItem
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public LocalFileBufferItem()
        {
        }

        private string _Url = null;
        /// <summary>
        /// 远程URL地址
        /// </summary>
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }

        private string _LocalFileName = null;
        /// <summary>
        /// 本地文件名
        /// </summary>
        public string LocalFileName
        {
            get { return _LocalFileName; }
            set { _LocalFileName = value; }
        }

        private int _Length = 0;
        /// <summary>
        /// 缓存的文件长度
        /// </summary>
        public int Length
        {
            get { return _Length; }
            set { _Length = value; }
        }
        private DateTime _CreationTime = DateTime.Now;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime
        {
            get { return _CreationTime; }
            set { _CreationTime = value; }
        }
    }
}
