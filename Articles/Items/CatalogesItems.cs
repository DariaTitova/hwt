using Articles.Interfaces;
using Articles.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Articles.Items
{
    public class CatalogesItems : IParentItem
    {
        private Cataloges cataloge;
        private ICollection<IMenyItem> children;
        public CatalogesItems(Cataloges cataloge)
        {
            this.cataloge = cataloge;
            var childrenCataloges = cataloge.Children.Select(ch => new CatalogesItems(ch) as IMenyItem).ToList();
            var childrenClauses = cataloge.Clauses.Select(ch => new ClausesItems(ch) as IMenyItem).ToList();
           

            children = childrenClauses.Concat(childrenCataloges).ToList(); 
        }
        public void Add(IMenyItem child)
        {
            children.Add(child);
        }

        public int Count()
        {
            return children.Count();
        }

        public string MenyText()
        {
             return cataloge.Name;
        }

        public string Name()
        {
           return "каталог";
        }

        public void Remove(IMenyItem child)
        {
            children.Remove(child);
        }

        public TagBuilder TagInMeny()
        {
            throw new System.NotImplementedException();
        }

        public List<IMenyItem> ToList()
        {
            return children.ToList();
        }
    }
}