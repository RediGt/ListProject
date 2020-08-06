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
            //LoadMenu();
        }

        public List<String> menu { get; set; } = new List<string>();
        public int timesModified { get; set; } = 0;
        public DateTime dtModified { get; set; } = new DateTime();

        public void LoadMenu()
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

                this.menu = JsonConvert.DeserializeObject<List<string>>(json);
               /* var options = new JsonSerializerOptions
                {
                    IgnoreNullValues = true
                };
                Menu newMenu = new Menu();
                newMenu = System.Text.Json.JsonSerializer.Deserialize<Menu>(jsonString, options);*/
            }
            catch
            {
                InitMenu();
            }
        }

        public void SaveMenu()
        {
            String json = JsonConvert.SerializeObject(this.menu, Newtonsoft.Json.Formatting.Indented);

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

        public void InitMenu()
        {
            this.menu.Add("New");
            this.menu.Add("Exit");
        }

        public static string GetMenuFile()
        {
            string filename = Directory.GetCurrentDirectory();
            filename += @"\Menu1.json";

            return filename;
        }

        public void PrintMenu()
        {
            Console.WriteLine("MENU status:");
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
