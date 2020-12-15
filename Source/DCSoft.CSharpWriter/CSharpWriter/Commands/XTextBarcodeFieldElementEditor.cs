using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 条码元素编辑器
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class XTextBarcodeFieldElementEditor : ElementEditor
    {
        public override bool IsSupportMethod(DCSoft.CSharpWriter.ElementEditMethod method)
        {
            return true;
        }
        public override bool Edit(ElementEditEventArgs args)
        {
            using (dlgBarcodeElementEditor dlg = new dlgBarcodeElementEditor())
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
