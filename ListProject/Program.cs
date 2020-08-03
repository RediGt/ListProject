using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace ListProject
{
    class Program
    {
        static void Main(string[] args)
        {
            int timesModified = 0;
            DateTime dtModified = new DateTime();
            List<String> menu = new List<String>();
            string userChoice = null;

            LoadMenu(menu, ref timesModified, ref dtModified);
            //InitMenu(menu);
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

        static void InitMenu(List<string> menu)
        {
            menu.Add("New");
            menu.Add("Exit");
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

        /*       static void SaveMenu(List<string> menu, ref int timesModified, ref DateTime dtModified)
               {
                   XmlWriterSettings settings = new XmlWriterSettings();
                   settings.Indent = true;

                   XmlWriter xmlOut = XmlWriter.Create(GetMenuFile(), settings);

                   xmlOut.WriteStartDocument();
                   xmlOut.WriteComment("File for storage content menu.");

                   xmlOut.WriteStartElement("Menu");

                   xmlOut.WriteAttributeString("Times_Modified", Convert.ToString(timesModified));
                   xmlOut.WriteAttributeString("Last_Modified", dtModified.ToString());

                   foreach (var item in menu)
                   {
                       xmlOut.WriteStartElement("MenuItem");
                       xmlOut.WriteString(item);
                       xmlOut.WriteEndElement();
                   }

                   xmlOut.WriteEndElement();
                   xmlOut.WriteEndDocument();
                   xmlOut.Flush();
                   xmlOut.Close();
               }

               static void LoadMenu(List<string> menu, ref int timesModified, ref DateTime dtModified)
               {
                   bool firstEncounter = true;

                   try
                   {
                       XmlReader xmlIn = XmlReader.Create(GetMenuFile());
                       while (xmlIn.Read())
                       {
                           if (xmlIn.LocalName.Equals("Menu") && firstEncounter)
                           {
                               string attribut;
                               attribut = xmlIn.GetAttribute("Times_Modified");
                               Console.WriteLine("Times modified: {0}", attribut);
                               timesModified = Convert.ToInt32(attribut);

                               attribut = xmlIn.GetAttribute("Last_Modified");
                               Console.WriteLine("Last modified: {0}", attribut);
                               dtModified = Convert.ToDateTime(attribut);

                               Console.WriteLine();
                               firstEncounter = false;
                           }

                           if (xmlIn.LocalName.Equals("MenuItem"))
                           {
                               xmlIn.Read();
                               menu.Add(xmlIn.Value);
                               xmlIn.Read();
                           }
                       }
                       xmlIn.Close();
                   }
                   catch
                   {
                       InitMenu(menu);
                   }
               }*/
       
        static void SaveMenu(List<string> menu, ref int timesModified, ref DateTime dtModified)
        {
            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(GetMenuFile()))
            {
                JsonSerializer serializer = new JsonSerializer();
                foreach (var item in menu)
                {
                    //JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, item);
                }
            }           
        }

        static void LoadMenu(List<string> menu, ref int timesModified, ref DateTime dtModified)
        {
            bool firstEncounter = true;

            try
            {
                // deserialize JSON directly from a file
                using (StreamReader file = File.OpenText(GetMenuFile()))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    int i = 0;
                    foreach (var item in menu)
                    {
                        //JsonSerializer serializer = new JsonSerializer();
                        menu[i] = (List<String>)serializer.Deserialize(file, typeof(menu));
                        i++;
                    }
                    //Movie movie2 = (Movie)serializer.Deserialize(file, typeof(Movie));
                }
            catch
            {
                InitMenu(menu);
            }
        }

        public static string GetMenuFile()
        {
            string filename = Directory.GetCurrentDirectory();
            filename += @"\Menu.xsd";

            return filename;
        }
    }
}
