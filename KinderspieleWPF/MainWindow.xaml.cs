using System.Windows;

namespace KinderspieleWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new ViewModels.MainWindowViewModel();
            InitializeComponent();
        }
    }
}
