using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Bombbrothers.Additional;
using Bombbrothers.Logic;

namespace Bombbrothers.Interface.Screens
{
    /// <summary>
    ///     Logika dla kontrolki NewGame.xaml. Dziedziczy po UserControl oraz implementuje interfejs INavigable.
    /// </summary>
    public partial class NewGame : INavigable
    {
        /// <summary>
        ///     Konstruktor.
        /// </summary>
        public NewGame()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Implementacja interfejsu INavigable. Wywoływane, gdy kontrolka została załadowana z przekazaniem parametru.
        /// </summary>
        /// <param name="state">Obiekt przekazany kontrolce.</param>
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Obsługa zdarzenia puszczenia przycisku myszy nad elementem typu Border.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var s = sender as Border;
            var tOld = new Thickness(0);
            Brush bNew = Brushes.Red;
            var tNew = new Thickness(3);

            if (s == null) return;
            switch (s.Name)
            {
                case "Agile":
                    Agile.BorderBrush = bNew;
                    Agile.BorderThickness = tNew;
                    Fast.BorderBrush = null;
                    Fast.BorderThickness = tOld;
                    Shy.BorderBrush = null;
                    Shy.BorderThickness = tOld;
                    GameParameters.PlayerType = Enums.PlayerType.Agile;
                    break;
                case "Fast":
                    Agile.BorderBrush = null;
                    Agile.BorderThickness = tOld;
                    Fast.BorderBrush = bNew;
                    Fast.BorderThickness = tNew;
                    Shy.BorderBrush = null;
                    Shy.BorderThickness = tOld;
                    GameParameters.PlayerType = Enums.PlayerType.Fast;
                    break;
                case "Shy":
                    Agile.BorderBrush = null;
                    Agile.BorderThickness = tOld;
                    Fast.BorderBrush = null;
                    Fast.BorderThickness = tOld;
                    Shy.BorderBrush = bNew;
                    Shy.BorderThickness = tNew;
                    GameParameters.PlayerType = Enums.PlayerType.Shy;
                    break;
            }
        }

        /// <summary>
        ///     Obsługa zdarzenia puszczenia przycisku myszy nad blokiem tekstowym.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var s = sender as TextBlock;
            if (s == null) return;
            switch (s.Name)
            {
                case "Easy":
                    Easy.FontWeight = FontWeights.ExtraBold;
                    Medium.FontWeight = FontWeights.Normal;
                    Hard.FontWeight = FontWeights.Normal;
                    GameParameters.Difficulty = Enums.LevelDifficulty.Easy;
                    break;
                case "Medium":
                    Easy.FontWeight = FontWeights.Normal;
                    Medium.FontWeight = FontWeights.ExtraBold;
                    Hard.FontWeight = FontWeights.Normal;
                    GameParameters.Difficulty = Enums.LevelDifficulty.Medium;
                    break;
                case "Hard":
                    Easy.FontWeight = FontWeights.Normal;
                    Medium.FontWeight = FontWeights.Normal;
                    Hard.FontWeight = FontWeights.ExtraBold;
                    GameParameters.Difficulty = Enums.LevelDifficulty.Hard;
                    break;
            }
        }

        /// <summary>
        ///     Obsługa zdarzenia przyciśnięcia przycisku. Zmienia zawartość okna na kontrolkę Game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationController.NavigateTo(new Game());
        }
    }
}