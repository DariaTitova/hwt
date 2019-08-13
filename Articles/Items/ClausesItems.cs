using Articles.interfaces;
using Articles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Articles.Items
{
    public class ClausesItems : IMenyItem
    {
        private Clauses clause;
        public ClausesItems(Clauses clause)
        {
            this.clause = clause;
        }
        public string Name()
        {
            return clause.Name;
        }
    }
}