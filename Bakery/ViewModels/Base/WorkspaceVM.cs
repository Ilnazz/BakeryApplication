using Bakery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bakery.ViewModels.Base
{
    public abstract class WorkspaceVM : ViewModelBase
    {
        #region Constructor
        public WorkspaceVM()
        {
            CloseCommand = new RelayCommand(OnClose);
        }
        #endregion

        #region Properties

        public event Action Closing;

        public ICommand CloseCommand { get; }
        #endregion

        #region Callbacks

        private void OnClose(object param)
        {
            if (Close() == false)
                return;

            Closing?.Invoke();
        }

        protected virtual bool Close() => true;
        #endregion

        protected void DiscardChanges(DBEntities dbContext)
        {
            dbContext.ChangeTracker
                .Entries().Where(e => e.State != EntityState.Unchanged).ToList().ForEach(e =>
                {
                    switch (e.State)
                    {
                        case EntityState.Modified:
                            e.CurrentValues.SetValues(e.OriginalValues);
                            e.State = EntityState.Unchanged;
                            break;
                        case EntityState.Added:
                            e.State = EntityState.Detached;
                            break;
                        case EntityState.Deleted:
                            e.State = EntityState.Unchanged;
                            break;
                    }
                });
        }
    }
}
