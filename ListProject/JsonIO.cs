using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ListProject
{
    class JsonIO
    {
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
                return Menu.CreateNewMenu();
            }
        }
    }
}
