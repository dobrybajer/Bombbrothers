using Bombbrothers.Additional;
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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl, INavigable
    {
        private SettingsControl ActualSettings = new SettingsControl();

        public Settings()
        {
            InitializeComponent();
            ActualSettings = GameParameters.ActualSettings;
    
            t1.Text = ActualSettings.Up;
            t2.Text = ActualSettings.Down;
            t3.Text = ActualSettings.Left;
            t4.Text = ActualSettings.Right;
            t5.Text = ActualSettings.DropBomb;
            t6.Text = ActualSettings.UseBonus;
            t7.Text = ActualSettings.UsePlayerBonus;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsControl sc = new SettingsControl();
            sc.UserIdSpecified = true;
            sc.UserId = GameParameters.ActualUser.Id;
            sc.Up = t1.Text;
            sc.Down = t2.Text;
            sc.Left = t3.Text;
            sc.Right = t4.Text;
            sc.DropBomb = t5.Text;
            sc.UseBonus = t6.Text;
            sc.UsePlayerBonus = t7.Text;
            FileManager.SetSettings(sc);
            
            NavigationController.NavigateTo(new Menu());
        }

        private void text_KeyDown(object sender, KeyEventArgs e)
        {
            var t = sender as TextBox;
            t.Text = e.Key.ToString();
        }
    }
}
