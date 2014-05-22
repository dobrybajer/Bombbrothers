using System;
using System.Windows;
using Bombbrothers.Logic;

namespace Bombbrothers.Interface.Screens
{
    /// <summary>
    ///     Logika dla kontrolki LogIn.xaml. Dziedziczy po UserControl oraz implementuje interfejs INavigable.
    /// </summary>
    public partial class LogIn : INavigable
    {
        /// <summary>
        ///     Konstruktor.
        /// </summary>
        public LogIn()
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
        ///     Obsługa zdarzenia przyciśnięcia przycisku. Loguje użytkownika wyświetlając odpowiedni komunikat. Po poprawnym
        ///     zalogowaniu zmienia zawartość okna na kontrolkę Menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || password.Password == "")
            {
                MessageBox.Show("Login and password cannot be empty");
                return;
            }

            if (FileManager.GetUser(name.Text, password.Password))
            {
                NavigationController.NavigateTo(new Menu());
            }
        }
    }
}