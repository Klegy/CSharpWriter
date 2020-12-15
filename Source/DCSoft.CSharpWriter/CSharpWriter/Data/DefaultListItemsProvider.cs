using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 默认的列表项目提供者对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class DefaultListItemsProvider :IListItemsProvider 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DefaultListItemsProvider()
        {
        }

        private static Dictionary<string, ListItemCollection> _LoadedListItems 
            = new Dictionary<string, ListItemCollection>();


        /// <summary>
        /// 根据知识库节点的ListItemsSource加载下拉列表项目
        /// </summary>
        /// <param name="host">宿主对象</param>
        /// <param name="sourceName">列表名称</param>
        /// <returns>获得的下拉列表项目</returns>
        public ListItemCollection GetListItems(WriterAppHost host, string sourceName)
        {
            KBLibrary lib = ( KBLibrary ) host.Services.GetService(typeof(KBLibrary));
            if (lib == null)
            {
                return null;
            }
            if (string.IsNullOrEmpty(sourceName))
            {
                return null;
            }
            string src = sourceName.Trim();
            string url = src;
            if (string.IsNullOrEmpty(lib.ListItemsSourceFormatString) == false)
            {
                url = string.Format(lib.ListItemsSourceFormatString, src);
            }
            url = WriterUtils.CombinUrl(
                lib.RuntimeBaseURL,
                url);

            WriterDebugger debugger = host.DebugMode ? host.Services.Debugger : null ;
            if( _LoadedListItems.ContainsKey( url ))
            {
                if( host.Debuger != null )
                {
                    host.Debuger.WriteLine(string.Format(
                        WriterStrings.LoadListItemsFromBuffer_Name_URL,
                        sourceName,
                        url));
                }
                return _LoadedListItems[url].Clone();
            }

            IFileSystem fs = host.FileSystems.KBListItem;
            VFileInfo info = fs.GetFileInfo( host.Services , url);
            if (info.Exists == false)
            {
                // 文件不存在
                return null;
            }
            else
            {
                if ( host.Debuger != null )
                {
                    host.Debuger.DebugLoadingFile( url );
                }
                XmlDocument xmlDoc = null ;
                try
                {
                    System.IO.Stream stream = fs.Open(host.Services, url);
                    if (stream != null)
                    {
                        using (stream)
                        {
                            xmlDoc = new System.Xml.XmlDocument();
                            xmlDoc.Load(stream);
                            if (host.Debuger != null )
                            {
                                host.Debuger.DebugLoadFileComplete( ( int ) stream.Length );
                            }
                        }
                    }
                }
                catch (Exception ext)
                {
                    System.Diagnostics.Debug.WriteLine(ext.ToString());
                }
                if( xmlDoc != null )
                {
                    ListItemCollection items = new ListItemCollection();
                    foreach (System.Xml.XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                    {
                        if (node.Name == "Item")
                        {
                            ListItem newItem = new ListItem();
                            foreach (XmlNode node2 in node.ChildNodes)
                            {
                                if (node2.Name == "Text")
                                {
                                    newItem.Text = node2.InnerText;
                                }
                                else if (node2.Name == "Value")
                                {
                                    newItem.Value = node2.InnerText;
                                }
                            }
                            items.Add(newItem);
                        }
                    }
                    //if (host.Debuger != null )
                    //{
                    //    host.Debuger.WriteLine(
                    //        string.Format(
                    //            WriterStrings.LoadListItems_ProviderType_Name_Num,
                    //            this.GetType().Name ,
                    //            sourceName ,
                    //            items == null ? "NULL" : items.Count.ToString()));
                    //}
                    _LoadedListItems[url] = items.Clone();
                    return items;
                }
            }
            return null;
        }
    }
}
