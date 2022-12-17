using System;
using System.Collections.Generic;
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
    }
}
