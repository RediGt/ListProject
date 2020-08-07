using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ListProject
{
    class ProgramRun
    {
        static void Main(string[] args)
        {
            Menu userMenu = new Menu();           
            userMenu = JsonIO.LoadFromFile();

            string userChoice = null;

            userMenu.PrintMenu();

            while (userChoice != "Q")
            {
                userChoice = UserAction();

                switch (userChoice)
                {
                    case "1":
                        userMenu.AddString();
                        break;
                    case "2":
                        userMenu.DeleteString();
                        break;
                    case "Q":
                        JsonIO.SaveToFile(userMenu);
                        break;
                    default:
                        Console.WriteLine("Incorrect input.\n");
                        break;
                }
                userMenu.PrintMenu();
            }
        }

        static string UserAction()
        {
            Console.Write("\nAllowed actions: ");
            Console.WriteLine("\n1 - add menu string\n" +
                "2 - delete menu string\n" +
                "q - exit\n");
            Console.Write("Make your choice: ");

            return Console.ReadLine().ToUpper();
        }       
    }
}
