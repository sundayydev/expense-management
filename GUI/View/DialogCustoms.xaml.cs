using System;
using System.Windows;

namespace GUI.View
{
    public partial class DialogCustoms : Window
    {
        public static int YesNo = 1;
        public static int OK = 0;
        public static new int Show = 0;

        public DialogCustoms()
        {
            InitializeComponent();
        }

        public DialogCustoms(string mess, string title, int mode) : this()
        {
            try
            {
                this.Title = title;
                txblMess.Text = mess;
                if (mode == YesNo)
                {
                    BtnOK.Visibility = Visibility.Hidden;
                }
                else if (mode == OK)
                {
                    BtnYes.Visibility = Visibility.Hidden;
                    BtnNo.Visibility = Visibility.Hidden;
                }
                else if (mode == Show)
                {
                    BtnYes.Visibility = Visibility.Hidden;
                    BtnNo.Visibility = Visibility.Hidden;
                    BtnOK.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi DialogCustom :" + ex.Message);
            }

        }
        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}