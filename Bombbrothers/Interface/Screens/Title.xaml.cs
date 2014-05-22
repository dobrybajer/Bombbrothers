using System;
using System.Windows.Threading;
using Bombbrothers.Logic;

namespace Bombbrothers.Interface.Screens
{
    /// <summary>
    ///     Logika dla kontrolki Title.xaml. Dziedziczy po UserControl oraz implementuje interfejs INavigable.
    /// </summary>
    public partial class Title : INavigable
    {
        /// <summary>
        ///     Konstruktor. Tworzy DispatcherTimer;
        /// </summary>
        public Title()
        {
            InitializeComponent();

            var dt = new DispatcherTimer();
            dt.Tick += dispatcherTimer_Tick;
            dt.Interval = new TimeSpan(0, 0, 1);
            dt.Start();
        }

        /// <summary>
        ///     Implementacja interfejsu INavigable. Wywoływane, gdy kontrolka została załadowana z przekazaniem parametru.
        /// </summary>
        /// <param name="state">Obiekt przekazany kontrolce.</param>
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private static void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var dt = sender as DispatcherTimer;
            if (dt != null) dt.Stop();
            NavigationController.NavigateTo(new LogIn());
        }
    }
}