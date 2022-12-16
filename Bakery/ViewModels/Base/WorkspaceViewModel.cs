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
        private void OnClose()
        {
            Closing?.Invoke(this, EventArgs.Empty);

            Close();
        }

        protected virtual void Close() { }
        protected virtual void Close(object parameter) { }

        protected virtual bool CanClose(object parameter) => true;
        #endregion
    }
}
