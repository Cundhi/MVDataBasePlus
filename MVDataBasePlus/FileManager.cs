using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVDataBasePlus
{
    static class FileManager
    {
        public static string path = @"C:\Users\dave0\Documents\Games\TestRPGMakerMV\data\";
        public static Dictionary<string, JArray> jsonDictionary = new Dictionary<string, JArray>();
        public static string[] files = new string[] { @"Actors", @"Animations", @"Armors", @"Classes", @"CommonEvents", @"Enemies", @"Items", @"Skills", @"States", @"System", @"Tilesets", @"Troops", @"Weapons" };
        
        public static string ChoiceFile { get; set; }
        public static ChoiceItem choiceItemInfo = new ChoiceItem();


        public static void Initialize()
        {
            choiceItemInfo = new ChoiceItem();
            LoadJsonFiles();
        }

        public static void LoadJsonFiles()
        {
            foreach (string file in files)
            {
                jsonDictionary.Add(file, ReadJson(path + file + @".json"));
            }
        }
        public static void SaveJsonFiles()
        {
            var we = jsonDictionary["Weapons"];
            foreach(var jst in jsonDictionary)
            {
                WriteJson(path + jst.Key + @".json");
            }
        }

        public static JArray ReadJson(string file)
        {
            string str = File.ReadAllText(file);
            JArray jsonObj;
            if (str[0]=='[')
            {
                jsonObj = JArray.Parse(str);
            }
            else
            {
                jsonObj = JArray.Parse(@"["+str+@"]");
            }
            
            return jsonObj;
        }

        public static string WriteJson(string file)
        {
            string str = jsonDictionary[file.Replace(path,@"").Replace(@".json",@"")].ToString();
            File.WriteAllText(file, str);

            return str;
        }

    }
    public class ChoiceItem
    {
        public JToken jolist { get; set; }
        public string file { get; set; }
        public int id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
