using System.Windows.Controls;

namespace Bombbrothers.Logic
{
    /// <summary>
    ///     Kontroler nawigujący aktualnie wyświetlaną zawartość okna.
    /// </summary>
    public static class NavigationController
    {
        /// <summary>
        ///     Instancja okna - rodzica
        /// </summary>
        private static MainWindow _mainWindow;

        /// <summary>
        ///     Właściwość ustawiająca okno - rodzica
        /// </summary>
        public static MainWindow Window
        {
            set { _mainWindow = value; }
        }

        /// <summary>
        ///     Zmiana zawartośći okna.
        /// </summary>
        /// <param name="target">Nowa zawartość okna</param>
        public static void NavigateTo(UserControl target)
        {
            _mainWindow.Navigate(target);
        }

        /// <summary>
        ///     Zmiana zawartośći okna. Pozwala na przekazanie obiektu między zawartościami okna.
        /// </summary>
        /// <param name="target">Nowa zawartość okna</param>
        /// <param name="state">Obiekt przekazany do nowej zawartości okna.</param>
        public static void Switch(UserControl target, object state)
        {
            _mainWindow.Navigate(target, state);
        }

        /// <summary>
        ///     Zamknięcie okna głównego aplikacji.
        /// </summary>
        public static void Exit()
        {
            _mainWindow.Exit();
        }
    }
}