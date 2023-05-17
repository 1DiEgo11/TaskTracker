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
using System.Windows.Media.Animation;

namespace TaskManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DoubleAnimation btnAnimation = new DoubleAnimation
            {
                From = 0,
                To = 450,
                Duration = TimeSpan.FromSeconds(2)
            };
            RegButton.BeginAnimation(Button.WidthProperty, btnAnimation);
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            string login = TextBoxLogin.Text.Trim();
            string pass = passBox.Password.Trim();
            string pass_2 = passBox_2.Password.Trim();
            string email = emailBox.Text.Trim();
            
            if(login.Length < 5)
            {   
                TextBoxLogin.ToolTip = "Поле введено не коректно";
                TextBoxLogin.Background = Brushes.OrangeRed;
            }
            else
            {
                TextBoxLogin.ToolTip = "";
                TextBoxLogin.Background = Brushes.Transparent;
            }
            if (pass.Length < 5)
            {
                passBox.ToolTip = "Поле введено не коректно";
                passBox.Background = Brushes.OrangeRed;
            }
            else
            {
                passBox.ToolTip = "";
                passBox.Background = Brushes.Transparent;
            }
            if (pass != pass_2 || pass_2.Length < 5)
            {
                passBox_2.ToolTip = "Поле введено не коректно";
                passBox_2.Background = Brushes.OrangeRed;
            }
            else
            {
                passBox_2.ToolTip = "";
                passBox_2.Background = Brushes.Transparent;
            }
            if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {
                emailBox.ToolTip = "Поле введено не коректно";
                emailBox.Background = Brushes.OrangeRed;
            }
            else
            {
                emailBox.ToolTip = "";
                emailBox.Background = Brushes.Transparent;
            }
            if(TextBoxLogin.Background == Brushes.Transparent && passBox.Background == Brushes.Transparent && passBox_2.Background == Brushes.Transparent && emailBox.Background == Brushes.Transparent)
            {
                User user = new User(Read.GetId(),login, pass, email);
                MessageBox.Show("Вы успешно зарегестрировались!");
                UserPageWindow userPageWindow = new UserPageWindow();
                userPageWindow.Show();
                Close();
            }
        }
        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }
    }
}
