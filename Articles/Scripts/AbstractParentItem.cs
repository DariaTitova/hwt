using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Articles.Scripts
{
    public abstract class AbstractParentItem: AbstractMenyItem
    {
        private ICollection<AbstractMenyItem> children;

        public AbstractParentItem(string Name, ICollection<AbstractMenyItem> children) :base(Name)
        {
            this.children = children;
        }

        public void Remove(AbstractMenyItem child)
        {
            children.Remove(child);
        }

        public void Add(AbstractMenyItem child)
        {
            children.Add(child);
        }

        public List<AbstractMenyItem> ToList()
        {
            return children.ToList();
        }
    }
}