using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ListProject
{
    class JsonMenu
    {
        static void Main(string[] args)
        {
            Menu userMenu = new Menu();
            /*userMenu.menu.Add("New");
            userMenu.menu.Add("Load");
            userMenu.timesModified = 1;
            userMenu.dtModified = DateTime.Now;*/
            
            userMenu = LoadFromFile();

            //SaveToFile(userMenu);

            /*tring jsonString;
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true
            };
            Menu newMenu = new Menu();
            newMenu = System.Text.Json.JsonSerializer.Deserialize<Menu>(jsonString, options);
            Console.WriteLine(newMenu.dtModified);
            //Console.WriteLine(newMenu.menu[2]);*/

            /*Menu userMenu = new Menu();*/
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
                        SaveToFile(userMenu);
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

        static void SaveToFile(Menu userMenu)
        {
            string jsonString;
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            jsonString = System.Text.Json.JsonSerializer.Serialize(userMenu, options);

            Console.WriteLine(jsonString);
            File.WriteAllText(Menu.GetMenuFile(), jsonString);
        }

        static Menu LoadFromFile()
        {
            try
            {
                StreamReader reader = new StreamReader(Menu.GetMenuFile());
                String line = reader.ReadLine();
                String json = "";

                while (line != null)
                {
                    json += line;
                    line = reader.ReadLine();
                }
                reader.Close();

                var options = new JsonSerializerOptions
                {
                    IgnoreNullValues = true
                };
                return System.Text.Json.JsonSerializer.Deserialize<Menu>(json, options);
            }
            catch
            {
                //InitMenu();userMenu.menu.Add("New");
                Menu userMenu = new Menu();
                userMenu.menu.Add("Error");
                userMenu.menu.Add("Load");
                userMenu.timesModified = 1;
                userMenu.dtModified = DateTime.Now;
                return userMenu;
            }
        }
    }
}
