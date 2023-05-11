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
    internal class BoardColumn
    {
        private StackPanel myStackPanel;
        public static ScrollViewer Draw_Stack()
        {
            var a = new BoardColumn();
            var myScrollViewer = new ScrollViewer
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                Content = a.VertStack()
            };

            return myScrollViewer;
        }
        private StackPanel VertStack()
        {

            var nameBoard = new TextBlock
            {
                Margin = new Thickness(4, 5, 30, 0),
                Text = "Столбцы из ...",
                FontSize = 20
            };
            var chBox = new CheckBox
            {
                Content = "Сделать доску общедоступной"
            };
            
            
            StackPanel panel = new StackPanel
            {
                Orientation = Orientation.Vertical
            };
            panel.Children.Add(nameBoard);
            panel.Children.Add(All_Cards());
            panel.Children.Add(chBox);
            return panel;
        }

        private StackPanel All_Cards()
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
        private Button Card(string name)
        {
            var task = new Button
            {
                Background = new SolidColorBrush(Colors.Blue),
                Margin = new Thickness(10),
                Width = 225,
                Height = 40,
                Content = name
            };
            task.Click += (s, e) => Task_Click(task);
            return task;
        }

        private void Task_Click(Button card)
        {
            TaskWindow taskWindow = new TaskWindow(card);
            taskWindow.Show();
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

            myStack.Children.Add(plus);
            bord.Child = myStack;

            return bord;
        }
        private void PlusCard(StackPanel stack)
        {
            var newCard = Card("Новая карточка");
            stack.Children.Insert(stack.Children.Count - 1, newCard);
            TaskWindow taskWindow = new TaskWindow(newCard);
            taskWindow.Show();
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
