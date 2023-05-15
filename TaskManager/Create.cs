using Checks;
using read;
using System.Collections.Generic;

namespace create
{
    public class Create
    {
        public static void CreateCards(User user, int desk, int column)
        {
            List<User> users = Read.Reading();
            int number = user.desk[desk].column[column].cards.Count + 1;
            string name = "Card" + number.ToString();
            Card card = new Card(name, "Оцените наше приложение в Play Market!");
            user.desk[desk].column[column].cards.Add(card);
            Read.Write(users);
        }
        public static void CreateColumn(User user, int desk)
        {
            List<User> users = Read.Reading();
            int number = 1;
            if (user.desk[desk].column != null)
            {
                number = user.desk[desk].column.Count + 1;
            }
            string name = "Column" + number.ToString();
            Column column = new Column(name);
            user.desk[desk].column.Add(column);
            Read.Write(users);
        }
        public static void CreateDesk(User user, int access, int[] whitelist)
        {
            List<User> users = Read.Reading();
            int number = 1;
            if (user.desk != null)
            {
                number = user.desk.Count + 1;
            }
            string name = "Desk" + number.ToString();
            Desk desk = new Desk(access, whitelist, name);
            user.desk.Add(desk);
            Read.Write(users);
            for (int i = 0; i < 3; i++)
            {
                CreateColumn(user, number - 1);
            }
        }

        public static void Reg(string login, string password, string email)
        {
            List<User> users = Read.Reading();
            if (checks.Check_Login(login) && checks.Check_newPassword(password) && checks.CheckingForEngaged(login))
            {
                User user = new User(Read.GetId(), login, password, email);
                user.login = login;
                user.password = password;
                user.email = email;
                users.Add(user);
                Read.Write(users);
            }
        }
    }
}