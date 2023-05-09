using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media.Effects;

namespace TaskManager
{
    internal class Test
    {
        public StackPanel myStackPanel;
        public static ScrollViewer Draw_Stack()
        {
            var a = new Test();
            var myScrollViewer = new ScrollViewer
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto
            };

            myScrollViewer.Content = a.MyBorder();

            return myScrollViewer;
        }

        private StackPanel MyBorder()
        {
            myStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            myStackPanel = Stack(myStackPanel);

            //move_button.Click += (s, a) => Moving_cards.Move(myStackPanel); 

            return myStackPanel;
        }
  
        private static StackPanel Stack( StackPanel stack)
        { 
            stack.Children.Add(Column());
            stack.Children.Add(Column());
            stack.Children.Add(Column());
            stack.Children.Add(Column());
            stack.Children.Add(Jopumn());
            return stack;
        }
        private static Border Column()
        {
            Border bord = new Border
            {
                Margin = new Thickness(10),
                Background = new SolidColorBrush(Colors.White),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Padding = new Thickness(3),
                CornerRadius = new CornerRadius(5),
                Effect = new DropShadowEffect { BlurRadius = 30, Color = Colors.Black, ShadowDepth = 0 }
            };

            StackPanel myStack = new StackPanel();

            var WhatToDo = new Button
            {
                Background = new SolidColorBrush(Colors.Blue),
                Margin = new Thickness(10),
                Width = 225,
                Height = 40,
                Content = "Сделать покушать!"
            };

            var WhatToDo2 = new Button
            {
                Background = new SolidColorBrush(Colors.Blue),
                Margin = new Thickness(10),
                Width = 225,
                Height = 40,
                Content = "Скушать кушанье!"
            };

            var Plus = new Button
            {
                Margin = new Thickness(20),
                Width = 225,
                Height = 40,
                Content = "+ Карточка"
            };

            myStack.Children.Add(WhatToDo);
            myStack.Children.Add(WhatToDo2);
            myStack.Children.Add(Plus);
            bord.Child = myStack;

            return bord;
        }
        private static Border Jopumn()
        {
            Border Man = new Border
            {
                Margin = new Thickness(10),
                Background = new SolidColorBrush(Colors.White),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right,
                Padding = new Thickness(3),
                CornerRadius = new CornerRadius(5),
                Effect = new DropShadowEffect { BlurRadius = 30, Color = Colors.Black, ShadowDepth = 0 }
            };
            StackPanel Bruh = new StackPanel();

            Button AddJopumn = new Button
            {
                Background = new SolidColorBrush(Colors.Blue),
                Margin = new Thickness(10),
                Width = 50,
                Height = 40,
                Content = "+"
            };
            AddJopumn.Click += new RoutedEventHandler(AddJopumn_Click);

            Bruh.Children.Add(AddJopumn);
            Man.Child = Bruh;
            return Man;

        }
        private static void AddJopumn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hi!");
        }
    }
}
