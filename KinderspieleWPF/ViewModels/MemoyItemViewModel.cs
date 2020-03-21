using KinderspieleWPF.Commands;
using KinderspieleWPF.Enumerations;
using System;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KinderspieleWPF.ViewModels
{
    public class MemoyItemViewModel : ViewModelBase
    {

        public bool Locked { get; set; }
        public ICommand FlipCommand { get; set; }
        public ECardSide VisibleSite { get => visibleSite; set {if (visibleSite != value)
                {
                    visibleSite = value;
                    OnPropertyChanged(nameof(Quelle));
                }
                } 
        }         
        // Bitmap bmp = Properties.Resources.<name_you_gave_it>;
        public MemoyItemViewModel(Bitmap front, Bitmap back)
        {
            this.ImageOrigin = front;
            this.Frontside = BitmapToBitmapSource(front);
            this.backside = BitmapToBitmapSource(back);
            FlipCommand = new RelayCommand<MemoyItemViewModel>(FlipExecute);
        }

        private BitmapSource backside;
        public BitmapSource Frontside { get; private set; }
        public Bitmap ImageOrigin { get; private set; }

        private ECardSide visibleSite = ECardSide.backside;

        public void FlipExecute(object obj)
        {
            Flip();
        }

        /// <summary>
        /// Flip side or force to backside
        /// </summary>
        /// <param name="toBack"></param>
        public void Flip(bool toBack = false)
        {
            if (VisibleSite == ECardSide.frontside || toBack)
            {
                VisibleSite = ECardSide.backside;
            }
            else
            {
                VisibleSite = ECardSide.frontside;
            }
        }

        public ImageSource Quelle
        {
            get
            {
                if (VisibleSite == ECardSide.frontside)
                {
                    return Frontside;
                }
                return backside;
            }
        }

        public BitmapSource BitmapToBitmapSource(Bitmap source)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                          source.GetHbitmap(),
                          System.IntPtr.Zero,
                          System.Windows.Int32Rect.Empty,
                          BitmapSizeOptions.FromEmptyOptions());
        }



    }
}
