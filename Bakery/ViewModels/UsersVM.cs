using Bakery.Models;
using Bakery.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Bakery.ViewModels
{
    public class UsersVM : WorkspaceVM
    {
        #region Constructor
        public UsersVM()
        {
            DisplayTitle = "Пользователи";

            EditCommand = new RelayCommand(Edit);
            AddCommand = new RelayCommand(Add);
            RefreshCommand = new RelayCommand(Refresh);

            _dbContext.Users.Load();
            Users = _dbContext.Users.Local;
        }
        #endregion
        
        #region Properties
        private DBEntities _dbContext = new DBEntities();

        private IEnumerable<User> _users;
        public IEnumerable<User> Users
        {
            get => _users;
            set => Set(ref _users, value);
        }
        #endregion

        #region Commands

        #region Editing
        public ICommand EditCommand { get; }

        private void Edit(object param)
        {
            var user = param as User;

            if (IsUserAlreadyEditing(user))
            {
                MessageBox.Show("Уже редактируется", "Сообщение", MessageBoxButton.OK);
                return;
            }

            var userVM = new UserAddEditVM(user.Id);
            WorkspacesModel.Workspaces.Add(userVM);
        }

        private bool IsUserAlreadyEditing(User user)
            => WorkspacesModel.Workspaces.Any(viewModel =>
            {
                if (viewModel is UserAddEditVM userVM == true
                    && userVM.EditingUser.Id == user.Id)
                    return true;
                return false;
            });

        #endregion

        #region Adding
        public ICommand AddCommand { get; }

        private void Add(object param)
        {
            var userAddEditVM = new UserAddEditVM();

            WorkspacesModel.Workspaces.Add(userAddEditVM);
        }
        #endregion

        #region Refreshing
        public ICommand RefreshCommand { get; }

        private void Refresh(object param)
        {
            _dbContext.Dispose();
            _dbContext = new DBEntities();
            _dbContext.Users.Load();
            Users = _dbContext.Users.Local;
        }
        #endregion

        #region Closing

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _dbContext.Dispose();
        }
        #endregion

        #endregion
    }
}
