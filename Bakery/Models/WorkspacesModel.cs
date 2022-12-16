using Bakery.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Models
{
    public static class WorkspacesModel
    {
        static WorkspacesModel()
        {
            _workspaces.CollectionChanged += OnWorkspacesChanged;
        }
        private static readonly ObservableCollection<WorkspaceViewModel> _workspaces = new ObservableCollection<WorkspaceViewModel>();
        public static ObservableCollection<WorkspaceViewModel> Workspaces { get => _workspaces; }

        private static void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.Closing += OnWorkspaceClosing;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.Closing -= OnWorkspaceClosing;
        }

        private static void OnWorkspaceClosing(object sender, EventArgs e)
        {
            _workspaces.Remove(sender as WorkspaceViewModel);
        }
    }
}
