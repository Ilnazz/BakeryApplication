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
        #region Constructor
        static WorkspacesModel()
        {
            _workspaces.CollectionChanged += OnWorkspacesChanged;
        }
        #endregion

        #region Properties
        private static readonly ObservableCollection<WorkspaceVM> _workspaces = new ObservableCollection<WorkspaceVM>();
        public static ObservableCollection<WorkspaceVM> Workspaces { get => _workspaces; }
        #endregion

        #region Methods
        private static void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceVM workspaceVM in e.NewItems)
                    workspaceVM.Closing += () => _workspaces.Remove(workspaceVM);
        }
        #endregion
    }
}
