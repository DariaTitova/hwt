using Articles.interfaces;
using Articles.Models;
using Articles.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Articles.Items
{
    public class CatalogesItems : IParentItem
    {
        private Cataloges cataloge;
        private ICollection<IMenyItem> children;
        public CatalogesItems(Cataloges cataloge)
        {
            this.cataloge = cataloge;
            children = cataloge.Children.Select(ch=>new ClausesItems(ch)as IMenyItem).ToList();
        }
        public void Add(IMenyItem child)
        {
            children.Add(child);
        }

        public int Count()
        {
            return children.Count();
        }

        public string Name()
        {
           return cataloge.Name;
        }

        public void Remove(IMenyItem child)
        {
            children.Remove(child);
        }

        public List<IMenyItem> ToList()
        {
            return children.ToList();
        }
    }
}