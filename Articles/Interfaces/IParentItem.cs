using System.Collections.Generic;

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