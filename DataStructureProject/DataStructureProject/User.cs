using DataStructures;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureProject
{
    static internal class User
    {
        static Manager m = new Manager();

        public static void SendMessage(string groupName, String message)
        {
            m.SendMessageToGroup(groupName, message);
            Console.WriteLine("You have succefully sent a message");
        }
        public static void GetAndRemoveMessage(string groupName)
        {
            if (m.GetAndRemoveMessageFromGroup(groupName, out Message msg))
            {
                Console.WriteLine($"The message is:{msg.MessageData}");
            }
            else
            {
                Console.WriteLine("Wrong username/no more messages");
            }

        }
        public static void GetOldestMessage()
        {
            if(m.CutOldestMessage(out Message msg))
            {
                Console.WriteLine($"The oldest message is:{msg.MessageData} from group: {msg.Name}, the message has been removed");
            }
            else
            {
                Console.WriteLine("No more messages");
            }
        }

        internal static void GetAllGroups()
        {
            if (m.GetAllGroupNames(out string[] allGroups))
            {
                foreach (string group in allGroups)
                {
                    Console.WriteLine(group);
                }
            }
            else
            {
                Console.WriteLine("No groups");
            }
        }

        internal static void GetOldestMessages(int messageAmount)
        {
            if (m.GetOldestMessages(messageAmount, out Message[] messegas))
            {
                foreach (Message msg in messegas)
                {
                    Console.WriteLine(msg.MessageData);
                }
            }
            else
            {
                Console.WriteLine("There are no messages");
            }

        }

        internal static void GetMessagesByDate(DateTime date)
        {
            m.GetMessagesFromDate(date, out DataStructures.LinkedList<Message> messegas);
            foreach(Message msg in messegas)
            {
                Console.WriteLine($"Group name: {msg.Name}, Message content:{msg.MessageData}, Message date sent:{msg.DateSent}");
            }
        }
        internal static void GetMessageByGroupAndContent(string groupName,string contant)
        {
            if (m.ReadMessagesByGroupAndContent(groupName, contant, out DataStructures.LinkedList<Message> allMessages))
            {
                foreach (Message msg in allMessages)
                {
                    Console.WriteLine($"Group name: {msg.Name}, Message content:{msg.MessageData}, Message date sent:{msg.DateSent}");
                }
            }
            else
            {
                Console.WriteLine("Theres not such message/group name");
            }

        }
    }
}
