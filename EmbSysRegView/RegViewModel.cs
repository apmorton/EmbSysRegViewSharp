using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aga.Controls.Tree;

namespace EmbSysRegView
{
    public abstract class BaseItem
    {
        public RegViewModel Owner { get; set; }
        public BaseItem ParentItem { get; set; }
        public string ItemPath { get; set; }
        public XElement Element { get; set; }

        public virtual Image Icon { get; set; }

        public virtual string Name { get; set; }
        public virtual uint Value { get; set; }
        public virtual uint Reset { get; set; }
        public virtual string Access { get; set; }
        public virtual uint Address { get; set; }
        public virtual string Description { get; set; }

        public virtual bool Changed { get { return false; } }

        public abstract List<BaseItem> LoadChildren();

        public virtual string FormatBin()
        {
            return "";
        }

        public virtual string FormatHex()
        {
            return "";
        }

        public virtual string FormatReset()
        {
            return "";
        }

        public virtual string FormatAddress()
        {
            return "";
        }

        public virtual bool IsEmpty()
        {
            return false;
        }
    }

    public class GroupItem : BaseItem
    {
        public List<RegisterGroupItem> Children;

        public GroupItem(XElement group, RegViewModel owner)
        {
            Element = group;
            Owner = owner;

            Name = group.Attribute("name").Value;
            Description = group.Attribute("description").Value;
            ItemPath = Name;
        }

        public override List<BaseItem> LoadChildren()
        {
            var items = new List<BaseItem>();
            Children = new List<RegisterGroupItem>();
            foreach (var regGroup in Element.Descendants("registergroup"))
            {
                var item = new RegisterGroupItem(regGroup, this, Owner);
                Children.Add(item);
                items.Add(item);
            }
            return items;
        }

        public override bool IsEmpty()
        {
            return Children.All(item => item.IsEmpty());
        }
    }

    public class RegisterGroupItem : BaseItem
    {
        public GroupItem Parent { get; set; }
        public List<RegisterItem> Children;

        public RegisterGroupItem(XElement regGroup, GroupItem parent, RegViewModel owner)
        {
            Element = regGroup;
            ParentItem = Parent = parent;
            Owner = owner;

            Name = regGroup.Attribute("name").Value;
            Description = regGroup.Attribute("description").Value;
            ItemPath = Path.Combine(parent.ItemPath, Name);
        }

        public override List<BaseItem> LoadChildren()
        {
            var items = new List<BaseItem>();
            Children = new List<RegisterItem>();
            foreach (var reg in Element.Descendants("register"))
            {
                var item = new RegisterItem(reg, this, Owner);
                Children.Add(item);
                items.Add(item);
            }
            return items;
        }

        public override bool IsEmpty()
        {
            return Children.All(item => item.IsEmpty());
        }
    }

    public class RegisterItem : BaseItem
    {
        public RegisterGroupItem Parent { get; set; }
        public List<FieldItem> Children;

        public override Image Icon
        {
            get
            {
                if (Enabled)
                    return EmbSysRegView.Properties.Resources.selected_register;
                return EmbSysRegView.Properties.Resources.unselected_register;
            }
            set { }
        }

