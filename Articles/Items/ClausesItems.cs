using System.Web.UI.WebControls;
using Articles.Interfaces;
using Articles.Models;

namespace Articles.Items
{
    public class ClausesItems : IMenyItem , IShownItem
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

        public string Name()
        {
            return "cтатья";
        }

 
        public string View()
        {
 
            return "/Home/Clauses?clausesId=" + clause.Id;
        }
    }
}