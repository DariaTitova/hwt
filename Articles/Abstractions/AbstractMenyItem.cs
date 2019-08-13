using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Articles.Scripts
{
    public abstract class AbstractMenyItem
    {
        private string name;
        public AbstractMenyItem(string Name)
        {
            this.Name = Name;
        }
        public string Name { get => name; set => name = value; }
    }
}