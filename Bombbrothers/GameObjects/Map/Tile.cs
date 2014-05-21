using System.ComponentModel;
using System.Windows.Controls;

namespace Bombbrothers.GameObjects.Map
{
    /// <summary>
    /// Klasa reprezentująca pojedynczy kafelek (Tile). Dziedziczy po klasie Image oraz implementuje interfejs INotifyPropertyChanged
    /// </summary>
    public class Tile : Image, INotifyPropertyChanged
    {
        /// <summary>
        /// Numer wiersza kafelka.
        /// </summary>
        public int Row { get; set; }
        
        /// <summary>
        /// Numer kolumny kafelka.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Informacja czy dany kafelek jest stałym elementem otoczenia.
        /// </summary>
        public bool IsConstElement { get; set; }

        /// <summary>
        /// Informacja czy dany kafelek jest pusty (jest typu Background).
        /// </summary>
        public bool IsEmpty { get; set; }

        private int _type;
        /// <summary>
        /// Typ kafelka. Wywołuje OnPropertyChanged przy zmianie wartości.
        /// </summary>
        public int Type
        {
            get { return _type; }
            set 
            {
                _type = value;
                NotifyPropertyChanged("Type"); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Implementacja interfejsu INotifyPropertyChanged.
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
