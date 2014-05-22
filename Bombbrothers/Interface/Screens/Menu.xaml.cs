using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Bombbrothers.Additional;
using Bombbrothers.Logic;

namespace Bombbrothers.Interface.Screens
{
    /// <summary>
    ///     Logika dla kontrolki Menu.xaml. Dziedziczy po UserControl oraz implementuje interfejs INavigable.
    /// </summary>
    public partial class Menu : INavigable
    {
        /// <summary>
        ///     Konstruktor.
        /// </summary>
        public Menu()
        {
            InitializeComponent();
            m0.Text = "Witaj " + GameParameters.ActualUser.Name + " ! Co chcesz zrobić ?";
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
        ///     Obsługa zdarzenia najechania kursorem na blok tekstowy.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            var s = sender as TextBlock;

            if (s != null && !s.IsEnabled)
                return;

            if (s != null) s.Foreground = Brushes.White;
        }

        /// <summary>
        ///     Obsługa zdarzenia wyjechania kursorem poza blok tekstowy.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            var s = sender as TextBlock;

            if (s != null && !s.IsEnabled)
                return;

            if (s != null) s.Foreground = Brushes.Black;
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
                case "m1":
                    NavigationController.NavigateTo(new Game());
                    break;
                case "m2":
                    NavigationController.NavigateTo(new NewGame());
                    break;
                case "m3":
                    NavigationController.NavigateTo(new HighScores());
                    break;
                case "m4":
                    NavigationController.NavigateTo(new Settings());
                    break;
                case "m5":
                    NavigationController.Exit();
                    break;
                case "m6":
                    NavigationController.NavigateTo(new LogIn());
                    break;
            }
        }
    }
}