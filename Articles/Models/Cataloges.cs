using NHibernate.Mapping;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [DisplayName("Название")]
        [Required(ErrorMessage = "Название должно быть установлено")]
        [StringLength(255, ErrorMessage = "Длина строки должна быть до 255 символов")]
        public virtual string Name { get; set; }

        [Bag(0, Name = "Clauses", Inverse = true)]
        [NHibernate.Mapping.Attributes.Key(1, Column = "IdParent")]
        [OneToMany(2, ClassType = typeof(Clauses))]
        private IList<Clauses> _clauses;
        public virtual IList<Clauses> Clauses
        {
            get
            {
                return _clauses ?? (_clauses = new List<Clauses>());
            }
            set { _clauses = value; }
        }

        //Родительский элемент
        [ManyToOne(Name = "Parent", Column = "IdParent",
         ClassType = typeof(Cataloges), Cascade = "delete")]
        public virtual Cataloges Parent { get; set; }

        //Дочерние элементы
        [Bag(0, Name = "Children", Inverse = true)]
        [NHibernate.Mapping.Attributes.Key(1, Column = "IdParent")]
        [OneToMany(2, ClassType = typeof(Cataloges))]
        private IList<Cataloges> _children;
        public virtual IList<Cataloges> Children
        {
            get
            {
                return _children ?? (_children = new List<Cataloges>());
            }
            set { _children = value; }
        }
    }
}