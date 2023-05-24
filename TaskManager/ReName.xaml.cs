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

        public ReName(Desk desk, int num_col, int num_des, StackPanel stack, string name, StackPanel main_stack)
        {
            InitializeComponent();
            Setting(desk, num_col, num_des, stack, name, main_stack);
        }

        private void Setting(Desk desk, int num_col, int num_des, StackPanel stack, string name, StackPanel main_stack)
        {
            Height = 170;
            TextBlock textBlock = new TextBlock()
            {
                Text = "Переименовать:",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10, 10, 0, 0),
                Width = 156,
                Height = 20,
                FontFamily = new FontFamily("Arial"),
                FontSize = 18
            };

            TextBox renameLine = new TextBox()
            {
                Text = name,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 309,
                Height = 43,
                FontFamily = new FontFamily("Arial"),
                FontSize = 20
            };

            Button save = new Button()
            {
                Content = "Сохранить",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 10, 0, 0)
            };
            save.Click += (s, e) => Save_click(renameLine, desk, num_col, num_des, stack);

            Button del = new Button()
            {
                Content = "Удалить",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 10, 0, 0),
                Background = new SolidColorBrush(Colors.Red)
            };
            del.Click += (s, e) => Del_click(desk, num_col, num_des, main_stack);

            StackPanel stackPanel = new StackPanel();
            stackPanel.Children.Add(textBlock);
            stackPanel.Children.Add(renameLine);
            stackPanel.Children.Add(save);
            stackPanel.Children.Add(del);
            Content = stackPanel;
        }

        private void Del_click(Desk desk, int num_col, int num_des, StackPanel main_stack)
        {
            main_stack.Children.RemoveAt(num_col);
            users[desk.parrent_id - 1].desk[num_des].column.RemoveAt(num_col);
            Read.Write(users);
            Close();
        }

        private void Save_click(TextBox text1, Desk desk, int num_col, int num_des, StackPanel stack)
        {
            users[desk.parrent_id - 1].desk[num_des].column[num_col].name = text1.Text;
            ((TextBlock)stack.Children[0]).Text = text1.Text;
            Read.Write(users);
            Close();
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
