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

        public string MenyText()
        {
            return clause.Name;
        }

        public static string AddView()
        {
            return "/Clauses/Create";
        }
        public static string Name()
        {
            return "cтатья";
        }

        public string ChangeView()
        {
            return "/Clauses/Edit/" + clause.Id;
        }


        public string DeleteView()
        {
            return "/Clauses/Delete/" + clause.Id;
        }

        public string ShowView()
        {
            return "/Clauses/Index?clausesId=" + clause.Id;
        }
    }
}