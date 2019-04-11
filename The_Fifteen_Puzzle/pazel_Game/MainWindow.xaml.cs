using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Xml.Linq;

namespace pazel_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
            SetLanguageDictionary();
        }
        private void SetLanguageDictionary()
        {
            //todo
            //string ProjectPath = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory.ToString()).FullName).FullName;//path of xml files
            //string traineesPath = ProjectPath + "\\Resources\\StringResources.en-US.xaml";

            //ResourceDictionary dict = new ResourceDictionary();
            //switch (Thread.CurrentThread.CurrentCulture.ToString())
            //{
            //    case "he-IL":
            //        dict.Source = new Uri("C:\\Users\\Avraham_2\\source\\repos\\pazel_Game\\pazel_Game\\Resources\\‏‏StringResources.en-US.xaml");
            //        //dict.Source = new Uri("C:/Users/Avraham_2/source/repos/pazel_Game/Resources" + "/StringResources.he-IL.xaml");
            //        //dict.Source = new Uri(ProjectPath + "/Resources/StringResources.he-IL.xaml" );
            //        break;
            //    default:
            //        dict.Source = new Uri("C:/Users/Avraham_2/source/repos/pazel_Game/Resources" + "/StringResources.he-IL.xaml");
            //        //dict.Source = new Uri(ProjectPath + "/Resources/StringResources.en-US.xaml",
            //        //UriKind.Relative);
            //        break;
            //}
            //this.Resources.MergedDictionaries.Add(dict);
        }

        private void StartPlayButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var checkedButton = RadioButtonsGrid.Children.OfType<RadioButton>()
                                      .FirstOrDefault(r => (bool)r.IsChecked);
            Difficulty difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), checkedButton.Content.ToString(), true);
            new Game((int)this.SizeNumericUpDown.Value, difficulty).ShowDialog();
        }

        private void StartPlayViewbox_MouseEnter(object sender, MouseEventArgs e)
        {
            (e.Source as System.Windows.Shapes.Path).Fill = Brushes.Brown;
        }

        private void Path_MouseLeave(object sender, MouseEventArgs e)
        {
            (e.Source as System.Windows.Shapes.Path).Fill = Brushes.Black;
        }
    }
}
