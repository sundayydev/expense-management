using System.Globalization;
using System.Threading;
using System.Windows;

namespace GUI
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App
   {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            CultureInfo culture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}