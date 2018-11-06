using System.Collections.ObjectModel;

namespace ViewModel
{
    internal interface IExpander
    {
        void Expand(ObservableCollection<TreeViewItem> children);
    }
}