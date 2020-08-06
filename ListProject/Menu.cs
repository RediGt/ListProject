using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ListProject
{
    class Menu
    {
        public Menu()
        {
        }

        public List<String> menu { get; set; } = new List<string>();
        public int timesModified { get; set; } = 0;
        public DateTime dtModified { get; set; } = new DateTime();

        public void AddString()
        {
            Console.Write("Input menu string: ");
            string menuStr = Console.ReadLine();
            this.menu.Add(menuStr);
            Console.WriteLine();
            this.timesModified++;
            this.dtModified = DateTime.Now;
        }

        public void DeleteString()
        {
            bool correctInput = false;
            string menuStr;
            do
            {
                Console.Write("Input number of menu string to delete: ");
                menuStr = Console.ReadLine();

                for (int i = 0; i < this.menu.Count; i++)
                {
                    if (menuStr == Convert.ToString(i + 1))
                        correctInput = true;
                }
            }
            while (!correctInput);

            this.menu.RemoveAt(Convert.ToInt32(menuStr) - 1);
            this.timesModified++;
            this.dtModified = DateTime.Now;
        }

        public static string GetMenuFile()
        {
            string filename = Directory.GetCurrentDirectory();
            filename += @"\Menu.json";

            return filename;
        }

        public static void SaveToFile(Menu userMenu)
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

        public static Menu LoadFromFile()
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
                Console.WriteLine("Error loading the menu. A new menu is created.");              
                return CreateNewMenu();
            }
        }

        public static Menu CreateNewMenu()
        {
            Menu userMenu = new Menu();
            userMenu.menu.Add("New");
            userMenu.menu.Add("Exit");
            userMenu.timesModified = 0;
            userMenu.dtModified = DateTime.Now;
            return userMenu;
        }

        public void PrintMenu()
        {
            Console.WriteLine("MENU status:");
            if (this.timesModified == 0)
                Console.WriteLine("Created: {0}\n", this.dtModified);
            else
                Console.WriteLine("Times modified: {0}, Last modified: {1}\n", this.timesModified, this.dtModified);

            Console.WriteLine("Program MENU:");
            for (int i = 0; i < this.menu.Count; i++)
            {
                Console.Write("   - " + (i + 1) + ". ");
                Console.WriteLine(this.menu[i]);
            }
        }
    }
}
