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

            // Add Layout control
            var myStackPanel = new StackPanel();

            Border bord = new Border
            {
                Margin = new Thickness(10),
                Background = new SolidColorBrush(Colors.White),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                MinWidth = 40,
                MaxWidth = 140,
                Padding = new Thickness(3),
                CornerRadius = new CornerRadius(5),

                Effect =  new DropShadowEffect { BlurRadius = 30, Color = Colors.Black, ShadowDepth = 0 }
            };
            
            

            TextBlock myTextBlock = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(20),
                Width = 100,
                Height = 20,
                Text = "Scrolling is enabled when it is necessary. Resize the Window, making it larger and smaller."
            };

            var button_Add = new Button
            {
                Margin = new Thickness(20),
                Width = 118,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Content = "+ Карточка"

            };

            
            // Add child elements to the parent StackPanel
            myStackPanel.Children.Add(myTextBlock);
            myStackPanel.Children.Add(button_Add);
            bord.Child = myStackPanel;

            // Add the StackPanel as the lone Child of the Border
            myScrollViewer.Content = bord;

            // Add the Border as the Content of the Parent Window Object
            return myScrollViewer;
        }
    }
}
