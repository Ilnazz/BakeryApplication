using Bakery.Models;
using Bakery.ViewModels.Base;
using Bakery.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bakery.ViewModels
{
    public class MainWindowVM : WorkspaceVM
    {
        #region Constructor
        public MainWindowVM(int userId)
        {
            NavigateCommand = new RelayCommand(Navigate);
            UserLogOutCommand = new RelayCommand(UserLogOut);

            NavigationCommands = new List<CommandVM>()
            {
                new CommandVM("Спецификации продуктов", NavigateCommand),
            };

            Workspaces.CollectionChanged += (s, e) =>
            {
                IsThereWorkspace = Workspaces.Count != 0;
            };

            CurrentUser = _dbContext.Users.First(u => u.Id == userId);
        }
        #endregion

        #region Properties
        public ObservableCollection<WorkspaceVM> Workspaces { get => WorkspacesModel.Workspaces; }

        private bool _isThereWorkspace = false;
        public bool IsThereWorkspace {
            get => _isThereWorkspace;
            set => Set(ref _isThereWorkspace, value);
        }

        private DBEntities _dbContext = new DBEntities();

        public User CurrentUser { get; }
        #endregion

        #region Commands

        #region Navigation
        public IEnumerable<CommandVM> NavigationCommands { get; }

        public ICommand NavigateCommand { get; }

        private void Navigate(object param)
        {
            var vmTitle = param as string;

            if (IsViewAlreadyOpened(vmTitle))
                return;

            WorkspaceVM workspaceVM;
            switch (vmTitle)
            {
                case "Спецификации продуктов":
                    workspaceVM = new ProdSpecsVM();
                    break;
                default:
                    throw new ArgumentException();
            }

            Workspaces.Add(workspaceVM);
        }

        private bool IsViewAlreadyOpened(string vmTitle)
            => Workspaces.Any(ws => ws.DisplayTitle == vmTitle);
        #endregion

        #region User logging out
        public ICommand UserLogOutCommand { get; }

        private bool _isUserLoggingOut = false;
        private void UserLogOut(object param)
        {
            var result = MessageBox.Show("Выйти из системы?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;

            var userAuthWindow = new UserAuthWindow();
            userAuthWindow.Show();

            _isUserLoggingOut = true;
            base.CloseCommand.Execute(null);
        }
        #endregion

        #region Closing
        protected override bool Close()
        {
            if (_isUserLoggingOut)
                return true;

            var result = MessageBox.Show("Закрыть приложение?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                return true;
            return false;
        }

        public bool OnClose() => Close();
        #endregion

        #endregion
    }
}
