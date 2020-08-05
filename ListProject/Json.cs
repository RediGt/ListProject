using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using Newtonsoft.Json;

namespace ListProject
{
    class Json
    {
        static void Main(string[] args)
        {
            int timesModified = 0;
            DateTime dtModified = new DateTime();

            List<String> menu = LoadMenu();
            string userChoice = null;

            PrintMenu(menu);

            while (userChoice != "Q")
            {
                userChoice = UserAction();

                switch (userChoice)
                {
                    case "1":
                        AddString(menu, ref timesModified, ref dtModified);
                        break;
                    case "2":
                        DeleteString(menu, ref timesModified, ref dtModified);
                        break;
                    case "Q":
                        SaveMenu(menu, ref timesModified, ref dtModified);
                        break;
                    default:
                        Console.WriteLine("Incorrect input.\n");
                        break;
                }
                PrintMenu(menu);
            }
        }

        static List<string> InitMenu()
        {
            List<string> menu = new List<string>();
            menu.Add("New");
            menu.Add("Exit");
            return menu;
        }

        static void AddString(List<string> menu, ref int timesModified, ref DateTime dtModified)
        {
            Console.Write("Input menu string: ");
            string menuStr = Console.ReadLine();
            menu.Add(menuStr);
            Console.WriteLine();
            timesModified++;
            dtModified = DateTime.Now;
        }

        static void DeleteString(List<string> menu, ref int timesModified, ref DateTime dtModified)
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
            timesModified++;
            dtModified = DateTime.Now;
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

            return Console.ReadLine().ToUpper();
        }       
       
        static void SaveMenu(List<string> menu, ref int timesModified, ref DateTime dtModified)
        {
            String json = JsonConvert.SerializeObject(menu, Newtonsoft.Json.Formatting.Indented);

            try
            {
                StreamWriter sw = new StreamWriter(GetMenuFile());
                sw.Write(json);
                sw.Close();
            }
            catch
            {
                Console.WriteLine("Fail!");
            }
        }

        static List<string> LoadMenu()//List<string> menu)//, ref int timesModified, ref DateTime dtModified)
        {
            try
            {
                StreamReader reader = new StreamReader(GetMenuFile());
                String line = reader.ReadLine();
                String json = "";

                while (line != null)
                {
                    json += line;
                    line = reader.ReadLine();
                }
                reader.Close();

                List<string> menu = JsonConvert.DeserializeObject<List<string>>(json);                
                return menu;
            }
            catch
            {
                List<string> menu = InitMenu();
                return menu;
            }
        }

        public static string GetMenuFile()
        {
            string filename = Directory.GetCurrentDirectory();
            filename += @"\Menu.json";

            return filename;
        }
    }
}
