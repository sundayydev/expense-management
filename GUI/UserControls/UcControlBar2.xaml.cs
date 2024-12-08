using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI.UserControls
{
   public partial class UcControlBar2 : UserControl
   {
      public UcControlBar2()
      {
         InitializeComponent();
      }
      
      private void Button_Close(object sender, RoutedEventArgs e)
      {
         Button btn_close = sender as Button;
         Window mainwindows = Window.GetWindow(btn_close);
         mainwindows.Close();
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
   }
}