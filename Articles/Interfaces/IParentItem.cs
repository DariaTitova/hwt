using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Articles.interfaces
{
    public interface IParentItem: IMenyItem
    {
        void Remove(IMenyItem child);

        void Add(IMenyItem child);

        List<IMenyItem> ToList();

        int Count();
    }
}