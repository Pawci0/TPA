using System.Collections.ObjectModel;

namespace ViewModel
{
    public interface IExpandable
    {
        void Expand(ObservableCollection<TreeViewItem> children);
    }
}
