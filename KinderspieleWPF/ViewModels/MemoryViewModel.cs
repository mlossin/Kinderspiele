using KinderspieleWPF.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KinderspieleWPF.ViewModels
{
    public class MemoryViewModel : ViewModelBase
    {
        private Bitmap back;
        public MemoryViewModel()
        {
            back = Properties.Resources.DinoStempel;
            NewGame();
        }

        public void NewGame(object obj = null)
        {
            var items = Create16();
            Shuffle<MemoyItemViewModel>(items);
            Karten = new ObservableCollection<MemoyItemViewModel>(items);
            OnPropertyChanged(nameof(Karten));
        }

        private List<MemoyItemViewModel> Create16()
        {
            List<MemoyItemViewModel> result = new List<MemoyItemViewModel>();


            // Bitmap bmp = Properties.Resources.<name_you_gave_it>;
            Bitmap frontImage = Properties.Resources.benjamin_blumchen;
            result.Add(CreateCard(frontImage));
            result.Add(CreateCard(frontImage));
            // 2
            frontImage = Properties.Resources.cars_mcqueen;
            result.Add(CreateCard(frontImage)); 
            result.Add(CreateCard(frontImage));
            // 3
            frontImage = Properties.Resources.DinoOut;
            result.Add(CreateCard(frontImage));
            result.Add(CreateCard(frontImage));
            // 4
            frontImage = Properties.Resources.Feuerwehrmann_Sam;
            result.Add(CreateCard(frontImage));
            result.Add(CreateCard(frontImage));
            // 5
            frontImage = Properties.Resources.Frozen_AnnaElsa;
            result.Add(CreateCard(frontImage));
            result.Add(CreateCard(frontImage));
            // 6
            frontImage = Properties.Resources.opa_bennie;
            result.Add(CreateCard(frontImage));
            result.Add(CreateCard(frontImage));
            // 7
            frontImage = Properties.Resources.pawpatrol;
            result.Add(CreateCard(frontImage));
            result.Add(CreateCard(frontImage));
            // 8
            frontImage = Properties.Resources.stinkydirty;
            result.Add(CreateCard(frontImage));
            result.Add(CreateCard(frontImage));
            return result;
        }

        private MemoyItemViewModel CreateCard( Bitmap frontImage)
        {
            MemoyItemViewModel cardItem = new MemoyItemViewModel(frontImage, back);
            cardItem.PropertyChanged += SideChanged; //Add EventListener
            return cardItem;
        }

        private void SideChanged(object sender, PropertyChangedEventArgs e)
        {
            //Wenn richtige seite hochzählen
            if (sender is MemoyItemViewModel)
            {
                var item = (MemoyItemViewModel)sender;
                if (item.VisibleSite == Enumerations.ECardSide.frontside)
                {
                    var visibleItems = Karten.Where(x => x.VisibleSite == Enumerations.ECardSide.frontside && x.Locked == false).ToArray();
                    //wenn 2 da, warten und zurückdrehen
                    if (visibleItems.Count() >= 2)
                    {
                        
                        if(visibleItems[0].ImageOrigin == visibleItems[1].ImageOrigin)
                        {
                            visibleItems[0].Locked = true;
                            visibleItems[1].Locked = true;
                        }
                        else
                        {
                            //Zurücksetzten nach verzögerung
                            Task.Factory.StartNew(() => {
                                Thread.Sleep(WAITFORBACKFLIP);
                                TurnAllToBackside();
                            });
                        }
                    }
                }
            }     
        }

        private void TurnAllToBackside()
        {
            Karten.Where(x => !x.Locked).ToList().ForEach(x => x.Flip(true));
        }

        public ObservableCollection<MemoyItemViewModel> Karten { get; set; }

        #region "List Randomizer"
        private static Random rng = new Random();
        private readonly int WAITFORBACKFLIP = 600;

        //this + static entfernt.. somit keine extension method mehr
        public void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        #endregion
    }
}
