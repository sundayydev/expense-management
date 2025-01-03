﻿using System.Windows;
using System.Windows.Controls;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for TextHolder.xaml
    /// </summary>
    public partial class TextHolder : UserControl
    {
        public TextHolder()
        {
            InitializeComponent();
        }
        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }
        public static readonly DependencyProperty PlaceHolderProperty = DependencyProperty.Register(
        "PlaceHolder",
        typeof(string),
        typeof(TextHolder)
      );

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        "Text",
        typeof(string),
        typeof(TextHolder)
     );

        public bool IsPass
        {
            get { return (bool)GetValue(IsPassProperty); }
            set { SetValue(IsPassProperty, value); }
        }
        public static readonly DependencyProperty IsPassProperty = DependencyProperty.Register(
        "IsPass",
        typeof(bool),
        typeof(TextHolder)
     );

        private void passbox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            email.Text = passbox.Password;
        }
    }
}

