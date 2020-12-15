using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 知识库
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    public class KBLibrary
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public KBLibrary()
        {
        }

        private string _TemplateFileSystemName = WriterConst.FS_Template ;
        /// <summary>
        /// 知识库中加载模板使用的文件系统名称
        /// </summary>
        [DefaultValue(WriterConst.FS_Template)]
        public string TemplateFileSystemName
        {
            get
            {
                return _TemplateFileSystemName; 
            }
            set
            {
                _TemplateFileSystemName = value; 
            }
        }

        private string _BaseURL = null;
        /// <summary>
        /// 动态下载数据使用的基础路径
        /// </summary>
        [Browsable( false )]
        public string BaseURL
        {
            get
            {
                return _BaseURL; 
            }
            set
            {
                _BaseURL = value; 
            }
        }

        [NonSerialized]
        private string _RuntimeBaseURL = null;
        /// <summary>
        /// 运行时的基础路径
        /// </summary>
        internal string RuntimeBaseURL
        {
            get
            {
                if (string.IsNullOrEmpty(_BaseURL))
                {
                    return _RuntimeBaseURL;
                }
                else
                {
                    return _BaseURL;
                }
            }
            set
            {
                _RuntimeBaseURL = value; 
            }
        }

        private string _ListItemsSourceFormatString = null;
        /// <summary>
        /// 列表项目来源格式化字符串
        /// </summary>
        [DefaultValue( null )]
        public string ListItemsSourceFormatString
        {
            get
            {
                return _ListItemsSourceFormatString; 
            }
            set
            {
                _ListItemsSourceFormatString = value; 
            }
        }

        private string _TemplateSourceFormatString = null;
        /// <summary>
        /// 模板来源格式化字符串
        /// </summary>
        [DefaultValue( null )]
        public string TemplateSourceFormatString
        {
            get
            {
                return _TemplateSourceFormatString; 
            }
            set
            {
                _TemplateSourceFormatString = value; 
            }
        }

        private FileFormat _TemplateFileFormat = FileFormat.XML;
        /// <summary>
        /// 模板文件格式
        /// </summary>
        [DefaultValue( FileFormat.XML )]
        public FileFormat TemplateFileFormat
        {
            get
            {
                return _TemplateFileFormat; 
            }
            set
            {
                _TemplateFileFormat = value; 
            }
        }

        private string _Version = null;
        /// <summary>
        /// 知识库版本号
        /// </summary>
        [DefaultValue( null )]
        [XmlAttribute]
        public string Version
        {
            get
            {
                return _Version; 
            }
            set
            {
                _Version = value; 
            }
        }

        private KBEntryList _KBEntries = null;
        /// <summary>
        /// 知识库列表
        /// </summary>
        [DefaultValue( null )]
        [XmlArrayItem("Entry" , typeof( KBEntry ))]
        public virtual KBEntryList KBEntries
        {
            get
            {
                return _KBEntries; 
            }
            set
            {
                _KBEntries = value; 
            }
        }

        /// <summary>
        /// 系统中所有的知识库节点列表
        /// </summary>
        [NonSerialized]
        private KBEntryList _AllKBEntries = null;
        /// <summary>
        /// 返回系统中所有的知识库节点列表
        /// </summary>
        [Browsable( false )]
        public KBEntryList AllKBEntries
        {
            get
            {
                if (_AllKBEntries == null)
                {
                    _AllKBEntries = new KBEntryList();
                    if (this.KBEntries != null)
                    {
                        FillAllKBEntries(this.KBEntries, _AllKBEntries);
                    }
                }
                return _AllKBEntries; 
            }
        }

        private void FillAllKBEntries(KBEntryList rootList, KBEntryList result)
        {
            foreach (KBEntry kb in rootList)
            {
                result.Add(kb);
                if (kb.SubEntries != null && kb.SubEntries.Count > 0)
                {
                    FillAllKBEntries(kb.SubEntries, result);
                }
            }
        }

        /// <summary>
        /// 更新AllKBEntries属性值
        /// </summary>
        public void UpdateAllKBEntries()
        {
            _AllKBEntries = null;
            _KBEntriesForSearch = null;
        }

        /// <summary>
        /// 参与搜索的知识库节点列表集合
        /// </summary>
        [NonSerialized]
        private KBEntryList _KBEntriesForSearch = null;

        /// <summary>
        /// 根据拼音码检索知识库
        /// </summary>
        /// <param name="spellCode">拼音码</param>
        /// <returns>检索得到的知识库节点列表</returns>
        public KBEntryList SearchKBEntries(string spellCode)
        {
            if (spellCode == null)
            {
                spellCode = "";
            }
            spellCode = spellCode.Trim();
            if (spellCode.Length == 0)
            {
                // 若没有指明拼音码则直接返回根节点列表
                return this.KBEntries;
            }
            if (_KBEntriesForSearch == null)
            {
                _KBEntriesForSearch = new KBEntryList();
                foreach (KBEntry item in this.AllKBEntries)
                {
                    if (string.IsNullOrEmpty(item.Text))
                    {
                        // 文本为空的节点不参与搜索
                        continue;
                    }
                    if (item.SubEntries != null && item.SubEntries.Count > 0)
                    {
                        // 带有子节点的节点不参与搜索
                        continue;
                    }
                    _KBEntriesForSearch.Add(item);
                }//foreach
                // 按节点文本进行排序
                _KBEntriesForSearch.Sort(new KBTextComparer( false ));
            }
            KBEntryList firstList = new KBEntryList();
            KBEntryList secendlist = new KBEntryList();
            KBEntryList thirdlist = new KBEntryList();
            foreach (KBEntry item in _KBEntriesForSearch )
            {
                if( item.Text.StartsWith( spellCode ))
                {
                    // 第一梯队
                    firstList.Add( item );
                }
                else if (item.SpellCode.StartsWith(spellCode))
                {
                    // 第二梯队
                    secendlist.Add(item);
                }
                else if( item.SpellCode.IndexOf( spellCode ) > 0 )
                {
                    // 第三梯队
                    thirdlist.Add(item);
                }
            }
            //if (firstList.Count > 1)
            //{
            //    firstList.Sort(new KBTextComparer());
            //}
            //if (secendlist.Count > 1)
            //{
            //    secendlist.Sort(new KBTextComparer());
            //}
            //if (thirdlist.Count > 1)
            //{
            //    thirdlist.Sort(new KBTextComparer());
            //}
            KBEntryList result = new KBEntryList();
            result.AddRange(firstList);
            result.AddRange(secendlist);
            // 对于精确匹配的，还要按照文本长度重新排序
            result.Sort(new KBTextComparer(true));
            //if (secendlist.Count > 0)
            //{
            //    if (firstList.Count > 1 )
            //    {
            //        result.Add(KBEntry.NullKBEntry);
            //    }
            //    result.AddRange(secendlist);
            //}
            if ( result.Count > 0 
                && result.Last != KBEntry.NullKBEntry
                && thirdlist.Count > 0)
            {
                result.Add(KBEntry.NullKBEntry);
            }
            //if ( result.Count > 0 && result[0] == KBEntry.NullKBEntry)
            //{
            //    System.Console.Write("");
            //}
            result.AddRange(thirdlist);
            return result;
        }

        private class KBTextComparer : IComparer<KBEntry>
        {
            public KBTextComparer( bool cl )
            {
                _compareTextLength = cl;
            }
            private bool _compareTextLength = false;

            public int Compare(KBEntry x, KBEntry y)
            {
                if (_compareTextLength)
                {
                    int len1 = x.Text.Length;
                    int len2 = y.Text.Length;
                    if (len1 != len2)
                    {
                        return len1 - len2;
                    }
                }
                return string.Compare(x.Text, y.Text);
            }
        }

        /// <summary>
        /// 查找指定ID号的知识节点
        /// </summary>
        /// <param name="id">ID号</param>
        /// <returns>找到的知识节点对象</returns>
        public KBEntry SearchKBEntry(string id)
        {
            if (this.KBEntries != null && this.KBEntries.Count > 0)
            {
                return InnserSearchKBEntry(this.KBEntries, id);
            }
            else
            {
                return null;
            }
        }

        private KBEntry InnserSearchKBEntry(List<KBEntry> entries, string id)
        {
            foreach (KBEntry item in entries)
            {
                if (item.ID == id)
                {
                    return item;
                }
                if (item.SubEntries != null && item.SubEntries.Count > 0)
                {
                    KBEntry item2 = InnserSearchKBEntry(item.SubEntries, id);
                    if (item2 != null)
                    {
                        return item2;
                    }
                }
            }
            return null;
        }

        private List<KBTemplateDocument> _TemplateDocuments = null;
        /// <summary>
        /// 文档模板列表
        /// </summary>
        [DefaultValue( null )]
        [XmlArrayItem( "Template" , typeof( KBTemplateDocument ))]
        public virtual List<KBTemplateDocument> TemplateDocuments
        {
            get
            {
                return _TemplateDocuments; 
            }
            set
            {
                _TemplateDocuments = value; 
            }
        }
        /// <summary>
        /// 从文件流中加载整个知识库
        /// </summary>
        /// <param name="stream">文件流对象</param>
        /// <returns>操作是否成功</returns>
        public virtual bool Load(System.IO.Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            XmlSerializer ser = new XmlSerializer(typeof(KBLibrary));

            KBLibrary lib = (KBLibrary)ser.Deserialize(stream);
            CopyContent(lib);
            return true;
        }

        /// <summary>
        /// 从文件流中加载整个知识库
        /// </summary>
        /// <param name="reader">文件流对象</param>
        /// <returns>操作是否成功</returns>
        public virtual bool Load(System.IO.TextReader reader )
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            XmlSerializer ser = new XmlSerializer(typeof(KBLibrary));

            KBLibrary lib = (KBLibrary)ser.Deserialize(reader);
            CopyContent(lib);
            return true;
        }

        private void CopyContent(KBLibrary lib)
        {
            this.UpdateAllKBEntries();
            this.BaseURL = lib.BaseURL;
            this.Version = lib.Version;
            this.TemplateDocuments = lib.TemplateDocuments;
            this.KBEntries = lib.KBEntries;
            this.TemplateFileSystemName = lib.TemplateFileSystemName;
            this.ListItemsSourceFormatString = lib.ListItemsSourceFormatString;
            this.TemplateFileFormat = lib.TemplateFileFormat;
            this.TemplateSourceFormatString = lib.TemplateSourceFormatString;
            if (this.KBEntries != null)
            {
                foreach (KBEntry item in this.KBEntries)
                {
                    UpdateDomState(item);
                }
            }
        }

        private void UpdateDomState(KBEntry root)
        {
            foreach (KBEntry item in root.SubEntries)
            {
                item.Parent = root;
                if (item.SubEntries != null && item.SubEntries.Count > 0)
                {
                    UpdateDomState( item );
                }
            }
        }

        /// <summary>
        /// 保存对象到文件流中
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns>操作是否成功</returns>
        public virtual bool Save(System.IO.Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            XmlSerializer ser = new XmlSerializer(typeof(KBLibrary));
            ser.Serialize(stream, this);
            return true;
        }

        /// <summary>
        /// 保存对象到文件流中
        /// </summary>
        /// <param name="writer">文件流</param>
        /// <returns>操作是否成功</returns>
        public virtual bool Save(System.IO.TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            XmlSerializer ser = new XmlSerializer(typeof(KBLibrary));
            ser.Serialize(writer, this);
            return true;
        }

    }

    /// <summary>
    /// 知识库中的文档模板信息
    /// </summary>
    [Serializable]
    public class KBTemplateDocument
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public KBTemplateDocument()
        {
        }

        private string _Name = null;
        /// <summary>
        /// 模板名称
        /// </summary>
        [DefaultValue( null )]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Title = null;
        /// <summary>
        /// 标题
        /// </summary>
        [DefaultValue( null )]
        public string Title
        {
            get
            {
                return _Title; 
            }
            set
            {
                _Title = value; 
            }
        }
        private string _Version = null;
        /// <summary>
        /// 版本号
        /// </summary>
        [DefaultValue( null )]
        public string Version
        {
            get { return _Version; }
            set { _Version = value; }
        }

        private string _FileName = null;
        /// <summary>
        /// 文档文件名
        /// </summary>
        [DefaultValue( null )]
        public string FileName
        {
            get
            {
                return _FileName; 
            }
            set
            {
                _FileName = value; 
            }
        }

        private string _Content = null;
        /// <summary>
        /// 文档内容
        /// </summary>
        [DefaultValue( null )]
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }
    }
}
