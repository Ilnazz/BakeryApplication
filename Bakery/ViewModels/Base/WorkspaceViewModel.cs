using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bakery.ViewModels.Base
{
    public abstract class WorkspaceViewModel : ViewModelBase
    {
        #region Constructor
        public WorkspaceViewModel()
        {
            CloseCommand = new RelayCommand(OnClose, CanClose);
        }
        #endregion

        #region Properties
        public event EventHandler Closing;

        public ICommand CloseCommand { get; }
        #endregion

        #region Callbacks
        private void OnClose(object parameter)
        {
            Closing?.Invoke(this, EventArgs.Empty);

            Close(parameter);
        }

        protected abstract void Close(object parameter);

        protected abstract bool CanClose(object parameter);
        #endregion
    }
}
