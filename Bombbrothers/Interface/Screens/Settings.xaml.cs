using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Bombbrothers.Additional;
using Bombbrothers.Logic;

namespace Bombbrothers.Interface.Screens
{
    /// <summary>
    ///     Logika dla kontrolki Settings.xaml. Dziedziczy po UserControl oraz implementuje interfejs INavigable.
    /// </summary>
    public partial class Settings : INavigable
    {
        /// <summary>
        ///     Konstruktor.
        /// </summary>
        public Settings()
        {
            InitializeComponent();
            SettingsControl actualSettings = GameParameters.ActualSettings;

            t1.Text = actualSettings.Up;
            t2.Text = actualSettings.Down;
            t3.Text = actualSettings.Left;
            t4.Text = actualSettings.Right;
            t5.Text = actualSettings.DropBomb;
            t6.Text = actualSettings.UseBonus;
            t7.Text = actualSettings.UsePlayerBonus;
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
        ///     Obsługa zdarzenia przyciśnięcia przycisku. Zmienia ustawienia dla danego użytkownika. Po poprawnym zalogowaniu
        ///     zmienia zawartość okna na kontrolkę Menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var sc = new SettingsControl
            {
                UserIdSpecified = true,
                UserId = GameParameters.ActualUser.Id,
                Up = t1.Text,
                Down = t2.Text,
                Left = t3.Text,
                Right = t4.Text,
                DropBomb = t5.Text,
                UseBonus = t6.Text,
                UsePlayerBonus = t7.Text
            };
            FileManager.SetSettings(sc);

            NavigationController.NavigateTo(new Menu());
        }

        /// <summary>
        ///     Obsługa zdarzenia wciśnięcia klawisza na polu tekstowym.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void text_KeyDown(object sender, KeyEventArgs e)
        {
            var t = sender as TextBox;
            if (t != null) t.Text = e.Key.ToString();
        }
    }
}