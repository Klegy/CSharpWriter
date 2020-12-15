/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Drawing;

namespace DCSoft.WinForms.Design
{
    /// <summary>
    /// 编辑字体大小的编辑器
    /// </summary>
    public class FontSizeEditor : CustomDrawValueListBoxEditor
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public FontSizeEditor()
        {
            this.Provider = new FontSizeEditProvider();
        }
    }

    public class FontSizeEditProvider : ListControlEditProvider
    {
        public override System.Drawing.Size BoxSize
        {
            get
            {
                return System.Drawing.Size.Empty;
            }
        }

        public override System.Collections.IEnumerable GetItems()
        {
            return FontSizeInfo.StandSizes;
        }
        public override object GetItemValue(object item)
        {
            FontSizeInfo size = (FontSizeInfo)item;
            return size.Size;
        }
    }


    /// <summary>
    /// 字体大小信息
    /// </summary>
    [Serializable()]
    public class FontSizeInfo
    {
        /// <summary>
        /// 根据字体大小获得字体大小的名称
        /// </summary>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public static string GetStandSizeName(float size)
        {
            foreach (FontSizeInfo item in StandSizes)
            {
                if (Math.Abs(size - item.Size) < 0.01)
                {
                    return item.Name;
                }
            }
            return size.ToString();
        }

        public static float GetFontSize(string fontSizeName , float defaultFontSize )
        {
            foreach (FontSizeInfo item in StandSizes)
            {
                if (string.Compare(fontSizeName, item.Name, true) == 0)
                {
                    return item.Size;
                }
            }
            float size = 0;
            if (float.TryParse(fontSizeName, out size))
            {
                return size;
            }
            else
            {
                return defaultFontSize ;
            }
        }

        private static FontSizeInfo[] myStandSizes = null;
        /// <summary>
        /// 获得标准字体大小列表
        /// </summary>
        public static FontSizeInfo[] StandSizes
        {
            get
            {
                if (myStandSizes == null)
                {
                    System.Collections.ArrayList list = new System.Collections.ArrayList();
                    if (System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName
                        == "zh")
                    {
                        // 若为简体中文或者繁体中文系统则添加字号
                        list.Add(new FontSizeInfo("初号", 42));
                        list.Add(new FontSizeInfo("小初", 36));
                        list.Add(new FontSizeInfo("一号", 26.25f));
                        list.Add(new FontSizeInfo("小一", 24));
                        list.Add(new FontSizeInfo("二号", 21.75f));
                        list.Add(new FontSizeInfo("小二", 18));
                        list.Add(new FontSizeInfo("三号", 15.75f));
                        list.Add(new FontSizeInfo("小三", 15));
                        list.Add(new FontSizeInfo("四号", 14.25f));
                        list.Add(new FontSizeInfo("小四", 12));
                        list.Add(new FontSizeInfo("五号", 10.5f));
                        list.Add(new FontSizeInfo("小五", 9));
                        list.Add(new FontSizeInfo("六号", 7.5f));
                        list.Add(new FontSizeInfo("小六", 6.75f));
                        list.Add(new FontSizeInfo("七号", 5.25f));
                        list.Add(new FontSizeInfo("八号", 5.25f));
                    }
                    list.Add(new FontSizeInfo(8));
                    list.Add(new FontSizeInfo(9));
                    list.Add(new FontSizeInfo(10));
                    list.Add(new FontSizeInfo(11));
                    list.Add(new FontSizeInfo(12));
                    list.Add(new FontSizeInfo(14));
                    list.Add(new FontSizeInfo(16));
                    list.Add(new FontSizeInfo(18));
                    list.Add(new FontSizeInfo(20));
                    list.Add(new FontSizeInfo(22));
                    list.Add(new FontSizeInfo(24));
                    list.Add(new FontSizeInfo(26));
                    list.Add(new FontSizeInfo(28));
                    list.Add(new FontSizeInfo(36));
                    list.Add(new FontSizeInfo(48));
                    list.Add(new FontSizeInfo(72));
                    
                    myStandSizes = (FontSizeInfo[])list.ToArray(typeof(FontSizeInfo));
                }
                return myStandSizes;
            }
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="name">字体名称</param>
        /// <param name="size">字体大小</param>
        public FontSizeInfo(string name, float size)
        {
            strName = name;
            fSize = size;
        }
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="size">字体大小</param>
        public FontSizeInfo(float size)
        {
            strName = size.ToString();
            fSize = size;
        }

        private string strName = null;
        /// <summary>
        /// 字体名称
        /// </summary>
        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }

        private float fSize = 9f;
        /// <summary>
        /// 字体大小
        /// </summary>
        public float Size
        {
            get { return fSize; }
            set { fSize = value; }
        }
        public override string ToString()
        {
            return strName;
        }
    }//public class FontSize
}
