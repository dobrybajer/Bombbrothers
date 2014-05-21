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
using Bombbrothers.Logic;
using System.Windows.Threading;

namespace Bombbrothers.Interface.Screens
{
    /// <summary>
    /// Interaction logic for Title.xaml
    /// </summary>
    public partial class Title : UserControl, INavigable
    {
        private DispatcherTimer dt;
        private int actualSeconds;
        private const int allSeconds = 1;

        public Title()
        {
            InitializeComponent();

            actualSeconds = 0;

            dt = new System.Windows.Threading.DispatcherTimer();
            dt.Tick += new EventHandler(dispatcherTimer_Tick);
            dt.Interval = new TimeSpan(0, 0, 1);
            dt.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            actualSeconds++;
            if(actualSeconds == allSeconds)
            {
                dt.Stop();
                NavigationController.NavigateTo(new LogIn());
            }
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
