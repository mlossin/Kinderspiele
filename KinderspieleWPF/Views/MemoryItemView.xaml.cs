using System.Windows;
using System.Windows.Controls;

namespace KinderspieleWPF.Views
{
    /// <summary>
    /// Interaction logic for MemoryImageButton.xaml
    /// </summary>
    public partial class MemoryItemView : UserControl
    {
        public MemoryItemView()
        {
            InitializeComponent();
        }

        private void Flip(object sender, RoutedEventArgs e)
        {
            if (IsFlipable)
            {
                if (Quelle == Backside)
                {
                    Quelle = FrontSide;
                }
                else
                {
                    Quelle = Backside;
                } 
            }
        }

        public bool IsFlipable { get; set; } = true;

        public Image Backside { get; set; }
        public Image FrontSide { get; set; }

        public Image Quelle { get; set; }
    }
}
