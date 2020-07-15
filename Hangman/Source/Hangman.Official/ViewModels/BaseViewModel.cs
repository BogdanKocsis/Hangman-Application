using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hangman.Official.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(object sender, string propertyname)
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyname));
        }
        protected virtual void SetProperty<T>(ref T member, T val,
            [CallerMemberName]string propertyName = null)
        {
            //verificam daca valoarea chiar s-a schimbat
            if (object.Equals(member, val))
            {
                return;
            }

            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
