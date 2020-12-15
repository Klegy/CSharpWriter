using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.CSharpWriter.Dom;

namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 默认的知识库提供者对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class DefaultKBProvider : IKBProvider
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DefaultKBProvider()
        {
        }

        /// <summary>
        /// 获得子知识节点列表
        /// </summary>
        /// <param name="host">宿主对象</param>
        /// <param name="root">根知识点节点</param>
        /// <returns>获得的子知识点列表</returns>
        public virtual KBEntryList GetSubEntries(WriterAppHost host, KBEntry root)
        {
            if (root == null)
            {
                KBLibrary lib = (KBLibrary)host.Services.GetService(typeof(KBLibrary));
                if (lib != null)
                {
                    return lib.KBEntries;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return root.SubEntries;
            }
        }

        /// <summary>
        /// 根据知识节点信息创建文档元素对象
        /// </summary>
        /// <param name="host">编辑器宿主对象</param>
        /// <param name="document">文档对象</param>
        /// <param name="item">知识节点对象</param>
        /// <returns>创建的文档元素对象集合</returns>
        public virtual DomElementList CreateElements(
            WriterAppHost host, 
            DomDocument document,
            KBEntry item)
        {
            KBLibrary lib = (KBLibrary)host.Services.GetService(typeof(KBLibrary));
            DomElementList list = new DomElementList();
            if (item.SubEntries == null || item.SubEntries.Count == 0)
            {
                // 没有子节点的知识节点才能创建文档元素
                if (item.Style == KBItemStyle.List)
                {
                    DomInputFieldElement field = new DomInputFieldElement();
                    field.BackgroundText = item.Text;
                    field.Name = item.Text;
                    field.ID = item.Value;
                    field.FieldSettings = new InputFieldSettings();
                    field.FieldSettings.ListSource = new ListSourceInfo();
                    field.FieldSettings.EditStyle = InputFieldEditStyle.DropdownList;
                    string source = item.ID;
                    if (string.IsNullOrEmpty(source))
                    {
                        source = item.Value;
                    }
                    field.FieldSettings.ListSource.SourceName = source;
                    field.UserEditable = false;
                    list.Add(field);
                }
                else if (item.Style == KBItemStyle.Template)
                {
                    // 导入模板
                    try
                    {
                        string fileName = item.Value;
                        FileFormat specifyFormat = FileFormat.XML ;
                        System.IO.Stream stream = OpenTemplateFileStream(host, fileName, ref specifyFormat);
                        if (stream != null)
                        {
                            if (specifyFormat == FileFormat.None)
                            {
                                specifyFormat = FileFormat.XML;
                            }
                            DomDocument subdocument = (DomDocument)document.Clone(false);
                            subdocument.Load(stream, specifyFormat);
                            if (host.Debuger != null)
                            {
                                host.Debuger.DebugLoadFileComplete((int)stream.Length);
                            }
                            subdocument.CommitUserTrace();
                            if (subdocument.Body.Elements.Count > 1)
                            {
                                DomElementList elements = new DomElementList();
                                elements.AddRange(subdocument.Body.Elements);
                                WriterUtils.RemoveAutoCreateParagraphFlag(elements);
                                if (elements.Count > 0)
                                {
                                    document.ImportElements(elements);
                                    list.AddRange(elements);
                                }
                            }//if
                        }//if
                    }
                    catch (Exception ext)
                    {
                        if (host.Debuger != null)
                        {
                            host.Debuger.WriteLine(ext.Message);
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 获得指定名称的模板的文件流对象
        /// </summary>
        /// <param name="host">应用程序宿主对象</param>
        /// <param name="fileName">文件名</param>
        /// <returns>获得的文件流对象</returns>
        public virtual System.IO.Stream OpenTemplateFileStream(
            WriterAppHost host, 
            string fileName , 
            ref FileFormat format )
        {
            KBLibrary lib = (KBLibrary)host.Services.GetService(typeof(KBLibrary));
            IFileSystem fs = null;
            if (string.IsNullOrEmpty(lib.TemplateFileSystemName) == false)
            {
                // 获得指定名称的文件系统名称
                fs = host.FileSystems.GetFileSystem(lib.TemplateFileSystemName);
            }
            if (fs == null)
            {
                // 获得默认使用的文件系统名称
                fs = host.FileSystems.Template;
            }
            format = FileFormat.None;
            if (lib != null)
            {
                if (string.IsNullOrEmpty(lib.TemplateSourceFormatString) == false)
                {
                    fileName = string.Format(lib.TemplateSourceFormatString, fileName);
                    fileName = WriterUtils.CombinUrl(lib.RuntimeBaseURL, fileName);
                    format = lib.TemplateFileFormat;
                }
            }
            if (host.Debuger != null)
            {
                host.Debuger.WriteLine(string.Format(
                    WriterStrings.LoadTemplate_FS_FileName,
                    fs.GetType().Name,
                    fileName));
            }
            VFileInfo info = fs.GetFileInfo(host.Services, fileName);
            if (info.Exists == false)
            {
                // 文件不存在
                if (host.Debuger != null)
                {
                    host.Debuger.WriteLine(
                        string.Format(WriterStrings.FileNotExist_FileName, fileName));
                }
                return null;
            }
            if (format == FileFormat.None)
            {
                format = WriterUtils.ParseFileFormat(info.Format);
            }
            if (host.Debuger != null)
            {
                host.Debuger.DebugLoadingFile(fileName);
            }
            return fs.Open(host.Services, fileName);
        }
    }
}
