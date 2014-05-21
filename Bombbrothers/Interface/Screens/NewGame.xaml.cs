using Bombbrothers.Logic;
using System;
using System.Collections.Generic;
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
using Bombbrothers.Additional;
using LevelDifficulty = Bombbrothers.Logic.Enums.LevelDifficulty;
using PlayerType = Bombbrothers.Logic.Enums.PlayerType;

namespace Bombbrothers.Interface.Screens
{
    /// <summary>
    /// Interaction logic for NewGame.xaml
    /// </summary>
    public partial class NewGame : UserControl, INavigable
    {
        public NewGame()
        {
            InitializeComponent();
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var s = sender as Border;
            Thickness t_old = new Thickness(0);
            Brush b_old = null;
            Brush b_new = Brushes.Red;
            Thickness t_new = new Thickness(3);

            switch(s.Name)
            {
                case "Agile":
                    Agile.BorderBrush = b_new;
                    Agile.BorderThickness = t_new;
                    Fast.BorderBrush = b_old;
                    Fast.BorderThickness = t_old;
                    Shy.BorderBrush = b_old;
                    Shy.BorderThickness = t_old;
                    GameParameters.PlayerType = PlayerType.Agile;
                    break;
                case "Fast":
                    Agile.BorderBrush = b_old;
                    Agile.BorderThickness = t_old;
                    Fast.BorderBrush = b_new;
                    Fast.BorderThickness = t_new;
                    Shy.BorderBrush = b_old;
                    Shy.BorderThickness = t_old;
                    GameParameters.PlayerType = PlayerType.Fast;
                    break;
                case "Shy":
                    Agile.BorderBrush = b_old;
                    Agile.BorderThickness = t_old;
                    Fast.BorderBrush = b_old;
                    Fast.BorderThickness = t_old;
                    Shy.BorderBrush = b_new;
                    Shy.BorderThickness = t_new;
                    GameParameters.PlayerType = PlayerType.Shy;
                    break;
                default:
                    break;
            }
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var s = sender as TextBlock;
            switch(s.Name)
            {
                case "Easy":
                    Easy.FontWeight = FontWeights.ExtraBold;
                    Medium.FontWeight = FontWeights.Normal;
                    Hard.FontWeight = FontWeights.Normal;
                    GameParameters.Difficulty = LevelDifficulty.Easy;
                    break;
                case "Medium":
                    Easy.FontWeight = FontWeights.Normal;
                    Medium.FontWeight = FontWeights.ExtraBold;
                    Hard.FontWeight = FontWeights.Normal;
                    GameParameters.Difficulty = LevelDifficulty.Medium;
                    break;
                case "Hard":
                    Easy.FontWeight = FontWeights.Normal;
                    Medium.FontWeight = FontWeights.Normal;
                    Hard.FontWeight = FontWeights.ExtraBold;
                    GameParameters.Difficulty = LevelDifficulty.Hard;
                    break;
                default:
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationController.NavigateTo(new Game());
        }
    }
}
