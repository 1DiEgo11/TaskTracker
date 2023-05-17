using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Animation;
using MaterialDesignThemes.Wpf.Transitions;
using System.Windows.Documents;
using System.ComponentModel.Design;

namespace TaskManager
{
    internal class Board
    {
        private Window window;
        private List<User> users;
        private User user;
        public ScrollViewer Window_with_bords(Window window, List<User> _users, User user)
        {
            this.user = user;
            users = _users;
            this.window = window;

            StackPanel myStackPanel = new StackPanel();
            
            var myScrollViewer = new ScrollViewer
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                Content = Main_Stack(myStackPanel)
            };

            return myScrollViewer;
        }

        private Border Main_Stack(StackPanel stack)
        {
            stack.Orientation=Orientation.Vertical;
            Border bord = new Border
            {
                Width = 1200,
                Margin = new Thickness(20),
                Background = new SolidColorBrush(Colors.White),
                Padding = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Center,
                CornerRadius = new CornerRadius(5),
                Effect = new DropShadowEffect { BlurRadius = 30, Color = Colors.Black, ShadowDepth = 0 }
            };
            
            var MyText = new TextBlock
            {
                Margin = new Thickness(60, 15, 15, 0),
                Text = "Все доступные вам доски",
                FontSize = 20
            };

            Button btn = new Button
            {
                Content = "Меню",
                Margin = new Thickness(60, 30, 15, 0),
                Width = 100,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Left,
            };

            ContextMenu menu = new ContextMenu();
            MenuItem mi = new MenuItem();
            mi.Header = "Смена пользователя";
            MenuItem mia = new MenuItem();
            mia.Header = "Выйти";
            menu.Items.Add(mi);
            menu.Items.Add(mia);
            mia.Click += Exit;
            mi.Click += ChangePerson;

            btn.ContextMenu = menu;
        
            stack.Children.Add(btn);
            stack.Children.Add(MyText);
            stack.Children.Add(Bords());
            bord.Child = stack;
            return bord;
        }
        
        private Button Create_bord(string name, List<Column> columns)
        {
            var Text = new TextBox
            {
                Width = 70,
                Text = name,
            };
            var myCart = new Button
            {
                Background = new SolidColorBrush(Colors.Gray),
                Margin = new Thickness(30),
                Width = 100,
                Height = 100,
                Content = Text
            };
            myCart.Click += (s, e) => OpenDesk(columns, name);

            return myCart;
        }

        private WrapPanel Bords()
        {
            WrapPanel myPanel = new WrapPanel
            {
                VerticalAlignment = VerticalAlignment.Top,
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center

            };

            var button_Add = new Button
            {
                Margin = new Thickness(30),
                Width = 100,
                Height = 100,
                FontSize = 30,
                Content = "+"
            };

            
            foreach (var user in users)
            {
                foreach(var bord in user.desk)
                {
                    if (bord.access == 1)
                    {
                        myPanel.Children.Add(Create_bord(bord.name, bord.column));
                    }
                    if (bord.access == 0 && bord.whitelist.Contains(this.user.id))
                    {
                        myPanel.Children.Add(Create_bord(bord.name, bord.column));
                    }
                }
            }
            
            myPanel.Children.Add(button_Add);
            
            return myPanel;
        }


        private void OpenDesk(List<Column> columns, string name)
        {
            var a = new BoardColumn();
            window.Content = a.Draw_Stack(users, columns, name);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ChangePerson(object sender, RoutedEventArgs e)
        {
            AuthWindow new_window = new AuthWindow();
            new_window.Show();
            window.Close();        
        }
    }
}
