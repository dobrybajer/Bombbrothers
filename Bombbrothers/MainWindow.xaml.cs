using System.Windows;
using Bombbrothers.Logic;
using Bombbrothers.Interface.Screens;
using System;
using System.Windows.Controls;
using System.Media;

namespace Bombbrothers
{
    /// <summary>
    /// Logika dla MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Konstruktor. Przygotowuje system plików aplikacji oraz ustawia zawartość okna na kontrolkę Title. Kontrolkami używanymi przez MainWindow są kontrolki typu UserControl.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            FileManager.Prepare();

            NavigationController.Window = this;
            NavigationController.NavigateTo(new Title());
        }

        /// <summary>
        /// Metoda zmieniająca zawartość okna na kontrolkę podaną w parametrze.
        /// </summary>
        /// <param name="target">Docelowa zawartość okna.</param>
        public void Navigate(UserControl target)
        {
            this.content.Children.Clear();
            this.content.Children.Add(target);
        }

        /// <summary>
        /// Metoda zmieniająca zawartość okna na kontrolkę podaną w parametrze. Pozwala przekazać parametr pomiędzy kontrolkami.
        /// </summary>
        /// <param name="target">Docelowa zawartość okna.</param>
        /// <param name="state">Obiekt przekazany między kontrolkami.</param>
        public void Navigate(UserControl target, object state)
        {
            this.content.Children.Clear();
            this.content.Children.Add(target);

            INavigable s = target as INavigable;

            if (s != null)
            {
                s.UtilizeState(state);
            }
            else
            {
                throw new ArgumentException("Target " + target.GetType().ToString() + " is not INavigable!");
            }
        }

        /// <summary>
        /// Kończy działanie całej aplikacji.
        /// </summary>
        public void Exit()
        {
            this.Close();
        }

        /// <summary>
        /// Metoda przypisana do zdarzenia KeyDown głównego okna. Pozwala na wyjście do Menu Głównego za pomocą klawisza ESC w każdym miejscu działania programu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Escape)
            {
                NavigationController.NavigateTo(new Bombbrothers.Interface.Screens.Menu());
            }
        }
    }
}
