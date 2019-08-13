using Articles.interfaces;
using Articles.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Articles.Items
{
    public class CatalogesItems : IParentItem
    {

        public void Add(IMenyItem child)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public string Name()
        {
            throw new NotImplementedException();
        }

        public void Remove(IMenyItem child)
        {
            throw new NotImplementedException();
        }

        public List<IMenyItem> ToList()
        {
            throw new NotImplementedException();
        }
    }
}