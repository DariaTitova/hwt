using System.Web.UI.WebControls;
using Articles.Interfaces;
using Articles.Models;

namespace Articles.Items
{
    public class ClausesItems : IMenyItem , IShownItem, IEditableItem
    {
        private Clauses clause;
        public ClausesItems(Clauses clause)
        {
            this.clause = clause;
        }

        public string ChangeView()
        {
            return "/Clauses/Edit/" + clause.Id;
        }

        public string CreateView()
        {
            return "/Clauses/Create";
        }


        public string DeleteView()
        {
            return "/Clauses/Delete/" + clause.Id;
        }

        public string MenyText()
        {
            return clause.Name;
        }

        public string Name()
        {
            return "cтатья";
        }

        public string ShowView()
        {
            return "/Home/Clauses?clausesId=" + clause.Id;
        }
    }
}