using Bakery.ViewModels.Base;
using System;
using System.Windows.Input;

namespace Bakery.ViewModels
{
    public class CommandVM : ViewModelBase
    {
        #region Constructor
        public CommandVM(string displayTitle, ICommand command)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));
            DisplayTitle = displayTitle;
        }
        #endregion

        #region Properties
        public ICommand Command { get; }
        #endregion
    }
}
