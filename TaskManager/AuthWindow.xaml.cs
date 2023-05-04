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
using System.Windows.Shapes;

namespace TaskManager
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void AuthBox_Click(object sender, RoutedEventArgs e)
        {
            string login = TextBoxLogin.Text.Trim();
            string pass = passBox.Password.Trim();

            if (login.Length < 5)
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
            if (TextBoxLogin.Background == Brushes.Transparent && passBox.Background == Brushes.Transparent)
            {
                MessageBox.Show("Вы успешно вошли!");
                UserPageWindow userPageWindow = new UserPageWindow();
                userPageWindow.Show();
                Close();
            }
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
