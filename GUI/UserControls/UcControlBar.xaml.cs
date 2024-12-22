using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcControlBar.xaml
    /// </summary>
    public partial class UcControlBar : UserControl
    {
        public UcControlBar()
        {
            InitializeComponent();
        }

        #region event
        private void Button_Close(object sender, RoutedEventArgs e)
        {
            Button btn_close = sender as Button;
            Window mainwindows = Window.GetWindow(btn_close);
            mainwindows.Close();
        }

        private void Button_Maximize(object sender, RoutedEventArgs e)
        {
            Button btn_close = sender as Button;
            Window mainwindows = Window.GetWindow(btn_close);
            if (mainwindows != null)
            {
                if (mainwindows.WindowState != WindowState.Maximized)
                {
                    mainwindows.WindowState = WindowState.Maximized;
                    btn_maximize.ToolTip = "Normal";
                    ControlBar.MinHeight = 1;
                }
                else
                {
                    mainwindows.WindowState = WindowState.Normal;
                    btn_maximize.ToolTip = "Maximize";
                    ControlBar.MinHeight = 0;
                }
            }
        }
        private void Button_Minimize(object sender, RoutedEventArgs e)
        {
            Button btn_close = sender as Button;
            Window mainwindows = Window.GetWindow(btn_close);
            mainwindows.WindowState = WindowState.Minimized;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            Window mainwindows = Window.GetWindow(grid);
            mainwindows.DragMove();
        }
        #endregion
    }
}
