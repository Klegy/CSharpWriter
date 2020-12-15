using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 复选框元素编辑器
    /// </summary>
    public class XTextCheckBoxElementEditor : ElementEditor 
    {
        public override bool IsSupportMethod(DCSoft.CSharpWriter.ElementEditMethod method)
        {
            return true;
        }
        public override bool Edit(ElementEditEventArgs args)
        {
            using (dlgCheckBoxElementEditor dlg = new dlgCheckBoxElementEditor())
            {
                dlg.SourceEventArgs = args;
                if (dlg.ShowDialog(args.ParentWindow) == System.Windows.Forms.DialogResult.OK)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
