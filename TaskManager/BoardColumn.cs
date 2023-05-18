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
        private List<User> users;
        private List<Column> col;
        private string nameB;
        public ScrollViewer Draw_Stack(List<User> _users, List<Column> columns, string name_of_board)
        {
            users = _users;
            col = columns;
            nameB = name_of_board;

            var myScrollViewer = new ScrollViewer
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                Content = VertStack()
            };

            return myScrollViewer;
        }
        private StackPanel VertStack()
        {
            var nameBoard = new TextBlock
            {
                Margin = new Thickness(4, 5, 30, 0),
                Text = "Столбцы из " + nameB,
                FontSize = 20
            };

            StackPanel panel = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            panel.Children.Add(nameBoard);
            panel.Children.Add(All_Coloum());
            return panel;
        }

        private StackPanel All_Coloum()
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
            foreach (var c in col)
            {
                myStackPanel.Children.Add(Column(c.cards));
            }

            myStackPanel.Children.Add(Jopumn());
            
            return myStackPanel;
        }

        private void Task_Click(StackPanel cards, int index, Cards card)
        {
            TaskWindow taskWindow = new TaskWindow(cards, index, card);
            taskWindow.Show();
            //Сохранение изменений карточки карточки
        }

        private Border Column(List<Cards> cards_all)
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
            if (cards_all != null)
            {
                foreach (var card in cards_all)
                {
                    card.btn.Click += (s, e) => Task_Click(myStack, myStack.Children.Count, card);
                    myStack.Children.Add(card.btn);
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
            var newCard = new Cards("Новая карточка", null);
            newCard.btn.Click += (s, e) => Task_Click(stack, stack.Children.Count - 1, newCard);
            stack.Children.Insert(stack.Children.Count - 1, newCard.btn);
            TaskWindow taskWindow = new TaskWindow(stack, stack.Children.Count - 2, newCard);
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
