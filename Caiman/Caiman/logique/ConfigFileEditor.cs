using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Caiman.logique
{
    public class ConfigFileEditor
    {
        string path;
        string filename;



        public ConfigFileEditor(string pathp, string filenamep)
        {
            path = pathp;
            filename = filenamep;
        }
        /// <summary>
        /// retourne le chemin complet du fichier
        /// </summary>
        public string fullPath { get => (path + filename); }

        /// <summary>
        /// Permet de lire une propriété d'un fichier de config .ini
        /// </summary>
        /// <param name="cherchValue"></param>
        /// <returns></returns>
        public string ReadProperties(string properties)
        {
            string tempValue = "";
            //pattern de recherches
            string patternRecherche = @"^" + properties + @"";
            string patternExlusion = @"^" + properties + @"[ ]*=[ ]*(.*)";

            Regex rgRecherche = new Regex(patternRecherche);
            Regex rgvalue = new Regex(patternExlusion);
            MatchCollection matchedValue;
            Match valeursRetourRegex;


            using (StreamReader sr = File.OpenText(fullPath))
            {
                string words;
                while ((words = sr.ReadLine()) != null)
                {

                    if (rgRecherche.IsMatch(words))
                    {
                        //si la recherche a aboutit retourne la valeur du la propriété
                        matchedValue = rgvalue.Matches(words);
                        valeursRetourRegex = rgvalue.Match(words);
                        tempValue = valeursRetourRegex.Groups[1].ToString();
                    }
                }
            }


            return tempValue;
        }

        /// <summary>
        /// permet de métre a jour une propriété 
        /// </summary>
        public void UpdateProperties(string properties, string updateValue)
        {
            string fileUpdate = "";
            string patternRecherche = @"^" + properties + @"";

            Regex rgRecherche = new Regex(patternRecherche);

            using (StreamReader sr = File.OpenText(fullPath))
            {
                string words;
                while ((words = sr.ReadLine()) != null)
                {
                    if (words != "")
                    {
                        if (rgRecherche.IsMatch(words))
                        {
                            fileUpdate += properties + " = " + updateValue;
                        }
                        else
                        {
                            fileUpdate += words;
                        }
                        fileUpdate += "\r\n";
                    }
                }
            }
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, filename)))
            {
                outputFile.WriteLine(fileUpdate);
            }

        }

        /// <summary>
        /// Add value in file
        /// </summary>
        public void AddValue(string value)
        {
            string appendText = value + Environment.NewLine;
            File.AppendAllText(fullPath, appendText);
        }

        /// <summary>
        /// Remove line in file
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public void DeleteValue(string properties)
        {
            string fileUpdate = "";
            string patternRecherche = @"^" + properties + @"";

            Regex rgRecherche = new Regex(patternRecherche);

            using (StreamReader sr = File.OpenText(fullPath))
            {
                string words;
                while ((words = sr.ReadLine()) != null)
                {
                    if (words != "")
                    {
                        if (rgRecherche.IsMatch(words))
                        {
                            fileUpdate += "";
                        }
                        else
                        {
                            fileUpdate += words;
                            fileUpdate += "\r\n";
                        }
                       
                    }
                }
            }
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, filename)))
            {
                outputFile.WriteLine(fileUpdate);
            }
          
        }

        public List<string> GetAllValueInList()
        {
            string tempValue = "";
            List<string> lst_value = new List<string>();



            using (StreamReader sr = File.OpenText(fullPath))
            {
                string words;
                while ((words = sr.ReadLine()) != null)
                {
                    if (words != "")
                    {

                        lst_value.Add(words);
                    }
                    
                }
            }


            return lst_value;
        }

    }
}
