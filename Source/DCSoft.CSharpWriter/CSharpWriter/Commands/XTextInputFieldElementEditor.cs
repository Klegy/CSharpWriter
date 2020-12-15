using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.CSharpWriter.Dom;

namespace DCSoft.CSharpWriter.Commands
{

    /// <summary>
    /// 输入域的元素编辑器
    /// </summary>
    public class XTextInputFieldElementEditor : ElementEditor
    {
        public override bool IsSupportMethod(ElementEditMethod method)
        {
            return true;
        }
        public override bool Edit(ElementEditEventArgs args)
        {
            using (dlgInputFieldEditor dlg = new dlgInputFieldEditor())
            {
                dlg.SourceEventArgs = args;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return true;
                }
            }
            return false;
        }
    }

}
