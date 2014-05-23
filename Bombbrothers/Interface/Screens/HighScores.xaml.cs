using System;
using System.Collections.Generic;
using Bombbrothers.Logic;
using Bombbrothers.Resources.XMLModel;

namespace Bombbrothers.Interface.Screens
{
    /// <summary>
    ///     Logika dla kontrolki HighScores.xaml. Dziedziczy po UserControl oraz implementuje interfejs INavigable.
    /// </summary>
    public partial class HighScores : INavigable
    {
        private List<ScoresScore> _scores = new List<ScoresScore>();

        /// <summary>
        ///     Konstruktor. Ładuje wyniki z pliku.
        /// </summary>
        public HighScores()
        {
            var scores = FileManager.GetHighScores();

            _scores = scores.Scores1;
            InitializeComponent();
        }

        /// <summary>
        ///     Kolekcja obiektów typu ScoresScore, które reprezentują wynik danego gracza.
        /// </summary>
        public List<ScoresScore> Scores
        {
            get { return _scores; }
            set { _scores = value; }
        }

        /// <summary>
        ///     Implementacja interfejsu INavigable. Wywoływane, gdy kontrolka została załadowana z przekazaniem parametru.
        /// </summary>
        /// <param name="state">Obiekt przekazany kontrolce.</param>
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}