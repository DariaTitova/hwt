using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Articles.Models
{
    [Class]
    public class Clauses
    {
        [Id(0, Name = "Id")]
        [Generator(1, Class = "native")]
        public virtual int Id { get; set; }
        [Property]
        public virtual string Name { get; set; }
        [Property]
        public virtual string Text { get; set; }

        [ManyToOne(Name = "Cataloges", Column = "IdParent",
        ClassType = typeof(Cataloges), Cascade = "save-update")]
        public virtual Cataloges Cataloges { get; set; }

    }
}