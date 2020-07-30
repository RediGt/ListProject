using System;
using System.Collections.Generic;

namespace ListProject
{
    class Program
    {
        static List<String> menu = new List<String>();
        static string userChoice;

        static void Main(string[] args)
        {
            InitMenu();
            PrintMenu();

            while (userChoice != "q" && userChoice != "Q")
            {
                UserAction();
                
                switch(userChoice)
                {
                    case "1":
                        AddString();
                        break;
                    case "2":
                        DeleteString();
                        break;
                    case "q":
                    case "Q":
                        break;
                    default:
                        Console.WriteLine("Incorrect input.\n");
                        break;
                }
                PrintMenu();
            }
        }

        static void InitMenu()
        {
            menu.Add("New");
            menu.Add("Exit");
        }

        static void AddString()
        {
            Console.Write("Input menu string: ");
            string menuStr = Console.ReadLine();
            menu.Add(menuStr);
        }

        static void DeleteString()
        {
            bool correctInput = false;
            string menuStr;
            do
            {
                Console.Write("Input number of menu string to delete: ");
                menuStr = Console.ReadLine();

                for (int i = 0; i < menu.Count; i++)
                {
                    if (menuStr == Convert.ToString(i + 1))
                        correctInput = true;
                }
            }
            while (!correctInput);

            menu.RemoveAt(Convert.ToInt32(menuStr) - 1);
        }

        static void PrintMenu()
        {
            Console.WriteLine("Program MENU:");
            for (int i = 0; i < menu.Count; i++)
            {
                Console.Write("   - " + (i + 1) + ". ");
                Console.WriteLine(menu[i]);
            }
        }

        static void UserAction()
        {      
                Console.WriteLine("\n1 - add menu string\n" +
                    "2 - delete menu string\n" +
                    "q - exit\n");
                Console.Write("Make your choice: ");
                userChoice = Console.ReadLine();
        }
    }
}
