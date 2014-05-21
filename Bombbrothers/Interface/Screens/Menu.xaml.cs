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

namespace Bombbrothers.Interface.Screens
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl, INavigable
    {
        public Menu()
        {
            InitializeComponent();
            m0.Text = "Witaj " + GameParameters.ActualUser.Name + " ! Co chcesz zrobić ?";
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            var s = sender as TextBlock;

            if (!s.IsEnabled)
                return;

            s.Foreground = Brushes.White;
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            var s = sender as TextBlock;

            if (!s.IsEnabled)
                return;

            s.Foreground = Brushes.Black;
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var s = sender as TextBlock;

            switch(s.Name)
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
                default:
                    break;
            }
            
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
