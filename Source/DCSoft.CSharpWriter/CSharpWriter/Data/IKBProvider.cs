using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.CSharpWriter.Dom;
using System.ComponentModel ;

namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 知识库提供者对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public interface IKBProvider
    {
        /// <summary>
        /// 获得子条目列表
        /// </summary>
        /// <param name="root">根节点对象,若为空表示获得根项目列表</param>
        /// <returns>获得的子条目列表</returns>
        KBEntryList GetSubEntries( WriterAppHost host , KBEntry root);
        /// <summary>
        /// 根据知识库条目创建文档元素对象
        /// </summary>
        /// <param name="document"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        DomElementList CreateElements(WriterAppHost host , DomDocument document, KBEntry item);
    }

    
}
