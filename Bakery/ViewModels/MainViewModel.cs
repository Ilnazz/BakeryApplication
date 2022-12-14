using Bakery.Properties;
using Bakery.ViewModels.Base;
using Bakery.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bakery.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Constructor
        public MainViewModel()
        {
            NavigateToOtherViewModelCommand = new RelayCommand(NavigateToOtherViewModel, CanNavigateToOtherViewModel);

            _titleViewPairs = new Dictionary<string, object>()
            {
                { "Ассортимент продукции", new ProductSpecificationsViewModel() },
            };
        }
        #endregion

        private Dictionary<string, object> _titleViewPairs;

        private object _currentViewModel;
        public object CurrentViewModel
        {
            get => _currentViewModel;
            set => Set(ref _currentViewModel, value);
        }

        public ICommand NavigateToOtherViewModelCommand { get; }

        private void NavigateToOtherViewModel(object parameter)
        {
            var viewModelTitle = parameter as string;
            CurrentViewModel = _titleViewPairs[viewModelTitle];
        }

        private bool CanNavigateToOtherViewModel(object parameter)
        {
            return true;
        }
    }
}
