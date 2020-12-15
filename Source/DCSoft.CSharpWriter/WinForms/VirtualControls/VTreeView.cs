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
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing ;
using DCSoft.Drawing ;
using System.Xml.Serialization ;

namespace DCSoft.WinForms.VirtualControls
{
    /// <summary>
    /// 虚拟的树状列表
    /// </summary>
    /// <remarks>编制  袁永福</remarks>
    [Serializable]
    public class VTreeView
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public VTreeView()
        {
        }

        private bool _AllowDrop = false;
        /// <summary>
        /// 是否允许拖拽
        /// </summary>
        [DefaultValue( false )]
        public bool AllowDrop 
        {
            get
            {
                return _AllowDrop;
            }
            set
            {
                _AllowDrop = value;
            }
        }

        private Color _BackColor = SystemColors.Window;
        /// <summary>
        /// 控件背景色
        /// </summary>
        [XmlIgnore]
        public Color BackColor
        {
            get { return _BackColor; }
            set { _BackColor = value; }
        }

        private Color _ForeColor = SystemColors.ControlText;
        /// <summary>
        /// 控件文本颜色
        /// </summary>
        [XmlIgnore]
        public Color ForeColor
        {
            get { return _ForeColor; }
            set { _ForeColor = value; }
        }

        private XFontValue _Font = new XFontValue();
        /// <summary>
        /// 控件字体
        /// </summary>
        public XFontValue Font
        {
            get { return _Font; }
            set { _Font = value; }
        }

        private VImageList _ImageList = null;
        /// <summary>
        /// 图片列表
        /// </summary>
        public VImageList ImageList
        {
            get { return _ImageList; }
            set { _ImageList = value; }
        }

        private bool _CheckBoxes = false;
        /// <summary>
        /// 是否显示复选框
        /// </summary>
        [DefaultValue( false )]
        public bool CheckBoxes
        {
            get { return _CheckBoxes; }
            set { _CheckBoxes = value; }
        }

        private bool _FullRowSelect = false;
        /// <summary>
        /// 是否行选择
        /// </summary>
        [DefaultValue( false )]
        public bool FullRowSelect
        {
            get { return _FullRowSelect; }
            set { _FullRowSelect = value; }
        }

        private bool _HideSelection = true ;
        [DefaultValue( true )]
        public bool HideSelection
        {
            get { return _HideSelection; }
            set { _HideSelection = value; }
        }

        private bool _ShowLines = true;
        [DefaultValue( true )]
        public bool ShowLines
        {
            get { return _ShowLines; }
            set { _ShowLines = value; }
        }

