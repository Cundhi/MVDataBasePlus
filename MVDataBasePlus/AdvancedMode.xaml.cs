using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace MVDataBasePlus
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AdvancedMode : Window
    {
        /*
        private List<ItemArr> itemArrs = new List<ItemArr>();

        public List<ItemArr> ItemArrs
        {
            get
            {
                return itemArrs;
            }

            set
            {
                itemArrs = value;
            }
        }
        */
        public AdvancedMode()
        {
            InitializeComponent();

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonApply_Click(object sender, RoutedEventArgs e)
        {
            ChoiceItem ci = FileManager.choiceItemInfo;
            ci.Name = this.comboBox.Text;
            ci.Value = this.textBox.Text;
            FileManager.jsonDictionary[ci.file][ci.id][ci.Name] = ci.Value;
            FileManager.SaveJsonFiles();
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            ChoiceItem ci = FileManager.choiceItemInfo;
            ci.Name = this.comboBox.Text;
            ci.Value = this.textBox.Text;
            FileManager.jsonDictionary[ci.file][ci.id][ci.Name] = ci.Value;
            FileManager.SaveJsonFiles();
            this.Close();
        }

        private void SelectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }


        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ChoiceItem ci = FileManager.choiceItemInfo;
            ci.Name = this.comboBox.Text;
            ci.Value = this.textBox.Text;
        }

        private void SelectComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ChoiceItem ci = FileManager.choiceItemInfo;
            ci.id = Convert.ToInt32(this.SelectComboBox.Text==""? "0":this.SelectComboBox.Text);
            ci.jolist = FileManager.jsonDictionary[FileManager.choiceItemInfo.file][ci.id];
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ChoiceItem ci = FileManager.choiceItemInfo;
        }

        private void buttonAddAttribute_Click(object sender, RoutedEventArgs e)
        {
            ChoiceItem ci = FileManager.choiceItemInfo;
            ci.Name = this.textBoxAddAttribute.Text;
            ci.Value = "";
            for(int i=1;i< FileManager.jsonDictionary[ci.file].Count;i++)
            {
                FileManager.jsonDictionary[ci.file][i][ci.Name] = ci.Value;
            }
            FileManager.SaveJsonFiles();
        }

        private void buttonDeleteAttribute_Click(object sender, RoutedEventArgs e)
        {
            ChoiceItem ci = FileManager.choiceItemInfo;
            ci.Name = this.comboBox.Text;
            ci.Value = this.textBox.Text;
            for (int i = 1; i < FileManager.jsonDictionary[ci.file].Count; i++)
            {
                JProperty id = FileManager.jsonDictionary[ci.file][i].Children<JProperty>().FirstOrDefault(p => p.Name ==ci.Name);
                if (id != null)
                    id.Remove();

            }
            FileManager.SaveJsonFiles();
        }
    }


    public class ItemArr : ObservableCollection<JProperty>
    {
        public int id { get; set; }
        //public JToken jss { get; set; }
        //public ItemArr items { get; set; }
        //public JObject jo { get; set; }
        public ItemArr(int i)
        {
            //items = this;
            id = i; 
            //jo = (JObject)FileManager.jsonDictionary[@"Weapons"][i];
            foreach (JProperty jp in FileManager.jsonDictionary[FileManager.choiceItemInfo.file][id])
            {
                this.Add(jp);

            }
            
        }
    }

    public class ItemArrs : ObservableCollection<ItemArr>
    {
        public ItemArrs()
        {
            for(int i=0;i< FileManager.jsonDictionary[FileManager.choiceItemInfo.file].Count;i++)
            {
                if(FileManager.jsonDictionary[FileManager.choiceItemInfo.file][i].ToString()=="")
                {
                    continue;
                }
                this.Add(new ItemArr(i));

            }

        }
    }

    public class JPropertyValueToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value==null)
            {
                return null;
            }
            JToken jtvalue = value as JToken;
            
            return jtvalue.ToString();

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ChoiceItem ci = FileManager.choiceItemInfo;
            string jtvalue = "{\"" + ci.Name + "\":" + "\""+value.ToString() + "\"}";
            JObject jo = JObject.Parse(jtvalue);
            return (JToken)jo;
            throw new NotImplementedException();
        }
    }

}
