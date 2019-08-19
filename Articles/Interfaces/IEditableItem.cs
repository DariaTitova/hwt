namespace Articles.Interfaces
{
    interface IEditableItem:IMenyItem
    {
        string DeleteView();
        string CreateView();
        string ChangeView();
    }
}