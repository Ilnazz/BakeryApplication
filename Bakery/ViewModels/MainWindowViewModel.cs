using Bakery.ViewModels.Base;
using Bakery.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Bakery.ViewModels
{
    public class MainWindowViewModel : WorkspaceViewModel
    {
        #region Constructor
        public MainWindowViewModel()
        {
            _workspaces.CollectionChanged += OnWorkspacesChanged;

            NavigateCommand = new RelayCommand(Navigate, CanNavigate);

            NavigationCommands = new List<CommandViewModel>()
            {
                new CommandViewModel("Спецификации продуктов", NavigateCommand),
            };
        }
        #endregion

        #region Workspaces
        private readonly ObservableCollection<WorkspaceViewModel> _workspaces = new ObservableCollection<WorkspaceViewModel>();
        public ObservableCollection<WorkspaceViewModel> Workspaces { get => _workspaces; }

        private int _selectedWorkspaceIndex = -1;
        public int SelectedWorkspaceIndex
        {
            get => _selectedWorkspaceIndex;
            set => Set(ref _selectedWorkspaceIndex, value);
        }

        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.Closing += OnWorkspaceClosing;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.Closing -= OnWorkspaceClosing;

            SelectedWorkspaceIndex = _workspaces.Count > 0 ? _workspaces.Count : -1;
        }

        private void OnWorkspaceClosing(object sender, EventArgs e)
        {
            _workspaces.Remove(sender as WorkspaceViewModel);
        }
        #endregion


        #region Commands

        #region Navigation
        public IEnumerable<CommandViewModel> NavigationCommands { get; }

        public ICommand NavigateCommand { get; }

        private void Navigate(object parameter)
        {
            var viewModelTitle = parameter as string;
            switch (viewModelTitle)
            {
                case "Спецификации продуктов":
                    _workspaces.Add(new ProductSpecificationsViewModel());
                    break;
                default:
                    throw new ArgumentException();
            }
            
        }

        private bool CanNavigate(object parameter)
        {
            return true;
        }
        #endregion

        #region Closing
        protected override void Close(object parameter)
        {

        }

        protected override bool CanClose(object parameter)
        {
            var close = MessageBox.Show("Закрыть приложение?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (close == MessageBoxResult.Yes)
                return true;
            return false;
        }
        #endregion

        #endregion
    }
}
