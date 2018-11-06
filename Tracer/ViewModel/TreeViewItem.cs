using System;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class TreeViewItem
    {
        public string Name { get; set; }
        public ObservableCollection<TreeViewItem> Children { get; set; }

        #region Fields
        public IExpander m_expander;
        private bool m_WasBuilt;
        private bool m_IsExpanded;
        #endregion

        public TreeViewItem()
        {
            Children = new ObservableCollection<TreeViewItem>() { null };
            this.m_WasBuilt = false;
        }

        public bool IsExpanded
        {
            get { return m_IsExpanded; }
            set
            {
                m_IsExpanded = value;
                if (m_WasBuilt)
                    return;
                Children.Clear();
                BuildMyself();
                m_WasBuilt = true;
            }
        }

        private void BuildMyself()
        {
            m_expander.Expand(Children);
        }

    }
}

