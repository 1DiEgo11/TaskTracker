using System;
using System.Collections.Generic;
using System.Data.Common;

namespace TaskManager
{
    public class Create
    {
        private static string Name(string name, int number)
        {
            return name + number.ToString();
        }

        private static int Number(int command, List<Desk> desk = null, List<Column> column = null, List<Cards> card = null)
        {
            switch (command)
            {
                case 0:
                    if (card == null)
                    {
                        return 1;
                    }
                    return card.Count+1;
                case 1:
                    if (column == null)
                    {
                        return 1;
                    }
                    return column.Count+1;
                case 2:
                    if (desk == null)
                    {
                        return 1;
                    }
                    return desk.Count + 1;
            }
            return 0;
        }
        public static void CreateCards(int id, int desk, int column)
        {
            List<User> users = Read.Reading();
            int number = Number(0, card: users[id - 1].desk[desk].column[column].cards);
            int[] path = new int[4] { id - 1, desk, column, 0 }; ;
            path[3] = Math.Abs(number-1);
            string name = Name("Card",number);
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
            int number = Number(1, column: users[id - 1].desk[desk].column);
            string name = Name("Column", number);
            Column column = new Column(name);
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

            int number = Number(2, users[id - 1].desk);
            string name = Name("Desk", number);
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