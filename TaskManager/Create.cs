using System.Collections.Generic;
using System.Windows;

namespace TaskManager
{
    public class Create
    {
        public static void CreateCards(int id, int desk, int column)
        {
            List<User> users = Read.Reading();
            int number = 1;
            int[] path = new int[4] { id - 1, desk, column, 0 }; ;
            if (users[id - 1].desk[desk].column[column].cards != null)
            {
                number = users[id - 1].desk[desk].column[column].cards.Count + 1;
                path[3] = number - 2;
            }
            string name = "Card" + number.ToString();
            Cards card = new Cards(name, "Оцените наше приложение в Play Market!", "#0000FF", path);
            card.path = path;
            if (users[id - 1].desk[desk].column[column].cards == null)
            {
                users[id - 1].desk[desk].column[column].cards = new List<Cards> { card };
            }
            else
            {
                users[id - 1].desk[desk].column[column].cards.Add(card);
            }
            Read.Write(users);
        }
        public static void CreateColumn(int id, int desk)
        {
            List<User> users = Read.Reading();
            
            string name = "New Bord";
            Column column = new Column(name, new List<Cards> {});

            if (users[id - 1].desk[desk].column == null)
            {
                users[id - 1].desk[desk].column = new List<Column> { column };
            }
            else
            {
                users[id - 1].desk[desk].column.Add(column);
            }
            Read.Write(users);
        }
        public static Desk CreateDesk(int id, int access, int[] whitelist)
        {
            List<User> users = Read.Reading();
            int number = 1;
            if (users[id - 1].desk != null)
            {
                number = users[id - 1].desk.Count + 1;
            }
            string name = "Desk" + number.ToString();
            Desk desk = new Desk( id, access, whitelist, name);
            if (users[id - 1].desk == null)
            {
                users[id - 1].desk = new List<Desk> { desk };
            }
            else
            {
                users[id - 1].desk.Add(desk);
            }
            Read.Write(users);
            for (int i = 0; i < 3; i++)
            {
                CreateColumn(id, number - 1);
            }
            users = Read.Reading();
            desk.column = users[id - 1].desk[number - 1].column;
            Read.Write(users);
            return desk;
        }

        public static User Reg(string login, string password, string email)
        {
            List<User> users = Read.Reading();
            if (checks.Check_Login(login) && checks.Check_newPassword(password) && checks.CheckingForEngaged(login))
            {
                User user = new User(Read.GetId(), login, password, email);
                users.Add(user);
                Read.Write(users);
                for (int i=0; i<3; i++) 
                {
                    CreateDesk(user.id, 0, new int[] { user.id });
                }
                return user;
            }
            return null;
        }
    }
}