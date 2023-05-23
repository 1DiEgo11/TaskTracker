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
        private WrapPanel content;
        private User user;
        public int id;
        public ScrollViewer Window_with_bords(Window window, List<User> _users, User user)
        {
            this.user = user;
            users = _users;
            this.window = window;
            id = user.id;
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




            CheckBox checkBox = new CheckBox
            {
                Content="Сделать доску общедоступной" //проверка на количество пользователей

            };

            stack.Children.Add(newStack);
            newStack.Children.Add(btn);
            stack.Children.Add(MyText);
            stack.Children.Add(Bords());
            bord.Child = stack;
            return bord;
        }
        
        private StackPanel Create_bord(Desk desk)
        {
            var butPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
            };

            var Text = new TextBox   //проверка на текст, название доски у пользователя
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
            myCart.Click += (s, e) => OpenDesk(desk.column, desk.name, desk);

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
            MenuItem mia = new MenuItem();
            mia.Header = "Удалить доску";
            mia.Click += Delete;

            menu.Items.Add(mi);
            menu.Items.Add(mia);

            MenuItem mi2 = new MenuItem();
            mi2.Header = "Список пользователей"; //вывести список пользователей

            MenuItem mi3 = new MenuItem();
            mi3.Header = new CheckBox() //проверка является ли доска общедоступной
            {
                Content = "Общедоступная"
            };

            miniMenu.Click += (s, e) => { miniMenu.ContextMenu.IsOpen = true; };

            mi.Items.Add(mi2);
            mi.Items.Add(mi3);

            miniMenu.ContextMenu = menu;

            butPanel.Children.Add(myCart);
            butPanel.Children.Add(miniMenu);
            
            return butPanel;
        }

        private WrapPanel Bords() //проверка на количество досок у пользователя
        {
            WrapPanel myPanel = new WrapPanel
            {
                VerticalAlignment = VerticalAlignment.Top,
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Left

            };

            var button_Add = new Button //сколько карточек у пользователя
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
                foreach(var bord in user.desk)
                {
                    if (bord.access == 1)
                    {
                        myPanel.Children.Add(Create_bord(bord));
                    }
                    if (bord.access == 0 && bord.whitelist.Contains(this.user.id))
                    {
                        myPanel.Children.Add(Create_bord(bord));
                    }
                }
            }
            
            myPanel.Children.Add(button_Add);
            
            return myPanel;
        }


        private void OpenDesk(List<Column> columns, string name, Desk desk)
        {
            var a = new BoardColumn();
           
            window.Content = a.Draw_Stack(users, columns, name, window, user, desk);
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
            var d = new Desk(user.id, 0, new int[] {user.id}, "Новая");
            myPanel.Children.Insert(myPanel.Children.Count - 1 , Create_bord(d));
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Parent is ContextMenu contextMenu && contextMenu.PlacementTarget is FrameworkElement targetElement && targetElement.Parent is StackPanel stackPanel)
            {
                for (int i = 0; i < 2; i++)
                {
                    // Remove the first child (assuming it is a StackPanel)
                    stackPanel.Children.RemoveAt(0);
                }
            }
        }

        
    }
}
