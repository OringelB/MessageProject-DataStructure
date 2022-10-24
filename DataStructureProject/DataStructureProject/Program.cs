using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureProject
{
    internal class Program
    {
        enum ActionMenu
        {
            SendMessage = 1,
            GetMessageByGroup,
            GetOldestMessage,
            GetAllGroups,
            GetOldestMessages,
            GetMessageByDate,
            SearchMessage,
            Exit
        }
        static void ShowMenu()
        {
            Console.WriteLine("1 - Send a message");
            Console.WriteLine("2 - Read and remove the oldest message by group name");
            Console.WriteLine("3 - Get and remove the oldest message");
            Console.WriteLine("4 - Get all groups names");
            Console.WriteLine("5 - Get messeges number");
            Console.WriteLine("6 - Get messeges by date");
            Console.WriteLine("7 - Search a message");

        }
        static void Main(string[] args)
        {

            ActionMenu choice = ActionMenu.SendMessage;
            string userName;
            string message;
            int messageAmount;
            DateTime date;
            while (choice != ActionMenu.Exit)
            {

                ShowMenu();
                choice = (ActionMenu)Enum.Parse(typeof(ActionMenu), Console.ReadLine()); // add trypare and numbers between 1-X

                switch (choice)
                {
                    case ActionMenu.SendMessage:
                        Console.WriteLine("Please enter the user name that you would like to send a message to:");
                        userName = Console.ReadLine();
                        Console.WriteLine("Please type your message");
                        message = Console.ReadLine();
                        User.SendMessage(userName, message);
                        break;
                    case ActionMenu.GetMessageByGroup:
                        Console.WriteLine("Please enter the user name");
                        userName = Console.ReadLine();
                        User.GetAndRemoveMessage(userName);
                        break;
                    case ActionMenu.GetOldestMessage:
                        User.GetOldestMessage();
                        break;
                    case ActionMenu.GetAllGroups:
                        User.GetAllGroups();
                        break;
                    case ActionMenu.GetOldestMessages:
                        Console.WriteLine("Please choose the amount of messages that you want:");
                        messageAmount = int.Parse(Console.ReadLine());
                        User.GetOldestMessages(messageAmount);
                        break;
                    case ActionMenu.GetMessageByDate:
                            Console.WriteLine("Please state the year (1-9999)");
                        if(!int.TryParse(Console.ReadLine(), out int year))
                        {
                            Console.WriteLine("You have to enter a number");
                            break;
                        }

                        Console.WriteLine("Please state the month");
                        if(!int.TryParse(Console.ReadLine(), out int month))
                        {
                            Console.WriteLine("You have to enter a number");
                            break;
                        }
                        Console.WriteLine("Please state the day");
                        if(!int.TryParse(Console.ReadLine(), out int day))
                        {
                            Console.WriteLine("You have to enter a number");
                            break;
                        }
                        try
                        {
                            date = new DateTime(year, month, day);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("There is something wrong with your date");
                            break;
                        }
                        date = new DateTime(year, month, day);
                        User.GetMessagesByDate(date);
                        break;
                    case ActionMenu.SearchMessage:
                        Console.WriteLine("Please type group name");
                        userName = Console.ReadLine();
                        Console.WriteLine("Please type what would you like to search");
                        message = Console.ReadLine();
                        User.GetMessageByGroupAndContent(userName, message);
                        break;
                }

            }
        }
    }
}
