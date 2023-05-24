using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows.Input;
using System.Collections;
using System.Windows.Documents;
using System.Xml.Linq;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace TaskManager
{
    internal class Task
    {
        private Button selectedButton;
        private StackPanel cards;
        private Window window;
        private TextBox newText;
        private TextBox descript;
        private Cards crd;
        private List<User> users = Read.Reading();
        private User user;
        Desk desk; 
        int num_col;


        public ScrollViewer CardSettings(Window window, StackPanel stack, Cards card, User user, Desk desk, int num_col)
        {
            this.num_col = num_col;
            this.desk = desk;
            this.user = user;
            crd = card;
            cards = stack;
            selectedButton = card.btn;
            this.window = window;
            
            StackPanel myStackPanel = new StackPanel();
            var myScrollViewer = new ScrollViewer
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                Content = Stack(myStackPanel)
            };

            return myScrollViewer;
        }
        private Border Stack(StackPanel stack)
        {
            Border bord = new Border
            {
                Width = 600,
                Margin = new Thickness(20),
                Background = new SolidColorBrush(Colors.White),
                Padding = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Center,
                CornerRadius = new CornerRadius(5),
                Effect = new DropShadowEffect { BlurRadius = 30, Color = Colors.Black, ShadowDepth = 0 }
            };

            var save = new Button
            {
                Background = new SolidColorBrush(Colors.Green),
                Margin = new Thickness(3),
                Width = 120,
                Height = 40,
                Content = "Сохранить"
            };
            save.Click += ChangeText; 
            

            var RenameText = new TextBlock
            {
                Margin = new Thickness(30, 30, 30, 0),
                Text = "Переименовать:",
                FontSize = 20
            };
            var Rename = new TextBox
            {
                Text = selectedButton.Content as string,
                Background = new SolidColorBrush(Colors.ForestGreen),
                Foreground = new SolidColorBrush(Colors.Azure)
            };
            newText = Rename;

            var Description = new TextBlock
            {
                Margin = new Thickness(30, 30, 30, 0),
                Text = "Описание:",
                FontSize = 20
            };

            var ChangeColorText = new TextBlock
            {
                Margin = new Thickness(30, 30, 30, 0),
                Text = "Поменять цвет:",
                FontSize = 20
            };
        
            stack.Children.Add(RenameText);
            stack.Children.Add(Rename);
            stack.Children.Add(Description);
            stack.Children.Add(Descriptione());
            stack.Children.Add(ChangeColorText);
            stack.Children.Add(Horizontal());
            stack.Children.Add(save);
            stack.Children.Add(Delete());
            bord.Child = stack;
            return bord;
        }

        private void ChangeText(object sender, RoutedEventArgs e)
        {

            selectedButton.Content = newText.Text.Trim();
            crd.description = descript.Text;
            crd.name = newText.Text.Trim();

            users[crd.path[0]].desk[crd.path[1]].column[crd.path[2]].cards[crd.path[3]] = crd;
            Read.Write(users);
            
            window.Close();
        }

        private TextBox Descriptione()
        {
            TextBox Descriptor = new TextBox
            {
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Top,
                TextWrapping = TextWrapping.Wrap,
                Background = new SolidColorBrush(Colors.Purple),
                Height = 250,
                Foreground = new SolidColorBrush(Colors.Azure),
                Text = crd.description
            };
            descript = Descriptor;

            return Descriptor;
        }
        

        private WrapPanel Horizontal()
        {
            WrapPanel myPanel = new WrapPanel
            {
                VerticalAlignment = VerticalAlignment.Bottom,
                Orientation = Orientation.Horizontal
            };
            

            var Purple = new Button
            {
                Background = new SolidColorBrush(Colors.Purple),
                Margin = new Thickness(12.5),
                Width = 120,
                Height = 40,
                Content = "Фиолетовый"
            };
            Purple.Click += (s, e) => PickColor(Purple.Background);

            var Blue = new Button
            {
                Background = new SolidColorBrush(Colors.Blue),
                Margin = new Thickness(12.5),
                Width = 120,
                Height = 40,
                Content = "Синий"
            };
            Blue.Click += (s, e) => PickColor(Blue.Background);

            var Red = new Button
            {
                Background = new SolidColorBrush(Colors.PaleVioletRed),
                Margin = new Thickness(12.5),
                Width = 120,
                Height = 40,
                Content = "Розовый"
            };
            Red.Click += (s, e) => PickColor(Red.Background);

            var Green = new Button
            {
                Background = new SolidColorBrush(Colors.SeaGreen),
                Margin = new Thickness(12.5),
                Width = 120,
                Height = 40,
                Content = "Зелёный"
            };
            Green.Click += (s, e) => PickColor(Green.Background);

            myPanel.Children.Add(Green);
            myPanel.Children.Add(Purple);
            myPanel.Children.Add(Blue);
            myPanel.Children.Add(Red);

            return myPanel;
        }
        private void PickColor(Brush color)
        {
            selectedButton.Background = color;
            crd.colour = "#" + selectedButton.Background.ToString().Substring(3);
        }
        private Button Delete()
        {
            var deleter = new Button
            {
                Background = new SolidColorBrush(Colors.Red),
                Margin = new Thickness(30),
                Width = 120,
                Height = 40,
                Content = "Удалить"
            };
            deleter.Click += (s, e) => Deleter_Click();
            return deleter;
        }

        private void Deleter_Click()
        {
            cards.Children.Remove(crd.btn);
            users[crd.path[0]].desk[crd.path[1]].column[crd.path[2]].cards.RemoveAt(crd.path[3]);
            Read.Write(users);
            
            window.Close();
        }
    }
}
