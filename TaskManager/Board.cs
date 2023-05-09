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

namespace TaskManager
{
    internal class Board
    {
        public static ScrollViewer Draw_Stack()
        {
            var myScrollViewer = new ScrollViewer
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto
            };
            myScrollViewer.Content = MyBorder();

            return myScrollViewer;
        }

        private static StackPanel MyBorder()
        {
            StackPanel myStackPanel = new StackPanel();
            myStackPanel.Orientation = Orientation.Horizontal;
            return Stack(myStackPanel);
        } 
        
        private static StackPanel Stack(StackPanel stack)
        {
            stack.Children.Add(Column());
            return stack;
        }

        private static Border Column()
        {
            Border bord = new Border
            {
                Width = 700,
                Height = 400,
                Margin = new Thickness(150, 20, 0, 0),
                Background = new SolidColorBrush(Colors.White),
                Padding = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Left,
                CornerRadius = new CornerRadius(5),
                Effect = new DropShadowEffect { BlurRadius = 30, Color = Colors.Black, ShadowDepth = 0 }
                
            };

            

            var MyText = new TextBlock 
            {
               FontSize = 20,
               Margin = new Thickness(30,30,500,0),
               Text = "Мои доски",
             };

            WrapPanel myPanel = new WrapPanel();
            myPanel.VerticalAlignment = VerticalAlignment.Top;
            myPanel.Orientation = Orientation.Horizontal;
            myPanel.HorizontalAlignment = HorizontalAlignment.Left;

            var myCart = new Button
            {
                Background = new SolidColorBrush(Colors.Gray),
                Margin = new Thickness(30),
                Width = 100,
                Height = 100,
                FontSize = 30,
                Content = "+"
            };

            var button_Add = new Button
            {
                Margin = new Thickness(30),
                Width = 100,
                Height = 100,
                Content = "+ Карточка"
            };


            
            myPanel.Children.Add(MyText);
            myPanel.Children.Add(myCart);
            myPanel.Children.Add(button_Add);
            bord.Child = myPanel;
            return bord;
        }
    }
}
