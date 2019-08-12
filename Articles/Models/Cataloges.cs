using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Articles.Models
{
    [Class]
    public class Cataloges
    {
        [Id(0, Name = "Id")]
        [Generator(1, Class = "native")]
        public virtual int Id { get; set; }
        [Property]
        public virtual string Name { get; set; }
        [Property]
        public virtual int? IdParent { get; set; }
    }
}