using Articles.Interfaces;
using Articles.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Articles.Items
{
    public class CatalogesItems : IParentItem, IEditableItem
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


        public static string AddView()
        {
            return "/Cataloges/Create/";
        }
        public static string Name()
        {
            return "каталог";
        }



        public void Add(IMenyItem child)
        {
            children.Add(child);
        }

        public string ChangeView()
        {
            return "/Cataloges/Edit/" + cataloge.Id;
        }

        public int Count()
        {
            return children.Count();
        }

        public string DeleteView()
        {
            return "/Cataloges/Delete/" + cataloge.Id;
        }

        public string MenyText()
        {
             return cataloge.Name;
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