        private string _Tag = null;
        [DefaultValue( null )]
        public string Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }
        private VTreeNodeCollection _Nodes = null;
        /// <summary>
        /// 根节点列表
        /// </summary>
        [DefaultValue( null )]
        [XmlArrayItem( "Node" , typeof( VTreeNode ))]
        public VTreeNodeCollection Nodes
        {
            get { return _Nodes; }
            set { _Nodes = value; }
        }

        private bool _LabelEdit = false;
        [DefaultValue( false )]
        public bool LabelEdit
        {
            get { return _LabelEdit; }
            set { _LabelEdit = value; }
        }

        private bool _ShowPlusMinus = true;
        [DefaultValue( true )]
        public bool ShowPlusMinus
        {
            get { return _ShowPlusMinus; }
            set { _ShowPlusMinus = value; }
        }


        public void Fill(TreeView tvw)
        {
            if (tvw == null)
            {
                throw new ArgumentNullException("tvw");
            }
            tvw.AllowDrop = this.AllowDrop;
            tvw.BackColor = this.BackColor;
            tvw.ForeColor = this.ForeColor;
            if (this.Font != null)
            {
                tvw.Font = this.Font.Value;
            }
            tvw.CheckBoxes = this.CheckBoxes;
            tvw.ShowLines = this.ShowLines;
            tvw.FullRowSelect = this.FullRowSelect;
            tvw.HideSelection = this.HideSelection;
            tvw.LabelEdit = this.LabelEdit;
            tvw.ShowPlusMinus = this.ShowPlusMinus;
            if (this.ImageList != null)
            {
                tvw.ImageList = this.ImageList.CreateImageListControl();
            }
            if (this.Nodes != null && this.Nodes.Count > 0)
            {
                foreach (VTreeNode vnode in this.Nodes)
                {
                    TreeNode node = new TreeNode();
                    FillNode(node, vnode);
                    tvw.Nodes.Add(node);
                }
            }
        }

        private void FillNode(TreeNode node, VTreeNode vnode)
        {
            node.Text = vnode.Text;
            node.Tag = vnode.Tag;
            node.Name = vnode.Name;
            if (vnode.NodeFont != null)
            {
                node.NodeFont = vnode.NodeFont.Value;
            }
            node.ImageIndex = vnode.ImageIndex;
            node.ImageKey = vnode.ImageKey;
            node.SelectedImageIndex = vnode.SelectedImageIndex;
            node.SelectedImageKey = vnode.SelectedImageKey;
            if (vnode.Nodes != null && vnode.Nodes.Count > 0)
            {
                foreach (VTreeNode vnode2 in vnode.Nodes)
                {
                    TreeNode node2 = new TreeNode();
                    FillNode(node2, vnode2);
                    node.Nodes.Add(node2);
                }
            }
        }

        public void ReadFrom(TreeView tvw)
        {
            if (tvw == null)
            {
                throw new ArgumentNullException("tvw");
            }
            this.AllowDrop = tvw.AllowDrop;
            this.BackColor = tvw.BackColor;
            this.CheckBoxes = tvw.CheckBoxes;
            this.Font = new XFontValue(tvw.Font);
            this.ForeColor = tvw.ForeColor;
            this.HideSelection = tvw.HideSelection;
            if (tvw.ImageList != null)
            {
                this.ImageList = new VImageList();
                this.ImageList.ReadFrom(tvw.ImageList);
            }
            this.LabelEdit = tvw.LabelEdit;
            this.Nodes = new VTreeNodeCollection();
            ReadNodes(tvw.Nodes, this.Nodes);
            this.ShowLines = tvw.ShowLines;
            this.ShowPlusMinus = tvw.ShowPlusMinus;
            this.Tag = Convert.ToString(tvw.Tag);
        }

        private void ReadNodes(TreeNodeCollection nodes, VTreeNodeCollection vnodes)
        {
            foreach (TreeNode node in nodes)
            {
                VTreeNode vnode = new VTreeNode();
                vnode.BackColor = node.BackColor;
                vnode.Checked = node.Checked;
                vnode.ForeColor = node.ForeColor;
                vnode.ImageIndex = node.ImageIndex;
                vnode.ImageKey = node.ImageKey;
                vnode.Name = node.Name;
                if (node.NodeFont != null)
                {
                    vnode.NodeFont = new XFontValue(node.NodeFont);
                }
                if (node.Nodes != null && node.Nodes.Count > 0)
                {
                    vnode.Nodes = new VTreeNodeCollection();
                    ReadNodes(node.Nodes, vnode.Nodes);
                }
                vnode.SelectedImageIndex = node.SelectedImageIndex;
                vnode.SelectedImageKey = node.SelectedImageKey;
                vnode.Tag = Convert.ToString(node.Tag);
                vnode.Text = node.Text;
                vnode.ToolTipText = node.ToolTipText;
                vnodes.Add(vnode);
            }
        }
    }

    /// <summary>
    /// 虚拟树状列表节点集合
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    public class VTreeNodeCollection : List<VTreeNode> 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public VTreeNodeCollection()
        {
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="deeply">是否深度复制</param>
        /// <returns>复制品</returns>
        public VTreeNodeCollection Clone(bool deeply)
        {
            if (deeply)
            {
                VTreeNodeCollection list = (VTreeNodeCollection)System.Activator.CreateInstance(this.GetType());
                foreach (VTreeNode node in this)
                {
                    list.Add(node.Clone(true));
                }
                return list;
            }
            else
            {
                return (VTreeNodeCollection)base.MemberwiseClone();
            }
        }
    }

    /// <summary>
    /// 虚拟树状列表节点
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    public class VTreeNode : ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public VTreeNode()
        {
        }

        private Color _BackColor = Color.Empty;
        /// <summary>
        /// 背景文本
        /// </summary>
        [XmlIgnore]
        public Color BackColor
        {
            get { return _BackColor; }
            set { _BackColor = value; }
        }

        [Browsable( false )]
        [XmlElement()]
        [DefaultValue("Empty")]
        public string BackColorValue
        {
            get
            {
                return VUtils.ColorToString(this.BackColor);
            }
            set
            {
                this.BackColor = VUtils.StringToColor(value , Color.Empty );
            }
        }

        private bool _Checked = false;
        /// <summary>
        /// 是否被勾选
        /// </summary>
        [DefaultValue( false )]
        public bool Checked
        {
            get { return _Checked; }
            set { _Checked = value; }
        }

        private Color _ForeColor = SystemColors.ControlText;
        /// <summary>
        /// 文本颜色
        /// </summary>
        [XmlIgnore]
        public Color ForeColor
        {
            get { return _ForeColor; }
            set { _ForeColor = value; }
        }

        [Browsable( false )]
        [XmlElement]
        [DefaultValue("ControlText")]
        public string ForeColorValue
        {
            get
            {
                return VUtils.ColorToString(this.ForeColor);
            }
            set
            {
                this.ForeColor = VUtils.StringToColor(value, SystemColors.ControlText);
            }
        }

        private int _ImageIndex = 0;
        /// <summary>
        /// 图标序号
        /// </summary>
        [DefaultValue( 0 )]
        public int ImageIndex
        {
            get { return _ImageIndex; }
            set { _ImageIndex = value; }
        }

        private string _ImageKey = null;
        /// <summary>
        /// 图标编号
        /// </summary>
        [DefaultValue( null )]
        public string ImageKey
        {
            get { return _ImageKey; }
            set { _ImageKey = value; }
        }

        private string _Name = null;
        /// <summary>
        /// 名称
        /// </summary>
        [DefaultValue( null )]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Tag = null;
        /// <summary>
        /// 额外的数据
        /// </summary>
        [DefaultValue( null )]
        public string Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

        private string _Text = null;
        /// <summary>
        /// 文本值
        /// </summary>
        [DefaultValue( null )]
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        private string _ToolTipText = null;
        /// <summary>
        /// 提示文本值
        /// </summary>
        [DefaultValue( null) ]
        public string ToolTipText
        {
            get { return _ToolTipText; }
            set { _ToolTipText = value; }
        }

        private XFontValue _NodeFont = null;
        /// <summary>
        /// 节点字体
        /// </summary>
        [DefaultValue( null )]
        public XFontValue NodeFont
        {
            get { return _NodeFont; }
            set { _NodeFont = value; }
        }

        private VTreeNodeCollection _Nodes = null;
        /// <summary>
        /// 子节点列表
        /// </summary>
        [DefaultValue( null )]
        [XmlArrayItem("Node" , typeof( VTreeNode ))]
        public VTreeNodeCollection Nodes
        {
            get { return _Nodes; }
            set { _Nodes = value; }
        }

        private int _SelectedImageIndex = 0;
        /// <summary>
        /// 节点被选择时的图标序号
        /// </summary>
        [DefaultValue( 0 )]
        public int SelectedImageIndex
        {
            get { return _SelectedImageIndex; }
            set { _SelectedImageIndex = value; }
        }

        private string _SelectedImageKey = null;
        /// <summary>
        /// 节点被选择时的图标编号
        /// </summary>
        [DefaultValue( null )]
        public string SelectedImageKey
        {
            get { return _SelectedImageKey; }
            set { _SelectedImageKey = value; }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="deeply">是否深度复制</param>
        /// <returns>复制品</returns>
        public VTreeNode Clone(bool deeply)
        {
            VTreeNode node = (VTreeNode)this.MemberwiseClone();
            if (deeply)
            {
                if (this.Nodes != null)
                {
                    node.Nodes = this.Nodes.Clone(true);
                }
            }
            return node ;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
