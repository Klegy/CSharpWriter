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
using System.Drawing ;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 高亮度显示区域
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    public class HighlightInfo
    {
        /// <summary>
        /// 比较两个对象设置是否一致
        /// </summary>
        /// <param name="info1">信息1</param>
        /// <param name="info2">信息2</param>
        /// <returns>设置是否一致</returns>
        public static bool Compare(HighlightInfo info1, HighlightInfo info2)
        {
            if (info1 == info2)
            {
                return true;
            }
            else
            {
                if (info1 == null || info2 == null )
                {
                    // 此时两个对象必然一个为空另外一个不为空，因此不可能相等
                    return false;
                }

                if (DomRange.Compare(info1.Range, info2.Range) == false
                    || info1._ActiveStyle != info2._ActiveStyle
                    || info1._BackColor != info2._BackColor
                    || info1._Color != info2._Color)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }


        /// <summary>
        /// 初始化对象
        /// </summary>
        public HighlightInfo()
        {

        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="range">区域</param>
        public HighlightInfo(DomRange range)
        {
            if (range == null)
            {
                throw new ArgumentNullException("range");
            }
            _Range = range;
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="range">区域</param>
        /// <param name="backColor">背景色</param>
        /// <param name="color">前景色</param>
        public HighlightInfo(DomRange range, Color backColor, Color color)
        {
            if (range == null)
            {
                throw new ArgumentNullException("range");
            }
            _Range = range;
            _BackColor = backColor;
            _Color = color;
        }

        private DomElement _OwnerElement = null;
        /// <summary>
        /// 该高亮度显示区域相关的文档元素对象
        /// </summary>
        public DomElement OwnerElement
        {
            get
            {
                return _OwnerElement; 
            }
            set
            {
                _OwnerElement = value; 
            }
        }

        private DomRange _Range = null;
        /// <summary>
        /// 高亮度区域
        /// </summary>
        public DomRange Range
        {
            get
            {
                return _Range; 
            }
            set
            {
                _Range = value; 
            }
        }

        private Color _BackColor = SystemColors.Highlight;
        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackColor
        {
            get
            {
                return _BackColor; 
            }
            set
            {
                _BackColor = value; 
            }
        }

        private Color _Color = SystemColors.HighlightText;
        /// <summary>
        /// 文本颜色
        /// </summary>
        public Color Color
        {
            get
            {
                return _Color; 
            }
            set
            {
                _Color = value; 
            }
        }

        private HighlightActiveStyle _ActiveStyle = HighlightActiveStyle.Hover;
        /// <summary>
        /// 激活模式
        /// </summary>
        public HighlightActiveStyle ActiveStyle
        {
            get
            {
                return _ActiveStyle; 
            }
            set
            {
                _ActiveStyle = value; 
            }
        }

        /// <summary>
        /// 判断指定的元素是否处于高亮度显示区域中
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool Contains(DomElement element)
        {
            return _Range.Contains(element);
        }
        public override string ToString()
        {
            return this.Range.ToString() + " BC:" + this.BackColor.ToString();
        }
    }
        

    /// <summary>
    /// 高亮度激活模式
    /// </summary>
    public enum HighlightActiveStyle
    {
        /// <summary>
        /// 鼠标悬停时才激活
        /// </summary>
        Hover ,
        /// <summary>
        /// 静态的，一直处于激活状态,但不能打印
        /// </summary>
        Static,
        /// <summary>
        /// 永久的，一直处于激活状态，而且能打印
        /// </summary>
        AllTime
    }

    /// <summary>
    /// 高亮度显示区域列表
    /// </summary>
    public class HighlightInfoList : List<HighlightInfo>
    {
        /// <summary>
        /// 查找包含自定元素的
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public HighlightInfo this[DomElement element]
        {
            get
            {
                for (int iCount = this.Count - 1; iCount >= 0; iCount--)
                {
                    if (this[iCount].Contains(element))
                    {
                        return this[iCount];
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 判断是否存在指定元素的高亮度显示信息
        /// </summary>
        /// <param name="element">文档元素对象</param>
        /// <returns>是否包含指定的元素相关的信息</returns>
        public bool ContainsOwnerElement(DomElement element)
        {
            foreach (HighlightInfo info in this)
            {
                if (info.OwnerElement == element)
                {
                    return true;
                }
            }
            return false;
        }

        internal void SortInfo()
        {
            base.Sort(new HighlighInfoComparer());
        }

        /// <summary>
        /// 两个高亮度显示区域位置的比较器
        /// </summary>
        private class HighlighInfoComparer : IComparer<HighlightInfo >
        {
            public int Compare(HighlightInfo x, HighlightInfo y)
            {
                DomElement element1 = x.OwnerElement;
                DomElement element2 = y.OwnerElement;
                if (element1 == element2)
                {
                    return 0;
                }

                DomElementList parents1 = WriterUtils.GetParentList(element1);
                parents1.Insert(0, element1);
                parents1.Reverse();
                DomElementList parents2 = WriterUtils.GetParentList(element2);
                parents2.Insert(0, element2);
                parents2.Reverse();
                int minLength = Math.Min(parents1.Count, parents2.Count) ;
                for (int iCount = 1; iCount < minLength ; iCount++)
                {
                    int result = parents1[iCount].ElementIndex - parents2[iCount].ElementIndex;
                    if (result != 0)
                    {
                        return result;
                    }
                }
                return parents1.Count - parents2.Count;
            }
        }
    }
}
