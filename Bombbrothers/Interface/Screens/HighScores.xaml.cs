using Bombbrothers.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bombbrothers.Interface.Screens
{
    /// <summary>
    /// Interaction logic for HighScores.xaml
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
