using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Articles.Interfaces
{
    public interface IShownItem
    {
        string Head();
        string Body();
        string GetLink();
    }
}