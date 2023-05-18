using TaskManager;
//using ;
using System.Collections.Generic;

namespace TaskManager
{
    public class Create
    {
        public static void CreateCards(int id, int desk, int column)
        {
            List<User> users = Read.Reading();
            int number = 1;
            if (users[id - 1].desk[desk].column[column].cards != null)
            {
                number = users[id - 1].desk[desk].column[column].cards.Count + 1;
            }
            string name = "Card" + number.ToString();
            Cards card = new Cards(name, "Оцените наше приложение в Play Market!");
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
            int number = 1;
            if (users[id - 1].desk[desk].column != null)
            {
                number = users[id - 1].desk[desk].column.Count + 1;
            }
            string name = "Column" + number.ToString();
            Column column = new Column(name);
            //Console.WriteLine(user.desk[desk].name);
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
        public static void CreateDesk(int id, int access, int[] whitelist)
        {
            List<User> users = Read.Reading();
            int number = 1;
            if (users[id - 1].desk != null)
            {
                number = users[id - 1].desk.Count + 1;
            }
            string name = "Desk" + number.ToString();
            Desk desk = new Desk(access, whitelist, name);
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
        }

        public static User Reg(string login, string password, string email)
        {
            List<User> users = Read.Reading();
            if (checks.Check_Login(login) && checks.Check_newPassword(password) && checks.CheckingForEngaged(login))
            {
                User user = new User(Read.GetId(), login, password, email)
                {
                    login = login,
                    password = password,
                    email = email
                };
                for (int i=0; i<3; i++) 
                {
                    CreateDesk(user.id, 0, new int[] { user.id });
                }
                users.Add(user);
                Read.Write(users);
                return user;
            }
            return null;
        }
    }
}