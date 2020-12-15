using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Xml;

namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 用于获得列表项目的提供者接口
    /// </summary>
    public interface IListSourceProvider
    {
        object GetListSource(WriterAppHost host , ListSourceInfo info);

        string GetDisplayText(WriterAppHost host , object Value, ListSourceInfo info);

        string GetValue(WriterAppHost host , object Value, ListSourceInfo info);

        string GetTag(WriterAppHost host , object Value, ListSourceInfo info);
    }

}
