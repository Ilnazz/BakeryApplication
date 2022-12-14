using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Bakery.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (field?.Equals(value) ?? false)
                return false;
            field = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }

        public void Dispose() => Dispose(true);

        private bool _isDisposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposing == false || _isDisposed == true)
                return;
            _isDisposed = true;
            // Освобождение управляемых ресурсов
        }
    }
}
