using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml;
using Model;
using ServiceModel;
using ViewModel;

namespace Host
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ServiceHost service = new ServiceHost(typeof(BaseServiceModel));
            service.Open();
            //LoadExercises();
        }

        public void LoadExercises()
        {
            string xmlPath = @"C:\Users\gold\Documents\Yakov - master\YakovDataLayer\ViewModel\AddExercisesXML.xml";
            ExerciseList exerciseList = new ExerciseList(); 
            ExerciseDB exDB = new ExerciseDB(); 
            try
            {
                XmlTextReader reader = new XmlTextReader(xmlPath);
                reader.WhitespaceHandling = WhitespaceHandling.None;

                Exercise ex = null;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "Exercise":
                                ex = new Exercise(); //יצירת עצם חדש
                                break;
                            case "ExerciseName":
                                ex.ExerciseName = reader.ReadString();
                                break;
                            case "Difficulty":
                                ex.Difficulty = int.Parse(reader.ReadString());
                                break;
                            case "IsCompound":
                                ex.IsCompound = bool.Parse(reader.ReadString());
                                break;
                            case "TargetMuscle":
                                ex.TargetMuscle = reader.ReadString();
                                break;
                            case "Type":
                                ex.Type = reader.ReadString();
                                break;
                            case "ExerciseUrl":
                                ex.ExerciseUrl = reader.ReadString();
                                break;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.Name == "Exercise") //סיום הגדרות של משחק
                            exerciseList.Add(ex); //הוספת המשחק לרשימה                      
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            foreach (Exercise ex in exerciseList)
                if (!exDB.IsExist(ex.ExerciseName))
                {
                    exDB.InsertExercises(ex);
                }

        }
    }
}
