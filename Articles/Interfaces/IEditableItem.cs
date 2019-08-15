namespace Articles.Interfaces
{
    interface IEditableItem
    {
        string DeleteView();
        string CreateView();
        string ChangeView();
    }
}