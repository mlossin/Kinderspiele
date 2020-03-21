using KinderspieleWPF.Commands;
using System.Windows.Input;

namespace KinderspieleWPF.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private MemoryViewModel MemoryViewModel;
        private ViewModelBase content;

        public MainWindowViewModel()
        {
            MemoryViewModel = new MemoryViewModel();
            Content = MemoryViewModel;
            NewMemoryCommand = new RelayCommand<object>(MemoryViewModel.NewGame);

        }
        public ViewModelBase Content { get => content; set => SetField(ref content , value); }

        public ICommand NewMemoryCommand { get; set; }
    }
}
