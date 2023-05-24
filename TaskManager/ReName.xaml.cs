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
    /// Логика взаимодействия для ReName.xaml
    /// </summary>
    public partial class ReName : Window
    {
        List<User> users = Read.Reading();
        public ReName()
        {
            InitializeComponent();
        }

        public ReName(Desk desk, int desk_num, Button btn) 
        {
            InitializeComponent();
            text.Text = desk.name;
            Save.Click += (s, e) => Save_Click(desk, desk_num, btn);
        }

        public ReName(Column column)
        {
            InitializeComponent();

        }

        private void Save_Click(Desk desk, int num, Button btn)
        {
            btn.Content = text.Text;
            desk.name = text.Text;
            users[desk.parrent_id - 1].desk[num].name = text.Text;
            Read.Write(users);
            Close();
        }
    }
}
