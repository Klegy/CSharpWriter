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

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 标准功能命令名称
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    public class StandardCommandNames
    {
        /////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 文件功能模块名称
        /// </summary>
        public const string ModuleFile = "ModuleFile";
        /// <summary>
        /// 打开文件
        /// </summary>
        public const string FileOpen = "FileOpen";
        /// <summary>
        /// 打开旧格式的XML文档
        /// </summary>
        public const string FileOpenOldXMLFormat = "FileOpenOldXMLFormat";
        /// <summary>
        /// 打开指定URL路径的文档
        /// </summary>
        public const string FileOpenUrl = "FileOpenUrl";
        /// <summary>
        /// 新建文件
        /// </summary>
        public const string FileNew = "FileNew";
        /// <summary>
        /// 保存文件
        /// </summary>
        public const string FileSave = "FileSave";
        /// <summary>
        /// 文件另存为
        /// </summary>
        public const string FileSaveAs = "FileSaveAs";
        /// <summary>
        /// 打印整个文档
        /// </summary>
        public const string FilePrint = "FilePrint";
        /// <summary>
        /// 整洁打印,不打印修改痕迹和标记。
        /// </summary>
        public const string FileCleanPrint = "FileCleanPrint";
        /// <summary>
        /// 打印当前页
        /// </summary>
        public const string FilePrintCurrentPage = "FilePrintCurrentPage";
        /// <summary>
        /// 文档页面设置
        /// </summary>
        public const string FilePageSettings = "FilePageSettings";
        public const string FileRecent = "FileRecent";
        /// <summary>
        /// 获得文件XML代码
        /// </summary>
        public const string ViewXMLSource = "ViewXMLSource";
        /// <summary>
        /// 注册
        /// </summary>
        public const string Register = "Register";
        /// <summary>
        /// 加载知识库文件
        /// </summary>
        public const string LoadKBLibrary = "LoadKBLibrary";
        /// <summary>
        /// 保存知识库文件
        /// </summary>
        public const string SaveKBLibrary = "SaveKBLibrary";
        /// <summary>
        /// 文档选项
        /// </summary>
        public const string DocumentOptions = "DocumentOptions";
        /// <summary>
        /// 设置文档的默认字体
        /// </summary>
        public const string DocumentDefaultFont = "DocumentDefaultFont";
        /// <summary>
        /// 根据数据源更新文档视图
        /// </summary>
        public const string UpdateViewForDataSource = "UpdateViewForDataSource";
        /// <summary>
        /// 读取文档视图，更新数据源
        /// </summary>
        public const string UpdateDataSourceForView = "UpdateDataSourceForView";
        /////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 浏览功能模块名称
        /// </summary>
        public const string ModuleBrowse = "ModuleBrowse";
        /// <summary>
        /// 显示调试输出窗口
        /// </summary>
        public const string DebugOutputWindow = "DebugOutputWindow";
        /// <summary>
        /// 管理员视图模式
        /// </summary>
        public const string AdministratorViewMode = "AdministratorViewMode";

        ///// <summary>
        ///// 显示字符统计个数
        ///// </summary>
        //public const string WordCount = "WordCount";
        /// <summary>
        /// 显示关于控件对话框
        /// </summary>
        public const string AboutControl = "AboutControl";
        /// <summary>
        /// 续打模式
        /// </summary>
        public const string JumpPrintMode = "JumpPrintMode";
        /// <summary>
        /// 页面视图方式
        /// </summary>
        public const string PageViewMode = "PageViewMode";
        /// <summary>
        /// 常规视图方式
        /// </summary>
        public const string NormalViewMode = "NormalViewMode";
        /// <summary>
        /// 表单视图方式
        /// </summary>
        public const string FormViewMode = "FormViewMode";
        /// <summary>
        /// 整洁视图模式
        /// </summary>
        public const string CleanViewMode = "CleanViewMode";
        /// <summary>
        /// 复合视图模式
        /// </summary>
        public const string ComplexViewMode = "ComplexViewMode";
        /// <summary>
        /// 调试模式
        /// </summary>
        public const string DebugMode = "DebugMode";
        /// <summary>
        /// 设计模式
        /// </summary>
        public const string DesignMode = "DesignMode";
        /// <summary>
        /// 搜索,暂不支持
        /// </summary>
        public const string Search = "Search";
        public const string SearchReplace = "SearchReplace";
        /// <summary>
        /// 移动插入点的位置
        /// </summary>
        public const string MoveTo = "MoveTo";
        /// <summary>
        /// 向上翻页
        /// </summary>
        public const string MovePageUp = "MovePageUp";
        /// <summary>
        /// 向下翻页
        /// </summary>
        public const string MovePageDown = "MovePageDown";
        /// <summary>
        /// 向上移动一行
        /// </summary>
        public const string MoveUp = "MoveUp";
        /// <summary>
        /// 向下移动一行
        /// </summary>
        public const string MoveDown = "MoveDown";
        /// <summary>
        /// 向左移动一列
        /// </summary>
        public const string MoveLeft = "MoveLeft";
        /// <summary>
        /// 向右移动一列
        /// </summary>
        public const string MoveRight = "MoveRight";
        /// <summary>
        /// 移动到行首
        /// </summary>
        public const string MoveHome = "MoveHome";
        /// <summary>
        /// 移动到行尾
        /// </summary>
        public const string MoveEnd = "MoveEnd";
        /// <summary>
        /// 带选择的向上翻页
        /// </summary>
        public const string ShiftMovePageUp = "ShiftMovePageUp";
        /// <summary>
        /// 带选择的向下翻页
        /// </summary>
        public const string ShiftMovePageDown = "ShiftMovePageDown";
        /// <summary>
        /// 带选择的向上移动一行
        /// </summary>
        public const string ShiftMoveUp = "ShiftMoveUp";
        /// <summary>
        /// 带选择的向下移动一行
        /// </summary>
        public const string ShiftMoveDown = "ShiftMoveDown";
        /// <summary>
        /// 带选择的向左移动一列
        /// </summary>
        public const string ShiftMoveLeft = "ShiftMoveLeft";
        /// <summary>
        /// 带选择的向右移动一列
        /// </summary>
        public const string ShiftMoveRight = "ShiftMoveRight";
        /// <summary>
        /// 带选择的移动到行首
        /// </summary>
        public const string ShiftMoveHome = "ShiftMoveHome";
        /// <summary>
        /// 带选择的移动到行尾
        /// </summary>
        public const string ShiftMoveEnd = "ShiftMoveEnd";
        /// <summary>
        /// 让页面向上滚动一行而不改变插入点的位置
        /// </summary>
        public const string CtlMoveUp = "CtlMoveUp";
        /// <summary>
        /// 让页面向下滚动一行而不改变插入点的位置
        /// </summary>
        public const string CtlMoveDown = "CtlMoveDown";
        /// <summary>
        /// 全选
        /// </summary>
        public const string SelectAll = "SelectAll";
        /// <summary>
        /// 字数统计
        /// </summary>
        public const string WordCount = "WordCount";
        /////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 编辑功能模块名称
        /// </summary>
        public const string ModuleEdit = "ModuleEdit";
        /// <summary>
        /// 将文档内容转换为一个输入域
        /// </summary>
        public const string ConvertContentToField = "ConvertContentToField";
        /// <summary>
        /// 将输入域转换为普通文档内容
        /// </summary>
        public const string ConvertFieldToContent = "ConvertFieldToContent";
        /// <summary>
        /// 替换,暂不支持
        /// </summary>
        public const string Replace = "Replace";
        /// <summary>
        /// 退格删除
        /// </summary>
        public const string Backspace = "Backspace";
        /// <summary>
        /// 复制
        /// </summary>
        public const string Copy = "Copy";
        /// <summary>
        /// 剪切
        /// </summary>
        public const string Cut = "Cut";
        /// <summary>
        /// 粘贴
        /// </summary>
        public const string Paste = "Paste";
        /// <summary>
        /// 选择性粘贴
        /// </summary>
        public const string SpecifyPaste = "SpecifyPaste";
        /// <summary>
        /// 删除
        /// </summary>
        public const string Delete = "Delete";
        /// <summary>
        /// 撤销
        /// </summary>
        public const string Undo = "Undo";
        /// <summary>
        /// 重做
        /// </summary>
        public const string Redo = "Redo";
        /// <summary>
        /// 段落左对齐
        /// </summary>
        public const string AlignLeft = "AlignLeft";
        /// <summary>
        /// 段落居中对齐
        /// </summary>
        public const string AlignCenter = "AlignCenter";
        /// <summary>
        /// 段落右对齐
        /// </summary>
        public const string AlignRight = "AlignRight";
        /// <summary>
        /// 段落两边对齐
        /// </summary>
        public const string AlignJustify = "AlignJustify";
        /// <summary>
        /// 粗体
        /// </summary>
        public const string Bold = "Bold";
        /// <summary>
        /// 斜体
        /// </summary>
        public const string Italic = "Italic";
        /// <summary>
        /// 下划线
        /// </summary>
        public const string Underline = "Underline";
        /// <summary>
        /// 设置删除线
        /// </summary>
        public const string Strikeout = "Strikeout";
        /// <summary>
        /// 字体设置
        /// </summary>
        public const string Font = "Font";
        /// <summary>
        /// 字体名称
        /// </summary>
        public const string FontName = "FontName";
        /// <summary>
        /// 字体大小
        /// </summary>
        public const string FontSize = "FontSize";
        /// <summary>
        /// 前景色
        /// </summary>
        public const string Color = "Color";
        /// <summary>
        /// 背景色
        /// </summary>
        public const string BackColor = "BackColor";

        /// <summary>
        /// 段落左边框线
        /// </summary>
        public const string BorderLeft = "BorderLeft";
        /// <summary>
        /// 段落上边框线
        /// </summary>
        public const string BorderTop = "BorderTop";
        /// <summary>
        /// 段落右边框线
        /// </summary>
        public const string BorderRight = "BorderRight";
        /// <summary>
        /// 段落下边框线
        /// </summary>
        public const string BorderBottom = "BorderBottom";
        /// <summary>
        /// 设置文档默认样式
        /// </summary>
        public const string SetDefaultStyle = "SetDefaultStyle";
        ///// <summary>
        ///// 设置段落默认样式
        ///// </summary>
        //public const string SetDefaultParagraphStyle = "SetDefaultParagraphStyle";
        /// <summary>
        /// 插入纯文本数据
        /// </summary>
        public const string InsertString = "InsertString";
        /// <summary>
        /// 插入一段XML文档
        /// </summary>
        public const string InsertXML = "InsertXML";
        /// <summary>
        /// 插入HTML文档
        /// </summary>
        public const string InsertHtml = "InsertHtml";
        /// <summary>
        /// 插入文件内容
        /// </summary>
        public const string InsertFileContent = "InsertFileContent";
        /// <summary>
        /// 插入文档内容链接
        /// </summary>
        public const string InsertContentLink = "InsertContentLink";
        /// <summary>
        /// 插入RTF文本数据
        /// </summary>
        public const string InsertRTF = "InsertRTF";
        /// <summary>
        /// 插入软回车
        /// </summary>
        public const string InsertLineBreak = "InsertLineBreak";
        /// <summary>
        /// 插入图片
        /// </summary>
        public const string InsertImage = "InsertImage";
        /// <summary>
        /// 插入条码
        /// </summary>
        public const string InsertBarcode = "InsertBarcode";
        /// <summary>
        /// 插入文本输入域
        /// </summary>
        public const string InsertInputField = "InsertInputField";
        /// <summary>
        /// 插入文档字段域
        /// </summary>
        public const string InsertDocumentField = "InsertDocumentField";
        /// <summary>
        /// 插入页码信息元素
        /// </summary>
        public const string InsertPageInfo = "InsertPageInfo";
        /// <summary>
        /// 插入复选框列表
        /// </summary>
        public const string InsertCheckBoxList = "InsertCheckBoxList";
        /// <summary>
        /// 插入复选框
        /// </summary>
        public const string InsertCheckBox = "InsertCheckBox";
        /// <summary>
        /// 签名锁定文档内容
        /// </summary>
        public const string SignDocument = "SignDocument";
        /// <summary>
        /// 删除文本输入域
        /// </summary>
        public const string DeleteField = "DeleteField";
        /// <summary>
        /// 旧的删除选中的文档内容
        /// </summary>
        public const string DeleteSelectionOld = "DeleteSelectionOld";
        /// <summary>
        /// 设置插入/替换模式
        /// </summary>
        public const string InsertMode = "InsertMode";
        /// <summary>
        /// 数字列表
        /// </summary>
        public const string NumberedList = "NumberedList";
        /// <summary>
        /// 圆点列表
        /// </summary>
        public const string BulletedList = "BulletedList";
        /// <summary>
        /// 首行缩进
        /// </summary>
        public const string FirstLineIndent = "FirstLineIndent";
        /// <summary>
        /// 上标
        /// </summary>
        public const string Superscript = "Superscript";
        /// <summary>
        /// 下标
        /// </summary>
        public const string Subscript = "Subscript";
        /// <summary>
        /// 插入变量
        /// </summary>
        public const string InsertParameter = "InsertParameter";
        /// <summary>
        /// 修改元素属性
        /// </summary>
        public const string ElementProperties = "ElementProperties";
        /// <summary>
        /// 编辑元素值
        /// </summary>
        public const string EditElementValue = "EditElementValue";
        /// <summary>
        /// 段落格式
        /// </summary>
        public const string ParagraphFormat = "ParagraphFormat";
        /// <summary>
        /// 边框和背景格式
        /// </summary>
        public const string BorderBackgroundFormat = "BorderBackgroundFormat";
        /// <summary>
        /// 编辑图片附加的图形
        /// </summary>
        public const string EditImageAdditionShape = "EditImageAdditionShape";
        /// <summary>
        /// 对文档中的数据进行校验
        /// </summary>
        public const string DocumentValueValidate = "DocumentValueValidate";
        /// <summary>
        /// 编辑VBA脚本代码
        /// </summary>
        public const string EditVBAScript = "EditVBAScript";
        /// <summary>
        /// 设置输入域固定宽度
        /// </summary>
        public const string FielSpecifyWidth = "FielSpecifyWidth";
        /// <summary>
        /// 清除用户留下的痕迹
        /// </summary>
        public const string ClearUserTrace = "ClearUserTrace";
        ///////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 表格功能模块名称
        /// </summary>
        public const string ModuleTable = "ModuleTable";


        ///<summary>在上面插入表格行</summary>
        public const string Table_InsertRowUp = "Table_InsertRowUp";

        ///<summary>在下面插入表格行</summary>
        public const string Table_InsertRowDown = "Table_InsertRowDown";

        ///<summary>删除表格行</summary>
        public const string Table_DeleteRow = "Table_DeleteRow";

        ///<summary>在左边插入表格列</summary>
        public const string Table_InsertColumnLeft = "Table_InsertColumnLeft";

        ///<summary>在右边插入表格列</summary>
        public const string Table_InsertColumnRight = "Table_InsertColumnRight";

        ///<summary>删除表格列</summary>
        public const string Table_DeleteColumn = "Table_DeleteColumn";

        ///<summary>合并单元格</summary>
        public const string Table_MergeCell = "Table_MergeCell";

        public const string Table_InsertTable = "Table_InsertTable";

        ///<summary>拆分单元格</summary>
        public const string Table_SplitCell = "Table_SplitCell";
        /// <summary>
        /// 删除整个表格
        /// </summary>
        public const string Table_DeleteTable = "Table_DeleteTable";
        /// <summary>
        /// 表格单元格的内容对齐方式
        /// </summary>
        public const string Table_CellAlign = "Table_CellAlign";
        /// <summary>
        /// 标题行
        /// </summary>
        public const string Table_HeaderRow = "Table_HeaderRow";
        public const string AlignBottomCenter = "AlignBottomCenter";
        public const string AlignBottomLeft = "AlignBottomLeft";
        public const string AlignBottomRight = "AlignBottomRight";
        public const string AlignMiddleCenter = "AlignMiddleCenter";
        public const string AlignMiddleLeft = "AlignMiddleLeft";
        public const string AlignMiddleRight = "AlignMiddleRight";
        public const string AlignTopCenter = "AlignTopCenter";
        public const string AlignTopLeft = "AlignTopLeft";
        public const string AlignTopRight = "AlignTopRight";
    }
}
