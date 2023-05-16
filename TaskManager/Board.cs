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
        public ScrollViewer Draw_Stack(Window window)
        {
            StackPanel myStackPanel = new StackPanel();
            this.window = window;
            var myScrollViewer = new ScrollViewer
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                Content = Stack(myStackPanel)
            };

            return myScrollViewer;
        }

        //private StackPanel MyBorder()
        //{
        //    StackPanel myStackPanel = new StackPanel();
        //    myStackPanel = Stack(myStackPanel);
        //    return myStackPanel;
        //} 
        
        private Border Stack(StackPanel stack)
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
            stack.Children.Add(Column());
            bord.Child = stack;
            return bord;
        }
        
        private Button Create()
        {
            
            var Text = new TextBox
            {
                Width = 70,
                Text ="Доска 1",
            };
            var myCart = new Button
            {
                Background = new SolidColorBrush(Colors.Gray),
                Margin = new Thickness(30),
                Width = 100,
                Height = 100,
                Content = Text
            };
            myCart.Click += OpenDesk;

            
            return myCart;
        }

        private WrapPanel Column()
        {
            WrapPanel myPanel = new WrapPanel
            {
                VerticalAlignment = VerticalAlignment.Top,
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center

            };
            var Text = new TextBox
            {
                Width = 70,
                Text = "Доска 1",
            };

            var myCart = new Button
            {
                Background = new SolidColorBrush(Colors.Gray),
                Margin = new Thickness(30),
                Width = 100,
                Height = 100,
                Content = Text
            };
            myCart.Click += OpenDesk;

            var button_Add = new Button
            {
                Margin = new Thickness(30),
                Width = 100,
                Height = 100,
                FontSize = 30,
                Content = "+"
            };

            
            myPanel.Children.Add(button_Add);
            myPanel.Children.Add(myCart);
            myPanel.Children.Add(Create());
            myPanel.Children.Add(Create());
            myPanel.Children.Add(Create());
            myPanel.Children.Add(Create());
            myPanel.Children.Add(Create());
            myPanel.Children.Add(Create());
            myPanel.Children.Add(Create());
            myPanel.Children.Add(Create());
            
            return myPanel;
        }


        private void OpenDesk(object sender, RoutedEventArgs e)
        {
            window.Content = BoardColumn.Draw_Stack();
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
