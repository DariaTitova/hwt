using System.Web.UI.WebControls;

namespace Articles.Interfaces
{
    interface IEditableItem
    {
        View Delete();
        View Create();
        View Change();
    }
}