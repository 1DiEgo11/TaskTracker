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
using System.Windows.Input;
using System.Collections;
using System.Xml.Linq;
using System.Windows.Controls.Primitives;

namespace TaskManager
{
    internal class Board
    {
        private Window window;
        private List<User> users;
        private User user;
        public int id;
        ScrollViewer myScrollViewer;
        public ScrollViewer Window_with_bords(Window window, List<User> _users, User user)
        {
            this.user = user;
            users = _users;
            this.window = window;
            id = user.id;
            StackPanel myStackPanel = new StackPanel();
            
            myScrollViewer = new ScrollViewer
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                Content = Main_Stack(myStackPanel)
            };

            return myScrollViewer;
        }

        private Border Main_Stack(StackPanel stack)
        {
            stack.Orientation=Orientation.Vertical;
            var newStack = new StackPanel();
            newStack.Orientation=Orientation.Horizontal;
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
                Margin = new Thickness(30, 15, 15, 0),
                Text = "Все доступные вам доски",
                FontSize = 20
            };

            Button btn = new Button
            {
                Content = "Меню",
                Margin = new Thickness(30, 30, 15, 0),
                Width = 100,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Left
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
            btn.Click += (s, e) => { btn.ContextMenu.IsOpen = true; };

            stack.Children.Add(newStack);
            newStack.Children.Add(btn);
            stack.Children.Add(MyText);
            stack.Children.Add(Bords());
            bord.Child = stack;
            return bord;
        }
        
        private StackPanel Create_bord(Desk desk, int desk_num)
        {
            var butPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
            };

            var Text = new TextBlock
            {
                Width = 70,
                Text = desk.name,
                HorizontalAlignment = HorizontalAlignment.Center
            };


            var myCart = new Button
            {
                Background = new SolidColorBrush(Colors.Gray),
                Margin = new Thickness(30, 30, 0, 30),
                Width = 100,
                Height = 100,
                Content = Text
            };
            myCart.Click += (s, e) => OpenDesk(desk.name, desk);

            var miniMenu = new Button
            {
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Top,
                Background = Brushes.Transparent,
                Foreground = new SolidColorBrush(Colors.Black),
                FontSize = 20,
                Width = 42,
                Height = 32,
                Margin = new Thickness(5),
                Content = "*"
            };


            ContextMenu menu = new ContextMenu();
            MenuItem mi = new MenuItem();
            mi.Header = "Пользователи";
            MenuItem rename = new MenuItem();
            rename.Header = "Переименовать";
            rename.Click += (s, e) => SaveName(desk, desk_num, myCart);
            MenuItem mia = new MenuItem();
            mia.Header = "Удалить доску";
            mia.Click += (s, e) => Delete(butPanel, desk, desk_num);

            menu.Items.Add(rename);
            menu.Items.Add(mi);
            menu.Items.Add(mia);

            MenuItem mi2 = new MenuItem();
            mi2.Header = "Список пользователей"; //вывести список пользователей
            
       
            foreach (var us in users)
            {
                MenuItem mius = new MenuItem();
                mius.Header = us.login;
                if (users[desk.parrent_id - 1].desk[desk_num].whitelist.Contains(us.id))
                {
                    mius.Background = new SolidColorBrush(Colors.LightGreen);
                }
                mius.Click += (s, e) => addUs(desk, desk_num, mius, us);
                mi2.Items.Add(mius);
            }
            

            MenuItem mi3 = new MenuItem();
            mi3.Header = "Общедоступность";
            if (desk.access == 1)
            {
                mi3.Background = new SolidColorBrush(Colors.LightGreen);
            }
            mi3.Click += (s, e) => openAccess(desk, desk_num, mi3);

            butPanel.Children.Add(myCart);
            if (user.id == desk.parrent_id)
            {
                miniMenu.Click += (s, e) => { miniMenu.ContextMenu.IsOpen = true; };
                mi.Items.Add(mi2);
                mi.Items.Add(mi3);
                miniMenu.ContextMenu = menu;
                butPanel.Children.Add(miniMenu);
            }            
            
            
            return butPanel;
        }

        private WrapPanel Bords() 
        {
            WrapPanel myPanel = new WrapPanel
            {
                VerticalAlignment = VerticalAlignment.Top,
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Left

            };

            var button_Add = new Button 
            {
                Margin = new Thickness(30, 30, 57, 30),
                Width = 100,
                Height = 100,
                FontSize = 30,
                Content = "+"
            };
            button_Add.Click += AddButton;

            foreach (var user in users)
            {
                int i = 0;
                foreach(var bord in user.desk)
                {
                    if (bord.access == 1)
                    {
                        myPanel.Children.Add(Create_bord(bord, i));
                    }
                    if (bord.access == 0 && bord.whitelist.Contains(this.user.id))
                    {
                        myPanel.Children.Add(Create_bord(bord, i));
                    }
                    i++;
                }
            }
            
            myPanel.Children.Add(button_Add);
            
            return myPanel;
        }


        private void OpenDesk(string name, Desk desk)
        {
            var a = new BoardColumn();
           
            window.Content = a.Draw_Stack(users, name, window, user, desk);
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

        private void AddButton(object sender, RoutedEventArgs e)
        {
            var myPanel = (sender as FrameworkElement).Parent as WrapPanel;
            Desk d = Create.CreateDesk(user.id, 0, new List<int> { user.id });
            myPanel.Children.Insert(myPanel.Children.Count - 1 , Create_bord(d, myPanel.Children.Count - 1));
            users = Read.Reading();
        }

        private void Delete(StackPanel stackPanel, Desk desk, int desk_num)
        {
            for (int i = 0; i < 2; i++)
            {
                if (stackPanel.Children.Count == 2)
                    stackPanel.Children.RemoveAt(0);
                else
                {
                    stackPanel.Children.RemoveAt(0);
                    break;
                }
            }
            users[desk.parrent_id - 1].desk.RemoveAt(desk_num);
            Read.Write(users);
        }

        private void SaveName(Desk desk, int desk_num, Button btn)
        {
            ReName re = new ReName(desk, desk_num, btn);
            re.text.Text = desk.name;
            re.Show();
        }
        private void openAccess(Desk desk, int num_desk, MenuItem item)
        {
            string message =
        "Доступ к доске = " + desk.access.ToString() + " Сделать доску общедоступной?";
            const string caption = "Доступ";
            var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                users[desk.parrent_id - 1].desk[num_desk].access = 1;
                item.Background = new SolidColorBrush(Colors.LightGreen);
            }
            else if (result == MessageBoxResult.No)
            {
                item.Background = new SolidColorBrush(Colors.Transparent);
                users[desk.parrent_id - 1].desk[num_desk].access = 0;
            }
            Read.Write(users);
        }
        private void addUs(Desk desk, int num_desk, MenuItem item, User user)
        {
            if (users[desk.parrent_id - 1].desk[num_desk].whitelist.Contains(user.id))
            {
                users[desk.parrent_id - 1].desk[num_desk].whitelist.Remove(user.id);
                item.Background = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                users[desk.parrent_id - 1].desk[num_desk].whitelist.Add(user.id);
                item.Background = new SolidColorBrush(Colors.LightGreen);
            }
            Read.Write(users);
        }
    }
}
