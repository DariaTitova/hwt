using Articles.interfaces;
using Articles.Models;
using Articles.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public string Head()
        {
            return clause.Name;
        }

        public string Name()
        {
            return clause.Name;
        }
    }
}