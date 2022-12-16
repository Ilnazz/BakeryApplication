using Bakery.Models;
using Bakery.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            NavigateCommand = new RelayCommand(Navigate);

            NavigationCommands = new List<CommandViewModel>()
            {
                new CommandViewModel("Спецификации продуктов", NavigateCommand),
            };

            Workspaces.CollectionChanged += (s, e) =>
            {
                SelectedWorkspaceIndex = Workspaces.Count != 0 ? Workspaces.Count - 1 : -1;
            };
        }
        #endregion

        #region Workspaces
        public ObservableCollection<WorkspaceViewModel> Workspaces { get => WorkspacesModel.Workspaces; }

        private int _selectedWorkspaceIndex;
        public int SelectedWorkspaceIndex
        {
            get => _selectedWorkspaceIndex;
            set => Set(ref _selectedWorkspaceIndex, value);
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
                    var prodSpecVM = new ProductSpecificationsViewModel();
                    Workspaces.Add(prodSpecVM);
                    break;
                default:
                    throw new ArgumentException();
            }
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
