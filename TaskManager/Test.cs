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
        private StackPanel myStackPanel;
        private int index = 0;
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

            myStackPanel = Stack();

            return myStackPanel;
        }
  
        private StackPanel Stack()
        { 
            List<string> list = new List<string>{"Сделать кушать", "Поспать"};
            myStackPanel.Children.Add(Column(list));
            myStackPanel.Children.Add(Column(list));
            myStackPanel.Children.Add(Column(list));
            myStackPanel.Children.Add(Column(list));
            myStackPanel.Children.Add(Jopumn());
            return myStackPanel;
        }
        private Button Card(string s)
        {
            var task = new Button
            {
                Background = new SolidColorBrush(Colors.Blue),
                Margin = new Thickness(10),
                Width = 225,
                Height = 40,
                Content = s
            };
            return task;
        }
        private Border Column(List<string> names)
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
            if (names != null)
            {
                foreach (string name in names)
                {
                    myStack.Children.Add(Card(name));
                }
            }

            var plus = new Button
            {
                Margin = new Thickness(20),
                Width = 225,
                Height = 40,
                Content = "+ Карточка"
            };
            plus.Click += (s, e) => PlusCard(myStack);
            plus.Click += new RoutedEventHandler(plus_Click);


            myStack.Children.Add(plus);
            bord.Child = myStack;

            return bord;
        }
        public void plus_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow taskWindow = new TaskWindow();
            taskWindow.Show();

        }

        private void PlusCard(StackPanel stack)
        {
            //StackPanel stack = (StackPanel)((Border)myStackPanel.Children[index]).Child;
            stack.Children.Insert(stack.Children.Count - 1, Card("Новая Карточка"));
        }

        private Border Jopumn()
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
        private void AddJopumn_Click(object sender, RoutedEventArgs e)
        {
            if (myStackPanel.Children.Count - 1 < 10)
            {
                myStackPanel.Children.Insert(myStackPanel.Children.Count - 1, Column(null));
                if(myStackPanel.Children.Count - 1 == 10)
                    myStackPanel.Children.RemoveAt(myStackPanel.Children.Count - 1);
            }
        }
    }
}
