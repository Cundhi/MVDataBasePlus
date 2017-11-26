using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace MVDataBasePlus
{
    /// <summary>
    /// Database__.xaml 的交互逻辑
    /// </summary>
    public partial class Database__ : Window
    {
        public Database__()
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if(fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileManager.path = fbd.SelectedPath + "\\";
                FileManager.Initialize();
                InitializeComponent();
            }
            else
            {
                FileManager.Initialize();
                InitializeComponent();
            }
        }

        private void buttonAdvancedMode_Click(object sender, RoutedEventArgs e)
        {
            FileManager.choiceItemInfo.file = (this.tabControl.SelectedValue as TabItem).Header.ToString();
            AdvancedMode advancdeMode = new AdvancedMode();
            advancdeMode.ShowDialog();
        }

        private void buttonApply_Click(object sender, RoutedEventArgs e)
        {
            FileManager.SaveJsonFiles();
        }

        private void buttonCanael_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            FileManager.SaveJsonFiles();
            this.Close();
        }
    }
}
