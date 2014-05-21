using System.Windows;
using Bombbrothers.Logic;
using Bombbrothers.Interface.Screens;
using System;
using System.Windows.Controls;
using System.Media;

namespace Bombbrothers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            FileManager.Prepare();

            NavigationController.Window = this;
            NavigationController.NavigateTo(new Title());
        }

        public void Navigate(UserControl target)
        {
            this.content.Children.Clear();
            this.content.Children.Add(target);
        }

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

        public void Exit()
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //var s = sender as Key;
            if(e.Key == System.Windows.Input.Key.Escape)
            {
                NavigationController.NavigateTo(new Bombbrothers.Interface.Screens.Menu());
            }
        }
    }
}
