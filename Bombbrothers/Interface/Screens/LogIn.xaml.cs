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
