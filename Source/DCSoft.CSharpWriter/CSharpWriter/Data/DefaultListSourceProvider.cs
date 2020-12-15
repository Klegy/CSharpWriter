using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.CSharpWriter.Dom;

namespace DCSoft.CSharpWriter.Data
{

    /// <summary>
    /// 默认的数据列表提供者对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class DefaultListSourceProvider : IListSourceProvider
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DefaultListSourceProvider()
        {
        }


        private static ListItemCollection NullListItems = new ListItemCollection();
        /// <summary>
        /// 获得列表项目
        /// </summary>
        /// <param name="host">编辑器宿主对象</param>
        /// <param name="info">列表信息</param>
        /// <returns>列表项目数据</returns>
        public virtual object GetListSource( WriterAppHost host , ListSourceInfo info)
        {
            if (info == null)
            {
                return null;
            }
            KBLibrary lib = (KBLibrary)host.Services.GetService(typeof(KBLibrary));
            string sourceName = info.SourceName;
            if (lib != null)
            {
                KBEntry item = lib.SearchKBEntry(sourceName);
                if (item != null)
                {
                    if (item.ListItems == NullListItems)
                    {
                        // 已经尝试加载过了，但未成功
                        return null;
                    }
                    if (item.ListItems != null && item.ListItems.Count > 0)
                    {
                        return item.ListItems;
                    }
                    IListItemsProvider p = (IListItemsProvider)host.Services.GetService(
                        typeof(IListItemsProvider));
                    if (p != null)
                    {
                        string source = item.ListItemsSource;
                        if (string.IsNullOrEmpty(source))
                        {
                            source = item.Value;
                        }
                        ListItemCollection list = p.GetListItems(host, source);
                        if (list == null || list.Count == 0)
                        {
                            list = NullListItems;
                        }
                        item.ListItems = list;
                        return list;
                    }
                }
            }
            // 没有从知识库找到相应的知识节点，直接调用接口获得下拉列表内容
            IListItemsProvider p2 = (IListItemsProvider)host.Services.GetService(
                typeof(IListItemsProvider));
            if (p2 != null)
            {
                ListItemCollection list = p2.GetListItems(host, info.SourceName);
                return list;
            }
            return null;
        }

        public virtual string GetDisplayText(WriterAppHost host, object Value, ListSourceInfo info)
        {
            XDataBindingProvider bp = (XDataBindingProvider)host.Services.GetService(typeof(XDataBindingProvider));
            return ListSourceInfo.StdGetDisplayText(Value, info, bp);
        }

        public virtual string GetValue(WriterAppHost host, object Value, ListSourceInfo info)
        {
            XDataBindingProvider bp = (XDataBindingProvider)host.Services.GetService(typeof(XDataBindingProvider));
            return ListSourceInfo.StdGetValue(Value, info, bp);
        }

        public virtual string GetTag(WriterAppHost host, object Value, ListSourceInfo info)
        {
            XDataBindingProvider bp = (XDataBindingProvider)host.Services.GetService(typeof(XDataBindingProvider));
            return ListSourceInfo.StdGetTag(Value, info, bp);
        }
    }
}
