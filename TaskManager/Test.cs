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
        public static ScrollViewer Draw_Stack()
        {
            var myScrollViewer = new ScrollViewer
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto
            };

            // Add the StackPanel as the lone Child of the Border
            myScrollViewer.Content = MyBorder();

            // Add the Border as the Content of the Parent Window Object
            return myScrollViewer;
        }

        private static StackPanel MyBorder()
        {
            StackPanel myStackPanel = new StackPanel();

            myStackPanel.Orientation = Orientation.Horizontal;

            



            return Stack(myStackPanel);
        }

        private static StackPanel Stack( StackPanel stack)
        { 
            stack.Children.Add(Column());
            stack.Children.Add(Column());
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

            var myCart = new Button
            {
                Background = new SolidColorBrush(Colors.Blue),
                Margin = new Thickness(10),
                Width = 200,
                Height = 40,
                Content = "Сделать покушать!"
            };

            var button_Add = new Button
            {
                Margin = new Thickness(20),
                Width = 200,
                Height = 40,
                Content = "+ Карточка"
            };

            myStack.Children.Add(myCart);
            myStack.Children.Add(button_Add);
            bord.Child = myStack;
            return bord;
        }
    }
}
