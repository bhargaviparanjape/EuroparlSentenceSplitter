using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SentenceSplitter
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[]  nobreak = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "Adj", "Adm", "Adv", "Asst", "Bart", "Bldg", "Brig", "Bros", "Capt", "Cmdr", "Col", "Comdr", "Con", "Corp", "Cpl", "DR", "Dr", "Drs", "Ens", "Gen", "Gov", "Hon", "Hr", "Hosp", "Insp", "Lt", "MM", "MR", "MRS", "MS", "Maj", "Messrs", "Mlle", "Mme", "Mr", "Mrs", "Ms", "Msgr", "Op", "Ord", "Pfc", "Ph", "Prof", "Pvt", "Rep", "Reps", "Res", "Rev", "Rt", "Sen", "Sens", "Sfc", "Sgt", "Sr", "St", "Supt", "Surg", "v", "vs", "i.e", "rev", "e.g", "No #", "Nos", "Art #", "Nr", "pp #" };
            string[] optbreak = {"No", "Art", "pp"};
            string test = "changed history.\" SIMONE DE BEAUVOIR.";

            //cleanup
            string result = Regex.Replace(test, " +", " ");
            result = Regex.Replace(result, " \n|\n ", "\n");
            result = Regex.Replace(result, "^ | $", "");
            result = Regex.Replace(result, "([?!]) +([\'\"([¿¡`]*[A-Z])", "$1\n$2");
            result = Regex.Replace(result, "([.][.]+) +([\'\"([¿¡`]*[A-Z])", "$1\n$2"); 
            result = Regex.Replace(result, "([?!.][ ]*[\'\"]+) +([\'\"([¿¡`]*[ ]*[A-Z])", "$1\n$2");
            result = Regex.Replace(result, "([?!.]) +([\'\"([¿¡`]+[ ]*[A-Z])", "$1\n$2");
            string[] words = result.Split(' '); //by all white characters
            string final = "";
            int i = 0;
            for(i=0; i < words.Length - 1; i++)
            {
                var match = Regex.Match(words[i], "([A-Za-z0-9-]*)([\'\"\\u005d\\u0029\\u0025]*)([.]+)$"); //full match will result in 4 group matches only since the last group is a essential
                if (match.Groups.Count == 4  && nobreak.Contains(match.Groups[1].Value) && match.Groups[2].Value == ""); //there should be a honorific but no starting punctuation but a . in the end
                else
                {
                    var match1 = Regex.Match(words[i], "([.])([A-Z-]+)([.]+)$"); //abbreaviations of the form .A-B.G.
                    if (match1.Groups.Count > 1); //multiple matched, no possibiity of 1, 2 matches
                    else
                    {
                        var match2 = Regex.Match(words[i + 1], "^([ ]*[\'\"`([¿¡]*[ ]*[A-Z0-9])");  //single match
                        var match3 = Regex.Match(words[i + 1], "^[0-9]+"); //single match
                        if (match.Groups.Count == 4 && optbreak.Contains(match.Groups[1].Value) && match.Groups[2].Value == "" && match3.Length > 0) ;
                        else if (match.Groups.Count == 4 && match2.Length > 0) { words[i] = words[i] + "\n"; Console.WriteLine("ENTERED"); }
                    }
                }
                final = final + words[i] + " ";
            }
            final = final + words[i];


            //again clean up
            final = Regex.Replace(final, " +", " ");
            final = Regex.Replace(final, " \n|\n ", "\n");
            final = Regex.Replace(final, "^ | $", "");
            Console.WriteLine(final);
            Console.ReadKey();
            //split and return

        }
    }
}
