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

        public string Body()
        {
            return clause.Text;
        }

        public string GetLink()
        {
            return "Clauses?id=" +clause.Id;
        }

        public string Head()
        {
            return clause.Name;
        }

        public string HtmlTag()
        {
            return "a";
        }

        public string MenyText()
        {
            return clause.Name;
        }

        public string Name()
        {
            return "cтатья";
        }
    }
}