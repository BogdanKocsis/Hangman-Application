using Hangman.Official.Commands;
using Hangman.Official.Enums;
using System.Windows;
using System.Windows.Input;


namespace Hangman.Official.ViewModels
{
    class SignUpViewModel : BaseViewModel
    {
        private Pages _currentPage;
        public Pages CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                NotifyPropertyChanged(this, nameof(CurrentPage));
            }
        }

        public ICommand CloseApplicationCommand { get; set; }
        public ICommand NavigateCommand { get; set; }

        public SignUpViewModel()
        {
            CloseApplicationCommand = new ActionCommand(CloseApplication);
            NavigateCommand = new ActionCommand<Pages>(NavigateTo);
        }

        private void CloseApplication()
        {
            Application.Current.Shutdown();
        }

        private void NavigateTo(Pages page)
        {
            CurrentPage = page;
        }
    }
}
