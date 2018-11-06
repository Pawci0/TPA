using System.Collections.ObjectModel;

namespace ViewModel
{
    public interface IExpander
    {
        void Expand(ObservableCollection<TreeViewItem> children);
    }
}