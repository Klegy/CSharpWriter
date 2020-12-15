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
using System.IO;
using System.Windows.Forms;
using System.ComponentModel.Design ;

namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 虚拟文件系统接口
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public interface IFileSystem
    {
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>打开的文件流对象</returns>
        Stream Open( System.ComponentModel.Design.IServiceContainer services, string fileName);
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>文件流对象</returns>
        Stream Save( System.ComponentModel.Design.IServiceContainer service , string fileName);
        /// <summary>
        /// 获得指定文件名的文件信息
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>获得的文件信息</returns>
        VFileInfo GetFileInfo(System.ComponentModel.Design.IServiceContainer service, string fileName);
        /// <summary>
        /// 浏览打开文件用的文件名
        /// </summary>
        /// <param name="initalizeFileName">初始化文件名</param>
        /// <returns>选择的文件名</returns>
        string BrowseOpen(System.ComponentModel.Design.IServiceContainer service, string initalizeFileName);
        /// <summary>
        /// 浏览保存文件时用的文件名
        /// </summary>
        /// <param name="initalizeFileName">初始化文件名</param>
        /// <returns>选择的文件名</returns>
        string BrowseSave(System.ComponentModel.Design.IServiceContainer service, string initalizeFileName);
    }

    /// <summary>
    /// 文件系统列表对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class FileSystemDictionary : Dictionary<string, IFileSystem>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public FileSystemDictionary()
        {
        }

        /// <summary>
        /// 打开和保存文档使用的文件系统
        /// </summary>
        public IFileSystem Docuemnt
        {
            get
            {
                return this.GetFileSystem(WriterConst.FS_Document);
            }
            set
            {
                this[WriterConst.FS_Document] = value;
            }
        }

        /// <summary>
        /// 打开和保存模板使用的文件系统
        /// </summary>
        public IFileSystem Template
        {
            get
            {
                return this.GetFileSystem(WriterConst.FS_Template);
            }
            set
            {
                this[WriterConst.FS_Template] = value;
            }
        }

        /// <summary>
        /// 打开和保存知识库使用的文件系统
        /// </summary>
        public IFileSystem KBLibray
        {
            get
            {
                return this.GetFileSystem(WriterConst.FS_KBLibrary);
            }
            set
            {
                this[WriterConst.FS_KBLibrary] = value;
            }
        }

        /// <summary>
        /// 加载知识节点列表项目使用的文件系统
        /// </summary>
        public IFileSystem KBListItem
        {
            get
            {
                return this.GetFileSystem(WriterConst.FS_KBListItem);
            }
            set
            {
                this[WriterConst.FS_KBListItem] = value;
            }
        }

        /// <summary>
        /// 默认文件系统，如其他专用的文件系统为空则转而使用默认文件系统。
        /// </summary>
        public IFileSystem Default
        {
            get
            {
                return this.GetFileSystem(WriterConst.FS_Default);
            }
            set
            {
                this[WriterConst.FS_Default] = value;
            }
        }

        /// <summary>
        /// 获得指定名称的文件系统,本函数能确保不返回空引用
        /// </summary>
        /// <remarks>
        /// 本方法首先区分大小写的查找名称，若没找到则使用不区分大小写的查找，
        /// 若还没找到则返回默认名称default的项目，若还没找到则返回默认的标准文件系统对象。
        /// </remarks>
        /// <param name="name">指定的名称</param>
        /// <returns>获得的文件系统</returns>
        public IFileSystem GetFileSystem(string name)
        {
            IFileSystem result = null;
            if (this.ContainsKey(name))
            {
                result = this[name];
            }
            if (result == null)
            {
                foreach (string key in this.Keys)
                {
                    if (string.Compare(key, name, true) == 0)
                    {
                        result = this[key];
                        break;
                    }
                }
            }
            if (result == null)
            {
                foreach (string key in this.Keys)
                {
                    if (string.Compare(key, "default", true) == 0)
                    {
                        result = this[key];
                        break;
                    }
                }
            }
            if (result == null)
            {
                result = DefaultFileSystem.Instance;
            }
            return result;
        }
    }

    /// <summary>
    /// 文件系统方法标记
    /// </summary>
    [Flags]
    public enum FileSystemMethods
    {
        /// <summary>
        /// 打开文件
        /// </summary>
        Open = 1,
        /// <summary>
        /// 保存文件
        /// </summary>
        Save =2 ,
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        Exists = 4 ,
        /// <summary>
        /// 为打开文件而浏览文件
        /// </summary>
        BrowseOpen = 8,
        /// <summary>
        /// 为保存文件而浏览文件
        /// </summary>
        BrowseSave = 16,
        /// <summary>
        /// 获得文件格式
        /// </summary>
        GetFileFormat=32
    }
}
