using Bombbrothers.Logic;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Bombbrothers.Interface.Screens
{
    /// <summary>
    /// Logika dla kontrolki HighScores.xaml. Dziedziczy po UserControl oraz implementuje interfejs INavigable.
    /// </summary>
    public partial class HighScores : UserControl, INavigable
    {
        public List<ScoresScore> _scores = new List<ScoresScore>();

        public HighScores()
        {
            Scores scores = FileManager.GetHighScores();
   
            _scores = scores.Scores1;
            InitializeComponent();
        }

        public List<ScoresScore> Scores
        { 
            get { return _scores; }
            set { _scores = value; }
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
