using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ViewModel;

namespace ServiceModel
{
    public class LoadPictures
    {
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
