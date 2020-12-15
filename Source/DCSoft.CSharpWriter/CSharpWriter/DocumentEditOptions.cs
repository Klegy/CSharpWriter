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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Xml.Serialization;

namespace DCSoft.CSharpWriter
{
    /// <summary>
    /// 编辑器选项控制
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable()]
    [TypeConverter(typeof(CommonTypeConverter))]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class DocumentEditOptions : System.ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DocumentEditOptions()
        {
        }

        private bool _KeepTableWidthWhenInsertDeleteColumn = true;
        /// <summary>
        /// 在插入和删除表格列时是否保持表格的宽度不变。默认true。
        /// </summary>
        [DefaultValue(true)]
        public bool KeepTableWidthWhenInsertDeleteColumn
        {
            get
            {
                return _KeepTableWidthWhenInsertDeleteColumn;
            }
            set
            {
                _KeepTableWidthWhenInsertDeleteColumn = value;
            }
        }

        private bool _FixWidthWhenInsertImage = true;
        /// <summary>
        /// 在插入图片时为容器元素修正图片的宽度，使得图片元素的宽度不会超过容器客户区宽度。
        /// 默认为true。
        /// </summary>
        [DefaultValue(true)]
        public bool FixWidthWhenInsertImage
        {
            get
            {
                return _FixWidthWhenInsertImage;
            }
            set
            {
                _FixWidthWhenInsertImage = value;
            }
        }

        private bool _FixWidthWhenInsertTable = true;
        /// <summary>
        /// 在插入表格时为容器元素修正表格的宽度，使得表格元素的宽度不会超过容器客户区宽度，默认true。
        /// </summary>
        [DefaultValue(true)]
        public bool FixWidthWhenInsertTable
        {
            get
            {
                return _FixWidthWhenInsertTable;
            }
            set
            {
                _FixWidthWhenInsertTable = value;
            }
        }

        private bool _TabKeyToFirstLineIndent = true;
        /// <summary>
        /// 是否使用Tab键来设置段落首行缩进，默认为true。
        /// </summary>
        [DefaultValue(true)]
        public bool TabKeyToFirstLineIndent
        {
            get
            {
                return _TabKeyToFirstLineIndent;
            }
            set
            {
                _TabKeyToFirstLineIndent = value;
            }
        }

        private bool _TabKeyToInsertTableRow = true;
        /// <summary>
        /// 是否允许使用Tab键来新增表格行，默认为true。
        /// </summary>
        [DefaultValue(true)]
        public bool TabKeyToInsertTableRow
        {
            get
            {
                return _TabKeyToInsertTableRow;
            }
            set
            {
                _TabKeyToInsertTableRow = value;
            }
        }

        private bool _AutoEditElementValue = false;
        /// <summary>
        /// 自动进行文档元素值编辑操作，若为true,则插入点进入文本输入域时就会自动激活元素数值编辑器。默认为false。
        /// </summary>
        [DefaultValue(false)]
        public bool AutoEditElementValue
        {
            get
            {
                return _AutoEditElementValue;
            }
            set
            {
                _AutoEditElementValue = value;
            }
        }

        private DocumentValueValidateMode _ValueValidateMode = DocumentValueValidateMode.LostFocus;
        /// <summary>
        /// 文档数据校验模式，默认为LostFocus。
        /// </summary>
        [DefaultValue(DocumentValueValidateMode.LostFocus)]
        public DocumentValueValidateMode ValueValidateMode
        {
            get
            {
                return _ValueValidateMode;
            }
            set
            {
                _ValueValidateMode = value;
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            DocumentEditOptions opt = (DocumentEditOptions)this.MemberwiseClone();
            return opt;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public DocumentEditOptions Clone()
        {
            return (DocumentEditOptions)((ICloneable)this).Clone();
        }
    }

    /// <summary>
    /// DocumentViewOptions类型配套的类型转换器
    /// </summary>
    public class DocumentEditOptionsTypeConverter : System.ComponentModel.TypeConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override PropertyDescriptorCollection GetProperties(
            ITypeDescriptorContext context,
            object value,
            Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(value, attributes);
        }
    }

    /// <summary>
    /// 文档数据校验模式
    /// </summary>
    public enum DocumentValueValidateMode
    {
        /// <summary>
        /// 禁止数据校验
        /// </summary>
        None ,
        /// <summary>
        /// 实时的数据校验
        /// </summary>
        Dynamic ,
        /// <summary>
        /// 当文本输入域失去输入焦点，也就是说光标离开输入域时才进行数据校验。
        /// </summary>
        LostFocus ,
        /// <summary>
        /// 编辑器不自动进行数据校验，由应用程序编程调用来进行数据校验。
        /// </summary>
        Program 
    }
}
