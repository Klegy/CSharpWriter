using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 知识库节点下拉列表项目提供者
    /// </summary>
    public interface IListItemsProvider
    {
        ListItemCollection GetListItems(WriterAppHost host , string sourceName );
    }
}
