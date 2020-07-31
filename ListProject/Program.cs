using System;
using System.Collections.Generic;

namespace ListProject
{
    class Program
    {       
        static void Main(string[] args)
        {
            List<String> menu = new List<String>();
            string userChoice = null;
            
            InitMenu(menu);
            PrintMenu(menu);

            while (userChoice != "q" && userChoice != "Q")
            {
                userChoice = UserAction();
                
                switch(userChoice)
                {
                    case "1":
                        AddString(menu);
                        break;
                    case "2":
                        DeleteString(menu);
                        break;
                    case "q":
                    case "Q":
                        break;
                    default:
                        Console.WriteLine("Incorrect input.\n");
                        break;
                }
                PrintMenu(menu);
            }
        }

        static void InitMenu(List<string> menu)
        {
            menu.Add("New");
            menu.Add("Exit");
        }

        static void AddString(List<string> menu)
        {
            Console.Write("Input menu string: ");
            string menuStr = Console.ReadLine();
            menu.Add(menuStr);
            Console.WriteLine();
        }

        static void DeleteString(List<string> menu)
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

        static void PrintMenu(List<string> menu)
        {
            Console.WriteLine("Program MENU:");
            for (int i = 0; i < menu.Count; i++)
            {
                Console.Write("   - " + (i + 1) + ". ");
                Console.WriteLine(menu[i]);
            }
        }

        static string UserAction()
        {           
            Console.Write("\nAllowed actions: ");
            Console.WriteLine("\n1 - add menu string\n" +
                "2 - delete menu string\n" +
                "q - exit\n");
            Console.Write("Make your choice: ");

            return Console.ReadLine();
        }
    }
}
