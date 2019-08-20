using System.Collections.Generic;

namespace Articles.Interfaces
{
    public interface IParentItem: IMenyItem
    {
        int Id();
        void Remove(IMenyItem child);

        void Add(IMenyItem child);

        List<IMenyItem> ToList();

        int Count();
    }
}