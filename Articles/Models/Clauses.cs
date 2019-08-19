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
    public class Clauses
    {
        [Id(0, Name = "Id")]
        [Generator(1, Class = "native")]
        public virtual int Id { get; set; }


        [Required(ErrorMessage = "Заголовок должен быть установлен")]
        [StringLength(255, ErrorMessage = "Длина строки должна быть до 255 символов")]
        [DisplayName("Заголовок")]
        [Property]
        public virtual string Name { get; set; }

        [Property]
        [Required(ErrorMessage = "Текст должен быть установлен")]
        [StringLength(1500, ErrorMessage = "Длина строки должна быть до 1500 символов")]
        [DisplayName("Текст")]
        [DataType(DataType.MultilineText)]
        public virtual string Text { get; set; }

        [ManyToOne(Name = "Cataloges", Column = "IdParent",
        ClassType = typeof(Cataloges), Cascade = "save-update")]
        public virtual Cataloges Cataloges { get; set; }

    }
}