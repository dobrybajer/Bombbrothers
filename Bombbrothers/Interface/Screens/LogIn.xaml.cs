using Bombbrothers.Logic;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Bombbrothers.Interface.Screens
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : UserControl, INavigable
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(name.Text == "" || password.Password == "")
            {
                MessageBox.Show("Login and password cannot be empty");
                return;
            }

            if(FileManager.GetUser(name.Text, password.Password))
            {
                NavigationController.NavigateTo(new Menu());
            }
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