        private bool enabled = false;
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
                Owner.OnNodesChanged(this);
            }
        }

        public uint LastValue { get; set; }

        public override uint Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                // store last values
                LastValue = base.Value;
                foreach (var child in Children)
                {
                    child.LastValue = child.Value;
                }

                // set new value
                base.Value = value;
                Owner.OnNodesChanged(this);
            }
        }

        public override bool Changed
        {
            get
            {
                return LastValue != Value;
            }
        }

        public RegisterItem(XElement reg, RegisterGroupItem parent, RegViewModel owner)
        {
            Element = reg;
            ParentItem = Parent = parent;
            Owner = owner;

            Name = reg.Attribute("name").Value;
            Description = reg.Attribute("description").Value;

            ItemPath = Path.Combine(parent.ItemPath, Name);
            Icon = EmbSysRegView.Properties.Resources.unselected_register;

            try
            {
                var val = (reg.Attribute("resetvalue") != null) ? reg.Attribute("resetvalue").Value.Trim() : "";
                if (val.IndexOf('x') > 0)
                    val = val.Substring(val.IndexOf('x') + 1);
                Reset = Convert.ToUInt32((val.Length > 0) ? val : "0x00", 16);
                Access = reg.Attribute("access").Value;
                val = reg.Attribute("address").Value.Trim();
                if (val.IndexOf('x') > 0)
                    val = val.Substring(val.IndexOf('x') + 1);
                Address = Convert.ToUInt32(val, 16);
            }
            catch (Exception ex)
            {
                Description = "PARSE ERROR: " + ex.Message;
            }
        }

        public override List<BaseItem> LoadChildren()
        {
            var items = new List<BaseItem>();
            Children = new List<FieldItem>();
            foreach (var field in Element.Descendants("field"))
            {
                var item = new FieldItem(field, this, Owner);
                Children.Add(item);
                items.Add(item);
            }
            return items;
        }

        public override bool IsEmpty()
        {
            return !Enabled;
        }

        public override string FormatBin()
        {
            if (!Enabled)
                return "";
            return Convert.ToString(Value, 2).PadLeft(32, '0').Trim();
        }

        public override string FormatHex()
        {
            if (!Enabled)
                return "";
            return String.Format("0x{0:X8}", Value);
        }

        public override string FormatReset()
        {
            return String.Format("0x{0:X8}", Reset);
        }

        public override string FormatAddress()
        {
            return String.Format("0x{0:X8}", Address);
        }
    }

    public class FieldItem : BaseItem
    {
        public RegisterItem Parent { get; set; }

        public int BitOffset { get; set; }
        public int BitLength { get; set; }

        private readonly Dictionary<uint, string> interpretations = new Dictionary<uint, string>();

        public override string Access
        {
            get
            {
                return Parent.Access;
            }
            set
            {
                Parent.Access = value;
            }
        }

        public uint LastValue { get; set; }

        public override uint Value
        {
            get
            {
                uint mask = 0;
                for (int i = 0; i < BitLength; i++)
                    mask |= (uint)1 << i;

                return (Parent.Value >> BitOffset) & mask;
            }
            set { }
        }

        public override bool Changed
        {
            get
            {
                return LastValue != Value;
            }
        }

        public override uint Reset
        {
            get
            {
                uint mask = 0;
                for (int i = 0; i < BitLength; i++)
                    mask |= (uint)1 << i;

                return (Parent.Reset >> BitOffset) & mask;
            }
            set { }
        }

        public override Image Icon
        {
            get
            {
                if (Parent.Enabled)
                    return EmbSysRegView.Properties.Resources.selected_field;
                return EmbSysRegView.Properties.Resources.unselected_field;
            }
            set { }
        }

        public FieldItem(XElement reg, RegisterItem parent, RegViewModel owner)
        {
            Element = reg;
            ParentItem = Parent = parent;
            Owner = owner;

            Name = reg.Attribute("name").Value;
            BitOffset = int.Parse(reg.Attribute("bitoffset").Value);
            BitLength = int.Parse(reg.Attribute("bitlength").Value);
            var attr = reg.Attribute("description");
            var elem = reg.Element("description");

            if (attr != null)
                Description = attr.Value;
            else if (elem != null)
                Description = elem.Value;
            else
                Description = "";

            if (BitLength > 1)
                Name = String.Format("{0} (bits {1}-{2})", Name, BitOffset, BitOffset + BitLength - 1);
            else
                Name = String.Format("{0} (bit {1})", Name, BitOffset);

            ItemPath = Path.Combine(parent.ItemPath, Name);
            Icon = EmbSysRegView.Properties.Resources.unselected_field;

            foreach (var interp in Element.Descendants("interpretation"))
            {
                var key = uint.Parse(interp.Attribute("key").Value);
                var text = interp.Attribute("text").Value.Trim();
                interpretations.Add(key, text);
            }
        }

        public override List<BaseItem> LoadChildren()
        {
            return new List<BaseItem>();
        }

        public override string FormatBin()
        {
            if (!Parent.Enabled)
                return "";
            return Convert.ToString(Value, 2).PadLeft(BitLength, '0').Trim();
        }

        public override string FormatHex()
        {
            if (!Parent.Enabled)
                return "";
            int byteLength = (int)Math.Ceiling(BitLength / 8.0) * 2;
            return String.Format("0x{0:X" + byteLength.ToString() + "}", Value);
        }

        public override string FormatReset()
        {
            int byteLength = (int)Math.Ceiling(BitLength / 8.0) * 2;
            return String.Format("0x{0:X" + byteLength.ToString() + "}", Reset);
        }

        public override string FormatAddress()
        {
            uint addr = Parent.Address;
            addr += (uint)BitOffset / 8;
            var bits = BitOffset % 8;

            return String.Format("0x{0:X8}:{1}", addr, bits);
        }

        public string Interpretation()
        {
            uint val = Value;
            if (interpretations.ContainsKey(val))
                return interpretations[val];
            return Description;
        }
    }

    public class RegViewModel : ITreeModel
    {
        private readonly Dictionary<string, List<BaseItem>> cache = new Dictionary<string, List<BaseItem>>();
        private readonly XDocument document;
        private bool showDisabled = true;

        public bool ShowDisabled
        {
            get
            {
                return showDisabled;
            }
            set
            {
                showDisabled = value;
                OnStructureChanged();
            }
        }

        public RegViewModel(XDocument document)
        {
            this.document = document;
        }

        public IEnumerable GetChildren(TreePath treePath)
        {
            List<BaseItem> items = null;

            if (treePath.IsEmpty())
            {
                if (cache.ContainsKey("ROOT"))
                {
                    items = cache["ROOT"];
                }
                else
                {
                    items = new List<BaseItem>();
                    cache.Add("ROOT", items);
                    foreach (var group in document.Root.Descendants("group"))
                    {
                        var item = new GroupItem(group, this);
                        items.Add(item);
                    }
                }
            }
            else
            {
                var parent = treePath.LastNode as BaseItem;
                if (parent != null)
                {
                    if (cache.ContainsKey(parent.ItemPath))
                    {
                        items = cache[parent.ItemPath];
                    }
                    else
                    {
                        items = parent.LoadChildren();
                        cache.Add(parent.ItemPath, items);
                    }
                }
            }

            if (!showDisabled)
            {
                return items.Where(item => !item.IsEmpty());
            }

            return items;
        }

        public bool IsLeaf(TreePath treePath)
        {
            return treePath.LastNode is FieldItem;
        }

        private TreePath GetPath(BaseItem item)
        {
            if (item == null)
                return TreePath.Empty;
            else
            {
                Stack<object> stack = new Stack<object>();
                while (item != null)
                {
                    stack.Push(item);
                    item = item.ParentItem;
                }
                return new TreePath(stack.ToArray());
            }
        }

        public event EventHandler<TreeModelEventArgs> NodesChanged;
        public void OnNodesChanged(BaseItem item)
        {
            if (NodesChanged != null)
            {
                var path = GetPath(item.ParentItem);
                NodesChanged(this, new TreeModelEventArgs(path, new object[] { item }));
            }
        }
        public event EventHandler<TreeModelEventArgs> NodesInserted;
        public event EventHandler<TreeModelEventArgs> NodesRemoved;
        public event EventHandler<TreePathEventArgs> StructureChanged;
        public void OnStructureChanged()
        {
            if (StructureChanged != null)
                StructureChanged(this, new TreePathEventArgs());
        }
    }
}
