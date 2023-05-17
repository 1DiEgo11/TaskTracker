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

namespace TaskManager
{
    internal class Task
    {
        private Button selectedButton;
        private StackPanel cards;
        private int index;
        private Window window;
        private TextBox newText;
        private TextBox Texto;
        private Cards crd;
        
        public ScrollViewer CardSettings(Window window, StackPanel stack ,int index, Cards card)
        {
            crd = card;
            cards = stack;
            this.index = index;
            selectedButton = (Button)stack.Children[index];
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
            window.PreviewKeyDown += ChangeText;

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
            stack.Children.Add(Delete());
            bord.Child = stack;
            return bord;
        }

        private void ChangeText(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                selectedButton.Content = newText.Text.Trim();
            }
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
                Foreground = new SolidColorBrush(Colors.Azure)
            };
            Texto = Descriptor;

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
            deleter.Click += Deleter_Click;
            return deleter;
        }

        private void Deleter_Click(object sender, RoutedEventArgs e)
        {
            cards.Children.RemoveAt(index);
            window.Close();
        }
    }
}
